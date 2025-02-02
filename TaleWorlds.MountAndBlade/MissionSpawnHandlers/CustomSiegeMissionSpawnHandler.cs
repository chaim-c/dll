using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade.MissionSpawnHandlers
{
	// Token: 0x020003A9 RID: 937
	public class CustomSiegeMissionSpawnHandler : CustomMissionSpawnHandler
	{
		// Token: 0x0600328D RID: 12941 RVA: 0x000D1678 File Offset: 0x000CF878
		public CustomSiegeMissionSpawnHandler(IBattleCombatant defenderBattleCombatant, IBattleCombatant attackerBattleCombatant, bool spawnWithHorses)
		{
			this._battleCombatants = new CustomBattleCombatant[]
			{
				(CustomBattleCombatant)defenderBattleCombatant,
				(CustomBattleCombatant)attackerBattleCombatant
			};
			this._spawnWithHorses = spawnWithHorses;
		}

		// Token: 0x0600328E RID: 12942 RVA: 0x000D16A8 File Offset: 0x000CF8A8
		public override void AfterStart()
		{
			int numberOfHealthyMembers = this._battleCombatants[0].NumberOfHealthyMembers;
			int numberOfHealthyMembers2 = this._battleCombatants[1].NumberOfHealthyMembers;
			int defenderInitialSpawn = numberOfHealthyMembers;
			int attackerInitialSpawn = numberOfHealthyMembers2;
			this._missionAgentSpawnLogic.SetSpawnHorses(BattleSideEnum.Defender, this._spawnWithHorses);
			this._missionAgentSpawnLogic.SetSpawnHorses(BattleSideEnum.Attacker, this._spawnWithHorses);
			MissionSpawnSettings missionSpawnSettings = CustomMissionSpawnHandler.CreateCustomBattleWaveSpawnSettings();
			this._missionAgentSpawnLogic.InitWithSinglePhase(numberOfHealthyMembers, numberOfHealthyMembers2, defenderInitialSpawn, attackerInitialSpawn, false, false, missionSpawnSettings);
		}

		// Token: 0x040015D8 RID: 5592
		private CustomBattleCombatant[] _battleCombatants;

		// Token: 0x040015D9 RID: 5593
		private bool _spawnWithHorses;
	}
}
