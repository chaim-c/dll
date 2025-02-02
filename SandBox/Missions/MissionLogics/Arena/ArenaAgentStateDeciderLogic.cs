using System;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.MissionLogics.Arena
{
	// Token: 0x0200006F RID: 111
	public class ArenaAgentStateDeciderLogic : MissionLogic, IAgentStateDecider, IMissionBehavior
	{
		// Token: 0x06000438 RID: 1080 RVA: 0x0001CFF0 File Offset: 0x0001B1F0
		public AgentState GetAgentState(Agent effectedAgent, float deathProbability, out bool usedSurgery)
		{
			usedSurgery = false;
			return AgentState.Unconscious;
		}
	}
}
