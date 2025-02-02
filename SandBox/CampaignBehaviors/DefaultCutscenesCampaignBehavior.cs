using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Election;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.MapNotificationTypes;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.SceneInformationPopupTypes;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace SandBox.CampaignBehaviors
{
	// Token: 0x020000A9 RID: 169
	public class DefaultCutscenesCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x060007AE RID: 1966 RVA: 0x0003A72C File Offset: 0x0003892C
		public override void RegisterEvents()
		{
			CampaignEvents.HeroesMarried.AddNonSerializedListener(this, new Action<Hero, Hero, bool>(DefaultCutscenesCampaignBehavior.OnHeroesMarried));
			CampaignEvents.MapEventEnded.AddNonSerializedListener(this, new Action<MapEvent>(this.OnMapEventEnd));
			CampaignEvents.HeroComesOfAgeEvent.AddNonSerializedListener(this, new Action<Hero>(this.OnHeroComesOfAge));
			CampaignEvents.KingdomCreatedEvent.AddNonSerializedListener(this, new Action<Kingdom>(this.OnKingdomCreated));
			CampaignEvents.KingdomDestroyedEvent.AddNonSerializedListener(this, new Action<Kingdom>(this.OnKingdomDestroyed));
			CampaignEvents.OnClanChangedKingdomEvent.AddNonSerializedListener(this, new Action<Clan, Kingdom, Kingdom, ChangeKingdomAction.ChangeKingdomActionDetail, bool>(this.OnClanChangedKingdom));
			CampaignEvents.KingdomDecisionConcluded.AddNonSerializedListener(this, new Action<KingdomDecision, DecisionOutcome, bool>(this.OnKingdomDecisionConcluded));
			CampaignEvents.OnBeforeMainCharacterDiedEvent.AddNonSerializedListener(this, new Action<Hero, Hero, KillCharacterAction.KillCharacterActionDetail, bool>(this.OnBeforeMainCharacterDied));
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x0003A7F4 File Offset: 0x000389F4
		private void OnBeforeMainCharacterDied(Hero victim, Hero killer, KillCharacterAction.KillCharacterActionDetail detail, bool showNotification = true)
		{
			SceneNotificationData sceneNotificationData = null;
			if (victim == Hero.MainHero)
			{
				if (detail == KillCharacterAction.KillCharacterActionDetail.DiedOfOldAge)
				{
					sceneNotificationData = new DeathOldAgeSceneNotificationItem(victim);
				}
				else if (detail == KillCharacterAction.KillCharacterActionDetail.DiedInBattle)
				{
					if (this._heroWonLastMapEVent)
					{
						bool noCompanions = !victim.CompanionsInParty.Any<Hero>();
						List<CharacterObject> encounterAllyCharacters = new List<CharacterObject>();
						DefaultCutscenesCampaignBehavior.FillAllyCharacters(noCompanions, ref encounterAllyCharacters);
						sceneNotificationData = new MainHeroBattleVictoryDeathNotificationItem(victim, encounterAllyCharacters);
					}
					else
					{
						sceneNotificationData = new MainHeroBattleDeathNotificationItem(victim, this._lastEnemyCulture);
					}
				}
				else if (detail == KillCharacterAction.KillCharacterActionDetail.Executed)
				{
					TextObject to = new TextObject("{=uYjEknNX}{VICTIM.NAME}'s execution by {EXECUTER.NAME}", null);
					to.SetCharacterProperties("VICTIM", victim.CharacterObject, false);
					to.SetCharacterProperties("EXECUTER", killer.CharacterObject, false);
					sceneNotificationData = HeroExecutionSceneNotificationData.CreateForInformingPlayer(killer, victim, SceneNotificationData.RelevantContextType.Map);
				}
			}
			if (sceneNotificationData != null)
			{
				MBInformationManager.ShowSceneNotification(sceneNotificationData);
			}
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0003A8A0 File Offset: 0x00038AA0
		private void OnKingdomDecisionConcluded(KingdomDecision decision, DecisionOutcome chosenOutcome, bool isPlayerInvolved)
		{
			KingSelectionKingdomDecision.KingSelectionDecisionOutcome kingSelectionDecisionOutcome;
			if ((kingSelectionDecisionOutcome = (chosenOutcome as KingSelectionKingdomDecision.KingSelectionDecisionOutcome)) != null && isPlayerInvolved && kingSelectionDecisionOutcome.King == Hero.MainHero)
			{
				MBInformationManager.ShowSceneNotification(new BecomeKingSceneNotificationItem(kingSelectionDecisionOutcome.King));
			}
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0003A8DC File Offset: 0x00038ADC
		private void OnClanChangedKingdom(Clan clan, Kingdom oldKingdom, Kingdom newKingdom, ChangeKingdomAction.ChangeKingdomActionDetail detail, bool showNotification = true)
		{
			SceneNotificationData sceneNotificationData = null;
			if (clan == Clan.PlayerClan && detail == ChangeKingdomAction.ChangeKingdomActionDetail.JoinKingdom)
			{
				sceneNotificationData = new JoinKingdomSceneNotificationItem(clan, newKingdom);
			}
			else if (Clan.PlayerClan.Kingdom == newKingdom && detail == ChangeKingdomAction.ChangeKingdomActionDetail.JoinKingdomByDefection)
			{
				sceneNotificationData = new JoinKingdomSceneNotificationItem(clan, newKingdom);
			}
			if (sceneNotificationData != null)
			{
				MBInformationManager.ShowSceneNotification(sceneNotificationData);
			}
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0003A928 File Offset: 0x00038B28
		private void OnKingdomDestroyed(Kingdom kingdom)
		{
			if (!kingdom.IsRebelClan)
			{
				if (kingdom.Leader == Hero.MainHero)
				{
					MBInformationManager.ShowSceneNotification(Campaign.Current.Models.CutsceneSelectionModel.GetKingdomDestroyedSceneNotification(kingdom));
					return;
				}
				Campaign.Current.CampaignInformationManager.NewMapNoticeAdded(new KingdomDestroyedMapNotification(kingdom, CampaignTime.Now));
			}
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x0003A97F File Offset: 0x00038B7F
		private void OnKingdomCreated(Kingdom kingdom)
		{
			if (Hero.MainHero.Clan.Kingdom == kingdom)
			{
				MBInformationManager.ShowSceneNotification(new KingdomCreatedSceneNotificationItem(kingdom));
			}
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x0003A9A0 File Offset: 0x00038BA0
		private void OnHeroComesOfAge(Hero hero)
		{
			Hero mother = hero.Mother;
			if (((mother != null) ? mother.Clan : null) != Clan.PlayerClan)
			{
				Hero father = hero.Father;
				if (((father != null) ? father.Clan : null) != Clan.PlayerClan)
				{
					return;
				}
			}
			Hero mentorHeroForComeOfAge = this.GetMentorHeroForComeOfAge(hero);
			TextObject textObject = new TextObject("{=t4KwQOB7}{HERO.NAME} is now of age.", null);
			textObject.SetCharacterProperties("HERO", hero.CharacterObject, false);
			Campaign.Current.CampaignInformationManager.NewMapNoticeAdded(new HeirComeOfAgeMapNotification(hero, mentorHeroForComeOfAge, textObject, CampaignTime.Now));
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x0003AA24 File Offset: 0x00038C24
		private void OnMapEventEnd(MapEvent mapEvent)
		{
			if (mapEvent.IsPlayerMapEvent)
			{
				this._heroWonLastMapEVent = (mapEvent.WinningSide != BattleSideEnum.None && mapEvent.WinningSide == mapEvent.PlayerSide);
				this._lastEnemyCulture = ((mapEvent.PlayerSide == BattleSideEnum.Attacker) ? mapEvent.DefenderSide.MapFaction.Culture : mapEvent.AttackerSide.MapFaction.Culture);
			}
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x0003AA8A File Offset: 0x00038C8A
		private static void OnHeroesMarried(Hero firstHero, Hero secondHero, bool showNotification)
		{
			if (firstHero == Hero.MainHero || secondHero == Hero.MainHero)
			{
				Hero hero = firstHero.IsFemale ? secondHero : firstHero;
				MBInformationManager.ShowSceneNotification(new MarriageSceneNotificationItem(hero, hero.Spouse, CampaignTime.Now, SceneNotificationData.RelevantContextType.Any));
			}
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x0003AABE File Offset: 0x00038CBE
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x0003AAC0 File Offset: 0x00038CC0
		private static void FillAllyCharacters(bool noCompanions, ref List<CharacterObject> allyCharacters)
		{
			if (noCompanions)
			{
				allyCharacters.Add(Hero.MainHero.MapFaction.Culture.RangedEliteMilitiaTroop);
				return;
			}
			List<CharacterObject> source = (from c in MobileParty.MainParty.MemberRoster.GetTroopRoster()
			where c.Character != CharacterObject.PlayerCharacter && c.Character.IsHero
			select c into t
			select t.Character).ToList<CharacterObject>();
			allyCharacters.AddRange(source.Take(3));
			int count = allyCharacters.Count;
			for (int i = 0; i < 3 - count; i++)
			{
				allyCharacters.Add(Hero.AllAliveHeroes.GetRandomElement<Hero>().CharacterObject);
			}
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x0003AB84 File Offset: 0x00038D84
		private Hero GetMentorHeroForComeOfAge(Hero hero)
		{
			Hero result = Hero.MainHero;
			if (hero.IsFemale)
			{
				if (hero.Mother != null && hero.Mother.IsAlive)
				{
					result = hero.Mother;
				}
				else if (hero.Father != null && hero.Father.IsAlive)
				{
					result = hero.Father;
				}
			}
			else if (hero.Father != null && hero.Father.IsAlive)
			{
				result = hero.Father;
			}
			else if (hero.Mother != null && hero.Mother.IsAlive)
			{
				result = hero.Mother;
			}
			if (hero.Mother == Hero.MainHero || hero.Father == Hero.MainHero)
			{
				result = Hero.MainHero;
			}
			return result;
		}

		// Token: 0x04000309 RID: 777
		private bool _heroWonLastMapEVent;

		// Token: 0x0400030A RID: 778
		private CultureObject _lastEnemyCulture;
	}
}
