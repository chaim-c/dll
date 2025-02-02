using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace HarmonyLib
{
	// Token: 0x02000038 RID: 56
	public class Harmony
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00009FFC File Offset: 0x000081FC
		// (set) Token: 0x06000117 RID: 279 RVA: 0x0000A004 File Offset: 0x00008204
		public string Id { get; private set; }

		// Token: 0x06000118 RID: 280 RVA: 0x0000A010 File Offset: 0x00008210
		public Harmony(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				throw new ArgumentException("id cannot be null or empty");
			}
			try
			{
				string text = Environment.GetEnvironmentVariable("HARMONY_DEBUG");
				if (text != null && text.Length > 0)
				{
					text = text.Trim();
					Harmony.DEBUG = (text == "1" || bool.Parse(text));
				}
			}
			catch
			{
			}
			if (Harmony.DEBUG)
			{
				Assembly assembly = typeof(Harmony).Assembly;
				Version version = assembly.GetName().Version;
				string text2 = assembly.Location;
				string value = Environment.Version.ToString();
				string value2 = Environment.OSVersion.Platform.ToString();
				if (string.IsNullOrEmpty(text2))
				{
					text2 = new Uri(assembly.CodeBase).LocalPath;
				}
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(57, 5);
				defaultInterpolatedStringHandler.AppendLiteral("### Harmony id=");
				defaultInterpolatedStringHandler.AppendFormatted(id);
				defaultInterpolatedStringHandler.AppendLiteral(", version=");
				defaultInterpolatedStringHandler.AppendFormatted<Version>(version);
				defaultInterpolatedStringHandler.AppendLiteral(", location=");
				defaultInterpolatedStringHandler.AppendFormatted(text2);
				defaultInterpolatedStringHandler.AppendLiteral(", env/clr=");
				defaultInterpolatedStringHandler.AppendFormatted(value);
				defaultInterpolatedStringHandler.AppendLiteral(", platform=");
				defaultInterpolatedStringHandler.AppendFormatted(value2);
				FileLog.Log(defaultInterpolatedStringHandler.ToStringAndClear());
				MethodBase outsideCaller = AccessTools.GetOutsideCaller();
				if (outsideCaller.DeclaringType != null)
				{
					Assembly assembly2 = outsideCaller.DeclaringType.Assembly;
					text2 = assembly2.Location;
					if (string.IsNullOrEmpty(text2))
					{
						text2 = new Uri(assembly2.CodeBase).LocalPath;
					}
					FileLog.Log("### Started from " + outsideCaller.FullDescription() + ", location " + text2);
					defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(7, 1);
					defaultInterpolatedStringHandler.AppendLiteral("### At ");
					defaultInterpolatedStringHandler.AppendFormatted<DateTime>(DateTime.Now, "yyyy-MM-dd hh.mm.ss");
					FileLog.Log(defaultInterpolatedStringHandler.ToStringAndClear());
				}
			}
			this.Id = id;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x0000A204 File Offset: 0x00008404
		public void PatchAll()
		{
			MethodBase method = new StackTrace().GetFrame(1).GetMethod();
			Assembly assembly = method.ReflectedType.Assembly;
			this.PatchAll(assembly);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000A235 File Offset: 0x00008435
		public PatchProcessor CreateProcessor(MethodBase original)
		{
			return new PatchProcessor(this, original);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0000A23E File Offset: 0x0000843E
		public PatchClassProcessor CreateClassProcessor(Type type)
		{
			return new PatchClassProcessor(this, type);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000A247 File Offset: 0x00008447
		public ReversePatcher CreateReversePatcher(MethodBase original, HarmonyMethod standin)
		{
			return new ReversePatcher(this, original, standin);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000A251 File Offset: 0x00008451
		public void PatchAll(Assembly assembly)
		{
			AccessTools.GetTypesFromAssembly(assembly).Do(delegate(Type type)
			{
				this.CreateClassProcessor(type).Patch();
			});
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000A26C File Offset: 0x0000846C
		public void PatchAllUncategorized()
		{
			MethodBase method = new StackTrace().GetFrame(1).GetMethod();
			Assembly assembly = method.ReflectedType.Assembly;
			this.PatchAllUncategorized(assembly);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000A2A0 File Offset: 0x000084A0
		public void PatchAllUncategorized(Assembly assembly)
		{
			PatchClassProcessor[] sequence = AccessTools.GetTypesFromAssembly(assembly).Select(new Func<Type, PatchClassProcessor>(this.CreateClassProcessor)).ToArray<PatchClassProcessor>();
			sequence.DoIf((PatchClassProcessor patchClass) => string.IsNullOrEmpty(patchClass.Category), delegate(PatchClassProcessor patchClass)
			{
				patchClass.Patch();
			});
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000A310 File Offset: 0x00008510
		public void PatchCategory(string category)
		{
			MethodBase method = new StackTrace().GetFrame(1).GetMethod();
			Assembly assembly = method.ReflectedType.Assembly;
			this.PatchCategory(assembly, category);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000A344 File Offset: 0x00008544
		public void PatchCategory(Assembly assembly, string category)
		{
			AccessTools.GetTypesFromAssembly(assembly).Where(delegate(Type type)
			{
				List<HarmonyMethod> fromType = HarmonyMethodExtensions.GetFromType(type);
				HarmonyMethod harmonyMethod = HarmonyMethod.Merge(fromType);
				return harmonyMethod.category == category;
			}).Do(delegate(Type type)
			{
				this.CreateClassProcessor(type).Patch();
			});
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000A390 File Offset: 0x00008590
		public MethodInfo Patch(MethodBase original, HarmonyMethod prefix = null, HarmonyMethod postfix = null, HarmonyMethod transpiler = null, HarmonyMethod finalizer = null)
		{
			PatchProcessor patchProcessor = this.CreateProcessor(original);
			patchProcessor.AddPrefix(prefix);
			patchProcessor.AddPostfix(postfix);
			patchProcessor.AddTranspiler(transpiler);
			patchProcessor.AddFinalizer(finalizer);
			return patchProcessor.Patch();
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0000A3CD File Offset: 0x000085CD
		public static MethodInfo ReversePatch(MethodBase original, HarmonyMethod standin, MethodInfo transpiler = null)
		{
			return PatchFunctions.ReversePatch(standin, original, transpiler);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000A3D8 File Offset: 0x000085D8
		public void UnpatchAll(string harmonyID = null)
		{
			Harmony.<>c__DisplayClass17_0 CS$<>8__locals1 = new Harmony.<>c__DisplayClass17_0();
			CS$<>8__locals1.harmonyID = harmonyID;
			CS$<>8__locals1.<>4__this = this;
			List<MethodBase> list = Harmony.GetAllPatchedMethods().ToList<MethodBase>();
			using (List<MethodBase>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					MethodBase original = enumerator.Current;
					bool flag = original.HasMethodBody();
					Patches patchInfo2 = Harmony.GetPatchInfo(original);
					if (flag)
					{
						patchInfo2.Postfixes.DoIf(new Func<Patch, bool>(CS$<>8__locals1.<UnpatchAll>g__IDCheck|0), delegate(Patch patchInfo)
						{
							CS$<>8__locals1.<>4__this.Unpatch(original, patchInfo.PatchMethod);
						});
						patchInfo2.Prefixes.DoIf(new Func<Patch, bool>(CS$<>8__locals1.<UnpatchAll>g__IDCheck|0), delegate(Patch patchInfo)
						{
							CS$<>8__locals1.<>4__this.Unpatch(original, patchInfo.PatchMethod);
						});
					}
					patchInfo2.Transpilers.DoIf(new Func<Patch, bool>(CS$<>8__locals1.<UnpatchAll>g__IDCheck|0), delegate(Patch patchInfo)
					{
						CS$<>8__locals1.<>4__this.Unpatch(original, patchInfo.PatchMethod);
					});
					if (flag)
					{
						patchInfo2.Finalizers.DoIf(new Func<Patch, bool>(CS$<>8__locals1.<UnpatchAll>g__IDCheck|0), delegate(Patch patchInfo)
						{
							CS$<>8__locals1.<>4__this.Unpatch(original, patchInfo.PatchMethod);
						});
					}
				}
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000A51C File Offset: 0x0000871C
		public void Unpatch(MethodBase original, HarmonyPatchType type, string harmonyID = "*")
		{
			PatchProcessor patchProcessor = this.CreateProcessor(original);
			patchProcessor.Unpatch(type, harmonyID);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000A53C File Offset: 0x0000873C
		public void Unpatch(MethodBase original, MethodInfo patch)
		{
			PatchProcessor patchProcessor = this.CreateProcessor(original);
			patchProcessor.Unpatch(patch);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0000A55C File Offset: 0x0000875C
		public void UnpatchCategory(string category)
		{
			MethodBase method = new StackTrace().GetFrame(1).GetMethod();
			Assembly assembly = method.ReflectedType.Assembly;
			this.UnpatchCategory(assembly, category);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000A590 File Offset: 0x00008790
		public void UnpatchCategory(Assembly assembly, string category)
		{
			AccessTools.GetTypesFromAssembly(assembly).Where(delegate(Type type)
			{
				List<HarmonyMethod> fromType = HarmonyMethodExtensions.GetFromType(type);
				HarmonyMethod harmonyMethod = HarmonyMethod.Merge(fromType);
				return harmonyMethod.category == category;
			}).Do(delegate(Type type)
			{
				this.CreateClassProcessor(type).Unpatch();
			});
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000A5DC File Offset: 0x000087DC
		public static bool HasAnyPatches(string harmonyID)
		{
			IEnumerable<MethodBase> allPatchedMethods = Harmony.GetAllPatchedMethods();
			Func<MethodBase, Patches> selector;
			if ((selector = Harmony.<>O.<0>__GetPatchInfo) == null)
			{
				selector = (Harmony.<>O.<0>__GetPatchInfo = new Func<MethodBase, Patches>(Harmony.GetPatchInfo));
			}
			return allPatchedMethods.Select(selector).Any((Patches info) => info.Owners.Contains(harmonyID));
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000A62C File Offset: 0x0000882C
		public static Patches GetPatchInfo(MethodBase method)
		{
			return PatchProcessor.GetPatchInfo(method);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000A634 File Offset: 0x00008834
		public IEnumerable<MethodBase> GetPatchedMethods()
		{
			return from original in Harmony.GetAllPatchedMethods()
			where Harmony.GetPatchInfo(original).Owners.Contains(this.Id)
			select original;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000A64C File Offset: 0x0000884C
		public static IEnumerable<MethodBase> GetAllPatchedMethods()
		{
			return PatchProcessor.GetAllPatchedMethods();
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000A653 File Offset: 0x00008853
		public static MethodBase GetOriginalMethod(MethodInfo replacement)
		{
			if (replacement == null)
			{
				throw new ArgumentNullException("replacement");
			}
			return HarmonySharedState.GetRealMethod(replacement, false);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000A670 File Offset: 0x00008870
		public static MethodBase GetMethodFromStackframe(StackFrame frame)
		{
			if (frame == null)
			{
				throw new ArgumentNullException("frame");
			}
			return HarmonySharedState.GetStackFrameMethod(frame, true);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000A687 File Offset: 0x00008887
		public static MethodBase GetOriginalMethodFromStackframe(StackFrame frame)
		{
			if (frame == null)
			{
				throw new ArgumentNullException("frame");
			}
			return HarmonySharedState.GetStackFrameMethod(frame, false);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000A69E File Offset: 0x0000889E
		public static Dictionary<string, Version> VersionInfo(out Version currentVersion)
		{
			return PatchProcessor.VersionInfo(out currentVersion);
		}

		// Token: 0x04000088 RID: 136
		public static bool DEBUG;

		// Token: 0x0200007F RID: 127
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000187 RID: 391
			public static Func<MethodBase, Patches> <0>__GetPatchInfo;
		}
	}
}
