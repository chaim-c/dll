using System;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.ViewModelCollection.Input;
using TaleWorlds.Core;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.TroopSelection
{
	// Token: 0x0200008E RID: 142
	public class GameMenuTroopSelectionVM : ViewModel
	{
		// Token: 0x06000DFB RID: 3579 RVA: 0x00038DD4 File Offset: 0x00036FD4
		public GameMenuTroopSelectionVM(TroopRoster fullRoster, TroopRoster initialSelections, Func<CharacterObject, bool> canChangeChangeStatusOfTroop, Action<TroopRoster> onDone, int maxSelectableTroopCount, int minSelectableTroopCount)
		{
			this._canChangeChangeStatusOfTroop = canChangeChangeStatusOfTroop;
			this._onDone = onDone;
			this._fullRoster = fullRoster;
			this._initialSelections = initialSelections;
			this._maxSelectableTroopCount = maxSelectableTroopCount;
			this._minSelectableTroopCount = minSelectableTroopCount;
			this.InitList();
			this.RefreshValues();
			this.OnCurrentSelectedAmountChange();
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x00038E48 File Offset: 0x00037048
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.TitleText = this._titleTextObject.ToString();
			this.CurrentSelectedAmountTitle = this._chosenTitleTextObject.ToString();
			this.DoneText = GameTexts.FindText("str_done", null).ToString();
			this.CancelText = GameTexts.FindText("str_cancel", null).ToString();
			this.ClearSelectionText = new TextObject("{=QMNWbmao}Clear Selection", null).ToString();
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x00038EC0 File Offset: 0x000370C0
		private void InitList()
		{
			this.Troops = new MBBindingList<TroopSelectionItemVM>();
			this._currentTotalSelectedTroopCount = 0;
			foreach (TroopRosterElement troopRosterElement in this._fullRoster.GetTroopRoster())
			{
				TroopSelectionItemVM troopSelectionItemVM = new TroopSelectionItemVM(troopRosterElement, new Action<TroopSelectionItemVM>(this.OnAddCount), new Action<TroopSelectionItemVM>(this.OnRemoveCount));
				troopSelectionItemVM.IsLocked = (!this._canChangeChangeStatusOfTroop(troopRosterElement.Character) || troopRosterElement.Number - troopRosterElement.WoundedNumber <= 0);
				this.Troops.Add(troopSelectionItemVM);
				int troopCount = this._initialSelections.GetTroopCount(troopRosterElement.Character);
				if (troopCount > 0)
				{
					troopSelectionItemVM.CurrentAmount = troopCount;
					this._currentTotalSelectedTroopCount += troopCount;
				}
			}
			this.Troops.Sort(new TroopItemComparer());
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x00038FC0 File Offset: 0x000371C0
		private void OnRemoveCount(TroopSelectionItemVM troopItem)
		{
			if (troopItem.CurrentAmount > 0)
			{
				int num = 1;
				if (this.IsEntireStackModifierActive)
				{
					num = troopItem.CurrentAmount;
				}
				else if (this.IsFiveStackModifierActive)
				{
					num = MathF.Min(troopItem.CurrentAmount, 5);
				}
				troopItem.CurrentAmount -= num;
				this._currentTotalSelectedTroopCount -= num;
			}
			this.OnCurrentSelectedAmountChange();
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x00039020 File Offset: 0x00037220
		private void OnAddCount(TroopSelectionItemVM troopItem)
		{
			if (troopItem.CurrentAmount < troopItem.MaxAmount && this._currentTotalSelectedTroopCount < this._maxSelectableTroopCount)
			{
				int num = 1;
				if (this.IsEntireStackModifierActive)
				{
					num = MathF.Min(troopItem.MaxAmount - troopItem.CurrentAmount, this._maxSelectableTroopCount - this._currentTotalSelectedTroopCount);
				}
				else if (this.IsFiveStackModifierActive)
				{
					num = MathF.Min(MathF.Min(troopItem.MaxAmount - troopItem.CurrentAmount, this._maxSelectableTroopCount - this._currentTotalSelectedTroopCount), 5);
				}
				troopItem.CurrentAmount += num;
				this._currentTotalSelectedTroopCount += num;
			}
			this.OnCurrentSelectedAmountChange();
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x000390C8 File Offset: 0x000372C8
		private void OnCurrentSelectedAmountChange()
		{
			foreach (TroopSelectionItemVM troopSelectionItemVM in this.Troops)
			{
				troopSelectionItemVM.IsRosterFull = (this._currentTotalSelectedTroopCount >= this._maxSelectableTroopCount);
			}
			GameTexts.SetVariable("LEFT", this._currentTotalSelectedTroopCount);
			GameTexts.SetVariable("RIGHT", this._maxSelectableTroopCount);
			this.CurrentSelectedAmountText = GameTexts.FindText("str_LEFT_over_RIGHT_in_paranthesis", null).ToString();
			this.IsDoneEnabled = (this._currentTotalSelectedTroopCount <= this._maxSelectableTroopCount && this._currentTotalSelectedTroopCount >= this._minSelectableTroopCount);
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x00039184 File Offset: 0x00037384
		private void OnDone()
		{
			TroopRoster troopRoster = TroopRoster.CreateDummyTroopRoster();
			foreach (TroopSelectionItemVM troopSelectionItemVM in this.Troops)
			{
				if (troopSelectionItemVM.CurrentAmount > 0)
				{
					troopRoster.AddToCounts(troopSelectionItemVM.Troop.Character, troopSelectionItemVM.CurrentAmount, false, 0, 0, true, -1);
				}
			}
			this.IsEnabled = false;
			this._onDone.DynamicInvokeWithLog(new object[]
			{
				troopRoster
			});
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x00039214 File Offset: 0x00037414
		public void ExecuteDone()
		{
			if (this._currentTotalSelectedTroopCount < this._maxSelectableTroopCount)
			{
				string text = new TextObject("{=z2Slmx4N}There are still some room for more soldiers. Do you want to proceed?", null).ToString();
				InformationManager.ShowInquiry(new InquiryData(this.TitleText, text, true, true, GameTexts.FindText("str_yes", null).ToString(), GameTexts.FindText("str_no", null).ToString(), new Action(this.OnDone), null, "", 0f, null, null, null), false, false);
				return;
			}
			this.OnDone();
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x00039296 File Offset: 0x00037496
		public void ExecuteCancel()
		{
			this.IsEnabled = false;
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x0003929F File Offset: 0x0003749F
		public void ExecuteReset()
		{
			this.InitList();
			this.OnCurrentSelectedAmountChange();
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x000392AD File Offset: 0x000374AD
		public void ExecuteClearSelection()
		{
			this.Troops.ApplyActionOnAllItems(delegate(TroopSelectionItemVM troopItem)
			{
				if (this._canChangeChangeStatusOfTroop(troopItem.Troop.Character))
				{
					int currentAmount = troopItem.CurrentAmount;
					for (int i = 0; i < currentAmount; i++)
					{
						troopItem.ExecuteRemove();
					}
				}
			});
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x000392C6 File Offset: 0x000374C6
		public override void OnFinalize()
		{
			base.OnFinalize();
			InputKeyItemVM cancelInputKey = this.CancelInputKey;
			if (cancelInputKey != null)
			{
				cancelInputKey.OnFinalize();
			}
			InputKeyItemVM doneInputKey = this.DoneInputKey;
			if (doneInputKey != null)
			{
				doneInputKey.OnFinalize();
			}
			InputKeyItemVM resetInputKey = this.ResetInputKey;
			if (resetInputKey == null)
			{
				return;
			}
			resetInputKey.OnFinalize();
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x00039300 File Offset: 0x00037500
		public void SetCancelInputKey(HotKey hotkey)
		{
			this.CancelInputKey = InputKeyItemVM.CreateFromHotKey(hotkey, true);
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x0003930F File Offset: 0x0003750F
		public void SetDoneInputKey(HotKey hotkey)
		{
			this.DoneInputKey = InputKeyItemVM.CreateFromHotKey(hotkey, true);
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x0003931E File Offset: 0x0003751E
		public void SetResetInputKey(HotKey hotkey)
		{
			this.ResetInputKey = InputKeyItemVM.CreateFromHotKey(hotkey, true);
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06000E0A RID: 3594 RVA: 0x0003932D File Offset: 0x0003752D
		// (set) Token: 0x06000E0B RID: 3595 RVA: 0x00039335 File Offset: 0x00037535
		[DataSourceProperty]
		public InputKeyItemVM DoneInputKey
		{
			get
			{
				return this._doneInputKey;
			}
			set
			{
				if (value != this._doneInputKey)
				{
					this._doneInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "DoneInputKey");
				}
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06000E0C RID: 3596 RVA: 0x00039353 File Offset: 0x00037553
		// (set) Token: 0x06000E0D RID: 3597 RVA: 0x0003935B File Offset: 0x0003755B
		[DataSourceProperty]
		public InputKeyItemVM CancelInputKey
		{
			get
			{
				return this._cancelInputKey;
			}
			set
			{
				if (value != this._cancelInputKey)
				{
					this._cancelInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "CancelInputKey");
				}
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06000E0E RID: 3598 RVA: 0x00039379 File Offset: 0x00037579
		// (set) Token: 0x06000E0F RID: 3599 RVA: 0x00039381 File Offset: 0x00037581
		[DataSourceProperty]
		public InputKeyItemVM ResetInputKey
		{
			get
			{
				return this._resetInputKey;
			}
			set
			{
				if (value != this._resetInputKey)
				{
					this._resetInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "ResetInputKey");
				}
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06000E10 RID: 3600 RVA: 0x0003939F File Offset: 0x0003759F
		// (set) Token: 0x06000E11 RID: 3601 RVA: 0x000393A7 File Offset: 0x000375A7
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

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06000E12 RID: 3602 RVA: 0x000393C5 File Offset: 0x000375C5
		// (set) Token: 0x06000E13 RID: 3603 RVA: 0x000393CD File Offset: 0x000375CD
		[DataSourceProperty]
		public bool IsDoneEnabled
		{
			get
			{
				return this._isDoneEnabled;
			}
			set
			{
				if (value != this._isDoneEnabled)
				{
					this._isDoneEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsDoneEnabled");
				}
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06000E14 RID: 3604 RVA: 0x000393EB File Offset: 0x000375EB
		// (set) Token: 0x06000E15 RID: 3605 RVA: 0x000393F3 File Offset: 0x000375F3
		[DataSourceProperty]
		public MBBindingList<TroopSelectionItemVM> Troops
		{
			get
			{
				return this._troops;
			}
			set
			{
				if (value != this._troops)
				{
					this._troops = value;
					base.OnPropertyChangedWithValue<MBBindingList<TroopSelectionItemVM>>(value, "Troops");
				}
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06000E16 RID: 3606 RVA: 0x00039411 File Offset: 0x00037611
		// (set) Token: 0x06000E17 RID: 3607 RVA: 0x00039419 File Offset: 0x00037619
		[DataSourceProperty]
		public string DoneText
		{
			get
			{
				return this._doneText;
			}
			set
			{
				if (value != this._doneText)
				{
					this._doneText = value;
					base.OnPropertyChangedWithValue<string>(value, "DoneText");
				}
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06000E18 RID: 3608 RVA: 0x0003943C File Offset: 0x0003763C
		// (set) Token: 0x06000E19 RID: 3609 RVA: 0x00039444 File Offset: 0x00037644
		[DataSourceProperty]
		public string CancelText
		{
			get
			{
				return this._cancelText;
			}
			set
			{
				if (value != this._cancelText)
				{
					this._cancelText = value;
					base.OnPropertyChangedWithValue<string>(value, "CancelText");
				}
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06000E1A RID: 3610 RVA: 0x00039467 File Offset: 0x00037667
		// (set) Token: 0x06000E1B RID: 3611 RVA: 0x0003946F File Offset: 0x0003766F
		[DataSourceProperty]
		public string TitleText
		{
			get
			{
				return this._titleText;
			}
			set
			{
				if (value != this._titleText)
				{
					this._titleText = value;
					base.OnPropertyChangedWithValue<string>(value, "TitleText");
				}
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06000E1C RID: 3612 RVA: 0x00039492 File Offset: 0x00037692
		// (set) Token: 0x06000E1D RID: 3613 RVA: 0x0003949A File Offset: 0x0003769A
		[DataSourceProperty]
		public string ClearSelectionText
		{
			get
			{
				return this._clearSelectionText;
			}
			set
			{
				if (value != this._clearSelectionText)
				{
					this._clearSelectionText = value;
					base.OnPropertyChangedWithValue<string>(value, "ClearSelectionText");
				}
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06000E1E RID: 3614 RVA: 0x000394BD File Offset: 0x000376BD
		// (set) Token: 0x06000E1F RID: 3615 RVA: 0x000394C5 File Offset: 0x000376C5
		[DataSourceProperty]
		public string CurrentSelectedAmountText
		{
			get
			{
				return this._currentSelectedAmountText;
			}
			set
			{
				if (value != this._currentSelectedAmountText)
				{
					this._currentSelectedAmountText = value;
					base.OnPropertyChangedWithValue<string>(value, "CurrentSelectedAmountText");
				}
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06000E20 RID: 3616 RVA: 0x000394E8 File Offset: 0x000376E8
		// (set) Token: 0x06000E21 RID: 3617 RVA: 0x000394F0 File Offset: 0x000376F0
		[DataSourceProperty]
		public string CurrentSelectedAmountTitle
		{
			get
			{
				return this._currentSelectedAmountTitle;
			}
			set
			{
				if (value != this._currentSelectedAmountTitle)
				{
					this._currentSelectedAmountTitle = value;
					base.OnPropertyChangedWithValue<string>(value, "CurrentSelectedAmountTitle");
				}
			}
		}

		// Token: 0x0400067C RID: 1660
		private readonly Action<TroopRoster> _onDone;

		// Token: 0x0400067D RID: 1661
		private readonly TroopRoster _fullRoster;

		// Token: 0x0400067E RID: 1662
		private readonly TroopRoster _initialSelections;

		// Token: 0x0400067F RID: 1663
		private readonly Func<CharacterObject, bool> _canChangeChangeStatusOfTroop;

		// Token: 0x04000680 RID: 1664
		private readonly int _maxSelectableTroopCount;

		// Token: 0x04000681 RID: 1665
		private readonly int _minSelectableTroopCount;

		// Token: 0x04000682 RID: 1666
		private readonly TextObject _titleTextObject = new TextObject("{=uQgNPJnc}Manage Troops", null);

		// Token: 0x04000683 RID: 1667
		private readonly TextObject _chosenTitleTextObject = new TextObject("{=InqmgBiF}Chosen Crew", null);

		// Token: 0x04000684 RID: 1668
		private int _currentTotalSelectedTroopCount;

		// Token: 0x04000685 RID: 1669
		public bool IsFiveStackModifierActive;

		// Token: 0x04000686 RID: 1670
		public bool IsEntireStackModifierActive;

		// Token: 0x04000687 RID: 1671
		private InputKeyItemVM _doneInputKey;

		// Token: 0x04000688 RID: 1672
		private InputKeyItemVM _cancelInputKey;

		// Token: 0x04000689 RID: 1673
		private InputKeyItemVM _resetInputKey;

		// Token: 0x0400068A RID: 1674
		private bool _isEnabled;

		// Token: 0x0400068B RID: 1675
		private bool _isDoneEnabled;

		// Token: 0x0400068C RID: 1676
		private string _doneText;

		// Token: 0x0400068D RID: 1677
		private string _cancelText;

		// Token: 0x0400068E RID: 1678
		private string _titleText;

		// Token: 0x0400068F RID: 1679
		private string _clearSelectionText;

		// Token: 0x04000690 RID: 1680
		private string _currentSelectedAmountText;

		// Token: 0x04000691 RID: 1681
		private string _currentSelectedAmountTitle;

		// Token: 0x04000692 RID: 1682
		private MBBindingList<TroopSelectionItemVM> _troops;
	}
}
