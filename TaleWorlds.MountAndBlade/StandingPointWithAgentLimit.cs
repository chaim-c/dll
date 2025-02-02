using System;
using System.Collections.Generic;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000348 RID: 840
	public class StandingPointWithAgentLimit : StandingPoint
	{
		// Token: 0x06002E10 RID: 11792 RVA: 0x000BBBC0 File Offset: 0x000B9DC0
		public void AddValidAgent(Agent agent)
		{
			if (agent != null)
			{
				this._validAgents.Add(agent);
			}
		}

		// Token: 0x06002E11 RID: 11793 RVA: 0x000BBBD1 File Offset: 0x000B9DD1
		public void ClearValidAgents()
		{
			this._validAgents.Clear();
		}

		// Token: 0x06002E12 RID: 11794 RVA: 0x000BBBDE File Offset: 0x000B9DDE
		public override bool IsDisabledForAgent(Agent agent)
		{
			return !this._validAgents.Contains(agent) || base.IsDisabledForAgent(agent);
		}

		// Token: 0x04001332 RID: 4914
		private readonly List<Agent> _validAgents = new List<Agent>();
	}
}
