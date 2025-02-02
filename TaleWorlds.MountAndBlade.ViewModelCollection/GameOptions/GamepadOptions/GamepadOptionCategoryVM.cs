using System;
using System.Collections.Generic;
using TaleWorlds.Core.ViewModelCollection.Selector;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.Options;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.GameOptions.GamepadOptions
{
	// Token: 0x02000065 RID: 101
	public class GamepadOptionCategoryVM : GroupedOptionCategoryVM
	{
		// Token: 0x06000812 RID: 2066 RVA: 0x0001EC50 File Offset: 0x0001CE50
		public GamepadOptionCategoryVM(OptionsVM options, TextObject name, OptionCategory category, bool isEnabled, bool isResetSupported = false) : base(options, name, category, isEnabled, isResetSupported)
		{
			this.OtherKeys = new MBBindingList<GamepadOptionKeyItemVM>();
			this.DpadKeys = new MBBindingList<GamepadOptionKeyItemVM>();
			this.LeftAnalogKeys = new MBBindingList<GamepadOptionKeyItemVM>();
			this.RightAnalogKeys = new MBBindingList<GamepadOptionKeyItemVM>();
			this.FaceKeys = new MBBindingList<GamepadOptionKeyItemVM>();
			this.LeftTriggerAndBumperKeys = new MBBindingList<GamepadOptionKeyItemVM>();
			this.RightTriggerAndBumperKeys = new MBBindingList<GamepadOptionKeyItemVM>();
			if (Input.ControllerType == Input.ControllerTypes.PlayStationDualSense)
			{
				this.SetCurrentGamepadType(GamepadOptionCategoryVM.GamepadType.Playstation5);
			}
			else if (Input.ControllerType == Input.ControllerTypes.PlayStationDualShock)
			{
				this.SetCurrentGamepadType(GamepadOptionCategoryVM.GamepadType.Playstation4);
			}
			else
			{
				this.SetCurrentGamepadType(GamepadOptionCategoryVM.GamepadType.Xbox);
			}
			this.Actions = new MBBindingList<SelectorVM<SelectorItemVM>>();
			this._categories = new SelectorVM<SelectorItemVM>(0, null);
			this._categories.AddItem(new SelectorItemVM(new TextObject("{=gamepadActionKeybind}Action", null)));
			this._categories.AddItem(new SelectorItemVM(new TextObject("{=gamepadMapKeybind}Map", null)));
			this._categories.SetOnChangeAction(new Action<SelectorVM<SelectorItemVM>>(this.OnCategoryChange));
			this._categories.SelectedIndex = 0;
			this.Actions.Add(this._categories);
			this.RefreshValues();
			Input.OnGamepadActiveStateChanged = (Action)Delegate.Combine(Input.OnGamepadActiveStateChanged, new Action(this.OnGamepadActiveStateChanged));
			base.IsEnabled = Input.IsGamepadActive;
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x0001ED9C File Offset: 0x0001CF9C
		private void OnCategoryChange(SelectorVM<SelectorItemVM> obj)
		{
			if (obj.SelectedIndex >= 0)
			{
				this.OtherKeys.Clear();
				this.DpadKeys.Clear();
				this.LeftAnalogKeys.Clear();
				this.RightAnalogKeys.Clear();
				this.FaceKeys.Clear();
				this.LeftTriggerAndBumperKeys.Clear();
				this.RightTriggerAndBumperKeys.Clear();
				IEnumerable<GamepadOptionKeyItemVM> enumerable = null;
				if (obj.SelectedIndex == 0)
				{
					enumerable = GamepadOptionCategoryVM.GetActionKeys();
				}
				else if (obj.SelectedIndex == 1)
				{
					enumerable = GamepadOptionCategoryVM.GetMapKeys();
				}
				foreach (GamepadOptionKeyItemVM gamepadOptionKeyItemVM in enumerable)
				{
					InputKey key = gamepadOptionKeyItemVM.Key ?? InputKey.Invalid;
					if (Key.IsLeftAnalogInput(key))
					{
						this.LeftAnalogKeys.Add(gamepadOptionKeyItemVM);
					}
					else if (Key.IsRightAnalogInput(key))
					{
						this.RightAnalogKeys.Add(gamepadOptionKeyItemVM);
					}
					else if (Key.IsDpadInput(key))
					{
						this.DpadKeys.Add(gamepadOptionKeyItemVM);
					}
					else if (Key.IsFaceKeyInput(key))
					{
						this.FaceKeys.Add(gamepadOptionKeyItemVM);
					}
					else
					{
						this.OtherKeys.Add(gamepadOptionKeyItemVM);
					}
					if (Key.IsLeftBumperOrTriggerInput(key))
					{
						this.LeftTriggerAndBumperKeys.Add(gamepadOptionKeyItemVM);
					}
					else if (Key.IsRightBumperOrTriggerInput(key))
					{
						this.RightTriggerAndBumperKeys.Add(gamepadOptionKeyItemVM);
					}
				}
			}
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0001EF08 File Offset: 0x0001D108
		public override void RefreshValues()
		{
			base.RefreshValues();
			MBBindingList<GamepadOptionKeyItemVM> otherKeys = this.OtherKeys;
			if (otherKeys != null)
			{
				otherKeys.ApplyActionOnAllItems(delegate(GamepadOptionKeyItemVM x)
				{
					x.RefreshValues();
				});
			}
			MBBindingList<GamepadOptionKeyItemVM> leftAnalogKeys = this.LeftAnalogKeys;
			if (leftAnalogKeys != null)
			{
				leftAnalogKeys.ApplyActionOnAllItems(delegate(GamepadOptionKeyItemVM x)
				{
					x.RefreshValues();
				});
			}
			MBBindingList<GamepadOptionKeyItemVM> rightAnalogKeys = this.RightAnalogKeys;
			if (rightAnalogKeys != null)
			{
				rightAnalogKeys.ApplyActionOnAllItems(delegate(GamepadOptionKeyItemVM x)
				{
					x.RefreshValues();
				});
			}
			MBBindingList<GamepadOptionKeyItemVM> faceKeys = this.FaceKeys;
			if (faceKeys != null)
			{
				faceKeys.ApplyActionOnAllItems(delegate(GamepadOptionKeyItemVM x)
				{
					x.RefreshValues();
				});
			}
			MBBindingList<GamepadOptionKeyItemVM> dpadKeys = this.DpadKeys;
			if (dpadKeys != null)
			{
				dpadKeys.ApplyActionOnAllItems(delegate(GamepadOptionKeyItemVM x)
				{
					x.RefreshValues();
				});
			}
			MBBindingList<GamepadOptionKeyItemVM> leftTriggerAndBumperKeys = this.LeftTriggerAndBumperKeys;
			if (leftTriggerAndBumperKeys != null)
			{
				leftTriggerAndBumperKeys.ApplyActionOnAllItems(delegate(GamepadOptionKeyItemVM x)
				{
					x.RefreshValues();
				});
			}
			MBBindingList<GamepadOptionKeyItemVM> rightTriggerAndBumperKeys = this.RightTriggerAndBumperKeys;
			if (rightTriggerAndBumperKeys != null)
			{
				rightTriggerAndBumperKeys.ApplyActionOnAllItems(delegate(GamepadOptionKeyItemVM x)
				{
					x.RefreshValues();
				});
			}
			MBBindingList<SelectorVM<SelectorItemVM>> actions = this.Actions;
			if (actions == null)
			{
				return;
			}
			actions.ApplyActionOnAllItems(delegate(SelectorVM<SelectorItemVM> x)
			{
				x.RefreshValues();
			});
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x0001F09A File Offset: 0x0001D29A
		private void SetCurrentGamepadType(GamepadOptionCategoryVM.GamepadType type)
		{
			this.CurrentGamepadType = (int)type;
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0001F0A4 File Offset: 0x0001D2A4
		private void OnGamepadActiveStateChanged()
		{
			base.IsEnabled = Input.IsGamepadActive;
			Debug.Print("GAMEPAD TAB ENABLED: " + base.IsEnabled.ToString(), 0, Debug.DebugColor.White, 17592186044416UL);
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0001F0E5 File Offset: 0x0001D2E5
		public override void OnFinalize()
		{
			base.OnFinalize();
			Input.OnGamepadActiveStateChanged = (Action)Delegate.Remove(Input.OnGamepadActiveStateChanged, new Action(this.OnGamepadActiveStateChanged));
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0001F10D File Offset: 0x0001D30D
		private static IEnumerable<GamepadOptionKeyItemVM> GetActionKeys()
		{
			yield return new GamepadOptionKeyItemVM(InputKey.ControllerLStick, new TextObject("{=i28Kjuay}Move Character", null));
			yield return new GamepadOptionKeyItemVM(InputKey.ControllerRStick, new TextObject("{=1hlaGzGI}Look", null));
			yield return new GamepadOptionKeyItemVM(InputKey.ControllerLOption, new TextObject("{=9pgOGq7X}Log", null));
			yield return new GamepadOptionKeyItemVM(HotKeyManager.GetCategory("CombatHotKeyCategory").GetGameKey(31));
			yield return new GamepadOptionKeyItemVM(HotKeyManager.GetCategory("CombatHotKeyCategory").GetGameKey(33));
			yield return new GamepadOptionKeyItemVM(HotKeyManager.GetCategory("CombatHotKeyCategory").GetGameKey(25));
			yield return new GamepadOptionKeyItemVM(HotKeyManager.GetCategory("CombatHotKeyCategory").GetGameKey(15));
			yield return new GamepadOptionKeyItemVM(HotKeyManager.GetCategory("CombatHotKeyCategory").GetGameKey(13));
			yield return new GamepadOptionKeyItemVM(HotKeyManager.GetCategory("ScoreboardHotKeyCategory").GetHotKey("HoldShow"));
			yield return new GamepadOptionKeyItemVM(HotKeyManager.GetCategory("CombatHotKeyCategory").GetGameKey(16));
			yield return new GamepadOptionKeyItemVM(HotKeyManager.GetCategory("CombatHotKeyCategory").GetGameKey(14));
			yield return new GamepadOptionKeyItemVM(HotKeyManager.GetCategory("CombatHotKeyCategory").GetGameKey(9));
			yield return new GamepadOptionKeyItemVM(HotKeyManager.GetCategory("CombatHotKeyCategory").GetGameKey(10));
			yield return new GamepadOptionKeyItemVM(HotKeyManager.GetCategory("CombatHotKeyCategory").GetGameKey(26));
			yield return new GamepadOptionKeyItemVM(HotKeyManager.GetCategory("CombatHotKeyCategory").GetGameKey(27));
			yield return new GamepadOptionKeyItemVM(HotKeyManager.GetCategory("Generic").GetGameKey(5));
			yield return new GamepadOptionKeyItemVM(HotKeyManager.GetCategory("CombatHotKeyCategory").GetGameKey(34));
			yield return new GamepadOptionKeyItemVM(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("ToggleEscapeMenu"));
			yield break;
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0001F116 File Offset: 0x0001D316
		private static IEnumerable<GamepadOptionKeyItemVM> GetMapKeys()
		{
			yield return new GamepadOptionKeyItemVM(InputKey.ControllerLStick, new TextObject("{=hdGay8xc}Map Cursor Move", null));
			yield return new GamepadOptionKeyItemVM(InputKey.ControllerRStick, new TextObject("{=atUHbDeM}Map Camera Rotate", null));
			yield return new GamepadOptionKeyItemVM(InputKey.ControllerLUp, new TextObject("{=u78WUP9W}Fast Cursor Move Up", null));
			yield return new GamepadOptionKeyItemVM(InputKey.ControllerLRight, new TextObject("{=bLPSaLNv}Fast Cursor Move Right", null));
			yield return new GamepadOptionKeyItemVM(InputKey.ControllerLLeft, new TextObject("{=82LuSDnd}Fast Cursor Move Left", null));
			yield return new GamepadOptionKeyItemVM(InputKey.ControllerLDown, new TextObject("{=nEpZvaEl}Fast Cursor Move Down", null));
			yield return new GamepadOptionKeyItemVM(HotKeyManager.GetCategory("MapHotKeyCategory").GetHotKey("MapChangeCursorMode"));
			yield return new GamepadOptionKeyItemVM(HotKeyManager.GetCategory("GenericCampaignPanelsGameKeyCategory").GetGameKey(39));
			yield return new GamepadOptionKeyItemVM(HotKeyManager.GetCategory("MapHotKeyCategory").GetGameKey(62));
			yield return new GamepadOptionKeyItemVM(HotKeyManager.GetCategory("MapHotKeyCategory").GetGameKey(55));
			yield return new GamepadOptionKeyItemVM(HotKeyManager.GetCategory("MapHotKeyCategory").GetGameKey(64));
			yield return new GamepadOptionKeyItemVM(HotKeyManager.GetCategory("MapHotKeyCategory").GetGameKey(56));
			yield return new GamepadOptionKeyItemVM(InputKey.ControllerLBumper, new TextObject("{=mueocuFG}Show Indicators", null));
			yield return new GamepadOptionKeyItemVM(HotKeyManager.GetCategory("MapHotKeyCategory").GetGameKey(63));
			yield return new GamepadOptionKeyItemVM(HotKeyManager.GetCategory("MapHotKeyCategory").GetGameKey(65));
			yield return new GamepadOptionKeyItemVM(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("ToggleEscapeMenu"));
			yield return new GamepadOptionKeyItemVM(HotKeyManager.GetCategory("MapHotKeyCategory").GetHotKey("MapClick"));
			yield break;
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x0600081A RID: 2074 RVA: 0x0001F11F File Offset: 0x0001D31F
		// (set) Token: 0x0600081B RID: 2075 RVA: 0x0001F127 File Offset: 0x0001D327
		[DataSourceProperty]
		public int CurrentGamepadType
		{
			get
			{
				return this._currentGamepadType;
			}
			set
			{
				if (value != this._currentGamepadType)
				{
					this._currentGamepadType = value;
					base.OnPropertyChangedWithValue(value, "CurrentGamepadType");
				}
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x0600081C RID: 2076 RVA: 0x0001F145 File Offset: 0x0001D345
		// (set) Token: 0x0600081D RID: 2077 RVA: 0x0001F14D File Offset: 0x0001D34D
		[DataSourceProperty]
		public MBBindingList<GamepadOptionKeyItemVM> OtherKeys
		{
			get
			{
				return this._otherKeys;
			}
			set
			{
				if (value != this._otherKeys)
				{
					this._otherKeys = value;
					base.OnPropertyChangedWithValue<MBBindingList<GamepadOptionKeyItemVM>>(value, "OtherKeys");
				}
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x0600081E RID: 2078 RVA: 0x0001F16B File Offset: 0x0001D36B
		// (set) Token: 0x0600081F RID: 2079 RVA: 0x0001F173 File Offset: 0x0001D373
		[DataSourceProperty]
		public MBBindingList<GamepadOptionKeyItemVM> DpadKeys
		{
			get
			{
				return this._dpadKeys;
			}
			set
			{
				if (value != this._dpadKeys)
				{
					this._dpadKeys = value;
					base.OnPropertyChangedWithValue<MBBindingList<GamepadOptionKeyItemVM>>(value, "DpadKeys");
				}
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000820 RID: 2080 RVA: 0x0001F191 File Offset: 0x0001D391
		// (set) Token: 0x06000821 RID: 2081 RVA: 0x0001F199 File Offset: 0x0001D399
		[DataSourceProperty]
		public MBBindingList<GamepadOptionKeyItemVM> LeftTriggerAndBumperKeys
		{
			get
			{
				return this._leftTriggerAndBumperKeys;
			}
			set
			{
				if (value != this._leftTriggerAndBumperKeys)
				{
					this._leftTriggerAndBumperKeys = value;
					base.OnPropertyChangedWithValue<MBBindingList<GamepadOptionKeyItemVM>>(value, "LeftTriggerAndBumperKeys");
				}
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000822 RID: 2082 RVA: 0x0001F1B7 File Offset: 0x0001D3B7
		// (set) Token: 0x06000823 RID: 2083 RVA: 0x0001F1BF File Offset: 0x0001D3BF
		[DataSourceProperty]
		public MBBindingList<GamepadOptionKeyItemVM> RightTriggerAndBumperKeys
		{
			get
			{
				return this._rightTriggerAndBumperKeys;
			}
			set
			{
				if (value != this._rightTriggerAndBumperKeys)
				{
					this._rightTriggerAndBumperKeys = value;
					base.OnPropertyChangedWithValue<MBBindingList<GamepadOptionKeyItemVM>>(value, "RightTriggerAndBumperKeys");
				}
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000824 RID: 2084 RVA: 0x0001F1DD File Offset: 0x0001D3DD
		// (set) Token: 0x06000825 RID: 2085 RVA: 0x0001F1E5 File Offset: 0x0001D3E5
		[DataSourceProperty]
		public MBBindingList<GamepadOptionKeyItemVM> RightAnalogKeys
		{
			get
			{
				return this._rightAnalogKeys;
			}
			set
			{
				if (value != this._rightAnalogKeys)
				{
					this._rightAnalogKeys = value;
					base.OnPropertyChangedWithValue<MBBindingList<GamepadOptionKeyItemVM>>(value, "RightAnalogKeys");
				}
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000826 RID: 2086 RVA: 0x0001F203 File Offset: 0x0001D403
		// (set) Token: 0x06000827 RID: 2087 RVA: 0x0001F20B File Offset: 0x0001D40B
		[DataSourceProperty]
		public MBBindingList<GamepadOptionKeyItemVM> LeftAnalogKeys
		{
			get
			{
				return this._leftAnalogKeys;
			}
			set
			{
				if (value != this._leftAnalogKeys)
				{
					this._leftAnalogKeys = value;
					base.OnPropertyChangedWithValue<MBBindingList<GamepadOptionKeyItemVM>>(value, "LeftAnalogKeys");
				}
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000828 RID: 2088 RVA: 0x0001F229 File Offset: 0x0001D429
		// (set) Token: 0x06000829 RID: 2089 RVA: 0x0001F231 File Offset: 0x0001D431
		[DataSourceProperty]
		public MBBindingList<GamepadOptionKeyItemVM> FaceKeys
		{
			get
			{
				return this._faceKeys;
			}
			set
			{
				if (value != this._faceKeys)
				{
					this._faceKeys = value;
					base.OnPropertyChangedWithValue<MBBindingList<GamepadOptionKeyItemVM>>(value, "FaceKeys");
				}
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x0600082A RID: 2090 RVA: 0x0001F24F File Offset: 0x0001D44F
		// (set) Token: 0x0600082B RID: 2091 RVA: 0x0001F257 File Offset: 0x0001D457
		[DataSourceProperty]
		public MBBindingList<SelectorVM<SelectorItemVM>> Actions
		{
			get
			{
				return this._actions;
			}
			set
			{
				if (value != this._actions)
				{
					this._actions = value;
					base.OnPropertyChangedWithValue<MBBindingList<SelectorVM<SelectorItemVM>>>(value, "Actions");
				}
			}
		}

		// Token: 0x040003C5 RID: 965
		private SelectorVM<SelectorItemVM> _categories;

		// Token: 0x040003C6 RID: 966
		private int _currentGamepadType = -1;

		// Token: 0x040003C7 RID: 967
		private MBBindingList<GamepadOptionKeyItemVM> _leftAnalogKeys;

		// Token: 0x040003C8 RID: 968
		private MBBindingList<GamepadOptionKeyItemVM> _rightAnalogKeys;

		// Token: 0x040003C9 RID: 969
		private MBBindingList<GamepadOptionKeyItemVM> _dpadKeys;

		// Token: 0x040003CA RID: 970
		private MBBindingList<GamepadOptionKeyItemVM> _rightTriggerAndBumperKeys;

		// Token: 0x040003CB RID: 971
		private MBBindingList<GamepadOptionKeyItemVM> _leftTriggerAndBumperKeys;

		// Token: 0x040003CC RID: 972
		private MBBindingList<GamepadOptionKeyItemVM> _otherKeys;

		// Token: 0x040003CD RID: 973
		private MBBindingList<GamepadOptionKeyItemVM> _faceKeys;

		// Token: 0x040003CE RID: 974
		private MBBindingList<SelectorVM<SelectorItemVM>> _actions;

		// Token: 0x020000E5 RID: 229
		private enum GamepadType
		{
			// Token: 0x04000643 RID: 1603
			Xbox,
			// Token: 0x04000644 RID: 1604
			Playstation4,
			// Token: 0x04000645 RID: 1605
			Playstation5
		}
	}
}
