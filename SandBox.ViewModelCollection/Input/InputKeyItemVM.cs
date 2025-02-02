using System;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

namespace SandBox.ViewModelCollection.Input
{
	// Token: 0x02000037 RID: 55
	public class InputKeyItemVM : ViewModel
	{
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x0001294F File Offset: 0x00010B4F
		// (set) Token: 0x06000410 RID: 1040 RVA: 0x00012957 File Offset: 0x00010B57
		public GameKey GameKey { get; private set; }

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x00012960 File Offset: 0x00010B60
		// (set) Token: 0x06000412 RID: 1042 RVA: 0x00012968 File Offset: 0x00010B68
		public HotKey HotKey { get; private set; }

		// Token: 0x06000413 RID: 1043 RVA: 0x00012971 File Offset: 0x00010B71
		private InputKeyItemVM()
		{
			Input.OnGamepadActiveStateChanged = (Action)Delegate.Combine(Input.OnGamepadActiveStateChanged, new Action(this.OnGamepadActiveStateChanged));
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00012999 File Offset: 0x00010B99
		public override void OnFinalize()
		{
			base.OnFinalize();
			Input.OnGamepadActiveStateChanged = (Action)Delegate.Remove(Input.OnGamepadActiveStateChanged, new Action(this.OnGamepadActiveStateChanged));
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x000129C1 File Offset: 0x00010BC1
		private void OnGamepadActiveStateChanged()
		{
			this.ForceRefresh();
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x000129C9 File Offset: 0x00010BC9
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.ForceRefresh();
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x000129D7 File Offset: 0x00010BD7
		public void SetForcedVisibility(bool? isVisible)
		{
			this._forcedVisibility = isVisible;
			this.UpdateVisibility();
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x000129E8 File Offset: 0x00010BE8
		private void ForceRefresh()
		{
			this.UpdateVisibility();
			if (this.GameKey != null)
			{
				string keyID;
				if (!Input.IsGamepadActive)
				{
					Key keyboardKey = this.GameKey.KeyboardKey;
					keyID = ((keyboardKey != null) ? keyboardKey.InputKey.ToString() : null);
				}
				else
				{
					Key controllerKey = this.GameKey.ControllerKey;
					keyID = ((controllerKey != null) ? controllerKey.InputKey.ToString() : null);
				}
				this.KeyID = keyID;
				TextObject forcedName = this._forcedName;
				this.KeyName = (((forcedName != null) ? forcedName.ToString() : null) ?? Module.CurrentModule.GlobalTextManager.FindText("str_key_name", this.GameKey.GroupId + "_" + this.GameKey.StringId).ToString());
				return;
			}
			if (this.HotKey != null)
			{
				string keyID2;
				if (!Input.IsGamepadActive)
				{
					Key key = this.HotKey.Keys.Find((Key k) => !k.IsControllerInput);
					keyID2 = ((key != null) ? key.InputKey.ToString() : null);
				}
				else
				{
					Key key2 = this.HotKey.Keys.Find((Key k) => k.IsControllerInput);
					keyID2 = ((key2 != null) ? key2.InputKey.ToString() : null);
				}
				this.KeyID = keyID2;
				TextObject forcedName2 = this._forcedName;
				this.KeyName = (((forcedName2 != null) ? forcedName2.ToString() : null) ?? Module.CurrentModule.GlobalTextManager.FindText("str_key_name", this.HotKey.GroupId + "_" + this.HotKey.Id).ToString());
				return;
			}
			if (this._forcedID != null)
			{
				this.KeyID = this._forcedID;
				TextObject forcedName3 = this._forcedName;
				this.KeyName = (((forcedName3 != null) ? forcedName3.ToString() : null) ?? string.Empty);
				return;
			}
			this.KeyID = string.Empty;
			this.KeyName = string.Empty;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00012C04 File Offset: 0x00010E04
		private void UpdateVisibility()
		{
			this.IsVisible = (this._forcedVisibility ?? (!this._isVisibleToConsoleOnly || Input.IsGamepadActive));
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00012C40 File Offset: 0x00010E40
		public static InputKeyItemVM CreateFromGameKey(GameKey gameKey, bool isConsoleOnly)
		{
			InputKeyItemVM inputKeyItemVM = new InputKeyItemVM();
			inputKeyItemVM.GameKey = gameKey;
			inputKeyItemVM._isVisibleToConsoleOnly = isConsoleOnly;
			inputKeyItemVM.ForceRefresh();
			return inputKeyItemVM;
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00012C5B File Offset: 0x00010E5B
		public static InputKeyItemVM CreateFromHotKey(HotKey hotKey, bool isConsoleOnly)
		{
			InputKeyItemVM inputKeyItemVM = new InputKeyItemVM();
			inputKeyItemVM.HotKey = hotKey;
			inputKeyItemVM._isVisibleToConsoleOnly = isConsoleOnly;
			inputKeyItemVM.ForceRefresh();
			return inputKeyItemVM;
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00012C76 File Offset: 0x00010E76
		public static InputKeyItemVM CreateFromHotKeyWithForcedName(HotKey hotKey, TextObject forcedName, bool isConsoleOnly)
		{
			InputKeyItemVM inputKeyItemVM = new InputKeyItemVM();
			inputKeyItemVM.HotKey = hotKey;
			inputKeyItemVM._isVisibleToConsoleOnly = isConsoleOnly;
			inputKeyItemVM._forcedName = forcedName;
			inputKeyItemVM.ForceRefresh();
			return inputKeyItemVM;
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00012C98 File Offset: 0x00010E98
		public static InputKeyItemVM CreateFromGameKeyWithForcedName(GameKey gameKey, TextObject forcedName, bool isConsoleOnly)
		{
			InputKeyItemVM inputKeyItemVM = new InputKeyItemVM();
			inputKeyItemVM.GameKey = gameKey;
			inputKeyItemVM._isVisibleToConsoleOnly = isConsoleOnly;
			inputKeyItemVM._forcedName = forcedName;
			inputKeyItemVM.ForceRefresh();
			return inputKeyItemVM;
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00012CBA File Offset: 0x00010EBA
		public static InputKeyItemVM CreateFromForcedID(string forcedID, TextObject forcedName)
		{
			InputKeyItemVM inputKeyItemVM = new InputKeyItemVM();
			inputKeyItemVM._forcedID = forcedID;
			inputKeyItemVM._forcedName = forcedName;
			inputKeyItemVM.ForceRefresh();
			return inputKeyItemVM;
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x00012CD5 File Offset: 0x00010ED5
		// (set) Token: 0x06000420 RID: 1056 RVA: 0x00012CDD File Offset: 0x00010EDD
		[DataSourceProperty]
		public string KeyID
		{
			get
			{
				return this._keyID;
			}
			set
			{
				if (value != this._keyID)
				{
					this._keyID = value;
					base.OnPropertyChangedWithValue<string>(value, "KeyID");
				}
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x00012D00 File Offset: 0x00010F00
		// (set) Token: 0x06000422 RID: 1058 RVA: 0x00012D08 File Offset: 0x00010F08
		[DataSourceProperty]
		public string KeyName
		{
			get
			{
				return this._keyName;
			}
			set
			{
				if (value != this._keyName)
				{
					this._keyName = value;
					base.OnPropertyChangedWithValue<string>(value, "KeyName");
				}
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x00012D2B File Offset: 0x00010F2B
		// (set) Token: 0x06000424 RID: 1060 RVA: 0x00012D33 File Offset: 0x00010F33
		[DataSourceProperty]
		public bool IsVisible
		{
			get
			{
				return this._isVisible;
			}
			set
			{
				if (value != this._isVisible)
				{
					this._isVisible = value;
					base.OnPropertyChangedWithValue(value, "IsVisible");
				}
			}
		}

		// Token: 0x04000222 RID: 546
		private bool _isVisibleToConsoleOnly;

		// Token: 0x04000223 RID: 547
		private TextObject _forcedName;

		// Token: 0x04000224 RID: 548
		private string _forcedID;

		// Token: 0x04000225 RID: 549
		private bool? _forcedVisibility;

		// Token: 0x04000226 RID: 550
		private string _keyID;

		// Token: 0x04000227 RID: 551
		private string _keyName;

		// Token: 0x04000228 RID: 552
		private bool _isVisible;
	}
}
