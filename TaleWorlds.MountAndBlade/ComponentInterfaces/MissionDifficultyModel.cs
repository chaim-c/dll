using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade.ComponentInterfaces
{
	// Token: 0x020003C6 RID: 966
	public abstract class MissionDifficultyModel : GameModel
	{
		// Token: 0x06003360 RID: 13152
		public abstract float GetDamageMultiplierOfCombatDifficulty(Agent victimAgent, Agent attackerAgent = null);
	}
}
