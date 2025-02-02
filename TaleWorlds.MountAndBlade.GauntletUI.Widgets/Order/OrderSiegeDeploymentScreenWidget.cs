using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Order
{
	// Token: 0x0200006B RID: 107
	public class OrderSiegeDeploymentScreenWidget : Widget
	{
		// Token: 0x060005B9 RID: 1465 RVA: 0x00011235 File Offset: 0x0000F435
		public OrderSiegeDeploymentScreenWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00011240 File Offset: 0x0000F440
		public void SetSelectedDeploymentItem(OrderSiegeDeploymentItemButtonWidget deploymentItem)
		{
			this.DeploymentListPanel.ParentWidget.IsVisible = (deploymentItem != null);
			if (deploymentItem == null)
			{
				return;
			}
			this.DeploymentListPanel.MarginLeft = (deploymentItem.GlobalPosition.X + deploymentItem.Size.Y + 20f - base.EventManager.LeftUsableAreaStart) / base._scaleToUse;
			this.DeploymentListPanel.MarginTop = (deploymentItem.GlobalPosition.Y + (deploymentItem.Size.Y / 2f - this.DeploymentListPanel.Size.Y / 2f) - base.EventManager.TopUsableAreaStart) / base._scaleToUse;
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x000112F2 File Offset: 0x0000F4F2
		private void UpdateEnabledState(bool isEnabled)
		{
			this.SetGlobalAlphaRecursively(isEnabled ? 1f : 0.5f);
			base.DoNotPassEventsToChildren = !isEnabled;
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x00011313 File Offset: 0x0000F513
		// (set) Token: 0x060005BD RID: 1469 RVA: 0x0001131B File Offset: 0x0000F51B
		public bool IsSiegeDeploymentDisabled
		{
			get
			{
				return this._isSiegeDeploymentDisabled;
			}
			set
			{
				if (value != this._isSiegeDeploymentDisabled)
				{
					this._isSiegeDeploymentDisabled = value;
					base.OnPropertyChanged(value, "IsSiegeDeploymentDisabled");
					this.UpdateEnabledState(!value);
				}
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x00011343 File Offset: 0x0000F543
		// (set) Token: 0x060005BF RID: 1471 RVA: 0x0001134B File Offset: 0x0000F54B
		public Widget DeploymentTargetsParent
		{
			get
			{
				return this._deploymentTargetsParent;
			}
			set
			{
				if (this._deploymentTargetsParent != value)
				{
					this._deploymentTargetsParent = value;
					base.OnPropertyChanged<Widget>(value, "DeploymentTargetsParent");
				}
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x00011369 File Offset: 0x0000F569
		// (set) Token: 0x060005C1 RID: 1473 RVA: 0x00011371 File Offset: 0x0000F571
		public ListPanel DeploymentListPanel
		{
			get
			{
				return this._deploymentListPanel;
			}
			set
			{
				if (this._deploymentListPanel != value)
				{
					this._deploymentListPanel = value;
					base.OnPropertyChanged<ListPanel>(value, "DeploymentListPanel");
				}
			}
		}

		// Token: 0x0400027A RID: 634
		private bool _isSiegeDeploymentDisabled;

		// Token: 0x0400027B RID: 635
		private Widget _deploymentTargetsParent;

		// Token: 0x0400027C RID: 636
		private ListPanel _deploymentListPanel;
	}
}
