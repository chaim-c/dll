﻿using System;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x02000451 RID: 1105
	public static class MergePartiesAction
	{
		// Token: 0x060040FC RID: 16636 RVA: 0x00140C78 File Offset: 0x0013EE78
		private static void ApplyInternal(PartyBase majorParty, PartyBase joinedParty)
		{
			int numberOfAllMembers = joinedParty.NumberOfAllMembers;
			majorParty.AddMembers(joinedParty.MemberRoster);
			majorParty.AddPrisoners(joinedParty.PrisonRoster);
			DestroyPartyAction.Apply(null, joinedParty.MobileParty);
			if (majorParty == PartyBase.MainParty)
			{
				MBTextManager.SetTextVariable("NUMBER_OF_TROOPS", numberOfAllMembers);
				MBInformationManager.AddQuickInformation(new TextObject("{=511LONpe}{NUMBER_OF_TROOPS} troops joined you.", null), 0, null, "");
			}
		}

		// Token: 0x060040FD RID: 16637 RVA: 0x00140CDA File Offset: 0x0013EEDA
		public static void Apply(PartyBase majorParty, PartyBase joinedParty)
		{
			MergePartiesAction.ApplyInternal(majorParty, joinedParty);
		}
	}
}
