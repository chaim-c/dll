using System;
using TaleWorlds.CampaignSystem.Roster;

namespace TaleWorlds.CampaignSystem.Party
{
	// Token: 0x020002A0 RID: 672
	// (Invoke) Token: 0x060026C1 RID: 9921
	public delegate bool PartyPresentationDoneButtonDelegate(TroopRoster leftMemberRoster, TroopRoster leftPrisonRoster, TroopRoster rightMemberRoster, TroopRoster rightPrisonRoster, FlattenedTroopRoster takenPrisonerRoster, FlattenedTroopRoster releasedPrisonerRoster, bool isForced, PartyBase leftParty = null, PartyBase rightParty = null);
}
