using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace SandBox
{
	// Token: 0x02000026 RID: 38
	public static class SandBoxHelpers
	{
		// Token: 0x020000FE RID: 254
		public static class MissionHelper
		{
			// Token: 0x06000B54 RID: 2900 RVA: 0x00051C64 File Offset: 0x0004FE64
			public static void FadeOutAgents(IEnumerable<Agent> agents, bool hideInstantly, bool hideMount)
			{
				if (agents != null)
				{
					Agent[] array = agents.ToArray<Agent>();
					foreach (Agent agent in array)
					{
						if (!agent.IsMount)
						{
							agent.FadeOut(hideInstantly, hideMount);
						}
					}
					foreach (Agent agent2 in array)
					{
						if (agent2.State != AgentState.Routed)
						{
							agent2.FadeOut(hideInstantly, hideMount);
						}
					}
				}
			}
		}
	}
}
