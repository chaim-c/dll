using System;
using System.Linq;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.CraftingSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection.Input;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting.WeaponDesign
{
	// Token: 0x020000E2 RID: 226
	public class CraftingHistoryVM : ViewModel
	{
		// Token: 0x06001527 RID: 5415 RVA: 0x00050074 File Offset: 0x0004E274
		public CraftingHistoryVM(Crafting crafting, ICraftingCampaignBehavior craftingBehavior, Func<CraftingOrder> getActiveOrder, Action<WeaponDesignSelectorVM> onDone)
		{
			this._crafting = crafting;
			this._craftingBehavior = craftingBehavior;
			this._getActiveOrder = getActiveOrder;
			this._onDone = onDone;
			this.CraftingHistory = new MBBindingList<WeaponDesignSelectorVM>();
			this.HistoryHint = new HintViewModel(CraftingHistoryVM._craftingHistoryText, null);
			this.HistoryDisabledHint = new HintViewModel(CraftingHistoryVM._noItemsHint, null);
			this.RefreshValues();
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x000500D8 File Offset: 0x0004E2D8
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.TitleText = CraftingHistoryVM._craftingHistoryText.ToString();
			this.DoneText = GameTexts.FindText("str_done", null).ToString();
			this.CancelText = GameTexts.FindText("str_cancel", null).ToString();
			this.RefreshAvailability();
		}

		// Token: 0x06001529 RID: 5417 RVA: 0x00050130 File Offset: 0x0004E330
		private void RefreshCraftingHistory()
		{
			this.FinalizeHistory();
			CraftingOrder craftingOrder = this._getActiveOrder();
			foreach (WeaponDesign weaponDesign in this._craftingBehavior.CraftingHistory)
			{
				if (craftingOrder == null || weaponDesign.Template.TemplateName.ToString() == craftingOrder.PreCraftedWeaponDesignItem.WeaponDesign.Template.TemplateName.ToString())
				{
					this.CraftingHistory.Add(new WeaponDesignSelectorVM(weaponDesign, new Action<WeaponDesignSelectorVM>(this.ExecuteSelect)));
				}
			}
			this.HasItemsInHistory = (this.CraftingHistory.Count > 0);
			this.ExecuteSelect(null);
		}

		// Token: 0x0600152A RID: 5418 RVA: 0x000501FC File Offset: 0x0004E3FC
		private void FinalizeHistory()
		{
			if (this.CraftingHistory.Count > 0)
			{
				foreach (WeaponDesignSelectorVM weaponDesignSelectorVM in this.CraftingHistory)
				{
					weaponDesignSelectorVM.OnFinalize();
				}
			}
			this.CraftingHistory.Clear();
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x00050260 File Offset: 0x0004E460
		public override void OnFinalize()
		{
			base.OnFinalize();
			this.FinalizeHistory();
			this.DoneKey.OnFinalize();
			this.CancelKey.OnFinalize();
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x00050284 File Offset: 0x0004E484
		public void RefreshAvailability()
		{
			CraftingOrder activeOrder = this._getActiveOrder();
			this.HasItemsInHistory = ((activeOrder == null) ? (this._craftingBehavior.CraftingHistory.Count > 0) : this._craftingBehavior.CraftingHistory.Any((WeaponDesign x) => x.Template.StringId == activeOrder.PreCraftedWeaponDesignItem.WeaponDesign.Template.StringId));
		}

		// Token: 0x0600152D RID: 5421 RVA: 0x000502E7 File Offset: 0x0004E4E7
		public void ExecuteOpen()
		{
			this.RefreshCraftingHistory();
			this.IsVisible = true;
		}

		// Token: 0x0600152E RID: 5422 RVA: 0x000502F6 File Offset: 0x0004E4F6
		public void ExecuteCancel()
		{
			this.IsVisible = false;
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x000502FF File Offset: 0x0004E4FF
		public void ExecuteDone()
		{
			Action<WeaponDesignSelectorVM> onDone = this._onDone;
			if (onDone != null)
			{
				onDone(this.SelectedDesign);
			}
			this.ExecuteCancel();
		}

		// Token: 0x06001530 RID: 5424 RVA: 0x0005031E File Offset: 0x0004E51E
		private void ExecuteSelect(WeaponDesignSelectorVM selector)
		{
			this.IsDoneAvailable = (selector != null);
			if (this.SelectedDesign != null)
			{
				this.SelectedDesign.IsSelected = false;
			}
			this.SelectedDesign = selector;
			if (this.SelectedDesign != null)
			{
				this.SelectedDesign.IsSelected = true;
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06001531 RID: 5425 RVA: 0x00050359 File Offset: 0x0004E559
		// (set) Token: 0x06001532 RID: 5426 RVA: 0x00050361 File Offset: 0x0004E561
		[DataSourceProperty]
		public bool IsDoneAvailable
		{
			get
			{
				return this._isDoneAvailable;
			}
			set
			{
				if (value != this._isDoneAvailable)
				{
					this._isDoneAvailable = value;
					base.OnPropertyChangedWithValue(value, "IsDoneAvailable");
				}
			}
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06001533 RID: 5427 RVA: 0x0005037F File Offset: 0x0004E57F
		// (set) Token: 0x06001534 RID: 5428 RVA: 0x00050387 File Offset: 0x0004E587
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

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06001535 RID: 5429 RVA: 0x000503A5 File Offset: 0x0004E5A5
		// (set) Token: 0x06001536 RID: 5430 RVA: 0x000503AD File Offset: 0x0004E5AD
		[DataSourceProperty]
		public bool HasItemsInHistory
		{
			get
			{
				return this._hasItemsInHistory;
			}
			set
			{
				if (value != this._hasItemsInHistory)
				{
					this._hasItemsInHistory = value;
					base.OnPropertyChangedWithValue(value, "HasItemsInHistory");
				}
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06001537 RID: 5431 RVA: 0x000503CB File Offset: 0x0004E5CB
		// (set) Token: 0x06001538 RID: 5432 RVA: 0x000503D3 File Offset: 0x0004E5D3
		[DataSourceProperty]
		public HintViewModel HistoryHint
		{
			get
			{
				return this._historyHint;
			}
			set
			{
				if (value != this._historyHint)
				{
					this._historyHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "HistoryHint");
				}
			}
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06001539 RID: 5433 RVA: 0x000503F1 File Offset: 0x0004E5F1
		// (set) Token: 0x0600153A RID: 5434 RVA: 0x000503F9 File Offset: 0x0004E5F9
		[DataSourceProperty]
		public HintViewModel HistoryDisabledHint
		{
			get
			{
				return this._historyDisabledHint;
			}
			set
			{
				if (value != this._historyDisabledHint)
				{
					this._historyDisabledHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "HistoryDisabledHint");
				}
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x0600153B RID: 5435 RVA: 0x00050417 File Offset: 0x0004E617
		// (set) Token: 0x0600153C RID: 5436 RVA: 0x0005041F File Offset: 0x0004E61F
		[DataSourceProperty]
		public MBBindingList<WeaponDesignSelectorVM> CraftingHistory
		{
			get
			{
				return this._craftingHistory;
			}
			set
			{
				if (value != this._craftingHistory)
				{
					this._craftingHistory = value;
					base.OnPropertyChangedWithValue<MBBindingList<WeaponDesignSelectorVM>>(value, "CraftingHistory");
				}
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x0600153D RID: 5437 RVA: 0x0005043D File Offset: 0x0004E63D
		// (set) Token: 0x0600153E RID: 5438 RVA: 0x00050445 File Offset: 0x0004E645
		[DataSourceProperty]
		public WeaponDesignSelectorVM SelectedDesign
		{
			get
			{
				return this._selectedDesign;
			}
			set
			{
				if (value != this._selectedDesign)
				{
					this._selectedDesign = value;
					base.OnPropertyChangedWithValue<WeaponDesignSelectorVM>(value, "SelectedDesign");
				}
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x0600153F RID: 5439 RVA: 0x00050463 File Offset: 0x0004E663
		// (set) Token: 0x06001540 RID: 5440 RVA: 0x0005046B File Offset: 0x0004E66B
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

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06001541 RID: 5441 RVA: 0x0005048E File Offset: 0x0004E68E
		// (set) Token: 0x06001542 RID: 5442 RVA: 0x00050496 File Offset: 0x0004E696
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

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06001543 RID: 5443 RVA: 0x000504B9 File Offset: 0x0004E6B9
		// (set) Token: 0x06001544 RID: 5444 RVA: 0x000504C1 File Offset: 0x0004E6C1
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

		// Token: 0x06001545 RID: 5445 RVA: 0x000504E4 File Offset: 0x0004E6E4
		public void SetDoneKey(HotKey hotkey)
		{
			this.DoneKey = InputKeyItemVM.CreateFromHotKey(hotkey, true);
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x000504F3 File Offset: 0x0004E6F3
		public void SetCancelKey(HotKey hotkey)
		{
			this.CancelKey = InputKeyItemVM.CreateFromHotKey(hotkey, true);
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06001547 RID: 5447 RVA: 0x00050502 File Offset: 0x0004E702
		// (set) Token: 0x06001548 RID: 5448 RVA: 0x0005050A File Offset: 0x0004E70A
		public InputKeyItemVM CancelKey
		{
			get
			{
				return this._cancelKey;
			}
			set
			{
				if (value != this._cancelKey)
				{
					this._cancelKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "CancelKey");
				}
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06001549 RID: 5449 RVA: 0x00050528 File Offset: 0x0004E728
		// (set) Token: 0x0600154A RID: 5450 RVA: 0x00050530 File Offset: 0x0004E730
		public InputKeyItemVM DoneKey
		{
			get
			{
				return this._doneKey;
			}
			set
			{
				if (value != this._doneKey)
				{
					this._doneKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "DoneKey");
				}
			}
		}

		// Token: 0x040009D2 RID: 2514
		private static TextObject _noItemsHint = new TextObject("{=saHYZKLt}There are no available items in history", null);

		// Token: 0x040009D3 RID: 2515
		private static TextObject _craftingHistoryText = new TextObject("{=xW4BPVLX}Crafting History", null);

		// Token: 0x040009D4 RID: 2516
		private ICraftingCampaignBehavior _craftingBehavior;

		// Token: 0x040009D5 RID: 2517
		private Func<CraftingOrder> _getActiveOrder;

		// Token: 0x040009D6 RID: 2518
		private Action<WeaponDesignSelectorVM> _onDone;

		// Token: 0x040009D7 RID: 2519
		private Crafting _crafting;

		// Token: 0x040009D8 RID: 2520
		private bool _isDoneAvailable;

		// Token: 0x040009D9 RID: 2521
		private bool _isVisible;

		// Token: 0x040009DA RID: 2522
		private bool _hasItemsInHistory;

		// Token: 0x040009DB RID: 2523
		private HintViewModel _historyHint;

		// Token: 0x040009DC RID: 2524
		private HintViewModel _historyDisabledHint;

		// Token: 0x040009DD RID: 2525
		private MBBindingList<WeaponDesignSelectorVM> _craftingHistory;

		// Token: 0x040009DE RID: 2526
		private WeaponDesignSelectorVM _selectedDesign;

		// Token: 0x040009DF RID: 2527
		private string _titleText;

		// Token: 0x040009E0 RID: 2528
		private string _doneText;

		// Token: 0x040009E1 RID: 2529
		private string _cancelText;

		// Token: 0x040009E2 RID: 2530
		private InputKeyItemVM _cancelKey;

		// Token: 0x040009E3 RID: 2531
		private InputKeyItemVM _doneKey;
	}
}
