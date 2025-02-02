using System;
using TaleWorlds.CampaignSystem.MapNotificationTypes;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Map.MapNotificationTypes
{
	// Token: 0x02000042 RID: 66
	public class MarriageOfferNotificationItemVM : MapNotificationItemBaseVM
	{
		// Token: 0x0600059A RID: 1434 RVA: 0x0001BF64 File Offset: 0x0001A164
		public MarriageOfferNotificationItemVM(MarriageOfferMapNotification data) : base(data)
		{
			this._suitor = data.Suitor;
			this._maiden = data.Maiden;
			base.NotificationIdentifier = "marriage";
			this._onInspect = delegate()
			{
				CampaignEventDispatcher.Instance.OnMarriageOfferedToPlayer(this._suitor, this._maiden);
				this._playerInspectedNotification = true;
				Campaign.Current.TimeControlMode = CampaignTimeControlMode.Stop;
				base.ExecuteRemove();
			};
			CampaignEvents.OnMarriageOfferCanceledEvent.AddNonSerializedListener(this, new Action<Hero, Hero>(this.OnMarriageOfferCanceled));
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0001BFC4 File Offset: 0x0001A1C4
		private void OnMarriageOfferCanceled(Hero suitor, Hero maiden)
		{
			if (Campaign.Current.CampaignInformationManager.InformationDataExists<MarriageOfferMapNotification>((MarriageOfferMapNotification x) => x.Suitor == suitor && x.Maiden == maiden))
			{
				base.ExecuteRemove();
			}
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0001C008 File Offset: 0x0001A208
		public override void OnFinalize()
		{
			base.OnFinalize();
			CampaignEventDispatcher.Instance.RemoveListeners(this);
			if (!this._playerInspectedNotification)
			{
				CampaignEventDispatcher.Instance.OnMarriageOfferCanceled(this._suitor, this._maiden);
			}
		}

		// Token: 0x0400025E RID: 606
		private bool _playerInspectedNotification;

		// Token: 0x0400025F RID: 607
		private readonly Hero _suitor;

		// Token: 0x04000260 RID: 608
		private readonly Hero _maiden;
	}
}
