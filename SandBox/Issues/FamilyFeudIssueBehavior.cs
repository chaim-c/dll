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
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.Issues;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.LinQuick;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.ObjectSystem;
using TaleWorlds.SaveSystem;

namespace SandBox.Issues
{
	// Token: 0x02000088 RID: 136
	public class FamilyFeudIssueBehavior : CampaignBehaviorBase
	{
		// Token: 0x0600054C RID: 1356 RVA: 0x00023E2C File Offset: 0x0002202C
		public override void RegisterEvents()
		{
			CampaignEvents.OnCheckForIssueEvent.AddNonSerializedListener(this, new Action<Hero>(this.OnCheckForIssue));
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00023E48 File Offset: 0x00022048
		public void OnCheckForIssue(Hero hero)
		{
			Settlement value;
			Hero key;
			if (this.ConditionsHold(hero, out value, out key))
			{
				KeyValuePair<Hero, Settlement> keyValuePair = new KeyValuePair<Hero, Settlement>(key, value);
				Campaign.Current.IssueManager.AddPotentialIssueData(hero, new PotentialIssueData(new PotentialIssueData.StartIssueDelegate(this.OnStartIssue), typeof(FamilyFeudIssueBehavior.FamilyFeudIssue), IssueBase.IssueFrequency.Rare, keyValuePair));
				return;
			}
			Campaign.Current.IssueManager.AddPotentialIssueData(hero, new PotentialIssueData(typeof(FamilyFeudIssueBehavior.FamilyFeudIssue), IssueBase.IssueFrequency.Rare));
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00023EC0 File Offset: 0x000220C0
		private bool ConditionsHold(Hero issueGiver, out Settlement otherVillage, out Hero otherNotable)
		{
			otherVillage = null;
			otherNotable = null;
			if (!issueGiver.IsNotable)
			{
				return false;
			}
			if (issueGiver.IsRuralNotable && issueGiver.CurrentSettlement.IsVillage)
			{
				Settlement bound = issueGiver.CurrentSettlement.Village.Bound;
				if (bound.IsTown)
				{
					foreach (Village village in bound.BoundVillages.WhereQ((Village x) => x != issueGiver.CurrentSettlement.Village))
					{
						Hero hero = village.Settlement.Notables.FirstOrDefaultQ((Hero y) => y.IsRuralNotable && y.CanHaveQuestsOrIssues() && y.GetTraitLevel(DefaultTraits.Mercy) <= 0);
						if (hero != null)
						{
							otherVillage = village.Settlement;
							otherNotable = hero;
						}
					}
					return otherVillage != null;
				}
			}
			return false;
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00023FC8 File Offset: 0x000221C8
		private IssueBase OnStartIssue(in PotentialIssueData pid, Hero issueOwner)
		{
			PotentialIssueData potentialIssueData = pid;
			KeyValuePair<Hero, Settlement> keyValuePair = (KeyValuePair<Hero, Settlement>)potentialIssueData.RelatedObject;
			return new FamilyFeudIssueBehavior.FamilyFeudIssue(issueOwner, keyValuePair.Key, keyValuePair.Value);
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00023FFD File Offset: 0x000221FD
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x04000289 RID: 649
		private const IssueBase.IssueFrequency FamilyFeudIssueFrequency = IssueBase.IssueFrequency.Rare;

		// Token: 0x02000159 RID: 345
		public class FamilyFeudIssueTypeDefiner : SaveableTypeDefiner
		{
			// Token: 0x06000C5C RID: 3164 RVA: 0x0005380E File Offset: 0x00051A0E
			public FamilyFeudIssueTypeDefiner() : base(1087000)
			{
			}

			// Token: 0x06000C5D RID: 3165 RVA: 0x0005381B File Offset: 0x00051A1B
			protected override void DefineClassTypes()
			{
				base.AddClassDefinition(typeof(FamilyFeudIssueBehavior.FamilyFeudIssue), 1, null);
				base.AddClassDefinition(typeof(FamilyFeudIssueBehavior.FamilyFeudIssueQuest), 2, null);
			}
		}

		// Token: 0x0200015A RID: 346
		public class FamilyFeudIssueMissionBehavior : MissionLogic
		{
			// Token: 0x06000C5E RID: 3166 RVA: 0x00053841 File Offset: 0x00051A41
			public FamilyFeudIssueMissionBehavior(Action<Agent, Agent, int> agentHitAction)
			{
				this.OnAgentHitAction = agentHitAction;
			}

			// Token: 0x06000C5F RID: 3167 RVA: 0x00053850 File Offset: 0x00051A50
			public override void OnAgentHit(Agent affectedAgent, Agent affectorAgent, in MissionWeapon affectorWeapon, in Blow blow, in AttackCollisionData attackCollisionData)
			{
				Action<Agent, Agent, int> onAgentHitAction = this.OnAgentHitAction;
				if (onAgentHitAction == null)
				{
					return;
				}
				onAgentHitAction(affectedAgent, affectorAgent, blow.InflictedDamage);
			}

			// Token: 0x040005D2 RID: 1490
			private Action<Agent, Agent, int> OnAgentHitAction;
		}

		// Token: 0x0200015B RID: 347
		public class FamilyFeudIssue : IssueBase
		{
			// Token: 0x170000F5 RID: 245
			// (get) Token: 0x06000C60 RID: 3168 RVA: 0x0005386B File Offset: 0x00051A6B
			public override IssueBase.AlternativeSolutionScaleFlag AlternativeSolutionScaleFlags
			{
				get
				{
					return IssueBase.AlternativeSolutionScaleFlag.FailureRisk;
				}
			}

			// Token: 0x170000F6 RID: 246
			// (get) Token: 0x06000C61 RID: 3169 RVA: 0x0005386E File Offset: 0x00051A6E
			public override int AlternativeSolutionBaseNeededMenCount
			{
				get
				{
					return 3 + MathF.Ceiling(5f * base.IssueDifficultyMultiplier);
				}
			}

			// Token: 0x170000F7 RID: 247
			// (get) Token: 0x06000C62 RID: 3170 RVA: 0x00053883 File Offset: 0x00051A83
			protected override int AlternativeSolutionBaseDurationInDaysInternal
			{
				get
				{
					return 3 + MathF.Ceiling(7f * base.IssueDifficultyMultiplier);
				}
			}

			// Token: 0x170000F8 RID: 248
			// (get) Token: 0x06000C63 RID: 3171 RVA: 0x00053898 File Offset: 0x00051A98
			protected override int RewardGold
			{
				get
				{
					return (int)(350f + 1500f * base.IssueDifficultyMultiplier);
				}
			}

			// Token: 0x170000F9 RID: 249
			// (get) Token: 0x06000C64 RID: 3172 RVA: 0x000538AD File Offset: 0x00051AAD
			// (set) Token: 0x06000C65 RID: 3173 RVA: 0x000538B5 File Offset: 0x00051AB5
			[SaveableProperty(30)]
			public override Hero CounterOfferHero { get; protected set; }

			// Token: 0x170000FA RID: 250
			// (get) Token: 0x06000C66 RID: 3174 RVA: 0x000538BE File Offset: 0x00051ABE
			public override int NeededInfluenceForLordSolution
			{
				get
				{
					return 20;
				}
			}

			// Token: 0x170000FB RID: 251
			// (get) Token: 0x06000C67 RID: 3175 RVA: 0x000538C2 File Offset: 0x00051AC2
			protected override int CompanionSkillRewardXP
			{
				get
				{
					return (int)(500f + 700f * base.IssueDifficultyMultiplier);
				}
			}

			// Token: 0x170000FC RID: 252
			// (get) Token: 0x06000C68 RID: 3176 RVA: 0x000538D8 File Offset: 0x00051AD8
			protected override TextObject AlternativeSolutionStartLog
			{
				get
				{
					TextObject textObject = new TextObject("{=zRJ1bQFO}{ISSUE_GIVER.LINK}, a landowner from {ISSUE_GIVER_SETTLEMENT}, told you about an incident that is about to turn into an ugly feud. One of the youngsters killed another in an accident and the victim's family refused blood money as compensation and wants blood. You decided to leave {COMPANION.LINK} with some men for {RETURN_DAYS} days to let things cool down. They should return with the reward of {REWARD_GOLD}{GOLD_ICON} denars as promised by {ISSUE_GIVER.LINK} after {RETURN_DAYS} days.", null);
					StringHelpers.SetCharacterProperties("ISSUE_GIVER", base.IssueOwner.CharacterObject, textObject, false);
					StringHelpers.SetCharacterProperties("COMPANION", base.AlternativeSolutionHero.CharacterObject, textObject, false);
					textObject.SetTextVariable("ISSUE_GIVER_SETTLEMENT", base.IssueOwner.CurrentSettlement.EncyclopediaLinkWithName);
					textObject.SetTextVariable("RETURN_DAYS", base.GetTotalAlternativeSolutionDurationInDays());
					textObject.SetTextVariable("REWARD_GOLD", this.RewardGold);
					textObject.SetTextVariable("GOLD_ICON", "{=!}<img src=\"General\\Icons\\Coin@2x\" extend=\"8\">");
					return textObject;
				}
			}

			// Token: 0x170000FD RID: 253
			// (get) Token: 0x06000C69 RID: 3177 RVA: 0x00053973 File Offset: 0x00051B73
			public override bool IsThereAlternativeSolution
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170000FE RID: 254
			// (get) Token: 0x06000C6A RID: 3178 RVA: 0x00053976 File Offset: 0x00051B76
			public override bool IsThereLordSolution
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170000FF RID: 255
			// (get) Token: 0x06000C6B RID: 3179 RVA: 0x00053979 File Offset: 0x00051B79
			public override TextObject IssueBriefByIssueGiver
			{
				get
				{
					TextObject textObject = new TextObject("{=7qPda0SA}Yes... We do have a problem. One of my relatives fell victim to his temper during a quarrel and killed a man from {TARGET_VILLAGE}.[ib:normal2][if:convo_dismayed] We offered to pay blood money but the family of the deceased have stubbornly refused it. As it turns out, the deceased is kin to {TARGET_NOTABLE}, an elder of this region and now the men of {TARGET_VILLAGE} have sworn to kill my relative.", null);
					textObject.SetTextVariable("TARGET_VILLAGE", this._targetVillage.Name);
					textObject.SetTextVariable("TARGET_NOTABLE", this._targetNotable.Name);
					return textObject;
				}
			}

			// Token: 0x17000100 RID: 256
			// (get) Token: 0x06000C6C RID: 3180 RVA: 0x000539B4 File Offset: 0x00051BB4
			public override TextObject IssueAcceptByPlayer
			{
				get
				{
					return new TextObject("{=XX3sWsVX}This sounds pretty serious. Go on.", null);
				}
			}

			// Token: 0x17000101 RID: 257
			// (get) Token: 0x06000C6D RID: 3181 RVA: 0x000539C4 File Offset: 0x00051BC4
			public override TextObject IssueQuestSolutionExplanationByIssueGiver
			{
				get
				{
					TextObject textObject = new TextObject("{=mgUoXwZt}My family is concerned for the boy's life. He has gone hiding around the village commons. We need someone who can protect him until [ib:normal][if:convo_normal]{TARGET_NOTABLE.LINK} sees reason, accepts the blood money and ends the feud. We would be eternally grateful, if you can help my relative and take him with you for a while maybe.", null);
					StringHelpers.SetCharacterProperties("TARGET_NOTABLE", this._targetNotable.CharacterObject, textObject, false);
					textObject.SetTextVariable("TARGET_VILLAGE", this._targetVillage.Name);
					return textObject;
				}
			}

			// Token: 0x17000102 RID: 258
			// (get) Token: 0x06000C6E RID: 3182 RVA: 0x00053A0D File Offset: 0x00051C0D
			public override TextObject IssueAlternativeSolutionExplanationByIssueGiver
			{
				get
				{
					TextObject textObject = new TextObject("{=cDYz49kZ}You can keep my relative under your protection for a time until the calls for vengeance die down.[ib:closed][if:convo_pondering] Maybe you can leave one of your warrior companions and {ALTERNATIVE_TROOP_COUNT} men with him to protect him.", null);
					textObject.SetTextVariable("ALTERNATIVE_TROOP_COUNT", base.GetTotalAlternativeSolutionNeededMenCount());
					return textObject;
				}
			}

			// Token: 0x17000103 RID: 259
			// (get) Token: 0x06000C6F RID: 3183 RVA: 0x00053A2C File Offset: 0x00051C2C
			protected override TextObject LordSolutionStartLog
			{
				get
				{
					TextObject textObject = new TextObject("{=oJt4bemH}{QUEST_GIVER.LINK}, a landowner from {QUEST_SETTLEMENT}, told you about an incident that is about to turn into an ugly feud. One young man killed another in an quarrel and the victim's family refused blood money compensation, demanding vengeance instead.", null);
					StringHelpers.SetCharacterProperties("TARGET_NOTABLE", this._targetNotable.CharacterObject, textObject, false);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.IssueOwner.CharacterObject, textObject, false);
					textObject.SetTextVariable("QUEST_SETTLEMENT", base.IssueOwner.CurrentSettlement.EncyclopediaLinkWithName);
					return textObject;
				}
			}

			// Token: 0x17000104 RID: 260
			// (get) Token: 0x06000C70 RID: 3184 RVA: 0x00053A94 File Offset: 0x00051C94
			protected override TextObject LordSolutionCounterOfferRefuseLog
			{
				get
				{
					TextObject textObject = new TextObject("{=JqN5BSjN}As the dispenser of justice in the district, you decided to allow {TARGET_NOTABLE.LINK} to take vengeance for {?TARGET_NOTABLE.GENDER}her{?}his{\\?} kinsman. You failed to protect the culprit as you promised. {QUEST_GIVER.LINK} is furious.", null);
					StringHelpers.SetCharacterProperties("TARGET_NOTABLE", this._targetNotable.CharacterObject, textObject, false);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.IssueOwner.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x17000105 RID: 261
			// (get) Token: 0x06000C71 RID: 3185 RVA: 0x00053AE0 File Offset: 0x00051CE0
			protected override TextObject LordSolutionCounterOfferAcceptLog
			{
				get
				{
					TextObject textObject = new TextObject("{=UxrXNSW7}As the ruler, you have let {TARGET_NOTABLE.LINK} to take {?TARGET_NOTABLE.GENDER}her{?}him{\\?} kinsman's vengeance and failed to protect the boy as you have promised to {QUEST_GIVER.LINK}. {QUEST_GIVER.LINK} is furious.", null);
					StringHelpers.SetCharacterProperties("TARGET_NOTABLE", this._targetNotable.CharacterObject, textObject, false);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.IssueOwner.CharacterObject, textObject, false);
					StringHelpers.SetCharacterProperties("PLAYER", CharacterObject.PlayerCharacter, textObject, false);
					return textObject;
				}
			}

			// Token: 0x17000106 RID: 262
			// (get) Token: 0x06000C72 RID: 3186 RVA: 0x00053B3C File Offset: 0x00051D3C
			public override TextObject IssueLordSolutionExplanationByIssueGiver
			{
				get
				{
					TextObject textObject = new TextObject("{=tsjwrZCZ}I am sure that, as {?PLAYER.GENDER}lady{?}lord{\\?} of this district, you will not let these unlawful threats go unpunished. As the lord of the region, you can talk to {TARGET_NOTABLE.LINK} and force him to accept the blood money.", null);
					StringHelpers.SetCharacterProperties("TARGET_NOTABLE", this._targetNotable.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x17000107 RID: 263
			// (get) Token: 0x06000C73 RID: 3187 RVA: 0x00053B6E File Offset: 0x00051D6E
			public override TextObject IssuePlayerResponseAfterLordExplanation
			{
				get
				{
					return new TextObject("{=A3GfCPUb}I'm not sure about using my authority in this way. Is there any other way to solve this?", null);
				}
			}

			// Token: 0x17000108 RID: 264
			// (get) Token: 0x06000C74 RID: 3188 RVA: 0x00053B7B File Offset: 0x00051D7B
			public override TextObject IssuePlayerResponseAfterAlternativeExplanation
			{
				get
				{
					return new TextObject("{=8EaCJ2uw}What else can I do?", null);
				}
			}

			// Token: 0x17000109 RID: 265
			// (get) Token: 0x06000C75 RID: 3189 RVA: 0x00053B88 File Offset: 0x00051D88
			public override TextObject IssueLordSolutionAcceptByPlayer
			{
				get
				{
					TextObject textObject = new TextObject("{=Du31GKSb}As the magistrate of this district, I hereby order that blood money shall be accepted. This is a crime of passion, not malice. Tell {TARGET_NOTABLE.LINK} to take the silver or face my wrath!", null);
					StringHelpers.SetCharacterProperties("TARGET_NOTABLE", this._targetNotable.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x1700010A RID: 266
			// (get) Token: 0x06000C76 RID: 3190 RVA: 0x00053BBA File Offset: 0x00051DBA
			public override TextObject IssueLordSolutionResponseByIssueGiver
			{
				get
				{
					return new TextObject("{=xNyLPMnx}Thank you, my {?PLAYER.GENDER}lady{?}lord{\\?}, thank you.", null);
				}
			}

			// Token: 0x1700010B RID: 267
			// (get) Token: 0x06000C77 RID: 3191 RVA: 0x00053BC8 File Offset: 0x00051DC8
			public override TextObject IssueLordSolutionCounterOfferExplanationByOtherNpc
			{
				get
				{
					TextObject textObject = new TextObject("{=vjk2q3OT}{?PLAYER.GENDER}Madam{?}Sir{\\?}, {TARGET_NOTABLE.LINK}'s nephew murdered one of my kinsman, [ib:aggressive][if:convo_bared_teeth]and it is our right to take vengeance on the murderer. Custom gives us the right of vengeance. Everyone must know that we are willing to avenge our sons, or others will think little of killing them. Does it do us good to be a clan of old men and women, drowning in silver, if all our sons are slain? Please sir, allow us to take vengeance. We promise we won't let this turn into a senseless blood feud.", null);
					StringHelpers.SetCharacterProperties("TARGET_NOTABLE", this._targetNotable.CharacterObject, textObject, false);
					StringHelpers.SetCharacterProperties("PLAYER", CharacterObject.PlayerCharacter, textObject, false);
					return textObject;
				}
			}

			// Token: 0x1700010C RID: 268
			// (get) Token: 0x06000C78 RID: 3192 RVA: 0x00053C0C File Offset: 0x00051E0C
			public override TextObject IssueLordSolutionCounterOfferBriefByOtherNpc
			{
				get
				{
					return new TextObject("{=JhbbB2dp}My {?PLAYER.GENDER}lady{?}lord{\\?}, may I have a word please?", null);
				}
			}

			// Token: 0x1700010D RID: 269
			// (get) Token: 0x06000C79 RID: 3193 RVA: 0x00053C19 File Offset: 0x00051E19
			public override TextObject IssueLordSolutionCounterOfferAcceptByPlayer
			{
				get
				{
					return new TextObject("{=TIVHLAjy}You may have a point. I hereby revoke my previous decision.", null);
				}
			}

			// Token: 0x1700010E RID: 270
			// (get) Token: 0x06000C7A RID: 3194 RVA: 0x00053C26 File Offset: 0x00051E26
			public override TextObject IssueLordSolutionCounterOfferAcceptResponseByOtherNpc
			{
				get
				{
					return new TextObject("{=A9uSikTY}Thank you my {?PLAYER.GENDER}lady{?}lord{\\?}.", null);
				}
			}

			// Token: 0x1700010F RID: 271
			// (get) Token: 0x06000C7B RID: 3195 RVA: 0x00053C33 File Offset: 0x00051E33
			public override TextObject IssueLordSolutionCounterOfferDeclineByPlayer
			{
				get
				{
					return new TextObject("{=Vs9DfZmJ}No. My word is final. You will have to take the blood money.", null);
				}
			}

			// Token: 0x17000110 RID: 272
			// (get) Token: 0x06000C7C RID: 3196 RVA: 0x00053C40 File Offset: 0x00051E40
			public override TextObject IssueLordSolutionCounterOfferDeclineResponseByOtherNpc
			{
				get
				{
					return new TextObject("{=3oaVUNdr}I hope you won't be [if:convo_disbelief]regret with your decision, my {?PLAYER.GENDER}lady{?}lord{\\?}.", null);
				}
			}

			// Token: 0x17000111 RID: 273
			// (get) Token: 0x06000C7D RID: 3197 RVA: 0x00053C4D File Offset: 0x00051E4D
			public override TextObject IssueQuestSolutionAcceptByPlayer
			{
				get
				{
					return new TextObject("{=VcfZdKcp}Don't worry, I will protect your relative.", null);
				}
			}

			// Token: 0x17000112 RID: 274
			// (get) Token: 0x06000C7E RID: 3198 RVA: 0x00053C5A File Offset: 0x00051E5A
			public override TextObject Title
			{
				get
				{
					return new TextObject("{=ZpDQxmzJ}Family Feud", null);
				}
			}

			// Token: 0x17000113 RID: 275
			// (get) Token: 0x06000C7F RID: 3199 RVA: 0x00053C68 File Offset: 0x00051E68
			public override TextObject Description
			{
				get
				{
					TextObject textObject = new TextObject("{=aSZvZRYC}A relative of {QUEST_GIVER.NAME} kills a relative of {TARGET_NOTABLE.NAME}. {QUEST_GIVER.NAME} offers to pay blood money for the crime but {TARGET_NOTABLE.NAME} wants revenge.", null);
					StringHelpers.SetCharacterProperties("TARGET_NOTABLE", this._targetNotable.CharacterObject, textObject, false);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.IssueOwner.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x17000114 RID: 276
			// (get) Token: 0x06000C80 RID: 3200 RVA: 0x00053CB2 File Offset: 0x00051EB2
			public override TextObject IssueAlternativeSolutionAcceptByPlayer
			{
				get
				{
					TextObject textObject = new TextObject("{=9ZngZ6W7}I will have one of my companions and {REQUIRED_TROOP_AMOUNT} of my men protect your kinsman for {RETURN_DAYS} days. ", null);
					textObject.SetTextVariable("REQUIRED_TROOP_AMOUNT", base.GetTotalAlternativeSolutionNeededMenCount());
					textObject.SetTextVariable("RETURN_DAYS", base.GetTotalAlternativeSolutionDurationInDays());
					return textObject;
				}
			}

			// Token: 0x17000115 RID: 277
			// (get) Token: 0x06000C81 RID: 3201 RVA: 0x00053CE3 File Offset: 0x00051EE3
			public override TextObject IssueDiscussAlternativeSolution
			{
				get
				{
					TextObject textObject = new TextObject("{=n9QRnxbC}I have no doubt that {TARGET_NOTABLE.LINK} will have to accept[ib:closed][if:convo_grateful] the offer after seeing the boy with that many armed men behind him. Thank you, {?PLAYER.GENDER}madam{?}sir{\\?}, for helping to ending this without more blood.", null);
					textObject.SetCharacterProperties("TARGET_NOTABLE", this._targetNotable.CharacterObject, false);
					return textObject;
				}
			}

			// Token: 0x17000116 RID: 278
			// (get) Token: 0x06000C82 RID: 3202 RVA: 0x00053D08 File Offset: 0x00051F08
			public override TextObject IssueAlternativeSolutionResponseByIssueGiver
			{
				get
				{
					TextObject textObject = new TextObject("{=MaGPKGHA}Thank you my {?PLAYER.GENDER}lady{?}lord{\\?}. [if:convo_pondering]I am sure your men will protect the boy and {TARGET_NOTABLE.LINK} will have nothing to do but to accept the blood money. I have to add, I'm ready to pay you {REWARD_GOLD}{GOLD_ICON} denars for your trouble.", null);
					StringHelpers.SetCharacterProperties("PLAYER", CharacterObject.PlayerCharacter, textObject, false);
					StringHelpers.SetCharacterProperties("TARGET_NOTABLE", this._targetNotable.CharacterObject, textObject, false);
					textObject.SetTextVariable("REWARD_GOLD", this.RewardGold);
					textObject.SetTextVariable("GOLD_ICON", "{=!}<img src=\"General\\Icons\\Coin@2x\" extend=\"8\">");
					return textObject;
				}
			}

			// Token: 0x17000117 RID: 279
			// (get) Token: 0x06000C83 RID: 3203 RVA: 0x00053D70 File Offset: 0x00051F70
			public override TextObject IssueAsRumorInSettlement
			{
				get
				{
					TextObject textObject = new TextObject("{=lmVCRD4Q}I hope {QUEST_GIVER.LINK} [if:convo_disbelief]can work out that trouble with {?QUEST_GIVER.GENDER}her{?}his{\\?} kinsman.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.IssueOwner.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x17000118 RID: 280
			// (get) Token: 0x06000C84 RID: 3204 RVA: 0x00053DA4 File Offset: 0x00051FA4
			public override TextObject IssueAlternativeSolutionSuccessLog
			{
				get
				{
					TextObject textObject = new TextObject("{=vS6oZJPA}Your companion {COMPANION.LINK} and your men returns with the news of their success. Apparently {TARGET_NOTABLE.LINK} and {?TARGET_NOTABLE.GENDER}her{?}his{\\?} thugs finds the culprit and tries to murder him but your men manages to drive them away. {COMPANION.LINK} tells you that they bloodied their noses so badly that they wouldn’t dare to try again. {QUEST_GIVER.LINK} is grateful and sends {?QUEST_GIVER.GENDER}her{?}his{\\?} regards with a purse full of {REWARD}{GOLD_ICON} denars.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.IssueOwner.CharacterObject, textObject, false);
					StringHelpers.SetCharacterProperties("COMPANION", base.AlternativeSolutionHero.CharacterObject, textObject, false);
					StringHelpers.SetCharacterProperties("TARGET_NOTABLE", this._targetNotable.CharacterObject, textObject, false);
					textObject.SetTextVariable("REWARD", this.RewardGold);
					textObject.SetTextVariable("GOLD_ICON", "{=!}<img src=\"General\\Icons\\Coin@2x\" extend=\"8\">");
					return textObject;
				}
			}

			// Token: 0x06000C85 RID: 3205 RVA: 0x00053E29 File Offset: 0x00052029
			public FamilyFeudIssue(Hero issueOwner, Hero targetNotable, Settlement targetVillage) : base(issueOwner, CampaignTime.DaysFromNow(30f))
			{
				this._targetNotable = targetNotable;
				this._targetVillage = targetVillage;
			}

			// Token: 0x06000C86 RID: 3206 RVA: 0x00053E4A File Offset: 0x0005204A
			public override void OnHeroCanBeSelectedInInventoryInfoIsRequested(Hero hero, ref bool result)
			{
				this.CommonResrictionInfoIsRequested(hero, ref result);
			}

			// Token: 0x06000C87 RID: 3207 RVA: 0x00053E54 File Offset: 0x00052054
			public override void OnHeroCanHavePartyRoleOrBeGovernorInfoIsRequested(Hero hero, ref bool result)
			{
				this.CommonResrictionInfoIsRequested(hero, ref result);
			}

			// Token: 0x06000C88 RID: 3208 RVA: 0x00053E5E File Offset: 0x0005205E
			public override void OnHeroCanLeadPartyInfoIsRequested(Hero hero, ref bool result)
			{
				this.CommonResrictionInfoIsRequested(hero, ref result);
			}

			// Token: 0x06000C89 RID: 3209 RVA: 0x00053E68 File Offset: 0x00052068
			public override void OnHeroCanHaveQuestOrIssueInfoIsRequested(Hero hero, ref bool result)
			{
				this.CommonResrictionInfoIsRequested(hero, ref result);
			}

			// Token: 0x06000C8A RID: 3210 RVA: 0x00053E72 File Offset: 0x00052072
			private void CommonResrictionInfoIsRequested(Hero hero, ref bool result)
			{
				if (this._targetNotable == hero)
				{
					result = false;
				}
			}

			// Token: 0x06000C8B RID: 3211 RVA: 0x00053E80 File Offset: 0x00052080
			protected override float GetIssueEffectAmountInternal(IssueEffect issueEffect)
			{
				if (issueEffect == DefaultIssueEffects.SettlementSecurity)
				{
					return -1f;
				}
				if (issueEffect == DefaultIssueEffects.IssueOwnerPower)
				{
					return -0.1f;
				}
				return 0f;
			}

			// Token: 0x06000C8C RID: 3212 RVA: 0x00053EA3 File Offset: 0x000520A3
			public override ValueTuple<SkillObject, int> GetAlternativeSolutionSkill(Hero hero)
			{
				return new ValueTuple<SkillObject, int>((hero.GetSkillValue(DefaultSkills.Athletics) >= hero.GetSkillValue(DefaultSkills.Charm)) ? DefaultSkills.Athletics : DefaultSkills.Charm, 120);
			}

			// Token: 0x06000C8D RID: 3213 RVA: 0x00053ED0 File Offset: 0x000520D0
			protected override void LordSolutionConsequenceWithAcceptCounterOffer()
			{
				TraitLevelingHelper.OnIssueSolvedThroughBetrayal(base.IssueOwner, new Tuple<TraitObject, int>[]
				{
					new Tuple<TraitObject, int>(DefaultTraits.Honor, -50)
				});
				this.RelationshipChangeWithIssueOwner = -10;
				ChangeRelationAction.ApplyPlayerRelation(this._targetNotable, 5, true, true);
				base.IssueOwner.CurrentSettlement.Village.Bound.Town.Prosperity -= 5f;
				base.IssueOwner.CurrentSettlement.Village.Bound.Town.Security -= 5f;
			}

			// Token: 0x06000C8E RID: 3214 RVA: 0x00053F69 File Offset: 0x00052169
			protected override void LordSolutionConsequenceWithRefuseCounterOffer()
			{
				this.ApplySuccessRewards();
			}

			// Token: 0x06000C8F RID: 3215 RVA: 0x00053F71 File Offset: 0x00052171
			public override bool LordSolutionCondition(out TextObject explanation)
			{
				if (base.IssueOwner.CurrentSettlement.OwnerClan == Clan.PlayerClan)
				{
					explanation = TextObject.Empty;
					return true;
				}
				explanation = new TextObject("{=9y0zpKUF}You need to be the owner of this settlement!", null);
				return false;
			}

			// Token: 0x06000C90 RID: 3216 RVA: 0x00053FA1 File Offset: 0x000521A1
			public override bool AlternativeSolutionCondition(out TextObject explanation)
			{
				explanation = TextObject.Empty;
				return QuestHelper.CheckRosterForAlternativeSolution(MobileParty.MainParty.MemberRoster, base.GetTotalAlternativeSolutionNeededMenCount(), ref explanation, 2, false);
			}

			// Token: 0x06000C91 RID: 3217 RVA: 0x00053FC2 File Offset: 0x000521C2
			public override bool DoTroopsSatisfyAlternativeSolution(TroopRoster troopRoster, out TextObject explanation)
			{
				explanation = TextObject.Empty;
				return QuestHelper.CheckRosterForAlternativeSolution(troopRoster, base.GetTotalAlternativeSolutionNeededMenCount(), ref explanation, 2, false);
			}

			// Token: 0x06000C92 RID: 3218 RVA: 0x00053FDA File Offset: 0x000521DA
			public override bool IsTroopTypeNeededByAlternativeSolution(CharacterObject character)
			{
				return character.Tier >= 2;
			}

			// Token: 0x06000C93 RID: 3219 RVA: 0x00053FE8 File Offset: 0x000521E8
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
				base.AlternativeSolutionHero.AddSkillXp(skill, (float)((int)(500f + 700f * base.IssueDifficultyMultiplier)));
			}

			// Token: 0x06000C94 RID: 3220 RVA: 0x0005404C File Offset: 0x0005224C
			protected override void AlternativeSolutionEndWithFailureConsequence()
			{
				this.RelationshipChangeWithIssueOwner = -10;
				ChangeRelationAction.ApplyPlayerRelation(this._targetNotable, 5, true, true);
				base.IssueOwner.CurrentSettlement.Village.Bound.Town.Security -= 5f;
				base.IssueOwner.CurrentSettlement.Village.Bound.Town.Prosperity -= 5f;
			}

			// Token: 0x06000C95 RID: 3221 RVA: 0x000540C8 File Offset: 0x000522C8
			private void ApplySuccessRewards()
			{
				GainRenownAction.Apply(Hero.MainHero, 1f, false);
				this.RelationshipChangeWithIssueOwner = 10;
				ChangeRelationAction.ApplyPlayerRelation(this._targetNotable, -5, true, true);
				base.IssueOwner.CurrentSettlement.Village.Bound.Town.Security += 10f;
			}

			// Token: 0x06000C96 RID: 3222 RVA: 0x00054127 File Offset: 0x00052327
			protected override void AfterIssueCreation()
			{
				this.CounterOfferHero = base.IssueOwner.CurrentSettlement.Notables.FirstOrDefault((Hero x) => x.CharacterObject.IsHero && x.CharacterObject.HeroObject != base.IssueOwner);
			}

			// Token: 0x06000C97 RID: 3223 RVA: 0x00054150 File Offset: 0x00052350
			protected override void OnGameLoad()
			{
			}

			// Token: 0x06000C98 RID: 3224 RVA: 0x00054152 File Offset: 0x00052352
			protected override void HourlyTick()
			{
			}

			// Token: 0x06000C99 RID: 3225 RVA: 0x00054154 File Offset: 0x00052354
			protected override QuestBase GenerateIssueQuest(string questId)
			{
				return new FamilyFeudIssueBehavior.FamilyFeudIssueQuest(questId, base.IssueOwner, CampaignTime.DaysFromNow(20f), this._targetVillage, this._targetNotable, this.RewardGold);
			}

			// Token: 0x06000C9A RID: 3226 RVA: 0x0005417E File Offset: 0x0005237E
			public override IssueBase.IssueFrequency GetFrequency()
			{
				return IssueBase.IssueFrequency.Rare;
			}

			// Token: 0x06000C9B RID: 3227 RVA: 0x00054184 File Offset: 0x00052384
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
				return flag == IssueBase.PreconditionFlags.None;
			}

			// Token: 0x06000C9C RID: 3228 RVA: 0x000541DC File Offset: 0x000523DC
			public override bool IssueStayAliveConditions()
			{
				return this._targetNotable != null && this._targetNotable.IsActive && (this.CounterOfferHero == null || (this.CounterOfferHero.IsActive && this.CounterOfferHero.CurrentSettlement == base.IssueSettlement));
			}

			// Token: 0x06000C9D RID: 3229 RVA: 0x0005422C File Offset: 0x0005242C
			protected override void CompleteIssueWithTimedOutConsequences()
			{
			}

			// Token: 0x06000C9E RID: 3230 RVA: 0x0005422E File Offset: 0x0005242E
			internal static void AutoGeneratedStaticCollectObjectsFamilyFeudIssue(object o, List<object> collectedObjects)
			{
				((FamilyFeudIssueBehavior.FamilyFeudIssue)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
			}

			// Token: 0x06000C9F RID: 3231 RVA: 0x0005423C File Offset: 0x0005243C
			protected override void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
			{
				base.AutoGeneratedInstanceCollectObjects(collectedObjects);
				collectedObjects.Add(this._targetVillage);
				collectedObjects.Add(this._targetNotable);
				collectedObjects.Add(this.CounterOfferHero);
			}

			// Token: 0x06000CA0 RID: 3232 RVA: 0x00054269 File Offset: 0x00052469
			internal static object AutoGeneratedGetMemberValueCounterOfferHero(object o)
			{
				return ((FamilyFeudIssueBehavior.FamilyFeudIssue)o).CounterOfferHero;
			}

			// Token: 0x06000CA1 RID: 3233 RVA: 0x00054276 File Offset: 0x00052476
			internal static object AutoGeneratedGetMemberValue_targetVillage(object o)
			{
				return ((FamilyFeudIssueBehavior.FamilyFeudIssue)o)._targetVillage;
			}

			// Token: 0x06000CA2 RID: 3234 RVA: 0x00054283 File Offset: 0x00052483
			internal static object AutoGeneratedGetMemberValue_targetNotable(object o)
			{
				return ((FamilyFeudIssueBehavior.FamilyFeudIssue)o)._targetNotable;
			}

			// Token: 0x040005D3 RID: 1491
			private const int CompanionRequiredSkillLevel = 120;

			// Token: 0x040005D4 RID: 1492
			private const int QuestTimeLimit = 20;

			// Token: 0x040005D5 RID: 1493
			private const int IssueDuration = 30;

			// Token: 0x040005D6 RID: 1494
			private const int TroopTierForAlternativeSolution = 2;

			// Token: 0x040005D7 RID: 1495
			[SaveableField(10)]
			private Settlement _targetVillage;

			// Token: 0x040005D8 RID: 1496
			[SaveableField(20)]
			private Hero _targetNotable;
		}

		// Token: 0x0200015C RID: 348
		public class FamilyFeudIssueQuest : QuestBase
		{
			// Token: 0x17000119 RID: 281
			// (get) Token: 0x06000CA4 RID: 3236 RVA: 0x000542B7 File Offset: 0x000524B7
			public override bool IsRemainingTimeHidden
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700011A RID: 282
			// (get) Token: 0x06000CA5 RID: 3237 RVA: 0x000542BA File Offset: 0x000524BA
			private bool FightEnded
			{
				get
				{
					return this._isCulpritDiedInMissionFight || this._isNotableKnockedDownInMissionFight || this._persuationInDoneAndSuccessfull;
				}
			}

			// Token: 0x1700011B RID: 283
			// (get) Token: 0x06000CA6 RID: 3238 RVA: 0x000542D4 File Offset: 0x000524D4
			public override TextObject Title
			{
				get
				{
					return new TextObject("{=ZpDQxmzJ}Family Feud", null);
				}
			}

			// Token: 0x1700011C RID: 284
			// (get) Token: 0x06000CA7 RID: 3239 RVA: 0x000542E4 File Offset: 0x000524E4
			private TextObject PlayerStartsQuestLogText1
			{
				get
				{
					TextObject textObject = new TextObject("{=rjHQpVDZ}{QUEST_GIVER.LINK} a landowner from {QUEST_GIVER_SETTLEMENT}, told you about an incident that is about to turn into an ugly feud. One of the youngsters killed another during a quarrel and the victim's family refuses the blood money as compensation and wants blood.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					textObject.SetTextVariable("QUEST_GIVER_SETTLEMENT", base.QuestGiver.CurrentSettlement.EncyclopediaLinkWithName);
					return textObject;
				}
			}

			// Token: 0x1700011D RID: 285
			// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x00054334 File Offset: 0x00052534
			private TextObject PlayerStartsQuestLogText2
			{
				get
				{
					TextObject textObject = new TextObject("{=fgRq7kF2}You agreed to talk to {CULPRIT.LINK} in {QUEST_GIVER_SETTLEMENT} first and convince him to go to {TARGET_NOTABLE.LINK} with you in {TARGET_SETTLEMENT} and mediate the issue between them peacefully and end unnecessary bloodshed. {QUEST_GIVER.LINK} said {?QUEST_GIVER.GENDER}she{?}he{\\?} will pay you {REWARD_GOLD} once the boy is safe again.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					StringHelpers.SetCharacterProperties("TARGET_NOTABLE", this._targetNotable.CharacterObject, textObject, false);
					StringHelpers.SetCharacterProperties("CULPRIT", this._culprit.CharacterObject, textObject, false);
					textObject.SetTextVariable("QUEST_GIVER_SETTLEMENT", base.QuestGiver.CurrentSettlement.EncyclopediaLinkWithName);
					textObject.SetTextVariable("TARGET_SETTLEMENT", this._targetSettlement.EncyclopediaLinkWithName);
					textObject.SetTextVariable("REWARD_GOLD", this._rewardGold);
					return textObject;
				}
			}

			// Token: 0x1700011E RID: 286
			// (get) Token: 0x06000CA9 RID: 3241 RVA: 0x000543DC File Offset: 0x000525DC
			private TextObject SuccessQuestSolutionLogText
			{
				get
				{
					TextObject textObject = new TextObject("{=KJ61SXEU}You have successfully protected {CULPRIT.LINK} from harm as you have promised. {QUEST_GIVER.LINK} is grateful for your service and sends his regards with a purse full of {REWARD_GOLD}{GOLD_ICON} denars for your trouble.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					StringHelpers.SetCharacterProperties("CULPRIT", this._culprit.CharacterObject, textObject, false);
					textObject.SetTextVariable("REWARD_GOLD", this._rewardGold);
					textObject.SetTextVariable("GOLD_ICON", "{=!}<img src=\"General\\Icons\\Coin@2x\" extend=\"8\">");
					return textObject;
				}
			}

			// Token: 0x1700011F RID: 287
			// (get) Token: 0x06000CAA RID: 3242 RVA: 0x0005444C File Offset: 0x0005264C
			private TextObject CulpritJoinedPlayerPartyLogText
			{
				get
				{
					TextObject textObject = new TextObject("{=s5fXZf2f}You have convinced {CULPRIT.LINK} to go to {TARGET_SETTLEMENT} to face {TARGET_NOTABLE.LINK} to try to solve this issue peacefully. He agreed on the condition that you protect him from his victim's angry relatives.", null);
					StringHelpers.SetCharacterProperties("CULPRIT", this._culprit.CharacterObject, textObject, false);
					StringHelpers.SetCharacterProperties("TARGET_NOTABLE", this._targetNotable.CharacterObject, textObject, false);
					textObject.SetTextVariable("TARGET_SETTLEMENT", this._targetSettlement.EncyclopediaLinkWithName);
					return textObject;
				}
			}

			// Token: 0x17000120 RID: 288
			// (get) Token: 0x06000CAB RID: 3243 RVA: 0x000544B0 File Offset: 0x000526B0
			private TextObject QuestGiverVillageRaidedBeforeTalkingToCulpritCancel
			{
				get
				{
					TextObject textObject = new TextObject("{=gJG0xmAq}{QUEST_GIVER.LINK}'s village {QUEST_SETTLEMENT} was raided. Your agreement with {QUEST_GIVER.LINK} is canceled.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					textObject.SetTextVariable("QUEST_SETTLEMENT", base.QuestGiver.CurrentSettlement.EncyclopediaLinkWithName);
					return textObject;
				}
			}

			// Token: 0x17000121 RID: 289
			// (get) Token: 0x06000CAC RID: 3244 RVA: 0x00054500 File Offset: 0x00052700
			private TextObject TargetVillageRaidedBeforeTalkingToCulpritCancel
			{
				get
				{
					TextObject textObject = new TextObject("{=WqY4nvHc}{TARGET_NOTABLE.LINK}'s village {TARGET_SETTLEMENT} was raided. Your agreement with {QUEST_GIVER.LINK} is canceled.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					StringHelpers.SetCharacterProperties("TARGET_NOTABLE", this._targetNotable.CharacterObject, textObject, false);
					textObject.SetTextVariable("TARGET_SETTLEMENT", this._targetSettlement.EncyclopediaLinkWithName);
					return textObject;
				}
			}

			// Token: 0x17000122 RID: 290
			// (get) Token: 0x06000CAD RID: 3245 RVA: 0x00054564 File Offset: 0x00052764
			private TextObject CulpritDiedQuestFail
			{
				get
				{
					TextObject textObject = new TextObject("{=6zcG8eng}You tried to defend {CULPRIT.LINK} but you were overcome. {NOTABLE.LINK} took {?NOTABLE.GENDER}her{?}his{\\?} revenge. You failed to protect {CULPRIT.LINK} as promised to {QUEST_GIVER.LINK}. {?QUEST_GIVER.GENDER}she{?}he{\\?} is furious.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					StringHelpers.SetCharacterProperties("CULPRIT", this._culprit.CharacterObject, textObject, false);
					StringHelpers.SetCharacterProperties("NOTABLE", this._targetNotable.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x17000123 RID: 291
			// (get) Token: 0x06000CAE RID: 3246 RVA: 0x000545C8 File Offset: 0x000527C8
			private TextObject PlayerDiedInNotableBattle
			{
				get
				{
					TextObject textObject = new TextObject("{=kG92fjCY}You fell unconscious while defending {CULPRIT.LINK}. {TARGET_NOTABLE.LINK} has taken revenge. You failed to protect {CULPRIT.LINK} as you promised {QUEST_GIVER.LINK}. {?QUEST_GIVER.GENDER}She{?}He{\\?} is furious.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					StringHelpers.SetCharacterProperties("CULPRIT", this._culprit.CharacterObject, textObject, false);
					StringHelpers.SetCharacterProperties("TARGET_NOTABLE", this._targetNotable.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x17000124 RID: 292
			// (get) Token: 0x06000CAF RID: 3247 RVA: 0x0005462C File Offset: 0x0005282C
			private TextObject FailQuestLogText
			{
				get
				{
					TextObject textObject = new TextObject("{=LWjIbTBi}You failed to protect {CULPRIT.LINK} as you promised {QUEST_GIVER.LINK}. {QUEST_GIVER.LINK} is furious.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					StringHelpers.SetCharacterProperties("CULPRIT", this._culprit.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x17000125 RID: 293
			// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x00054678 File Offset: 0x00052878
			private TextObject CulpritNoLongerAClanMember
			{
				get
				{
					TextObject textObject = new TextObject("{=wWrEvkuj}{CULPRIT.LINK} is no longer a member of your clan. Your agreement with {QUEST_GIVER.LINK} was terminated.", null);
					StringHelpers.SetCharacterProperties("CULPRIT", this._culprit.CharacterObject, textObject, false);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x06000CB1 RID: 3249 RVA: 0x000546C4 File Offset: 0x000528C4
			public FamilyFeudIssueQuest(string questId, Hero questGiver, CampaignTime duration, Settlement targetSettlement, Hero targetHero, int rewardGold) : base(questId, questGiver, duration, rewardGold)
			{
				this._targetSettlement = targetSettlement;
				this._targetNotable = targetHero;
				this._culpritJoinedPlayerParty = false;
				this._checkForMissionEvents = false;
				this._culprit = HeroCreator.CreateSpecialHero(MBObjectManager.Instance.GetObject<CharacterObject>("townsman_" + targetSettlement.Culture.StringId), targetSettlement, null, null, -1);
				this._culprit.SetNewOccupation(Occupation.Wanderer);
				ItemObject @object = MBObjectManager.Instance.GetObject<ItemObject>("pugio");
				this._culprit.CivilianEquipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.WeaponItemBeginSlot, new EquipmentElement(@object, null, null, false));
				this._culprit.BattleEquipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.WeaponItemBeginSlot, new EquipmentElement(@object, null, null, false));
				this._notableGangsterCharacterObject = questGiver.CurrentSettlement.MapFaction.Culture.GangleaderBodyguard;
				this._rewardGold = rewardGold;
				this.InitializeQuestDialogs();
				this.SetDialogs();
				base.InitializeQuestOnCreation();
			}

			// Token: 0x06000CB2 RID: 3250 RVA: 0x000547B0 File Offset: 0x000529B0
			private void InitializeQuestDialogs()
			{
				Campaign.Current.ConversationManager.AddDialogFlow(this.GetCulpritDialogFlow(), this);
				Campaign.Current.ConversationManager.AddDialogFlow(this.GetNotableThugDialogFlow(), this);
				Campaign.Current.ConversationManager.AddDialogFlow(this.GetNotableDialogFlowBeforeTalkingToCulprit(), this);
				Campaign.Current.ConversationManager.AddDialogFlow(this.GetNotableDialogFlowAfterTalkingToCulprit(), this);
				Campaign.Current.ConversationManager.AddDialogFlow(this.GetNotableDialogFlowAfterKillingCulprit(), this);
				Campaign.Current.ConversationManager.AddDialogFlow(this.GetNotableDialogFlowAfterPlayerBetrayCulprit(), this);
				Campaign.Current.ConversationManager.AddDialogFlow(this.GetCulpritDialogFlowAfterCulpritJoin(), this);
				Campaign.Current.ConversationManager.AddDialogFlow(this.GetNotableDialogFlowAfterNotableKnowdown(), this);
				Campaign.Current.ConversationManager.AddDialogFlow(this.GetNotableDialogFlowAfterQuestEnd(), this);
				Campaign.Current.ConversationManager.AddDialogFlow(this.GetCulpritDialogFlowAfterQuestEnd(), this);
			}

			// Token: 0x06000CB3 RID: 3251 RVA: 0x00054899 File Offset: 0x00052A99
			protected override void InitializeQuestOnGameLoad()
			{
				this.SetDialogs();
				this.InitializeQuestDialogs();
				this._notableGangsterCharacterObject = MBObjectManager.Instance.GetObject<CharacterObject>("gangster_1");
			}

			// Token: 0x06000CB4 RID: 3252 RVA: 0x000548BC File Offset: 0x00052ABC
			protected override void HourlyTick()
			{
			}

			// Token: 0x06000CB5 RID: 3253 RVA: 0x000548C0 File Offset: 0x00052AC0
			private DialogFlow GetNotableDialogFlowBeforeTalkingToCulprit()
			{
				return DialogFlow.CreateDialogFlow("start", 125).NpcLine(new TextObject("{=dpTHWqwv}Are you the {?PLAYER.GENDER}woman{?}man{\\?} who thinks our blood is cheap, that we will accept silver for the life of one of our own?", null), null, null).Condition(new ConversationSentence.OnConditionDelegate(this.notable_culprit_is_not_near_on_condition)).NpcLine(new TextObject("{=Vd22iVGE}Well {?PLAYER.GENDER}lady{?}sir{\\?}, sorry to disappoint you, but our people have some self-respect.", null), null, null).PlayerLine(new TextObject("{=a3AFjfsU}We will see. ", null), null).NpcLine(new TextObject("{=AeJqCMJc}Yes, you will see. Good day to you. ", null), null, null).CloseDialog();
			}

			// Token: 0x06000CB6 RID: 3254 RVA: 0x00054938 File Offset: 0x00052B38
			private DialogFlow GetNotableDialogFlowAfterKillingCulprit()
			{
				return DialogFlow.CreateDialogFlow("start", 125).NpcLine(new TextObject("{=108Dchvt}Stop! We don't need to fight any longer. We have no quarrel with you as justice has been served.", null), null, null).Condition(() => this._isCulpritDiedInMissionFight && Hero.OneToOneConversationHero == this._targetNotable && !this._playerBetrayedCulprit).NpcLine(new TextObject("{=NMrzr7Me}Now, leave peacefully...", null), null, null).Consequence(delegate
				{
					Campaign.Current.ConversationManager.ConversationEndOneShot += this.CulpritDiedInNotableFightFail;
				}).CloseDialog();
			}

			// Token: 0x06000CB7 RID: 3255 RVA: 0x0005499C File Offset: 0x00052B9C
			private DialogFlow GetNotableDialogFlowAfterPlayerBetrayCulprit()
			{
				return DialogFlow.CreateDialogFlow("start", 125).NpcLine(new TextObject("{=4aiabOd4}I knew you are a reasonable {?PLAYER.GENDER}woman{?}man{\\?}.", null), null, null).Condition(() => this._isCulpritDiedInMissionFight && this._playerBetrayedCulprit && Hero.OneToOneConversationHero == this._targetNotable).NpcLine(new TextObject("{=NMrzr7Me}Now, leave peacefully...", null), null, null).Consequence(delegate
				{
					Campaign.Current.ConversationManager.ConversationEndOneShot += this.CulpritDiedInNotableFightFail;
				}).CloseDialog();
			}

			// Token: 0x06000CB8 RID: 3256 RVA: 0x00054A00 File Offset: 0x00052C00
			private DialogFlow GetCulpritDialogFlowAfterCulpritJoin()
			{
				TextObject textObject = new TextObject("{=56ynu2bW}Yes, {?PLAYER.GENDER}milady{?}sir{\\?}.", null);
				TextObject textObject2 = new TextObject("{=c452Kevh}Well I'm anxious, but I am in your hands now. I trust you will protect me {?PLAYER.GENDER}milady{?}sir{\\?}.", null);
				StringHelpers.SetCharacterProperties("PLAYER", CharacterObject.PlayerCharacter, textObject, false);
				StringHelpers.SetCharacterProperties("PLAYER", CharacterObject.PlayerCharacter, textObject2, false);
				return DialogFlow.CreateDialogFlow("start", 125).NpcLine(textObject, null, null).Condition(() => !this.FightEnded && this._culpritJoinedPlayerParty && Hero.OneToOneConversationHero == this._culprit).PlayerLine(new TextObject("{=p1ETQbzg}Just checking on you.", null), null).NpcLine(textObject2, null, null).CloseDialog();
			}

			// Token: 0x06000CB9 RID: 3257 RVA: 0x00054A8C File Offset: 0x00052C8C
			private DialogFlow GetNotableDialogFlowAfterQuestEnd()
			{
				return DialogFlow.CreateDialogFlow("start", 125).NpcLine(new TextObject("{=UBFS1JLj}I have no problem with the boy anymore,[ib:closed][if:convo_annoyed] okay? Just leave me alone.", null), null, null).Condition(() => this.FightEnded && !this._persuationInDoneAndSuccessfull && Hero.OneToOneConversationHero == this._targetNotable && !this._playerBetrayedCulprit).CloseDialog().NpcLine(new TextObject("{=adbQR9j0}I got my gold, you got your boy.[if:convo_bored2] Now leave me alone...", null), null, null).Condition(() => this.FightEnded && this._persuationInDoneAndSuccessfull && Hero.OneToOneConversationHero == this._targetNotable && !this._playerBetrayedCulprit).CloseDialog();
			}

			// Token: 0x06000CBA RID: 3258 RVA: 0x00054AF5 File Offset: 0x00052CF5
			private DialogFlow GetCulpritDialogFlowAfterQuestEnd()
			{
				return DialogFlow.CreateDialogFlow("start", 125).NpcLine(new TextObject("{=OybG76Kf}Thank you for saving me, sir.[ib:normal][if:convo_astonished] I won't forget what you did here today.", null), null, null).Condition(() => this.FightEnded && Hero.OneToOneConversationHero == this._culprit).CloseDialog();
			}

			// Token: 0x06000CBB RID: 3259 RVA: 0x00054B2C File Offset: 0x00052D2C
			private DialogFlow GetNotableDialogFlowAfterNotableKnowdown()
			{
				return DialogFlow.CreateDialogFlow("start", 125).NpcLine(new TextObject("{=c6GbRQlg}Stop. We don’t need to fight any longer. [ib:closed][if:convo_insulted]You have made your point. We will accept the blood money.", null), (IAgent agent) => this.IsTargetNotable(agent), (IAgent agent) => this.IsMainAgent(agent)).Condition(new ConversationSentence.OnConditionDelegate(this.multi_character_conversation_condition_after_fight)).Consequence(new ConversationSentence.OnConsequenceDelegate(this.multi_character_conversation_consequence_after_fight)).NpcLine(new TextObject("{=pS0bBRjt}You! Go to your family and tell [if:convo_angry]them to send us the blood money.", null), (IAgent agent) => this.IsTargetNotable(agent), (IAgent agent) => this.IsCulprit(agent)).NpcLine(new TextObject("{=nxs2U0Yk}Leave now and never come back! [if:convo_furious]If we ever see you here we will kill you.", null), (IAgent agent) => this.IsTargetNotable(agent), (IAgent agent) => this.IsCulprit(agent)).NpcLine("{=udD7Y7mO}Thank you, my {?PLAYER.GENDER}lady{?}sir{\\?}, for protecting me. I will go and tell {ISSUE_GIVER.LINK} of your success here.", (IAgent agent) => this.IsCulprit(agent), (IAgent agent) => this.IsMainAgent(agent)).Condition(new ConversationSentence.OnConditionDelegate(this.AfterNotableKnowdownEndingCondition)).PlayerLine(new TextObject("{=g8qb3Ame}Thank you.", null), (IAgent agent) => this.IsCulprit(agent)).Consequence(delegate
				{
					Campaign.Current.ConversationManager.ConversationEndOneShot += this.PlayerAndCulpritKnockedDownNotableQuestSuccess;
				}).CloseDialog();
			}

			// Token: 0x06000CBC RID: 3260 RVA: 0x00054C44 File Offset: 0x00052E44
			private bool AfterNotableKnowdownEndingCondition()
			{
				StringHelpers.SetCharacterProperties("ISSUE_GIVER", base.QuestGiver.CharacterObject, null, false);
				return true;
			}

			// Token: 0x06000CBD RID: 3261 RVA: 0x00054C5F File Offset: 0x00052E5F
			private void PlayerAndCulpritKnockedDownNotableQuestSuccess()
			{
				this._conversationAfterFightIsDone = true;
				this.HandleAgentBehaviorAfterQuestConversations();
			}

			// Token: 0x06000CBE RID: 3262 RVA: 0x00054C70 File Offset: 0x00052E70
			private void HandleAgentBehaviorAfterQuestConversations()
			{
				foreach (AccompanyingCharacter accompanyingCharacter in PlayerEncounter.LocationEncounter.CharactersAccompanyingPlayer)
				{
					if (accompanyingCharacter.LocationCharacter.Character == this._culprit.CharacterObject && this._culpritAgent.IsActive())
					{
						accompanyingCharacter.LocationCharacter.SpecialTargetTag = "npc_common";
						accompanyingCharacter.LocationCharacter.CharacterRelation = LocationCharacter.CharacterRelations.Neutral;
						this._culpritAgent.SetMortalityState(Agent.MortalityState.Invulnerable);
						this._culpritAgent.SetTeam(Team.Invalid, false);
						DailyBehaviorGroup behaviorGroup = this._culpritAgent.GetComponent<CampaignAgentComponent>().AgentNavigator.GetBehaviorGroup<DailyBehaviorGroup>();
						behaviorGroup.AddBehavior<WalkingBehavior>();
						behaviorGroup.RemoveBehavior<FollowAgentBehavior>();
						this._culpritAgent.ResetEnemyCaches();
						this._culpritAgent.InvalidateTargetAgent();
						this._culpritAgent.InvalidateAIWeaponSelections();
						this._culpritAgent.SetWatchState(Agent.WatchState.Patrolling);
						if (this._notableAgent != null)
						{
							this._notableAgent.ResetEnemyCaches();
							this._notableAgent.InvalidateTargetAgent();
							this._notableAgent.InvalidateAIWeaponSelections();
							this._notableAgent.SetWatchState(Agent.WatchState.Patrolling);
						}
						this._culpritAgent.TryToSheathWeaponInHand(Agent.HandIndex.OffHand, Agent.WeaponWieldActionType.WithAnimationUninterruptible);
						this._culpritAgent.TryToSheathWeaponInHand(Agent.HandIndex.MainHand, Agent.WeaponWieldActionType.WithAnimationUninterruptible);
					}
				}
				Mission.Current.SetMissionMode(MissionMode.StartUp, false);
			}

			// Token: 0x06000CBF RID: 3263 RVA: 0x00054DE0 File Offset: 0x00052FE0
			private void ApplySuccessConsequences()
			{
				GiveGoldAction.ApplyBetweenCharacters(null, Hero.MainHero, this._rewardGold, false);
				Hero.MainHero.Clan.Renown += 1f;
				this.RelationshipChangeWithQuestGiver = 10;
				ChangeRelationAction.ApplyPlayerRelation(this._targetNotable, -5, true, true);
				base.QuestGiver.CurrentSettlement.Village.Bound.Town.Security += 10f;
				base.CompleteQuestWithSuccess();
			}

			// Token: 0x06000CC0 RID: 3264 RVA: 0x00054E62 File Offset: 0x00053062
			private bool multi_character_conversation_condition_after_fight()
			{
				return !this._conversationAfterFightIsDone && Hero.OneToOneConversationHero == this._targetNotable && this._isNotableKnockedDownInMissionFight;
			}

			// Token: 0x06000CC1 RID: 3265 RVA: 0x00054E81 File Offset: 0x00053081
			private void multi_character_conversation_consequence_after_fight()
			{
				if (Mission.Current.GetMissionBehavior<MissionConversationLogic>() != null)
				{
					Campaign.Current.ConversationManager.AddConversationAgents(new List<Agent>
					{
						this._culpritAgent
					}, true);
				}
				this._conversationAfterFightIsDone = true;
			}

			// Token: 0x06000CC2 RID: 3266 RVA: 0x00054EB8 File Offset: 0x000530B8
			private DialogFlow GetNotableDialogFlowAfterTalkingToCulprit()
			{
				DialogFlow dialogFlow = DialogFlow.CreateDialogFlow("start", 125).NpcLine(new TextObject("{=nh7a3Nog}Well well. Who did you bring to see us? [ib:confident][if:convo_irritable]Did he bring his funeral shroud with him? I hope so. He's not leaving here alive.", null), (IAgent agent) => this.IsTargetNotable(agent), (IAgent agent) => this.IsCulprit(agent)).Condition(new ConversationSentence.OnConditionDelegate(this.multi_character_conversation_on_condition)).NpcLine(new TextObject("{=RsOmvdmU}We have come to talk! Just listen to us please![if:convo_shocked]", null), (IAgent agent) => this.IsCulprit(agent), (IAgent agent) => this.IsTargetNotable(agent)).NpcLine("{=JUjvu4XL}I knew we'd find you eventually. Now you will face justice![if:convo_evil_smile]", (IAgent agent) => this.IsTargetNotable(agent), (IAgent agent) => this.IsCulprit(agent)).PlayerLine("{=UQyCoQCY}Wait! This lad is now under my protection. We have come to talk in peace..", (IAgent agent) => this.IsTargetNotable(agent)).NpcLine("{=7AiP4BwY}What there is to talk about? [if:convo_confused_annoyed]This bastard murdered one of my kinsman, and it is our right to take vengeance on him!", (IAgent agent) => this.IsTargetNotable(agent), (IAgent agent) => this.IsMainAgent(agent)).BeginPlayerOptions().PlayerOption(new TextObject("{=2iVytG2y}I am not convinced. I will protect the accused until you see reason.", null), null).NpcLine(new TextObject("{=4HokUcma}You will regret pushing [if:convo_very_stern]your nose into issues that do not concern you!", null), null, null).NpcLine(new TextObject("{=vjOkDM6C}If you defend a murderer [ib:warrior][if:convo_furious]then you die like a murderer. Boys, kill them all!", null), null, null).Consequence(delegate
				{
					Campaign.Current.ConversationManager.ConversationEndOneShot += delegate()
					{
						this.StartFightWithNotableGang(false);
					};
				}).CloseDialog().PlayerOption(new TextObject("{=boAcQxVV}You're breaking the law.", null), null).Condition(delegate
				{
					if (this._task != null)
					{
						return !this._task.Options.All((PersuasionOptionArgs x) => x.IsBlocked);
					}
					return true;
				}).GotoDialogState("start_notable_family_feud_persuasion").PlayerOption(new TextObject("{=J5cQPqGQ}You are right. You are free to deliver justice as you see fit.", null), null).NpcLine(new TextObject("{=aRPLW15x}Thank you. I knew you are a reasonable[ib:aggressive][if:convo_evil_smile] {?PLAYER.GENDER}woman{?}man{\\?}.", null), null, null).NpcLine(new TextObject("{=k5R4qGtL}What? Are you just going [ib:nervous][if:convo_nervous2]to leave me here to be killed? My kin will never forget this!", null), new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsCulprit), new ConversationSentence.OnMultipleConversationConsequenceDelegate(this.IsMainAgent)).Consequence(delegate
				{
					Campaign.Current.ConversationManager.ConversationEndOneShot += delegate()
					{
						this._playerBetrayedCulprit = true;
						this.StartFightWithNotableGang(this._playerBetrayedCulprit);
					};
				}).CloseDialog();
				this.AddPersuasionDialogs(dialogFlow);
				return dialogFlow;
			}

			// Token: 0x06000CC3 RID: 3267 RVA: 0x00055072 File Offset: 0x00053272
			private bool IsMainAgent(IAgent agent)
			{
				return agent == Mission.Current.MainAgent;
			}

			// Token: 0x06000CC4 RID: 3268 RVA: 0x00055081 File Offset: 0x00053281
			private bool IsTargetNotable(IAgent agent)
			{
				return agent.Character == this._targetNotable.CharacterObject;
			}

			// Token: 0x06000CC5 RID: 3269 RVA: 0x00055096 File Offset: 0x00053296
			private bool IsCulprit(IAgent agent)
			{
				return agent.Character == this._culprit.CharacterObject;
			}

			// Token: 0x06000CC6 RID: 3270 RVA: 0x000550AC File Offset: 0x000532AC
			private bool notable_culprit_is_not_near_on_condition()
			{
				return Hero.OneToOneConversationHero == this._targetNotable && Mission.Current != null && !this.FightEnded && Mission.Current.GetNearbyAgents(Agent.Main.Position.AsVec2, 10f, new MBList<Agent>()).All((Agent a) => a.Character != this._culprit.CharacterObject);
			}

			// Token: 0x06000CC7 RID: 3271 RVA: 0x00055110 File Offset: 0x00053310
			private bool multi_character_conversation_on_condition()
			{
				if (Hero.OneToOneConversationHero != this._targetNotable || Mission.Current == null || this.FightEnded)
				{
					return false;
				}
				MBList<Agent> nearbyAgents = Mission.Current.GetNearbyAgents(Agent.Main.Position.AsVec2, 10f, new MBList<Agent>());
				if (nearbyAgents.IsEmpty<Agent>() || nearbyAgents.All((Agent a) => a.Character != this._culprit.CharacterObject))
				{
					return false;
				}
				foreach (Agent agent in nearbyAgents)
				{
					if (agent.Character == this._culprit.CharacterObject)
					{
						this._culpritAgent = agent;
						if (Mission.Current.GetMissionBehavior<MissionConversationLogic>() != null)
						{
							Campaign.Current.ConversationManager.AddConversationAgents(new List<Agent>
							{
								this._culpritAgent
							}, true);
							break;
						}
						break;
					}
				}
				return true;
			}

			// Token: 0x06000CC8 RID: 3272 RVA: 0x00055204 File Offset: 0x00053404
			private void AddPersuasionDialogs(DialogFlow dialog)
			{
				dialog.AddDialogLine("family_feud_notable_persuasion_check_accepted", "start_notable_family_feud_persuasion", "family_feud_notable_persuasion_start_reservation", "{=6P1ruzsC}Maybe...", null, new ConversationSentence.OnConsequenceDelegate(this.persuasion_start_with_notable_on_consequence), this, 100, null, null, null);
				dialog.AddDialogLine("family_feud_notable_persuasion_failed", "family_feud_notable_persuasion_start_reservation", "persuation_failed", "{=!}{FAILED_PERSUASION_LINE}", new ConversationSentence.OnConditionDelegate(this.persuasion_failed_with_family_feud_notable_on_condition), new ConversationSentence.OnConsequenceDelegate(this.persuasion_failed_with_notable_on_consequence), this, 100, null, null, null);
				dialog.AddDialogLine("family_feud_notable_persuasion_rejected", "persuation_failed", "close_window", "{=vjOkDM6C}If you defend a murderer [ib:warrior][if:convo_furious]then you die like a murderer. Boys, kill them all!", null, new ConversationSentence.OnConsequenceDelegate(this.persuasion_failed_with_notable_start_fight_on_consequence), this, 100, null, null, null);
				dialog.AddDialogLine("family_feud_notable_persuasion_attempt", "family_feud_notable_persuasion_start_reservation", "family_feud_notable_persuasion_select_option", "{CONTINUE_PERSUASION_LINE}", () => !this.persuasion_failed_with_family_feud_notable_on_condition(), null, this, 100, null, null, null);
				dialog.AddDialogLine("family_feud_notable_persuasion_success", "family_feud_notable_persuasion_start_reservation", "close_window", "{=qIQbIjVS}All right! I spare the boy's life. Now get out of my sight[ib:closed][if:convo_nonchalant]", new ConversationSentence.OnConditionDelegate(ConversationManager.GetPersuasionProgressSatisfied), new ConversationSentence.OnConsequenceDelegate(this.persuasion_complete_with_notable_on_consequence), this, int.MaxValue, null, null, null);
				string id = "family_feud_notable_persuasion_select_option_1";
				string inputToken = "family_feud_notable_persuasion_select_option";
				string outputToken = "family_feud_notable_persuasion_selected_option_response";
				string text = "{=!}{FAMILY_FEUD_PERSUADE_ATTEMPT_1}";
				ConversationSentence.OnConditionDelegate conditionDelegate = new ConversationSentence.OnConditionDelegate(this.persuasion_select_option_1_on_condition);
				ConversationSentence.OnConsequenceDelegate consequenceDelegate = new ConversationSentence.OnConsequenceDelegate(this.persuasion_select_option_1_on_consequence);
				ConversationSentence.OnPersuasionOptionDelegate persuasionOptionDelegate = new ConversationSentence.OnPersuasionOptionDelegate(this.persuasion_setup_option_1);
				ConversationSentence.OnClickableConditionDelegate clickableConditionDelegate = new ConversationSentence.OnClickableConditionDelegate(this.persuasion_clickable_option_1_on_condition);
				dialog.AddPlayerLine(id, inputToken, outputToken, text, conditionDelegate, consequenceDelegate, this, 100, clickableConditionDelegate, persuasionOptionDelegate, null, null);
				string id2 = "family_feud_notable_persuasion_select_option_2";
				string inputToken2 = "family_feud_notable_persuasion_select_option";
				string outputToken2 = "family_feud_notable_persuasion_selected_option_response";
				string text2 = "{=!}{FAMILY_FEUD_PERSUADE_ATTEMPT_2}";
				ConversationSentence.OnConditionDelegate conditionDelegate2 = new ConversationSentence.OnConditionDelegate(this.persuasion_select_option_2_on_condition);
				ConversationSentence.OnConsequenceDelegate consequenceDelegate2 = new ConversationSentence.OnConsequenceDelegate(this.persuasion_select_option_2_on_consequence);
				persuasionOptionDelegate = new ConversationSentence.OnPersuasionOptionDelegate(this.persuasion_setup_option_2);
				clickableConditionDelegate = new ConversationSentence.OnClickableConditionDelegate(this.persuasion_clickable_option_2_on_condition);
				dialog.AddPlayerLine(id2, inputToken2, outputToken2, text2, conditionDelegate2, consequenceDelegate2, this, 100, clickableConditionDelegate, persuasionOptionDelegate, null, null);
				string id3 = "family_feud_notable_persuasion_select_option_3";
				string inputToken3 = "family_feud_notable_persuasion_select_option";
				string outputToken3 = "family_feud_notable_persuasion_selected_option_response";
				string text3 = "{=!}{FAMILY_FEUD_PERSUADE_ATTEMPT_3}";
				ConversationSentence.OnConditionDelegate conditionDelegate3 = new ConversationSentence.OnConditionDelegate(this.persuasion_select_option_3_on_condition);
				ConversationSentence.OnConsequenceDelegate consequenceDelegate3 = new ConversationSentence.OnConsequenceDelegate(this.persuasion_select_option_3_on_consequence);
				persuasionOptionDelegate = new ConversationSentence.OnPersuasionOptionDelegate(this.persuasion_setup_option_3);
				clickableConditionDelegate = new ConversationSentence.OnClickableConditionDelegate(this.persuasion_clickable_option_3_on_condition);
				dialog.AddPlayerLine(id3, inputToken3, outputToken3, text3, conditionDelegate3, consequenceDelegate3, this, 100, clickableConditionDelegate, persuasionOptionDelegate, null, null);
				dialog.AddDialogLine("family_feud_notable_persuasion_select_option_reaction", "family_feud_notable_persuasion_selected_option_response", "family_feud_notable_persuasion_start_reservation", "{=D0xDRqvm}{PERSUASION_REACTION}", new ConversationSentence.OnConditionDelegate(this.persuasion_selected_option_response_on_condition), new ConversationSentence.OnConsequenceDelegate(this.persuasion_selected_option_response_on_consequence), this, 100, null, null, null);
			}

			// Token: 0x06000CC9 RID: 3273 RVA: 0x00055445 File Offset: 0x00053645
			private void persuasion_complete_with_notable_on_consequence()
			{
				ConversationManager.EndPersuasion();
				this._persuationInDoneAndSuccessfull = true;
				this.HandleAgentBehaviorAfterQuestConversations();
			}

			// Token: 0x06000CCA RID: 3274 RVA: 0x00055459 File Offset: 0x00053659
			private void persuasion_failed_with_notable_on_consequence()
			{
				ConversationManager.EndPersuasion();
			}

			// Token: 0x06000CCB RID: 3275 RVA: 0x00055460 File Offset: 0x00053660
			private void persuasion_failed_with_notable_start_fight_on_consequence()
			{
				Campaign.Current.ConversationManager.ConversationEndOneShot += delegate()
				{
					this.StartFightWithNotableGang(false);
				};
			}

			// Token: 0x06000CCC RID: 3276 RVA: 0x00055480 File Offset: 0x00053680
			private bool persuasion_failed_with_family_feud_notable_on_condition()
			{
				MBTextManager.SetTextVariable("CONTINUE_PERSUASION_LINE", "{=7B7BhVhV}Let's see what you will come up with...[if:convo_confused_annoyed]", false);
				if (this._task.Options.Any((PersuasionOptionArgs x) => x.IsBlocked))
				{
					MBTextManager.SetTextVariable("CONTINUE_PERSUASION_LINE", "{=wvbiyZfp}What else do you have to say?[if:convo_confused_annoyed]", false);
				}
				if (this._task.Options.All((PersuasionOptionArgs x) => x.IsBlocked) && !ConversationManager.GetPersuasionProgressSatisfied())
				{
					MBTextManager.SetTextVariable("FAILED_PERSUASION_LINE", this._task.FinalFailLine, false);
					return true;
				}
				return false;
			}

			// Token: 0x06000CCD RID: 3277 RVA: 0x00055530 File Offset: 0x00053730
			private void persuasion_selected_option_response_on_consequence()
			{
				Tuple<PersuasionOptionArgs, PersuasionOptionResult> tuple = ConversationManager.GetPersuasionChosenOptions().Last<Tuple<PersuasionOptionArgs, PersuasionOptionResult>>();
				float difficulty = Campaign.Current.Models.PersuasionModel.GetDifficulty(PersuasionDifficulty.MediumHard);
				float moveToNextStageChance;
				float blockRandomOptionChance;
				Campaign.Current.Models.PersuasionModel.GetEffectChances(tuple.Item1, out moveToNextStageChance, out blockRandomOptionChance, difficulty);
				this._task.ApplyEffects(moveToNextStageChance, blockRandomOptionChance);
			}

			// Token: 0x06000CCE RID: 3278 RVA: 0x0005558C File Offset: 0x0005378C
			private bool persuasion_selected_option_response_on_condition()
			{
				PersuasionOptionResult item = ConversationManager.GetPersuasionChosenOptions().Last<Tuple<PersuasionOptionArgs, PersuasionOptionResult>>().Item2;
				MBTextManager.SetTextVariable("PERSUASION_REACTION", PersuasionHelper.GetDefaultPersuasionOptionReaction(item), false);
				return true;
			}

			// Token: 0x06000CCF RID: 3279 RVA: 0x000555BB File Offset: 0x000537BB
			private void persuasion_start_with_notable_on_consequence()
			{
				this._task = this.GetPersuasionTask();
				ConversationManager.StartPersuasion(2f, 1f, 0f, 2f, 2f, 0f, PersuasionDifficulty.MediumHard);
			}

			// Token: 0x06000CD0 RID: 3280 RVA: 0x000555F0 File Offset: 0x000537F0
			private bool persuasion_select_option_1_on_condition()
			{
				if (this._task.Options.Count > 0)
				{
					TextObject textObject = new TextObject("{=bSo9hKwr}{PERSUASION_OPTION_LINE} {SUCCESS_CHANCE}", null);
					textObject.SetTextVariable("SUCCESS_CHANCE", PersuasionHelper.ShowSuccess(this._task.Options.ElementAt(0), false));
					textObject.SetTextVariable("PERSUASION_OPTION_LINE", this._task.Options.ElementAt(0).Line);
					MBTextManager.SetTextVariable("FAMILY_FEUD_PERSUADE_ATTEMPT_1", textObject, false);
					return true;
				}
				return false;
			}

			// Token: 0x06000CD1 RID: 3281 RVA: 0x00055670 File Offset: 0x00053870
			private bool persuasion_select_option_2_on_condition()
			{
				if (this._task.Options.Count > 1)
				{
					TextObject textObject = new TextObject("{=bSo9hKwr}{PERSUASION_OPTION_LINE} {SUCCESS_CHANCE}", null);
					textObject.SetTextVariable("SUCCESS_CHANCE", PersuasionHelper.ShowSuccess(this._task.Options.ElementAt(1), false));
					textObject.SetTextVariable("PERSUASION_OPTION_LINE", this._task.Options.ElementAt(1).Line);
					MBTextManager.SetTextVariable("FAMILY_FEUD_PERSUADE_ATTEMPT_2", textObject, false);
					return true;
				}
				return false;
			}

			// Token: 0x06000CD2 RID: 3282 RVA: 0x000556F0 File Offset: 0x000538F0
			private bool persuasion_select_option_3_on_condition()
			{
				if (this._task.Options.Count > 2)
				{
					TextObject textObject = new TextObject("{=bSo9hKwr}{PERSUASION_OPTION_LINE} {SUCCESS_CHANCE}", null);
					textObject.SetTextVariable("SUCCESS_CHANCE", PersuasionHelper.ShowSuccess(this._task.Options.ElementAt(2), false));
					textObject.SetTextVariable("PERSUASION_OPTION_LINE", this._task.Options.ElementAt(2).Line);
					MBTextManager.SetTextVariable("FAMILY_FEUD_PERSUADE_ATTEMPT_3", textObject, false);
					return true;
				}
				return false;
			}

			// Token: 0x06000CD3 RID: 3283 RVA: 0x00055770 File Offset: 0x00053970
			private void persuasion_select_option_1_on_consequence()
			{
				if (this._task.Options.Count > 0)
				{
					this._task.Options[0].BlockTheOption(true);
				}
			}

			// Token: 0x06000CD4 RID: 3284 RVA: 0x0005579C File Offset: 0x0005399C
			private void persuasion_select_option_2_on_consequence()
			{
				if (this._task.Options.Count > 1)
				{
					this._task.Options[1].BlockTheOption(true);
				}
			}

			// Token: 0x06000CD5 RID: 3285 RVA: 0x000557C8 File Offset: 0x000539C8
			private void persuasion_select_option_3_on_consequence()
			{
				if (this._task.Options.Count > 2)
				{
					this._task.Options[2].BlockTheOption(true);
				}
			}

			// Token: 0x06000CD6 RID: 3286 RVA: 0x000557F4 File Offset: 0x000539F4
			private PersuasionOptionArgs persuasion_setup_option_1()
			{
				return this._task.Options.ElementAt(0);
			}

			// Token: 0x06000CD7 RID: 3287 RVA: 0x00055807 File Offset: 0x00053A07
			private PersuasionOptionArgs persuasion_setup_option_2()
			{
				return this._task.Options.ElementAt(1);
			}

			// Token: 0x06000CD8 RID: 3288 RVA: 0x0005581A File Offset: 0x00053A1A
			private PersuasionOptionArgs persuasion_setup_option_3()
			{
				return this._task.Options.ElementAt(2);
			}

			// Token: 0x06000CD9 RID: 3289 RVA: 0x00055830 File Offset: 0x00053A30
			private bool persuasion_clickable_option_1_on_condition(out TextObject hintText)
			{
				hintText = new TextObject("{=9ACJsI6S}Blocked", null);
				if (this._task.Options.Any<PersuasionOptionArgs>())
				{
					hintText = (this._task.Options.ElementAt(0).IsBlocked ? hintText : TextObject.Empty);
					return !this._task.Options.ElementAt(0).IsBlocked;
				}
				return false;
			}

			// Token: 0x06000CDA RID: 3290 RVA: 0x0005589C File Offset: 0x00053A9C
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

			// Token: 0x06000CDB RID: 3291 RVA: 0x00055908 File Offset: 0x00053B08
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

			// Token: 0x06000CDC RID: 3292 RVA: 0x00055974 File Offset: 0x00053B74
			private PersuasionTask GetPersuasionTask()
			{
				PersuasionTask persuasionTask = new PersuasionTask(0);
				persuasionTask.FinalFailLine = new TextObject("{=rzGqa5oD}Revenge will be taken. Save your breath for the fight...", null);
				persuasionTask.TryLaterLine = new TextObject("{=!}IF YOU SEE THIS. CALL CAMPAIGN TEAM.", null);
				persuasionTask.SpokenLine = new TextObject("{=6P1ruzsC}Maybe...", null);
				PersuasionOptionArgs option = new PersuasionOptionArgs(DefaultSkills.Charm, DefaultTraits.Calculating, TraitEffect.Positive, PersuasionArgumentStrength.Easy, false, new TextObject("{=K9i5SaDc}Blood money is appropriate for a crime of passion. But you kill this boy in cold blood, you will be a real murderer in the eyes of the law, and will no doubt die.", null), null, false, false, false);
				persuasionTask.AddOptionToTask(option);
				PersuasionOptionArgs option2 = new PersuasionOptionArgs(DefaultSkills.Roguery, DefaultTraits.Valor, TraitEffect.Positive, PersuasionArgumentStrength.ExtremelyHard, true, new TextObject("{=FUL8TcYa}I promised to protect the boy at the cost of my life. If you try to harm him, you will bleed for it.", null), null, true, false, false);
				persuasionTask.AddOptionToTask(option2);
				PersuasionOptionArgs option3 = new PersuasionOptionArgs(DefaultSkills.Charm, DefaultTraits.Mercy, TraitEffect.Positive, PersuasionArgumentStrength.Normal, false, new TextObject("{=Ytws5O9S}Some day you may wish to save the life of one of your sons through blood money. If you refuse mercy, mercy may be refused you.", null), null, false, false, false);
				persuasionTask.AddOptionToTask(option3);
				return persuasionTask;
			}

			// Token: 0x06000CDD RID: 3293 RVA: 0x00055A38 File Offset: 0x00053C38
			private void StartFightWithNotableGang(bool playerBetrayedCulprit)
			{
				this._notableAgent = (Agent)MissionConversationLogic.Current.ConversationManager.ConversationAgents[0];
				List<Agent> list = new List<Agent>
				{
					this._culpritAgent
				};
				List<Agent> list2 = new List<Agent>
				{
					this._notableAgent
				};
				MBList<Agent> agents = new MBList<Agent>();
				foreach (Agent agent in Mission.Current.GetNearbyAgents(Agent.Main.Position.AsVec2, 30f, agents))
				{
					if ((CharacterObject)agent.Character == this._notableGangsterCharacterObject)
					{
						list2.Add(agent);
					}
				}
				if (playerBetrayedCulprit)
				{
					Agent.Main.SetTeam(Mission.Current.SpectatorTeam, false);
				}
				else
				{
					list.Add(Agent.Main);
					foreach (Agent agent2 in list2)
					{
						agent2.Defensiveness = 2f;
					}
					this._culpritAgent.Health = 350f;
					this._culpritAgent.BaseHealthLimit = 350f;
					this._culpritAgent.HealthLimit = 350f;
				}
				this._notableAgent.Health = 350f;
				this._notableAgent.BaseHealthLimit = 350f;
				this._notableAgent.HealthLimit = 350f;
				Mission.Current.GetMissionBehavior<MissionFightHandler>().StartCustomFight(list, list2, false, false, delegate(bool isPlayerSideWon)
				{
					if (this._isNotableKnockedDownInMissionFight)
					{
						if (Agent.Main != null && this._notableAgent.Position.DistanceSquared(Agent.Main.Position) < 49f)
						{
							MissionConversationLogic.Current.StartConversation(this._notableAgent, false, false);
							return;
						}
						this.PlayerAndCulpritKnockedDownNotableQuestSuccess();
						return;
					}
					else
					{
						if (Agent.Main != null && this._notableAgent.Position.DistanceSquared(Agent.Main.Position) < 49f)
						{
							MissionConversationLogic.Current.StartConversation(this._notableAgent, false, false);
							return;
						}
						this.CulpritDiedInNotableFightFail();
						return;
					}
				});
			}

			// Token: 0x06000CDE RID: 3294 RVA: 0x00055BEC File Offset: 0x00053DEC
			private void OnAgentHit(Agent affectedAgent, Agent affectorAgent, int damage)
			{
				if (base.IsOngoing && !this._persuationInDoneAndSuccessfull && affectedAgent.Health <= (float)damage && Agent.Main != null)
				{
					if (affectedAgent == this._notableAgent && !this._isNotableKnockedDownInMissionFight)
					{
						affectedAgent.Health = 50f;
						this._isNotableKnockedDownInMissionFight = true;
						Mission.Current.GetMissionBehavior<MissionFightHandler>().EndFight();
					}
					if (affectedAgent == this._culpritAgent && !this._isCulpritDiedInMissionFight)
					{
						Blow b = new Blow
						{
							DamageCalculated = true,
							BaseMagnitude = (float)damage,
							InflictedDamage = damage,
							DamagedPercentage = 1f,
							OwnerId = ((affectorAgent != null) ? affectorAgent.Index : -1)
						};
						affectedAgent.Die(b, Agent.KillInfo.Invalid);
						this._isCulpritDiedInMissionFight = true;
					}
				}
			}

			// Token: 0x06000CDF RID: 3295 RVA: 0x00055CBC File Offset: 0x00053EBC
			protected override void SetDialogs()
			{
				this.OfferDialogFlow = DialogFlow.CreateDialogFlow("issue_classic_quest_start", 100).NpcLine(new TextObject("{=JjXETjYb}Thank you.[ib:demure][if:convo_thinking] I have to add, I'm ready to pay you {REWARD_GOLD}{GOLD_ICON} denars for your trouble. He is hiding somewhere nearby. Go talk to him, and tell him that you're here to sort things out.", null), null, null).Condition(delegate
				{
					MBTextManager.SetTextVariable("REWARD_GOLD", this._rewardGold);
					MBTextManager.SetTextVariable("GOLD_ICON", "{=!}<img src=\"General\\Icons\\Coin@2x\" extend=\"8\">", false);
					return Hero.OneToOneConversationHero == base.QuestGiver;
				}).Consequence(new ConversationSentence.OnConsequenceDelegate(this.QuestAcceptedConsequences)).CloseDialog();
				this.DiscussDialogFlow = DialogFlow.CreateDialogFlow("quest_discuss", 100).NpcLine(new TextObject("{=ndDpjT8s}Have you been able to talk with my boy yet?[if:convo_innocent_smile]", null), null, null).Condition(() => Hero.OneToOneConversationHero == base.QuestGiver).BeginPlayerOptions().PlayerOption(new TextObject("{=ETiAbgHa}I will talk with them right away", null), null).NpcLine(new TextObject("{=qmqTLZ9R}Thank you {?PLAYER.GENDER}madam{?}sir{\\?}. You are a savior.", null), null, null).CloseDialog().PlayerOption(new TextObject("{=18NtjryL}Not yet, but I will soon.", null), null).NpcLine(new TextObject("{=HeIIW3EH}We are waiting for your good news {?PLAYER.GENDER}milady{?}sir{\\?}.", null), null, null).CloseDialog().EndPlayerOptions().CloseDialog();
			}

			// Token: 0x06000CE0 RID: 3296 RVA: 0x00055DA8 File Offset: 0x00053FA8
			private void QuestAcceptedConsequences()
			{
				base.StartQuest();
				base.AddLog(this.PlayerStartsQuestLogText1, false);
				base.AddLog(this.PlayerStartsQuestLogText2, false);
				base.AddTrackedObject(this._targetNotable);
				base.AddTrackedObject(this._culprit);
				Location locationWithId = Settlement.CurrentSettlement.LocationComplex.GetLocationWithId("village_center");
				Settlement.CurrentSettlement.LocationComplex.ChangeLocation(this.CreateCulpritLocationCharacter(Settlement.CurrentSettlement.Culture, LocationCharacter.CharacterRelations.Neutral), null, locationWithId);
			}

			// Token: 0x06000CE1 RID: 3297 RVA: 0x00055E28 File Offset: 0x00054028
			private DialogFlow GetCulpritDialogFlow()
			{
				return DialogFlow.CreateDialogFlow("start", 125).NpcLine(new TextObject("{=w0HPC53e}Who are you? What do you want from me?[ib:nervous][if:convo_bared_teeth]", null), null, null).Condition(() => !this._culpritJoinedPlayerParty && Hero.OneToOneConversationHero == this._culprit).PlayerLine(new TextObject("{=UGTCe2qP}Relax. I've talked with your relative, {QUEST_GIVER.NAME}. I know all about your situation. I'm here to help.", null), null).Condition(delegate
				{
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, null, false);
					return Hero.OneToOneConversationHero == this._culprit;
				}).Consequence(delegate
				{
					this._culprit.SetHasMet();
				}).NpcLine(new TextObject("{=45llLiYG}How will you help? Will you protect me?[ib:normal][if:convo_astonished]", null), null, null).PlayerLine(new TextObject("{=4mwSvCgG}Yes I will. Come now, I will take you with me to {TARGET_NOTABLE.NAME} to resolve this issue peacefully.", null), null).Condition(delegate
				{
					StringHelpers.SetCharacterProperties("TARGET_NOTABLE", this._targetNotable.CharacterObject, null, false);
					return Hero.OneToOneConversationHero == this._culprit;
				}).NpcLine(new TextObject("{=bHRZhYzd}No! I won't go anywhere near them! They'll kill me![ib:closed2][if:convo_stern]", null), null, null).PlayerLine(new TextObject("{=sakSp6H8}You can't hide in the shadows forever. I pledge on my honor to protect you if things turn ugly.", null), null).NpcLine(new TextObject("{=4CFOH0kB}I'm still not sure about all this, but I suppose you're right that I don't have much choice. Let's go get this over.[ib:closed][if:convo_pondering]", null), null, null).Consequence(delegate
				{
					Campaign.Current.ConversationManager.ConversationEndOneShot += this.CulpritJoinedPlayersArmy;
				}).CloseDialog();
			}

			// Token: 0x06000CE2 RID: 3298 RVA: 0x00055F18 File Offset: 0x00054118
			private DialogFlow GetNotableThugDialogFlow()
			{
				TextObject textObject = new TextObject("{=QMaYa25R}If you dare to even breathe a word against {TARGET_NOTABLE.LINK},[ib:aggressive2][if:convo_furious] it will be your last. You got it scum?", null);
				StringHelpers.SetCharacterProperties("TARGET_NOTABLE", this._targetNotable.CharacterObject, textObject, false);
				TextObject textObject2 = new TextObject("{=vGnY4KBO}I care very little for your threats. My business is with {TARGET_NOTABLE.LINK}.", null);
				StringHelpers.SetCharacterProperties("TARGET_NOTABLE", this._targetNotable.CharacterObject, textObject2, false);
				return DialogFlow.CreateDialogFlow("start", 125).NpcLine(textObject, null, null).Condition(delegate
				{
					if (this._notableThugs != null)
					{
						return this._notableThugs.Exists((LocationCharacter x) => x.AgentOrigin == Campaign.Current.ConversationManager.ConversationAgents[0].Origin);
					}
					return false;
				}).PlayerLine(textObject2, null).CloseDialog();
			}

			// Token: 0x06000CE3 RID: 3299 RVA: 0x00055FA0 File Offset: 0x000541A0
			private void CulpritJoinedPlayersArmy()
			{
				AddCompanionAction.Apply(Clan.PlayerClan, this._culprit);
				AddHeroToPartyAction.Apply(this._culprit, MobileParty.MainParty, true);
				base.AddLog(this.CulpritJoinedPlayerPartyLogText, false);
				if (Mission.Current != null)
				{
					DailyBehaviorGroup behaviorGroup = ((Agent)MissionConversationLogic.Current.ConversationManager.ConversationAgents[0]).GetComponent<CampaignAgentComponent>().AgentNavigator.GetBehaviorGroup<DailyBehaviorGroup>();
					FollowAgentBehavior followAgentBehavior = behaviorGroup.AddBehavior<FollowAgentBehavior>();
					behaviorGroup.SetScriptedBehavior<FollowAgentBehavior>();
					followAgentBehavior.SetTargetAgent(Agent.Main);
				}
				this._culpritJoinedPlayerParty = true;
			}

			// Token: 0x06000CE4 RID: 3300 RVA: 0x0005602C File Offset: 0x0005422C
			protected override void RegisterEvents()
			{
				CampaignEvents.MapEventEnded.AddNonSerializedListener(this, new Action<MapEvent>(this.OnMapEventEnded));
				CampaignEvents.VillageBeingRaided.AddNonSerializedListener(this, new Action<Village>(this.OnVillageRaid));
				CampaignEvents.BeforeMissionOpenedEvent.AddNonSerializedListener(this, new Action(this.OnBeforeMissionOpened));
				CampaignEvents.GameMenuOpened.AddNonSerializedListener(this, new Action<MenuCallbackArgs>(this.OnGameMenuOpened));
				CampaignEvents.OnMissionEndedEvent.AddNonSerializedListener(this, new Action<IMission>(this.OnMissionEnd));
				CampaignEvents.OnMissionStartedEvent.AddNonSerializedListener(this, new Action<IMission>(this.OnMissionStarted));
				CampaignEvents.OnSettlementLeftEvent.AddNonSerializedListener(this, new Action<MobileParty, Settlement>(this.OnSettlementLeft));
				CampaignEvents.CompanionRemoved.AddNonSerializedListener(this, new Action<Hero, RemoveCompanionAction.RemoveCompanionDetail>(this.OnCompanionRemoved));
				CampaignEvents.HeroPrisonerTaken.AddNonSerializedListener(this, new Action<PartyBase, Hero>(this.OnPrisonerTaken));
				CampaignEvents.HeroKilledEvent.AddNonSerializedListener(this, new Action<Hero, Hero, KillCharacterAction.KillCharacterActionDetail, bool>(this.OnHeroKilled));
				CampaignEvents.MapEventStarted.AddNonSerializedListener(this, new Action<MapEvent, PartyBase, PartyBase>(this.OnMapEventStarted));
				CampaignEvents.CanMoveToSettlementEvent.AddNonSerializedListener(this, new ReferenceAction<Hero, bool>(this.CanMoveToSettlement));
				CampaignEvents.CanHeroDieEvent.AddNonSerializedListener(this, new ReferenceAction<Hero, KillCharacterAction.KillCharacterActionDetail, bool>(this.CanHeroDie));
				CampaignEvents.LocationCharactersAreReadyToSpawnEvent.AddNonSerializedListener(this, new Action<Dictionary<string, int>>(this.LocationCharactersAreReadyToSpawn));
			}

			// Token: 0x06000CE5 RID: 3301 RVA: 0x0005617C File Offset: 0x0005437C
			private void LocationCharactersAreReadyToSpawn(Dictionary<string, int> unusedUsablePointCount)
			{
				if (!this._culpritJoinedPlayerParty && Settlement.CurrentSettlement == base.QuestGiver.CurrentSettlement)
				{
					Settlement.CurrentSettlement.LocationComplex.GetLocationWithId("village_center").AddLocationCharacters(new CreateLocationCharacterDelegate(this.CreateCulpritLocationCharacter), Settlement.CurrentSettlement.Culture, LocationCharacter.CharacterRelations.Neutral, 1);
				}
			}

			// Token: 0x06000CE6 RID: 3302 RVA: 0x000561D4 File Offset: 0x000543D4
			private void CanMoveToSettlement(Hero hero, ref bool result)
			{
				this.CommonRestrictionInfoIsRequested(hero, ref result);
			}

			// Token: 0x06000CE7 RID: 3303 RVA: 0x000561DE File Offset: 0x000543DE
			public override void OnHeroCanBeSelectedInInventoryInfoIsRequested(Hero hero, ref bool result)
			{
				this.CommonRestrictionInfoIsRequested(hero, ref result);
			}

			// Token: 0x06000CE8 RID: 3304 RVA: 0x000561E8 File Offset: 0x000543E8
			public override void OnHeroCanHavePartyRoleOrBeGovernorInfoIsRequested(Hero hero, ref bool result)
			{
				this.CommonRestrictionInfoIsRequested(hero, ref result);
			}

			// Token: 0x06000CE9 RID: 3305 RVA: 0x000561F2 File Offset: 0x000543F2
			public override void OnHeroCanLeadPartyInfoIsRequested(Hero hero, ref bool result)
			{
				this.CommonRestrictionInfoIsRequested(hero, ref result);
			}

			// Token: 0x06000CEA RID: 3306 RVA: 0x000561FC File Offset: 0x000543FC
			public override void OnHeroCanHaveQuestOrIssueInfoIsRequested(Hero hero, ref bool result)
			{
				this.CommonRestrictionInfoIsRequested(hero, ref result);
			}

			// Token: 0x06000CEB RID: 3307 RVA: 0x00056206 File Offset: 0x00054406
			private void CommonRestrictionInfoIsRequested(Hero hero, ref bool result)
			{
				if (hero == this._culprit || this._targetNotable == hero)
				{
					result = false;
				}
			}

			// Token: 0x06000CEC RID: 3308 RVA: 0x0005621D File Offset: 0x0005441D
			private void OnMapEventStarted(MapEvent mapEvent, PartyBase attackerParty, PartyBase defenderParty)
			{
				if (QuestHelper.CheckMinorMajorCoercion(this, mapEvent, attackerParty))
				{
					QuestHelper.ApplyGenericMinorMajorCoercionConsequences(this, mapEvent);
				}
			}

			// Token: 0x06000CED RID: 3309 RVA: 0x00056230 File Offset: 0x00054430
			private void CanHeroDie(Hero hero, KillCharacterAction.KillCharacterActionDetail causeOfDeath, ref bool result)
			{
				if (hero == this._targetNotable)
				{
					result = false;
					return;
				}
				if (hero == Hero.MainHero && Settlement.CurrentSettlement == this._targetSettlement && Mission.Current != null)
				{
					result = false;
				}
			}

			// Token: 0x06000CEE RID: 3310 RVA: 0x00056260 File Offset: 0x00054460
			private void OnHeroKilled(Hero victim, Hero killer, KillCharacterAction.KillCharacterActionDetail detail, bool showNotification = true)
			{
				if (victim == this._targetNotable)
				{
					TextObject textObject = (detail == KillCharacterAction.KillCharacterActionDetail.Lost) ? this.TargetHeroDisappearedLogText : this.TargetHeroDiedLogText;
					StringHelpers.SetCharacterProperties("QUEST_TARGET", this._targetNotable.CharacterObject, textObject, false);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					base.AddLog(textObject, false);
					base.CompleteQuestWithCancel(null);
				}
			}

			// Token: 0x06000CEF RID: 3311 RVA: 0x000562C9 File Offset: 0x000544C9
			private void OnPrisonerTaken(PartyBase capturer, Hero prisoner)
			{
				if (prisoner == this._culprit)
				{
					base.AddLog(this.FailQuestLogText, false);
					this.TiemoutFailConsequences();
					base.CompleteQuestWithFail(null);
				}
			}

			// Token: 0x06000CF0 RID: 3312 RVA: 0x000562F0 File Offset: 0x000544F0
			private void OnVillageRaid(Village village)
			{
				if (village == this._targetSettlement.Village)
				{
					base.AddLog(this.TargetVillageRaidedBeforeTalkingToCulpritCancel, false);
					base.CompleteQuestWithCancel(null);
					return;
				}
				if (village == base.QuestGiver.CurrentSettlement.Village && !this._culpritJoinedPlayerParty)
				{
					base.AddLog(this.QuestGiverVillageRaidedBeforeTalkingToCulpritCancel, false);
					base.CompleteQuestWithCancel(null);
				}
			}

			// Token: 0x06000CF1 RID: 3313 RVA: 0x00056351 File Offset: 0x00054551
			private void OnCompanionRemoved(Hero companion, RemoveCompanionAction.RemoveCompanionDetail detail)
			{
				if (base.IsOngoing && !this._isCulpritDiedInMissionFight && !this._isPlayerKnockedOutMissionFight && companion == this._culprit)
				{
					base.AddLog(this.CulpritNoLongerAClanMember, false);
					this.TiemoutFailConsequences();
					base.CompleteQuestWithFail(null);
				}
			}

			// Token: 0x06000CF2 RID: 3314 RVA: 0x00056390 File Offset: 0x00054590
			public void OnMissionStarted(IMission iMission)
			{
				if (this._checkForMissionEvents)
				{
					if (PlayerEncounter.LocationEncounter.CharactersAccompanyingPlayer.All((AccompanyingCharacter x) => x.LocationCharacter.Character != this._culprit.CharacterObject))
					{
						LocationCharacter locationCharacterOfHero = LocationComplex.Current.GetLocationCharacterOfHero(this._culprit);
						if (locationCharacterOfHero != null)
						{
							PlayerEncounter.LocationEncounter.AddAccompanyingCharacter(locationCharacterOfHero, true);
						}
					}
					FamilyFeudIssueBehavior.FamilyFeudIssueMissionBehavior missionBehavior = new FamilyFeudIssueBehavior.FamilyFeudIssueMissionBehavior(new Action<Agent, Agent, int>(this.OnAgentHit));
					Mission.Current.AddMissionBehavior(missionBehavior);
					Mission.Current.GetMissionBehavior<MissionConversationLogic>().SetSpawnArea("alley_2");
				}
			}

			// Token: 0x06000CF3 RID: 3315 RVA: 0x00056414 File Offset: 0x00054614
			private void OnMissionEnd(IMission mission)
			{
				if (this._checkForMissionEvents)
				{
					this._notableAgent = null;
					this._culpritAgent = null;
					if (Agent.Main == null)
					{
						base.AddLog(this.PlayerDiedInNotableBattle, false);
						this.RelationshipChangeWithQuestGiver = -10;
						base.QuestGiver.CurrentSettlement.Village.Bound.Town.Prosperity -= 5f;
						base.QuestGiver.CurrentSettlement.Village.Bound.Town.Security -= 5f;
						this._isPlayerKnockedOutMissionFight = true;
						base.CompleteQuestWithFail(null);
						return;
					}
					if (this._isCulpritDiedInMissionFight)
					{
						if (this._playerBetrayedCulprit)
						{
							base.AddLog(this.FailQuestLogText, false);
							TraitLevelingHelper.OnIssueSolvedThroughBetrayal(Hero.MainHero, new Tuple<TraitObject, int>[]
							{
								new Tuple<TraitObject, int>(DefaultTraits.Honor, -50)
							});
							ChangeRelationAction.ApplyPlayerRelation(this._targetNotable, 5, true, true);
						}
						else
						{
							base.AddLog(this.CulpritDiedQuestFail, false);
						}
						this.RelationshipChangeWithQuestGiver = -10;
						base.QuestGiver.CurrentSettlement.Village.Bound.Town.Prosperity -= 5f;
						base.QuestGiver.CurrentSettlement.Village.Bound.Town.Security -= 5f;
						base.CompleteQuestWithFail(null);
						return;
					}
					if (this._persuationInDoneAndSuccessfull)
					{
						base.AddLog(this.SuccessQuestSolutionLogText, false);
						this.ApplySuccessConsequences();
						return;
					}
					if (this._isNotableKnockedDownInMissionFight)
					{
						base.AddLog(this.SuccessQuestSolutionLogText, false);
						this.ApplySuccessConsequences();
					}
				}
			}

			// Token: 0x06000CF4 RID: 3316 RVA: 0x000565B9 File Offset: 0x000547B9
			private void OnGameMenuOpened(MenuCallbackArgs args)
			{
				if (this._culpritJoinedPlayerParty && Hero.MainHero.CurrentSettlement == this._targetSettlement)
				{
					this._checkForMissionEvents = (args.MenuContext.GameMenu.StringId == "village");
				}
			}

			// Token: 0x06000CF5 RID: 3317 RVA: 0x000565F5 File Offset: 0x000547F5
			public void OnSettlementLeft(MobileParty party, Settlement settlement)
			{
				if (party == MobileParty.MainParty)
				{
					if (settlement == this._targetSettlement)
					{
						this._checkForMissionEvents = false;
					}
					if (settlement == base.QuestGiver.CurrentSettlement && this._culpritJoinedPlayerParty)
					{
						base.AddTrackedObject(this._targetSettlement);
					}
				}
			}

			// Token: 0x06000CF6 RID: 3318 RVA: 0x00056634 File Offset: 0x00054834
			public void OnBeforeMissionOpened()
			{
				if (this._checkForMissionEvents)
				{
					Location locationWithId = Settlement.CurrentSettlement.LocationComplex.GetLocationWithId("village_center");
					if (locationWithId != null)
					{
						locationWithId.GetLocationCharacter(this._targetNotable).SpecialTargetTag = "alley_2";
						if (this._notableThugs == null)
						{
							this._notableThugs = new List<LocationCharacter>();
						}
						else
						{
							this._notableThugs.Clear();
						}
						locationWithId.AddLocationCharacters(new CreateLocationCharacterDelegate(this.CreateNotablesThugs), Settlement.CurrentSettlement.Culture, LocationCharacter.CharacterRelations.Neutral, MathF.Ceiling(Campaign.Current.Models.IssueModel.GetIssueDifficultyMultiplier() * 3f));
					}
				}
			}

			// Token: 0x06000CF7 RID: 3319 RVA: 0x000566D8 File Offset: 0x000548D8
			private LocationCharacter CreateCulpritLocationCharacter(CultureObject culture, LocationCharacter.CharacterRelations relation)
			{
				Monster monsterWithSuffix = TaleWorlds.Core.FaceGen.GetMonsterWithSuffix(this._culprit.CharacterObject.Race, "_settlement");
				Tuple<string, Monster> tuple = new Tuple<string, Monster>(ActionSetCode.GenerateActionSetNameWithSuffix(monsterWithSuffix, this._culprit.CharacterObject.IsFemale, "_villager"), monsterWithSuffix);
				return new LocationCharacter(new AgentData(new SimpleAgentOrigin(this._culprit.CharacterObject, -1, null, default(UniqueTroopDescriptor))).Monster(tuple.Item2), new LocationCharacter.AddBehaviorsDelegate(SandBoxManager.Instance.AgentBehaviorManager.AddFirstCompanionBehavior), "alley_2", true, relation, tuple.Item1, true, false, null, false, false, true);
			}

			// Token: 0x06000CF8 RID: 3320 RVA: 0x0005677C File Offset: 0x0005497C
			private LocationCharacter CreateNotablesThugs(CultureObject culture, LocationCharacter.CharacterRelations relation)
			{
				Monster monsterWithSuffix = TaleWorlds.Core.FaceGen.GetMonsterWithSuffix(this._notableGangsterCharacterObject.Race, "_settlement");
				Tuple<string, Monster> tuple = new Tuple<string, Monster>(ActionSetCode.GenerateActionSetNameWithSuffix(monsterWithSuffix, this._notableGangsterCharacterObject.IsFemale, "_villain"), monsterWithSuffix);
				LocationCharacter locationCharacter = new LocationCharacter(new AgentData(new SimpleAgentOrigin(this._notableGangsterCharacterObject, -1, null, default(UniqueTroopDescriptor))).Monster(tuple.Item2), new LocationCharacter.AddBehaviorsDelegate(SandBoxManager.Instance.AgentBehaviorManager.AddFixedCharacterBehaviors), "alley_2", true, relation, tuple.Item1, true, false, null, false, false, true);
				this._notableThugs.Add(locationCharacter);
				return locationCharacter;
			}

			// Token: 0x06000CF9 RID: 3321 RVA: 0x00056820 File Offset: 0x00054A20
			private void OnMapEventEnded(MapEvent mapEvent)
			{
				if (mapEvent.IsPlayerMapEvent && this._culpritJoinedPlayerParty && !MobileParty.MainParty.MemberRoster.GetTroopRoster().Exists((TroopRosterElement x) => x.Character == this._culprit.CharacterObject))
				{
					base.AddLog(this.FailQuestLogText, false);
					this.TiemoutFailConsequences();
					base.CompleteQuestWithFail(null);
				}
			}

			// Token: 0x06000CFA RID: 3322 RVA: 0x0005687A File Offset: 0x00054A7A
			private void CulpritDiedInNotableFightFail()
			{
				this._conversationAfterFightIsDone = true;
				this.HandleAgentBehaviorAfterQuestConversations();
			}

			// Token: 0x06000CFB RID: 3323 RVA: 0x0005688C File Offset: 0x00054A8C
			protected override void OnFinalize()
			{
				if (this._culprit.IsPlayerCompanion)
				{
					if (this._culprit.IsPrisoner)
					{
						EndCaptivityAction.ApplyByEscape(this._culprit, null);
					}
					RemoveCompanionAction.ApplyAfterQuest(Clan.PlayerClan, this._culprit);
				}
				if (this._culprit.IsAlive)
				{
					this._culprit.Clan = null;
					KillCharacterAction.ApplyByRemove(this._culprit, false, true);
				}
			}

			// Token: 0x06000CFC RID: 3324 RVA: 0x000568F5 File Offset: 0x00054AF5
			protected override void OnTimedOut()
			{
				base.AddLog(this.FailQuestLogText, false);
				this.TiemoutFailConsequences();
			}

			// Token: 0x06000CFD RID: 3325 RVA: 0x0005690C File Offset: 0x00054B0C
			private void TiemoutFailConsequences()
			{
				TraitLevelingHelper.OnIssueSolvedThroughBetrayal(base.QuestGiver, new Tuple<TraitObject, int>[]
				{
					new Tuple<TraitObject, int>(DefaultTraits.Honor, -50)
				});
				this.RelationshipChangeWithQuestGiver = -10;
				base.QuestGiver.CurrentSettlement.Village.Bound.Town.Prosperity -= 5f;
				base.QuestGiver.CurrentSettlement.Village.Bound.Town.Security -= 5f;
			}

			// Token: 0x06000CFE RID: 3326 RVA: 0x00056997 File Offset: 0x00054B97
			internal static void AutoGeneratedStaticCollectObjectsFamilyFeudIssueQuest(object o, List<object> collectedObjects)
			{
				((FamilyFeudIssueBehavior.FamilyFeudIssueQuest)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
			}

			// Token: 0x06000CFF RID: 3327 RVA: 0x000569A5 File Offset: 0x00054BA5
			protected override void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
			{
				base.AutoGeneratedInstanceCollectObjects(collectedObjects);
				collectedObjects.Add(this._targetSettlement);
				collectedObjects.Add(this._targetNotable);
				collectedObjects.Add(this._culprit);
			}

			// Token: 0x06000D00 RID: 3328 RVA: 0x000569D2 File Offset: 0x00054BD2
			internal static object AutoGeneratedGetMemberValue_targetSettlement(object o)
			{
				return ((FamilyFeudIssueBehavior.FamilyFeudIssueQuest)o)._targetSettlement;
			}

			// Token: 0x06000D01 RID: 3329 RVA: 0x000569DF File Offset: 0x00054BDF
			internal static object AutoGeneratedGetMemberValue_targetNotable(object o)
			{
				return ((FamilyFeudIssueBehavior.FamilyFeudIssueQuest)o)._targetNotable;
			}

			// Token: 0x06000D02 RID: 3330 RVA: 0x000569EC File Offset: 0x00054BEC
			internal static object AutoGeneratedGetMemberValue_culprit(object o)
			{
				return ((FamilyFeudIssueBehavior.FamilyFeudIssueQuest)o)._culprit;
			}

			// Token: 0x06000D03 RID: 3331 RVA: 0x000569F9 File Offset: 0x00054BF9
			internal static object AutoGeneratedGetMemberValue_culpritJoinedPlayerParty(object o)
			{
				return ((FamilyFeudIssueBehavior.FamilyFeudIssueQuest)o)._culpritJoinedPlayerParty;
			}

			// Token: 0x06000D04 RID: 3332 RVA: 0x00056A0B File Offset: 0x00054C0B
			internal static object AutoGeneratedGetMemberValue_checkForMissionEvents(object o)
			{
				return ((FamilyFeudIssueBehavior.FamilyFeudIssueQuest)o)._checkForMissionEvents;
			}

			// Token: 0x06000D05 RID: 3333 RVA: 0x00056A1D File Offset: 0x00054C1D
			internal static object AutoGeneratedGetMemberValue_rewardGold(object o)
			{
				return ((FamilyFeudIssueBehavior.FamilyFeudIssueQuest)o)._rewardGold;
			}

			// Token: 0x040005DA RID: 1498
			private const int CustomCulpritAgentHealth = 350;

			// Token: 0x040005DB RID: 1499
			private const int CustomTargetNotableAgentHealth = 350;

			// Token: 0x040005DC RID: 1500
			public const string CommonAreaTag = "alley_2";

			// Token: 0x040005DD RID: 1501
			[SaveableField(10)]
			private readonly Settlement _targetSettlement;

			// Token: 0x040005DE RID: 1502
			[SaveableField(20)]
			private Hero _targetNotable;

			// Token: 0x040005DF RID: 1503
			[SaveableField(30)]
			private Hero _culprit;

			// Token: 0x040005E0 RID: 1504
			[SaveableField(40)]
			private bool _culpritJoinedPlayerParty;

			// Token: 0x040005E1 RID: 1505
			[SaveableField(50)]
			private bool _checkForMissionEvents;

			// Token: 0x040005E2 RID: 1506
			[SaveableField(70)]
			private int _rewardGold;

			// Token: 0x040005E3 RID: 1507
			private bool _isCulpritDiedInMissionFight;

			// Token: 0x040005E4 RID: 1508
			private bool _isPlayerKnockedOutMissionFight;

			// Token: 0x040005E5 RID: 1509
			private bool _isNotableKnockedDownInMissionFight;

			// Token: 0x040005E6 RID: 1510
			private bool _conversationAfterFightIsDone;

			// Token: 0x040005E7 RID: 1511
			private bool _persuationInDoneAndSuccessfull;

			// Token: 0x040005E8 RID: 1512
			private bool _playerBetrayedCulprit;

			// Token: 0x040005E9 RID: 1513
			private Agent _notableAgent;

			// Token: 0x040005EA RID: 1514
			private Agent _culpritAgent;

			// Token: 0x040005EB RID: 1515
			private CharacterObject _notableGangsterCharacterObject;

			// Token: 0x040005EC RID: 1516
			private List<LocationCharacter> _notableThugs;

			// Token: 0x040005ED RID: 1517
			private PersuasionTask _task;

			// Token: 0x040005EE RID: 1518
			private const PersuasionDifficulty Difficulty = PersuasionDifficulty.MediumHard;
		}
	}
}
