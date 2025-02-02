using System;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.MissionLogics
{
	// Token: 0x02000065 RID: 101
	public class SandBoxSiegeMissionSpawnHandler : SandBoxMissionSpawnHandler
	{
		// Token: 0x060003E1 RID: 993 RVA: 0x0001AD4C File Offset: 0x00018F4C
		public override void AfterStart()
		{
			int numberOfInvolvedMen = this._mapEvent.GetNumberOfInvolvedMen(BattleSideEnum.Defender);
			int numberOfInvolvedMen2 = this._mapEvent.GetNumberOfInvolvedMen(BattleSideEnum.Attacker);
			int defenderInitialSpawn = numberOfInvolvedMen;
			int attackerInitialSpawn = numberOfInvolvedMen2;
			this._missionAgentSpawnLogic.SetSpawnHorses(BattleSideEnum.Defender, false);
			this._missionAgentSpawnLogic.SetSpawnHorses(BattleSideEnum.Attacker, false);
			MissionSpawnSettings missionSpawnSettings = SandBoxMissionSpawnHandler.CreateSandBoxBattleWaveSpawnSettings();
			missionSpawnSettings.DefenderAdvantageFactor = 1.5f;
			this._missionAgentSpawnLogic.InitWithSinglePhase(numberOfInvolvedMen, numberOfInvolvedMen2, defenderInitialSpawn, attackerInitialSpawn, false, false, missionSpawnSettings);
		}
	}
}
