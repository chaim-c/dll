using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Core.ViewModelCollection.Information.RundownTooltip;
using TaleWorlds.Engine;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.Engine.Options;
using TaleWorlds.GauntletUI.ExtraWidgets;
using TaleWorlds.GauntletUI.GamepadNavigation;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.ModuleManager;
using TaleWorlds.MountAndBlade.GauntletUI.SceneNotification;
using TaleWorlds.MountAndBlade.GauntletUI.Widgets;
using TaleWorlds.ScreenSystem;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI
{
	// Token: 0x02000010 RID: 16
	public class GauntletUISubModule : MBSubModuleBase
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00005A84 File Offset: 0x00003C84
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00005A8B File Offset: 0x00003C8B
		public static GauntletUISubModule Instance { get; private set; }

		// Token: 0x06000082 RID: 130 RVA: 0x00005A9C File Offset: 0x00003C9C
		protected override void OnSubModuleLoad()
		{
			base.OnSubModuleLoad();
			ResourceDepot resourceDepot = new ResourceDepot();
			resourceDepot.AddLocation(BasePath.Name, "GUI/GauntletUI/");
			List<string> list = new List<string>();
			string[] modulesNames = Utilities.GetModulesNames();
			for (int i = 0; i < modulesNames.Length; i++)
			{
				ModuleInfo moduleInfo = ModuleHelper.GetModuleInfo(modulesNames[i]);
				if (moduleInfo != null)
				{
					string folderPath = moduleInfo.FolderPath;
					if (Directory.Exists(folderPath + "/GUI/"))
					{
						resourceDepot.AddLocation(folderPath, "/GUI/");
					}
					foreach (SubModuleInfo subModuleInfo in moduleInfo.SubModules)
					{
						if (subModuleInfo != null && subModuleInfo.DLLExists && !string.IsNullOrEmpty(subModuleInfo.DLLName))
						{
							list.Add(subModuleInfo.DLLName);
						}
					}
				}
			}
			resourceDepot.CollectResources();
			CustomWidgetManager.Initilize();
			BannerlordCustomWidgetManager.Initialize();
			UIResourceManager.Initialize(resourceDepot, list);
			UIResourceManager.WidgetFactory.GeneratedPrefabContext.CollectPrefabs();
			SpriteData spriteData = UIResourceManager.SpriteData;
			TwoDimensionEngineResourceContext resourceContext = UIResourceManager.ResourceContext;
			this._fullBackgroundCategory = spriteData.SpriteCategories["ui_fullbackgrounds"];
			this._fullBackgroundCategory.Load(UIResourceManager.ResourceContext, UIResourceManager.UIResourceDepot);
			this._backgroundCategory = spriteData.SpriteCategories["ui_backgrounds"];
			this._backgroundCategory.Load(UIResourceManager.ResourceContext, UIResourceManager.UIResourceDepot);
			this._fullscreensCategory = spriteData.SpriteCategories["ui_fullscreens"];
			this._fullscreensCategory.Load(UIResourceManager.ResourceContext, UIResourceManager.UIResourceDepot);
			SpriteCategory[] array = (from x in spriteData.SpriteCategories.Values
			where x.AlwaysLoad
			select x).ToArray<SpriteCategory>();
			int num = array.Length;
			float num2 = 0.2f / (float)(num - 1);
			for (int j = 0; j < array.Length; j++)
			{
				array[j].Load(resourceContext, resourceDepot);
				Utilities.SetLoadingScreenPercentage(0.4f + (float)j * num2);
			}
			Utilities.SetLoadingScreenPercentage(0.6f);
			ScreenManager.OnControllerDisconnected += this.OnControllerDisconnected;
			ManagedOptions.OnManagedOptionChanged = (ManagedOptions.OnManagedOptionChangedDelegate)Delegate.Combine(ManagedOptions.OnManagedOptionChanged, new ManagedOptions.OnManagedOptionChangedDelegate(this.OnManagedOptionChanged));
			Input.OnControllerTypeChanged = (Action<Input.ControllerTypes>)Delegate.Combine(Input.OnControllerTypeChanged, new Action<Input.ControllerTypes>(this.OnControllerTypeChanged));
			NativeOptions.GetConfig(NativeOptions.NativeOptionsType.DisplayMode);
			GauntletGamepadNavigationManager.Initialize();
			GauntletUISubModule.Instance = this;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00005D2C File Offset: 0x00003F2C
		private void OnControllerTypeChanged(Input.ControllerTypes newType)
		{
			bool isTouchpadMouseActive = this._isTouchpadMouseActive;
			if (newType == Input.ControllerTypes.PlayStationDualSense || newType == Input.ControllerTypes.PlayStationDualShock)
			{
				this._isTouchpadMouseActive = (NativeOptions.GetConfig(NativeOptions.NativeOptionsType.EnableTouchpadMouse) != 0f);
			}
			if (isTouchpadMouseActive != this._isTouchpadMouseActive && !(ScreenManager.TopScreen is GauntletInitialScreen))
			{
				object obj = new TextObject("{=qkPfC3Cb}Warning", null);
				TextObject textObject = new TextObject("{=LDRV5PxX}Touchpad Mouse setting won't take affect correctly until returning to initial menu! Exiting to main menu is recommended!", null);
				InformationManager.ShowInquiry(new InquiryData(obj.ToString(), textObject.ToString(), true, false, new TextObject("{=yS7PvrTD}OK", null).ToString(), null, null, null, "", 0f, null, null, null), false, true);
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00005DC2 File Offset: 0x00003FC2
		private void OnControllerDisconnected()
		{
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00005DC4 File Offset: 0x00003FC4
		private void OnManagedOptionChanged(ManagedOptions.ManagedOptionsType changedManagedOptionsType)
		{
			if (changedManagedOptionsType == ManagedOptions.ManagedOptionsType.Language)
			{
				UIResourceManager.OnLanguageChange(BannerlordConfig.Language);
				ScreenManager.UpdateLayout();
				return;
			}
			if (changedManagedOptionsType == ManagedOptions.ManagedOptionsType.UIScale)
			{
				ScreenManager.OnScaleChange(BannerlordConfig.UIScale);
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00005DE8 File Offset: 0x00003FE8
		protected override void OnSubModuleUnloaded()
		{
			ScreenManager.OnControllerDisconnected -= this.OnControllerDisconnected;
			ManagedOptions.OnManagedOptionChanged = (ManagedOptions.OnManagedOptionChangedDelegate)Delegate.Remove(ManagedOptions.OnManagedOptionChanged, new ManagedOptions.OnManagedOptionChangedDelegate(this.OnManagedOptionChanged));
			Input.OnControllerTypeChanged = (Action<Input.ControllerTypes>)Delegate.Remove(Input.OnControllerTypeChanged, new Action<Input.ControllerTypes>(this.OnControllerTypeChanged));
			UIResourceManager.Clear();
			LoadingWindow.Destroy();
			if (GauntletGamepadNavigationManager.Instance != null)
			{
				GauntletGamepadNavigationManager.Instance.OnFinalize();
			}
			GauntletUISubModule.Instance = null;
			base.OnSubModuleUnloaded();
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00005E70 File Offset: 0x00004070
		protected override void OnBeforeInitialModuleScreenSetAsRoot()
		{
			if (!this._initialized)
			{
				if (!Utilities.CommandLineArgumentExists("VisualTests"))
				{
					GauntletInformationView.Initialize();
					GauntletGameNotification.Initialize();
					GauntletSceneNotification.Initialize();
					GauntletSceneNotification.Current.RegisterContextProvider(new NativeSceneNotificationContextProvider());
					GauntletChatLogView.Initialize();
					GauntletGamepadCursor.Initialize();
					InformationManager.RegisterTooltip<List<TooltipProperty>, PropertyBasedTooltipVM>(new Action<PropertyBasedTooltipVM, object[]>(PropertyBasedTooltipVM.RefreshGenericPropertyBasedTooltip), "PropertyBasedTooltip");
					InformationManager.RegisterTooltip<RundownLineVM, RundownTooltipVM>(new Action<RundownTooltipVM, object[]>(RundownTooltipVM.RefreshGenericRundownTooltip), "RundownTooltip");
					InformationManager.RegisterTooltip<string, HintVM>(new Action<HintVM, object[]>(HintVM.RefreshGenericHintTooltip), "HintTooltip");
					this._queryManager = new GauntletQueryManager();
					this._queryManager.Initialize();
					this._queryManager.InitializeKeyVisuals();
				}
				this._loadingWindowManager = new LoadingWindowManager();
				LoadingWindow.Initialize(this._loadingWindowManager);
				UIResourceManager.OnLanguageChange(BannerlordConfig.Language);
				ScreenManager.OnScaleChange(BannerlordConfig.UIScale);
				this._initialized = true;
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00005F53 File Offset: 0x00004153
		public override void OnMultiplayerGameStart(Game game, object starterObject)
		{
			base.OnMultiplayerGameStart(game, starterObject);
			if (!this._isMultiplayer)
			{
				this._loadingWindowManager.SetCurrentModeIsMultiplayer(true);
				this._isMultiplayer = true;
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00005F78 File Offset: 0x00004178
		public override void OnGameEnd(Game game)
		{
			base.OnGameEnd(game);
			if (this._isMultiplayer)
			{
				this._loadingWindowManager.SetCurrentModeIsMultiplayer(false);
				this._isMultiplayer = false;
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00005F9C File Offset: 0x0000419C
		protected override void OnApplicationTick(float dt)
		{
			base.OnApplicationTick(dt);
			UIResourceManager.Update();
			if (GauntletGamepadNavigationManager.Instance != null && ScreenManager.GetMouseVisibility())
			{
				GauntletGamepadNavigationManager.Instance.Update(dt);
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00005FC3 File Offset: 0x000041C3
		[CommandLineFunctionality.CommandLineArgumentFunction("clear", "chatlog")]
		public static string ClearChatLog(List<string> strings)
		{
			InformationManager.ClearAllMessages();
			return "Chatlog cleared!";
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00005FD0 File Offset: 0x000041D0
		[CommandLineFunctionality.CommandLineArgumentFunction("can_focus_while_in_mission", "chatlog")]
		public static string SetCanFocusWhileInMission(List<string> strings)
		{
			if (strings[0] == "0" || strings[0] == "1")
			{
				GauntletChatLogView.Current.SetCanFocusWhileInMission(strings[0] == "1");
				return "Chat window will" + ((strings[0] == "1") ? " " : " NOT ") + " be able to gain focus now.";
			}
			return "Wrong input";
		}

		// Token: 0x04000062 RID: 98
		private bool _initialized;

		// Token: 0x04000063 RID: 99
		private bool _isMultiplayer;

		// Token: 0x04000064 RID: 100
		private GauntletQueryManager _queryManager;

		// Token: 0x04000065 RID: 101
		private LoadingWindowManager _loadingWindowManager;

		// Token: 0x04000066 RID: 102
		private SpriteCategory _fullBackgroundCategory;

		// Token: 0x04000067 RID: 103
		private SpriteCategory _backgroundCategory;

		// Token: 0x04000068 RID: 104
		private SpriteCategory _fullscreensCategory;

		// Token: 0x04000069 RID: 105
		private bool _isTouchpadMouseActive;
	}
}
