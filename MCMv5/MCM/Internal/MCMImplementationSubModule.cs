using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using BUTR.DependencyInjection;
using BUTR.DependencyInjection.Extensions;
using MCM.Abstractions;
using MCM.Abstractions.GameFeatures;
using MCM.Abstractions.Properties;
using MCM.Implementation;
using MCM.Implementation.FluentBuilder;
using MCM.Implementation.Global;
using MCM.Implementation.PerCampaign;
using MCM.Implementation.PerSave;
using MCM.Internal.Extensions;
using MCM.Internal.GameFeatures;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.MountAndBlade;

namespace MCM.Internal
{
	// Token: 0x0200000C RID: 12
	[NullableContext(2)]
	[Nullable(0)]
	internal class MCMImplementationSubModule : MBSubModuleBase, IGameEventListener
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000027 RID: 39 RVA: 0x00002610 File Offset: 0x00000810
		// (remove) Token: 0x06000028 RID: 40 RVA: 0x00002648 File Offset: 0x00000848
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action GameStarted;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000029 RID: 41 RVA: 0x00002680 File Offset: 0x00000880
		// (remove) Token: 0x0600002A RID: 42 RVA: 0x000026B8 File Offset: 0x000008B8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action GameLoaded;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600002B RID: 43 RVA: 0x000026F0 File Offset: 0x000008F0
		// (remove) Token: 0x0600002C RID: 44 RVA: 0x00002728 File Offset: 0x00000928
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action GameEnded;

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002D RID: 45 RVA: 0x0000275D File Offset: 0x0000095D
		// (set) Token: 0x0600002E RID: 46 RVA: 0x00002765 File Offset: 0x00000965
		private bool ServiceRegistrationWasCalled { get; set; }

