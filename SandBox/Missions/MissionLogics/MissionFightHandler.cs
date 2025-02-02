using System;
using System.Collections.Generic;
using SandBox.Missions.AgentBehaviors;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.MissionLogics
{
	// Token: 0x0200005B RID: 91
	public class MissionFightHandler : MissionLogic
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060003AE RID: 942 RVA: 0x00019CAB File Offset: 0x00017EAB
		private static MissionFightHandler _current
		{
			get
			{
				return Mission.Current.GetMissionBehavior<MissionFightHandler>();
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060003AF RID: 943 RVA: 0x00019CB7 File Offset: 0x00017EB7
		public IEnumerable<Agent> PlayerSideAgents
		{
			get
			{
				return this._playerSideAgents.AsReadOnly();
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x00019CC4 File Offset: 0x00017EC4
		public IEnumerable<Agent> OpponentSideAgents
		{
			get
			{
				return this._opponentSideAgents.AsReadOnly();
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x00019CD1 File Offset: 0x00017ED1
		public bool IsPlayerSideWon
		{
			get
			{
				return this._isPlayerSideWon;
			}
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00019CD9 File Offset: 0x00017ED9
		public override void OnBehaviorInitialize()
		{
			base.Mission.IsAgentInteractionAllowed_AdditionalCondition += this.IsAgentInteractionAllowed_AdditionalCondition;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00019CF2 File Offset: 0x00017EF2
		public override void EarlyStart()
		{
			this._playerSideAgents = new List<Agent>();
			this._opponentSideAgents = new List<Agent>();
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00019D0A File Offset: 0x00017F0A
		public override void AfterStart()
		{
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00019D0C File Offset: 0x00017F0C
		public override void OnMissionTick(float dt)
		{
			if (this._finishTimer != null && this._finishTimer.ElapsedTime > 5f)
			{
				this._finishTimer = null;
				this.EndFight();
				this._prepareTimer = new BasicMissionTimer();
			}
			if (this._prepareTimer != null && this._prepareTimer.ElapsedTime > 3f)
			{
				this._prepareTimer = null;
			}
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00019D6C File Offset: 0x00017F6C
		public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow killingBlow)
		{
			if (this._state != MissionFightHandler.State.Fighting)
			{
				return;
			}
			if (affectedAgent == Agent.Main)
			{
				Mission.Current.NextCheckTimeEndMission += 8f;
			}
			if (affectorAgent != null && this._playerSideAgents.Contains(affectedAgent))
			{
				this._playerSideAgents.Remove(affectedAgent);
				if (this._playerSideAgents.Count == 0)
				{
					this._isPlayerSideWon = false;
					this.ResetScriptedBehaviors();
					this._finishTimer = new BasicMissionTimer();
					return;
				}
			}
			else if (affectorAgent != null && this._opponentSideAgents.Contains(affectedAgent))
			{
				this._opponentSideAgents.Remove(affectedAgent);
				if (this._opponentSideAgents.Count == 0)
				{
					this._isPlayerSideWon = true;
					this.ResetScriptedBehaviors();
					this._finishTimer = new BasicMissionTimer();
				}
			}
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00019E28 File Offset: 0x00018028
		public void StartCustomFight(List<Agent> playerSideAgents, List<Agent> opponentSideAgents, bool dropWeapons, bool isItemUseDisabled, MissionFightHandler.OnFightEndDelegate onFightEndDelegate)
		{
			this._state = MissionFightHandler.State.Fighting;
			this._opponentSideAgents = opponentSideAgents;
			this._playerSideAgents = playerSideAgents;
			this._playerSideAgentsOldTeamData = new Dictionary<Agent, Team>();
			this._opponentSideAgentsOldTeamData = new Dictionary<Agent, Team>();
			MissionFightHandler._onFightEnd = onFightEndDelegate;
			Mission.Current.MainAgent.IsItemUseDisabled = isItemUseDisabled;
			foreach (Agent agent in this._opponentSideAgents)
			{
				if (dropWeapons)
				{
					this.DropAllWeapons(agent);
				}
				this._opponentSideAgentsOldTeamData.Add(agent, agent.Team);
				this.ForceAgentForFight(agent);
			}
			foreach (Agent agent2 in this._playerSideAgents)
			{
				if (dropWeapons)
				{
					this.DropAllWeapons(agent2);
				}
				this._playerSideAgentsOldTeamData.Add(agent2, agent2.Team);
				this.ForceAgentForFight(agent2);
			}
			this.SetTeamsForFightAndDuel();
			this._oldMissionMode = Mission.Current.Mode;
			Mission.Current.SetMissionMode(MissionMode.Battle, false);
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00019F5C File Offset: 0x0001815C
		public override InquiryData OnEndMissionRequest(out bool canPlayerLeave)
		{
			canPlayerLeave = true;
			if (this._state == MissionFightHandler.State.Fighting && (this._opponentSideAgents.Count > 0 || this._playerSideAgents.Count > 0))
			{
				MBInformationManager.AddQuickInformation(new TextObject("{=Fpk3BUBs}Your duel has not ended yet!", null), 0, null, "");
				canPlayerLeave = false;
			}
			return null;
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00019FAC File Offset: 0x000181AC
		private void ForceAgentForFight(Agent agent)
		{
			if (agent.GetComponent<CampaignAgentComponent>().AgentNavigator != null)
			{
				AlarmedBehaviorGroup behaviorGroup = agent.GetComponent<CampaignAgentComponent>().AgentNavigator.GetBehaviorGroup<AlarmedBehaviorGroup>();
				behaviorGroup.DisableCalmDown = true;
				behaviorGroup.AddBehavior<FightBehavior>();
				behaviorGroup.SetScriptedBehavior<FightBehavior>();
			}
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00019FDE File Offset: 0x000181DE
		protected override void OnEndMission()
		{
			base.Mission.IsAgentInteractionAllowed_AdditionalCondition -= this.IsAgentInteractionAllowed_AdditionalCondition;
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00019FF8 File Offset: 0x000181F8
		private void SetTeamsForFightAndDuel()
		{
			Mission.Current.PlayerEnemyTeam.SetIsEnemyOf(Mission.Current.PlayerTeam, true);
			foreach (Agent agent in this._playerSideAgents)
			{
				if (agent.IsHuman)
				{
					if (agent.IsAIControlled)
					{
						agent.SetWatchState(Agent.WatchState.Alarmed);
					}
					agent.SetTeam(Mission.Current.PlayerTeam, true);
				}
			}
			foreach (Agent agent2 in this._opponentSideAgents)
			{
				if (agent2.IsHuman)
				{
					if (agent2.IsAIControlled)
					{
						agent2.SetWatchState(Agent.WatchState.Alarmed);
					}
					agent2.SetTeam(Mission.Current.PlayerEnemyTeam, true);
				}
			}
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0001A0EC File Offset: 0x000182EC
		private void ResetTeamsForFightAndDuel()
		{
			foreach (Agent agent in this._playerSideAgents)
			{
				if (agent.IsAIControlled)
				{
					agent.ResetEnemyCaches();
					agent.InvalidateTargetAgent();
					agent.InvalidateAIWeaponSelections();
					agent.SetWatchState(Agent.WatchState.Patrolling);
				}
				agent.SetTeam(new Team(this._playerSideAgentsOldTeamData[agent].MBTeam, BattleSideEnum.None, base.Mission, uint.MaxValue, uint.MaxValue, null), true);
			}
			foreach (Agent agent2 in this._opponentSideAgents)
			{
				if (agent2.IsAIControlled)
				{
					agent2.ResetEnemyCaches();
					agent2.InvalidateTargetAgent();
					agent2.InvalidateAIWeaponSelections();
					agent2.SetWatchState(Agent.WatchState.Patrolling);
				}
				agent2.SetTeam(new Team(this._opponentSideAgentsOldTeamData[agent2].MBTeam, BattleSideEnum.None, base.Mission, uint.MaxValue, uint.MaxValue, null), true);
			}
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0001A204 File Offset: 0x00018404
		private bool IsAgentInteractionAllowed_AdditionalCondition()
		{
			return this._state != MissionFightHandler.State.Fighting;
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0001A214 File Offset: 0x00018414
		public static Agent GetAgentToSpectate()
		{
			MissionFightHandler current = MissionFightHandler._current;
			if (current._playerSideAgents.Count > 0)
			{
				return current._playerSideAgents[0];
			}
			if (current._opponentSideAgents.Count > 0)
			{
				return current._opponentSideAgents[0];
			}
			return null;
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0001A260 File Offset: 0x00018460
		private void DropAllWeapons(Agent agent)
		{
			for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.NumAllWeaponSlots; equipmentIndex++)
			{
				if (!agent.Equipment[equipmentIndex].IsEmpty)
				{
					agent.DropItem(equipmentIndex, WeaponClass.Undefined);
				}
			}
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0001A298 File Offset: 0x00018498
		private void ResetScriptedBehaviors()
		{
			foreach (Agent agent in this._playerSideAgents)
			{
				if (agent.IsActive() && agent.GetComponent<CampaignAgentComponent>().AgentNavigator != null)
				{
					agent.GetComponent<CampaignAgentComponent>().AgentNavigator.GetBehaviorGroup<AlarmedBehaviorGroup>().DisableScriptedBehavior();
				}
			}
			foreach (Agent agent2 in this._opponentSideAgents)
			{
				if (agent2.IsActive() && agent2.GetComponent<CampaignAgentComponent>().AgentNavigator != null)
				{
					agent2.GetComponent<CampaignAgentComponent>().AgentNavigator.GetBehaviorGroup<AlarmedBehaviorGroup>().DisableScriptedBehavior();
				}
			}
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0001A374 File Offset: 0x00018574
		public void EndFight()
		{
			this.ResetTeamsForFightAndDuel();
			this._state = MissionFightHandler.State.FightEnded;
			foreach (Agent agent in this._playerSideAgents)
			{
				agent.TryToSheathWeaponInHand(Agent.HandIndex.MainHand, Agent.WeaponWieldActionType.WithAnimationUninterruptible);
				agent.TryToSheathWeaponInHand(Agent.HandIndex.OffHand, Agent.WeaponWieldActionType.WithAnimationUninterruptible);
			}
			foreach (Agent agent2 in this._opponentSideAgents)
			{
				agent2.TryToSheathWeaponInHand(Agent.HandIndex.MainHand, Agent.WeaponWieldActionType.WithAnimationUninterruptible);
				agent2.TryToSheathWeaponInHand(Agent.HandIndex.OffHand, Agent.WeaponWieldActionType.WithAnimationUninterruptible);
			}
			this._playerSideAgents.Clear();
			this._opponentSideAgents.Clear();
			if (Mission.Current.MainAgent != null)
			{
				Mission.Current.MainAgent.IsItemUseDisabled = false;
			}
			if (this._oldMissionMode == MissionMode.Conversation && !Campaign.Current.ConversationManager.IsConversationFlowActive)
			{
				this._oldMissionMode = MissionMode.StartUp;
			}
			Mission.Current.SetMissionMode(this._oldMissionMode, false);
			if (MissionFightHandler._onFightEnd != null)
			{
				MissionFightHandler._onFightEnd(this._isPlayerSideWon);
				MissionFightHandler._onFightEnd = null;
			}
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0001A4A8 File Offset: 0x000186A8
		public bool IsThereActiveFight()
		{
			return this._state == MissionFightHandler.State.Fighting;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0001A4B4 File Offset: 0x000186B4
		public void AddAgentToSide(Agent agent, bool isPlayerSide)
		{
			if (!this.IsThereActiveFight() || this._playerSideAgents.Contains(agent) || this._opponentSideAgents.Contains(agent))
			{
				return;
			}
			if (isPlayerSide)
			{
				agent.SetTeam(Mission.Current.PlayerTeam, true);
				this._playerSideAgents.Add(agent);
				return;
			}
			agent.SetTeam(Mission.Current.PlayerEnemyTeam, true);
			this._opponentSideAgents.Add(agent);
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0001A524 File Offset: 0x00018724
		public IEnumerable<Agent> GetDangerSources(Agent ownerAgent)
		{
			if (!(ownerAgent.Character is CharacterObject))
			{
				Debug.FailedAssert("false", "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox\\Missions\\MissionLogics\\MissionFightHandler.cs", "GetDangerSources", 370);
				return new List<Agent>();
			}
			if (this.IsThereActiveFight() && !MissionFightHandler.IsAgentAggressive(ownerAgent) && Agent.Main != null)
			{
				return new List<Agent>
				{
					Agent.Main
				};
			}
			return new List<Agent>();
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0001A58C File Offset: 0x0001878C
		public static bool IsAgentAggressive(Agent agent)
		{
			CharacterObject characterObject = agent.Character as CharacterObject;
			return agent.HasWeapon() || (characterObject != null && (characterObject.Occupation == Occupation.Mercenary || MissionFightHandler.IsAgentVillian(characterObject) || MissionFightHandler.IsAgentJusticeWarrior(characterObject)));
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0001A5CD File Offset: 0x000187CD
		public static bool IsAgentJusticeWarrior(CharacterObject character)
		{
			return character.Occupation == Occupation.Soldier || character.Occupation == Occupation.Guard || character.Occupation == Occupation.PrisonGuard;
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0001A5EE File Offset: 0x000187EE
		public static bool IsAgentVillian(CharacterObject character)
		{
			return character.Occupation == Occupation.Gangster || character.Occupation == Occupation.GangLeader || character.Occupation == Occupation.Bandit;
		}

		// Token: 0x040001B4 RID: 436
		private static MissionFightHandler.OnFightEndDelegate _onFightEnd;

		// Token: 0x040001B5 RID: 437
		private List<Agent> _playerSideAgents;

		// Token: 0x040001B6 RID: 438
		private List<Agent> _opponentSideAgents;

		// Token: 0x040001B7 RID: 439
		private Dictionary<Agent, Team> _playerSideAgentsOldTeamData;

		// Token: 0x040001B8 RID: 440
		private Dictionary<Agent, Team> _opponentSideAgentsOldTeamData;

		// Token: 0x040001B9 RID: 441
		private MissionFightHandler.State _state;

		// Token: 0x040001BA RID: 442
		private BasicMissionTimer _finishTimer;

		// Token: 0x040001BB RID: 443
		private BasicMissionTimer _prepareTimer;

		// Token: 0x040001BC RID: 444
		private bool _isPlayerSideWon;

		// Token: 0x040001BD RID: 445
		private MissionMode _oldMissionMode;

		// Token: 0x0200013E RID: 318
		private enum State
		{
			// Token: 0x04000588 RID: 1416
			NoFight,
			// Token: 0x04000589 RID: 1417
			Fighting,
			// Token: 0x0400058A RID: 1418
			FightEnded
		}

		// Token: 0x0200013F RID: 319
		// (Invoke) Token: 0x06000C08 RID: 3080
		public delegate void OnFightEndDelegate(bool isPlayerSideWon);
	}
}
