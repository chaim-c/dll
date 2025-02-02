using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using SandBox.BoardGames.MissionLogics;
using SandBox.CampaignBehaviors;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.Issues;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.CampaignSystem.Siege;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.SaveSystem;

namespace SandBox.Issues
{
	// Token: 0x0200008C RID: 140
	public class RuralNotableInnAndOutIssueBehavior : CampaignBehaviorBase
	{
		// Token: 0x06000571 RID: 1393 RVA: 0x000249BC File Offset: 0x00022BBC
		public override void RegisterEvents()
		{
			CampaignEvents.OnCheckForIssueEvent.AddNonSerializedListener(this, new Action<Hero>(this.OnCheckForIssue));
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x000249D5 File Offset: 0x00022BD5
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x000249D8 File Offset: 0x00022BD8
		private bool ConditionsHold(Hero issueGiver)
		{
			return (issueGiver.IsRuralNotable || issueGiver.IsHeadman) && issueGiver.CurrentSettlement.Village != null && issueGiver.CurrentSettlement.Village.Bound.IsTown && issueGiver.GetTraitLevel(DefaultTraits.Mercy) + issueGiver.GetTraitLevel(DefaultTraits.Honor) < 0 && Campaign.Current.GetCampaignBehavior<BoardGameCampaignBehavior>() != null && issueGiver.CurrentSettlement.Village.Bound.Culture.BoardGame != CultureObject.BoardGameType.None;
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00024A64 File Offset: 0x00022C64
		public void OnCheckForIssue(Hero hero)
		{
			if (this.ConditionsHold(hero))
			{
				Campaign.Current.IssueManager.AddPotentialIssueData(hero, new PotentialIssueData(new PotentialIssueData.StartIssueDelegate(this.OnSelected), typeof(RuralNotableInnAndOutIssueBehavior.RuralNotableInnAndOutIssue), IssueBase.IssueFrequency.Common, null));
				return;
			}
			Campaign.Current.IssueManager.AddPotentialIssueData(hero, new PotentialIssueData(typeof(RuralNotableInnAndOutIssueBehavior.RuralNotableInnAndOutIssue), IssueBase.IssueFrequency.Common));
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00024AC8 File Offset: 0x00022CC8
		private IssueBase OnSelected(in PotentialIssueData pid, Hero issueOwner)
		{
			return new RuralNotableInnAndOutIssueBehavior.RuralNotableInnAndOutIssue(issueOwner);
		}

		// Token: 0x04000294 RID: 660
		private const IssueBase.IssueFrequency RuralNotableInnAndOutIssueFrequency = IssueBase.IssueFrequency.Common;

		// Token: 0x04000295 RID: 661
		private const float IssueDuration = 30f;

		// Token: 0x04000296 RID: 662
		private const float QuestDuration = 14f;

		// Token: 0x0200016B RID: 363
		public class RuralNotableInnAndOutIssueTypeDefiner : SaveableTypeDefiner
		{
			// Token: 0x06000ED7 RID: 3799 RVA: 0x0005E529 File Offset: 0x0005C729
			public RuralNotableInnAndOutIssueTypeDefiner() : base(585900)
			{
			}

			// Token: 0x06000ED8 RID: 3800 RVA: 0x0005E536 File Offset: 0x0005C736
			protected override void DefineClassTypes()
			{
				base.AddClassDefinition(typeof(RuralNotableInnAndOutIssueBehavior.RuralNotableInnAndOutIssue), 1, null);
				base.AddClassDefinition(typeof(RuralNotableInnAndOutIssueBehavior.RuralNotableInnAndOutIssueQuest), 2, null);
			}
		}

		// Token: 0x0200016C RID: 364
		public class RuralNotableInnAndOutIssue : IssueBase
		{
			// Token: 0x1700018A RID: 394
			// (get) Token: 0x06000ED9 RID: 3801 RVA: 0x0005E55C File Offset: 0x0005C75C
			public override IssueBase.AlternativeSolutionScaleFlag AlternativeSolutionScaleFlags
			{
				get
				{
					return IssueBase.AlternativeSolutionScaleFlag.FailureRisk;
				}
			}

			// Token: 0x1700018B RID: 395
			// (get) Token: 0x06000EDA RID: 3802 RVA: 0x0005E55F File Offset: 0x0005C75F
			protected override bool IssueQuestCanBeDuplicated
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700018C RID: 396
			// (get) Token: 0x06000EDB RID: 3803 RVA: 0x0005E562 File Offset: 0x0005C762
			public override int AlternativeSolutionBaseNeededMenCount
			{
				get
				{
					return 1 + MathF.Ceiling(3f * base.IssueDifficultyMultiplier);
				}
			}

			// Token: 0x1700018D RID: 397
			// (get) Token: 0x06000EDC RID: 3804 RVA: 0x0005E577 File Offset: 0x0005C777
			protected override int AlternativeSolutionBaseDurationInDaysInternal
			{
				get
				{
					return 1 + MathF.Ceiling(3f * base.IssueDifficultyMultiplier);
				}
			}

			// Token: 0x1700018E RID: 398
			// (get) Token: 0x06000EDD RID: 3805 RVA: 0x0005E58C File Offset: 0x0005C78C
			protected override int RewardGold
			{
				get
				{
					return 1000;
				}
			}

			// Token: 0x1700018F RID: 399
			// (get) Token: 0x06000EDE RID: 3806 RVA: 0x0005E593 File Offset: 0x0005C793
			public override TextObject Title
			{
				get
				{
					return new TextObject("{=uUhtKnfA}Inn and Out", null);
				}
			}

			// Token: 0x17000190 RID: 400
			// (get) Token: 0x06000EDF RID: 3807 RVA: 0x0005E5A0 File Offset: 0x0005C7A0
			public override TextObject Description
			{
				get
				{
					TextObject textObject = new TextObject("{=swamqBRq}{ISSUE_OWNER.NAME} wants you to beat the game host", null);
					StringHelpers.SetCharacterProperties("ISSUE_OWNER", base.IssueOwner.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x17000191 RID: 401
			// (get) Token: 0x06000EE0 RID: 3808 RVA: 0x0005E5D2 File Offset: 0x0005C7D2
			public override TextObject IssueBriefByIssueGiver
			{
				get
				{
					TextObject textObject = new TextObject("{=T0zupcGB}Ah yes... It is a bit embarrassing to mention, [ib:nervous][if:convo_nervous]but... Well, when I am in town, I often have a drink at the inn and perhaps play a round of {GAME_TYPE} or two. Normally I play for low stakes but let's just say that last time the wine went to my head, and I lost something I couldn't afford to lose.", null);
					textObject.SetTextVariable("GAME_TYPE", GameTexts.FindText("str_boardgame_name", this._boardGameType.ToString()));
					return textObject;
				}
			}

			// Token: 0x17000192 RID: 402
			// (get) Token: 0x06000EE1 RID: 3809 RVA: 0x0005E606 File Offset: 0x0005C806
			public override TextObject IssueAcceptByPlayer
			{
				get
				{
					return new TextObject("{=h2tMadtI}I've heard that story before. What did you lose?", null);
				}
			}

			// Token: 0x17000193 RID: 403
			// (get) Token: 0x06000EE2 RID: 3810 RVA: 0x0005E614 File Offset: 0x0005C814
			public override TextObject IssueQuestSolutionExplanationByIssueGiver
			{
				get
				{
					TextObject textObject = new TextObject("{=LD4tGYCA}It's a deed to a plot of farmland. Not a big or valuable plot,[ib:normal][if:convo_disbelief] mind you, but I'd rather not have to explain to my men why they won't be sowing it this year. You can find the man who took it from me at the tavern in {TARGET_SETTLEMENT}. They call him the \"Game Host\". Just be straight about what you're doing. He's in no position to work the land. I don't imagine that he'll turn down a chance to make more money off of it. Bring it back and {REWARD}{GOLD_ICON} is yours.", null);
					textObject.SetTextVariable("REWARD", this.RewardGold);
					textObject.SetTextVariable("GOLD_ICON", "{=!}<img src=\"General\\Icons\\Coin@2x\" extend=\"8\">");
					textObject.SetTextVariable("TARGET_SETTLEMENT", this._targetSettlement.Name);
					return textObject;
				}
			}

			// Token: 0x17000194 RID: 404
			// (get) Token: 0x06000EE3 RID: 3811 RVA: 0x0005E666 File Offset: 0x0005C866
			public override TextObject IssueAlternativeSolutionExplanationByIssueGiver
			{
				get
				{
					TextObject textObject = new TextObject("{=urCXu9Fc}Well, I could try and buy it from him, but I would not really prefer that.[if:convo_innocent_smile] I would be the joke of the tavern for months to come... If you choose to do that, I can only offer {REWARD}{GOLD_ICON} to compensate for your payment. If you have a man with a knack for such games he might do the trick.", null);
					textObject.SetTextVariable("REWARD", this.RewardGold);
					textObject.SetTextVariable("GOLD_ICON", "{=!}<img src=\"General\\Icons\\Coin@2x\" extend=\"8\">");
					return textObject;
				}
			}

			// Token: 0x17000195 RID: 405
			// (get) Token: 0x06000EE4 RID: 3812 RVA: 0x0005E696 File Offset: 0x0005C896
			public override TextObject IssueQuestSolutionAcceptByPlayer
			{
				get
				{
					return new TextObject("{=KMThnMbt}I'll go to the tavern and win it back the same way you lost it.", null);
				}
			}

			// Token: 0x17000196 RID: 406
			// (get) Token: 0x06000EE5 RID: 3813 RVA: 0x0005E6A4 File Offset: 0x0005C8A4
			public override TextObject IssueAlternativeSolutionAcceptByPlayer
			{
				get
				{
					TextObject textObject = new TextObject("{=QdKWaabR}Worry not {ISSUE_OWNER.NAME}, my men will be back with your deed in no time.", null);
					StringHelpers.SetCharacterProperties("ISSUE_OWNER", base.IssueOwner.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x17000197 RID: 407
			// (get) Token: 0x06000EE6 RID: 3814 RVA: 0x0005E6D6 File Offset: 0x0005C8D6
			public override TextObject IssueDiscussAlternativeSolution
			{
				get
				{
					return new TextObject("{=1yEyUHJe}I really hope your men can get my deed back. [if:convo_excited]On my father's name, I will never gamble again.", null);
				}
			}

			// Token: 0x17000198 RID: 408
			// (get) Token: 0x06000EE7 RID: 3815 RVA: 0x0005E6E4 File Offset: 0x0005C8E4
			public override TextObject IssueAlternativeSolutionResponseByIssueGiver
			{
				get
				{
					TextObject textObject = new TextObject("{=kiaN39yb}Thank you, {PLAYER.NAME}. I'm sure your companion will be persuasive.[if:convo_relaxed_happy]", null);
					StringHelpers.SetCharacterProperties("PLAYER", CharacterObject.PlayerCharacter, textObject, false);
					return textObject;
				}
			}

			// Token: 0x17000199 RID: 409
			// (get) Token: 0x06000EE8 RID: 3816 RVA: 0x0005E710 File Offset: 0x0005C910
			public override bool IsThereAlternativeSolution
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700019A RID: 410
			// (get) Token: 0x06000EE9 RID: 3817 RVA: 0x0005E713 File Offset: 0x0005C913
			public override bool IsThereLordSolution
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700019B RID: 411
			// (get) Token: 0x06000EEA RID: 3818 RVA: 0x0005E718 File Offset: 0x0005C918
			protected override TextObject AlternativeSolutionStartLog
			{
				get
				{
					TextObject textObject = new TextObject("{=MIxzaqzi}{QUEST_GIVER.LINK} told you that he lost a land deed in a wager in {TARGET_CITY}. He needs to buy it back, and he wants your companions to intimidate the seller into offering a reasonable price. You asked {COMPANION.LINK} to take {TROOP_COUNT} of your men to go and take care of it. They should report back to you in {RETURN_DAYS} days.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.IssueOwner.CharacterObject, textObject, false);
					StringHelpers.SetCharacterProperties("COMPANION", base.AlternativeSolutionHero.CharacterObject, textObject, false);
					textObject.SetTextVariable("TARGET_CITY", this._targetSettlement.EncyclopediaLinkWithName);
					textObject.SetTextVariable("RETURN_DAYS", base.GetTotalAlternativeSolutionDurationInDays());
					textObject.SetTextVariable("TROOP_COUNT", this.AlternativeSolutionSentTroops.TotalManCount - 1);
					return textObject;
				}
			}

			// Token: 0x06000EEB RID: 3819 RVA: 0x0005E7A4 File Offset: 0x0005C9A4
			public RuralNotableInnAndOutIssue(Hero issueOwner) : base(issueOwner, CampaignTime.DaysFromNow(30f))
			{
				this.InitializeQuestVariables();
			}

			// Token: 0x06000EEC RID: 3820 RVA: 0x0005E7BD File Offset: 0x0005C9BD
			protected override float GetIssueEffectAmountInternal(IssueEffect issueEffect)
			{
				if (issueEffect == DefaultIssueEffects.VillageHearth)
				{
					return -0.1f;
				}
				if (issueEffect == DefaultIssueEffects.IssueOwnerPower)
				{
					return -0.1f;
				}
				return 0f;
			}

			// Token: 0x06000EED RID: 3821 RVA: 0x0005E7E0 File Offset: 0x0005C9E0
			public override ValueTuple<SkillObject, int> GetAlternativeSolutionSkill(Hero hero)
			{
				return new ValueTuple<SkillObject, int>((hero.GetSkillValue(DefaultSkills.Charm) >= hero.GetSkillValue(DefaultSkills.Tactics)) ? DefaultSkills.Charm : DefaultSkills.Tactics, 120);
			}

			// Token: 0x06000EEE RID: 3822 RVA: 0x0005E80D File Offset: 0x0005CA0D
			public override bool AlternativeSolutionCondition(out TextObject explanation)
			{
				explanation = TextObject.Empty;
				return QuestHelper.CheckRosterForAlternativeSolution(MobileParty.MainParty.MemberRoster, base.GetTotalAlternativeSolutionNeededMenCount(), ref explanation, 0, false) && QuestHelper.CheckGoldForAlternativeSolution(1000, ref explanation);
			}

			// Token: 0x1700019C RID: 412
			// (get) Token: 0x06000EEF RID: 3823 RVA: 0x0005E83D File Offset: 0x0005CA3D
			protected override int CompanionSkillRewardXP
			{
				get
				{
					return (int)(500f + 1000f * base.IssueDifficultyMultiplier);
				}
			}

			// Token: 0x06000EF0 RID: 3824 RVA: 0x0005E854 File Offset: 0x0005CA54
			protected override void AlternativeSolutionEndWithSuccessConsequence()
			{
				this.RelationshipChangeWithIssueOwner = 5;
				GainRenownAction.Apply(Hero.MainHero, 5f, false);
				base.IssueOwner.CurrentSettlement.Village.Bound.Town.Loyalty += 5f;
			}

			// Token: 0x06000EF1 RID: 3825 RVA: 0x0005E8A3 File Offset: 0x0005CAA3
			protected override void AlternativeSolutionEndWithFailureConsequence()
			{
				this.RelationshipChangeWithIssueOwner -= 5;
				base.IssueOwner.CurrentSettlement.Village.Bound.Town.Loyalty -= 5f;
			}

			// Token: 0x06000EF2 RID: 3826 RVA: 0x0005E8DE File Offset: 0x0005CADE
			public override bool DoTroopsSatisfyAlternativeSolution(TroopRoster troopRoster, out TextObject explanation)
			{
				explanation = TextObject.Empty;
				return QuestHelper.CheckRosterForAlternativeSolution(troopRoster, base.GetTotalAlternativeSolutionNeededMenCount(), ref explanation, 0, false);
			}

			// Token: 0x06000EF3 RID: 3827 RVA: 0x0005E8F6 File Offset: 0x0005CAF6
			public override IssueBase.IssueFrequency GetFrequency()
			{
				return IssueBase.IssueFrequency.Common;
			}

			// Token: 0x06000EF4 RID: 3828 RVA: 0x0005E8FC File Offset: 0x0005CAFC
			public override bool IssueStayAliveConditions()
			{
				BoardGameCampaignBehavior campaignBehavior = Campaign.Current.GetCampaignBehavior<BoardGameCampaignBehavior>();
				return campaignBehavior != null && !campaignBehavior.WonBoardGamesInOneWeekInSettlement.Contains(this._targetSettlement) && !base.IssueOwner.CurrentSettlement.IsRaided && !base.IssueOwner.CurrentSettlement.IsUnderRaid;
			}

			// Token: 0x06000EF5 RID: 3829 RVA: 0x0005E951 File Offset: 0x0005CB51
			protected override void CompleteIssueWithTimedOutConsequences()
			{
			}

			// Token: 0x06000EF6 RID: 3830 RVA: 0x0005E953 File Offset: 0x0005CB53
			private void InitializeQuestVariables()
			{
				this._targetSettlement = base.IssueOwner.CurrentSettlement.Village.Bound;
				this._boardGameType = this._targetSettlement.Culture.BoardGame;
			}

			// Token: 0x06000EF7 RID: 3831 RVA: 0x0005E986 File Offset: 0x0005CB86
			protected override void OnGameLoad()
			{
				this.InitializeQuestVariables();
			}

			// Token: 0x06000EF8 RID: 3832 RVA: 0x0005E98E File Offset: 0x0005CB8E
			protected override void HourlyTick()
			{
			}

			// Token: 0x06000EF9 RID: 3833 RVA: 0x0005E990 File Offset: 0x0005CB90
			protected override QuestBase GenerateIssueQuest(string questId)
			{
				return new RuralNotableInnAndOutIssueBehavior.RuralNotableInnAndOutIssueQuest(questId, base.IssueOwner, CampaignTime.DaysFromNow(14f), this.RewardGold);
			}

			// Token: 0x06000EFA RID: 3834 RVA: 0x0005E9B0 File Offset: 0x0005CBB0
			protected override bool CanPlayerTakeQuestConditions(Hero issueGiver, out IssueBase.PreconditionFlags flag, out Hero relationHero, out SkillObject skill)
			{
				skill = null;
				relationHero = null;
				flag = IssueBase.PreconditionFlags.None;
				if (issueGiver.GetRelationWithPlayer() < -10f)
				{
					flag |= IssueBase.PreconditionFlags.Relation;
					relationHero = issueGiver;
				}
				if (FactionManager.IsAtWarAgainstFaction(issueGiver.CurrentSettlement.MapFaction, Hero.MainHero.MapFaction))
				{
					flag |= IssueBase.PreconditionFlags.AtWar;
				}
				if (Hero.MainHero.Gold < 2000)
				{
					flag |= IssueBase.PreconditionFlags.Money;
				}
				return flag == IssueBase.PreconditionFlags.None;
			}

			// Token: 0x06000EFB RID: 3835 RVA: 0x0005EA1C File Offset: 0x0005CC1C
			internal static void AutoGeneratedStaticCollectObjectsRuralNotableInnAndOutIssue(object o, List<object> collectedObjects)
			{
				((RuralNotableInnAndOutIssueBehavior.RuralNotableInnAndOutIssue)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
			}

			// Token: 0x06000EFC RID: 3836 RVA: 0x0005EA2A File Offset: 0x0005CC2A
			protected override void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
			{
				base.AutoGeneratedInstanceCollectObjects(collectedObjects);
			}

			// Token: 0x0400065F RID: 1631
			private const int CompanionSkillLimit = 120;

			// Token: 0x04000660 RID: 1632
			private const int QuestMoneyLimit = 2000;

			// Token: 0x04000661 RID: 1633
			private const int AlternativeSolutionGoldCost = 1000;

			// Token: 0x04000662 RID: 1634
			private CultureObject.BoardGameType _boardGameType;

			// Token: 0x04000663 RID: 1635
			private Settlement _targetSettlement;
		}

		// Token: 0x0200016D RID: 365
		public class RuralNotableInnAndOutIssueQuest : QuestBase
		{
			// Token: 0x1700019D RID: 413
			// (get) Token: 0x06000EFD RID: 3837 RVA: 0x0005EA34 File Offset: 0x0005CC34
			private TextObject _questStartLog
			{
				get
				{
					TextObject textObject = new TextObject("{=tirG1BB2}{QUEST_GIVER.LINK} told you that he lost a land deed while playing games in a tavern in {TARGET_SETTLEMENT}. He wants you to go find the game host and win it back for him. You told him that you will take care of the situation yourself.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					textObject.SetTextVariable("TARGET_SETTLEMENT", this._targetSettlement.EncyclopediaLinkWithName);
					return textObject;
				}
			}

			// Token: 0x1700019E RID: 414
			// (get) Token: 0x06000EFE RID: 3838 RVA: 0x0005EA80 File Offset: 0x0005CC80
			private TextObject _successLog
			{
				get
				{
					TextObject textObject = new TextObject("{=bvhWLb4C}You defeated the Game Host and got the deed back. {QUEST_GIVER.LINK}.{newline}\"Thank you for resolving this issue so neatly. Please accept these {GOLD}{GOLD_ICON} denars with our gratitude.\"", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					textObject.SetTextVariable("GOLD", this.RewardGold);
					textObject.SetTextVariable("GOLD_ICON", "{=!}<img src=\"General\\Icons\\Coin@2x\" extend=\"8\">");
					return textObject;
				}
			}

			// Token: 0x1700019F RID: 415
			// (get) Token: 0x06000EFF RID: 3839 RVA: 0x0005EAD8 File Offset: 0x0005CCD8
			private TextObject _successWithPayingLog
			{
				get
				{
					TextObject textObject = new TextObject("{=TIPxWsYW}You have bought the deed from the game host. {QUEST_GIVER.LINK}.{newline}\"I am happy that I got my land back. I'm not so happy that everyone knows I had to pay for it, but... Anyway, please accept these {GOLD}{GOLD_ICON} denars with my gratitude.\"", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					textObject.SetTextVariable("GOLD", 800);
					textObject.SetTextVariable("GOLD_ICON", "{=!}<img src=\"General\\Icons\\Coin@2x\" extend=\"8\">");
					return textObject;
				}
			}

			// Token: 0x170001A0 RID: 416
			// (get) Token: 0x06000F00 RID: 3840 RVA: 0x0005EB2C File Offset: 0x0005CD2C
			private TextObject _lostLog
			{
				get
				{
					TextObject textObject = new TextObject("{=ye4oqBFB}You lost the board game and failed to help {QUEST_GIVER.LINK}. \"Thank you for trying, {PLAYER.NAME}, but I guess I chose the wrong person for the job.\"", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					StringHelpers.SetCharacterProperties("PLAYER", CharacterObject.PlayerCharacter, textObject, false);
					return textObject;
				}
			}

			// Token: 0x170001A1 RID: 417
			// (get) Token: 0x06000F01 RID: 3841 RVA: 0x0005EB70 File Offset: 0x0005CD70
			private TextObject _questCanceledTargetVillageRaided
			{
				get
				{
					TextObject textObject = new TextObject("{=YGVTXNrf}{SETTLEMENT} was raided, Your agreement with {QUEST_GIVER.LINK} is canceled.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					textObject.SetTextVariable("SETTLEMENT", this._targetSettlement.EncyclopediaLinkWithName);
					return textObject;
				}
			}

			// Token: 0x170001A2 RID: 418
			// (get) Token: 0x06000F02 RID: 3842 RVA: 0x0005EBB9 File Offset: 0x0005CDB9
			private TextObject _questCanceledWarDeclared
			{
				get
				{
					TextObject textObject = new TextObject("{=cKz1cyuM}Your clan is now at war with {QUEST_GIVER_SETTLEMENT_FACTION}. Quest is canceled.", null);
					textObject.SetTextVariable("QUEST_GIVER_SETTLEMENT_FACTION", base.QuestGiver.CurrentSettlement.MapFaction.Name);
					return textObject;
				}
			}

			// Token: 0x170001A3 RID: 419
			// (get) Token: 0x06000F03 RID: 3843 RVA: 0x0005EBE8 File Offset: 0x0005CDE8
			private TextObject _playerDeclaredWarQuestLogText
			{
				get
				{
					TextObject textObject = new TextObject("{=bqeWVVEE}Your actions have started a war with {QUEST_GIVER.LINK}'s faction. {?QUEST_GIVER.GENDER}She{?}He{\\?} cancels your agreement and the quest is a failure.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x170001A4 RID: 420
			// (get) Token: 0x06000F04 RID: 3844 RVA: 0x0005EC1C File Offset: 0x0005CE1C
			private TextObject _questCanceledSettlementIsUnderSiege
			{
				get
				{
					TextObject textObject = new TextObject("{=b5LdBYpF}{SETTLEMENT} is under siege. Your agreement with {QUEST_GIVER.LINK} is canceled.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					textObject.SetTextVariable("SETTLEMENT", this._targetSettlement.EncyclopediaLinkWithName);
					return textObject;
				}
			}

			// Token: 0x170001A5 RID: 421
			// (get) Token: 0x06000F05 RID: 3845 RVA: 0x0005EC68 File Offset: 0x0005CE68
			private TextObject _timeoutLog
			{
				get
				{
					TextObject textObject = new TextObject("{=XLy8anVr}You received a message from {QUEST_GIVER.LINK}. \"This may not have seemed like an important task, but I placed my trust in you. I guess I was wrong to do so.\"", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x170001A6 RID: 422
			// (get) Token: 0x06000F06 RID: 3846 RVA: 0x0005EC9A File Offset: 0x0005CE9A
			public override TextObject Title
			{
				get
				{
					return new TextObject("{=uUhtKnfA}Inn and Out", null);
				}
			}

			// Token: 0x170001A7 RID: 423
			// (get) Token: 0x06000F07 RID: 3847 RVA: 0x0005ECA7 File Offset: 0x0005CEA7
			public override bool IsRemainingTimeHidden
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000F08 RID: 3848 RVA: 0x0005ECAA File Offset: 0x0005CEAA
			public RuralNotableInnAndOutIssueQuest(string questId, Hero giverHero, CampaignTime duration, int rewardGold) : base(questId, giverHero, duration, rewardGold)
			{
				this.InitializeQuestVariables();
				this.SetDialogs();
				base.InitializeQuestOnCreation();
			}

			// Token: 0x06000F09 RID: 3849 RVA: 0x0005ECC9 File Offset: 0x0005CEC9
			private void InitializeQuestVariables()
			{
				this._targetSettlement = base.QuestGiver.CurrentSettlement.Village.Bound;
				this._boardGameType = this._targetSettlement.Culture.BoardGame;
			}

			// Token: 0x06000F0A RID: 3850 RVA: 0x0005ECFC File Offset: 0x0005CEFC
			private void QuestAcceptedConsequences()
			{
				base.StartQuest();
				base.AddLog(this._questStartLog, false);
				base.AddTrackedObject(this._targetSettlement);
			}

			// Token: 0x06000F0B RID: 3851 RVA: 0x0005ED1E File Offset: 0x0005CF1E
			protected override void InitializeQuestOnGameLoad()
			{
				this.InitializeQuestVariables();
				this.SetDialogs();
				if (Campaign.Current.GetCampaignBehavior<BoardGameCampaignBehavior>() == null)
				{
					base.CompleteQuestWithCancel(null);
				}
			}

			// Token: 0x06000F0C RID: 3852 RVA: 0x0005ED3F File Offset: 0x0005CF3F
			protected override void HourlyTick()
			{
			}

			// Token: 0x06000F0D RID: 3853 RVA: 0x0005ED44 File Offset: 0x0005CF44
			protected override void RegisterEvents()
			{
				CampaignEvents.OnPlayerBoardGameOverEvent.AddNonSerializedListener(this, new Action<Hero, BoardGameHelper.BoardGameState>(this.OnBoardGameEnd));
				CampaignEvents.WarDeclared.AddNonSerializedListener(this, new Action<IFaction, IFaction, DeclareWarAction.DeclareWarDetail>(this.OnWarDeclared));
				CampaignEvents.OnClanChangedKingdomEvent.AddNonSerializedListener(this, new Action<Clan, Kingdom, Kingdom, ChangeKingdomAction.ChangeKingdomActionDetail, bool>(this.OnClanChangedKingdom));
				CampaignEvents.OnSiegeEventStartedEvent.AddNonSerializedListener(this, new Action<SiegeEvent>(this.OnSiegeStarted));
				CampaignEvents.MapEventStarted.AddNonSerializedListener(this, new Action<MapEvent, PartyBase, PartyBase>(this.OnMapEventStarted));
				CampaignEvents.VillageBeingRaided.AddNonSerializedListener(this, new Action<Village>(this.OnVillageBeingRaided));
				CampaignEvents.LocationCharactersSimulatedEvent.AddNonSerializedListener(this, new Action(this.OnLocationCharactersSimulated));
			}

			// Token: 0x06000F0E RID: 3854 RVA: 0x0005EDF4 File Offset: 0x0005CFF4
			private void OnLocationCharactersSimulated()
			{
				if (Settlement.CurrentSettlement != null && Settlement.CurrentSettlement == this._targetSettlement && Campaign.Current.GameMenuManager.MenuLocations.First<Location>().StringId == "tavern")
				{
					foreach (Agent agent in Mission.Current.Agents)
					{
						LocationCharacter locationCharacter = LocationComplex.Current.GetLocationWithId("tavern").GetLocationCharacter(agent.Origin);
						if (locationCharacter != null && locationCharacter.Character.Occupation == Occupation.TavernGameHost)
						{
							locationCharacter.IsVisualTracked = true;
						}
					}
				}
			}

			// Token: 0x06000F0F RID: 3855 RVA: 0x0005EEB8 File Offset: 0x0005D0B8
			private void OnMapEventStarted(MapEvent mapEvent, PartyBase attackerParty, PartyBase defenderParty)
			{
				if (QuestHelper.CheckMinorMajorCoercion(this, mapEvent, attackerParty))
				{
					QuestHelper.ApplyGenericMinorMajorCoercionConsequences(this, mapEvent);
				}
			}

			// Token: 0x06000F10 RID: 3856 RVA: 0x0005EECB File Offset: 0x0005D0CB
			private void OnVillageBeingRaided(Village village)
			{
				if (village == base.QuestGiver.CurrentSettlement.Village)
				{
					base.CompleteQuestWithCancel(this._questCanceledTargetVillageRaided);
				}
			}

			// Token: 0x06000F11 RID: 3857 RVA: 0x0005EEEC File Offset: 0x0005D0EC
			private void OnBoardGameEnd(Hero opposingHero, BoardGameHelper.BoardGameState state)
			{
				if (this._checkForBoardGameEnd)
				{
					this._playerWonTheGame = (state == BoardGameHelper.BoardGameState.Win);
				}
			}

			// Token: 0x06000F12 RID: 3858 RVA: 0x0005EF00 File Offset: 0x0005D100
			private void OnSiegeStarted(SiegeEvent siegeEvent)
			{
				if (siegeEvent.BesiegedSettlement == this._targetSettlement)
				{
					base.CompleteQuestWithCancel(this._questCanceledSettlementIsUnderSiege);
				}
			}

			// Token: 0x06000F13 RID: 3859 RVA: 0x0005EF1C File Offset: 0x0005D11C
			protected override void SetDialogs()
			{
				TextObject textObject = new TextObject("{=I6amLvVE}Good, good. That's the best way to do these things. [if:convo_normal]Go to {TARGET_SETTLEMENT}, find this game host and wipe the smirk off of his face.", null);
				textObject.SetTextVariable("TARGET_SETTLEMENT", this._targetSettlement.Name);
				this.OfferDialogFlow = DialogFlow.CreateDialogFlow("issue_classic_quest_start", 100).NpcLine(textObject, null, null).Condition(() => Hero.OneToOneConversationHero == base.QuestGiver).Consequence(new ConversationSentence.OnConsequenceDelegate(this.QuestAcceptedConsequences)).CloseDialog();
				this.DiscussDialogFlow = DialogFlow.CreateDialogFlow("quest_discuss", 100).NpcLine(new TextObject("{=HGRWs0zE}Have you met the man who took my deed? Did you get it back?[if:convo_astonished]", null), null, null).Condition(() => Hero.OneToOneConversationHero == base.QuestGiver).BeginPlayerOptions().PlayerOption(new TextObject("{=uJPAYUU7}I will be on my way soon enough.", null), null).NpcLine(new TextObject("{=MOmePlJQ}Could you hurry this along? I don't want him to find another buyer.[if:convo_pondering] Thank you.", null), null, null).CloseDialog().PlayerOption(new TextObject("{=azVhRGik}I am waiting for the right moment.", null), null).NpcLine(new TextObject("{=bRMLn0jj}Well, if he wanders off to another town, or gets his throat slit,[if:convo_pondering] or loses the deed, that would be the wrong moment, now wouldn't it?", null), null, null).CloseDialog().EndPlayerOptions();
				Campaign.Current.ConversationManager.AddDialogFlow(this.GetGameHostDialogFlow(), this);
				Campaign.Current.ConversationManager.AddDialogFlow(this.GetGameHostDialogueAfterFirstGame(), this);
			}

			// Token: 0x06000F14 RID: 3860 RVA: 0x0005F048 File Offset: 0x0005D248
			private DialogFlow GetGameHostDialogFlow()
			{
				return DialogFlow.CreateDialogFlow("start", 125).NpcLine("{=dzWioKRa}Hello there, are you looking for a friendly match? A wager perhaps?[if:convo_mocking_aristocratic]", null, null).Condition(() => this.TavernHostDialogCondition(true)).PlayerLine(new TextObject("{=eOle8pYT}You won a deed of land from my associate. I'm here to win it back.", null), null).NpcLine("{=bEipgE5E}Ah, yes, these are the most interesting kinds of games, aren't they? [if:convo_excited]I won't deny myself the pleasure but clearly that deed is worth more to him than just the value of the land. I'll wager the deed, but you need to put up 1000 denars.", null, null).BeginPlayerOptions().PlayerOption("{=XvkSbY6N}I see your wager. Let's play.", null).Condition(() => Hero.MainHero.Gold >= 1000).Consequence(new ConversationSentence.OnConsequenceDelegate(this.StartBoardGame)).CloseDialog().PlayerOption("{=89b5ao7P}As of now, I do not have 1000 denars to afford on gambling. I may get back to you once I get the required amount.", null).Condition(() => Hero.MainHero.Gold < 1000).NpcLine(new TextObject("{=ppi6eVos}As you wish.", null), null, null).CloseDialog().PlayerOption("{=WrnvRayQ}Let's just save ourselves some trouble, and I'll just pay you that amount.", null).ClickableCondition(new ConversationSentence.OnClickableConditionDelegate(this.CheckPlayerHasEnoughDenarsClickableCondition)).NpcLine("{=pa3RY39w}Sure. I'm happy to turn paper into silver... 1000 denars it is.[if:convo_evil_smile]", null, null).Consequence(new ConversationSentence.OnConsequenceDelegate(this.PlayerPaid1000QuestSuccess)).CloseDialog().PlayerOption("{=BSeplVwe}That's too much. I will be back later.", null).CloseDialog().EndPlayerOptions().CloseDialog();
			}

			// Token: 0x06000F15 RID: 3861 RVA: 0x0005F184 File Offset: 0x0005D384
			private DialogFlow GetGameHostDialogueAfterFirstGame()
			{
				return DialogFlow.CreateDialogFlow("start", 125).BeginNpcOptions().NpcOption(new TextObject("{=dyhZUHao}Well, I thought you were here to be sheared, [if:convo_shocked]but it looks like the sheep bites back. Very well, nicely played, here's your man's land back.", null), () => this._playerWonTheGame && this.TavernHostDialogCondition(false), null, null).Consequence(new ConversationSentence.OnConsequenceDelegate(this.PlayerWonTheBoardGame)).CloseDialog().NpcOption("{=TdnD29Ax}Ah! You almost had me! Maybe you just weren't paying attention. [if:convo_mocking_teasing]Care to put another 1000 denars on the table and have another go?", () => !this._playerWonTheGame && this._tryCount < 2 && this.TavernHostDialogCondition(false), null, null).BeginPlayerOptions().PlayerOption("{=fiMZ696A}Yes, I'll play again.", null).ClickableCondition(new ConversationSentence.OnClickableConditionDelegate(this.CheckPlayerHasEnoughDenarsClickableCondition)).Consequence(new ConversationSentence.OnConsequenceDelegate(this.StartBoardGame)).CloseDialog().PlayerOption("{=zlFSIvD5}No, no. I know a trap when I see one. You win. Good-bye.", null).NpcLine(new TextObject("{=ppi6eVos}As you wish.", null), null, null).Consequence(new ConversationSentence.OnConsequenceDelegate(this.PlayerFailAfterBoardGame)).CloseDialog().EndPlayerOptions().NpcOption("{=hkNrC5d3}That was fun, but I've learned not to inflict too great a humiliation on those who carry a sword.[if:convo_merry] I'll take my winnings and enjoy them now. Good-bye to you!", () => this._tryCount >= 2 && this.TavernHostDialogCondition(false), null, null).Consequence(new ConversationSentence.OnConsequenceDelegate(this.PlayerFailAfterBoardGame)).CloseDialog().EndNpcOptions();
			}

			// Token: 0x06000F16 RID: 3862 RVA: 0x0005F290 File Offset: 0x0005D490
			private bool CheckPlayerHasEnoughDenarsClickableCondition(out TextObject explanation)
			{
				if (Hero.MainHero.Gold >= 1000)
				{
					explanation = TextObject.Empty;
					return true;
				}
				explanation = new TextObject("{=AMlaYbJv}You don't have 1000 denars.", null);
				return false;
			}

			// Token: 0x06000F17 RID: 3863 RVA: 0x0005F2BC File Offset: 0x0005D4BC
			private bool TavernHostDialogCondition(bool isInitialDialogue = false)
			{
				if ((!this._checkForBoardGameEnd || !isInitialDialogue) && Settlement.CurrentSettlement == this._targetSettlement && CharacterObject.OneToOneConversationCharacter.Occupation == Occupation.TavernGameHost)
				{
					LocationComplex locationComplex = LocationComplex.Current;
					if (((locationComplex != null) ? locationComplex.GetLocationWithId("tavern") : null) != null)
					{
						Mission.Current.GetMissionBehavior<MissionBoardGameLogic>().DetectOpposingAgent();
						return Mission.Current.GetMissionBehavior<MissionBoardGameLogic>().CheckIfBothSidesAreSitting();
					}
				}
				return false;
			}

			// Token: 0x06000F18 RID: 3864 RVA: 0x0005F327 File Offset: 0x0005D527
			private void PlayerPaid1000QuestSuccess()
			{
				base.AddLog(this._successWithPayingLog, false);
				this._applyLesserReward = true;
				GiveGoldAction.ApplyBetweenCharacters(Hero.MainHero, null, 1000, false);
				base.CompleteQuestWithSuccess();
			}

			// Token: 0x06000F19 RID: 3865 RVA: 0x0005F358 File Offset: 0x0005D558
			protected override void OnFinalize()
			{
				if (Mission.Current != null)
				{
					foreach (Agent agent in Mission.Current.Agents)
					{
						Location locationWithId = LocationComplex.Current.GetLocationWithId("tavern");
						if (locationWithId != null)
						{
							LocationCharacter locationCharacter = locationWithId.GetLocationCharacter(agent.Origin);
							if (locationCharacter != null && locationCharacter.Character.Occupation == Occupation.TavernGameHost)
							{
								locationCharacter.IsVisualTracked = false;
							}
						}
					}
				}
			}

			// Token: 0x06000F1A RID: 3866 RVA: 0x0005F3E8 File Offset: 0x0005D5E8
			private void ApplySuccessRewards()
			{
				GiveGoldAction.ApplyBetweenCharacters(null, Hero.MainHero, this._applyLesserReward ? 800 : this.RewardGold, false);
				ChangeRelationAction.ApplyPlayerRelation(base.QuestGiver, 5, true, true);
				GainRenownAction.Apply(Hero.MainHero, 1f, false);
				base.QuestGiver.CurrentSettlement.Village.Bound.Town.Loyalty += 5f;
			}

			// Token: 0x06000F1B RID: 3867 RVA: 0x0005F45F File Offset: 0x0005D65F
			protected override void OnCompleteWithSuccess()
			{
				this.ApplySuccessRewards();
			}

			// Token: 0x06000F1C RID: 3868 RVA: 0x0005F468 File Offset: 0x0005D668
			private void StartBoardGame()
			{
				MissionBoardGameLogic missionBehavior = Mission.Current.GetMissionBehavior<MissionBoardGameLogic>();
				Campaign.Current.GetCampaignBehavior<BoardGameCampaignBehavior>().SetBetAmount(1000);
				missionBehavior.DetectOpposingAgent();
				missionBehavior.SetCurrentDifficulty(BoardGameHelper.AIDifficulty.Normal);
				missionBehavior.SetBoardGame(this._boardGameType);
				missionBehavior.StartBoardGame();
				this._checkForBoardGameEnd = true;
				this._tryCount++;
			}

			// Token: 0x06000F1D RID: 3869 RVA: 0x0005F4C6 File Offset: 0x0005D6C6
			private void PlayerWonTheBoardGame()
			{
				base.AddLog(this._successLog, false);
				base.CompleteQuestWithSuccess();
			}

			// Token: 0x06000F1E RID: 3870 RVA: 0x0005F4DC File Offset: 0x0005D6DC
			private void PlayerFailAfterBoardGame()
			{
				base.AddLog(this._lostLog, false);
				this.RelationshipChangeWithQuestGiver = -5;
				base.QuestGiver.CurrentSettlement.Village.Bound.Town.Loyalty -= 5f;
				base.CompleteQuestWithFail(null);
			}

			// Token: 0x06000F1F RID: 3871 RVA: 0x0005F531 File Offset: 0x0005D731
			private void OnClanChangedKingdom(Clan clan, Kingdom oldKingdom, Kingdom newKingdom, ChangeKingdomAction.ChangeKingdomActionDetail detail, bool showNotification = true)
			{
				if (base.QuestGiver.CurrentSettlement.MapFaction.IsAtWarWith(Hero.MainHero.MapFaction))
				{
					base.CompleteQuestWithCancel(this._questCanceledWarDeclared);
				}
			}

			// Token: 0x06000F20 RID: 3872 RVA: 0x0005F560 File Offset: 0x0005D760
			private void OnWarDeclared(IFaction faction1, IFaction faction2, DeclareWarAction.DeclareWarDetail detail)
			{
				QuestHelper.CheckWarDeclarationAndFailOrCancelTheQuest(this, faction1, faction2, detail, this._playerDeclaredWarQuestLogText, this._questCanceledWarDeclared, false);
			}

			// Token: 0x06000F21 RID: 3873 RVA: 0x0005F578 File Offset: 0x0005D778
			public override GameMenuOption.IssueQuestFlags IsLocationTrackedByQuest(Location location)
			{
				if (PlayerEncounter.LocationEncounter.Settlement == this._targetSettlement && location.StringId == "tavern")
				{
					return GameMenuOption.IssueQuestFlags.ActiveIssue;
				}
				return GameMenuOption.IssueQuestFlags.None;
			}

			// Token: 0x06000F22 RID: 3874 RVA: 0x0005F5A4 File Offset: 0x0005D7A4
			protected override void OnTimedOut()
			{
				this.RelationshipChangeWithQuestGiver = -5;
				base.QuestGiver.CurrentSettlement.Village.Bound.Town.Loyalty -= 5f;
				base.AddLog(this._timeoutLog, false);
			}

			// Token: 0x06000F23 RID: 3875 RVA: 0x0005F5F2 File Offset: 0x0005D7F2
			internal static void AutoGeneratedStaticCollectObjectsRuralNotableInnAndOutIssueQuest(object o, List<object> collectedObjects)
			{
				((RuralNotableInnAndOutIssueBehavior.RuralNotableInnAndOutIssueQuest)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
			}

			// Token: 0x06000F24 RID: 3876 RVA: 0x0005F600 File Offset: 0x0005D800
			protected override void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
			{
				base.AutoGeneratedInstanceCollectObjects(collectedObjects);
			}

			// Token: 0x06000F25 RID: 3877 RVA: 0x0005F609 File Offset: 0x0005D809
			internal static object AutoGeneratedGetMemberValue_tryCount(object o)
			{
				return ((RuralNotableInnAndOutIssueBehavior.RuralNotableInnAndOutIssueQuest)o)._tryCount;
			}

			// Token: 0x04000664 RID: 1636
			public const int LesserReward = 800;

			// Token: 0x04000665 RID: 1637
			private CultureObject.BoardGameType _boardGameType;

			// Token: 0x04000666 RID: 1638
			private Settlement _targetSettlement;

			// Token: 0x04000667 RID: 1639
			private bool _checkForBoardGameEnd;

			// Token: 0x04000668 RID: 1640
			private bool _playerWonTheGame;

			// Token: 0x04000669 RID: 1641
			private bool _applyLesserReward;

			// Token: 0x0400066A RID: 1642
			[SaveableField(1)]
			private int _tryCount;
		}
	}
}
