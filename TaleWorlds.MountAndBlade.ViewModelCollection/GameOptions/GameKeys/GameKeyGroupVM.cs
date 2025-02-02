using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.GameOptions.GameKeys
{
	// Token: 0x02000067 RID: 103
	public class GameKeyGroupVM : ViewModel
	{
		// Token: 0x06000839 RID: 2105 RVA: 0x0001F5F4 File Offset: 0x0001D7F4
		public GameKeyGroupVM(string categoryId, IEnumerable<GameKey> keys, Action<KeyOptionVM> onKeybindRequest, Action<int, InputKey> setAllKeysOfId)
		{
			this._onKeybindRequest = onKeybindRequest;
			this._setAllKeysOfId = setAllKeysOfId;
			this._categoryId = categoryId;
			this._gameKeys = new MBBindingList<GameKeyOptionVM>();
			this._keys = keys;
			this.PopulateGameKeys();
			this.RefreshValues();
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x0001F630 File Offset: 0x0001D830
		private void PopulateGameKeys()
		{
			this.GameKeys.Clear();
			foreach (GameKey gameKey in this._keys)
			{
				if (Input.IsGamepadActive ? (((gameKey != null) ? gameKey.DefaultControllerKey : null) != null && (gameKey == null || gameKey.DefaultControllerKey.InputKey != InputKey.Invalid)) : (((gameKey != null) ? gameKey.DefaultKeyboardKey : null) != null && (gameKey == null || gameKey.DefaultKeyboardKey.InputKey != InputKey.Invalid)))
				{
					this.GameKeys.Add(new GameKeyOptionVM(gameKey, this._onKeybindRequest, new Action<GameKeyOptionVM, InputKey>(this.SetGameKey)));
				}
			}
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x0001F714 File Offset: 0x0001D914
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Description = Module.CurrentModule.GlobalTextManager.FindText("str_key_category_name", this._categoryId).ToString();
			this.GameKeys.ApplyActionOnAllItems(delegate(GameKeyOptionVM x)
			{
				x.RefreshValues();
			});
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x0001F778 File Offset: 0x0001D978
		private void SetGameKey(GameKeyOptionVM option, InputKey newKey)
		{
			option.CurrentKey.ChangeKey(newKey);
			option.OptionValueText = Module.CurrentModule.GlobalTextManager.FindText("str_game_key_text", option.CurrentKey.ToString().ToLower()).ToString();
			this._setAllKeysOfId(option.CurrentGameKey.Id, newKey);
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x0001F7D8 File Offset: 0x0001D9D8
		internal void Update()
		{
			foreach (GameKeyOptionVM gameKeyOptionVM in this.GameKeys)
			{
				gameKeyOptionVM.Update();
			}
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x0001F824 File Offset: 0x0001DA24
		public void OnDone()
		{
			foreach (GameKeyOptionVM gameKeyOptionVM in this.GameKeys)
			{
				gameKeyOptionVM.OnDone();
			}
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x0001F870 File Offset: 0x0001DA70
		internal bool IsChanged()
		{
			return this.GameKeys.Any((GameKeyOptionVM k) => k.IsChanged());
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x0001F89C File Offset: 0x0001DA9C
		public void OnGamepadActiveStateChanged()
		{
			this.PopulateGameKeys();
			this.Update();
			this.OnDone();
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0001F8B0 File Offset: 0x0001DAB0
		public void Cancel()
		{
			this.GameKeys.ApplyActionOnAllItems(delegate(GameKeyOptionVM g)
			{
				g.Revert();
			});
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x0001F8DC File Offset: 0x0001DADC
		public void ApplyValues()
		{
			this.GameKeys.ApplyActionOnAllItems(delegate(GameKeyOptionVM g)
			{
				g.Apply();
			});
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000843 RID: 2115 RVA: 0x0001F908 File Offset: 0x0001DB08
		// (set) Token: 0x06000844 RID: 2116 RVA: 0x0001F910 File Offset: 0x0001DB10
		[DataSourceProperty]
		public MBBindingList<GameKeyOptionVM> GameKeys
		{
			get
			{
				return this._gameKeys;
			}
			set
			{
				if (value != this._gameKeys)
				{
					this._gameKeys = value;
					base.OnPropertyChangedWithValue<MBBindingList<GameKeyOptionVM>>(value, "GameKeys");
				}
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x0001F92E File Offset: 0x0001DB2E
		// (set) Token: 0x06000846 RID: 2118 RVA: 0x0001F936 File Offset: 0x0001DB36
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

		// Token: 0x040003D6 RID: 982
		private readonly Action<KeyOptionVM> _onKeybindRequest;

		// Token: 0x040003D7 RID: 983
		private readonly Action<int, InputKey> _setAllKeysOfId;

		// Token: 0x040003D8 RID: 984
		private readonly string _categoryId;

		// Token: 0x040003D9 RID: 985
		private IEnumerable<GameKey> _keys;

		// Token: 0x040003DA RID: 986
		private string _description;

		// Token: 0x040003DB RID: 987
		private MBBindingList<GameKeyOptionVM> _gameKeys;
	}
}
