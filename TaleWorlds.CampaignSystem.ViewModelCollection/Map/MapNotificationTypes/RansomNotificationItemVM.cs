using System;
using TaleWorlds.CampaignSystem.MapNotificationTypes;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Map.MapNotificationTypes
{
	// Token: 0x02000049 RID: 73
	public class RansomNotificationItemVM : MapNotificationItemBaseVM
	{
		// Token: 0x060005B8 RID: 1464 RVA: 0x0001C768 File Offset: 0x0001A968
		public RansomNotificationItemVM(RansomOfferMapNotification data) : base(data)
		{
			RansomNotificationItemVM <>4__this = this;
			this._hero = data.CaptiveHero;
			this._onInspect = delegate()
			{
				<>4__this._playerInspectedNotification = true;
				CampaignEventDispatcher.Instance.OnRansomOfferedToPlayer(data.CaptiveHero);
				<>4__this.ExecuteRemove();
			};
			CampaignEvents.OnRansomOfferCancelledEvent.AddNonSerializedListener(this, new Action<Hero>(this.OnRansomOfferCancelled));
			base.NotificationIdentifier = "ransom";
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x0001C7DA File Offset: 0x0001A9DA
		private void OnRansomOfferCancelled(Hero captiveHero)
		{
			if (captiveHero == this._hero)
			{
				base.ExecuteRemove();
			}
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0001C7EB File Offset: 0x0001A9EB
		public override void OnFinalize()
		{
			base.OnFinalize();
			CampaignEvents.OnRansomOfferCancelledEvent.ClearListeners(this);
			if (!this._playerInspectedNotification)
			{
				CampaignEventDispatcher.Instance.OnRansomOfferCancelled(this._hero);
			}
		}

		// Token: 0x04000271 RID: 625
		private bool _playerInspectedNotification;

		// Token: 0x04000272 RID: 626
		private Hero _hero;
	}
}
