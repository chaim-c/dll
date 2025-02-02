using System;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.Engine.Options;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.Source.Missions;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.MissionViews;
using TaleWorlds.MountAndBlade.ViewModelCollection.GameOptions;
using TaleWorlds.MountAndBlade.ViewModelCollection.GameOptions.AuxiliaryKeys;
using TaleWorlds.MountAndBlade.ViewModelCollection.GameOptions.GameKeys;
using TaleWorlds.ScreenSystem;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Mission
{
	// Token: 0x0200002A RID: 42
	[OverrideView(typeof(MissionOptionsUIHandler))]
	public class MissionGauntletOptionsUIHandler : MissionView
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x0000B1B8 File Offset: 0x000093B8
		// (set) Token: 0x060001D6 RID: 470 RVA: 0x0000B1C0 File Offset: 0x000093C0
		public bool IsEnabled { get; private set; }

		// Token: 0x060001D7 RID: 471 RVA: 0x0000B1C9 File Offset: 0x000093C9
		public MissionGauntletOptionsUIHandler()
		{
			this.ViewOrderPriority = 49;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000B1DC File Offset: 0x000093DC
		public override void OnMissionScreenInitialize()
		{
			base.OnMissionScreenInitialize();
			base.Mission.GetMissionBehavior<MissionOptionsComponent>().OnOptionsAdded += this.OnShowOptions;
			this._keybindingPopup = new KeybindingPopup(new Action<Key>(this.SetHotKey), base.MissionScreen);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000B228 File Offset: 0x00009428
		public override void OnMissionScreenFinalize()
		{
			base.Mission.GetMissionBehavior<MissionOptionsComponent>().OnOptionsAdded -= this.OnShowOptions;
			base.OnMissionScreenFinalize();
			OptionsVM dataSource = this._dataSource;
			if (dataSource != null)
			{
				dataSource.OnFinalize();
			}
			this._dataSource = null;
			this._movie = null;
			KeybindingPopup keybindingPopup = this._keybindingPopup;
			if (keybindingPopup != null)
			{
				keybindingPopup.OnToggle(false);
			}
			this._keybindingPopup = null;
			this._gauntletLayer = null;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000B298 File Offset: 0x00009498
		public override void OnMissionScreenTick(float dt)
		{
			base.OnMissionScreenTick(dt);
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
			KeybindingPopup keybindingPopup = this._keybindingPopup;
			if (keybindingPopup == null)
			{
				return;
			}
			keybindingPopup.Tick();
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000B429 File Offset: 0x00009629
		public override bool OnEscape()
		{
			if (this._dataSource != null)
			{
				this._dataSource.ExecuteCloseOptions();
				return true;
			}
			return base.OnEscape();
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000B446 File Offset: 0x00009646
		private void OnShowOptions()
		{
			this.IsEnabled = true;
			this.OnEscapeMenuToggled(true);
			this._initialClothSimValue = (NativeOptions.GetConfig(NativeOptions.NativeOptionsType.ClothSimulation) == 0f);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000B46C File Offset: 0x0000966C
		private void OnCloseOptions()
		{
			this.IsEnabled = false;
			this.OnEscapeMenuToggled(false);
			bool flag = NativeOptions.GetConfig(NativeOptions.NativeOptionsType.ClothSimulation) == 0f;
			if (this._initialClothSimValue != flag)
			{
				InformationManager.ShowInquiry(new InquiryData(Module.CurrentModule.GlobalTextManager.FindText("str_option_wont_take_effect_mission_title", null).ToString(), Module.CurrentModule.GlobalTextManager.FindText("str_option_wont_take_effect_mission_desc", null).ToString(), true, false, Module.CurrentModule.GlobalTextManager.FindText("str_ok", null).ToString(), string.Empty, null, null, "", 0f, null, null, null), true, false);
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000B514 File Offset: 0x00009714
		public override bool IsOpeningEscapeMenuOnFocusChangeAllowed()
		{
			return this._gauntletLayer == null;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000B520 File Offset: 0x00009720
		private void OnEscapeMenuToggled(bool isOpened)
		{
			if (isOpened)
			{
				if (!GameNetwork.IsMultiplayer)
				{
					MBCommon.PauseGameEngine();
				}
			}
			else
			{
				MBCommon.UnPauseGameEngine();
			}
			if (isOpened)
			{
				OptionsVM.OptionsMode optionsMode = GameNetwork.IsMultiplayer ? OptionsVM.OptionsMode.Multiplayer : OptionsVM.OptionsMode.Singleplayer;
				this._dataSource = new OptionsVM(optionsMode, new Action(this.OnCloseOptions), new Action<KeyOptionVM>(this.OnKeybindRequest), null, null);
				this._dataSource.SetDoneInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Confirm"));
				this._dataSource.SetCancelInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Exit"));
				this._dataSource.SetPreviousTabInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("SwitchToPreviousTab"));
				this._dataSource.SetNextTabInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("SwitchToNextTab"));
				this._dataSource.SetResetInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Reset"));
				int num = this.ViewOrderPriority + 1;
				this.ViewOrderPriority = num;
				this._gauntletLayer = new GauntletLayer(num, "GauntletLayer", false);
				this._gauntletLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
				this._gauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericPanelGameKeyCategory"));
				this._optionsSpriteCategory = UIResourceManager.SpriteData.SpriteCategories["ui_options"];
				this._optionsSpriteCategory.Load(UIResourceManager.ResourceContext, UIResourceManager.UIResourceDepot);
				this._fullScreensSpriteCategory = UIResourceManager.SpriteData.SpriteCategories["ui_fullscreens"];
				this._fullScreensSpriteCategory.Load(UIResourceManager.ResourceContext, UIResourceManager.UIResourceDepot);
				this._movie = this._gauntletLayer.LoadMovie("Options", this._dataSource);
				base.MissionScreen.AddLayer(this._gauntletLayer);
				this._gauntletLayer.IsFocusLayer = true;
				ScreenManager.TrySetFocus(this._gauntletLayer);
				Game game = Game.Current;
				if (game == null)
				{
					return;
				}
				game.EventManager.TriggerEvent<TutorialContextChangedEvent>(new TutorialContextChangedEvent(TutorialContexts.OptionsScreen));
				return;
			}
			else
			{
				this._gauntletLayer.InputRestrictions.ResetInputRestrictions();
				this._gauntletLayer.IsFocusLayer = false;
				ScreenManager.TryLoseFocus(this._gauntletLayer);
				base.MissionScreen.RemoveLayer(this._gauntletLayer);
				KeybindingPopup keybindingPopup = this._keybindingPopup;
				if (keybindingPopup != null)
				{
					keybindingPopup.OnToggle(false);
				}
				this._optionsSpriteCategory.Unload();
				this._fullScreensSpriteCategory.Unload();
				this._gauntletLayer = null;
				this._dataSource.OnFinalize();
				this._dataSource = null;
				this._gauntletLayer = null;
				Game game2 = Game.Current;
				if (game2 == null)
				{
					return;
				}
				game2.EventManager.TriggerEvent<TutorialContextChangedEvent>(new TutorialContextChangedEvent(TutorialContexts.Mission));
				return;
			}
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000B7BC File Offset: 0x000099BC
		private void OnKeybindRequest(KeyOptionVM requestedHotKeyToChange)
		{
			this._currentKey = requestedHotKeyToChange;
			this._keybindingPopup.OnToggle(true);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000B7D4 File Offset: 0x000099D4
		private void SetHotKey(Key key)
		{
			if (key.IsControllerInput)
			{
				Debug.FailedAssert("Trying to use SetHotKey with a controller input", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.GauntletUI\\Mission\\MissionGauntletOptionsUIHandler.cs", "SetHotKey", 239);
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
						Debug.FailedAssert("Could not find AuxiliaryKeyGroup during SetHotKey", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.GauntletUI\\Mission\\MissionGauntletOptionsUIHandler.cs", "SetHotKey", 278);
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
				Debug.FailedAssert("Could not find GameKeyGroup during SetHotKey", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.GauntletUI\\Mission\\MissionGauntletOptionsUIHandler.cs", "SetHotKey", 251);
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

		// Token: 0x0400010B RID: 267
		private GauntletLayer _gauntletLayer;

		// Token: 0x0400010C RID: 268
		private OptionsVM _dataSource;

		// Token: 0x0400010D RID: 269
		private IGauntletMovie _movie;

		// Token: 0x0400010E RID: 270
		private KeybindingPopup _keybindingPopup;

		// Token: 0x0400010F RID: 271
		private KeyOptionVM _currentKey;

		// Token: 0x04000110 RID: 272
		private SpriteCategory _optionsSpriteCategory;

		// Token: 0x04000111 RID: 273
		private SpriteCategory _fullScreensSpriteCategory;

		// Token: 0x04000112 RID: 274
		private bool _initialClothSimValue;
	}
}
