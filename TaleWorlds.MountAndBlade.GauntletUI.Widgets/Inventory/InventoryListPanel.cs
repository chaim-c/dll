using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Inventory
{
	// Token: 0x02000135 RID: 309
	public class InventoryListPanel : NavigatableListPanel
	{
		// Token: 0x0600102A RID: 4138 RVA: 0x0002C7D8 File Offset: 0x0002A9D8
		public InventoryListPanel(UIContext context) : base(context)
		{
			this._sortByTypeClickHandler = new Action<Widget>(this.OnSortByType);
			this._sortByNameClickHandler = new Action<Widget>(this.OnSortByName);
			this._sortByQuantityClickHandler = new Action<Widget>(this.OnSortByQuantity);
			this._sortByCostClickHandler = new Action<Widget>(this.OnSortByCost);
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x0002C834 File Offset: 0x0002AA34
		private void OnSortByType(Widget widget)
		{
			base.RefreshChildNavigationIndices();
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x0002C83C File Offset: 0x0002AA3C
		private void OnSortByName(Widget widget)
		{
			base.RefreshChildNavigationIndices();
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x0002C844 File Offset: 0x0002AA44
		private void OnSortByQuantity(Widget widget)
		{
			base.RefreshChildNavigationIndices();
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x0002C84C File Offset: 0x0002AA4C
		private void OnSortByCost(Widget widget)
		{
			base.RefreshChildNavigationIndices();
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x0600102F RID: 4143 RVA: 0x0002C854 File Offset: 0x0002AA54
		// (set) Token: 0x06001030 RID: 4144 RVA: 0x0002C85C File Offset: 0x0002AA5C
		[Editor(false)]
		public ButtonWidget SortByTypeBtn
		{
			get
			{
				return this._sortByTypeBtn;
			}
			set
			{
				if (this._sortByTypeBtn != value)
				{
					if (this._sortByTypeBtn != null)
					{
						this._sortByTypeBtn.ClickEventHandlers.Remove(this._sortByTypeClickHandler);
					}
					this._sortByTypeBtn = value;
					if (this._sortByTypeBtn != null)
					{
						this._sortByTypeBtn.ClickEventHandlers.Add(this._sortByTypeClickHandler);
					}
					base.OnPropertyChanged<ButtonWidget>(value, "SortByTypeBtn");
				}
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06001031 RID: 4145 RVA: 0x0002C8C2 File Offset: 0x0002AAC2
		// (set) Token: 0x06001032 RID: 4146 RVA: 0x0002C8CC File Offset: 0x0002AACC
		[Editor(false)]
		public ButtonWidget SortByNameBtn
		{
			get
			{
				return this._sortByNameBtn;
			}
			set
			{
				if (this._sortByNameBtn != value)
				{
					if (this._sortByNameBtn != null)
					{
						this._sortByNameBtn.ClickEventHandlers.Remove(this._sortByNameClickHandler);
					}
					this._sortByNameBtn = value;
					if (this._sortByNameBtn != null)
					{
						this._sortByNameBtn.ClickEventHandlers.Add(this._sortByNameClickHandler);
					}
					base.OnPropertyChanged<ButtonWidget>(value, "SortByNameBtn");
				}
			}
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06001033 RID: 4147 RVA: 0x0002C932 File Offset: 0x0002AB32
		// (set) Token: 0x06001034 RID: 4148 RVA: 0x0002C93C File Offset: 0x0002AB3C
		[Editor(false)]
		public ButtonWidget SortByQuantityBtn
		{
			get
			{
				return this._sortByQuantityBtn;
			}
			set
			{
				if (this._sortByQuantityBtn != value)
				{
					if (this._sortByQuantityBtn != null)
					{
						this._sortByQuantityBtn.ClickEventHandlers.Remove(this._sortByQuantityClickHandler);
					}
					this._sortByQuantityBtn = value;
					if (this._sortByQuantityBtn != null)
					{
						this._sortByQuantityBtn.ClickEventHandlers.Add(this._sortByQuantityClickHandler);
					}
					base.OnPropertyChanged<ButtonWidget>(value, "SortByQuantityBtn");
				}
			}
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06001035 RID: 4149 RVA: 0x0002C9A2 File Offset: 0x0002ABA2
		// (set) Token: 0x06001036 RID: 4150 RVA: 0x0002C9AC File Offset: 0x0002ABAC
		[Editor(false)]
		public ButtonWidget SortByCostBtn
		{
			get
			{
				return this._sortByCostBtn;
			}
			set
			{
				if (this._sortByCostBtn != value)
				{
					if (this._sortByCostBtn != null)
					{
						this._sortByCostBtn.ClickEventHandlers.Remove(this._sortByCostClickHandler);
					}
					this._sortByCostBtn = value;
					if (this._sortByCostBtn != null)
					{
						this._sortByCostBtn.ClickEventHandlers.Remove(this._sortByCostClickHandler);
					}
					base.OnPropertyChanged<ButtonWidget>(value, "SortByCostBtn");
				}
			}
		}

		// Token: 0x04000757 RID: 1879
		private Action<Widget> _sortByTypeClickHandler;

		// Token: 0x04000758 RID: 1880
		private Action<Widget> _sortByNameClickHandler;

		// Token: 0x04000759 RID: 1881
		private Action<Widget> _sortByQuantityClickHandler;

		// Token: 0x0400075A RID: 1882
		private Action<Widget> _sortByCostClickHandler;

		// Token: 0x0400075B RID: 1883
		private ButtonWidget _sortByTypeBtn;

		// Token: 0x0400075C RID: 1884
		private ButtonWidget _sortByNameBtn;

		// Token: 0x0400075D RID: 1885
		private ButtonWidget _sortByQuantityBtn;

		// Token: 0x0400075E RID: 1886
		private ButtonWidget _sortByCostBtn;
	}
}
