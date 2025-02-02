using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Party
{
	// Token: 0x0200005F RID: 95
	public class PartyManageTroopPopupWidget : Widget
	{
		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x0000F475 File Offset: 0x0000D675
		// (set) Token: 0x060004FB RID: 1275 RVA: 0x0000F47D File Offset: 0x0000D67D
		public Widget PrimaryInputKeyVisualParent { get; set; }

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x0000F486 File Offset: 0x0000D686
		// (set) Token: 0x060004FD RID: 1277 RVA: 0x0000F48E File Offset: 0x0000D68E
		public Widget SecondaryInputKeyVisualParent { get; set; }

		// Token: 0x060004FE RID: 1278 RVA: 0x0000F497 File Offset: 0x0000D697
		public PartyManageTroopPopupWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0000F4A0 File Offset: 0x0000D6A0
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (base.IsVisible)
			{
				Widget hoveredView = base.EventManager.HoveredView;
				if (hoveredView != null && this.PrimaryInputKeyVisualParent != null && this.SecondaryInputKeyVisualParent != null)
				{
					PartyTroopManagementItemButtonWidget firstParentTupleOfWidget = this.GetFirstParentTupleOfWidget(hoveredView);
					if (firstParentTupleOfWidget != null)
					{
						Widget actionButtonAtIndex = firstParentTupleOfWidget.GetActionButtonAtIndex(0);
						bool flag = false;
						if (this.IsPrimaryActionAvailable && actionButtonAtIndex != null)
						{
							this.PrimaryInputKeyVisualParent.IsVisible = true;
							this.PrimaryInputKeyVisualParent.ScaledPositionXOffset = actionButtonAtIndex.GlobalPosition.X - 10f;
							this.PrimaryInputKeyVisualParent.ScaledPositionYOffset = actionButtonAtIndex.GlobalPosition.Y - 10f;
							flag = true;
						}
						else
						{
							this.PrimaryInputKeyVisualParent.IsVisible = false;
						}
						Widget widget = flag ? firstParentTupleOfWidget.GetActionButtonAtIndex(1) : actionButtonAtIndex;
						if (this.IsSecondaryActionAvailable && widget != null)
						{
							this.SecondaryInputKeyVisualParent.IsVisible = true;
							this.SecondaryInputKeyVisualParent.ScaledPositionXOffset = widget.GlobalPosition.X + widget.Size.X + 4f;
							this.SecondaryInputKeyVisualParent.ScaledPositionYOffset = widget.GlobalPosition.Y - 10f;
							return;
						}
						this.SecondaryInputKeyVisualParent.IsVisible = false;
						return;
					}
					else
					{
						this.PrimaryInputKeyVisualParent.IsVisible = false;
						this.SecondaryInputKeyVisualParent.IsVisible = false;
					}
				}
			}
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0000F5F4 File Offset: 0x0000D7F4
		private PartyTroopManagementItemButtonWidget GetFirstParentTupleOfWidget(Widget widget)
		{
			for (Widget widget2 = widget; widget2 != null; widget2 = widget2.ParentWidget)
			{
				PartyTroopManagementItemButtonWidget result;
				if ((result = (widget2 as PartyTroopManagementItemButtonWidget)) != null)
				{
					return result;
				}
			}
			return null;
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x0000F61C File Offset: 0x0000D81C
		// (set) Token: 0x06000502 RID: 1282 RVA: 0x0000F624 File Offset: 0x0000D824
		public bool IsPrimaryActionAvailable
		{
			get
			{
				return this._isPrimaryActionAvailable;
			}
			set
			{
				if (value != this._isPrimaryActionAvailable)
				{
					this._isPrimaryActionAvailable = value;
					base.OnPropertyChanged(value, "IsPrimaryActionAvailable");
				}
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x0000F642 File Offset: 0x0000D842
		// (set) Token: 0x06000504 RID: 1284 RVA: 0x0000F64A File Offset: 0x0000D84A
		public bool IsSecondaryActionAvailable
		{
			get
			{
				return this._isSecondaryActionAvailable;
			}
			set
			{
				if (value != this._isSecondaryActionAvailable)
				{
					this._isSecondaryActionAvailable = value;
					base.OnPropertyChanged(value, "IsSecondaryActionAvailable");
				}
			}
		}

		// Token: 0x0400022D RID: 557
		private bool _isPrimaryActionAvailable;

		// Token: 0x0400022E RID: 558
		private bool _isSecondaryActionAvailable;
	}
}
