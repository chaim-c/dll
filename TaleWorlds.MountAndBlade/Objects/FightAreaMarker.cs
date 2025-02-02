using System;
using System.Collections.Generic;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade.Objects
{
	// Token: 0x02000388 RID: 904
	public class FightAreaMarker : AreaMarker
	{
		// Token: 0x0600317D RID: 12669 RVA: 0x000CC450 File Offset: 0x000CA650
		public IEnumerable<Agent> GetAgentsInRange(Team team, bool humanOnly = true)
		{
			foreach (Agent agent in team.ActiveAgents)
			{
				if ((!humanOnly || agent.IsHuman) && base.IsPositionInRange(agent.Position))
				{
					yield return agent;
				}
			}
			List<Agent>.Enumerator enumerator = default(List<Agent>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x0600317E RID: 12670 RVA: 0x000CC46E File Offset: 0x000CA66E
		public IEnumerable<Agent> GetAgentsInRange(BattleSideEnum side, bool humanOnly = true)
		{
			foreach (Team team in Mission.Current.Teams)
			{
				if (team.Side == side)
				{
					foreach (Agent agent in this.GetAgentsInRange(team, humanOnly))
					{
						yield return agent;
					}
					IEnumerator<Agent> enumerator2 = null;
				}
			}
			List<Team>.Enumerator enumerator = default(List<Team>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x0400152E RID: 5422
		public int SubAreaIndex = 1;
	}
}
