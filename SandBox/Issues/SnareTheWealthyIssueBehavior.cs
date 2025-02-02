using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.AgentOrigins;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.Issues;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Party.PartyComponents;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.SaveSystem;

namespace SandBox.Issues
{
	// Token: 0x0200008D RID: 141
	public class SnareTheWealthyIssueBehavior : CampaignBehaviorBase
	{
		// Token: 0x06000577 RID: 1399 RVA: 0x00024AD8 File Offset: 0x00022CD8
		public override void RegisterEvents()
		{
			CampaignEvents.OnCheckForIssueEvent.AddNonSerializedListener(this, new Action<Hero>(this.OnCheckForIssue));
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00024AF4 File Offset: 0x00022CF4
		private void OnCheckForIssue(Hero hero)
		{
			if (this.ConditionsHold(hero))
			{
				Campaign.Current.IssueManager.AddPotentialIssueData(hero, new PotentialIssueData(new PotentialIssueData.StartIssueDelegate(this.OnStartIssue), typeof(SnareTheWealthyIssueBehavior.SnareTheWealthyIssue), IssueBase.IssueFrequency.Rare, null));
				return;
			}
			Campaign.Current.IssueManager.AddPotentialIssueData(hero, new PotentialIssueData(typeof(SnareTheWealthyIssueBehavior.SnareTheWealthyIssue), IssueBase.IssueFrequency.Rare));
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00024B58 File Offset: 0x00022D58
		private bool ConditionsHold(Hero issueGiver)
		{
			return issueGiver.IsGangLeader && issueGiver.CurrentSettlement != null && issueGiver.CurrentSettlement.IsTown && issueGiver.CurrentSettlement.Town.Security <= 50f && this.GetTargetMerchant(issueGiver) != null;
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00024BA8 File Offset: 0x00022DA8
		private Hero GetTargetMerchant(Hero issueOwner)
		{
			Hero result = null;
			foreach (Hero hero in issueOwner.CurrentSettlement.Notables)
			{
				if (hero != issueOwner && hero.IsMerchant && hero.Power >= 150f && hero.GetTraitLevel(DefaultTraits.Mercy) + hero.GetTraitLevel(DefaultTraits.Honor) < 0 && hero.CanHaveQuestsOrIssues() && !Campaign.Current.IssueManager.HasIssueCoolDown(typeof(SnareTheWealthyIssueBehavior.SnareTheWealthyIssue), hero) && !Campaign.Current.IssueManager.HasIssueCoolDown(typeof(EscortMerchantCaravanIssueBehavior), hero) && !Campaign.Current.IssueManager.HasIssueCoolDown(typeof(CaravanAmbushIssueBehavior), hero))
				{
					result = hero;
					break;
				}
			}
			return result;
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00024C9C File Offset: 0x00022E9C
		private IssueBase OnStartIssue(in PotentialIssueData pid, Hero issueOwner)
		{
			Hero targetMerchant = this.GetTargetMerchant(issueOwner);
			return new SnareTheWealthyIssueBehavior.SnareTheWealthyIssue(issueOwner, targetMerchant.CharacterObject);
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00024CBD File Offset: 0x00022EBD
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x04000297 RID: 663
		private const IssueBase.IssueFrequency SnareTheWealthyIssueFrequency = IssueBase.IssueFrequency.Rare;

		// Token: 0x0200016E RID: 366
		public class SnareTheWealthyIssueTypeDefiner : SaveableTypeDefiner
		{
			// Token: 0x06000F2C RID: 3884 RVA: 0x0005F685 File Offset: 0x0005D885
			public SnareTheWealthyIssueTypeDefiner() : base(340000)
			{
			}

			// Token: 0x06000F2D RID: 3885 RVA: 0x0005F692 File Offset: 0x0005D892
			protected override void DefineClassTypes()
			{
				base.AddClassDefinition(typeof(SnareTheWealthyIssueBehavior.SnareTheWealthyIssue), 1, null);
				base.AddClassDefinition(typeof(SnareTheWealthyIssueBehavior.SnareTheWealthyIssueQuest), 2, null);
			}

			// Token: 0x06000F2E RID: 3886 RVA: 0x0005F6B8 File Offset: 0x0005D8B8
			protected override void DefineEnumTypes()
			{
				base.AddEnumDefinition(typeof(SnareTheWealthyIssueBehavior.SnareTheWealthyIssueQuest.SnareTheWealthyQuestChoice), 3, null);
			}
		}

		// Token: 0x0200016F RID: 367
		public class SnareTheWealthyIssue : IssueBase
		{
			// Token: 0x170001A8 RID: 424
			// (get) Token: 0x06000F2F RID: 3887 RVA: 0x0005F6CC File Offset: 0x0005D8CC
			private int AlternativeSolutionReward
			{
				get
				{
					return MathF.Floor(1000f + 3000f * base.IssueDifficultyMultiplier);
				}
			}

			// Token: 0x06000F30 RID: 3888 RVA: 0x0005F6E5 File Offset: 0x0005D8E5
			public SnareTheWealthyIssue(Hero issueOwner, CharacterObject targetMerchant) : base(issueOwner, CampaignTime.DaysFromNow(30f))
			{
				this._targetMerchantCharacter = targetMerchant;
			}

			// Token: 0x170001A9 RID: 425
			// (get) Token: 0x06000F31 RID: 3889 RVA: 0x0005F700 File Offset: 0x0005D900
			public override TextObject IssueBriefByIssueGiver
			{
				get
				{
					TextObject textObject = new TextObject("{=bLigh8Sd}Well, let's just say there's an idea I've been mulling over.[ib:confident2][if:convo_bemused] You may be able to help. Have you met {TARGET_MERCHANT.NAME}? {?TARGET_MERCHANT.GENDER}She{?}He{\\?} is a very rich merchant. Very rich indeed. But not very honest… It's not right that someone without morals should have so much wealth, is it? I have a plan to redistribute it a bit.", null);
					StringHelpers.SetCharacterProperties("TARGET_MERCHANT", this._targetMerchantCharacter, textObject, false);
					return textObject;
				}
			}

			// Token: 0x170001AA RID: 426
			// (get) Token: 0x06000F32 RID: 3890 RVA: 0x0005F72D File Offset: 0x0005D92D
			public override TextObject IssueAcceptByPlayer
			{
				get
				{
					return new TextObject("{=keKEFagm}So what's the plan?", null);
				}
			}

			// Token: 0x170001AB RID: 427
			// (get) Token: 0x06000F33 RID: 3891 RVA: 0x0005F73C File Offset: 0x0005D93C
			public override TextObject IssueQuestSolutionExplanationByIssueGiver
			{
				get
				{
					TextObject textObject = new TextObject("{=SliFGAX4}{TARGET_MERCHANT.NAME} is always looking for extra swords to protect[if:convo_evil_smile] {?TARGET_MERCHANT.GENDER}her{?}his{\\?} caravans. The wicked are the ones who fear wickedness the most, you might say. What if those guards turned out to be robbers? {TARGET_MERCHANT.NAME} wouldn't trust just anyone but I think {?TARGET_MERCHANT.GENDER}she{?}he{\\?} might hire a renowned warrior like yourself. And if that warrior were to lead the caravan into an ambush… Oh I suppose it's all a bit dishonorable, but I wouldn't worry too much about your reputation. {TARGET_MERCHANT.NAME} is known to defraud {?TARGET_MERCHANT.GENDER}her{?}his{\\?} partners. If something happened to one of {?TARGET_MERCHANT.GENDER}her{?}his{\\?} caravans - well, most people won't know who to believe, and won't really care either.", null);
					StringHelpers.SetCharacterProperties("TARGET_MERCHANT", this._targetMerchantCharacter, textObject, false);
					return textObject;
				}
			}

			// Token: 0x170001AC RID: 428
			// (get) Token: 0x06000F34 RID: 3892 RVA: 0x0005F769 File Offset: 0x0005D969
			public override TextObject IssueQuestSolutionAcceptByPlayer
			{
				get
				{
					return new TextObject("{=4upBpsnb}All right. I am in.", null);
				}
			}

			// Token: 0x170001AD RID: 429
			// (get) Token: 0x06000F35 RID: 3893 RVA: 0x0005F776 File Offset: 0x0005D976
			public override TextObject IssueAlternativeSolutionExplanationByIssueGiver
			{
				get
				{
					TextObject textObject = new TextObject("{=ivNVRP69}I prefer if you do this yourself, but one of your trusted companions with a strong[if:convo_evil_smile] sword-arm and enough brains to set an ambush can do the job with {TROOP_COUNT} fighters. We'll split the loot, and I'll throw in a little bonus on top of that for you..", null);
					textObject.SetTextVariable("TROOP_COUNT", base.GetTotalAlternativeSolutionNeededMenCount());
					return textObject;
				}
			}

			// Token: 0x170001AE RID: 430
			// (get) Token: 0x06000F36 RID: 3894 RVA: 0x0005F795 File Offset: 0x0005D995
			public override TextObject IssueAlternativeSolutionAcceptByPlayer
			{
				get
				{
					return new TextObject("{=biqYiCnr}My companion can handle it. Do not worry.", null);
				}
			}

			// Token: 0x170001AF RID: 431
			// (get) Token: 0x06000F37 RID: 3895 RVA: 0x0005F7A2 File Offset: 0x0005D9A2
			public override TextObject IssueAlternativeSolutionResponseByIssueGiver
			{
				get
				{
					return new TextObject("{=UURamhdC}Thank you. This should make both of us a pretty penny.[if:convo_delighted]", null);
				}
			}

			// Token: 0x170001B0 RID: 432
			// (get) Token: 0x06000F38 RID: 3896 RVA: 0x0005F7AF File Offset: 0x0005D9AF
			public override TextObject IssueDiscussAlternativeSolution
			{
				get
				{
					return new TextObject("{=pmuEeFV8}We are still arranging with your men how we'll spring this ambush. Do not worry. Everything will go smoothly.", null);
				}
			}

			// Token: 0x170001B1 RID: 433
			// (get) Token: 0x06000F39 RID: 3897 RVA: 0x0005F7BC File Offset: 0x0005D9BC
			protected override TextObject AlternativeSolutionStartLog
			{
				get
				{
					TextObject textObject = new TextObject("{=28lLrXOe}{ISSUE_GIVER.LINK} shared their plan for robbing {TARGET_MERCHANT.LINK} with you. You agreed to send your companion along with {TROOP_COUNT} men to lead the ambush for them. They will return after {RETURN_DAYS} days.", null);
					StringHelpers.SetCharacterProperties("ISSUE_GIVER", base.IssueOwner.CharacterObject, textObject, false);
					StringHelpers.SetCharacterProperties("TARGET_MERCHANT", this._targetMerchantCharacter, textObject, false);
					textObject.SetTextVariable("TROOP_COUNT", this.AlternativeSolutionSentTroops.TotalManCount - 1);
					textObject.SetTextVariable("RETURN_DAYS", base.GetTotalAlternativeSolutionDurationInDays());
					return textObject;
				}
			}

			// Token: 0x170001B2 RID: 434
			// (get) Token: 0x06000F3A RID: 3898 RVA: 0x0005F82C File Offset: 0x0005DA2C
			public override bool IsThereAlternativeSolution
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170001B3 RID: 435
			// (get) Token: 0x06000F3B RID: 3899 RVA: 0x0005F82F File Offset: 0x0005DA2F
			public override bool IsThereLordSolution
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170001B4 RID: 436
			// (get) Token: 0x06000F3C RID: 3900 RVA: 0x0005F832 File Offset: 0x0005DA32
			public override TextObject Title
			{
				get
				{
					return new TextObject("{=IeihUvCD}Snare The Wealthy", null);
				}
			}

			// Token: 0x170001B5 RID: 437
			// (get) Token: 0x06000F3D RID: 3901 RVA: 0x0005F840 File Offset: 0x0005DA40
			public override TextObject Description
			{
				get
				{
					TextObject textObject = new TextObject("{=8LghFfQO}Help {ISSUE_GIVER.NAME} to rob {TARGET_MERCHANT.NAME} by acting as their guard.", null);
					StringHelpers.SetCharacterProperties("TARGET_MERCHANT", this._targetMerchantCharacter, textObject, false);
					StringHelpers.SetCharacterProperties("ISSUE_GIVER", base.IssueOwner.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x170001B6 RID: 438
			// (get) Token: 0x06000F3E RID: 3902 RVA: 0x0005F885 File Offset: 0x0005DA85
			protected override bool IssueQuestCanBeDuplicated
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000F3F RID: 3903 RVA: 0x0005F888 File Offset: 0x0005DA88
			protected override float GetIssueEffectAmountInternal(IssueEffect issueEffect)
			{
				if (issueEffect == DefaultIssueEffects.SettlementLoyalty)
				{
					return -0.1f;
				}
				if (issueEffect == DefaultIssueEffects.SettlementSecurity)
				{
					return -0.5f;
				}
				return 0f;
			}

			// Token: 0x170001B7 RID: 439
			// (get) Token: 0x06000F40 RID: 3904 RVA: 0x0005F8AB File Offset: 0x0005DAAB
			public override IssueBase.AlternativeSolutionScaleFlag AlternativeSolutionScaleFlags
			{
				get
				{
					return IssueBase.AlternativeSolutionScaleFlag.Casualties | IssueBase.AlternativeSolutionScaleFlag.FailureRisk;
				}
			}

			// Token: 0x170001B8 RID: 440
			// (get) Token: 0x06000F41 RID: 3905 RVA: 0x0005F8B0 File Offset: 0x0005DAB0
			public override int AlternativeSolutionBaseNeededMenCount
			{
				get
				{
					return 10 + MathF.Ceiling(16f * base.IssueDifficultyMultiplier);
				}
			}

			// Token: 0x170001B9 RID: 441
			// (get) Token: 0x06000F42 RID: 3906 RVA: 0x0005F8C6 File Offset: 0x0005DAC6
			protected override int AlternativeSolutionBaseDurationInDaysInternal
			{
				get
				{
					return 2 + MathF.Ceiling(4f * base.IssueDifficultyMultiplier);
				}
			}

			// Token: 0x170001BA RID: 442
			// (get) Token: 0x06000F43 RID: 3907 RVA: 0x0005F8DB File Offset: 0x0005DADB
			protected override int CompanionSkillRewardXP
			{
				get
				{
					return (int)(800f + 1000f * base.IssueDifficultyMultiplier);
				}
			}

			// Token: 0x06000F44 RID: 3908 RVA: 0x0005F8F0 File Offset: 0x0005DAF0
			public override ValueTuple<SkillObject, int> GetAlternativeSolutionSkill(Hero hero)
			{
				return new ValueTuple<SkillObject, int>((hero.GetSkillValue(DefaultSkills.Roguery) >= hero.GetSkillValue(DefaultSkills.Tactics)) ? DefaultSkills.Roguery : DefaultSkills.Tactics, 120);
			}

			// Token: 0x06000F45 RID: 3909 RVA: 0x0005F91D File Offset: 0x0005DB1D
			public override bool AlternativeSolutionCondition(out TextObject explanation)
			{
				explanation = TextObject.Empty;
				return QuestHelper.CheckRosterForAlternativeSolution(MobileParty.MainParty.MemberRoster, base.GetTotalAlternativeSolutionNeededMenCount(), ref explanation, 2, false);
			}

			// Token: 0x06000F46 RID: 3910 RVA: 0x0005F93E File Offset: 0x0005DB3E
			public override bool IsTroopTypeNeededByAlternativeSolution(CharacterObject character)
			{
				return character.Tier >= 2;
			}

			// Token: 0x06000F47 RID: 3911 RVA: 0x0005F94C File Offset: 0x0005DB4C
			public override bool DoTroopsSatisfyAlternativeSolution(TroopRoster troopRoster, out TextObject explanation)
			{
				explanation = TextObject.Empty;
				return QuestHelper.CheckRosterForAlternativeSolution(troopRoster, base.GetTotalAlternativeSolutionNeededMenCount(), ref explanation, 2, false);
			}

			// Token: 0x06000F48 RID: 3912 RVA: 0x0005F964 File Offset: 0x0005DB64
			protected override void AlternativeSolutionEndWithSuccessConsequence()
			{
				TraitLevelingHelper.OnIssueSolvedThroughAlternativeSolution(Hero.MainHero, new Tuple<TraitObject, int>[]
				{
					new Tuple<TraitObject, int>(DefaultTraits.Honor, -50)
				});
				TraitLevelingHelper.OnIssueSolvedThroughAlternativeSolution(Hero.MainHero, new Tuple<TraitObject, int>[]
				{
					new Tuple<TraitObject, int>(DefaultTraits.Calculating, 50)
				});
				ChangeRelationAction.ApplyPlayerRelation(base.IssueOwner, 5, true, true);
				ChangeRelationAction.ApplyPlayerRelation(this._targetMerchantCharacter.HeroObject, -10, true, true);
				GiveGoldAction.ApplyBetweenCharacters(null, Hero.MainHero, this.AlternativeSolutionReward, false);
			}

			// Token: 0x06000F49 RID: 3913 RVA: 0x0005F9E4 File Offset: 0x0005DBE4
			protected override void AlternativeSolutionEndWithFailureConsequence()
			{
				TraitLevelingHelper.OnIssueSolvedThroughAlternativeSolution(Hero.MainHero, new Tuple<TraitObject, int>[]
				{
					new Tuple<TraitObject, int>(DefaultTraits.Honor, -100)
				});
				TraitLevelingHelper.OnIssueSolvedThroughAlternativeSolution(Hero.MainHero, new Tuple<TraitObject, int>[]
				{
					new Tuple<TraitObject, int>(DefaultTraits.Calculating, 100)
				});
				ChangeRelationAction.ApplyPlayerRelation(base.IssueOwner, -10, true, true);
				ChangeRelationAction.ApplyPlayerRelation(this._targetMerchantCharacter.HeroObject, -10, true, true);
			}

			// Token: 0x06000F4A RID: 3914 RVA: 0x0005FA52 File Offset: 0x0005DC52
			protected override void OnGameLoad()
			{
			}

			// Token: 0x06000F4B RID: 3915 RVA: 0x0005FA54 File Offset: 0x0005DC54
			protected override void HourlyTick()
			{
			}

			// Token: 0x06000F4C RID: 3916 RVA: 0x0005FA56 File Offset: 0x0005DC56
			protected override QuestBase GenerateIssueQuest(string questId)
			{
				return new SnareTheWealthyIssueBehavior.SnareTheWealthyIssueQuest(questId, base.IssueOwner, this._targetMerchantCharacter, base.IssueDifficultyMultiplier, CampaignTime.DaysFromNow(10f));
			}

			// Token: 0x06000F4D RID: 3917 RVA: 0x0005FA7C File Offset: 0x0005DC7C
			protected override void OnIssueFinalized()
			{
				if (base.IsSolvingWithQuest)
				{
					Campaign.Current.IssueManager.AddIssueCoolDownData(base.GetType(), new HeroRelatedIssueCoolDownData(this._targetMerchantCharacter.HeroObject, CampaignTime.DaysFromNow((float)Campaign.Current.Models.IssueModel.IssueOwnerCoolDownInDays)));
					Campaign.Current.IssueManager.AddIssueCoolDownData(typeof(EscortMerchantCaravanIssueBehavior.EscortMerchantCaravanIssueQuest), new HeroRelatedIssueCoolDownData(this._targetMerchantCharacter.HeroObject, CampaignTime.DaysFromNow((float)Campaign.Current.Models.IssueModel.IssueOwnerCoolDownInDays)));
					Campaign.Current.IssueManager.AddIssueCoolDownData(typeof(CaravanAmbushIssueBehavior.CaravanAmbushIssueQuest), new HeroRelatedIssueCoolDownData(this._targetMerchantCharacter.HeroObject, CampaignTime.DaysFromNow((float)Campaign.Current.Models.IssueModel.IssueOwnerCoolDownInDays)));
				}
			}

			// Token: 0x06000F4E RID: 3918 RVA: 0x0005FB59 File Offset: 0x0005DD59
			public override IssueBase.IssueFrequency GetFrequency()
			{
				return IssueBase.IssueFrequency.Rare;
			}

			// Token: 0x06000F4F RID: 3919 RVA: 0x0005FB5C File Offset: 0x0005DD5C
			protected override bool CanPlayerTakeQuestConditions(Hero issueGiver, out IssueBase.PreconditionFlags flag, out Hero relationHero, out SkillObject skill)
			{
				flag = IssueBase.PreconditionFlags.None;
				relationHero = null;
				skill = null;
				if (MobileParty.MainParty.MemberRoster.TotalHealthyCount < 20)
				{
					flag |= IssueBase.PreconditionFlags.NotEnoughTroops;
				}
				if (issueGiver.GetRelationWithPlayer() < -10f)
				{
					flag |= IssueBase.PreconditionFlags.Relation;
					relationHero = issueGiver;
				}
				if (issueGiver.CurrentSettlement.OwnerClan == Clan.PlayerClan)
				{
					flag |= IssueBase.PreconditionFlags.PlayerIsOwnerOfSettlement;
				}
				return flag == IssueBase.PreconditionFlags.None;
			}

			// Token: 0x06000F50 RID: 3920 RVA: 0x0005FBC7 File Offset: 0x0005DDC7
			public override bool IssueStayAliveConditions()
			{
				return base.IssueOwner.IsAlive && base.IssueOwner.CurrentSettlement.Town.Security <= 80f && this._targetMerchantCharacter.HeroObject.IsAlive;
			}

			// Token: 0x06000F51 RID: 3921 RVA: 0x0005FC04 File Offset: 0x0005DE04
			protected override void CompleteIssueWithTimedOutConsequences()
			{
			}

			// Token: 0x06000F52 RID: 3922 RVA: 0x0005FC06 File Offset: 0x0005DE06
			public override void OnHeroCanHaveQuestOrIssueInfoIsRequested(Hero hero, ref bool result)
			{
				if (hero == this._targetMerchantCharacter.HeroObject)
				{
					result = false;
				}
			}

			// Token: 0x06000F53 RID: 3923 RVA: 0x0005FC19 File Offset: 0x0005DE19
			internal static void AutoGeneratedStaticCollectObjectsSnareTheWealthyIssue(object o, List<object> collectedObjects)
			{
				((SnareTheWealthyIssueBehavior.SnareTheWealthyIssue)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
			}

			// Token: 0x06000F54 RID: 3924 RVA: 0x0005FC27 File Offset: 0x0005DE27
			protected override void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
			{
				base.AutoGeneratedInstanceCollectObjects(collectedObjects);
				collectedObjects.Add(this._targetMerchantCharacter);
			}

			// Token: 0x06000F55 RID: 3925 RVA: 0x0005FC3C File Offset: 0x0005DE3C
			internal static object AutoGeneratedGetMemberValue_targetMerchantCharacter(object o)
			{
				return ((SnareTheWealthyIssueBehavior.SnareTheWealthyIssue)o)._targetMerchantCharacter;
			}

			// Token: 0x0400066B RID: 1643
			private const int IssueDuration = 30;

			// Token: 0x0400066C RID: 1644
			private const int IssueQuestDuration = 10;

			// Token: 0x0400066D RID: 1645
			private const int MinimumRequiredMenCount = 20;

			// Token: 0x0400066E RID: 1646
			private const int MinimumRequiredRelationWithIssueGiver = -10;

			// Token: 0x0400066F RID: 1647
			private const int AlternativeSolutionMinimumTroopTier = 2;

			// Token: 0x04000670 RID: 1648
			private const int CompanionRoguerySkillValueThreshold = 120;

			// Token: 0x04000671 RID: 1649
			[SaveableField(1)]
			private readonly CharacterObject _targetMerchantCharacter;
		}

		// Token: 0x02000170 RID: 368
		public class SnareTheWealthyIssueQuest : QuestBase
		{
			// Token: 0x170001BB RID: 443
			// (get) Token: 0x06000F56 RID: 3926 RVA: 0x0005FC49 File Offset: 0x0005DE49
			private int CaravanPartyTroopCount
			{
				get
				{
					return 20 + MathF.Ceiling(40f * this._questDifficulty);
				}
			}

			// Token: 0x170001BC RID: 444
			// (get) Token: 0x06000F57 RID: 3927 RVA: 0x0005FC5F File Offset: 0x0005DE5F
			private int GangPartyTroopCount
			{
				get
				{
					return 10 + MathF.Ceiling(25f * this._questDifficulty);
				}
			}

			// Token: 0x170001BD RID: 445
			// (get) Token: 0x06000F58 RID: 3928 RVA: 0x0005FC75 File Offset: 0x0005DE75
			private int Reward1
			{
				get
				{
					return MathF.Floor(1000f + 3000f * this._questDifficulty);
				}
			}

			// Token: 0x170001BE RID: 446
			// (get) Token: 0x06000F59 RID: 3929 RVA: 0x0005FC8E File Offset: 0x0005DE8E
			private int Reward2
			{
				get
				{
					return MathF.Floor((float)this.Reward1 * 0.4f);
				}
			}

			// Token: 0x06000F5A RID: 3930 RVA: 0x0005FCA2 File Offset: 0x0005DEA2
			public SnareTheWealthyIssueQuest(string questId, Hero questGiver, CharacterObject targetMerchantCharacter, float questDifficulty, CampaignTime duration) : base(questId, questGiver, duration, 0)
			{
				this._targetMerchantCharacter = targetMerchantCharacter;
				this._targetSettlement = this.GetTargetSettlement();
				this._questDifficulty = questDifficulty;
				this.SetDialogs();
				base.InitializeQuestOnCreation();
			}

			// Token: 0x170001BF RID: 447
			// (get) Token: 0x06000F5B RID: 3931 RVA: 0x0005FCDD File Offset: 0x0005DEDD
			public override TextObject Title
			{
				get
				{
					return new TextObject("{=IeihUvCD}Snare The Wealthy", null);
				}
			}

			// Token: 0x170001C0 RID: 448
			// (get) Token: 0x06000F5C RID: 3932 RVA: 0x0005FCEA File Offset: 0x0005DEEA
			public override bool IsRemainingTimeHidden
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170001C1 RID: 449
			// (get) Token: 0x06000F5D RID: 3933 RVA: 0x0005FCF0 File Offset: 0x0005DEF0
			private TextObject _questStartedLogText
			{
				get
				{
					TextObject textObject = new TextObject("{=Ba9nsfHc}{QUEST_GIVER.LINK} shared their plan for robbing {TARGET_MERCHANT.LINK} with you. You agreed to talk with {TARGET_MERCHANT.LINK} to convince {?TARGET_MERCHANT.GENDER}her{?}him{\\?} to guard {?TARGET_MERCHANT.GENDER}her{?}his{\\?} caravan and lead the caravan to ambush around {TARGET_SETTLEMENT}.", null);
					StringHelpers.SetCharacterProperties("TARGET_MERCHANT", this._targetMerchantCharacter, textObject, false);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					textObject.SetTextVariable("TARGET_SETTLEMENT", this._targetSettlement.EncyclopediaLinkWithName);
					return textObject;
				}
			}

			// Token: 0x170001C2 RID: 450
			// (get) Token: 0x06000F5E RID: 3934 RVA: 0x0005FD4C File Offset: 0x0005DF4C
			private TextObject _success1LogText
			{
				get
				{
					TextObject textObject = new TextObject("{=bblwaDi1}You have successfully robbed {TARGET_MERCHANT.LINK}'s caravan with {QUEST_GIVER.LINK}.", null);
					StringHelpers.SetCharacterProperties("TARGET_MERCHANT", this._targetMerchantCharacter, textObject, false);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x170001C3 RID: 451
			// (get) Token: 0x06000F5F RID: 3935 RVA: 0x0005FD94 File Offset: 0x0005DF94
			private TextObject _sidedWithGangLogText
			{
				get
				{
					TextObject textObject = new TextObject("{=lZjj3MZg}When {QUEST_GIVER.LINK} arrived, you kept your side of the bargain and attacked the caravan", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x170001C4 RID: 452
			// (get) Token: 0x06000F60 RID: 3936 RVA: 0x0005FDC8 File Offset: 0x0005DFC8
			private TextObject _timedOutWithoutTalkingToMerchantText
			{
				get
				{
					TextObject textObject = new TextObject("{=OMKgidoP}You have failed to convince the merchant to guard {?TARGET_MERCHANT.GENDER}her{?}his{\\?} caravan in time. {QUEST_GIVER.LINK} must be furious.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					StringHelpers.SetCharacterProperties("TARGET_MERCHANT", this._targetMerchantCharacter, textObject, false);
					return textObject;
				}
			}

			// Token: 0x170001C5 RID: 453
			// (get) Token: 0x06000F61 RID: 3937 RVA: 0x0005FE0D File Offset: 0x0005E00D
			private TextObject _fail1LogText
			{
				get
				{
					return new TextObject("{=DRpcqEMI}The caravan leader said your decisions were wasting their time and decided to go on his way. You have failed to uphold your part in the plan.", null);
				}
			}

			// Token: 0x170001C6 RID: 454
			// (get) Token: 0x06000F62 RID: 3938 RVA: 0x0005FE1A File Offset: 0x0005E01A
			private TextObject _fail2LogText
			{
				get
				{
					return new TextObject("{=EFjas6hI}At the last moment, you decided to side with the caravan guard and defend them.", null);
				}
			}

			// Token: 0x170001C7 RID: 455
			// (get) Token: 0x06000F63 RID: 3939 RVA: 0x0005FE28 File Offset: 0x0005E028
			private TextObject _fail2OutcomeLogText
			{
				get
				{
					TextObject textObject = new TextObject("{=JgrG0uoO}Having the {TARGET_MERCHANT.LINK} by your side, you were successful in protecting the caravan.", null);
					StringHelpers.SetCharacterProperties("TARGET_MERCHANT", this._targetMerchantCharacter, textObject, false);
					return textObject;
				}
			}

			// Token: 0x170001C8 RID: 456
			// (get) Token: 0x06000F64 RID: 3940 RVA: 0x0005FE55 File Offset: 0x0005E055
			private TextObject _fail3LogText
			{
				get
				{
					return new TextObject("{=0NxiTi8b}You didn't feel like splitting the loot, so you betrayed both the merchant and the gang leader.", null);
				}
			}

			// Token: 0x170001C9 RID: 457
			// (get) Token: 0x06000F65 RID: 3941 RVA: 0x0005FE62 File Offset: 0x0005E062
			private TextObject _fail3OutcomeLogText
			{
				get
				{
					return new TextObject("{=KbMew14D}Although the gang leader and the caravaneer joined their forces, you have successfully defeated them and kept the loot for yourself.", null);
				}
			}

			// Token: 0x170001CA RID: 458
			// (get) Token: 0x06000F66 RID: 3942 RVA: 0x0005FE70 File Offset: 0x0005E070
			private TextObject _fail4LogText
			{
				get
				{
					TextObject textObject = new TextObject("{=22nahm29}You have lost the battle against the merchant's caravan and failed to help {QUEST_GIVER.LINK}.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x170001CB RID: 459
			// (get) Token: 0x06000F67 RID: 3943 RVA: 0x0005FEA4 File Offset: 0x0005E0A4
			private TextObject _fail5LogText
			{
				get
				{
					TextObject textObject = new TextObject("{=QEgzLRnC}You have lost the battle against {QUEST_GIVER.LINK} and failed to help the merchant as you promised.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x170001CC RID: 460
			// (get) Token: 0x06000F68 RID: 3944 RVA: 0x0005FED8 File Offset: 0x0005E0D8
			private TextObject _fail6LogText
			{
				get
				{
					TextObject textObject = new TextObject("{=pGu2mcar}You have lost the battle against the combined forces of the {QUEST_GIVER.LINK} and the caravan.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x170001CD RID: 461
			// (get) Token: 0x06000F69 RID: 3945 RVA: 0x0005FF0A File Offset: 0x0005E10A
			private TextObject _playerCapturedQuestSettlementLogText
			{
				get
				{
					return new TextObject("{=gPFfHluf}Your clan is now owner of the settlement. As the lord of the settlement you cannot be part of the criminal activities anymore. Your agreement with the questgiver has canceled.", null);
				}
			}

			// Token: 0x170001CE RID: 462
			// (get) Token: 0x06000F6A RID: 3946 RVA: 0x0005FF18 File Offset: 0x0005E118
			private TextObject _questSettlementWasCapturedLogText
			{
				get
				{
					TextObject textObject = new TextObject("{=uVigJ3LP}{QUEST_GIVER.LINK} has lost the control of {SETTLEMENT} and the deal is now invalid.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					textObject.SetTextVariable("SETTLEMENT", base.QuestGiver.CurrentSettlement.EncyclopediaLinkWithName);
					return textObject;
				}
			}

			// Token: 0x170001CF RID: 463
			// (get) Token: 0x06000F6B RID: 3947 RVA: 0x0005FF68 File Offset: 0x0005E168
			private TextObject _warDeclaredBetweenPlayerAndQuestGiverLogText
			{
				get
				{
					TextObject textObject = new TextObject("{=ojpW4WRD}Your clan is now at war with the {QUEST_GIVER.LINK}'s lord. Your agreement with {QUEST_GIVER.LINK} was canceled.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x170001D0 RID: 464
			// (get) Token: 0x06000F6C RID: 3948 RVA: 0x0005FF9C File Offset: 0x0005E19C
			private TextObject _targetSettlementRaidedLogText
			{
				get
				{
					TextObject textObject = new TextObject("{=QkbkesNJ}{QUEST_GIVER.LINK} called off the ambush after {TARGET_SETTLEMENT} was raided.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					textObject.SetTextVariable("TARGET_SETTLEMENT", this._targetSettlement.EncyclopediaLinkWithName);
					return textObject;
				}
			}

			// Token: 0x170001D1 RID: 465
			// (get) Token: 0x06000F6D RID: 3949 RVA: 0x0005FFE8 File Offset: 0x0005E1E8
			private TextObject _talkedToMerchantLogText
			{
				get
				{
					TextObject textObject = new TextObject("{=N1ZiaLRL}You talked to {TARGET_MERCHANT.LINK} as {QUEST_GIVER.LINK} asked. The caravan is waiting for you outside the gates to be escorted to {TARGET_SETTLEMENT}.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					StringHelpers.SetCharacterProperties("TARGET_MERCHANT", this._targetMerchantCharacter, textObject, false);
					textObject.SetTextVariable("TARGET_SETTLEMENT", this._targetSettlement.EncyclopediaLinkWithName);
					return textObject;
				}
			}

			// Token: 0x06000F6E RID: 3950 RVA: 0x00060044 File Offset: 0x0005E244
			protected override void InitializeQuestOnGameLoad()
			{
				this.SetDialogs();
				Campaign.Current.ConversationManager.AddDialogFlow(this.GetEncounterDialogue(), this);
				Campaign.Current.ConversationManager.AddDialogFlow(this.GetDialogueWithMerchant(), this);
				Campaign.Current.ConversationManager.AddDialogFlow(this.GetDialogueWithCaravan(), this);
				Campaign.Current.ConversationManager.AddDialogFlow(this.GetDialogueWithGangWithoutCaravan(), this);
			}

			// Token: 0x06000F6F RID: 3951 RVA: 0x000600B0 File Offset: 0x0005E2B0
			private Settlement GetTargetSettlement()
			{
				MapDistanceModel model = Campaign.Current.Models.MapDistanceModel;
				return (from t in Settlement.All
				where t != this.QuestGiver.CurrentSettlement && t.IsTown
				orderby model.GetDistance(t, this.QuestGiver.CurrentSettlement)
				select t).ElementAt(0).BoundVillages.GetRandomElement<Village>().Settlement;
			}

			// Token: 0x06000F70 RID: 3952 RVA: 0x0006011C File Offset: 0x0005E31C
			protected override void SetDialogs()
			{
				TextObject discussIntroDialogue = new TextObject("{=lOFR5sq6}Have you talked with {TARGET_MERCHANT.NAME}? It would be a damned waste if we waited too long and word of our plans leaked out.", null);
				TextObject textObject = new TextObject("{=cc4EEDMg}Splendid. Go have a word with {TARGET_MERCHANT.LINK}. [if:convo_focused_happy]If you can convince {?TARGET_MERCHANT.GENDER}her{?}him{\\?} to guide the caravan, we will wait in ambush along their route.", null);
				StringHelpers.SetCharacterProperties("TARGET_MERCHANT", this._targetMerchantCharacter, textObject, false);
				this.OfferDialogFlow = DialogFlow.CreateDialogFlow("issue_classic_quest_start", 100).NpcLine(textObject, null, null).Condition(() => Hero.OneToOneConversationHero == this.QuestGiver).Consequence(new ConversationSentence.OnConsequenceDelegate(this.OnQuestAccepted)).CloseDialog();
				this.DiscussDialogFlow = DialogFlow.CreateDialogFlow("quest_discuss", 100).NpcLine(discussIntroDialogue, null, null).Condition(delegate
				{
					StringHelpers.SetCharacterProperties("TARGET_MERCHANT", this._targetMerchantCharacter, discussIntroDialogue, false);
					return Hero.OneToOneConversationHero == this.QuestGiver;
				}).BeginPlayerOptions().PlayerOption("{=YuabHAbV}I'll take care of it shortly..", null).NpcLine("{=CDXUehf0}Good, good.", null, null).CloseDialog().PlayerOption("{=2haJj9mp}I have but I need to deal with some other problems before leading the caravan.", null).NpcLine("{=bSDIHQzO}Please do so. Hate to have word leak out.[if:convo_nervous]", null, null).CloseDialog().EndPlayerOptions().CloseDialog();
			}

			// Token: 0x06000F71 RID: 3953 RVA: 0x00060220 File Offset: 0x0005E420
			private DialogFlow GetDialogueWithMerchant()
			{
				TextObject npcText = new TextObject("{=OJtUNAbN}Very well. You'll find the caravan [if:convo_calm_friendly]getting ready outside the gates. You will get your payment after the job. Good luck, friend.", null);
				return DialogFlow.CreateDialogFlow("hero_main_options", 125).BeginPlayerOptions().PlayerOption(new TextObject("{=K1ICRis9}I have heard you are looking for extra swords to protect your caravan. I am here to offer my services.", null), null).Condition(() => Hero.OneToOneConversationHero == this._targetMerchantCharacter.HeroObject && this._caravanParty == null).NpcLine("{=ltbu3S63}Yes, you have heard correctly. I am looking for a capable [if:convo_astonished]leader with a good number of followers. You only need to escort the caravan until they reach {TARGET_SETTLEMENT}. A simple job, but the cargo is very important. I'm willing to pay {MERCHANT_REWARD} denars. And of course, if you betrayed me...", null, null).Condition(delegate
				{
					MBTextManager.SetTextVariable("TARGET_SETTLEMENT", this._targetSettlement.EncyclopediaLinkWithName, false);
					MBTextManager.SetTextVariable("MERCHANT_REWARD", this.Reward2);
					return true;
				}).Consequence(new ConversationSentence.OnConsequenceDelegate(this.SpawnQuestParties)).BeginPlayerOptions().PlayerOption("{=AGnd7nDb}Worry not. The outlaws in these parts know my name well, and fear it.", null).NpcLine(npcText, null, null).CloseDialog().PlayerOption("{=RCsbpizl}If you have the denars we'll do the job,.", null).NpcLine(npcText, null, null).CloseDialog().PlayerOption("{=TfDomerj}I think my men and I are more than enough to protect the caravan, good {?TARGET_MERCHANT.GENDER}madam{?}sir{\\?}.", null).Condition(delegate
				{
					StringHelpers.SetCharacterProperties("TARGET_MERCHANT", this._targetMerchantCharacter, null, false);
					return true;
				}).NpcLine(npcText, null, null).CloseDialog().EndPlayerOptions().EndPlayerOptions().CloseDialog();
			}

			// Token: 0x06000F72 RID: 3954 RVA: 0x00060308 File Offset: 0x0005E508
			private DialogFlow GetDialogueWithCaravan()
			{
				return DialogFlow.CreateDialogFlow("start", 125).NpcLine("{=Xs7Qweuw}Lead the way, {PLAYER.NAME}.", null, null).Condition(() => MobileParty.ConversationParty == this._caravanParty && this._caravanParty != null && !this._canEncounterConversationStart).Consequence(delegate
				{
					PlayerEncounter.LeaveEncounter = true;
				}).CloseDialog();
			}

			// Token: 0x06000F73 RID: 3955 RVA: 0x00060368 File Offset: 0x0005E568
			private DialogFlow GetDialogueWithGangWithoutCaravan()
			{
				return DialogFlow.CreateDialogFlow("start", 125).NpcLine("{=F44s8kPB}Where is the caravan? My men can't wait here for too long.[if:convo_undecided_open]", null, null).Condition(() => MobileParty.ConversationParty == this._gangParty && this._gangParty != null && !this._canEncounterConversationStart).BeginPlayerOptions().PlayerOption("{=Yqv1jk7D}Don't worry, they are coming towards our trap.", null).NpcLine("{=fHc6fwrb}Good, let's finish this.", null, null).CloseDialog().EndPlayerOptions().CloseDialog();
			}

			// Token: 0x06000F74 RID: 3956 RVA: 0x000603CC File Offset: 0x0005E5CC
			private DialogFlow GetEncounterDialogue()
			{
				return DialogFlow.CreateDialogFlow("start", 125).NpcLine("{=vVH7wT07}Who are these men? Be on your guard {PLAYER.NAME}, I smell trouble![if:convo_confused_annoyed]", null, null).Condition(() => MobileParty.ConversationParty == this._caravanParty && this._caravanParty != null && this._canEncounterConversationStart).Consequence(delegate
				{
					StringHelpers.SetCharacterProperties("TARGET_MERCHANT", this._targetMerchantCharacter, null, false);
					AgentBuildData agentBuildData = new AgentBuildData(ConversationHelper.GetConversationCharacterPartyLeader(this._gangParty.Party));
					agentBuildData.TroopOrigin(new SimpleAgentOrigin(agentBuildData.AgentCharacter, -1, null, default(UniqueTroopDescriptor)));
					Vec3 v = Agent.Main.LookDirection * 10f;
					v.RotateAboutZ(1.3962634f);
					AgentBuildData agentBuildData2 = agentBuildData;
					Vec3 vec = Agent.Main.Position + v;
					agentBuildData2.InitialPosition(vec);
					AgentBuildData agentBuildData3 = agentBuildData;
					vec = Agent.Main.LookDirection;
					Vec2 vec2 = vec.AsVec2;
					vec2 = -vec2.Normalized();
					agentBuildData3.InitialDirection(vec2);
					Agent item = Mission.Current.SpawnAgent(agentBuildData, false);
					Campaign.Current.ConversationManager.AddConversationAgents(new List<IAgent>
					{
						item
					}, true);
				}).NpcLine("{=LJ2AoQyS}Well, well. What do we have here? Must be one of our lucky days, [if:convo_huge_smile]huh? Release all the valuables you carry and nobody gets hurt.", new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsGangPartyLeader), new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsCaravanMaster)).NpcLine("{=SdgDF4OZ}Hah! You're making a big mistake. See that group of men over there, [if:convo_excited]led by the warrior {PLAYER.NAME}? They're with us, and they'll cut you open.", new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsCaravanMaster), new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsGangPartyLeader)).NpcLine("{=LaHWB3r0}Oh… I'm afraid there's been a misunderstanding. {PLAYER.NAME} is with us, you see.[if:convo_evil_smile] Did {TARGET_MERCHANT.LINK} stuff you with lies and then send you out to your doom? Oh, shameful, shameful. {?TARGET_MERCHANT.GENDER}She{?}He{\\?} does that fairly often, unfortunately.", new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsGangPartyLeader), new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsCaravanMaster)).NpcLine("{=EGC4BA4h}{PLAYER.NAME}! Is this true? Look, you're a smart {?PLAYER.GENDER}woman{?}man{\\?}. [if:convo_shocked]You know that {TARGET_MERCHANT.LINK} can pay more than these scum. Take the money and keep your reputation.", new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsCaravanMaster), new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsMainHero)).NpcLine("{=zUKqWeUa}Come on, {PLAYER.NAME}. All this back-and-forth  is making me anxious. Let's finish this.[if:convo_nervous]", new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsGangPartyLeader), new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsMainHero)).BeginPlayerOptions().PlayerOption("{=UEY5aQ2l}I'm here to rob {TARGET_MERCHANT.NAME}, not be {?TARGET_MERCHANT.GENDER}her{?}his{\\?} lackey. Now, cough up the goods or fight.", new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsGangPartyLeader)).NpcLine("{=tHUHfe6C}You're with them? This is the basest treachery I have ever witnessed![if:convo_furious]", new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsCaravanMaster), new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsMainHero)).Consequence(delegate
				{
					base.AddLog(this._sidedWithGangLogText, false);
				}).NpcLine("{=IKeZLbIK}No offense, captain, but if that's the case you need to get out more. [if:convo_mocking_teasing]Anyway, shall we go to it?", new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsGangPartyLeader), new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsMainHero)).Consequence(delegate
				{
					this.StartBattle(SnareTheWealthyIssueBehavior.SnareTheWealthyIssueQuest.SnareTheWealthyQuestChoice.SidedWithGang);
				}).CloseDialog().PlayerOption("{=W7TD4yTc}You know, {TARGET_MERCHANT.NAME}'s man makes a good point. I'm guarding this caravan.", new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsGangPartyLeader)).NpcLine("{=VXp0R7da}Heaven protect you! I knew you'd never be tempted by such a perfidious offer.[if:convo_huge_smile]", new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsCaravanMaster), new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsMainHero)).Consequence(delegate
				{
					base.AddLog(this._fail2LogText, false);
				}).NpcLine("{=XJOqws2b}Hmf. A funny sense of honor you have… Anyway, I'm not going home empty handed, so let's do this.[if:convo_furious]", new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsGangPartyLeader), new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsMainHero)).Consequence(delegate
				{
					this.StartBattle(SnareTheWealthyIssueBehavior.SnareTheWealthyIssueQuest.SnareTheWealthyQuestChoice.SidedWithCaravan);
				}).CloseDialog().PlayerOption("{=ILrYPvTV}You know, I think I'd prefer to take all the loot for myself.", new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsGangPartyLeader)).NpcLine("{=cpTMttNb}Is that so? Hey, caravan captain, whatever your name is… [if:convo_contemptuous]As long as we're all switching sides here, how about I join with you to defeat this miscreant who just betrayed both of us? Whichever of us comes out of this with the most men standing keeps your goods.", new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsGangPartyLeader), new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsMainHero)).Consequence(delegate
				{
					base.AddLog(this._fail3LogText, false);
				}).NpcLine("{=15UCTrNA}I have no choice, do I? Well, better an honest robber than a traitor![if:convo_aggressive] Let's take {?PLAYER.GENDER}her{?}him{\\?} down.", new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsCaravanMaster), new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsMainHero)).Consequence(delegate
				{
					this.StartBattle(SnareTheWealthyIssueBehavior.SnareTheWealthyIssueQuest.SnareTheWealthyQuestChoice.BetrayedBoth);
				}).CloseDialog().EndPlayerOptions().CloseDialog();
			}

			// Token: 0x06000F75 RID: 3957 RVA: 0x00060650 File Offset: 0x0005E850
			private void OnQuestAccepted()
			{
				base.StartQuest();
				base.AddLog(this._questStartedLogText, false);
				base.AddTrackedObject(this._targetMerchantCharacter.HeroObject);
				Campaign.Current.ConversationManager.AddDialogFlow(this.GetEncounterDialogue(), this);
				Campaign.Current.ConversationManager.AddDialogFlow(this.GetDialogueWithMerchant(), this);
				Campaign.Current.ConversationManager.AddDialogFlow(this.GetDialogueWithCaravan(), this);
				Campaign.Current.ConversationManager.AddDialogFlow(this.GetDialogueWithGangWithoutCaravan(), this);
			}

			// Token: 0x06000F76 RID: 3958 RVA: 0x000606DC File Offset: 0x0005E8DC
			public void GetMountAndHarnessVisualIdsForQuestCaravan(CultureObject culture, out string mountStringId, out string harnessStringId)
			{
				if (culture.StringId == "khuzait" || culture.StringId == "aserai")
				{
					mountStringId = "camel";
					harnessStringId = "camel_saddle_b";
					return;
				}
				mountStringId = "mule";
				harnessStringId = "mule_load_c";
			}

			// Token: 0x06000F77 RID: 3959 RVA: 0x0006072C File Offset: 0x0005E92C
			private void SpawnQuestParties()
			{
				TextObject textObject = new TextObject("{=Bh4sZo9o}Caravan of {TARGET_MERCHANT}", null);
				textObject.SetTextVariable("TARGET_MERCHANT", this._targetMerchantCharacter.HeroObject.Name);
				string partyMountStringId;
				string partyHarnessStringId;
				this.GetMountAndHarnessVisualIdsForQuestCaravan(this._targetMerchantCharacter.Culture, out partyMountStringId, out partyHarnessStringId);
				this._caravanParty = CustomPartyComponent.CreateQuestParty(this._targetMerchantCharacter.HeroObject.CurrentSettlement.GatePosition, 0.1f, this._targetMerchantCharacter.HeroObject.CurrentSettlement, textObject, this._targetMerchantCharacter.HeroObject.Clan, this._targetMerchantCharacter.HeroObject.Culture.CaravanPartyTemplate, this._targetMerchantCharacter.HeroObject, this.CaravanPartyTroopCount, partyMountStringId, partyHarnessStringId, MobileParty.MainParty.Speed, false);
				this._caravanParty.MemberRoster.AddToCounts(this._targetMerchantCharacter.Culture.CaravanMaster, 1, false, 0, 0, true, -1);
				this._caravanParty.ItemRoster.AddToCounts(Game.Current.ObjectManager.GetObject<ItemObject>("grain"), 40);
				this._caravanParty.IgnoreByOtherPartiesTill(base.QuestDueTime);
				SetPartyAiAction.GetActionForEscortingParty(this._caravanParty, MobileParty.MainParty);
				this._caravanParty.Ai.SetDoNotMakeNewDecisions(true);
				this._caravanParty.SetPartyUsedByQuest(true);
				base.AddTrackedObject(this._caravanParty);
				MobilePartyHelper.TryMatchPartySpeedWithItemWeight(this._caravanParty, MobileParty.MainParty.Speed * 1.5f, null);
				Settlement closestHideout = SettlementHelper.FindNearestHideout((Settlement x) => x.IsActive, null);
				Clan clan = Clan.BanditFactions.FirstOrDefault((Clan t) => t.Culture == closestHideout.Culture);
				Vec2 gatePosition = this._targetSettlement.GatePosition;
				PartyTemplateObject partyTemplate = Campaign.Current.ObjectManager.GetObject<PartyTemplateObject>("kingdom_hero_party_caravan_ambushers") ?? base.QuestGiver.Culture.BanditBossPartyTemplate;
				this._gangParty = CustomPartyComponent.CreateQuestParty(gatePosition, 0.1f, this._targetSettlement, new TextObject("{=gJNdkwHV}Gang Party", null), null, partyTemplate, base.QuestGiver, this.GangPartyTroopCount, "", "", 0f, false);
				this._gangParty.MemberRoster.AddToCounts(clan.Culture.BanditBoss, 1, true, 0, 0, true, -1);
				this._gangParty.ItemRoster.AddToCounts(Game.Current.ObjectManager.GetObject<ItemObject>("grain"), 40);
				this._gangParty.SetPartyUsedByQuest(true);
				this._gangParty.IgnoreByOtherPartiesTill(base.QuestDueTime);
				this._gangParty.Ai.SetDoNotMakeNewDecisions(true);
				this._gangParty.Ai.DisableAi();
				MobilePartyHelper.TryMatchPartySpeedWithItemWeight(this._gangParty, 0.2f, null);
				this._gangParty.Ai.SetMoveGoToSettlement(this._targetSettlement);
				EnterSettlementAction.ApplyForParty(this._gangParty, this._targetSettlement);
				base.AddTrackedObject(this._targetSettlement);
				base.AddLog(this._talkedToMerchantLogText, false);
			}

			// Token: 0x06000F78 RID: 3960 RVA: 0x00060A40 File Offset: 0x0005EC40
			private void StartBattle(SnareTheWealthyIssueBehavior.SnareTheWealthyIssueQuest.SnareTheWealthyQuestChoice playerChoice)
			{
				this._playerChoice = playerChoice;
				if (this._caravanParty.MapEvent != null)
				{
					this._caravanParty.MapEvent.FinalizeEvent();
				}
				Settlement closestHideout = SettlementHelper.FindNearestHideout((Settlement x) => x.IsActive, null);
				Clan clan = Clan.BanditFactions.FirstOrDefault((Clan t) => t.Culture == closestHideout.Culture);
				Clan actualClan = (playerChoice != SnareTheWealthyIssueBehavior.SnareTheWealthyIssueQuest.SnareTheWealthyQuestChoice.SidedWithCaravan) ? clan : this._caravanParty.Owner.SupporterOf;
				this._caravanParty.ActualClan = actualClan;
				Clan actualClan2 = (playerChoice == SnareTheWealthyIssueBehavior.SnareTheWealthyIssueQuest.SnareTheWealthyQuestChoice.SidedWithGang) ? base.QuestGiver.SupporterOf : clan;
				this._gangParty.ActualClan = actualClan2;
				PartyBase attackerParty = (playerChoice != SnareTheWealthyIssueBehavior.SnareTheWealthyIssueQuest.SnareTheWealthyQuestChoice.SidedWithGang) ? this._gangParty.Party : this._caravanParty.Party;
				PlayerEncounter.Start();
				PlayerEncounter.Current.SetupFields(attackerParty, PartyBase.MainParty);
				PlayerEncounter.StartBattle();
				if (playerChoice == SnareTheWealthyIssueBehavior.SnareTheWealthyIssueQuest.SnareTheWealthyQuestChoice.BetrayedBoth)
				{
					this._caravanParty.MapEventSide = this._gangParty.MapEventSide;
					return;
				}
				if (playerChoice == SnareTheWealthyIssueBehavior.SnareTheWealthyIssueQuest.SnareTheWealthyQuestChoice.SidedWithCaravan)
				{
					this._caravanParty.MapEventSide = PartyBase.MainParty.MapEventSide;
					return;
				}
				this._gangParty.MapEventSide = PartyBase.MainParty.MapEventSide;
			}

			// Token: 0x06000F79 RID: 3961 RVA: 0x00060B80 File Offset: 0x0005ED80
			private void StartEncounterDialogue()
			{
				if (this._gangParty.CurrentSettlement != null)
				{
					LeaveSettlementAction.ApplyForParty(this._gangParty);
				}
				PlayerEncounter.Finish(true);
				this._canEncounterConversationStart = true;
				ConversationCharacterData playerCharacterData = new ConversationCharacterData(CharacterObject.PlayerCharacter, PartyBase.MainParty, true, false, false, false, false, false);
				ConversationCharacterData conversationPartnerData = new ConversationCharacterData(ConversationHelper.GetConversationCharacterPartyLeader(this._caravanParty.Party), this._caravanParty.Party, true, false, false, false, false, true);
				CampaignMission.OpenConversationMission(playerCharacterData, conversationPartnerData, "", "");
			}

			// Token: 0x06000F7A RID: 3962 RVA: 0x00060C00 File Offset: 0x0005EE00
			private void StartDialogueWithoutCaravan()
			{
				PlayerEncounter.Finish(true);
				ConversationCharacterData playerCharacterData = new ConversationCharacterData(CharacterObject.PlayerCharacter, PartyBase.MainParty, true, false, false, false, false, false);
				ConversationCharacterData conversationPartnerData = new ConversationCharacterData(ConversationHelper.GetConversationCharacterPartyLeader(this._gangParty.Party), this._gangParty.Party, true, false, false, false, false, false);
				CampaignMission.OpenConversationMission(playerCharacterData, conversationPartnerData, "", "");
			}

			// Token: 0x06000F7B RID: 3963 RVA: 0x00060C64 File Offset: 0x0005EE64
			protected override void HourlyTick()
			{
				if (this._caravanParty != null)
				{
					if (this._caravanParty.Ai.DefaultBehavior != AiBehavior.EscortParty || this._caravanParty.ShortTermBehavior != AiBehavior.EscortParty)
					{
						SetPartyAiAction.GetActionForEscortingParty(this._caravanParty, MobileParty.MainParty);
					}
					(this._caravanParty.PartyComponent as CustomPartyComponent).CustomPartyBaseSpeed = MobileParty.MainParty.Speed;
					if (MobileParty.MainParty.TargetParty == this._caravanParty)
					{
						this._caravanParty.Ai.SetMoveModeHold();
						this._isCaravanFollowing = false;
						return;
					}
					if (!this._isCaravanFollowing)
					{
						SetPartyAiAction.GetActionForEscortingParty(this._caravanParty, MobileParty.MainParty);
						this._isCaravanFollowing = true;
					}
				}
			}

			// Token: 0x06000F7C RID: 3964 RVA: 0x00060D17 File Offset: 0x0005EF17
			private void OnSettlementOwnerChanged(Settlement settlement, bool openToClaim, Hero newOwner, Hero oldOwner, Hero capturerHero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail detail)
			{
				if (settlement == base.QuestGiver.CurrentSettlement)
				{
					if (newOwner.Clan == Clan.PlayerClan)
					{
						this.OnCancel4();
						return;
					}
					this.OnCancel2();
				}
			}

			// Token: 0x06000F7D RID: 3965 RVA: 0x00060D41 File Offset: 0x0005EF41
			public void OnWarDeclared(IFaction faction1, IFaction faction2, DeclareWarAction.DeclareWarDetail reason)
			{
				if ((faction1 == base.QuestGiver.MapFaction && faction2 == Hero.MainHero.MapFaction) || (faction2 == base.QuestGiver.MapFaction && faction1 == Hero.MainHero.MapFaction))
				{
					this.OnCancel1();
				}
			}

			// Token: 0x06000F7E RID: 3966 RVA: 0x00060D7F File Offset: 0x0005EF7F
			public void OnVillageStateChanged(Village village, Village.VillageStates oldState, Village.VillageStates newState, MobileParty raiderParty)
			{
				if (village == this._targetSettlement.Village && newState != Village.VillageStates.Normal)
				{
					this.OnCancel3();
				}
			}

			// Token: 0x06000F7F RID: 3967 RVA: 0x00060D98 File Offset: 0x0005EF98
			public void OnMapEventEnded(MapEvent mapEvent)
			{
				if (mapEvent.IsPlayerMapEvent && this._caravanParty != null)
				{
					if (mapEvent.InvolvedParties.Contains(this._caravanParty.Party))
					{
						if (!mapEvent.InvolvedParties.Contains(this._gangParty.Party))
						{
							this.OnFail1();
							return;
						}
						if (mapEvent.WinningSide == mapEvent.PlayerSide)
						{
							if (this._playerChoice == SnareTheWealthyIssueBehavior.SnareTheWealthyIssueQuest.SnareTheWealthyQuestChoice.SidedWithGang)
							{
								this.OnSuccess1();
								return;
							}
							if (this._playerChoice == SnareTheWealthyIssueBehavior.SnareTheWealthyIssueQuest.SnareTheWealthyQuestChoice.SidedWithCaravan)
							{
								this.OnFail2();
								return;
							}
							this.OnFail3();
							return;
						}
						else
						{
							if (this._playerChoice == SnareTheWealthyIssueBehavior.SnareTheWealthyIssueQuest.SnareTheWealthyQuestChoice.SidedWithGang)
							{
								this.OnFail4();
								return;
							}
							if (this._playerChoice == SnareTheWealthyIssueBehavior.SnareTheWealthyIssueQuest.SnareTheWealthyQuestChoice.SidedWithCaravan)
							{
								this.OnFail5();
								return;
							}
							this.OnFail6();
							return;
						}
					}
					else
					{
						this.OnFail1();
					}
				}
			}

			// Token: 0x06000F80 RID: 3968 RVA: 0x00060E54 File Offset: 0x0005F054
			private void OnPartyJoinedArmy(MobileParty mobileParty)
			{
				if (mobileParty == MobileParty.MainParty && this._caravanParty != null)
				{
					this.OnFail1();
				}
			}

			// Token: 0x06000F81 RID: 3969 RVA: 0x00060E6C File Offset: 0x0005F06C
			private void OnGameMenuOpened(MenuCallbackArgs args)
			{
				if (this._startConversationDelegate != null && MobileParty.MainParty.CurrentSettlement == this._targetSettlement && this._caravanParty != null)
				{
					this._startConversationDelegate();
					this._startConversationDelegate = null;
				}
			}

			// Token: 0x06000F82 RID: 3970 RVA: 0x00060EA4 File Offset: 0x0005F0A4
			public void OnSettlementEntered(MobileParty party, Settlement settlement, Hero hero)
			{
				if (party == MobileParty.MainParty && settlement == this._targetSettlement && this._caravanParty != null)
				{
					if (this._caravanParty.Position2D.DistanceSquared(this._targetSettlement.Position2D) <= 20f)
					{
						this._startConversationDelegate = new SnareTheWealthyIssueBehavior.SnareTheWealthyIssueQuest.QuestEndDelegate(this.StartEncounterDialogue);
						return;
					}
					this._startConversationDelegate = new SnareTheWealthyIssueBehavior.SnareTheWealthyIssueQuest.QuestEndDelegate(this.StartDialogueWithoutCaravan);
				}
			}

			// Token: 0x06000F83 RID: 3971 RVA: 0x00060F14 File Offset: 0x0005F114
			public void OnSettlementLeft(MobileParty party, Settlement settlement)
			{
				if (party == MobileParty.MainParty && this._caravanParty != null)
				{
					SetPartyAiAction.GetActionForEscortingParty(this._caravanParty, MobileParty.MainParty);
				}
			}

			// Token: 0x06000F84 RID: 3972 RVA: 0x00060F36 File Offset: 0x0005F136
			private void CanHeroBecomePrisoner(Hero hero, ref bool result)
			{
				if (hero == Hero.MainHero && this._playerChoice != SnareTheWealthyIssueBehavior.SnareTheWealthyIssueQuest.SnareTheWealthyQuestChoice.None)
				{
					result = false;
				}
			}

			// Token: 0x06000F85 RID: 3973 RVA: 0x00060F4C File Offset: 0x0005F14C
			protected override void OnFinalize()
			{
				if (this._caravanParty != null && this._caravanParty.IsActive)
				{
					DestroyPartyAction.Apply(null, this._caravanParty);
				}
				if (this._gangParty != null && this._gangParty.IsActive)
				{
					DestroyPartyAction.Apply(null, this._gangParty);
				}
			}

			// Token: 0x06000F86 RID: 3974 RVA: 0x00060F9C File Offset: 0x0005F19C
			private void OnSuccess1()
			{
				base.AddLog(this._success1LogText, false);
				TraitLevelingHelper.OnIssueSolvedThroughQuest(Hero.MainHero, new Tuple<TraitObject, int>[]
				{
					new Tuple<TraitObject, int>(DefaultTraits.Honor, -100)
				});
				TraitLevelingHelper.OnIssueSolvedThroughQuest(Hero.MainHero, new Tuple<TraitObject, int>[]
				{
					new Tuple<TraitObject, int>(DefaultTraits.Calculating, 50)
				});
				ChangeRelationAction.ApplyPlayerRelation(base.QuestGiver, 5, true, true);
				ChangeRelationAction.ApplyPlayerRelation(this._targetMerchantCharacter.HeroObject, -10, true, true);
				base.QuestGiver.AddPower(30f);
				GiveGoldAction.ApplyBetweenCharacters(null, Hero.MainHero, this.Reward1, false);
				base.CompleteQuestWithSuccess();
			}

			// Token: 0x06000F87 RID: 3975 RVA: 0x0006103F File Offset: 0x0005F23F
			private void OnTimedOutWithoutTalkingToMerchant()
			{
				base.AddLog(this._timedOutWithoutTalkingToMerchantText, false);
				TraitLevelingHelper.OnIssueFailed(Hero.MainHero, new Tuple<TraitObject, int>[]
				{
					new Tuple<TraitObject, int>(DefaultTraits.Honor, -50)
				});
				ChangeRelationAction.ApplyPlayerRelation(base.QuestGiver, -5, true, true);
			}

			// Token: 0x06000F88 RID: 3976 RVA: 0x0006107D File Offset: 0x0005F27D
			private void OnFail1()
			{
				this.ApplyFail1Consequences();
				base.CompleteQuestWithFail(null);
			}

			// Token: 0x06000F89 RID: 3977 RVA: 0x0006108C File Offset: 0x0005F28C
			private void ApplyFail1Consequences()
			{
				base.AddLog(this._fail1LogText, false);
				TraitLevelingHelper.OnIssueFailed(Hero.MainHero, new Tuple<TraitObject, int>[]
				{
					new Tuple<TraitObject, int>(DefaultTraits.Honor, -50)
				});
				ChangeRelationAction.ApplyPlayerRelation(base.QuestGiver, -5, true, true);
				ChangeRelationAction.ApplyPlayerRelation(this._targetMerchantCharacter.HeroObject, -5, true, true);
			}

			// Token: 0x06000F8A RID: 3978 RVA: 0x000610EC File Offset: 0x0005F2EC
			private void OnFail2()
			{
				base.AddLog(this._fail2OutcomeLogText, false);
				TraitLevelingHelper.OnIssueFailed(Hero.MainHero, new Tuple<TraitObject, int>[]
				{
					new Tuple<TraitObject, int>(DefaultTraits.Honor, 100)
				});
				ChangeRelationAction.ApplyPlayerRelation(base.QuestGiver, -10, true, true);
				ChangeRelationAction.ApplyPlayerRelation(this._targetMerchantCharacter.HeroObject, 5, true, true);
				GiveGoldAction.ApplyBetweenCharacters(null, Hero.MainHero, this.Reward2, false);
				base.CompleteQuestWithBetrayal(null);
			}

			// Token: 0x06000F8B RID: 3979 RVA: 0x00061164 File Offset: 0x0005F364
			private void OnFail3()
			{
				base.AddLog(this._fail3OutcomeLogText, false);
				TraitLevelingHelper.OnIssueFailed(Hero.MainHero, new Tuple<TraitObject, int>[]
				{
					new Tuple<TraitObject, int>(DefaultTraits.Honor, -200)
				});
				TraitLevelingHelper.OnIssueFailed(Hero.MainHero, new Tuple<TraitObject, int>[]
				{
					new Tuple<TraitObject, int>(DefaultTraits.Calculating, 100)
				});
				ChangeRelationAction.ApplyPlayerRelation(base.QuestGiver, -15, true, true);
				ChangeRelationAction.ApplyPlayerRelation(this._targetMerchantCharacter.HeroObject, -20, true, true);
				base.CompleteQuestWithBetrayal(null);
			}

			// Token: 0x06000F8C RID: 3980 RVA: 0x000611EC File Offset: 0x0005F3EC
			private void OnFail4()
			{
				base.AddLog(this._fail4LogText, false);
				TraitLevelingHelper.OnIssueFailed(Hero.MainHero, new Tuple<TraitObject, int>[]
				{
					new Tuple<TraitObject, int>(DefaultTraits.Honor, -100)
				});
				TraitLevelingHelper.OnIssueFailed(Hero.MainHero, new Tuple<TraitObject, int>[]
				{
					new Tuple<TraitObject, int>(DefaultTraits.Calculating, 100)
				});
				ChangeRelationAction.ApplyPlayerRelation(base.QuestGiver, -10, true, true);
				ChangeRelationAction.ApplyPlayerRelation(this._targetMerchantCharacter.HeroObject, -10, true, true);
				base.CompleteQuestWithFail(null);
			}

			// Token: 0x06000F8D RID: 3981 RVA: 0x00061270 File Offset: 0x0005F470
			private void OnFail5()
			{
				base.AddLog(this._fail5LogText, false);
				TraitLevelingHelper.OnIssueFailed(Hero.MainHero, new Tuple<TraitObject, int>[]
				{
					new Tuple<TraitObject, int>(DefaultTraits.Honor, -100)
				});
				TraitLevelingHelper.OnIssueFailed(Hero.MainHero, new Tuple<TraitObject, int>[]
				{
					new Tuple<TraitObject, int>(DefaultTraits.Calculating, 100)
				});
				ChangeRelationAction.ApplyPlayerRelation(base.QuestGiver, -10, true, true);
				ChangeRelationAction.ApplyPlayerRelation(this._targetMerchantCharacter.HeroObject, -10, true, true);
				base.CompleteQuestWithBetrayal(null);
			}

			// Token: 0x06000F8E RID: 3982 RVA: 0x000612F4 File Offset: 0x0005F4F4
			private void OnFail6()
			{
				base.AddLog(this._fail6LogText, false);
				TraitLevelingHelper.OnIssueFailed(Hero.MainHero, new Tuple<TraitObject, int>[]
				{
					new Tuple<TraitObject, int>(DefaultTraits.Honor, -200)
				});
				TraitLevelingHelper.OnIssueFailed(Hero.MainHero, new Tuple<TraitObject, int>[]
				{
					new Tuple<TraitObject, int>(DefaultTraits.Calculating, 100)
				});
				ChangeRelationAction.ApplyPlayerRelation(base.QuestGiver, -15, true, true);
				ChangeRelationAction.ApplyPlayerRelation(this._targetMerchantCharacter.HeroObject, -20, true, true);
				base.CompleteQuestWithBetrayal(null);
			}

			// Token: 0x06000F8F RID: 3983 RVA: 0x0006137A File Offset: 0x0005F57A
			protected override void OnTimedOut()
			{
				if (this._caravanParty == null)
				{
					this.OnTimedOutWithoutTalkingToMerchant();
					return;
				}
				this.ApplyFail1Consequences();
			}

			// Token: 0x06000F90 RID: 3984 RVA: 0x00061391 File Offset: 0x0005F591
			private void OnCancel1()
			{
				base.AddLog(this._warDeclaredBetweenPlayerAndQuestGiverLogText, false);
				base.CompleteQuestWithCancel(null);
			}

			// Token: 0x06000F91 RID: 3985 RVA: 0x000613A8 File Offset: 0x0005F5A8
			private void OnCancel2()
			{
				base.AddLog(this._questSettlementWasCapturedLogText, false);
				base.CompleteQuestWithCancel(null);
			}

			// Token: 0x06000F92 RID: 3986 RVA: 0x000613BF File Offset: 0x0005F5BF
			private void OnCancel3()
			{
				base.AddLog(this._targetSettlementRaidedLogText, false);
				base.CompleteQuestWithCancel(null);
			}

			// Token: 0x06000F93 RID: 3987 RVA: 0x000613D6 File Offset: 0x0005F5D6
			private void OnCancel4()
			{
				base.AddLog(this._playerCapturedQuestSettlementLogText, false);
				base.QuestGiver.AddPower(-10f);
				ChangeRelationAction.ApplyPlayerRelation(base.QuestGiver, -5, true, true);
				base.CompleteQuestWithCancel(null);
			}

			// Token: 0x06000F94 RID: 3988 RVA: 0x0006140C File Offset: 0x0005F60C
			private bool IsGangPartyLeader(IAgent agent)
			{
				return agent.Character == ConversationHelper.GetConversationCharacterPartyLeader(this._gangParty.Party);
			}

			// Token: 0x06000F95 RID: 3989 RVA: 0x00061426 File Offset: 0x0005F626
			private bool IsCaravanMaster(IAgent agent)
			{
				return agent.Character == ConversationHelper.GetConversationCharacterPartyLeader(this._caravanParty.Party);
			}

			// Token: 0x06000F96 RID: 3990 RVA: 0x00061440 File Offset: 0x0005F640
			private bool IsMainHero(IAgent agent)
			{
				return agent.Character == CharacterObject.PlayerCharacter;
			}

			// Token: 0x06000F97 RID: 3991 RVA: 0x0006144F File Offset: 0x0005F64F
			public override void OnHeroCanHaveQuestOrIssueInfoIsRequested(Hero hero, ref bool result)
			{
				if (hero == this._targetMerchantCharacter.HeroObject)
				{
					result = false;
				}
			}

			// Token: 0x06000F98 RID: 3992 RVA: 0x00061464 File Offset: 0x0005F664
			protected override void RegisterEvents()
			{
				CampaignEvents.OnSettlementOwnerChangedEvent.AddNonSerializedListener(this, new Action<Settlement, bool, Hero, Hero, Hero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail>(this.OnSettlementOwnerChanged));
				CampaignEvents.WarDeclared.AddNonSerializedListener(this, new Action<IFaction, IFaction, DeclareWarAction.DeclareWarDetail>(this.OnWarDeclared));
				CampaignEvents.VillageStateChanged.AddNonSerializedListener(this, new Action<Village, Village.VillageStates, Village.VillageStates, MobileParty>(this.OnVillageStateChanged));
				CampaignEvents.MapEventEnded.AddNonSerializedListener(this, new Action<MapEvent>(this.OnMapEventEnded));
				CampaignEvents.OnPartyJoinedArmyEvent.AddNonSerializedListener(this, new Action<MobileParty>(this.OnPartyJoinedArmy));
				CampaignEvents.GameMenuOpened.AddNonSerializedListener(this, new Action<MenuCallbackArgs>(this.OnGameMenuOpened));
				CampaignEvents.SettlementEntered.AddNonSerializedListener(this, new Action<MobileParty, Settlement, Hero>(this.OnSettlementEntered));
				CampaignEvents.OnSettlementLeftEvent.AddNonSerializedListener(this, new Action<MobileParty, Settlement>(this.OnSettlementLeft));
				CampaignEvents.CanHeroBecomePrisonerEvent.AddNonSerializedListener(this, new ReferenceAction<Hero, bool>(this.CanHeroBecomePrisoner));
				CampaignEvents.CanHaveQuestsOrIssuesEvent.AddNonSerializedListener(this, new ReferenceAction<Hero, bool>(this.OnHeroCanHaveQuestOrIssueInfoIsRequested));
			}

			// Token: 0x06000F99 RID: 3993 RVA: 0x00061558 File Offset: 0x0005F758
			internal static void AutoGeneratedStaticCollectObjectsSnareTheWealthyIssueQuest(object o, List<object> collectedObjects)
			{
				((SnareTheWealthyIssueBehavior.SnareTheWealthyIssueQuest)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
			}

			// Token: 0x06000F9A RID: 3994 RVA: 0x00061566 File Offset: 0x0005F766
			protected override void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
			{
				base.AutoGeneratedInstanceCollectObjects(collectedObjects);
				collectedObjects.Add(this._targetMerchantCharacter);
				collectedObjects.Add(this._targetSettlement);
				collectedObjects.Add(this._caravanParty);
				collectedObjects.Add(this._gangParty);
			}

			// Token: 0x06000F9B RID: 3995 RVA: 0x0006159F File Offset: 0x0005F79F
			internal static object AutoGeneratedGetMemberValue_targetMerchantCharacter(object o)
			{
				return ((SnareTheWealthyIssueBehavior.SnareTheWealthyIssueQuest)o)._targetMerchantCharacter;
			}

			// Token: 0x06000F9C RID: 3996 RVA: 0x000615AC File Offset: 0x0005F7AC
			internal static object AutoGeneratedGetMemberValue_targetSettlement(object o)
			{
				return ((SnareTheWealthyIssueBehavior.SnareTheWealthyIssueQuest)o)._targetSettlement;
			}

			// Token: 0x06000F9D RID: 3997 RVA: 0x000615B9 File Offset: 0x0005F7B9
			internal static object AutoGeneratedGetMemberValue_caravanParty(object o)
			{
				return ((SnareTheWealthyIssueBehavior.SnareTheWealthyIssueQuest)o)._caravanParty;
			}

			// Token: 0x06000F9E RID: 3998 RVA: 0x000615C6 File Offset: 0x0005F7C6
			internal static object AutoGeneratedGetMemberValue_gangParty(object o)
			{
				return ((SnareTheWealthyIssueBehavior.SnareTheWealthyIssueQuest)o)._gangParty;
			}

			// Token: 0x06000F9F RID: 3999 RVA: 0x000615D3 File Offset: 0x0005F7D3
			internal static object AutoGeneratedGetMemberValue_questDifficulty(object o)
			{
				return ((SnareTheWealthyIssueBehavior.SnareTheWealthyIssueQuest)o)._questDifficulty;
			}

			// Token: 0x06000FA0 RID: 4000 RVA: 0x000615E5 File Offset: 0x0005F7E5
			internal static object AutoGeneratedGetMemberValue_playerChoice(object o)
			{
				return ((SnareTheWealthyIssueBehavior.SnareTheWealthyIssueQuest)o)._playerChoice;
			}

			// Token: 0x06000FA1 RID: 4001 RVA: 0x000615F7 File Offset: 0x0005F7F7
			internal static object AutoGeneratedGetMemberValue_canEncounterConversationStart(object o)
			{
				return ((SnareTheWealthyIssueBehavior.SnareTheWealthyIssueQuest)o)._canEncounterConversationStart;
			}

			// Token: 0x06000FA2 RID: 4002 RVA: 0x00061609 File Offset: 0x0005F809
			internal static object AutoGeneratedGetMemberValue_isCaravanFollowing(object o)
			{
				return ((SnareTheWealthyIssueBehavior.SnareTheWealthyIssueQuest)o)._isCaravanFollowing;
			}

			// Token: 0x04000672 RID: 1650
			private const float CaravanEncounterStartDistance = 20f;

			// Token: 0x04000673 RID: 1651
			private SnareTheWealthyIssueBehavior.SnareTheWealthyIssueQuest.QuestEndDelegate _startConversationDelegate;

			// Token: 0x04000674 RID: 1652
			[SaveableField(1)]
			private CharacterObject _targetMerchantCharacter;

			// Token: 0x04000675 RID: 1653
			[SaveableField(2)]
			private Settlement _targetSettlement;

			// Token: 0x04000676 RID: 1654
			[SaveableField(3)]
			private MobileParty _caravanParty;

			// Token: 0x04000677 RID: 1655
			[SaveableField(4)]
			private MobileParty _gangParty;

			// Token: 0x04000678 RID: 1656
			[SaveableField(5)]
			private readonly float _questDifficulty;

			// Token: 0x04000679 RID: 1657
			[SaveableField(6)]
			private SnareTheWealthyIssueBehavior.SnareTheWealthyIssueQuest.SnareTheWealthyQuestChoice _playerChoice;

			// Token: 0x0400067A RID: 1658
			[SaveableField(7)]
			private bool _canEncounterConversationStart;

			// Token: 0x0400067B RID: 1659
			[SaveableField(8)]
			private bool _isCaravanFollowing = true;

			// Token: 0x020001E4 RID: 484
			internal enum SnareTheWealthyQuestChoice
			{
				// Token: 0x040007C4 RID: 1988
				None,
				// Token: 0x040007C5 RID: 1989
				SidedWithCaravan,
				// Token: 0x040007C6 RID: 1990
				SidedWithGang,
				// Token: 0x040007C7 RID: 1991
				BetrayedBoth
			}

			// Token: 0x020001E5 RID: 485
			// (Invoke) Token: 0x0600116B RID: 4459
			private delegate void QuestEndDelegate();
		}
	}
}
