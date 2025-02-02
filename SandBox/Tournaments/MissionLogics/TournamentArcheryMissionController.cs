using System;
using System.Collections.Generic;
using System.Linq;
using SandBox.Missions.MissionLogics;
using SandBox.Tournaments.AgentControllers;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.AgentOrigins;
using TaleWorlds.CampaignSystem.TournamentGames;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace SandBox.Tournaments.MissionLogics
{
	// Token: 0x0200002D RID: 45
	public class TournamentArcheryMissionController : MissionLogic, ITournamentGameBehavior
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00007FFC File Offset: 0x000061FC
		public IEnumerable<ArcheryTournamentAgentController> AgentControllers
		{
			get
			{
				return this._agentControllers;
			}
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00008004 File Offset: 0x00006204
		public TournamentArcheryMissionController(CultureObject culture)
		{
			this._culture = culture;
			this.ShootingPositions = new List<GameEntity>();
			this._agentControllers = new List<ArcheryTournamentAgentController>();
			this._archeryEquipment = new Equipment();
			this._archeryEquipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.WeaponItemBeginSlot, new EquipmentElement(Game.Current.ObjectManager.GetObject<ItemObject>("nordic_shortbow"), null, null, false));
			this._archeryEquipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon1, new EquipmentElement(Game.Current.ObjectManager.GetObject<ItemObject>("blunt_arrows"), null, null, false));
			this._archeryEquipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Body, new EquipmentElement(Game.Current.ObjectManager.GetObject<ItemObject>("desert_lamellar"), null, null, false));
			this._archeryEquipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Gloves, new EquipmentElement(Game.Current.ObjectManager.GetObject<ItemObject>("reinforced_mail_mitten"), null, null, false));
			this._archeryEquipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Leg, new EquipmentElement(Game.Current.ObjectManager.GetObject<ItemObject>("leather_cavalier_boots"), null, null, false));
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00008108 File Offset: 0x00006308
		public override void AfterStart()
		{
			TournamentBehavior.DeleteTournamentSetsExcept(base.Mission.Scene.FindEntityWithTag("tournament_archery"));
			this._spawnPoints = base.Mission.Scene.FindEntitiesWithTag("sp_arena").ToList<GameEntity>();
			base.Mission.SetMissionMode(MissionMode.Battle, true);
			this._targets = (from x in base.Mission.ActiveMissionObjects.FindAllWithType<DestructableComponent>()
			where x.GameEntity.HasTag("archery_target")
			select x).ToList<DestructableComponent>();
			foreach (DestructableComponent destructableComponent in this._targets)
			{
				destructableComponent.OnDestroyed += new DestructableComponent.OnHitTakenAndDestroyedDelegate(this.OnTargetDestroyed);
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000081EC File Offset: 0x000063EC
		public void StartMatch(TournamentMatch match, bool isLastRound)
		{
			this._match = match;
			this.ResetTargets();
			int count = this._spawnPoints.Count;
			int num = 0;
			int num2 = 0;
			foreach (TournamentTeam tournamentTeam in this._match.Teams)
			{
				Team team = base.Mission.Teams.Add(BattleSideEnum.None, MissionAgentHandler.GetRandomTournamentTeamColor(num2), uint.MaxValue, null, true, false, true);
				foreach (TournamentParticipant tournamentParticipant in tournamentTeam.Participants)
				{
					tournamentParticipant.MatchEquipment = this._archeryEquipment.Clone(false);
					MatrixFrame globalFrame = this._spawnPoints[num % count].GetGlobalFrame();
					globalFrame.rotation.OrthonormalizeAccordingToForwardAndKeepUpAsZAxis();
					this.SetItemsAndSpawnCharacter(tournamentParticipant, team, globalFrame);
					num++;
				}
				num2++;
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000082FC File Offset: 0x000064FC
		public void SkipMatch(TournamentMatch match)
		{
			this._match = match;
			this.Simulate();
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000830C File Offset: 0x0000650C
		private void Simulate()
		{
			this._isSimulated = false;
			List<TournamentParticipant> list = this._match.Participants.ToList<TournamentParticipant>();
			int i = this._targets.Count;
			while (i > 0)
			{
				foreach (TournamentParticipant tournamentParticipant in list)
				{
					if (i == 0)
					{
						break;
					}
					if (MBRandom.RandomFloat < this.GetDeadliness(tournamentParticipant))
					{
						tournamentParticipant.AddScore(1);
						i--;
					}
				}
			}
			this._isSimulated = true;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x000083A4 File Offset: 0x000065A4
		public bool IsMatchEnded()
		{
			if (this._isSimulated || this._match == null)
			{
				return true;
			}
			if (this._endTimer != null && this._endTimer.ElapsedTime > 6f)
			{
				this._endTimer = null;
				return true;
			}
			if (this._endTimer == null && (!this.IsThereAnyTargetLeft() || !this.IsThereAnyArrowLeft()))
			{
				this._endTimer = new BasicMissionTimer();
			}
			return false;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000840C File Offset: 0x0000660C
		public void OnMatchEnded()
		{
			SandBoxHelpers.MissionHelper.FadeOutAgents(base.Mission.Agents, true, false);
			base.Mission.ClearCorpses(false);
			base.Mission.Teams.Clear();
			base.Mission.RemoveSpawnedItemsAndMissiles();
			this._match = null;
			this._endTimer = null;
			this._isSimulated = false;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00008468 File Offset: 0x00006668
		private void ResetTargets()
		{
			foreach (DestructableComponent destructableComponent in this._targets)
			{
				destructableComponent.Reset();
			}
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000084B8 File Offset: 0x000066B8
		private void SetItemsAndSpawnCharacter(TournamentParticipant participant, Team team, MatrixFrame frame)
		{
			AgentBuildData agentBuildData = new AgentBuildData(new SimpleAgentOrigin(participant.Character, -1, null, participant.Descriptor)).Team(team).Equipment(participant.MatchEquipment).InitialPosition(frame.origin);
			Vec2 vec = frame.rotation.f.AsVec2;
			vec = vec.Normalized();
			AgentBuildData agentBuildData2 = agentBuildData.InitialDirection(vec).Controller(participant.Character.IsPlayerCharacter ? Agent.ControllerType.Player : Agent.ControllerType.AI);
			Agent agent = base.Mission.SpawnAgent(agentBuildData2, false);
			agent.Health = agent.HealthLimit;
			ArcheryTournamentAgentController archeryTournamentAgentController = agent.AddController(typeof(ArcheryTournamentAgentController)) as ArcheryTournamentAgentController;
			archeryTournamentAgentController.SetTargets(this._targets);
			this._agentControllers.Add(archeryTournamentAgentController);
			if (participant.Character.IsPlayerCharacter)
			{
				agent.WieldInitialWeapons(Agent.WeaponWieldActionType.InstantAfterPickUp, Equipment.InitialWeaponEquipPreference.Any);
				base.Mission.PlayerTeam = team;
				return;
			}
			agent.SetWatchState(Agent.WatchState.Alarmed);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000085A4 File Offset: 0x000067A4
		public void OnTargetDestroyed(DestructableComponent destroyedComponent, Agent destroyerAgent, in MissionWeapon attackerWeapon, ScriptComponentBehavior attackerScriptComponentBehavior, int inflictedDamage)
		{
			foreach (ArcheryTournamentAgentController archeryTournamentAgentController in this.AgentControllers)
			{
				archeryTournamentAgentController.OnTargetHit(destroyerAgent, destroyedComponent);
				this._match.GetParticipant(destroyerAgent.Origin.UniqueSeed).AddScore(1);
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00008610 File Offset: 0x00006810
		public override void OnMissionTick(float dt)
		{
			base.OnMissionTick(dt);
			if (!this.IsMatchEnded())
			{
				foreach (Agent agent in base.Mission.Agents)
				{
					ArcheryTournamentAgentController controller = agent.GetController<ArcheryTournamentAgentController>();
					if (controller != null)
					{
						controller.OnTick();
					}
				}
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00008680 File Offset: 0x00006880
		public override void OnAgentHit(Agent affectedAgent, Agent affectorAgent, in MissionWeapon attackerWeapon, in Blow blow, in AttackCollisionData attackCollisionData)
		{
			base.Mission.EndMission();
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000868D File Offset: 0x0000688D
		private bool IsThereAnyTargetLeft()
		{
			return this._targets.Any((DestructableComponent e) => !e.IsDestroyed);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000086B9 File Offset: 0x000068B9
		private bool IsThereAnyArrowLeft()
		{
			return base.Mission.Agents.Any((Agent agent) => agent.Equipment.GetAmmoAmount(EquipmentIndex.WeaponItemBeginSlot) > 0);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000086EA File Offset: 0x000068EA
		private float GetDeadliness(TournamentParticipant participant)
		{
			return 0.01f + (float)participant.Character.GetSkillValue(DefaultSkills.Bow) / 300f * 0.19f;
		}

		// Token: 0x04000055 RID: 85
		private readonly List<ArcheryTournamentAgentController> _agentControllers;

		// Token: 0x04000056 RID: 86
		private TournamentMatch _match;

		// Token: 0x04000057 RID: 87
		private BasicMissionTimer _endTimer;

		// Token: 0x04000058 RID: 88
		private List<GameEntity> _spawnPoints;

		// Token: 0x04000059 RID: 89
		private bool _isSimulated;

		// Token: 0x0400005A RID: 90
		private CultureObject _culture;

		// Token: 0x0400005B RID: 91
		private List<DestructableComponent> _targets;

		// Token: 0x0400005C RID: 92
		public List<GameEntity> ShootingPositions;

		// Token: 0x0400005D RID: 93
		private readonly Equipment _archeryEquipment;
	}
}
