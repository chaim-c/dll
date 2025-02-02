using System;
using System.Collections.Generic;
using System.Linq;
using SandBox.Tournaments.AgentControllers;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.AgentOrigins;
using TaleWorlds.CampaignSystem.TournamentGames;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

namespace SandBox.Tournaments.MissionLogics
{
	// Token: 0x02000030 RID: 48
	public class TournamentJoustingMissionController : MissionLogic, ITournamentGameBehavior
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000183 RID: 387 RVA: 0x0000A658 File Offset: 0x00008858
		// (remove) Token: 0x06000184 RID: 388 RVA: 0x0000A690 File Offset: 0x00008890
		public event TournamentJoustingMissionController.JoustingEventDelegate VictoryAchieved;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000185 RID: 389 RVA: 0x0000A6C8 File Offset: 0x000088C8
		// (remove) Token: 0x06000186 RID: 390 RVA: 0x0000A700 File Offset: 0x00008900
		public event TournamentJoustingMissionController.JoustingEventDelegate PointGanied;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000187 RID: 391 RVA: 0x0000A738 File Offset: 0x00008938
		// (remove) Token: 0x06000188 RID: 392 RVA: 0x0000A770 File Offset: 0x00008970
		public event TournamentJoustingMissionController.JoustingEventDelegate Disqualified;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000189 RID: 393 RVA: 0x0000A7A8 File Offset: 0x000089A8
		// (remove) Token: 0x0600018A RID: 394 RVA: 0x0000A7E0 File Offset: 0x000089E0
		public event TournamentJoustingMissionController.JoustingEventDelegate Unconscious;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600018B RID: 395 RVA: 0x0000A818 File Offset: 0x00008A18
		// (remove) Token: 0x0600018C RID: 396 RVA: 0x0000A850 File Offset: 0x00008A50
		public event TournamentJoustingMissionController.JoustingAgentStateChangedEventDelegate AgentStateChanged;

