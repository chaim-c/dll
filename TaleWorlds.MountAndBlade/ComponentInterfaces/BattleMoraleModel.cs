using System;
using System.Runtime.CompilerServices;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade.ComponentInterfaces
{
	// Token: 0x020003C8 RID: 968
	public abstract class BattleMoraleModel : GameModel
	{
		// Token: 0x06003366 RID: 13158
		[return: TupleElementNames(new string[]
		{
			"affectedSideMaxMoraleLoss",
			"affectorSideMaxMoraleGain"
		})]
		public abstract ValueTuple<float, float> CalculateMaxMoraleChangeDueToAgentIncapacitated(Agent affectedAgent, AgentState affectedAgentState, Agent affectorAgent, in KillingBlow killingBlow);

		// Token: 0x06003367 RID: 13159
		[return: TupleElementNames(new string[]
		{
			"affectedSideMaxMoraleLoss",
			"affectorSideMaxMoraleGain"
		})]
		public abstract ValueTuple<float, float> CalculateMaxMoraleChangeDueToAgentPanicked(Agent agent);

		// Token: 0x06003368 RID: 13160
		public abstract float CalculateMoraleChangeToCharacter(Agent agent, float maxMoraleChange);

		// Token: 0x06003369 RID: 13161
		public abstract float GetEffectiveInitialMorale(Agent agent, float baseMorale);

		// Token: 0x0600336A RID: 13162
		public abstract bool CanPanicDueToMorale(Agent agent);

		// Token: 0x0600336B RID: 13163
		public abstract float CalculateCasualtiesFactor(BattleSideEnum battleSide);

		// Token: 0x0600336C RID: 13164
		public abstract float GetAverageMorale(Formation formation);

		// Token: 0x04001642 RID: 5698
		public const float BaseMoraleGainOnKill = 3f;

		// Token: 0x04001643 RID: 5699
		public const float BaseMoraleLossOnKill = 4f;

		// Token: 0x04001644 RID: 5700
		public const float BaseMoraleGainOnPanic = 2f;

		// Token: 0x04001645 RID: 5701
		public const float BaseMoraleLossOnPanic = 1.1f;

		// Token: 0x04001646 RID: 5702
		public const float MeleeWeaponMoraleMultiplier = 0.75f;

		// Token: 0x04001647 RID: 5703
		public const float RangedWeaponMoraleMultiplier = 0.5f;

		// Token: 0x04001648 RID: 5704
		public const float SiegeWeaponMoraleMultiplier = 0.25f;

		// Token: 0x04001649 RID: 5705
		public const float BurningSiegeWeaponMoraleBonus = 0.25f;

		// Token: 0x0400164A RID: 5706
		public const float CasualtyFactorRate = 2f;
	}
}
