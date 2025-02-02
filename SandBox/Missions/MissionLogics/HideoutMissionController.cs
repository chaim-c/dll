using System;
using System.Collections.Generic;
using System.Linq;
using SandBox.Conversation.MissionLogics;
using SandBox.Objects.AnimationPoints;
using SandBox.Objects.AreaMarkers;
using SandBox.Objects.Usables;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.MissionLogics
{
	// Token: 0x0200004F RID: 79
	public class HideoutMissionController : MissionLogic, IMissionAgentSpawnLogic, IMissionBehavior
	{
		// Token: 0x06000319 RID: 793 RVA: 0x00013A60 File Offset: 0x00011C60
		public HideoutMissionController(IMissionTroopSupplier[] suppliers, BattleSideEnum playerSide, int firstPhaseEnemyTroopCount, int firstPhasePlayerSideTroopCount)
		{
			this._areaMarkers = new List<CommonAreaMarker>();
			this._patrolAreas = new List<PatrolArea>();
			this._defenderAgentObjects = new Dictionary<Agent, HideoutMissionController.UsedObject>();
			this._firstPhaseEnemyTroopCount = firstPhaseEnemyTroopCount;
			this._firstPhasePlayerSideTroopCount = firstPhasePlayerSideTroopCount;
			this._missionSides = new HideoutMissionController.MissionSide[2];
			for (int i = 0; i < 2; i++)
			{
				IMissionTroopSupplier troopSupplier = suppliers[i];
				bool isPlayerSide = i == (int)playerSide;
				this._missionSides[i] = new HideoutMissionController.MissionSide((BattleSideEnum)i, troopSupplier, isPlayerSide);
			}
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00013AD4 File Offset: 0x00011CD4
		public override void OnCreated()
		{
			base.OnCreated();
			base.Mission.DoesMissionRequireCivilianEquipment = false;
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00013AE8 File Offset: 0x00011CE8
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			this._battleAgentLogic = base.Mission.GetMissionBehavior<BattleAgentLogic>();
			this._battleEndLogic = base.Mission.GetMissionBehavior<BattleEndLogic>();
			this._battleEndLogic.ChangeCanCheckForEndCondition(false);
			this._agentVictoryLogic = base.Mission.GetMissionBehavior<AgentVictoryLogic>();
			this._cinematicController = base.Mission.GetMissionBehavior<HideoutCinematicController>();
			base.Mission.IsMainAgentObjectInteractionEnabled = false;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00013B58 File Offset: 0x00011D58
		public override void OnObjectStoppedBeingUsed(Agent userAgent, UsableMissionObject usedObject)
		{
			if (usedObject != null && usedObject is AnimationPoint && userAgent.IsActive() && userAgent.IsAIControlled && userAgent.CurrentWatchState == Agent.WatchState.Patrolling)
			{
				PatrolArea firstScriptOfType = usedObject.GameEntity.Parent.GetFirstScriptOfType<PatrolArea>();
				if (firstScriptOfType == null)
				{
					return;
				}
				((IDetachment)firstScriptOfType).AddAgent(userAgent, -1);
			}
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00013BA4 File Offset: 0x00011DA4
		public override void OnAgentAlarmedStateChanged(Agent agent, Agent.AIStateFlag flag)
		{
			bool flag2 = flag == Agent.AIStateFlag.Alarmed;
			if (flag2 || flag == Agent.AIStateFlag.Cautious)
			{
				if (agent.IsUsingGameObject)
				{
					agent.StopUsingGameObjectMT(true, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
				}
				else
				{
					agent.DisableScriptedMovement();
					if (agent.IsAIControlled && agent.AIMoveToGameObjectIsEnabled())
					{
						agent.AIMoveToGameObjectDisable();
						Formation formation = agent.Formation;
						if (formation != null)
						{
							formation.Team.DetachmentManager.RemoveScoresOfAgentFromDetachments(agent);
						}
					}
				}
				this._defenderAgentObjects[agent].IsMachineAITicked = false;
			}
			else if (flag == Agent.AIStateFlag.None)
			{
				this._defenderAgentObjects[agent].IsMachineAITicked = true;
				agent.TryToSheathWeaponInHand(Agent.HandIndex.MainHand, Agent.WeaponWieldActionType.WithAnimation);
				((IDetachment)this._defenderAgentObjects[agent].Machine).AddAgent(agent, -1);
			}
			if (flag2)
			{
				agent.SetWantsToYell();
			}
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00013C58 File Offset: 0x00011E58
		public override void OnMissionTick(float dt)
		{
			if (!this._isMissionInitialized)
			{
				this.InitializeMission();
				this._isMissionInitialized = true;
				return;
			}
			if (!this._troopsInitialized)
			{
				this._troopsInitialized = true;
				foreach (Agent agent in base.Mission.Agents)
				{
					this._battleAgentLogic.OnAgentBuild(agent, null);
				}
			}
			this.UsedObjectTick(dt);
			if (!this._battleResolved)
			{
				this.CheckBattleResolved();
			}
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00013CF0 File Offset: 0x00011EF0
		public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow blow)
		{
			if (this._hideoutMissionState == HideoutMissionController.HideoutMissionState.BossFightWithDuel)
			{
				using (List<Agent>.Enumerator enumerator = base.Mission.Agents.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Agent agent = enumerator.Current;
						if (agent != affectedAgent && agent != affectorAgent && agent.IsActive() && agent.GetLookAgent() == affectedAgent)
						{
							agent.SetLookAgent(null);
						}
					}
					return;
				}
			}
			if (this._hideoutMissionState == HideoutMissionController.HideoutMissionState.InitialFightBeforeBossFight && affectedAgent.IsMainAgent)
			{
				base.Mission.PlayerTeam.PlayerOrderController.SelectAllFormations(false);
				affectedAgent.Formation = null;
				base.Mission.PlayerTeam.PlayerOrderController.SetOrder(OrderType.Retreat);
			}
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00013DB0 File Offset: 0x00011FB0
		private void InitializeMission()
		{
			base.Mission.GetMissionBehavior<MissionConversationLogic>().DisableStartConversation(true);
			base.Mission.SetMissionMode(MissionMode.Stealth, true);
			this._areaMarkers.AddRange(from area in base.Mission.ActiveMissionObjects.FindAllWithType<CommonAreaMarker>()
			orderby area.AreaIndex
			select area);
			this._patrolAreas.AddRange(from area in base.Mission.ActiveMissionObjects.FindAllWithType<PatrolArea>()
			orderby area.AreaIndex
			select area);
			this.DecideMissionState();
			base.Mission.MakeDefaultDeploymentPlans();
			for (int i = 0; i < 2; i++)
			{
				int spawnCount;
				if (this._missionSides[i].IsPlayerSide)
				{
					spawnCount = this._firstPhasePlayerSideTroopCount;
				}
				else
				{
					if (this._missionSides[i].NumberOfTroopsNotSupplied <= this._firstPhaseEnemyTroopCount)
					{
						Debug.FailedAssert("_missionSides[i].NumberOfTroopsNotSupplied <= _firstPhaseEnemyTroopCount", "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox\\Missions\\MissionLogics\\HideoutMissionController.cs", "InitializeMission", 449);
						this._firstPhaseEnemyTroopCount = (int)((float)this._missionSides[i].NumberOfTroopsNotSupplied * 0.7f);
					}
					spawnCount = ((this._hideoutMissionState == HideoutMissionController.HideoutMissionState.InitialFightBeforeBossFight) ? this._firstPhaseEnemyTroopCount : this._missionSides[i].NumberOfTroopsNotSupplied);
				}
				this._missionSides[i].SpawnTroops(this._areaMarkers, this._patrolAreas, this._defenderAgentObjects, spawnCount);
			}
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00013F1C File Offset: 0x0001211C
		private void UsedObjectTick(float dt)
		{
			foreach (KeyValuePair<Agent, HideoutMissionController.UsedObject> keyValuePair in this._defenderAgentObjects)
			{
				if (keyValuePair.Value.IsMachineAITicked)
				{
					keyValuePair.Value.MachineAI.Tick(keyValuePair.Key, null, null, dt);
				}
			}
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00013F94 File Offset: 0x00012194
		protected override void OnEndMission()
		{
			int num = 0;
			if (this._hideoutMissionState == HideoutMissionController.HideoutMissionState.BossFightWithDuel)
			{
				if (Agent.Main == null || !Agent.Main.IsActive())
				{
					List<Agent> duelPhaseAllyAgents = this._duelPhaseAllyAgents;
					num = ((duelPhaseAllyAgents != null) ? duelPhaseAllyAgents.Count : 0);
				}
				else if (this._bossAgent == null || !this._bossAgent.IsActive())
				{
					PlayerEncounter.EnemySurrender = true;
				}
			}
			if (MobileParty.MainParty.MemberRoster.TotalHealthyCount <= num && MapEvent.PlayerMapEvent.BattleState == BattleState.None)
			{
				MapEvent.PlayerMapEvent.SetOverrideWinner(BattleSideEnum.Defender);
			}
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00014018 File Offset: 0x00012218
		private void CheckBattleResolved()
		{
			if (this._hideoutMissionState != HideoutMissionController.HideoutMissionState.CutSceneBeforeBossFight && this._hideoutMissionState != HideoutMissionController.HideoutMissionState.ConversationBetweenLeaders)
			{
				if (this.IsSideDepleted(BattleSideEnum.Attacker))
				{
					if (this._hideoutMissionState == HideoutMissionController.HideoutMissionState.BossFightWithDuel)
					{
						this.OnDuelOver(BattleSideEnum.Defender);
					}
					this._battleEndLogic.ChangeCanCheckForEndCondition(true);
					this._battleResolved = true;
					return;
				}
				if (this.IsSideDepleted(BattleSideEnum.Defender))
				{
					if (this._hideoutMissionState == HideoutMissionController.HideoutMissionState.InitialFightBeforeBossFight)
					{
						if (this._firstPhaseEndTimer == null)
						{
							this._firstPhaseEndTimer = new Timer(base.Mission.CurrentTime, 4f, true);
							this._oldMissionMode = Mission.Current.Mode;
							Mission.Current.SetMissionMode(MissionMode.CutScene, false);
							return;
						}
						if (this._firstPhaseEndTimer.Check(base.Mission.CurrentTime))
						{
							this._cinematicController.StartCinematic(new HideoutCinematicController.OnInitialFadeOutFinished(this.OnInitialFadeOutOver), new HideoutCinematicController.OnHideoutCinematicFinished(this.OnCutSceneOver), 0.4f, 0.2f, 8f);
							return;
						}
					}
					else
					{
						if (this._hideoutMissionState == HideoutMissionController.HideoutMissionState.BossFightWithDuel)
						{
							this.OnDuelOver(BattleSideEnum.Attacker);
						}
						this._battleEndLogic.ChangeCanCheckForEndCondition(true);
						MapEvent.PlayerMapEvent.SetOverrideWinner(BattleSideEnum.Attacker);
						this._battleResolved = true;
					}
				}
			}
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0001413D File Offset: 0x0001233D
		public void StartSpawner(BattleSideEnum side)
		{
			this._missionSides[(int)side].SetSpawnTroops(true);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0001414D File Offset: 0x0001234D
		public void StopSpawner(BattleSideEnum side)
		{
			this._missionSides[(int)side].SetSpawnTroops(false);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0001415D File Offset: 0x0001235D
		public bool IsSideSpawnEnabled(BattleSideEnum side)
		{
			return this._missionSides[(int)side].TroopSpawningActive;
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0001416C File Offset: 0x0001236C
		public float GetReinforcementInterval()
		{
			return 0f;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00014174 File Offset: 0x00012374
		public unsafe bool IsSideDepleted(BattleSideEnum side)
		{
			bool flag = this._missionSides[(int)side].NumberOfActiveTroops == 0;
			if (!flag)
			{
				if ((Agent.Main == null || !Agent.Main.IsActive()) && side == BattleSideEnum.Attacker)
				{
					if (this._hideoutMissionState == HideoutMissionController.HideoutMissionState.BossFightWithDuel || this._hideoutMissionState == HideoutMissionController.HideoutMissionState.InitialFightBeforeBossFight)
					{
						flag = true;
					}
					else if (this._hideoutMissionState == HideoutMissionController.HideoutMissionState.WithoutBossFight || this._hideoutMissionState == HideoutMissionController.HideoutMissionState.BossFightWithAll)
					{
						bool flag2 = base.Mission.Teams.Attacker.FormationsIncludingEmpty.Any(delegate(Formation f)
						{
							if (f.CountOfUnits > 0)
							{
								MovementOrder movementOrder = *f.GetReadonlyMovementOrderReference();
								return movementOrder.OrderType == OrderType.Charge;
							}
							return false;
						});
						bool flag3 = base.Mission.Teams.Defender.ActiveAgents.Any((Agent t) => t.CurrentWatchState == Agent.WatchState.Alarmed);
						flag = (!flag2 && !flag3);
					}
				}
				else if (side == BattleSideEnum.Defender && this._hideoutMissionState == HideoutMissionController.HideoutMissionState.BossFightWithDuel && (this._bossAgent == null || !this._bossAgent.IsActive()))
				{
					flag = true;
				}
			}
			else if (side == BattleSideEnum.Defender && this._hideoutMissionState == HideoutMissionController.HideoutMissionState.InitialFightBeforeBossFight && (Agent.Main == null || !Agent.Main.IsActive()))
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000329 RID: 809 RVA: 0x000142AC File Offset: 0x000124AC
		private void DecideMissionState()
		{
			HideoutMissionController.MissionSide missionSide = this._missionSides[0];
			this._hideoutMissionState = ((!missionSide.IsPlayerSide) ? HideoutMissionController.HideoutMissionState.InitialFightBeforeBossFight : HideoutMissionController.HideoutMissionState.WithoutBossFight);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x000142D4 File Offset: 0x000124D4
		private void SetWatchStateOfAIAgents(Agent.WatchState state)
		{
			foreach (Agent agent in base.Mission.Agents)
			{
				if (agent.IsAIControlled)
				{
					agent.SetWatchState(state);
				}
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00014334 File Offset: 0x00012534
		private void SpawnBossAndBodyguards()
		{
			HideoutMissionController.MissionSide missionSide = this._missionSides[0];
			MatrixFrame banditsInitialFrame = this._cinematicController.GetBanditsInitialFrame();
			missionSide.SpawnRemainingTroopsForBossFight(new List<MatrixFrame>
			{
				banditsInitialFrame
			}, missionSide.NumberOfTroopsNotSupplied);
			this._bossAgent = this.SelectBossAgent();
			this._bossAgent.WieldInitialWeapons(Agent.WeaponWieldActionType.InstantAfterPickUp, Equipment.InitialWeaponEquipPreference.MeleeForMainHand);
			foreach (Agent agent in this._enemyTeam.ActiveAgents)
			{
				if (agent != this._bossAgent)
				{
					agent.WieldInitialWeapons(Agent.WeaponWieldActionType.WithAnimationUninterruptible, Equipment.InitialWeaponEquipPreference.Any);
				}
			}
		}

		// Token: 0x0600032C RID: 812 RVA: 0x000143DC File Offset: 0x000125DC
		private Agent SelectBossAgent()
		{
			Agent agent = null;
			Agent agent2 = null;
			foreach (Agent agent3 in base.Mission.Agents)
			{
				if (agent3.Team == this._enemyTeam && agent3.IsHuman)
				{
					if (agent3.IsHero)
					{
						agent = agent3;
						break;
					}
					if (agent3.Character.Culture.IsBandit)
					{
						CultureObject cultureObject = agent3.Character.Culture as CultureObject;
						if (((cultureObject != null) ? cultureObject.BanditBoss : null) != null && ((CultureObject)agent3.Character.Culture).BanditBoss == agent3.Character)
						{
							agent = agent3;
						}
					}
					if (agent2 == null || agent3.Character.Level > agent2.Character.Level)
					{
						agent2 = agent3;
					}
				}
			}
			agent = (agent ?? agent2);
			return agent;
		}

		// Token: 0x0600032D RID: 813 RVA: 0x000144D4 File Offset: 0x000126D4
		private void OnInitialFadeOutOver(ref Agent playerAgent, ref List<Agent> playerCompanions, ref Agent bossAgent, ref List<Agent> bossCompanions, ref float placementPerturbation, ref float placementAngle)
		{
			this._hideoutMissionState = HideoutMissionController.HideoutMissionState.CutSceneBeforeBossFight;
			this._enemyTeam = base.Mission.PlayerEnemyTeam;
			this.SpawnBossAndBodyguards();
			base.Mission.PlayerTeam.SetIsEnemyOf(this._enemyTeam, false);
			this.SetWatchStateOfAIAgents(Agent.WatchState.Patrolling);
			if (Agent.Main.IsUsingGameObject)
			{
				Agent.Main.StopUsingGameObject(false, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
			}
			playerAgent = Agent.Main;
			playerCompanions = (from x in base.Mission.Agents
			where x.IsActive() && x.Team == base.Mission.PlayerTeam && x.IsHuman && x.IsAIControlled
			select x).ToList<Agent>();
			bossAgent = this._bossAgent;
			bossCompanions = (from x in base.Mission.Agents
			where x.IsActive() && x.Team == this._enemyTeam && x.IsHuman && x.IsAIControlled && x != this._bossAgent
			select x).ToList<Agent>();
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0001458B File Offset: 0x0001278B
		private void OnCutSceneOver()
		{
			Mission.Current.SetMissionMode(this._oldMissionMode, false);
			this._hideoutMissionState = HideoutMissionController.HideoutMissionState.ConversationBetweenLeaders;
			MissionConversationLogic missionBehavior = base.Mission.GetMissionBehavior<MissionConversationLogic>();
			missionBehavior.DisableStartConversation(false);
			missionBehavior.StartConversation(this._bossAgent, false, false);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x000145C4 File Offset: 0x000127C4
		private void OnDuelOver(BattleSideEnum winnerSide)
		{
			AgentVictoryLogic missionBehavior = base.Mission.GetMissionBehavior<AgentVictoryLogic>();
			if (missionBehavior != null)
			{
				missionBehavior.SetCheerActionGroup(AgentVictoryLogic.CheerActionGroupEnum.HighCheerActions);
			}
			if (missionBehavior != null)
			{
				missionBehavior.SetCheerReactionTimerSettings(0.25f, 3f);
			}
			if (winnerSide == BattleSideEnum.Attacker && this._duelPhaseAllyAgents != null)
			{
				using (List<Agent>.Enumerator enumerator = this._duelPhaseAllyAgents.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Agent agent = enumerator.Current;
						if (agent.State == AgentState.Active)
						{
							agent.SetTeam(base.Mission.PlayerTeam, true);
							agent.SetWatchState(Agent.WatchState.Alarmed);
						}
					}
					return;
				}
			}
			if (winnerSide == BattleSideEnum.Defender && this._duelPhaseBanditAgents != null)
			{
				foreach (Agent agent2 in this._duelPhaseBanditAgents)
				{
					if (agent2.State == AgentState.Active)
					{
						agent2.SetTeam(this._enemyTeam, true);
						agent2.SetWatchState(Agent.WatchState.Alarmed);
					}
				}
			}
		}

		// Token: 0x06000330 RID: 816 RVA: 0x000146D0 File Offset: 0x000128D0
		public static void StartBossFightDuelMode()
		{
			Mission mission = Mission.Current;
			HideoutMissionController hideoutMissionController = (mission != null) ? mission.GetMissionBehavior<HideoutMissionController>() : null;
			if (hideoutMissionController == null)
			{
				return;
			}
			hideoutMissionController.StartBossFightDuelModeInternal();
		}

		// Token: 0x06000331 RID: 817 RVA: 0x000146F0 File Offset: 0x000128F0
		private void StartBossFightDuelModeInternal()
		{
			base.Mission.GetMissionBehavior<MissionConversationLogic>().DisableStartConversation(true);
			base.Mission.PlayerTeam.SetIsEnemyOf(this._enemyTeam, true);
			this._duelPhaseAllyAgents = (from x in base.Mission.Agents
			where x.IsActive() && x.Team == base.Mission.PlayerTeam && x.IsHuman && x.IsAIControlled && x != Agent.Main
			select x).ToList<Agent>();
			this._duelPhaseBanditAgents = (from x in base.Mission.Agents
			where x.IsActive() && x.Team == this._enemyTeam && x.IsHuman && x.IsAIControlled && x != this._bossAgent
			select x).ToList<Agent>();
			foreach (Agent agent in this._duelPhaseAllyAgents)
			{
				agent.SetTeam(Team.Invalid, true);
				WorldPosition worldPosition = agent.GetWorldPosition();
				agent.SetScriptedPosition(ref worldPosition, false, Agent.AIScriptedFrameFlags.None);
				agent.SetLookAgent(Agent.Main);
			}
			foreach (Agent agent2 in this._duelPhaseBanditAgents)
			{
				agent2.SetTeam(Team.Invalid, true);
				WorldPosition worldPosition2 = agent2.GetWorldPosition();
				agent2.SetScriptedPosition(ref worldPosition2, false, Agent.AIScriptedFrameFlags.None);
				agent2.SetLookAgent(this._bossAgent);
			}
			this._bossAgent.SetWatchState(Agent.WatchState.Alarmed);
			this._hideoutMissionState = HideoutMissionController.HideoutMissionState.BossFightWithDuel;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00014850 File Offset: 0x00012A50
		public static void StartBossFightBattleMode()
		{
			Mission mission = Mission.Current;
			HideoutMissionController hideoutMissionController = (mission != null) ? mission.GetMissionBehavior<HideoutMissionController>() : null;
			if (hideoutMissionController == null)
			{
				return;
			}
			hideoutMissionController.StartBossFightBattleModeInternal();
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00014870 File Offset: 0x00012A70
		private void StartBossFightBattleModeInternal()
		{
			base.Mission.GetMissionBehavior<MissionConversationLogic>().DisableStartConversation(true);
			base.Mission.PlayerTeam.SetIsEnemyOf(this._enemyTeam, true);
			this.SetWatchStateOfAIAgents(Agent.WatchState.Alarmed);
			this._hideoutMissionState = HideoutMissionController.HideoutMissionState.BossFightWithAll;
			foreach (Formation formation in base.Mission.PlayerTeam.FormationsIncludingEmpty)
			{
				if (formation.CountOfUnits > 0)
				{
					formation.SetMovementOrder(MovementOrder.MovementOrderCharge);
					formation.FiringOrder = FiringOrder.FiringOrderFireAtWill;
				}
			}
		}

		// Token: 0x04000179 RID: 377
		private const int FirstPhaseEndInSeconds = 4;

		// Token: 0x0400017A RID: 378
		private readonly List<CommonAreaMarker> _areaMarkers;

		// Token: 0x0400017B RID: 379
		private readonly List<PatrolArea> _patrolAreas;

		// Token: 0x0400017C RID: 380
		private readonly Dictionary<Agent, HideoutMissionController.UsedObject> _defenderAgentObjects;

		// Token: 0x0400017D RID: 381
		private readonly HideoutMissionController.MissionSide[] _missionSides;

		// Token: 0x0400017E RID: 382
		private List<Agent> _duelPhaseAllyAgents;

		// Token: 0x0400017F RID: 383
		private List<Agent> _duelPhaseBanditAgents;

		// Token: 0x04000180 RID: 384
		private BattleAgentLogic _battleAgentLogic;

		// Token: 0x04000181 RID: 385
		private BattleEndLogic _battleEndLogic;

		// Token: 0x04000182 RID: 386
		private AgentVictoryLogic _agentVictoryLogic;

		// Token: 0x04000183 RID: 387
		private HideoutCinematicController _cinematicController;

		// Token: 0x04000184 RID: 388
		private HideoutMissionController.HideoutMissionState _hideoutMissionState;

		// Token: 0x04000185 RID: 389
		private Agent _bossAgent;

		// Token: 0x04000186 RID: 390
		private Team _enemyTeam;

		// Token: 0x04000187 RID: 391
		private Timer _firstPhaseEndTimer;

		// Token: 0x04000188 RID: 392
		private bool _troopsInitialized;

		// Token: 0x04000189 RID: 393
		private bool _isMissionInitialized;

		// Token: 0x0400018A RID: 394
		private bool _battleResolved;

		// Token: 0x0400018B RID: 395
		private int _firstPhaseEnemyTroopCount;

		// Token: 0x0400018C RID: 396
		private int _firstPhasePlayerSideTroopCount;

		// Token: 0x0400018D RID: 397
		private MissionMode _oldMissionMode;

		// Token: 0x0200012E RID: 302
		private class MissionSide
		{
			// Token: 0x170000EE RID: 238
			// (get) Token: 0x06000BC7 RID: 3015 RVA: 0x000526AD File Offset: 0x000508AD
			// (set) Token: 0x06000BC8 RID: 3016 RVA: 0x000526B5 File Offset: 0x000508B5
			public bool TroopSpawningActive { get; private set; }

			// Token: 0x170000EF RID: 239
			// (get) Token: 0x06000BC9 RID: 3017 RVA: 0x000526BE File Offset: 0x000508BE
			public int NumberOfActiveTroops
			{
				get
				{
					return this._numberOfSpawnedTroops - this._troopSupplier.NumRemovedTroops;
				}
			}

			// Token: 0x170000F0 RID: 240
			// (get) Token: 0x06000BCA RID: 3018 RVA: 0x000526D2 File Offset: 0x000508D2
			public int NumberOfTroopsNotSupplied
			{
				get
				{
					return this._troopSupplier.NumTroopsNotSupplied;
				}
			}

			// Token: 0x06000BCB RID: 3019 RVA: 0x000526DF File Offset: 0x000508DF
			public MissionSide(BattleSideEnum side, IMissionTroopSupplier troopSupplier, bool isPlayerSide)
			{
				this._side = side;
				this.IsPlayerSide = isPlayerSide;
				this._troopSupplier = troopSupplier;
			}

			// Token: 0x06000BCC RID: 3020 RVA: 0x000526FC File Offset: 0x000508FC
			public void SpawnTroops(List<CommonAreaMarker> areaMarkers, List<PatrolArea> patrolAreas, Dictionary<Agent, HideoutMissionController.UsedObject> defenderAgentObjects, int spawnCount)
			{
				int num = 0;
				bool flag = false;
				List<StandingPoint> list = new List<StandingPoint>();
				foreach (CommonAreaMarker commonAreaMarker in areaMarkers)
				{
					foreach (UsableMachine usableMachine in commonAreaMarker.GetUsableMachinesInRange(null))
					{
						list.AddRange(usableMachine.StandingPoints);
					}
				}
				List<IAgentOriginBase> list2 = this._troopSupplier.SupplyTroops(spawnCount).ToList<IAgentOriginBase>();
				for (int i = 0; i < list2.Count; i++)
				{
					if (BattleSideEnum.Attacker == this._side)
					{
						Mission.Current.SpawnTroop(list2[i], true, true, false, false, 0, 0, true, true, true, null, null, null, null, FormationClass.NumberOfAllFormations, false);
						this._numberOfSpawnedTroops++;
					}
					else if (areaMarkers.Count > num)
					{
						StandingPoint standingPoint = null;
						int num2 = list2.Count - i;
						if (num2 < list.Count / 2 && num2 < 4)
						{
							flag = true;
						}
						if (!flag)
						{
							list.Shuffle<StandingPoint>();
							standingPoint = list.FirstOrDefault((StandingPoint point) => !point.IsDeactivated && !point.IsDisabled && !point.HasUser);
						}
						else
						{
							IEnumerable<PatrolArea> source = from area in patrolAreas
							where area.StandingPoints.All((StandingPoint point) => !point.HasUser && !point.HasAIMovingTo)
							select area;
							if (!source.IsEmpty<PatrolArea>())
							{
								standingPoint = source.First<PatrolArea>().StandingPoints[0];
							}
						}
						if (standingPoint != null && !standingPoint.IsDisabled)
						{
							MatrixFrame globalFrame = standingPoint.GameEntity.GetGlobalFrame();
							globalFrame.rotation.OrthonormalizeAccordingToForwardAndKeepUpAsZAxis();
							Agent agent = Mission.Current.SpawnTroop(list2[i], false, false, false, false, 0, 0, false, false, false, new Vec3?(globalFrame.origin), new Vec2?(globalFrame.rotation.f.AsVec2.Normalized()), "_hideout_bandit", null, FormationClass.NumberOfAllFormations, false);
							this.InitializeBanditAgent(agent, standingPoint, flag, defenderAgentObjects);
							this._numberOfSpawnedTroops++;
							int groupId = ((AnimationPoint)standingPoint).GroupId;
							if (flag)
							{
								goto IL_291;
							}
							using (List<StandingPoint>.Enumerator enumerator3 = standingPoint.GameEntity.Parent.GetFirstScriptOfType<UsableMachine>().StandingPoints.GetEnumerator())
							{
								while (enumerator3.MoveNext())
								{
									StandingPoint standingPoint2 = enumerator3.Current;
									int groupId2 = ((AnimationPoint)standingPoint2).GroupId;
									if (groupId == groupId2 && standingPoint2 != standingPoint)
									{
										standingPoint2.SetDisabledAndMakeInvisible(false);
									}
								}
								goto IL_291;
							}
						}
						num++;
					}
					IL_291:;
				}
				foreach (Formation formation in Mission.Current.AttackerTeam.FormationsIncludingEmpty)
				{
					if (formation.CountOfUnits > 0)
					{
						formation.SetMovementOrder(MovementOrder.MovementOrderMove(formation.QuerySystem.MedianPosition));
					}
					formation.FiringOrder = FiringOrder.FiringOrderHoldYourFire;
					if (Mission.Current.AttackerTeam == Mission.Current.PlayerTeam)
					{
						formation.PlayerOwner = Mission.Current.MainAgent;
					}
				}
			}

			// Token: 0x06000BCD RID: 3021 RVA: 0x00052A70 File Offset: 0x00050C70
			public void SpawnRemainingTroopsForBossFight(List<MatrixFrame> spawnFrames, int spawnCount)
			{
				List<IAgentOriginBase> list = this._troopSupplier.SupplyTroops(spawnCount).ToList<IAgentOriginBase>();
				for (int i = 0; i < list.Count; i++)
				{
					MatrixFrame matrixFrame = spawnFrames.FirstOrDefault<MatrixFrame>();
					matrixFrame.rotation.OrthonormalizeAccordingToForwardAndKeepUpAsZAxis();
					Agent agent = Mission.Current.SpawnTroop(list[i], false, false, false, false, 0, 0, false, false, false, new Vec3?(matrixFrame.origin), new Vec2?(matrixFrame.rotation.f.AsVec2.Normalized()), "_hideout_bandit", null, FormationClass.NumberOfAllFormations, false);
					AgentFlag agentFlags = agent.GetAgentFlags();
					if (agentFlags.HasAnyFlag(AgentFlag.CanRetreat))
					{
						agent.SetAgentFlags(agentFlags & ~AgentFlag.CanRetreat);
					}
					this._numberOfSpawnedTroops++;
				}
				foreach (Formation formation in Mission.Current.AttackerTeam.FormationsIncludingEmpty)
				{
					if (formation.CountOfUnits > 0)
					{
						formation.SetMovementOrder(MovementOrder.MovementOrderMove(formation.QuerySystem.MedianPosition));
					}
					formation.FiringOrder = FiringOrder.FiringOrderHoldYourFire;
					if (Mission.Current.AttackerTeam == Mission.Current.PlayerTeam)
					{
						formation.PlayerOwner = Mission.Current.MainAgent;
					}
				}
			}

			// Token: 0x06000BCE RID: 3022 RVA: 0x00052BD8 File Offset: 0x00050DD8
			private void InitializeBanditAgent(Agent agent, StandingPoint spawnPoint, bool isPatrolling, Dictionary<Agent, HideoutMissionController.UsedObject> defenderAgentObjects)
			{
				UsableMachine usableMachine = isPatrolling ? spawnPoint.GameEntity.Parent.GetScriptComponents<PatrolArea>().FirstOrDefault<PatrolArea>() : spawnPoint.GameEntity.Parent.GetScriptComponents<UsableMachine>().FirstOrDefault<UsableMachine>();
				if (isPatrolling)
				{
					((IDetachment)usableMachine).AddAgent(agent, -1);
					agent.WieldInitialWeapons(Agent.WeaponWieldActionType.InstantAfterPickUp, Equipment.InitialWeaponEquipPreference.Any);
				}
				else
				{
					agent.UseGameObject(spawnPoint, -1);
				}
				defenderAgentObjects.Add(agent, new HideoutMissionController.UsedObject(usableMachine, isPatrolling));
				AgentFlag agentFlags = agent.GetAgentFlags();
				agent.SetAgentFlags((agentFlags | AgentFlag.CanGetAlarmed) & ~AgentFlag.CanRetreat);
				agent.GetComponent<CampaignAgentComponent>().CreateAgentNavigator();
				this.SimulateTick(agent);
			}

			// Token: 0x06000BCF RID: 3023 RVA: 0x00052C70 File Offset: 0x00050E70
			private void SimulateTick(Agent agent)
			{
				int num = MBRandom.RandomInt(1, 20);
				for (int i = 0; i < num; i++)
				{
					if (agent.IsUsingGameObject)
					{
						agent.CurrentlyUsedGameObject.SimulateTick(0.1f);
					}
				}
			}

			// Token: 0x06000BD0 RID: 3024 RVA: 0x00052CAA File Offset: 0x00050EAA
			public void SetSpawnTroops(bool spawnTroops)
			{
				this.TroopSpawningActive = spawnTroops;
			}

			// Token: 0x04000553 RID: 1363
			private readonly BattleSideEnum _side;

			// Token: 0x04000554 RID: 1364
			private readonly IMissionTroopSupplier _troopSupplier;

			// Token: 0x04000555 RID: 1365
			public readonly bool IsPlayerSide;

			// Token: 0x04000557 RID: 1367
			private int _numberOfSpawnedTroops;
		}

		// Token: 0x0200012F RID: 303
		private class UsedObject
		{
			// Token: 0x06000BD1 RID: 3025 RVA: 0x00052CB3 File Offset: 0x00050EB3
			public UsedObject(UsableMachine machine, bool isMachineAITicked)
			{
				this.Machine = machine;
				this.MachineAI = machine.CreateAIBehaviorObject();
				this.IsMachineAITicked = isMachineAITicked;
			}

			// Token: 0x04000558 RID: 1368
			public readonly UsableMachine Machine;

			// Token: 0x04000559 RID: 1369
			public readonly UsableMachineAIBase MachineAI;

			// Token: 0x0400055A RID: 1370
			public bool IsMachineAITicked;
		}

		// Token: 0x02000130 RID: 304
		private enum HideoutMissionState
		{
			// Token: 0x0400055C RID: 1372
			NotDecided,
			// Token: 0x0400055D RID: 1373
			WithoutBossFight,
			// Token: 0x0400055E RID: 1374
			InitialFightBeforeBossFight,
			// Token: 0x0400055F RID: 1375
			CutSceneBeforeBossFight,
			// Token: 0x04000560 RID: 1376
			ConversationBetweenLeaders,
			// Token: 0x04000561 RID: 1377
			BossFightWithDuel,
			// Token: 0x04000562 RID: 1378
			BossFightWithAll
		}
	}
}
