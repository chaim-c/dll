using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000181 RID: 385
	public class TeamAiMultiplayerSiegeAttacker : TeamAISiegeComponent
	{
		// Token: 0x060013DE RID: 5086 RVA: 0x0004B67E File Offset: 0x0004987E
		public TeamAiMultiplayerSiegeAttacker(Mission currentMission, Team currentTeam, float thinkTimerTime, float applyTimerTime) : base(currentMission, currentTeam, thinkTimerTime, applyTimerTime)
		{
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x0004B68B File Offset: 0x0004988B
		public override void OnUnitAddedToFormationForTheFirstTime(Formation formation)
		{
		}
	}
}
