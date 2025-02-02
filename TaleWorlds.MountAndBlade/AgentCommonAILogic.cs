using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000260 RID: 608
	public class AgentCommonAILogic : MissionLogic
	{
		// Token: 0x06002069 RID: 8297 RVA: 0x00073563 File Offset: 0x00071763
		public override void OnAgentCreated(Agent agent)
		{
			base.OnAgentCreated(agent);
			if (agent.IsAIControlled)
			{
				agent.AddComponent(new CommonAIComponent(agent));
			}
		}

		// Token: 0x0600206A RID: 8298 RVA: 0x00073580 File Offset: 0x00071780
		protected internal override void OnAgentControllerChanged(Agent agent, Agent.ControllerType oldController)
		{
			base.OnAgentControllerChanged(agent, oldController);
			if (agent.Controller == Agent.ControllerType.AI)
			{
				agent.AddComponent(new CommonAIComponent(agent));
				return;
			}
			if (oldController == Agent.ControllerType.AI && agent.CommonAIComponent != null)
			{
				agent.RemoveComponent(agent.CommonAIComponent);
			}
		}
	}
}
