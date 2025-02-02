using System;
using System.Collections.Generic;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Nameplate
{
	// Token: 0x02000079 RID: 121
	public class SettlementNameplateManagerWidget : Widget
	{
		// Token: 0x060006BE RID: 1726 RVA: 0x00013FC5 File Offset: 0x000121C5
		public SettlementNameplateManagerWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x00013FE4 File Offset: 0x000121E4
		protected override void OnRender(TwoDimensionContext twoDimensionContext, TwoDimensionDrawContext drawContext)
		{
			this._visibleNameplates.Clear();
			foreach (SettlementNameplateWidget settlementNameplateWidget in this._allChildrenNameplates)
			{
				if (settlementNameplateWidget != null && settlementNameplateWidget.IsVisibleOnMap)
				{
					this._visibleNameplates.Add(settlementNameplateWidget);
				}
			}
			this._visibleNameplates.Sort();
			foreach (SettlementNameplateWidget settlementNameplateWidget2 in this._visibleNameplates)
			{
				settlementNameplateWidget2.DisableRender = false;
				settlementNameplateWidget2.Render(twoDimensionContext, drawContext);
				settlementNameplateWidget2.DisableRender = true;
			}
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x000140AC File Offset: 0x000122AC
		protected override void OnChildAdded(Widget child)
		{
			base.OnChildAdded(child);
			child.DisableRender = true;
			this._allChildrenNameplates.Add(child as SettlementNameplateWidget);
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x000140CD File Offset: 0x000122CD
		protected override void OnChildRemoved(Widget child)
		{
			base.OnChildRemoved(child);
			this._allChildrenNameplates.Remove(child as SettlementNameplateWidget);
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x000140E8 File Offset: 0x000122E8
		protected override void OnDisconnectedFromRoot()
		{
			base.OnDisconnectedFromRoot();
			this._allChildrenNameplates.Clear();
			this._allChildrenNameplates = null;
		}

		// Token: 0x040002F4 RID: 756
		private readonly List<SettlementNameplateWidget> _visibleNameplates = new List<SettlementNameplateWidget>();

		// Token: 0x040002F5 RID: 757
		private List<SettlementNameplateWidget> _allChildrenNameplates = new List<SettlementNameplateWidget>();
	}
}
