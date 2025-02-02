using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using SandBox.Missions.MissionLogics;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.AgentOrigins;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.Issues;
using TaleWorlds.CampaignSystem.Overlay;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.CampaignSystem.Siege;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.LinQuick;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.ObjectSystem;
using TaleWorlds.SaveSystem;

namespace SandBox.Issues
{
	// Token: 0x0200008B RID: 139
	public class RivalGangMovingInIssueBehavior : CampaignBehaviorBase
	{
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000560 RID: 1376 RVA: 0x0002458C File Offset: 0x0002278C
		private static RivalGangMovingInIssueBehavior.RivalGangMovingInIssueQuest Instance
		{
			get
			{
				RivalGangMovingInIssueBehavior campaignBehavior = Campaign.Current.GetCampaignBehavior<RivalGangMovingInIssueBehavior>();
				if (campaignBehavior._cachedQuest != null && campaignBehavior._cachedQuest.IsOngoing)
				{
					return campaignBehavior._cachedQuest;
				}
				using (List<QuestBase>.Enumerator enumerator = Campaign.Current.QuestManager.Quests.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						RivalGangMovingInIssueBehavior.RivalGangMovingInIssueQuest cachedQuest;
						if ((cachedQuest = (enumerator.Current as RivalGangMovingInIssueBehavior.RivalGangMovingInIssueQuest)) != null)
						{
							campaignBehavior._cachedQuest = cachedQuest;
							return campaignBehavior._cachedQuest;
						}
					}
				}
				return null;
			}
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00024624 File Offset: 0x00022824
		private void OnCheckForIssue(Hero hero)
		{
			if (this.ConditionsHold(hero))
			{
				Campaign.Current.IssueManager.AddPotentialIssueData(hero, new PotentialIssueData(new PotentialIssueData.StartIssueDelegate(this.OnStartIssue), typeof(RivalGangMovingInIssueBehavior.RivalGangMovingInIssue), IssueBase.IssueFrequency.Common, null));
				return;
			}
			Campaign.Current.IssueManager.AddPotentialIssueData(hero, new PotentialIssueData(typeof(RivalGangMovingInIssueBehavior.RivalGangMovingInIssue), IssueBase.IssueFrequency.Common));
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x00024688 File Offset: 0x00022888
		private IssueBase OnStartIssue(in PotentialIssueData pid, Hero issueOwner)
		{
			Hero rivalGangLeader = this.GetRivalGangLeader(issueOwner);
			return new RivalGangMovingInIssueBehavior.RivalGangMovingInIssue(issueOwner, rivalGangLeader);
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x000246A4 File Offset: 0x000228A4
		private static void rival_gang_wait_duration_is_over_menu_on_init(MenuCallbackArgs args)
		{
			Campaign.Current.TimeControlMode = CampaignTimeControlMode.Stop;
			TextObject text = new TextObject("{=9Kr9pjGs}{QUEST_GIVER.LINK} has prepared {?QUEST_GIVER.GENDER}her{?}his{\\?} men and is waiting for you.", null);
			StringHelpers.SetCharacterProperties("QUEST_GIVER", RivalGangMovingInIssueBehavior.Instance.QuestGiver.CharacterObject, null, false);
			MBTextManager.SetTextVariable("MENU_TEXT", text, false);
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x000246F0 File Offset: 0x000228F0
		private bool ConditionsHold(Hero issueGiver)
		{
			return issueGiver.IsGangLeader && issueGiver.CurrentSettlement != null && issueGiver.CurrentSettlement.IsTown && issueGiver.CurrentSettlement.Town.Security <= 60f && this.GetRivalGangLeader(issueGiver) != null;
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00024740 File Offset: 0x00022940
		private void rival_gang_quest_wait_duration_is_over_yes_consequence(MenuCallbackArgs args)
		{
			CampaignMapConversation.OpenConversation(new ConversationCharacterData(CharacterObject.PlayerCharacter, null, true, true, false, false, false, false), new ConversationCharacterData(RivalGangMovingInIssueBehavior.Instance.QuestGiver.CharacterObject, null, true, true, false, false, false, false));
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00024780 File Offset: 0x00022980
		private Hero GetRivalGangLeader(Hero issueOwner)
		{
			Hero result = null;
			foreach (Hero hero in issueOwner.CurrentSettlement.Notables)
			{
				if (hero != issueOwner && hero.IsGangLeader && hero.CanHaveQuestsOrIssues())
				{
					result = hero;
					break;
				}
			}
			return result;
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x000247EC File Offset: 0x000229EC
		private bool rival_gang_quest_wait_duration_is_over_yes_condition(MenuCallbackArgs args)
		{
			args.optionLeaveType = GameMenuOption.LeaveType.Continue;
			return true;
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x000247F7 File Offset: 0x000229F7
		private bool rival_gang_quest_wait_duration_is_over_no_condition(MenuCallbackArgs args)
		{
			args.optionLeaveType = GameMenuOption.LeaveType.Leave;
			return true;
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00024802 File Offset: 0x00022A02
		public override void RegisterEvents()
		{
			CampaignEvents.OnCheckForIssueEvent.AddNonSerializedListener(this, new Action<Hero>(this.OnCheckForIssue));
			CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnSessionLaunched));
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00024834 File Offset: 0x00022A34
		private void OnSessionLaunched(CampaignGameStarter gameStarter)
		{
			gameStarter.AddGameMenu("rival_gang_quest_before_fight", "", new OnInitDelegate(RivalGangMovingInIssueBehavior.rival_gang_quest_before_fight_init), GameOverlays.MenuOverlayType.SettlementWithBoth, GameMenu.MenuFlags.None, null);
			gameStarter.AddGameMenu("rival_gang_quest_after_fight", "", new OnInitDelegate(RivalGangMovingInIssueBehavior.rival_gang_quest_after_fight_init), GameOverlays.MenuOverlayType.SettlementWithBoth, GameMenu.MenuFlags.None, null);
			gameStarter.AddGameMenu("rival_gang_quest_wait_duration_is_over", "{MENU_TEXT}", new OnInitDelegate(RivalGangMovingInIssueBehavior.rival_gang_wait_duration_is_over_menu_on_init), GameOverlays.MenuOverlayType.None, GameMenu.MenuFlags.None, null);
			gameStarter.AddGameMenuOption("rival_gang_quest_wait_duration_is_over", "rival_gang_quest_wait_duration_is_over_yes", "{=aka03VdU}Meet {?QUEST_GIVER.GENDER}her{?}him{\\?} now", new GameMenuOption.OnConditionDelegate(this.rival_gang_quest_wait_duration_is_over_yes_condition), new GameMenuOption.OnConsequenceDelegate(this.rival_gang_quest_wait_duration_is_over_yes_consequence), false, -1, false, null);
			gameStarter.AddGameMenuOption("rival_gang_quest_wait_duration_is_over", "rival_gang_quest_wait_duration_is_over_no", "{=NIzQb6nT}Leave and meet {?QUEST_GIVER.GENDER}her{?}him{\\?} later", new GameMenuOption.OnConditionDelegate(this.rival_gang_quest_wait_duration_is_over_no_condition), new GameMenuOption.OnConsequenceDelegate(this.rival_gang_quest_wait_duration_is_over_no_consequence), true, -1, false, null);
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00024900 File Offset: 0x00022B00
		private void rival_gang_quest_wait_duration_is_over_no_consequence(MenuCallbackArgs args)
		{
			Campaign.Current.CurrentMenuContext.SwitchToMenu("town_wait_menus");
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00024916 File Offset: 0x00022B16
		private static void rival_gang_quest_before_fight_init(MenuCallbackArgs args)
		{
			if (RivalGangMovingInIssueBehavior.Instance != null && RivalGangMovingInIssueBehavior.Instance._isFinalStage)
			{
				RivalGangMovingInIssueBehavior.Instance.StartAlleyBattle();
			}
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00024938 File Offset: 0x00022B38
		private static void rival_gang_quest_after_fight_init(MenuCallbackArgs args)
		{
			if (RivalGangMovingInIssueBehavior.Instance != null && RivalGangMovingInIssueBehavior.Instance._isReadyToBeFinalized)
			{
				bool hasPlayerWon = PlayerEncounter.Battle.WinningSide == PlayerEncounter.Battle.PlayerSide;
				PlayerEncounter.Current.FinalizeBattle();
				RivalGangMovingInIssueBehavior.Instance.HandlePlayerEncounterResult(hasPlayerWon);
			}
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00024984 File Offset: 0x00022B84
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00024988 File Offset: 0x00022B88
		[GameMenuInitializationHandler("rival_gang_quest_after_fight")]
		[GameMenuInitializationHandler("rival_gang_quest_wait_duration_is_over")]
		private static void game_menu_rival_gang_quest_end_on_init(MenuCallbackArgs args)
		{
			Settlement currentSettlement = Settlement.CurrentSettlement;
			if (currentSettlement != null)
			{
				args.MenuContext.SetBackgroundMeshName(currentSettlement.SettlementComponent.WaitMeshName);
			}
		}

		// Token: 0x04000292 RID: 658
		private const IssueBase.IssueFrequency RivalGangLeaderIssueFrequency = IssueBase.IssueFrequency.Common;

		// Token: 0x04000293 RID: 659
		private RivalGangMovingInIssueBehavior.RivalGangMovingInIssueQuest _cachedQuest;

		// Token: 0x02000168 RID: 360
		public class RivalGangMovingInIssueTypeDefiner : SaveableTypeDefiner
		{
			// Token: 0x06000E5D RID: 3677 RVA: 0x0005C43A File Offset: 0x0005A63A
			public RivalGangMovingInIssueTypeDefiner() : base(310000)
			{
			}

			// Token: 0x06000E5E RID: 3678 RVA: 0x0005C447 File Offset: 0x0005A647
			protected override void DefineClassTypes()
			{
				base.AddClassDefinition(typeof(RivalGangMovingInIssueBehavior.RivalGangMovingInIssue), 1, null);
				base.AddClassDefinition(typeof(RivalGangMovingInIssueBehavior.RivalGangMovingInIssueQuest), 2, null);
			}
		}

		// Token: 0x02000169 RID: 361
		public class RivalGangMovingInIssue : IssueBase
		{
			// Token: 0x17000167 RID: 359
			// (get) Token: 0x06000E5F RID: 3679 RVA: 0x0005C46D File Offset: 0x0005A66D
			public override IssueBase.AlternativeSolutionScaleFlag AlternativeSolutionScaleFlags
			{
				get
				{
					return IssueBase.AlternativeSolutionScaleFlag.Casualties | IssueBase.AlternativeSolutionScaleFlag.FailureRisk;
				}
			}

			// Token: 0x17000168 RID: 360
			// (get) Token: 0x06000E60 RID: 3680 RVA: 0x0005C471 File Offset: 0x0005A671
			// (set) Token: 0x06000E61 RID: 3681 RVA: 0x0005C479 File Offset: 0x0005A679
			[SaveableProperty(207)]
			public Hero RivalGangLeader { get; private set; }

			// Token: 0x17000169 RID: 361
			// (get) Token: 0x06000E62 RID: 3682 RVA: 0x0005C482 File Offset: 0x0005A682
			public override int AlternativeSolutionBaseNeededMenCount
			{
				get
				{
					return 4 + MathF.Ceiling(6f * base.IssueDifficultyMultiplier);
				}
			}

			// Token: 0x1700016A RID: 362
			// (get) Token: 0x06000E63 RID: 3683 RVA: 0x0005C497 File Offset: 0x0005A697
			protected override int AlternativeSolutionBaseDurationInDaysInternal
			{
				get
				{
					return 3 + MathF.Ceiling(5f * base.IssueDifficultyMultiplier);
				}
			}

			// Token: 0x1700016B RID: 363
			// (get) Token: 0x06000E64 RID: 3684 RVA: 0x0005C4AC File Offset: 0x0005A6AC
			protected override int RewardGold
			{
				get
				{
					return (int)(600f + 1700f * base.IssueDifficultyMultiplier);
				}
			}

			// Token: 0x1700016C RID: 364
			// (get) Token: 0x06000E65 RID: 3685 RVA: 0x0005C4C1 File Offset: 0x0005A6C1
			protected override int CompanionSkillRewardXP
			{
				get
				{
					return (int)(750f + 1000f * base.IssueDifficultyMultiplier);
				}
			}

			// Token: 0x1700016D RID: 365
			// (get) Token: 0x06000E66 RID: 3686 RVA: 0x0005C4D8 File Offset: 0x0005A6D8
			public override TextObject IssueBriefByIssueGiver
			{
				get
				{
					TextObject textObject = new TextObject("{=GXk6f9ah}I've got a problem... [ib:confident][if:convo_undecided_closed]And {?TARGET_NOTABLE.GENDER}her{?}his{\\?} name is {TARGET_NOTABLE.LINK}. {?TARGET_NOTABLE.GENDER}Her{?}His{\\?} people have been coming around outside the walls, robbing the dice-players and the drinkers enjoying themselves under our protection. Me and my boys are eager to teach them a lesson but I figure some extra muscle wouldn't hurt.", null);
					if (base.IssueOwner.RandomInt(2) == 0)
					{
						textObject = new TextObject("{=rgTGzfzI}Yeah. I have a problem all right. [ib:confident][if:convo_undecided_closed]{?TARGET_NOTABLE.GENDER}Her{?}His{\\?} name is {TARGET_NOTABLE.LINK}. {?TARGET_NOTABLE.GENDER}Her{?}His{\\?} people have been bothering shop owners under our protection, demanding money and making threats. Let me tell you something - those shop owners are my cows, and no one else gets to milk them. We're ready to teach these interlopers a lesson, but I could use some help.", null);
					}
					if (this.RivalGangLeader != null)
					{
						StringHelpers.SetCharacterProperties("TARGET_NOTABLE", this.RivalGangLeader.CharacterObject, textObject, false);
					}
					return textObject;
				}
			}

			// Token: 0x1700016E RID: 366
			// (get) Token: 0x06000E67 RID: 3687 RVA: 0x0005C52C File Offset: 0x0005A72C
			public override TextObject IssueAcceptByPlayer
			{
				get
				{
					return new TextObject("{=kc6vCycY}What exactly do you want me to do?", null);
				}
			}

			// Token: 0x1700016F RID: 367
			// (get) Token: 0x06000E68 RID: 3688 RVA: 0x0005C539 File Offset: 0x0005A739
			public override TextObject IssueQuestSolutionExplanationByIssueGiver
			{
				get
				{
					TextObject textObject = new TextObject("{=tyyAfWRR}We already had a small scuffle with them recently. [if:convo_mocking_revenge]They'll be waiting for us to come down hard. Instead, we'll hold off for {NUMBER} days. Let them think that we're backing off… Then, after {NUMBER} days, your men and mine will hit them in the middle of the night when they least expect it. I'll send you a messenger when the time comes and we'll strike them down together.", null);
					textObject.SetTextVariable("NUMBER", 2);
					return textObject;
				}
			}

			// Token: 0x17000170 RID: 368
			// (get) Token: 0x06000E69 RID: 3689 RVA: 0x0005C553 File Offset: 0x0005A753
			public override TextObject IssueAlternativeSolutionExplanationByIssueGiver
			{
				get
				{
					TextObject textObject = new TextObject("{=sSIjPCPO}If you'd rather not go into the fray yourself, [if:convo_mocking_aristocratic]you can leave me one of your companions together with {TROOP_COUNT} or so good men. If they stuck around for {RETURN_DAYS} days or so, I'd count it a very big favor.", null);
					textObject.SetTextVariable("TROOP_COUNT", base.GetTotalAlternativeSolutionNeededMenCount());
					textObject.SetTextVariable("RETURN_DAYS", base.GetTotalAlternativeSolutionDurationInDays());
					return textObject;
				}
			}

			// Token: 0x17000171 RID: 369
			// (get) Token: 0x06000E6A RID: 3690 RVA: 0x0005C584 File Offset: 0x0005A784
			protected override TextObject AlternativeSolutionStartLog
			{
				get
				{
					TextObject textObject = new TextObject("{=ymbVPod1}{ISSUE_GIVER.LINK}, a gang leader from {SETTLEMENT}, has told you about a new gang that is trying to get a hold on the town. You asked {COMPANION.LINK} to take {TROOP_COUNT} of your best men to stay with {ISSUE_GIVER.LINK} and help {?ISSUE_GIVER.GENDER}her{?}him{\\?} in the coming gang war. They should return to you in {RETURN_DAYS} days.", null);
					StringHelpers.SetCharacterProperties("ISSUE_GIVER", base.IssueOwner.CharacterObject, textObject, false);
					StringHelpers.SetCharacterProperties("COMPANION", base.AlternativeSolutionHero.CharacterObject, textObject, false);
					textObject.SetTextVariable("SETTLEMENT", base.IssueOwner.CurrentSettlement.EncyclopediaLinkWithName);
					textObject.SetTextVariable("TROOP_COUNT", this.AlternativeSolutionSentTroops.TotalManCount - 1);
					textObject.SetTextVariable("RETURN_DAYS", base.GetTotalAlternativeSolutionDurationInDays());
					return textObject;
				}
			}

			// Token: 0x17000172 RID: 370
			// (get) Token: 0x06000E6B RID: 3691 RVA: 0x0005C615 File Offset: 0x0005A815
			public override TextObject IssueQuestSolutionAcceptByPlayer
			{
				get
				{
					return new TextObject("{=LdCte9H0}I'll fight the other gang with you myself.", null);
				}
			}

			// Token: 0x17000173 RID: 371
			// (get) Token: 0x06000E6C RID: 3692 RVA: 0x0005C622 File Offset: 0x0005A822
			public override TextObject IssueAlternativeSolutionAcceptByPlayer
			{
				get
				{
					return new TextObject("{=AdbiUqtT}I'm busy, but I will leave a companion and some men.", null);
				}
			}

			// Token: 0x17000174 RID: 372
			// (get) Token: 0x06000E6D RID: 3693 RVA: 0x0005C62F File Offset: 0x0005A82F
			public override TextObject IssueAlternativeSolutionResponseByIssueGiver
			{
				get
				{
					return new TextObject("{=0enbhess}Thank you. [ib:normal][if:convo_approving]I'm sure your guys are worth their salt..", null);
				}
			}

			// Token: 0x17000175 RID: 373
			// (get) Token: 0x06000E6E RID: 3694 RVA: 0x0005C63C File Offset: 0x0005A83C
			public override TextObject IssueDiscussAlternativeSolution
			{
				get
				{
					return new TextObject("{=QR0V8Ae5}Our lads are well hidden nearby,[ib:normal][if:convo_excited] waiting for the signal to go get those bastards. I won't forget this little favor you're doing me.", null);
				}
			}

			// Token: 0x17000176 RID: 374
			// (get) Token: 0x06000E6F RID: 3695 RVA: 0x0005C649 File Offset: 0x0005A849
			public override bool IsThereAlternativeSolution
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000177 RID: 375
			// (get) Token: 0x06000E70 RID: 3696 RVA: 0x0005C64C File Offset: 0x0005A84C
			public override bool IsThereLordSolution
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000178 RID: 376
			// (get) Token: 0x06000E71 RID: 3697 RVA: 0x0005C64F File Offset: 0x0005A84F
			public override TextObject Title
			{
				get
				{
					TextObject textObject = new TextObject("{=vAjgn7yx}Rival Gang Moving In at {SETTLEMENT}", null);
					string tag = "SETTLEMENT";
					Settlement issueSettlement = base.IssueSettlement;
					textObject.SetTextVariable(tag, ((issueSettlement != null) ? issueSettlement.Name : null) ?? base.IssueOwner.HomeSettlement.Name);
					return textObject;
				}
			}

			// Token: 0x17000179 RID: 377
			// (get) Token: 0x06000E72 RID: 3698 RVA: 0x0005C68E File Offset: 0x0005A88E
			public override TextObject Description
			{
				get
				{
					return new TextObject("{=H4EVfKAh}Gang leader needs help to beat the rival gang.", null);
				}
			}

			// Token: 0x1700017A RID: 378
			// (get) Token: 0x06000E73 RID: 3699 RVA: 0x0005C69C File Offset: 0x0005A89C
			public override TextObject IssueAsRumorInSettlement
			{
				get
				{
					TextObject textObject = new TextObject("{=C9feTaca}I hear {QUEST_GIVER.LINK} is going to sort it out with {RIVAL_GANG_LEADER.LINK} once and for all.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.IssueOwner.CharacterObject, textObject, false);
					StringHelpers.SetCharacterProperties("RIVAL_GANG_LEADER", this.RivalGangLeader.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x1700017B RID: 379
			// (get) Token: 0x06000E74 RID: 3700 RVA: 0x0005C6E6 File Offset: 0x0005A8E6
			protected override bool IssueQuestCanBeDuplicated
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000E75 RID: 3701 RVA: 0x0005C6E9 File Offset: 0x0005A8E9
			public RivalGangMovingInIssue(Hero issueOwner, Hero rivalGangLeader) : base(issueOwner, CampaignTime.DaysFromNow(15f))
			{
				this.RivalGangLeader = rivalGangLeader;
			}

			// Token: 0x06000E76 RID: 3702 RVA: 0x0005C703 File Offset: 0x0005A903
			public override void OnHeroCanHaveQuestOrIssueInfoIsRequested(Hero hero, ref bool result)
			{
				if (hero == this.RivalGangLeader)
				{
					result = false;
				}
			}

			// Token: 0x06000E77 RID: 3703 RVA: 0x0005C711 File Offset: 0x0005A911
			protected override float GetIssueEffectAmountInternal(IssueEffect issueEffect)
			{
				if (issueEffect == DefaultIssueEffects.IssueOwnerPower)
				{
					return -0.2f;
				}
				if (issueEffect == DefaultIssueEffects.SettlementSecurity)
				{
					return -0.5f;
				}
				return 0f;
			}

			// Token: 0x06000E78 RID: 3704 RVA: 0x0005C734 File Offset: 0x0005A934
			protected override void AlternativeSolutionEndWithSuccessConsequence()
			{
				this.RelationshipChangeWithIssueOwner = 5;
				ChangeRelationAction.ApplyPlayerRelation(this.RivalGangLeader, -5, true, true);
				base.IssueOwner.AddPower(10f);
				this.RivalGangLeader.AddPower(-10f);
			}

			// Token: 0x06000E79 RID: 3705 RVA: 0x0005C76C File Offset: 0x0005A96C
			protected override void AlternativeSolutionEndWithFailureConsequence()
			{
				this.RelationshipChangeWithIssueOwner = -5;
				base.IssueSettlement.Town.Security += -10f;
				base.IssueOwner.AddPower(-10f);
			}

			// Token: 0x06000E7A RID: 3706 RVA: 0x0005C7A4 File Offset: 0x0005A9A4
			public override ValueTuple<SkillObject, int> GetAlternativeSolutionSkill(Hero hero)
			{
				int skillValue = hero.GetSkillValue(DefaultSkills.OneHanded);
				int skillValue2 = hero.GetSkillValue(DefaultSkills.TwoHanded);
				int skillValue3 = hero.GetSkillValue(DefaultSkills.Polearm);
				int skillValue4 = hero.GetSkillValue(DefaultSkills.Roguery);
				if (skillValue >= skillValue2 && skillValue >= skillValue3 && skillValue >= skillValue4)
				{
					return new ValueTuple<SkillObject, int>(DefaultSkills.OneHanded, 150);
				}
				if (skillValue2 >= skillValue3 && skillValue2 >= skillValue4)
				{
					return new ValueTuple<SkillObject, int>(DefaultSkills.TwoHanded, 150);
				}
				if (skillValue3 < skillValue4)
				{
					return new ValueTuple<SkillObject, int>(DefaultSkills.Roguery, 120);
				}
				return new ValueTuple<SkillObject, int>(DefaultSkills.Polearm, 150);
			}

			// Token: 0x06000E7B RID: 3707 RVA: 0x0005C835 File Offset: 0x0005AA35
			public override bool AlternativeSolutionCondition(out TextObject explanation)
			{
				explanation = TextObject.Empty;
				return QuestHelper.CheckRosterForAlternativeSolution(MobileParty.MainParty.MemberRoster, base.GetTotalAlternativeSolutionNeededMenCount(), ref explanation, 2, false);
			}

			// Token: 0x06000E7C RID: 3708 RVA: 0x0005C856 File Offset: 0x0005AA56
			public override bool DoTroopsSatisfyAlternativeSolution(TroopRoster troopRoster, out TextObject explanation)
			{
				explanation = TextObject.Empty;
				return QuestHelper.CheckRosterForAlternativeSolution(troopRoster, base.GetTotalAlternativeSolutionNeededMenCount(), ref explanation, 2, false);
			}

			// Token: 0x06000E7D RID: 3709 RVA: 0x0005C86E File Offset: 0x0005AA6E
			public override bool IsTroopTypeNeededByAlternativeSolution(CharacterObject character)
			{
				return character.Tier >= 2;
			}

			// Token: 0x06000E7E RID: 3710 RVA: 0x0005C87C File Offset: 0x0005AA7C
			protected override void OnGameLoad()
			{
			}

			// Token: 0x06000E7F RID: 3711 RVA: 0x0005C87E File Offset: 0x0005AA7E
			protected override void HourlyTick()
			{
			}

			// Token: 0x06000E80 RID: 3712 RVA: 0x0005C880 File Offset: 0x0005AA80
			protected override QuestBase GenerateIssueQuest(string questId)
			{
				return new RivalGangMovingInIssueBehavior.RivalGangMovingInIssueQuest(questId, base.IssueOwner, this.RivalGangLeader, 8, this.RewardGold, base.IssueDifficultyMultiplier);
			}

			// Token: 0x06000E81 RID: 3713 RVA: 0x0005C8A1 File Offset: 0x0005AAA1
			public override IssueBase.IssueFrequency GetFrequency()
			{
				return IssueBase.IssueFrequency.Common;
			}

			// Token: 0x06000E82 RID: 3714 RVA: 0x0005C8A4 File Offset: 0x0005AAA4
			protected override bool CanPlayerTakeQuestConditions(Hero issueGiver, out IssueBase.PreconditionFlags flag, out Hero relationHero, out SkillObject skill)
			{
				flag = IssueBase.PreconditionFlags.None;
				relationHero = null;
				skill = null;
				if (Hero.MainHero.IsWounded)
				{
					flag |= IssueBase.PreconditionFlags.Wounded;
				}
				if (issueGiver.GetRelationWithPlayer() < -10f)
				{
					flag |= IssueBase.PreconditionFlags.Relation;
					relationHero = issueGiver;
				}
				if (MobileParty.MainParty.MemberRoster.TotalHealthyCount < 5)
				{
					flag |= IssueBase.PreconditionFlags.NotEnoughTroops;
				}
				if (base.IssueOwner.CurrentSettlement.OwnerClan == Clan.PlayerClan)
				{
					flag |= IssueBase.PreconditionFlags.PlayerIsOwnerOfSettlement;
				}
				return flag == IssueBase.PreconditionFlags.None;
			}

			// Token: 0x06000E83 RID: 3715 RVA: 0x0005C928 File Offset: 0x0005AB28
			public override bool IssueStayAliveConditions()
			{
				return this.RivalGangLeader.IsAlive && base.IssueOwner.CurrentSettlement.OwnerClan != Clan.PlayerClan && base.IssueOwner.CurrentSettlement.Town.Security <= 80f;
			}

			// Token: 0x06000E84 RID: 3716 RVA: 0x0005C97A File Offset: 0x0005AB7A
			protected override void CompleteIssueWithTimedOutConsequences()
			{
			}

			// Token: 0x06000E85 RID: 3717 RVA: 0x0005C97C File Offset: 0x0005AB7C
			internal static void AutoGeneratedStaticCollectObjectsRivalGangMovingInIssue(object o, List<object> collectedObjects)
			{
				((RivalGangMovingInIssueBehavior.RivalGangMovingInIssue)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
			}

			// Token: 0x06000E86 RID: 3718 RVA: 0x0005C98A File Offset: 0x0005AB8A
			protected override void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
			{
				base.AutoGeneratedInstanceCollectObjects(collectedObjects);
				collectedObjects.Add(this.RivalGangLeader);
			}

			// Token: 0x06000E87 RID: 3719 RVA: 0x0005C99F File Offset: 0x0005AB9F
			internal static object AutoGeneratedGetMemberValueRivalGangLeader(object o)
			{
				return ((RivalGangMovingInIssueBehavior.RivalGangMovingInIssue)o).RivalGangLeader;
			}

			// Token: 0x04000626 RID: 1574
			private const int AlternativeSolutionRelationChange = 5;

			// Token: 0x04000627 RID: 1575
			private const int AlternativeSolutionFailRelationChange = -5;

			// Token: 0x04000628 RID: 1576
			private const int AlternativeSolutionQuestGiverPowerChange = 10;

			// Token: 0x04000629 RID: 1577
			private const int AlternativeSolutionRivalGangLeaderPowerChange = -10;

			// Token: 0x0400062A RID: 1578
			private const int AlternativeSolutionFailQuestGiverPowerChange = -10;

			// Token: 0x0400062B RID: 1579
			private const int AlternativeSolutionFailSecurityChange = -10;

			// Token: 0x0400062C RID: 1580
			private const int AlternativeSolutionRivalGangLeaderRelationChange = -5;

			// Token: 0x0400062D RID: 1581
			private const int AlternativeSolutionMinimumTroopTier = 2;

			// Token: 0x0400062E RID: 1582
			private const int IssueDuration = 15;

			// Token: 0x0400062F RID: 1583
			private const int MinimumRequiredMenCount = 5;

			// Token: 0x04000630 RID: 1584
			private const int IssueQuestDuration = 8;

			// Token: 0x04000631 RID: 1585
			private const int MeleeSkillValueThreshold = 150;

			// Token: 0x04000632 RID: 1586
			private const int RoguerySkillValueThreshold = 120;

			// Token: 0x04000633 RID: 1587
			private const int PreparationDurationInDays = 2;
		}

		// Token: 0x0200016A RID: 362
		public class RivalGangMovingInIssueQuest : QuestBase
		{
			// Token: 0x1700017C RID: 380
			// (get) Token: 0x06000E88 RID: 3720 RVA: 0x0005C9AC File Offset: 0x0005ABAC
			private TextObject _onQuestStartedLogText
			{
				get
				{
					TextObject textObject = new TextObject("{=dav5rmDd}{QUEST_GIVER.LINK}, a gang leader from {SETTLEMENT} has told you about a rival that is trying to get a foothold in {?QUEST_GIVER.GENDER}her{?}his{\\?} town. {?QUEST_GIVER.GENDER}She{?}He{\\?} asked you to wait {DAY_COUNT} days so that the other gang lets its guard down.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					textObject.SetTextVariable("SETTLEMENT", this._questSettlement.EncyclopediaLinkWithName);
					textObject.SetTextVariable("DAY_COUNT", 2);
					return textObject;
				}
			}

			// Token: 0x1700017D RID: 381
			// (get) Token: 0x06000E89 RID: 3721 RVA: 0x0005CA04 File Offset: 0x0005AC04
			private TextObject _onQuestFailedWithRejectionLogText
			{
				get
				{
					TextObject textObject = new TextObject("{=aXMg9M7t}You decided to stay out of the fight. {?QUEST_GIVER.GENDER}She{?}He{\\?} will certainly lose to the rival gang without your help.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x1700017E RID: 382
			// (get) Token: 0x06000E8A RID: 3722 RVA: 0x0005CA38 File Offset: 0x0005AC38
			private TextObject _onQuestFailedWithBetrayalLogText
			{
				get
				{
					TextObject textObject = new TextObject("{=Rf0QqRIX}You have chosen to side with the rival gang leader, {RIVAL_GANG_LEADER.LINK}. {QUEST_GIVER.LINK} must be furious.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					StringHelpers.SetCharacterProperties("RIVAL_GANG_LEADER", this._rivalGangLeader.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x1700017F RID: 383
			// (get) Token: 0x06000E8B RID: 3723 RVA: 0x0005CA84 File Offset: 0x0005AC84
			private TextObject _onQuestFailedWithDefeatLogText
			{
				get
				{
					TextObject textObject = new TextObject("{=du3dpMaV}You were unable to defeat {RIVAL_GANG_LEADER.LINK}'s gang, and thus failed to fulfill your commitment to {QUEST_GIVER.LINK}.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					StringHelpers.SetCharacterProperties("RIVAL_GANG_LEADER", this._rivalGangLeader.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x17000180 RID: 384
			// (get) Token: 0x06000E8C RID: 3724 RVA: 0x0005CAD0 File Offset: 0x0005ACD0
			private TextObject _onQuestSucceededLogText
			{
				get
				{
					TextObject textObject = new TextObject("{=vpUl7xcy}You have defeated the rival gang and protected the interests of {QUEST_GIVER.LINK} in {SETTLEMENT}.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					textObject.SetTextVariable("SETTLEMENT", this._questSettlement.EncyclopediaLinkWithName);
					return textObject;
				}
			}

			// Token: 0x17000181 RID: 385
			// (get) Token: 0x06000E8D RID: 3725 RVA: 0x0005CB1C File Offset: 0x0005AD1C
			private TextObject _onQuestPreperationsCompletedLogText
			{
				get
				{
					TextObject textObject = new TextObject("{=OIBiRTRP}{QUEST_GIVER.LINK} is waiting for you at {SETTLEMENT}.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					textObject.SetTextVariable("SETTLEMENT", this._questSettlement.EncyclopediaLinkWithName);
					return textObject;
				}
			}

			// Token: 0x17000182 RID: 386
			// (get) Token: 0x06000E8E RID: 3726 RVA: 0x0005CB68 File Offset: 0x0005AD68
			private TextObject _onQuestCancelledDueToWarLogText
			{
				get
				{
					TextObject textObject = new TextObject("{=vaUlAZba}Your clan is now at war with {QUEST_GIVER.LINK}. Your agreement with {QUEST_GIVER.LINK} was canceled.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x17000183 RID: 387
			// (get) Token: 0x06000E8F RID: 3727 RVA: 0x0005CB9C File Offset: 0x0005AD9C
			private TextObject _playerDeclaredWarQuestLogText
			{
				get
				{
					TextObject textObject = new TextObject("{=bqeWVVEE}Your actions have started a war with {QUEST_GIVER.LINK}'s faction. {?QUEST_GIVER.GENDER}She{?}He{\\?} cancels your agreement and the quest is a failure.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x17000184 RID: 388
			// (get) Token: 0x06000E90 RID: 3728 RVA: 0x0005CBD0 File Offset: 0x0005ADD0
			private TextObject _onQuestCancelledDueToSiegeLogText
			{
				get
				{
					TextObject textObject = new TextObject("{=s1GWSE9Y}{QUEST_GIVER.LINK} cancels your plans due to the siege of {SETTLEMENT}. {?QUEST_GIVER.GENDER}She{?}He{\\?} has worse troubles than {?QUEST_GIVER.GENDER}her{?}his{\\?} quarrel with the rival gang.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					textObject.SetTextVariable("SETTLEMENT", this._questSettlement.EncyclopediaLinkWithName);
					return textObject;
				}
			}

			// Token: 0x17000185 RID: 389
			// (get) Token: 0x06000E91 RID: 3729 RVA: 0x0005CC1C File Offset: 0x0005AE1C
			private TextObject _playerStartedAlleyFightWithRivalGangLeader
			{
				get
				{
					TextObject textObject = new TextObject("{=OeKgpuAv}After your attack on the rival gang's alley, {QUEST_GIVER.LINK} decided to change {?QUEST_GIVER.GENDER}her{?}his{\\?} plans, and doesn't need your assistance anymore. Quest is canceled.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x17000186 RID: 390
			// (get) Token: 0x06000E92 RID: 3730 RVA: 0x0005CC50 File Offset: 0x0005AE50
			private TextObject _playerStartedAlleyFightWithQuestgiver
			{
				get
				{
					TextObject textObject = new TextObject("{=VPGkIqlh}Your attack on {QUEST_GIVER.LINK}'s gang has angered {?QUEST_GIVER.GENDER}her{?}him{\\?} and {?QUEST_GIVER.GENDER}she{?}he{\\?} broke off the agreement that you had.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x17000187 RID: 391
			// (get) Token: 0x06000E93 RID: 3731 RVA: 0x0005CC84 File Offset: 0x0005AE84
			private TextObject OwnerOfQuestSettlementIsPlayerClanLogText
			{
				get
				{
					TextObject textObject = new TextObject("{=KxEnNEoD}Your clan is now owner of the settlement. As the {?PLAYER.GENDER}lady{?}lord{\\?} of the settlement you cannot get involved in gang wars anymore. Your agreement with the {QUEST_GIVER.LINK} has canceled.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					StringHelpers.SetCharacterProperties("PLAYER", CharacterObject.PlayerCharacter, textObject, false);
					return textObject;
				}
			}

			// Token: 0x06000E94 RID: 3732 RVA: 0x0005CCC8 File Offset: 0x0005AEC8
			public RivalGangMovingInIssueQuest(string questId, Hero questGiver, Hero rivalGangLeader, int duration, int rewardGold, float issueDifficulty) : base(questId, questGiver, CampaignTime.DaysFromNow((float)duration), rewardGold)
			{
				this._rivalGangLeader = rivalGangLeader;
				this._rewardGold = rewardGold;
				this._issueDifficulty = issueDifficulty;
				this._timeoutDurationInDays = (float)duration;
				this._preparationCompletionTime = CampaignTime.DaysFromNow(2f);
				this._questTimeoutTime = CampaignTime.DaysFromNow(this._timeoutDurationInDays);
				this._sentTroops = new List<CharacterObject>();
				this._allPlayerTroops = new List<TroopRosterElement>();
				this.InitializeQuestSettlement();
				this.SetDialogs();
				base.InitializeQuestOnCreation();
			}

			// Token: 0x17000188 RID: 392
			// (get) Token: 0x06000E95 RID: 3733 RVA: 0x0005CD50 File Offset: 0x0005AF50
			public override TextObject Title
			{
				get
				{
					TextObject textObject = new TextObject("{=WVorNMNc}Rival Gang Moving In At {SETTLEMENT}", null);
					textObject.SetTextVariable("SETTLEMENT", this._questSettlement.Name);
					return textObject;
				}
			}

			// Token: 0x17000189 RID: 393
			// (get) Token: 0x06000E96 RID: 3734 RVA: 0x0005CD74 File Offset: 0x0005AF74
			public override bool IsRemainingTimeHidden
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000E97 RID: 3735 RVA: 0x0005CD78 File Offset: 0x0005AF78
			protected override void InitializeQuestOnGameLoad()
			{
				this.InitializeQuestSettlement();
				this.SetDialogs();
				Campaign.Current.ConversationManager.AddDialogFlow(this.GetRivalGangLeaderDialogFlow(), this);
				Campaign.Current.ConversationManager.AddDialogFlow(this.GetQuestGiverPreparationCompletedDialogFlow(), this);
				MobileParty rivalGangLeaderParty = this._rivalGangLeaderParty;
				if (rivalGangLeaderParty != null)
				{
					rivalGangLeaderParty.SetPartyUsedByQuest(true);
				}
				this._sentTroops = new List<CharacterObject>();
				this._allPlayerTroops = new List<TroopRosterElement>();
			}

			// Token: 0x06000E98 RID: 3736 RVA: 0x0005CDE5 File Offset: 0x0005AFE5
			private void InitializeQuestSettlement()
			{
				this._questSettlement = base.QuestGiver.CurrentSettlement;
			}

			// Token: 0x06000E99 RID: 3737 RVA: 0x0005CDF8 File Offset: 0x0005AFF8
			protected override void SetDialogs()
			{
				this.OfferDialogFlow = DialogFlow.CreateDialogFlow("issue_classic_quest_start", 100).NpcLine("{=Fwm0PwVb}Great. As I said we need minimum of {NUMBER} days,[ib:normal][if:convo_mocking_revenge] so they'll let their guard down. I will let you know when it's time. Remember, we wait for the dark of the night to strike.", null, null).Condition(delegate
				{
					MBTextManager.SetTextVariable("SETTLEMENT", this._questSettlement.EncyclopediaLinkWithName, false);
					MBTextManager.SetTextVariable("NUMBER", 2);
					return Hero.OneToOneConversationHero == base.QuestGiver;
				}).Consequence(new ConversationSentence.OnConsequenceDelegate(this.OnQuestAccepted)).CloseDialog();
				this.DiscussDialogFlow = DialogFlow.CreateDialogFlow("quest_discuss", 100).NpcLine("{=z43j3Tzq}I'm still gathering my men for the fight. I'll send a runner for you when the time comes.", null, null).Condition(delegate
				{
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, null, false);
					return Hero.OneToOneConversationHero == base.QuestGiver && !this._isFinalStage && !this._preparationsComplete;
				}).BeginPlayerOptions().PlayerOption("{=4IHRAmnA}All right. I am waiting for your runner.", null).NpcLine("{=xEs830bT}You'll know right away once the preparations are complete.[ib:closed][if:convo_mocking_teasing] Just don't leave town.", null, null).CloseDialog().PlayerOption("{=6g8qvD2M}I can't just hang on here forever. Be quick about it.", null).NpcLine("{=lM7AscLo}I'm getting this together as quickly as I can.[ib:closed][if:convo_nervous]", null, null).CloseDialog().EndPlayerOptions().CloseDialog();
			}

			// Token: 0x06000E9A RID: 3738 RVA: 0x0005CEC0 File Offset: 0x0005B0C0
			private DialogFlow GetRivalGangLeaderDialogFlow()
			{
				return DialogFlow.CreateDialogFlow("start", 125).NpcLine("{=IfeN8lYd}Coming to fight us, eh? Did {QUEST_GIVER.LINK} put you up to this?[ib:aggressive2][if:convo_confused_annoyed] Look, there's no need for bloodshed. This town is big enough for all of us. But... if bloodshed is what you want, we will be happy to provide.", null, null).Condition(delegate
				{
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, null, false);
					return Hero.OneToOneConversationHero == this._rivalGangLeaderHenchmanHero && this._isReadyToBeFinalized;
				}).NpcLine("{=WSJxl2Hu}What I want to say is... [if:convo_mocking_teasing]You don't need to be a part of this. My boss will double whatever {?QUEST_GIVER.GENDER}she{?}he{\\?} is paying you if you join us.", null, null).BeginPlayerOptions().PlayerOption("{=GPBja02V}I gave my word to {QUEST_GIVER.LINK}, and I won't be bought.", null).Consequence(delegate
				{
					Campaign.Current.ConversationManager.ConversationEndOneShot += delegate()
					{
						CombatMissionWithDialogueController missionBehavior = Mission.Current.GetMissionBehavior<CombatMissionWithDialogueController>();
						if (missionBehavior == null)
						{
							return;
						}
						missionBehavior.StartFight(false);
					};
				}).NpcLine("{=OSgBicif}You will regret this![ib:warrior][if:convo_furious]", null, null).CloseDialog().PlayerOption("{=RB4uQpPV}You're going to pay me a lot then, {REWARD}{GOLD_ICON} to be exact. But at that price, I agree.", null).Condition(delegate
				{
					MBTextManager.SetTextVariable("REWARD", this._rewardGold * 2);
					return true;
				}).Consequence(delegate
				{
					Campaign.Current.ConversationManager.ConversationEndOneShot += delegate()
					{
						this._hasBetrayedQuestGiver = true;
						CombatMissionWithDialogueController missionBehavior = Mission.Current.GetMissionBehavior<CombatMissionWithDialogueController>();
						if (missionBehavior == null)
						{
							return;
						}
						missionBehavior.StartFight(true);
					};
				}).NpcLine("{=5jW4FVDc}Welcome to our ranks then. [ib:warrior][if:convo_evil_smile]Let's kill those bastards!", null, null).CloseDialog().EndPlayerOptions().CloseDialog();
			}

			// Token: 0x06000E9B RID: 3739 RVA: 0x0005CF90 File Offset: 0x0005B190
			private DialogFlow GetQuestGiverPreparationCompletedDialogFlow()
			{
				return DialogFlow.CreateDialogFlow("start", 125).BeginNpcOptions().NpcOption(new TextObject("{=hM7LSuB1}Good to see you. But we still need to wait until after dusk. {HERO.LINK}'s men may be watching, so let's keep our distance from each other until night falls.", null), delegate()
				{
					StringHelpers.SetCharacterProperties("HERO", this._rivalGangLeader.CharacterObject, null, false);
					return Hero.OneToOneConversationHero == base.QuestGiver && !this._isFinalStage && this._preparationCompletionTime.IsPast && (!this._preparationsComplete || !CampaignTime.Now.IsNightTime);
				}, null, null).CloseDialog().NpcOption("{=JxNlB547}Are you ready for the fight?[ib:normal][if:convo_undecided_open]", () => Hero.OneToOneConversationHero == base.QuestGiver && this._preparationsComplete && !this._isFinalStage && CampaignTime.Now.IsNightTime, null, null).EndNpcOptions().BeginPlayerOptions().PlayerOption("{=NzMX0s21}I am ready.", null).Condition(() => !Hero.MainHero.IsWounded).NpcLine("{=dNjepcKu}Let's finish this![ib:hip][if:convo_mocking_revenge]", null, null).Consequence(delegate
				{
					Campaign.Current.ConversationManager.ConversationEndOneShot += this.rival_gang_start_fight_on_consequence;
				}).CloseDialog().PlayerOption("{=B2Donbwz}I need more time.", null).Condition(() => !Hero.MainHero.IsWounded).NpcLine("{=advPT3WY}You'd better hurry up![ib:closed][if:convo_astonished]", null, null).Consequence(delegate
				{
					Campaign.Current.ConversationManager.ConversationEndOneShot += this.rival_gang_need_more_time_on_consequence;
				}).CloseDialog().PlayerOption("{=QaN26CZ5}My wounds are still fresh. I need some time to recover.", null).Condition(() => Hero.MainHero.IsWounded).NpcLine("{=s0jKaYo0}We must attack before the rival gang hears about our plan. You'd better hurry up![if:convo_astonished]", null, null).CloseDialog().EndPlayerOptions().CloseDialog();
			}

			// Token: 0x06000E9C RID: 3740 RVA: 0x0005D0DF File Offset: 0x0005B2DF
			public override void OnHeroCanDieInfoIsRequested(Hero hero, KillCharacterAction.KillCharacterActionDetail causeOfDeath, ref bool result)
			{
				if (hero == base.QuestGiver || hero == this._rivalGangLeader)
				{
					result = false;
				}
			}

			// Token: 0x06000E9D RID: 3741 RVA: 0x0005D0F6 File Offset: 0x0005B2F6
			private void rival_gang_start_fight_on_consequence()
			{
				this._isFinalStage = true;
				if (Mission.Current != null)
				{
					Mission.Current.EndMission();
				}
				Campaign.Current.GameMenuManager.SetNextMenu("rival_gang_quest_before_fight");
			}

			// Token: 0x06000E9E RID: 3742 RVA: 0x0005D124 File Offset: 0x0005B324
			private void rival_gang_need_more_time_on_consequence()
			{
				if (Campaign.Current.CurrentMenuContext.GameMenu.StringId == "rival_gang_quest_wait_duration_is_over")
				{
					Campaign.Current.GameMenuManager.SetNextMenu("town_wait_menus");
				}
			}

			// Token: 0x06000E9F RID: 3743 RVA: 0x0005D15C File Offset: 0x0005B35C
			private void AddQuestGiverGangLeaderOnSuccessDialogFlow()
			{
				Campaign.Current.ConversationManager.AddDialogFlow(DialogFlow.CreateDialogFlow("start", 125).NpcLine("{=zNPzh5jO}Ah! Now that was as good a fight as any I've had. Here, take this purse, It is all yours as {QUEST_GIVER.LINK} has promised.[ib:hip2][if:convo_huge_smile]", null, null).Condition(delegate
				{
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, null, false);
					return base.IsOngoing && Hero.OneToOneConversationHero == this._allyGangLeaderHenchmanHero;
				}).Consequence(delegate
				{
					Campaign.Current.ConversationManager.ConversationEndOneShot += this.OnQuestSucceeded;
				}).CloseDialog(), null);
			}

			// Token: 0x06000EA0 RID: 3744 RVA: 0x0005D1B8 File Offset: 0x0005B3B8
			private CharacterObject GetTroopTypeTemplateForDifficulty()
			{
				int difficultyRange = MBMath.ClampInt(MathF.Ceiling(this._issueDifficulty / 0.1f), 1, 10);
				CharacterObject characterObject;
				if (difficultyRange == 1)
				{
					characterObject = CharacterObject.All.FirstOrDefault((CharacterObject t) => t.StringId == "looter");
				}
				else if (difficultyRange == 10)
				{
					characterObject = CharacterObject.All.FirstOrDefault((CharacterObject t) => t.StringId == "mercenary_8");
				}
				else
				{
					characterObject = CharacterObject.All.FirstOrDefault((CharacterObject t) => t.StringId == "mercenary_" + (difficultyRange - 1));
				}
				if (characterObject == null)
				{
					Debug.FailedAssert("Can't find troop in rival gang leader quest", "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox\\Issues\\RivalGangMovingInIssueBehavior.cs", "GetTroopTypeTemplateForDifficulty", 785);
					characterObject = CharacterObject.All.First((CharacterObject t) => t.IsBasicTroop && t.IsSoldier);
				}
				return characterObject;
			}

			// Token: 0x06000EA1 RID: 3745 RVA: 0x0005D2B4 File Offset: 0x0005B4B4
			internal void StartAlleyBattle()
			{
				this.CreateRivalGangLeaderParty();
				this.CreateAllyGangLeaderParty();
				this.PreparePlayerParty();
				PlayerEncounter.RestartPlayerEncounter(this._rivalGangLeaderParty.Party, PartyBase.MainParty, false);
				PlayerEncounter.StartBattle();
				this._allyGangLeaderParty.MapEventSide = PlayerEncounter.Battle.GetMapEventSide(PlayerEncounter.Battle.PlayerSide);
				GameMenu.ActivateGameMenu("rival_gang_quest_after_fight");
				this._isReadyToBeFinalized = true;
				PlayerEncounter.StartCombatMissionWithDialogueInTownCenter(this._rivalGangLeaderHenchmanHero.CharacterObject);
			}

			// Token: 0x06000EA2 RID: 3746 RVA: 0x0005D330 File Offset: 0x0005B530
			private void CreateRivalGangLeaderParty()
			{
				this._rivalGangLeaderParty = MobileParty.CreateParty("rival_gang_leader_party", null, null);
				TextObject textObject = new TextObject("{=u4jhIFwG}{GANG_LEADER}'s Party", null);
				textObject.SetTextVariable("RIVAL_GANG_LEADER", this._rivalGangLeader.Name);
				textObject.SetTextVariable("GANG_LEADER", this._rivalGangLeader.Name);
				this._rivalGangLeaderParty.InitializeMobilePartyAroundPosition(new TroopRoster(this._rivalGangLeaderParty.Party), new TroopRoster(this._rivalGangLeaderParty.Party), this._questSettlement.GatePosition, 1f, 0.5f);
				this._rivalGangLeaderParty.SetCustomName(textObject);
				this._rivalGangLeaderParty.SetPartyUsedByQuest(true);
				CharacterObject troopTypeTemplateForDifficulty = this.GetTroopTypeTemplateForDifficulty();
				this._rivalGangLeaderParty.MemberRoster.AddToCounts(troopTypeTemplateForDifficulty, 15, false, 0, 0, true, -1);
				CharacterObject @object = MBObjectManager.Instance.GetObject<CharacterObject>("gangster_3");
				this._rivalGangLeaderHenchmanHero = HeroCreator.CreateSpecialHero(@object, null, null, null, -1);
				TextObject textObject2 = new TextObject("{=zJqEdDiq}Henchman of {GANG_LEADER}", null);
				textObject2.SetTextVariable("GANG_LEADER", this._rivalGangLeader.Name);
				this._rivalGangLeaderHenchmanHero.SetName(textObject2, textObject2);
				this._rivalGangLeaderParty.MemberRoster.AddToCounts(this._rivalGangLeaderHenchmanHero.CharacterObject, 1, false, 0, 0, true, -1);
				Settlement closestHideout = SettlementHelper.FindNearestHideout((Settlement x) => x.IsActive, null);
				Clan actualClan = Clan.BanditFactions.FirstOrDefaultQ((Clan t) => t.Culture == closestHideout.Culture);
				this._rivalGangLeaderParty.ActualClan = actualClan;
				EnterSettlementAction.ApplyForParty(this._rivalGangLeaderParty, this._questSettlement);
			}

			// Token: 0x06000EA3 RID: 3747 RVA: 0x0005D4E0 File Offset: 0x0005B6E0
			private void CreateAllyGangLeaderParty()
			{
				this._allyGangLeaderParty = MobileParty.CreateParty("ally_gang_leader_party", null, null);
				TextObject textObject = new TextObject("{=u4jhIFwG}{GANG_LEADER}'s Party", null);
				textObject.SetTextVariable("GANG_LEADER", base.QuestGiver.Name);
				this._allyGangLeaderParty.InitializeMobilePartyAroundPosition(new TroopRoster(this._allyGangLeaderParty.Party), new TroopRoster(this._allyGangLeaderParty.Party), this._questSettlement.GatePosition, 1f, 0.5f);
				this._allyGangLeaderParty.SetCustomName(textObject);
				this._allyGangLeaderParty.SetPartyUsedByQuest(true);
				CharacterObject troopTypeTemplateForDifficulty = this.GetTroopTypeTemplateForDifficulty();
				this._allyGangLeaderParty.MemberRoster.AddToCounts(troopTypeTemplateForDifficulty, 20, false, 0, 0, true, -1);
				CharacterObject @object = MBObjectManager.Instance.GetObject<CharacterObject>("gangster_2");
				this._allyGangLeaderHenchmanHero = HeroCreator.CreateSpecialHero(@object, null, null, null, -1);
				TextObject textObject2 = new TextObject("{=zJqEdDiq}Henchman of {GANG_LEADER}", null);
				textObject2.SetTextVariable("GANG_LEADER", base.QuestGiver.Name);
				this._allyGangLeaderHenchmanHero.SetName(textObject2, textObject2);
				this._allyGangLeaderParty.MemberRoster.AddToCounts(this._allyGangLeaderHenchmanHero.CharacterObject, 1, false, 0, 0, true, -1);
				Settlement closestHideout = SettlementHelper.FindNearestHideout((Settlement x) => x.IsActive, null);
				Clan actualClan = Clan.BanditFactions.FirstOrDefaultQ((Clan t) => t.Culture == closestHideout.Culture);
				this._allyGangLeaderParty.ActualClan = actualClan;
				EnterSettlementAction.ApplyForParty(this._allyGangLeaderParty, this._questSettlement);
			}

			// Token: 0x06000EA4 RID: 3748 RVA: 0x0005D678 File Offset: 0x0005B878
			private void PreparePlayerParty()
			{
				this._allPlayerTroops.Clear();
				foreach (TroopRosterElement troopRosterElement in PartyBase.MainParty.MemberRoster.GetTroopRoster())
				{
					if (!troopRosterElement.Character.IsPlayerCharacter)
					{
						this._allPlayerTroops.Add(troopRosterElement);
					}
				}
				PartyBase.MainParty.MemberRoster.RemoveIf((TroopRosterElement t) => !t.Character.IsPlayerCharacter);
				if (!this._allPlayerTroops.IsEmpty<TroopRosterElement>())
				{
					this._sentTroops.Clear();
					int num = 5;
					foreach (TroopRosterElement troopRosterElement2 in from t in this._allPlayerTroops
					orderby t.Character.Level descending
					select t)
					{
						if (num <= 0)
						{
							break;
						}
						int num2 = 0;
						while (num2 < troopRosterElement2.Number - troopRosterElement2.WoundedNumber && num > 0)
						{
							this._sentTroops.Add(troopRosterElement2.Character);
							num--;
							num2++;
						}
					}
					foreach (CharacterObject character in this._sentTroops)
					{
						PartyBase.MainParty.MemberRoster.AddToCounts(character, 1, false, 0, 0, true, -1);
					}
				}
			}

			// Token: 0x06000EA5 RID: 3749 RVA: 0x0005D82C File Offset: 0x0005BA2C
			internal void HandlePlayerEncounterResult(bool hasPlayerWon)
			{
				PlayerEncounter.Finish(false);
				EncounterManager.StartSettlementEncounter(MobileParty.MainParty, this._questSettlement);
				GameMenu.SwitchToMenu("town");
				TroopRoster troopRoster = PartyBase.MainParty.MemberRoster.CloneRosterData();
				PartyBase.MainParty.MemberRoster.RemoveIf((TroopRosterElement t) => !t.Character.IsPlayerCharacter);
				using (List<TroopRosterElement>.Enumerator enumerator = this._allPlayerTroops.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						TroopRosterElement playerTroop = enumerator.Current;
						int num = troopRoster.FindIndexOfTroop(playerTroop.Character);
						int num2 = playerTroop.Number;
						int num3 = playerTroop.WoundedNumber;
						int num4 = playerTroop.Xp;
						if (num >= 0)
						{
							TroopRosterElement elementCopyAtIndex = troopRoster.GetElementCopyAtIndex(num);
							num2 -= this._sentTroops.Count((CharacterObject t) => t == playerTroop.Character) - elementCopyAtIndex.Number;
							num3 += elementCopyAtIndex.WoundedNumber;
							num4 += elementCopyAtIndex.DeltaXp;
						}
						PartyBase.MainParty.MemberRoster.AddToCounts(playerTroop.Character, num2, false, num3, num4, true, -1);
					}
				}
				if (this._rivalGangLeader.PartyBelongedTo == this._rivalGangLeaderParty)
				{
					this._rivalGangLeaderParty.MemberRoster.AddToCounts(this._rivalGangLeader.CharacterObject, -1, false, 0, 0, true, -1);
				}
				if (hasPlayerWon)
				{
					if (!this._hasBetrayedQuestGiver)
					{
						this.AddQuestGiverGangLeaderOnSuccessDialogFlow();
						this.SpawnAllyHenchmanAfterMissionSuccess();
						PlayerEncounter.LocationEncounter.CreateAndOpenMissionController(LocationComplex.Current.GetLocationOfCharacter(this._allyGangLeaderHenchmanHero), null, this._allyGangLeaderHenchmanHero.CharacterObject, null);
						return;
					}
					this.OnBattleWonWithBetrayal();
					return;
				}
				else
				{
					if (!this._hasBetrayedQuestGiver)
					{
						this.OnQuestFailedWithDefeat();
						return;
					}
					this.OnBattleLostWithBetrayal();
					return;
				}
			}

			// Token: 0x06000EA6 RID: 3750 RVA: 0x0005DA20 File Offset: 0x0005BC20
			protected override void RegisterEvents()
			{
				CampaignEvents.HeroKilledEvent.AddNonSerializedListener(this, new Action<Hero, Hero, KillCharacterAction.KillCharacterActionDetail, bool>(this.OnHeroKilled));
				CampaignEvents.AlleyClearedByPlayer.AddNonSerializedListener(this, new Action<Alley>(this.OnAlleyClearedByPlayer));
				CampaignEvents.AlleyOccupiedByPlayer.AddNonSerializedListener(this, new Action<Alley, TroopRoster>(this.OnAlleyOccupiedByPlayer));
				CampaignEvents.WarDeclared.AddNonSerializedListener(this, new Action<IFaction, IFaction, DeclareWarAction.DeclareWarDetail>(this.OnWarDeclared));
				CampaignEvents.OnSiegeEventStartedEvent.AddNonSerializedListener(this, new Action<SiegeEvent>(this.OnSiegeEventStarted));
				CampaignEvents.OnClanChangedKingdomEvent.AddNonSerializedListener(this, new Action<Clan, Kingdom, Kingdom, ChangeKingdomAction.ChangeKingdomActionDetail, bool>(this.OnClanChangedKingdom));
				CampaignEvents.OnSettlementOwnerChangedEvent.AddNonSerializedListener(this, new Action<Settlement, bool, Hero, Hero, Hero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail>(this.OnSettlementOwnerChanged));
			}

			// Token: 0x06000EA7 RID: 3751 RVA: 0x0005DAD0 File Offset: 0x0005BCD0
			private void SpawnAllyHenchmanAfterMissionSuccess()
			{
				Monster monsterWithSuffix = TaleWorlds.Core.FaceGen.GetMonsterWithSuffix(this._allyGangLeaderHenchmanHero.CharacterObject.Race, "_settlement");
				LocationCharacter locationCharacter = new LocationCharacter(new AgentData(new SimpleAgentOrigin(this._allyGangLeaderHenchmanHero.CharacterObject, -1, null, default(UniqueTroopDescriptor))).Monster(monsterWithSuffix), new LocationCharacter.AddBehaviorsDelegate(SandBoxManager.Instance.AgentBehaviorManager.AddWandererBehaviors), "npc_common", true, LocationCharacter.CharacterRelations.Neutral, null, true, false, null, false, false, true);
				LocationComplex.Current.GetLocationWithId("center").AddCharacter(locationCharacter);
			}

			// Token: 0x06000EA8 RID: 3752 RVA: 0x0005DB60 File Offset: 0x0005BD60
			private void OnSettlementOwnerChanged(Settlement settlement, bool openToClaim, Hero newOwner, Hero oldOwner, Hero capturerHero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail detail)
			{
				if (settlement == base.QuestGiver.CurrentSettlement && newOwner == Hero.MainHero)
				{
					base.AddLog(this.OwnerOfQuestSettlementIsPlayerClanLogText, false);
					base.QuestGiver.AddPower(-10f);
					ChangeRelationAction.ApplyPlayerRelation(base.QuestGiver, -5, true, true);
					base.CompleteQuestWithCancel(null);
				}
			}

			// Token: 0x06000EA9 RID: 3753 RVA: 0x0005DBB7 File Offset: 0x0005BDB7
			public override void OnHeroCanHaveQuestOrIssueInfoIsRequested(Hero hero, ref bool result)
			{
				if (hero == this._rivalGangLeader)
				{
					result = false;
				}
			}

			// Token: 0x06000EAA RID: 3754 RVA: 0x0005DBC5 File Offset: 0x0005BDC5
			private void OnClanChangedKingdom(Clan clan, Kingdom oldKingdom, Kingdom newKingdom, ChangeKingdomAction.ChangeKingdomActionDetail detail, bool showNotification = true)
			{
				if (base.QuestGiver.CurrentSettlement.MapFaction.IsAtWarWith(Hero.MainHero.MapFaction))
				{
					base.CompleteQuestWithCancel(this._onQuestCancelledDueToWarLogText);
				}
			}

			// Token: 0x06000EAB RID: 3755 RVA: 0x0005DBF4 File Offset: 0x0005BDF4
			private void OnWarDeclared(IFaction faction1, IFaction faction2, DeclareWarAction.DeclareWarDetail detail)
			{
				QuestHelper.CheckWarDeclarationAndFailOrCancelTheQuest(this, faction1, faction2, detail, this._playerDeclaredWarQuestLogText, this._onQuestCancelledDueToWarLogText, false);
			}

			// Token: 0x06000EAC RID: 3756 RVA: 0x0005DC0C File Offset: 0x0005BE0C
			private void OnSiegeEventStarted(SiegeEvent siegeEvent)
			{
				if (siegeEvent.BesiegedSettlement == this._questSettlement)
				{
					base.AddLog(this._onQuestCancelledDueToSiegeLogText, false);
					base.CompleteQuestWithCancel(null);
				}
			}

			// Token: 0x06000EAD RID: 3757 RVA: 0x0005DC34 File Offset: 0x0005BE34
			protected override void HourlyTick()
			{
				if (RivalGangMovingInIssueBehavior.Instance != null && RivalGangMovingInIssueBehavior.Instance.IsOngoing && (2f - RivalGangMovingInIssueBehavior.Instance._preparationCompletionTime.RemainingDaysFromNow) / 2f >= 1f && !this._preparationsComplete && CampaignTime.Now.IsNightTime)
				{
					this.OnGuestGiverPreparationsCompleted();
				}
			}

			// Token: 0x06000EAE RID: 3758 RVA: 0x0005DC98 File Offset: 0x0005BE98
			private void OnHeroKilled(Hero victim, Hero killer, KillCharacterAction.KillCharacterActionDetail detail, bool showNotification = true)
			{
				if (victim == this._rivalGangLeader)
				{
					TextObject textObject = (detail == KillCharacterAction.KillCharacterActionDetail.Lost) ? this.TargetHeroDisappearedLogText : this.TargetHeroDiedLogText;
					StringHelpers.SetCharacterProperties("QUEST_TARGET", this._rivalGangLeader.CharacterObject, textObject, false);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					base.AddLog(textObject, false);
					base.CompleteQuestWithCancel(null);
				}
			}

			// Token: 0x06000EAF RID: 3759 RVA: 0x0005DD01 File Offset: 0x0005BF01
			private void OnPlayerAlleyFightEnd(Alley alley)
			{
				if (!this._isReadyToBeFinalized)
				{
					if (alley.Owner == this._rivalGangLeader)
					{
						this.OnPlayerAttackedRivalGangAlley();
						return;
					}
					if (alley.Owner == base.QuestGiver)
					{
						this.OnPlayerAttackedQuestGiverAlley();
					}
				}
			}

			// Token: 0x06000EB0 RID: 3760 RVA: 0x0005DD34 File Offset: 0x0005BF34
			private void OnAlleyClearedByPlayer(Alley alley)
			{
				this.OnPlayerAlleyFightEnd(alley);
			}

			// Token: 0x06000EB1 RID: 3761 RVA: 0x0005DD3D File Offset: 0x0005BF3D
			private void OnAlleyOccupiedByPlayer(Alley alley, TroopRoster troops)
			{
				this.OnPlayerAlleyFightEnd(alley);
			}

			// Token: 0x06000EB2 RID: 3762 RVA: 0x0005DD46 File Offset: 0x0005BF46
			private void OnPlayerAttackedRivalGangAlley()
			{
				base.AddLog(this._playerStartedAlleyFightWithRivalGangLeader, false);
				base.CompleteQuestWithCancel(null);
			}

			// Token: 0x06000EB3 RID: 3763 RVA: 0x0005DD60 File Offset: 0x0005BF60
			private void OnPlayerAttackedQuestGiverAlley()
			{
				TraitLevelingHelper.OnIssueSolvedThroughQuest(base.QuestGiver, new Tuple<TraitObject, int>[]
				{
					new Tuple<TraitObject, int>(DefaultTraits.Honor, -150)
				});
				base.QuestGiver.AddPower(-10f);
				ChangeRelationAction.ApplyPlayerRelation(base.QuestGiver, -8, true, true);
				this._questSettlement.Town.Security += -10f;
				base.AddLog(this._playerStartedAlleyFightWithQuestgiver, false);
				base.CompleteQuestWithFail(null);
			}

			// Token: 0x06000EB4 RID: 3764 RVA: 0x0005DDE0 File Offset: 0x0005BFE0
			protected override void OnTimedOut()
			{
				this.OnQuestFailedWithRejectionOrTimeout();
			}

			// Token: 0x06000EB5 RID: 3765 RVA: 0x0005DDE8 File Offset: 0x0005BFE8
			private void OnGuestGiverPreparationsCompleted()
			{
				this._preparationsComplete = true;
				if (Settlement.CurrentSettlement != null && Settlement.CurrentSettlement == this._questSettlement && Campaign.Current.CurrentMenuContext != null && Campaign.Current.CurrentMenuContext.GameMenu.StringId == "town_wait_menus")
				{
					Campaign.Current.CurrentMenuContext.SwitchToMenu("rival_gang_quest_wait_duration_is_over");
				}
				TextObject textObject = new TextObject("{=DUKbtlNb}{QUEST_GIVER.LINK} has finally sent a messenger telling you it's time to meet {?QUEST_GIVER.GENDER}her{?}him{\\?} and join the fight.", null);
				StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
				base.AddLog(this._onQuestPreperationsCompletedLogText, false);
				MBInformationManager.AddQuickInformation(textObject, 0, null, "");
			}

			// Token: 0x06000EB6 RID: 3766 RVA: 0x0005DE90 File Offset: 0x0005C090
			private void OnQuestAccepted()
			{
				base.StartQuest();
				this._onQuestStartedLog = base.AddLog(this._onQuestStartedLogText, false);
				Campaign.Current.ConversationManager.AddDialogFlow(this.GetRivalGangLeaderDialogFlow(), this);
				Campaign.Current.ConversationManager.AddDialogFlow(this.GetQuestGiverPreparationCompletedDialogFlow(), this);
			}

			// Token: 0x06000EB7 RID: 3767 RVA: 0x0005DEE4 File Offset: 0x0005C0E4
			private void OnQuestSucceeded()
			{
				this._onQuestSucceededLog = base.AddLog(this._onQuestSucceededLogText, false);
				GainRenownAction.Apply(Hero.MainHero, 1f, false);
				GiveGoldAction.ApplyBetweenCharacters(null, Hero.MainHero, this._rewardGold, false);
				TraitLevelingHelper.OnIssueSolvedThroughQuest(base.QuestGiver, new Tuple<TraitObject, int>[]
				{
					new Tuple<TraitObject, int>(DefaultTraits.Honor, 50)
				});
				base.QuestGiver.AddPower(10f);
				this._rivalGangLeader.AddPower(-10f);
				this.RelationshipChangeWithQuestGiver = 5;
				ChangeRelationAction.ApplyPlayerRelation(this._rivalGangLeader, -5, true, true);
				base.CompleteQuestWithSuccess();
			}

			// Token: 0x06000EB8 RID: 3768 RVA: 0x0005DF82 File Offset: 0x0005C182
			private void OnQuestFailedWithRejectionOrTimeout()
			{
				base.AddLog(this._onQuestFailedWithRejectionLogText, false);
				TraitLevelingHelper.OnIssueFailed(base.QuestGiver, new Tuple<TraitObject, int>[]
				{
					new Tuple<TraitObject, int>(DefaultTraits.Honor, -20)
				});
				this.RelationshipChangeWithQuestGiver = -5;
				this.ApplyQuestFailConsequences();
			}

			// Token: 0x06000EB9 RID: 3769 RVA: 0x0005DFC0 File Offset: 0x0005C1C0
			private void OnBattleWonWithBetrayal()
			{
				base.AddLog(this._onQuestFailedWithBetrayalLogText, false);
				this.RelationshipChangeWithQuestGiver = -15;
				if (!this._rivalGangLeader.IsDead)
				{
					ChangeRelationAction.ApplyPlayerRelation(this._rivalGangLeader, 5, true, true);
				}
				GiveGoldAction.ApplyBetweenCharacters(null, Hero.MainHero, this._rewardGold * 2, false);
				TraitLevelingHelper.OnIssueSolvedThroughBetrayal(base.QuestGiver, new Tuple<TraitObject, int>[]
				{
					new Tuple<TraitObject, int>(DefaultTraits.Honor, -100)
				});
				this._rivalGangLeader.AddPower(10f);
				this.ApplyQuestFailConsequences();
				base.CompleteQuestWithBetrayal(null);
			}

			// Token: 0x06000EBA RID: 3770 RVA: 0x0005E050 File Offset: 0x0005C250
			private void OnBattleLostWithBetrayal()
			{
				base.AddLog(this._onQuestFailedWithBetrayalLogText, false);
				this.RelationshipChangeWithQuestGiver = -10;
				if (!this._rivalGangLeader.IsDead)
				{
					ChangeRelationAction.ApplyPlayerRelation(this._rivalGangLeader, -5, true, true);
				}
				this._rivalGangLeader.AddPower(-10f);
				TraitLevelingHelper.OnIssueSolvedThroughBetrayal(base.QuestGiver, new Tuple<TraitObject, int>[]
				{
					new Tuple<TraitObject, int>(DefaultTraits.Honor, -100)
				});
				this.ApplyQuestFailConsequences();
				base.CompleteQuestWithBetrayal(null);
			}

			// Token: 0x06000EBB RID: 3771 RVA: 0x0005E0CC File Offset: 0x0005C2CC
			private void OnQuestFailedWithDefeat()
			{
				this.RelationshipChangeWithQuestGiver = -5;
				base.AddLog(this._onQuestFailedWithDefeatLogText, false);
				this.ApplyQuestFailConsequences();
				base.CompleteQuestWithFail(null);
			}

			// Token: 0x06000EBC RID: 3772 RVA: 0x0005E0F4 File Offset: 0x0005C2F4
			private void ApplyQuestFailConsequences()
			{
				base.QuestGiver.AddPower(-10f);
				this._questSettlement.Town.Security += -10f;
				if (this._rivalGangLeaderParty != null && this._rivalGangLeaderParty.IsActive)
				{
					DestroyPartyAction.Apply(null, this._rivalGangLeaderParty);
				}
			}

			// Token: 0x06000EBD RID: 3773 RVA: 0x0005E150 File Offset: 0x0005C350
			protected override void OnFinalize()
			{
				if (this._rivalGangLeaderParty != null && this._rivalGangLeaderParty.IsActive)
				{
					DestroyPartyAction.Apply(null, this._rivalGangLeaderParty);
				}
				if (this._allyGangLeaderParty != null && this._allyGangLeaderParty.IsActive)
				{
					DestroyPartyAction.Apply(null, this._allyGangLeaderParty);
				}
				if (this._allyGangLeaderHenchmanHero != null && this._allyGangLeaderHenchmanHero.IsAlive)
				{
					this._allyGangLeaderHenchmanHero.SetNewOccupation(Occupation.NotAssigned);
					KillCharacterAction.ApplyByRemove(this._allyGangLeaderHenchmanHero, false, true);
				}
				if (this._rivalGangLeaderHenchmanHero != null && this._rivalGangLeaderHenchmanHero.IsAlive)
				{
					this._rivalGangLeaderHenchmanHero.SetNewOccupation(Occupation.NotAssigned);
					KillCharacterAction.ApplyByRemove(this._rivalGangLeaderHenchmanHero, false, true);
				}
			}

			// Token: 0x06000EBE RID: 3774 RVA: 0x0005E1FB File Offset: 0x0005C3FB
			internal static void AutoGeneratedStaticCollectObjectsRivalGangMovingInIssueQuest(object o, List<object> collectedObjects)
			{
				((RivalGangMovingInIssueBehavior.RivalGangMovingInIssueQuest)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
			}

			// Token: 0x06000EBF RID: 3775 RVA: 0x0005E20C File Offset: 0x0005C40C
			protected override void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
			{
				base.AutoGeneratedInstanceCollectObjects(collectedObjects);
				collectedObjects.Add(this._rivalGangLeader);
				collectedObjects.Add(this._rivalGangLeaderParty);
				CampaignTime.AutoGeneratedStaticCollectObjectsCampaignTime(this._preparationCompletionTime, collectedObjects);
				CampaignTime.AutoGeneratedStaticCollectObjectsCampaignTime(this._questTimeoutTime, collectedObjects);
			}

			// Token: 0x06000EC0 RID: 3776 RVA: 0x0005E25A File Offset: 0x0005C45A
			internal static object AutoGeneratedGetMemberValue_rivalGangLeader(object o)
			{
				return ((RivalGangMovingInIssueBehavior.RivalGangMovingInIssueQuest)o)._rivalGangLeader;
			}

			// Token: 0x06000EC1 RID: 3777 RVA: 0x0005E267 File Offset: 0x0005C467
			internal static object AutoGeneratedGetMemberValue_timeoutDurationInDays(object o)
			{
				return ((RivalGangMovingInIssueBehavior.RivalGangMovingInIssueQuest)o)._timeoutDurationInDays;
			}

			// Token: 0x06000EC2 RID: 3778 RVA: 0x0005E279 File Offset: 0x0005C479
			internal static object AutoGeneratedGetMemberValue_isFinalStage(object o)
			{
				return ((RivalGangMovingInIssueBehavior.RivalGangMovingInIssueQuest)o)._isFinalStage;
			}

			// Token: 0x06000EC3 RID: 3779 RVA: 0x0005E28B File Offset: 0x0005C48B
			internal static object AutoGeneratedGetMemberValue_isReadyToBeFinalized(object o)
			{
				return ((RivalGangMovingInIssueBehavior.RivalGangMovingInIssueQuest)o)._isReadyToBeFinalized;
			}

			// Token: 0x06000EC4 RID: 3780 RVA: 0x0005E29D File Offset: 0x0005C49D
			internal static object AutoGeneratedGetMemberValue_hasBetrayedQuestGiver(object o)
			{
				return ((RivalGangMovingInIssueBehavior.RivalGangMovingInIssueQuest)o)._hasBetrayedQuestGiver;
			}

			// Token: 0x06000EC5 RID: 3781 RVA: 0x0005E2AF File Offset: 0x0005C4AF
			internal static object AutoGeneratedGetMemberValue_rivalGangLeaderParty(object o)
			{
				return ((RivalGangMovingInIssueBehavior.RivalGangMovingInIssueQuest)o)._rivalGangLeaderParty;
			}

			// Token: 0x06000EC6 RID: 3782 RVA: 0x0005E2BC File Offset: 0x0005C4BC
			internal static object AutoGeneratedGetMemberValue_preparationCompletionTime(object o)
			{
				return ((RivalGangMovingInIssueBehavior.RivalGangMovingInIssueQuest)o)._preparationCompletionTime;
			}

			// Token: 0x06000EC7 RID: 3783 RVA: 0x0005E2CE File Offset: 0x0005C4CE
			internal static object AutoGeneratedGetMemberValue_questTimeoutTime(object o)
			{
				return ((RivalGangMovingInIssueBehavior.RivalGangMovingInIssueQuest)o)._questTimeoutTime;
			}

			// Token: 0x06000EC8 RID: 3784 RVA: 0x0005E2E0 File Offset: 0x0005C4E0
			internal static object AutoGeneratedGetMemberValue_preparationsComplete(object o)
			{
				return ((RivalGangMovingInIssueBehavior.RivalGangMovingInIssueQuest)o)._preparationsComplete;
			}

			// Token: 0x06000EC9 RID: 3785 RVA: 0x0005E2F2 File Offset: 0x0005C4F2
			internal static object AutoGeneratedGetMemberValue_rewardGold(object o)
			{
				return ((RivalGangMovingInIssueBehavior.RivalGangMovingInIssueQuest)o)._rewardGold;
			}

			// Token: 0x06000ECA RID: 3786 RVA: 0x0005E304 File Offset: 0x0005C504
			internal static object AutoGeneratedGetMemberValue_issueDifficulty(object o)
			{
				return ((RivalGangMovingInIssueBehavior.RivalGangMovingInIssueQuest)o)._issueDifficulty;
			}

			// Token: 0x04000635 RID: 1589
			private const int QuestGiverRelationChangeOnSuccess = 5;

			// Token: 0x04000636 RID: 1590
			private const int RivalGangLeaderRelationChangeOnSuccess = -5;

			// Token: 0x04000637 RID: 1591
			private const int QuestGiverNotablePowerChangeOnSuccess = 10;

			// Token: 0x04000638 RID: 1592
			private const int RivalGangLeaderPowerChangeOnSuccess = -10;

			// Token: 0x04000639 RID: 1593
			private const int RenownChangeOnSuccess = 1;

			// Token: 0x0400063A RID: 1594
			private const int QuestGiverRelationChangeOnFail = -5;

			// Token: 0x0400063B RID: 1595
			private const int QuestGiverRelationChangeOnTimedOut = -5;

			// Token: 0x0400063C RID: 1596
			private const int NotablePowerChangeOnFail = -10;

			// Token: 0x0400063D RID: 1597
			private const int TownSecurityChangeOnFail = -10;

			// Token: 0x0400063E RID: 1598
			private const int RivalGangLeaderRelationChangeOnSuccessfulBetrayal = 5;

			// Token: 0x0400063F RID: 1599
			private const int QuestGiverRelationChangeOnSuccessfulBetrayal = -15;

			// Token: 0x04000640 RID: 1600
			private const int RivalGangLeaderPowerChangeOnSuccessfulBetrayal = 10;

			// Token: 0x04000641 RID: 1601
			private const int QuestGiverRelationChangeOnFailedBetrayal = -10;

			// Token: 0x04000642 RID: 1602
			private const int PlayerAttackedQuestGiverHonorChange = -150;

			// Token: 0x04000643 RID: 1603
			private const int PlayerAttackedQuestGiverPowerChange = -10;

			// Token: 0x04000644 RID: 1604
			private const int NumberofRegularEnemyTroops = 15;

			// Token: 0x04000645 RID: 1605
			private const int PlayerAttackedQuestGiverRelationChange = -8;

			// Token: 0x04000646 RID: 1606
			private const int PlayerAttackedQuestGiverSecurityChange = -10;

			// Token: 0x04000647 RID: 1607
			private const int NumberOfRegularAllyTroops = 20;

			// Token: 0x04000648 RID: 1608
			private const int MaxNumberOfPlayerOwnedTroops = 5;

			// Token: 0x04000649 RID: 1609
			private const string AllyGangLeaderHenchmanStringId = "gangster_2";

			// Token: 0x0400064A RID: 1610
			private const string RivalGangLeaderHenchmanStringId = "gangster_3";

			// Token: 0x0400064B RID: 1611
			private const int PreparationDurationInDays = 2;

			// Token: 0x0400064C RID: 1612
			[SaveableField(10)]
			internal readonly Hero _rivalGangLeader;

			// Token: 0x0400064D RID: 1613
			[SaveableField(20)]
			private MobileParty _rivalGangLeaderParty;

			// Token: 0x0400064E RID: 1614
			private Hero _rivalGangLeaderHenchmanHero;

			// Token: 0x0400064F RID: 1615
			[SaveableField(30)]
			private readonly CampaignTime _preparationCompletionTime;

			// Token: 0x04000650 RID: 1616
			private Hero _allyGangLeaderHenchmanHero;

			// Token: 0x04000651 RID: 1617
			private MobileParty _allyGangLeaderParty;

			// Token: 0x04000652 RID: 1618
			[SaveableField(40)]
			private readonly CampaignTime _questTimeoutTime;

			// Token: 0x04000653 RID: 1619
			[SaveableField(60)]
			internal readonly float _timeoutDurationInDays;

			// Token: 0x04000654 RID: 1620
			[SaveableField(70)]
			internal bool _isFinalStage;

			// Token: 0x04000655 RID: 1621
			[SaveableField(80)]
			internal bool _isReadyToBeFinalized;

			// Token: 0x04000656 RID: 1622
			[SaveableField(90)]
			internal bool _hasBetrayedQuestGiver;

			// Token: 0x04000657 RID: 1623
			private List<TroopRosterElement> _allPlayerTroops;

			// Token: 0x04000658 RID: 1624
			private List<CharacterObject> _sentTroops;

			// Token: 0x04000659 RID: 1625
			[SaveableField(110)]
			private bool _preparationsComplete;

			// Token: 0x0400065A RID: 1626
			[SaveableField(120)]
			private int _rewardGold;

			// Token: 0x0400065B RID: 1627
			[SaveableField(130)]
			private float _issueDifficulty;

			// Token: 0x0400065C RID: 1628
			private Settlement _questSettlement;

			// Token: 0x0400065D RID: 1629
			private JournalLog _onQuestStartedLog;

			// Token: 0x0400065E RID: 1630
			private JournalLog _onQuestSucceededLog;
		}
	}
}
