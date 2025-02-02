﻿using System;
using TaleWorlds.CampaignSystem.Roster;

namespace TaleWorlds.CampaignSystem.Party
{
	// Token: 0x020002A4 RID: 676
	// (Invoke) Token: 0x060026D1 RID: 9937
	public delegate void PartyScreenClosedDelegate(PartyBase leftOwnerParty, TroopRoster leftMemberRoster, TroopRoster leftPrisonRoster, PartyBase rightOwnerParty, TroopRoster rightMemberRoster, TroopRoster rightPrisonRoster, bool fromCancel);
}
