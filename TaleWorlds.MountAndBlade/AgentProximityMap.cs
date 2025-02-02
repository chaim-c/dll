using System;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000101 RID: 257
	public class AgentProximityMap
	{
		// Token: 0x06000C91 RID: 3217 RVA: 0x00016478 File Offset: 0x00014678
		public static bool CanSearchRadius(float searchRadius)
		{
			float num = Mission.Current.ProximityMapMaxSearchRadius();
			return searchRadius <= num;
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x00016498 File Offset: 0x00014698
		public static AgentProximityMap.ProximityMapSearchStruct BeginSearch(Mission mission, Vec2 searchPos, float searchRadius, bool extendRangeByBiggestAgentCollisionPadding = false)
		{
			if (extendRangeByBiggestAgentCollisionPadding)
			{
				searchRadius += mission.GetBiggestAgentCollisionPadding() + 1f;
			}
			AgentProximityMap.ProximityMapSearchStruct proximityMapSearchStruct = default(AgentProximityMap.ProximityMapSearchStruct);
			float num = mission.ProximityMapMaxSearchRadius();
			proximityMapSearchStruct.LoopAllAgents = (searchRadius > num);
			if (proximityMapSearchStruct.LoopAllAgents)
			{
				proximityMapSearchStruct.SearchStructInternal.SearchPos = searchPos;
				proximityMapSearchStruct.SearchStructInternal.SearchDistSq = searchRadius * searchRadius;
				proximityMapSearchStruct.LastAgentLoopIndex = 0;
				proximityMapSearchStruct.LastFoundAgent = null;
				MBReadOnlyList<Agent> agents = mission.Agents;
				while (agents.Count > proximityMapSearchStruct.LastAgentLoopIndex)
				{
					Agent agent = agents[proximityMapSearchStruct.LastAgentLoopIndex];
					if (agent.Position.AsVec2.DistanceSquared(searchPos) <= proximityMapSearchStruct.SearchStructInternal.SearchDistSq)
					{
						proximityMapSearchStruct.LastFoundAgent = agent;
						break;
					}
					proximityMapSearchStruct.LastAgentLoopIndex++;
				}
			}
			else
			{
				proximityMapSearchStruct.SearchStructInternal = mission.ProximityMapBeginSearch(searchPos, searchRadius);
				proximityMapSearchStruct.RefreshLastFoundAgent(mission);
			}
			return proximityMapSearchStruct;
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x00016584 File Offset: 0x00014784
		public static void FindNext(Mission mission, ref AgentProximityMap.ProximityMapSearchStruct searchStruct)
		{
			if (searchStruct.LoopAllAgents)
			{
				searchStruct.LastAgentLoopIndex++;
				searchStruct.LastFoundAgent = null;
				MBReadOnlyList<Agent> agents = mission.Agents;
				while (agents.Count > searchStruct.LastAgentLoopIndex)
				{
					Agent agent = agents[searchStruct.LastAgentLoopIndex];
					if (agent.Position.AsVec2.DistanceSquared(searchStruct.SearchStructInternal.SearchPos) <= searchStruct.SearchStructInternal.SearchDistSq)
					{
						searchStruct.LastFoundAgent = agent;
						return;
					}
					searchStruct.LastAgentLoopIndex++;
				}
				return;
			}
			mission.ProximityMapFindNext(ref searchStruct.SearchStructInternal);
			searchStruct.RefreshLastFoundAgent(mission);
		}

		// Token: 0x020003F5 RID: 1013
		public struct ProximityMapSearchStruct
		{
			// Token: 0x1700093E RID: 2366
			// (get) Token: 0x060033DE RID: 13278 RVA: 0x000D64BB File Offset: 0x000D46BB
			// (set) Token: 0x060033DF RID: 13279 RVA: 0x000D64C3 File Offset: 0x000D46C3
			public Agent LastFoundAgent { get; internal set; }

			// Token: 0x060033E0 RID: 13280 RVA: 0x000D64CC File Offset: 0x000D46CC
			internal void RefreshLastFoundAgent(Mission mission)
			{
				this.LastFoundAgent = this.SearchStructInternal.GetCurrentAgent(mission);
			}

			// Token: 0x0400177A RID: 6010
			internal AgentProximityMap.ProximityMapSearchStructInternal SearchStructInternal;

			// Token: 0x0400177B RID: 6011
			internal bool LoopAllAgents;

			// Token: 0x0400177C RID: 6012
			internal int LastAgentLoopIndex;
		}

		// Token: 0x020003F6 RID: 1014
		[EngineStruct("Managed_proximity_map_search_struct", false)]
		[Serializable]
		internal struct ProximityMapSearchStructInternal
		{
			// Token: 0x060033E1 RID: 13281 RVA: 0x000D64E0 File Offset: 0x000D46E0
			internal Agent GetCurrentAgent(Mission mission)
			{
				return mission.FindAgentWithIndex(this.CurrentElementIndex);
			}

			// Token: 0x0400177E RID: 6014
			internal int CurrentElementIndex;

			// Token: 0x0400177F RID: 6015
			internal Vec2i Loc;

			// Token: 0x04001780 RID: 6016
			internal Vec2i GridMin;

			// Token: 0x04001781 RID: 6017
			internal Vec2i GridMax;

			// Token: 0x04001782 RID: 6018
			internal Vec2 SearchPos;

			// Token: 0x04001783 RID: 6019
			internal float SearchDistSq;
		}
	}
}
