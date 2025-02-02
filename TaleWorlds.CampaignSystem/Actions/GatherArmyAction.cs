using System;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x02000444 RID: 1092
	public static class GatherArmyAction
	{
		// Token: 0x060040CB RID: 16587 RVA: 0x0013F2F4 File Offset: 0x0013D4F4
		private static void ApplyInternal(MobileParty leaderParty, Settlement targetSettlement, float playerInvolvement = 0f)
		{
			Army army = leaderParty.Army;
			army.AIBehavior = Army.AIBehaviorFlags.Gathering;
			CampaignEventDispatcher.Instance.OnArmyGathered(army, targetSettlement);
		}

		// Token: 0x060040CC RID: 16588 RVA: 0x0013F31B File Offset: 0x0013D51B
		public static void Apply(MobileParty leaderParty, Settlement targetSettlement)
		{
			GatherArmyAction.ApplyInternal(leaderParty, targetSettlement, (leaderParty == MobileParty.MainParty) ? 1f : 0f);
		}
	}
}
