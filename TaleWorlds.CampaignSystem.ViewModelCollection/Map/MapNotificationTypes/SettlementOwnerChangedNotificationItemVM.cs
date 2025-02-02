using System;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.MapNotificationTypes;
using TaleWorlds.CampaignSystem.Settlements;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Map.MapNotificationTypes
{
	// Token: 0x0200004B RID: 75
	public class SettlementOwnerChangedNotificationItemVM : MapNotificationItemBaseVM
	{
		// Token: 0x060005BE RID: 1470 RVA: 0x0001C880 File Offset: 0x0001AA80
		public SettlementOwnerChangedNotificationItemVM(SettlementOwnerChangedMapNotification data) : base(data)
		{
			this._settlement = data.Settlement;
			this._newOwner = data.NewOwner;
			base.NotificationIdentifier = "settlementownerchanged";
			this._onInspect = delegate()
			{
				base.GoToMapPosition(this._settlement.Position2D);
			};
			CampaignEvents.OnSettlementOwnerChangedEvent.AddNonSerializedListener(this, new Action<Settlement, bool, Hero, Hero, Hero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail>(this.OnSettlementOwnerChanged));
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0001C8E0 File Offset: 0x0001AAE0
		private void OnSettlementOwnerChanged(Settlement settlement, bool openToClaim, Hero newOwner, Hero oldOwner, Hero capturerHero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail detail)
		{
			if (settlement == this._settlement && newOwner != this._newOwner)
			{
				base.ExecuteRemove();
			}
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x0001C8FA File Offset: 0x0001AAFA
		public override void OnFinalize()
		{
			base.OnFinalize();
			CampaignEvents.OnSettlementOwnerChangedEvent.ClearListeners(this);
		}

		// Token: 0x04000275 RID: 629
		private Settlement _settlement;

		// Token: 0x04000276 RID: 630
		private Hero _newOwner;
	}
}
