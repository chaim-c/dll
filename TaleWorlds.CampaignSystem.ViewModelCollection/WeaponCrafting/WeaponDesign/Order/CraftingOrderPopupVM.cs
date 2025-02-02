using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.CraftingSystem;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting.WeaponDesign.Order
{
	// Token: 0x020000F2 RID: 242
	public class CraftingOrderPopupVM : ViewModel
	{
		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x060016E3 RID: 5859 RVA: 0x000556D2 File Offset: 0x000538D2
		public bool HasOrders
		{
			get
			{
				return this.CraftingOrders.Count > 0;
			}
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x060016E4 RID: 5860 RVA: 0x000556E2 File Offset: 0x000538E2
		public bool HasEnabledOrders
		{
			get
			{
				return this.CraftingOrders.Count((CraftingOrderItemVM x) => x.IsEnabled) > 0;
			}
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x00055711 File Offset: 0x00053911
		public CraftingOrderPopupVM(Action<CraftingOrderItemVM> onDoneAction, Func<CraftingAvailableHeroItemVM> getCurrentCraftingHero, Func<CraftingOrder, IEnumerable<CraftingStatData>> getOrderStatDatas)
		{
			this._onDoneAction = onDoneAction;
			this._getCurrentCraftingHero = getCurrentCraftingHero;
			this._getOrderStatDatas = getOrderStatDatas;
			this._craftingBehavior = Campaign.Current.GetCampaignBehavior<ICraftingCampaignBehavior>();
			this.CraftingOrders = new MBBindingList<CraftingOrderItemVM>();
		}

		// Token: 0x060016E6 RID: 5862 RVA: 0x0005574C File Offset: 0x0005394C
		public void RefreshOrders()
		{
			this.CraftingOrders.Clear();
			if (Campaign.Current.GameMode == CampaignGameMode.Tutorial)
			{
				return;
			}
			IReadOnlyDictionary<Town, CraftingCampaignBehavior.CraftingOrderSlots> craftingOrders = this._craftingBehavior.CraftingOrders;
			Settlement currentSettlement = Settlement.CurrentSettlement;
			CraftingCampaignBehavior.CraftingOrderSlots craftingOrderSlots = craftingOrders[(currentSettlement != null) ? currentSettlement.Town : null];
			if (craftingOrderSlots == null)
			{
				return;
			}
			CraftingOrderPopupVM.OrderComparer comparer = new CraftingOrderPopupVM.OrderComparer();
			List<CraftingOrder> list = (from x in craftingOrderSlots.CustomOrders
			where x != null
			select x).ToList<CraftingOrder>();
			list.Sort(comparer);
			List<CraftingOrder> list2 = (from x in craftingOrderSlots.Slots
			where x != null
			select x).ToList<CraftingOrder>();
			list2.Sort(comparer);
			CampaignUIHelper.IssueQuestFlags issueQuestFlags = CampaignUIHelper.IssueQuestFlags.None;
			for (int i = 0; i < list.Count; i++)
			{
				List<CraftingStatData> orderStatDatas = this._getOrderStatDatas(list[i]).ToList<CraftingStatData>();
				CampaignUIHelper.IssueQuestFlags questFlagsForOrder = this.GetQuestFlagsForOrder(list[i]);
				this.CraftingOrders.Add(new CraftingOrderItemVM(list[i], new Action<CraftingOrderItemVM>(this.SelectOrder), this._getCurrentCraftingHero, orderStatDatas, questFlagsForOrder));
				issueQuestFlags |= questFlagsForOrder;
			}
			this.QuestType = (int)issueQuestFlags;
			for (int j = 0; j < list2.Count; j++)
			{
				List<CraftingStatData> orderStatDatas2 = this._getOrderStatDatas(list2[j]).ToList<CraftingStatData>();
				this.CraftingOrders.Add(new CraftingOrderItemVM(list2[j], new Action<CraftingOrderItemVM>(this.SelectOrder), this._getCurrentCraftingHero, orderStatDatas2, CampaignUIHelper.IssueQuestFlags.None));
			}
			TextObject textObject = new TextObject("{=MkVTRqAw}Orders ({ORDER_COUNT})", null);
			textObject.SetTextVariable("ORDER_COUNT", this.CraftingOrders.Count);
			this.OrderCountText = textObject.ToString();
		}

		// Token: 0x060016E7 RID: 5863 RVA: 0x00055919 File Offset: 0x00053B19
		private CampaignUIHelper.IssueQuestFlags GetQuestFlagsForOrder(CraftingOrder order)
		{
			if (Campaign.Current.QuestManager.TrackedObjects.ContainsKey(order))
			{
				return CampaignUIHelper.IssueQuestFlags.ActiveIssue;
			}
			return CampaignUIHelper.IssueQuestFlags.None;
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x00055935 File Offset: 0x00053B35
		public void SelectOrder(CraftingOrderItemVM order)
		{
			if (this.SelectedCraftingOrder != null)
			{
				this.SelectedCraftingOrder.IsSelected = false;
			}
			this.SelectedCraftingOrder = order;
			this.SelectedCraftingOrder.IsSelected = true;
			this._onDoneAction(order);
			this.IsVisible = false;
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x00055971 File Offset: 0x00053B71
		public void ExecuteOpenPopup()
		{
			this.IsVisible = true;
		}

		// Token: 0x060016EA RID: 5866 RVA: 0x0005597A File Offset: 0x00053B7A
		public void ExecuteCloseWithoutSelection()
		{
			this.IsVisible = false;
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x060016EB RID: 5867 RVA: 0x00055983 File Offset: 0x00053B83
		// (set) Token: 0x060016EC RID: 5868 RVA: 0x0005598B File Offset: 0x00053B8B
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
					Game game = Game.Current;
					if (game == null)
					{
						return;
					}
					game.EventManager.TriggerEvent<CraftingOrderSelectionOpenedEvent>(new CraftingOrderSelectionOpenedEvent(this._isVisible));
				}
			}
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x060016ED RID: 5869 RVA: 0x000559C8 File Offset: 0x00053BC8
		// (set) Token: 0x060016EE RID: 5870 RVA: 0x000559D0 File Offset: 0x00053BD0
		[DataSourceProperty]
		public int QuestType
		{
			get
			{
				return this._questType;
			}
			set
			{
				if (value != this._questType)
				{
					this._questType = value;
					base.OnPropertyChangedWithValue(value, "QuestType");
				}
			}
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x060016EF RID: 5871 RVA: 0x000559EE File Offset: 0x00053BEE
		// (set) Token: 0x060016F0 RID: 5872 RVA: 0x000559F6 File Offset: 0x00053BF6
		[DataSourceProperty]
		public string OrderCountText
		{
			get
			{
				return this._orderCountText;
			}
			set
			{
				if (value != this._orderCountText)
				{
					this._orderCountText = value;
					base.OnPropertyChangedWithValue<string>(value, "OrderCountText");
				}
			}
		}

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x060016F1 RID: 5873 RVA: 0x00055A19 File Offset: 0x00053C19
		// (set) Token: 0x060016F2 RID: 5874 RVA: 0x00055A21 File Offset: 0x00053C21
		[DataSourceProperty]
		public CraftingOrderItemVM SelectedCraftingOrder
		{
			get
			{
				return this._selectedCraftingOrder;
			}
			set
			{
				if (value != this._selectedCraftingOrder)
				{
					this._selectedCraftingOrder = value;
					base.OnPropertyChangedWithValue<CraftingOrderItemVM>(value, "SelectedCraftingOrder");
				}
			}
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x060016F3 RID: 5875 RVA: 0x00055A3F File Offset: 0x00053C3F
		// (set) Token: 0x060016F4 RID: 5876 RVA: 0x00055A47 File Offset: 0x00053C47
		[DataSourceProperty]
		public MBBindingList<CraftingOrderItemVM> CraftingOrders
		{
			get
			{
				return this._craftingOrders;
			}
			set
			{
				if (value != this._craftingOrders)
				{
					this._craftingOrders = value;
					base.OnPropertyChangedWithValue<MBBindingList<CraftingOrderItemVM>>(value, "CraftingOrders");
				}
			}
		}

		// Token: 0x04000AAD RID: 2733
		private Action<CraftingOrderItemVM> _onDoneAction;

		// Token: 0x04000AAE RID: 2734
		private Func<CraftingAvailableHeroItemVM> _getCurrentCraftingHero;

		// Token: 0x04000AAF RID: 2735
		private Func<CraftingOrder, IEnumerable<CraftingStatData>> _getOrderStatDatas;

		// Token: 0x04000AB0 RID: 2736
		private readonly ICraftingCampaignBehavior _craftingBehavior;

		// Token: 0x04000AB1 RID: 2737
		private bool _isVisible;

		// Token: 0x04000AB2 RID: 2738
		private int _questType;

		// Token: 0x04000AB3 RID: 2739
		private string _orderCountText;

		// Token: 0x04000AB4 RID: 2740
		private MBBindingList<CraftingOrderItemVM> _craftingOrders;

		// Token: 0x04000AB5 RID: 2741
		private CraftingOrderItemVM _selectedCraftingOrder;

		// Token: 0x02000230 RID: 560
		private class OrderComparer : IComparer<CraftingOrder>
		{
			// Token: 0x0600227F RID: 8831 RVA: 0x00075185 File Offset: 0x00073385
			public int Compare(CraftingOrder x, CraftingOrder y)
			{
				return (int)(x.OrderDifficulty - y.OrderDifficulty);
			}
		}
	}
}
