using System;
using System.Collections.Generic;
using System.IO;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Engine.InputSystem;
using TaleWorlds.Engine.Options;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.ModuleManager;
using TaleWorlds.MountAndBlade.GameKeyCategory;
using TaleWorlds.MountAndBlade.View.Screens;
using TaleWorlds.MountAndBlade.View.Tableaus;
using TaleWorlds.ScreenSystem;

namespace TaleWorlds.MountAndBlade.View
{
	// Token: 0x0200001E RID: 30
	public class ViewSubModule : MBSubModuleBase
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00007658 File Offset: 0x00005858
		// (set) Token: 0x060000CD RID: 205 RVA: 0x00007664 File Offset: 0x00005864
		public static Dictionary<Tuple<Material, BannerCode>, Material> BannerTexturedMaterialCache
		{
			get
			{
				return ViewSubModule._instance._bannerTexturedMaterialCache;
			}
			set
			{
				ViewSubModule._instance._bannerTexturedMaterialCache = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00007671 File Offset: 0x00005871
		public static GameStateScreenManager GameStateScreenManager
		{
			get
			{
				return ViewSubModule._instance._gameStateScreenManager;
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00007680 File Offset: 0x00005880
		private void InitializeHotKeyManager(bool loadKeys)
		{
			string fileName = "BannerlordGameKeys.xml";
			HotKeyManager.Initialize(new PlatformFilePath(EngineFilePaths.ConfigsPath, fileName), !ScreenManager.IsEnterButtonRDown);
			HotKeyManager.RegisterInitialContexts(new List<GameKeyContext>
			{
				new GenericGameKeyContext(),
				new GenericCampaignPanelsGameKeyCategory("GenericCampaignPanelsGameKeyCategory"),
				new GenericPanelGameKeyCategory("GenericPanelGameKeyCategory"),
				new ArmyManagementHotkeyCategory(),
				new BoardGameHotkeyCategory(),
				new ChatLogHotKeyCategory(),
				new CombatHotKeyCategory(),
				new CraftingHotkeyCategory(),
				new FaceGenHotkeyCategory(),
				new InventoryHotKeyCategory(),
				new PartyHotKeyCategory(),
				new MapHotKeyCategory(),
				new MapNotificationHotKeyCategory(),
				new MissionOrderHotkeyCategory(),
				new MultiplayerHotkeyCategory(),
				new ScoreboardHotKeyCategory(),
				new ConversationHotKeyCategory(),
				new CheatsHotKeyCategory(),
				new PhotoModeHotKeyCategory(),
				new PollHotkeyCategory()
			}, loadKeys);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x0000779C File Offset: 0x0000599C
		private void InitializeBannerVisualManager()
		{
			if (BannerManager.Instance == null)
			{
				BannerManager.Initialize();
				BannerManager.Instance.LoadBannerIcons(ModuleHelper.GetModuleFullPath("Native") + "ModuleData/banner_icons.xml");
				string[] modulesNames = Utilities.GetModulesNames();
				for (int i = 0; i < modulesNames.Length; i++)
				{
					ModuleInfo moduleInfo = ModuleHelper.GetModuleInfo(modulesNames[i]);
					if (moduleInfo != null && !moduleInfo.IsNative)
					{
						string text = moduleInfo.FolderPath + "/ModuleData/banner_icons.xml";
						if (File.Exists(text))
						{
							BannerManager.Instance.LoadBannerIcons(text);
						}
					}
				}
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00007820 File Offset: 0x00005A20
		protected override void OnSubModuleLoad()
		{
			base.OnSubModuleLoad();
			ViewSubModule._instance = this;
			this.InitializeHotKeyManager(false);
			this.InitializeBannerVisualManager();
			CraftedDataViewManager.Initialize();
			ScreenManager.OnPushScreen += this.OnScreenManagerPushScreen;
			this._gameStateScreenManager = new GameStateScreenManager();
			Module.CurrentModule.GlobalGameStateManager.RegisterListener(this._gameStateScreenManager);
			MBMusicManager.Create();
			TextObject coreContentDisabledReason = new TextObject("{=V8BXjyYq}Disabled during installation.", null);
			if (Utilities.EditModeEnabled)
			{
				Module.CurrentModule.AddInitialStateOption(new InitialStateOption("Editor", new TextObject("{=bUh0x6rA}Editor", null), -1, delegate()
				{
					MBInitialScreenBase.OnEditModeEnterPress();
				}, () => new ValueTuple<bool, TextObject>(Module.CurrentModule.IsOnlyCoreContentEnabled, coreContentDisabledReason), null));
			}
			Module.CurrentModule.AddInitialStateOption(new InitialStateOption("Options", new TextObject("{=NqarFr4P}Options", null), 9998, delegate()
			{
				ScreenManager.PushScreen(ViewCreator.CreateOptionsScreen(true));
			}, () => new ValueTuple<bool, TextObject>(false, TextObject.Empty), null));
			Module.CurrentModule.AddInitialStateOption(new InitialStateOption("Credits", new TextObject("{=ODQmOrIw}Credits", null), 9999, delegate()
			{
				ScreenManager.PushScreen(ViewCreator.CreateCreditsScreen());
			}, () => new ValueTuple<bool, TextObject>(false, TextObject.Empty), null));
			Module.CurrentModule.AddInitialStateOption(new InitialStateOption("Exit", new TextObject("{=YbpzLHzk}Exit Game", null), 10000, delegate()
			{
				MBInitialScreenBase.DoExitButtonAction();
			}, () => new ValueTuple<bool, TextObject>(Module.CurrentModule.IsOnlyCoreContentEnabled, coreContentDisabledReason), null));
			Module.CurrentModule.ImguiProfilerTick += this.OnImguiProfilerTick;
			Input.OnControllerTypeChanged = (Action<Input.ControllerTypes>)Delegate.Combine(Input.OnControllerTypeChanged, new Action<Input.ControllerTypes>(this.OnControllerTypeChanged));
			NativeOptions.OnNativeOptionChanged = (NativeOptions.OnNativeOptionChangedDelegate)Delegate.Combine(NativeOptions.OnNativeOptionChanged, new NativeOptions.OnNativeOptionChangedDelegate(this.OnNativeOptionChanged));
			ViewModel.CollectPropertiesAndMethods();
			HyperlinkTexts.IsPlayStationGamepadActive = new Func<bool>(this.GetIsPlaystationGamepadActive);
			EngineController.OnConstrainedStateChanged += this.OnConstrainedStateChange;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00007A7F File Offset: 0x00005C7F
		private void OnConstrainedStateChange(bool isConstrained)
		{
			ScreenManager.OnConstrainStateChanged(isConstrained);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00007A87 File Offset: 0x00005C87
		private bool GetIsPlaystationGamepadActive()
		{
			return Input.IsGamepadActive && (Input.ControllerType == Input.ControllerTypes.PlayStationDualSense || Input.ControllerType == Input.ControllerTypes.PlayStationDualShock);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00007AA4 File Offset: 0x00005CA4
		private void OnControllerTypeChanged(Input.ControllerTypes newType)
		{
			this.ReInitializeHotKeyManager();
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00007AAC File Offset: 0x00005CAC
		private void OnNativeOptionChanged(NativeOptions.NativeOptionsType changedNativeOptionsType)
		{
			if (changedNativeOptionsType == NativeOptions.NativeOptionsType.EnableTouchpadMouse)
			{
				this.ReInitializeHotKeyManager();
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00007AB9 File Offset: 0x00005CB9
		private void ReInitializeHotKeyManager()
		{
			this.InitializeHotKeyManager(true);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00007AC4 File Offset: 0x00005CC4
		protected override void OnSubModuleUnloaded()
		{
			ScreenManager.OnPushScreen -= this.OnScreenManagerPushScreen;
			NativeOptions.OnNativeOptionChanged = (NativeOptions.OnNativeOptionChangedDelegate)Delegate.Remove(NativeOptions.OnNativeOptionChanged, new NativeOptions.OnNativeOptionChangedDelegate(this.OnNativeOptionChanged));
			TableauCacheManager.ClearManager();
			BannerlordTableauManager.ClearManager();
			CraftedDataViewManager.Clear();
			Module.CurrentModule.ImguiProfilerTick -= this.OnImguiProfilerTick;
			Input.OnControllerTypeChanged = (Action<Input.ControllerTypes>)Delegate.Remove(Input.OnControllerTypeChanged, new Action<Input.ControllerTypes>(this.OnControllerTypeChanged));
			ViewSubModule._instance = null;
			EngineController.OnConstrainedStateChanged -= this.OnConstrainedStateChange;
			base.OnSubModuleUnloaded();
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00007B64 File Offset: 0x00005D64
		protected override void OnBeforeInitialModuleScreenSetAsRoot()
		{
			if (!this._initialized)
			{
				HotKeyManager.Load();
				BannerlordTableauManager.InitializeCharacterTableauRenderSystem();
				TableauCacheManager.InitializeManager();
				this._initialized = true;
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00007B84 File Offset: 0x00005D84
		protected override void OnApplicationTick(float dt)
		{
			base.OnApplicationTick(dt);
			if (Input.DebugInput.IsHotKeyPressed("ToggleUI"))
			{
				MBDebug.DisableUI(new List<string>());
			}
			HotKeyManager.Tick(dt);
			MBMusicManager mbmusicManager = MBMusicManager.Current;
			if (mbmusicManager != null)
			{
				mbmusicManager.Update(dt);
			}
			TableauCacheManager tableauCacheManager = TableauCacheManager.Current;
			if (tableauCacheManager == null)
			{
				return;
			}
			tableauCacheManager.Tick();
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00007BDA File Offset: 0x00005DDA
		protected override void AfterAsyncTickTick(float dt)
		{
			MBMusicManager mbmusicManager = MBMusicManager.Current;
			if (mbmusicManager == null)
			{
				return;
			}
			mbmusicManager.Update(dt);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00007BEC File Offset: 0x00005DEC
		protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
		{
			MissionWeapon.OnGetWeaponDataHandler = new MissionWeapon.OnGetWeaponDataDelegate(ItemCollectionElementViewExtensions.OnGetWeaponData);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00007BFF File Offset: 0x00005DFF
		public override void OnCampaignStart(Game game, object starterObject)
		{
			Game.Current.GameStateManager.RegisterListener(this._gameStateScreenManager);
			this._newGameInitialization = false;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00007C1E File Offset: 0x00005E1E
		public override void OnMultiplayerGameStart(Game game, object starterObject)
		{
			Game.Current.GameStateManager.RegisterListener(this._gameStateScreenManager);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00007C36 File Offset: 0x00005E36
		public override void OnGameLoaded(Game game, object initializerObject)
		{
			Game.Current.GameStateManager.RegisterListener(this._gameStateScreenManager);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00007C50 File Offset: 0x00005E50
		public override void OnGameInitializationFinished(Game game)
		{
			base.OnGameInitializationFinished(game);
			foreach (ItemObject itemObject in Game.Current.ObjectManager.GetObjectTypeList<ItemObject>())
			{
				if (itemObject.MultiMeshName != "")
				{
					MBUnusedResourceManager.SetMeshUsed(itemObject.MultiMeshName);
				}
				HorseComponent horseComponent = itemObject.HorseComponent;
				if (horseComponent != null)
				{
					foreach (KeyValuePair<string, bool> keyValuePair in horseComponent.AdditionalMeshesNameList)
					{
						MBUnusedResourceManager.SetMeshUsed(keyValuePair.Key);
					}
				}
				if (itemObject.PrimaryWeapon != null)
				{
					MBUnusedResourceManager.SetMeshUsed(itemObject.HolsterMeshName);
					MBUnusedResourceManager.SetMeshUsed(itemObject.HolsterWithWeaponMeshName);
					MBUnusedResourceManager.SetMeshUsed(itemObject.FlyingMeshName);
					MBUnusedResourceManager.SetBodyUsed(itemObject.BodyName);
					MBUnusedResourceManager.SetBodyUsed(itemObject.HolsterBodyName);
					MBUnusedResourceManager.SetBodyUsed(itemObject.CollisionBodyName);
				}
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00007D6C File Offset: 0x00005F6C
		public override void BeginGameStart(Game game)
		{
			base.BeginGameStart(game);
			Game.Current.BannerVisualCreator = new BannerVisualCreator();
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00007D84 File Offset: 0x00005F84
		public override bool DoLoading(Game game)
		{
			if (this._newGameInitialization)
			{
				return true;
			}
			this._newGameInitialization = true;
			return this._newGameInitialization;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00007D9D File Offset: 0x00005F9D
		public override void OnGameEnd(Game game)
		{
			MissionWeapon.OnGetWeaponDataHandler = null;
			CraftedDataViewManager.Clear();
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00007DAA File Offset: 0x00005FAA
		private void OnImguiProfilerTick()
		{
			TableauCacheManager.Current.OnImguiProfilerTick();
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00007DB6 File Offset: 0x00005FB6
		private void OnScreenManagerPushScreen(ScreenBase pushedScreen)
		{
		}

		// Token: 0x04000046 RID: 70
		private Dictionary<Tuple<Material, BannerCode>, Material> _bannerTexturedMaterialCache;

		// Token: 0x04000047 RID: 71
		private GameStateScreenManager _gameStateScreenManager;

		// Token: 0x04000048 RID: 72
		private bool _newGameInitialization;

		// Token: 0x04000049 RID: 73
		private static ViewSubModule _instance;

		// Token: 0x0400004A RID: 74
		private bool _initialized;
	}
}
