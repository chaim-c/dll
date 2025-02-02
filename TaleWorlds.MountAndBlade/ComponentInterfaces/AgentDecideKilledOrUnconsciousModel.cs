using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade.ComponentInterfaces
{
	// Token: 0x020003C5 RID: 965
	public abstract class AgentDecideKilledOrUnconsciousModel : GameModel
	{
		// Token: 0x0600335E RID: 13150
		public abstract float GetAgentStateProbability(Agent affectorAgent, Agent effectedAgent, DamageTypes damageType, WeaponFlags weaponFlags, out float useSurgeryProbability);
	}
}
