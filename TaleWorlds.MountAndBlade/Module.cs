using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using TaleWorlds.AchievementSystem;
using TaleWorlds.ActivitySystem;
using TaleWorlds.Avatar.PlayerServices;
using TaleWorlds.Core;
using TaleWorlds.Diamond.ClientApplication;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Engine.Options;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.ModuleManager;
using TaleWorlds.ObjectSystem;
using TaleWorlds.PlatformService;
using TaleWorlds.SaveSystem;
using TaleWorlds.ScreenSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002C7 RID: 711
	public sealed class Module : DotNetObject, IGameStateManagerOwner
	{
		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x0600273E RID: 10046 RVA: 0x00095D9A File Offset: 0x00093F9A
		// (set) Token: 0x0600273F RID: 10047 RVA: 0x00095DA2 File Offset: 0x00093FA2
		public GameTextManager GlobalTextManager { get; private set; }

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06002740 RID: 10048 RVA: 0x00095DAB File Offset: 0x00093FAB
		// (set) Token: 0x06002741 RID: 10049 RVA: 0x00095DB3 File Offset: 0x00093FB3
		public JobManager JobManager { get; private set; }

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06002742 RID: 10050 RVA: 0x00095DBC File Offset: 0x00093FBC
		public MBReadOnlyList<MBSubModuleBase> SubModules
		{
			get
			{
				return this._submodules;
			}
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06002743 RID: 10051 RVA: 0x00095DC4 File Offset: 0x00093FC4
		// (set) Token: 0x06002744 RID: 10052 RVA: 0x00095DCC File Offset: 0x00093FCC
		public GameStateManager GlobalGameStateManager { get; private set; }

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06002745 RID: 10053 RVA: 0x00095DD5 File Offset: 0x00093FD5
		// (set) Token: 0x06002746 RID: 10054 RVA: 0x00095DDD File Offset: 0x00093FDD
		public bool ReturnToEditorState { get; private set; }

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06002747 RID: 10055 RVA: 0x00095DE6 File Offset: 0x00093FE6
		// (set) Token: 0x06002748 RID: 10056 RVA: 0x00095DEE File Offset: 0x00093FEE
		public bool LoadingFinished { get; private set; }

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06002749 RID: 10057 RVA: 0x00095DF7 File Offset: 0x00093FF7
		// (set) Token: 0x0600274A RID: 10058 RVA: 0x00095DFF File Offset: 0x00093FFF
		public bool IsOnlyCoreContentEnabled { get; private set; }

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x0600274B RID: 10059 RVA: 0x00095E08 File Offset: 0x00094008
		public bool MultiplayerRequested
		{
			get
			{
				return this.StartupInfo.StartupType == GameStartupType.Multiplayer || PlatformServices.SessionInvitationType == SessionInvitationType.Multiplayer || PlatformServices.IsPlatformRequestedMultiplayer;
			}
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x0600274C RID: 10060 RVA: 0x00095E27 File Offset: 0x00094027
		// (set) Token: 0x0600274D RID: 10061 RVA: 0x00095E2F File Offset: 0x0009402F
		public GameStartupInfo StartupInfo { get; private set; }

		// Token: 0x0600274E RID: 10062 RVA: 0x00095E38 File Offset: 0x00094038
		private Module()
		{
			MBDebug.Print("Creating module...", 0, Debug.DebugColor.White, 17592186044416UL);
			this.StartupInfo = new GameStartupInfo();
			this._testContext = new TestContext();
			this._loadedSubmoduleTypes = new Dictionary<string, Type>();
			this._submodules = new MBList<MBSubModuleBase>();
			this.GlobalGameStateManager = new GameStateManager(this, GameStateManager.GameStateManagerType.Global);
			GameStateManager.Current = this.GlobalGameStateManager;
			this.GlobalTextManager = new GameTextManager();
			this.JobManager = new JobManager();
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x0600274F RID: 10063 RVA: 0x00095EC6 File Offset: 0x000940C6
		// (set) Token: 0x06002750 RID: 10064 RVA: 0x00095ECD File Offset: 0x000940CD
		public static Module CurrentModule { get; private set; }

		// Token: 0x06002751 RID: 10065 RVA: 0x00095ED5 File Offset: 0x000940D5
		internal static void CreateModule()
		{
			Module.CurrentModule = new Module();
			Utilities.SetLoadingScreenPercentage(0.4f);
		}

		// Token: 0x06002752 RID: 10066 RVA: 0x00095EEC File Offset: 0x000940EC
		private void AddSubModule(Assembly subModuleAssembly, string name)
		{
			Type type = subModuleAssembly.GetType(name);
			this._loadedSubmoduleTypes.Add(name, type);
			Managed.AddTypes(this.CollectModuleAssemblyTypes(subModuleAssembly));
		}

		// Token: 0x06002753 RID: 10067 RVA: 0x00095F1C File Offset: 0x0009411C
		private Dictionary<string, Type> CollectModuleAssemblyTypes(Assembly moduleAssembly)
		{
			Dictionary<string, Type> result;
			try
			{
				Dictionary<string, Type> dictionary = new Dictionary<string, Type>();
				foreach (Type type in moduleAssembly.GetTypes())
				{
					if (typeof(ManagedObject).IsAssignableFrom(type) || typeof(DotNetObject).IsAssignableFrom(type))
					{
						dictionary.Add(type.Name, type);
					}
				}
				result = dictionary;
			}
			catch (Exception ex)
			{
				MBDebug.Print("Error while getting types and loading" + ex.Message, 0, Debug.DebugColor.White, 17592186044416UL);
				ReflectionTypeLoadException ex2;
				if ((ex2 = (ex as ReflectionTypeLoadException)) != null)
				{
					string text = "";
					foreach (Exception ex3 in ex2.LoaderExceptions)
					{
						MBDebug.Print("Loader Exceptions: " + ex3.Message, 0, Debug.DebugColor.White, 17592186044416UL);
						text = text + ex3.Message + Environment.NewLine;
					}
					Debug.SetCrashReportCustomString(text);
					foreach (Type type2 in ex2.Types)
					{
						if (type2 != null)
						{
							MBDebug.Print("Loaded Types: " + type2.FullName, 0, Debug.DebugColor.White, 17592186044416UL);
						}
					}
				}
				if (ex.InnerException != null)
				{
					MBDebug.Print("Inner excetion: " + ex.StackTrace, 0, Debug.DebugColor.White, 17592186044416UL);
				}
				throw;
			}
			return result;
		}

		// Token: 0x06002754 RID: 10068 RVA: 0x000960AC File Offset: 0x000942AC
		private void InitializeSubModules()
		{
			Managed.AddConstructorDelegateOfClass<SpawnedItemEntity>();
			foreach (Type type in this._loadedSubmoduleTypes.Values)
			{
				MBSubModuleBase mbsubModuleBase = (MBSubModuleBase)type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, new Type[0], null).Invoke(new object[0]);
				this._submodules.Add(mbsubModuleBase);
				mbsubModuleBase.OnSubModuleLoad();
			}
		}

		// Token: 0x06002755 RID: 10069 RVA: 0x00096138 File Offset: 0x00094338
		private void FinalizeSubModules()
		{
			foreach (MBSubModuleBase mbsubModuleBase in this._submodules)
			{
				mbsubModuleBase.OnSubModuleUnloaded();
			}
		}

		// Token: 0x06002756 RID: 10070 RVA: 0x00096188 File Offset: 0x00094388
		public Type GetSubModule(string name)
		{
			return this._loadedSubmoduleTypes[name];
		}

		// Token: 0x06002757 RID: 10071 RVA: 0x00096198 File Offset: 0x00094398
		[MBCallback]
		internal void Initialize()
		{
			MBDebug.Print("Module Initialize begin...", 0, Debug.DebugColor.White, 17592186044416UL);
			TWParallel.InitializeAndSetImplementation(new NativeParallelDriver());
			MBSaveLoad.SetSaveDriver(new AsyncFileSaveDriver());
			this.ProcessApplicationArguments();
			this.SetWindowTitle();
			this._initialStateOptions = new List<InitialStateOption>();
			this.FillMultiplayerGameTypes();
			if (!GameNetwork.IsDedicatedServer && !MBDebug.TestModeEnabled)
			{
				MBDebug.Print("Loading platform services...", 0, Debug.DebugColor.White, 17592186044416UL);
				this.LoadPlatformServices();
			}
			string[] modulesNames = Utilities.GetModulesNames();
			List<string> list = new List<string>();
			string[] array = modulesNames;
			for (int i = 0; i < array.Length; i++)
			{
				ModuleInfo moduleInfo = ModuleHelper.GetModuleInfo(array[i]);
				if (moduleInfo != null)
				{
					list.Add(moduleInfo.FolderPath);
				}
			}
			LocalizedTextManager.LoadLocalizationXmls(list.ToArray());
			this.GlobalTextManager.LoadDefaultTexts();
			this.IsOnlyCoreContentEnabled = Utilities.IsOnlyCoreContentEnabled();
			NativeConfig.OnConfigChanged();
			this.LoadSubModules();
			MBDebug.Print("Adding trace listener...", 0, Debug.DebugColor.White, 17592186044416UL);
			MBDebug.Print("MBModuleBase Initialize begin...", 0, Debug.DebugColor.White, 17592186044416UL);
			MBDebug.Print("MBModuleBase Initialize end...", 0, Debug.DebugColor.White, 17592186044416UL);
			GameNetwork.FindGameNetworkMessages();
			GameNetwork.FindSynchedMissionObjectTypes();
			HasTableauCache.CollectTableauCacheTypes();
			MBDebug.Print("Module Initialize end...", 0, Debug.DebugColor.White, 17592186044416UL);
			MBDebug.TestModeEnabled = Utilities.CommandLineArgumentExists("/runTest");
			this.FindMissions();
			NativeOptions.ReadRGLConfigFiles();
			BannerlordConfig.Initialize();
			EngineController.ConfigChange += this.OnConfigChanged;
			EngineController.OnConstrainedStateChanged += this.OnConstrainedStateChange;
			ScreenManager.FocusGained += this.OnFocusGained;
			ScreenManager.PlatformTextRequested += this.OnPlatformTextRequested;
			PlatformServices.Instance.OnTextEnteredFromPlatform += this.OnTextEnteredFromPlatform;
			SaveManager.InitializeGlobalDefinitionContext();
			this.EnsureAsyncJobsAreFinished();
		}

		// Token: 0x06002758 RID: 10072 RVA: 0x0009635F File Offset: 0x0009455F
		private void OnPlatformTextRequested(string initialText, string descriptionText, int maxLength, int keyboardTypeEnum)
		{
			IPlatformServices instance = PlatformServices.Instance;
			if (instance == null)
			{
				return;
			}
			instance.ShowGamepadTextInput(descriptionText, initialText, (uint)maxLength, keyboardTypeEnum == 2);
		}

		// Token: 0x06002759 RID: 10073 RVA: 0x00096378 File Offset: 0x00094578
		private void SetWindowTitle()
		{
			string applicationName = Utilities.GetApplicationName();
			string text;
			if (this.StartupInfo.StartupType == GameStartupType.Singleplayer)
			{
				text = applicationName + " - Singleplayer";
			}
			else if (this.StartupInfo.StartupType == GameStartupType.Multiplayer)
			{
				text = applicationName + " - Multiplayer";
			}
			else if (this.StartupInfo.StartupType == GameStartupType.GameServer)
			{
				text = string.Concat(new object[]
				{
					"[",
					Utilities.GetCurrentProcessID(),
					"] ",
					applicationName,
					" Dedicated Server Port:",
					this.StartupInfo.ServerPort
				});
			}
			else
			{
				text = applicationName;
			}
			text = Utilities.ProcessWindowTitle(text);
			Utilities.SetWindowTitle(text);
		}

		// Token: 0x0600275A RID: 10074 RVA: 0x0009642A File Offset: 0x0009462A
		private void EnsureAsyncJobsAreFinished()
		{
			if (!GameNetwork.IsDedicatedServer)
			{
				while (!MBMusicManager.IsCreationCompleted())
				{
					Thread.Sleep(1);
				}
			}
			if (!GameNetwork.IsDedicatedServer && !MBDebug.TestModeEnabled)
			{
				while (!AchievementManager.AchievementService.IsInitializationCompleted())
				{
					Thread.Sleep(1);
				}
			}
		}

		// Token: 0x0600275B RID: 10075 RVA: 0x00096464 File Offset: 0x00094664
		private void ProcessApplicationArguments()
		{
			this.StartupInfo.StartupType = GameStartupType.None;
			string[] array = Utilities.GetFullCommandLineString().Split(new char[]
			{
				' '
			});
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i].ToLowerInvariant();
				if (text == "/dedicatedmatchmakingserver".ToLower())
				{
					int serverPort = Convert.ToInt32(array[i + 1]);
					string serverRegion = array[i + 2];
					sbyte serverPriority = Convert.ToSByte(array[i + 3]);
					string serverGameMode = array[i + 4];
					i += 4;
					this.StartupInfo.StartupType = GameStartupType.GameServer;
					this.StartupInfo.DedicatedServerType = DedicatedServerType.Matchmaker;
					this.StartupInfo.ServerPort = serverPort;
					this.StartupInfo.ServerRegion = serverRegion;
					this.StartupInfo.ServerPriority = serverPriority;
					this.StartupInfo.ServerGameMode = serverGameMode;
				}
				else if (text == "/dedicatedcustomserver".ToLower())
				{
					int serverPort2 = Convert.ToInt32(array[i + 1]);
					string serverRegion2 = array[i + 2];
					int permission = Convert.ToInt32(array[i + 3]);
					i += 3;
					this.StartupInfo.StartupType = GameStartupType.GameServer;
					this.StartupInfo.DedicatedServerType = DedicatedServerType.Custom;
					this.StartupInfo.ServerPort = serverPort2;
					this.StartupInfo.ServerRegion = serverRegion2;
					this.StartupInfo.Permission = permission;
				}
				else if (text == "/dedicatedcommunityserver".ToLower())
				{
					int serverPort3 = Convert.ToInt32(array[i + 1]);
					i++;
					this.StartupInfo.StartupType = GameStartupType.GameServer;
					this.StartupInfo.DedicatedServerType = DedicatedServerType.Community;
					this.StartupInfo.ServerPort = serverPort3;
				}
				else if (text == "/dedicatedcustomserverconfigfile".ToLower())
				{
					string customGameServerConfigFile = array[i + 1];
					i++;
					this.StartupInfo.CustomGameServerConfigFile = customGameServerConfigFile;
				}
				else if (text == "/dedicatedcustomservernameoverride".ToLower())
				{
					string customGameServerNameOverride = array[i + 1];
					i++;
					this.StartupInfo.CustomGameServerNameOverride = customGameServerNameOverride;
				}
				else if (text == "/dedicatedcustomserverpasswordoverride".ToLower())
				{
					string customGameServerPasswordOverride = array[i + 1];
					i++;
					this.StartupInfo.CustomGameServerPasswordOverride = customGameServerPasswordOverride;
				}
				else if (text == "/dedicatedcustomserverauthtoken".ToLower())
				{
					string customGameServerAuthToken = array[i + 1];
					i++;
					this.StartupInfo.CustomGameServerAuthToken = customGameServerAuthToken;
				}
				else if (text == "/dedicatedcustomserverDontAllowOptionalModules".ToLower())
				{
					this.StartupInfo.CustomGameServerAllowsOptionalModules = false;
				}
				else if (text == "/playerHostedDedicatedServer".ToLower())
				{
					this.StartupInfo.PlayerHostedDedicatedServer = true;
				}
				else if (text == "/singleplatform")
				{
					this.StartupInfo.IsSinglePlatformServer = true;
				}
				else if (text == "/customserverhost")
				{
					string customServerHostIP = array[i + 1];
					i++;
					this.StartupInfo.CustomServerHostIP = customServerHostIP;
				}
				else if (text == "/singleplayer".ToLower())
				{
					this.StartupInfo.StartupType = GameStartupType.Singleplayer;
				}
				else if (text == "/multiplayer".ToLower())
				{
					this.StartupInfo.StartupType = GameStartupType.Multiplayer;
				}
				else if (text == "/clientConfigurationCategory".ToLower())
				{
					ClientApplicationConfiguration.SetDefualtConfigurationCategory(array[i + 1]);
					i++;
				}
				else if (text == "/overridenusername".ToLower())
				{
					string overridenUserName = array[i + 1];
					this.StartupInfo.OverridenUserName = overridenUserName;
					i++;
				}
				else if (text.StartsWith("-AUTH_PASSWORD".ToLowerInvariant()))
				{
					this.StartupInfo.EpicExchangeCode = text.Split(new char[]
					{
						'='
					})[1];
				}
				else if (text == "/continuegame".ToLower())
				{
					this.StartupInfo.IsContinueGame = true;
				}
				else if (text == "/serverbandwidthlimitmbps".ToLower())
				{
					double serverBandwidthLimitInMbps = Convert.ToDouble(array[i + 1]);
					this.StartupInfo.ServerBandwidthLimitInMbps = serverBandwidthLimitInMbps;
					i++;
				}
				else if (text == "/tickrate".ToLower())
				{
					int serverTickRate = Convert.ToInt32(array[i + 1]);
					this.StartupInfo.ServerTickRate = serverTickRate;
					i++;
				}
			}
		}

		// Token: 0x0600275C RID: 10076 RVA: 0x0009689C File Offset: 0x00094A9C
		internal void OnApplicationTick(float dt)
		{
			bool isOnlyCoreContentEnabled = this.IsOnlyCoreContentEnabled;
			this.IsOnlyCoreContentEnabled = Utilities.IsOnlyCoreContentEnabled();
			if (isOnlyCoreContentEnabled != this.IsOnlyCoreContentEnabled && isOnlyCoreContentEnabled)
			{
				InitialState initialState;
				if ((initialState = (GameStateManager.Current.ActiveState as InitialState)) != null)
				{
					Utilities.DisableCoreGame();
					InformationManager.ShowInquiry(new InquiryData(new TextObject("{=CaSafuAH}Content Download Complete", null).ToString(), new TextObject("{=1nKa4pQX}Rest of the game content has been downloaded.", null).ToString(), true, false, new TextObject("{=yS7PvrTD}OK", null).ToString(), null, delegate()
					{
						initialState.RefreshContentState();
					}, null, "", 0f, null, null, null), false, false);
				}
				else
				{
					InformationManager.ShowInquiry(new InquiryData(new TextObject("{=CaSafuAH}Content Download Complete", null).ToString(), new TextObject("{=BFhMw4bl}Rest of the game content has been downloaded. Do you want to return to the main menu?", null).ToString(), true, true, new TextObject("{=aeouhelq}Yes", null).ToString(), new TextObject("{=8OkPHu4f}No", null).ToString(), new Action(this.OnConfirmReturnToMainMenu), null, "", 0f, null, null, null), false, false);
					this._enableCoreContentOnReturnToRoot = true;
				}
			}
			if (this._synchronizationContext == null)
			{
				this._synchronizationContext = new SingleThreadedSynchronizationContext();
				SynchronizationContext.SetSynchronizationContext(this._synchronizationContext);
			}
			this._testContext.OnApplicationTick(dt);
			if (!GameNetwork.MultiplayerDisabled)
			{
				this.OnNetworkTick(dt);
			}
			if (GameStateManager.Current == null)
			{
				GameStateManager.Current = this.GlobalGameStateManager;
			}
			if (GameStateManager.Current == this.GlobalGameStateManager)
			{
				if (this.LoadingFinished && this.GlobalGameStateManager.ActiveState == null)
				{
					if (this.ReturnToEditorState)
					{
						this.ReturnToEditorState = false;
						this.SetEditorScreenAsRootScreen();
					}
					else
					{
						this.SetInitialModuleScreenAsRootScreen();
					}
				}
				this.GlobalGameStateManager.OnTick(dt);
			}
			Utilities.RunJobs();
			IPlatformServices instance = PlatformServices.Instance;
			if (instance != null)
			{
				instance.Tick(dt);
			}
			this._synchronizationContext.Tick();
			if (GameManagerBase.Current != null)
			{
				GameManagerBase.Current.OnTick(dt);
			}
			foreach (MBSubModuleBase mbsubModuleBase in this.SubModules)
			{
				mbsubModuleBase.OnApplicationTick(dt);
			}
			this.JobManager.OnTick(dt);
			AvatarServices.UpdateAvatarServices(dt);
		}

		// Token: 0x0600275D RID: 10077 RVA: 0x00096ADC File Offset: 0x00094CDC
		private void OnConfirmReturnToMainMenu()
		{
			MBGameManager.EndGame();
		}

		// Token: 0x0600275E RID: 10078 RVA: 0x00096AE4 File Offset: 0x00094CE4
		private void OnNetworkTick(float dt)
		{
			foreach (MBSubModuleBase mbsubModuleBase in this.SubModules)
			{
				mbsubModuleBase.OnNetworkTick(dt);
			}
		}

		// Token: 0x0600275F RID: 10079 RVA: 0x00096B38 File Offset: 0x00094D38
		[MBCallback]
		internal void RunTest(string commandLine)
		{
			MBDebug.Print(" TEST MODE ENABLED. Command line string: " + commandLine, 0, Debug.DebugColor.White, 17592186044416UL);
			this._testContext.RunTestAux(commandLine);
		}

		// Token: 0x06002760 RID: 10080 RVA: 0x00096B62 File Offset: 0x00094D62
		[MBCallback]
		internal void TickTest(float dt)
		{
			this._testContext.TickTest(dt);
		}

		// Token: 0x06002761 RID: 10081 RVA: 0x00096B70 File Offset: 0x00094D70
		[MBCallback]
		internal void OnDumpCreated()
		{
			if (TestCommonBase.BaseInstance != null)
			{
				TestCommonBase.BaseInstance.ToggleTimeoutTimer();
				TestCommonBase.BaseInstance.StartTimeoutTimer();
			}
		}

		// Token: 0x06002762 RID: 10082 RVA: 0x00096B8D File Offset: 0x00094D8D
		[MBCallback]
		internal void OnDumpCreationStarted()
		{
			if (TestCommonBase.BaseInstance != null)
			{
				TestCommonBase.BaseInstance.ToggleTimeoutTimer();
			}
		}

		// Token: 0x06002763 RID: 10083 RVA: 0x00096BA0 File Offset: 0x00094DA0
		public static void GetMetaMeshPackageMapping(Dictionary<string, string> metaMeshPackageMappings)
		{
			foreach (ItemObject itemObject in Game.Current.ObjectManager.GetObjectTypeList<ItemObject>())
			{
				if (itemObject.HasArmorComponent)
				{
					string value = ((itemObject.Culture != null) ? itemObject.Culture.StringId : "shared") + "_armor";
					metaMeshPackageMappings[itemObject.MultiMeshName] = value;
					metaMeshPackageMappings[itemObject.MultiMeshName + "_converted"] = value;
					metaMeshPackageMappings[itemObject.MultiMeshName + "_converted_slim"] = value;
					metaMeshPackageMappings[itemObject.MultiMeshName + "_slim"] = value;
				}
				if (itemObject.WeaponComponent != null)
				{
					string value2 = ((itemObject.Culture != null) ? itemObject.Culture.StringId : "shared") + "_weapon";
					metaMeshPackageMappings[itemObject.MultiMeshName] = value2;
					if (itemObject.HolsterMeshName != null)
					{
						metaMeshPackageMappings[itemObject.HolsterMeshName] = value2;
					}
					if (itemObject.HolsterWithWeaponMeshName != null)
					{
						metaMeshPackageMappings[itemObject.HolsterWithWeaponMeshName] = value2;
					}
				}
				if (itemObject.HasHorseComponent)
				{
					string value3 = "horses";
					metaMeshPackageMappings[itemObject.MultiMeshName] = value3;
				}
				if (itemObject.IsFood)
				{
					string value4 = "food";
					metaMeshPackageMappings[itemObject.MultiMeshName] = value4;
				}
			}
			foreach (CraftingPiece craftingPiece in Game.Current.ObjectManager.GetObjectTypeList<CraftingPiece>())
			{
				string value5 = ((craftingPiece.Culture != null) ? craftingPiece.Culture.StringId : "shared") + "_crafting";
				metaMeshPackageMappings[craftingPiece.MeshName] = value5;
			}
		}

		// Token: 0x06002764 RID: 10084 RVA: 0x00096DB0 File Offset: 0x00094FB0
		public static void GetItemMeshNames(HashSet<string> itemMeshNames)
		{
			foreach (ItemObject itemObject in Game.Current.ObjectManager.GetObjectTypeList<ItemObject>())
			{
				if (!itemObject.IsCraftedWeapon)
				{
					itemMeshNames.Add(itemObject.MultiMeshName);
				}
				if (itemObject.PrimaryWeapon != null)
				{
					if (itemObject.FlyingMeshName != null && !itemObject.FlyingMeshName.IsEmpty<char>())
					{
						itemMeshNames.Add(itemObject.FlyingMeshName);
					}
					if (itemObject.HolsterMeshName != null && !itemObject.HolsterMeshName.IsEmpty<char>())
					{
						itemMeshNames.Add(itemObject.HolsterMeshName);
					}
					if (itemObject.HolsterWithWeaponMeshName != null && !itemObject.HolsterWithWeaponMeshName.IsEmpty<char>())
					{
						itemMeshNames.Add(itemObject.HolsterWithWeaponMeshName);
					}
				}
				if (itemObject.HasHorseComponent)
				{
					foreach (KeyValuePair<string, bool> keyValuePair in itemObject.HorseComponent.AdditionalMeshesNameList)
					{
						if (keyValuePair.Key != null && !keyValuePair.Key.IsEmpty<char>())
						{
							itemMeshNames.Add(keyValuePair.Key);
						}
					}
				}
			}
		}

		// Token: 0x06002765 RID: 10085 RVA: 0x00096EFC File Offset: 0x000950FC
		[MBCallback]
		internal string GetMetaMeshPackageMapping()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			Module.GetMetaMeshPackageMapping(dictionary);
			string text = "";
			foreach (string text2 in dictionary.Keys)
			{
				text = string.Concat(new string[]
				{
					text,
					text2,
					"|",
					dictionary[text2],
					","
				});
			}
			return text;
		}

		// Token: 0x06002766 RID: 10086 RVA: 0x00096F8C File Offset: 0x0009518C
		[MBCallback]
		internal string GetItemMeshNames()
		{
			HashSet<string> hashSet = new HashSet<string>();
			Module.GetItemMeshNames(hashSet);
			foreach (CraftingPiece craftingPiece in MBObjectManager.Instance.GetObjectTypeList<CraftingPiece>())
			{
				hashSet.Add(craftingPiece.MeshName);
				if (craftingPiece.BladeData != null)
				{
					hashSet.Add(craftingPiece.BladeData.HolsterMeshName);
				}
			}
			foreach (BannerIconGroup bannerIconGroup in BannerManager.Instance.BannerIconGroups)
			{
				foreach (KeyValuePair<int, BannerIconData> keyValuePair in bannerIconGroup.AllIcons)
				{
					if (keyValuePair.Value.MaterialName != "")
					{
						hashSet.Add(keyValuePair.Value.MaterialName + keyValuePair.Value.TextureIndex);
					}
				}
			}
			string text = "";
			foreach (string text2 in hashSet)
			{
				if (text2 != null && !text2.IsEmpty<char>())
				{
					text = text + text2 + "#";
				}
			}
			return text;
		}

		// Token: 0x06002767 RID: 10087 RVA: 0x00097138 File Offset: 0x00095338
		[MBCallback]
		internal string GetHorseMaterialNames()
		{
			HashSet<string> hashSet = new HashSet<string>();
			string text = "";
			foreach (ItemObject itemObject in Game.Current.ObjectManager.GetObjectTypeList<ItemObject>())
			{
				if (itemObject.HasHorseComponent && itemObject.HorseComponent.HorseMaterialNames != null && itemObject.HorseComponent.HorseMaterialNames.Count > 0)
				{
					foreach (HorseComponent.MaterialProperty materialProperty in itemObject.HorseComponent.HorseMaterialNames)
					{
						hashSet.Add(materialProperty.Name);
					}
				}
			}
			foreach (string text2 in hashSet)
			{
				if (text2 != null && !text2.IsEmpty<char>())
				{
					text = text + text2 + "#";
				}
			}
			return text;
		}

		// Token: 0x06002768 RID: 10088 RVA: 0x00097268 File Offset: 0x00095468
		public void SetInitialModuleScreenAsRootScreen()
		{
			if (GameStateManager.Current != this.GlobalGameStateManager)
			{
				GameStateManager.Current = this.GlobalGameStateManager;
			}
			foreach (MBSubModuleBase mbsubModuleBase in this.SubModules)
			{
				mbsubModuleBase.OnBeforeInitialModuleScreenSetAsRoot();
			}
			if (!GameNetwork.IsDedicatedServer)
			{
				string text = ModuleHelper.GetModuleFullPath("Native") + "Videos/TWLogo_and_Partners.ivf";
				string text2 = ModuleHelper.GetModuleFullPath("Native") + "Videos/TWLogo_and_Partners.ogg";
				if (!this._splashScreenPlayed && File.Exists(text) && (text2 == "" || File.Exists(text2)) && !Debugger.IsAttached)
				{
					VideoPlaybackState videoPlaybackState = this.GlobalGameStateManager.CreateState<VideoPlaybackState>();
					videoPlaybackState.SetStartingParameters(text, text2, string.Empty, 30f, true);
					videoPlaybackState.SetOnVideoFinisedDelegate(delegate
					{
						this.OnInitialModuleScreenActivated(true);
					});
					this.GlobalGameStateManager.CleanAndPushState(videoPlaybackState, 0);
					this._splashScreenPlayed = true;
					return;
				}
				this.OnInitialModuleScreenActivated(false);
			}
		}

		// Token: 0x06002769 RID: 10089 RVA: 0x00097380 File Offset: 0x00095580
		private void OnInitialModuleScreenActivated(bool isFromSplashScreenVideo)
		{
			Utilities.EnableGlobalLoadingWindow();
			LoadingWindow.EnableGlobalLoadingWindow();
			if (!this.StartupInfo.IsContinueGame)
			{
				this.StartupInfo.IsContinueGame = (PlatformServices.IsPlatformRequestedContinueGame && !this.IsOnlyCoreContentEnabled);
			}
			if (this._enableCoreContentOnReturnToRoot)
			{
				Utilities.DisableCoreGame();
				this._enableCoreContentOnReturnToRoot = false;
			}
			if (this.IsOnlyCoreContentEnabled && PlatformServices.SessionInvitationType == SessionInvitationType.Multiplayer)
			{
				PlatformServices.OnSessionInvitationHandled();
			}
			if (this.IsOnlyCoreContentEnabled && PlatformServices.IsPlatformRequestedMultiplayer)
			{
				PlatformServices.OnPlatformMultiplayerRequestHandled();
			}
			if (this.IsOnlyCoreContentEnabled || !this.MultiplayerRequested)
			{
				this.GlobalGameStateManager.CleanAndPushState(this.GlobalGameStateManager.CreateState<InitialState>(), 0);
			}
			foreach (MBSubModuleBase mbsubModuleBase in this.SubModules)
			{
				mbsubModuleBase.OnInitialState();
			}
		}

		// Token: 0x0600276A RID: 10090 RVA: 0x0009746C File Offset: 0x0009566C
		private void OnSignInStateUpdated(bool isLoggedIn, TextObject message)
		{
			if (!isLoggedIn && !(this.GlobalGameStateManager.ActiveState is ProfileSelectionState))
			{
				this.GlobalGameStateManager.CleanAndPushState(this.GlobalGameStateManager.CreateState<ProfileSelectionState>(), 0);
			}
		}

		// Token: 0x0600276B RID: 10091 RVA: 0x0009749C File Offset: 0x0009569C
		[MBCallback]
		internal bool SetEditorScreenAsRootScreen()
		{
			if (GameStateManager.Current != this.GlobalGameStateManager)
			{
				GameStateManager.Current = this.GlobalGameStateManager;
			}
			if (!(this.GlobalGameStateManager.ActiveState is EditorState))
			{
				this.GlobalGameStateManager.CleanAndPushState(GameStateManager.Current.CreateState<EditorState>(), 0);
				return true;
			}
			return false;
		}

		// Token: 0x0600276C RID: 10092 RVA: 0x000974EC File Offset: 0x000956EC
		private bool CheckAssemblyForMissionMethods(Assembly assembly)
		{
			Assembly assembly2 = Assembly.GetAssembly(typeof(MissionMethod));
			if (assembly == assembly2)
			{
				return true;
			}
			AssemblyName[] referencedAssemblies = assembly.GetReferencedAssemblies();
			for (int i = 0; i < referencedAssemblies.Length; i++)
			{
				if (referencedAssemblies[i].FullName == assembly2.FullName)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600276D RID: 10093 RVA: 0x00097544 File Offset: 0x00095744
		private void FindMissions()
		{
			MBDebug.Print("Searching Mission Methods", 0, Debug.DebugColor.White, 17592186044416UL);
			this._missionInfos = new List<MissionInfo>();
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			List<Type> list = new List<Type>();
			foreach (Assembly assembly in assemblies)
			{
				if (this.CheckAssemblyForMissionMethods(assembly))
				{
					foreach (Type type in assembly.GetTypesSafe(null))
					{
						object[] customAttributesSafe = type.GetCustomAttributesSafe(typeof(MissionManager), true);
						if (customAttributesSafe != null && customAttributesSafe.Length != 0)
						{
							list.Add(type);
						}
					}
				}
			}
			MBDebug.Print("Found " + list.Count + " mission managers", 0, Debug.DebugColor.White, 17592186044416UL);
			foreach (Type type2 in list)
			{
				foreach (MethodInfo methodInfo in type2.GetMethods(BindingFlags.Static | BindingFlags.Public))
				{
					object[] customAttributesSafe2 = methodInfo.GetCustomAttributesSafe(typeof(MissionMethod), true);
					if (customAttributesSafe2 != null && customAttributesSafe2.Length != 0)
					{
						MissionMethod missionMethod = customAttributesSafe2[0] as MissionMethod;
						MissionInfo missionInfo = new MissionInfo();
						missionInfo.Creator = methodInfo;
						missionInfo.Manager = type2;
						missionInfo.UsableByEditor = missionMethod.UsableByEditor;
						missionInfo.Name = methodInfo.Name;
						if (missionInfo.Name.StartsWith("Open"))
						{
							missionInfo.Name = missionInfo.Name.Substring(4);
						}
						if (missionInfo.Name.EndsWith("Mission"))
						{
							missionInfo.Name = missionInfo.Name.Substring(0, missionInfo.Name.Length - 7);
						}
						MissionInfo missionInfo2 = missionInfo;
						missionInfo2.Name = missionInfo2.Name + "[" + type2.Name + "]";
						this._missionInfos.Add(missionInfo);
					}
				}
			}
			MBDebug.Print("Found " + this._missionInfos.Count + " missions", 0, Debug.DebugColor.White, 17592186044416UL);
		}

		// Token: 0x0600276E RID: 10094 RVA: 0x000977CC File Offset: 0x000959CC
		[MBCallback]
		internal string GetMissionControllerClassNames()
		{
			string text = "";
			for (int i = 0; i < this._missionInfos.Count; i++)
			{
				MissionInfo missionInfo = this._missionInfos[i];
				if (missionInfo.UsableByEditor)
				{
					text += missionInfo.Name;
					if (i + 1 != this._missionInfos.Count)
					{
						text += " ";
					}
				}
			}
			return text;
		}

		// Token: 0x0600276F RID: 10095 RVA: 0x00097834 File Offset: 0x00095A34
		private void LoadPlatformServices()
		{
			IPlatformServices platformServices = null;
			Assembly assembly = null;
			PlatformInitParams platformInitParams = new PlatformInitParams();
			if (ApplicationPlatform.CurrentPlatform == Platform.WindowsSteam)
			{
				assembly = AssemblyLoader.LoadFrom(ManagedDllFolder.Name + "TaleWorlds.PlatformService.Steam.dll", true);
			}
			else if (ApplicationPlatform.CurrentPlatform == Platform.WindowsEpic)
			{
				assembly = AssemblyLoader.LoadFrom(ManagedDllFolder.Name + "TaleWorlds.PlatformService.Epic.dll", true);
				platformInitParams.Add("ExchangeCode", this.StartupInfo.EpicExchangeCode);
			}
			else if (ApplicationPlatform.CurrentPlatform == Platform.WindowsGOG)
			{
				assembly = AssemblyLoader.LoadFrom(ManagedDllFolder.Name + "TaleWorlds.PlatformService.GOG.dll", true);
				platformInitParams.Add("AchievementDataXmlPath", ModuleHelper.GetModuleFullPath("Native") + "ModuleData/AchievementData/gog_achievement_data.xml");
			}
			else if (ApplicationPlatform.CurrentPlatform == Platform.GDKDesktop || ApplicationPlatform.CurrentPlatform == Platform.Durango)
			{
				assembly = AssemblyLoader.LoadFrom(ManagedDllFolder.Name + "TaleWorlds.PlatformService.GDK.dll", true);
			}
			else if (ApplicationPlatform.CurrentPlatform == Platform.Orbis)
			{
				assembly = AssemblyLoader.LoadFrom(ManagedDllFolder.Name + "TaleWorlds.PlatformService.PS.dll", true);
				platformInitParams.Add("AchievementDataXmlPath", ModuleHelper.GetModuleFullPath("Native") + "ModuleData/AchievementData/ps_achievement_data.xml");
			}
			else if (ApplicationPlatform.CurrentPlatform == Platform.WindowsNoPlatform)
			{
				string userName = "TestUser" + DateTime.Now.Ticks % 10000L;
				if (!string.IsNullOrEmpty(this.StartupInfo.OverridenUserName))
				{
					userName = this.StartupInfo.OverridenUserName;
				}
				platformServices = new TestPlatformServices(userName);
			}
			if (assembly != null)
			{
				List<Type> typesSafe = assembly.GetTypesSafe(null);
				Type type = null;
				foreach (Type type2 in typesSafe)
				{
					if (type2.GetInterfaces().Contains(typeof(IPlatformServices)))
					{
						type = type2;
						break;
					}
				}
				platformServices = (IPlatformServices)type.GetConstructor(new Type[]
				{
					typeof(PlatformInitParams)
				}).Invoke(new object[]
				{
					platformInitParams
				});
			}
			if (platformServices != null)
			{
				PlatformServices.Setup(platformServices);
				PlatformServices.OnSessionInvitationAccepted = (Action<SessionInvitationType>)Delegate.Combine(PlatformServices.OnSessionInvitationAccepted, new Action<SessionInvitationType>(this.OnSessionInvitationAccepted));
				PlatformServices.OnPlatformRequestedMultiplayer = (Action)Delegate.Combine(PlatformServices.OnPlatformRequestedMultiplayer, new Action(this.OnPlatformRequestedMultiplayer));
				BannerlordFriendListService bannerlordFriendListService = new BannerlordFriendListService();
				ClanFriendListService clanFriendListService = new ClanFriendListService();
				RecentPlayersFriendListService recentPlayersFriendListService = new RecentPlayersFriendListService();
				PlatformServices.Initialize(new IFriendListService[]
				{
					bannerlordFriendListService,
					clanFriendListService,
					recentPlayersFriendListService
				});
				AchievementManager.AchievementService = platformServices.GetAchievementService();
				ActivityManager.ActivityService = platformServices.GetActivityService();
			}
		}

		// Token: 0x06002770 RID: 10096 RVA: 0x00097AD4 File Offset: 0x00095CD4
		private void OnSessionInvitationAccepted(SessionInvitationType targetGameType)
		{
			if (targetGameType == SessionInvitationType.Multiplayer)
			{
				if (this.IsOnlyCoreContentEnabled)
				{
					PlatformServices.OnSessionInvitationHandled();
					return;
				}
				this.JobManager.AddJob(new OnSessionInvitationAcceptedJob(targetGameType));
			}
		}

		// Token: 0x06002771 RID: 10097 RVA: 0x00097AF9 File Offset: 0x00095CF9
		private void OnPlatformRequestedMultiplayer()
		{
			if (this.IsOnlyCoreContentEnabled)
			{
				PlatformServices.OnPlatformMultiplayerRequestHandled();
				return;
			}
			this.JobManager.AddJob(new OnPlatformRequestedMultiplayerJob());
		}

		// Token: 0x06002772 RID: 10098 RVA: 0x00097B1C File Offset: 0x00095D1C
		private void LoadSubModules()
		{
			MBDebug.Print("Loading submodules...", 0, Debug.DebugColor.White, 17592186044416UL);
			List<ModuleInfo> list = new List<ModuleInfo>();
			string[] modulesNames = Utilities.GetModulesNames();
			for (int i = 0; i < modulesNames.Length; i++)
			{
				ModuleInfo moduleInfo = ModuleHelper.GetModuleInfo(modulesNames[i]);
				if (moduleInfo != null)
				{
					list.Add(moduleInfo);
					XmlResource.GetMbprojxmls(modulesNames[i]);
					XmlResource.GetXmlListAndApply(modulesNames[i]);
				}
			}
			string configName = Common.ConfigName;
			foreach (ModuleInfo moduleInfo2 in list)
			{
				foreach (SubModuleInfo subModuleInfo in moduleInfo2.SubModules)
				{
					if (this.CheckIfSubmoduleCanBeLoadable(subModuleInfo) && !this._loadedSubmoduleTypes.ContainsKey(subModuleInfo.SubModuleClassType))
					{
						string path = Path.Combine(moduleInfo2.FolderPath, "bin", configName);
						string text = Path.Combine(path, subModuleInfo.DLLName);
						string text2 = ManagedDllFolder.Name + subModuleInfo.DLLName;
						foreach (string text3 in subModuleInfo.Assemblies)
						{
							string text4 = Path.Combine(path, text3);
							string assemblyFile = ManagedDllFolder.Name + text3;
							if (File.Exists(text4))
							{
								AssemblyLoader.LoadFrom(text4, true);
							}
							else
							{
								AssemblyLoader.LoadFrom(assemblyFile, true);
							}
						}
						if (File.Exists(text))
						{
							Assembly subModuleAssembly = AssemblyLoader.LoadFrom(text, true);
							this.AddSubModule(subModuleAssembly, subModuleInfo.SubModuleClassType);
						}
						else if (File.Exists(text2))
						{
							Assembly subModuleAssembly2 = AssemblyLoader.LoadFrom(text2, true);
							this.AddSubModule(subModuleAssembly2, subModuleInfo.SubModuleClassType);
						}
						else
						{
							string lpText = "Cannot find: " + text;
							string lpCaption = "Error";
							Debug.ShowMessageBox(lpText, lpCaption, 4U);
						}
					}
				}
			}
			this.InitializeSubModules();
		}

		// Token: 0x06002773 RID: 10099 RVA: 0x00097D68 File Offset: 0x00095F68
		public bool CheckIfSubmoduleCanBeLoadable(SubModuleInfo subModuleInfo)
		{
			if (subModuleInfo.Tags.Count > 0)
			{
				foreach (Tuple<SubModuleInfo.SubModuleTags, string> tuple in subModuleInfo.Tags)
				{
					if (!this.GetSubModuleValiditiy(tuple.Item1, tuple.Item2))
					{
						return false;
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x06002774 RID: 10100 RVA: 0x00097DE0 File Offset: 0x00095FE0
		private bool GetSubModuleValiditiy(SubModuleInfo.SubModuleTags tag, string value)
		{
			switch (tag)
			{
			case SubModuleInfo.SubModuleTags.RejectedPlatform:
			{
				Platform platform;
				if (Enum.TryParse<Platform>(value, out platform))
				{
					return ApplicationPlatform.CurrentPlatform != platform;
				}
				break;
			}
			case SubModuleInfo.SubModuleTags.ExclusivePlatform:
			{
				Platform platform;
				if (Enum.TryParse<Platform>(value, out platform))
				{
					return ApplicationPlatform.CurrentPlatform == platform;
				}
				break;
			}
			case SubModuleInfo.SubModuleTags.DedicatedServerType:
			{
				string a = value.ToLower();
				if (a == "none")
				{
					return this.StartupInfo.DedicatedServerType == DedicatedServerType.None;
				}
				if (a == "both" || a == "all")
				{
					return this.StartupInfo.DedicatedServerType != DedicatedServerType.None;
				}
				if (a == "custom")
				{
					return this.StartupInfo.DedicatedServerType == DedicatedServerType.Custom;
				}
				if (a == "matchmaker")
				{
					return this.StartupInfo.DedicatedServerType == DedicatedServerType.Matchmaker;
				}
				if (a == "community")
				{
					return this.StartupInfo.DedicatedServerType == DedicatedServerType.Community;
				}
				break;
			}
			case SubModuleInfo.SubModuleTags.IsNoRenderModeElement:
				return value.Equals("false");
			case SubModuleInfo.SubModuleTags.DependantRuntimeLibrary:
			{
				Runtime runtime;
				if (Enum.TryParse<Runtime>(value, out runtime))
				{
					return ApplicationPlatform.CurrentRuntimeLibrary == runtime;
				}
				break;
			}
			case SubModuleInfo.SubModuleTags.PlayerHostedDedicatedServer:
			{
				string text = value.ToLower();
				if (this.StartupInfo.PlayerHostedDedicatedServer)
				{
					return text.Equals("true");
				}
				return text.Equals("false");
			}
			}
			return true;
		}

		// Token: 0x06002775 RID: 10101 RVA: 0x00097F38 File Offset: 0x00096138
		[MBCallback]
		internal static void MBThrowException()
		{
			Debug.FailedAssert("MBThrowException", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Module.cs", "MBThrowException", 1424);
		}

		// Token: 0x06002776 RID: 10102 RVA: 0x00097F53 File Offset: 0x00096153
		[MBCallback]
		internal void OnEnterEditMode(bool isFirstTime)
		{
		}

		// Token: 0x06002777 RID: 10103 RVA: 0x00097F57 File Offset: 0x00096157
		[MBCallback]
		internal static Module GetInstance()
		{
			return Module.CurrentModule;
		}

		// Token: 0x06002778 RID: 10104 RVA: 0x00097F5E File Offset: 0x0009615E
		[MBCallback]
		internal static string GetGameStatus()
		{
			if (TestCommonBase.BaseInstance != null)
			{
				return TestCommonBase.BaseInstance.GetGameStatus();
			}
			return "";
		}

		// Token: 0x06002779 RID: 10105 RVA: 0x00097F78 File Offset: 0x00096178
		private void FinalizeModule()
		{
			if (Game.Current != null)
			{
				Game.Current.OnFinalize();
			}
			if (TestCommonBase.BaseInstance != null)
			{
				TestCommonBase.BaseInstance.OnFinalize();
			}
			this._testContext.FinalizeContext();
			MBInformationManager.Clear();
			InformationManager.Clear();
			ScreenManager.OnFinalize();
			BannerlordConfig.Save();
			this.FinalizeSubModules();
			IPlatformServices instance = PlatformServices.Instance;
			if (instance != null)
			{
				instance.Terminate();
			}
			Common.MemoryCleanupGC(false);
			GC.WaitForPendingFinalizers();
		}

		// Token: 0x0600277A RID: 10106 RVA: 0x00097FE8 File Offset: 0x000961E8
		internal static void FinalizeCurrentModule()
		{
			Module.CurrentModule.FinalizeModule();
			Module.CurrentModule = null;
		}

		// Token: 0x0600277B RID: 10107 RVA: 0x00097FFA File Offset: 0x000961FA
		[MBCallback]
		internal void SetLoadingFinished()
		{
			this.LoadingFinished = true;
		}

		// Token: 0x0600277C RID: 10108 RVA: 0x00098003 File Offset: 0x00096203
		[MBCallback]
		internal void OnCloseSceneEditorPresentation()
		{
			GameStateManager.Current.PopState(0);
		}

		// Token: 0x0600277D RID: 10109 RVA: 0x00098010 File Offset: 0x00096210
		[MBCallback]
		internal void OnSceneEditorModeOver()
		{
			GameStateManager.Current.PopState(0);
		}

		// Token: 0x0600277E RID: 10110 RVA: 0x00098020 File Offset: 0x00096220
		private void OnConfigChanged()
		{
			foreach (MBSubModuleBase mbsubModuleBase in this.SubModules)
			{
				mbsubModuleBase.OnConfigChanged();
			}
		}

		// Token: 0x0600277F RID: 10111 RVA: 0x00098070 File Offset: 0x00096270
		private void OnConstrainedStateChange(bool isConstrained)
		{
			if (!isConstrained)
			{
				PlatformServices.Instance.OnFocusGained();
			}
		}

		// Token: 0x06002780 RID: 10112 RVA: 0x0009807F File Offset: 0x0009627F
		private void OnFocusGained()
		{
			PlatformServices.Instance.OnFocusGained();
		}

		// Token: 0x06002781 RID: 10113 RVA: 0x0009808B File Offset: 0x0009628B
		private void OnTextEnteredFromPlatform(string text)
		{
			ScreenManager.OnOnscreenKeyboardDone(text);
		}

		// Token: 0x06002782 RID: 10114 RVA: 0x00098093 File Offset: 0x00096293
		[MBCallback]
		internal void OnSkinsXMLHasChanged()
		{
			if (this.SkinsXMLHasChanged != null)
			{
				this.SkinsXMLHasChanged();
			}
		}

		// Token: 0x14000077 RID: 119
		// (add) Token: 0x06002783 RID: 10115 RVA: 0x000980A8 File Offset: 0x000962A8
		// (remove) Token: 0x06002784 RID: 10116 RVA: 0x000980E0 File Offset: 0x000962E0
		public event Action SkinsXMLHasChanged;

		// Token: 0x06002785 RID: 10117 RVA: 0x00098115 File Offset: 0x00096315
		[MBCallback]
		internal void OnImguiProfilerTick()
		{
			if (this.ImguiProfilerTick != null)
			{
				this.ImguiProfilerTick();
			}
		}

		// Token: 0x06002786 RID: 10118 RVA: 0x0009812C File Offset: 0x0009632C
		[MBCallback]
		internal static string CreateProcessedSkinsXMLForNative(out string baseSkinsXmlPath)
		{
			List<string> list;
			XDocument xdocument = MBObjectManager.ToXDocument(MBObjectManager.GetMergedXmlForNative("soln_skins", out list));
			for (int i = 0; i < xdocument.Descendants("race").Count<XElement>(); i++)
			{
				for (int j = i + 1; j < xdocument.Descendants("race").Count<XElement>(); j++)
				{
					if (xdocument.Descendants("race").ElementAt(i).FirstAttribute.ToString() == xdocument.Descendants("race").ElementAt(j).FirstAttribute.ToString())
					{
						xdocument.Descendants("race").ElementAt(i).Add(xdocument.Descendants("race").ElementAt(j).Descendants());
						xdocument.Descendants("race").ElementAt(j).Remove();
						j--;
					}
				}
			}
			XmlNode xmlNode = MBObjectManager.ToXmlDocument(xdocument);
			StringWriter stringWriter = new StringWriter();
			XmlTextWriter w = new XmlTextWriter(stringWriter);
			xmlNode.WriteTo(w);
			baseSkinsXmlPath = list[0];
			return stringWriter.ToString();
		}

		// Token: 0x06002787 RID: 10119 RVA: 0x00098270 File Offset: 0x00096470
		[MBCallback]
		internal static string CreateProcessedActionSetsXMLForNative()
		{
			List<string> list;
			XmlDocument xmlDocument = MBObjectManager.GetMergedXmlForNative("soln_action_sets", out list);
			Dictionary<string, XElement> dictionary = new Dictionary<string, XElement>();
			XDocument xdocument = MBObjectManager.ToXDocument(xmlDocument);
			IEnumerable<XElement> source = xdocument.Descendants("action_set");
			for (int i = 0; i < source.Count<XElement>(); i++)
			{
				XElement xelement = source.ElementAt(i);
				string key = xelement.FirstAttribute.ToString();
				if (dictionary.ContainsKey(key))
				{
					dictionary[key].Add(xelement.Descendants());
					xelement.Remove();
					i--;
				}
				else
				{
					dictionary.Add(key, xelement);
				}
			}
			xmlDocument = MBObjectManager.ToXmlDocument(xdocument);
			StringWriter stringWriter = new StringWriter();
			XmlTextWriter w = new XmlTextWriter(stringWriter);
			xmlDocument.WriteTo(w);
			return stringWriter.ToString();
		}

		// Token: 0x06002788 RID: 10120 RVA: 0x00098330 File Offset: 0x00096530
		[MBCallback]
		internal static string CreateProcessedActionTypesXMLForNative()
		{
			List<string> list;
			XmlDocument mergedXmlForNative = MBObjectManager.GetMergedXmlForNative("soln_action_types", out list);
			StringWriter stringWriter = new StringWriter();
			XmlTextWriter w = new XmlTextWriter(stringWriter);
			mergedXmlForNative.WriteTo(w);
			return stringWriter.ToString();
		}

		// Token: 0x06002789 RID: 10121 RVA: 0x00098364 File Offset: 0x00096564
		[MBCallback]
		internal static string CreateProcessedAnimationsXMLForNative(out string animationsXmlPaths)
		{
			List<string> list;
			XmlNode mergedXmlForNative = MBObjectManager.GetMergedXmlForNative("soln_animations", out list);
			StringWriter stringWriter = new StringWriter();
			XmlTextWriter w = new XmlTextWriter(stringWriter);
			mergedXmlForNative.WriteTo(w);
			animationsXmlPaths = "";
			for (int i = 0; i < list.Count; i++)
			{
				animationsXmlPaths += list[i];
				if (i != list.Count - 1)
				{
					animationsXmlPaths += "\n";
				}
			}
			return stringWriter.ToString();
		}

		// Token: 0x0600278A RID: 10122 RVA: 0x000983D8 File Offset: 0x000965D8
		[MBCallback]
		internal static string CreateProcessedVoiceDefinitionsXMLForNative()
		{
			List<string> list;
			XmlDocument xmlDocument = MBObjectManager.GetMergedXmlForNative("soln_voice_definitions", out list);
			XDocument xdocument = MBObjectManager.ToXDocument(xmlDocument);
			XElement xelement = xdocument.Descendants("voice_type_declarations").First<XElement>();
			for (int i = 1; i < xdocument.Descendants("voice_type_declarations").Count<XElement>(); i++)
			{
				xelement.Add(xdocument.Descendants("voice_type_declarations").ElementAt(i).Descendants());
				xdocument.Descendants("voice_type_declarations").ElementAt(i).Remove();
				i--;
			}
			for (int j = 0; j < xdocument.Descendants("voice_definition").Count<XElement>(); j++)
			{
				for (int k = j + 1; k < xdocument.Descendants("voice_definition").Count<XElement>(); k++)
				{
					if (xdocument.Descendants("voice_definition").ElementAt(j).FirstAttribute.ToString() == xdocument.Descendants("voice_definition").ElementAt(k).FirstAttribute.ToString())
					{
						xdocument.Descendants("voice_definition").ElementAt(j).Add(xdocument.Descendants("voice_definition").ElementAt(k).Descendants());
						xdocument.Descendants("voice_definition").ElementAt(k).Remove();
						k--;
					}
				}
			}
			xmlDocument = MBObjectManager.ToXmlDocument(xdocument);
			StringWriter stringWriter = new StringWriter();
			XmlTextWriter w = new XmlTextWriter(stringWriter);
			xmlDocument.WriteTo(w);
			return stringWriter.ToString();
		}

		// Token: 0x0600278B RID: 10123 RVA: 0x00098594 File Offset: 0x00096794
		[MBCallback]
		internal static string CreateProcessedModuleDataXMLForNative(string xmlType)
		{
			List<string> list;
			XmlDocument xmlDocument = MBObjectManager.GetMergedXmlForNative("soln_" + xmlType, out list);
			if (xmlType == "full_movement_sets")
			{
				XDocument xdocument = MBObjectManager.ToXDocument(xmlDocument);
				for (int i = 0; i < xdocument.Descendants("full_movement_set").Count<XElement>(); i++)
				{
					for (int j = i + 1; j < xdocument.Descendants("full_movement_set").Count<XElement>(); j++)
					{
						if (xdocument.Descendants("full_movement_set").ElementAt(i).FirstAttribute.ToString() == xdocument.Descendants("full_movement_set").ElementAt(j).FirstAttribute.ToString())
						{
							xdocument.Descendants("full_movement_set").ElementAt(i).Add(xdocument.Descendants("full_movement_set").ElementAt(j).Descendants());
							xdocument.Descendants("full_movement_set").ElementAt(j).Remove();
							j--;
						}
					}
				}
				xmlDocument = MBObjectManager.ToXmlDocument(xdocument);
			}
			StringWriter stringWriter = new StringWriter();
			XmlTextWriter w = new XmlTextWriter(stringWriter);
			xmlDocument.WriteTo(w);
			return stringWriter.ToString();
		}

		// Token: 0x14000078 RID: 120
		// (add) Token: 0x0600278C RID: 10124 RVA: 0x000986E4 File Offset: 0x000968E4
		// (remove) Token: 0x0600278D RID: 10125 RVA: 0x0009871C File Offset: 0x0009691C
		public event Action ImguiProfilerTick;

		// Token: 0x0600278E RID: 10126 RVA: 0x00098751 File Offset: 0x00096951
		public void ClearStateOptions()
		{
			this._initialStateOptions.Clear();
		}

		// Token: 0x0600278F RID: 10127 RVA: 0x0009875E File Offset: 0x0009695E
		public void AddInitialStateOption(InitialStateOption initialStateOption)
		{
			this._initialStateOptions.Add(initialStateOption);
		}

		// Token: 0x06002790 RID: 10128 RVA: 0x0009876C File Offset: 0x0009696C
		public IEnumerable<InitialStateOption> GetInitialStateOptions()
		{
			return from s in this._initialStateOptions
			orderby s.OrderIndex
			select s;
		}

		// Token: 0x06002791 RID: 10129 RVA: 0x00098798 File Offset: 0x00096998
		public InitialStateOption GetInitialStateOptionWithId(string id)
		{
			foreach (InitialStateOption initialStateOption in this._initialStateOptions)
			{
				if (initialStateOption.Id == id)
				{
					return initialStateOption;
				}
			}
			return null;
		}

		// Token: 0x06002792 RID: 10130 RVA: 0x000987FC File Offset: 0x000969FC
		public void ExecuteInitialStateOptionWithId(string id)
		{
			InitialStateOption initialStateOptionWithId = this.GetInitialStateOptionWithId(id);
			if (initialStateOptionWithId != null)
			{
				initialStateOptionWithId.DoAction();
			}
		}

		// Token: 0x06002793 RID: 10131 RVA: 0x0009881A File Offset: 0x00096A1A
		void IGameStateManagerOwner.OnStateStackEmpty()
		{
		}

		// Token: 0x06002794 RID: 10132 RVA: 0x0009881C File Offset: 0x00096A1C
		void IGameStateManagerOwner.OnStateChanged(GameState oldState)
		{
		}

		// Token: 0x06002795 RID: 10133 RVA: 0x0009881E File Offset: 0x00096A1E
		public void SetEditorMissionTester(IEditorMissionTester editorMissionTester)
		{
			this._editorMissionTester = editorMissionTester;
		}

		// Token: 0x06002796 RID: 10134 RVA: 0x00098827 File Offset: 0x00096A27
		[MBCallback]
		internal void StartMissionForEditor(string missionName, string sceneName, string levels)
		{
			if (this._editorMissionTester != null)
			{
				this._editorMissionTester.StartMissionForEditor(missionName, sceneName, levels);
			}
		}

		// Token: 0x06002797 RID: 10135 RVA: 0x0009883F File Offset: 0x00096A3F
		[MBCallback]
		internal void StartMissionForReplayEditor(string missionName, string sceneName, string levels, string fileName, bool record, float startTime, float endTime)
		{
			if (this._editorMissionTester != null)
			{
				this._editorMissionTester.StartMissionForReplayEditor(missionName, sceneName, levels, fileName, record, startTime, endTime);
			}
		}

		// Token: 0x06002798 RID: 10136 RVA: 0x00098860 File Offset: 0x00096A60
		public void StartMissionForEditorAux(string missionName, string sceneName, string levels, bool forReplay, string replayFileName, bool isRecord)
		{
			GameStateManager.Current = Game.Current.GameStateManager;
			this.ReturnToEditorState = true;
			MissionInfo missionInfo = this._missionInfos.Find((MissionInfo mi) => mi.Name == missionName);
			if (missionInfo == null)
			{
				missionInfo = this._missionInfos.Find((MissionInfo mi) => mi.Name.Contains(missionName));
			}
			if (forReplay)
			{
				missionInfo.Creator.Invoke(null, new object[]
				{
					replayFileName,
					isRecord
				});
				return;
			}
			missionInfo.Creator.Invoke(null, new object[]
			{
				sceneName,
				levels
			});
		}

		// Token: 0x06002799 RID: 10137 RVA: 0x00098905 File Offset: 0x00096B05
		private void FillMultiplayerGameTypes()
		{
			this._multiplayerGameModesWithNames = new Dictionary<string, MultiplayerGameMode>();
			this._multiplayerGameTypes = new MBList<MultiplayerGameTypeInfo>();
		}

		// Token: 0x0600279A RID: 10138 RVA: 0x00098920 File Offset: 0x00096B20
		public MultiplayerGameMode GetMultiplayerGameMode(string gameType)
		{
			MultiplayerGameMode result;
			if (this._multiplayerGameModesWithNames.TryGetValue(gameType, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x0600279B RID: 10139 RVA: 0x00098940 File Offset: 0x00096B40
		public void AddMultiplayerGameMode(MultiplayerGameMode multiplayerGameMode)
		{
			this._multiplayerGameModesWithNames.Add(multiplayerGameMode.Name, multiplayerGameMode);
			this._multiplayerGameTypes.Add(new MultiplayerGameTypeInfo("Native", multiplayerGameMode.Name));
		}

		// Token: 0x0600279C RID: 10140 RVA: 0x0009896F File Offset: 0x00096B6F
		public MBReadOnlyList<MultiplayerGameTypeInfo> GetMultiplayerGameTypes()
		{
			return this._multiplayerGameTypes;
		}

		// Token: 0x0600279D RID: 10141 RVA: 0x00098978 File Offset: 0x00096B78
		public bool StartMultiplayerGame(string multiplayerGameType, string scene)
		{
			MultiplayerGameMode multiplayerGameMode;
			if (this._multiplayerGameModesWithNames.TryGetValue(multiplayerGameType, out multiplayerGameMode))
			{
				multiplayerGameMode.StartMultiplayerGame(scene);
				return true;
			}
			return false;
		}

		// Token: 0x0600279E RID: 10142 RVA: 0x000989A0 File Offset: 0x00096BA0
		public async void ShutDownWithDelay(string reason, int seconds)
		{
			if (!this._isShuttingDown)
			{
				this._isShuttingDown = true;
				for (int i = 0; i < seconds; i++)
				{
					int num = seconds - i;
					string text = string.Concat(new object[]
					{
						"Shutting down in ",
						num,
						" seconds with reason '",
						reason,
						"'"
					});
					Debug.Print(text, 0, Debug.DebugColor.White, 17592186044416UL);
					Console.WriteLine(text);
					await Task.Delay(1000);
				}
				if (Game.Current != null)
				{
					Debug.Print("Active game exist during ShutDownWithDelay", 0, Debug.DebugColor.White, 17592186044416UL);
					MBGameManager.EndGame();
				}
				Utilities.QuitGame();
			}
		}

		// Token: 0x04000E96 RID: 3734
		private TestContext _testContext;

		// Token: 0x04000E97 RID: 3735
		private List<MissionInfo> _missionInfos;

		// Token: 0x04000E98 RID: 3736
		private Dictionary<string, Type> _loadedSubmoduleTypes;

		// Token: 0x04000E99 RID: 3737
		private readonly MBList<MBSubModuleBase> _submodules;

		// Token: 0x04000E9A RID: 3738
		private SingleThreadedSynchronizationContext _synchronizationContext;

		// Token: 0x04000EA0 RID: 3744
		private bool _enableCoreContentOnReturnToRoot;

		// Token: 0x04000EA3 RID: 3747
		private bool _splashScreenPlayed;

		// Token: 0x04000EA6 RID: 3750
		private List<InitialStateOption> _initialStateOptions;

		// Token: 0x04000EA7 RID: 3751
		private IEditorMissionTester _editorMissionTester;

		// Token: 0x04000EA8 RID: 3752
		private Dictionary<string, MultiplayerGameMode> _multiplayerGameModesWithNames;

		// Token: 0x04000EA9 RID: 3753
		private MBList<MultiplayerGameTypeInfo> _multiplayerGameTypes = new MBList<MultiplayerGameTypeInfo>();

		// Token: 0x04000EAA RID: 3754
		private bool _isShuttingDown;

		// Token: 0x0200058A RID: 1418
		public enum XmlInformationType
		{
			// Token: 0x04001D80 RID: 7552
			Parameters,
			// Token: 0x04001D81 RID: 7553
			MbObjectType
		}

		// Token: 0x0200058B RID: 1419
		private enum StartupType
		{
			// Token: 0x04001D83 RID: 7555
			None,
			// Token: 0x04001D84 RID: 7556
			TestMode,
			// Token: 0x04001D85 RID: 7557
			GameServer,
			// Token: 0x04001D86 RID: 7558
			Singleplayer,
			// Token: 0x04001D87 RID: 7559
			Multiplayer,
			// Token: 0x04001D88 RID: 7560
			Count
		}
	}
}
