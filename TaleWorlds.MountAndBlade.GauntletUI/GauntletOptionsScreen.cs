using System;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.Screens;
using TaleWorlds.MountAndBlade.ViewModelCollection.GameOptions;
using TaleWorlds.MountAndBlade.ViewModelCollection.GameOptions.AuxiliaryKeys;
using TaleWorlds.MountAndBlade.ViewModelCollection.GameOptions.GameKeys;
using TaleWorlds.ScreenSystem;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI
{
	// Token: 0x0200000D RID: 13
	[OverrideView(typeof(OptionsScreen))]
	public class GauntletOptionsScreen : ScreenBase
	{
		// Token: 0x06000060 RID: 96 RVA: 0x00004830 File Offset: 0x00002A30
		public GauntletOptionsScreen(bool isFromMainMenu)
		{
			this._isFromMainMenu = isFromMainMenu;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00004840 File Offset: 0x00002A40
		protected override void OnInitialize()
		{
			base.OnInitialize();
			SpriteData spriteData = UIResourceManager.SpriteData;
			TwoDimensionEngineResourceContext resourceContext = UIResourceManager.ResourceContext;
			ResourceDepot uiresourceDepot = UIResourceManager.UIResourceDepot;
			this._optionsSpriteCategory = spriteData.SpriteCategories["ui_options"];
			this._optionsSpriteCategory.Load(resourceContext, uiresourceDepot);
			OptionsVM.OptionsMode optionsMode = this._isFromMainMenu ? OptionsVM.OptionsMode.MainMenu : OptionsVM.OptionsMode.Singleplayer;
			this._dataSource = new OptionsVM(true, optionsMode, new Action<KeyOptionVM>(this.OnKeybindRequest), null, null);
			this._dataSource.SetDoneInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Confirm"));
			this._dataSource.SetCancelInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Exit"));
			this._dataSource.SetPreviousTabInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("SwitchToPreviousTab"));
			this._dataSource.SetNextTabInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("SwitchToNextTab"));
			this._dataSource.SetResetInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Reset"));
			this._dataSource.ExposurePopUp.SetCancelInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Exit"));
			this._dataSource.ExposurePopUp.SetConfirmInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Confirm"));
			this._dataSource.BrightnessPopUp.SetCancelInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Exit"));
			this._dataSource.BrightnessPopUp.SetConfirmInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Confirm"));
			this._gauntletLayer = new GauntletLayer(4000, "GauntletLayer", false);
			this._gauntletMovie = this._gauntletLayer.LoadMovie("Options", this._dataSource);
			this._gauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericPanelGameKeyCategory"));
			this._gauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericCampaignPanelsGameKeyCategory"));
			this._gauntletLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
			this._gauntletLayer.IsFocusLayer = true;
			this._keybindingPopup = new KeybindingPopup(new Action<Key>(this.SetHotKey), this);
			base.AddLayer(this._gauntletLayer);
			ScreenManager.TrySetFocus(this._gauntletLayer);
			if (BannerlordConfig.ForceVSyncInMenus)
			{
				Utilities.SetForceVsync(true);
			}
			Game game = Game.Current;
			if (game == null)
			{
				return;
			}
			game.EventManager.TriggerEvent<TutorialContextChangedEvent>(new TutorialContextChangedEvent(TutorialContexts.OptionsScreen));
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00004AB6 File Offset: 0x00002CB6
		protected override void OnFinalize()
		{
			base.OnFinalize();
			this._dataSource.OnFinalize();
			this._optionsSpriteCategory.Unload();
			Utilities.SetForceVsync(false);
			Game game = Game.Current;
			if (game == null)
			{
				return;
			}
			game.EventManager.TriggerEvent<TutorialContextChangedEvent>(new TutorialContextChangedEvent(TutorialContexts.None));
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00004AF4 File Offset: 0x00002CF4
		protected override void OnDeactivate()
		{
			LoadingWindow.EnableGlobalLoadingWindow();
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00004AFC File Offset: 0x00002CFC
		protected override void OnFrameTick(float dt)
		{
			base.OnFrameTick(dt);
			if (this._gauntletLayer != null && !this._keybindingPopup.IsActive)
			{
				if (this._gauntletLayer.Input.IsHotKeyReleased("Exit"))
				{
					UISoundsHelper.PlayUISound("event:/ui/default");
					if (this._dataSource.ExposurePopUp.Visible)
					{
						this._dataSource.ExposurePopUp.ExecuteCancel();
					}
					else if (this._dataSource.BrightnessPopUp.Visible)
					{
						this._dataSource.BrightnessPopUp.ExecuteCancel();
					}
					else
					{
						this._dataSource.ExecuteCancel();
					}
				}
				else if (this._gauntletLayer.Input.IsHotKeyReleased("Confirm"))
				{
					UISoundsHelper.PlayUISound("event:/ui/default");
					if (this._dataSource.ExposurePopUp.Visible)
					{
						this._dataSource.ExposurePopUp.ExecuteConfirm();
					}
					else if (this._dataSource.BrightnessPopUp.Visible)
					{
						this._dataSource.BrightnessPopUp.ExecuteConfirm();
					}
					else
					{
						this._dataSource.ExecuteDone();
					}
				}
				else if (this._gauntletLayer.Input.IsHotKeyPressed("SwitchToPreviousTab"))
				{
					UISoundsHelper.PlayUISound("event:/ui/tab");
					this._dataSource.SelectPreviousCategory();
				}
				else if (this._gauntletLayer.Input.IsHotKeyPressed("SwitchToNextTab"))
				{
					UISoundsHelper.PlayUISound("event:/ui/tab");
					this._dataSource.SelectNextCategory();
				}
			}
			this._keybindingPopup.Tick();
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00004C88 File Offset: 0x00002E88
		private void OnKeybindRequest(KeyOptionVM requestedHotKeyToChange)
		{
			this._currentKey = requestedHotKeyToChange;
			this._keybindingPopup.OnToggle(true);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00004CA0 File Offset: 0x00002EA0
		private void SetHotKey(Key key)
		{
			if (key.IsControllerInput)
			{
				Debug.FailedAssert("Trying to use SetHotKey with a controller input", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.GauntletUI\\GauntletOptionsScreen.cs", "SetHotKey", 161);
				MBInformationManager.AddQuickInformation(new TextObject("{=B41vvGuo}Invalid key", null), 0, null, "");
				this._keybindingPopup.OnToggle(false);
				return;
			}
			GameKeyOptionVM gameKey;
			if ((gameKey = (this._currentKey as GameKeyOptionVM)) == null)
			{
				AuxiliaryKeyOptionVM auxiliaryKey;
				if ((auxiliaryKey = (this._currentKey as AuxiliaryKeyOptionVM)) != null)
				{
					AuxiliaryKeyGroupVM auxiliaryKeyGroupVM = this._dataSource.GameKeyOptionGroups.AuxiliaryKeyGroups.FirstOrDefault((AuxiliaryKeyGroupVM g) => g.HotKeys.Contains(auxiliaryKey));
					if (auxiliaryKeyGroupVM == null)
					{
						Debug.FailedAssert("Could not find AuxiliaryKeyGroup during SetHotKey", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.GauntletUI\\GauntletOptionsScreen.cs", "SetHotKey", 200);
						MBInformationManager.AddQuickInformation(new TextObject("{=oZrVNUOk}Error", null), 0, null, "");
						this._keybindingPopup.OnToggle(false);
						return;
					}
					if (this._gauntletLayer.Input.IsHotKeyReleased("Exit"))
					{
						this._keybindingPopup.OnToggle(false);
						return;
					}
					if (auxiliaryKeyGroupVM.HotKeys.Any((AuxiliaryKeyOptionVM k) => k.CurrentKey.InputKey == key.InputKey && k.CurrentHotKey.HasSameModifiers(auxiliaryKey.CurrentHotKey)))
					{
						MBInformationManager.AddQuickInformation(new TextObject("{=n4UUrd1p}Already in use", null), 0, null, "");
						return;
					}
					AuxiliaryKeyOptionVM auxiliaryKey2 = auxiliaryKey;
					if (auxiliaryKey2 != null)
					{
						auxiliaryKey2.Set(key.InputKey);
					}
					auxiliaryKey = null;
					this._keybindingPopup.OnToggle(false);
				}
				return;
			}
			GameKeyGroupVM gameKeyGroupVM = this._dataSource.GameKeyOptionGroups.GameKeyGroups.FirstOrDefault((GameKeyGroupVM g) => g.GameKeys.Contains(gameKey));
			if (gameKeyGroupVM == null)
			{
				Debug.FailedAssert("Could not find GameKeyGroup during SetHotKey", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.GauntletUI\\GauntletOptionsScreen.cs", "SetHotKey", 173);
				MBInformationManager.AddQuickInformation(new TextObject("{=oZrVNUOk}Error", null), 0, null, "");
				this._keybindingPopup.OnToggle(false);
				return;
			}
			if (this._gauntletLayer.Input.IsHotKeyReleased("Exit"))
			{
				this._keybindingPopup.OnToggle(false);
				return;
			}
			if (gameKeyGroupVM.GameKeys.Any((GameKeyOptionVM k) => k.CurrentKey.InputKey == key.InputKey))
			{
				MBInformationManager.AddQuickInformation(new TextObject("{=n4UUrd1p}Already in use", null), 0, null, "");
				return;
			}
			GameKeyOptionVM gameKey2 = gameKey;
			if (gameKey2 != null)
			{
				gameKey2.Set(key.InputKey);
			}
			gameKey = null;
			this._keybindingPopup.OnToggle(false);
		}

		// Token: 0x0400004C RID: 76
		private GauntletLayer _gauntletLayer;

		// Token: 0x0400004D RID: 77
		private OptionsVM _dataSource;

		// Token: 0x0400004E RID: 78
		private IGauntletMovie _gauntletMovie;

		// Token: 0x0400004F RID: 79
		private KeybindingPopup _keybindingPopup;

		// Token: 0x04000050 RID: 80
		private KeyOptionVM _currentKey;

		// Token: 0x04000051 RID: 81
		private SpriteCategory _optionsSpriteCategory;

		// Token: 0x04000052 RID: 82
		private bool _isFromMainMenu;
	}
}
