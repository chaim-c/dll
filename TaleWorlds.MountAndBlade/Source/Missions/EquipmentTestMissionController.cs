using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade.Source.Missions
{
	// Token: 0x020003B2 RID: 946
	public class EquipmentTestMissionController : MissionLogic
	{
		// Token: 0x060032C2 RID: 12994 RVA: 0x000D2F94 File Offset: 0x000D1194
		public override void AfterStart()
		{
			base.AfterStart();
			GameEntity entity = base.Mission.Scene.FindEntityWithTag("spawnpoint_player");
			base.Mission.SpawnAgent(new AgentBuildData(Game.Current.PlayerTroop).Team(base.Mission.AttackerTeam).InitialFrameFromSpawnPointEntity(entity).CivilianEquipment(false).Controller(Agent.ControllerType.Player), false);
		}
	}
}
