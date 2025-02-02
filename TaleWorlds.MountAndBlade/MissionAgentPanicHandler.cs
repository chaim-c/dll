using System;
using System.Collections.Generic;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000278 RID: 632
	public class MissionAgentPanicHandler : MissionLogic
	{
		// Token: 0x0600213D RID: 8509 RVA: 0x000789F2 File Offset: 0x00076BF2
		public MissionAgentPanicHandler()
		{
			this._panickedAgents = new List<Agent>(256);
			this._panickedFormations = new List<Formation>(24);
			this._panickedTeams = new List<Team>(2);
		}

		// Token: 0x0600213E RID: 8510 RVA: 0x00078A24 File Offset: 0x00076C24
		public override void OnAgentPanicked(Agent agent)
		{
			this._panickedAgents.Add(agent);
			if (agent.Formation != null && agent.Team != null)
			{
				if (!this._panickedFormations.Contains(agent.Formation))
				{
					this._panickedFormations.Add(agent.Formation);
				}
				if (!this._panickedTeams.Contains(agent.Team))
				{
					this._panickedTeams.Add(agent.Team);
				}
			}
		}

		// Token: 0x0600213F RID: 8511 RVA: 0x00078A98 File Offset: 0x00076C98
		public override void OnPreMissionTick(float dt)
		{
			if (this._panickedAgents.Count > 0)
			{
				foreach (Team team in this._panickedTeams)
				{
					team.UpdateCachedEnemyDataForFleeing();
				}
				foreach (Formation formation in this._panickedFormations)
				{
					formation.OnBatchUnitRemovalStart();
				}
				foreach (Agent agent in this._panickedAgents)
				{
					CommonAIComponent commonAIComponent = agent.CommonAIComponent;
					if (commonAIComponent != null)
					{
						commonAIComponent.Retreat(false);
					}
					Mission.Current.OnAgentFleeing(agent);
				}
				foreach (Formation formation2 in this._panickedFormations)
				{
					formation2.OnBatchUnitRemovalEnd();
				}
				this._panickedAgents.Clear();
				this._panickedFormations.Clear();
				this._panickedTeams.Clear();
			}
		}

		// Token: 0x06002140 RID: 8512 RVA: 0x00078BF0 File Offset: 0x00076DF0
		public override void OnRemoveBehavior()
		{
			this._panickedAgents.Clear();
			this._panickedFormations.Clear();
			this._panickedTeams.Clear();
			base.OnRemoveBehavior();
		}

		// Token: 0x04000C5E RID: 3166
		private readonly List<Agent> _panickedAgents;

		// Token: 0x04000C5F RID: 3167
		private readonly List<Formation> _panickedFormations;

		// Token: 0x04000C60 RID: 3168
		private readonly List<Team> _panickedTeams;
	}
}
