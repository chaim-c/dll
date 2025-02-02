using System;

namespace TaleWorlds.MountAndBlade.Source.Missions.Handlers.Logic
{
	// Token: 0x020003BD RID: 957
	public class BattleMissionAgentInteractionLogic : MissionLogic
	{
		// Token: 0x0600330E RID: 13070 RVA: 0x000D49AC File Offset: 0x000D2BAC
		public override bool IsThereAgentAction(Agent userAgent, Agent otherAgent)
		{
			return !otherAgent.IsEnemyOf(userAgent);
		}
	}
}
