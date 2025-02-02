using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000259 RID: 601
	public interface IAgentStateDecider : IMissionBehavior
	{
		// Token: 0x06002008 RID: 8200
		AgentState GetAgentState(Agent affectedAgent, float deathProbability, out bool usedSurgery);
	}
}
