using System;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x02000453 RID: 1107
	public static class RemoveCompanionAction
	{
		// Token: 0x06004101 RID: 16641 RVA: 0x00140E24 File Offset: 0x0013F024
		private static void ApplyInternal(Clan clan, Hero companion, RemoveCompanionAction.RemoveCompanionDetail detail)
		{
			PartyBase partyBase;
			if (companion.PartyBelongedTo == null)
			{
				Settlement currentSettlement = companion.CurrentSettlement;
				partyBase = ((currentSettlement != null) ? currentSettlement.Party : null);
			}
			else
			{
				partyBase = companion.PartyBelongedTo.Party;
			}
			PartyBase partyBase2 = partyBase;
			companion.CompanionOf = null;
			if (partyBase2 != null && detail != RemoveCompanionAction.RemoveCompanionDetail.ByTurningToLord)
			{
				if (partyBase2.LeaderHero != companion)
				{
					partyBase2.MemberRoster.AddToCounts(companion.CharacterObject, -1, false, 0, 0, true, -1);
				}
				else
				{
					partyBase2.MemberRoster.AddToCounts(companion.CharacterObject, -1, false, 0, 0, true, -1);
					partyBase2.MobileParty.RemovePartyLeader();
					if (partyBase2.MemberRoster.Count == 0)
					{
						DestroyPartyAction.Apply(null, partyBase2.MobileParty);
					}
					else
					{
						DisbandPartyAction.StartDisband(partyBase2.MobileParty);
					}
				}
			}
			if (detail == RemoveCompanionAction.RemoveCompanionDetail.Fire)
			{
				companion.CompanionOf = null;
				if (companion.PartyBelongedToAsPrisoner != null)
				{
					EndCaptivityAction.ApplyByEscape(companion, null);
				}
				else
				{
					MakeHeroFugitiveAction.Apply(companion);
				}
				if (companion.IsWanderer)
				{
					companion.ResetEquipments();
				}
			}
			if (companion.GovernorOf != null)
			{
				ChangeGovernorAction.RemoveGovernorOf(companion);
			}
			CampaignEventDispatcher.Instance.OnCompanionRemoved(companion, detail);
		}

		// Token: 0x06004102 RID: 16642 RVA: 0x00140F1C File Offset: 0x0013F11C
		public static void ApplyByFire(Clan clan, Hero companion)
		{
			RemoveCompanionAction.ApplyInternal(clan, companion, RemoveCompanionAction.RemoveCompanionDetail.Fire);
		}

		// Token: 0x06004103 RID: 16643 RVA: 0x00140F26 File Offset: 0x0013F126
		public static void ApplyAfterQuest(Clan clan, Hero companion)
		{
			RemoveCompanionAction.ApplyInternal(clan, companion, RemoveCompanionAction.RemoveCompanionDetail.AfterQuest);
		}

		// Token: 0x06004104 RID: 16644 RVA: 0x00140F30 File Offset: 0x0013F130
		public static void ApplyByDeath(Clan clan, Hero companion)
		{
			RemoveCompanionAction.ApplyInternal(clan, companion, RemoveCompanionAction.RemoveCompanionDetail.Death);
		}

		// Token: 0x06004105 RID: 16645 RVA: 0x00140F3A File Offset: 0x0013F13A
		public static void ApplyByByTurningToLord(Clan clan, Hero companion)
		{
			RemoveCompanionAction.ApplyInternal(clan, companion, RemoveCompanionAction.RemoveCompanionDetail.ByTurningToLord);
		}

		// Token: 0x02000779 RID: 1913
		public enum RemoveCompanionDetail
		{
			// Token: 0x04001F43 RID: 8003
			Fire,
			// Token: 0x04001F44 RID: 8004
			Death,
			// Token: 0x04001F45 RID: 8005
			AfterQuest,
			// Token: 0x04001F46 RID: 8006
			ByTurningToLord
		}
	}
}
