using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Xml;
using Bannerlord.BUTR.Shared.Extensions;
using Bannerlord.ModuleManager;
using HarmonyLib;
using HarmonyLib.BUTR.Extensions;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.ModuleManager;
using TaleWorlds.MountAndBlade;

namespace Bannerlord.BUTR.Shared.Helpers
{
	// Token: 0x02000042 RID: 66
	[NullableContext(1)]
	[Nullable(0)]
	[ExcludeFromCodeCoverage]
	[DebuggerNonUserCode]
	internal static class ModuleInfoHelper
	{
		// Token: 0x06000284 RID: 644 RVA: 0x0000A445 File Offset: 0x00008645
		static ModuleInfoHelper()
		{
			ModuleInfoHelper._platformModuleExtensionField = AccessTools2.StaticFieldRefAccess<IPlatformModuleExtension>("TaleWorlds.ModuleManager.ModuleHelper:_platformModuleExtension", true);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000A480 File Offset: 0x00008680
		[return: Nullable(2)]
		public static ModuleInfoExtendedWithMetadata LoadFromId(string id)
		{
			return ModuleInfoHelper.GetModules().FirstOrDefault((ModuleInfoExtendedWithMetadata x) => x.Id == id);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000A4B0 File Offset: 0x000086B0
		public static IEnumerable<ModuleInfoExtendedWithMetadata> GetLoadedModules()
		{
			string[] moduleNames = Utilities.GetModulesNames();
			bool flag = moduleNames.Length == 0;
			if (flag)
			{
				yield break;
			}
			Dictionary<string, ModuleInfoExtendedWithMetadata> allModulesAvailable = ModuleInfoHelper.GetModules().ToDictionary((ModuleInfoExtendedWithMetadata x) => x.Id, (ModuleInfoExtendedWithMetadata x) => x);
			foreach (string modulesId in moduleNames)
			{
				ModuleInfoExtendedWithMetadata moduleInfo;
				bool flag2 = allModulesAvailable.TryGetValue(modulesId, out moduleInfo);
				if (flag2)
				{
					yield return moduleInfo;
				}
				moduleInfo = null;
				modulesId = null;
			}
			string[] array = null;
			yield break;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000A4B9 File Offset: 0x000086B9
		public static IEnumerable<ModuleInfoExtendedWithMetadata> GetModules()
		{
			return ModuleInfoHelper._cachedModules.Value;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000A4C8 File Offset: 0x000086C8
		[NullableContext(2)]
		public static string GetModulePath(Type type)
		{
			bool flag = type == null;
			string result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = string.IsNullOrWhiteSpace(type.Assembly.Location);
				if (flag2)
				{
					result = null;
				}
				else
				{
					string modulePath;
					bool flag3 = ModuleInfoHelper._cachedAssemblyLocationToModulePath.TryGetValue(type.Assembly.Location, out modulePath);
					if (flag3)
					{
						result = modulePath;
					}
					else
					{
						FileInfo assemblyFile = new FileInfo(System.IO.Path.GetFullPath(type.Assembly.Location));
						DirectoryInfo directoryInfo = ModuleInfoHelper.<GetModulePath>g__GetMainDirectory|9_0(assemblyFile.Directory);
						modulePath = (result = ((directoryInfo != null) ? directoryInfo.FullName : null));
					}
				}
			}
			return result;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000A554 File Offset: 0x00008754
		[return: Nullable(2)]
		public static string GetModulePath(ModuleInfoExtended module)
		{
			ModuleInfoExtendedWithMetadata moduleInfoExtendedWithMetadata = ModuleInfoHelper._cachedModules.Value.FirstOrDefault((ModuleInfoExtendedWithMetadata x) => x.Id == module.Id);
			return (moduleInfoExtendedWithMetadata != null) ? moduleInfoExtendedWithMetadata.Path : null;
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000A598 File Offset: 0x00008798
		[NullableContext(2)]
		public static ModuleInfoExtendedWithMetadata GetModuleByType(Type type)
		{
			string modulePath = ModuleInfoHelper.GetModulePath(type);
			return ModuleInfoHelper._cachedModules.Value.FirstOrDefault((ModuleInfoExtendedWithMetadata x) => x.Path == modulePath);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000A5D7 File Offset: 0x000087D7
		private static string GetFullPathWithEndingSlashes(string input)
		{
			return string.Format("{0}{1}", System.IO.Path.GetFullPath(input).TrimEnd(new char[]
			{
				System.IO.Path.DirectorySeparatorChar,
				System.IO.Path.AltDirectorySeparatorChar
			}), System.IO.Path.DirectorySeparatorChar);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000A60E File Offset: 0x0000880E
		public static IEnumerable<ModuleInfoExtendedWithMetadata> GetPhysicalModules()
		{
			bool flag = string.IsNullOrEmpty(BasePath.Name);
			if (flag)
			{
				yield break;
			}
			foreach (string modulePath in Directory.GetDirectories(System.IO.Path.Combine(BasePath.Name, "Modules")))
			{
				XmlDocument xml;
				ModuleInfoExtended moduleInfo;
				bool flag2;
				if (ModuleInfoHelper.TryReadXml(System.IO.Path.Combine(modulePath, "SubModule.xml"), out xml))
				{
					moduleInfo = ModuleInfoExtended.FromXml(xml);
					flag2 = (moduleInfo != null);
				}
				else
				{
					flag2 = false;
				}
				bool flag3 = flag2;
				if (flag3)
				{
					yield return new ModuleInfoExtendedWithMetadata(moduleInfo, false, System.IO.Path.GetFullPath(modulePath));
				}
				xml = null;
				moduleInfo = null;
				modulePath = null;
			}
			string[] array = null;
			yield break;
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000A617 File Offset: 0x00008817
		public unsafe static IEnumerable<ModuleInfoExtendedWithMetadata> GetPlatformModules()
		{
			bool flag = ModuleInfoHelper._platformModuleExtensionField == null;
			if (flag)
			{
				yield break;
			}
			IPlatformModuleExtension platformModuleExtension = *ModuleInfoHelper._platformModuleExtensionField();
			bool flag2 = platformModuleExtension == null;
			if (flag2)
			{
				yield break;
			}
			string[] modulePaths = platformModuleExtension.GetModulePaths();
			bool flag3 = modulePaths == null;
			if (flag3)
			{
				yield break;
			}
			foreach (string modulePath in modulePaths)
			{
				XmlDocument xml;
				ModuleInfoExtended moduleInfo;
				bool flag4;
				if (ModuleInfoHelper.TryReadXml(System.IO.Path.Combine(modulePath, "SubModule.xml"), out xml))
				{
					moduleInfo = ModuleInfoExtended.FromXml(xml);
					flag4 = (moduleInfo != null);
				}
				else
				{
					flag4 = false;
				}
				bool flag5 = flag4;
				if (flag5)
				{
					yield return new ModuleInfoExtendedWithMetadata(moduleInfo, true, System.IO.Path.GetFullPath(modulePath));
				}
				xml = null;
				moduleInfo = null;
				modulePath = null;
			}
			string[] array = null;
			yield break;
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000A620 File Offset: 0x00008820
		public static bool CheckIfSubModuleCanBeLoaded(SubModuleInfoExtended subModuleInfo)
		{
			return ModuleInfoHelper.CheckIfSubModuleCanBeLoaded(subModuleInfo, ApplicationPlatform.CurrentPlatform, ApplicationPlatform.CurrentRuntimeLibrary, TaleWorlds.MountAndBlade.Module.CurrentModule.StartupInfo.DedicatedServerType, TaleWorlds.MountAndBlade.Module.CurrentModule.StartupInfo.PlayerHostedDedicatedServer);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000A650 File Offset: 0x00008850
		public static bool CheckIfSubModuleCanBeLoaded(SubModuleInfoExtended subModuleInfo, Platform cPlatform, Runtime cRuntime, DedicatedServerType cServerType, bool playerHostedDedicatedServer)
		{
			bool flag = subModuleInfo.Tags.Count <= 0;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				foreach (KeyValuePair<string, IReadOnlyList<string>> tuple in subModuleInfo.Tags)
				{
					string text;
					IReadOnlyList<string> readOnlyList;
					tuple.Deconstruct(out text, out readOnlyList);
					string key = text;
					IReadOnlyList<string> values = readOnlyList;
					SubModuleTags tag;
					bool flag2 = !Enum.TryParse<SubModuleTags>(key, out tag);
					if (!flag2)
					{
						bool flag3 = values.Any((string value) => !ModuleInfoHelper.GetSubModuleTagValiditiy(tag, value, cPlatform, cRuntime, cServerType, playerHostedDedicatedServer));
						if (flag3)
						{
							return false;
						}
					}
				}
				result = true;
			}
			return result;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000A738 File Offset: 0x00008938
		public static bool GetSubModuleTagValiditiy(SubModuleTags tag, string value)
		{
			return ModuleInfoHelper.GetSubModuleTagValiditiy(tag, value, ApplicationPlatform.CurrentPlatform, ApplicationPlatform.CurrentRuntimeLibrary, TaleWorlds.MountAndBlade.Module.CurrentModule.StartupInfo.DedicatedServerType, TaleWorlds.MountAndBlade.Module.CurrentModule.StartupInfo.PlayerHostedDedicatedServer);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000A76C File Offset: 0x0000896C
		public static bool GetSubModuleTagValiditiy(SubModuleTags tag, string value, Platform cPlatform, Runtime cRuntime, DedicatedServerType cServerType, bool playerHostedDedicatedServer)
		{
			if (!true)
			{
			}
			bool result;
			switch (tag)
			{
			case SubModuleTags.RejectedPlatform:
			{
				Platform platform;
				result = (!Enum.TryParse<Platform>(value, out platform) || cPlatform != platform);
				break;
			}
			case SubModuleTags.ExclusivePlatform:
			{
				Platform platform2;
				result = (!Enum.TryParse<Platform>(value, out platform2) || cPlatform == platform2);
				break;
			}
			case SubModuleTags.DedicatedServerType:
			{
				string a = value.ToLower();
				if (!true)
				{
				}
				bool flag;
				if (!(a == "none"))
				{
					if (!(a == "both"))
					{
						if (!(a == "custom"))
						{
							flag = (a == "matchmaker" && cServerType == DedicatedServerType.Matchmaker);
						}
						else
						{
							flag = (cServerType == DedicatedServerType.Custom);
						}
					}
					else
					{
						flag = (cServerType == DedicatedServerType.None);
					}
				}
				else
				{
					flag = (cServerType == DedicatedServerType.None);
				}
				if (!true)
				{
				}
				result = flag;
				break;
			}
			case SubModuleTags.IsNoRenderModeElement:
				result = value.Equals("false");
				break;
			case SubModuleTags.DependantRuntimeLibrary:
			{
				Runtime runtime;
				result = (!Enum.TryParse<Runtime>(value, out runtime) || cRuntime == runtime);
				break;
			}
			case SubModuleTags.PlayerHostedDedicatedServer:
				result = (playerHostedDedicatedServer && value.Equals("true"));
				break;
			default:
				result = true;
				break;
			}
			if (!true)
			{
			}
			return result;
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000A890 File Offset: 0x00008A90
		public static bool ValidateLoadOrder(Type subModuleType, out string report)
		{
			return ModuleInfoHelper.ValidateLoadOrder(ModuleInfoHelper.GetModuleByType(subModuleType), out report);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000A8B0 File Offset: 0x00008AB0
		public static bool ValidateLoadOrder([Nullable(2)] ModuleInfoExtended moduleInfo, out string report)
		{
			ModuleInfoHelper.<>c__DisplayClass20_0 CS$<>8__locals1;
			CS$<>8__locals1.moduleInfo = moduleInfo;
			bool flag = CS$<>8__locals1.moduleInfo == null;
			bool result;
			if (flag)
			{
				report = "CRITICAL ERROR";
				result = false;
			}
			else
			{
				List<ModuleInfoExtendedWithMetadata> loadedModules = ModuleInfoHelper.GetLoadedModules().ToList<ModuleInfoExtendedWithMetadata>();
				CS$<>8__locals1.moduleIndex = ModuleInfoHelper.<ValidateLoadOrder>g__IndexOf|20_0<ModuleInfoExtended>(loadedModules, CS$<>8__locals1.moduleInfo);
				CS$<>8__locals1.sb = new StringBuilder();
				using (IEnumerator<DependentModule> enumerator = CS$<>8__locals1.moduleInfo.DependentModules.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						DependentModule dependedModule = enumerator.Current;
						ModuleInfoExtendedWithMetadata module = loadedModules.SingleOrDefault((ModuleInfoExtendedWithMetadata x) => x.Id == dependedModule.Id);
						int dependedModuleIndex = (module != null) ? loadedModules.IndexOf(module) : -1;
						ModuleInfoHelper.<ValidateLoadOrder>g__ValidateDependedModuleLoadBeforeThis|20_6(dependedModuleIndex, dependedModule.Id, false, ref CS$<>8__locals1);
					}
				}
				using (IEnumerator<DependentModuleMetadata> enumerator2 = CS$<>8__locals1.moduleInfo.DependentModuleMetadatas.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						DependentModuleMetadata dependedModule = enumerator2.Current;
						ModuleInfoExtendedWithMetadata module2 = loadedModules.SingleOrDefault((ModuleInfoExtendedWithMetadata x) => x.Id == dependedModule.Id);
						int dependedModuleIndex2 = (module2 != null) ? loadedModules.IndexOf(module2) : -1;
						bool isIncompatible = dependedModule.IsIncompatible;
						if (isIncompatible)
						{
							bool flag2 = CS$<>8__locals1.moduleInfo.DependentModules.Any((DependentModule dm) => dm.Id == dependedModule.Id);
							if (flag2)
							{
								ModuleInfoHelper.<ValidateLoadOrder>g__ReportMutuallyExclusiveDirectives|20_4(dependedModule.Id, ref CS$<>8__locals1);
							}
							else
							{
								ModuleInfoHelper.<ValidateLoadOrder>g__ValidateDependedModuleCompatibility|20_5(dependedModuleIndex2, dependedModule.Id, ref CS$<>8__locals1);
							}
						}
						else
						{
							bool flag3 = dependedModule.LoadType == LoadType.LoadBeforeThis;
							if (flag3)
							{
								bool flag4 = CS$<>8__locals1.moduleInfo.DependentModules.Any((DependentModule dm) => dm.Id == dependedModule.Id);
								if (!flag4)
								{
									ModuleInfoHelper.<ValidateLoadOrder>g__ValidateDependedModuleLoadBeforeThis|20_6(dependedModuleIndex2, dependedModule.Id, dependedModule.IsOptional, ref CS$<>8__locals1);
								}
							}
							else
							{
								bool flag5 = dependedModule.LoadType == LoadType.LoadAfterThis;
								if (flag5)
								{
									bool flag6 = CS$<>8__locals1.moduleInfo.DependentModules.Any((DependentModule dm) => dm.Id == dependedModule.Id) || CS$<>8__locals1.moduleInfo.DependentModuleMetadatas.Any((DependentModuleMetadata dm) => dm.Id == dependedModule.Id && dm.LoadType == LoadType.LoadBeforeThis);
									if (flag6)
									{
										ModuleInfoHelper.<ValidateLoadOrder>g__ReportMutuallyExclusiveDirectives|20_4(dependedModule.Id, ref CS$<>8__locals1);
									}
									else
									{
										ModuleInfoHelper.<ValidateLoadOrder>g__ValidateDependedModuleLoadAfterThis|20_7(dependedModuleIndex2, dependedModule.Id, dependedModule.IsOptional, ref CS$<>8__locals1);
									}
								}
							}
						}
					}
				}
				bool flag7 = CS$<>8__locals1.sb.Length > 0;
				if (flag7)
				{
					report = CS$<>8__locals1.sb.ToString();
					result = false;
				}
				else
				{
					report = string.Empty;
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000ABD8 File Offset: 0x00008DD8
		public static bool IsModuleAssembly(ModuleInfoExtendedWithMetadata loadedModule, Assembly assembly)
		{
			bool flag = assembly.IsDynamic || string.IsNullOrWhiteSpace(assembly.CodeBase);
			return !flag && ModuleInfoHelper.IsInModule(loadedModule, assembly.CodeBase);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000AC14 File Offset: 0x00008E14
		public static bool IsInModule(ModuleInfoExtendedWithMetadata loadedModule, string filePath)
		{
			bool flag = string.IsNullOrWhiteSpace(filePath);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				Uri modulePath = new Uri(System.IO.Path.GetFullPath(loadedModule.Path));
				string moduleDirectory = System.IO.Path.GetFileName(loadedModule.Path);
				Uri assemblyPath = new Uri(filePath);
				Uri relativePath = modulePath.MakeRelativeUri(assemblyPath);
				result = relativePath.OriginalString.StartsWith(moduleDirectory);
			}
			return result;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000AC74 File Offset: 0x00008E74
		private static bool TryReadXml(string path, [Nullable(2)] out XmlDocument xml)
		{
			bool result;
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(File.ReadAllText(path));
				xml = xmlDocument;
				result = true;
			}
			catch (Exception)
			{
				xml = null;
				result = false;
			}
			return result;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000ACB8 File Offset: 0x00008EB8
		[NullableContext(2)]
		[CompilerGenerated]
		internal static DirectoryInfo <GetModulePath>g__GetMainDirectory|9_0(DirectoryInfo directoryInfo)
		{
			while (((directoryInfo != null) ? directoryInfo.Parent : null) != null && directoryInfo.Exists)
			{
				bool flag = directoryInfo.GetFiles("SubModule.xml").Length == 1;
				if (flag)
				{
					return directoryInfo;
				}
				directoryInfo = directoryInfo.Parent;
			}
			return null;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000AD0C File Offset: 0x00008F0C
		[NullableContext(0)]
		[CompilerGenerated]
		internal static int <ValidateLoadOrder>g__IndexOf|20_0<T>([Nullable(new byte[]
		{
			1,
			0
		})] IReadOnlyList<T> self, T elementToFind)
		{
			int i = 0;
			foreach (T element in self)
			{
				bool flag = object.Equals(element, elementToFind);
				if (flag)
				{
					return i;
				}
				i++;
			}
			return -1;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000AD7C File Offset: 0x00008F7C
		[CompilerGenerated]
		internal static void <ValidateLoadOrder>g__ReportMissingModule|20_1(string requiredModuleId, ref ModuleInfoHelper.<>c__DisplayClass20_0 A_1)
		{
			bool flag = A_1.sb.Length != 0;
			if (flag)
			{
				A_1.sb.AppendLine();
			}
			StringBuilder sb = A_1.sb;
			TextObject textObject = new TextObject("{=FE6ya1gzZR}{REQUIRED_MODULE} module was not found!", null);
			string text;
			if (textObject == null)
			{
				text = null;
			}
			else
			{
				TextObject textObject2 = textObject.SetTextVariable("REQUIRED_MODULE", requiredModuleId);
				text = ((textObject2 != null) ? textObject2.ToString() : null);
			}
			sb.AppendLine(text ?? "ERROR");
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000ADE8 File Offset: 0x00008FE8
		[CompilerGenerated]
		internal static void <ValidateLoadOrder>g__ReportIncompatibleModule|20_2(string deniedModuleId, ref ModuleInfoHelper.<>c__DisplayClass20_0 A_1)
		{
			bool flag = A_1.sb.Length != 0;
			if (flag)
			{
				A_1.sb.AppendLine();
			}
			StringBuilder sb = A_1.sb;
			TextObject textObject = new TextObject("{=EvI6KPAqTT}Incompatible module {DENIED_MODULE} was found!", null);
			string text;
			if (textObject == null)
			{
				text = null;
			}
			else
			{
				TextObject textObject2 = textObject.SetTextVariable("DENIED_MODULE", deniedModuleId);
				text = ((textObject2 != null) ? textObject2.ToString() : null);
			}
			sb.AppendLine(text ?? "ERROR");
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000AE54 File Offset: 0x00009054
		[CompilerGenerated]
		internal static void <ValidateLoadOrder>g__ReportLoadingOrderIssue|20_3(string reason, string requiredModuleId, ref ModuleInfoHelper.<>c__DisplayClass20_0 A_2)
		{
			bool flag = A_2.sb.Length != 0;
			if (flag)
			{
				A_2.sb.AppendLine();
			}
			StringBuilder sb = A_2.sb;
			TextObject textObject = new TextObject(reason, null);
			string text;
			if (textObject == null)
			{
				text = null;
			}
			else
			{
				TextObject textObject2 = textObject.SetTextVariable("MODULE", A_2.moduleInfo.Id);
				if (textObject2 == null)
				{
					text = null;
				}
				else
				{
					TextObject textObject3 = textObject2.SetTextVariable("REQUIRED_MODULE", requiredModuleId);
					if (textObject3 == null)
					{
						text = null;
					}
					else
					{
						TextObject textObject4 = textObject3.SetTextVariable("NL", Environment.NewLine);
						text = ((textObject4 != null) ? textObject4.ToString() : null);
					}
				}
			}
			sb.AppendLine(text ?? "ERROR");
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000AEEC File Offset: 0x000090EC
		[CompilerGenerated]
		internal static void <ValidateLoadOrder>g__ReportMutuallyExclusiveDirectives|20_4(string requiredModuleId, ref ModuleInfoHelper.<>c__DisplayClass20_0 A_1)
		{
			bool flag = A_1.sb.Length != 0;
			if (flag)
			{
				A_1.sb.AppendLine();
			}
			StringBuilder sb = A_1.sb;
			TextObject textObject = new TextObject("{=FcR4BXnhx8}{MODULE} has mutually exclusive mod order directives specified for the {REQUIRED_MODULE}!", null);
			string text;
			if (textObject == null)
			{
				text = null;
			}
			else
			{
				TextObject textObject2 = textObject.SetTextVariable("MODULE", A_1.moduleInfo.Id);
				if (textObject2 == null)
				{
					text = null;
				}
				else
				{
					TextObject textObject3 = textObject2.SetTextVariable("REQUIRED_MODULE", requiredModuleId);
					text = ((textObject3 != null) ? textObject3.ToString() : null);
				}
			}
			sb.AppendLine(text ?? "ERROR");
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000AF74 File Offset: 0x00009174
		[CompilerGenerated]
		internal static void <ValidateLoadOrder>g__ValidateDependedModuleCompatibility|20_5(int deniedModuleIndex, string deniedModuleId, ref ModuleInfoHelper.<>c__DisplayClass20_0 A_2)
		{
			bool flag = deniedModuleIndex != -1;
			if (flag)
			{
				ModuleInfoHelper.<ValidateLoadOrder>g__ReportIncompatibleModule|20_2(deniedModuleId, ref A_2);
			}
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000AF98 File Offset: 0x00009198
		[CompilerGenerated]
		internal static void <ValidateLoadOrder>g__ValidateDependedModuleLoadBeforeThis|20_6(int requiredModuleIndex, string requiredModuleId, bool isOptional = false, ref ModuleInfoHelper.<>c__DisplayClass20_0 A_3)
		{
			bool flag = !isOptional && requiredModuleIndex == -1;
			if (flag)
			{
				ModuleInfoHelper.<ValidateLoadOrder>g__ReportMissingModule|20_1(requiredModuleId, ref A_3);
			}
			else
			{
				bool flag2 = requiredModuleIndex > A_3.moduleIndex;
				if (flag2)
				{
					ModuleInfoHelper.<ValidateLoadOrder>g__ReportLoadingOrderIssue|20_3("{=5G9zffrgMh}{MODULE} is loaded before the {REQUIRED_MODULE}!{NL}Make sure {MODULE} is loaded after it!", requiredModuleId, ref A_3);
				}
			}
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000AFDC File Offset: 0x000091DC
		[CompilerGenerated]
		internal static void <ValidateLoadOrder>g__ValidateDependedModuleLoadAfterThis|20_7(int requiredModuleIndex, string requiredModuleId, bool isOptional, ref ModuleInfoHelper.<>c__DisplayClass20_0 A_3)
		{
			bool flag = requiredModuleIndex == -1;
			if (flag)
			{
				bool flag2 = !isOptional;
				if (flag2)
				{
					ModuleInfoHelper.<ValidateLoadOrder>g__ReportMissingModule|20_1(requiredModuleId, ref A_3);
				}
			}
			else
			{
				bool flag3 = requiredModuleIndex < A_3.moduleIndex;
				if (flag3)
				{
					ModuleInfoHelper.<ValidateLoadOrder>g__ReportLoadingOrderIssue|20_3("{=UZ8zfvudMs}{MODULE} is loaded after the {REQUIRED_MODULE}!{NL}Make sure {MODULE} is loaded before it!", requiredModuleId, ref A_3);
				}
			}
		}

		// Token: 0x040000A6 RID: 166
		public const string ModulesFolder = "Modules";

		// Token: 0x040000A7 RID: 167
		public const string SubModuleFile = "SubModule.xml";

		// Token: 0x040000A8 RID: 168
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private static readonly AccessTools.FieldRef<IPlatformModuleExtension> _platformModuleExtensionField;

		// Token: 0x040000A9 RID: 169
		private static Lazy<List<ModuleInfoExtendedWithMetadata>> _cachedModules = new Lazy<List<ModuleInfoExtendedWithMetadata>>(delegate()
		{
			List<ModuleInfoExtendedWithMetadata> list = new List<ModuleInfoExtendedWithMetadata>();
			HashSet<string> foundIds = new HashSet<string>();
			foreach (ModuleInfoExtendedWithMetadata moduleInfo in ModuleInfoHelper.GetPhysicalModules().Concat(ModuleInfoHelper.GetPlatformModules()))
			{
				bool flag = !foundIds.Contains(moduleInfo.Id.ToLower());
				if (flag)
				{
					foundIds.Add(moduleInfo.Id.ToLower());
					list.Add(moduleInfo);
				}
			}
			return list;
		}, LazyThreadSafetyMode.ExecutionAndPublication);

		// Token: 0x040000AA RID: 170
		private static ConcurrentDictionary<string, string> _cachedAssemblyLocationToModulePath = new ConcurrentDictionary<string, string>();
	}
}
