using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.InputSystem;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Order
{
	// Token: 0x02000069 RID: 105
	public class OrderReturnButtonWidget : OrderItemButtonWidget
	{
		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x00010DBD File Offset: 0x0000EFBD
		// (set) Token: 0x06000597 RID: 1431 RVA: 0x00010DC5 File Offset: 0x0000EFC5
		public Widget InputVisualParent { get; set; }

		// Token: 0x06000598 RID: 1432 RVA: 0x00010DCE File Offset: 0x0000EFCE
		public OrderReturnButtonWidget(UIContext context) : base(context)
		{
			Input.OnGamepadActiveStateChanged = (Action)Delegate.Combine(Input.OnGamepadActiveStateChanged, new Action(this.OnGamepadActiveStateChanged));
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00010DF7 File Offset: 0x0000EFF7
		private void OnGamepadActiveStateChanged()
		{
			this.UpdateVisibility();
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00010DFF File Offset: 0x0000EFFF
		private void UpdateVisibility()
		{
			base.IsVisible = (this.IsDeployment && Input.IsGamepadActive && !this.IsHolding);
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00010E22 File Offset: 0x0000F022
		private void UpdateInputVisualVisibility()
		{
			if (this.InputVisualParent != null)
			{
				this.InputVisualParent.IsVisible = (this.CanUseShortcuts && !this.IsAnyOrderSetActive);
			}
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00010E4B File Offset: 0x0000F04B
		protected override void OnDisconnectedFromRoot()
		{
			base.OnDisconnectedFromRoot();
			Input.OnGamepadActiveStateChanged = (Action)Delegate.Remove(Input.OnGamepadActiveStateChanged, new Action(this.OnGamepadActiveStateChanged));
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x00010E73 File Offset: 0x0000F073
		// (set) Token: 0x0600059E RID: 1438 RVA: 0x00010E7B File Offset: 0x0000F07B
		public bool IsHolding
		{
			get
			{
				return this._isHolding;
			}
			set
			{
				if (value != this._isHolding)
				{
					this._isHolding = value;
					base.OnPropertyChanged(value, "IsHolding");
					this.UpdateVisibility();
				}
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x00010E9F File Offset: 0x0000F09F
		// (set) Token: 0x060005A0 RID: 1440 RVA: 0x00010EA7 File Offset: 0x0000F0A7
		public bool CanUseShortcuts
		{
			get
			{
				return this._canUseShortcuts;
			}
			set
			{
				if (value != this._canUseShortcuts)
				{
					this._canUseShortcuts = value;
					base.OnPropertyChanged(value, "CanUseShortcuts");
					this.UpdateInputVisualVisibility();
				}
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x00010ECB File Offset: 0x0000F0CB
		// (set) Token: 0x060005A2 RID: 1442 RVA: 0x00010ED3 File Offset: 0x0000F0D3
		public bool IsAnyOrderSetActive
		{
			get
			{
				return this._isAnyOrderSetActive;
			}
			set
			{
				if (value != this._isAnyOrderSetActive)
				{
					this._isAnyOrderSetActive = value;
					base.OnPropertyChanged(value, "IsAnyOrderSetActive");
					this.UpdateInputVisualVisibility();
				}
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x00010EF7 File Offset: 0x0000F0F7
		// (set) Token: 0x060005A4 RID: 1444 RVA: 0x00010EFF File Offset: 0x0000F0FF
		public bool IsDeployment
		{
			get
			{
				return this._isDeployment;
			}
			set
			{
				if (value != this._isDeployment)
				{
					this._isDeployment = value;
					base.OnPropertyChanged(value, "IsDeployment");
					this.UpdateVisibility();
				}
			}
		}

		// Token: 0x0400026C RID: 620
		private bool _isHolding;

		// Token: 0x0400026D RID: 621
		private bool _canUseShortcuts;

		// Token: 0x0400026E RID: 622
		private bool _isAnyOrderSetActive;

		// Token: 0x0400026F RID: 623
		private bool _isDeployment;
	}
}
