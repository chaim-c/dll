using System;
using System.Linq;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.GameOptions.GamepadOptions
{
	// Token: 0x02000066 RID: 102
	public class GamepadOptionKeyItemVM : ViewModel
	{
		// Token: 0x1700026A RID: 618
		// (get) Token: 0x0600082C RID: 2092 RVA: 0x0001F275 File Offset: 0x0001D475
		public GameKey GamepadKey { get; }

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x0600082D RID: 2093 RVA: 0x0001F27D File Offset: 0x0001D47D
		public HotKey GamepadHotKey { get; }

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x0600082E RID: 2094 RVA: 0x0001F285 File Offset: 0x0001D485
		public InputKey? Key { get; }

		// Token: 0x0600082F RID: 2095 RVA: 0x0001F290 File Offset: 0x0001D490
		public GamepadOptionKeyItemVM(GameKey gamepadGameKey)
		{
			this.GamepadKey = gamepadGameKey;
			this.Action = Module.CurrentModule.GlobalTextManager.FindText("str_key_name", this.GamepadKey.GroupId + "_" + this.GamepadKey.StringId).ToString();
			this.Key = new InputKey?(gamepadGameKey.ControllerKey.InputKey);
			this.KeyId = (int)this.Key.Value;
			string text;
			if (gamepadGameKey == null)
			{
				text = null;
			}
			else
			{
				Key controllerKey = gamepadGameKey.ControllerKey;
				text = ((controllerKey != null) ? controllerKey.InputKey.ToString() : null);
			}
			this.KeyIdAsString = (text ?? string.Empty);
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x0001F348 File Offset: 0x0001D548
		public GamepadOptionKeyItemVM(HotKey gamepadHotKey)
		{
			this.GamepadHotKey = gamepadHotKey;
			this.Action = Module.CurrentModule.GlobalTextManager.FindText("str_key_name", this.GamepadHotKey.GroupId + "_" + this.GamepadHotKey.Id).ToString();
			Key key = gamepadHotKey.Keys.FirstOrDefault((Key k) => k.IsControllerInput);
			InputKey? inputKey;
			InputKey? inputKey2;
			if (key == null)
			{
				inputKey = null;
				inputKey2 = inputKey;
			}
			else
			{
				inputKey2 = new InputKey?(key.InputKey);
			}
			this.Key = inputKey2;
			inputKey = this.Key;
			this.KeyId = (int)inputKey.Value;
			inputKey = this.Key;
			this.KeyIdAsString = (((inputKey != null) ? inputKey.GetValueOrDefault().ToString() : null) ?? string.Empty);
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x0001F438 File Offset: 0x0001D638
		public GamepadOptionKeyItemVM(InputKey key, TextObject name)
		{
			this.Key = new InputKey?(key);
			InputKey? key2 = this.Key;
			this.KeyId = (int)key2.Value;
			key2 = this.Key;
			this.KeyIdAsString = (((key2 != null) ? key2.GetValueOrDefault().ToString() : null) ?? string.Empty);
			this._nameObject = name;
			this.Action = this._nameObject.ToString();
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0001F4BC File Offset: 0x0001D6BC
		public override void RefreshValues()
		{
			base.RefreshValues();
			if (this.GamepadKey != null)
			{
				this.Action = Module.CurrentModule.GlobalTextManager.FindText("str_key_name", this.GamepadKey.GroupId + "_" + this.GamepadKey.StringId).ToString();
				return;
			}
			if (this.GamepadHotKey != null)
			{
				this.Action = Module.CurrentModule.GlobalTextManager.FindText("str_key_name", this.GamepadHotKey.GroupId + "_" + this.GamepadHotKey.Id).ToString();
				return;
			}
			if (this._nameObject != null)
			{
				this.Action = this._nameObject.ToString();
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000833 RID: 2099 RVA: 0x0001F578 File Offset: 0x0001D778
		// (set) Token: 0x06000834 RID: 2100 RVA: 0x0001F580 File Offset: 0x0001D780
		[DataSourceProperty]
		public string Action
		{
			get
			{
				return this._action;
			}
			set
			{
				if (value != this._action)
				{
					this._action = value;
					base.OnPropertyChangedWithValue<string>(value, "Action");
				}
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000835 RID: 2101 RVA: 0x0001F5A3 File Offset: 0x0001D7A3
		// (set) Token: 0x06000836 RID: 2102 RVA: 0x0001F5AB File Offset: 0x0001D7AB
		[DataSourceProperty]
		public int KeyId
		{
			get
			{
				return this._keyId;
			}
			set
			{
				if (value != this._keyId)
				{
					this._keyId = value;
					base.OnPropertyChangedWithValue(value, "KeyId");
				}
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000837 RID: 2103 RVA: 0x0001F5C9 File Offset: 0x0001D7C9
		// (set) Token: 0x06000838 RID: 2104 RVA: 0x0001F5D1 File Offset: 0x0001D7D1
		[DataSourceProperty]
		public string KeyIdAsString
		{
			get
			{
				return this._keyIdAsString;
			}
			set
			{
				if (value != this._keyIdAsString)
				{
					this._keyIdAsString = value;
					base.OnPropertyChangedWithValue<string>(value, "KeyIdAsString");
				}
			}
		}

		// Token: 0x040003D1 RID: 977
		private TextObject _nameObject;

		// Token: 0x040003D3 RID: 979
		private string _action;

		// Token: 0x040003D4 RID: 980
		private string _keyIdAsString;

		// Token: 0x040003D5 RID: 981
		private int _keyId;
	}
}
