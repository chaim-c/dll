using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using SandBox.Conversation.MissionLogics;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.AgentOrigins;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.GameState;
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
	// Token: 0x0200008E RID: 142
	public class TheSpyPartyIssueQuestBehavior : CampaignBehaviorBase
	{
		// Token: 0x0600057E RID: 1406 RVA: 0x00024CC7 File Offset: 0x00022EC7
		public override void RegisterEvents()
		{
			CampaignEvents.OnCheckForIssueEvent.AddNonSerializedListener(this, new Action<Hero>(this.OnCheckForIssue));
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00024CE0 File Offset: 0x00022EE0
		public void OnCheckForIssue(Hero hero)
		{
			Settlement relatedObject;
			if (this.ConditionsHold(hero, out relatedObject))
			{
				Campaign.Current.IssueManager.AddPotentialIssueData(hero, new PotentialIssueData(new PotentialIssueData.StartIssueDelegate(this.OnStartIssue), typeof(TheSpyPartyIssueQuestBehavior.TheSpyPartyIssue), IssueBase.IssueFrequency.Rare, relatedObject));
				return;
			}
			Campaign.Current.IssueManager.AddPotentialIssueData(hero, new PotentialIssueData(typeof(TheSpyPartyIssueQuestBehavior.TheSpyPartyIssue), IssueBase.IssueFrequency.Rare));
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00024D48 File Offset: 0x00022F48
		private bool ConditionsHold(Hero issueGiver, out Settlement selectedSettlement)
		{
			selectedSettlement = null;
			if (issueGiver.IsLord && issueGiver.Clan != Clan.PlayerClan)
			{
				if (issueGiver.Clan.Settlements.Any((Settlement x) => x.IsTown))
				{
					selectedSettlement = issueGiver.Clan.Settlements.GetRandomElementWithPredicate((Settlement x) => x.IsTown);
					string difficultySuffix = TheSpyPartyIssueQuestBehavior.GetDifficultySuffix(Campaign.Current.Models.IssueModel.GetIssueDifficultyMultiplier());
					bool flag = MBObjectManager.Instance.GetObject<CharacterObject>("bold_contender_" + difficultySuffix) != null && MBObjectManager.Instance.GetObject<CharacterObject>("confident_contender_" + difficultySuffix) != null && MBObjectManager.Instance.GetObject<CharacterObject>("dignified_contender_" + difficultySuffix) != null && MBObjectManager.Instance.GetObject<CharacterObject>("hardy_contender_" + difficultySuffix) != null;
					if (!flag)
					{
						CampaignEventDispatcher.Instance.RemoveListeners(this);
					}
					return flag;
				}
			}
			return false;
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00024E62 File Offset: 0x00023062
		private static string GetDifficultySuffix(float difficulty)
		{
			if (difficulty <= 0.25f)
			{
				return "easy";
			}
			if (difficulty <= 0.5f)
			{
				return "normal";
			}
			if (difficulty <= 0.75f)
			{
				return "hard";
			}
			return "very_hard";
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00024E94 File Offset: 0x00023094
		private IssueBase OnStartIssue(in PotentialIssueData pid, Hero issueOwner)
		{
			PotentialIssueData potentialIssueData = pid;
			return new TheSpyPartyIssueQuestBehavior.TheSpyPartyIssue(issueOwner, potentialIssueData.RelatedObject as Settlement);
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00024EBA File Offset: 0x000230BA
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x04000298 RID: 664
		private const IssueBase.IssueFrequency TheSpyPartyIssueFrequency = IssueBase.IssueFrequency.Rare;

		// Token: 0x04000299 RID: 665
		private const int IssueDuration = 5;

		// Token: 0x02000171 RID: 369
		public class TheSpyPartyIssueQuestTypeDefiner : SaveableTypeDefiner
		{
			// Token: 0x06000FB0 RID: 4016 RVA: 0x0006180B File Offset: 0x0005FA0B
			public TheSpyPartyIssueQuestTypeDefiner() : base(121250)
			{
			}

			// Token: 0x06000FB1 RID: 4017 RVA: 0x00061818 File Offset: 0x0005FA18
			protected override void DefineClassTypes()
			{
				base.AddClassDefinition(typeof(TheSpyPartyIssueQuestBehavior.TheSpyPartyIssue), 1, null);
				base.AddClassDefinition(typeof(TheSpyPartyIssueQuestBehavior.TheSpyPartyIssueQuest), 2, null);
			}

			// Token: 0x06000FB2 RID: 4018 RVA: 0x0006183E File Offset: 0x0005FA3E
			protected override void DefineStructTypes()
			{
				base.AddStructDefinition(typeof(TheSpyPartyIssueQuestBehavior.SuspectNpc), 3, null);
			}
		}

		// Token: 0x02000172 RID: 370
		public struct SuspectNpc
		{
			// Token: 0x06000FB3 RID: 4019 RVA: 0x00061852 File Offset: 0x0005FA52
			public SuspectNpc(CharacterObject characterObject, bool hasHair, bool hasBigSword, bool withoutMarkings, bool hasBeard)
			{
				this.CharacterObject = characterObject;
				this.HasHair = hasHair;
				this.HasBigSword = hasBigSword;
				this.WithoutMarkings = withoutMarkings;
				this.HasBeard = hasBeard;
			}

			// Token: 0x06000FB4 RID: 4020 RVA: 0x0006187C File Offset: 0x0005FA7C
			public static void AutoGeneratedStaticCollectObjectsSuspectNpc(object o, List<object> collectedObjects)
			{
				((TheSpyPartyIssueQuestBehavior.SuspectNpc)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
			}

			// Token: 0x06000FB5 RID: 4021 RVA: 0x00061898 File Offset: 0x0005FA98
			private void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
			{
				collectedObjects.Add(this.CharacterObject);
			}

			// Token: 0x06000FB6 RID: 4022 RVA: 0x000618A6 File Offset: 0x0005FAA6
			internal static object AutoGeneratedGetMemberValueCharacterObject(object o)
			{
				return ((TheSpyPartyIssueQuestBehavior.SuspectNpc)o).CharacterObject;
			}

			// Token: 0x06000FB7 RID: 4023 RVA: 0x000618B3 File Offset: 0x0005FAB3
			internal static object AutoGeneratedGetMemberValueHasHair(object o)
			{
				return ((TheSpyPartyIssueQuestBehavior.SuspectNpc)o).HasHair;
			}

			// Token: 0x06000FB8 RID: 4024 RVA: 0x000618C5 File Offset: 0x0005FAC5
			internal static object AutoGeneratedGetMemberValueHasBigSword(object o)
			{
				return ((TheSpyPartyIssueQuestBehavior.SuspectNpc)o).HasBigSword;
			}

			// Token: 0x06000FB9 RID: 4025 RVA: 0x000618D7 File Offset: 0x0005FAD7
			internal static object AutoGeneratedGetMemberValueWithoutMarkings(object o)
			{
				return ((TheSpyPartyIssueQuestBehavior.SuspectNpc)o).WithoutMarkings;
			}

			// Token: 0x06000FBA RID: 4026 RVA: 0x000618E9 File Offset: 0x0005FAE9
			internal static object AutoGeneratedGetMemberValueHasBeard(object o)
			{
				return ((TheSpyPartyIssueQuestBehavior.SuspectNpc)o).HasBeard;
			}

			// Token: 0x0400067C RID: 1660
			[SaveableField(10)]
			public readonly CharacterObject CharacterObject;

			// Token: 0x0400067D RID: 1661
			[SaveableField(20)]
			public readonly bool HasHair;

			// Token: 0x0400067E RID: 1662
			[SaveableField(30)]
			public readonly bool HasBigSword;

			// Token: 0x0400067F RID: 1663
			[SaveableField(40)]
			public readonly bool WithoutMarkings;

			// Token: 0x04000680 RID: 1664
			[SaveableField(50)]
			public readonly bool HasBeard;
		}

		// Token: 0x02000173 RID: 371
		public class TheSpyPartyIssue : IssueBase
		{
			// Token: 0x170001D2 RID: 466
			// (get) Token: 0x06000FBB RID: 4027 RVA: 0x000618FB File Offset: 0x0005FAFB
			public override IssueBase.AlternativeSolutionScaleFlag AlternativeSolutionScaleFlags
			{
				get
				{
					return IssueBase.AlternativeSolutionScaleFlag.FailureRisk;
				}
			}

			// Token: 0x170001D3 RID: 467
			// (get) Token: 0x06000FBC RID: 4028 RVA: 0x000618FE File Offset: 0x0005FAFE
			protected override int CompanionSkillRewardXP
			{
				get
				{
					return (int)(600f + 800f * base.IssueDifficultyMultiplier);
				}
			}

			// Token: 0x170001D4 RID: 468
			// (get) Token: 0x06000FBD RID: 4029 RVA: 0x00061913 File Offset: 0x0005FB13
			public override bool IsThereAlternativeSolution
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170001D5 RID: 469
			// (get) Token: 0x06000FBE RID: 4030 RVA: 0x00061916 File Offset: 0x0005FB16
			public override bool IsThereLordSolution
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170001D6 RID: 470
			// (get) Token: 0x06000FBF RID: 4031 RVA: 0x00061919 File Offset: 0x0005FB19
			public override int AlternativeSolutionBaseNeededMenCount
			{
				get
				{
					return 1 + MathF.Ceiling(4f * base.IssueDifficultyMultiplier);
				}
			}

			// Token: 0x170001D7 RID: 471
			// (get) Token: 0x06000FC0 RID: 4032 RVA: 0x0006192E File Offset: 0x0005FB2E
			protected override int AlternativeSolutionBaseDurationInDaysInternal
			{
				get
				{
					return 3 + MathF.Ceiling(3f * base.IssueDifficultyMultiplier);
				}
			}

			// Token: 0x170001D8 RID: 472
			// (get) Token: 0x06000FC1 RID: 4033 RVA: 0x00061943 File Offset: 0x0005FB43
			protected override int RewardGold
			{
				get
				{
					return (int)(500f + 3000f * base.IssueDifficultyMultiplier);
				}
			}

			// Token: 0x170001D9 RID: 473
			// (get) Token: 0x06000FC2 RID: 4034 RVA: 0x00061958 File Offset: 0x0005FB58
			public override TextObject IssueBriefByIssueGiver
			{
				get
				{
					TextObject textObject = new TextObject("{=tFPJySG7}I am hosting a tournament at {SELECTED_SETTLEMENT}. [ib:convo_undecided_open][if:convo_pondering]I am expecting contenders to partake from all over the realm. I have my reasons to believe one of the attending warriors is actually a spy, sent to gather information about its defenses.", null);
					textObject.SetTextVariable("SELECTED_SETTLEMENT", this._selectedSettlement.EncyclopediaLinkWithName);
					return textObject;
				}
			}

			// Token: 0x170001DA RID: 474
			// (get) Token: 0x06000FC3 RID: 4035 RVA: 0x0006197C File Offset: 0x0005FB7C
			public override TextObject IssueAcceptByPlayer
			{
				get
				{
					return new TextObject("{=EYT7b2J5}Any traveler can be asked by an enemy to spy on the places he travels. How can I track this one down?", null);
				}
			}

			// Token: 0x170001DB RID: 475
			// (get) Token: 0x06000FC4 RID: 4036 RVA: 0x00061989 File Offset: 0x0005FB89
			public override TextObject IssueQuestSolutionExplanationByIssueGiver
			{
				get
				{
					TextObject textObject = new TextObject("{=2lgkL9db}Of course. I have employed spies myself. But if a tournament [if:convo_pondering][ib:confident3]participant is asking questions about the state of the garrison and the walls, things which would concern no honest traveler - well, between that and the private information I've received, I think we'd have our man. The spy must be hiding inside {SELECTED_SETTLEMENT}. Once you are there start questioning the townsfolk.", null);
					textObject.SetTextVariable("SELECTED_SETTLEMENT", this._selectedSettlement.EncyclopediaLinkWithName);
					return textObject;
				}
			}

			// Token: 0x170001DC RID: 476
			// (get) Token: 0x06000FC5 RID: 4037 RVA: 0x000619AD File Offset: 0x0005FBAD
			public override TextObject IssuePlayerResponseAfterAlternativeExplanation
			{
				get
				{
					return new TextObject("{=2nFBTmao}Is there any other way to solve this other than asking around?", null);
				}
			}

			// Token: 0x170001DD RID: 477
			// (get) Token: 0x06000FC6 RID: 4038 RVA: 0x000619BA File Offset: 0x0005FBBA
			public override TextObject IssueAlternativeSolutionExplanationByIssueGiver
			{
				get
				{
					TextObject textObject = new TextObject("{=avVno3H8}Well, you can assign a companion of yours with a knack for this kind of game[if:convo_thinking] and enough muscles to back him up. Judging from what I have heard, a group of {NEEDED_MEN_COUNT} should be enough.", null);
					textObject.SetTextVariable("NEEDED_MEN_COUNT", base.GetTotalAlternativeSolutionNeededMenCount());
					return textObject;
				}
			}

			// Token: 0x170001DE RID: 478
			// (get) Token: 0x06000FC7 RID: 4039 RVA: 0x000619D9 File Offset: 0x0005FBD9
			public override TextObject IssueQuestSolutionAcceptByPlayer
			{
				get
				{
					return new TextObject("{=99OsuHGa}I will find the one you are looking for.", null);
				}
			}

			// Token: 0x170001DF RID: 479
			// (get) Token: 0x06000FC8 RID: 4040 RVA: 0x000619E6 File Offset: 0x0005FBE6
			public override TextObject Title
			{
				get
				{
					return new TextObject("{=SJHtVaNa}The Spy Among Us", null);
				}
			}

			// Token: 0x170001E0 RID: 480
			// (get) Token: 0x06000FC9 RID: 4041 RVA: 0x000619F4 File Offset: 0x0005FBF4
			public override TextObject Description
			{
				get
				{
					TextObject textObject = new TextObject("{=C6rbcpbi}{QUEST_GIVER.LINK} wants you to find a spy before he causes any harm...", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.IssueOwner.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x170001E1 RID: 481
			// (get) Token: 0x06000FCA RID: 4042 RVA: 0x00061A26 File Offset: 0x0005FC26
			public override TextObject IssueAlternativeSolutionAcceptByPlayer
			{
				get
				{
					TextObject textObject = new TextObject("{=FEcAwSfk}I will assign a companion with {NEEDED_MEN_COUNT} good men for {ALTERNATIVE_SOLUTION_DURATION} days.", null);
					textObject.SetTextVariable("NEEDED_MEN_COUNT", base.GetTotalAlternativeSolutionNeededMenCount());
					textObject.SetTextVariable("ALTERNATIVE_SOLUTION_DURATION", base.GetTotalAlternativeSolutionDurationInDays());
					return textObject;
				}
			}

			// Token: 0x170001E2 RID: 482
			// (get) Token: 0x06000FCB RID: 4043 RVA: 0x00061A57 File Offset: 0x0005FC57
			public override TextObject IssueDiscussAlternativeSolution
			{
				get
				{
					return new TextObject("{=O0Cjam62}I hope your people are careful about how they proceed.[if:convo_focused_happy] It would be a pity if that spy got away.", null);
				}
			}

			// Token: 0x170001E3 RID: 483
			// (get) Token: 0x06000FCC RID: 4044 RVA: 0x00061A64 File Offset: 0x0005FC64
			public override TextObject IssueAlternativeSolutionResponseByIssueGiver
			{
				get
				{
					TextObject textObject = new TextObject("{=ciXBiMMa}Thank you {PLAYER.NAME}, I hope your people will be successful.[if:convo_focused_happy]", null);
					StringHelpers.SetCharacterProperties("PLAYER", CharacterObject.PlayerCharacter, textObject, false);
					return textObject;
				}
			}

			// Token: 0x170001E4 RID: 484
			// (get) Token: 0x06000FCD RID: 4045 RVA: 0x00061A90 File Offset: 0x0005FC90
			protected override TextObject AlternativeSolutionStartLog
			{
				get
				{
					TextObject textObject = new TextObject("{=s5qs0bPs}{ISSUE_GIVER.LINK}, the {?ISSUE_GIVER.GENDER}lady{?}lord{\\?} of {QUEST_SETTLEMENT}, has told you about a spy that hides as a tournament attendee. You are asked to expose the spy and take care of him. You asked {COMPANION.LINK} to take {NEEDED_MEN_COUNT} of your best men to go and take care of it. They should report back to you in {ALTERNATIVE_SOLUTION_DURATION} days.", null);
					StringHelpers.SetCharacterProperties("ISSUE_GIVER", base.IssueOwner.CharacterObject, textObject, false);
					StringHelpers.SetCharacterProperties("COMPANION", base.AlternativeSolutionHero.CharacterObject, textObject, false);
					textObject.SetTextVariable("QUEST_SETTLEMENT", this._selectedSettlement.EncyclopediaLinkWithName);
					textObject.SetTextVariable("ALTERNATIVE_SOLUTION_DURATION", base.GetTotalAlternativeSolutionDurationInDays());
					textObject.SetTextVariable("NEEDED_MEN_COUNT", this.AlternativeSolutionSentTroops.TotalManCount - 1);
					return textObject;
				}
			}

			// Token: 0x06000FCE RID: 4046 RVA: 0x00061B1C File Offset: 0x0005FD1C
			public TheSpyPartyIssue(Hero issueOwner, Settlement selectedSettlement) : base(issueOwner, CampaignTime.DaysFromNow(5f))
			{
				this._selectedSettlement = selectedSettlement;
			}

			// Token: 0x06000FCF RID: 4047 RVA: 0x00061B36 File Offset: 0x0005FD36
			protected override float GetIssueEffectAmountInternal(IssueEffect issueEffect)
			{
				if (issueEffect == DefaultIssueEffects.SettlementSecurity)
				{
					return -2f;
				}
				if (issueEffect == DefaultIssueEffects.SettlementLoyalty)
				{
					return -0.5f;
				}
				if (issueEffect == DefaultIssueEffects.ClanInfluence)
				{
					return -0.2f;
				}
				return 0f;
			}

			// Token: 0x06000FD0 RID: 4048 RVA: 0x00061B67 File Offset: 0x0005FD67
			public override ValueTuple<SkillObject, int> GetAlternativeSolutionSkill(Hero hero)
			{
				return new ValueTuple<SkillObject, int>((hero.GetSkillValue(DefaultSkills.Charm) >= hero.GetSkillValue(DefaultSkills.Roguery)) ? DefaultSkills.Charm : DefaultSkills.Roguery, 150);
			}

			// Token: 0x06000FD1 RID: 4049 RVA: 0x00061B97 File Offset: 0x0005FD97
			public override bool AlternativeSolutionCondition(out TextObject explanation)
			{
				explanation = TextObject.Empty;
				return QuestHelper.CheckRosterForAlternativeSolution(MobileParty.MainParty.MemberRoster, base.GetTotalAlternativeSolutionNeededMenCount(), ref explanation, 2, false);
			}

			// Token: 0x06000FD2 RID: 4050 RVA: 0x00061BB8 File Offset: 0x0005FDB8
			public override bool IsTroopTypeNeededByAlternativeSolution(CharacterObject character)
			{
				return character.Tier >= 2;
			}

			// Token: 0x06000FD3 RID: 4051 RVA: 0x00061BC6 File Offset: 0x0005FDC6
			public override bool DoTroopsSatisfyAlternativeSolution(TroopRoster troopRoster, out TextObject explanation)
			{
				explanation = TextObject.Empty;
				return QuestHelper.CheckRosterForAlternativeSolution(troopRoster, base.GetTotalAlternativeSolutionNeededMenCount(), ref explanation, 2, false);
			}

			// Token: 0x06000FD4 RID: 4052 RVA: 0x00061BDE File Offset: 0x0005FDDE
			protected override void AlternativeSolutionEndWithSuccessConsequence()
			{
				GainRenownAction.Apply(Hero.MainHero, 1f, false);
				this.RelationshipChangeWithIssueOwner = 5;
				this._selectedSettlement.Town.Prosperity += 5f;
			}

			// Token: 0x06000FD5 RID: 4053 RVA: 0x00061C14 File Offset: 0x0005FE14
			protected override void AlternativeSolutionEndWithFailureConsequence()
			{
				this.RelationshipChangeWithIssueOwner = -5;
				base.IssueOwner.AddPower(-5f);
				this._selectedSettlement.Town.Security -= 10f;
				this._selectedSettlement.Town.Loyalty -= 10f;
			}

			// Token: 0x06000FD6 RID: 4054 RVA: 0x00061C71 File Offset: 0x0005FE71
			protected override void OnGameLoad()
			{
			}

			// Token: 0x06000FD7 RID: 4055 RVA: 0x00061C73 File Offset: 0x0005FE73
			protected override void HourlyTick()
			{
			}

			// Token: 0x06000FD8 RID: 4056 RVA: 0x00061C75 File Offset: 0x0005FE75
			protected override QuestBase GenerateIssueQuest(string questId)
			{
				return new TheSpyPartyIssueQuestBehavior.TheSpyPartyIssueQuest(questId, base.IssueOwner, CampaignTime.DaysFromNow(16f), this.RewardGold, this._selectedSettlement, base.IssueDifficultyMultiplier);
			}

			// Token: 0x06000FD9 RID: 4057 RVA: 0x00061C9F File Offset: 0x0005FE9F
			public override IssueBase.IssueFrequency GetFrequency()
			{
				return IssueBase.IssueFrequency.Rare;
			}

			// Token: 0x06000FDA RID: 4058 RVA: 0x00061CA4 File Offset: 0x0005FEA4
			protected override bool CanPlayerTakeQuestConditions(Hero issueGiver, out IssueBase.PreconditionFlags flag, out Hero relationHero, out SkillObject skill)
			{
				flag = IssueBase.PreconditionFlags.None;
				relationHero = null;
				skill = null;
				if (issueGiver.GetRelationWithPlayer() < -10f)
				{
					flag |= IssueBase.PreconditionFlags.Relation;
					relationHero = issueGiver;
				}
				if (issueGiver.MapFaction.IsAtWarWith(Hero.MainHero.MapFaction))
				{
					flag |= IssueBase.PreconditionFlags.AtWar;
				}
				return flag == IssueBase.PreconditionFlags.None;
			}

			// Token: 0x06000FDB RID: 4059 RVA: 0x00061CF4 File Offset: 0x0005FEF4
			public override bool IssueStayAliveConditions()
			{
				return base.IssueOwner.IsAlive && this._selectedSettlement.OwnerClan == base.IssueOwner.Clan && base.IssueOwner.Clan != Clan.PlayerClan;
			}

			// Token: 0x06000FDC RID: 4060 RVA: 0x00061D32 File Offset: 0x0005FF32
			protected override void CompleteIssueWithTimedOutConsequences()
			{
			}

			// Token: 0x06000FDD RID: 4061 RVA: 0x00061D34 File Offset: 0x0005FF34
			internal static void AutoGeneratedStaticCollectObjectsTheSpyPartyIssue(object o, List<object> collectedObjects)
			{
				((TheSpyPartyIssueQuestBehavior.TheSpyPartyIssue)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
			}

			// Token: 0x06000FDE RID: 4062 RVA: 0x00061D42 File Offset: 0x0005FF42
			protected override void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
			{
				base.AutoGeneratedInstanceCollectObjects(collectedObjects);
				collectedObjects.Add(this._selectedSettlement);
			}

			// Token: 0x06000FDF RID: 4063 RVA: 0x00061D57 File Offset: 0x0005FF57
			internal static object AutoGeneratedGetMemberValue_selectedSettlement(object o)
			{
				return ((TheSpyPartyIssueQuestBehavior.TheSpyPartyIssue)o)._selectedSettlement;
			}

			// Token: 0x04000681 RID: 1665
			private const int QuestDurationInDays = 16;

			// Token: 0x04000682 RID: 1666
			private const int RequiredSkillValue = 150;

			// Token: 0x04000683 RID: 1667
			private const int AlternativeSolutionTroopTierRequirement = 2;

			// Token: 0x04000684 RID: 1668
			[SaveableField(10)]
			private readonly Settlement _selectedSettlement;
		}

		// Token: 0x02000174 RID: 372
		public class TheSpyPartyIssueQuest : QuestBase
		{
			// Token: 0x170001E5 RID: 485
			// (get) Token: 0x06000FE0 RID: 4064 RVA: 0x00061D64 File Offset: 0x0005FF64
			public override bool IsRemainingTimeHidden
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170001E6 RID: 486
			// (get) Token: 0x06000FE1 RID: 4065 RVA: 0x00061D67 File Offset: 0x0005FF67
			public override TextObject Title
			{
				get
				{
					return new TextObject("{=SJHtVaNa}The Spy Among Us", null);
				}
			}

			// Token: 0x170001E7 RID: 487
			// (get) Token: 0x06000FE2 RID: 4066 RVA: 0x00061D74 File Offset: 0x0005FF74
			private TextObject _questStartedLog
			{
				get
				{
					TextObject textObject = new TextObject("{=94WRYoQp}{?QUEST_GIVER.GENDER}Lady{?}Lord{\\?} {QUEST_GIVER.LINK} from {QUEST_SETTLEMENT}, has told you about rumors of a spy disguised amongst the tournament attendees. You agreed to take care of the situation by yourself. {QUEST_GIVER.LINK} believes that the spy is posing as an tournament attendee in the city of {QUEST_SETTLEMENT}.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					textObject.SetTextVariable("QUEST_SETTLEMENT", this._selectedSettlement.EncyclopediaLinkWithName);
					return textObject;
				}
			}

			// Token: 0x170001E8 RID: 488
			// (get) Token: 0x06000FE3 RID: 4067 RVA: 0x00061DC0 File Offset: 0x0005FFC0
			private TextObject _questSuccessQuestLog
			{
				get
				{
					TextObject textObject = new TextObject("{=YIxpNP4k}You received a message from {QUEST_GIVER.LINK}. \"Thank you for killing the spy.[ib:hip][if:convo_grateful] Please accept these {REWARD_GOLD}{GOLD_ICON} denars with our gratitude.\"", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					textObject.SetTextVariable("REWARD_GOLD", this.RewardGold);
					textObject.SetTextVariable("GOLD_ICON", "{=!}<img src=\"General\\Icons\\Coin@2x\" extend=\"8\">");
					return textObject;
				}
			}

			// Token: 0x170001E9 RID: 489
			// (get) Token: 0x06000FE4 RID: 4068 RVA: 0x00061E18 File Offset: 0x00060018
			private TextObject _questFailedKilledAnotherQuestLog
			{
				get
				{
					TextObject textObject = new TextObject("{=tTKpOFRK}You won the duel but your opponent was innocent. {QUEST_GIVER.LINK} is disappointed.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x170001EA RID: 490
			// (get) Token: 0x06000FE5 RID: 4069 RVA: 0x00061E4C File Offset: 0x0006004C
			private TextObject _playerFoundTheSpyButLostTheFight
			{
				get
				{
					TextObject textObject = new TextObject("{=hJ1SFkmq}You managed to find the spy but lost the duel. {QUEST_GIVER.LINK} is disappointed.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x170001EB RID: 491
			// (get) Token: 0x06000FE6 RID: 4070 RVA: 0x00061E80 File Offset: 0x00060080
			private TextObject _playerCouldNotFoundTheSpyAndLostTheFight
			{
				get
				{
					TextObject textObject = new TextObject("{=dOdp1aKA}You couldn't find the spy and dueled the wrong warrior. {QUEST_GIVER.LINK} is disappointed.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x170001EC RID: 492
			// (get) Token: 0x06000FE7 RID: 4071 RVA: 0x00061EB4 File Offset: 0x000600B4
			private TextObject _timedOutQuestLog
			{
				get
				{
					TextObject textObject = new TextObject("{=0dlDkkJV}You have failed to find the spy. {QUEST_GIVER.LINK} is disappointed.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x170001ED RID: 493
			// (get) Token: 0x06000FE8 RID: 4072 RVA: 0x00061EE8 File Offset: 0x000600E8
			private TextObject _questGiverLostOwnershipQuestLog
			{
				get
				{
					TextObject textObject = new TextObject("{=2OmrHVjp}{QUEST_GIVER.LINK} has lost the ownership of {QUEST_SETTLEMENT}. Your contract with {?QUEST_GIVER.GENDER}her{?}him{\\?} has canceled.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					textObject.SetTextVariable("QUEST_SETTLEMENT", this._selectedSettlement.EncyclopediaLinkWithName);
					return textObject;
				}
			}

			// Token: 0x170001EE RID: 494
			// (get) Token: 0x06000FE9 RID: 4073 RVA: 0x00061F31 File Offset: 0x00060131
			private TextObject _warDeclaredQuestLog
			{
				get
				{
					TextObject textObject = new TextObject("{=cKz1cyuM}Your clan is now at war with {QUEST_GIVER_SETTLEMENT_FACTION}. Quest is canceled.", null);
					textObject.SetTextVariable("QUEST_GIVER_SETTLEMENT_FACTION", base.QuestGiver.MapFaction.Name);
					return textObject;
				}
			}

			// Token: 0x170001EF RID: 495
			// (get) Token: 0x06000FEA RID: 4074 RVA: 0x00061F5C File Offset: 0x0006015C
			private TextObject _playerDeclaredWarQuestLogText
			{
				get
				{
					TextObject textObject = new TextObject("{=bqeWVVEE}Your actions have started a war with {QUEST_GIVER.LINK}'s faction. {?QUEST_GIVER.GENDER}She{?}He{\\?} cancels your agreement and the quest is a failure.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x06000FEB RID: 4075 RVA: 0x00061F90 File Offset: 0x00060190
			public TheSpyPartyIssueQuest(string questId, Hero questGiver, CampaignTime duration, int rewardGold, Settlement selectedSettlement, float issueDifficultyMultiplier) : base(questId, questGiver, duration, rewardGold)
			{
				this._selectedSettlement = selectedSettlement;
				this._alreadySpokenAgents = new List<Agent>();
				this._issueDifficultyMultiplier = issueDifficultyMultiplier;
				this._giveClueChange = 0.1f;
				this.SetDialogs();
				base.InitializeQuestOnCreation();
				this.InitializeSuspectNpcs();
				this._selectedSpy = this._suspectList.GetRandomElement<TheSpyPartyIssueQuestBehavior.SuspectNpc>();
				if (!base.IsTracked(this._selectedSettlement))
				{
					base.AddTrackedObject(this._selectedSettlement);
				}
			}

			// Token: 0x06000FEC RID: 4076 RVA: 0x0006200C File Offset: 0x0006020C
			private void InitializeSuspectNpcs()
			{
				this._suspectList = new MBList<TheSpyPartyIssueQuestBehavior.SuspectNpc>();
				this._currentDifficultySuffix = TheSpyPartyIssueQuestBehavior.GetDifficultySuffix(this._issueDifficultyMultiplier);
				this._suspectList.Add(new TheSpyPartyIssueQuestBehavior.SuspectNpc(MBObjectManager.Instance.GetObject<CharacterObject>("bold_contender_" + this._currentDifficultySuffix), false, true, true, true));
				this._suspectList.Add(new TheSpyPartyIssueQuestBehavior.SuspectNpc(MBObjectManager.Instance.GetObject<CharacterObject>("confident_contender_" + this._currentDifficultySuffix), true, false, true, true));
				this._suspectList.Add(new TheSpyPartyIssueQuestBehavior.SuspectNpc(MBObjectManager.Instance.GetObject<CharacterObject>("dignified_contender_" + this._currentDifficultySuffix), true, true, false, true));
				this._suspectList.Add(new TheSpyPartyIssueQuestBehavior.SuspectNpc(MBObjectManager.Instance.GetObject<CharacterObject>("hardy_contender_" + this._currentDifficultySuffix), true, true, true, false));
			}

			// Token: 0x06000FED RID: 4077 RVA: 0x000620ED File Offset: 0x000602ED
			protected override void InitializeQuestOnGameLoad()
			{
				this._alreadySpokenAgents = new List<Agent>();
				this._giveClueChange = 0.1f;
				this.InitializeSuspectNpcs();
				this.SetDialogs();
				if (Settlement.CurrentSettlement != null && Settlement.CurrentSettlement == this._selectedSettlement)
				{
					this._addSpyNpcsToSettlement = true;
				}
			}

			// Token: 0x06000FEE RID: 4078 RVA: 0x0006212C File Offset: 0x0006032C
			protected override void HourlyTick()
			{
			}

			// Token: 0x06000FEF RID: 4079 RVA: 0x00062130 File Offset: 0x00060330
			protected override void SetDialogs()
			{
				TextObject textObject = new TextObject("{=wql79Eta}Good! We understand the spy is going to {TARGET_SETTLEMENT}. If they're trying to gather information,[ib:aggressive2][if:convo_undecided_open] they'll be wandering around the market asking questions in the guise of making small talk. Just go around talking to the townsfolk, and you should be able to figure out who it is.", null);
				textObject.SetTextVariable("TARGET_SETTLEMENT", this._selectedSettlement.EncyclopediaLinkWithName);
				TextObject textObject2 = new TextObject("{=aC0Fq6IE}Do not waste time, {PLAYER.NAME}. The spy probably won't linger any longer than he has to.[if:convo_thinking] Or she has to.", null);
				StringHelpers.SetCharacterProperties("PLAYER", CharacterObject.PlayerCharacter, textObject2, false);
				this.OfferDialogFlow = DialogFlow.CreateDialogFlow("issue_classic_quest_start", 100).NpcLine(textObject, null, null).Condition(() => Hero.OneToOneConversationHero == base.QuestGiver).Consequence(new ConversationSentence.OnConsequenceDelegate(this.QuestAcceptedConsequences)).CloseDialog();
				this.DiscussDialogFlow = DialogFlow.CreateDialogFlow("quest_discuss", 100).NpcLine(new TextObject("{=yLRfb5zb}Any news? Have you managed to find him yet?[if:convo_astonished]", null), null, null).Condition(() => Hero.OneToOneConversationHero == base.QuestGiver).BeginPlayerOptions().PlayerOption(new TextObject("{=wErSpkjy}I'm still working on it.", null), null).NpcLine(textObject2, null, null).Consequence(new ConversationSentence.OnConsequenceDelegate(this.LeaveEncounter)).CloseDialog().PlayerOption(new TextObject("{=I8raOMRH}Sorry. No progress yet.", null), null).NpcLine(new TextObject("{=ajSm2FEU}I know spies are hard to catch but I tasked this to you for a reason. [if:convo_stern]Do not let me down {PLAYER.NAME}.", null), null, null).NpcLine(new TextObject("{=pW69nUp8}Finish this task before it is too late.[if:convo_stern]", null), null, null).Consequence(new ConversationSentence.OnConsequenceDelegate(this.LeaveEncounter)).CloseDialog().EndPlayerOptions().CloseDialog();
				Campaign.Current.ConversationManager.AddDialogFlow(this.GetTownsPeopleDialogFlow(), this);
				Campaign.Current.ConversationManager.AddDialogFlow(this.GetNotablesDialogFlow(), this);
				Campaign.Current.ConversationManager.AddDialogFlow(this.GetTradersDialogFlow(), this);
				Campaign.Current.ConversationManager.AddDialogFlow(this.GetSuspectsDialogFlow(), this);
			}

			// Token: 0x06000FF0 RID: 4080 RVA: 0x000622D5 File Offset: 0x000604D5
			private void LeaveEncounter()
			{
				if (PlayerEncounter.Current != null)
				{
					PlayerEncounter.LeaveEncounter = true;
				}
			}

			// Token: 0x06000FF1 RID: 4081 RVA: 0x000622E4 File Offset: 0x000604E4
			private void QuestAcceptedConsequences()
			{
				base.StartQuest();
				base.AddLog(this._questStartedLog, false);
				if (Settlement.CurrentSettlement != null && Settlement.CurrentSettlement == this._selectedSettlement)
				{
					this._addSpyNpcsToSettlement = true;
				}
			}

			// Token: 0x06000FF2 RID: 4082 RVA: 0x00062318 File Offset: 0x00060518
			private DialogFlow GetSuspectsDialogFlow()
			{
				return DialogFlow.CreateDialogFlow("start", 125).NpcLine(new TextObject("{=IqhGJ8Dy}Hello there friend. Are you here for the tournament.[ib:confident3][if:convo_relaxed_happy]", null), null, null).Condition(() => this._suspectList.Any((TheSpyPartyIssueQuestBehavior.SuspectNpc x) => x.CharacterObject == CharacterObject.OneToOneConversationCharacter)).BeginPlayerOptions().PlayerOption(new TextObject("{=SRa9NyP1}No, my friend. I am on a hunt.", null), null).NpcLine(new TextObject("{=gYCSwLB2}Eh, what do you mean by that?[ib:closed][if:convo_confused_normal]", null), null, null).BeginPlayerOptions().PlayerOption(new TextObject("{=oddzOnad}I'm hunting a spy. And now I have found him.", null), null).NpcLine(new TextObject("{=MU8nbzwJ}You have nothing on me. If you try to take me anywhere I'll kill you, and it will be in self-defense.[if:convo_grave]", null), null, null).PlayerLine(new TextObject("{=WDdlPUHw}Not if it's a duel. I challenge you. No true tournament fighter would refuse.", null), null).NpcLine(new TextObject("{=Ll8q45h5}Hmf... Very well. I shall wipe out this insult with your blood.[ib:warrior][if:convo_furious]", null), null, null).Consequence(delegate
				{
					Campaign.Current.ConversationManager.ConversationEndOneShot += this.StartFightWithSpy;
				}).CloseDialog().PlayerOption(new TextObject("{=O5PDH9Bc}Nothing, nothing. Go on your way.", null), null).CloseDialog().EndPlayerOptions().PlayerOption(new TextObject("{=O7j0uzcH}I should be on my way.", null), null).CloseDialog().EndPlayerOptions();
			}

			// Token: 0x06000FF3 RID: 4083 RVA: 0x00062414 File Offset: 0x00060614
			private void StartFightWithSpy()
			{
				this._playerManagedToFindSpy = (this._selectedSpy.CharacterObject == CharacterObject.OneToOneConversationCharacter);
				this._duelCharacter = CharacterObject.OneToOneConversationCharacter;
				this._startFightWithSpy = true;
				Campaign.Current.GameMenuManager.NextLocation = LocationComplex.Current.GetLocationWithId("arena");
				Mission.Current.EndMission();
			}

			// Token: 0x06000FF4 RID: 4084 RVA: 0x00062474 File Offset: 0x00060674
			private DialogFlow GetNotablesDialogFlow()
			{
				TextObject textObject = new TextObject("{=0RTwaPBJ}I speak to many people. Of course, as I am loyal to {QUEST_GIVER.NAME}, [if:convo_pondering]I am always on the lookout for spies. But I've seen no one like this.", null);
				StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
				return DialogFlow.CreateDialogFlow("hero_main_options", 125).BeginPlayerOptions().PlayerOption(new TextObject("{=xPTxkzVM}I am looking for a spy. Have you seen any warriors in the tournament wandering about, asking too many suspicious questions?", null), null).Condition(() => Settlement.CurrentSettlement == this._selectedSettlement && Hero.OneToOneConversationHero.IsNotable).NpcLine(textObject, null, null).CloseDialog().EndPlayerOptions();
			}

			// Token: 0x06000FF5 RID: 4085 RVA: 0x000624EC File Offset: 0x000606EC
			private DialogFlow GetTradersDialogFlow()
			{
				return DialogFlow.CreateDialogFlow("weaponsmith_talk_player", 125).BeginPlayerOptions().PlayerOption(new TextObject("{=SHwlcdp3}I ask you because you are a trader here. Have you seen one of the warriors in the tournament walking around here, asking people a lot of suspicious questions?", null), null).Condition(() => Settlement.CurrentSettlement == this._selectedSettlement && (CharacterObject.OneToOneConversationCharacter.Occupation == Occupation.HorseTrader || CharacterObject.OneToOneConversationCharacter.Occupation == Occupation.GoodsTrader || CharacterObject.OneToOneConversationCharacter.Occupation == Occupation.Blacksmith || CharacterObject.OneToOneConversationCharacter.Occupation == Occupation.Weaponsmith)).NpcLine(new TextObject("{=ocoHNhNk}Hmm... I keep pretty busy with my own trade. I haven't heard anything like that.[if:convo_pondering]", null), null, null).CloseDialog().EndPlayerOptions();
			}

			// Token: 0x06000FF6 RID: 4086 RVA: 0x00062548 File Offset: 0x00060748
			private DialogFlow GetTownsPeopleDialogFlow()
			{
				TextObject playerOption1 = new TextObject("{=A2oos2Uo}Listen to me. I'm on assignment from {QUEST_GIVER.NAME}. Have any strangers been around here, asking odd questions about the garrison?", null);
				StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, playerOption1, false);
				TextObject playerOption2 = new TextObject("{=RXhBl8e1}Act normal. Have any of the participants in the tournament come round, asking very odd questions?", null);
				TextObject playerOption3 = new TextObject("{=HF2GIpbI}Listen to me. Have any of the tournament participants spent long hours in the market and tavern? More than usual?", null);
				float dontGiveClueResponse = 0f;
				bool giveClue = false;
				return DialogFlow.CreateDialogFlow("town_or_village_player", 125).BeginPlayerOptions().PlayerOption(new TextObject("{=GtgGnMe1}{PLAYER_OPTION}", null), null).Condition(delegate
				{
					if (Settlement.CurrentSettlement == this._selectedSettlement)
					{
						float randomFloat = MBRandom.RandomFloat;
						dontGiveClueResponse = MBRandom.RandomFloat;
						if (randomFloat < 0.33f)
						{
							MBTextManager.SetTextVariable("PLAYER_OPTION", playerOption1, false);
						}
						else if (randomFloat >= 0.33f && randomFloat <= 0.66f)
						{
							MBTextManager.SetTextVariable("PLAYER_OPTION", playerOption2, false);
						}
						else
						{
							MBTextManager.SetTextVariable("PLAYER_OPTION", playerOption3, false);
						}
						return true;
					}
					return false;
				}).Consequence(delegate
				{
					giveClue = (this._giveClueChange >= MBRandom.RandomFloat);
					Campaign.Current.ConversationManager.ConversationEndOneShot += this.AddAgentToAlreadySpokenList;
				}).BeginNpcOptions().NpcOption(new TextObject("{=8gmne3b9}Not to me {?PLAYER.GENDER}madam{?}sir{\\?}, no. I did overhear someone talking to another merchant about such things. I remember him because he had this nasty looking sword by his side.[if:convo_disbelief]", null), () => giveClue && this._selectedSpy.HasBigSword && !this._playerLearnedHasBigSword && this.CommonCondition(), null, null).PlayerLine(new TextObject("{=VP6s1YFW}Many contenders have swords on their backs. Still this information might prove useful.", null), null).Consequence(new ConversationSentence.OnConsequenceDelegate(this.PlayerLearnedSpyHasSword)).CloseDialog().NpcOption(new TextObject("{=gHnMYU9n}Why yes... At the tavern last night... Cornered a drunk and kept pressing him for information about the gatehouse. Had a beard, that one did.[if:convo_pondering]", null), () => giveClue && this._selectedSpy.HasBeard && !this._playerLearnedHasBeard && this.CommonCondition(), null, null).PlayerLine(new TextObject("{=QaAzicqA}Many men have beards. Still, that is something.", null), null).Consequence(new ConversationSentence.OnConsequenceDelegate(this.PlayerLearnedSpyHasBeard)).CloseDialog().NpcOption(new TextObject("{=DUVqJifX}Yeah. I've seen one like that around the arena, asking all matter of outlandish questions. Middle-aged, normal head of hair, that's really all I can remember though.[if:convo_thinking]", null), () => giveClue && this._selectedSpy.HasHair && !this._playerLearnedHasHair && this.CommonCondition(), null, null).PlayerLine(new TextObject("{=JjtmptiD}More men have hair than not, but this is another tile in the mosaic.", null), null).Consequence(new ConversationSentence.OnConsequenceDelegate(this.PlayerLearnedSpyHasHair)).CloseDialog().NpcOption(new TextObject("{=tXpmCzoZ}Well, there was one warrior. A handsome young lad. Didn't have any of those scars that some fighters pick up in battle, nor any of those marks or tattoos or whatever that [if:convo_pondering]some of the hard cases like to show off.", null), () => giveClue && this._selectedSpy.WithoutMarkings && !this._playerLearnedHasNoMarkings && this.CommonCondition(), null, null).PlayerLine(new TextObject("{=ZCbQvqqv}A face without scars and markings is usual for farmers and merchants but less so for warriors. This might be useful.", null), null).Consequence(new ConversationSentence.OnConsequenceDelegate(this.PlayerLearnedSpyHasNoMarkings)).CloseDialog().NpcOption(new TextObject("{=sfxfiWxl}{?PLAYER.GENDER}Madam{?}Sir{\\?}, people gossip. Everyone around here knows you've been asking those questions. Your quarry is going to slip away if you don't move quickly.", null), () => dontGiveClueResponse <= 0.2f, null, null).PlayerLine(new TextObject("{=04gFKwY1}Well, if you see anyone like that, let me know.", null), null).CloseDialog().NpcOption(new TextObject("{=VWaNqkqJ}Can't say I've seen anyone around here like that, {?PLAYER.GENDER}madam{?}sir{\\?}.", null), () => dontGiveClueResponse > 0.2f && dontGiveClueResponse <= 0.4f, null, null).PlayerLine(new TextObject("{=QbzsgawM}Okay, just keep your eyes open.", null), null).CloseDialog().NpcOption(new TextObject("{=ff5XEKPB}Afraid I can't recall anyone like that, {?PLAYER.GENDER}madam{?}sir{\\?}.", null), () => dontGiveClueResponse > 0.4f && dontGiveClueResponse <= 0.6f, null, null).PlayerLine(new TextObject("{=ArseaKsm}Very well. Thanks for your time.", null), null).CloseDialog().NpcOption(new TextObject("{=C6EOT3yY}No, sorry. Haven't seen anything like that.", null), () => dontGiveClueResponse > 0.6f && dontGiveClueResponse <= 0.8f, null, null).PlayerLine(new TextObject("{=3UX334MB}Hmm.. Very well. Sorry for interrupting.", null), null).CloseDialog().NpcOption(new TextObject("{=9DDWjL9Y}Hmm... Maybe, but I can't remember who. I didn't think it suspicious.", null), () => dontGiveClueResponse > 0.8f, null, null).PlayerLine(new TextObject("{=QbzsgawM}Okay, just keep your eyes open.", null), null).CloseDialog().EndNpcOptions().EndPlayerOptions();
			}

			// Token: 0x06000FF7 RID: 4087 RVA: 0x0006282F File Offset: 0x00060A2F
			private void AddAgentToAlreadySpokenList()
			{
				this._giveClueChange += 0.15f;
				this._alreadySpokenAgents.Add((Agent)MissionConversationLogic.Current.ConversationManager.ConversationAgents[0]);
			}

			// Token: 0x06000FF8 RID: 4088 RVA: 0x00062868 File Offset: 0x00060A68
			private bool CommonCondition()
			{
				return Settlement.CurrentSettlement == this._selectedSettlement && !this._alreadySpokenAgents.Contains((Agent)MissionConversationLogic.Current.ConversationManager.ConversationAgents[0]) && CharacterObject.OneToOneConversationCharacter.Occupation == Occupation.Townsfolk;
			}

			// Token: 0x06000FF9 RID: 4089 RVA: 0x000628B8 File Offset: 0x00060AB8
			private void CheckIfPlayerLearnedEverything()
			{
				int num = 0;
				num = (this._playerLearnedHasBeard ? (num + 1) : num);
				num = (this._playerLearnedHasBigSword ? (num + 1) : num);
				num = (this._playerLearnedHasHair ? (num + 1) : num);
				num = (this._playerLearnedHasNoMarkings ? (num + 1) : num);
				if (num >= 3)
				{
					TextObject text = new TextObject("{=2LW2jWuG}You should now have enough evidence to identify the spy. You might be able to find the tournament participants hanging out in the alleys with local thugs. Find and speak with them.", null);
					base.AddLog(text, false);
				}
			}

			// Token: 0x06000FFA RID: 4090 RVA: 0x00062924 File Offset: 0x00060B24
			private void PlayerLearnedSpyHasSword()
			{
				this._giveClueChange = 0f;
				this._playerLearnedHasBigSword = true;
				base.AddLog(new TextObject("{=awYMellZ}The spy is known to carry a sword.", null), false);
				this._alreadySpokenAgents.Add(MissionConversationLogic.Current.ConversationAgent);
				this.CheckIfPlayerLearnedEverything();
			}

			// Token: 0x06000FFB RID: 4091 RVA: 0x00062974 File Offset: 0x00060B74
			private void PlayerLearnedSpyHasBeard()
			{
				this._giveClueChange = 0f;
				this._playerLearnedHasBeard = true;
				base.AddLog(new TextObject("{=5om6Wv1n}After questioning some folk in town, you learned that the spy has a beard.", null), false);
				this._alreadySpokenAgents.Add(MissionConversationLogic.Current.ConversationAgent);
				this.CheckIfPlayerLearnedEverything();
			}

			// Token: 0x06000FFC RID: 4092 RVA: 0x000629C4 File Offset: 0x00060BC4
			private void PlayerLearnedSpyHasHair()
			{
				this._giveClueChange = 0f;
				this._playerLearnedHasHair = true;
				base.AddLog(new TextObject("{=PLgOm8tV}The townsfolk told you that the spy is not bald.", null), false);
				this._alreadySpokenAgents.Add(MissionConversationLogic.Current.ConversationAgent);
				this.CheckIfPlayerLearnedEverything();
			}

			// Token: 0x06000FFD RID: 4093 RVA: 0x00062A14 File Offset: 0x00060C14
			private void PlayerLearnedSpyHasNoMarkings()
			{
				this._giveClueChange = 0f;
				this._playerLearnedHasNoMarkings = true;
				base.AddLog(new TextObject("{=1ieLd5qq}The townsfolk told you that the spy has no distinctive scars or other facial markings.", null), false);
				this._alreadySpokenAgents.Add(MissionConversationLogic.Current.ConversationAgent);
				this.CheckIfPlayerLearnedEverything();
			}

			// Token: 0x06000FFE RID: 4094 RVA: 0x00062A64 File Offset: 0x00060C64
			protected override void RegisterEvents()
			{
				CampaignEvents.SettlementEntered.AddNonSerializedListener(this, new Action<MobileParty, Settlement, Hero>(this.OnSettlementEntered));
				CampaignEvents.OnSettlementLeftEvent.AddNonSerializedListener(this, new Action<MobileParty, Settlement>(this.OnSettlementLeft));
				CampaignEvents.AfterMissionStarted.AddNonSerializedListener(this, new Action<IMission>(this.OnMissionStarted));
				CampaignEvents.OnSettlementOwnerChangedEvent.AddNonSerializedListener(this, new Action<Settlement, bool, Hero, Hero, Hero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail>(this.OnSettlementOwnerChanged));
				CampaignEvents.WarDeclared.AddNonSerializedListener(this, new Action<IFaction, IFaction, DeclareWarAction.DeclareWarDetail>(this.OnWarDeclared));
				CampaignEvents.OnClanChangedKingdomEvent.AddNonSerializedListener(this, new Action<Clan, Kingdom, Kingdom, ChangeKingdomAction.ChangeKingdomActionDetail, bool>(this.OnClanChangedKingdom));
				CampaignEvents.BeforeGameMenuOpenedEvent.AddNonSerializedListener(this, new Action<MenuCallbackArgs>(this.BeforeGameMenuOpenedEvent));
				CampaignEvents.MapEventStarted.AddNonSerializedListener(this, new Action<MapEvent, PartyBase, PartyBase>(this.OnMapEventStarted));
			}

			// Token: 0x06000FFF RID: 4095 RVA: 0x00062B29 File Offset: 0x00060D29
			private void OnMapEventStarted(MapEvent mapEvent, PartyBase attackerParty, PartyBase defenderParty)
			{
				if (QuestHelper.CheckMinorMajorCoercion(this, mapEvent, attackerParty))
				{
					QuestHelper.ApplyGenericMinorMajorCoercionConsequences(this, mapEvent);
				}
			}

			// Token: 0x06001000 RID: 4096 RVA: 0x00062B3C File Offset: 0x00060D3C
			private void BeforeGameMenuOpenedEvent(MenuCallbackArgs args)
			{
				if (Settlement.CurrentSettlement == this._selectedSettlement && args.MenuContext.GameMenu.StringId == "town")
				{
					if (this._startFightWithSpy && Campaign.Current.GameMenuManager.NextLocation == LocationComplex.Current.GetLocationWithId("arena") && GameStateManager.Current.ActiveState is MapState)
					{
						this._startFightWithSpy = false;
						CampaignMission.OpenArenaDuelMission(LocationComplex.Current.GetLocationWithId("arena").GetSceneName(this._selectedSettlement.Town.GetWallLevel()), LocationComplex.Current.GetLocationWithId("arena"), this._duelCharacter, false, false, new Action<CharacterObject>(this.OnFightEnd), 225f);
						Campaign.Current.GameMenuManager.NextLocation = null;
					}
					if (this._checkForBattleResult)
					{
						if (this._playerWonTheFight)
						{
							if (this._playerManagedToFindSpy)
							{
								this.PlayerFoundTheSpyAndKilledHim();
								return;
							}
							this.PlayerCouldNotFoundTheSpyAndKilledAnotherSuspect();
							return;
						}
						else
						{
							if (this._playerManagedToFindSpy)
							{
								this.PlayerFoundTheSpyButLostTheFight();
								return;
							}
							this.PlayerCouldNotFoundTheSpyAndLostTheFight();
						}
					}
				}
			}

			// Token: 0x06001001 RID: 4097 RVA: 0x00062C58 File Offset: 0x00060E58
			private void PlayerFoundTheSpyAndKilledHim()
			{
				base.AddLog(this._questSuccessQuestLog, false);
				GainRenownAction.Apply(Hero.MainHero, 1f, false);
				GiveGoldAction.ApplyBetweenCharacters(null, Hero.MainHero, this.RewardGold, false);
				this.RelationshipChangeWithQuestGiver = 5;
				this._selectedSettlement.Town.Prosperity += 5f;
				base.CompleteQuestWithSuccess();
			}

			// Token: 0x06001002 RID: 4098 RVA: 0x00062CC0 File Offset: 0x00060EC0
			private void PlayerCouldNotFoundTheSpyAndKilledAnotherSuspect()
			{
				base.AddLog(this._questFailedKilledAnotherQuestLog, false);
				ChangeCrimeRatingAction.Apply(base.QuestGiver.MapFaction, 10f, true);
				this.RelationshipChangeWithQuestGiver = -5;
				this._selectedSettlement.Town.Security -= 10f;
				this._selectedSettlement.Town.Loyalty -= 10f;
				base.CompleteQuestWithFail(null);
			}

			// Token: 0x06001003 RID: 4099 RVA: 0x00062D38 File Offset: 0x00060F38
			private void PlayerFoundTheSpyButLostTheFight()
			{
				base.AddLog(this._playerFoundTheSpyButLostTheFight, false);
				this.RelationshipChangeWithQuestGiver = -5;
				this._selectedSettlement.Town.Security -= 10f;
				this._selectedSettlement.Town.Loyalty -= 10f;
				base.CompleteQuestWithFail(null);
			}

			// Token: 0x06001004 RID: 4100 RVA: 0x00062D9C File Offset: 0x00060F9C
			private void PlayerCouldNotFoundTheSpyAndLostTheFight()
			{
				base.AddLog(this._playerCouldNotFoundTheSpyAndLostTheFight, false);
				this.RelationshipChangeWithQuestGiver = -5;
				this._selectedSettlement.Town.Security -= 10f;
				this._selectedSettlement.Town.Loyalty -= 10f;
				base.CompleteQuestWithFail(null);
			}

			// Token: 0x06001005 RID: 4101 RVA: 0x00062DFE File Offset: 0x00060FFE
			private void OnFightEnd(CharacterObject winnerCharacterObject)
			{
				this._checkForBattleResult = true;
				this._playerWonTheFight = (winnerCharacterObject == CharacterObject.PlayerCharacter);
			}

			// Token: 0x06001006 RID: 4102 RVA: 0x00062E15 File Offset: 0x00061015
			private void OnClanChangedKingdom(Clan clan, Kingdom oldKingdom, Kingdom newKingdom, ChangeKingdomAction.ChangeKingdomActionDetail detail, bool showNotification = true)
			{
				if (base.QuestGiver.MapFaction.IsAtWarWith(Hero.MainHero.MapFaction))
				{
					base.CompleteQuestWithCancel(this._warDeclaredQuestLog);
				}
			}

			// Token: 0x06001007 RID: 4103 RVA: 0x00062E3F File Offset: 0x0006103F
			private void OnWarDeclared(IFaction faction1, IFaction faction2, DeclareWarAction.DeclareWarDetail detail)
			{
				QuestHelper.CheckWarDeclarationAndFailOrCancelTheQuest(this, faction1, faction2, detail, this._playerDeclaredWarQuestLogText, this._warDeclaredQuestLog, false);
			}

			// Token: 0x06001008 RID: 4104 RVA: 0x00062E57 File Offset: 0x00061057
			private void OnSettlementOwnerChanged(Settlement settlement, bool openToClaim, Hero newOwner, Hero oldOwner, Hero capturerHero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail detail)
			{
				if (settlement == this._selectedSettlement && oldOwner.Clan == base.QuestGiver.Clan)
				{
					base.AddLog(this._questGiverLostOwnershipQuestLog, false);
					base.CompleteQuestWithCancel(null);
				}
			}

			// Token: 0x06001009 RID: 4105 RVA: 0x00062E8B File Offset: 0x0006108B
			private void OnSettlementEntered(MobileParty party, Settlement settlement, Hero hero)
			{
				if (party != null && party.IsMainParty && settlement == this._selectedSettlement && hero == Hero.MainHero)
				{
					this._addSpyNpcsToSettlement = true;
				}
			}

			// Token: 0x0600100A RID: 4106 RVA: 0x00062EB0 File Offset: 0x000610B0
			public override GameMenuOption.IssueQuestFlags IsLocationTrackedByQuest(Location location)
			{
				if (PlayerEncounter.LocationEncounter.Settlement == this._selectedSettlement && location.StringId == "center")
				{
					return GameMenuOption.IssueQuestFlags.ActiveIssue;
				}
				return GameMenuOption.IssueQuestFlags.None;
			}

			// Token: 0x0600100B RID: 4107 RVA: 0x00062ED9 File Offset: 0x000610D9
			private void OnSettlementLeft(MobileParty party, Settlement settlement)
			{
				if (party.IsMainParty && settlement == this._selectedSettlement)
				{
					this._addSpyNpcsToSettlement = false;
				}
			}

			// Token: 0x0600100C RID: 4108 RVA: 0x00062EF4 File Offset: 0x000610F4
			private void OnMissionStarted(IMission mission)
			{
				if (this._addSpyNpcsToSettlement)
				{
					Location locationWithId = Settlement.CurrentSettlement.LocationComplex.GetLocationWithId("center");
					if (locationWithId != null)
					{
						locationWithId.AddLocationCharacters(new CreateLocationCharacterDelegate(this.CreateBoldSpyLocationCharacter), Settlement.CurrentSettlement.Culture, LocationCharacter.CharacterRelations.Neutral, 1);
						locationWithId.AddLocationCharacters(new CreateLocationCharacterDelegate(this.CreateConfidentSpyLocationCharacter), Settlement.CurrentSettlement.Culture, LocationCharacter.CharacterRelations.Neutral, 1);
						locationWithId.AddLocationCharacters(new CreateLocationCharacterDelegate(this.CreateDignifiedSpyLocationCharacter), Settlement.CurrentSettlement.Culture, LocationCharacter.CharacterRelations.Neutral, 1);
						locationWithId.AddLocationCharacters(new CreateLocationCharacterDelegate(this.CreateHardySpyLocationCharacters), Settlement.CurrentSettlement.Culture, LocationCharacter.CharacterRelations.Neutral, 1);
					}
				}
			}

			// Token: 0x0600100D RID: 4109 RVA: 0x00062F9C File Offset: 0x0006119C
			private LocationCharacter CreateBoldSpyLocationCharacter(CultureObject culture, LocationCharacter.CharacterRelations relation)
			{
				CharacterObject @object = MBObjectManager.Instance.GetObject<CharacterObject>("bold_contender_" + this._currentDifficultySuffix);
				Monster monsterWithSuffix = TaleWorlds.Core.FaceGen.GetMonsterWithSuffix(@object.Race, "_settlement");
				Tuple<string, Monster> tuple = new Tuple<string, Monster>(ActionSetCode.GenerateActionSetNameWithSuffix(monsterWithSuffix, @object.IsFemale, "_villain"), monsterWithSuffix);
				return new LocationCharacter(new AgentData(new SimpleAgentOrigin(@object, -1, null, default(UniqueTroopDescriptor))).Monster(tuple.Item2), new LocationCharacter.AddBehaviorsDelegate(SandBoxManager.Instance.AgentBehaviorManager.AddFixedCharacterBehaviors), "alley_1", true, relation, tuple.Item1, true, false, null, false, true, false);
			}

			// Token: 0x0600100E RID: 4110 RVA: 0x0006303C File Offset: 0x0006123C
			private LocationCharacter CreateConfidentSpyLocationCharacter(CultureObject culture, LocationCharacter.CharacterRelations relation)
			{
				CharacterObject @object = MBObjectManager.Instance.GetObject<CharacterObject>("confident_contender_" + this._currentDifficultySuffix);
				Monster monsterWithSuffix = TaleWorlds.Core.FaceGen.GetMonsterWithSuffix(@object.Race, "_settlement");
				Tuple<string, Monster> tuple = new Tuple<string, Monster>(ActionSetCode.GenerateActionSetNameWithSuffix(monsterWithSuffix, @object.IsFemale, "_villain"), monsterWithSuffix);
				return new LocationCharacter(new AgentData(new SimpleAgentOrigin(@object, -1, null, default(UniqueTroopDescriptor))).Monster(tuple.Item2), new LocationCharacter.AddBehaviorsDelegate(SandBoxManager.Instance.AgentBehaviorManager.AddFixedCharacterBehaviors), "alley_3", true, relation, tuple.Item1, true, false, null, false, true, false);
			}

			// Token: 0x0600100F RID: 4111 RVA: 0x000630DC File Offset: 0x000612DC
			private LocationCharacter CreateDignifiedSpyLocationCharacter(CultureObject culture, LocationCharacter.CharacterRelations relation)
			{
				CharacterObject @object = MBObjectManager.Instance.GetObject<CharacterObject>("dignified_contender_" + this._currentDifficultySuffix);
				Monster monsterWithSuffix = TaleWorlds.Core.FaceGen.GetMonsterWithSuffix(@object.Race, "_settlement");
				Tuple<string, Monster> tuple = new Tuple<string, Monster>(ActionSetCode.GenerateActionSetNameWithSuffix(monsterWithSuffix, @object.IsFemale, "_villain"), monsterWithSuffix);
				return new LocationCharacter(new AgentData(new SimpleAgentOrigin(@object, -1, null, default(UniqueTroopDescriptor))).Monster(tuple.Item2), new LocationCharacter.AddBehaviorsDelegate(SandBoxManager.Instance.AgentBehaviorManager.AddFixedCharacterBehaviors), "alley_3", true, relation, tuple.Item1, true, false, null, false, true, false);
			}

			// Token: 0x06001010 RID: 4112 RVA: 0x0006317C File Offset: 0x0006137C
			private LocationCharacter CreateHardySpyLocationCharacters(CultureObject culture, LocationCharacter.CharacterRelations relation)
			{
				CharacterObject @object = MBObjectManager.Instance.GetObject<CharacterObject>("hardy_contender_" + this._currentDifficultySuffix);
				Monster monsterWithSuffix = TaleWorlds.Core.FaceGen.GetMonsterWithSuffix(@object.Race, "_settlement");
				Tuple<string, Monster> tuple = new Tuple<string, Monster>(ActionSetCode.GenerateActionSetNameWithSuffix(monsterWithSuffix, @object.IsFemale, "_villain"), monsterWithSuffix);
				return new LocationCharacter(new AgentData(new SimpleAgentOrigin(@object, -1, null, default(UniqueTroopDescriptor))).Monster(tuple.Item2), new LocationCharacter.AddBehaviorsDelegate(SandBoxManager.Instance.AgentBehaviorManager.AddFixedCharacterBehaviors), "alley_2", true, relation, tuple.Item1, true, false, null, false, true, false);
			}

			// Token: 0x06001011 RID: 4113 RVA: 0x0006321C File Offset: 0x0006141C
			protected override void OnTimedOut()
			{
				base.AddLog(this._timedOutQuestLog, false);
				base.QuestGiver.AddPower(-5f);
				this.RelationshipChangeWithQuestGiver = -5;
				this._selectedSettlement.Town.Security -= 10f;
				this._selectedSettlement.Town.Loyalty -= 10f;
			}

			// Token: 0x06001012 RID: 4114 RVA: 0x00063287 File Offset: 0x00061487
			internal static void AutoGeneratedStaticCollectObjectsTheSpyPartyIssueQuest(object o, List<object> collectedObjects)
			{
				((TheSpyPartyIssueQuestBehavior.TheSpyPartyIssueQuest)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
			}

			// Token: 0x06001013 RID: 4115 RVA: 0x00063295 File Offset: 0x00061495
			protected override void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
			{
				base.AutoGeneratedInstanceCollectObjects(collectedObjects);
				collectedObjects.Add(this._selectedSettlement);
				TheSpyPartyIssueQuestBehavior.SuspectNpc.AutoGeneratedStaticCollectObjectsSuspectNpc(this._selectedSpy, collectedObjects);
			}

			// Token: 0x06001014 RID: 4116 RVA: 0x000632BB File Offset: 0x000614BB
			internal static object AutoGeneratedGetMemberValue_selectedSettlement(object o)
			{
				return ((TheSpyPartyIssueQuestBehavior.TheSpyPartyIssueQuest)o)._selectedSettlement;
			}

			// Token: 0x06001015 RID: 4117 RVA: 0x000632C8 File Offset: 0x000614C8
			internal static object AutoGeneratedGetMemberValue_selectedSpy(object o)
			{
				return ((TheSpyPartyIssueQuestBehavior.TheSpyPartyIssueQuest)o)._selectedSpy;
			}

			// Token: 0x06001016 RID: 4118 RVA: 0x000632DA File Offset: 0x000614DA
			internal static object AutoGeneratedGetMemberValue_playerLearnedHasHair(object o)
			{
				return ((TheSpyPartyIssueQuestBehavior.TheSpyPartyIssueQuest)o)._playerLearnedHasHair;
			}

			// Token: 0x06001017 RID: 4119 RVA: 0x000632EC File Offset: 0x000614EC
			internal static object AutoGeneratedGetMemberValue_playerLearnedHasNoMarkings(object o)
			{
				return ((TheSpyPartyIssueQuestBehavior.TheSpyPartyIssueQuest)o)._playerLearnedHasNoMarkings;
			}

			// Token: 0x06001018 RID: 4120 RVA: 0x000632FE File Offset: 0x000614FE
			internal static object AutoGeneratedGetMemberValue_playerLearnedHasBigSword(object o)
			{
				return ((TheSpyPartyIssueQuestBehavior.TheSpyPartyIssueQuest)o)._playerLearnedHasBigSword;
			}

			// Token: 0x06001019 RID: 4121 RVA: 0x00063310 File Offset: 0x00061510
			internal static object AutoGeneratedGetMemberValue_playerLearnedHasBeard(object o)
			{
				return ((TheSpyPartyIssueQuestBehavior.TheSpyPartyIssueQuest)o)._playerLearnedHasBeard;
			}

			// Token: 0x0600101A RID: 4122 RVA: 0x00063322 File Offset: 0x00061522
			internal static object AutoGeneratedGetMemberValue_issueDifficultyMultiplier(object o)
			{
				return ((TheSpyPartyIssueQuestBehavior.TheSpyPartyIssueQuest)o)._issueDifficultyMultiplier;
			}

			// Token: 0x0600101B RID: 4123 RVA: 0x00063334 File Offset: 0x00061534
			internal static object AutoGeneratedGetMemberValue_currentDifficultySuffix(object o)
			{
				return ((TheSpyPartyIssueQuestBehavior.TheSpyPartyIssueQuest)o)._currentDifficultySuffix;
			}

			// Token: 0x04000685 RID: 1669
			public const float CustomAgentHealth = 225f;

			// Token: 0x04000686 RID: 1670
			[SaveableField(10)]
			private Settlement _selectedSettlement;

			// Token: 0x04000687 RID: 1671
			[SaveableField(20)]
			private TheSpyPartyIssueQuestBehavior.SuspectNpc _selectedSpy;

			// Token: 0x04000688 RID: 1672
			private MBList<TheSpyPartyIssueQuestBehavior.SuspectNpc> _suspectList;

			// Token: 0x04000689 RID: 1673
			private List<Agent> _alreadySpokenAgents;

			// Token: 0x0400068A RID: 1674
			[SaveableField(30)]
			private bool _playerLearnedHasHair;

			// Token: 0x0400068B RID: 1675
			[SaveableField(40)]
			private bool _playerLearnedHasNoMarkings;

			// Token: 0x0400068C RID: 1676
			[SaveableField(50)]
			private bool _playerLearnedHasBigSword;

			// Token: 0x0400068D RID: 1677
			[SaveableField(60)]
			private bool _playerLearnedHasBeard;

			// Token: 0x0400068E RID: 1678
			private bool _playerWonTheFight;

			// Token: 0x0400068F RID: 1679
			private bool _addSpyNpcsToSettlement;

			// Token: 0x04000690 RID: 1680
			private bool _startFightWithSpy;

			// Token: 0x04000691 RID: 1681
			private bool _checkForBattleResult;

			// Token: 0x04000692 RID: 1682
			private bool _playerManagedToFindSpy;

			// Token: 0x04000693 RID: 1683
			private float _giveClueChange;

			// Token: 0x04000694 RID: 1684
			private CharacterObject _duelCharacter;

			// Token: 0x04000695 RID: 1685
			[SaveableField(70)]
			private float _issueDifficultyMultiplier;

			// Token: 0x04000696 RID: 1686
			[SaveableField(80)]
			private string _currentDifficultySuffix;
		}
	}
}
