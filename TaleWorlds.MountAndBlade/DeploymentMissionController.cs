using System;
using System.Diagnostics;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Missions.Handlers;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000273 RID: 627
	public class DeploymentMissionController : MissionLogic
	{
		// Token: 0x06002106 RID: 8454 RVA: 0x00076C28 File Offset: 0x00074E28
		public DeploymentMissionController(bool isPlayerAttacker)
		{
			this._isPlayerAttacker = isPlayerAttacker;
		}

		// Token: 0x06002107 RID: 8455 RVA: 0x00076C37 File Offset: 0x00074E37
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			this._battleDeploymentHandler = base.Mission.GetMissionBehavior<BattleDeploymentHandler>();
			this.MissionAgentSpawnLogic = base.Mission.GetMissionBehavior<MissionAgentSpawnLogic>();
		}

		// Token: 0x06002108 RID: 8456 RVA: 0x00076C64 File Offset: 0x00074E64
		public override void AfterStart()
		{
			base.AfterStart();
			base.Mission.AllowAiTicking = false;
			for (int i = 0; i < 2; i++)
			{
				this.MissionAgentSpawnLogic.SetSpawnTroops((BattleSideEnum)i, false, false);
			}
			this.MissionAgentSpawnLogic.SetReinforcementsSpawnEnabled(false, true);
		}

		// Token: 0x06002109 RID: 8457 RVA: 0x00076CAC File Offset: 0x00074EAC
		private void SetupTeams()
		{
			Utilities.SetLoadingScreenPercentage(0.92f);
			base.Mission.DisableDying = true;
			BattleSideEnum battleSideEnum = this._isPlayerAttacker ? BattleSideEnum.Defender : BattleSideEnum.Attacker;
			BattleSideEnum side = this._isPlayerAttacker ? BattleSideEnum.Attacker : BattleSideEnum.Defender;
			this.SetupTeamsOfSide(battleSideEnum);
			this.OnSideDeploymentFinished(battleSideEnum);
			if (this._isPlayerAttacker)
			{
				foreach (Agent agent in base.Mission.Agents)
				{
					if (agent.IsHuman && agent.Team != null && agent.Team.Side == battleSideEnum)
					{
						agent.SetRenderCheckEnabled(false);
						agent.AgentVisuals.SetVisible(false);
						Agent mountAgent = agent.MountAgent;
						if (mountAgent != null)
						{
							mountAgent.SetRenderCheckEnabled(false);
						}
						Agent mountAgent2 = agent.MountAgent;
						if (mountAgent2 != null)
						{
							mountAgent2.AgentVisuals.SetVisible(false);
						}
					}
				}
			}
			this.SetupTeamsOfSide(side);
			base.Mission.IsTeleportingAgents = true;
			Utilities.SetLoadingScreenPercentage(0.96f);
			if (!MissionGameModels.Current.BattleInitializationModel.CanPlayerSideDeployWithOrderOfBattle())
			{
				this.FinishDeployment();
			}
		}

		// Token: 0x0600210A RID: 8458 RVA: 0x00076DD4 File Offset: 0x00074FD4
		public override void OnAgentControllerSetToPlayer(Agent agent)
		{
			if (!this._isPlayerControllerSetToAI)
			{
				agent.Controller = Agent.ControllerType.AI;
				agent.SetIsAIPaused(true);
				agent.SetDetachableFromFormation(false);
				this._isPlayerControllerSetToAI = true;
			}
		}

		// Token: 0x0600210B RID: 8459 RVA: 0x00076DFA File Offset: 0x00074FFA
		public override void OnMissionTick(float dt)
		{
			base.OnMissionTick(dt);
			if (!this.TeamSetupOver && base.Mission.Scene != null)
			{
				this.SetupTeams();
				this.TeamSetupOver = true;
			}
		}

		// Token: 0x0600210C RID: 8460 RVA: 0x00076E2B File Offset: 0x0007502B
		[Conditional("DEBUG")]
		private void DebugTick()
		{
			if (Input.DebugInput.IsHotKeyPressed("SwapToEnemy"))
			{
				base.Mission.MainAgent.Controller = Agent.ControllerType.AI;
				base.Mission.PlayerEnemyTeam.Leader.Controller = Agent.ControllerType.Player;
				this.SwapTeams();
			}
		}

		// Token: 0x0600210D RID: 8461 RVA: 0x00076E6B File Offset: 0x0007506B
		private void SwapTeams()
		{
			base.Mission.PlayerTeam = base.Mission.PlayerEnemyTeam;
		}

		// Token: 0x0600210E RID: 8462 RVA: 0x00076E84 File Offset: 0x00075084
		protected void SetupTeamsOfSideAux(BattleSideEnum side)
		{
			Team team = (side == BattleSideEnum.Attacker) ? base.Mission.AttackerTeam : base.Mission.DefenderTeam;
			foreach (Formation formation in team.FormationsIncludingSpecialAndEmpty)
			{
				if (formation.CountOfUnits > 0)
				{
					formation.ApplyActionOnEachUnit(delegate(Agent agent)
					{
						if (agent.IsAIControlled)
						{
							agent.AIStateFlags &= ~Agent.AIStateFlag.Alarmed;
							agent.SetIsAIPaused(true);
						}
					}, null);
				}
			}
			Team team2 = (side == BattleSideEnum.Attacker) ? base.Mission.AttackerAllyTeam : base.Mission.DefenderAllyTeam;
			if (team2 != null)
			{
				foreach (Formation formation2 in team2.FormationsIncludingSpecialAndEmpty)
				{
					if (formation2.CountOfUnits > 0)
					{
						formation2.ApplyActionOnEachUnit(delegate(Agent agent)
						{
							if (agent.IsAIControlled)
							{
								agent.AIStateFlags &= ~Agent.AIStateFlag.Alarmed;
								agent.SetIsAIPaused(true);
							}
						}, null);
					}
				}
			}
			this.MissionAgentSpawnLogic.OnBattleSideDeployed(team.Side);
		}

		// Token: 0x0600210F RID: 8463 RVA: 0x00076FBC File Offset: 0x000751BC
		protected virtual void SetupTeamsOfSide(BattleSideEnum side)
		{
			this.MissionAgentSpawnLogic.SetSpawnTroops(side, true, true);
			this.SetupTeamsOfSideAux(side);
		}

		// Token: 0x06002110 RID: 8464 RVA: 0x00076FD4 File Offset: 0x000751D4
		protected void OnSideDeploymentFinished(BattleSideEnum side)
		{
			Team team = (side == BattleSideEnum.Attacker) ? base.Mission.AttackerTeam : base.Mission.DefenderTeam;
			if (side != base.Mission.PlayerTeam.Side)
			{
				base.Mission.IsTeleportingAgents = true;
				this.DeployFormationsOfTeam(team);
				Team team2 = (side == BattleSideEnum.Attacker) ? base.Mission.AttackerAllyTeam : base.Mission.DefenderAllyTeam;
				if (team2 != null)
				{
					this.DeployFormationsOfTeam(team2);
				}
				base.Mission.IsTeleportingAgents = false;
			}
		}

		// Token: 0x06002111 RID: 8465 RVA: 0x00077058 File Offset: 0x00075258
		protected void DeployFormationsOfTeam(Team team)
		{
			foreach (Formation formation in team.FormationsIncludingEmpty)
			{
				if (formation.CountOfUnits > 0)
				{
					formation.SetControlledByAI(true, false);
				}
			}
			team.QuerySystem.Expire();
			base.Mission.AllowAiTicking = true;
			base.Mission.ForceTickOccasionally = true;
			team.ResetTactic();
			bool isTeleportingAgents = Mission.Current.IsTeleportingAgents;
			base.Mission.IsTeleportingAgents = true;
			team.Tick(0f);
			base.Mission.IsTeleportingAgents = isTeleportingAgents;
			base.Mission.AllowAiTicking = false;
			base.Mission.ForceTickOccasionally = false;
		}

		// Token: 0x06002112 RID: 8466 RVA: 0x00077124 File Offset: 0x00075324
		public void FinishDeployment()
		{
			this.OnBeforeDeploymentFinished();
			if (this._isPlayerAttacker)
			{
				foreach (Agent agent in base.Mission.Agents)
				{
					if (agent.IsHuman && agent.Team != null && agent.Team.Side == BattleSideEnum.Defender)
					{
						agent.SetRenderCheckEnabled(true);
						agent.AgentVisuals.SetVisible(true);
						Agent mountAgent = agent.MountAgent;
						if (mountAgent != null)
						{
							mountAgent.SetRenderCheckEnabled(true);
						}
						Agent mountAgent2 = agent.MountAgent;
						if (mountAgent2 != null)
						{
							mountAgent2.AgentVisuals.SetVisible(true);
						}
					}
				}
			}
			base.Mission.IsTeleportingAgents = false;
			Mission.Current.OnDeploymentFinished();
			foreach (Agent agent2 in base.Mission.Agents)
			{
				if (agent2.IsAIControlled)
				{
					agent2.AIStateFlags |= Agent.AIStateFlag.Alarmed;
					agent2.SetIsAIPaused(false);
					if (agent2.GetAgentFlags().HasAnyFlag(AgentFlag.CanWieldWeapon))
					{
						agent2.ResetEnemyCaches();
					}
					HumanAIComponent humanAIComponent = agent2.HumanAIComponent;
					if (humanAIComponent != null)
					{
						humanAIComponent.SyncBehaviorParamsIfNecessary();
					}
				}
			}
			Agent mainAgent = base.Mission.MainAgent;
			if (mainAgent != null)
			{
				mainAgent.SetDetachableFromFormation(true);
				mainAgent.Controller = Agent.ControllerType.Player;
			}
			base.Mission.AllowAiTicking = true;
			base.Mission.DisableDying = false;
			this.MissionAgentSpawnLogic.SetReinforcementsSpawnEnabled(true, true);
			this.OnAfterDeploymentFinished();
			base.Mission.RemoveMissionBehavior(this);
		}

		// Token: 0x06002113 RID: 8467 RVA: 0x000772D0 File Offset: 0x000754D0
		public virtual void OnBeforeDeploymentFinished()
		{
			this.OnSideDeploymentFinished(base.Mission.PlayerTeam.Side);
		}

		// Token: 0x06002114 RID: 8468 RVA: 0x000772E8 File Offset: 0x000754E8
		public virtual void OnAfterDeploymentFinished()
		{
			base.Mission.RemoveMissionBehavior(this._battleDeploymentHandler);
		}

		// Token: 0x04000C3C RID: 3132
		private BattleDeploymentHandler _battleDeploymentHandler;

		// Token: 0x04000C3D RID: 3133
		protected MissionAgentSpawnLogic MissionAgentSpawnLogic;

		// Token: 0x04000C3E RID: 3134
		private readonly bool _isPlayerAttacker;

		// Token: 0x04000C3F RID: 3135
		protected bool TeamSetupOver;

		// Token: 0x04000C40 RID: 3136
		private bool _isPlayerControllerSetToAI;
	}
}
