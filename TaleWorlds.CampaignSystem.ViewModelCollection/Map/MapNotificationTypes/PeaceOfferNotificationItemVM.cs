﻿using System;
using TaleWorlds.CampaignSystem.Election;
using TaleWorlds.CampaignSystem.MapNotificationTypes;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Map.MapNotificationTypes
{
	// Token: 0x02000047 RID: 71
	public class PeaceOfferNotificationItemVM : MapNotificationItemBaseVM
	{
		// Token: 0x060005AF RID: 1455 RVA: 0x0001C4E0 File Offset: 0x0001A6E0
		public PeaceOfferNotificationItemVM(PeaceOfferMapNotification data) : base(data)
		{
			PeaceOfferNotificationItemVM <>4__this = this;
			this._opponentFaction = data.OpponentFaction;
			this._tributeAmount = data.TributeAmount;
			this._onInspect = delegate()
			{
				CampaignEventDispatcher.Instance.OnPeaceOfferedToPlayer(data.OpponentFaction, data.TributeAmount);
				<>4__this._playerInspectedNotification = true;
				<>4__this.ExecuteRemove();
			};
			CampaignEvents.OnPeaceOfferCancelledEvent.AddNonSerializedListener(this, new Action<IFaction>(this.OnPeaceOfferCancelled));
			base.NotificationIdentifier = "ransom";
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0001C563 File Offset: 0x0001A763
		private void OnPeaceOfferCancelled(IFaction opponentFaction)
		{
			if (Campaign.Current.CampaignInformationManager.InformationDataExists<PeaceOfferMapNotification>((PeaceOfferMapNotification x) => x == base.Data))
			{
				base.ExecuteRemove();
				this._opponentFaction = null;
			}
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x0001C590 File Offset: 0x0001A790
		public override void OnFinalize()
		{
			base.OnFinalize();
			CampaignEventDispatcher.Instance.RemoveListeners(this);
			if (!this._playerInspectedNotification && Hero.MainHero.MapFaction.Leader != Hero.MainHero)
			{
				bool flag = false;
				foreach (KingdomDecision kingdomDecision in ((Kingdom)Hero.MainHero.MapFaction).UnresolvedDecisions)
				{
					if (kingdomDecision is MakePeaceKingdomDecision && ((MakePeaceKingdomDecision)kingdomDecision).ProposerClan.MapFaction == Hero.MainHero.MapFaction && ((MakePeaceKingdomDecision)kingdomDecision).FactionToMakePeaceWith == this._opponentFaction)
					{
						flag = true;
					}
				}
				if (!flag)
				{
					MakePeaceKingdomDecision kingdomDecision2 = new MakePeaceKingdomDecision(Hero.MainHero.MapFaction.Leader.Clan, this._opponentFaction, -this._tributeAmount, true);
					((Kingdom)Hero.MainHero.MapFaction).AddDecision(kingdomDecision2, false);
				}
			}
		}

		// Token: 0x04000269 RID: 617
		private IFaction _opponentFaction;

		// Token: 0x0400026A RID: 618
		private int _tributeAmount;

		// Token: 0x0400026B RID: 619
		private bool _playerInspectedNotification;
	}
}
