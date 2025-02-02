using System;
using TaleWorlds.CampaignSystem.MapNotificationTypes;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Siege;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Map.MapNotificationTypes
{
	// Token: 0x0200004C RID: 76
	public class SettlementUnderSiegeMapNotificationItemVM : MapNotificationItemBaseVM
	{
		// Token: 0x060005C2 RID: 1474 RVA: 0x0001C920 File Offset: 0x0001AB20
		public SettlementUnderSiegeMapNotificationItemVM(SettlementUnderSiegeMapNotification data) : base(data)
		{
			this._settlement = data.BesiegedSettlement;
			base.NotificationIdentifier = "settlementundersiege";
			this._onInspect = delegate()
			{
				base.GoToMapPosition(this._settlement.Position2D);
			};
			CampaignEvents.OnSiegeEventEndedEvent.AddNonSerializedListener(this, new Action<SiegeEvent>(this.OnSiegeEventEnded));
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0001C974 File Offset: 0x0001AB74
		private void OnSiegeEventEnded(SiegeEvent obj)
		{
			if (obj.BesiegedSettlement == this._settlement)
			{
				base.ExecuteRemove();
			}
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x0001C98A File Offset: 0x0001AB8A
		public override void OnFinalize()
		{
			base.OnFinalize();
			CampaignEvents.OnSiegeEventEndedEvent.ClearListeners(this);
		}

		// Token: 0x04000277 RID: 631
		private Settlement _settlement;
	}
}