		// Token: 0x0600018D RID: 397 RVA: 0x0000A888 File Offset: 0x00008A88
		public TournamentJoustingMissionController(CultureObject culture)
		{
			this._culture = culture;
			this._match = null;
			this.RegionBoxList = new List<GameEntity>(2);
			this.RegionExitBoxList = new List<GameEntity>(2);
			this.CornerBackStartList = new List<MatrixFrame>();
			this.CornerStartList = new List<GameEntity>(2);
			this.CornerMiddleList = new List<MatrixFrame>();
			this.CornerFinishList = new List<MatrixFrame>();
			this.IsSwordDuelStarted = false;
			this._joustingEquipment = new Equipment();
			this._joustingEquipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.ArmorItemEndSlot, new EquipmentElement(Game.Current.ObjectManager.GetObject<ItemObject>("charger"), null, null, false));
			this._joustingEquipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.HorseHarness, new EquipmentElement(Game.Current.ObjectManager.GetObject<ItemObject>("horse_harness_e"), null, null, false));
			this._joustingEquipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.WeaponItemBeginSlot, new EquipmentElement(Game.Current.ObjectManager.GetObject<ItemObject>("vlandia_lance_2_t4"), null, null, false));
			this._joustingEquipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Weapon1, new EquipmentElement(Game.Current.ObjectManager.GetObject<ItemObject>("leather_round_shield"), null, null, false));
			this._joustingEquipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Body, new EquipmentElement(Game.Current.ObjectManager.GetObject<ItemObject>("desert_lamellar"), null, null, false));
			this._joustingEquipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.NumAllWeaponSlots, new EquipmentElement(Game.Current.ObjectManager.GetObject<ItemObject>("nasal_helmet_with_mail"), null, null, false));
			this._joustingEquipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Gloves, new EquipmentElement(Game.Current.ObjectManager.GetObject<ItemObject>("reinforced_mail_mitten"), null, null, false));
			this._joustingEquipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Leg, new EquipmentElement(Game.Current.ObjectManager.GetObject<ItemObject>("leather_cavalier_boots"), null, null, false));
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000AA44 File Offset: 0x00008C44
		public override void AfterStart()
		{
			TournamentBehavior.DeleteTournamentSetsExcept(base.Mission.Scene.FindEntityWithTag("tournament_jousting"));
			for (int i = 0; i < 2; i++)
			{
				GameEntity gameEntity = base.Mission.Scene.FindEntityWithTag("sp_jousting_back_" + i);
				GameEntity item = base.Mission.Scene.FindEntityWithTag("sp_jousting_start_" + i);
				GameEntity gameEntity2 = base.Mission.Scene.FindEntityWithTag("sp_jousting_middle_" + i);
				GameEntity gameEntity3 = base.Mission.Scene.FindEntityWithTag("sp_jousting_finish_" + i);
				this.CornerBackStartList.Add(gameEntity.GetGlobalFrame());
				this.CornerStartList.Add(item);
				this.CornerMiddleList.Add(gameEntity2.GetGlobalFrame());
				this.CornerFinishList.Add(gameEntity3.GetGlobalFrame());
			}
			GameEntity item2 = base.Mission.Scene.FindEntityWithName("region_box_0");
			this.RegionBoxList.Add(item2);
			GameEntity item3 = base.Mission.Scene.FindEntityWithName("region_box_1");
			this.RegionBoxList.Add(item3);
			GameEntity item4 = base.Mission.Scene.FindEntityWithName("region_end_box_0");
			this.RegionExitBoxList.Add(item4);
			GameEntity item5 = base.Mission.Scene.FindEntityWithName("region_end_box_1");
			this.RegionExitBoxList.Add(item5);
			base.Mission.SetMissionMode(MissionMode.Battle, true);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000ABE8 File Offset: 0x00008DE8
		public void StartMatch(TournamentMatch match, bool isLastRound)
		{
			this._match = match;
			int num = 0;
			foreach (TournamentTeam tournamentTeam in this._match.Teams)
			{
				Team team = base.Mission.Teams.Add(BattleSideEnum.None, uint.MaxValue, uint.MaxValue, null, true, false, true);
				foreach (TournamentParticipant tournamentParticipant in tournamentTeam.Participants)
				{
					tournamentParticipant.MatchEquipment = this._joustingEquipment.Clone(false);
					this.SetItemsAndSpawnCharacter(tournamentParticipant, team, num);
				}
				num++;
			}
			List<Team> list = base.Mission.Teams.ToList<Team>();
			for (int i = 0; i < list.Count; i++)
			{
				for (int j = i + 1; j < list.Count; j++)
				{
					list[i].SetIsEnemyOf(list[j], true);
				}
			}
			base.Mission.Scene.SetAbilityOfFacesWithId(1, false);
			base.Mission.Scene.SetAbilityOfFacesWithId(2, false);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000AD28 File Offset: 0x00008F28
		public void SkipMatch(TournamentMatch match)
		{
			this._match = match;
			this.Simulate();
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000AD38 File Offset: 0x00008F38
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
			if (this._endTimer == null && this._winnerTeam != null)
			{
				this._endTimer = new BasicMissionTimer();
			}
			return false;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000AD98 File Offset: 0x00008F98
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

		// Token: 0x06000193 RID: 403 RVA: 0x0000ADF4 File Offset: 0x00008FF4
		private void Simulate()
		{
			this._isSimulated = false;
			List<TournamentParticipant> participants = this._match.Participants.ToList<TournamentParticipant>();
			while (participants.Count > 1 && participants.Any((TournamentParticipant x) => x.Team != participants[0].Team))
			{
				if (participants.Any((TournamentParticipant x) => x.Score >= 3))
				{
					break;
				}
				TournamentParticipant tournamentParticipant = participants[MBRandom.RandomInt(participants.Count)];
				TournamentParticipant tournamentParticipant2 = participants[MBRandom.RandomInt(participants.Count)];
				while (tournamentParticipant == tournamentParticipant2 || tournamentParticipant.Team == tournamentParticipant2.Team)
				{
					tournamentParticipant2 = participants[MBRandom.RandomInt(participants.Count)];
				}
				tournamentParticipant.AddScore(1);
			}
			this._isSimulated = true;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000AEF4 File Offset: 0x000090F4
		private void SetItemsAndSpawnCharacter(TournamentParticipant participant, Team team, int cornerIndex)
		{
			AgentBuildData agentBuildData = new AgentBuildData(new SimpleAgentOrigin(participant.Character, -1, null, participant.Descriptor)).Team(team).InitialFrameFromSpawnPointEntity(this.CornerStartList[cornerIndex]).Equipment(participant.MatchEquipment).Controller(participant.Character.IsPlayerCharacter ? Agent.ControllerType.Player : Agent.ControllerType.AI);
			Agent agent = base.Mission.SpawnAgent(agentBuildData, false);
			agent.Health = agent.HealthLimit;
			this.AddJoustingAgentController(agent);
			agent.GetController<JoustingAgentController>().CurrentCornerIndex = cornerIndex;
			if (participant.Character.IsPlayerCharacter)
			{
				agent.WieldInitialWeapons(Agent.WeaponWieldActionType.InstantAfterPickUp, Equipment.InitialWeaponEquipPreference.Any);
				base.Mission.PlayerTeam = team;
				return;
			}
			agent.SetWatchState(Agent.WatchState.Alarmed);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000AFA8 File Offset: 0x000091A8
		private void AddJoustingAgentController(Agent agent)
		{
			agent.AddController(typeof(JoustingAgentController));
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000AFBC File Offset: 0x000091BC
		public bool IsAgentInTheTrack(Agent agent, bool inCurrentTrack = true)
		{
			bool result = false;
			if (agent != null)
			{
				JoustingAgentController controller = agent.GetController<JoustingAgentController>();
				int index = inCurrentTrack ? controller.CurrentCornerIndex : (1 - controller.CurrentCornerIndex);
				result = this.RegionBoxList[index].CheckPointWithOrientedBoundingBox(agent.Position);
			}
			return result;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000B004 File Offset: 0x00009204
		public override void OnMissionTick(float dt)
		{
			base.OnMissionTick(dt);
			if (!base.Mission.IsMissionEnding)
			{
				foreach (Agent agent in base.Mission.Agents)
				{
					JoustingAgentController controller = agent.GetController<JoustingAgentController>();
					if (controller != null)
					{
						controller.UpdateState();
					}
				}
				this.CheckStartOfSwordDuel();
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000B080 File Offset: 0x00009280
		private void CheckStartOfSwordDuel()
		{
			if (!base.Mission.IsMissionEnding)
			{
				if (!this.IsSwordDuelStarted)
				{
					if (base.Mission.Agents.Count <= 0)
					{
						return;
					}
					if (base.Mission.Agents.Count((Agent a) => a.IsMount) >= 2)
					{
						return;
					}
					this.IsSwordDuelStarted = true;
					this.RemoveBarriers();
					base.Mission.Scene.SetAbilityOfFacesWithId(2, true);
					using (List<Agent>.Enumerator enumerator = base.Mission.Agents.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Agent agent = enumerator.Current;
							if (agent.IsHuman)
							{
								JoustingAgentController controller = agent.GetController<JoustingAgentController>();
								controller.State = JoustingAgentController.JoustingAgentState.SwordDuel;
								controller.PrepareAgentToSwordDuel();
							}
						}
						return;
					}
				}
				foreach (Agent agent2 in base.Mission.Agents)
				{
					if (agent2.IsHuman)
					{
						JoustingAgentController controller2 = agent2.GetController<JoustingAgentController>();
						controller2.State = JoustingAgentController.JoustingAgentState.SwordDuel;
						if (controller2.PrepareEquipmentsAfterDismount && agent2.MountAgent == null)
						{
							CharacterObject characterObject = (CharacterObject)agent2.Character;
							controller2.PrepareEquipmentsForSwordDuel();
							agent2.DisableScriptedMovement();
							if (characterObject == CharacterObject.PlayerCharacter)
							{
								agent2.Controller = Agent.ControllerType.Player;
							}
						}
					}
				}
			}
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000B200 File Offset: 0x00009400
		private void RemoveBarriers()
		{
			foreach (GameEntity gameEntity in base.Mission.Scene.FindEntitiesWithTag("jousting_barrier").ToList<GameEntity>())
			{
				gameEntity.Remove(95);
			}
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000B268 File Offset: 0x00009468
		public override void OnAgentHit(Agent affectedAgent, Agent affectorAgent, in MissionWeapon attackerWeapon, in Blow blow, in AttackCollisionData attackCollisionData)
		{
			if (!base.Mission.IsMissionEnding && !this.IsSwordDuelStarted && affectedAgent.IsHuman && affectorAgent != null && affectorAgent.IsHuman && affectedAgent != affectorAgent)
			{
				JoustingAgentController controller = affectorAgent.GetController<JoustingAgentController>();
				JoustingAgentController controller2 = affectedAgent.GetController<JoustingAgentController>();
				if (this.IsAgentInTheTrack(affectorAgent, true) && controller2.IsRiding() && controller.IsRiding())
				{
					this._match.GetParticipant(affectorAgent.Origin.UniqueSeed).AddScore(1);
					controller.Score++;
					if (controller.Score >= 3)
					{
						this._winnerTeam = affectorAgent.Team;
						TournamentJoustingMissionController.JoustingEventDelegate victoryAchieved = this.VictoryAchieved;
						if (victoryAchieved == null)
						{
							return;
						}
						victoryAchieved(affectorAgent, affectedAgent);
						return;
					}
					else
					{
						TournamentJoustingMissionController.JoustingEventDelegate pointGanied = this.PointGanied;
						if (pointGanied == null)
						{
							return;
						}
						pointGanied(affectorAgent, affectedAgent);
						return;
					}
				}
				else
				{
					this._match.GetParticipant(affectorAgent.Origin.UniqueSeed).AddScore(-100);
					this._winnerTeam = affectedAgent.Team;
					MBTextManager.SetTextVariable("OPPONENT_GENDER", affectorAgent.Character.IsFemale ? "0" : "1", false);
					TournamentJoustingMissionController.JoustingEventDelegate disqualified = this.Disqualified;
					if (disqualified == null)
					{
						return;
					}
					disqualified(affectorAgent, affectedAgent);
				}
			}
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000B3A4 File Offset: 0x000095A4
		public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow killingBlow)
		{
			if (!base.Mission.IsMissionEnding && affectedAgent.IsHuman && affectorAgent != null && affectorAgent.IsHuman && affectedAgent != affectorAgent)
			{
				if (this.IsAgentInTheTrack(affectorAgent, true) || this.IsSwordDuelStarted)
				{
					this._match.GetParticipant(affectorAgent.Origin.UniqueSeed).AddScore(100);
					this._winnerTeam = affectorAgent.Team;
					if (this.Unconscious != null)
					{
						this.Unconscious(affectorAgent, affectedAgent);
						return;
					}
				}
				else
				{
					this._match.GetParticipant(affectorAgent.Origin.UniqueSeed).AddScore(-100);
					this._winnerTeam = affectedAgent.Team;
					MBTextManager.SetTextVariable("OPPONENT_GENDER", affectorAgent.Character.IsFemale ? "0" : "1", false);
					if (this.Disqualified != null)
					{
						this.Disqualified(affectorAgent, affectedAgent);
					}
				}
			}
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000B499 File Offset: 0x00009699
		public void OnJoustingAgentStateChanged(Agent agent, JoustingAgentController.JoustingAgentState state)
		{
			if (this.AgentStateChanged != null)
			{
				this.AgentStateChanged(agent, state);
			}
		}

		// Token: 0x04000089 RID: 137
		private Team _winnerTeam;

		// Token: 0x0400008A RID: 138
		public List<GameEntity> RegionBoxList;

		// Token: 0x0400008B RID: 139
		public List<GameEntity> RegionExitBoxList;

		// Token: 0x0400008C RID: 140
		public List<MatrixFrame> CornerBackStartList;

		// Token: 0x0400008D RID: 141
		public List<GameEntity> CornerStartList;

		// Token: 0x0400008E RID: 142
		public List<MatrixFrame> CornerMiddleList;

		// Token: 0x0400008F RID: 143
		public List<MatrixFrame> CornerFinishList;

		// Token: 0x04000090 RID: 144
		public bool IsSwordDuelStarted;

		// Token: 0x04000091 RID: 145
		private TournamentMatch _match;

		// Token: 0x04000092 RID: 146
		private BasicMissionTimer _endTimer;

		// Token: 0x04000093 RID: 147
		private bool _isSimulated;

		// Token: 0x04000094 RID: 148
		private CultureObject _culture;

		// Token: 0x04000095 RID: 149
		private readonly Equipment _joustingEquipment;

		// Token: 0x0200010F RID: 271
		// (Invoke) Token: 0x06000B82 RID: 2946
		public delegate void JoustingEventDelegate(Agent affectedAgent, Agent affectorAgent);

		// Token: 0x02000110 RID: 272
		// (Invoke) Token: 0x06000B86 RID: 2950
		public delegate void JoustingAgentStateChangedEventDelegate(Agent agent, JoustingAgentController.JoustingAgentState state);
	}
}
