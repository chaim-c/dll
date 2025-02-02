using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.ExtraWidgets;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Inventory
{
	// Token: 0x02000138 RID: 312
	public class InventoryTwoWaySliderWidget : TwoWaySliderWidget
	{
		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x0600107B RID: 4219 RVA: 0x0002DBEA File Offset: 0x0002BDEA
		// (set) Token: 0x0600107C RID: 4220 RVA: 0x0002DBF2 File Offset: 0x0002BDF2
		public bool IsExtended
		{
			get
			{
				return this._isExtended;
			}
			set
			{
				if (this._isExtended != value)
				{
					this.CheckFillerState();
					this._isExtended = value;
				}
			}
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x0002DC0A File Offset: 0x0002BE0A
		public InventoryTwoWaySliderWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x0002DC13 File Offset: 0x0002BE13
		protected override void OnParallelUpdate(float dt)
		{
			if (this._initFiller == null && base.Filler != null)
			{
				this._initFiller = base.Filler;
			}
			if (this.IsExtended)
			{
				base.OnParallelUpdate(dt);
				this.CheckFillerState();
			}
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x0002DC48 File Offset: 0x0002BE48
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			if (this._isBeingDragged && !base.IsPressed)
			{
				Widget handle = base.Handle;
				if (handle != null && !handle.IsPressed)
				{
					this._shouldRemoveZeroCounts = true;
				}
			}
			bool isBeingDragged;
			if (!base.IsPressed)
			{
				Widget handle2 = base.Handle;
				isBeingDragged = (handle2 != null && handle2.IsPressed);
			}
			else
			{
				isBeingDragged = true;
			}
			this._isBeingDragged = isBeingDragged;
			if (this._shouldRemoveZeroCounts)
			{
				base.EventFired("RemoveZeroCounts", Array.Empty<object>());
				this._shouldRemoveZeroCounts = false;
			}
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x0002DCCC File Offset: 0x0002BECC
		private void CheckFillerState()
		{
			if (this._initFiller != null)
			{
				if (this.IsExtended && base.Filler == null)
				{
					base.Filler = this._initFiller;
					return;
				}
				if (!this.IsExtended && base.Filler != null)
				{
					base.Filler = null;
				}
			}
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x0002DD0A File Offset: 0x0002BF0A
		private void OnStockChangeClick(Widget obj)
		{
			this._manuallyIncreased = true;
			this._shouldRemoveZeroCounts = true;
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06001082 RID: 4226 RVA: 0x0002DD1A File Offset: 0x0002BF1A
		// (set) Token: 0x06001083 RID: 4227 RVA: 0x0002DD22 File Offset: 0x0002BF22
		[Editor(false)]
		public ButtonWidget IncreaseStockButtonWidget
		{
			get
			{
				return this._increaseStockButtonWidget;
			}
			set
			{
				if (this._increaseStockButtonWidget != value)
				{
					this._increaseStockButtonWidget = value;
					base.OnPropertyChanged<ButtonWidget>(value, "IncreaseStockButtonWidget");
					value.ClickEventHandlers.Add(new Action<Widget>(this.OnStockChangeClick));
				}
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06001084 RID: 4228 RVA: 0x0002DD57 File Offset: 0x0002BF57
		// (set) Token: 0x06001085 RID: 4229 RVA: 0x0002DD5F File Offset: 0x0002BF5F
		[Editor(false)]
		public ButtonWidget DecreaseStockButtonWidget
		{
			get
			{
				return this._decreaseStockButtonWidget;
			}
			set
			{
				if (this._decreaseStockButtonWidget != value)
				{
					this._decreaseStockButtonWidget = value;
					base.OnPropertyChanged<ButtonWidget>(value, "DecreaseStockButtonWidget");
					value.ClickEventHandlers.Add(new Action<Widget>(this.OnStockChangeClick));
				}
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06001086 RID: 4230 RVA: 0x0002DD94 File Offset: 0x0002BF94
		// (set) Token: 0x06001087 RID: 4231 RVA: 0x0002DD9C File Offset: 0x0002BF9C
		[Editor(false)]
		public bool IsRightSide
		{
			get
			{
				return this._isRightSide;
			}
			set
			{
				if (this._isRightSide != value)
				{
					this._isRightSide = value;
					base.OnPropertyChanged(value, "IsRightSide");
				}
			}
		}

		// Token: 0x04000781 RID: 1921
		private bool _isExtended;

		// Token: 0x04000782 RID: 1922
		private Widget _initFiller;

		// Token: 0x04000783 RID: 1923
		private bool _isBeingDragged;

		// Token: 0x04000784 RID: 1924
		private bool _shouldRemoveZeroCounts;

		// Token: 0x04000785 RID: 1925
		private ButtonWidget _increaseStockButtonWidget;

		// Token: 0x04000786 RID: 1926
		private ButtonWidget _decreaseStockButtonWidget;

		// Token: 0x04000787 RID: 1927
		private bool _isRightSide;
	}
}
