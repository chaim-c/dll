using System;
using TaleWorlds.CampaignSystem.MapNotificationTypes;
using TaleWorlds.CampaignSystem.Settlements;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Map.MapNotificationTypes
{
	// Token: 0x0200004A RID: 74
	public class RebellionNotificationItemVM : MapNotificationItemBaseVM
	{
		// Token: 0x060005BB RID: 1467 RVA: 0x0001C818 File Offset: 0x0001AA18
		public RebellionNotificationItemVM(SettlementRebellionMapNotification data) : base(data)
		{
			this._settlement = data.RebelliousSettlement;
			this._onInspect = (this._onInspectAction = delegate()
			{
				INavigationHandler navigationHandler = base.NavigationHandler;
				if (navigationHandler == null)
				{
					return;
				}
				navigationHandler.OpenKingdom(this._settlement);
			});
			base.NotificationIdentifier = "rebellion";
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0001C85E File Offset: 0x0001AA5E
		public override void ManualRefreshRelevantStatus()
		{
			base.ManualRefreshRelevantStatus();
		}

		// Token: 0x04000273 RID: 627
		private Settlement _settlement;

		// Token: 0x04000274 RID: 628
		protected Action _onInspectAction;
	}
}
