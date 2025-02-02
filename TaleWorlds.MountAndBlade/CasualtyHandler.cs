using System;
using System.Collections.Generic;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000270 RID: 624
	public class CasualtyHandler : MissionLogic
	{
		// Token: 0x060020EF RID: 8431 RVA: 0x00076545 File Offset: 0x00074745
		public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow killingBlow)
		{
			this.RegisterCasualty(affectedAgent);
		}

		// Token: 0x060020F0 RID: 8432 RVA: 0x0007654E File Offset: 0x0007474E
		public override void OnAgentFleeing(Agent affectedAgent)
		{
			this.RegisterCasualty(affectedAgent);
		}

		// Token: 0x060020F1 RID: 8433 RVA: 0x00076558 File Offset: 0x00074758
		public int GetCasualtyCountOfFormation(Formation formation)
		{
			int result;
			if (!this._casualtyCounts.TryGetValue(formation, out result))
			{
				result = 0;
				this._casualtyCounts[formation] = 0;
			}
			return result;
		}

		// Token: 0x060020F2 RID: 8434 RVA: 0x00076588 File Offset: 0x00074788
		public float GetCasualtyPowerLossOfFormation(Formation formation)
		{
			float result;
			if (!this._powerLoss.TryGetValue(formation, out result))
			{
				result = 0f;
				this._powerLoss[formation] = 0f;
			}
			return result;
		}

		// Token: 0x060020F3 RID: 8435 RVA: 0x000765C0 File Offset: 0x000747C0
		private void RegisterCasualty(Agent agent)
		{
			Formation formation = agent.Formation;
			if (formation != null)
			{
				if (this._casualtyCounts.ContainsKey(formation))
				{
					Dictionary<Formation, int> casualtyCounts = this._casualtyCounts;
					Formation key = formation;
					int num = casualtyCounts[key];
					casualtyCounts[key] = num + 1;
				}
				else
				{
					this._casualtyCounts[formation] = 1;
				}
				if (this._powerLoss.ContainsKey(formation))
				{
					Dictionary<Formation, float> powerLoss = this._powerLoss;
					Formation key = formation;
					powerLoss[key] += agent.Character.GetPower();
					return;
				}
				this._powerLoss[formation] = agent.Character.GetPower();
			}
		}

		// Token: 0x04000C37 RID: 3127
		private readonly Dictionary<Formation, int> _casualtyCounts = new Dictionary<Formation, int>();

		// Token: 0x04000C38 RID: 3128
		private readonly Dictionary<Formation, float> _powerLoss = new Dictionary<Formation, float>();
	}
}
