using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Inventory
{
	// Token: 0x02000132 RID: 306
	public class InventoryItemPreviewWidget : Widget
	{
		// Token: 0x06000FD3 RID: 4051 RVA: 0x0002B988 File Offset: 0x00029B88
		public InventoryItemPreviewWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x0002B991 File Offset: 0x00029B91
		public void SetLastFocusedItem(Widget lastFocusedWidget)
		{
			this._lastFocusedWidget = lastFocusedWidget;
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06000FD5 RID: 4053 RVA: 0x0002B99A File Offset: 0x00029B9A
		// (set) Token: 0x06000FD6 RID: 4054 RVA: 0x0002B9A4 File Offset: 0x00029BA4
		[Editor(false)]
		public bool IsPreviewOpen
		{
			get
			{
				return this._isPreviewOpen;
			}
			set
			{
				if (this._isPreviewOpen != value)
				{
					if (!value)
					{
						base.EventManager.SetWidgetFocused(this._lastFocusedWidget, false);
						this._lastFocusedWidget = null;
					}
					this._isPreviewOpen = value;
					base.IsVisible = value;
					base.OnPropertyChanged(value, "IsPreviewOpen");
				}
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06000FD7 RID: 4055 RVA: 0x0002B9F0 File Offset: 0x00029BF0
		// (set) Token: 0x06000FD8 RID: 4056 RVA: 0x0002B9F8 File Offset: 0x00029BF8
		[Editor(false)]
		public ItemTableauWidget ItemTableau
		{
			get
			{
				return this._itemTableau;
			}
			set
			{
				if (this._itemTableau != value)
				{
					this._itemTableau = value;
					base.OnPropertyChanged<ItemTableauWidget>(value, "ItemTableau");
				}
			}
		}

		// Token: 0x04000731 RID: 1841
		private Widget _lastFocusedWidget;

		// Token: 0x04000732 RID: 1842
		private ItemTableauWidget _itemTableau;

		// Token: 0x04000733 RID: 1843
		private bool _isPreviewOpen;
	}
}