		// Token: 0x0600002F RID: 47 RVA: 0x00002770 File Offset: 0x00000970
		public void OnServiceRegistration()
		{
			this.ServiceRegistrationWasCalled = true;
			IGenericServiceContainer services = this.GetServiceContainer();
			bool flag = services != null;
			if (flag)
			{
				services.AddSettingsContainer<FluentGlobalSettingsContainer>();
				services.AddSettingsContainer<ExternalGlobalSettingsContainer>();
				services.AddSettingsContainer<GlobalSettingsContainer>();
				services.AddSettingsContainer<FluentPerSaveSettingsContainer>();
				services.AddSettingsContainer<PerSaveSettingsContainer>();
				services.AddSettingsContainer<FluentPerCampaignSettingsContainer>();
				services.AddSettingsContainer<PerCampaignSettingsContainer>();
				services.AddSettingsFormat<JsonSettingsFormat>();
				services.AddSettingsFormat<XmlSettingsFormat>();
				services.AddSettingsPropertyDiscoverer<IAttributeSettingsPropertyDiscoverer, AttributeSettingsPropertyDiscoverer>();
				services.AddSettingsPropertyDiscoverer<IFluentSettingsPropertyDiscoverer, FluentSettingsPropertyDiscoverer>();
				services.AddSettingsBuilderFactory<DefaultSettingsBuilderFactory>();
				services.AddSettingsProvider<DefaultSettingsProvider>();
				services.AddSingleton((IGenericServiceFactory sp) => this);
				services.AddSingleton<ICampaignIdProvider, CampaignIdProvider>();
				services.AddSingleton<IFileSystemProvider, FileSystemProvider>();
				services.AddScoped<PerSaveCampaignBehavior>();
				services.AddTransient((IGenericServiceFactory sp) => sp.GetService<PerSaveCampaignBehavior>());
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002844 File Offset: 0x00000A44
		protected override void OnSubModuleLoad()
		{
			base.OnSubModuleLoad();
			MCMImplementationSubModule.PerformMigration001();
			MCMImplementationSubModule.PerformMigration002();
			bool flag = !this.ServiceRegistrationWasCalled;
			if (flag)
			{
				this.OnServiceRegistration();
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002879 File Offset: 0x00000A79
		protected override void OnBeforeInitialModuleScreenSetAsRoot()
		{
			base.OnBeforeInitialModuleScreenSetAsRoot();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002884 File Offset: 0x00000A84
		[NullableContext(1)]
		protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
		{
			base.OnGameStart(game, gameStarterObject);
			bool flag = game.GameType is Campaign;
			if (flag)
			{
				Action gameStarted = this.GameStarted;
				if (gameStarted != null)
				{
					gameStarted();
				}
				CampaignGameStarter gameStarter = (CampaignGameStarter)gameStarterObject;
				gameStarter.AddBehavior(GenericServiceProvider.GetService<PerSaveCampaignBehavior>());
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000028D4 File Offset: 0x00000AD4
		[NullableContext(1)]
		public override void OnNewGameCreated(Game game, object initializerObject)
		{
			base.OnNewGameCreated(game, initializerObject);
			bool flag = game.GameType is Campaign;
			if (flag)
			{
				Action gameLoaded = this.GameLoaded;
				if (gameLoaded != null)
				{
					gameLoaded();
				}
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002914 File Offset: 0x00000B14
		[NullableContext(1)]
		public override void OnGameLoaded(Game game, object initializerObject)
		{
			base.OnGameLoaded(game, initializerObject);
			bool flag = game.GameType is Campaign;
			if (flag)
			{
				Action gameLoaded = this.GameLoaded;
				if (gameLoaded != null)
				{
					gameLoaded();
				}
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002954 File Offset: 0x00000B54
		[NullableContext(1)]
		public override void OnGameEnd(Game game)
		{
			base.OnGameEnd(game);
			bool flag = game.GameType is Campaign;
			if (flag)
			{
				Action gameEnded = this.GameEnded;
				if (gameEnded != null)
				{
					gameEnded();
				}
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002990 File Offset: 0x00000B90
		private static void PerformMigration001()
		{
			try
			{
				string oldConfigPath = Path.GetFullPath("Configs");
				string oldPath = Path.Combine(oldConfigPath, "ModSettings");
				string newPath = Path.Combine(PlatformFileHelperPCExtended.GetDirectoryFullPath(EngineFilePaths.ConfigsPath) ?? string.Empty, "ModSettings");
				bool flag = Directory.Exists(oldPath) && Directory.Exists(newPath);
				if (flag)
				{
					foreach (string filePath in Directory.GetFiles(oldPath))
					{
						string fileName = Path.GetFileName(filePath);
						string newFilePath = Path.Combine(newPath, fileName);
						try
						{
							File.Copy(filePath, newFilePath, true);
							File.Delete(filePath);
						}
						catch (Exception)
						{
						}
					}
					foreach (string directoryPath in Directory.GetDirectories(oldPath))
					{
						string directoryName = Path.GetFileName(directoryPath);
						string newDirectoryPath = Path.Combine(newPath, directoryName);
						try
						{
							MCMImplementationSubModule.<PerformMigration001>g__MoveDirectory|20_0(directoryPath, newDirectoryPath);
						}
						catch (Exception)
						{
						}
					}
					string[] array = Directory.GetFiles(oldPath);
					bool flag2;
					if (array != null && array.Length == 0)
					{
						array = Directory.GetDirectories(oldPath);
						flag2 = (array != null && array.Length == 0);
					}
					else
					{
						flag2 = false;
					}
					bool flag3 = flag2;
					if (flag3)
					{
						Directory.Delete(oldPath, true);
					}
					array = Directory.GetFiles(oldConfigPath);
					bool flag4;
					if (array != null && array.Length == 0)
					{
						array = Directory.GetDirectories(oldConfigPath);
						flag4 = (array != null && array.Length == 0);
					}
					else
					{
						flag4 = false;
					}
					bool flag5 = flag4;
					if (flag5)
					{
						Directory.Delete(oldConfigPath, true);
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002B54 File Offset: 0x00000D54
		private static void PerformMigration002()
		{
			try
			{
				string oldConfigPath = Path.GetFullPath("Configs");
				string oldPath = Path.GetFullPath(Path.Combine(PlatformFileHelperPCExtended.GetDirectoryFullPath(EngineFilePaths.ConfigsPath) ?? string.Empty, "../", "ModSettings"));
				string newPath = Path.Combine(PlatformFileHelperPCExtended.GetDirectoryFullPath(EngineFilePaths.ConfigsPath) ?? string.Empty, "ModSettings");
				bool flag = Directory.Exists(oldPath) && Directory.Exists(newPath);
				if (flag)
				{
					foreach (string filePath in Directory.GetFiles(oldPath))
					{
						string fileName = Path.GetFileName(filePath);
						string newFilePath = Path.Combine(newPath, fileName);
						try
						{
							File.Copy(filePath, newFilePath, true);
							File.Delete(filePath);
						}
						catch (Exception)
						{
						}
					}
					foreach (string directoryPath in Directory.GetDirectories(oldPath))
					{
						string directoryName = Path.GetFileName(directoryPath);
						string newDirectoryPath = Path.Combine(newPath, directoryName);
						try
						{
							MCMImplementationSubModule.<PerformMigration002>g__MoveDirectory|21_0(directoryPath, newDirectoryPath);
						}
						catch (Exception)
						{
						}
					}
					string[] array = Directory.GetFiles(oldPath);
					bool flag2;
					if (array != null && array.Length == 0)
					{
						array = Directory.GetDirectories(oldPath);
						flag2 = (array != null && array.Length == 0);
					}
					else
					{
						flag2 = false;
					}
					bool flag3 = flag2;
					if (flag3)
					{
						Directory.Delete(oldPath, true);
					}
					array = Directory.GetFiles(oldConfigPath);
					bool flag4;
					if (array != null && array.Length == 0)
					{
						array = Directory.GetDirectories(oldConfigPath);
						flag4 = (array != null && array.Length == 0);
					}
					else
					{
						flag4 = false;
					}
					bool flag5 = flag4;
					if (flag5)
					{
						Directory.Delete(oldConfigPath, true);
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002D40 File Offset: 0x00000F40
		[NullableContext(1)]
		[CompilerGenerated]
		internal static void <PerformMigration001>g__MoveDirectory|20_0(string source, string target)
		{
			string sourcePath = source.TrimEnd(new char[]
			{
				'\\',
				' '
			});
			string targetPath = target.TrimEnd(new char[]
			{
				'\\',
				' '
			});
			IEnumerable<string> source2 = Directory.EnumerateFiles(sourcePath, "*", SearchOption.AllDirectories);
			Func<string, string> keySelector;
			if ((keySelector = MCMImplementationSubModule.<>O.<0>__GetDirectoryName) == null)
			{
				keySelector = (MCMImplementationSubModule.<>O.<0>__GetDirectoryName = new Func<string, string>(Path.GetDirectoryName));
			}
			foreach (IGrouping<string, string> folder in source2.GroupBy(keySelector))
			{
				string targetFolder = folder.Key.Replace(sourcePath, targetPath);
				Directory.CreateDirectory(targetFolder);
				foreach (string file in folder)
				{
					string targetFile = Path.Combine(targetFolder, Path.GetFileName(file));
					bool flag = File.Exists(targetFile);
					if (flag)
					{
						File.Delete(targetFile);
					}
					File.Move(file, targetFile);
				}
			}
			Directory.Delete(source, true);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002E6C File Offset: 0x0000106C
		[NullableContext(1)]
		[CompilerGenerated]
		internal static void <PerformMigration002>g__MoveDirectory|21_0(string source, string target)
		{
			string sourcePath = source.TrimEnd(new char[]
			{
				'\\',
				' '
			});
			string targetPath = target.TrimEnd(new char[]
			{
				'\\',
				' '
			});
			IEnumerable<string> source2 = Directory.EnumerateFiles(sourcePath, "*", SearchOption.AllDirectories);
			Func<string, string> keySelector;
			if ((keySelector = MCMImplementationSubModule.<>O.<0>__GetDirectoryName) == null)
			{
				keySelector = (MCMImplementationSubModule.<>O.<0>__GetDirectoryName = new Func<string, string>(Path.GetDirectoryName));
			}
			foreach (IGrouping<string, string> folder in source2.GroupBy(keySelector))
			{
				string targetFolder = folder.Key.Replace(sourcePath, targetPath);
				Directory.CreateDirectory(targetFolder);
				foreach (string file in folder)
				{
					string targetFile = Path.Combine(targetFolder, Path.GetFileName(file));
					bool flag = File.Exists(targetFile);
					if (flag)
					{
						File.Delete(targetFile);
					}
					File.Move(file, targetFile);
				}
			}
			Directory.Delete(source, true);
		}

		// Token: 0x02000165 RID: 357
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040002AE RID: 686
			[Nullable(0)]
			public static Func<string, string> <0>__GetDirectoryName;
		}
	}
}
