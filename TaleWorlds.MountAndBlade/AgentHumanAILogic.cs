using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000261 RID: 609
	public class AgentHumanAILogic : MissionLogic
	{
		// Token: 0x0600206C RID: 8300 RVA: 0x000735C1 File Offset: 0x000717C1
		public override void OnAgentCreated(Agent agent)
		{
			base.OnAgentCreated(agent);
			if (agent.IsAIControlled && agent.IsHuman)
			{
				agent.AddComponent(new HumanAIComponent(agent));
			}
		}

		// Token: 0x0600206D RID: 8301 RVA: 0x000735E8 File Offset: 0x000717E8
		protected internal override void OnAgentControllerChanged(Agent agent, Agent.ControllerType oldController)
		{
			base.OnAgentControllerChanged(agent, oldController);
			if (agent.IsHuman)
			{
				if (agent.Controller == Agent.ControllerType.AI)
				{
					agent.AddComponent(new HumanAIComponent(agent));
					return;
				}
				if (oldController == Agent.ControllerType.AI && agent.HumanAIComponent != null)
				{
					agent.RemoveComponent(agent.HumanAIComponent);
				}
			}
		}
	}
}
