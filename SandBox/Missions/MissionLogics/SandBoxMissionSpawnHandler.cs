using System;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.MissionLogics
{
	// Token: 0x02000063 RID: 99
	public class SandBoxMissionSpawnHandler : MissionLogic
	{
		// Token: 0x060003DB RID: 987 RVA: 0x0001AC9F File Offset: 0x00018E9F
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			this._missionAgentSpawnLogic = base.Mission.GetMissionBehavior<MissionAgentSpawnLogic>();
			this._mapEvent = MapEvent.PlayerMapEvent;
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0001ACC4 File Offset: 0x00018EC4
		protected static MissionSpawnSettings CreateSandBoxBattleWaveSpawnSettings()
		{
			int reinforcementWaveCount = BannerlordConfig.GetReinforcementWaveCount();
			return new MissionSpawnSettings(MissionSpawnSettings.InitialSpawnMethod.BattleSizeAllocating, MissionSpawnSettings.ReinforcementTimingMethod.GlobalTimer, MissionSpawnSettings.ReinforcementSpawnMethod.Wave, 3f, 0f, 0f, 0.5f, reinforcementWaveCount, 0f, 0f, 1f, 0.75f);
		}

		// Token: 0x040001C1 RID: 449
		protected MissionAgentSpawnLogic _missionAgentSpawnLogic;

		// Token: 0x040001C2 RID: 450
		protected MapEvent _mapEvent;
	}
}
