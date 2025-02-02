using System;
using TaleWorlds.Core;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Input
{
	// Token: 0x02000089 RID: 137
	public class InputKeyItemVM : ViewModel
	{
		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06000D95 RID: 3477 RVA: 0x00037354 File Offset: 0x00035554
		// (set) Token: 0x06000D96 RID: 3478 RVA: 0x0003735C File Offset: 0x0003555C
		public GameKey GameKey { get; private set; }

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06000D97 RID: 3479 RVA: 0x00037365 File Offset: 0x00035565
		// (set) Token: 0x06000D98 RID: 3480 RVA: 0x0003736D File Offset: 0x0003556D
		public HotKey HotKey { get; private set; }

		// Token: 0x06000D99 RID: 3481 RVA: 0x00037376 File Offset: 0x00035576
		private InputKeyItemVM()
		{
			Input.OnGamepadActiveStateChanged = (Action)Delegate.Combine(Input.OnGamepadActiveStateChanged, new Action(this.OnGamepadActiveStateChanged));
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x0003739E File Offset: 0x0003559E
		public override void OnFinalize()
		{
			base.OnFinalize();
			Input.OnGamepadActiveStateChanged = (Action)Delegate.Remove(Input.OnGamepadActiveStateChanged, new Action(this.OnGamepadActiveStateChanged));
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x000373C6 File Offset: 0x000355C6
		private void OnGamepadActiveStateChanged()
		{
			this.ForceRefresh();
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x000373CE File Offset: 0x000355CE
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.ForceRefresh();
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x000373DC File Offset: 0x000355DC
		public void SetForcedVisibility(bool? isVisible)
		{
			this._forcedVisibility = isVisible;
			this.UpdateVisibility();
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x000373EC File Offset: 0x000355EC
		private void ForceRefresh()
		{
			this.UpdateVisibility();
			if (this.GameKey != null && Game.Current != null)
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
				this.KeyName = (((forcedName != null) ? forcedName.ToString() : null) ?? Game.Current.GameTextManager.FindText("str_key_name", this.GameKey.GroupId + "_" + this.GameKey.StringId).ToString());
				return;
			}
			if (this.HotKey != null && Game.Current != null)
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
				this.KeyName = (((forcedName2 != null) ? forcedName2.ToString() : null) ?? Game.Current.GameTextManager.FindText("str_key_name", this.HotKey.GroupId + "_" + this.HotKey.Id).ToString());
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

		// Token: 0x06000D9F RID: 3487 RVA: 0x0003761C File Offset: 0x0003581C
		private void UpdateVisibility()
		{
			this.IsVisible = (this._forcedVisibility ?? (!this._isVisibleToConsoleOnly || Input.IsGamepadActive));
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x00037658 File Offset: 0x00035858
		public static InputKeyItemVM CreateFromGameKey(GameKey gameKey, bool isConsoleOnly)
		{
			InputKeyItemVM inputKeyItemVM = new InputKeyItemVM();
			inputKeyItemVM.GameKey = gameKey;
			inputKeyItemVM._isVisibleToConsoleOnly = isConsoleOnly;
			inputKeyItemVM.ForceRefresh();
			return inputKeyItemVM;
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x00037673 File Offset: 0x00035873
		public static InputKeyItemVM CreateFromHotKey(HotKey hotKey, bool isConsoleOnly)
		{
			InputKeyItemVM inputKeyItemVM = new InputKeyItemVM();
			inputKeyItemVM.HotKey = hotKey;
			inputKeyItemVM._isVisibleToConsoleOnly = isConsoleOnly;
			inputKeyItemVM.ForceRefresh();
			return inputKeyItemVM;
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x0003768E File Offset: 0x0003588E
		public static InputKeyItemVM CreateFromHotKeyWithForcedName(HotKey hotKey, TextObject forcedName, bool isConsoleOnly)
		{
			InputKeyItemVM inputKeyItemVM = new InputKeyItemVM();
			inputKeyItemVM.HotKey = hotKey;
			inputKeyItemVM._isVisibleToConsoleOnly = isConsoleOnly;
			inputKeyItemVM._forcedName = forcedName;
			inputKeyItemVM.ForceRefresh();
			return inputKeyItemVM;
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x000376B0 File Offset: 0x000358B0
		public static InputKeyItemVM CreateFromGameKeyWithForcedName(GameKey gameKey, TextObject forcedName, bool isConsoleOnly)
		{
			InputKeyItemVM inputKeyItemVM = new InputKeyItemVM();
			inputKeyItemVM.GameKey = gameKey;
			inputKeyItemVM._isVisibleToConsoleOnly = isConsoleOnly;
			inputKeyItemVM._forcedName = forcedName;
			inputKeyItemVM.ForceRefresh();
			return inputKeyItemVM;
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x000376D2 File Offset: 0x000358D2
		public static InputKeyItemVM CreateFromForcedID(string forcedID, TextObject forcedName, bool isConsoleOnly)
		{
			InputKeyItemVM inputKeyItemVM = new InputKeyItemVM();
			inputKeyItemVM._forcedID = forcedID;
			inputKeyItemVM._forcedName = forcedName;
			inputKeyItemVM._isVisibleToConsoleOnly = isConsoleOnly;
			inputKeyItemVM.ForceRefresh();
			return inputKeyItemVM;
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06000DA5 RID: 3493 RVA: 0x000376F4 File Offset: 0x000358F4
		// (set) Token: 0x06000DA6 RID: 3494 RVA: 0x000376FC File Offset: 0x000358FC
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

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06000DA7 RID: 3495 RVA: 0x0003771F File Offset: 0x0003591F
		// (set) Token: 0x06000DA8 RID: 3496 RVA: 0x00037727 File Offset: 0x00035927
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

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06000DA9 RID: 3497 RVA: 0x0003774A File Offset: 0x0003594A
		// (set) Token: 0x06000DAA RID: 3498 RVA: 0x00037752 File Offset: 0x00035952
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

		// Token: 0x0400063E RID: 1598
		private bool _isVisibleToConsoleOnly;

		// Token: 0x0400063F RID: 1599
		private TextObject _forcedName;

		// Token: 0x04000640 RID: 1600
		private string _forcedID;

		// Token: 0x04000641 RID: 1601
		private bool? _forcedVisibility;

		// Token: 0x04000642 RID: 1602
		private string _keyID;

		// Token: 0x04000643 RID: 1603
		private string _keyName;

		// Token: 0x04000644 RID: 1604
		private bool _isVisible;
	}
}
