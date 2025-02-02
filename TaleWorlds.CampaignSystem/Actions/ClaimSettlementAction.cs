using System;
using TaleWorlds.CampaignSystem.Settlements;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x02000437 RID: 1079
	public static class ClaimSettlementAction
	{
		// Token: 0x0600408C RID: 16524 RVA: 0x0013E409 File Offset: 0x0013C609
		private static void ApplyInternal(Hero claimant, Settlement claimedSettlement)
		{
			ClaimSettlementAction.ImpactRelations(claimant, claimedSettlement);
		}

		// Token: 0x0600408D RID: 16525 RVA: 0x0013E414 File Offset: 0x0013C614
		private static void ImpactRelations(Hero claimant, Settlement claimedSettlement)
		{
			if (claimedSettlement.OwnerClan.Leader != null)
			{
				ChangeRelationAction.ApplyRelationChangeBetweenHeroes(claimant, claimedSettlement.OwnerClan.Leader, -50, false);
				if (!claimedSettlement.OwnerClan.IsMapFaction)
				{
					ChangeRelationAction.ApplyRelationChangeBetweenHeroes(claimant, claimedSettlement.OwnerClan.Leader, -20, false);
				}
			}
		}

		// Token: 0x0600408E RID: 16526 RVA: 0x0013E463 File Offset: 0x0013C663
		public static void Apply(Hero claimant, Settlement claimedSettlement)
		{
			ClaimSettlementAction.ApplyInternal(claimant, claimedSettlement);
		}
	}
}
