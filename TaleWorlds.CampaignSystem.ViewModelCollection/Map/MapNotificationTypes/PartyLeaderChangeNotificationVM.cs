using System;
using TaleWorlds.CampaignSystem.MapNotificationTypes;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Map.MapNotificationTypes
{
	// Token: 0x02000045 RID: 69
	public class PartyLeaderChangeNotificationVM : MapNotificationItemBaseVM
	{
		// Token: 0x060005A4 RID: 1444 RVA: 0x0001C268 File Offset: 0x0001A468
		public PartyLeaderChangeNotificationVM(PartyLeaderChangeNotification data) : base(data)
		{
			this._party = data.Party;
			base.NotificationIdentifier = "death";
			this._onInspect = delegate()
			{
				InformationManager.ShowInquiry(new InquiryData(this._decisionPopupTitleText.ToString(), this._partyLeaderChangePopupText.ToString(), true, true, GameTexts.FindText("str_yes", null).ToString(), GameTexts.FindText("str_no", null).ToString(), delegate()
				{
					INavigationHandler navigationHandler = base.NavigationHandler;
					if (navigationHandler == null)
					{
						return;
					}
					navigationHandler.OpenClan(this._party.Party);
				}, null, "", 0f, null, null, null), false, false);
				Campaign.Current.TimeControlMode = CampaignTimeControlMode.Stop;
				this._playerInspectedNotification = true;
				base.ExecuteRemove();
			};
			CampaignEvents.OnPartyLeaderChangeOfferCanceledEvent.AddNonSerializedListener(this, new Action<MobileParty>(this.OnPartyLeaderChangeOfferCanceled));
			CampaignEvents.MobilePartyDestroyed.AddNonSerializedListener(this, new Action<MobileParty, PartyBase>(this.OnMobilePartyDestroyed));
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x0001C2F5 File Offset: 0x0001A4F5
		private void OnPartyLeaderChangeOfferCanceled(MobileParty party)
		{
			this.CheckAndExecuteRemove(party);
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x0001C2FE File Offset: 0x0001A4FE
		private void OnMobilePartyDestroyed(MobileParty party, PartyBase destroyerParty)
		{
			this.CheckAndExecuteRemove(party);
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0001C308 File Offset: 0x0001A508
		private void CheckAndExecuteRemove(MobileParty party)
		{
			if (Campaign.Current.CampaignInformationManager.InformationDataExists<PartyLeaderChangeNotification>((PartyLeaderChangeNotification x) => x.Party == party))
			{
				base.ExecuteRemove();
			}
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0001C345 File Offset: 0x0001A545
		public override void OnFinalize()
		{
			base.OnFinalize();
			CampaignEventDispatcher.Instance.RemoveListeners(this);
			if (!this._playerInspectedNotification)
			{
				CampaignEventDispatcher.Instance.OnPartyLeaderChangeOfferCanceled(this._party);
			}
		}

		// Token: 0x04000264 RID: 612
		private bool _playerInspectedNotification;

		// Token: 0x04000265 RID: 613
		private readonly MobileParty _party;

		// Token: 0x04000266 RID: 614
		private TextObject _decisionPopupTitleText = new TextObject("{=nFl0ufe3}A party without a leader", null);

		// Token: 0x04000267 RID: 615
		private TextObject _partyLeaderChangePopupText = new TextObject("{=OMqHwpXF}One of your parties has lost its leader. It will disband after a day has passed. You can assign a new clan member to lead it, if you wish to keep the party.{newline}{newline}Do you want to assign a new leader?", null);
	}
}
