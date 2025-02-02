using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000147 RID: 327
	public interface IDetachment
	{
		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06001044 RID: 4164
		MBReadOnlyList<Formation> UserFormations { get; }

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06001045 RID: 4165
		bool IsLoose { get; }

		// Token: 0x06001046 RID: 4166
		bool IsAgentUsingOrInterested(Agent agent);

		// Token: 0x06001047 RID: 4167
		float? GetWeightOfNextSlot(BattleSideEnum side);

		// Token: 0x06001048 RID: 4168
		float GetDetachmentWeight(BattleSideEnum side);

		// Token: 0x06001049 RID: 4169
		float ComputeAndCacheDetachmentWeight(BattleSideEnum side);

		// Token: 0x0600104A RID: 4170
		float GetDetachmentWeightFromCache();

		// Token: 0x0600104B RID: 4171
		void GetSlotIndexWeightTuples(List<ValueTuple<int, float>> slotIndexWeightTuples);

		// Token: 0x0600104C RID: 4172
		bool IsSlotAtIndexAvailableForAgent(int slotIndex, Agent agent);

		// Token: 0x0600104D RID: 4173
		bool IsAgentEligible(Agent agent);

		// Token: 0x0600104E RID: 4174
		void AddAgentAtSlotIndex(Agent agent, int slotIndex);

		// Token: 0x0600104F RID: 4175
		Agent GetMovingAgentAtSlotIndex(int slotIndex);

		// Token: 0x06001050 RID: 4176
		void MarkSlotAtIndex(int slotIndex);

		// Token: 0x06001051 RID: 4177
		bool IsDetachmentRecentlyEvaluated();

		// Token: 0x06001052 RID: 4178
		void UnmarkDetachment();

		// Token: 0x06001053 RID: 4179
		float? GetWeightOfAgentAtNextSlot(List<Agent> candidates, out Agent match);

		// Token: 0x06001054 RID: 4180
		float? GetWeightOfAgentAtNextSlot(List<ValueTuple<Agent, float>> agentTemplateScores, out Agent match);

		// Token: 0x06001055 RID: 4181
		float GetTemplateWeightOfAgent(Agent candidate);

		// Token: 0x06001056 RID: 4182
		List<float> GetTemplateCostsOfAgent(Agent candidate, List<float> oldValue);

		// Token: 0x06001057 RID: 4183
		float GetExactCostOfAgentAtSlot(Agent candidate, int slotIndex);

		// Token: 0x06001058 RID: 4184
		float GetWeightOfOccupiedSlot(Agent detachedAgent);

		// Token: 0x06001059 RID: 4185
		float? GetWeightOfAgentAtOccupiedSlot(Agent detachedAgent, List<Agent> candidates, out Agent match);

		// Token: 0x0600105A RID: 4186
		bool IsStandingPointAvailableForAgent(Agent agent);

		// Token: 0x0600105B RID: 4187
		void AddAgent(Agent agent, int slotIndex = -1);

		// Token: 0x0600105C RID: 4188
		void RemoveAgent(Agent detachedAgent);

		// Token: 0x0600105D RID: 4189
		int GetNumberOfUsableSlots();

		// Token: 0x0600105E RID: 4190
		void FormationStartUsing(Formation formation);

		// Token: 0x0600105F RID: 4191
		void FormationStopUsing(Formation formation);

		// Token: 0x06001060 RID: 4192
		bool IsUsedByFormation(Formation formation);

		// Token: 0x06001061 RID: 4193
		WorldFrame? GetAgentFrame(Agent detachedAgent);

		// Token: 0x06001062 RID: 4194
		void ResetEvaluation();

		// Token: 0x06001063 RID: 4195
		bool IsEvaluated();

		// Token: 0x06001064 RID: 4196
		void SetAsEvaluated();

		// Token: 0x06001065 RID: 4197
		void OnFormationLeave(Formation formation);
	}
}
