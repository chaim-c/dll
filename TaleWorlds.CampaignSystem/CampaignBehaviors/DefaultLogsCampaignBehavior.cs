using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Election;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.CampaignSystem.LogEntries;
using TaleWorlds.CampaignSystem.MapNotificationTypes;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Siege;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x02000385 RID: 901
	public class DefaultLogsCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x06003572 RID: 13682 RVA: 0x000E79C4 File Offset: 0x000E5BC4
		public override void RegisterEvents()
		{
			CampaignEvents.AlleyOwnerChanged.AddNonSerializedListener(this, new Action<Alley, Hero, Hero>(this.OnAlleyOwnerChanged));
			CampaignEvents.ArmyGathered.AddNonSerializedListener(this, new Action<Army, Settlement>(this.OnArmyGathered));
			CampaignEvents.BattleStarted.AddNonSerializedListener(this, new Action<PartyBase, PartyBase, object, bool>(this.OnBattleStarted));
			CampaignEvents.CharacterBecameFugitive.AddNonSerializedListener(this, new Action<Hero>(this.OnCharacterBecameFugitive));
			CampaignEvents.OnClanChangedKingdomEvent.AddNonSerializedListener(this, new Action<Clan, Kingdom, Kingdom, ChangeKingdomAction.ChangeKingdomActionDetail, bool>(this.ClanChangedKingdom));
			CampaignEvents.HeroPrisonerTaken.AddNonSerializedListener(this, new Action<PartyBase, Hero>(this.OnPrisonerTaken));
			CampaignEvents.HeroPrisonerReleased.AddNonSerializedListener(this, new Action<Hero, PartyBase, IFaction, EndCaptivityDetail>(this.OnHeroPrisonerReleased));
			CampaignEvents.HeroesMarried.AddNonSerializedListener(this, new Action<Hero, Hero, bool>(this.OnHeroesMarried));
			CampaignEvents.ArmyDispersed.AddNonSerializedListener(this, new Action<Army, Army.ArmyDispersionReason, bool>(this.OnArmyDispersed));
			CampaignEvents.ArmyCreated.AddNonSerializedListener(this, new Action<Army>(this.OnArmyCreated));
			CampaignEvents.RebellionFinished.AddNonSerializedListener(this, new Action<Settlement, Clan>(this.OnRebellionFinished));
			CampaignEvents.KingdomDecisionAdded.AddNonSerializedListener(this, new Action<KingdomDecision, bool>(this.OnKingdomDecisionAdded));
			CampaignEvents.KingdomDecisionConcluded.AddNonSerializedListener(this, new Action<KingdomDecision, DecisionOutcome, bool>(this.OnKingdomDecisionConcluded));
			CampaignEvents.TournamentFinished.AddNonSerializedListener(this, new Action<CharacterObject, MBReadOnlyList<CharacterObject>, Town, ItemObject>(this.OnTournamentFinished));
			CampaignEvents.OnSiegeEventStartedEvent.AddNonSerializedListener(this, new Action<SiegeEvent>(this.OnSiegeEventStarted));
			CampaignEvents.PlayerTraitChangedEvent.AddNonSerializedListener(this, new Action<TraitObject, int>(this.OnPlayerTraitChanged));
			CampaignEvents.OnPlayerCharacterChangedEvent.AddNonSerializedListener(this, new Action<Hero, Hero, MobileParty, bool>(this.OnPlayerCharacterChanged));
			CampaignEvents.OnSiegeAftermathAppliedEvent.AddNonSerializedListener(this, new Action<MobileParty, Settlement, SiegeAftermathAction.SiegeAftermath, Clan, Dictionary<MobileParty, float>>(this.OnSiegeAftermathApplied));
		}

		// Token: 0x06003573 RID: 13683 RVA: 0x000E7B6F File Offset: 0x000E5D6F
		private void OnSiegeAftermathApplied(MobileParty attackerParty, Settlement settlement, SiegeAftermathAction.SiegeAftermath aftermathType, Clan previousSettlementOwner, Dictionary<MobileParty, float> partyContributions)
		{
			LogEntry.AddLogEntry(new SiegeAftermathLogEntry(attackerParty, partyContributions.Keys, settlement, aftermathType));
		}

		// Token: 0x06003574 RID: 13684 RVA: 0x000E7B85 File Offset: 0x000E5D85
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x06003575 RID: 13685 RVA: 0x000E7B87 File Offset: 0x000E5D87
		private void OnPlayerCharacterChanged(Hero oldPlayer, Hero newPlayer, MobileParty newMobileParty, bool isMainPartyChanged)
		{
			LogEntry.AddLogEntry(new PlayerCharacterChangedLogEntry(oldPlayer, newPlayer));
		}

		// Token: 0x06003576 RID: 13686 RVA: 0x000E7B95 File Offset: 0x000E5D95
		private void OnPrisonerTaken(PartyBase party, Hero hero)
		{
			LogEntry.AddLogEntry(new TakePrisonerLogEntry(party, hero));
		}

		// Token: 0x06003577 RID: 13687 RVA: 0x000E7BA3 File Offset: 0x000E5DA3
		private void OnHeroPrisonerReleased(Hero hero, PartyBase party, IFaction captuererFaction, EndCaptivityDetail detail)
		{
			LogEntry.AddLogEntry(new EndCaptivityLogEntry(hero, captuererFaction, detail));
		}

		// Token: 0x06003578 RID: 13688 RVA: 0x000E7BB3 File Offset: 0x000E5DB3
		private void OnCommonAreaFightOccured(MobileParty attackerParty, MobileParty defenderParty, Hero attackerHero, Settlement settlement)
		{
			LogEntry.AddLogEntry(new CommonAreaFightLogEntry(attackerParty, defenderParty, attackerHero, settlement));
		}

		// Token: 0x06003579 RID: 13689 RVA: 0x000E7BC4 File Offset: 0x000E5DC4
		private void ClanChangedKingdom(Clan clan, Kingdom oldKingdom, Kingdom newKingdom, ChangeKingdomAction.ChangeKingdomActionDetail detail, bool showNotifications)
		{
			if (detail == ChangeKingdomAction.ChangeKingdomActionDetail.JoinAsMercenary || detail == ChangeKingdomAction.ChangeKingdomActionDetail.LeaveAsMercenary)
			{
				LogEntry.AddLogEntry(new MercenaryClanChangedKingdomLogEntry(clan, oldKingdom, newKingdom));
			}
		}

		// Token: 0x0600357A RID: 13690 RVA: 0x000E7BDC File Offset: 0x000E5DDC
		private void OnCharacterBecameFugitive(Hero hero)
		{
			LogEntry.AddLogEntry(new CharacterBecameFugitiveLogEntry(hero));
		}

		// Token: 0x0600357B RID: 13691 RVA: 0x000E7BE9 File Offset: 0x000E5DE9
		private void OnBattleStarted(PartyBase attackerParty, PartyBase defenderParty, object subject, bool showNotification)
		{
			if (showNotification)
			{
				LogEntry.AddLogEntry(new BattleStartedLogEntry(attackerParty, defenderParty, subject));
			}
		}

		// Token: 0x0600357C RID: 13692 RVA: 0x000E7BFC File Offset: 0x000E5DFC
		public void OnArmyDispersed(Army army, Army.ArmyDispersionReason reason, bool isPlayersArmy)
		{
			if (isPlayersArmy)
			{
				ArmyDispersionLogEntry armyDispersionLogEntry = new ArmyDispersionLogEntry(army, reason);
				LogEntry.AddLogEntry(armyDispersionLogEntry);
				if (army.LeaderParty.MapFaction == Hero.MainHero.MapFaction && army.Parties.IndexOf(MobileParty.MainParty) < 0)
				{
					Campaign.Current.CampaignInformationManager.NewMapNoticeAdded(new ArmyDispersionMapNotification(army, reason, armyDispersionLogEntry.GetEncyclopediaText()));
				}
			}
		}

		// Token: 0x0600357D RID: 13693 RVA: 0x000E7C60 File Offset: 0x000E5E60
		private void OnArmyGathered(Army army, Settlement targetSettlement)
		{
			LogEntry.AddLogEntry(new GatherArmyLogEntry(army, targetSettlement));
		}

		// Token: 0x0600357E RID: 13694 RVA: 0x000E7C70 File Offset: 0x000E5E70
		private void OnArmyCreated(Army army)
		{
			ArmyCreationLogEntry armyCreationLogEntry = new ArmyCreationLogEntry(army);
			LogEntry.AddLogEntry(armyCreationLogEntry);
			if (army.LeaderParty.MapFaction == MobileParty.MainParty.MapFaction && army.LeaderParty != MobileParty.MainParty)
			{
				Campaign.Current.CampaignInformationManager.NewMapNoticeAdded(new ArmyCreationMapNotification(army, armyCreationLogEntry.GetEncyclopediaText()));
			}
		}

		// Token: 0x0600357F RID: 13695 RVA: 0x000E7CCC File Offset: 0x000E5ECC
		private void OnRebellionFinished(Settlement settlement, Clan oldOwnerClan)
		{
			RebellionStartedLogEntry rebellionStartedLogEntry = new RebellionStartedLogEntry(settlement, oldOwnerClan);
			LogEntry.AddLogEntry(rebellionStartedLogEntry);
			if (oldOwnerClan == Clan.PlayerClan)
			{
				Campaign.Current.CampaignInformationManager.NewMapNoticeAdded(new SettlementRebellionMapNotification(settlement, rebellionStartedLogEntry.GetNotificationText()));
			}
		}

		// Token: 0x06003580 RID: 13696 RVA: 0x000E7D0C File Offset: 0x000E5F0C
		private void OnKingdomDecisionAdded(KingdomDecision decision, bool isPlayerInvolved)
		{
			LogEntry.AddLogEntry(new KingdomDecisionAddedLogEntry(decision, isPlayerInvolved));
			if (decision.NotifyPlayer && isPlayerInvolved && !decision.IsEnforced)
			{
				TextObject descriptionText = decision.DetermineChooser().Leader.IsHumanPlayerCharacter ? decision.GetChooseTitle() : decision.GetSupportTitle();
				Campaign.Current.CampaignInformationManager.NewMapNoticeAdded(new KingdomDecisionMapNotification(decision.Kingdom, decision, descriptionText));
			}
		}

		// Token: 0x06003581 RID: 13697 RVA: 0x000E7D74 File Offset: 0x000E5F74
		private void OnKingdomDecisionConcluded(KingdomDecision decision, DecisionOutcome chosenOutcome, bool isPlayerInvolved)
		{
			KingdomDecisionConcludedLogEntry kingdomDecisionConcludedLogEntry = new KingdomDecisionConcludedLogEntry(decision, chosenOutcome, isPlayerInvolved);
			LogEntry.AddLogEntry(kingdomDecisionConcludedLogEntry);
			if (decision.Kingdom == Hero.MainHero.MapFaction && decision.NotifyPlayer && !decision.IsEnforced && !isPlayerInvolved)
			{
				MBInformationManager.AddQuickInformation(kingdomDecisionConcludedLogEntry.GetNotificationText(), 0, null, "event:/ui/notification/kingdom_decision");
				Campaign.Current.CampaignInformationManager.NewMapNoticeAdded(new KingdomDecisionMapNotification(decision.Kingdom, decision, kingdomDecisionConcludedLogEntry.GetNotificationText()));
			}
		}

		// Token: 0x06003582 RID: 13698 RVA: 0x000E7DE8 File Offset: 0x000E5FE8
		private void OnAlleyOwnerChanged(Alley alley, Hero newOwner, Hero oldOwner)
		{
			LogEntry.AddLogEntry(new ChangeAlleyOwnerLogEntry(alley, newOwner, oldOwner));
		}

		// Token: 0x06003583 RID: 13699 RVA: 0x000E7DF8 File Offset: 0x000E5FF8
		private void OnHeroesMarried(Hero marriedHero, Hero marriedTo, bool showNotification)
		{
			CharacterMarriedLogEntry characterMarriedLogEntry = new CharacterMarriedLogEntry(marriedHero, marriedTo);
			LogEntry.AddLogEntry(characterMarriedLogEntry);
			if (marriedHero.Clan == Clan.PlayerClan || marriedTo.Clan == Clan.PlayerClan)
			{
				Campaign.Current.CampaignInformationManager.NewMapNoticeAdded(new MarriageMapNotification(marriedHero, marriedTo, characterMarriedLogEntry.GetEncyclopediaText(), CampaignTime.Now));
			}
		}

		// Token: 0x06003584 RID: 13700 RVA: 0x000E7E50 File Offset: 0x000E6050
		private void OnSiegeEventStarted(SiegeEvent siegeEvent)
		{
			BesiegeSettlementLogEntry besiegeSettlementLogEntry = new BesiegeSettlementLogEntry(siegeEvent.BesiegerCamp.LeaderParty, siegeEvent.BesiegedSettlement);
			LogEntry.AddLogEntry(besiegeSettlementLogEntry);
			if (siegeEvent.BesiegedSettlement.OwnerClan == Clan.PlayerClan)
			{
				Campaign.Current.CampaignInformationManager.NewMapNoticeAdded(new SettlementUnderSiegeMapNotification(siegeEvent, besiegeSettlementLogEntry.GetEncyclopediaText()));
			}
		}

		// Token: 0x06003585 RID: 13701 RVA: 0x000E7EA7 File Offset: 0x000E60A7
		private void OnTournamentFinished(CharacterObject character, MBReadOnlyList<CharacterObject> participants, Town town, ItemObject prize)
		{
			if (character.IsHero)
			{
				LogEntry.AddLogEntry(new TournamentWonLogEntry(character.HeroObject, town, participants));
			}
		}

		// Token: 0x06003586 RID: 13702 RVA: 0x000E7EC4 File Offset: 0x000E60C4
		private void OnPlayerTraitChanged(TraitObject trait, int previousLevel)
		{
			int traitLevel = Hero.MainHero.GetTraitLevel(trait);
			TextObject traitChangedText = DefaultLogsCampaignBehavior.GetTraitChangedText(trait, traitLevel, previousLevel);
			Campaign.Current.CampaignInformationManager.NewMapNoticeAdded(new TraitChangedMapNotification(trait, traitLevel != 0, previousLevel, traitChangedText));
		}

		// Token: 0x06003587 RID: 13703 RVA: 0x000E7F04 File Offset: 0x000E6104
		private static TextObject GetTraitChangedText(TraitObject traitObject, int level, int previousLevel)
		{
			TextObject variable;
			TextObject textObject;
			if (level != 0)
			{
				variable = GameTexts.FindText("str_trait_name_" + traitObject.StringId.ToLower(), (level + MathF.Abs(traitObject.MinValue)).ToString());
				textObject = GameTexts.FindText("str_trait_gained_text", null);
			}
			else
			{
				variable = GameTexts.FindText("str_trait_name_" + traitObject.StringId.ToLower(), (previousLevel + MathF.Abs(traitObject.MinValue)).ToString());
				textObject = GameTexts.FindText("str_trait_lost_text", null);
			}
			textObject.SetCharacterProperties("HERO", Hero.MainHero.CharacterObject, false);
			textObject.SetTextVariable("TRAIT_NAME", variable);
			return textObject;
		}
	}
}
