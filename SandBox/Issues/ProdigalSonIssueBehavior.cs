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
using TaleWorlds.CampaignSystem.Map;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.LinQuick;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.ObjectSystem;
using TaleWorlds.SaveSystem;

namespace SandBox.Issues
{
	// Token: 0x0200008A RID: 138
	public class ProdigalSonIssueBehavior : CampaignBehaviorBase
	{
		// Token: 0x06000559 RID: 1369 RVA: 0x00024281 File Offset: 0x00022481
		public override void RegisterEvents()
		{
			CampaignEvents.OnCheckForIssueEvent.AddNonSerializedListener(this, new Action<Hero>(this.CheckForIssue));
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0002429A File Offset: 0x0002249A
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0002429C File Offset: 0x0002249C
		public void CheckForIssue(Hero hero)
		{
			Hero item;
			Hero item2;
			if (this.ConditionsHold(hero, out item, out item2))
			{
				Campaign.Current.IssueManager.AddPotentialIssueData(hero, new PotentialIssueData(new PotentialIssueData.StartIssueDelegate(this.OnStartIssue), typeof(ProdigalSonIssueBehavior.ProdigalSonIssue), IssueBase.IssueFrequency.Rare, new Tuple<Hero, Hero>(item, item2)));
				return;
			}
			Campaign.Current.IssueManager.AddPotentialIssueData(hero, new PotentialIssueData(typeof(ProdigalSonIssueBehavior.ProdigalSonIssue), IssueBase.IssueFrequency.Rare));
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0002430C File Offset: 0x0002250C
		private bool ConditionsHoldForSettlement(Settlement settlement, Hero issueGiver)
		{
			if (settlement.IsTown && settlement != issueGiver.CurrentSettlement && settlement.OwnerClan != issueGiver.Clan && settlement.OwnerClan != Clan.PlayerClan)
			{
				if (settlement.HeroesWithoutParty.FirstOrDefault((Hero x) => x.CanHaveQuestsOrIssues() && x.IsGangLeader) != null)
				{
					return settlement.LocationComplex.GetListOfLocations().AnyQ((Location x) => x.CanBeReserved && !x.IsReserved);
				}
			}
			return false;
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x000243A8 File Offset: 0x000225A8
		private bool ConditionsHold(Hero issueGiver, out Hero selectedHero, out Hero targetHero)
		{
			selectedHero = null;
			targetHero = null;
			if (issueGiver.IsLord && !issueGiver.IsPrisoner && issueGiver.Clan != Clan.PlayerClan && issueGiver.Age > 30f && issueGiver.GetTraitLevel(DefaultTraits.Mercy) <= 0 && (issueGiver.CurrentSettlement != null || issueGiver.PartyBelongedTo != null))
			{
				selectedHero = issueGiver.Clan.Lords.GetRandomElementWithPredicate((Hero x) => x.IsActive && !x.IsFemale && x.Age < 35f && (int)x.Age + 10 <= (int)issueGiver.Age && !x.IsPrisoner && x.CanHaveQuestsOrIssues() && x.PartyBelongedTo == null && x.CurrentSettlement != null && x.GovernorOf == null && x.GetTraitLevel(DefaultTraits.Honor) + x.GetTraitLevel(DefaultTraits.Calculating) < 0);
				if (selectedHero != null)
				{
					IMapPoint currentSettlement = issueGiver.CurrentSettlement;
					IMapPoint mapPoint = currentSettlement ?? issueGiver.PartyBelongedTo;
					int num = 0;
					int num2 = -1;
					do
					{
						num2 = SettlementHelper.FindNextSettlementAroundMapPoint(mapPoint, 150f, num2);
						if (num2 >= 0 && this.ConditionsHoldForSettlement(Settlement.All[num2], issueGiver))
						{
							num++;
						}
					}
					while (num2 >= 0);
					if (num > 0)
					{
						int num3 = MBRandom.RandomInt(num);
						num2 = -1;
						for (;;)
						{
							num2 = SettlementHelper.FindNextSettlementAroundMapPoint(mapPoint, 150f, num2);
							if (num2 >= 0 && this.ConditionsHoldForSettlement(Settlement.All[num2], issueGiver))
							{
								num3--;
								if (num3 < 0)
								{
									break;
								}
							}
							if (num2 < 0)
							{
								goto IL_18E;
							}
						}
						targetHero = Settlement.All[num2].HeroesWithoutParty.FirstOrDefault((Hero x) => x.CanHaveQuestsOrIssues() && x.IsGangLeader);
					}
				}
			}
			IL_18E:
			return selectedHero != null && targetHero != null;
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00024550 File Offset: 0x00022750
		private IssueBase OnStartIssue(in PotentialIssueData pid, Hero issueOwner)
		{
			PotentialIssueData potentialIssueData = pid;
			Tuple<Hero, Hero> tuple = potentialIssueData.RelatedObject as Tuple<Hero, Hero>;
			return new ProdigalSonIssueBehavior.ProdigalSonIssue(issueOwner, tuple.Item1, tuple.Item2);
		}

		// Token: 0x0400028E RID: 654
		private const IssueBase.IssueFrequency ProdigalSonIssueFrequency = IssueBase.IssueFrequency.Rare;

		// Token: 0x0400028F RID: 655
		private const int AgeLimitForSon = 35;

		// Token: 0x04000290 RID: 656
		private const int AgeLimitForIssueOwner = 30;

		// Token: 0x04000291 RID: 657
		private const int MinimumAgeDifference = 10;

		// Token: 0x02000163 RID: 355
		public class ProdigalSonIssueTypeDefiner : SaveableTypeDefiner
		{
			// Token: 0x06000DD4 RID: 3540 RVA: 0x0005A333 File Offset: 0x00058533
			public ProdigalSonIssueTypeDefiner() : base(345000)
			{
			}

			// Token: 0x06000DD5 RID: 3541 RVA: 0x0005A340 File Offset: 0x00058540
			protected override void DefineClassTypes()
			{
				base.AddClassDefinition(typeof(ProdigalSonIssueBehavior.ProdigalSonIssue), 1, null);
				base.AddClassDefinition(typeof(ProdigalSonIssueBehavior.ProdigalSonIssueQuest), 2, null);
			}
		}

		// Token: 0x02000164 RID: 356
		public class ProdigalSonIssue : IssueBase
		{
			// Token: 0x17000145 RID: 325
			// (get) Token: 0x06000DD6 RID: 3542 RVA: 0x0005A366 File Offset: 0x00058566
			public override IssueBase.AlternativeSolutionScaleFlag AlternativeSolutionScaleFlags
			{
				get
				{
					return IssueBase.AlternativeSolutionScaleFlag.FailureRisk;
				}
			}

			// Token: 0x17000146 RID: 326
			// (get) Token: 0x06000DD7 RID: 3543 RVA: 0x0005A369 File Offset: 0x00058569
			private Clan Clan
			{
				get
				{
					return base.IssueOwner.Clan;
				}
			}

			// Token: 0x17000147 RID: 327
			// (get) Token: 0x06000DD8 RID: 3544 RVA: 0x0005A376 File Offset: 0x00058576
			protected override int RewardGold
			{
				get
				{
					return 1200 + (int)(3000f * base.IssueDifficultyMultiplier);
				}
			}

			// Token: 0x17000148 RID: 328
			// (get) Token: 0x06000DD9 RID: 3545 RVA: 0x0005A38C File Offset: 0x0005858C
			public override TextObject IssueBriefByIssueGiver
			{
				get
				{
					TextObject textObject = new TextObject("{=5a6KlSXt}I have a problem. [ib:normal2][if:convo_pondering]My young kinsman {PRODIGAL_SON.LINK} has gone to town to have fun, drinking, wenching and gambling. Many young men do that, but it seems he was a bit reckless. Now he sends news that he owes a large sum of money to {TARGET_HERO.LINK}, one of the local gang bosses in the city of {SETTLEMENT_LINK}. These ruffians are holding him as a “guest” in their house until someone pays his debt.", null);
					StringHelpers.SetCharacterProperties("PRODIGAL_SON", this._prodigalSon.CharacterObject, textObject, false);
					StringHelpers.SetCharacterProperties("TARGET_HERO", this._targetHero.CharacterObject, textObject, false);
					textObject.SetTextVariable("SETTLEMENT_LINK", this._targetSettlement.EncyclopediaLinkWithName);
					return textObject;
				}
			}

			// Token: 0x17000149 RID: 329
			// (get) Token: 0x06000DDA RID: 3546 RVA: 0x0005A3ED File Offset: 0x000585ED
			public override TextObject IssueAcceptByPlayer
			{
				get
				{
					return new TextObject("{=YtS3cgto}What are you planning to do?", null);
				}
			}

			// Token: 0x1700014A RID: 330
			// (get) Token: 0x06000DDB RID: 3547 RVA: 0x0005A3FA File Offset: 0x000585FA
			public override TextObject IssueQuestSolutionExplanationByIssueGiver
			{
				get
				{
					TextObject textObject = new TextObject("{=ZC1slXw1}I'm not inclined to pay the debt. [ib:closed][if:convo_worried]I'm not going to reward this kind of lawlessness, when even the best families aren't safe. I've sent word to the lord of {SETTLEMENT_NAME} but I can't say I expect to hear back, what with the wars and all. I want someone to go there and free the lad. You could pay, I suppose, but I'd prefer it if you taught those bastards a lesson. I'll pay you either way but obviously you get to keep more if you use force.", null);
					textObject.SetTextVariable("SETTLEMENT_NAME", this._targetSettlement.EncyclopediaLinkWithName);
					return textObject;
				}
			}

			// Token: 0x1700014B RID: 331
			// (get) Token: 0x06000DDC RID: 3548 RVA: 0x0005A41E File Offset: 0x0005861E
			public override TextObject IssuePlayerResponseAfterAlternativeExplanation
			{
				get
				{
					return new TextObject("{=4zf1lg6L}I could go myself, or send a companion.", null);
				}
			}

			// Token: 0x1700014C RID: 332
			// (get) Token: 0x06000DDD RID: 3549 RVA: 0x0005A42B File Offset: 0x0005862B
			public override TextObject IssueAlternativeSolutionExplanationByIssueGiver
			{
				get
				{
					TextObject textObject = new TextObject("{=CWbAoGRu}Yes, I don't care how you solve it. [if:convo_normal]Just solve it any way you like. I reckon {NEEDED_MEN_COUNT} led by someone who knows how to handle thugs could solve this in about {ALTERNATIVE_SOLUTION_DURATION} days. I'd send my own men but it could cause complications for us to go marching in wearing our clan colors in another lord's territory.", null);
					textObject.SetTextVariable("NEEDED_MEN_COUNT", base.GetTotalAlternativeSolutionNeededMenCount());
					textObject.SetTextVariable("ALTERNATIVE_SOLUTION_DURATION", base.GetTotalAlternativeSolutionDurationInDays());
					return textObject;
				}
			}

			// Token: 0x1700014D RID: 333
			// (get) Token: 0x06000DDE RID: 3550 RVA: 0x0005A45C File Offset: 0x0005865C
			public override TextObject IssueQuestSolutionAcceptByPlayer
			{
				get
				{
					return new TextObject("{=aKbyJsho}I will free your kinsman myself.", null);
				}
			}

			// Token: 0x1700014E RID: 334
			// (get) Token: 0x06000DDF RID: 3551 RVA: 0x0005A469 File Offset: 0x00058669
			public override TextObject IssueAlternativeSolutionAcceptByPlayer
			{
				get
				{
					TextObject textObject = new TextObject("{=PuuVGOyM}I will send {NEEDED_MEN_COUNT} of my men with one of my lieutenants for {ALTERNATIVE_SOLUTION_DURATION} days to help you.", null);
					textObject.SetTextVariable("NEEDED_MEN_COUNT", base.GetTotalAlternativeSolutionNeededMenCount());
					textObject.SetTextVariable("ALTERNATIVE_SOLUTION_DURATION", base.GetTotalAlternativeSolutionDurationInDays());
					return textObject;
				}
			}

			// Token: 0x1700014F RID: 335
			// (get) Token: 0x06000DE0 RID: 3552 RVA: 0x0005A49A File Offset: 0x0005869A
			public override TextObject IssueDiscussAlternativeSolution
			{
				get
				{
					return new TextObject("{=qxhMagyZ}I'm glad someone's on it.[if:convo_relaxed_happy] Just see that they do it quickly.", null);
				}
			}

			// Token: 0x17000150 RID: 336
			// (get) Token: 0x06000DE1 RID: 3553 RVA: 0x0005A4A7 File Offset: 0x000586A7
			public override TextObject IssueAlternativeSolutionResponseByIssueGiver
			{
				get
				{
					return new TextObject("{=mDXzDXKY}Very good. [if:convo_relaxed_happy]I'm sure you'll chose competent men to bring our boy back.", null);
				}
			}

			// Token: 0x17000151 RID: 337
			// (get) Token: 0x06000DE2 RID: 3554 RVA: 0x0005A4B4 File Offset: 0x000586B4
			protected override TextObject AlternativeSolutionStartLog
			{
				get
				{
					TextObject textObject = new TextObject("{=Z9sp21rl}{QUEST_GIVER.LINK}, a lord from the {QUEST_GIVER_CLAN} clan, asked you to free {?QUEST_GIVER.GENDER}her{?}his{\\?} relative. The young man is currently held by {TARGET_HERO.LINK} a local gang leader because of his debts. {?QUEST_GIVER.GENDER}Lady{?}Lord{\\?} {QUEST_GIVER.LINK} has given you enough gold to settle {?QUEST_GIVER.GENDER}her{?}his{\\?} debts but {?QUEST_GIVER.GENDER}she{?}he{\\?} encourages you to keep the money to yourself and make an example of these criminals so no one would dare to hold a nobleman again. You have sent {COMPANION.LINK} and {NEEDED_MEN_COUNT} men to take care of the situation for you. They should be back in {ALTERNATIVE_SOLUTION_DURATION} days.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.IssueOwner.CharacterObject, textObject, false);
					StringHelpers.SetCharacterProperties("TARGET_HERO", this._targetHero.CharacterObject, textObject, false);
					StringHelpers.SetCharacterProperties("COMPANION", base.AlternativeSolutionHero.CharacterObject, textObject, false);
					textObject.SetTextVariable("QUEST_GIVER_CLAN", base.IssueOwner.Clan.EncyclopediaLinkWithName);
					textObject.SetTextVariable("SETTLEMENT", this._targetSettlement.EncyclopediaLinkWithName);
					textObject.SetTextVariable("NEEDED_MEN_COUNT", base.GetTotalAlternativeSolutionNeededMenCount());
					textObject.SetTextVariable("ALTERNATIVE_SOLUTION_DURATION", base.GetTotalAlternativeSolutionDurationInDays());
					return textObject;
				}
			}

			// Token: 0x17000152 RID: 338
			// (get) Token: 0x06000DE3 RID: 3555 RVA: 0x0005A570 File Offset: 0x00058770
			public override TextObject IssueAlternativeSolutionSuccessLog
			{
				get
				{
					TextObject textObject = new TextObject("{=IXnvQ8kG}{COMPANION.LINK} and the men you sent with {?COMPANION.GENDER}her{?}him{\\?} safely return with the news of success. {QUEST_GIVER.LINK} is happy and sends you {?QUEST_GIVER.GENDER}her{?}his{\\?} regards with {REWARD}{GOLD_ICON} the money he promised.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.IssueOwner.CharacterObject, textObject, false);
					StringHelpers.SetCharacterProperties("COMPANION", base.AlternativeSolutionHero.CharacterObject, textObject, false);
					textObject.SetTextVariable("REWARD", this.RewardGold);
					return textObject;
				}
			}

			// Token: 0x17000153 RID: 339
			// (get) Token: 0x06000DE4 RID: 3556 RVA: 0x0005A5CC File Offset: 0x000587CC
			public override bool IsThereAlternativeSolution
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000154 RID: 340
			// (get) Token: 0x06000DE5 RID: 3557 RVA: 0x0005A5CF File Offset: 0x000587CF
			public override int AlternativeSolutionBaseNeededMenCount
			{
				get
				{
					return 1 + MathF.Ceiling(3f * base.IssueDifficultyMultiplier);
				}
			}

			// Token: 0x17000155 RID: 341
			// (get) Token: 0x06000DE6 RID: 3558 RVA: 0x0005A5E4 File Offset: 0x000587E4
			protected override int AlternativeSolutionBaseDurationInDaysInternal
			{
				get
				{
					return 7 + MathF.Ceiling(7f * base.IssueDifficultyMultiplier);
				}
			}

			// Token: 0x17000156 RID: 342
			// (get) Token: 0x06000DE7 RID: 3559 RVA: 0x0005A5F9 File Offset: 0x000587F9
			protected override int CompanionSkillRewardXP
			{
				get
				{
					return (int)(700f + 900f * base.IssueDifficultyMultiplier);
				}
			}

			// Token: 0x17000157 RID: 343
			// (get) Token: 0x06000DE8 RID: 3560 RVA: 0x0005A60E File Offset: 0x0005880E
			public override bool IsThereLordSolution
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000158 RID: 344
			// (get) Token: 0x06000DE9 RID: 3561 RVA: 0x0005A611 File Offset: 0x00058811
			public override TextObject Title
			{
				get
				{
					TextObject textObject = new TextObject("{=Mr2rt8g8}Prodigal son of {CLAN_NAME}", null);
					textObject.SetTextVariable("CLAN_NAME", this.Clan.Name);
					return textObject;
				}
			}

			// Token: 0x17000159 RID: 345
			// (get) Token: 0x06000DEA RID: 3562 RVA: 0x0005A638 File Offset: 0x00058838
			public override TextObject Description
			{
				get
				{
					TextObject textObject = new TextObject("{=5puy0Jle}{ISSUE_OWNER.NAME} asks the player to aid a young clan member. He is supposed to have huge gambling debts so the gang leaders holds him as a hostage. You are asked to retrieve him any way possible.", null);
					StringHelpers.SetCharacterProperties("ISSUE_OWNER", base.IssueOwner.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x06000DEB RID: 3563 RVA: 0x0005A66C File Offset: 0x0005886C
			public ProdigalSonIssue(Hero issueOwner, Hero prodigalSon, Hero targetGangHero) : base(issueOwner, CampaignTime.DaysFromNow(50f))
			{
				this._prodigalSon = prodigalSon;
				this._targetHero = targetGangHero;
				this._targetSettlement = this._targetHero.CurrentSettlement;
				this._targetHouse = this._targetSettlement.LocationComplex.GetListOfLocations().FirstOrDefault((Location x) => x.CanBeReserved && !x.IsReserved);
				TextObject textObject = new TextObject("{=EZ19JOGj}{MENTOR.NAME}'s House", null);
				StringHelpers.SetCharacterProperties("MENTOR", this._targetHero.CharacterObject, textObject, false);
				this._targetHouse.ReserveLocation(textObject, textObject);
				DisableHeroAction.Apply(this._prodigalSon);
			}

			// Token: 0x06000DEC RID: 3564 RVA: 0x0005A71F File Offset: 0x0005891F
			public override void OnHeroCanHaveQuestOrIssueInfoIsRequested(Hero hero, ref bool result)
			{
				if (hero == this._targetHero || hero == this._prodigalSon)
				{
					result = false;
				}
			}

			// Token: 0x06000DED RID: 3565 RVA: 0x0005A736 File Offset: 0x00058936
			protected override float GetIssueEffectAmountInternal(IssueEffect issueEffect)
			{
				if (issueEffect == DefaultIssueEffects.IssueOwnerPower)
				{
					return -0.2f;
				}
				return 0f;
			}

			// Token: 0x06000DEE RID: 3566 RVA: 0x0005A74B File Offset: 0x0005894B
			public override ValueTuple<SkillObject, int> GetAlternativeSolutionSkill(Hero hero)
			{
				return new ValueTuple<SkillObject, int>((hero.GetSkillValue(DefaultSkills.Charm) >= hero.GetSkillValue(DefaultSkills.Roguery)) ? DefaultSkills.Charm : DefaultSkills.Roguery, 120);
			}

			// Token: 0x06000DEF RID: 3567 RVA: 0x0005A778 File Offset: 0x00058978
			protected override void OnGameLoad()
			{
				Town town = Town.AllTowns.FirstOrDefault((Town x) => x.Settlement.LocationComplex.GetListOfLocations().Contains(this._targetHouse));
				if (town != null)
				{
					this._targetSettlement = town.Settlement;
				}
			}

			// Token: 0x06000DF0 RID: 3568 RVA: 0x0005A7AB File Offset: 0x000589AB
			protected override void HourlyTick()
			{
			}

			// Token: 0x06000DF1 RID: 3569 RVA: 0x0005A7AD File Offset: 0x000589AD
			protected override QuestBase GenerateIssueQuest(string questId)
			{
				return new ProdigalSonIssueBehavior.ProdigalSonIssueQuest(questId, base.IssueOwner, this._targetHero, this._prodigalSon, this._targetHouse, base.IssueDifficultyMultiplier, CampaignTime.DaysFromNow(24f), this.RewardGold);
			}

			// Token: 0x06000DF2 RID: 3570 RVA: 0x0005A7E3 File Offset: 0x000589E3
			public override IssueBase.IssueFrequency GetFrequency()
			{
				return IssueBase.IssueFrequency.Rare;
			}

			// Token: 0x06000DF3 RID: 3571 RVA: 0x0005A7E8 File Offset: 0x000589E8
			protected override bool CanPlayerTakeQuestConditions(Hero issueGiver, out IssueBase.PreconditionFlags flag, out Hero relationHero, out SkillObject skill)
			{
				bool flag2 = issueGiver.GetRelationWithPlayer() >= -10f && !issueGiver.MapFaction.IsAtWarWith(Hero.MainHero.MapFaction) && Clan.PlayerClan.Tier >= 1;
				flag = (flag2 ? IssueBase.PreconditionFlags.None : ((!issueGiver.MapFaction.IsAtWarWith(Hero.MainHero.MapFaction)) ? ((Clan.PlayerClan.Tier >= 1) ? IssueBase.PreconditionFlags.Relation : IssueBase.PreconditionFlags.ClanTier) : IssueBase.PreconditionFlags.AtWar));
				relationHero = issueGiver;
				skill = null;
				return flag2;
			}

			// Token: 0x06000DF4 RID: 3572 RVA: 0x0005A86D File Offset: 0x00058A6D
			public override bool IssueStayAliveConditions()
			{
				return this._targetHero.IsActive;
			}

			// Token: 0x06000DF5 RID: 3573 RVA: 0x0005A87A File Offset: 0x00058A7A
			protected override void CompleteIssueWithTimedOutConsequences()
			{
			}

			// Token: 0x06000DF6 RID: 3574 RVA: 0x0005A87C File Offset: 0x00058A7C
			public override bool DoTroopsSatisfyAlternativeSolution(TroopRoster troopRoster, out TextObject explanation)
			{
				explanation = TextObject.Empty;
				return QuestHelper.CheckRosterForAlternativeSolution(troopRoster, base.GetTotalAlternativeSolutionNeededMenCount(), ref explanation, 2, false);
			}

			// Token: 0x06000DF7 RID: 3575 RVA: 0x0005A894 File Offset: 0x00058A94
			public override bool IsTroopTypeNeededByAlternativeSolution(CharacterObject character)
			{
				return character.Tier >= 2;
			}

			// Token: 0x06000DF8 RID: 3576 RVA: 0x0005A8A2 File Offset: 0x00058AA2
			public override bool AlternativeSolutionCondition(out TextObject explanation)
			{
				explanation = TextObject.Empty;
				return QuestHelper.CheckRosterForAlternativeSolution(MobileParty.MainParty.MemberRoster, base.GetTotalAlternativeSolutionNeededMenCount(), ref explanation, 2, false);
			}

			// Token: 0x06000DF9 RID: 3577 RVA: 0x0005A8C3 File Offset: 0x00058AC3
			protected override void AlternativeSolutionEndWithSuccessConsequence()
			{
				base.AlternativeSolutionHero.AddSkillXp(DefaultSkills.Charm, (float)((int)(700f + 900f * base.IssueDifficultyMultiplier)));
				this.RelationshipChangeWithIssueOwner = 5;
				GainRenownAction.Apply(Hero.MainHero, 3f, false);
			}

			// Token: 0x06000DFA RID: 3578 RVA: 0x0005A900 File Offset: 0x00058B00
			protected override void AlternativeSolutionEndWithFailureConsequence()
			{
				this.RelationshipChangeWithIssueOwner = -5;
			}

			// Token: 0x06000DFB RID: 3579 RVA: 0x0005A90A File Offset: 0x00058B0A
			protected override void OnIssueFinalized()
			{
				if (this._prodigalSon.HeroState == Hero.CharacterStates.Disabled)
				{
					this._prodigalSon.ChangeState(Hero.CharacterStates.Released);
				}
			}

			// Token: 0x06000DFC RID: 3580 RVA: 0x0005A926 File Offset: 0x00058B26
			internal static void AutoGeneratedStaticCollectObjectsProdigalSonIssue(object o, List<object> collectedObjects)
			{
				((ProdigalSonIssueBehavior.ProdigalSonIssue)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
			}

			// Token: 0x06000DFD RID: 3581 RVA: 0x0005A934 File Offset: 0x00058B34
			protected override void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
			{
				base.AutoGeneratedInstanceCollectObjects(collectedObjects);
				collectedObjects.Add(this._prodigalSon);
				collectedObjects.Add(this._targetHero);
				collectedObjects.Add(this._targetHouse);
			}

			// Token: 0x06000DFE RID: 3582 RVA: 0x0005A961 File Offset: 0x00058B61
			internal static object AutoGeneratedGetMemberValue_prodigalSon(object o)
			{
				return ((ProdigalSonIssueBehavior.ProdigalSonIssue)o)._prodigalSon;
			}

			// Token: 0x06000DFF RID: 3583 RVA: 0x0005A96E File Offset: 0x00058B6E
			internal static object AutoGeneratedGetMemberValue_targetHero(object o)
			{
				return ((ProdigalSonIssueBehavior.ProdigalSonIssue)o)._targetHero;
			}

			// Token: 0x06000E00 RID: 3584 RVA: 0x0005A97B File Offset: 0x00058B7B
			internal static object AutoGeneratedGetMemberValue_targetHouse(object o)
			{
				return ((ProdigalSonIssueBehavior.ProdigalSonIssue)o)._targetHouse;
			}

			// Token: 0x0400060B RID: 1547
			private const int IssueDurationInDays = 50;

			// Token: 0x0400060C RID: 1548
			private const int QuestDurationInDays = 24;

			// Token: 0x0400060D RID: 1549
			private const int TroopTierForAlternativeSolution = 2;

			// Token: 0x0400060E RID: 1550
			private const int RequiredSkillValueForAlternativeSolution = 120;

			// Token: 0x0400060F RID: 1551
			[SaveableField(10)]
			private readonly Hero _prodigalSon;

			// Token: 0x04000610 RID: 1552
			[SaveableField(20)]
			private readonly Hero _targetHero;

			// Token: 0x04000611 RID: 1553
			[SaveableField(30)]
			private readonly Location _targetHouse;

			// Token: 0x04000612 RID: 1554
			private Settlement _targetSettlement;
		}

		// Token: 0x02000165 RID: 357
		public class ProdigalSonIssueQuest : QuestBase
		{
			// Token: 0x1700015A RID: 346
			// (get) Token: 0x06000E02 RID: 3586 RVA: 0x0005A9A5 File Offset: 0x00058BA5
			public override TextObject Title
			{
				get
				{
					TextObject textObject = new TextObject("{=7kqz1LlI}Prodigal son of {CLAN}", null);
					textObject.SetTextVariable("CLAN", base.QuestGiver.Clan.Name);
					return textObject;
				}
			}

			// Token: 0x1700015B RID: 347
			// (get) Token: 0x06000E03 RID: 3587 RVA: 0x0005A9CE File Offset: 0x00058BCE
			public override bool IsRemainingTimeHidden
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700015C RID: 348
			// (get) Token: 0x06000E04 RID: 3588 RVA: 0x0005A9D1 File Offset: 0x00058BD1
			private Settlement Settlement
			{
				get
				{
					return this._targetHero.CurrentSettlement;
				}
			}

			// Token: 0x1700015D RID: 349
			// (get) Token: 0x06000E05 RID: 3589 RVA: 0x0005A9DE File Offset: 0x00058BDE
			private int DebtWithInterest
			{
				get
				{
					return (int)((float)this.RewardGold * 1.1f);
				}
			}

			// Token: 0x1700015E RID: 350
			// (get) Token: 0x06000E06 RID: 3590 RVA: 0x0005A9F0 File Offset: 0x00058BF0
			private TextObject QuestStartedLog
			{
				get
				{
					TextObject textObject = new TextObject("{=CXw9a1i5}{QUEST_GIVER.LINK}, a {?QUEST_GIVER.GENDER}lady{?}lord{\\?} from the {QUEST_GIVER_CLAN} clan, asked you to go to {SETTLEMENT} to free {?QUEST_GIVER.GENDER}her{?}his{\\?} relative. The young man is currently held by {TARGET_HERO.LINK}, a local gang leader, because of his debts. {QUEST_GIVER.LINK} has suggested that you make an example of the gang so no one would dare to hold a nobleman again. {?QUEST_GIVER.GENDER}She{?}He{\\?} said you can easily find the house in which the young nobleman is held in the town square.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					StringHelpers.SetCharacterProperties("TARGET_HERO", this._targetHero.CharacterObject, textObject, false);
					textObject.SetTextVariable("QUEST_GIVER_CLAN", base.QuestGiver.Clan.EncyclopediaLinkWithName);
					textObject.SetTextVariable("SETTLEMENT", this.Settlement.EncyclopediaLinkWithName);
					return textObject;
				}
			}

			// Token: 0x1700015F RID: 351
			// (get) Token: 0x06000E07 RID: 3591 RVA: 0x0005AA70 File Offset: 0x00058C70
			private TextObject PlayerDefeatsThugsQuestSuccessLog
			{
				get
				{
					TextObject textObject = new TextObject("{=axLR9bQo}You have defeated the thugs that held {PRODIGAL_SON.LINK} as {QUEST_GIVER.LINK} has asked you to. {?QUEST_GIVER.GENDER}Lady{?}Lord{\\?} {QUEST_GIVER.LINK} soon sends {?QUEST_GIVER.GENDER}her{?}his{\\?} best regards and a sum of {REWARD}{GOLD_ICON} as a reward.", null);
					StringHelpers.SetCharacterProperties("PRODIGAL_SON", this._prodigalSon.CharacterObject, textObject, false);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					textObject.SetTextVariable("REWARD", this.RewardGold);
					return textObject;
				}
			}

			// Token: 0x17000160 RID: 352
			// (get) Token: 0x06000E08 RID: 3592 RVA: 0x0005AACC File Offset: 0x00058CCC
			private TextObject PlayerPaysTheDebtQuestSuccessLog
			{
				get
				{
					TextObject textObject = new TextObject("{=skMoB7c6}You have paid the debt that {PRODIGAL_SON.LINK} owes. True to {?TARGET_HERO.GENDER}her{?}his{\\?} word {TARGET_HERO.LINK} releases the boy immediately. Soon after, {?QUEST_GIVER.GENDER}Lady{?}Lord{\\?} {QUEST_GIVER.LINK} sends {?QUEST_GIVER.GENDER}her{?}his{\\?} best regards and a sum of {REWARD}{GOLD_ICON} as a reward.", null);
					StringHelpers.SetCharacterProperties("PRODIGAL_SON", this._prodigalSon.CharacterObject, textObject, false);
					StringHelpers.SetCharacterProperties("TARGET_HERO", this._targetHero.CharacterObject, textObject, false);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					textObject.SetTextVariable("REWARD", this.RewardGold);
					return textObject;
				}
			}

			// Token: 0x17000161 RID: 353
			// (get) Token: 0x06000E09 RID: 3593 RVA: 0x0005AB40 File Offset: 0x00058D40
			private TextObject QuestTimeOutFailLog
			{
				get
				{
					TextObject textObject = new TextObject("{=dmijPqWn}You have failed to extract {QUEST_GIVER.LINK}'s relative captive in time. They have moved the boy to a more secure place. Its impossible to find him now. {QUEST_GIVER.LINK} will have to deal with {TARGET_HERO.LINK} himself now. {?QUEST_GIVER.GENDER}She{?}He{\\?} won't be happy to hear this.", null);
					StringHelpers.SetCharacterProperties("TARGET_HERO", this._targetHero.CharacterObject, textObject, false);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x17000162 RID: 354
			// (get) Token: 0x06000E0A RID: 3594 RVA: 0x0005AB8C File Offset: 0x00058D8C
			private TextObject PlayerHasDefeatedQuestFailLog
			{
				get
				{
					TextObject textObject = new TextObject("{=d5a8xQos}You have failed to defeat the thugs that keep {QUEST_GIVER.LINK}'s relative captive. After your assault you learn that they move the boy to a more secure place. Now its impossible to find him. {QUEST_GIVER.LINK} will have to deal with {TARGET_HERO.LINK} himself now. {?QUEST_GIVER.GENDER}She{?}He{\\?} won't be happy to hear this.", null);
					StringHelpers.SetCharacterProperties("TARGET_HERO", this._targetHero.CharacterObject, textObject, false);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x17000163 RID: 355
			// (get) Token: 0x06000E0B RID: 3595 RVA: 0x0005ABD8 File Offset: 0x00058DD8
			private TextObject PlayerConvincesGangLeaderQuestSuccessLog
			{
				get
				{
					TextObject textObject = new TextObject("{=Rb7g1U2s}You have convinced {TARGET_HERO.LINK} to release {PRODIGAL_SON.LINK}. Soon after, {?QUEST_GIVER.GENDER}Lady{?}Lord{\\?} {QUEST_GIVER.LINK} sends {?QUEST_GIVER.GENDER}her{?}his{\\?} best regards and a sum of {REWARD}{GOLD_ICON} as a reward.", null);
					StringHelpers.SetCharacterProperties("PRODIGAL_SON", this._prodigalSon.CharacterObject, textObject, false);
					StringHelpers.SetCharacterProperties("TARGET_HERO", this._targetHero.CharacterObject, textObject, false);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					textObject.SetTextVariable("REWARD", this.RewardGold);
					return textObject;
				}
			}

			// Token: 0x17000164 RID: 356
			// (get) Token: 0x06000E0C RID: 3596 RVA: 0x0005AC4C File Offset: 0x00058E4C
			private TextObject WarDeclaredQuestCancelLog
			{
				get
				{
					TextObject textObject = new TextObject("{=VuqZuSe2}Your clan is now at war with the {QUEST_GIVER.LINK}'s faction. Your agreement has been canceled.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x17000165 RID: 357
			// (get) Token: 0x06000E0D RID: 3597 RVA: 0x0005AC80 File Offset: 0x00058E80
			private TextObject PlayerDeclaredWarQuestLogText
			{
				get
				{
					TextObject textObject = new TextObject("{=bqeWVVEE}Your actions have started a war with {QUEST_GIVER.LINK}'s faction. {?QUEST_GIVER.GENDER}She{?}He{\\?} cancels your agreement and the quest is a failure.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x17000166 RID: 358
			// (get) Token: 0x06000E0E RID: 3598 RVA: 0x0005ACB2 File Offset: 0x00058EB2
			private TextObject CrimeRatingCancelLog
			{
				get
				{
					TextObject textObject = new TextObject("{=oulvvl52}You are accused in {SETTLEMENT} of a crime, and {QUEST_GIVER.LINK} no longer trusts you in this matter.", null);
					textObject.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, false);
					textObject.SetTextVariable("SETTLEMENT", this.Settlement.EncyclopediaLinkWithName);
					return textObject;
				}
			}

			// Token: 0x06000E0F RID: 3599 RVA: 0x0005ACED File Offset: 0x00058EED
			public ProdigalSonIssueQuest(string questId, Hero questGiver, Hero targetHero, Hero prodigalSon, Location targetHouse, float questDifficulty, CampaignTime duration, int rewardGold) : base(questId, questGiver, duration, rewardGold)
			{
				this._targetHero = targetHero;
				this._prodigalSon = prodigalSon;
				this._targetHouse = targetHouse;
				this._questDifficulty = questDifficulty;
				this.SetDialogs();
				base.InitializeQuestOnCreation();
			}

			// Token: 0x06000E10 RID: 3600 RVA: 0x0005AD28 File Offset: 0x00058F28
			protected override void SetDialogs()
			{
				TextObject textObject = new TextObject("{=bQnVtegC}Good, even better. [ib:confident][if:convo_astonished]You can find the house easily when you go to {SETTLEMENT} and walk around the town square. Or you could just speak to this gang leader, {TARGET_HERO.LINK}, and make {?TARGET_HERO.GENDER}her{?}him{\\?} understand and get my boy released. Good luck. I await good news.", null);
				StringHelpers.SetCharacterProperties("TARGET_HERO", this._targetHero.CharacterObject, textObject, false);
				Settlement settlement = (this._targetHero.CurrentSettlement != null) ? this._targetHero.CurrentSettlement : this._targetHero.PartyBelongedTo.HomeSettlement;
				textObject.SetTextVariable("SETTLEMENT", settlement.EncyclopediaLinkWithName);
				this.OfferDialogFlow = DialogFlow.CreateDialogFlow("issue_classic_quest_start", 100).NpcLine(textObject, null, null).Condition(new ConversationSentence.OnConditionDelegate(this.is_talking_to_quest_giver)).Consequence(new ConversationSentence.OnConsequenceDelegate(this.QuestAcceptedConsequences)).CloseDialog();
				this.DiscussDialogFlow = DialogFlow.CreateDialogFlow("quest_discuss", 100).NpcLine(new TextObject("{=TkYk5yxn}Yes? Go already. Get our boy back.[if:convo_excited]", null), null, null).Condition(new ConversationSentence.OnConditionDelegate(this.is_talking_to_quest_giver)).BeginPlayerOptions().PlayerOption(new TextObject("{=kqXxvtwQ}Don't worry I'll free him.", null), null).NpcLine(new TextObject("{=ddEu5IFQ}I hope so.", null), null, null).Consequence(new ConversationSentence.OnConsequenceDelegate(MapEventHelper.OnConversationEnd)).CloseDialog().PlayerOption(new TextObject("{=Jss9UqZC}I'll go right away", null), null).NpcLine(new TextObject("{=IdKG3IaS}Good to hear that.", null), null, null).Consequence(new ConversationSentence.OnConsequenceDelegate(MapEventHelper.OnConversationEnd)).CloseDialog().EndPlayerOptions().CloseDialog();
				Campaign.Current.ConversationManager.AddDialogFlow(this.GetTargetHeroDialogFlow(), this);
				Campaign.Current.ConversationManager.AddDialogFlow(this.GetProdigalSonDialogFlow(), this);
			}

			// Token: 0x06000E11 RID: 3601 RVA: 0x0005AEB9 File Offset: 0x000590B9
			protected override void InitializeQuestOnGameLoad()
			{
				this.SetDialogs();
			}

			// Token: 0x06000E12 RID: 3602 RVA: 0x0005AEC1 File Offset: 0x000590C1
			protected override void HourlyTick()
			{
			}

			// Token: 0x06000E13 RID: 3603 RVA: 0x0005AEC4 File Offset: 0x000590C4
			protected override void RegisterEvents()
			{
				CampaignEvents.BeforeMissionOpenedEvent.AddNonSerializedListener(this, new Action(this.BeforeMissionOpened));
				CampaignEvents.MissionTickEvent.AddNonSerializedListener(this, new Action<float>(this.OnMissionTick));
				CampaignEvents.WarDeclared.AddNonSerializedListener(this, new Action<IFaction, IFaction, DeclareWarAction.DeclareWarDetail>(this.OnWarDeclared));
				CampaignEvents.OnClanChangedKingdomEvent.AddNonSerializedListener(this, new Action<Clan, Kingdom, Kingdom, ChangeKingdomAction.ChangeKingdomActionDetail, bool>(this.OnClanChangedKingdom));
				CampaignEvents.HeroKilledEvent.AddNonSerializedListener(this, new Action<Hero, Hero, KillCharacterAction.KillCharacterActionDetail, bool>(this.OnHeroKilled));
				CampaignEvents.MapEventStarted.AddNonSerializedListener(this, new Action<MapEvent, PartyBase, PartyBase>(this.OnMapEventStarted));
				CampaignEvents.OnMissionStartedEvent.AddNonSerializedListener(this, new Action<IMission>(this.OnMissionStarted));
			}

			// Token: 0x06000E14 RID: 3604 RVA: 0x0005AF72 File Offset: 0x00059172
			private void OnMissionStarted(IMission mission)
			{
				ICampaignMission campaignMission = CampaignMission.Current;
				if (((campaignMission != null) ? campaignMission.Location : null) == this._targetHouse)
				{
					this._isFirstMissionTick = true;
				}
			}

			// Token: 0x06000E15 RID: 3605 RVA: 0x0005AF94 File Offset: 0x00059194
			public override void OnHeroCanHaveQuestOrIssueInfoIsRequested(Hero hero, ref bool result)
			{
				if (hero == this._prodigalSon || hero == this._targetHero)
				{
					result = false;
				}
			}

			// Token: 0x06000E16 RID: 3606 RVA: 0x0005AFAB File Offset: 0x000591AB
			public override void OnHeroCanMoveToSettlementInfoIsRequested(Hero hero, ref bool result)
			{
				if (hero == this._prodigalSon)
				{
					result = false;
				}
			}

			// Token: 0x06000E17 RID: 3607 RVA: 0x0005AFB9 File Offset: 0x000591B9
			private void OnMapEventStarted(MapEvent mapEvent, PartyBase attackerParty, PartyBase defenderParty)
			{
				if (QuestHelper.CheckMinorMajorCoercion(this, mapEvent, attackerParty))
				{
					QuestHelper.ApplyGenericMinorMajorCoercionConsequences(this, mapEvent);
				}
			}

			// Token: 0x06000E18 RID: 3608 RVA: 0x0005AFCC File Offset: 0x000591CC
			private void OnHeroKilled(Hero victim, Hero killer, KillCharacterAction.KillCharacterActionDetail detail, bool showNotification = true)
			{
				if (victim == this._targetHero || victim == this._prodigalSon)
				{
					TextObject textObject = (detail == KillCharacterAction.KillCharacterActionDetail.Lost) ? this.TargetHeroDisappearedLogText : this.TargetHeroDiedLogText;
					StringHelpers.SetCharacterProperties("QUEST_TARGET", victim.CharacterObject, textObject, false);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					base.AddLog(textObject, false);
					base.CompleteQuestWithCancel(null);
				}
			}

			// Token: 0x06000E19 RID: 3609 RVA: 0x0005B039 File Offset: 0x00059239
			protected override void OnTimedOut()
			{
				this.FinishQuestFail1();
			}

			// Token: 0x06000E1A RID: 3610 RVA: 0x0005B041 File Offset: 0x00059241
			protected override void OnFinalize()
			{
				this._targetHouse.RemoveReservation();
			}

			// Token: 0x06000E1B RID: 3611 RVA: 0x0005B050 File Offset: 0x00059250
			private void BeforeMissionOpened()
			{
				if (Settlement.CurrentSettlement == this.Settlement && LocationComplex.Current != null)
				{
					if (LocationComplex.Current.GetLocationOfCharacter(this._prodigalSon) == null)
					{
						this.SpawnProdigalSonInHouse();
						if (!this._isHouseFightFinished)
						{
							this.SpawnThugsInHouse();
							this._isMissionFightInitialized = false;
						}
					}
					using (List<AccompanyingCharacter>.Enumerator enumerator = PlayerEncounter.LocationEncounter.CharactersAccompanyingPlayer.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							AccompanyingCharacter character = enumerator.Current;
							if (!character.CanEnterLocation(this._targetHouse))
							{
								character.AllowEntranceToLocations((Location x) => character.CanEnterLocation(x) || x == this._targetHouse);
							}
						}
					}
				}
			}

			// Token: 0x06000E1C RID: 3612 RVA: 0x0005B124 File Offset: 0x00059324
			private void OnMissionTick(float dt)
			{
				if (CampaignMission.Current.Location == this._targetHouse)
				{
					Mission mission = Mission.Current;
					if (this._isFirstMissionTick)
					{
						Mission.Current.Agents.First((Agent x) => x.Character == this._prodigalSon.CharacterObject).GetComponent<CampaignAgentComponent>().AgentNavigator.RemoveBehaviorGroup<AlarmedBehaviorGroup>();
						this._isFirstMissionTick = false;
					}
					if (!this._isMissionFightInitialized && !this._isHouseFightFinished && mission.Agents.Count > 0)
					{
						this._isMissionFightInitialized = true;
						MissionFightHandler missionBehavior = mission.GetMissionBehavior<MissionFightHandler>();
						List<Agent> list = new List<Agent>();
						List<Agent> list2 = new List<Agent>();
						foreach (Agent agent in mission.Agents)
						{
							if (agent.IsEnemyOf(Agent.Main))
							{
								list.Add(agent);
							}
							else if (agent.Team == Agent.Main.Team)
							{
								list2.Add(agent);
							}
						}
						missionBehavior.StartCustomFight(list2, list, false, false, new MissionFightHandler.OnFightEndDelegate(this.HouseFightFinished));
						foreach (Agent agent2 in list)
						{
							agent2.Defensiveness = 2f;
						}
					}
				}
			}

			// Token: 0x06000E1D RID: 3613 RVA: 0x0005B290 File Offset: 0x00059490
			private void OnWarDeclared(IFaction faction1, IFaction faction2, DeclareWarAction.DeclareWarDetail detail)
			{
				if (base.QuestGiver.MapFaction.IsAtWarWith(Hero.MainHero.MapFaction))
				{
					if (detail == DeclareWarAction.DeclareWarDetail.CausedByCrimeRatingChange)
					{
						this.RelationshipChangeWithQuestGiver = -5;
						Tuple<TraitObject, int>[] effectedTraits = new Tuple<TraitObject, int>[]
						{
							new Tuple<TraitObject, int>(DefaultTraits.Honor, -50)
						};
						TraitLevelingHelper.OnIssueSolvedThroughQuest(Hero.MainHero, effectedTraits);
					}
					if (DiplomacyHelper.IsWarCausedByPlayer(faction1, faction2, detail))
					{
						base.CompleteQuestWithFail(this.PlayerDeclaredWarQuestLogText);
						return;
					}
					base.CompleteQuestWithCancel((detail == DeclareWarAction.DeclareWarDetail.CausedByCrimeRatingChange) ? this.CrimeRatingCancelLog : this.WarDeclaredQuestCancelLog);
				}
			}

			// Token: 0x06000E1E RID: 3614 RVA: 0x0005B318 File Offset: 0x00059518
			private void OnClanChangedKingdom(Clan clan, Kingdom oldKingdom, Kingdom newKingdom, ChangeKingdomAction.ChangeKingdomActionDetail detail, bool showNotification = true)
			{
				if (clan == Clan.PlayerClan && ((newKingdom != null && newKingdom.IsAtWarWith(base.QuestGiver.MapFaction)) || (newKingdom == null && clan.IsAtWarWith(base.QuestGiver.MapFaction))))
				{
					base.CompleteQuestWithCancel(this.WarDeclaredQuestCancelLog);
				}
			}

			// Token: 0x06000E1F RID: 3615 RVA: 0x0005B368 File Offset: 0x00059568
			private void HouseFightFinished(bool isPlayerSideWon)
			{
				if (isPlayerSideWon)
				{
					Agent agent = Mission.Current.Agents.First((Agent x) => x.Character == this._prodigalSon.CharacterObject);
					if (agent.Position.Distance(Agent.Main.Position) > agent.GetInteractionDistanceToUsable(Agent.Main))
					{
						ScriptBehavior.AddTargetWithDelegate(agent, new ScriptBehavior.SelectTargetDelegate(this.SelectPlayerAsTarget), new ScriptBehavior.OnTargetReachedDelegate(this.OnTargetReached));
					}
					else
					{
						Agent agent2 = null;
						UsableMachine usableMachine = null;
						WorldFrame invalid = WorldFrame.Invalid;
						this.OnTargetReached(agent, ref agent2, ref usableMachine, ref invalid);
					}
				}
				else
				{
					this.FinishQuestFail2();
				}
				this._isHouseFightFinished = true;
			}

			// Token: 0x06000E20 RID: 3616 RVA: 0x0005B401 File Offset: 0x00059601
			private bool OnTargetReached(Agent agent, ref Agent targetAgent, ref UsableMachine targetUsableMachine, ref WorldFrame targetFrame)
			{
				Mission.Current.GetMissionBehavior<MissionConversationLogic>().StartConversation(agent, false, false);
				targetAgent = null;
				return false;
			}

			// Token: 0x06000E21 RID: 3617 RVA: 0x0005B41C File Offset: 0x0005961C
			private bool SelectPlayerAsTarget(Agent agent, ref Agent targetAgent, ref UsableMachine targetUsableMachine, ref WorldFrame targetFrame)
			{
				targetAgent = null;
				if (agent.Position.Distance(Agent.Main.Position) > agent.GetInteractionDistanceToUsable(Agent.Main))
				{
					targetAgent = Agent.Main;
				}
				return targetAgent != null;
			}

			// Token: 0x06000E22 RID: 3618 RVA: 0x0005B460 File Offset: 0x00059660
			private void SpawnProdigalSonInHouse()
			{
				Monster monsterWithSuffix = TaleWorlds.Core.FaceGen.GetMonsterWithSuffix(this._prodigalSon.CharacterObject.Race, "_settlement");
				LocationCharacter locationCharacter = new LocationCharacter(new AgentData(new SimpleAgentOrigin(this._prodigalSon.CharacterObject, -1, null, default(UniqueTroopDescriptor))).Monster(monsterWithSuffix), new LocationCharacter.AddBehaviorsDelegate(SandBoxManager.Instance.AgentBehaviorManager.AddWandererBehaviors), "npc_common", true, LocationCharacter.CharacterRelations.Neutral, null, true, false, null, false, false, true);
				this._targetHouse.AddCharacter(locationCharacter);
			}

			// Token: 0x06000E23 RID: 3619 RVA: 0x0005B4E4 File Offset: 0x000596E4
			private void SpawnThugsInHouse()
			{
				CharacterObject @object = MBObjectManager.Instance.GetObject<CharacterObject>("gangster_1");
				CharacterObject object2 = MBObjectManager.Instance.GetObject<CharacterObject>("gangster_2");
				CharacterObject object3 = MBObjectManager.Instance.GetObject<CharacterObject>("gangster_3");
				List<CharacterObject> list = new List<CharacterObject>();
				if (this._questDifficulty < 0.4f)
				{
					list.Add(@object);
					list.Add(@object);
					if (this._questDifficulty >= 0.2f)
					{
						list.Add(object2);
					}
				}
				else if (this._questDifficulty < 0.6f)
				{
					list.Add(@object);
					list.Add(object2);
					list.Add(object2);
				}
				else
				{
					list.Add(object2);
					list.Add(object3);
					list.Add(object3);
				}
				foreach (CharacterObject characterObject in list)
				{
					Monster monsterWithSuffix = TaleWorlds.Core.FaceGen.GetMonsterWithSuffix(characterObject.Race, "_settlement");
					LocationCharacter locationCharacter = new LocationCharacter(new AgentData(new SimpleAgentOrigin(characterObject, -1, null, default(UniqueTroopDescriptor))).Monster(monsterWithSuffix), new LocationCharacter.AddBehaviorsDelegate(SandBoxManager.Instance.AgentBehaviorManager.AddWandererBehaviors), "npc_common", true, LocationCharacter.CharacterRelations.Enemy, null, true, false, null, false, false, true);
					this._targetHouse.AddCharacter(locationCharacter);
				}
			}

			// Token: 0x06000E24 RID: 3620 RVA: 0x0005B634 File Offset: 0x00059834
			private void QuestAcceptedConsequences()
			{
				base.StartQuest();
				base.AddTrackedObject(this.Settlement);
				base.AddTrackedObject(this._targetHero);
				base.AddLog(this.QuestStartedLog, false);
			}

			// Token: 0x06000E25 RID: 3621 RVA: 0x0005B664 File Offset: 0x00059864
			private DialogFlow GetProdigalSonDialogFlow()
			{
				return DialogFlow.CreateDialogFlow("start", 125).NpcLine("{=DYq30shK}Thank you, {?PLAYER.GENDER}milady{?}sir{\\?}.", null, null).Condition(() => Hero.OneToOneConversationHero == this._prodigalSon).NpcLine("{=K8TSoRSD}Did {?QUEST_GIVER.GENDER}Lady{?}Lord{\\?} {QUEST_GIVER.LINK} send you to rescue me?", null, null).Condition(delegate
				{
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, null, false);
					return true;
				}).PlayerLine("{=ln3bGyIO}Yes, I'm here to take you back.", null).NpcLine("{=evIohG6b}Thank you, but there's no need. Once we are out of here I can manage to return on my own.[if:convo_happy] I appreciate your efforts. I'll tell everyone in my clan of your heroism.", null, null).NpcLine("{=qsJxhNGZ}Safe travels {?PLAYER.GENDER}milady{?}sir{\\?}.", null, null).Consequence(delegate
				{
					Mission.Current.Agents.First((Agent x) => x.Character == this._prodigalSon.CharacterObject).GetComponent<CampaignAgentComponent>().AgentNavigator.GetBehaviorGroup<DailyBehaviorGroup>().DisableScriptedBehavior();
					Campaign.Current.ConversationManager.ConversationEndOneShot += this.OnEndHouseMissionDialog;
				}).CloseDialog();
			}

			// Token: 0x06000E26 RID: 3622 RVA: 0x0005B6F0 File Offset: 0x000598F0
			private DialogFlow GetTargetHeroDialogFlow()
			{
				DialogFlow dialogFlow = DialogFlow.CreateDialogFlow("start", 125).BeginNpcOptions().NpcOption(new TextObject("{=M0vxXQGB}Yes? Do you have something to say?[ib:closed][if:convo_nonchalant]", null), () => Hero.OneToOneConversationHero == this._targetHero && !this._playerTalkedToTargetHero, null, null).Consequence(delegate
				{
					StringHelpers.SetCharacterProperties("PRODIGAL_SON", this._prodigalSon.CharacterObject, null, false);
					this._playerTalkedToTargetHero = true;
				}).PlayerLine("{=K5DgDU2a}I am here for the boy. {PRODIGAL_SON.LINK}. You know who I mean.", null).GotoDialogState("start").NpcOption(new TextObject("{=I979VDEn}Yes, did you bring {GOLD_AMOUNT}{GOLD_ICON}? [ib:hip][if:convo_stern]That's what he owes... With an interest of course.", null), delegate()
				{
					bool flag = Hero.OneToOneConversationHero == this._targetHero && this._playerTalkedToTargetHero;
					if (flag)
					{
						MBTextManager.SetTextVariable("GOLD_AMOUNT", this.DebtWithInterest);
					}
					return flag;
				}, null, null).BeginPlayerOptions().PlayerOption("{=IboStvbL}Here is the money, now release him!", null).ClickableCondition(delegate(out TextObject explanation)
				{
					bool result = false;
					if (Hero.MainHero.Gold >= this.DebtWithInterest)
					{
						explanation = TextObject.Empty;
						result = true;
					}
					else
					{
						explanation = new TextObject("{=YuLLsAUb}You don't have {GOLD_AMOUNT}{GOLD_ICON}.", null);
						explanation.SetTextVariable("GOLD_AMOUNT", this.DebtWithInterest);
					}
					return result;
				}).NpcLine("{=7k03GxZ1}It's great doing business with you. I'll order my men to release him immediately.[if:convo_mocking_teasing]", null, null).Consequence(new ConversationSentence.OnConsequenceDelegate(this.FinishQuestSuccess4)).CloseDialog().PlayerOption("{=9pTkQ5o2}It would be in your interest to let this young nobleman go...", null).Condition(() => !this._playerTriedToPersuade).Consequence(delegate
				{
					this._playerTriedToPersuade = true;
					this._task = this.GetPersuasionTask();
					this.persuasion_start_on_consequence();
				}).GotoDialogState("persuade_gang_start_reservation").PlayerOption("{=AwZhx2tT}I will be back.", null).NpcLine("{=0fp67gxl}Have a good day.", null, null).CloseDialog().EndPlayerOptions().EndNpcOptions();
				this.AddPersuasionDialogs(dialogFlow);
				return dialogFlow;
			}

			// Token: 0x06000E27 RID: 3623 RVA: 0x0005B81C File Offset: 0x00059A1C
			private void AddPersuasionDialogs(DialogFlow dialog)
			{
				dialog.AddDialogLine("persuade_gang_introduction", "persuade_gang_start_reservation", "persuade_gang_player_option", "{=EIsQnfLP}Tell me how it's in my interest...[ib:closed][if:convo_nonchalant]", new ConversationSentence.OnConditionDelegate(this.persuasion_start_on_condition), null, this, 100, null, null, null);
				dialog.AddDialogLine("persuade_gang_success", "persuade_gang_start_reservation", "close_window", "{=alruamIW}Hmm... You may be right. It's not worth it. I'll release the boy immediately.[ib:hip][if:convo_pondering]", new ConversationSentence.OnConditionDelegate(ConversationManager.GetPersuasionProgressSatisfied), new ConversationSentence.OnConsequenceDelegate(this.persuasion_success_on_consequence), this, int.MaxValue, null, null, null);
				dialog.AddDialogLine("persuade_gang_failed", "persuade_gang_start_reservation", "start", "{=1YGgXOB7}Meh... Do you think ruling the streets of a city is easy? You underestimate us. Now, about the money.[ib:closed2][if:convo_nonchalant]", null, new ConversationSentence.OnConsequenceDelegate(ConversationManager.EndPersuasion), this, 100, null, null, null);
				string id = "persuade_gang_player_option_1";
				string inputToken = "persuade_gang_player_option";
				string outputToken = "persuade_gang_player_option_response";
				string text = "{=!}{PERSUADE_GANG_ATTEMPT_1}";
				ConversationSentence.OnConditionDelegate conditionDelegate = new ConversationSentence.OnConditionDelegate(this.persuasion_select_option_1_on_condition);
				ConversationSentence.OnConsequenceDelegate consequenceDelegate = new ConversationSentence.OnConsequenceDelegate(this.persuasion_select_option_1_on_consequence);
				ConversationSentence.OnPersuasionOptionDelegate persuasionOptionDelegate = new ConversationSentence.OnPersuasionOptionDelegate(this.persuasion_setup_option_1);
				ConversationSentence.OnClickableConditionDelegate clickableConditionDelegate = new ConversationSentence.OnClickableConditionDelegate(this.persuasion_clickable_option_1_on_condition);
				dialog.AddPlayerLine(id, inputToken, outputToken, text, conditionDelegate, consequenceDelegate, this, 100, clickableConditionDelegate, persuasionOptionDelegate, null, null);
				string id2 = "persuade_gang_player_option_2";
				string inputToken2 = "persuade_gang_player_option";
				string outputToken2 = "persuade_gang_player_option_response";
				string text2 = "{=!}{PERSUADE_GANG_ATTEMPT_2}";
				ConversationSentence.OnConditionDelegate conditionDelegate2 = new ConversationSentence.OnConditionDelegate(this.persuasion_select_option_2_on_condition);
				ConversationSentence.OnConsequenceDelegate consequenceDelegate2 = new ConversationSentence.OnConsequenceDelegate(this.persuasion_select_option_2_on_consequence);
				persuasionOptionDelegate = new ConversationSentence.OnPersuasionOptionDelegate(this.persuasion_setup_option_2);
				clickableConditionDelegate = new ConversationSentence.OnClickableConditionDelegate(this.persuasion_clickable_option_2_on_condition);
				dialog.AddPlayerLine(id2, inputToken2, outputToken2, text2, conditionDelegate2, consequenceDelegate2, this, 100, clickableConditionDelegate, persuasionOptionDelegate, null, null);
				string id3 = "persuade_gang_player_option_3";
				string inputToken3 = "persuade_gang_player_option";
				string outputToken3 = "persuade_gang_player_option_response";
				string text3 = "{=!}{PERSUADE_GANG_ATTEMPT_3}";
				ConversationSentence.OnConditionDelegate conditionDelegate3 = new ConversationSentence.OnConditionDelegate(this.persuasion_select_option_3_on_condition);
				ConversationSentence.OnConsequenceDelegate consequenceDelegate3 = new ConversationSentence.OnConsequenceDelegate(this.persuasion_select_option_3_on_consequence);
				persuasionOptionDelegate = new ConversationSentence.OnPersuasionOptionDelegate(this.persuasion_setup_option_3);
				clickableConditionDelegate = new ConversationSentence.OnClickableConditionDelegate(this.persuasion_clickable_option_3_on_condition);
				dialog.AddPlayerLine(id3, inputToken3, outputToken3, text3, conditionDelegate3, consequenceDelegate3, this, 100, clickableConditionDelegate, persuasionOptionDelegate, null, null);
				dialog.AddDialogLine("persuade_gang_option_reaction", "persuade_gang_player_option_response", "persuade_gang_start_reservation", "{=!}{PERSUASION_REACTION}", new ConversationSentence.OnConditionDelegate(this.persuasion_selected_option_response_on_condition), new ConversationSentence.OnConsequenceDelegate(this.persuasion_selected_option_response_on_consequence), this, 100, null, null, null);
			}

			// Token: 0x06000E28 RID: 3624 RVA: 0x0005B9F6 File Offset: 0x00059BF6
			private bool is_talking_to_quest_giver()
			{
				return Hero.OneToOneConversationHero == base.QuestGiver;
			}

			// Token: 0x06000E29 RID: 3625 RVA: 0x0005BA08 File Offset: 0x00059C08
			private bool persuasion_start_on_condition()
			{
				if (Hero.OneToOneConversationHero == this._targetHero && !ConversationManager.GetPersuasionIsFailure())
				{
					return this._task.Options.Any((PersuasionOptionArgs x) => !x.IsBlocked);
				}
				return false;
			}

			// Token: 0x06000E2A RID: 3626 RVA: 0x0005BA5C File Offset: 0x00059C5C
			private void persuasion_selected_option_response_on_consequence()
			{
				Tuple<PersuasionOptionArgs, PersuasionOptionResult> tuple = ConversationManager.GetPersuasionChosenOptions().Last<Tuple<PersuasionOptionArgs, PersuasionOptionResult>>();
				float difficulty = Campaign.Current.Models.PersuasionModel.GetDifficulty(PersuasionDifficulty.Hard);
				float moveToNextStageChance;
				float blockRandomOptionChance;
				Campaign.Current.Models.PersuasionModel.GetEffectChances(tuple.Item1, out moveToNextStageChance, out blockRandomOptionChance, difficulty);
				this._task.ApplyEffects(moveToNextStageChance, blockRandomOptionChance);
			}

			// Token: 0x06000E2B RID: 3627 RVA: 0x0005BAB8 File Offset: 0x00059CB8
			private bool persuasion_selected_option_response_on_condition()
			{
				PersuasionOptionResult item = ConversationManager.GetPersuasionChosenOptions().Last<Tuple<PersuasionOptionArgs, PersuasionOptionResult>>().Item2;
				MBTextManager.SetTextVariable("PERSUASION_REACTION", PersuasionHelper.GetDefaultPersuasionOptionReaction(item), false);
				if (item == PersuasionOptionResult.CriticalFailure)
				{
					this._task.BlockAllOptions();
				}
				return true;
			}

			// Token: 0x06000E2C RID: 3628 RVA: 0x0005BAF8 File Offset: 0x00059CF8
			private bool persuasion_select_option_1_on_condition()
			{
				if (this._task.Options.Count > 0)
				{
					TextObject textObject = new TextObject("{=bSo9hKwr}{PERSUASION_OPTION_LINE} {SUCCESS_CHANCE}", null);
					textObject.SetTextVariable("SUCCESS_CHANCE", PersuasionHelper.ShowSuccess(this._task.Options.ElementAt(0), false));
					textObject.SetTextVariable("PERSUASION_OPTION_LINE", this._task.Options.ElementAt(0).Line);
					MBTextManager.SetTextVariable("PERSUADE_GANG_ATTEMPT_1", textObject, false);
					return true;
				}
				return false;
			}

			// Token: 0x06000E2D RID: 3629 RVA: 0x0005BB78 File Offset: 0x00059D78
			private bool persuasion_select_option_2_on_condition()
			{
				if (this._task.Options.Count > 1)
				{
					TextObject textObject = new TextObject("{=bSo9hKwr}{PERSUASION_OPTION_LINE} {SUCCESS_CHANCE}", null);
					textObject.SetTextVariable("SUCCESS_CHANCE", PersuasionHelper.ShowSuccess(this._task.Options.ElementAt(1), false));
					textObject.SetTextVariable("PERSUASION_OPTION_LINE", this._task.Options.ElementAt(1).Line);
					MBTextManager.SetTextVariable("PERSUADE_GANG_ATTEMPT_2", textObject, false);
					return true;
				}
				return false;
			}

			// Token: 0x06000E2E RID: 3630 RVA: 0x0005BBF8 File Offset: 0x00059DF8
			private bool persuasion_select_option_3_on_condition()
			{
				if (this._task.Options.Count > 2)
				{
					TextObject textObject = new TextObject("{=bSo9hKwr}{PERSUASION_OPTION_LINE} {SUCCESS_CHANCE}", null);
					textObject.SetTextVariable("SUCCESS_CHANCE", PersuasionHelper.ShowSuccess(this._task.Options.ElementAt(2), false));
					textObject.SetTextVariable("PERSUASION_OPTION_LINE", this._task.Options.ElementAt(2).Line);
					MBTextManager.SetTextVariable("PERSUADE_GANG_ATTEMPT_3", textObject, false);
					return true;
				}
				return false;
			}

			// Token: 0x06000E2F RID: 3631 RVA: 0x0005BC78 File Offset: 0x00059E78
			private void persuasion_select_option_1_on_consequence()
			{
				if (this._task.Options.Count > 0)
				{
					this._task.Options[0].BlockTheOption(true);
				}
			}

			// Token: 0x06000E30 RID: 3632 RVA: 0x0005BCA4 File Offset: 0x00059EA4
			private void persuasion_select_option_2_on_consequence()
			{
				if (this._task.Options.Count > 1)
				{
					this._task.Options[1].BlockTheOption(true);
				}
			}

			// Token: 0x06000E31 RID: 3633 RVA: 0x0005BCD0 File Offset: 0x00059ED0
			private void persuasion_select_option_3_on_consequence()
			{
				if (this._task.Options.Count > 2)
				{
					this._task.Options[2].BlockTheOption(true);
				}
			}

			// Token: 0x06000E32 RID: 3634 RVA: 0x0005BCFC File Offset: 0x00059EFC
			private PersuasionOptionArgs persuasion_setup_option_1()
			{
				return this._task.Options.ElementAt(0);
			}

			// Token: 0x06000E33 RID: 3635 RVA: 0x0005BD0F File Offset: 0x00059F0F
			private PersuasionOptionArgs persuasion_setup_option_2()
			{
				return this._task.Options.ElementAt(1);
			}

			// Token: 0x06000E34 RID: 3636 RVA: 0x0005BD22 File Offset: 0x00059F22
			private PersuasionOptionArgs persuasion_setup_option_3()
			{
				return this._task.Options.ElementAt(2);
			}

			// Token: 0x06000E35 RID: 3637 RVA: 0x0005BD38 File Offset: 0x00059F38
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

			// Token: 0x06000E36 RID: 3638 RVA: 0x0005BDA4 File Offset: 0x00059FA4
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

			// Token: 0x06000E37 RID: 3639 RVA: 0x0005BE10 File Offset: 0x0005A010
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

			// Token: 0x06000E38 RID: 3640 RVA: 0x0005BE7B File Offset: 0x0005A07B
			private void persuasion_success_on_consequence()
			{
				ConversationManager.EndPersuasion();
				this.FinishQuestSuccess3();
			}

			// Token: 0x06000E39 RID: 3641 RVA: 0x0005BE88 File Offset: 0x0005A088
			private void OnEndHouseMissionDialog()
			{
				Campaign.Current.GameMenuManager.NextLocation = LocationComplex.Current.GetLocationWithId("center");
				Campaign.Current.GameMenuManager.PreviousLocation = CampaignMission.Current.Location;
				Mission.Current.EndMission();
				this.FinishQuestSuccess1();
			}

			// Token: 0x06000E3A RID: 3642 RVA: 0x0005BEDC File Offset: 0x0005A0DC
			private PersuasionTask GetPersuasionTask()
			{
				PersuasionTask persuasionTask = new PersuasionTask(0);
				persuasionTask.FinalFailLine = TextObject.Empty;
				persuasionTask.TryLaterLine = TextObject.Empty;
				persuasionTask.SpokenLine = new TextObject("{=6P1ruzsC}Maybe...", null);
				PersuasionOptionArgs option = new PersuasionOptionArgs(DefaultSkills.Charm, DefaultTraits.Calculating, TraitEffect.Positive, PersuasionArgumentStrength.ExtremelyHard, true, new TextObject("{=Lol4clzR}Look, it was a good try, but they're not going to pay. Releasing the kid is the only move that makes sense.", null), null, false, false, false);
				persuasionTask.AddOptionToTask(option);
				PersuasionOptionArgs option2 = new PersuasionOptionArgs(DefaultSkills.Roguery, DefaultTraits.Mercy, TraitEffect.Negative, PersuasionArgumentStrength.Hard, false, new TextObject("{=wJCVlVF7}These nobles aren't like you and me. They've kept their wealth by crushing people like you for generations. Don't mess with them.", null), null, false, false, false);
				persuasionTask.AddOptionToTask(option2);
				PersuasionOptionArgs option3 = new PersuasionOptionArgs(DefaultSkills.Roguery, DefaultTraits.Generosity, TraitEffect.Positive, PersuasionArgumentStrength.Normal, false, new TextObject("{=o1KOn4WZ}If you let this boy go, his family will remember you did them a favor. That's a better deal for you than a fight you can't hope to win.", null), null, false, false, false);
				persuasionTask.AddOptionToTask(option3);
				return persuasionTask;
			}

			// Token: 0x06000E3B RID: 3643 RVA: 0x0005BF92 File Offset: 0x0005A192
			private void persuasion_start_on_consequence()
			{
				ConversationManager.StartPersuasion(2f, 1f, 1f, 2f, 2f, 0f, PersuasionDifficulty.Hard);
			}

			// Token: 0x06000E3C RID: 3644 RVA: 0x0005BFB8 File Offset: 0x0005A1B8
			private void FinishQuestSuccess1()
			{
				base.CompleteQuestWithSuccess();
				base.AddLog(this.PlayerDefeatsThugsQuestSuccessLog, false);
				ChangeRelationAction.ApplyPlayerRelation(base.QuestGiver, 5, true, true);
				GainRenownAction.Apply(Hero.MainHero, 3f, false);
				GiveGoldAction.ApplyForQuestBetweenCharacters(base.QuestGiver, Hero.MainHero, this.RewardGold, false);
			}

			// Token: 0x06000E3D RID: 3645 RVA: 0x0005C010 File Offset: 0x0005A210
			private void FinishQuestSuccess3()
			{
				base.CompleteQuestWithSuccess();
				base.AddLog(this.PlayerConvincesGangLeaderQuestSuccessLog, false);
				ChangeRelationAction.ApplyPlayerRelation(base.QuestGiver, 5, true, true);
				GainRenownAction.Apply(Hero.MainHero, 1f, false);
				GiveGoldAction.ApplyForQuestBetweenCharacters(base.QuestGiver, Hero.MainHero, this.RewardGold, false);
			}

			// Token: 0x06000E3E RID: 3646 RVA: 0x0005C068 File Offset: 0x0005A268
			private void FinishQuestSuccess4()
			{
				GainRenownAction.Apply(Hero.MainHero, 1f, false);
				GiveGoldAction.ApplyForQuestBetweenCharacters(Hero.MainHero, this._targetHero, this.DebtWithInterest, false);
				base.CompleteQuestWithSuccess();
				base.AddLog(this.PlayerPaysTheDebtQuestSuccessLog, false);
				ChangeRelationAction.ApplyPlayerRelation(base.QuestGiver, 5, true, true);
				GiveGoldAction.ApplyForQuestBetweenCharacters(base.QuestGiver, Hero.MainHero, this.RewardGold, false);
			}

			// Token: 0x06000E3F RID: 3647 RVA: 0x0005C0D5 File Offset: 0x0005A2D5
			private void FinishQuestFail1()
			{
				base.AddLog(this.QuestTimeOutFailLog, false);
				ChangeRelationAction.ApplyPlayerRelation(base.QuestGiver, -5, true, true);
			}

			// Token: 0x06000E40 RID: 3648 RVA: 0x0005C0F4 File Offset: 0x0005A2F4
			private void FinishQuestFail2()
			{
				base.CompleteQuestWithFail(null);
				base.AddLog(this.PlayerHasDefeatedQuestFailLog, false);
				ChangeRelationAction.ApplyPlayerRelation(base.QuestGiver, -5, true, true);
			}

			// Token: 0x06000E41 RID: 3649 RVA: 0x0005C11A File Offset: 0x0005A31A
			internal static void AutoGeneratedStaticCollectObjectsProdigalSonIssueQuest(object o, List<object> collectedObjects)
			{
				((ProdigalSonIssueBehavior.ProdigalSonIssueQuest)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
			}

			// Token: 0x06000E42 RID: 3650 RVA: 0x0005C128 File Offset: 0x0005A328
			protected override void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
			{
				base.AutoGeneratedInstanceCollectObjects(collectedObjects);
				collectedObjects.Add(this._targetHero);
				collectedObjects.Add(this._prodigalSon);
				collectedObjects.Add(this._targetHouse);
			}

			// Token: 0x06000E43 RID: 3651 RVA: 0x0005C155 File Offset: 0x0005A355
			internal static object AutoGeneratedGetMemberValue_targetHero(object o)
			{
				return ((ProdigalSonIssueBehavior.ProdigalSonIssueQuest)o)._targetHero;
			}

			// Token: 0x06000E44 RID: 3652 RVA: 0x0005C162 File Offset: 0x0005A362
			internal static object AutoGeneratedGetMemberValue_prodigalSon(object o)
			{
				return ((ProdigalSonIssueBehavior.ProdigalSonIssueQuest)o)._prodigalSon;
			}

			// Token: 0x06000E45 RID: 3653 RVA: 0x0005C16F File Offset: 0x0005A36F
			internal static object AutoGeneratedGetMemberValue_playerTalkedToTargetHero(object o)
			{
				return ((ProdigalSonIssueBehavior.ProdigalSonIssueQuest)o)._playerTalkedToTargetHero;
			}

			// Token: 0x06000E46 RID: 3654 RVA: 0x0005C181 File Offset: 0x0005A381
			internal static object AutoGeneratedGetMemberValue_targetHouse(object o)
			{
				return ((ProdigalSonIssueBehavior.ProdigalSonIssueQuest)o)._targetHouse;
			}

			// Token: 0x06000E47 RID: 3655 RVA: 0x0005C18E File Offset: 0x0005A38E
			internal static object AutoGeneratedGetMemberValue_questDifficulty(object o)
			{
				return ((ProdigalSonIssueBehavior.ProdigalSonIssueQuest)o)._questDifficulty;
			}

			// Token: 0x06000E48 RID: 3656 RVA: 0x0005C1A0 File Offset: 0x0005A3A0
			internal static object AutoGeneratedGetMemberValue_isHouseFightFinished(object o)
			{
				return ((ProdigalSonIssueBehavior.ProdigalSonIssueQuest)o)._isHouseFightFinished;
			}

			// Token: 0x06000E49 RID: 3657 RVA: 0x0005C1B2 File Offset: 0x0005A3B2
			internal static object AutoGeneratedGetMemberValue_playerTriedToPersuade(object o)
			{
				return ((ProdigalSonIssueBehavior.ProdigalSonIssueQuest)o)._playerTriedToPersuade;
			}

			// Token: 0x04000613 RID: 1555
			private const PersuasionDifficulty Difficulty = PersuasionDifficulty.Hard;

			// Token: 0x04000614 RID: 1556
			private const int DistanceSquaredToStartConversation = 4;

			// Token: 0x04000615 RID: 1557
			private const int CrimeRatingCancelRelationshipPenalty = -5;

			// Token: 0x04000616 RID: 1558
			private const int CrimeRatingCancelHonorXpPenalty = -50;

			// Token: 0x04000617 RID: 1559
			[SaveableField(10)]
			private readonly Hero _targetHero;

			// Token: 0x04000618 RID: 1560
			[SaveableField(20)]
			private readonly Hero _prodigalSon;

			// Token: 0x04000619 RID: 1561
			[SaveableField(30)]
			private bool _playerTalkedToTargetHero;

			// Token: 0x0400061A RID: 1562
			[SaveableField(40)]
			private readonly Location _targetHouse;

			// Token: 0x0400061B RID: 1563
			[SaveableField(50)]
			private readonly float _questDifficulty;

			// Token: 0x0400061C RID: 1564
			[SaveableField(60)]
			private bool _isHouseFightFinished;

			// Token: 0x0400061D RID: 1565
			[SaveableField(70)]
			private bool _playerTriedToPersuade;

			// Token: 0x0400061E RID: 1566
			private PersuasionTask _task;

			// Token: 0x0400061F RID: 1567
			private bool _isMissionFightInitialized;

			// Token: 0x04000620 RID: 1568
			private bool _isFirstMissionTick;
		}
	}
}
