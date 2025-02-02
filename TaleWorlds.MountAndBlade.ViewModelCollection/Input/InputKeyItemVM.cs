using System;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.Input
{
	// Token: 0x0200003C RID: 60
	public class InputKeyItemVM : ViewModel
	{
		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x00016A50 File Offset: 0x00014C50
		// (set) Token: 0x0600053A RID: 1338 RVA: 0x00016A58 File Offset: 0x00014C58
		public GameKey GameKey { get; private set; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x00016A61 File Offset: 0x00014C61
		// (set) Token: 0x0600053C RID: 1340 RVA: 0x00016A69 File Offset: 0x00014C69
		public HotKey HotKey { get; private set; }

		// Token: 0x0600053D RID: 1341 RVA: 0x00016A72 File Offset: 0x00014C72
		private InputKeyItemVM()
		{
			Input.OnGamepadActiveStateChanged = (Action)Delegate.Combine(Input.OnGamepadActiveStateChanged, new Action(this.OnGamepadActiveStateChanged));
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00016A9A File Offset: 0x00014C9A
		public override void OnFinalize()
		{
			base.OnFinalize();
			Input.OnGamepadActiveStateChanged = (Action)Delegate.Remove(Input.OnGamepadActiveStateChanged, new Action(this.OnGamepadActiveStateChanged));
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00016AC2 File Offset: 0x00014CC2
		private void OnGamepadActiveStateChanged()
		{
			this.ForceRefresh();
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00016ACA File Offset: 0x00014CCA
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.ForceRefresh();
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00016AD8 File Offset: 0x00014CD8
		public void SetForcedVisibility(bool? isVisible)
		{
			this._forcedVisibility = isVisible;
			this.UpdateVisibility();
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00016AE8 File Offset: 0x00014CE8
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

		// Token: 0x06000543 RID: 1347 RVA: 0x00016D04 File Offset: 0x00014F04
		private void UpdateVisibility()
		{
			this.IsVisible = (this._forcedVisibility ?? (!this._isVisibleToConsoleOnly || Input.IsGamepadActive));
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00016D40 File Offset: 0x00014F40
		public static InputKeyItemVM CreateFromGameKey(GameKey gameKey, bool isConsoleOnly)
		{
			InputKeyItemVM inputKeyItemVM = new InputKeyItemVM();
			inputKeyItemVM.GameKey = gameKey;
			inputKeyItemVM._isVisibleToConsoleOnly = isConsoleOnly;
			inputKeyItemVM.ForceRefresh();
			return inputKeyItemVM;
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00016D5B File Offset: 0x00014F5B
		public static InputKeyItemVM CreateFromHotKey(HotKey hotKey, bool isConsoleOnly)
		{
			InputKeyItemVM inputKeyItemVM = new InputKeyItemVM();
			inputKeyItemVM.HotKey = hotKey;
			inputKeyItemVM._isVisibleToConsoleOnly = isConsoleOnly;
			inputKeyItemVM.ForceRefresh();
			return inputKeyItemVM;
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00016D76 File Offset: 0x00014F76
		public static InputKeyItemVM CreateFromHotKeyWithForcedName(HotKey hotKey, TextObject forcedName, bool isConsoleOnly)
		{
			InputKeyItemVM inputKeyItemVM = new InputKeyItemVM();
			inputKeyItemVM.HotKey = hotKey;
			inputKeyItemVM._isVisibleToConsoleOnly = isConsoleOnly;
			inputKeyItemVM._forcedName = forcedName;
			inputKeyItemVM.ForceRefresh();
			return inputKeyItemVM;
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00016D98 File Offset: 0x00014F98
		public static InputKeyItemVM CreateFromGameKeyWithForcedName(GameKey gameKey, TextObject forcedName, bool isConsoleOnly)
		{
			InputKeyItemVM inputKeyItemVM = new InputKeyItemVM();
			inputKeyItemVM.GameKey = gameKey;
			inputKeyItemVM._isVisibleToConsoleOnly = isConsoleOnly;
			inputKeyItemVM._forcedName = forcedName;
			inputKeyItemVM.ForceRefresh();
			return inputKeyItemVM;
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00016DBA File Offset: 0x00014FBA
		public static InputKeyItemVM CreateFromForcedID(string forcedID, TextObject forcedName, bool isConsoleOnly)
		{
			InputKeyItemVM inputKeyItemVM = new InputKeyItemVM();
			inputKeyItemVM._forcedID = forcedID;
			inputKeyItemVM._forcedName = forcedName;
			inputKeyItemVM._isVisibleToConsoleOnly = isConsoleOnly;
			inputKeyItemVM.ForceRefresh();
			return inputKeyItemVM;
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x00016DDC File Offset: 0x00014FDC
		// (set) Token: 0x0600054A RID: 1354 RVA: 0x00016DE4 File Offset: 0x00014FE4
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

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x00016E07 File Offset: 0x00015007
		// (set) Token: 0x0600054C RID: 1356 RVA: 0x00016E0F File Offset: 0x0001500F
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

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x00016E32 File Offset: 0x00015032
		// (set) Token: 0x0600054E RID: 1358 RVA: 0x00016E3A File Offset: 0x0001503A
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

		// Token: 0x04000289 RID: 649
		private bool _isVisibleToConsoleOnly;

		// Token: 0x0400028A RID: 650
		private TextObject _forcedName;

		// Token: 0x0400028B RID: 651
		private string _forcedID;

		// Token: 0x0400028C RID: 652
		private bool? _forcedVisibility;

		// Token: 0x0400028D RID: 653
		private string _keyID;

		// Token: 0x0400028E RID: 654
		private string _keyName;

		// Token: 0x0400028F RID: 655
		private bool _isVisible;
	}
}
