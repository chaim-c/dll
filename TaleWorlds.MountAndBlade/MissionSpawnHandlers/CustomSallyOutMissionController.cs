using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade.MissionSpawnHandlers
{
	// Token: 0x020003A8 RID: 936
	public class CustomSallyOutMissionController : SallyOutMissionController
	{
		// Token: 0x0600328B RID: 12939 RVA: 0x000D1632 File Offset: 0x000CF832
		public CustomSallyOutMissionController(IBattleCombatant defenderBattleCombatant, IBattleCombatant attackerBattleCombatant)
		{
			this._battleCombatants = new CustomBattleCombatant[]
			{
				(CustomBattleCombatant)defenderBattleCombatant,
				(CustomBattleCombatant)attackerBattleCombatant
			};
		}

		// Token: 0x0600328C RID: 12940 RVA: 0x000D1658 File Offset: 0x000CF858
		protected override void GetInitialTroopCounts(out int besiegedTotalTroopCount, out int besiegerTotalTroopCount)
		{
			besiegedTotalTroopCount = this._battleCombatants[0].NumberOfHealthyMembers;
			besiegerTotalTroopCount = this._battleCombatants[1].NumberOfHealthyMembers;
		}

		// Token: 0x040015D7 RID: 5591
		private readonly CustomBattleCombatant[] _battleCombatants;
	}
}
