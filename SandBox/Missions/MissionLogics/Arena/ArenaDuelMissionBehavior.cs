using System;
using SandBox.Tournaments.MissionLogics;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.MissionLogics.Arena
{
	// Token: 0x02000070 RID: 112
	public class ArenaDuelMissionBehavior : MissionLogic
	{
		// Token: 0x0600043A RID: 1082 RVA: 0x0001CFFE File Offset: 0x0001B1FE
		public override void AfterStart()
		{
			TournamentBehavior.DeleteTournamentSetsExcept(base.Mission.Scene.FindEntityWithTag("tournament_fight"));
		}
	}
}
