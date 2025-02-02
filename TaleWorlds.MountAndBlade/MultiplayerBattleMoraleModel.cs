using System;
using System.Runtime.CompilerServices;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade.ComponentInterfaces;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001FF RID: 511
	public class MultiplayerBattleMoraleModel : BattleMoraleModel
	{
		// Token: 0x06001C58 RID: 7256 RVA: 0x00062F3D File Offset: 0x0006113D
		[return: TupleElementNames(new string[]
		{
			"affectedSideMaxMoraleLoss",
			"affectorSideMaxMoraleGain"
		})]
		public override ValueTuple<float, float> CalculateMaxMoraleChangeDueToAgentIncapacitated(Agent affectedAgent, AgentState affectedAgentState, Agent affectorAgent, in KillingBlow killingBlow)
		{
			return new ValueTuple<float, float>(0f, 0f);
		}

		// Token: 0x06001C59 RID: 7257 RVA: 0x00062F4E File Offset: 0x0006114E
		[return: TupleElementNames(new string[]
		{
			"affectedSideMaxMoraleLoss",
			"affectorSideMaxMoraleGain"
		})]
		public override ValueTuple<float, float> CalculateMaxMoraleChangeDueToAgentPanicked(Agent agent)
		{
			return new ValueTuple<float, float>(0f, 0f);
		}

		// Token: 0x06001C5A RID: 7258 RVA: 0x00062F5F File Offset: 0x0006115F
		public override float CalculateMoraleChangeToCharacter(Agent agent, float maxMoraleChange)
		{
			return 0f;
		}

		// Token: 0x06001C5B RID: 7259 RVA: 0x00062F66 File Offset: 0x00061166
		public override float GetEffectiveInitialMorale(Agent agent, float baseMorale)
		{
			return baseMorale;
		}

		// Token: 0x06001C5C RID: 7260 RVA: 0x00062F69 File Offset: 0x00061169
		public override bool CanPanicDueToMorale(Agent agent)
		{
			return true;
		}

		// Token: 0x06001C5D RID: 7261 RVA: 0x00062F6C File Offset: 0x0006116C
		public override float CalculateCasualtiesFactor(BattleSideEnum battleSide)
		{
			return 1f;
		}

		// Token: 0x06001C5E RID: 7262 RVA: 0x00062F73 File Offset: 0x00061173
		public override float GetAverageMorale(Formation formation)
		{
			return 0f;
		}
	}
}
