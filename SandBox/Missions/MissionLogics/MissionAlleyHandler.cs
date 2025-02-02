using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using SandBox.Conversation.MissionLogics;
using SandBox.Missions.AgentBehaviors;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.MissionLogics
{
	// Token: 0x02000057 RID: 87
	public class MissionAlleyHandler : MissionLogic
	{
		// Token: 0x06000394 RID: 916 RVA: 0x00019140 File Offset: 0x00017340
		public override void OnRenderingStarted()
		{
			foreach (Agent agent in base.Mission.Agents)
			{
				if (agent.IsHuman)
				{
					CampaignAgentComponent component = agent.GetComponent<CampaignAgentComponent>();
					bool flag;
					if (component == null)
					{
						flag = (null != null);
					}
					else
					{
						AgentNavigator agentNavigator = component.AgentNavigator;
						flag = (((agentNavigator != null) ? agentNavigator.MemberOfAlley : null) != null);
					}
					if (flag && component.AgentNavigator.MemberOfAlley.Owner != Hero.MainHero && !this._rivalThugAgentsAndAgentNavigators.ContainsKey(agent))
					{
						this._rivalThugAgentsAndAgentNavigators.Add(agent, component.AgentNavigator);
					}
				}
			}
		}

		// Token: 0x06000395 RID: 917 RVA: 0x000191F4 File Offset: 0x000173F4
		public override void OnMissionTick(float dt)
		{
			if (Mission.Current.Mode == MissionMode.Battle)
			{
				this.EndFightIfPlayerIsFarAwayOrNearGuard();
				return;
			}
			if (MBRandom.RandomFloat < dt * 10f)
			{
				this.CheckAndTriggerConversationWithRivalThug();
			}
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00019228 File Offset: 0x00017428
		private void CheckAndTriggerConversationWithRivalThug()
		{
			if (!this._conversationTriggeredWithRivalThug && !Campaign.Current.ConversationManager.IsConversationFlowActive && Agent.Main != null)
			{
				foreach (KeyValuePair<Agent, AgentNavigator> keyValuePair in this._rivalThugAgentsAndAgentNavigators)
				{
					if (keyValuePair.Key.IsActive())
					{
						Agent key = keyValuePair.Key;
						if (key.GetTrackDistanceToMainAgent() < 5f && keyValuePair.Value.CanSeeAgent(Agent.Main))
						{
							Mission.Current.GetMissionBehavior<MissionConversationLogic>().StartConversation(key, false, false);
							this._conversationTriggeredWithRivalThug = true;
							break;
						}
					}
				}
			}
		}

		// Token: 0x06000398 RID: 920 RVA: 0x000192F0 File Offset: 0x000174F0
		public override void AfterStart()
		{
			MissionAlleyHandler._guardAgents = new List<Agent>();
			this._rivalThugAgentsAndAgentNavigators = new Dictionary<Agent, AgentNavigator>();
			MissionAlleyHandler._fightPosition = Vec3.Invalid;
			this._missionFightHandler = Mission.Current.GetMissionBehavior<MissionFightHandler>();
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00019324 File Offset: 0x00017524
		private void EndFightIfPlayerIsFarAwayOrNearGuard()
		{
			if (Agent.Main != null)
			{
				bool flag = false;
				foreach (Agent agent in MissionAlleyHandler._guardAgents)
				{
					if ((Agent.Main.Position - agent.Position).Length <= 10f)
					{
						flag = true;
						break;
					}
				}
				if (MissionAlleyHandler._fightPosition != Vec3.Invalid && (Agent.Main.Position - MissionAlleyHandler._fightPosition).Length >= 20f)
				{
					flag = true;
				}
				if (flag)
				{
					this.EndFight();
				}
			}
		}

		// Token: 0x0600039A RID: 922 RVA: 0x000193E4 File Offset: 0x000175E4
		private ValueTuple<bool, string> CanPlayerOccupyTheCurrentAlley()
		{
			TextObject textObject = TextObject.Empty;
			if (!Settlement.CurrentSettlement.Alleys.All((Alley x) => x.Owner != Hero.MainHero))
			{
				textObject = new TextObject("{=ribkM9dl}You already own another alley in the settlement.", null);
				return new ValueTuple<bool, string>(false, textObject.ToString());
			}
			if (!Campaign.Current.Models.AlleyModel.GetClanMembersAndAvailabilityDetailsForLeadingAnAlley(CampaignMission.Current.LastVisitedAlley).Any((ValueTuple<Hero, DefaultAlleyModel.AlleyMemberAvailabilityDetail> x) => x.Item2 == DefaultAlleyModel.AlleyMemberAvailabilityDetail.Available || x.Item2 == DefaultAlleyModel.AlleyMemberAvailabilityDetail.AvailableWithDelay))
			{
				textObject = new TextObject("{=hnhKJYbx}You don't have any suitable clan members to assign this alley. ({ROGUERY_SKILL} skill {NEEDED_SKILL_LEVEL} or higher, {TRAIT_NAME} trait {MAX_TRAIT_AMOUNT} or lower)", null);
				textObject.SetTextVariable("ROGUERY_SKILL", DefaultSkills.Roguery.Name);
				textObject.SetTextVariable("NEEDED_SKILL_LEVEL", 30);
				textObject.SetTextVariable("TRAIT_NAME", DefaultTraits.Mercy.Name);
				textObject.SetTextVariable("MAX_TRAIT_AMOUNT", 0);
				return new ValueTuple<bool, string>(false, textObject.ToString());
			}
			if (MobileParty.MainParty.MemberRoster.TotalRegulars < Campaign.Current.Models.AlleyModel.MinimumTroopCountInPlayerOwnedAlley)
			{
				textObject = new TextObject("{=zLnqZdIK}You don't have enough troops to assign this alley. (Needed at least {NEEDED_TROOP_NUMBER})", null);
				textObject.SetTextVariable("NEEDED_TROOP_NUMBER", Campaign.Current.Models.AlleyModel.MinimumTroopCountInPlayerOwnedAlley);
				return new ValueTuple<bool, string>(false, textObject.ToString());
			}
			return new ValueTuple<bool, string>(true, null);
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00019550 File Offset: 0x00017750
		private void EndFight()
		{
			this._missionFightHandler.EndFight();
			foreach (Agent agent in MissionAlleyHandler._guardAgents)
			{
				agent.GetComponent<CampaignAgentComponent>().AgentNavigator.GetBehaviorGroup<AlarmedBehaviorGroup>().GetBehavior<FightBehavior>().IsActive = false;
			}
			MissionAlleyHandler._guardAgents.Clear();
			Mission.Current.SetMissionMode(MissionMode.StartUp, false);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x000195D8 File Offset: 0x000177D8
		private void OnTakeOverTheAlley()
		{
			AlleyHelper.CreateMultiSelectionInquiryForSelectingClanMemberToAlley(CampaignMission.Current.LastVisitedAlley, new Action<List<InquiryElement>>(this.OnCompanionSelectedForNewAlley), new Action<List<InquiryElement>>(this.OnCompanionSelectionCancel));
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00019601 File Offset: 0x00017801
		private void OnCompanionSelectionCancel(List<InquiryElement> obj)
		{
			this.OnLeaveItEmpty();
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0001960C File Offset: 0x0001780C
		private void OnCompanionSelectedForNewAlley(List<InquiryElement> companion)
		{
			CharacterObject character = companion.First<InquiryElement>().Identifier as CharacterObject;
			TroopRoster troopRoster = TroopRoster.CreateDummyTroopRoster();
			troopRoster.AddToCounts(character, 1, false, 0, 0, true, -1);
			AlleyHelper.OpenScreenForManagingAlley(troopRoster, new PartyPresentationDoneButtonDelegate(this.OnPartyScreenDoneClicked), new TextObject("{=s8dsW6m0}New Alley", null), new PartyPresentationCancelButtonDelegate(this.OnPartyScreenCancel));
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00019665 File Offset: 0x00017865
		private void OnPartyScreenCancel()
		{
			this.OnLeaveItEmpty();
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00019670 File Offset: 0x00017870
		public override void OnAgentHit(Agent affectedAgent, Agent affectorAgent, in MissionWeapon attackerWeapon, in Blow blow, in AttackCollisionData attackCollisionData)
		{
			if (!affectedAgent.IsHuman)
			{
				return;
			}
			if (affectorAgent != null && affectorAgent == Agent.Main && affectorAgent.IsHuman && affectedAgent.GetComponent<CampaignAgentComponent>().AgentNavigator != null)
			{
				TalkBehavior behavior = affectedAgent.GetComponent<CampaignAgentComponent>().AgentNavigator.GetBehaviorGroup<InterruptingBehaviorGroup>().GetBehavior<TalkBehavior>();
				if (behavior != null)
				{
					behavior.Disable();
				}
				if (!affectedAgent.IsEnemyOf(affectorAgent) && affectedAgent.GetComponent<CampaignAgentComponent>().AgentNavigator.MemberOfAlley != null)
				{
					this.StartCommonAreaBattle(affectedAgent.GetComponent<CampaignAgentComponent>().AgentNavigator.MemberOfAlley);
				}
			}
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x000196F7 File Offset: 0x000178F7
		private bool OnPartyScreenDoneClicked(TroopRoster leftMemberRoster, TroopRoster leftPrisonRoster, TroopRoster rightMemberRoster, TroopRoster rightPrisonRoster, FlattenedTroopRoster takenPrisonerRoster, FlattenedTroopRoster releasedPrisonerRoster, bool isForced, PartyBase leftParty, PartyBase rightParty)
		{
			CampaignEventDispatcher.Instance.OnAlleyOccupiedByPlayer(CampaignMission.Current.LastVisitedAlley, leftMemberRoster);
			return true;
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00019710 File Offset: 0x00017910
		public void StartCommonAreaBattle(Alley alley)
		{
			MissionAlleyHandler._guardAgents.Clear();
			this._conversationTriggeredWithRivalThug = true;
			List<Agent> accompanyingAgents = new List<Agent>();
			foreach (Agent agent2 in Mission.Current.Agents)
			{
				LocationCharacter locationCharacter = LocationComplex.Current.FindCharacter(agent2);
				AccompanyingCharacter accompanyingCharacter = PlayerEncounter.LocationEncounter.GetAccompanyingCharacter(locationCharacter);
				CharacterObject characterObject = (CharacterObject)agent2.Character;
				if (accompanyingCharacter != null && accompanyingCharacter.IsFollowingPlayerAtMissionStart)
				{
					accompanyingAgents.Add(agent2);
				}
				else if (characterObject != null && (characterObject.Occupation == Occupation.Guard || characterObject.Occupation == Occupation.Soldier))
				{
					MissionAlleyHandler._guardAgents.Add(agent2);
				}
			}
			List<Agent> playerSideAgents = (from agent in Mission.Current.Agents
			where agent.IsHuman && agent.Character.IsHero && (agent.IsPlayerControlled || accompanyingAgents.Contains(agent))
			select agent).ToList<Agent>();
			List<Agent> opponentSideAgents = (from agent in Mission.Current.Agents
			where agent.IsHuman && agent.GetComponent<CampaignAgentComponent>().AgentNavigator != null && agent.GetComponent<CampaignAgentComponent>().AgentNavigator.MemberOfAlley == alley
			select agent).ToList<Agent>();
			MissionAlleyHandler._fightPosition = Agent.Main.Position;
			Mission.Current.GetMissionBehavior<MissionFightHandler>().StartCustomFight(playerSideAgents, opponentSideAgents, false, false, new MissionFightHandler.OnFightEndDelegate(this.OnAlleyFightEnd));
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00019868 File Offset: 0x00017A68
		private void OnLeaveItEmpty()
		{
			CampaignEventDispatcher.Instance.OnAlleyClearedByPlayer(CampaignMission.Current.LastVisitedAlley);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00019880 File Offset: 0x00017A80
		private void OnAlleyFightEnd(bool isPlayerSideWon)
		{
			if (isPlayerSideWon)
			{
				object obj = new TextObject("{=4QfQBi2k}Alley fight won", null);
				TextObject textObject = new TextObject("{=8SK2BZum}You have cleared an alley which belonged to a gang leader. Now, you can either take it over for your own benefit or leave it empty to help the town. To own an alley, you will need to assign a suitable clan member and some troops to watch over it. This will provide denars to your clan, but also increase your crime rating.", null);
				TextObject textObject2 = new TextObject("{=qxY2ASqp}Take over the alley", null);
				TextObject textObject3 = new TextObject("{=jjEzdO0Y}Leave it empty", null);
				InformationManager.ShowInquiry(new InquiryData(obj.ToString(), textObject.ToString(), true, true, textObject2.ToString(), textObject3.ToString(), new Action(this.OnTakeOverTheAlley), new Action(this.OnLeaveItEmpty), "", 0f, null, new Func<ValueTuple<bool, string>>(this.CanPlayerOccupyTheCurrentAlley), null), true, false);
			}
			else if (Agent.Main == null || !Agent.Main.IsActive())
			{
				Mission.Current.NextCheckTimeEndMission = 0f;
				Campaign.Current.GameMenuManager.SetNextMenu("settlement_player_unconscious");
			}
			MissionAlleyHandler._fightPosition = Vec3.Invalid;
		}

		// Token: 0x040001AC RID: 428
		private const float ConstantForInitiatingConversation = 5f;

		// Token: 0x040001AD RID: 429
		private static Vec3 _fightPosition = Vec3.Invalid;

		// Token: 0x040001AE RID: 430
		private Dictionary<Agent, AgentNavigator> _rivalThugAgentsAndAgentNavigators;

		// Token: 0x040001AF RID: 431
		private const int DistanceForEndingAlleyFight = 20;

		// Token: 0x040001B0 RID: 432
		private const int GuardAgentSafeZone = 10;

		// Token: 0x040001B1 RID: 433
		private static List<Agent> _guardAgents;

		// Token: 0x040001B2 RID: 434
		private bool _conversationTriggeredWithRivalThug;

		// Token: 0x040001B3 RID: 435
		private MissionFightHandler _missionFightHandler;
	}
}
