using System;
using System.Collections.Generic;
using Helpers;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Party.PartyComponents;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x02000394 RID: 916
	public class HeroKnownInformationCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x06003721 RID: 14113 RVA: 0x000F8920 File Offset: 0x000F6B20
		public override void RegisterEvents()
		{
			CampaignEvents.DailyTickHeroEvent.AddNonSerializedListener(this, new Action<Hero>(this.OnDailyTickHero));
			CampaignEvents.ConversationEnded.AddNonSerializedListener(this, new Action<IEnumerable<CharacterObject>>(this.ConversationEnded));
			CampaignEvents.OnAgentJoinedConversationEvent.AddNonSerializedListener(this, new Action<IAgent>(this.OnAgentJoinedConversation));
			CampaignEvents.OnPlayerMetHeroEvent.AddNonSerializedListener(this, new Action<Hero>(this.OnPlayerMetHero));
			CampaignEvents.HeroesMarried.AddNonSerializedListener(this, new Action<Hero, Hero, bool>(this.OnHeroesMarried));
			CampaignEvents.HeroCreated.AddNonSerializedListener(this, new Action<Hero, bool>(this.OnHeroCreated));
			CampaignEvents.OnGameLoadFinishedEvent.AddNonSerializedListener(this, new Action(this.OnGameLoadFinishedEvent));
			CampaignEvents.OnCharacterCreationIsOverEvent.AddNonSerializedListener(this, new Action(this.OnCharacterCreationIsOver));
			CampaignEvents.OnPlayerLearnsAboutHeroEvent.AddNonSerializedListener(this, new Action<Hero>(this.OnPlayerLearnsAboutHero));
			CampaignEvents.NearbyPartyAddedToPlayerMapEvent.AddNonSerializedListener(this, new Action<MobileParty>(this.OnNearbyPartyAddedToPlayerMapEvent));
			CampaignEvents.GameMenuOpened.AddNonSerializedListener(this, new Action<MenuCallbackArgs>(this.OnGameMenuChanged));
			CampaignEvents.AfterMissionStarted.AddNonSerializedListener(this, new Action<IMission>(this.OnAfterMissionStarted));
			CampaignEvents.MapEventStarted.AddNonSerializedListener(this, new Action<MapEvent, PartyBase, PartyBase>(this.OnMapEventStarted));
			CampaignEvents.PartyAttachedAnotherParty.AddNonSerializedListener(this, new Action<MobileParty>(this.OnPartyAttachedAnotherParty));
			CampaignEvents.OnPlayerJoinedTournamentEvent.AddNonSerializedListener(this, new Action<Town, bool>(this.OnPlayerJoinedTournament));
			CampaignEvents.OnMarriageOfferedToPlayerEvent.AddNonSerializedListener(this, new Action<Hero, Hero>(this.OnMarriageOfferedToPlayer));
		}

		// Token: 0x06003722 RID: 14114 RVA: 0x000F8A9D File Offset: 0x000F6C9D
		private void OnMarriageOfferedToPlayer(Hero suitor, Hero maiden)
		{
			if (suitor.Clan == Clan.PlayerClan)
			{
				maiden.IsKnownToPlayer = true;
				return;
			}
			suitor.IsKnownToPlayer = true;
		}

		// Token: 0x06003723 RID: 14115 RVA: 0x000F8ABC File Offset: 0x000F6CBC
		private void OnPlayerJoinedTournament(Town town, bool isParticipant)
		{
			foreach (CharacterObject characterObject in Campaign.Current.TournamentManager.GetTournamentGame(town).GetParticipantCharacters(town.Settlement, false))
			{
				if (characterObject.IsHero && !characterObject.HeroObject.IsKnownToPlayer)
				{
					characterObject.HeroObject.IsKnownToPlayer = true;
				}
			}
		}

		// Token: 0x06003724 RID: 14116 RVA: 0x000F8B40 File Offset: 0x000F6D40
		private void OnNearbyPartyAddedToPlayerMapEvent(MobileParty mobileParty)
		{
			if (mobileParty.LeaderHero != null)
			{
				mobileParty.LeaderHero.IsKnownToPlayer = true;
			}
		}

		// Token: 0x06003725 RID: 14117 RVA: 0x000F8B58 File Offset: 0x000F6D58
		private void OnPartyAttachedAnotherParty(MobileParty party)
		{
			if (party == MobileParty.MainParty)
			{
				if (party.AttachedTo.LeaderHero != null)
				{
					party.AttachedTo.LeaderHero.IsKnownToPlayer = true;
				}
				using (List<MobileParty>.Enumerator enumerator = party.AttachedTo.AttachedParties.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						MobileParty mobileParty = enumerator.Current;
						if (mobileParty.LeaderHero != null)
						{
							mobileParty.LeaderHero.IsKnownToPlayer = true;
						}
					}
					return;
				}
			}
			if ((party.AttachedTo == MobileParty.MainParty || party.AttachedTo == MobileParty.MainParty.AttachedTo) && party.LeaderHero != null)
			{
				party.LeaderHero.IsKnownToPlayer = true;
			}
		}

		// Token: 0x06003726 RID: 14118 RVA: 0x000F8C18 File Offset: 0x000F6E18
		private void OnPartyAttachedToAnotherParty(MobileParty mobileParty)
		{
			if (mobileParty == MobileParty.MainParty)
			{
				if (mobileParty.AttachedTo.LeaderHero != null)
				{
					mobileParty.AttachedTo.LeaderHero.IsKnownToPlayer = true;
				}
				using (List<MobileParty>.Enumerator enumerator = mobileParty.AttachedTo.AttachedParties.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						MobileParty mobileParty2 = enumerator.Current;
						if (mobileParty2.LeaderHero != null)
						{
							mobileParty2.LeaderHero.IsKnownToPlayer = true;
						}
					}
					return;
				}
			}
			if ((mobileParty.AttachedTo == MobileParty.MainParty || mobileParty.AttachedTo == MobileParty.MainParty.AttachedTo) && mobileParty.LeaderHero != null)
			{
				mobileParty.LeaderHero.IsKnownToPlayer = true;
			}
		}

		// Token: 0x06003727 RID: 14119 RVA: 0x000F8CD8 File Offset: 0x000F6ED8
		private void OnMapEventStarted(MapEvent mapEvent, PartyBase attackerParty, PartyBase defenderParty)
		{
			if (MapEvent.PlayerMapEvent == mapEvent)
			{
				foreach (PartyBase partyBase in mapEvent.InvolvedParties)
				{
					if (partyBase.LeaderHero != null)
					{
						partyBase.LeaderHero.IsKnownToPlayer = true;
					}
				}
			}
		}

		// Token: 0x06003728 RID: 14120 RVA: 0x000F8D3C File Offset: 0x000F6F3C
		private void OnPlayerLearnsAboutHero(Hero hero)
		{
			if (hero.Clan != Clan.PlayerClan)
			{
				TextObject textObject = new TextObject("{=lLMlPcl4}You have discovered {HERO.NAME}", null);
				textObject.SetCharacterProperties("HERO", hero.CharacterObject, false);
				InformationManager.DisplayMessage(new InformationMessage(textObject.ToString()));
			}
		}

		// Token: 0x06003729 RID: 14121 RVA: 0x000F8D77 File Offset: 0x000F6F77
		private void OnAfterMissionStarted(IMission mission)
		{
			if (CampaignMission.Current.Location != null)
			{
				this.LearnAboutLocationCharacters(CampaignMission.Current.Location);
			}
		}

		// Token: 0x0600372A RID: 14122 RVA: 0x000F8D98 File Offset: 0x000F6F98
		private void OnGameMenuChanged(MenuCallbackArgs args)
		{
			foreach (Location location in Campaign.Current.GameMenuManager.MenuLocations)
			{
				this.LearnAboutLocationCharacters(location);
			}
		}

		// Token: 0x0600372B RID: 14123 RVA: 0x000F8DF4 File Offset: 0x000F6FF4
		private void LearnAboutLocationCharacters(Location location)
		{
			foreach (LocationCharacter locationCharacter in location.GetCharacterList())
			{
				if (locationCharacter.Character.IsHero && !locationCharacter.IsHidden && locationCharacter.Character.HeroObject.CurrentSettlement == Settlement.CurrentSettlement)
				{
					locationCharacter.Character.HeroObject.IsKnownToPlayer = true;
				}
			}
		}

		// Token: 0x0600372C RID: 14124 RVA: 0x000F8E78 File Offset: 0x000F7078
		private void OnPlayerMetHero(Hero hero)
		{
			this.UpdateHeroLocation(hero);
			hero.IsKnownToPlayer = true;
		}

		// Token: 0x0600372D RID: 14125 RVA: 0x000F8E88 File Offset: 0x000F7088
		private void OnDailyTickHero(Hero hero)
		{
			this.UpdateHeroLocation(hero);
		}

		// Token: 0x0600372E RID: 14126 RVA: 0x000F8E94 File Offset: 0x000F7094
		private void OnAgentJoinedConversation(IAgent agent)
		{
			CharacterObject characterObject = (CharacterObject)agent.Character;
			if (characterObject.IsHero)
			{
				this.UpdateHeroLocation(characterObject.HeroObject);
				characterObject.HeroObject.IsKnownToPlayer = true;
			}
			MobileParty conversationParty = MobileParty.ConversationParty;
			Hero hero;
			if (conversationParty == null)
			{
				hero = null;
			}
			else
			{
				CaravanPartyComponent caravanPartyComponent = conversationParty.CaravanPartyComponent;
				hero = ((caravanPartyComponent != null) ? caravanPartyComponent.Owner : null);
			}
			Hero hero2 = hero;
			if (hero2 != null)
			{
				hero2.IsKnownToPlayer = true;
			}
		}

		// Token: 0x0600372F RID: 14127 RVA: 0x000F8EF8 File Offset: 0x000F70F8
		private void UpdateHeroLocation(Hero hero)
		{
			if (hero.IsActive || hero.IsPrisoner)
			{
				Settlement closestSettlement = HeroHelper.GetClosestSettlement(hero);
				if (closestSettlement != null)
				{
					hero.UpdateLastKnownClosestSettlement(closestSettlement);
				}
			}
		}

		// Token: 0x06003730 RID: 14128 RVA: 0x000F8F28 File Offset: 0x000F7128
		private void OnCharacterCreationIsOver()
		{
			foreach (Hero hero in Hero.AllAliveHeroes)
			{
				this.UpdateHeroLocation(hero);
			}
		}

		// Token: 0x06003731 RID: 14129 RVA: 0x000F8F7C File Offset: 0x000F717C
		private void OnGameLoadFinishedEvent()
		{
			if (MBSaveLoad.IsUpdatingGameVersion && MBSaveLoad.LastLoadedGameVersion < ApplicationVersion.FromString("e1.8.1.0", 45697))
			{
				foreach (Hero hero in Clan.PlayerClan.Heroes)
				{
					hero.SetHasMet();
				}
				foreach (Hero hero2 in Hero.AllAliveHeroes)
				{
					if (hero2.LastKnownClosestSettlement == null)
					{
						this.UpdateHeroLocation(hero2);
					}
					if (hero2.HasMet)
					{
						hero2.IsKnownToPlayer = true;
					}
				}
			}
		}

		// Token: 0x06003732 RID: 14130 RVA: 0x000F9050 File Offset: 0x000F7250
		private void OnHeroesMarried(Hero hero1, Hero hero2, bool showNotification)
		{
			if (hero1 == Hero.MainHero)
			{
				hero2.SetHasMet();
			}
			if (hero2 == Hero.MainHero)
			{
				hero1.SetHasMet();
			}
		}

		// Token: 0x06003733 RID: 14131 RVA: 0x000F906E File Offset: 0x000F726E
		private void OnHeroCreated(Hero hero, bool isBornNaturally)
		{
			if (hero.Clan == Clan.PlayerClan)
			{
				hero.SetHasMet();
			}
		}

		// Token: 0x06003734 RID: 14132 RVA: 0x000F9084 File Offset: 0x000F7284
		private void ConversationEnded(IEnumerable<CharacterObject> conversationCharacters)
		{
			foreach (CharacterObject characterObject in conversationCharacters)
			{
				if (characterObject.IsHero)
				{
					characterObject.HeroObject.SetHasMet();
				}
			}
		}

		// Token: 0x06003735 RID: 14133 RVA: 0x000F90D8 File Offset: 0x000F72D8
		public override void SyncData(IDataStore dataStore)
		{
		}
	}
}
