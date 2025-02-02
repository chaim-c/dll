using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200026D RID: 621
	public class BattleObserverMissionLogic : MissionLogic
	{
		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x060020D9 RID: 8409 RVA: 0x00075E8B File Offset: 0x0007408B
		// (set) Token: 0x060020DA RID: 8410 RVA: 0x00075E93 File Offset: 0x00074093
		public IBattleObserver BattleObserver { get; private set; }

		// Token: 0x060020DB RID: 8411 RVA: 0x00075E9C File Offset: 0x0007409C
		public void SetObserver(IBattleObserver observer)
		{
			this.BattleObserver = observer;
		}

		// Token: 0x060020DC RID: 8412 RVA: 0x00075EA5 File Offset: 0x000740A5
		public override void AfterStart()
		{
			this._builtAgentCountForSides = new int[2];
			this._removedAgentCountForSides = new int[2];
		}

		// Token: 0x060020DD RID: 8413 RVA: 0x00075EC0 File Offset: 0x000740C0
		public override void OnAgentBuild(Agent agent, Banner banner)
		{
			if (agent.IsHuman)
			{
				BattleSideEnum side = agent.Team.Side;
				this.BattleObserver.TroopNumberChanged(side, agent.Origin.BattleCombatant, agent.Character, 1, 0, 0, 0, 0, 0);
				this._builtAgentCountForSides[(int)side]++;
			}
		}

		// Token: 0x060020DE RID: 8414 RVA: 0x00075F18 File Offset: 0x00074118
		public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow blow)
		{
			if (affectedAgent.IsHuman)
			{
				BattleSideEnum side = affectedAgent.Team.Side;
				switch (agentState)
				{
				case AgentState.Routed:
					this.BattleObserver.TroopNumberChanged(side, affectedAgent.Origin.BattleCombatant, affectedAgent.Character, -1, 0, 0, 1, 0, 0);
					break;
				case AgentState.Unconscious:
					this.BattleObserver.TroopNumberChanged(side, affectedAgent.Origin.BattleCombatant, affectedAgent.Character, -1, 0, 1, 0, 0, 0);
					break;
				case AgentState.Killed:
					this.BattleObserver.TroopNumberChanged(side, affectedAgent.Origin.BattleCombatant, affectedAgent.Character, -1, 1, 0, 0, 0, 0);
					break;
				default:
					throw new ArgumentOutOfRangeException("agentState", agentState, null);
				}
				this._removedAgentCountForSides[(int)side]++;
				if (affectorAgent != null && affectorAgent.IsHuman && (agentState == AgentState.Unconscious || agentState == AgentState.Killed))
				{
					this.BattleObserver.TroopNumberChanged(affectorAgent.Team.Side, affectorAgent.Origin.BattleCombatant, affectorAgent.Character, 0, 0, 0, 0, 1, 0);
				}
			}
		}

		// Token: 0x060020DF RID: 8415 RVA: 0x00076024 File Offset: 0x00074224
		public override void OnMissionResultReady(MissionResult missionResult)
		{
			if (missionResult.PlayerVictory)
			{
				this.BattleObserver.BattleResultsReady();
			}
		}

		// Token: 0x060020E0 RID: 8416 RVA: 0x00076039 File Offset: 0x00074239
		public float GetDeathToBuiltAgentRatioForSide(BattleSideEnum side)
		{
			return (float)this._removedAgentCountForSides[(int)side] / (float)this._builtAgentCountForSides[(int)side];
		}

		// Token: 0x04000C30 RID: 3120
		private int[] _builtAgentCountForSides;

		// Token: 0x04000C31 RID: 3121
		private int[] _removedAgentCountForSides;
	}
}
