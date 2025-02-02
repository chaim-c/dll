using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Order
{
	// Token: 0x02000068 RID: 104
	public class OrderItemButtonWidget : ButtonWidget
	{
		// Token: 0x0600058B RID: 1419 RVA: 0x00010C14 File Offset: 0x0000EE14
		public OrderItemButtonWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00010C20 File Offset: 0x0000EE20
		private void SelectionStateChanged()
		{
			switch (this.SelectionState)
			{
			case 0:
				this.SelectionVisualWidget.SetState("Disabled");
				return;
			case 2:
				this.SelectionVisualWidget.SetState("PartiallyActive");
				return;
			case 3:
				this.SelectionVisualWidget.SetState("Active");
				return;
			}
			this.SelectionVisualWidget.SetState("Default");
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00010C90 File Offset: 0x0000EE90
		private void UpdateIcon()
		{
			if (this.IconWidget == null || string.IsNullOrEmpty(this.OrderIconID))
			{
				return;
			}
			this.IconWidget.Sprite = base.Context.SpriteData.GetSprite("Order\\ItemIcons\\OI" + this.OrderIconID);
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x00010CDE File Offset: 0x0000EEDE
		// (set) Token: 0x0600058F RID: 1423 RVA: 0x00010CE6 File Offset: 0x0000EEE6
		[Editor(false)]
		public int SelectionState
		{
			get
			{
				return this._selectionState;
			}
			set
			{
				if (this._selectionState != value)
				{
					this._selectionState = value;
					base.OnPropertyChanged(value, "SelectionState");
					this.SelectionStateChanged();
				}
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x00010D0A File Offset: 0x0000EF0A
		// (set) Token: 0x06000591 RID: 1425 RVA: 0x00010D12 File Offset: 0x0000EF12
		[Editor(false)]
		public string OrderIconID
		{
			get
			{
				return this._orderIconID;
			}
			set
			{
				if (this._orderIconID != value)
				{
					this._orderIconID = value;
					base.OnPropertyChanged<string>(value, "OrderIconID");
					this.UpdateIcon();
				}
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x00010D3B File Offset: 0x0000EF3B
		// (set) Token: 0x06000593 RID: 1427 RVA: 0x00010D43 File Offset: 0x0000EF43
		[Editor(false)]
		public Widget IconWidget
		{
			get
			{
				return this._iconWidget;
			}
			set
			{
				if (this._iconWidget != value)
				{
					this._iconWidget = value;
					base.OnPropertyChanged<Widget>(value, "IconWidget");
					this.UpdateIcon();
				}
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x00010D67 File Offset: 0x0000EF67
		// (set) Token: 0x06000595 RID: 1429 RVA: 0x00010D70 File Offset: 0x0000EF70
		[Editor(false)]
		public ImageWidget SelectionVisualWidget
		{
			get
			{
				return this._selectionVisualWidget;
			}
			set
			{
				if (this._selectionVisualWidget != value)
				{
					this._selectionVisualWidget = value;
					base.OnPropertyChanged<ImageWidget>(value, "SelectionVisualWidget");
					if (value != null)
					{
						value.AddState("Disabled");
						value.AddState("PartiallyActive");
						value.AddState("Active");
					}
				}
			}
		}

		// Token: 0x04000267 RID: 615
		private int _selectionState;

		// Token: 0x04000268 RID: 616
		private string _orderIconID;

		// Token: 0x04000269 RID: 617
		private Widget _iconWidget;

		// Token: 0x0400026A RID: 618
		private ImageWidget _selectionVisualWidget;
	}
}
