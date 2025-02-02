using System;
using System.Collections.Generic;
using Helpers;
using SandBox.Conversation;
using SandBox.Conversation.MissionLogics;
using SandBox.Missions.AgentBehaviors;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.CampaignSystem.Siege;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.MissionLogics
{
	// Token: 0x0200004A RID: 74
	public class CampaignMissionComponent : MissionLogic, ICampaignMission
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0001197F File Offset: 0x0000FB7F
		public GameState State
		{
			get
			{
				return this._state;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x00011987 File Offset: 0x0000FB87
		// (set) Token: 0x060002BA RID: 698 RVA: 0x0001198F File Offset: 0x0000FB8F
		public IMissionTroopSupplier AgentSupplier { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060002BB RID: 699 RVA: 0x00011998 File Offset: 0x0000FB98
		// (set) Token: 0x060002BC RID: 700 RVA: 0x000119A0 File Offset: 0x0000FBA0
		public Location Location { get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060002BD RID: 701 RVA: 0x000119A9 File Offset: 0x0000FBA9
		// (set) Token: 0x060002BE RID: 702 RVA: 0x000119B1 File Offset: 0x0000FBB1
		public Alley LastVisitedAlley { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060002BF RID: 703 RVA: 0x000119BA File Offset: 0x0000FBBA
		MissionMode ICampaignMission.Mode
		{
			get
			{
				return base.Mission.Mode;
			}
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x000119C7 File Offset: 0x0000FBC7
		void ICampaignMission.SetMissionMode(MissionMode newMode, bool atStart)
		{
			base.Mission.SetMissionMode(newMode, atStart);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x000119D8 File Offset: 0x0000FBD8
		public override void OnAgentCreated(Agent agent)
		{
			base.OnAgentCreated(agent);
			agent.AddComponent(new CampaignAgentComponent(agent));
			CharacterObject characterObject = (CharacterObject)agent.Character;
			if (((characterObject != null) ? characterObject.HeroObject : null) != null && characterObject.HeroObject.IsPlayerCompanion)
			{
				agent.AgentRole = new TextObject("{=kPTp6TPT}({AGENT_ROLE})", null);
				agent.AgentRole.SetTextVariable("AGENT_ROLE", GameTexts.FindText("str_companion", null));
			}
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00011A4C File Offset: 0x0000FC4C
		public override void OnPreDisplayMissionTick(float dt)
		{
			base.OnPreDisplayMissionTick(dt);
			if (this._soundEvent != null && !this._soundEvent.IsPlaying())
			{
				this.RemovePreviousAgentsSoundEvent();
				this._soundEvent.Stop();
				this._soundEvent = null;
			}
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00011A82 File Offset: 0x0000FC82
		public override void OnMissionTick(float dt)
		{
			base.OnMissionTick(dt);
			if (Campaign.Current != null)
			{
				CampaignEventDispatcher.Instance.MissionTick(dt);
			}
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00011AA0 File Offset: 0x0000FCA0
		protected override void OnObjectDisabled(DestructableComponent missionObject)
		{
			SiegeWeapon firstScriptOfType = missionObject.GameEntity.GetFirstScriptOfType<SiegeWeapon>();
			if (firstScriptOfType != null && Campaign.Current != null && Campaign.Current.GameMode == CampaignGameMode.Campaign)
			{
				CampaignSiegeStateHandler missionBehavior = Mission.Current.GetMissionBehavior<CampaignSiegeStateHandler>();
				if (missionBehavior != null && missionBehavior.IsSallyOut)
				{
					ISiegeEventSide siegeEventSide = missionBehavior.Settlement.SiegeEvent.GetSiegeEventSide(firstScriptOfType.Side);
					siegeEventSide.SiegeEvent.BreakSiegeEngine(siegeEventSide, firstScriptOfType.GetSiegeEngineType());
				}
			}
			base.OnObjectDisabled(missionObject);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00011B16 File Offset: 0x0000FD16
		public override void EarlyStart()
		{
			this._state = (Game.Current.GameStateManager.ActiveState as MissionState);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00011B32 File Offset: 0x0000FD32
		public override void OnCreated()
		{
			CampaignMission.Current = this;
			this._isMainAgentAnimationSet = false;
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00011B41 File Offset: 0x0000FD41
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			CampaignEventDispatcher.Instance.OnMissionStarted(base.Mission);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00011B59 File Offset: 0x0000FD59
		public override void AfterStart()
		{
			base.AfterStart();
			CampaignEventDispatcher.Instance.OnAfterMissionStarted(base.Mission);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00011B74 File Offset: 0x0000FD74
		private static void SimulateRunningAwayAgents()
		{
			foreach (Agent agent in Mission.Current.Agents)
			{
				PartyBase ownerParty = agent.GetComponent<CampaignAgentComponent>().OwnerParty;
				if (ownerParty != null && !agent.IsHero && agent.IsRunningAway && MBRandom.RandomFloat < 0.5f)
				{
					CharacterObject character = (CharacterObject)agent.Character;
					ownerParty.MemberRoster.AddToCounts(character, -1, false, 0, 0, true, -1);
				}
			}
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00011C10 File Offset: 0x0000FE10
		public override void OnMissionResultReady(MissionResult missionResult)
		{
			if (Campaign.Current.GameMode == CampaignGameMode.Campaign && PlayerEncounter.IsActive && PlayerEncounter.Battle != null)
			{
				if (missionResult.PlayerVictory)
				{
					PlayerEncounter.SetPlayerVictorious();
				}
				else if (missionResult.BattleState == BattleState.DefenderPullBack)
				{
					PlayerEncounter.SetPlayerSiegeContinueWithDefenderPullBack();
				}
				MissionResult missionResult2 = base.Mission.MissionResult;
				PlayerEncounter.CampaignBattleResult = CampaignBattleResult.GetResult((missionResult2 != null) ? missionResult2.BattleState : BattleState.None, missionResult.EnemyRetreated);
			}
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00011C7C File Offset: 0x0000FE7C
		protected override void OnEndMission()
		{
			if (Campaign.Current.GameMode == CampaignGameMode.Campaign)
			{
				if (PlayerEncounter.Battle != null && (PlayerEncounter.Battle.IsSiegeAssault || PlayerEncounter.Battle.IsSiegeAmbush) && (Mission.Current.MissionTeamAIType == Mission.MissionTeamAITypeEnum.Siege || Mission.Current.MissionTeamAIType == Mission.MissionTeamAITypeEnum.SallyOut))
				{
					IEnumerable<IMissionSiegeWeapon> defenderMissionSiegeEngineData;
					IEnumerable<IMissionSiegeWeapon> attackerMissionSiegeEngineData;
					Mission.Current.GetMissionBehavior<MissionSiegeEnginesLogic>().GetMissionSiegeWeapons(out defenderMissionSiegeEngineData, out attackerMissionSiegeEngineData);
					PlayerEncounter.Battle.GetLeaderParty(BattleSideEnum.Attacker).SiegeEvent.SetSiegeEngineStatesAfterSiegeMission(attackerMissionSiegeEngineData, defenderMissionSiegeEngineData);
				}
				if (this._soundEvent != null)
				{
					this.RemovePreviousAgentsSoundEvent();
					this._soundEvent.Stop();
					this._soundEvent = null;
				}
			}
			CampaignEventDispatcher.Instance.OnMissionEnded(base.Mission);
			CampaignMission.Current = null;
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00011D34 File Offset: 0x0000FF34
		void ICampaignMission.OnCloseEncounterMenu()
		{
			if (base.Mission.Mode == MissionMode.Conversation)
			{
				Campaign.Current.ConversationManager.EndConversation();
				if (Game.Current.GameStateManager.ActiveState is MissionState)
				{
					Game.Current.GameStateManager.PopState(0);
				}
			}
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00011D84 File Offset: 0x0000FF84
		bool ICampaignMission.AgentLookingAtAgent(IAgent agent1, IAgent agent2)
		{
			return base.Mission.AgentLookingAtAgent((Agent)agent1, (Agent)agent2);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00011DA0 File Offset: 0x0000FFA0
		void ICampaignMission.OnCharacterLocationChanged(LocationCharacter locationCharacter, Location fromLocation, Location toLocation)
		{
			MissionAgentHandler missionBehavior = base.Mission.GetMissionBehavior<MissionAgentHandler>();
			if (toLocation == null)
			{
				missionBehavior.FadeoutExitingLocationCharacter(locationCharacter);
				return;
			}
			missionBehavior.SpawnEnteringLocationCharacter(locationCharacter, fromLocation);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00011DCC File Offset: 0x0000FFCC
		void ICampaignMission.OnProcessSentence()
		{
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00011DCE File Offset: 0x0000FFCE
		void ICampaignMission.OnConversationContinue()
		{
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00011DD0 File Offset: 0x0000FFD0
		bool ICampaignMission.CheckIfAgentCanFollow(IAgent agent)
		{
			AgentNavigator agentNavigator = ((Agent)agent).GetComponent<CampaignAgentComponent>().AgentNavigator;
			if (agentNavigator != null)
			{
				DailyBehaviorGroup behaviorGroup = agentNavigator.GetBehaviorGroup<DailyBehaviorGroup>();
				return behaviorGroup != null && behaviorGroup.GetBehavior<FollowAgentBehavior>() == null;
			}
			return false;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00011E08 File Offset: 0x00010008
		void ICampaignMission.AddAgentFollowing(IAgent agent)
		{
			Agent agent2 = (Agent)agent;
			if (agent2.GetComponent<CampaignAgentComponent>().AgentNavigator != null)
			{
				DailyBehaviorGroup behaviorGroup = agent2.GetComponent<CampaignAgentComponent>().AgentNavigator.GetBehaviorGroup<DailyBehaviorGroup>();
				behaviorGroup.AddBehavior<FollowAgentBehavior>();
				behaviorGroup.SetScriptedBehavior<FollowAgentBehavior>();
			}
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00011E48 File Offset: 0x00010048
		bool ICampaignMission.CheckIfAgentCanUnFollow(IAgent agent)
		{
			Agent agent2 = (Agent)agent;
			if (agent2.GetComponent<CampaignAgentComponent>().AgentNavigator != null)
			{
				DailyBehaviorGroup behaviorGroup = agent2.GetComponent<CampaignAgentComponent>().AgentNavigator.GetBehaviorGroup<DailyBehaviorGroup>();
				return behaviorGroup != null && behaviorGroup.GetBehavior<FollowAgentBehavior>() != null;
			}
			return false;
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00011E8C File Offset: 0x0001008C
		void ICampaignMission.RemoveAgentFollowing(IAgent agent)
		{
			Agent agent2 = (Agent)agent;
			if (agent2.GetComponent<CampaignAgentComponent>().AgentNavigator != null)
			{
				agent2.GetComponent<CampaignAgentComponent>().AgentNavigator.GetBehaviorGroup<DailyBehaviorGroup>().RemoveBehavior<FollowAgentBehavior>();
			}
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00011EC2 File Offset: 0x000100C2
		void ICampaignMission.EndMission()
		{
			base.Mission.EndMission();
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00011ED0 File Offset: 0x000100D0
		private string GetIdleAnimationId(Agent agent, string selectedId, bool startingConversation)
		{
			Agent.ActionCodeType currentActionType = agent.GetCurrentActionType(0);
			if (currentActionType == Agent.ActionCodeType.Sit)
			{
				return "sit";
			}
			if (currentActionType == Agent.ActionCodeType.SitOnTheFloor)
			{
				return "sit_floor";
			}
			if (currentActionType == Agent.ActionCodeType.SitOnAThrone)
			{
				return "sit_throne";
			}
			if (agent.MountAgent != null)
			{
				ValueTuple<string, ConversationAnimData> animDataForRiderAndMountAgents = this.GetAnimDataForRiderAndMountAgents(agent);
				this.SetMountAgentAnimation(agent.MountAgent, animDataForRiderAndMountAgents.Item2, startingConversation);
				return animDataForRiderAndMountAgents.Item1;
			}
			if (agent == Agent.Main)
			{
				return "normal";
			}
			if (startingConversation)
			{
				return CharacterHelper.GetStandingBodyIdle((CharacterObject)agent.Character);
			}
			return selectedId;
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00011F54 File Offset: 0x00010154
		private ValueTuple<string, ConversationAnimData> GetAnimDataForRiderAndMountAgents(Agent riderAgent)
		{
			bool flag = false;
			string item = "";
			bool flag2 = false;
			ConversationAnimData item2 = null;
			foreach (KeyValuePair<string, ConversationAnimData> keyValuePair in Campaign.Current.ConversationManager.ConversationAnimationManager.ConversationAnims)
			{
				if (keyValuePair.Value != null)
				{
					if (keyValuePair.Value.FamilyType == riderAgent.MountAgent.Monster.FamilyType)
					{
						item2 = keyValuePair.Value;
						flag2 = true;
					}
					else if (keyValuePair.Value.FamilyType == riderAgent.Monster.FamilyType && keyValuePair.Value.MountFamilyType == riderAgent.MountAgent.Monster.FamilyType)
					{
						item = keyValuePair.Key;
						flag = true;
					}
					if (flag2 && flag)
					{
						break;
					}
				}
			}
			return new ValueTuple<string, ConversationAnimData>(item, item2);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00012044 File Offset: 0x00010244
		private int GetActionChannelNoForConversation(Agent agent)
		{
			if (agent.IsSitting())
			{
				return 0;
			}
			if (agent.MountAgent != null)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0001205C File Offset: 0x0001025C
		private void SetMountAgentAnimation(IAgent agent, ConversationAnimData mountAnimData, bool startingConversation)
		{
			Agent agent2 = (Agent)agent;
			if (mountAnimData != null)
			{
				if (startingConversation)
				{
					this._conversationAgents.Add(new CampaignMissionComponent.AgentConversationState(agent2));
				}
				ActionIndexCache action = string.IsNullOrEmpty(mountAnimData.IdleAnimStart) ? ActionIndexCache.Create(mountAnimData.IdleAnimLoop) : ActionIndexCache.Create(mountAnimData.IdleAnimStart);
				this.SetConversationAgentActionAtChannel(agent2, action, this.GetActionChannelNoForConversation(agent2), false, false);
			}
		}

		// Token: 0x060002DA RID: 730 RVA: 0x000120C0 File Offset: 0x000102C0
		void ICampaignMission.OnConversationStart(IAgent iAgent, bool setActionsInstantly)
		{
			((Agent)iAgent).AgentVisuals.SetAgentLodZeroOrMax(true);
			Agent.Main.AgentVisuals.SetAgentLodZeroOrMax(true);
			if (!this._isMainAgentAnimationSet)
			{
				this._isMainAgentAnimationSet = true;
				this.StartConversationAnimations(Agent.Main, setActionsInstantly);
			}
			this.StartConversationAnimations(iAgent, setActionsInstantly);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00012114 File Offset: 0x00010314
		private void StartConversationAnimations(IAgent iAgent, bool setActionsInstantly)
		{
			Agent agent = (Agent)iAgent;
			this._conversationAgents.Add(new CampaignMissionComponent.AgentConversationState(agent));
			string idleAnimationId = this.GetIdleAnimationId(agent, "", true);
			string defaultFaceIdle = CharacterHelper.GetDefaultFaceIdle((CharacterObject)agent.Character);
			int actionChannelNoForConversation = this.GetActionChannelNoForConversation(agent);
			ConversationAnimData conversationAnimData;
			if (Campaign.Current.ConversationManager.ConversationAnimationManager.ConversationAnims.TryGetValue(idleAnimationId, out conversationAnimData))
			{
				ActionIndexCache action = string.IsNullOrEmpty(conversationAnimData.IdleAnimStart) ? ActionIndexCache.Create(conversationAnimData.IdleAnimLoop) : ActionIndexCache.Create(conversationAnimData.IdleAnimStart);
				this.SetConversationAgentActionAtChannel(agent, action, actionChannelNoForConversation, setActionsInstantly, false);
				this.SetFaceIdle(agent, defaultFaceIdle);
			}
			if (agent.IsUsingGameObject)
			{
				agent.CurrentlyUsedGameObject.OnUserConversationStart();
			}
		}

		// Token: 0x060002DC RID: 732 RVA: 0x000121D0 File Offset: 0x000103D0
		private void EndConversationAnimations(IAgent iAgent)
		{
			Agent agent = (Agent)iAgent;
			if (agent.IsHuman)
			{
				agent.SetAgentFacialAnimation(Agent.FacialAnimChannel.High, "", false);
				agent.SetAgentFacialAnimation(Agent.FacialAnimChannel.Mid, "", false);
				if (agent.HasMount)
				{
					this.EndConversationAnimations(agent.MountAgent);
				}
			}
			int num = -1;
			int count = this._conversationAgents.Count;
			for (int i = 0; i < count; i++)
			{
				CampaignMissionComponent.AgentConversationState agentConversationState = this._conversationAgents[i];
				if (agentConversationState.Agent == agent)
				{
					for (int j = 0; j < 2; j++)
					{
						if (agentConversationState.IsChannelModified(j))
						{
							agent.SetActionChannel(j, ActionIndexCache.act_none, false, (ulong)((long)agent.GetCurrentActionPriority(j)), 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true);
						}
					}
					if (agent.IsUsingGameObject)
					{
						agent.CurrentlyUsedGameObject.OnUserConversationEnd();
					}
					num = i;
					break;
				}
			}
			if (num != -1)
			{
				this._conversationAgents.RemoveAt(num);
			}
		}

		// Token: 0x060002DD RID: 733 RVA: 0x000122D0 File Offset: 0x000104D0
		void ICampaignMission.OnConversationPlay(string idleActionId, string idleFaceAnimId, string reactionId, string reactionFaceAnimId, string soundPath)
		{
			this._currentAgent = (Agent)Campaign.Current.ConversationManager.SpeakerAgent;
			this.RemovePreviousAgentsSoundEvent();
			this.StopPreviousSound();
			string idleAnimationId = this.GetIdleAnimationId(this._currentAgent, idleActionId, false);
			ConversationAnimData conversationAnimData;
			if (!string.IsNullOrEmpty(idleAnimationId) && Campaign.Current.ConversationManager.ConversationAnimationManager.ConversationAnims.TryGetValue(idleAnimationId, out conversationAnimData))
			{
				if (!string.IsNullOrEmpty(reactionId))
				{
					this.SetConversationAgentActionAtChannel(this._currentAgent, ActionIndexCache.Create(conversationAnimData.Reactions[reactionId]), 0, false, true);
				}
				else
				{
					ActionIndexCache action = string.IsNullOrEmpty(conversationAnimData.IdleAnimStart) ? ActionIndexCache.Create(conversationAnimData.IdleAnimLoop) : ActionIndexCache.Create(conversationAnimData.IdleAnimStart);
					this.SetConversationAgentActionAtChannel(this._currentAgent, action, this.GetActionChannelNoForConversation(this._currentAgent), false, false);
				}
			}
			if (!string.IsNullOrEmpty(reactionFaceAnimId))
			{
				this._currentAgent.SetAgentFacialAnimation(Agent.FacialAnimChannel.Mid, reactionFaceAnimId, false);
			}
			else if (!string.IsNullOrEmpty(idleFaceAnimId))
			{
				this.SetFaceIdle(this._currentAgent, idleFaceAnimId);
			}
			else
			{
				this._currentAgent.SetAgentFacialAnimation(Agent.FacialAnimChannel.High, "", false);
			}
			if (!string.IsNullOrEmpty(soundPath))
			{
				this.PlayConversationSoundEvent(soundPath);
			}
		}

		// Token: 0x060002DE RID: 734 RVA: 0x000123FC File Offset: 0x000105FC
		private string GetRhubarbXmlPathFromSoundPath(string soundPath)
		{
			int length = soundPath.LastIndexOf('.');
			return soundPath.Substring(0, length) + ".xml";
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00012424 File Offset: 0x00010624
		public void PlayConversationSoundEvent(string soundPath)
		{
			Vec3 position = ConversationMission.OneToOneConversationAgent.Position;
			Debug.Print(string.Concat(new object[]
			{
				"Conversation sound playing: ",
				soundPath,
				", position: ",
				position
			}), 5, Debug.DebugColor.White, 17592186044416UL);
			this._soundEvent = SoundEvent.CreateEventFromExternalFile("event:/Extra/voiceover", soundPath, Mission.Current.Scene);
			this._soundEvent.SetPosition(position);
			this._soundEvent.Play();
			int soundId = this._soundEvent.GetSoundId();
			this._agentSoundEvents.Add(this._currentAgent, soundId);
			string rhubarbXmlPathFromSoundPath = this.GetRhubarbXmlPathFromSoundPath(soundPath);
			this._currentAgent.AgentVisuals.StartRhubarbRecord(rhubarbXmlPathFromSoundPath, soundId);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x000124E0 File Offset: 0x000106E0
		private void StopPreviousSound()
		{
			if (this._soundEvent != null)
			{
				this._soundEvent.Stop();
				this._soundEvent = null;
			}
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x000124FC File Offset: 0x000106FC
		private void RemovePreviousAgentsSoundEvent()
		{
			if (this._soundEvent != null && this._agentSoundEvents.ContainsValue(this._soundEvent.GetSoundId()))
			{
				Agent agent = null;
				foreach (KeyValuePair<Agent, int> keyValuePair in this._agentSoundEvents)
				{
					if (keyValuePair.Value == this._soundEvent.GetSoundId())
					{
						agent = keyValuePair.Key;
					}
				}
				this._agentSoundEvents.Remove(agent);
				agent.AgentVisuals.StartRhubarbRecord("", -1);
			}
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x000125A8 File Offset: 0x000107A8
		void ICampaignMission.OnConversationEnd(IAgent iAgent)
		{
			Agent agent = (Agent)iAgent;
			agent.ResetLookAgent();
			agent.DisableLookToPointOfInterest();
			Agent.Main.ResetLookAgent();
			Agent.Main.DisableLookToPointOfInterest();
			if (Settlement.CurrentSettlement != null && !base.Mission.HasMissionBehavior<ConversationMissionLogic>())
			{
				agent.AgentVisuals.SetAgentLodZeroOrMax(true);
				Agent.Main.AgentVisuals.SetAgentLodZeroOrMax(true);
			}
			if (this._soundEvent != null)
			{
				this.RemovePreviousAgentsSoundEvent();
				this._soundEvent.Stop();
			}
			if (this._isMainAgentAnimationSet)
			{
				this._isMainAgentAnimationSet = false;
				this.EndConversationAnimations(Agent.Main);
			}
			this.EndConversationAnimations(iAgent);
			this._soundEvent = null;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0001264D File Offset: 0x0001084D
		private void SetFaceIdle(Agent agent, string idleFaceAnimId)
		{
			agent.SetAgentFacialAnimation(Agent.FacialAnimChannel.Mid, idleFaceAnimId, true);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00012658 File Offset: 0x00010858
		private void SetConversationAgentActionAtChannel(Agent agent, ActionIndexCache action, int channelNo, bool setInstantly, bool forceFaceMorphRestart)
		{
			agent.SetActionChannel(channelNo, action, false, 0UL, 0f, 1f, setInstantly ? 0f : -0.2f, 0.4f, 0f, false, -0.2f, 0, forceFaceMorphRestart);
			int count = this._conversationAgents.Count;
			for (int i = 0; i < count; i++)
			{
				if (this._conversationAgents[i].Agent == agent)
				{
					this._conversationAgents[i].SetChannelModified(channelNo);
					return;
				}
			}
		}

		// Token: 0x04000147 RID: 327
		private MissionState _state;

		// Token: 0x0400014B RID: 331
		private SoundEvent _soundEvent;

		// Token: 0x0400014C RID: 332
		private Agent _currentAgent;

		// Token: 0x0400014D RID: 333
		private bool _isMainAgentAnimationSet;

		// Token: 0x0400014E RID: 334
		private readonly Dictionary<Agent, int> _agentSoundEvents = new Dictionary<Agent, int>();

		// Token: 0x0400014F RID: 335
		private readonly List<CampaignMissionComponent.AgentConversationState> _conversationAgents = new List<CampaignMissionComponent.AgentConversationState>();

		// Token: 0x02000124 RID: 292
		private class AgentConversationState
		{
			// Token: 0x170000ED RID: 237
			// (get) Token: 0x06000BB0 RID: 2992 RVA: 0x000525ED File Offset: 0x000507ED
			// (set) Token: 0x06000BB1 RID: 2993 RVA: 0x000525F5 File Offset: 0x000507F5
			public Agent Agent { get; private set; }

			// Token: 0x06000BB2 RID: 2994 RVA: 0x000525FE File Offset: 0x000507FE
			public AgentConversationState(Agent agent)
			{
				this.Agent = agent;
				this._actionAtChannelModified = default(StackArray.StackArray2Bool);
				this._actionAtChannelModified[0] = false;
				this._actionAtChannelModified[1] = false;
			}

			// Token: 0x06000BB3 RID: 2995 RVA: 0x00052633 File Offset: 0x00050833
			public bool IsChannelModified(int channelNo)
			{
				return this._actionAtChannelModified[channelNo];
			}

			// Token: 0x06000BB4 RID: 2996 RVA: 0x00052641 File Offset: 0x00050841
			public void SetChannelModified(int channelNo)
			{
				this._actionAtChannelModified[channelNo] = true;
			}

			// Token: 0x04000536 RID: 1334
			private StackArray.StackArray2Bool _actionAtChannelModified;
		}
	}
}
