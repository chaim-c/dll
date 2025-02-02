using System;
using System.Linq;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Election;
using TaleWorlds.CampaignSystem.MapNotificationTypes;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Map.MapNotificationTypes
{
	// Token: 0x0200003F RID: 63
	public class KingdomVoteNotificationItemVM : MapNotificationItemBaseVM
	{
		// Token: 0x0600056F RID: 1391 RVA: 0x0001BA6C File Offset: 0x00019C6C
		public KingdomVoteNotificationItemVM(KingdomDecisionMapNotification data) : base(data)
		{
			KingdomVoteNotificationItemVM <>4__this = this;
			this._decision = data.Decision;
			this._kingdomOfDecision = data.KingdomOfDecision;
			base.NotificationIdentifier = "vote";
			CampaignEvents.OnClanChangedKingdomEvent.AddNonSerializedListener(this, new Action<Clan, Kingdom, Kingdom, ChangeKingdomAction.ChangeKingdomActionDetail, bool>(this.OnClanChangedKingdom));
			CampaignEvents.KingdomDecisionCancelled.AddNonSerializedListener(this, new Action<KingdomDecision, bool>(this.OnDecisionCancelled));
			CampaignEvents.KingdomDecisionConcluded.AddNonSerializedListener(this, new Action<KingdomDecision, DecisionOutcome, bool>(this.OnDecisionConcluded));
			this._onInspect = new Action(this.OnInspect);
			this._onInspectOpenKingdom = delegate()
			{
				<>4__this.NavigationHandler.OpenKingdom(data.Decision);
			};
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0001BB30 File Offset: 0x00019D30
		private void OnInspect()
		{
			if (!this._decision.ShouldBeCancelled())
			{
				Kingdom kingdom = Clan.PlayerClan.Kingdom;
				if (kingdom != null && kingdom.UnresolvedDecisions.Any((KingdomDecision d) => d == this._decision))
				{
					this._onInspectOpenKingdom();
					return;
				}
			}
			InformationManager.ShowInquiry(new InquiryData("", new TextObject("{=i9OsCshW}This kingdom decision is not relevant anymore.", null).ToString(), true, false, GameTexts.FindText("str_ok", null).ToString(), "", null, null, "", 0f, null, null, null), false, false);
			base.ExecuteRemove();
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0001BBCF File Offset: 0x00019DCF
		private void OnDecisionConcluded(KingdomDecision decision, DecisionOutcome arg2, bool arg3)
		{
			if (decision == this._decision)
			{
				base.ExecuteRemove();
			}
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0001BBE0 File Offset: 0x00019DE0
		private void OnDecisionCancelled(KingdomDecision decision, bool arg2)
		{
			if (decision == this._decision)
			{
				base.ExecuteRemove();
			}
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0001BBF1 File Offset: 0x00019DF1
		private void OnClanChangedKingdom(Clan clan, Kingdom oldKingdom, Kingdom newKingdom, ChangeKingdomAction.ChangeKingdomActionDetail detail, bool showNotification)
		{
			if (clan == Clan.PlayerClan)
			{
				base.ExecuteRemove();
			}
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0001BC01 File Offset: 0x00019E01
		public override void OnFinalize()
		{
			base.OnFinalize();
			CampaignEvents.OnClanChangedKingdomEvent.ClearListeners(this);
		}

		// Token: 0x0400024A RID: 586
		private KingdomDecision _decision;

		// Token: 0x0400024B RID: 587
		private Kingdom _kingdomOfDecision;

		// Token: 0x0400024C RID: 588
		private Action _onInspectOpenKingdom;
	}
}
