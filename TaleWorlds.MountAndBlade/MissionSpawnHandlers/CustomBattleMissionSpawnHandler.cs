using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade.MissionSpawnHandlers
{
	// Token: 0x020003A6 RID: 934
	public class CustomBattleMissionSpawnHandler : CustomMissionSpawnHandler
	{
		// Token: 0x06003286 RID: 12934 RVA: 0x000D155E File Offset: 0x000CF75E
		public CustomBattleMissionSpawnHandler(CustomBattleCombatant defenderParty, CustomBattleCombatant attackerParty)
		{
			this._defenderParty = defenderParty;
			this._attackerParty = attackerParty;
		}

		// Token: 0x06003287 RID: 12935 RVA: 0x000D1574 File Offset: 0x000CF774
		public override void AfterStart()
		{
			int numberOfHealthyMembers = this._defenderParty.NumberOfHealthyMembers;
			int numberOfHealthyMembers2 = this._attackerParty.NumberOfHealthyMembers;
			int defenderInitialSpawn = numberOfHealthyMembers;
			int attackerInitialSpawn = numberOfHealthyMembers2;
			this._missionAgentSpawnLogic.SetSpawnHorses(BattleSideEnum.Defender, true);
			this._missionAgentSpawnLogic.SetSpawnHorses(BattleSideEnum.Attacker, true);
			MissionSpawnSettings missionSpawnSettings = CustomMissionSpawnHandler.CreateCustomBattleWaveSpawnSettings();
			this._missionAgentSpawnLogic.InitWithSinglePhase(numberOfHealthyMembers, numberOfHealthyMembers2, defenderInitialSpawn, attackerInitialSpawn, true, true, missionSpawnSettings);
		}

		// Token: 0x040015D4 RID: 5588
		private CustomBattleCombatant _defenderParty;

		// Token: 0x040015D5 RID: 5589
		private CustomBattleCombatant _attackerParty;
	}
}
