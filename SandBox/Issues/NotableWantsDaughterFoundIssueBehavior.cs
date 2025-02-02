using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using SandBox.Conversation.MissionLogics;
using SandBox.Missions.AgentBehaviors;
using SandBox.Missions.MissionLogics;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.AgentOrigins;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.CampaignSystem.Conversation.Persuasion;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.CampaignSystem.Issues;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.ObjectSystem;
using TaleWorlds.SaveSystem;

namespace SandBox.Issues
{
	// Token: 0x02000089 RID: 137
	public class NotableWantsDaughterFoundIssueBehavior : CampaignBehaviorBase
	{
		// Token: 0x06000552 RID: 1362 RVA: 0x00024007 File Offset: 0x00022207
		public override void RegisterEvents()
		{
			CampaignEvents.OnCheckForIssueEvent.AddNonSerializedListener(this, new Action<Hero>(this.OnCheckForIssue));
			CampaignEvents.OnGameLoadFinishedEvent.AddNonSerializedListener(this, new Action(this.OnGameLoadFinished));
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00024038 File Offset: 0x00022238
		private void OnGameLoadFinished()
		{
			if (MBSaveLoad.IsUpdatingGameVersion && MBSaveLoad.LastLoadedGameVersion.IsOlderThan(ApplicationVersion.FromString("v1.2.8.31599", 45697)))
			{
				foreach (Hero hero in Hero.DeadOrDisabledHeroes)
				{
					if (hero.IsDead && hero.CompanionOf == Clan.PlayerClan && hero.Father != null && hero.Father.IsNotable && hero.Father.CurrentSettlement.IsVillage)
					{
						RemoveCompanionAction.ApplyByDeath(Clan.PlayerClan, hero);
					}
				}
			}
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x000240F4 File Offset: 0x000222F4
		public void OnCheckForIssue(Hero hero)
		{
			if (this.ConditionsHold(hero))
			{
				Campaign.Current.IssueManager.AddPotentialIssueData(hero, new PotentialIssueData(new PotentialIssueData.StartIssueDelegate(this.OnStartIssue), typeof(NotableWantsDaughterFoundIssueBehavior.NotableWantsDaughterFoundIssue), IssueBase.IssueFrequency.Rare, null));
				return;
			}
			Campaign.Current.IssueManager.AddPotentialIssueData(hero, new PotentialIssueData(typeof(NotableWantsDaughterFoundIssueBehavior.NotableWantsDaughterFoundIssue), IssueBase.IssueFrequency.Rare));
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00024158 File Offset: 0x00022358
		private bool ConditionsHold(Hero issueGiver)
		{
			if (issueGiver.IsRuralNotable && issueGiver.CurrentSettlement.IsVillage && issueGiver.CurrentSettlement.Village.Bound != null && issueGiver.CurrentSettlement.Village.Bound.BoundVillages.Count > 2 && issueGiver.CanHaveQuestsOrIssues() && issueGiver.Age > (float)(Campaign.Current.Models.AgeModel.HeroComesOfAge * 2))
			{
				if (issueGiver.CurrentSettlement.Culture.NotableAndWandererTemplates.Any((CharacterObject x) => x.Occupation == Occupation.Wanderer && x.IsFemale))
				{
					if (issueGiver.CurrentSettlement.Culture.NotableAndWandererTemplates.Any((CharacterObject x) => x.Occupation == Occupation.GangLeader && !x.IsFemale) && issueGiver.GetTraitLevel(DefaultTraits.Mercy) <= 0)
					{
						return issueGiver.GetTraitLevel(DefaultTraits.Generosity) <= 0;
					}
				}
			}
			return false;
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0002426F File Offset: 0x0002246F
		private IssueBase OnStartIssue(in PotentialIssueData pid, Hero issueOwner)
		{
			return new NotableWantsDaughterFoundIssueBehavior.NotableWantsDaughterFoundIssue(issueOwner);
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00024277 File Offset: 0x00022477
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x0400028A RID: 650
		private const IssueBase.IssueFrequency NotableWantsDaughterFoundIssueFrequency = IssueBase.IssueFrequency.Rare;

		// Token: 0x0400028B RID: 651
		private const int IssueDuration = 30;

		// Token: 0x0400028C RID: 652
		private const int QuestTimeLimit = 19;

		// Token: 0x0400028D RID: 653
		private const int BaseRewardGold = 500;

		// Token: 0x0200015F RID: 351
		public class NotableWantsDaughterFoundIssueTypeDefiner : SaveableTypeDefiner
		{
			// Token: 0x06000D3A RID: 3386 RVA: 0x00056F02 File Offset: 0x00055102
			public NotableWantsDaughterFoundIssueTypeDefiner() : base(1088000)
			{
			}

			// Token: 0x06000D3B RID: 3387 RVA: 0x00056F0F File Offset: 0x0005510F
			protected override void DefineClassTypes()
			{
				base.AddClassDefinition(typeof(NotableWantsDaughterFoundIssueBehavior.NotableWantsDaughterFoundIssue), 1, null);
				base.AddClassDefinition(typeof(NotableWantsDaughterFoundIssueBehavior.NotableWantsDaughterFoundIssueQuest), 2, null);
			}
		}

		// Token: 0x02000160 RID: 352
		public class NotableWantsDaughterFoundIssue : IssueBase
		{
			// Token: 0x17000126 RID: 294
			// (get) Token: 0x06000D3C RID: 3388 RVA: 0x00056F35 File Offset: 0x00055135
			public override IssueBase.AlternativeSolutionScaleFlag AlternativeSolutionScaleFlags
			{
				get
				{
					return IssueBase.AlternativeSolutionScaleFlag.FailureRisk;
				}
			}

			// Token: 0x17000127 RID: 295
			// (get) Token: 0x06000D3D RID: 3389 RVA: 0x00056F38 File Offset: 0x00055138
			public override bool IsThereAlternativeSolution
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000128 RID: 296
			// (get) Token: 0x06000D3E RID: 3390 RVA: 0x00056F3B File Offset: 0x0005513B
			public override bool IsThereLordSolution
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000129 RID: 297
			// (get) Token: 0x06000D3F RID: 3391 RVA: 0x00056F3E File Offset: 0x0005513E
			protected override int RewardGold
			{
				get
				{
					return 500 + MathF.Round(1200f * base.IssueDifficultyMultiplier);
				}
			}

			// Token: 0x1700012A RID: 298
			// (get) Token: 0x06000D40 RID: 3392 RVA: 0x00056F57 File Offset: 0x00055157
			public override int AlternativeSolutionBaseNeededMenCount
			{
				get
				{
					return 2 + MathF.Ceiling(4f * base.IssueDifficultyMultiplier);
				}
			}

			// Token: 0x1700012B RID: 299
			// (get) Token: 0x06000D41 RID: 3393 RVA: 0x00056F6C File Offset: 0x0005516C
			protected override int AlternativeSolutionBaseDurationInDaysInternal
			{
				get
				{
					return 4 + MathF.Ceiling(5f * base.IssueDifficultyMultiplier);
				}
			}

			// Token: 0x1700012C RID: 300
			// (get) Token: 0x06000D42 RID: 3394 RVA: 0x00056F81 File Offset: 0x00055181
			protected override int CompanionSkillRewardXP
			{
				get
				{
					return (int)(500f + 1000f * base.IssueDifficultyMultiplier);
				}
			}

			// Token: 0x06000D43 RID: 3395 RVA: 0x00056F96 File Offset: 0x00055196
			public NotableWantsDaughterFoundIssue(Hero issueOwner) : base(issueOwner, CampaignTime.DaysFromNow(30f))
			{
			}

			// Token: 0x06000D44 RID: 3396 RVA: 0x00056FA9 File Offset: 0x000551A9
			protected override float GetIssueEffectAmountInternal(IssueEffect issueEffect)
			{
				if (issueEffect == DefaultIssueEffects.IssueOwnerPower)
				{
					return -0.1f;
				}
				return 0f;
			}

			// Token: 0x1700012D RID: 301
			// (get) Token: 0x06000D45 RID: 3397 RVA: 0x00056FC0 File Offset: 0x000551C0
			public override TextObject IssueBriefByIssueGiver
			{
				get
				{
					TextObject textObject = new TextObject("{=x9VgLEzi}Yes... I've suffered a great misfortune. [ib:demure][if:convo_shocked]My daughter, a headstrong girl, has been bewitched by this never-do-well. I told her to stop seeing him but she wouldn't listen! Now she's missing - I'm sure she's been abducted by him! I'm offering a bounty of {BASE_REWARD_GOLD}{GOLD_ICON} to anyone who brings her back. Please {?PLAYER.GENDER}ma'am{?}sir{\\?}! Don't let a father's heart be broken.", null);
					StringHelpers.SetCharacterProperties("PLAYER", CharacterObject.PlayerCharacter, textObject, false);
					textObject.SetTextVariable("GOLD_ICON", "{=!}<img src=\"General\\Icons\\Coin@2x\" extend=\"8\">");
					textObject.SetTextVariable("BASE_REWARD_GOLD", this.RewardGold);
					return textObject;
				}
			}

			// Token: 0x1700012E RID: 302
			// (get) Token: 0x06000D46 RID: 3398 RVA: 0x0005700F File Offset: 0x0005520F
			public override TextObject IssueAcceptByPlayer
			{
				get
				{
					return new TextObject("{=35w6g8gM}Tell me more. What's wrong with the man? ", null);
				}
			}

			// Token: 0x1700012F RID: 303
			// (get) Token: 0x06000D47 RID: 3399 RVA: 0x0005701C File Offset: 0x0005521C
			public override TextObject IssueQuestSolutionExplanationByIssueGiver
			{
				get
				{
					return new TextObject("{=IY5b9vZV}Everything is wrong. [if:convo_annoyed]He is from a low family, the kind who is always involved in some land fraud scheme, or seen dealing with known bandits. Every village has a black sheep like that but I never imagined he would get his hooks into my daughter!", null);
				}
			}

			// Token: 0x17000130 RID: 304
			// (get) Token: 0x06000D48 RID: 3400 RVA: 0x00057029 File Offset: 0x00055229
			public override TextObject IssueAlternativeSolutionExplanationByIssueGiver
			{
				get
				{
					TextObject textObject = new TextObject("{=v0XsM7Zz}If you send your best tracker with a few men, I am sure they will find my girl [if:convo_pondering]and be back to you in no more than {ALTERNATIVE_SOLUTION_WAIT_DAYS} days.", null);
					textObject.SetTextVariable("ALTERNATIVE_SOLUTION_WAIT_DAYS", base.GetTotalAlternativeSolutionDurationInDays());
					return textObject;
				}
			}

			// Token: 0x17000131 RID: 305
			// (get) Token: 0x06000D49 RID: 3401 RVA: 0x00057048 File Offset: 0x00055248
			public override TextObject IssuePlayerResponseAfterAlternativeExplanation
			{
				get
				{
					return new TextObject("{=Ldp6ckgj}Don't worry, either I or one of my companions should be able to find her and see what's going on.", null);
				}
			}

			// Token: 0x17000132 RID: 306
			// (get) Token: 0x06000D4A RID: 3402 RVA: 0x00057055 File Offset: 0x00055255
			public override TextObject IssueQuestSolutionAcceptByPlayer
			{
				get
				{
					return new TextObject("{=uYrxCtDa}I should be able to find her and see what's going on.", null);
				}
			}

			// Token: 0x17000133 RID: 307
			// (get) Token: 0x06000D4B RID: 3403 RVA: 0x00057062 File Offset: 0x00055262
			public override TextObject IssueAlternativeSolutionAcceptByPlayer
			{
				get
				{
					TextObject textObject = new TextObject("{=WSrGHkal}I will have one of my trackers and {REQUIRED_TROOP_AMOUNT} of my men to find your daughter.", null);
					textObject.SetTextVariable("REQUIRED_TROOP_AMOUNT", base.GetTotalAlternativeSolutionNeededMenCount());
					return textObject;
				}
			}

			// Token: 0x17000134 RID: 308
			// (get) Token: 0x06000D4C RID: 3404 RVA: 0x00057081 File Offset: 0x00055281
			public override TextObject IssueDiscussAlternativeSolution
			{
				get
				{
					return new TextObject("{=mBPcZddA}{?PLAYER.GENDER}Madam{?}Sir{\\?}, we are still waiting [ib:demure][if:convo_undecided_open]for your men to bring my daughter back. I pray for their success.", null);
				}
			}

			// Token: 0x17000135 RID: 309
			// (get) Token: 0x06000D4D RID: 3405 RVA: 0x00057090 File Offset: 0x00055290
			public override TextObject IssueAlternativeSolutionResponseByIssueGiver
			{
				get
				{
					TextObject textObject = new TextObject("{=Hhd3KaKu}Thank you, my {?PLAYER.GENDER}lady{?}lord{\\?}. If your men can find my girl and bring her back to me, I will be so grateful.[if:convo_happy] I will pay you {BASE_REWARD_GOLD}{GOLD_ICON} for your trouble.", null);
					textObject.SetTextVariable("GOLD_ICON", "{=!}<img src=\"General\\Icons\\Coin@2x\" extend=\"8\">");
					textObject.SetTextVariable("BASE_REWARD_GOLD", this.RewardGold);
					StringHelpers.SetCharacterProperties("PLAYER", CharacterObject.PlayerCharacter, textObject, false);
					return textObject;
				}
			}

			// Token: 0x17000136 RID: 310
			// (get) Token: 0x06000D4E RID: 3406 RVA: 0x000570E0 File Offset: 0x000552E0
			protected override TextObject AlternativeSolutionStartLog
			{
				get
				{
					TextObject textObject = new TextObject("{=6OmbzoBs}{ISSUE_GIVER.LINK}, a merchant from {ISSUE_GIVER_SETTLEMENT}, has told you that {?ISSUE_GIVER.GENDER}her{?}his{\\?} daughter has gone missing. You choose {COMPANION.LINK} and {REQUIRED_TROOP_AMOUNT} men to search for her and bring her back. You expect them to complete this task and return in {ALTERNATIVE_SOLUTION_DAYS} days.", null);
					textObject.SetTextVariable("GOLD_ICON", "{=!}<img src=\"General\\Icons\\Coin@2x\" extend=\"8\">");
					textObject.SetTextVariable("BASE_REWARD_GOLD", this.RewardGold);
					textObject.SetTextVariable("ISSUE_GIVER_SETTLEMENT", base.IssueOwner.CurrentSettlement.Name);
					textObject.SetTextVariable("REQUIRED_TROOP_AMOUNT", this.AlternativeSolutionSentTroops.TotalManCount - 1);
					textObject.SetTextVariable("ALTERNATIVE_SOLUTION_DAYS", base.GetTotalAlternativeSolutionDurationInDays());
					StringHelpers.SetCharacterProperties("PLAYER", CharacterObject.PlayerCharacter, textObject, false);
					StringHelpers.SetCharacterProperties("ISSUE_GIVER", base.IssueOwner.CharacterObject, textObject, false);
					StringHelpers.SetCharacterProperties("COMPANION", base.AlternativeSolutionHero.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x06000D4F RID: 3407 RVA: 0x000571A8 File Offset: 0x000553A8
			protected override void AlternativeSolutionEndWithSuccessConsequence()
			{
				this.ApplySuccessRewards();
				float randomFloat = MBRandom.RandomFloat;
				SkillObject skill;
				if (randomFloat <= 0.33f)
				{
					skill = DefaultSkills.OneHanded;
				}
				else if (randomFloat <= 0.66f)
				{
					skill = DefaultSkills.TwoHanded;
				}
				else
				{
					skill = DefaultSkills.Polearm;
				}
				base.AlternativeSolutionHero.AddSkillXp(skill, (float)((int)(500f + 1000f * base.IssueDifficultyMultiplier)));
			}

			// Token: 0x06000D50 RID: 3408 RVA: 0x0005720C File Offset: 0x0005540C
			protected override void AlternativeSolutionEndWithFailureConsequence()
			{
				this.RelationshipChangeWithIssueOwner = -10;
				if (base.IssueOwner.CurrentSettlement.Village.Bound != null)
				{
					base.IssueOwner.CurrentSettlement.Village.Bound.Town.Prosperity -= 5f;
					base.IssueOwner.CurrentSettlement.Village.Bound.Town.Security -= 5f;
				}
			}

			// Token: 0x17000137 RID: 311
			// (get) Token: 0x06000D51 RID: 3409 RVA: 0x00057290 File Offset: 0x00055490
			public override TextObject IssueAlternativeSolutionSuccessLog
			{
				get
				{
					TextObject textObject = new TextObject("{=MaXA5HJi}Your companions report that the {ISSUE_GIVER.LINK}'s daughter returns to {?ISSUE_GIVER.GENDER}her{?}him{\\?} safe and sound. {?ISSUE_GIVER.GENDER}She{?}He{\\?} is happy and sends {?ISSUE_GIVER.GENDER}her{?}his{\\?} regards with a large pouch of {BASE_REWARD_GOLD}{GOLD_ICON}.", null);
					StringHelpers.SetCharacterProperties("ISSUE_GIVER", base.IssueOwner.CharacterObject, textObject, false);
					textObject.SetTextVariable("GOLD_ICON", "{=!}<img src=\"General\\Icons\\Coin@2x\" extend=\"8\">");
					textObject.SetTextVariable("BASE_REWARD_GOLD", this.RewardGold);
					return textObject;
				}
			}

			// Token: 0x06000D52 RID: 3410 RVA: 0x000572E8 File Offset: 0x000554E8
			private void ApplySuccessRewards()
			{
				GainRenownAction.Apply(Hero.MainHero, 2f, false);
				base.IssueOwner.AddPower(10f);
				this.RelationshipChangeWithIssueOwner = 10;
				if (base.IssueOwner.CurrentSettlement.Village.Bound != null)
				{
					base.IssueOwner.CurrentSettlement.Village.Bound.Town.Security += 10f;
				}
			}

			// Token: 0x17000138 RID: 312
			// (get) Token: 0x06000D53 RID: 3411 RVA: 0x00057360 File Offset: 0x00055560
			public override TextObject Title
			{
				get
				{
					TextObject textObject = new TextObject("{=kr68V5pm}{ISSUE_GIVER.NAME} wants {?ISSUE_GIVER.GENDER}her{?}his{\\?} daughter found", null);
					StringHelpers.SetCharacterProperties("ISSUE_GIVER", base.IssueOwner.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x17000139 RID: 313
			// (get) Token: 0x06000D54 RID: 3412 RVA: 0x00057394 File Offset: 0x00055594
			public override TextObject Description
			{
				get
				{
					TextObject textObject = new TextObject("{=SkzM5eSv}{ISSUE_GIVER.LINK}'s daughter is missing. {?ISSUE_GIVER.GENDER}She{?}He{\\?} is offering a substantial reward to find the young woman and bring her back safely.", null);
					StringHelpers.SetCharacterProperties("ISSUE_GIVER", base.IssueOwner.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x1700013A RID: 314
			// (get) Token: 0x06000D55 RID: 3413 RVA: 0x000573C8 File Offset: 0x000555C8
			public override TextObject IssueAsRumorInSettlement
			{
				get
				{
					TextObject textObject = new TextObject("{=7RyXSkEE}Wouldn't want to be the poor lovesick sap who ran off with {QUEST_GIVER.NAME}'s daughter.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.IssueOwner.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x06000D56 RID: 3414 RVA: 0x000573FA File Offset: 0x000555FA
			protected override void OnGameLoad()
			{
			}

			// Token: 0x06000D57 RID: 3415 RVA: 0x000573FC File Offset: 0x000555FC
			protected override void HourlyTick()
			{
			}

			// Token: 0x06000D58 RID: 3416 RVA: 0x000573FE File Offset: 0x000555FE
			protected override QuestBase GenerateIssueQuest(string questId)
			{
				return new NotableWantsDaughterFoundIssueBehavior.NotableWantsDaughterFoundIssueQuest(questId, base.IssueOwner, CampaignTime.DaysFromNow(19f), this.RewardGold, base.IssueDifficultyMultiplier);
			}

			// Token: 0x06000D59 RID: 3417 RVA: 0x00057422 File Offset: 0x00055622
			public override IssueBase.IssueFrequency GetFrequency()
			{
				return IssueBase.IssueFrequency.Rare;
			}

			// Token: 0x06000D5A RID: 3418 RVA: 0x00057425 File Offset: 0x00055625
			public override ValueTuple<SkillObject, int> GetAlternativeSolutionSkill(Hero hero)
			{
				return new ValueTuple<SkillObject, int>((hero.GetSkillValue(DefaultSkills.Charm) >= hero.GetSkillValue(DefaultSkills.Scouting)) ? DefaultSkills.Charm : DefaultSkills.Scouting, 120);
			}

			// Token: 0x06000D5B RID: 3419 RVA: 0x00057452 File Offset: 0x00055652
			public override bool AlternativeSolutionCondition(out TextObject explanation)
			{
				explanation = TextObject.Empty;
				return QuestHelper.CheckRosterForAlternativeSolution(MobileParty.MainParty.MemberRoster, base.GetTotalAlternativeSolutionNeededMenCount(), ref explanation, 2, false);
			}

			// Token: 0x06000D5C RID: 3420 RVA: 0x00057473 File Offset: 0x00055673
			public override bool DoTroopsSatisfyAlternativeSolution(TroopRoster troopRoster, out TextObject explanation)
			{
				explanation = TextObject.Empty;
				return QuestHelper.CheckRosterForAlternativeSolution(troopRoster, base.GetTotalAlternativeSolutionNeededMenCount(), ref explanation, 2, false);
			}

			// Token: 0x06000D5D RID: 3421 RVA: 0x0005748B File Offset: 0x0005568B
			public override bool IsTroopTypeNeededByAlternativeSolution(CharacterObject character)
			{
				return character.Tier >= 2;
			}

			// Token: 0x06000D5E RID: 3422 RVA: 0x0005749C File Offset: 0x0005569C
			protected override bool CanPlayerTakeQuestConditions(Hero issueGiver, out IssueBase.PreconditionFlags flag, out Hero relationHero, out SkillObject skill)
			{
				bool flag2 = issueGiver.GetRelationWithPlayer() >= -10f && !issueGiver.CurrentSettlement.MapFaction.IsAtWarWith(Hero.MainHero.MapFaction);
				flag = (flag2 ? IssueBase.PreconditionFlags.None : ((!issueGiver.CurrentSettlement.MapFaction.IsAtWarWith(Hero.MainHero.MapFaction)) ? IssueBase.PreconditionFlags.Relation : IssueBase.PreconditionFlags.AtWar));
				relationHero = issueGiver;
				skill = null;
				return flag2;
			}

			// Token: 0x06000D5F RID: 3423 RVA: 0x00057508 File Offset: 0x00055708
			public override bool IssueStayAliveConditions()
			{
				return !base.IssueOwner.CurrentSettlement.IsRaided && !base.IssueOwner.CurrentSettlement.IsUnderRaid;
			}

			// Token: 0x06000D60 RID: 3424 RVA: 0x00057531 File Offset: 0x00055731
			protected override void CompleteIssueWithTimedOutConsequences()
			{
			}

			// Token: 0x06000D61 RID: 3425 RVA: 0x00057533 File Offset: 0x00055733
			internal static void AutoGeneratedStaticCollectObjectsNotableWantsDaughterFoundIssue(object o, List<object> collectedObjects)
			{
				((NotableWantsDaughterFoundIssueBehavior.NotableWantsDaughterFoundIssue)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
			}

			// Token: 0x06000D62 RID: 3426 RVA: 0x00057541 File Offset: 0x00055741
			protected override void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
			{
				base.AutoGeneratedInstanceCollectObjects(collectedObjects);
			}

			// Token: 0x040005F2 RID: 1522
			private const int TroopTierForAlternativeSolution = 2;

			// Token: 0x040005F3 RID: 1523
			private const int RequiredSkillLevelForAlternativeSolution = 120;
		}

		// Token: 0x02000161 RID: 353
		public class NotableWantsDaughterFoundIssueQuest : QuestBase
		{
			// Token: 0x1700013B RID: 315
			// (get) Token: 0x06000D63 RID: 3427 RVA: 0x0005754C File Offset: 0x0005574C
			public override TextObject Title
			{
				get
				{
					TextObject textObject = new TextObject("{=PDhmSieV}{QUEST_GIVER.NAME}'s Kidnapped Daughter at {SETTLEMENT}", null);
					textObject.SetTextVariable("SETTLEMENT", base.QuestGiver.CurrentSettlement.Name);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x1700013C RID: 316
			// (get) Token: 0x06000D64 RID: 3428 RVA: 0x0005759A File Offset: 0x0005579A
			public override bool IsRemainingTimeHidden
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700013D RID: 317
			// (get) Token: 0x06000D65 RID: 3429 RVA: 0x0005759D File Offset: 0x0005579D
			private bool DoesMainPartyHasEnoughScoutingSkill
			{
				get
				{
					return (float)MobilePartyHelper.GetMainPartySkillCounsellor(DefaultSkills.Scouting).GetSkillValue(DefaultSkills.Scouting) >= 150f * this._questDifficultyMultiplier;
				}
			}

			// Token: 0x1700013E RID: 318
			// (get) Token: 0x06000D66 RID: 3430 RVA: 0x000575C8 File Offset: 0x000557C8
			private TextObject _playerStartsQuestLogText
			{
				get
				{
					TextObject textObject = new TextObject("{=1jExD58d}{QUEST_GIVER.LINK}, a merchant from {SETTLEMENT_NAME}, told you that {?QUEST_GIVER.GENDER}her{?}his{\\?} daughter {TARGET_HERO.NAME} has either been abducted or run off with a local rogue. You have agreed to search for her and bring her back to {SETTLEMENT_NAME}. If you cannot find their tracks when you exit settlement, you should visit the nearby villages of {SETTLEMENT_NAME} to look for clues and tracks of the kidnapper.", null);
					textObject.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, false);
					textObject.SetCharacterProperties("TARGET_HERO", this._daughterHero.CharacterObject, false);
					textObject.SetTextVariable("SETTLEMENT_NAME", base.QuestGiver.CurrentSettlement.EncyclopediaLinkWithName);
					textObject.SetTextVariable("BASE_REWARD_GOLD", this.RewardGold);
					textObject.SetTextVariable("GOLD_ICON", "{=!}<img src=\"General\\Icons\\Coin@2x\" extend=\"8\">");
					return textObject;
				}
			}

			// Token: 0x1700013F RID: 319
			// (get) Token: 0x06000D67 RID: 3431 RVA: 0x00057650 File Offset: 0x00055850
			private TextObject _successQuestLogText
			{
				get
				{
					TextObject textObject = new TextObject("{=asVE53ac}Daughter returns to {QUEST_GIVER.LINK}. {?QUEST_GIVER.GENDER}She{?}He{\\?} is happy. Sends {?QUEST_GIVER.GENDER}her{?}his{\\?} regards with a large pouch of {BASE_REWARD}{GOLD_ICON}.", null);
					textObject.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, false);
					textObject.SetTextVariable("GOLD_ICON", "{=!}<img src=\"General\\Icons\\Coin@2x\" extend=\"8\">");
					textObject.SetTextVariable("BASE_REWARD", this.RewardGold);
					return textObject;
				}
			}

			// Token: 0x17000140 RID: 320
			// (get) Token: 0x06000D68 RID: 3432 RVA: 0x000576A4 File Offset: 0x000558A4
			private TextObject _playerDefeatedByRogueLogText
			{
				get
				{
					TextObject textObject = new TextObject("{=i1sth9Ls}You were defeated by the rogue. He and {TARGET_HERO.NAME} ran off while you were unconscious. You failed to bring the daughter back to her {?QUEST_GIVER.GENDER}mother{?}father{\\?} as promised to {QUEST_GIVER.LINK}. {?QUEST_GIVER.GENDER}She{?}He{\\?} is furious.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					StringHelpers.SetCharacterProperties("TARGET_HERO", this._daughterHero.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x17000141 RID: 321
			// (get) Token: 0x06000D69 RID: 3433 RVA: 0x000576EE File Offset: 0x000558EE
			private TextObject _failQuestLogText
			{
				get
				{
					TextObject textObject = new TextObject("{=ak2EMWWR}You failed to bring the daughter back to her {?QUEST_GIVER.GENDER}mother{?}father{\\?} as promised to {QUEST_GIVER.LINK}. {QUEST_GIVER.LINK} is furious", null);
					textObject.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, false);
					return textObject;
				}
			}

			// Token: 0x17000142 RID: 322
			// (get) Token: 0x06000D6A RID: 3434 RVA: 0x00057712 File Offset: 0x00055912
			private TextObject _questCanceledWarDeclaredLog
			{
				get
				{
					TextObject textObject = new TextObject("{=vW6kBki9}Your clan is now at war with {QUEST_GIVER.LINK}'s realm. Your agreement with {QUEST_GIVER.LINK} is canceled.", null);
					textObject.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, false);
					return textObject;
				}
			}

			// Token: 0x17000143 RID: 323
			// (get) Token: 0x06000D6B RID: 3435 RVA: 0x00057736 File Offset: 0x00055936
			private TextObject _playerDeclaredWarQuestLogText
			{
				get
				{
					TextObject textObject = new TextObject("{=bqeWVVEE}Your actions have started a war with {QUEST_GIVER.LINK}'s faction. {?QUEST_GIVER.GENDER}She{?}He{\\?} cancels your agreement and the quest is a failure.", null);
					textObject.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, false);
					return textObject;
				}
			}

			// Token: 0x17000144 RID: 324
			// (get) Token: 0x06000D6C RID: 3436 RVA: 0x0005775A File Offset: 0x0005595A
			private TextObject _villageRaidedCancelQuestLogText
			{
				get
				{
					TextObject textObject = new TextObject("{=aN85Kfnq}{SETTLEMENT} was raided. Your agreement with {QUEST_GIVER.LINK} is canceled.", null);
					textObject.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, false);
					textObject.SetTextVariable("SETTLEMENT", base.QuestGiver.CurrentSettlement.EncyclopediaLinkWithName);
					return textObject;
				}
			}

			// Token: 0x06000D6D RID: 3437 RVA: 0x0005779C File Offset: 0x0005599C
			public NotableWantsDaughterFoundIssueQuest(string questId, Hero questGiver, CampaignTime duration, int baseReward, float issueDifficultyMultiplier) : base(questId, questGiver, duration, baseReward)
			{
				this._questDifficultyMultiplier = issueDifficultyMultiplier;
				this._targetVillage = questGiver.CurrentSettlement.Village.Bound.BoundVillages.GetRandomElementWithPredicate((Village x) => x != questGiver.CurrentSettlement.Village);
				Dictionary<string, CharacterObject> rogueCharacterBasedOnCulture = this._rogueCharacterBasedOnCulture;
				string key = "khuzait";
				Clan clan = Clan.BanditFactions.FirstOrDefault((Clan x) => x.StringId == "steppe_bandits");
				rogueCharacterBasedOnCulture.Add(key, (clan != null) ? clan.Culture.BanditBoss : null);
				Dictionary<string, CharacterObject> rogueCharacterBasedOnCulture2 = this._rogueCharacterBasedOnCulture;
				string key2 = "vlandia";
				Clan clan2 = Clan.BanditFactions.FirstOrDefault((Clan x) => x.StringId == "mountain_bandits");
				rogueCharacterBasedOnCulture2.Add(key2, (clan2 != null) ? clan2.Culture.BanditBoss : null);
				Dictionary<string, CharacterObject> rogueCharacterBasedOnCulture3 = this._rogueCharacterBasedOnCulture;
				string key3 = "aserai";
				Clan clan3 = Clan.BanditFactions.FirstOrDefault((Clan x) => x.StringId == "desert_bandits");
				rogueCharacterBasedOnCulture3.Add(key3, (clan3 != null) ? clan3.Culture.BanditBoss : null);
				Dictionary<string, CharacterObject> rogueCharacterBasedOnCulture4 = this._rogueCharacterBasedOnCulture;
				string key4 = "battania";
				Clan clan4 = Clan.BanditFactions.FirstOrDefault((Clan x) => x.StringId == "forest_bandits");
				rogueCharacterBasedOnCulture4.Add(key4, (clan4 != null) ? clan4.Culture.BanditBoss : null);
				Dictionary<string, CharacterObject> rogueCharacterBasedOnCulture5 = this._rogueCharacterBasedOnCulture;
				string key5 = "sturgia";
				Clan clan5 = Clan.BanditFactions.FirstOrDefault((Clan x) => x.StringId == "sea_raiders");
				rogueCharacterBasedOnCulture5.Add(key5, (clan5 != null) ? clan5.Culture.BanditBoss : null);
				Dictionary<string, CharacterObject> rogueCharacterBasedOnCulture6 = this._rogueCharacterBasedOnCulture;
				string key6 = "empire_w";
				Clan clan6 = Clan.BanditFactions.FirstOrDefault((Clan x) => x.StringId == "mountain_bandits");
				rogueCharacterBasedOnCulture6.Add(key6, (clan6 != null) ? clan6.Culture.BanditBoss : null);
				Dictionary<string, CharacterObject> rogueCharacterBasedOnCulture7 = this._rogueCharacterBasedOnCulture;
				string key7 = "empire_s";
				Clan clan7 = Clan.BanditFactions.FirstOrDefault((Clan x) => x.StringId == "mountain_bandits");
				rogueCharacterBasedOnCulture7.Add(key7, (clan7 != null) ? clan7.Culture.BanditBoss : null);
				Dictionary<string, CharacterObject> rogueCharacterBasedOnCulture8 = this._rogueCharacterBasedOnCulture;
				string key8 = "empire";
				Clan clan8 = Clan.BanditFactions.FirstOrDefault((Clan x) => x.StringId == "mountain_bandits");
				rogueCharacterBasedOnCulture8.Add(key8, (clan8 != null) ? clan8.Culture.BanditBoss : null);
				int heroComesOfAge = Campaign.Current.Models.AgeModel.HeroComesOfAge;
				int age = MBRandom.RandomInt(heroComesOfAge, 25);
				int age2 = MBRandom.RandomInt(heroComesOfAge, 25);
				CharacterObject randomElementWithPredicate = questGiver.CurrentSettlement.Culture.NotableAndWandererTemplates.GetRandomElementWithPredicate((CharacterObject x) => x.Occupation == Occupation.Wanderer && x.IsFemale);
				this._daughterHero = HeroCreator.CreateSpecialHero(randomElementWithPredicate, questGiver.HomeSettlement, questGiver.Clan, null, age);
				this._daughterHero.CharacterObject.HiddenInEncylopedia = true;
				this._daughterHero.Father = questGiver;
				this._rogueHero = HeroCreator.CreateSpecialHero(this.GetRogueCharacterBasedOnCulture(questGiver.Culture), questGiver.HomeSettlement, questGiver.Clan, null, age2);
				this._rogueHero.CharacterObject.HiddenInEncylopedia = true;
				this.SetDialogs();
				base.InitializeQuestOnCreation();
			}

			// Token: 0x06000D6E RID: 3438 RVA: 0x00057B64 File Offset: 0x00055D64
			private CharacterObject GetRogueCharacterBasedOnCulture(CultureObject culture)
			{
				CharacterObject characterObject;
				if (this._rogueCharacterBasedOnCulture.ContainsKey(culture.StringId))
				{
					characterObject = this._rogueCharacterBasedOnCulture[culture.StringId];
				}
				else
				{
					characterObject = base.QuestGiver.CurrentSettlement.Culture.NotableAndWandererTemplates.GetRandomElementWithPredicate((CharacterObject x) => x.Occupation == Occupation.GangLeader && !x.IsFemale);
				}
				characterObject.Culture = base.QuestGiver.Culture;
				return characterObject;
			}

			// Token: 0x06000D6F RID: 3439 RVA: 0x00057BE4 File Offset: 0x00055DE4
			protected override void SetDialogs()
			{
				TextObject textObject = new TextObject("{=PZq1EMcx}Thank you for your help. [if:convo_worried]I am still very worried about my girl {TARGET_HERO.FIRSTNAME}. Please find her and bring her back to me as soon as you can.", null);
				StringHelpers.SetCharacterProperties("TARGET_HERO", this._daughterHero.CharacterObject, textObject, false);
				TextObject npcText = new TextObject("{=sglD6abb}Please! Bring my daughter back.", null);
				TextObject npcText2 = new TextObject("{=ddEu5IFQ}I hope so.", null);
				TextObject npcText3 = new TextObject("{=IdKG3IaS}Good to hear that.", null);
				TextObject text = new TextObject("{=0hXofVLx}Don't worry I'll bring her.", null);
				TextObject text2 = new TextObject("{=zpqP5LsC}I'll go right away.", null);
				this.OfferDialogFlow = DialogFlow.CreateDialogFlow("issue_classic_quest_start", 100).NpcLine(textObject, null, null).Condition(() => Hero.OneToOneConversationHero == base.QuestGiver && !this._didPlayerBeatRouge).Consequence(new ConversationSentence.OnConsequenceDelegate(this.QuestAcceptedConsequences)).CloseDialog();
				this.DiscussDialogFlow = DialogFlow.CreateDialogFlow("quest_discuss", 100).NpcLine(npcText, null, null).Condition(() => Hero.OneToOneConversationHero == base.QuestGiver && !this._didPlayerBeatRouge).BeginPlayerOptions().PlayerOption(text, null).NpcLine(npcText2, null, null).CloseDialog().PlayerOption(text2, null).NpcLine(npcText3, null, null).CloseDialog();
				Campaign.Current.ConversationManager.AddDialogFlow(this.GetRougeDialogFlow(), this);
				Campaign.Current.ConversationManager.AddDialogFlow(this.GetDaughterAfterFightDialog(), this);
				Campaign.Current.ConversationManager.AddDialogFlow(this.GetDaughterAfterAcceptDialog(), this);
				Campaign.Current.ConversationManager.AddDialogFlow(this.GetDaughterAfterPersuadedDialog(), this);
				Campaign.Current.ConversationManager.AddDialogFlow(this.GetDaughterDialogWhenVillageRaid(), this);
				Campaign.Current.ConversationManager.AddDialogFlow(this.GetRougeAfterAcceptDialog(), this);
				Campaign.Current.ConversationManager.AddDialogFlow(this.GetRogueAfterPersuadedDialog(), this);
			}

			// Token: 0x06000D70 RID: 3440 RVA: 0x00057D88 File Offset: 0x00055F88
			protected override void InitializeQuestOnGameLoad()
			{
				this.SetDialogs();
				if (this._daughterHero != null)
				{
					this._daughterHero.CharacterObject.HiddenInEncylopedia = true;
				}
				if (this._rogueHero != null)
				{
					this._rogueHero.CharacterObject.HiddenInEncylopedia = true;
				}
			}

			// Token: 0x06000D71 RID: 3441 RVA: 0x00057DC2 File Offset: 0x00055FC2
			protected override void HourlyTick()
			{
			}

			// Token: 0x06000D72 RID: 3442 RVA: 0x00057DC4 File Offset: 0x00055FC4
			private bool IsRougeHero(IAgent agent)
			{
				return agent.Character == this._rogueHero.CharacterObject;
			}

			// Token: 0x06000D73 RID: 3443 RVA: 0x00057DD9 File Offset: 0x00055FD9
			private bool IsDaughterHero(IAgent agent)
			{
				return agent.Character == this._daughterHero.CharacterObject;
			}

			// Token: 0x06000D74 RID: 3444 RVA: 0x00057DEE File Offset: 0x00055FEE
			private bool IsMainHero(IAgent agent)
			{
				return agent.Character == CharacterObject.PlayerCharacter;
			}

			// Token: 0x06000D75 RID: 3445 RVA: 0x00057E00 File Offset: 0x00056000
			private bool multi_character_conversation_on_condition()
			{
				if (!this._villageIsRaidedTalkWithDaughter && !this._isDaughterPersuaded && !this._didPlayerBeatRouge && !this._acceptedDaughtersEscape && this._isQuestTargetMission && (CharacterObject.OneToOneConversationCharacter == this._daughterHero.CharacterObject || CharacterObject.OneToOneConversationCharacter == this._rogueHero.CharacterObject))
				{
					MBList<Agent> agents = new MBList<Agent>();
					foreach (Agent agent in Mission.Current.GetNearbyAgents(Agent.Main.Position.AsVec2, 100f, agents))
					{
						if (agent.Character == this._daughterHero.CharacterObject)
						{
							this._daughterAgent = agent;
							if (Mission.Current.GetMissionBehavior<MissionConversationLogic>() != null && Hero.OneToOneConversationHero != this._daughterHero)
							{
								Campaign.Current.ConversationManager.AddConversationAgents(new List<Agent>
								{
									this._daughterAgent
								}, true);
							}
						}
						else if (agent.Character == this._rogueHero.CharacterObject)
						{
							this._rogueAgent = agent;
							if (Mission.Current.GetMissionBehavior<MissionConversationLogic>() != null && Hero.OneToOneConversationHero != this._rogueHero)
							{
								Campaign.Current.ConversationManager.AddConversationAgents(new List<Agent>
								{
									this._rogueAgent
								}, true);
							}
						}
					}
					return this._daughterAgent != null && this._rogueAgent != null && this._daughterAgent.Health > 10f && this._rogueAgent.Health > 10f;
				}
				return false;
			}

			// Token: 0x06000D76 RID: 3446 RVA: 0x00057FB8 File Offset: 0x000561B8
			private bool daughter_conversation_after_fight_on_condition()
			{
				return CharacterObject.OneToOneConversationCharacter == this._daughterHero.CharacterObject && this._didPlayerBeatRouge;
			}

			// Token: 0x06000D77 RID: 3447 RVA: 0x00057FD4 File Offset: 0x000561D4
			private void multi_agent_conversation_on_consequence()
			{
				this._task = this.GetPersuasionTask();
			}

			// Token: 0x06000D78 RID: 3448 RVA: 0x00057FE4 File Offset: 0x000561E4
			private DialogFlow GetRougeDialogFlow()
			{
				TextObject textObject = new TextObject("{=ovFbMMTJ}Who are you? Are you one of the bounty hunters sent by {QUEST_GIVER.LINK} to track us? Like we're animals or something? Look friend, we have done nothing wrong. As you may have figured out already, this woman and I, we love each other. I didn't force her to do anything.[ib:closed][if:convo_innocent_smile]", null);
				StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
				TextObject textObject2 = new TextObject("{=D25oY3j1}Thank you {?PLAYER.GENDER}lady{?}sir{\\?}. For your kindness and understanding. We won't forget this.[ib:demure][if:convo_happy]", null);
				StringHelpers.SetCharacterProperties("PLAYER", CharacterObject.PlayerCharacter, textObject2, false);
				TextObject textObject3 = new TextObject("{=oL3amiu1}Come {DAUGHTER_NAME.NAME}, let's go before other hounds sniff our trail... I mean... No offense {?PLAYER.GENDER}madam{?}sir{\\?}.", null);
				StringHelpers.SetCharacterProperties("DAUGHTER_NAME", this._daughterHero.CharacterObject, textObject3, false);
				StringHelpers.SetCharacterProperties("PLAYER", CharacterObject.PlayerCharacter, textObject3, false);
				TextObject textObject4 = new TextObject("{=92sbq1YY}I'm no child, {?PLAYER.GENDER}lady{?}sir{\\?}! Draw your weapon! I challenge you to a duel![ib:warrior2][if:convo_excited]", null);
				StringHelpers.SetCharacterProperties("PLAYER", CharacterObject.PlayerCharacter, textObject4, false);
				TextObject textObject5 = new TextObject("{=jfzErupx}He is right! I ran away with him willingly. I love my {?QUEST_GIVER.GENDER}mother{?}father{\\?},[ib:closed][if:convo_grave] but {?QUEST_GIVER.GENDER}she{?}he{\\?} can be such a tyrant. Please {?PLAYER.GENDER}lady{?}sir{\\?}, if you believe in freedom and love, please leave us be.", null);
				StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject5, false);
				StringHelpers.SetCharacterProperties("PLAYER", CharacterObject.PlayerCharacter, textObject5, false);
				TextObject textObject6 = new TextObject("{=5NljlbLA}Thank you kind {?PLAYER.GENDER}lady{?}sir{\\?}, thank you.", null);
				StringHelpers.SetCharacterProperties("PLAYER", CharacterObject.PlayerCharacter, textObject6, false);
				TextObject textObject7 = new TextObject("{=i5fNZrhh}Please, {?PLAYER.GENDER}lady{?}sir{\\?}. I love him truly and I wish to spend the rest of my life with him.[ib:demure][if:convo_worried] I beg of you, please don't stand in our way.", null);
				StringHelpers.SetCharacterProperties("PLAYER", CharacterObject.PlayerCharacter, textObject7, false);
				TextObject textObject8 = new TextObject("{=0RCdPKj2}Yes {?QUEST_GIVER.GENDER}she{?}he{\\?} would probably be sad. But not because of what you think. See, {QUEST_GIVER.LINK} promised me to one of {?QUEST_GIVER.GENDER}her{?}his{\\?} allies' sons and this will devastate {?QUEST_GIVER.GENDER}her{?}his{\\?} plans. That is true.", null);
				StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject8, false);
				TextObject text = new TextObject("{=5W7Kxfq9}I understand. If that is the case, I will let you go.", null);
				TextObject text2 = new TextObject("{=3XimdHOn}How do I know he's not forcing you to say that?", null);
				TextObject textObject9 = new TextObject("{=zNqDEuAw}But I've promised to find you and return you to your {?QUEST_GIVER.GENDER}mother{?}father{\\?}. {?QUEST_GIVER.GENDER}She{?}He{\\?} would be devastated.", null);
				StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject9, false);
				TextObject textObject10 = new TextObject("{=tuaQ5uU3}I guess the only way to free you from this pretty boy's spell is to kill him.", null);
				TextObject textObject11 = new TextObject("{=HDCmeGhG}I'm sorry but I gave a promise. I don't break my promises.", null);
				TextObject text3 = new TextObject("{=VGrHWxzf}This will be a massacre, not a duel, but I'm fine with that.", null);
				TextObject text4 = new TextObject("{=sytYViXb}I accept your duel.", null);
				DialogFlow dialogFlow = DialogFlow.CreateDialogFlow("start", 125).NpcLine(textObject, new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsRougeHero), new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsMainHero)).Condition(new ConversationSentence.OnConditionDelegate(this.multi_character_conversation_on_condition)).Consequence(new ConversationSentence.OnConsequenceDelegate(this.multi_agent_conversation_on_consequence)).NpcLine(textObject5, new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsDaughterHero), new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsMainHero)).BeginPlayerOptions().PlayerOption(text, new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsDaughterHero)).NpcLine(textObject2, new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsRougeHero), new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsMainHero)).NpcLine(textObject3, new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsRougeHero), new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsDaughterHero)).NpcLine(textObject6, new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsDaughterHero), new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsMainHero)).Consequence(delegate
				{
					Campaign.Current.ConversationManager.ConversationEndOneShot += this.PlayerAcceptedDaughtersEscape;
				}).CloseDialog().PlayerOption(text2, new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsDaughterHero)).NpcLine(textObject7, new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsDaughterHero), new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsMainHero)).PlayerLine(textObject9, new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsDaughterHero)).NpcLine(textObject8, new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsDaughterHero), new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsMainHero)).GotoDialogState("start_daughter_persuade_to_come_persuasion").GoBackToDialogState("daughter_persuade_to_come_persuasion_finished").PlayerLine((Hero.MainHero.GetTraitLevel(DefaultTraits.Mercy) < 0) ? textObject10 : textObject11, new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsDaughterHero)).NpcLine(textObject4, new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsRougeHero), new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsMainHero)).BeginPlayerOptions().PlayerOption(text3, new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsRougeHero)).NpcLine(new TextObject("{=XWVW0oTB}You bastard![ib:aggressive][if:convo_furious]", null), new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsRougeHero), null).Consequence(delegate
				{
					Campaign.Current.ConversationManager.ConversationEndOneShot += this.PlayerRejectsDuelFight;
				}).CloseDialog().PlayerOption(text4, new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsRougeHero)).NpcLine(new TextObject("{=jqahxjWD}Heaven protect me![ib:aggressive][if:convo_astonished]", null), new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsRougeHero), null).Consequence(delegate
				{
					Campaign.Current.ConversationManager.ConversationEndOneShot += this.PlayerAcceptsDuelFight;
				}).CloseDialog().EndPlayerOptions().EndPlayerOptions().CloseDialog();
				this.AddPersuasionDialogs(dialogFlow);
				return dialogFlow;
			}

			// Token: 0x06000D79 RID: 3449 RVA: 0x000583FC File Offset: 0x000565FC
			private DialogFlow GetDaughterAfterFightDialog()
			{
				TextObject npcText = new TextObject("{=MN2v1AZQ}I hate you! You killed him! I can't believe it! I will hate you with all my heart till my dying days.[if:convo_angry]", null);
				TextObject npcText2 = new TextObject("{=TTkVcObg}What choice do I have, you heartless bastard?![if:convo_furious]", null);
				TextObject textObject = new TextObject("{=XqsrsjiL}I did what I had to do. Pack up, you need to go.", null);
				TextObject textObject2 = new TextObject("{=KQ3aYvp3}Some day you'll see I did you a favor. Pack up, you need to go.", null);
				return DialogFlow.CreateDialogFlow("start", 125).NpcLine(npcText, null, null).Condition(new ConversationSentence.OnConditionDelegate(this.daughter_conversation_after_fight_on_condition)).PlayerLine((Hero.MainHero.GetTraitLevel(DefaultTraits.Mercy) < 0) ? textObject : textObject2, null).NpcLine(npcText2, null, null).Consequence(delegate
				{
					Campaign.Current.ConversationManager.ConversationEndOneShot += this.PlayerWonTheFight;
				}).CloseDialog();
			}

			// Token: 0x06000D7A RID: 3450 RVA: 0x00058498 File Offset: 0x00056698
			private DialogFlow GetDaughterAfterAcceptDialog()
			{
				TextObject textObject = new TextObject("{=0Wg00sfN}Thank you, {?PLAYER.GENDER}madam{?}sir{\\?}. We will be moving immediately.", null);
				StringHelpers.SetCharacterProperties("PLAYER", CharacterObject.PlayerCharacter, textObject, false);
				TextObject playerText = new TextObject("{=kUReBc04}Good.", null);
				return DialogFlow.CreateDialogFlow("start", 125).NpcLine(textObject, null, null).Condition(new ConversationSentence.OnConditionDelegate(this.daughter_conversation_after_accept_on_condition)).PlayerLine(playerText, null).CloseDialog();
			}

			// Token: 0x06000D7B RID: 3451 RVA: 0x00058500 File Offset: 0x00056700
			private bool daughter_conversation_after_accept_on_condition()
			{
				return CharacterObject.OneToOneConversationCharacter == this._daughterHero.CharacterObject && this._acceptedDaughtersEscape;
			}

			// Token: 0x06000D7C RID: 3452 RVA: 0x0005851C File Offset: 0x0005671C
			private DialogFlow GetDaughterAfterPersuadedDialog()
			{
				TextObject textObject = new TextObject("{=B8bHpJRP}You are right, {?PLAYER.GENDER}my lady{?}sir{\\?}. I should be moving immediately.", null);
				StringHelpers.SetCharacterProperties("PLAYER", CharacterObject.PlayerCharacter, textObject, false);
				TextObject playerText = new TextObject("{=kUReBc04}Good.", null);
				return DialogFlow.CreateDialogFlow("start", 125).NpcLine(textObject, null, null).Condition(new ConversationSentence.OnConditionDelegate(this.daughter_conversation_after_persuaded_on_condition)).PlayerLine(playerText, null).CloseDialog();
			}

			// Token: 0x06000D7D RID: 3453 RVA: 0x00058584 File Offset: 0x00056784
			private DialogFlow GetDaughterDialogWhenVillageRaid()
			{
				return DialogFlow.CreateDialogFlow("start", 125).NpcLine(new TextObject("{=w0HPC53e}Who are you? What do you want from me?[ib:nervous][if:convo_bared_teeth]", null), null, null).Condition(() => this._villageIsRaidedTalkWithDaughter).PlayerLine(new TextObject("{=iRupMGI0}Calm down! Your father has sent me to find you.", null), null).NpcLine(new TextObject("{=dwNquUNr}My father? Oh, thank god! I saw terrible things. [ib:nervous2][if:convo_shocked]They took my beloved one and slew many innocents without hesitation.", null), null, null).PlayerLine("{=HtAr22re}Try to forget all about these and return to your father's house.", null).NpcLine("{=FgSIsasF}Yes, you are right. I shall be on my way...", null, null).Consequence(delegate
				{
					Campaign.Current.ConversationManager.ConversationEndOneShot += delegate()
					{
						this.ApplyDeliverySuccessConsequences();
						base.CompleteQuestWithSuccess();
						base.AddLog(this._successQuestLogText, false);
						this._villageIsRaidedTalkWithDaughter = false;
					};
				}).CloseDialog();
			}

			// Token: 0x06000D7E RID: 3454 RVA: 0x00058610 File Offset: 0x00056810
			private bool daughter_conversation_after_persuaded_on_condition()
			{
				return CharacterObject.OneToOneConversationCharacter == this._daughterHero.CharacterObject && this._isDaughterPersuaded;
			}

			// Token: 0x06000D7F RID: 3455 RVA: 0x0005862C File Offset: 0x0005682C
			private DialogFlow GetRougeAfterAcceptDialog()
			{
				TextObject textObject = new TextObject("{=wlKtDR2z}Thank you, {?PLAYER.GENDER}my lady{?}sir{\\?}.", null);
				StringHelpers.SetCharacterProperties("PLAYER", CharacterObject.PlayerCharacter, textObject, false);
				return DialogFlow.CreateDialogFlow("start", 125).NpcLine(textObject, null, null).Condition(new ConversationSentence.OnConditionDelegate(this.rogue_conversation_after_accept_on_condition)).PlayerLine(new TextObject("{=0YJGvJ7o}You should leave now.", null), null).NpcLine(new TextObject("{=6Q4cPOSG}Yes, we will.", null), null, null).CloseDialog();
			}

			// Token: 0x06000D80 RID: 3456 RVA: 0x000586A4 File Offset: 0x000568A4
			private bool rogue_conversation_after_accept_on_condition()
			{
				return CharacterObject.OneToOneConversationCharacter == this._rogueHero.CharacterObject && this._acceptedDaughtersEscape;
			}

			// Token: 0x06000D81 RID: 3457 RVA: 0x000586C0 File Offset: 0x000568C0
			private DialogFlow GetRogueAfterPersuadedDialog()
			{
				TextObject textObject = new TextObject("{=GFt9KiHP}You are right. Maybe we need to persuade {QUEST_GIVER.NAME}.", null);
				StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
				TextObject playerText = new TextObject("{=btJkBTSF}I am sure you can solve it.", null);
				return DialogFlow.CreateDialogFlow("start", 125).NpcLine(textObject, null, null).Condition(new ConversationSentence.OnConditionDelegate(this.rogue_conversation_after_persuaded_on_condition)).PlayerLine(playerText, null).CloseDialog();
			}

			// Token: 0x06000D82 RID: 3458 RVA: 0x0005872E File Offset: 0x0005692E
			private bool rogue_conversation_after_persuaded_on_condition()
			{
				return CharacterObject.OneToOneConversationCharacter == this._rogueHero.CharacterObject && this._isDaughterPersuaded;
			}

			// Token: 0x06000D83 RID: 3459 RVA: 0x0005874C File Offset: 0x0005694C
			protected override void OnTimedOut()
			{
				this.ApplyDeliveryRejectedFailConsequences();
				TextObject textObject = new TextObject("{=KAvwytDK}You didn't bring {DAUGHTER.NAME} to {QUEST_GIVER.LINK}. {?QUEST_GIVER.GENDER}she{?}he{\\?} must be furious.", null);
				StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
				StringHelpers.SetCharacterProperties("DAUGHTER", this._daughterHero.CharacterObject, textObject, false);
				base.AddLog(textObject, false);
			}

			// Token: 0x06000D84 RID: 3460 RVA: 0x000587A4 File Offset: 0x000569A4
			private void PlayerAcceptedDaughtersEscape()
			{
				this._acceptedDaughtersEscape = true;
			}

			// Token: 0x06000D85 RID: 3461 RVA: 0x000587AD File Offset: 0x000569AD
			private void PlayerWonTheFight()
			{
				this._isDaughterCaptured = true;
				Mission.Current.SetMissionMode(MissionMode.StartUp, false);
			}

			// Token: 0x06000D86 RID: 3462 RVA: 0x000587C4 File Offset: 0x000569C4
			private void ApplyDaughtersEscapeAcceptedFailConsequences()
			{
				this.RelationshipChangeWithQuestGiver = -10;
				if (base.QuestGiver.CurrentSettlement.Village.Bound != null)
				{
					base.QuestGiver.CurrentSettlement.Village.Bound.Town.Security -= 5f;
					base.QuestGiver.CurrentSettlement.Village.Bound.Town.Prosperity -= 5f;
				}
			}

			// Token: 0x06000D87 RID: 3463 RVA: 0x00058848 File Offset: 0x00056A48
			private void ApplyDeliveryRejectedFailConsequences()
			{
				this.RelationshipChangeWithQuestGiver = -10;
				if (base.QuestGiver.CurrentSettlement.Village.Bound != null)
				{
					base.QuestGiver.CurrentSettlement.Village.Bound.Town.Security -= 5f;
					base.QuestGiver.CurrentSettlement.Village.Bound.Town.Prosperity -= 5f;
				}
			}

			// Token: 0x06000D88 RID: 3464 RVA: 0x000588CC File Offset: 0x00056ACC
			private void ApplyDeliverySuccessConsequences()
			{
				GainRenownAction.Apply(Hero.MainHero, 2f, false);
				base.QuestGiver.AddPower(10f);
				this.RelationshipChangeWithQuestGiver = 10;
				if (base.QuestGiver.CurrentSettlement.Village.Bound != null)
				{
					base.QuestGiver.CurrentSettlement.Village.Bound.Town.Security += 10f;
				}
				GiveGoldAction.ApplyBetweenCharacters(null, Hero.MainHero, this.RewardGold, false);
			}

			// Token: 0x06000D89 RID: 3465 RVA: 0x00058958 File Offset: 0x00056B58
			private void PlayerRejectsDuelFight()
			{
				this._rogueAgent = (Agent)MissionConversationLogic.Current.ConversationManager.ConversationAgents.First((IAgent x) => !x.Character.IsFemale);
				List<Agent> list = new List<Agent>
				{
					Agent.Main
				};
				List<Agent> opponentSideAgents = new List<Agent>
				{
					this._rogueAgent
				};
				MBList<Agent> agents = new MBList<Agent>();
				foreach (Agent agent in Mission.Current.GetNearbyAgents(Agent.Main.Position.AsVec2, 30f, agents))
				{
					foreach (Hero hero in Hero.MainHero.CompanionsInParty)
					{
						if (agent.Character == hero.CharacterObject)
						{
							list.Add(agent);
							break;
						}
					}
				}
				this._rogueAgent.Health = (float)(150 + list.Count * 20);
				this._rogueAgent.Defensiveness = 1f;
				Mission.Current.GetMissionBehavior<MissionFightHandler>().StartCustomFight(list, opponentSideAgents, false, false, new MissionFightHandler.OnFightEndDelegate(this.StartConversationAfterFight));
			}

			// Token: 0x06000D8A RID: 3466 RVA: 0x00058ACC File Offset: 0x00056CCC
			private void PlayerAcceptsDuelFight()
			{
				this._rogueAgent = (Agent)MissionConversationLogic.Current.ConversationManager.ConversationAgents.First((IAgent x) => !x.Character.IsFemale);
				List<Agent> playerSideAgents = new List<Agent>
				{
					Agent.Main
				};
				List<Agent> opponentSideAgents = new List<Agent>
				{
					this._rogueAgent
				};
				MBList<Agent> agents = new MBList<Agent>();
				foreach (Agent agent in Mission.Current.GetNearbyAgents(Agent.Main.Position.AsVec2, 30f, agents))
				{
					foreach (Hero hero in Hero.MainHero.CompanionsInParty)
					{
						if (agent.Character == hero.CharacterObject)
						{
							agent.SetTeam(Mission.Current.SpectatorTeam, false);
							DailyBehaviorGroup behaviorGroup = agent.GetComponent<CampaignAgentComponent>().AgentNavigator.GetBehaviorGroup<DailyBehaviorGroup>();
							if (behaviorGroup.GetActiveBehavior() is FollowAgentBehavior)
							{
								behaviorGroup.GetBehavior<FollowAgentBehavior>().SetTargetAgent(null);
								break;
							}
							break;
						}
					}
				}
				this._rogueAgent.Health = 200f;
				Mission.Current.GetMissionBehavior<MissionFightHandler>().StartCustomFight(playerSideAgents, opponentSideAgents, false, false, new MissionFightHandler.OnFightEndDelegate(this.StartConversationAfterFight));
			}

			// Token: 0x06000D8B RID: 3467 RVA: 0x00058C64 File Offset: 0x00056E64
			private void StartConversationAfterFight(bool isPlayerSideWon)
			{
				if (isPlayerSideWon)
				{
					this._didPlayerBeatRouge = true;
					Campaign.Current.ConversationManager.SetupAndStartMissionConversation(this._daughterAgent, Mission.Current.MainAgent, false);
					TraitLevelingHelper.OnHostileAction(-50);
					return;
				}
				this._playerDefeatedByRogue = true;
			}

			// Token: 0x06000D8C RID: 3468 RVA: 0x00058CA0 File Offset: 0x00056EA0
			private void AddPersuasionDialogs(DialogFlow dialog)
			{
				TextObject textObject = new TextObject("{=ob5SejgJ}I will not abandon my love, {?PLAYER.GENDER}lady{?}sir{\\?}!", null);
				StringHelpers.SetCharacterProperties("PLAYER", CharacterObject.PlayerCharacter, textObject, false);
				TextObject textObject2 = new TextObject("{=cqe8FU8M}{?QUEST_GIVER.GENDER}She{?}He{\\?} cares nothing about me! Only about {?QUEST_GIVER.GENDER}her{?}his{\\?} reputation in our district.", null);
				StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject2, false);
				dialog.AddDialogLine("daughter_persuade_to_come_introduction", "start_daughter_persuade_to_come_persuasion", "daughter_persuade_to_come_start_reservation", textObject2.ToString(), null, new ConversationSentence.OnConsequenceDelegate(this.persuasion_start_with_daughter_on_consequence), this, 100, null, new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsDaughterHero), new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsMainHero));
				dialog.AddDialogLine("daughter_persuade_to_come_rejected", "daughter_persuade_to_come_start_reservation", "daughter_persuade_to_come_persuasion_failed", "{=!}{FAILED_PERSUASION_LINE}", new ConversationSentence.OnConditionDelegate(this.daughter_persuade_to_come_persuasion_failed_on_condition), new ConversationSentence.OnConsequenceDelegate(this.daughter_persuade_to_come_persuasion_failed_on_consequence), this, 100, null, new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsDaughterHero), new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsMainHero));
				dialog.AddDialogLine("daughter_persuade_to_come_failed", "daughter_persuade_to_come_persuasion_failed", "daughter_persuade_to_come_persuasion_finished", textObject.ToString(), null, null, this, 100, null, null, null);
				dialog.AddDialogLine("daughter_persuade_to_come_start", "daughter_persuade_to_come_start_reservation", "daughter_persuade_to_come_persuasion_select_option", "{=9b2BETct}I have already decided. Don't expect me to change my mind.", () => !this.daughter_persuade_to_come_persuasion_failed_on_condition(), null, this, 100, null, new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsDaughterHero), new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsMainHero));
				dialog.AddDialogLine("daughter_persuade_to_come_success", "daughter_persuade_to_come_start_reservation", "close_window", "{=3tmXBpRH}You're right. I cannot do this. I will return to my family. ", new ConversationSentence.OnConditionDelegate(ConversationManager.GetPersuasionProgressSatisfied), new ConversationSentence.OnConsequenceDelegate(this.daughter_persuade_to_come_persuasion_success_on_consequence), this, int.MaxValue, null, new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsDaughterHero), new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsMainHero));
				string id = "daughter_persuade_to_come_select_option_1";
				string inputToken = "daughter_persuade_to_come_persuasion_select_option";
				string outputToken = "daughter_persuade_to_come_persuasion_selected_option_response";
				string text = "{=!}{DAUGHTER_PERSUADE_TO_COME_PERSUADE_ATTEMPT_1}";
				ConversationSentence.OnConditionDelegate conditionDelegate = new ConversationSentence.OnConditionDelegate(this.persuasion_select_option_1_on_condition);
				ConversationSentence.OnConsequenceDelegate consequenceDelegate = new ConversationSentence.OnConsequenceDelegate(this.persuasion_select_option_1_on_consequence);
				ConversationSentence.OnPersuasionOptionDelegate persuasionOptionDelegate = new ConversationSentence.OnPersuasionOptionDelegate(this.persuasion_setup_option_1);
				ConversationSentence.OnClickableConditionDelegate clickableConditionDelegate = new ConversationSentence.OnClickableConditionDelegate(this.persuasion_clickable_option_1_on_condition);
				dialog.AddPlayerLine(id, inputToken, outputToken, text, conditionDelegate, consequenceDelegate, this, 100, clickableConditionDelegate, persuasionOptionDelegate, new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsMainHero), new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsDaughterHero));
				string id2 = "daughter_persuade_to_come_select_option_2";
				string inputToken2 = "daughter_persuade_to_come_persuasion_select_option";
				string outputToken2 = "daughter_persuade_to_come_persuasion_selected_option_response";
				string text2 = "{=!}{DAUGHTER_PERSUADE_TO_COME_PERSUADE_ATTEMPT_2}";
				ConversationSentence.OnConditionDelegate conditionDelegate2 = new ConversationSentence.OnConditionDelegate(this.persuasion_select_option_2_on_condition);
				ConversationSentence.OnConsequenceDelegate consequenceDelegate2 = new ConversationSentence.OnConsequenceDelegate(this.persuasion_select_option_2_on_consequence);
				persuasionOptionDelegate = new ConversationSentence.OnPersuasionOptionDelegate(this.persuasion_setup_option_2);
				clickableConditionDelegate = new ConversationSentence.OnClickableConditionDelegate(this.persuasion_clickable_option_2_on_condition);
				dialog.AddPlayerLine(id2, inputToken2, outputToken2, text2, conditionDelegate2, consequenceDelegate2, this, 100, clickableConditionDelegate, persuasionOptionDelegate, new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsMainHero), new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsDaughterHero));
				string id3 = "daughter_persuade_to_come_select_option_3";
				string inputToken3 = "daughter_persuade_to_come_persuasion_select_option";
				string outputToken3 = "daughter_persuade_to_come_persuasion_selected_option_response";
				string text3 = "{=!}{DAUGHTER_PERSUADE_TO_COME_PERSUADE_ATTEMPT_3}";
				ConversationSentence.OnConditionDelegate conditionDelegate3 = new ConversationSentence.OnConditionDelegate(this.persuasion_select_option_3_on_condition);
				ConversationSentence.OnConsequenceDelegate consequenceDelegate3 = new ConversationSentence.OnConsequenceDelegate(this.persuasion_select_option_3_on_consequence);
				persuasionOptionDelegate = new ConversationSentence.OnPersuasionOptionDelegate(this.persuasion_setup_option_3);
				clickableConditionDelegate = new ConversationSentence.OnClickableConditionDelegate(this.persuasion_clickable_option_3_on_condition);
				dialog.AddPlayerLine(id3, inputToken3, outputToken3, text3, conditionDelegate3, consequenceDelegate3, this, 100, clickableConditionDelegate, persuasionOptionDelegate, new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsMainHero), new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsDaughterHero));
				string id4 = "daughter_persuade_to_come_select_option_4";
				string inputToken4 = "daughter_persuade_to_come_persuasion_select_option";
				string outputToken4 = "daughter_persuade_to_come_persuasion_selected_option_response";
				string text4 = "{=!}{DAUGHTER_PERSUADE_TO_COME_PERSUADE_ATTEMPT_4}";
				ConversationSentence.OnConditionDelegate conditionDelegate4 = new ConversationSentence.OnConditionDelegate(this.persuasion_select_option_4_on_condition);
				ConversationSentence.OnConsequenceDelegate consequenceDelegate4 = new ConversationSentence.OnConsequenceDelegate(this.persuasion_select_option_4_on_consequence);
				persuasionOptionDelegate = new ConversationSentence.OnPersuasionOptionDelegate(this.persuasion_setup_option_4);
				clickableConditionDelegate = new ConversationSentence.OnClickableConditionDelegate(this.persuasion_clickable_option_4_on_condition);
				dialog.AddPlayerLine(id4, inputToken4, outputToken4, text4, conditionDelegate4, consequenceDelegate4, this, 100, clickableConditionDelegate, persuasionOptionDelegate, new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsMainHero), new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsDaughterHero));
				dialog.AddDialogLine("daughter_persuade_to_come_select_option_reaction", "daughter_persuade_to_come_persuasion_selected_option_response", "daughter_persuade_to_come_start_reservation", "{=D0xDRqvm}{PERSUASION_REACTION}", new ConversationSentence.OnConditionDelegate(this.persuasion_selected_option_response_on_condition), new ConversationSentence.OnConsequenceDelegate(this.persuasion_selected_option_response_on_consequence), this, 100, null, null, null);
			}

			// Token: 0x06000D8D RID: 3469 RVA: 0x00059020 File Offset: 0x00057220
			private void persuasion_selected_option_response_on_consequence()
			{
				Tuple<PersuasionOptionArgs, PersuasionOptionResult> tuple = ConversationManager.GetPersuasionChosenOptions().Last<Tuple<PersuasionOptionArgs, PersuasionOptionResult>>();
				float difficulty = Campaign.Current.Models.PersuasionModel.GetDifficulty(PersuasionDifficulty.Hard);
				float moveToNextStageChance;
				float blockRandomOptionChance;
				Campaign.Current.Models.PersuasionModel.GetEffectChances(tuple.Item1, out moveToNextStageChance, out blockRandomOptionChance, difficulty);
				this._task.ApplyEffects(moveToNextStageChance, blockRandomOptionChance);
			}

			// Token: 0x06000D8E RID: 3470 RVA: 0x0005907C File Offset: 0x0005727C
			private bool persuasion_selected_option_response_on_condition()
			{
				PersuasionOptionResult item = ConversationManager.GetPersuasionChosenOptions().Last<Tuple<PersuasionOptionArgs, PersuasionOptionResult>>().Item2;
				MBTextManager.SetTextVariable("PERSUASION_REACTION", PersuasionHelper.GetDefaultPersuasionOptionReaction(item), false);
				return true;
			}

			// Token: 0x06000D8F RID: 3471 RVA: 0x000590AC File Offset: 0x000572AC
			private bool persuasion_select_option_1_on_condition()
			{
				if (this._task.Options.Count > 0)
				{
					TextObject textObject = new TextObject("{=bSo9hKwr}{PERSUASION_OPTION_LINE} {SUCCESS_CHANCE}", null);
					textObject.SetTextVariable("SUCCESS_CHANCE", PersuasionHelper.ShowSuccess(this._task.Options.ElementAt(0), false));
					textObject.SetTextVariable("PERSUASION_OPTION_LINE", this._task.Options.ElementAt(0).Line);
					MBTextManager.SetTextVariable("DAUGHTER_PERSUADE_TO_COME_PERSUADE_ATTEMPT_1", textObject, false);
					return true;
				}
				return false;
			}

			// Token: 0x06000D90 RID: 3472 RVA: 0x0005912C File Offset: 0x0005732C
			private bool persuasion_select_option_2_on_condition()
			{
				if (this._task.Options.Count > 1)
				{
					TextObject textObject = new TextObject("{=bSo9hKwr}{PERSUASION_OPTION_LINE} {SUCCESS_CHANCE}", null);
					textObject.SetTextVariable("SUCCESS_CHANCE", PersuasionHelper.ShowSuccess(this._task.Options.ElementAt(1), false));
					textObject.SetTextVariable("PERSUASION_OPTION_LINE", this._task.Options.ElementAt(1).Line);
					MBTextManager.SetTextVariable("DAUGHTER_PERSUADE_TO_COME_PERSUADE_ATTEMPT_2", textObject, false);
					return true;
				}
				return false;
			}

			// Token: 0x06000D91 RID: 3473 RVA: 0x000591AC File Offset: 0x000573AC
			private bool persuasion_select_option_3_on_condition()
			{
				if (this._task.Options.Count > 2)
				{
					TextObject textObject = new TextObject("{=bSo9hKwr}{PERSUASION_OPTION_LINE} {SUCCESS_CHANCE}", null);
					textObject.SetTextVariable("SUCCESS_CHANCE", PersuasionHelper.ShowSuccess(this._task.Options.ElementAt(2), false));
					textObject.SetTextVariable("PERSUASION_OPTION_LINE", this._task.Options.ElementAt(2).Line);
					MBTextManager.SetTextVariable("DAUGHTER_PERSUADE_TO_COME_PERSUADE_ATTEMPT_3", textObject, false);
					return true;
				}
				return false;
			}

			// Token: 0x06000D92 RID: 3474 RVA: 0x0005922C File Offset: 0x0005742C
			private bool persuasion_select_option_4_on_condition()
			{
				if (this._task.Options.Count > 3)
				{
					TextObject textObject = new TextObject("{=bSo9hKwr}{PERSUASION_OPTION_LINE} {SUCCESS_CHANCE}", null);
					textObject.SetTextVariable("SUCCESS_CHANCE", PersuasionHelper.ShowSuccess(this._task.Options.ElementAt(3), false));
					textObject.SetTextVariable("PERSUASION_OPTION_LINE", this._task.Options.ElementAt(3).Line);
					MBTextManager.SetTextVariable("DAUGHTER_PERSUADE_TO_COME_PERSUADE_ATTEMPT_4", textObject, false);
					return true;
				}
				return false;
			}

			// Token: 0x06000D93 RID: 3475 RVA: 0x000592AC File Offset: 0x000574AC
			private void persuasion_select_option_1_on_consequence()
			{
				if (this._task.Options.Count > 0)
				{
					this._task.Options[0].BlockTheOption(true);
				}
			}

			// Token: 0x06000D94 RID: 3476 RVA: 0x000592D8 File Offset: 0x000574D8
			private void persuasion_select_option_2_on_consequence()
			{
				if (this._task.Options.Count > 1)
				{
					this._task.Options[1].BlockTheOption(true);
				}
			}

			// Token: 0x06000D95 RID: 3477 RVA: 0x00059304 File Offset: 0x00057504
			private void persuasion_select_option_3_on_consequence()
			{
				if (this._task.Options.Count > 2)
				{
					this._task.Options[2].BlockTheOption(true);
				}
			}

			// Token: 0x06000D96 RID: 3478 RVA: 0x00059330 File Offset: 0x00057530
			private void persuasion_select_option_4_on_consequence()
			{
				if (this._task.Options.Count > 3)
				{
					this._task.Options[3].BlockTheOption(true);
				}
			}

			// Token: 0x06000D97 RID: 3479 RVA: 0x0005935C File Offset: 0x0005755C
			private PersuasionOptionArgs persuasion_setup_option_1()
			{
				return this._task.Options.ElementAt(0);
			}

			// Token: 0x06000D98 RID: 3480 RVA: 0x0005936F File Offset: 0x0005756F
			private PersuasionOptionArgs persuasion_setup_option_2()
			{
				return this._task.Options.ElementAt(1);
			}

			// Token: 0x06000D99 RID: 3481 RVA: 0x00059382 File Offset: 0x00057582
			private PersuasionOptionArgs persuasion_setup_option_3()
			{
				return this._task.Options.ElementAt(2);
			}

			// Token: 0x06000D9A RID: 3482 RVA: 0x00059395 File Offset: 0x00057595
			private PersuasionOptionArgs persuasion_setup_option_4()
			{
				return this._task.Options.ElementAt(3);
			}

			// Token: 0x06000D9B RID: 3483 RVA: 0x000593A8 File Offset: 0x000575A8
			private bool persuasion_clickable_option_1_on_condition(out TextObject hintText)
			{
				hintText = new TextObject("{=9ACJsI6S}Blocked", null);
				if (this._task.Options.Count > 0)
				{
					hintText = (this._task.Options.ElementAt(0).IsBlocked ? hintText : TextObject.Empty);
					return !this._task.Options.ElementAt(0).IsBlocked;
				}
				return false;
			}

			// Token: 0x06000D9C RID: 3484 RVA: 0x00059414 File Offset: 0x00057614
			private bool persuasion_clickable_option_2_on_condition(out TextObject hintText)
			{
				hintText = new TextObject("{=9ACJsI6S}Blocked", null);
				if (this._task.Options.Count > 1)
				{
					hintText = (this._task.Options.ElementAt(1).IsBlocked ? hintText : TextObject.Empty);
					return !this._task.Options.ElementAt(1).IsBlocked;
				}
				return false;
			}

			// Token: 0x06000D9D RID: 3485 RVA: 0x00059480 File Offset: 0x00057680
			private bool persuasion_clickable_option_3_on_condition(out TextObject hintText)
			{
				hintText = new TextObject("{=9ACJsI6S}Blocked", null);
				if (this._task.Options.Count > 2)
				{
					hintText = (this._task.Options.ElementAt(2).IsBlocked ? hintText : TextObject.Empty);
					return !this._task.Options.ElementAt(2).IsBlocked;
				}
				return false;
			}

			// Token: 0x06000D9E RID: 3486 RVA: 0x000594EC File Offset: 0x000576EC
			private bool persuasion_clickable_option_4_on_condition(out TextObject hintText)
			{
				hintText = new TextObject("{=9ACJsI6S}Blocked", null);
				if (this._task.Options.Count > 3)
				{
					hintText = (this._task.Options.ElementAt(3).IsBlocked ? hintText : TextObject.Empty);
					return !this._task.Options.ElementAt(3).IsBlocked;
				}
				return false;
			}

			// Token: 0x06000D9F RID: 3487 RVA: 0x00059558 File Offset: 0x00057758
			private PersuasionTask GetPersuasionTask()
			{
				PersuasionTask persuasionTask = new PersuasionTask(0);
				persuasionTask.FinalFailLine = new TextObject("{=5aDlmdmb}No... No. It does not make sense.", null);
				persuasionTask.TryLaterLine = TextObject.Empty;
				persuasionTask.SpokenLine = new TextObject("{=6P1ruzsC}Maybe...", null);
				PersuasionOptionArgs option = new PersuasionOptionArgs(DefaultSkills.Leadership, DefaultTraits.Honor, TraitEffect.Positive, PersuasionArgumentStrength.Hard, true, new TextObject("{=Nhfl6tcM}Maybe, but that is your duty to your family.", null), null, false, false, false);
				persuasionTask.AddOptionToTask(option);
				TextObject textObject = new TextObject("{=lustkZ7s}Perhaps {?QUEST_GIVER.GENDER}she{?}he{\\?} made those plans because {?QUEST_GIVER.GENDER}she{?}he{\\?} loves you.", null);
				StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
				PersuasionOptionArgs option2 = new PersuasionOptionArgs(DefaultSkills.Charm, DefaultTraits.Mercy, TraitEffect.Positive, PersuasionArgumentStrength.VeryEasy, false, textObject, null, false, false, false);
				persuasionTask.AddOptionToTask(option2);
				PersuasionOptionArgs option3 = new PersuasionOptionArgs(DefaultSkills.Roguery, DefaultTraits.Calculating, TraitEffect.Positive, PersuasionArgumentStrength.VeryHard, false, new TextObject("{=Ns6Svjsn}Do you think this one will be faithful to you over many years? I know a rogue when I see one.", null), null, false, false, false);
				persuasionTask.AddOptionToTask(option3);
				PersuasionOptionArgs option4 = new PersuasionOptionArgs(DefaultSkills.Roguery, DefaultTraits.Mercy, TraitEffect.Negative, PersuasionArgumentStrength.ExtremelyHard, true, new TextObject("{=2dL6j8Hp}You want to marry a corpse? Because I'm going to kill your lover if you don't listen.", null), null, true, false, false);
				persuasionTask.AddOptionToTask(option4);
				return persuasionTask;
			}

			// Token: 0x06000DA0 RID: 3488 RVA: 0x0005965A File Offset: 0x0005785A
			private void persuasion_start_with_daughter_on_consequence()
			{
				ConversationManager.StartPersuasion(2f, 1f, 0f, 2f, 2f, 0f, PersuasionDifficulty.Hard);
			}

			// Token: 0x06000DA1 RID: 3489 RVA: 0x00059680 File Offset: 0x00057880
			private void daughter_persuade_to_come_persuasion_success_on_consequence()
			{
				ConversationManager.EndPersuasion();
				this._isDaughterPersuaded = true;
			}

			// Token: 0x06000DA2 RID: 3490 RVA: 0x00059690 File Offset: 0x00057890
			private bool daughter_persuade_to_come_persuasion_failed_on_condition()
			{
				if (this._task.Options.All((PersuasionOptionArgs x) => x.IsBlocked) && !ConversationManager.GetPersuasionProgressSatisfied())
				{
					MBTextManager.SetTextVariable("FAILED_PERSUASION_LINE", this._task.FinalFailLine, false);
					return true;
				}
				return false;
			}

			// Token: 0x06000DA3 RID: 3491 RVA: 0x000596EE File Offset: 0x000578EE
			private void daughter_persuade_to_come_persuasion_failed_on_consequence()
			{
				ConversationManager.EndPersuasion();
			}

			// Token: 0x06000DA4 RID: 3492 RVA: 0x000596F8 File Offset: 0x000578F8
			private void OnSettlementLeft(MobileParty party, Settlement settlement)
			{
				if (party.IsMainParty && settlement == base.QuestGiver.CurrentSettlement && this._exitedQuestSettlementForTheFirstTime)
				{
					if (this.DoesMainPartyHasEnoughScoutingSkill)
					{
						QuestHelper.AddMapArrowFromPointToTarget(new TextObject("{=YdwLnWa1}Direction of daughter and rogue", null), settlement.Position2D, this._targetVillage.Settlement.Position2D, 5f, 0.1f);
						MBInformationManager.AddQuickInformation(new TextObject("{=O15PyNUK}With the help of your scouting skill, you were able to trace their tracks.", null), 0, null, "");
						MBInformationManager.AddQuickInformation(new TextObject("{=gOWebWiK}Their direction is marked with an arrow in the campaign map.", null), 0, null, "");
						base.AddTrackedObject(this._targetVillage.Settlement);
					}
					else
					{
						foreach (Village village in base.QuestGiver.CurrentSettlement.Village.Bound.BoundVillages)
						{
							if (village != base.QuestGiver.CurrentSettlement.Village)
							{
								this._villagesAndAlreadyVisitedBooleans.Add(village, false);
								base.AddTrackedObject(village.Settlement);
							}
						}
					}
					TextObject textObject = new TextObject("{=FvtAJE2Q}In order to find {QUEST_GIVER.LINK}'s daughter, you have decided to visit nearby villages.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					base.AddLog(textObject, this.DoesMainPartyHasEnoughScoutingSkill);
					this._exitedQuestSettlementForTheFirstTime = false;
				}
				if (party.IsMainParty && settlement == this._targetVillage.Settlement)
				{
					this._isQuestTargetMission = false;
				}
			}

			// Token: 0x06000DA5 RID: 3493 RVA: 0x0005987C File Offset: 0x00057A7C
			public void OnBeforeMissionOpened()
			{
				if (this._isQuestTargetMission)
				{
					Location locationWithId = Settlement.CurrentSettlement.LocationComplex.GetLocationWithId("village_center");
					if (locationWithId != null)
					{
						this.HandleRogueEquipment();
						locationWithId.AddCharacter(this.CreateQuestLocationCharacter(this._daughterHero.CharacterObject, LocationCharacter.CharacterRelations.Neutral));
						locationWithId.AddCharacter(this.CreateQuestLocationCharacter(this._rogueHero.CharacterObject, LocationCharacter.CharacterRelations.Neutral));
					}
				}
			}

			// Token: 0x06000DA6 RID: 3494 RVA: 0x000598E0 File Offset: 0x00057AE0
			private void HandleRogueEquipment()
			{
				ItemObject @object = MBObjectManager.Instance.GetObject<ItemObject>("short_sword_t3");
				this._rogueHero.CivilianEquipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.WeaponItemBeginSlot, new EquipmentElement(@object, null, null, false));
				for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.NumAllWeaponSlots; equipmentIndex++)
				{
					ItemObject item = this._rogueHero.BattleEquipment[equipmentIndex].Item;
					if (item != null && item.WeaponComponent.PrimaryWeapon.IsShield)
					{
						this._rogueHero.BattleEquipment.AddEquipmentToSlotWithoutAgent(equipmentIndex, default(EquipmentElement));
					}
				}
			}

			// Token: 0x06000DA7 RID: 3495 RVA: 0x0005996C File Offset: 0x00057B6C
			private void OnMissionEnded(IMission mission)
			{
				if (this._isQuestTargetMission)
				{
					this._daughterAgent = null;
					this._rogueAgent = null;
					if (this._isDaughterPersuaded)
					{
						this.ApplyDeliverySuccessConsequences();
						base.CompleteQuestWithSuccess();
						base.AddLog(this._successQuestLogText, false);
						this.RemoveQuestCharacters();
						return;
					}
					if (this._acceptedDaughtersEscape)
					{
						this.ApplyDaughtersEscapeAcceptedFailConsequences();
						base.CompleteQuestWithFail(this._failQuestLogText);
						this.RemoveQuestCharacters();
						return;
					}
					if (this._isDaughterCaptured)
					{
						this.ApplyDeliverySuccessConsequences();
						base.CompleteQuestWithSuccess();
						base.AddLog(this._successQuestLogText, false);
						this.RemoveQuestCharacters();
						return;
					}
					if (this._playerDefeatedByRogue)
					{
						base.CompleteQuestWithFail(null);
						base.AddLog(this._playerDefeatedByRogueLogText, false);
						this.RemoveQuestCharacters();
					}
				}
			}

			// Token: 0x06000DA8 RID: 3496 RVA: 0x00059A28 File Offset: 0x00057C28
			private LocationCharacter CreateQuestLocationCharacter(CharacterObject character, LocationCharacter.CharacterRelations relation)
			{
				Monster monsterWithSuffix = TaleWorlds.Core.FaceGen.GetMonsterWithSuffix(character.Race, "_settlement");
				Tuple<string, Monster> tuple = new Tuple<string, Monster>(ActionSetCode.GenerateActionSetNameWithSuffix(monsterWithSuffix, character.IsFemale, "_villager"), monsterWithSuffix);
				return new LocationCharacter(new AgentData(new SimpleAgentOrigin(character, -1, null, default(UniqueTroopDescriptor))).Monster(tuple.Item2), new LocationCharacter.AddBehaviorsDelegate(SandBoxManager.Instance.AgentBehaviorManager.AddCompanionBehaviors), "alley_2", true, relation, tuple.Item1, false, false, null, false, true, true);
			}

			// Token: 0x06000DA9 RID: 3497 RVA: 0x00059AAD File Offset: 0x00057CAD
			private void RemoveQuestCharacters()
			{
				Settlement.CurrentSettlement.LocationComplex.RemoveCharacterIfExists(this._daughterHero);
				Settlement.CurrentSettlement.LocationComplex.RemoveCharacterIfExists(this._rogueHero);
			}

			// Token: 0x06000DAA RID: 3498 RVA: 0x00059ADC File Offset: 0x00057CDC
			private void OnSettlementEntered(MobileParty party, Settlement settlement, Hero hero)
			{
				if (party != null && party.IsMainParty && settlement.IsVillage)
				{
					if (this._villagesAndAlreadyVisitedBooleans.ContainsKey(settlement.Village) && !this._villagesAndAlreadyVisitedBooleans[settlement.Village])
					{
						if (settlement.Village != this._targetVillage)
						{
							TextObject textObject = settlement.IsRaided ? new TextObject("{=YTaM6G1E}It seems the village has been raided a short while ago. You found nothing but smoke, fire and crying people.", null) : new TextObject("{=2P3UJ8be}You ask around the village if anyone saw {TARGET_HERO.NAME} or some suspicious characters with a young woman.\n\nVillagers say that they saw a young man and woman ride in early in the morning. They bought some supplies and trotted off towards {TARGET_VILLAGE}.", null);
							textObject.SetTextVariable("TARGET_VILLAGE", this._targetVillage.Name);
							StringHelpers.SetCharacterProperties("TARGET_HERO", this._daughterHero.CharacterObject, textObject, false);
							InformationManager.ShowInquiry(new InquiryData(this.Title.ToString(), textObject.ToString(), true, false, new TextObject("{=yS7PvrTD}OK", null).ToString(), "", null, null, "", 0f, null, null, null), false, false);
							if (!this._isTrackerLogAdded)
							{
								TextObject textObject2 = new TextObject("{=WGi3Zuv7}You asked the villagers around {CURRENT_SETTLEMENT} if they saw a young woman matching the description of {QUEST_GIVER.LINK}'s daughter, {TARGET_HERO.NAME}.\n\nThey said a young woman and a young man dropped by early in the morning to buy some supplies and then rode off towards {TARGET_VILLAGE}.", null);
								textObject2.SetTextVariable("CURRENT_SETTLEMENT", Hero.MainHero.CurrentSettlement.Name);
								textObject2.SetTextVariable("TARGET_VILLAGE", this._targetVillage.Settlement.EncyclopediaLinkWithName);
								StringHelpers.SetCharacterProperties("TARGET_HERO", this._daughterHero.CharacterObject, textObject2, false);
								StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject2, false);
								base.AddLog(textObject2, false);
								this._isTrackerLogAdded = true;
							}
						}
						else
						{
							InquiryData data;
							if (settlement.IsRaided)
							{
								TextObject textObject3 = new TextObject("{=edoXFdmg}You have found {QUEST_GIVER.NAME}'s daughter.", null);
								StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject3, false);
								TextObject textObject4 = new TextObject("{=aYMW8bWi}Talk to her", null);
								data = new InquiryData(this.Title.ToString(), textObject3.ToString(), true, false, textObject4.ToString(), null, new Action(this.TalkWithDaughterAfterRaid), null, "", 0f, null, null, null);
							}
							else
							{
								TextObject textObject5 = new TextObject("{=bbwNIIKI}You ask around the village if anyone saw {TARGET_HERO.NAME} or some suspicious characters with a young woman.\n\nVillagers say that there was a young man and woman who arrived here exhausted. The villagers allowed them to stay for a while.\nYou can check the area, and see if they are still hiding here.", null);
								StringHelpers.SetCharacterProperties("TARGET_HERO", this._daughterHero.CharacterObject, textObject5, false);
								data = new InquiryData(this.Title.ToString(), textObject5.ToString(), true, true, new TextObject("{=bb6e8DoM}Search the village", null).ToString(), new TextObject("{=3CpNUnVl}Cancel", null).ToString(), new Action(this.SearchTheVillage), null, "", 0f, null, null, null);
							}
							InformationManager.ShowInquiry(data, false, false);
						}
						this._villagesAndAlreadyVisitedBooleans[settlement.Village] = true;
					}
					if (settlement == this._targetVillage.Settlement)
					{
						if (!base.IsTracked(this._daughterHero))
						{
							base.AddTrackedObject(this._daughterHero);
						}
						if (!base.IsTracked(this._rogueHero))
						{
							base.AddTrackedObject(this._rogueHero);
						}
						this._isQuestTargetMission = true;
					}
				}
			}

			// Token: 0x06000DAB RID: 3499 RVA: 0x00059DBD File Offset: 0x00057FBD
			private void SearchTheVillage()
			{
				VillageEncounter villageEncounter = PlayerEncounter.LocationEncounter as VillageEncounter;
				if (villageEncounter == null)
				{
					return;
				}
				villageEncounter.CreateAndOpenMissionController(LocationComplex.Current.GetLocationWithId("village_center"), null, null, null);
			}

			// Token: 0x06000DAC RID: 3500 RVA: 0x00059DE8 File Offset: 0x00057FE8
			private void TalkWithDaughterAfterRaid()
			{
				this._villageIsRaidedTalkWithDaughter = true;
				CampaignMapConversation.OpenConversation(new ConversationCharacterData(CharacterObject.PlayerCharacter, null, false, false, false, false, false, false), new ConversationCharacterData(this._daughterHero.CharacterObject, null, false, false, false, false, false, false));
			}

			// Token: 0x06000DAD RID: 3501 RVA: 0x00059E29 File Offset: 0x00058029
			private void QuestAcceptedConsequences()
			{
				base.StartQuest();
				base.AddLog(this._playerStartsQuestLogText, false);
			}

			// Token: 0x06000DAE RID: 3502 RVA: 0x00059E3F File Offset: 0x0005803F
			private void CanHeroDie(Hero victim, KillCharacterAction.KillCharacterActionDetail detail, ref bool result)
			{
				if (victim == Hero.MainHero && Settlement.CurrentSettlement == this._targetVillage.Settlement && Mission.Current != null)
				{
					result = false;
				}
			}

			// Token: 0x06000DAF RID: 3503 RVA: 0x00059E68 File Offset: 0x00058068
			protected override void RegisterEvents()
			{
				CampaignEvents.OnSettlementLeftEvent.AddNonSerializedListener(this, new Action<MobileParty, Settlement>(this.OnSettlementLeft));
				CampaignEvents.SettlementEntered.AddNonSerializedListener(this, new Action<MobileParty, Settlement, Hero>(this.OnSettlementEntered));
				CampaignEvents.BeforeMissionOpenedEvent.AddNonSerializedListener(this, new Action(this.OnBeforeMissionOpened));
				CampaignEvents.OnMissionEndedEvent.AddNonSerializedListener(this, new Action<IMission>(this.OnMissionEnded));
				CampaignEvents.WarDeclared.AddNonSerializedListener(this, new Action<IFaction, IFaction, DeclareWarAction.DeclareWarDetail>(this.OnWarDeclared));
				CampaignEvents.CanHeroDieEvent.AddNonSerializedListener(this, new ReferenceAction<Hero, KillCharacterAction.KillCharacterActionDetail, bool>(this.CanHeroDie));
				CampaignEvents.OnClanChangedKingdomEvent.AddNonSerializedListener(this, new Action<Clan, Kingdom, Kingdom, ChangeKingdomAction.ChangeKingdomActionDetail, bool>(this.OnClanChangedKingdom));
				CampaignEvents.MapEventStarted.AddNonSerializedListener(this, new Action<MapEvent, PartyBase, PartyBase>(this.OnMapEventStarted));
				CampaignEvents.RaidCompletedEvent.AddNonSerializedListener(this, new Action<BattleSideEnum, RaidEventComponent>(this.OnRaidCompleted));
			}

			// Token: 0x06000DB0 RID: 3504 RVA: 0x00059F44 File Offset: 0x00058144
			private void OnRaidCompleted(BattleSideEnum side, RaidEventComponent raidEventComponent)
			{
				if (raidEventComponent.MapEventSettlement == base.QuestGiver.CurrentSettlement)
				{
					base.CompleteQuestWithCancel(this._villageRaidedCancelQuestLogText);
				}
			}

			// Token: 0x06000DB1 RID: 3505 RVA: 0x00059F65 File Offset: 0x00058165
			public override void OnHeroCanHaveQuestOrIssueInfoIsRequested(Hero hero, ref bool result)
			{
				if (hero == this._rogueHero || hero == this._daughterHero)
				{
					result = false;
				}
			}

			// Token: 0x06000DB2 RID: 3506 RVA: 0x00059F7C File Offset: 0x0005817C
			public override void OnHeroCanMoveToSettlementInfoIsRequested(Hero hero, ref bool result)
			{
				if (hero == this._rogueHero || hero == this._daughterHero)
				{
					result = false;
				}
			}

			// Token: 0x06000DB3 RID: 3507 RVA: 0x00059F93 File Offset: 0x00058193
			private void OnMapEventStarted(MapEvent mapEvent, PartyBase attackerParty, PartyBase defenderParty)
			{
				if (QuestHelper.CheckMinorMajorCoercion(this, mapEvent, attackerParty))
				{
					QuestHelper.ApplyGenericMinorMajorCoercionConsequences(this, mapEvent);
				}
			}

			// Token: 0x06000DB4 RID: 3508 RVA: 0x00059FA6 File Offset: 0x000581A6
			private void OnClanChangedKingdom(Clan clan, Kingdom oldKingdom, Kingdom newKingdom, ChangeKingdomAction.ChangeKingdomActionDetail detail, bool showNotification = true)
			{
				if (base.QuestGiver.CurrentSettlement.MapFaction.IsAtWarWith(Hero.MainHero.MapFaction))
				{
					base.CompleteQuestWithCancel(this._questCanceledWarDeclaredLog);
				}
			}

			// Token: 0x06000DB5 RID: 3509 RVA: 0x00059FD5 File Offset: 0x000581D5
			private void OnWarDeclared(IFaction faction1, IFaction faction2, DeclareWarAction.DeclareWarDetail detail)
			{
				QuestHelper.CheckWarDeclarationAndFailOrCancelTheQuest(this, faction1, faction2, detail, this._playerDeclaredWarQuestLogText, this._questCanceledWarDeclaredLog, false);
			}

			// Token: 0x06000DB6 RID: 3510 RVA: 0x00059FF0 File Offset: 0x000581F0
			protected override void OnFinalize()
			{
				if (base.IsTracked(this._targetVillage.Settlement))
				{
					base.RemoveTrackedObject(this._targetVillage.Settlement);
				}
				if (!Hero.MainHero.IsPrisoner && !this.DoesMainPartyHasEnoughScoutingSkill)
				{
					foreach (Village village in base.QuestGiver.CurrentSettlement.BoundVillages)
					{
						if (base.IsTracked(village.Settlement))
						{
							base.RemoveTrackedObject(village.Settlement);
						}
					}
				}
				if (this._rogueHero != null && this._rogueHero.IsAlive)
				{
					KillCharacterAction.ApplyByRemove(this._rogueHero, false, true);
				}
				if (this._daughterHero != null && this._daughterHero.IsAlive)
				{
					KillCharacterAction.ApplyByRemove(this._daughterHero, false, true);
				}
			}

			// Token: 0x06000DB7 RID: 3511 RVA: 0x0005A0DC File Offset: 0x000582DC
			internal static void AutoGeneratedStaticCollectObjectsNotableWantsDaughterFoundIssueQuest(object o, List<object> collectedObjects)
			{
				((NotableWantsDaughterFoundIssueBehavior.NotableWantsDaughterFoundIssueQuest)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
			}

			// Token: 0x06000DB8 RID: 3512 RVA: 0x0005A0EA File Offset: 0x000582EA
			protected override void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
			{
				base.AutoGeneratedInstanceCollectObjects(collectedObjects);
				collectedObjects.Add(this._daughterHero);
				collectedObjects.Add(this._rogueHero);
				collectedObjects.Add(this._targetVillage);
				collectedObjects.Add(this._villagesAndAlreadyVisitedBooleans);
			}

			// Token: 0x06000DB9 RID: 3513 RVA: 0x0005A123 File Offset: 0x00058323
			internal static object AutoGeneratedGetMemberValue_daughterHero(object o)
			{
				return ((NotableWantsDaughterFoundIssueBehavior.NotableWantsDaughterFoundIssueQuest)o)._daughterHero;
			}

			// Token: 0x06000DBA RID: 3514 RVA: 0x0005A130 File Offset: 0x00058330
			internal static object AutoGeneratedGetMemberValue_rogueHero(object o)
			{
				return ((NotableWantsDaughterFoundIssueBehavior.NotableWantsDaughterFoundIssueQuest)o)._rogueHero;
			}

			// Token: 0x06000DBB RID: 3515 RVA: 0x0005A13D File Offset: 0x0005833D
			internal static object AutoGeneratedGetMemberValue_isQuestTargetMission(object o)
			{
				return ((NotableWantsDaughterFoundIssueBehavior.NotableWantsDaughterFoundIssueQuest)o)._isQuestTargetMission;
			}

			// Token: 0x06000DBC RID: 3516 RVA: 0x0005A14F File Offset: 0x0005834F
			internal static object AutoGeneratedGetMemberValue_didPlayerBeatRouge(object o)
			{
				return ((NotableWantsDaughterFoundIssueBehavior.NotableWantsDaughterFoundIssueQuest)o)._didPlayerBeatRouge;
			}

			// Token: 0x06000DBD RID: 3517 RVA: 0x0005A161 File Offset: 0x00058361
			internal static object AutoGeneratedGetMemberValue_exitedQuestSettlementForTheFirstTime(object o)
			{
				return ((NotableWantsDaughterFoundIssueBehavior.NotableWantsDaughterFoundIssueQuest)o)._exitedQuestSettlementForTheFirstTime;
			}

			// Token: 0x06000DBE RID: 3518 RVA: 0x0005A173 File Offset: 0x00058373
			internal static object AutoGeneratedGetMemberValue_isTrackerLogAdded(object o)
			{
				return ((NotableWantsDaughterFoundIssueBehavior.NotableWantsDaughterFoundIssueQuest)o)._isTrackerLogAdded;
			}

			// Token: 0x06000DBF RID: 3519 RVA: 0x0005A185 File Offset: 0x00058385
			internal static object AutoGeneratedGetMemberValue_isDaughterPersuaded(object o)
			{
				return ((NotableWantsDaughterFoundIssueBehavior.NotableWantsDaughterFoundIssueQuest)o)._isDaughterPersuaded;
			}

			// Token: 0x06000DC0 RID: 3520 RVA: 0x0005A197 File Offset: 0x00058397
			internal static object AutoGeneratedGetMemberValue_isDaughterCaptured(object o)
			{
				return ((NotableWantsDaughterFoundIssueBehavior.NotableWantsDaughterFoundIssueQuest)o)._isDaughterCaptured;
			}

			// Token: 0x06000DC1 RID: 3521 RVA: 0x0005A1A9 File Offset: 0x000583A9
			internal static object AutoGeneratedGetMemberValue_acceptedDaughtersEscape(object o)
			{
				return ((NotableWantsDaughterFoundIssueBehavior.NotableWantsDaughterFoundIssueQuest)o)._acceptedDaughtersEscape;
			}

			// Token: 0x06000DC2 RID: 3522 RVA: 0x0005A1BB File Offset: 0x000583BB
			internal static object AutoGeneratedGetMemberValue_targetVillage(object o)
			{
				return ((NotableWantsDaughterFoundIssueBehavior.NotableWantsDaughterFoundIssueQuest)o)._targetVillage;
			}

			// Token: 0x06000DC3 RID: 3523 RVA: 0x0005A1C8 File Offset: 0x000583C8
			internal static object AutoGeneratedGetMemberValue_villageIsRaidedTalkWithDaughter(object o)
			{
				return ((NotableWantsDaughterFoundIssueBehavior.NotableWantsDaughterFoundIssueQuest)o)._villageIsRaidedTalkWithDaughter;
			}

			// Token: 0x06000DC4 RID: 3524 RVA: 0x0005A1DA File Offset: 0x000583DA
			internal static object AutoGeneratedGetMemberValue_villagesAndAlreadyVisitedBooleans(object o)
			{
				return ((NotableWantsDaughterFoundIssueBehavior.NotableWantsDaughterFoundIssueQuest)o)._villagesAndAlreadyVisitedBooleans;
			}

			// Token: 0x06000DC5 RID: 3525 RVA: 0x0005A1E7 File Offset: 0x000583E7
			internal static object AutoGeneratedGetMemberValue_questDifficultyMultiplier(object o)
			{
				return ((NotableWantsDaughterFoundIssueBehavior.NotableWantsDaughterFoundIssueQuest)o)._questDifficultyMultiplier;
			}

			// Token: 0x040005F4 RID: 1524
			[SaveableField(10)]
			private readonly Hero _daughterHero;

			// Token: 0x040005F5 RID: 1525
			[SaveableField(20)]
			private readonly Hero _rogueHero;

			// Token: 0x040005F6 RID: 1526
			private Agent _daughterAgent;

			// Token: 0x040005F7 RID: 1527
			private Agent _rogueAgent;

			// Token: 0x040005F8 RID: 1528
			[SaveableField(50)]
			private bool _isQuestTargetMission;

			// Token: 0x040005F9 RID: 1529
			[SaveableField(60)]
			private bool _didPlayerBeatRouge;

			// Token: 0x040005FA RID: 1530
			[SaveableField(70)]
			private bool _exitedQuestSettlementForTheFirstTime = true;

			// Token: 0x040005FB RID: 1531
			[SaveableField(80)]
			private bool _isTrackerLogAdded;

			// Token: 0x040005FC RID: 1532
			[SaveableField(90)]
			private bool _isDaughterPersuaded;

			// Token: 0x040005FD RID: 1533
			[SaveableField(91)]
			private bool _isDaughterCaptured;

			// Token: 0x040005FE RID: 1534
			[SaveableField(100)]
			private bool _acceptedDaughtersEscape;

			// Token: 0x040005FF RID: 1535
			[SaveableField(110)]
			private readonly Village _targetVillage;

			// Token: 0x04000600 RID: 1536
			[SaveableField(120)]
			private bool _villageIsRaidedTalkWithDaughter;

			// Token: 0x04000601 RID: 1537
			[SaveableField(140)]
			private Dictionary<Village, bool> _villagesAndAlreadyVisitedBooleans = new Dictionary<Village, bool>();

			// Token: 0x04000602 RID: 1538
			private Dictionary<string, CharacterObject> _rogueCharacterBasedOnCulture = new Dictionary<string, CharacterObject>();

			// Token: 0x04000603 RID: 1539
			private bool _playerDefeatedByRogue;

			// Token: 0x04000604 RID: 1540
			private PersuasionTask _task;

			// Token: 0x04000605 RID: 1541
			private const PersuasionDifficulty Difficulty = PersuasionDifficulty.Hard;

			// Token: 0x04000606 RID: 1542
			private const int MaxAgeForDaughterAndRogue = 25;

			// Token: 0x04000607 RID: 1543
			[SaveableField(130)]
			private readonly float _questDifficultyMultiplier;
		}
	}
}
