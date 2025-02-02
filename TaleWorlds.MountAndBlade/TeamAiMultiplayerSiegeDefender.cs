using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000182 RID: 386
	public class TeamAiMultiplayerSiegeDefender : TeamAISiegeComponent
	{
		// Token: 0x060013E0 RID: 5088 RVA: 0x0004B68D File Offset: 0x0004988D
		public TeamAiMultiplayerSiegeDefender(Mission currentMission, Team currentTeam, float thinkTimerTime, float applyTimerTime) : base(currentMission, currentTeam, thinkTimerTime, applyTimerTime)
		{
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x0004B69A File Offset: 0x0004989A
		public override void OnUnitAddedToFormationForTheFirstTime(Formation formation)
		{
		}
	}
}
