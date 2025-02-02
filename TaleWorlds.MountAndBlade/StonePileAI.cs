using System;
using System.Collections.Generic;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000157 RID: 343
	public class StonePileAI : UsableMachineAIBase
	{
		// Token: 0x0600116B RID: 4459 RVA: 0x000376C5 File Offset: 0x000358C5
		public StonePileAI(StonePile stonePile) : base(stonePile)
		{
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x000376D0 File Offset: 0x000358D0
		public static Agent GetSuitableAgentForStandingPoint(StonePile usableMachine, StandingPoint standingPoint, List<Agent> agents, List<Agent> usedAgents)
		{
			float num = float.MinValue;
			Agent result = null;
			foreach (Agent agent in agents)
			{
				if (StonePileAI.IsAgentAssignable(agent) && !standingPoint.IsDisabledForAgent(agent) && standingPoint.GetUsageScoreForAgent(agent) > num)
				{
					num = standingPoint.GetUsageScoreForAgent(agent);
					result = agent;
				}
			}
			return result;
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x00037748 File Offset: 0x00035948
		public static Agent GetSuitableAgentForStandingPoint(StonePile stonePile, StandingPoint standingPoint, List<ValueTuple<Agent, float>> agents, List<Agent> usedAgents, float weight)
		{
			float num = float.MinValue;
			Agent result = null;
			foreach (ValueTuple<Agent, float> valueTuple in agents)
			{
				Agent item = valueTuple.Item1;
				if (StonePileAI.IsAgentAssignable(item) && !standingPoint.IsDisabledForAgent(item) && standingPoint.GetUsageScoreForAgent(item) > num)
				{
					num = standingPoint.GetUsageScoreForAgent(item);
					result = item;
				}
			}
			return result;
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x000377C4 File Offset: 0x000359C4
		public static bool IsAgentAssignable(Agent agent)
		{
			return agent != null && agent.IsAIControlled && agent.IsActive() && !agent.IsRunningAway && !agent.InteractingWithAnyGameObject() && (agent.Formation == null || !agent.IsDetachedFromFormation);
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x000377FE File Offset: 0x000359FE
		protected override void HandleAgentStopUsingStandingPoint(Agent agent, StandingPoint standingPoint)
		{
			agent.DisableScriptedCombatMovement();
			base.HandleAgentStopUsingStandingPoint(agent, standingPoint);
		}
	}
}
