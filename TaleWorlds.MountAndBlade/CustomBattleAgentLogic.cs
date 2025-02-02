using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000271 RID: 625
	public class CustomBattleAgentLogic : MissionLogic
	{
		// Token: 0x060020F5 RID: 8437 RVA: 0x00076674 File Offset: 0x00074874
		public override void OnAgentHit(Agent affectedAgent, Agent affectorAgent, in MissionWeapon affectorWeapon, in Blow blow, in AttackCollisionData attackCollisionData)
		{
			if (affectedAgent.Character != null && ((affectorAgent != null) ? affectorAgent.Character : null) != null && affectedAgent.State == AgentState.Active)
			{
				bool flag = affectedAgent.Health - (float)blow.InflictedDamage < 1f;
				bool flag2 = affectedAgent.Team.Side == affectorAgent.Team.Side;
				IAgentOriginBase origin = affectorAgent.Origin;
				BasicCharacterObject character = affectedAgent.Character;
				Formation formation = affectorAgent.Formation;
				BasicCharacterObject formationCaptain;
				if (formation == null)
				{
					formationCaptain = null;
				}
				else
				{
					Agent captain = formation.Captain;
					formationCaptain = ((captain != null) ? captain.Character : null);
				}
				int inflictedDamage = blow.InflictedDamage;
				bool isFatal = flag;
				bool isTeamKill = flag2;
				MissionWeapon missionWeapon = affectorWeapon;
				origin.OnScoreHit(character, formationCaptain, inflictedDamage, isFatal, isTeamKill, missionWeapon.CurrentUsageItem);
			}
		}

		// Token: 0x060020F6 RID: 8438 RVA: 0x0007671C File Offset: 0x0007491C
		public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow killingBlow)
		{
			if (affectorAgent == null && affectedAgent.IsMount && agentState == AgentState.Routed)
			{
				return;
			}
			if (affectedAgent.Origin != null)
			{
				if (agentState == AgentState.Unconscious)
				{
					affectedAgent.Origin.SetWounded();
					if (affectedAgent == base.Mission.MainAgent)
					{
						this.BecomeGhost();
						return;
					}
				}
				else
				{
					if (agentState == AgentState.Killed)
					{
						affectedAgent.Origin.SetKilled();
						return;
					}
					affectedAgent.Origin.SetRouted();
				}
			}
		}

		// Token: 0x060020F7 RID: 8439 RVA: 0x00076780 File Offset: 0x00074980
		private void BecomeGhost()
		{
			Agent leader = base.Mission.PlayerEnemyTeam.Leader;
			if (leader != null)
			{
				leader.Controller = Agent.ControllerType.AI;
			}
			base.Mission.MainAgent.Controller = Agent.ControllerType.AI;
		}
	}
}
