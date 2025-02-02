using System;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade.ComponentInterfaces;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001F5 RID: 501
	public class DefaultAgentDecideKilledOrUnconsciousModel : AgentDecideKilledOrUnconsciousModel
	{
		// Token: 0x06001C07 RID: 7175 RVA: 0x00060FE1 File Offset: 0x0005F1E1
		public override float GetAgentStateProbability(Agent affectorAgent, Agent effectedAgent, DamageTypes damageType, WeaponFlags weaponFlags, out float useSurgeryProbability)
		{
			useSurgeryProbability = 0f;
			return 1f;
		}
	}
}
