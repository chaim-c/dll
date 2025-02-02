using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.ViewModelCollection.GameOptions.AuxiliaryKeys;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.GameOptions.GameKeys
{
	// Token: 0x02000068 RID: 104
	public class GameKeyOptionCategoryVM : ViewModel
	{
		// Token: 0x06000847 RID: 2119 RVA: 0x0001F95C File Offset: 0x0001DB5C
		public GameKeyOptionCategoryVM(Action<KeyOptionVM> onKeybindRequest, IEnumerable<string> gameKeyCategories)
		{
			this._gameKeyCategories = new Dictionary<string, List<GameKey>>();
			foreach (string key in gameKeyCategories)
			{
				this._gameKeyCategories.Add(key, new List<GameKey>());
			}
			this._onKeybindRequest = onKeybindRequest;
			this.GameKeyGroups = new MBBindingList<GameKeyGroupVM>();
			this._auxiliaryKeyCategories = new Dictionary<string, List<HotKey>>();
			this.AuxiliaryKeyGroups = new MBBindingList<AuxiliaryKeyGroupVM>();
			foreach (GameKeyContext gameKeyContext in HotKeyManager.GetAllCategories())
			{
				if (gameKeyContext.Type == GameKeyContext.GameKeyContextType.AuxiliarySerializedAndShownInOptions)
				{
					this._auxiliaryKeyCategories.Add(gameKeyContext.GameKeyCategoryId, new List<HotKey>());
					using (Dictionary<string, HotKey>.ValueCollection.Enumerator enumerator3 = gameKeyContext.RegisteredHotKeys.GetEnumerator())
					{
						while (enumerator3.MoveNext())
						{
							HotKey hotKey = enumerator3.Current;
							List<HotKey> list;
							if (hotKey != null && this._auxiliaryKeyCategories.TryGetValue(hotKey.GroupId, out list) && !list.Contains(hotKey))
							{
								list.Add(hotKey);
							}
						}
						continue;
					}
				}
				if (gameKeyContext.Type == GameKeyContext.GameKeyContextType.Default)
				{
					foreach (GameKey gameKey in gameKeyContext.RegisteredGameKeys)
					{
						List<GameKey> list2;
						if (gameKey != null && this._gameKeyCategories.TryGetValue(gameKey.MainCategoryId, out list2) && !list2.Contains(gameKey))
						{
							list2.Add(gameKey);
						}
					}
				}
			}
			foreach (KeyValuePair<string, List<GameKey>> keyValuePair in this._gameKeyCategories)
			{
				if (keyValuePair.Value.Count > 0)
				{
					this.GameKeyGroups.Add(new GameKeyGroupVM(keyValuePair.Key, keyValuePair.Value, this._onKeybindRequest, new Action<int, InputKey>(this.UpdateKeysOfGamekeysWithID)));
				}
			}
			foreach (KeyValuePair<string, List<HotKey>> keyValuePair2 in this._auxiliaryKeyCategories)
			{
				if (keyValuePair2.Value.Count > 0)
				{
					this.AuxiliaryKeyGroups.Add(new AuxiliaryKeyGroupVM(keyValuePair2.Key, keyValuePair2.Value, this._onKeybindRequest));
				}
			}
			this.RefreshValues();
			Input.OnGamepadActiveStateChanged = (Action)Delegate.Combine(Input.OnGamepadActiveStateChanged, new Action(this.OnGamepadActiveStateChanged));
			this.IsEnabled = !Input.IsGamepadActive;
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0001FC98 File Offset: 0x0001DE98
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Name = new TextObject("{=qmNeO8FG}Keybindings", null).ToString();
			this.ResetText = new TextObject("{=RVIKFCno}Reset to Defaults", null).ToString();
			this.GameKeyGroups.ApplyActionOnAllItems(delegate(GameKeyGroupVM x)
			{
				x.RefreshValues();
			});
			this.AuxiliaryKeyGroups.ApplyActionOnAllItems(delegate(AuxiliaryKeyGroupVM x)
			{
				x.RefreshValues();
			});
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0001FD2C File Offset: 0x0001DF2C
		private void OnGamepadActiveStateChanged()
		{
			MBBindingList<GameKeyGroupVM> gameKeyGroups = this.GameKeyGroups;
			if (gameKeyGroups != null)
			{
				gameKeyGroups.ApplyActionOnAllItems(delegate(GameKeyGroupVM g)
				{
					g.OnGamepadActiveStateChanged();
				});
			}
			this.AuxiliaryKeyGroups.ApplyActionOnAllItems(delegate(AuxiliaryKeyGroupVM x)
			{
				x.OnGamepadActiveStateChanged();
			});
			this.IsEnabled = !Input.IsGamepadActive;
			Debug.Print("KEYBINDS TAB ENABLED: " + this.IsEnabled.ToString(), 0, Debug.DebugColor.White, 17592186044416UL);
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0001FDCC File Offset: 0x0001DFCC
		public bool IsChanged()
		{
			if (!this.GameKeyGroups.Any((GameKeyGroupVM x) => x.IsChanged()))
			{
				return this.AuxiliaryKeyGroups.Any((AuxiliaryKeyGroupVM x) => x.IsChanged());
			}
			return true;
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0001FE34 File Offset: 0x0001E034
		public void ExecuteResetToDefault()
		{
			InformationManager.ShowInquiry(new InquiryData(new TextObject("{=4gCU2ykB}Reset all keys to default", null).ToString(), new TextObject("{=YjbNtFcw}This will reset ALL keys to their default states. You won't be able to undo this action. {newline} {newline}Are you sure?", null).ToString(), true, true, new TextObject("{=aeouhelq}Yes", null).ToString(), new TextObject("{=8OkPHu4f}No", null).ToString(), delegate()
			{
				this.ResetToDefault();
			}, null, "", 0f, null, null, null), false, false);
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x0001FEAC File Offset: 0x0001E0AC
		public void OnDone()
		{
			this.GameKeyGroups.ApplyActionOnAllItems(delegate(GameKeyGroupVM x)
			{
				x.OnDone();
			});
			this.AuxiliaryKeyGroups.ApplyActionOnAllItems(delegate(AuxiliaryKeyGroupVM x)
			{
				x.OnDone();
			});
			foreach (KeyValuePair<GameKey, InputKey> keyValuePair in this._keysToChangeOnDone)
			{
				Key key = this.FindValidInputKey(keyValuePair.Key);
				if (key != null)
				{
					key.ChangeKey(keyValuePair.Value);
				}
			}
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0001FF6C File Offset: 0x0001E16C
		private void ResetToDefault()
		{
			HotKeyManager.Reset();
			this.GameKeyGroups.ApplyActionOnAllItems(delegate(GameKeyGroupVM x)
			{
				x.Update();
			});
			this.AuxiliaryKeyGroups.ApplyActionOnAllItems(delegate(AuxiliaryKeyGroupVM x)
			{
				x.Update();
			});
			this._keysToChangeOnDone.Clear();
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x0001FFDD File Offset: 0x0001E1DD
		public override void OnFinalize()
		{
			base.OnFinalize();
			Input.OnGamepadActiveStateChanged = (Action)Delegate.Remove(Input.OnGamepadActiveStateChanged, new Action(this.OnGamepadActiveStateChanged));
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x00020005 File Offset: 0x0001E205
		private Key FindValidInputKey(GameKey gameKey)
		{
			if (!Input.IsGamepadActive)
			{
				return gameKey.KeyboardKey;
			}
			return gameKey.ControllerKey;
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0002001C File Offset: 0x0001E21C
		private void UpdateKeysOfGamekeysWithID(int givenId, InputKey newKey)
		{
			Func<GameKey, bool> <>9__0;
			foreach (GameKeyContext gameKeyContext in HotKeyManager.GetAllCategories())
			{
				if (gameKeyContext.Type == GameKeyContext.GameKeyContextType.Default)
				{
					IEnumerable<GameKey> registeredGameKeys = gameKeyContext.RegisteredGameKeys;
					Func<GameKey, bool> predicate;
					if ((predicate = <>9__0) == null)
					{
						predicate = (<>9__0 = ((GameKey k) => k != null && k.Id == givenId));
					}
					foreach (GameKey key in registeredGameKeys.Where(predicate))
					{
						if (this._keysToChangeOnDone.ContainsKey(key))
						{
							this._keysToChangeOnDone[key] = newKey;
						}
						else
						{
							this._keysToChangeOnDone.Add(key, newKey);
						}
					}
				}
			}
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x00020114 File Offset: 0x0001E314
		public void Cancel()
		{
			this.GameKeyGroups.ApplyActionOnAllItems(delegate(GameKeyGroupVM g)
			{
				g.Cancel();
			});
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x00020140 File Offset: 0x0001E340
		public void ApplyValues()
		{
			this.GameKeyGroups.ApplyActionOnAllItems(delegate(GameKeyGroupVM g)
			{
				g.ApplyValues();
			});
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000853 RID: 2131 RVA: 0x0002016C File Offset: 0x0001E36C
		// (set) Token: 0x06000854 RID: 2132 RVA: 0x00020174 File Offset: 0x0001E374
		[DataSourceProperty]
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if (value != this._name)
				{
					this._name = value;
					base.OnPropertyChangedWithValue<string>(value, "Name");
				}
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000855 RID: 2133 RVA: 0x00020197 File Offset: 0x0001E397
		// (set) Token: 0x06000856 RID: 2134 RVA: 0x0002019F File Offset: 0x0001E39F
		[DataSourceProperty]
		public bool IsEnabled
		{
			get
			{
				return this._isEnabled;
			}
			set
			{
				if (value != this._isEnabled)
				{
					this._isEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsEnabled");
				}
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000857 RID: 2135 RVA: 0x000201BD File Offset: 0x0001E3BD
		// (set) Token: 0x06000858 RID: 2136 RVA: 0x000201C5 File Offset: 0x0001E3C5
		[DataSourceProperty]
		public string ResetText
		{
			get
			{
				return this._resetText;
			}
			set
			{
				if (value != this._resetText)
				{
					this._resetText = value;
					base.OnPropertyChangedWithValue<string>(value, "ResetText");
				}
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000859 RID: 2137 RVA: 0x000201E8 File Offset: 0x0001E3E8
		// (set) Token: 0x0600085A RID: 2138 RVA: 0x000201F0 File Offset: 0x0001E3F0
		[DataSourceProperty]
		public MBBindingList<GameKeyGroupVM> GameKeyGroups
		{
			get
			{
				return this._gameKeyGroups;
			}
			set
			{
				if (value != this._gameKeyGroups)
				{
					this._gameKeyGroups = value;
					base.OnPropertyChangedWithValue<MBBindingList<GameKeyGroupVM>>(value, "GameKeyGroups");
				}
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x0600085B RID: 2139 RVA: 0x0002020E File Offset: 0x0001E40E
		// (set) Token: 0x0600085C RID: 2140 RVA: 0x00020216 File Offset: 0x0001E416
		[DataSourceProperty]
		public MBBindingList<AuxiliaryKeyGroupVM> AuxiliaryKeyGroups
		{
			get
			{
				return this._auxiliaryKeyGroups;
			}
			set
			{
				if (value != this._auxiliaryKeyGroups)
				{
					this._auxiliaryKeyGroups = value;
					base.OnPropertyChangedWithValue<MBBindingList<AuxiliaryKeyGroupVM>>(value, "AuxiliaryKeyGroups");
				}
			}
		}

		// Token: 0x040003DC RID: 988
		private readonly Action<KeyOptionVM> _onKeybindRequest;

		// Token: 0x040003DD RID: 989
		private Dictionary<string, List<GameKey>> _gameKeyCategories;

		// Token: 0x040003DE RID: 990
		private Dictionary<string, List<HotKey>> _auxiliaryKeyCategories;

		// Token: 0x040003DF RID: 991
		private Dictionary<GameKey, InputKey> _keysToChangeOnDone = new Dictionary<GameKey, InputKey>();

		// Token: 0x040003E0 RID: 992
		private string _name;

		// Token: 0x040003E1 RID: 993
		private string _resetText;

		// Token: 0x040003E2 RID: 994
		private bool _isEnabled;

		// Token: 0x040003E3 RID: 995
		private MBBindingList<GameKeyGroupVM> _gameKeyGroups;

		// Token: 0x040003E4 RID: 996
		private MBBindingList<AuxiliaryKeyGroupVM> _auxiliaryKeyGroups;
	}
}
