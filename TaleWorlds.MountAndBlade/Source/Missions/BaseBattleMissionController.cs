using System;
using System.Diagnostics;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.Source.Missions
{
	// Token: 0x020003AD RID: 941
	public abstract class BaseBattleMissionController : MissionLogic
	{
		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x0600329F RID: 12959 RVA: 0x000D1FFF File Offset: 0x000D01FF
		// (set) Token: 0x060032A0 RID: 12960 RVA: 0x000D2007 File Offset: 0x000D0207
		private protected bool IsPlayerAttacker { protected get; private set; }

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x060032A1 RID: 12961 RVA: 0x000D2010 File Offset: 0x000D0210
		// (set) Token: 0x060032A2 RID: 12962 RVA: 0x000D2018 File Offset: 0x000D0218
		private protected int DeployedAttackerTroopCount { protected get; private set; }

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x060032A3 RID: 12963 RVA: 0x000D2021 File Offset: 0x000D0221
		// (set) Token: 0x060032A4 RID: 12964 RVA: 0x000D2029 File Offset: 0x000D0229
		private protected int DeployedDefenderTroopCount { protected get; private set; }

		// Token: 0x060032A5 RID: 12965 RVA: 0x000D2032 File Offset: 0x000D0232
		protected BaseBattleMissionController(bool isPlayerAttacker)
		{
			this.IsPlayerAttacker = isPlayerAttacker;
			this.game = Game.Current;
		}

		// Token: 0x060032A6 RID: 12966 RVA: 0x000D204C File Offset: 0x000D024C
		public override void EarlyStart()
		{
			this.EarlyStart();
		}

		// Token: 0x060032A7 RID: 12967 RVA: 0x000D2054 File Offset: 0x000D0254
		public override void AfterStart()
		{
			base.AfterStart();
			this.CreateTeams();
			base.Mission.SetMissionMode(MissionMode.Battle, true);
		}

		// Token: 0x060032A8 RID: 12968 RVA: 0x000D206F File Offset: 0x000D026F
		protected virtual void SetupTeam(Team team)
		{
			if (team.Side == BattleSideEnum.Attacker)
			{
				this.CreateAttackerTroops();
			}
			else
			{
				this.CreateDefenderTroops();
			}
			if (team == base.Mission.PlayerTeam)
			{
				this.CreatePlayer();
			}
		}

		// Token: 0x060032A9 RID: 12969
		protected abstract void CreateDefenderTroops();

		// Token: 0x060032AA RID: 12970
		protected abstract void CreateAttackerTroops();

		// Token: 0x060032AB RID: 12971 RVA: 0x000D209C File Offset: 0x000D029C
		public virtual TeamAIComponent GetTeamAI(Team team, float thinkTimerTime = 5f, float applyTimerTime = 1f)
		{
			return new TeamAIGeneral(base.Mission, team, thinkTimerTime, applyTimerTime);
		}

		// Token: 0x060032AC RID: 12972 RVA: 0x000D20AC File Offset: 0x000D02AC
		public override void OnMissionTick(float dt)
		{
			base.OnMissionTick(dt);
		}

		// Token: 0x060032AD RID: 12973 RVA: 0x000D20B5 File Offset: 0x000D02B5
		[Conditional("DEBUG")]
		private void DebugTick()
		{
			if (Input.DebugInput.IsHotKeyPressed("SwapToEnemy"))
			{
				this.BecomeEnemy();
			}
			if (Input.DebugInput.IsHotKeyDown("BaseBattleMissionControllerHotkeyBecomePlayer"))
			{
				this.BecomePlayer();
			}
		}

		// Token: 0x060032AE RID: 12974 RVA: 0x000D20E5 File Offset: 0x000D02E5
		protected bool IsPlayerDead()
		{
			return base.Mission.MainAgent == null || !base.Mission.MainAgent.IsActive();
		}

		// Token: 0x060032AF RID: 12975 RVA: 0x000D210C File Offset: 0x000D030C
		public override bool MissionEnded(ref MissionResult missionResult)
		{
			if (!this.IsDeploymentFinished)
			{
				return false;
			}
			if (this.IsPlayerDead())
			{
				missionResult = MissionResult.CreateDefeated(base.Mission);
				return true;
			}
			if (base.Mission.GetMemberCountOfSide(BattleSideEnum.Attacker) == 0)
			{
				missionResult = ((base.Mission.PlayerTeam.Side == BattleSideEnum.Attacker) ? MissionResult.CreateDefeated(base.Mission) : MissionResult.CreateSuccessful(base.Mission, false));
				return true;
			}
			if (base.Mission.GetMemberCountOfSide(BattleSideEnum.Defender) == 0)
			{
				missionResult = ((base.Mission.PlayerTeam.Side == BattleSideEnum.Attacker) ? MissionResult.CreateSuccessful(base.Mission, false) : MissionResult.CreateDefeated(base.Mission));
				return true;
			}
			return false;
		}

		// Token: 0x060032B0 RID: 12976 RVA: 0x000D21B8 File Offset: 0x000D03B8
		public override InquiryData OnEndMissionRequest(out bool canPlayerLeave)
		{
			canPlayerLeave = true;
			if (!this.IsPlayerDead() && base.Mission.IsPlayerCloseToAnEnemy(5f))
			{
				canPlayerLeave = false;
				MBInformationManager.AddQuickInformation(GameTexts.FindText("str_can_not_retreat", null), 0, null, "");
			}
			else
			{
				MissionResult missionResult = null;
				if (!this.IsPlayerDead() && !this.MissionEnded(ref missionResult))
				{
					return new InquiryData("", GameTexts.FindText("str_retreat_question", null).ToString(), true, true, GameTexts.FindText("str_ok", null).ToString(), GameTexts.FindText("str_cancel", null).ToString(), new Action(base.Mission.OnEndMissionResult), null, "", 0f, null, null, null);
				}
			}
			return null;
		}

		// Token: 0x060032B1 RID: 12977 RVA: 0x000D226F File Offset: 0x000D046F
		public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow killingBlow)
		{
		}

		// Token: 0x060032B2 RID: 12978 RVA: 0x000D2274 File Offset: 0x000D0474
		private void CreateTeams()
		{
			if (!base.Mission.Teams.IsEmpty<Team>())
			{
				throw new MBIllegalValueException("Number of teams is not 0.");
			}
			base.Mission.Teams.Add(BattleSideEnum.Defender, 4278190335U, 4278190335U, null, true, false, true);
			base.Mission.Teams.Add(BattleSideEnum.Attacker, 4278255360U, 4278255360U, null, true, false, true);
			if (this.IsPlayerAttacker)
			{
				base.Mission.PlayerTeam = base.Mission.AttackerTeam;
			}
			else
			{
				base.Mission.PlayerTeam = base.Mission.DefenderTeam;
			}
			TeamAIComponent teamAI = this.GetTeamAI(base.Mission.DefenderTeam, 5f, 1f);
			base.Mission.DefenderTeam.AddTeamAI(teamAI, false);
			TeamAIComponent teamAI2 = this.GetTeamAI(base.Mission.AttackerTeam, 5f, 1f);
			base.Mission.AttackerTeam.AddTeamAI(teamAI2, false);
		}

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x060032B3 RID: 12979 RVA: 0x000D2370 File Offset: 0x000D0570
		protected bool IsDeploymentFinished
		{
			get
			{
				return base.Mission.GetMissionBehavior<DeploymentHandler>() == null;
			}
		}

		// Token: 0x060032B4 RID: 12980 RVA: 0x000D2380 File Offset: 0x000D0580
		protected void IncrementDeploymedTroops(BattleSideEnum side)
		{
			int num;
			if (side == BattleSideEnum.Attacker)
			{
				num = this.DeployedAttackerTroopCount;
				this.DeployedAttackerTroopCount = num + 1;
				return;
			}
			num = this.DeployedDefenderTroopCount;
			this.DeployedDefenderTroopCount = num + 1;
		}

		// Token: 0x060032B5 RID: 12981 RVA: 0x000D23B4 File Offset: 0x000D05B4
		protected virtual void CreatePlayer()
		{
			this.game.PlayerTroop = Game.Current.ObjectManager.GetObject<BasicCharacterObject>("main_hero");
			FormationClass formationClass = base.Mission.GetFormationSpawnClass(base.Mission.PlayerTeam.Side, FormationClass.NumberOfRegularFormations, false);
			if (formationClass != FormationClass.NumberOfRegularFormations)
			{
				formationClass = this.game.PlayerTroop.DefaultFormationClass;
			}
			WorldPosition worldPosition;
			Vec2 vec;
			base.Mission.GetFormationSpawnFrame(base.Mission.PlayerTeam.Side, formationClass, false, out worldPosition, out vec);
			Mission mission = base.Mission;
			AgentBuildData agentBuildData = new AgentBuildData(this.game.PlayerTroop).Team(base.Mission.PlayerTeam);
			Vec3 groundVec = worldPosition.GetGroundVec3();
			Agent agent = mission.SpawnAgent(agentBuildData.InitialPosition(groundVec).InitialDirection(vec).Controller(Agent.ControllerType.Player), false);
			agent.WieldInitialWeapons(Agent.WeaponWieldActionType.InstantAfterPickUp, Equipment.InitialWeaponEquipPreference.Any);
			base.Mission.MainAgent = agent;
		}

		// Token: 0x060032B6 RID: 12982 RVA: 0x000D2492 File Offset: 0x000D0692
		protected void BecomeEnemy()
		{
			base.Mission.MainAgent.Controller = Agent.ControllerType.AI;
			base.Mission.PlayerEnemyTeam.Leader.Controller = Agent.ControllerType.Player;
			this.SwapTeams();
		}

		// Token: 0x060032B7 RID: 12983 RVA: 0x000D24C1 File Offset: 0x000D06C1
		protected void BecomePlayer()
		{
			base.Mission.MainAgent.Controller = Agent.ControllerType.Player;
			base.Mission.PlayerEnemyTeam.Leader.Controller = Agent.ControllerType.AI;
			this.SwapTeams();
		}

		// Token: 0x060032B8 RID: 12984 RVA: 0x000D24F0 File Offset: 0x000D06F0
		protected void SwapTeams()
		{
			base.Mission.PlayerTeam = base.Mission.PlayerEnemyTeam;
			this.IsPlayerAttacker = !this.IsPlayerAttacker;
		}

		// Token: 0x040015F1 RID: 5617
		protected readonly Game game;
	}
}
