using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x02000458 RID: 1112
	public static class SiegeAftermathAction
	{
		// Token: 0x06004118 RID: 16664 RVA: 0x00141903 File Offset: 0x0013FB03
		private static void ApplyInternal(MobileParty attackerParty, Settlement settlement, SiegeAftermathAction.SiegeAftermath aftermathType, Clan previousSettlementOwner, Dictionary<MobileParty, float> partyContributions)
		{
			CampaignEventDispatcher.Instance.OnSiegeAftermathApplied(attackerParty, settlement, aftermathType, previousSettlementOwner, partyContributions);
		}

		// Token: 0x06004119 RID: 16665 RVA: 0x00141915 File Offset: 0x0013FB15
		public static void ApplyAftermath(MobileParty attackerParty, Settlement settlement, SiegeAftermathAction.SiegeAftermath aftermathType, Clan previousSettlementOwner, Dictionary<MobileParty, float> partyContributions)
		{
			SiegeAftermathAction.ApplyInternal(attackerParty, settlement, aftermathType, previousSettlementOwner, partyContributions);
		}

		// Token: 0x0200077C RID: 1916
		public enum SiegeAftermath
		{
			// Token: 0x04001F54 RID: 8020
			Devastate,
			// Token: 0x04001F55 RID: 8021
			Pillage,
			// Token: 0x04001F56 RID: 8022
			ShowMercy
		}
	}
}
