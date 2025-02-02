using System;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.MissionLogics
{
	// Token: 0x0200005F RID: 95
	public class SandBoxBattleMissionSpawnHandler : SandBoxMissionSpawnHandler
	{
		// Token: 0x060003D2 RID: 978 RVA: 0x0001A9C4 File Offset: 0x00018BC4
		public override void AfterStart()
		{
			int numberOfInvolvedMen = this._mapEvent.GetNumberOfInvolvedMen(BattleSideEnum.Defender);
			int numberOfInvolvedMen2 = this._mapEvent.GetNumberOfInvolvedMen(BattleSideEnum.Attacker);
			int defenderInitialSpawn = numberOfInvolvedMen;
			int attackerInitialSpawn = numberOfInvolvedMen2;
			this._missionAgentSpawnLogic.SetSpawnHorses(BattleSideEnum.Defender, !this._mapEvent.IsSiegeAssault);
			this._missionAgentSpawnLogic.SetSpawnHorses(BattleSideEnum.Attacker, !this._mapEvent.IsSiegeAssault);
			MissionSpawnSettings missionSpawnSettings = SandBoxMissionSpawnHandler.CreateSandBoxBattleWaveSpawnSettings();
			this._missionAgentSpawnLogic.InitWithSinglePhase(numberOfInvolvedMen, numberOfInvolvedMen2, defenderInitialSpawn, attackerInitialSpawn, true, true, missionSpawnSettings);
		}
	}
}
