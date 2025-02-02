using System;
using System.Collections.Generic;
using SandBox.Missions.Handlers;
using SandBox.Missions.MissionLogics;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Source.Missions;

namespace SandBox.Missions.AgentControllers
{
	// Token: 0x02000075 RID: 117
	public class AmbushMissionController : BaseBattleMissionController
	{
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x0001E8BC File Offset: 0x0001CABC
		// (set) Token: 0x06000476 RID: 1142 RVA: 0x0001E8C4 File Offset: 0x0001CAC4
		public bool IsPlayerAmbusher
		{
			get
			{
				return base.IsPlayerAttacker;
			}
			private set
			{
			}
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0001E8C6 File Offset: 0x0001CAC6
		public AmbushMissionController(bool isPlayerAttacker) : base(isPlayerAttacker)
		{
			this._checkPoints = new List<GameEntity>();
			this._defenderSpawnPoints = new List<GameEntity>();
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0001E8EC File Offset: 0x0001CAEC
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			this._ambushDeploymentHandler = base.Mission.GetMissionBehavior<AmbushBattleDeploymentHandler>();
			this._ambushIntroLogic = base.Mission.GetMissionBehavior<AmbushIntroLogic>();
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0001E918 File Offset: 0x0001CB18
		public override void AfterStart()
		{
			base.AfterStart();
			base.Mission.SetMissionMode(MissionMode.Stealth, true);
			int num = 0;
			GameEntity gameEntity;
			do
			{
				gameEntity = Mission.Current.Scene.FindEntityWithTag("checkpoint_" + num.ToString());
				if (gameEntity != null)
				{
					this._checkPoints.Add(gameEntity);
					num++;
				}
			}
			while (gameEntity != null);
			num = 0;
			do
			{
				gameEntity = Mission.Current.Scene.FindEntityWithTag("spawnpoint_defender_" + num.ToString());
				if (gameEntity != null)
				{
					this._defenderSpawnPoints.Add(gameEntity);
					num++;
				}
			}
			while (gameEntity != null);
			if (base.Mission.PlayerTeam.Side == BattleSideEnum.Attacker)
			{
				this.SetupTeam(base.Mission.AttackerTeam);
			}
			else
			{
				this.SetupTeam(base.Mission.AttackerTeam);
				this.SetupTeam(base.Mission.DefenderTeam);
			}
			this._playerSoloTeam = base.Mission.Teams.Add(base.Mission.PlayerTeam.Side, uint.MaxValue, uint.MaxValue, null, true, false, false);
			base.Mission.AttackerTeam.SetIsEnemyOf(base.Mission.DefenderTeam, false);
			base.Mission.DefenderTeam.SetIsEnemyOf(base.Mission.AttackerTeam, false);
			base.Mission.AttackerTeam.SetIsEnemyOf(this._playerSoloTeam, false);
			base.Mission.DefenderTeam.SetIsEnemyOf(this._playerSoloTeam, true);
			base.Mission.AttackerTeam.ExpireAIQuerySystem();
			base.Mission.DefenderTeam.ExpireAIQuerySystem();
			Agent.Main.Controller = Agent.ControllerType.AI;
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0001EACC File Offset: 0x0001CCCC
		public override void OnMissionTick(float dt)
		{
			if (this._firstTick)
			{
				this._firstTick = false;
				if (!this.IsPlayerAmbusher)
				{
					base.Mission.AddMissionBehavior(new MissionBoundaryCrossingHandler());
					this._ambushIntroLogic.StartIntro();
				}
			}
			base.OnMissionTick(dt);
			this.UpdateAgents();
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0001EB18 File Offset: 0x0001CD18
		protected override void CreateDefenderTroops()
		{
			this.CreateTroop("guard", base.Mission.DefenderTeam, 30, false);
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0001EB33 File Offset: 0x0001CD33
		protected override void CreateAttackerTroops()
		{
			this.CreateTroop("guard", base.Mission.AttackerTeam, 10, false);
			this.CreateTroop("archer", base.Mission.AttackerTeam, 15, false);
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0001EB68 File Offset: 0x0001CD68
		protected void CreateTroop(string troopName, Team troopTeam, int troopCount, bool isReinforcement = false)
		{
			if (troopTeam.Side == BattleSideEnum.Attacker)
			{
				CharacterObject @object = Game.Current.ObjectManager.GetObject<CharacterObject>(troopName);
				FormationClass defaultFormationClass = @object.DefaultFormationClass;
				Formation formation = troopTeam.GetFormation(defaultFormationClass);
				WorldPosition value;
				Vec2 value2;
				base.Mission.GetFormationSpawnFrame(troopTeam.Side, defaultFormationClass, isReinforcement, out value, out value2);
				formation.SetPositioning(new WorldPosition?(value), new Vec2?(value2), null);
				for (int i = 0; i < troopCount; i++)
				{
					(base.Mission.SpawnAgent(new AgentBuildData(@object).Team(troopTeam).Formation(formation).FormationTroopSpawnCount(troopCount).FormationTroopSpawnIndex(i), false).AddController(typeof(AmbushBattleAgentController)) as AmbushBattleAgentController).IsAttacker = true;
					base.IncrementDeploymedTroops(BattleSideEnum.Attacker);
				}
				return;
			}
			CharacterObject object2 = this.game.ObjectManager.GetObject<CharacterObject>(troopName);
			for (int j = 0; j < troopCount; j++)
			{
				int count = this._defenderSpawnPoints.Count;
				this._columns = MathF.Ceiling((float)troopCount / (float)count);
				int num = base.DeployedDefenderTroopCount - base.DeployedDefenderTroopCount / this._columns * this._columns;
				MatrixFrame globalFrame = this._defenderSpawnPoints[base.DeployedDefenderTroopCount / this._columns].GetGlobalFrame();
				globalFrame.origin = globalFrame.TransformToParent(new Vec3(1f, 0f, 0f, -1f) * (float)num * 1f);
				Mission mission = base.Mission;
				AgentBuildData agentBuildData = new AgentBuildData(object2).Team(troopTeam).InitialPosition(globalFrame.origin);
				Vec2 vec = globalFrame.rotation.f.AsVec2;
				vec = vec.Normalized();
				(mission.SpawnAgent(agentBuildData.InitialDirection(vec), false).AddController(typeof(AmbushBattleAgentController)) as AmbushBattleAgentController).IsAttacker = false;
				base.IncrementDeploymedTroops(BattleSideEnum.Defender);
			}
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0001ED58 File Offset: 0x0001CF58
		public void OnPlayerDeploymentFinish(bool doDebugPause = false)
		{
			if (base.Mission.PlayerTeam.Side == BattleSideEnum.Attacker)
			{
				this.SetupTeam(base.Mission.DefenderTeam);
			}
			base.Mission.RemoveMissionBehavior(this._ambushDeploymentHandler);
			base.Mission.AddMissionBehavior(new MissionBoundaryCrossingHandler());
			this._ambushIntroLogic.StartIntro();
			if (this.PlayerDeploymentFinish != null)
			{
				this.PlayerDeploymentFinish();
			}
			Agent.Main.SetTeam(this._playerSoloTeam, true);
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0001EDD9 File Offset: 0x0001CFD9
		public void OnIntroductionFinish()
		{
			if (!base.IsPlayerAttacker)
			{
				this.StartFighting();
			}
			if (this.IntroFinish != null)
			{
				this.IntroFinish();
			}
			Agent.Main.Controller = Agent.ControllerType.Player;
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0001EE08 File Offset: 0x0001D008
		private void UpdateAgents()
		{
			int num = 0;
			int num2 = 0;
			foreach (Agent agent in base.Mission.Agents)
			{
				if (base.Mission.Mode == MissionMode.Stealth && agent.IsAIControlled && agent.CurrentWatchState == Agent.WatchState.Cautious && agent.IsHuman)
				{
					this.StartFighting();
				}
				if (!this.IsPlayerAmbusher && Agent.Main.IsAIControlled)
				{
					Vec2 movementDirection = Agent.Main.GetMovementDirection();
					WorldPosition worldPosition = Agent.Main.GetWorldPosition();
					worldPosition.SetVec2(worldPosition.AsVec2 + movementDirection * 5f);
					Agent.Main.DisableScriptedMovement();
					Agent.Main.SetScriptedPosition(ref worldPosition, false, Agent.AIScriptedFrameFlags.DoNotRun);
				}
				AmbushBattleAgentController controller = agent.GetController<AmbushBattleAgentController>();
				if (controller != null)
				{
					controller.UpdateState();
					if (!controller.IsAttacker && !controller.Aggressive)
					{
						if (num == 0)
						{
							if (controller.CheckArrivedAtWayPoint(this._checkPoints[this._currentPositionIndex]))
							{
								this._currentPositionIndex++;
								if (this._currentPositionIndex >= this._checkPoints.Count)
								{
									MBDebug.ShowWarning("The enemy has gotten away.");
								}
								else
								{
									WorldPosition worldPosition2 = new WorldPosition(Mission.Current.Scene, UIntPtr.Zero, this._checkPoints[this._currentPositionIndex].GlobalPosition, false);
									agent.SetScriptedPosition(ref worldPosition2, false, Agent.AIScriptedFrameFlags.DoNotRun);
								}
							}
						}
						else
						{
							WorldPosition worldPosition3;
							Vec2 v;
							if (num % this._columns != 0)
							{
								Agent agent2 = base.Mission.Agents[num2 - 1];
								worldPosition3 = agent2.GetWorldPosition();
								v = agent2.GetMovementDirection();
								v.RotateCCW(-1.5707964f);
							}
							else
							{
								Agent agent3 = base.Mission.Agents[num2 - this._columns];
								worldPosition3 = agent3.GetWorldPosition();
								v = agent.Position.AsVec2 - agent3.Position.AsVec2;
								v.Normalize();
							}
							worldPosition3.SetVec2(worldPosition3.AsVec2 + v * 1f);
							agent.DisableScriptedMovement();
							agent.SetScriptedPosition(ref worldPosition3, false, Agent.AIScriptedFrameFlags.DoNotRun);
						}
						num++;
					}
				}
				num2++;
			}
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0001F084 File Offset: 0x0001D284
		private void StartFighting()
		{
			base.Mission.AttackerTeam.SetIsEnemyOf(base.Mission.DefenderTeam, true);
			base.Mission.DefenderTeam.SetIsEnemyOf(base.Mission.AttackerTeam, true);
			if (base.Mission.PlayerAllyTeam != null)
			{
				base.Mission.PlayerAllyTeam.SetIsEnemyOf(base.Mission.PlayerEnemyTeam, true);
				base.Mission.PlayerEnemyTeam.SetIsEnemyOf(base.Mission.PlayerAllyTeam, true);
			}
			base.Mission.SetMissionMode(MissionMode.Battle, false);
			foreach (Agent agent in base.Mission.Agents)
			{
				AmbushBattleAgentController controller = agent.GetController<AmbushBattleAgentController>();
				if (controller != null)
				{
					controller.Aggressive = true;
					if (!controller.IsAttacker)
					{
						agent.DisableScriptedMovement();
						FormationClass agentTroopClass = base.Mission.GetAgentTroopClass(BattleSideEnum.Defender, agent.Character);
						agent.Formation = base.Mission.DefenderTeam.GetFormation(agentTroopClass);
						agent.Formation.SetMovementOrder(MovementOrder.MovementOrderCharge);
					}
				}
				if (agent.IsPlayerControlled)
				{
					agent.SetTeam(base.Mission.PlayerTeam, true);
				}
			}
			base.Mission.DefenderTeam.MasterOrderController.SelectAllFormations(false);
			base.Mission.DefenderTeam.MasterOrderController.SetOrder(OrderType.StandYourGround);
			base.Mission.DefenderTeam.MasterOrderController.SetOrder(OrderType.Charge);
			foreach (Formation formation in base.Mission.DefenderTeam.FormationsIncludingSpecialAndEmpty)
			{
				if (formation.CountOfUnits > 0)
				{
					base.Mission.DefenderTeam.MasterOrderController.DeselectFormation(formation);
				}
			}
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000482 RID: 1154 RVA: 0x0001F280 File Offset: 0x0001D480
		// (remove) Token: 0x06000483 RID: 1155 RVA: 0x0001F2B8 File Offset: 0x0001D4B8
		public event AmbushMissionEventDelegate PlayerDeploymentFinish;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000484 RID: 1156 RVA: 0x0001F2F0 File Offset: 0x0001D4F0
		// (remove) Token: 0x06000485 RID: 1157 RVA: 0x0001F328 File Offset: 0x0001D528
		public event AmbushMissionEventDelegate IntroFinish;

		// Token: 0x04000214 RID: 532
		private AmbushBattleDeploymentHandler _ambushDeploymentHandler;

		// Token: 0x04000215 RID: 533
		private AmbushIntroLogic _ambushIntroLogic;

		// Token: 0x04000216 RID: 534
		private readonly List<GameEntity> _checkPoints;

		// Token: 0x04000217 RID: 535
		private readonly List<GameEntity> _defenderSpawnPoints;

		// Token: 0x04000218 RID: 536
		private int _currentPositionIndex;

		// Token: 0x04000219 RID: 537
		private int _columns;

		// Token: 0x0400021A RID: 538
		private const float UnitSpread = 1f;

		// Token: 0x0400021B RID: 539
		private Team _playerSoloTeam;

		// Token: 0x0400021C RID: 540
		private bool _firstTick = true;
	}
}
