using System;

namespace TaleWorlds.MountAndBlade.MissionSpawnHandlers
{
	// Token: 0x020003A7 RID: 935
	public class CustomMissionSpawnHandler : MissionLogic
	{
		// Token: 0x06003288 RID: 12936 RVA: 0x000D15D1 File Offset: 0x000CF7D1
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			this._missionAgentSpawnLogic = base.Mission.GetMissionBehavior<MissionAgentSpawnLogic>();
		}

		// Token: 0x06003289 RID: 12937 RVA: 0x000D15EC File Offset: 0x000CF7EC
		protected static MissionSpawnSettings CreateCustomBattleWaveSpawnSettings()
		{
			return new MissionSpawnSettings(MissionSpawnSettings.InitialSpawnMethod.BattleSizeAllocating, MissionSpawnSettings.ReinforcementTimingMethod.GlobalTimer, MissionSpawnSettings.ReinforcementSpawnMethod.Wave, 3f, 0f, 0f, 0.5f, 0, 0f, 0f, 1f, 0.75f);
		}

		// Token: 0x040015D6 RID: 5590
		protected MissionAgentSpawnLogic _missionAgentSpawnLogic;
	}
}
