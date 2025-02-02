using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.GameOptions.AuxiliaryKeys
{
	// Token: 0x0200006A RID: 106
	public class AuxiliaryKeyGroupVM : ViewModel
	{
		// Token: 0x06000869 RID: 2153 RVA: 0x000204C4 File Offset: 0x0001E6C4
		public AuxiliaryKeyGroupVM(string categoryId, IEnumerable<HotKey> keys, Action<KeyOptionVM> onKeybindRequest)
		{
			this._onKeybindRequest = onKeybindRequest;
			this._categoryId = categoryId;
			this._hotKeys = new MBBindingList<AuxiliaryKeyOptionVM>();
			this._keys = keys;
			this.PopulateHotKeys();
			this.RefreshValues();
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x000204F8 File Offset: 0x0001E6F8
		private void PopulateHotKeys()
		{
			this.HotKeys.Clear();
			foreach (HotKey hotKey in this._keys)
			{
				bool flag;
				if (!Input.IsGamepadActive)
				{
					if (hotKey == null)
					{
						flag = false;
					}
					else
					{
						flag = hotKey.DefaultKeys.Any((Key x) => x != null && x.IsKeyboardInput && x.InputKey != InputKey.Invalid);
					}
				}
				else if (hotKey == null)
				{
					flag = false;
				}
				else
				{
					flag = hotKey.DefaultKeys.Any((Key x) => x != null && x.IsControllerInput && x.InputKey != InputKey.Invalid);
				}
				if (flag)
				{
					this.HotKeys.Add(new AuxiliaryKeyOptionVM(hotKey, this._onKeybindRequest, new Action<AuxiliaryKeyOptionVM, InputKey>(this.SetHotKey)));
				}
			}
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x000205E0 File Offset: 0x0001E7E0
		public override void RefreshValues()
		{
			base.RefreshValues();
			string description = this._categoryId;
			TextObject textObject;
			if (Module.CurrentModule.GlobalTextManager.TryGetText("str_hotkey_category_name", this._categoryId, out textObject))
			{
				description = textObject.ToString();
			}
			this.Description = description;
			this.HotKeys.ApplyActionOnAllItems(delegate(AuxiliaryKeyOptionVM x)
			{
				x.RefreshValues();
			});
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x00020650 File Offset: 0x0001E850
		private void SetHotKey(AuxiliaryKeyOptionVM option, InputKey newKey)
		{
			option.CurrentKey.ChangeKey(newKey);
			option.OptionValueText = Module.CurrentModule.GlobalTextManager.FindText("str_game_key_text", option.CurrentKey.ToString().ToLower()).ToString();
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x00020690 File Offset: 0x0001E890
		internal void Update()
		{
			foreach (AuxiliaryKeyOptionVM auxiliaryKeyOptionVM in this.HotKeys)
			{
				auxiliaryKeyOptionVM.Update();
			}
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x000206DC File Offset: 0x0001E8DC
		public void OnDone()
		{
			foreach (AuxiliaryKeyOptionVM auxiliaryKeyOptionVM in this.HotKeys)
			{
				auxiliaryKeyOptionVM.OnDone();
			}
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x00020728 File Offset: 0x0001E928
		internal bool IsChanged()
		{
			return this.HotKeys.Any((AuxiliaryKeyOptionVM k) => k.IsChanged());
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x00020754 File Offset: 0x0001E954
		public void OnGamepadActiveStateChanged()
		{
			this.Update();
			this.OnDone();
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000871 RID: 2161 RVA: 0x00020762 File Offset: 0x0001E962
		// (set) Token: 0x06000872 RID: 2162 RVA: 0x0002076A File Offset: 0x0001E96A
		[DataSourceProperty]
		public MBBindingList<AuxiliaryKeyOptionVM> HotKeys
		{
			get
			{
				return this._hotKeys;
			}
			set
			{
				if (value != this._hotKeys)
				{
					this._hotKeys = value;
					base.OnPropertyChangedWithValue<MBBindingList<AuxiliaryKeyOptionVM>>(value, "HotKeys");
				}
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000873 RID: 2163 RVA: 0x00020788 File Offset: 0x0001E988
		// (set) Token: 0x06000874 RID: 2164 RVA: 0x00020790 File Offset: 0x0001E990
		[DataSourceProperty]
		public string Description
		{
			get
			{
				return this._description;
			}
			set
			{
				if (value != this._description)
				{
					this._description = value;
					base.OnPropertyChangedWithValue<string>(value, "Description");
				}
			}
		}

		// Token: 0x040003E8 RID: 1000
		private readonly Action<KeyOptionVM> _onKeybindRequest;

		// Token: 0x040003E9 RID: 1001
		private readonly string _categoryId;

		// Token: 0x040003EA RID: 1002
		private IEnumerable<HotKey> _keys;

		// Token: 0x040003EB RID: 1003
		private string _description;

		// Token: 0x040003EC RID: 1004
		private MBBindingList<AuxiliaryKeyOptionVM> _hotKeys;
	}
}
