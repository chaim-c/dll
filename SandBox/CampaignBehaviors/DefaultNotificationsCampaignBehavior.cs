﻿using System;
using System.Collections.Generic;
using Helpers;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.CampaignSystem.Issues;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Buildings;
using TaleWorlds.CampaignSystem.Siege;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.LinQuick;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

namespace SandBox.CampaignBehaviors
{
	// Token: 0x020000AA RID: 170
	public class DefaultNotificationsCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x060007BB RID: 1979 RVA: 0x0003AC40 File Offset: 0x00038E40
		public override void RegisterEvents()
		{
			CampaignEvents.HourlyTickEvent.AddNonSerializedListener(this, new Action(this.OnHourlyTick));
			CampaignEvents.HeroRelationChanged.AddNonSerializedListener(this, new Action<Hero, Hero, int, bool, ChangeRelationAction.ChangeRelationDetail, Hero, Hero>(this.OnRelationChanged));
			CampaignEvents.HeroLevelledUp.AddNonSerializedListener(this, new Action<Hero, bool>(this.OnHeroLevelledUp));
			CampaignEvents.HeroGainedSkill.AddNonSerializedListener(this, new Action<Hero, SkillObject, int, bool>(this.OnHeroGainedSkill));
			CampaignEvents.OnClanChangedKingdomEvent.AddNonSerializedListener(this, new Action<Clan, Kingdom, Kingdom, ChangeKingdomAction.ChangeKingdomActionDetail, bool>(this.OnClanChangedFaction));
			CampaignEvents.ArmyCreated.AddNonSerializedListener(this, new Action<Army>(this.OnArmyCreated));
			CampaignEvents.OnSettlementOwnerChangedEvent.AddNonSerializedListener(this, new Action<Settlement, bool, Hero, Hero, Hero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail>(this.OnSettlementOwnerChanged));
			CampaignEvents.RenownGained.AddNonSerializedListener(this, new Action<Hero, int, bool>(this.OnRenownGained));
			CampaignEvents.HeroesMarried.AddNonSerializedListener(this, new Action<Hero, Hero, bool>(this.OnHeroesMarried));
			CampaignEvents.PartyRemovedFromArmyEvent.AddNonSerializedListener(this, new Action<MobileParty>(this.OnPartyRemovedFromArmy));
			CampaignEvents.ArmyDispersed.AddNonSerializedListener(this, new Action<Army, Army.ArmyDispersionReason, bool>(this.OnArmyDispersed));
			CampaignEvents.OnPartyJoinedArmyEvent.AddNonSerializedListener(this, new Action<MobileParty>(this.OnPartyJoinedArmy));
			CampaignEvents.OnChildConceivedEvent.AddNonSerializedListener(this, new Action<Hero>(this.OnChildConceived));
			CampaignEvents.OnGivenBirthEvent.AddNonSerializedListener(this, new Action<Hero, List<Hero>, int>(this.OnGivenBirth));
			CampaignEvents.HeroKilledEvent.AddNonSerializedListener(this, new Action<Hero, Hero, KillCharacterAction.KillCharacterActionDetail, bool>(this.OnHeroKilled));
			CampaignEvents.OnArmyLeaderThinkEvent.AddNonSerializedListener(this, new Action<Hero, Army.ArmyLeaderThinkReason>(this.OnArmyLeaderThink));
			CampaignEvents.HeroOrPartyTradedGold.AddNonSerializedListener(this, new Action<ValueTuple<Hero, PartyBase>, ValueTuple<Hero, PartyBase>, ValueTuple<int, string>, bool>(this.OnHeroOrPartyTradedGold));
			CampaignEvents.OnTroopsDesertedEvent.AddNonSerializedListener(this, new Action<MobileParty, TroopRoster>(this.OnTroopsDeserted));
			CampaignEvents.ClanTierIncrease.AddNonSerializedListener(this, new Action<Clan, bool>(this.OnClanTierIncreased));
			CampaignEvents.OnSiegeBombardmentHitEvent.AddNonSerializedListener(this, new Action<MobileParty, Settlement, BattleSideEnum, SiegeEngineType, SiegeBombardTargets>(this.OnSiegeBombardmentHit));
			CampaignEvents.OnSiegeBombardmentWallHitEvent.AddNonSerializedListener(this, new Action<MobileParty, Settlement, BattleSideEnum, SiegeEngineType, bool>(this.OnSiegeBombardmentWallHit));
			CampaignEvents.OnSiegeEngineDestroyedEvent.AddNonSerializedListener(this, new Action<MobileParty, Settlement, BattleSideEnum, SiegeEngineType>(this.OnSiegeEngineDestroyed));
			CampaignEvents.BattleStarted.AddNonSerializedListener(this, new Action<PartyBase, PartyBase, object, bool>(this.OnBattleStarted));
			CampaignEvents.OnSiegeEventStartedEvent.AddNonSerializedListener(this, new Action<SiegeEvent>(this.OnSiegeEventStarted));
			CampaignEvents.ItemsLooted.AddNonSerializedListener(this, new Action<MobileParty, ItemRoster>(this.OnItemsLooted));
			CampaignEvents.OnHideoutSpottedEvent.AddNonSerializedListener(this, new Action<PartyBase, PartyBase>(this.OnHideoutSpotted));
			CampaignEvents.OnHeroSharedFoodWithAnotherHeroEvent.AddNonSerializedListener(this, new Action<Hero, Hero, float>(this.OnHeroSharedFoodWithAnotherHero));
			CampaignEvents.OnQuestStartedEvent.AddNonSerializedListener(this, new Action<QuestBase>(this.OnQuestStarted));
			CampaignEvents.OnQuestCompletedEvent.AddNonSerializedListener(this, new Action<QuestBase, QuestBase.QuestCompleteDetails>(this.OnQuestCompleted));
			CampaignEvents.QuestLogAddedEvent.AddNonSerializedListener(this, new Action<QuestBase, bool>(this.OnQuestLogAdded));
			CampaignEvents.HeroPrisonerTaken.AddNonSerializedListener(this, new Action<PartyBase, Hero>(this.OnPrisonerTaken));
			CampaignEvents.HeroPrisonerReleased.AddNonSerializedListener(this, new Action<Hero, PartyBase, IFaction, EndCaptivityDetail>(this.OnHeroPrisonerReleased));
			CampaignEvents.OnClanDestroyedEvent.AddNonSerializedListener(this, new Action<Clan>(this.OnClanDestroyed));
			CampaignEvents.OnIssueUpdatedEvent.AddNonSerializedListener(this, new Action<IssueBase, IssueBase.IssueUpdateDetails, Hero>(this.OnIssueUpdated));
			CampaignEvents.HeroOrPartyGaveItem.AddNonSerializedListener(this, new Action<ValueTuple<Hero, PartyBase>, ValueTuple<Hero, PartyBase>, ItemRosterElement, bool>(this.OnHeroOrPartyGaveItem));
			CampaignEvents.RebellionFinished.AddNonSerializedListener(this, new Action<Settlement, Clan>(this.OnRebellionFinished));
			CampaignEvents.TournamentFinished.AddNonSerializedListener(this, new Action<CharacterObject, MBReadOnlyList<CharacterObject>, Town, ItemObject>(this.OnTournamentFinished));
			CampaignEvents.OnBuildingLevelChangedEvent.AddNonSerializedListener(this, new Action<Town, Building, int>(this.OnBuildingLevelChanged));
			CampaignEvents.CompanionRemoved.AddNonSerializedListener(this, new Action<Hero, RemoveCompanionAction.RemoveCompanionDetail>(this.OnCompanionRemoved));
			CampaignEvents.OnHeroTeleportationRequestedEvent.AddNonSerializedListener(this, new Action<Hero, Settlement, MobileParty, TeleportHeroAction.TeleportationDetail>(this.OnHeroTeleportationRequested));
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0003AFE8 File Offset: 0x000391E8
		private void OnCompanionRemoved(Hero hero, RemoveCompanionAction.RemoveCompanionDetail detail)
		{
			if (detail == RemoveCompanionAction.RemoveCompanionDetail.ByTurningToLord)
			{
				TextObject textObject = new TextObject("{=2Lj0WkSF}{COMPANION.NAME} is now a {?COMPANION.GENDER}noblewoman{?}lord{\\?} of the {KINGDOM}.", null);
				textObject.SetCharacterProperties("COMPANION", hero.CharacterObject, false);
				textObject.SetTextVariable("KINGDOM", Clan.PlayerClan.Kingdom.Name);
				MBInformationManager.AddQuickInformation(textObject, 0, null, "event:/ui/notification/relation");
			}
			if (detail == RemoveCompanionAction.RemoveCompanionDetail.Fire)
			{
				TextObject textObject2 = new TextObject("{=4zdyeTGn}{COMPANION.NAME} left your clan.", null);
				textObject2.SetCharacterProperties("COMPANION", hero.CharacterObject, false);
				MBInformationManager.AddQuickInformation(textObject2, 0, null, "event:/ui/notification/relation");
			}
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0003B069 File Offset: 0x00039269
		public override void SyncData(IDataStore dataStore)
		{
			dataStore.SyncData<List<Tuple<bool, float>>>("_foodNotificationList", ref this._foodNotificationList);
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x0003B07D File Offset: 0x0003927D
		private void OnHourlyTick()
		{
			if (MobileParty.MainParty.Army != null)
			{
				this.CheckFoodNotifications();
			}
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x0003B094 File Offset: 0x00039294
		private void OnIssueUpdated(IssueBase issue, IssueBase.IssueUpdateDetails details, Hero issueSolver)
		{
			if (details == IssueBase.IssueUpdateDetails.PlayerSentTroopsToQuest)
			{
				TextObject textObject = GameTexts.FindText("str_issue_updated", details.ToString());
				textObject.SetTextVariable("ISSUE_NAME", issue.Title);
				MBInformationManager.AddQuickInformation(textObject, 0, null, "event:/ui/notification/quest_start");
				return;
			}
			if (details == IssueBase.IssueUpdateDetails.SentTroopsFinishedQuest)
			{
				TextObject textObject2 = GameTexts.FindText("str_issue_updated", details.ToString());
				textObject2.SetTextVariable("ISSUE_NAME", issue.Title);
				MBInformationManager.AddQuickInformation(textObject2, 0, null, "event:/ui/notification/quest_finished");
				return;
			}
			if (details == IssueBase.IssueUpdateDetails.SentTroopsFailedQuest)
			{
				TextObject textObject3 = GameTexts.FindText("str_issue_updated", details.ToString());
				textObject3.SetTextVariable("ISSUE_NAME", issue.Title);
				MBInformationManager.AddQuickInformation(textObject3, 0, null, "event:/ui/notification/quest_fail");
			}
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0003B14E File Offset: 0x0003934E
		private void OnQuestLogAdded(QuestBase quest, bool hideInformation)
		{
			if (!hideInformation)
			{
				TextObject textObject = GameTexts.FindText("str_quest_log_added", null);
				textObject.SetTextVariable("TITLE", quest.Title);
				MBInformationManager.AddQuickInformation(textObject, 0, null, "event:/ui/notification/quest_update");
			}
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0003B17C File Offset: 0x0003937C
		private void OnQuestCompleted(QuestBase quest, QuestBase.QuestCompleteDetails detail)
		{
			if (detail == QuestBase.QuestCompleteDetails.Success)
			{
				TextObject textObject = GameTexts.FindText("str_quest_completed", detail.ToString());
				textObject.SetTextVariable("QUEST_TITLE", quest.Title);
				MBInformationManager.AddQuickInformation(textObject, 0, null, "event:/ui/notification/quest_finished");
				return;
			}
			if (detail - QuestBase.QuestCompleteDetails.Cancel > 2)
			{
				return;
			}
			TextObject textObject2 = GameTexts.FindText("str_quest_completed", detail.ToString());
			textObject2.SetTextVariable("QUEST_TITLE", quest.Title);
			MBInformationManager.AddQuickInformation(textObject2, 0, null, "event:/ui/notification/quest_fail");
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x0003B1FF File Offset: 0x000393FF
		private void OnQuestStarted(QuestBase quest)
		{
			TextObject textObject = GameTexts.FindText("str_quest_started", null);
			textObject.SetTextVariable("QUEST_TITLE", quest.Title);
			MBInformationManager.AddQuickInformation(textObject, 0, null, "event:/ui/notification/quest_start");
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x0003B22C File Offset: 0x0003942C
		private void OnRenownGained(Hero hero, int gainedRenown, bool doNotNotifyPlayer)
		{
			if (hero.Clan == Clan.PlayerClan && !doNotNotifyPlayer)
			{
				TextObject textObject;
				if (hero.PartyBelongedTo != null)
				{
					textObject = GameTexts.FindText("str_party_gained_renown", null);
					textObject.SetTextVariable("PARTY", hero.PartyBelongedTo.Name);
				}
				else
				{
					textObject = GameTexts.FindText("str_clan_gained_renown", null);
				}
				textObject.SetTextVariable("NEW_RENOWN", string.Format("{0:0.#}", hero.Clan.Renown));
				textObject.SetTextVariable("AMOUNT_TO_ADD", gainedRenown);
				textObject.SetTextVariable("CLAN", hero.Clan.Name);
				MBInformationManager.AddQuickInformation(textObject, 0, null, "");
			}
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x0003B2DF File Offset: 0x000394DF
		private void OnHideoutSpotted(PartyBase party, PartyBase hideoutParty)
		{
			if (party == PartyBase.MainParty)
			{
				InformationManager.DisplayMessage(new InformationMessage(GameTexts.FindText("str_hideout_spotted", null).ToString(), new Color(255f, 0f, 0f, 1f)));
			}
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x0003B31C File Offset: 0x0003951C
		private void OnPrisonerTaken(PartyBase capturer, Hero prisoner)
		{
			if (prisoner.Clan == Clan.PlayerClan)
			{
				TextObject textObject = GameTexts.FindText("str_on_prisoner_taken", null);
				if (capturer.IsSettlement && capturer.Settlement.IsTown)
				{
					TextObject textObject2 = GameTexts.FindText("str_garrison_party_name", null);
					textObject2.SetTextVariable("MAJOR_PARTY_LEADER", capturer.Settlement.Name);
					textObject.SetTextVariable("CAPTOR_NAME", textObject2);
				}
				else
				{
					textObject.SetTextVariable("CAPTOR_NAME", capturer.Name);
				}
				StringHelpers.SetCharacterProperties("PRISONER", prisoner.CharacterObject, textObject, false);
				MBInformationManager.AddQuickInformation(textObject, 0, null, "");
			}
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x0003B3C0 File Offset: 0x000395C0
		private void OnHeroPrisonerReleased(Hero hero, PartyBase party, IFaction capturerFaction, EndCaptivityDetail detail)
		{
			TextObject textObject = null;
			if (hero.Clan == Clan.PlayerClan)
			{
				if (detail <= EndCaptivityDetail.ReleasedAfterEscape)
				{
					textObject = GameTexts.FindText("str_on_prisoner_released_main_clan", detail.ToString());
				}
				else
				{
					textObject = GameTexts.FindText("str_on_prisoner_released_main_clan_default", null);
				}
			}
			else if (party != null && party.IsSettlement && party.Settlement.IsFortification && party.Settlement.OwnerClan == Clan.PlayerClan)
			{
				if (detail == EndCaptivityDetail.ReleasedAfterEscape)
				{
					textObject = GameTexts.FindText("str_on_prisoner_released_escaped_from_settlement", null);
					textObject.SetTextVariable("SETTLEMENT", party.Settlement.Name);
				}
			}
			else if (party != null && party.IsMobile && party.MobileParty == MobileParty.MainParty && detail == EndCaptivityDetail.ReleasedAfterEscape)
			{
				textObject = GameTexts.FindText("str_on_prisoner_released_escaped_from_party", null);
			}
			if (textObject != null)
			{
				StringHelpers.SetCharacterProperties("PRISONER", hero.CharacterObject, textObject, false);
				MBInformationManager.AddQuickInformation(textObject, 0, null, "");
			}
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x0003B4AC File Offset: 0x000396AC
		private void OnBattleStarted(PartyBase attackerParty, PartyBase defenderParty, object subject, bool showNotification)
		{
			Settlement settlement;
			if (showNotification && (settlement = (subject as Settlement)) != null && settlement.OwnerClan == Clan.PlayerClan && defenderParty.MapEvent != null && defenderParty.MapEvent.DefenderSide.Parties.FindIndexQ((MapEventParty p) => p.Party == settlement.Party) >= 0)
			{
				MBTextManager.SetTextVariable("PARTY", (attackerParty.MobileParty.Army != null) ? attackerParty.MobileParty.ArmyName : attackerParty.Name, false);
				MBTextManager.SetTextVariable("FACTION", attackerParty.MapFaction.Name, false);
				MBTextManager.SetTextVariable("SETTLEMENT", settlement.EncyclopediaLinkWithName, false);
				MBInformationManager.AddQuickInformation(new TextObject("{=ASOW1MuQ}Your settlement {SETTLEMENT} is under attack by {PARTY} of {FACTION}!", null), 0, null, "");
			}
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x0003B58C File Offset: 0x0003978C
		private void OnSiegeEventStarted(SiegeEvent siegeEvent)
		{
			if (siegeEvent.BesiegedSettlement != null && siegeEvent.BesiegedSettlement.OwnerClan == Clan.PlayerClan && siegeEvent.BesiegerCamp.LeaderParty != null)
			{
				MBTextManager.SetTextVariable("PARTY", (siegeEvent.BesiegerCamp.LeaderParty.Army != null) ? siegeEvent.BesiegerCamp.LeaderParty.ArmyName : siegeEvent.BesiegerCamp.LeaderParty.Name, false);
				MBTextManager.SetTextVariable("FACTION", siegeEvent.BesiegerCamp.LeaderParty.MapFaction.Name, false);
				MBTextManager.SetTextVariable("SETTLEMENT", siegeEvent.BesiegedSettlement.EncyclopediaLinkWithName, false);
				MBInformationManager.AddQuickInformation(new TextObject("{=3FvGk8k6}Your settlement {SETTLEMENT} is besieged by {PARTY} of {FACTION}!", null), 0, null, "");
			}
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x0003B658 File Offset: 0x00039858
		private void OnClanTierIncreased(Clan clan, bool shouldNotify = true)
		{
			if (shouldNotify && clan == Clan.PlayerClan)
			{
				MBTextManager.SetTextVariable("CLAN", clan.Name, false);
				MBTextManager.SetTextVariable("TIER_LEVEL", clan.Tier);
				MBInformationManager.AddQuickInformation(new TextObject("{=No04urXt}{CLAN} tier is increased to {TIER_LEVEL}", null), 0, null, "");
			}
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x0003B6A8 File Offset: 0x000398A8
		private void OnItemsLooted(MobileParty mobileParty, ItemRoster items)
		{
			if (mobileParty == MobileParty.MainParty)
			{
				bool flag = true;
				for (int i = 0; i < items.Count; i++)
				{
					ItemRosterElement elementCopyAtIndex = items.GetElementCopyAtIndex(i);
					int elementNumber = items.GetElementNumber(i);
					MBTextManager.SetTextVariable("NUMBER_OF", elementNumber);
					MBTextManager.SetTextVariable("ITEM", elementCopyAtIndex.EquipmentElement.Item.Name, false);
					if (flag)
					{
						MBTextManager.SetTextVariable("LEFT", GameTexts.FindText("str_number_of_item", null).ToString(), false);
						flag = false;
					}
					else
					{
						MBTextManager.SetTextVariable("RIGHT", GameTexts.FindText("str_number_of_item", null).ToString(), false);
						MBTextManager.SetTextVariable("LEFT", GameTexts.FindText("str_LEFT_comma_RIGHT", null).ToString(), false);
					}
				}
				MBTextManager.SetTextVariable("PRODUCTS", GameTexts.FindText("str_LEFT_ONLY", null).ToString(), false);
				InformationManager.DisplayMessage(new InformationMessage(new TextObject("{=GW8ITTMb}You plundered {PRODUCTS}.", null).ToString()));
			}
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x0003B7A4 File Offset: 0x000399A4
		private void OnRelationChanged(Hero effectiveHero, Hero effectiveHeroGainedRelationWith, int relationChange, bool showNotification, ChangeRelationAction.ChangeRelationDetail detail, Hero originalHero, Hero originalGainedRelationWith)
		{
			if (showNotification && relationChange != 0 && (effectiveHero == Hero.MainHero || effectiveHeroGainedRelationWith == Hero.MainHero))
			{
				Hero hero = effectiveHero.IsHumanPlayerCharacter ? effectiveHeroGainedRelationWith : effectiveHero;
				TextObject textObject;
				if (hero.Clan == null || hero.Clan == Clan.PlayerClan)
				{
					textObject = ((relationChange > 0) ? GameTexts.FindText("str_your_relation_increased_with_notable", null) : GameTexts.FindText("str_your_relation_decreased_with_notable", null));
					StringHelpers.SetCharacterProperties("HERO", hero.CharacterObject, textObject, false);
				}
				else
				{
					textObject = ((relationChange > 0) ? GameTexts.FindText("str_your_relation_increased_with_clan", null) : GameTexts.FindText("str_your_relation_decreased_with_clan", null));
					textObject.SetTextVariable("CLAN_LEADER", hero.Clan.Name);
				}
				textObject.SetTextVariable("VALUE", hero.GetRelation(Hero.MainHero));
				textObject.SetTextVariable("MAGNITUDE", MathF.Abs(relationChange));
				MBInformationManager.AddQuickInformation(textObject, 0, hero.IsNotable ? hero.CharacterObject : null, "event:/ui/notification/relation");
			}
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x0003B8A0 File Offset: 0x00039AA0
		private void OnHeroLevelledUp(Hero hero, bool shouldNotify = true)
		{
			if (!shouldNotify)
			{
				return;
			}
			if (hero == Hero.MainHero || hero.Clan == Clan.PlayerClan)
			{
				TextObject textObject = new TextObject("{=3wzCrzEq}{HERO.NAME} gained a level.", null);
				StringHelpers.SetCharacterProperties("HERO", hero.CharacterObject, textObject, false);
				MBInformationManager.AddQuickInformation(textObject, 0, null, "event:/ui/notification/levelup");
			}
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x0003B8F4 File Offset: 0x00039AF4
		private void OnHeroGainedSkill(Hero hero, SkillObject skill, int change = 1, bool shouldNotify = true)
		{
			if (shouldNotify && BannerlordConfig.ReportExperience && (hero == Hero.MainHero || hero.Clan == Clan.PlayerClan || hero.PartyBelongedTo == MobileParty.MainParty || (hero.CompanionOf != null && hero.CompanionOf == Clan.PlayerClan)))
			{
				TextObject textObject = GameTexts.FindText("str_skill_gained_notification", null);
				StringHelpers.SetCharacterProperties("HERO", hero.CharacterObject, textObject, false);
				textObject.SetTextVariable("PLURAL", (change > 1) ? 1 : 0);
				textObject.SetTextVariable("GAINED_POINTS", change);
				textObject.SetTextVariable("SKILL_NAME", skill.Name);
				textObject.SetTextVariable("UPDATED_SKILL_LEVEL", hero.GetSkillValue(skill));
				InformationManager.DisplayMessage(new InformationMessage(textObject.ToString()));
			}
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0003B9C4 File Offset: 0x00039BC4
		private void OnTroopsDeserted(MobileParty mobileParty, TroopRoster desertedTroops)
		{
			if (mobileParty == MobileParty.MainParty || mobileParty.Party.Owner == Hero.MainHero)
			{
				TextObject textObject = GameTexts.FindText("str_troops_deserting", null);
				textObject.SetTextVariable("PARTY", mobileParty.Name);
				textObject.SetTextVariable("DESERTER_COUNT", desertedTroops.TotalManCount);
				textObject.SetTextVariable("PLURAL", (desertedTroops.TotalManCount == 1) ? 0 : 1);
				MBInformationManager.AddQuickInformation(textObject, 0, null, "");
			}
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x0003BA40 File Offset: 0x00039C40
		private void OnClanChangedFaction(Clan clan, Kingdom oldKingdom, Kingdom newKingdom, ChangeKingdomAction.ChangeKingdomActionDetail detail, bool showNotification = true)
		{
			if (clan == Clan.PlayerClan || Hero.MainHero.MapFaction == oldKingdom || Hero.MainHero.MapFaction == newKingdom)
			{
				if (detail == ChangeKingdomAction.ChangeKingdomActionDetail.JoinAsMercenary || detail == ChangeKingdomAction.ChangeKingdomActionDetail.LeaveAsMercenary)
				{
					this.OnMercenaryClanChangedKingdom(clan, oldKingdom, newKingdom);
					return;
				}
				if (showNotification)
				{
					this.OnRegularClanChangedKingdom(clan, oldKingdom, newKingdom);
				}
			}
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x0003BA90 File Offset: 0x00039C90
		private void OnRegularClanChangedKingdom(Clan clan, Kingdom oldKingdom, Kingdom newKingdom)
		{
			TextObject textObject = TextObject.Empty;
			if (oldKingdom != null && newKingdom == null)
			{
				textObject = new TextObject("{=WNKkdpN3}The {CLAN_NAME} left the {OLD_FACTION_NAME}.", null);
			}
			else if (oldKingdom == null && newKingdom != null)
			{
				textObject = new TextObject("{=qeiVFn9s}The {CLAN_NAME} joined the {NEW_FACTION_NAME}", null);
			}
			else if (oldKingdom != null && newKingdom != null && oldKingdom != newKingdom)
			{
				textObject = new TextObject("{=HlrGpPkV}The {CLAN_NAME} changed from the {OLD_FACTION_NAME} to the {NEW_FACTION_NAME}.", null);
			}
			else if (oldKingdom != null && oldKingdom == newKingdom && !clan.IsUnderMercenaryService)
			{
				textObject = new TextObject("{=6f9Hs5zp}The {CLAN_NAME} ended its mercenary contract and became a vassal of the {NEW_FACTION_NAME}", null);
			}
			if (textObject != TextObject.Empty)
			{
				textObject.SetTextVariable("CLAN_NAME", (clan.Lords.Count == 1) ? clan.Lords[0].Name : clan.Name);
				if (oldKingdom != null)
				{
					textObject.SetTextVariable("OLD_FACTION_NAME", oldKingdom.InformalName);
				}
				if (newKingdom != null)
				{
					textObject.SetTextVariable("NEW_FACTION_NAME", newKingdom.InformalName);
				}
				MBInformationManager.AddQuickInformation(textObject, 0, null, "");
			}
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x0003BB70 File Offset: 0x00039D70
		private void OnMercenaryClanChangedKingdom(Clan clan, Kingdom oldKingdom, Kingdom newKingdom)
		{
			if (clan == Clan.PlayerClan || Hero.MainHero.MapFaction == oldKingdom || Hero.MainHero.MapFaction == newKingdom)
			{
				if (oldKingdom != null && (clan == Hero.MainHero.Clan || oldKingdom == Hero.MainHero.MapFaction))
				{
					TextObject textObject = clan.IsUnderMercenaryService ? new TextObject("{=a2AO5T1Q}The {CLAN_NAME} and the {KINGDOM_NAME} have ended their mercenary contract.", null) : new TextObject("{=g7qhnsnJ}The {CLAN_NAME} clan has left the {KINGDOM_NAME}.", null);
					textObject.SetTextVariable("CLAN_NAME", clan.Name);
					textObject.SetTextVariable("KINGDOM_NAME", oldKingdom.InformalName);
					MBInformationManager.AddQuickInformation(textObject, 0, null, "");
				}
				if (newKingdom != null && (clan == Hero.MainHero.Clan || newKingdom == Hero.MainHero.MapFaction) && clan.IsUnderMercenaryService)
				{
					TextObject textObject2 = new TextObject("{=AozaGCru}The {CLAN_NAME} and the {KINGDOM_NAME} have signed a mercenary contract.", null);
					textObject2.SetTextVariable("CLAN_NAME", clan.Name);
					textObject2.SetTextVariable("KINGDOM_NAME", newKingdom.InformalName);
					MBInformationManager.AddQuickInformation(textObject2, 0, null, "");
				}
			}
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0003BC70 File Offset: 0x00039E70
		private void OnArmyCreated(Army army)
		{
			if ((army.Kingdom == MobileParty.MainParty.MapFaction && MobileParty.MainParty.Army == null) || this._notificationCheatEnabled)
			{
				TextObject textObject = new TextObject("{=NMakguW4}{LEADER.LINK} has created an army around {SETTLEMENT}.", null);
				textObject.SetTextVariable("SETTLEMENT", army.AiBehaviorObject.Name);
				StringHelpers.SetCharacterProperties("LEADER", army.LeaderParty.LeaderHero.CharacterObject, textObject, false);
				MBInformationManager.AddQuickInformation(textObject, 0, army.LeaderParty.LeaderHero.CharacterObject, "");
			}
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x0003BD00 File Offset: 0x00039F00
		private void OnArmyLeaderThink(Hero hero, Army.ArmyLeaderThinkReason reason)
		{
			TextObject message = TextObject.Empty;
			if (reason <= Army.ArmyLeaderThinkReason.FromUnassignedToTravelling)
			{
				message = GameTexts.FindText("str_army_leader_think", reason.ToString());
			}
			else
			{
				message = GameTexts.FindText("str_army_leader_think", "Default");
			}
			MBInformationManager.AddQuickInformation(message, 0, hero.CharacterObject, "");
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x0003BD54 File Offset: 0x00039F54
		private void OnSiegeBombardmentHit(MobileParty besiegerParty, Settlement besiegedSettlement, BattleSideEnum side, SiegeEngineType weapon, SiegeBombardTargets target)
		{
			if ((besiegerParty.Army != null && besiegerParty.Army.Parties.Contains(MobileParty.MainParty)) || besiegerParty == MobileParty.MainParty || (MobileParty.MainParty.CurrentSettlement != null && MobileParty.MainParty.CurrentSettlement == besiegedSettlement))
			{
				TextObject textObject = TextObject.Empty;
				if (target != SiegeBombardTargets.RangedEngines)
				{
					if (target != SiegeBombardTargets.People)
					{
						Debug.FailedAssert("invalid bombardment type", "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox\\CampaignBehaviors\\DefaultNotificationsCampaignBehavior.cs", "OnSiegeBombardmentHit", 563);
					}
					else
					{
						textObject = ((side == BattleSideEnum.Defender) ? new TextObject("{=7WlQ0Twr}{WEAPON} of {SETTLEMENT} hit some soldiers of {BESIEGER}!", null) : new TextObject("{=ZrMeSyPu}The {WEAPON} of {BESIEGER} hit some soldiers in {SETTLEMENT}!", null));
					}
				}
				else
				{
					textObject = ((side == BattleSideEnum.Defender) ? new TextObject("{=gqdsXVNi}{WEAPON} of {SETTLEMENT} hit ranged engines of {BESIEGER}!", null) : new TextObject("{=FnkYfyGa}the {WEAPON} of {BESIEGER} hit ranged engines of {SETTLEMENT}!", null));
				}
				textObject.SetTextVariable("WEAPON", weapon.Name);
				textObject.SetTextVariable("BESIEGER", (besiegerParty.Army != null) ? besiegerParty.Army.Name : besiegerParty.Name);
				textObject.SetTextVariable("SETTLEMENT", besiegedSettlement.Name);
				InformationManager.DisplayMessage(new InformationMessage(textObject.ToString()));
			}
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0003BE6C File Offset: 0x0003A06C
		private void OnSiegeBombardmentWallHit(MobileParty besiegerParty, Settlement besiegedSettlement, BattleSideEnum side, SiegeEngineType weapon, bool isWallCracked)
		{
			if ((besiegerParty.Army != null && besiegerParty.Army.Parties.Contains(MobileParty.MainParty)) || besiegerParty == MobileParty.MainParty || (MobileParty.MainParty.CurrentSettlement != null && MobileParty.MainParty.CurrentSettlement == besiegedSettlement))
			{
				TextObject textObject = new TextObject("{=8Wy1OCsr}The {WEAPON} of {BESIEGER} hit wall of {SETTLEMENT}!", null);
				textObject.SetTextVariable("WEAPON", weapon.Name);
				textObject.SetTextVariable("BESIEGER", (besiegerParty.Army != null) ? besiegerParty.Army.Name : besiegerParty.Name);
				textObject.SetTextVariable("SETTLEMENT", besiegedSettlement.Name);
				InformationManager.DisplayMessage(new InformationMessage(textObject.ToString()));
				if (isWallCracked)
				{
					TextObject textObject2 = new TextObject("{=uJNvbag5}The walls of {SETTLEMENT} has been cracked.", null);
					textObject2.SetTextVariable("SETTLEMENT", besiegedSettlement.Name);
					MBInformationManager.AddQuickInformation(textObject2, 0, null, "");
				}
			}
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x0003BF54 File Offset: 0x0003A154
		private void OnSiegeEngineDestroyed(MobileParty besiegerParty, Settlement besiegedSettlement, BattleSideEnum side, SiegeEngineType destroyedEngine)
		{
			if ((besiegerParty.Army != null && besiegerParty.Army.Parties.Contains(MobileParty.MainParty)) || besiegerParty == MobileParty.MainParty || (MobileParty.MainParty.CurrentSettlement != null && MobileParty.MainParty.CurrentSettlement == besiegedSettlement))
			{
				TextObject textObject = (side == BattleSideEnum.Attacker) ? new TextObject("{=fa8sla4i}The {SIEGE_ENGINE} of {BESIEGER_PARTY} has been destroyed.", null) : new TextObject("{=U9zFz8Et}The {SIEGE_ENGINE} of {SIEGED_SETTLEMENT_NAME} has been cracked.", null);
				textObject.SetTextVariable("SIEGED_SETTLEMENT_NAME", besiegedSettlement.Name);
				textObject.SetTextVariable("BESIEGER_PARTY", besiegerParty.Name);
				textObject.SetTextVariable("SIEGE_ENGINE", destroyedEngine.Name);
				MBInformationManager.AddQuickInformation(textObject, 0, null, "");
			}
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x0003C000 File Offset: 0x0003A200
		private void OnHeroOrPartyTradedGold(ValueTuple<Hero, PartyBase> giverSide, ValueTuple<Hero, PartyBase> recipientSide, ValueTuple<int, string> transactionAmountAndId, bool displayNotification)
		{
			if (displayNotification)
			{
				int item = transactionAmountAndId.Item1;
				MBTextManager.SetTextVariable("GOLD_AMOUNT", MathF.Abs(item));
				bool flag = giverSide.Item1 == Hero.MainHero || giverSide.Item2 == PartyBase.MainParty;
				bool flag2 = recipientSide.Item1 == Hero.MainHero || recipientSide.Item2 == PartyBase.MainParty;
				if ((flag && item > 0) || (flag2 && item < 0))
				{
					InformationManager.DisplayMessage(new InformationMessage(GameTexts.FindText("str_gold_removed_with_icon", null).ToString(), "event:/ui/notification/coins_negative"));
					return;
				}
				if ((flag && item < 0) || (flag2 && item > 0))
				{
					InformationManager.DisplayMessage(new InformationMessage(GameTexts.FindText("str_you_received_gold_with_icon", null).ToString(), "event:/ui/notification/coins_positive"));
				}
			}
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x0003C0C4 File Offset: 0x0003A2C4
		private void OnPartyJoinedArmy(MobileParty party)
		{
			if (party.Army == MobileParty.MainParty.Army && party.LeaderHero != party.Army.LeaderParty.LeaderHero)
			{
				TextObject textObject = new TextObject("{=wD1YDmmg}{PARTY_NAME} has enlisted in {ARMY_NAME}.", null);
				textObject.SetTextVariable("PARTY_NAME", party.Name);
				textObject.SetTextVariable("ARMY_NAME", party.Army.Name);
				InformationManager.DisplayMessage(new InformationMessage(textObject.ToString()));
			}
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x0003C140 File Offset: 0x0003A340
		private void OnPartyAttachedAnotherParty(MobileParty party)
		{
			if (party.Army == MobileParty.MainParty.Army && party.LeaderHero != party.Army.LeaderParty.LeaderHero)
			{
				TextObject textObject = new TextObject("{=0aGYre5B}{LEADER.LINK} has arrived at {ARMY_NAME}.", null);
				StringHelpers.SetCharacterProperties("LEADER", party.LeaderHero.CharacterObject, textObject, false);
				textObject.SetTextVariable("ARMY_NAME", party.Army.Name);
				InformationManager.DisplayMessage(new InformationMessage(textObject.ToString()));
			}
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x0003C1C4 File Offset: 0x0003A3C4
		private void OnPartyRemovedFromArmy(MobileParty party)
		{
			if (party.Army == MobileParty.MainParty.Army)
			{
				TextObject textObject = new TextObject("{=ApG1xg7O}{PARTY_NAME} has left {ARMY_NAME}.", null);
				textObject.SetTextVariable("PARTY_NAME", party.Name);
				textObject.SetTextVariable("ARMY_NAME", party.Army.Name);
				InformationManager.DisplayMessage(new InformationMessage(textObject.ToString()));
			}
			if (party == MobileParty.MainParty)
			{
				this.CheckFoodNotifications();
			}
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x0003C234 File Offset: 0x0003A434
		private void OnArmyDispersed(Army army, Army.ArmyDispersionReason reason, bool isPlayersArmy)
		{
			if (isPlayersArmy)
			{
				this.CheckFoodNotifications();
			}
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x0003C240 File Offset: 0x0003A440
		private void OnHeroesMarried(Hero firstHero, Hero secondHero, bool showNotification)
		{
			if (showNotification && (firstHero.Clan == Clan.PlayerClan || secondHero.Clan == Clan.PlayerClan))
			{
				StringHelpers.SetCharacterProperties("MARRIED_TO", firstHero.CharacterObject, null, false);
				StringHelpers.SetCharacterProperties("MARRIED_HERO", secondHero.CharacterObject, null, false);
				MBInformationManager.AddQuickInformation(GameTexts.FindText("str_hero_married_hero", null), 0, null, "");
			}
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x0003C2A8 File Offset: 0x0003A4A8
		private void OnSettlementOwnerChanged(Settlement settlement, bool openToClaim, Hero newOwner, Hero previousOwner, Hero capturerHero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail detail)
		{
			if (detail == ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail.BySiege && settlement.MapFaction == Hero.MainHero.MapFaction && settlement.IsFortification)
			{
				TextObject textObject = Hero.MainHero.MapFaction.IsKingdomFaction ? new TextObject("{=OiCCfAeC}{SETTLEMENT} is taken. Election is started.", null) : new TextObject("{=2VRTPyZY}{SETTLEMENT} is yours.", null);
				textObject.SetTextVariable("SETTLEMENT", settlement.Name);
				MBInformationManager.AddQuickInformation(textObject, 0, null, "");
			}
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x0003C31C File Offset: 0x0003A51C
		private void OnChildConceived(Hero mother)
		{
			if (mother == Hero.MainHero)
			{
				MBInformationManager.AddQuickInformation(new TextObject("{=ZhpT2qVh}You have just learned that you are with child.", null), 0, null, "");
				return;
			}
			if (mother == Hero.MainHero.Spouse)
			{
				TextObject textObject = new TextObject("{=7v2dMsW5}Your spouse {MOTHER} has just learned that she is with child.", null);
				textObject.SetTextVariable("MOTHER", mother.Name);
				MBInformationManager.AddQuickInformation(textObject, 0, null, "");
				return;
			}
			if (mother.Clan == Clan.PlayerClan)
			{
				TextObject textObject2 = new TextObject("{=2AGIxoUN}Your clan member {MOTHER} has just learned that she is with child.", null);
				textObject2.SetTextVariable("MOTHER", mother.Name);
				MBInformationManager.AddQuickInformation(textObject2, 0, null, "");
			}
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x0003C3B8 File Offset: 0x0003A5B8
		private void OnGivenBirth(Hero mother, List<Hero> aliveOffsprings, int stillbornCount)
		{
			if (mother == Hero.MainHero || mother == Hero.MainHero.Spouse || mother.Clan == Clan.PlayerClan)
			{
				TextObject textObject;
				if (mother == Hero.MainHero)
				{
					textObject = new TextObject("{=oIA9lkpc}You have given birth to {DELIVERED_CHILDREN}.", null);
				}
				else if (mother == Hero.MainHero.Spouse)
				{
					textObject = new TextObject("{=TsbjAsxs}Your wife {MOTHER.NAME} has given birth to {DELIVERED_CHILDREN}.", null);
				}
				else
				{
					textObject = new TextObject("{=LsDRCPp0}Your clan member {MOTHER.NAME} has given birth to {DELIVERED_CHILDREN}.", null);
				}
				if (stillbornCount == 2)
				{
					textObject.SetTextVariable("DELIVERED_CHILDREN", new TextObject("{=Sn9a1Aba}two stillborn babies", null));
				}
				else if (stillbornCount == 1 && aliveOffsprings.Count == 0)
				{
					textObject.SetTextVariable("DELIVERED_CHILDREN", new TextObject("{=qWLq2y84}a stillborn baby", null));
				}
				else if (stillbornCount == 1 && aliveOffsprings.Count == 1)
				{
					textObject.SetTextVariable("DELIVERED_CHILDREN", new TextObject("{=vn13OyFV}one healthy and one stillborn baby", null));
				}
				else if (stillbornCount == 0 && aliveOffsprings.Count == 1)
				{
					textObject.SetTextVariable("DELIVERED_CHILDREN", new TextObject("{=lbRMmZym}a healthy baby", null));
				}
				else if (stillbornCount == 0 && aliveOffsprings.Count == 2)
				{
					textObject.SetTextVariable("DELIVERED_CHILDREN", new TextObject("{=EPbHr2DX}two healthy babies", null));
				}
				StringHelpers.SetCharacterProperties("MOTHER", mother.CharacterObject, textObject, false);
				MBInformationManager.AddQuickInformation(textObject, 0, null, "");
			}
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x0003C4FC File Offset: 0x0003A6FC
		private void OnHeroKilled(Hero victimHero, Hero killer, KillCharacterAction.KillCharacterActionDetail detail, bool showNotification)
		{
			if (showNotification && victimHero != null && victimHero.Clan == Clan.PlayerClan)
			{
				TextObject textObject;
				if (detail == KillCharacterAction.KillCharacterActionDetail.DiedInLabor || detail == KillCharacterAction.KillCharacterActionDetail.Murdered || detail == KillCharacterAction.KillCharacterActionDetail.DiedInBattle || detail == KillCharacterAction.KillCharacterActionDetail.DiedOfOldAge)
				{
					textObject = GameTexts.FindText("str_on_hero_killed", detail.ToString());
				}
				else if (detail == KillCharacterAction.KillCharacterActionDetail.Executed && killer != null)
				{
					textObject = GameTexts.FindText("str_on_hero_killed", detail.ToString());
					StringHelpers.SetCharacterProperties("KILLER", killer.CharacterObject, textObject, false);
				}
				else if (detail == KillCharacterAction.KillCharacterActionDetail.Lost)
				{
					textObject = GameTexts.FindText("str_on_hero_killed", detail.ToString());
					StringHelpers.SetCharacterProperties("VICTIM", victimHero.CharacterObject, textObject, false);
				}
				else
				{
					textObject = GameTexts.FindText("str_on_hero_killed", "Default");
				}
				StringHelpers.SetCharacterProperties("HERO", victimHero.CharacterObject, textObject, false);
				MBInformationManager.AddQuickInformation(textObject, 0, null, "");
			}
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x0003C5E5 File Offset: 0x0003A7E5
		private void OnHeroSharedFoodWithAnotherHero(Hero supporterHero, Hero supportedHero, float influence)
		{
			if (supporterHero == Hero.MainHero)
			{
				this._foodNotificationList.Add(new Tuple<bool, float>(true, influence));
				return;
			}
			if (supportedHero == Hero.MainHero)
			{
				this._foodNotificationList.Add(new Tuple<bool, float>(false, influence));
			}
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x0003C61C File Offset: 0x0003A81C
		private void CheckFoodNotifications()
		{
			float num = 0f;
			float num2 = 0f;
			bool flag = false;
			bool flag2 = false;
			foreach (Tuple<bool, float> tuple in this._foodNotificationList)
			{
				if (tuple.Item1)
				{
					num += tuple.Item2;
					flag = true;
				}
				else
				{
					num2 += tuple.Item2;
					flag2 = true;
				}
			}
			if (flag)
			{
				TextObject textObject = new TextObject("{=B0eBWPoO} You shared your food with starving soldiers of your army. You gained {INFLUENCE}{INFLUENCE_ICON}.", null);
				textObject.SetTextVariable("INFLUENCE", num.ToString("0.00"));
				textObject.SetTextVariable("INFLUENCE_ICON", "{=!}<img src=\"General\\Icons\\Influence@2x\" extend=\"7\">");
				InformationManager.DisplayMessage(new InformationMessage(textObject.ToString()));
			}
			if (flag2)
			{
				TextObject textObject2 = new TextObject("{=qQ71Ux7D} Your army shared their food with your starving soldiers. You spent {INFLUENCE}{INFLUENCE_ICON}.", null);
				textObject2.SetTextVariable("INFLUENCE", num2.ToString("0.00"));
				textObject2.SetTextVariable("INFLUENCE_ICON", "{=!}<img src=\"General\\Icons\\Influence@2x\" extend=\"7\">");
				InformationManager.DisplayMessage(new InformationMessage(textObject2.ToString()));
			}
			this._foodNotificationList.Clear();
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x0003C734 File Offset: 0x0003A934
		private void OnClanDestroyed(Clan destroyedClan)
		{
			TextObject textObject = new TextObject("{=PBq1FyrJ}{CLAN_NAME} clan was destroyed.", null);
			textObject.SetTextVariable("CLAN_NAME", destroyedClan.Name);
			MBInformationManager.AddQuickInformation(textObject, 0, null, "");
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0003C760 File Offset: 0x0003A960
		private void OnHeroOrPartyGaveItem(ValueTuple<Hero, PartyBase> giver, ValueTuple<Hero, PartyBase> receiver, ItemRosterElement itemRosterElement, bool showNotification)
		{
			if (showNotification && itemRosterElement.Amount > 0)
			{
				TextObject textObject = null;
				if (giver.Item1 == Hero.MainHero || giver.Item2 == PartyBase.MainParty)
				{
					if (receiver.Item1 != null)
					{
						textObject = GameTexts.FindText("str_hero_gave_item_to_hero", null);
						StringHelpers.SetCharacterProperties("HERO", receiver.Item1.CharacterObject, textObject, false);
					}
					else
					{
						textObject = GameTexts.FindText("str_hero_gave_item_to_party", null);
						textObject.SetTextVariable("PARTY_NAME", receiver.Item2.Name);
					}
				}
				else if (receiver.Item1 == Hero.MainHero || receiver.Item2 == PartyBase.MainParty)
				{
					if (giver.Item1 != null)
					{
						textObject = GameTexts.FindText("str_hero_received_item_from_hero", null);
						StringHelpers.SetCharacterProperties("HERO", giver.Item1.CharacterObject, textObject, false);
					}
					else
					{
						textObject = GameTexts.FindText("str_hero_received_item_from_party", null);
						textObject.SetTextVariable("PARTY_NAME", giver.Item2.Name);
					}
				}
				if (textObject != null)
				{
					textObject.SetTextVariable("ITEM", itemRosterElement.EquipmentElement.Item.Name);
					textObject.SetTextVariable("COUNT", itemRosterElement.Amount);
					InformationManager.DisplayMessage(new InformationMessage(textObject.ToString()));
				}
			}
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x0003C8A4 File Offset: 0x0003AAA4
		private void OnRebellionFinished(Settlement settlement, Clan oldOwnerClan)
		{
			TextObject textObject = GameTexts.FindText("str_rebellion_finished", null);
			textObject.SetTextVariable("SETTLEMENT", settlement.Name);
			textObject.SetTextVariable("RULER", oldOwnerClan.Leader.Name);
			MBInformationManager.AddQuickInformation(textObject, 0, null, "");
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x0003C8F4 File Offset: 0x0003AAF4
		private void OnTournamentFinished(CharacterObject winner, MBReadOnlyList<CharacterObject> participants, Town town, ItemObject prize)
		{
			if (winner.IsHero && winner.HeroObject.Clan == Clan.PlayerClan && winner.HeroObject.PartyBelongedTo == MobileParty.MainParty)
			{
				TextObject textObject = GameTexts.FindText("str_tournament_companion_won_prize", null);
				textObject.SetTextVariable("ITEM_NAME", prize.Name);
				textObject.SetCharacterProperties("COMPANION", winner, false);
				MBInformationManager.AddQuickInformation(textObject, 0, null, "");
			}
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x0003C964 File Offset: 0x0003AB64
		private void OnBuildingLevelChanged(Town town, Building building, int levelChange)
		{
			if (levelChange > 0 && town.OwnerClan == Clan.PlayerClan)
			{
				TextObject textObject = (building.CurrentLevel == 1) ? GameTexts.FindText("str_building_completed", null) : GameTexts.FindText("str_building_level_gained", null);
				textObject.SetTextVariable("SETTLEMENT_NAME", town.Name);
				textObject.SetTextVariable("BUILDING_NAME", building.Name);
				InformationManager.DisplayMessage(new InformationMessage(textObject.ToString(), new Color(0f, 1f, 0f, 1f)));
			}
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x0003C9F0 File Offset: 0x0003ABF0
		private void OnHeroTeleportationRequested(Hero hero, Settlement targetSettlement, MobileParty targetParty, TeleportHeroAction.TeleportationDetail detail)
		{
			if (detail == TeleportHeroAction.TeleportationDetail.ImmediateTeleportToParty && targetParty == MobileParty.MainParty && MobileParty.MainParty.IsActive)
			{
				TextObject textObject = new TextObject("{=abux36nq}{HERO.NAME} joined your party.", null);
				textObject.SetCharacterProperties("HERO", hero.CharacterObject, false);
				MBInformationManager.AddQuickInformation(textObject, 0, null, "");
			}
		}

		// Token: 0x0400030B RID: 779
		private List<Tuple<bool, float>> _foodNotificationList = new List<Tuple<bool, float>>();

		// Token: 0x0400030C RID: 780
		private bool _notificationCheatEnabled;
	}
}
