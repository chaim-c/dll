﻿using System;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x0200043E RID: 1086
	public static class DisbandPartyAction
	{
		// Token: 0x060040AD RID: 16557 RVA: 0x0013EBD8 File Offset: 0x0013CDD8
		public static void StartDisband(MobileParty disbandParty)
		{
			if (disbandParty.IsDisbanding)
			{
				return;
			}
			if (disbandParty.MemberRoster.TotalManCount == 0)
			{
				DestroyPartyAction.Apply(null, disbandParty);
				return;
			}
			IDisbandPartyCampaignBehavior campaignBehavior = Campaign.Current.GetCampaignBehavior<IDisbandPartyCampaignBehavior>();
			if (campaignBehavior != null && campaignBehavior.IsPartyWaitingForDisband(disbandParty))
			{
				return;
			}
			if (disbandParty.Army != null)
			{
				if (disbandParty == disbandParty.Army.LeaderParty)
				{
					DisbandArmyAction.ApplyByUnknownReason(disbandParty.Army);
				}
				else
				{
					disbandParty.Army = null;
				}
			}
			TextObject textObject = new TextObject("{=ithcVNfA}{CLAN_NAME}{.o} Party", null);
			textObject.SetTextVariable("CLAN_NAME", (disbandParty.ActualClan != null) ? disbandParty.ActualClan.Name : CampaignData.NeutralFactionName);
			disbandParty.SetCustomName(textObject);
			CampaignEventDispatcher.Instance.OnPartyDisbandStarted(disbandParty);
		}

		// Token: 0x060040AE RID: 16558 RVA: 0x0013EC89 File Offset: 0x0013CE89
		public static void CancelDisband(MobileParty disbandParty)
		{
			CampaignEventDispatcher.Instance.OnPartyDisbandCanceled(disbandParty);
			disbandParty.IsDisbanding = false;
			disbandParty.SetCustomName(TextObject.Empty);
			disbandParty.Ai.SetMoveModeHold();
		}
	}
}
