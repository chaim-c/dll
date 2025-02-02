using System;
using TaleWorlds.CampaignSystem.MapNotificationTypes;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Map.MapNotificationTypes
{
	// Token: 0x02000043 RID: 67
	public class MercenaryOfferMapNotificationItemVM : MapNotificationItemBaseVM
	{
		// Token: 0x0600059E RID: 1438 RVA: 0x0001C06C File Offset: 0x0001A26C
		public MercenaryOfferMapNotificationItemVM(MercenaryOfferMapNotification data) : base(data)
		{
			this._offeredKingdom = data.OfferedKingdom;
			base.NotificationIdentifier = "vote";
			this._onInspect = delegate()
			{
				CampaignEventDispatcher.Instance.OnVassalOrMercenaryServiceOfferedToPlayer(this._offeredKingdom);
				this._playerInspectedNotification = true;
				base.ExecuteRemove();
			};
			CampaignEvents.OnVassalOrMercenaryServiceOfferCanceledEvent.AddNonSerializedListener(this, new Action<Kingdom>(this.OnVassalOrMercenaryServiceOfferCanceled));
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x0001C0C0 File Offset: 0x0001A2C0
		private void OnVassalOrMercenaryServiceOfferCanceled(Kingdom offeredKingdom)
		{
			if (Campaign.Current.CampaignInformationManager.InformationDataExists<MercenaryOfferMapNotification>((MercenaryOfferMapNotification x) => x.OfferedKingdom == offeredKingdom))
			{
				base.ExecuteRemove();
			}
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0001C0FD File Offset: 0x0001A2FD
		public override void OnFinalize()
		{
			base.OnFinalize();
			CampaignEventDispatcher.Instance.RemoveListeners(this);
			if (!this._playerInspectedNotification)
			{
				CampaignEventDispatcher.Instance.OnVassalOrMercenaryServiceOfferCanceled(this._offeredKingdom);
			}
		}

		// Token: 0x04000261 RID: 609
		private bool _playerInspectedNotification;

		// Token: 0x04000262 RID: 610
		private readonly Kingdom _offeredKingdom;
	}
}
