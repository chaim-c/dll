using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using SandBox.Conversation;
using SandBox.Missions.AgentBehaviors;
using SandBox.Missions.MissionLogics;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.AgentOrigins;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.LogEntries;
using TaleWorlds.CampaignSystem.MapNotificationTypes;
using TaleWorlds.CampaignSystem.Overlay;
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

namespace SandBox.CampaignBehaviors
{
	// Token: 0x0200009F RID: 159
	public class AlleyCampaignBehavior : CampaignBehaviorBase, IAlleyCampaignBehavior, ICampaignBehavior
	{
		// Token: 0x06000620 RID: 1568 RVA: 0x0002CC40 File Offset: 0x0002AE40
		public override void RegisterEvents()
		{
			CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnSessionLaunched));
			CampaignEvents.HeroKilledEvent.AddNonSerializedListener(this, new Action<Hero, Hero, KillCharacterAction.KillCharacterActionDetail, bool>(this.OnHeroKilled));
			CampaignEvents.LocationCharactersAreReadyToSpawnEvent.AddNonSerializedListener(this, new Action<Dictionary<string, int>>(this.LocationCharactersAreReadyToSpawn));
			CampaignEvents.OnNewGameCreatedPartialFollowUpEndEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnNewGameCreated));
			CampaignEvents.AlleyOccupiedByPlayer.AddNonSerializedListener(this, new Action<Alley, TroopRoster>(this.OnAlleyOccupiedByPlayer));
			CampaignEvents.AlleyClearedByPlayer.AddNonSerializedListener(this, new Action<Alley>(this.OnAlleyClearedByPlayer));
			CampaignEvents.AlleyOwnerChanged.AddNonSerializedListener(this, new Action<Alley, Hero, Hero>(this.OnAlleyOwnerChanged));
			CampaignEvents.DailyTickSettlementEvent.AddNonSerializedListener(this, new Action<Settlement>(this.DailyTickSettlement));
			CampaignEvents.DailyTickEvent.AddNonSerializedListener(this, new Action(this.DailyTick));
			CampaignEvents.CanHeroLeadPartyEvent.AddNonSerializedListener(this, new ReferenceAction<Hero, bool>(this.CommonAlleyLeaderRestriction));
			CampaignEvents.CanBeGovernorOrHavePartyRoleEvent.AddNonSerializedListener(this, new ReferenceAction<Hero, bool>(this.CommonAlleyLeaderRestriction));
			CampaignEvents.AfterMissionStarted.AddNonSerializedListener(this, new Action<IMission>(this.OnAfterMissionStarted));
			CampaignEvents.CanHeroDieEvent.AddNonSerializedListener(this, new ReferenceAction<Hero, KillCharacterAction.KillCharacterActionDetail, bool>(this.CanHeroDie));
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x0002CD78 File Offset: 0x0002AF78
		private void CanHeroDie(Hero hero, KillCharacterAction.KillCharacterActionDetail detail, ref bool result)
		{
			if (hero == Hero.MainHero && Mission.Current != null && this._playerIsInAlleyFightMission)
			{
				result = false;
			}
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0002CD94 File Offset: 0x0002AF94
		private void OnAfterMissionStarted(IMission mission)
		{
			this._playerIsInAlleyFightMission = false;
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0002CD9D File Offset: 0x0002AF9D
		private void OnAlleyOwnerChanged(Alley alley, Hero newOwner, Hero oldOwner)
		{
			if (oldOwner == Hero.MainHero)
			{
				TextObject textObject = new TextObject("{=wAgfOHio}You have lost the ownership of the alley at {SETTLEMENT}.", null);
				textObject.SetTextVariable("SETTLEMENT", alley.Settlement.Name);
				MBInformationManager.AddQuickInformation(textObject, 0, null, "");
			}
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x0002CDD8 File Offset: 0x0002AFD8
		private void CommonAlleyLeaderRestriction(Hero hero, ref bool result)
		{
			if (this._playerOwnedCommonAreaData.Any((AlleyCampaignBehavior.PlayerAlleyData x) => x.AssignedClanMember == hero))
			{
				result = false;
			}
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0002CE10 File Offset: 0x0002B010
		private void DailyTick()
		{
			for (int i = this._playerOwnedCommonAreaData.Count - 1; i >= 0; i--)
			{
				AlleyCampaignBehavior.PlayerAlleyData playerAlleyData = this._playerOwnedCommonAreaData[i];
				this.CheckConvertTroopsToBandits(playerAlleyData);
				SkillLevelingManager.OnDailyAlleyTick(playerAlleyData.Alley, playerAlleyData.AssignedClanMember);
				if (playerAlleyData.AssignedClanMember.IsDead && playerAlleyData.AssignedClanMember.DeathDay + Campaign.Current.Models.AlleyModel.DestroyAlleyAfterDaysWhenLeaderIsDeath < CampaignTime.Now)
				{
					this._playerOwnedCommonAreaData.Remove(playerAlleyData);
					playerAlleyData.DestroyAlley(false);
				}
				else if (!playerAlleyData.IsUnderAttack && !playerAlleyData.AssignedClanMember.IsDead)
				{
					this.CheckSpawningNewAlleyFight(playerAlleyData);
				}
				else if (playerAlleyData.IsUnderAttack && playerAlleyData.AttackResponseDueDate.IsPast)
				{
					this._playerOwnedCommonAreaData.Remove(playerAlleyData);
					playerAlleyData.DestroyAlley(false);
				}
			}
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0002CEFC File Offset: 0x0002B0FC
		private void CheckSpawningNewAlleyFight(AlleyCampaignBehavior.PlayerAlleyData playerOwnedArea)
		{
			if (MBRandom.RandomFloat < 0.015f)
			{
				if (playerOwnedArea.Alley.Settlement.Alleys.Any((Alley x) => x.State == Alley.AreaState.OccupiedByGangLeader))
				{
					this.StartNewAlleyAttack(playerOwnedArea);
				}
			}
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0002CF54 File Offset: 0x0002B154
		private void StartNewAlleyAttack(AlleyCampaignBehavior.PlayerAlleyData playerOwnedArea)
		{
			playerOwnedArea.UnderAttackBy = (from x in playerOwnedArea.Alley.Settlement.Alleys
			where x.State == Alley.AreaState.OccupiedByGangLeader
			select x).GetRandomElementInefficiently<Alley>();
			playerOwnedArea.UnderAttackBy.Owner.SetHasMet();
			float alleyAttackResponseTimeInDays = Campaign.Current.Models.AlleyModel.GetAlleyAttackResponseTimeInDays(playerOwnedArea.TroopRoster);
			playerOwnedArea.AttackResponseDueDate = CampaignTime.DaysFromNow(alleyAttackResponseTimeInDays);
			TextObject textObject = new TextObject("{=5bIpeW9X}Your alley in {SETTLEMENT} is under attack from neighboring gangs. Unless you go to their help, the alley will be lost in {RESPONSE_TIME} days.", null);
			textObject.SetTextVariable("SETTLEMENT", playerOwnedArea.Alley.Settlement.Name);
			textObject.SetTextVariable("RESPONSE_TIME", alleyAttackResponseTimeInDays);
			ChangeRelationAction.ApplyPlayerRelation(playerOwnedArea.UnderAttackBy.Owner, -5, true, true);
			Campaign.Current.CampaignInformationManager.NewMapNoticeAdded(new AlleyUnderAttackMapNotification(playerOwnedArea.Alley, textObject));
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x0002D03C File Offset: 0x0002B23C
		private void CheckConvertTroopsToBandits(AlleyCampaignBehavior.PlayerAlleyData playerOwnedArea)
		{
			foreach (FlattenedTroopRosterElement flattenedTroopRosterElement in playerOwnedArea.TroopRoster.ToFlattenedRoster())
			{
				if (MBRandom.RandomFloat < 0.01f && !flattenedTroopRosterElement.Troop.IsHero && flattenedTroopRosterElement.Troop.Occupation != Occupation.Gangster)
				{
					playerOwnedArea.TroopRoster.RemoveTroop(flattenedTroopRosterElement.Troop, 1, default(UniqueTroopDescriptor), 0);
					CharacterObject characterObject = this._thug;
					if (characterObject.Tier < flattenedTroopRosterElement.Troop.Tier)
					{
						characterObject = this._expertThug;
					}
					if (characterObject.Tier < flattenedTroopRosterElement.Troop.Tier)
					{
						characterObject = this._masterThug;
					}
					playerOwnedArea.TroopRoster.AddToCounts(characterObject, 1, false, 0, 0, true, -1);
				}
			}
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x0002D128 File Offset: 0x0002B328
		private void OnNewGameCreated(CampaignGameStarter gameStarter)
		{
			foreach (Town town in Town.AllTowns)
			{
				int num = MBRandom.RandomInt(0, town.Settlement.Alleys.Count);
				IEnumerable<Hero> source = from x in town.Settlement.Notables
				where x.IsGangLeader
				select x;
				for (int i = num; i < num + 2; i++)
				{
					town.Settlement.Alleys[i % town.Settlement.Alleys.Count].SetOwner(source.ElementAt(i % source.Count<Hero>()));
				}
			}
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x0002D20C File Offset: 0x0002B40C
		private void DailyTickSettlement(Settlement settlement)
		{
			this.TickAlleyOwnerships(settlement);
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0002D218 File Offset: 0x0002B418
		private void TickAlleyOwnerships(Settlement settlement)
		{
			foreach (Hero hero in settlement.Notables)
			{
				if (hero.IsGangLeader)
				{
					int count = hero.OwnedAlleys.Count;
					float num = 0.02f - (float)count * 0.005f;
					float num2 = (float)count * 0.005f;
					if (MBRandom.RandomFloat < num)
					{
						Alley alley = settlement.Alleys.FirstOrDefault((Alley x) => x.State == Alley.AreaState.Empty);
						if (alley != null)
						{
							alley.SetOwner(hero);
						}
					}
					if (MBRandom.RandomFloat < num2)
					{
						Alley randomElement = hero.OwnedAlleys.GetRandomElement<Alley>();
						if (randomElement != null)
						{
							randomElement.SetOwner(null);
						}
					}
					if (!hero.IsHealthFull())
					{
						hero.Heal(10, false);
					}
				}
			}
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0002D30C File Offset: 0x0002B50C
		private void OnAlleyOccupiedByPlayer(Alley alley, TroopRoster troopRoster)
		{
			alley.SetOwner(Hero.MainHero);
			AlleyCampaignBehavior.PlayerAlleyData playerAlleyData = new AlleyCampaignBehavior.PlayerAlleyData(alley, troopRoster);
			this._playerOwnedCommonAreaData.Add(playerAlleyData);
			TeleportHeroAction.ApplyDelayedTeleportToSettlement(playerAlleyData.AssignedClanMember, alley.Settlement);
			if (alley.Settlement.OwnerClan != Clan.PlayerClan)
			{
				ChangeRelationAction.ApplyPlayerRelation(alley.Settlement.Owner, -2, true, true);
			}
			SkillLevelingManager.OnAlleyCleared(alley);
			this.AddPlayerAlleyCharacters(alley);
			Mission.Current.ClearCorpses(false);
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0002D388 File Offset: 0x0002B588
		private void OnAlleyClearedByPlayer(Alley alley)
		{
			ChangeRelationAction.ApplyPlayerRelation(alley.Owner, -5, true, true);
			foreach (Hero hero in alley.Settlement.Notables)
			{
				if (!hero.IsGangLeader)
				{
					ChangeRelationAction.ApplyPlayerRelation(hero, 1, true, true);
				}
			}
			AlleyCampaignBehavior.PlayerAlleyData playerAlleyData = this._playerOwnedCommonAreaData.FirstOrDefault((AlleyCampaignBehavior.PlayerAlleyData x) => x.Alley.Settlement == Settlement.CurrentSettlement);
			if (((playerAlleyData != null) ? playerAlleyData.UnderAttackBy : null) == alley)
			{
				playerAlleyData.UnderAttackBy = null;
			}
			alley.SetOwner(null);
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0002D444 File Offset: 0x0002B644
		public override void SyncData(IDataStore dataStore)
		{
			dataStore.SyncData<List<AlleyCampaignBehavior.PlayerAlleyData>>("_playerOwnedCommonAreaData", ref this._playerOwnedCommonAreaData);
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0002D458 File Offset: 0x0002B658
		public void OnSessionLaunched(CampaignGameStarter campaignGameStarter)
		{
			this._thug = MBObjectManager.Instance.GetObject<CharacterObject>("gangster_1");
			this._expertThug = MBObjectManager.Instance.GetObject<CharacterObject>("gangster_2");
			this._masterThug = MBObjectManager.Instance.GetObject<CharacterObject>("gangster_3");
			this.AddGameMenus(campaignGameStarter);
			this.AddDialogs(campaignGameStarter);
			if (MBSaveLoad.IsUpdatingGameVersion && MBSaveLoad.LastLoadedGameVersion < ApplicationVersion.FromString("v1.2.0", 45697))
			{
				foreach (AlleyCampaignBehavior.PlayerAlleyData playerAlleyData in this._playerOwnedCommonAreaData)
				{
					if (playerAlleyData.IsUnderAttack && playerAlleyData.UnderAttackBy.Owner == null)
					{
						playerAlleyData.UnderAttackBy = null;
					}
				}
			}
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x0002D530 File Offset: 0x0002B730
		private void LocationCharactersAreReadyToSpawn(Dictionary<string, int> unusedUsablePointCount)
		{
			Settlement currentSettlement = Settlement.CurrentSettlement;
			if (currentSettlement.IsTown)
			{
				foreach (Alley alley in currentSettlement.Alleys)
				{
					if (alley.State == Alley.AreaState.OccupiedByGangLeader)
					{
						using (List<TroopRosterElement>.Enumerator enumerator2 = Campaign.Current.Models.AlleyModel.GetTroopsOfAIOwnedAlley(alley).GetTroopRoster().GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								TroopRosterElement troopRosterElement = enumerator2.Current;
								for (int i = 0; i < troopRosterElement.Number; i++)
								{
									this.AddCharacterToAlley(troopRosterElement.Character, alley);
								}
							}
							continue;
						}
					}
					if (alley.State == Alley.AreaState.OccupiedByPlayer)
					{
						this.AddPlayerAlleyCharacters(alley);
					}
				}
			}
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x0002D620 File Offset: 0x0002B820
		private void AddPlayerAlleyCharacters(Alley alley)
		{
			if (Mission.Current != null)
			{
				for (int i = Mission.Current.Agents.Count - 1; i >= 0; i--)
				{
					Agent agent = Mission.Current.Agents[i];
					if (agent.IsHuman && !agent.Character.IsHero)
					{
						CampaignAgentComponent component = agent.GetComponent<CampaignAgentComponent>();
						Hero hero;
						if (component == null)
						{
							hero = null;
						}
						else
						{
							AgentNavigator agentNavigator = component.AgentNavigator;
							if (agentNavigator == null)
							{
								hero = null;
							}
							else
							{
								Alley memberOfAlley = agentNavigator.MemberOfAlley;
								hero = ((memberOfAlley != null) ? memberOfAlley.Owner : null);
							}
						}
						if (hero == Hero.MainHero)
						{
							agent.FadeOut(false, true);
						}
					}
				}
			}
			foreach (TroopRosterElement troopRosterElement in this._playerOwnedCommonAreaData.First((AlleyCampaignBehavior.PlayerAlleyData x) => x.Alley == alley).TroopRoster.GetTroopRoster())
			{
				if (!troopRosterElement.Character.IsHero || !troopRosterElement.Character.HeroObject.IsTraveling)
				{
					for (int j = 0; j < troopRosterElement.Number; j++)
					{
						this.AddCharacterToAlley(troopRosterElement.Character, alley);
					}
				}
			}
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0002D764 File Offset: 0x0002B964
		private void AddCharacterToAlley(CharacterObject character, Alley alley)
		{
			Location locationWithId = Settlement.CurrentSettlement.LocationComplex.GetLocationWithId("center");
			LocationCharacter locationCharacter = null;
			if (character.IsHero)
			{
				Location locationOfCharacter = Settlement.CurrentSettlement.LocationComplex.GetLocationOfCharacter(character.HeroObject);
				if (locationOfCharacter != null && locationOfCharacter == locationWithId)
				{
					return;
				}
				locationCharacter = Settlement.CurrentSettlement.LocationComplex.GetLocationCharacterOfHero(character.HeroObject);
			}
			if (locationCharacter == null)
			{
				Monster monsterWithSuffix = TaleWorlds.Core.FaceGen.GetMonsterWithSuffix(character.Race, "_settlement");
				int minValue;
				int maxValue;
				Campaign.Current.Models.AgeModel.GetAgeLimitForLocation(character, out minValue, out maxValue, "AlleyGangMember");
				AgentData agentData = new AgentData(new SimpleAgentOrigin(character, -1, null, default(UniqueTroopDescriptor))).Monster(monsterWithSuffix).NoHorses(true).Age(MBRandom.RandomInt(minValue, maxValue));
				locationCharacter = new LocationCharacter(agentData, new LocationCharacter.AddBehaviorsDelegate(BehaviorSets.AddFixedCharacterBehaviors), alley.Tag, true, LocationCharacter.CharacterRelations.Neutral, ActionSetCode.GenerateActionSetNameWithSuffix(agentData.AgentMonster, agentData.AgentIsFemale, "_villain"), true, false, null, false, false, true);
			}
			locationCharacter.SpecialTargetTag = alley.Tag;
			locationCharacter.SetAlleyOfCharacter(alley);
			Settlement.CurrentSettlement.LocationComplex.ChangeLocation(locationCharacter, Settlement.CurrentSettlement.LocationComplex.GetLocationOfCharacter(locationCharacter), locationWithId);
			if (character.IsHero)
			{
				CampaignEventDispatcher.Instance.OnHeroGetsBusy(character.HeroObject, HeroGetsBusyReasons.BecomeAlleyLeader);
			}
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0002D8B4 File Offset: 0x0002BAB4
		protected void AddGameMenus(CampaignGameStarter campaignGameSystemStarter)
		{
			campaignGameSystemStarter.AddGameMenuOption("town", "manage_alley", "{=VkOtMe5a}Go to alley", new GameMenuOption.OnConditionDelegate(this.go_to_alley_on_condition), new GameMenuOption.OnConsequenceDelegate(this.go_to_alley_on_consequence), false, 5, false, null);
			campaignGameSystemStarter.AddGameMenu("manage_alley", "{=dWf6ztYu}You are in your alley by the {ALLEY_TYPE}, {FURTHER_INFO}", new OnInitDelegate(this.manage_alley_menu_on_init), GameOverlays.MenuOverlayType.SettlementWithBoth, GameMenu.MenuFlags.None, null);
			campaignGameSystemStarter.AddGameMenuOption("manage_alley", "confront_hostile_alley_leader", "{=grhRXqen}Confront {HOSTILE_GANG_LEADER.NAME} about {?HOSTILE_GANG_LEADER.GENDER}her{?}his{\\?} attack on your alley.", new GameMenuOption.OnConditionDelegate(this.alley_under_attack_on_condition), new GameMenuOption.OnConsequenceDelegate(this.alley_under_attack_response_on_consequence), false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("manage_alley", "change_leader_of_alley", "{=ClyaDhGU}Change the leader of the alley", new GameMenuOption.OnConditionDelegate(this.change_leader_of_alley_on_condition), new GameMenuOption.OnConsequenceDelegate(this.change_leader_of_the_alley_on_consequence), false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("manage_alley", "manage_alley_troops", "{=QrBCe41Z}Manage alley troops", new GameMenuOption.OnConditionDelegate(this.manage_alley_on_condition), new GameMenuOption.OnConsequenceDelegate(this.manage_troops_of_alley), false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("manage_alley", "abandon_alley", "{=ELfguvYD}Abandon the alley", new GameMenuOption.OnConditionDelegate(this.abandon_alley_on_condition), new GameMenuOption.OnConsequenceDelegate(this.abandon_alley_are_you_sure_consequence), false, -1, false, null);
			campaignGameSystemStarter.AddGameMenu("manage_alley_abandon_are_you_sure", "{=awjomtnJ}Are you sure?", null, GameOverlays.MenuOverlayType.SettlementWithBoth, GameMenu.MenuFlags.None, null);
			campaignGameSystemStarter.AddGameMenuOption("manage_alley_abandon_are_you_sure", "abandon_alley_yes", "{=aeouhelq}Yes", new GameMenuOption.OnConditionDelegate(this.alley_continue_on_condition), new GameMenuOption.OnConsequenceDelegate(this.abandon_alley_consequence), false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("manage_alley_abandon_are_you_sure", "abandon_alley_no", "{=8OkPHu4f}No", new GameMenuOption.OnConditionDelegate(this.alley_go_back_on_condition), new GameMenuOption.OnConsequenceDelegate(this.go_to_alley_on_consequence), true, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("manage_alley", "back", "{=4QNycK7T}Go back", new GameMenuOption.OnConditionDelegate(this.alley_go_back_on_condition), new GameMenuOption.OnConsequenceDelegate(this.leave_alley_menu_consequence), true, -1, false, null);
			campaignGameSystemStarter.AddGameMenu("alley_fight_lost", "{=po79q14T}You have failed to defend your alley against the attack, you have lost the ownership of it.", null, GameOverlays.MenuOverlayType.None, GameMenu.MenuFlags.None, null);
			campaignGameSystemStarter.AddGameMenuOption("alley_fight_lost", "continue", "{=DM6luo3c}Continue", new GameMenuOption.OnConditionDelegate(this.alley_continue_on_condition), new GameMenuOption.OnConsequenceDelegate(this.alley_fight_continue_on_consequence), false, -1, false, null);
			campaignGameSystemStarter.AddGameMenu("alley_fight_won", "{=i1sgAm0F}You have succeeded in defending your alley against the attack. You might want to leave some troops in order to compensate for your losses in the fight.", null, GameOverlays.MenuOverlayType.None, GameMenu.MenuFlags.None, null);
			campaignGameSystemStarter.AddGameMenuOption("alley_fight_won", "continue", "{=DM6luo3c}Continue", new GameMenuOption.OnConditionDelegate(this.alley_continue_on_condition), new GameMenuOption.OnConsequenceDelegate(this.alley_fight_continue_on_consequence), false, -1, false, null);
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0002DB06 File Offset: 0x0002BD06
		private void abandon_alley_are_you_sure_consequence(MenuCallbackArgs args)
		{
			GameMenu.SwitchToMenu("manage_alley_abandon_are_you_sure");
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0002DB12 File Offset: 0x0002BD12
		private bool alley_continue_on_condition(MenuCallbackArgs args)
		{
			args.optionLeaveType = GameMenuOption.LeaveType.Continue;
			return true;
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0002DB1D File Offset: 0x0002BD1D
		private void alley_fight_continue_on_consequence(MenuCallbackArgs args)
		{
			GameMenu.SwitchToMenu("town");
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0002DB29 File Offset: 0x0002BD29
		private bool alley_go_back_on_condition(MenuCallbackArgs args)
		{
			args.optionLeaveType = GameMenuOption.LeaveType.Leave;
			return true;
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0002DB34 File Offset: 0x0002BD34
		private bool abandon_alley_on_condition(MenuCallbackArgs args)
		{
			args.optionLeaveType = GameMenuOption.LeaveType.Surrender;
			return true;
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x0002DB40 File Offset: 0x0002BD40
		private void alley_under_attack_response_on_consequence(MenuCallbackArgs args)
		{
			AlleyCampaignBehavior.PlayerAlleyData playerAlleyData = this._playerOwnedCommonAreaData.First((AlleyCampaignBehavior.PlayerAlleyData x) => x.Alley.Settlement == Settlement.CurrentSettlement);
			CampaignMapConversation.OpenConversation(new ConversationCharacterData(CharacterObject.PlayerCharacter, null, false, false, false, false, false, false), new ConversationCharacterData(playerAlleyData.UnderAttackBy.Owner.CharacterObject, null, false, false, false, false, false, false));
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0002DBAC File Offset: 0x0002BDAC
		private bool alley_under_attack_on_condition(MenuCallbackArgs args)
		{
			AlleyCampaignBehavior.PlayerAlleyData playerAlleyData = this._playerOwnedCommonAreaData.FirstOrDefault((AlleyCampaignBehavior.PlayerAlleyData x) => x.Alley.Owner == Hero.MainHero && x.Alley.Settlement == Settlement.CurrentSettlement && x.IsUnderAttack);
			if (playerAlleyData != null)
			{
				args.optionLeaveType = GameMenuOption.LeaveType.DefendAction;
				StringHelpers.SetCharacterProperties("HOSTILE_GANG_LEADER", playerAlleyData.UnderAttackBy.Owner.CharacterObject, null, false);
				TextObject textObject = new TextObject("{=9t1LGNz6}{RESPONSE_TIME} {?RESPONSE_TIME>1}days{?}day{\\?} left.", null);
				textObject.SetTextVariable("RESPONSE_TIME", this.GetResponseTimeLeftForAttackInDays(playerAlleyData.Alley));
				args.Tooltip = textObject;
				return true;
			}
			return false;
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0002DC3A File Offset: 0x0002BE3A
		private bool manage_alley_on_condition(MenuCallbackArgs args)
		{
			if (this.alley_under_attack_on_condition(args))
			{
				args.IsEnabled = false;
				args.Tooltip = new TextObject("{=pdqi2qz1}You can not do this action while your alley is under attack.", null);
			}
			args.optionLeaveType = GameMenuOption.LeaveType.ManageGarrison;
			return true;
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x0002DC66 File Offset: 0x0002BE66
		private bool change_leader_of_alley_on_condition(MenuCallbackArgs args)
		{
			if (this.alley_under_attack_on_condition(args))
			{
				args.IsEnabled = false;
				args.Tooltip = new TextObject("{=pdqi2qz1}You can not do this action while your alley is under attack.", null);
			}
			args.optionLeaveType = GameMenuOption.LeaveType.Continue;
			return true;
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0002DC92 File Offset: 0x0002BE92
		private bool go_to_alley_on_condition(MenuCallbackArgs args)
		{
			args.optionLeaveType = GameMenuOption.LeaveType.Submenu;
			return this._playerOwnedCommonAreaData.Any((AlleyCampaignBehavior.PlayerAlleyData x) => x.Alley.Settlement == Settlement.CurrentSettlement);
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0002DCC5 File Offset: 0x0002BEC5
		private void go_to_alley_on_consequence(MenuCallbackArgs args)
		{
			GameMenu.SwitchToMenu("manage_alley");
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0002DCD1 File Offset: 0x0002BED1
		private void leave_alley_menu_consequence(MenuCallbackArgs args)
		{
			GameMenu.SwitchToMenu("town_outside");
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0002DCE0 File Offset: 0x0002BEE0
		private void abandon_alley_consequence(MenuCallbackArgs args)
		{
			args.optionLeaveType = GameMenuOption.LeaveType.LeaveTroopsAndFlee;
			AlleyCampaignBehavior.PlayerAlleyData playerAlleyData = this._playerOwnedCommonAreaData.First((AlleyCampaignBehavior.PlayerAlleyData x) => x.Alley.Settlement == Settlement.CurrentSettlement);
			this._playerOwnedCommonAreaData.Remove(playerAlleyData);
			playerAlleyData.AbandonTheAlley(false);
			GameMenu.SwitchToMenu("town_outside");
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0002DD40 File Offset: 0x0002BF40
		private void manage_troops_of_alley(MenuCallbackArgs args)
		{
			AlleyHelper.OpenScreenForManagingAlley(this._playerOwnedCommonAreaData.First((AlleyCampaignBehavior.PlayerAlleyData x) => x.Alley.Settlement == Settlement.CurrentSettlement).TroopRoster, new PartyPresentationDoneButtonDelegate(this.OnPartyScreenClosed), new TextObject("{=dQBArrqh}Manage Alley", null), null);
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0002DD9C File Offset: 0x0002BF9C
		private bool OnPartyScreenClosed(TroopRoster leftMemberRoster, TroopRoster leftPrisonRoster, TroopRoster rightMemberRoster, TroopRoster rightPrisonRoster, FlattenedTroopRoster takenPrisonerRoster, FlattenedTroopRoster releasedPrisonerRoster, bool isForced, PartyBase leftParty, PartyBase rightParty)
		{
			AlleyCampaignBehavior.PlayerAlleyData playerAlleyData = this._playerOwnedCommonAreaData.First((AlleyCampaignBehavior.PlayerAlleyData x) => x.Alley.Settlement == Settlement.CurrentSettlement);
			playerAlleyData.TroopRoster = leftMemberRoster;
			if (Mission.Current != null)
			{
				this.AddPlayerAlleyCharacters(playerAlleyData.Alley);
			}
			return true;
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x0002DDF0 File Offset: 0x0002BFF0
		private void change_leader_of_the_alley_on_consequence(MenuCallbackArgs args)
		{
			AlleyHelper.CreateMultiSelectionInquiryForSelectingClanMemberToAlley(this._playerOwnedCommonAreaData.First((AlleyCampaignBehavior.PlayerAlleyData x) => x.Alley.Settlement == Settlement.CurrentSettlement).Alley, new Action<List<InquiryElement>>(this.ChangeAssignedClanMemberOfAlley), null);
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0002DE40 File Offset: 0x0002C040
		private void ChangeAssignedClanMemberOfAlley(List<InquiryElement> newClanMemberInquiryElement)
		{
			AlleyCampaignBehavior.PlayerAlleyData alleyData = this._playerOwnedCommonAreaData.First((AlleyCampaignBehavior.PlayerAlleyData x) => x.Alley.Settlement == Settlement.CurrentSettlement);
			Hero heroObject = (newClanMemberInquiryElement.First<InquiryElement>().Identifier as CharacterObject).HeroObject;
			this.ChangeTheLeaderOfAlleyInternal(alleyData, heroObject);
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x0002DE98 File Offset: 0x0002C098
		private void manage_alley_menu_on_init(MenuCallbackArgs args)
		{
			Campaign.Current.GameMenuManager.MenuLocations.Clear();
			Campaign.Current.GameMenuManager.MenuLocations.Add(Settlement.CurrentSettlement.LocationComplex.GetLocationWithId("alley"));
			AlleyCampaignBehavior.PlayerAlleyData playerAlleyData = this._playerOwnedCommonAreaData.FirstOrDefault((AlleyCampaignBehavior.PlayerAlleyData x) => x.Alley.Settlement == Settlement.CurrentSettlement);
			if (playerAlleyData == null)
			{
				GameMenu.SwitchToMenu(this._playerAbandonedAlleyFromDialogRecently ? "town" : "alley_fight_lost");
				this._playerAbandonedAlleyFromDialogRecently = false;
				return;
			}
			MBTextManager.SetTextVariable("ALLEY_TYPE", playerAlleyData.Alley.Name, false);
			TextObject textObject = TextObject.Empty;
			if (playerAlleyData.AssignedClanMember.IsTraveling)
			{
				textObject = new TextObject("{=AjBYneFr}{CLAN_MEMBER.NAME} is in charge of the alley. {?CLAN_MEMBER.GENDER}She{?}He{\\?} is currently traveling to the alley and will arrive after {HOURS} {?HOURS > 1}hours{?}hour{\\?}.", null);
				int variable = MathF.Ceiling(TeleportationHelper.GetHoursLeftForTeleportingHeroToReachItsDestination(playerAlleyData.AssignedClanMember));
				textObject.SetTextVariable("HOURS", variable);
				MBTextManager.SetTextVariable("FURTHER_INFO", textObject, false);
			}
			else if (playerAlleyData.AssignedClanMember.IsDead)
			{
				textObject = new TextObject("{=P5UbgK4c}{CLAN_MEMBER.NAME} was in charge of the alley. {?CLAN_MEMBER.GENDER}She{?}He{\\?} is dead. Alley will be abandoned after {REMAINING_DAYS} {?REMAINING_DAYS>1}days{?}day{\\?} unless a new overseer is assigned.", null);
				textObject.SetTextVariable("REMAINING_DAYS", (int)Math.Ceiling((playerAlleyData.AssignedClanMember.DeathDay + Campaign.Current.Models.AlleyModel.DestroyAlleyAfterDaysWhenLeaderIsDeath - CampaignTime.Now).ToDays));
				MBTextManager.SetTextVariable("FURTHER_INFO", textObject, false);
			}
			else
			{
				TextObject text = new TextObject("{=fcqdfY09}{CLAN_MEMBER.NAME} is in charge of the alley.", null);
				MBTextManager.SetTextVariable("FURTHER_INFO", text, false);
			}
			StringHelpers.SetCharacterProperties("CLAN_MEMBER", playerAlleyData.AssignedClanMember.CharacterObject, null, false);
			if (this._waitForBattleResults)
			{
				this._waitForBattleResults = false;
				playerAlleyData.TroopRoster.AddToCounts(CharacterObject.PlayerCharacter, -1, true, 0, 0, true, -1);
				if ((playerAlleyData.TroopRoster.TotalManCount == 0 && this._playerDiedInMission) || this._playerRetreatedFromMission)
				{
					this._playerOwnedCommonAreaData.Remove(playerAlleyData);
					playerAlleyData.AlleyFightLost();
				}
				else
				{
					playerAlleyData.AlleyFightWon();
				}
				this._playerRetreatedFromMission = false;
				this._playerDiedInMission = false;
			}
			if (this._battleWillStartInCurrentSettlement)
			{
				this.StartAlleyFightWithOtherAlley();
			}
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0002E0B4 File Offset: 0x0002C2B4
		private void StartAlleyFightWithOtherAlley()
		{
			AlleyCampaignBehavior.PlayerAlleyData playerAlleyData = this._playerOwnedCommonAreaData.First((AlleyCampaignBehavior.PlayerAlleyData x) => x.Alley.Settlement == Settlement.CurrentSettlement);
			TroopRoster troopRoster = playerAlleyData.TroopRoster;
			if (playerAlleyData.AssignedClanMember.IsTraveling)
			{
				troopRoster.RemoveTroop(playerAlleyData.AssignedClanMember.CharacterObject, 1, default(UniqueTroopDescriptor), 0);
			}
			troopRoster.AddToCounts(CharacterObject.PlayerCharacter, 1, true, 0, 0, true, -1);
			TroopRoster troopsOfAlleyForBattleMission = Campaign.Current.Models.AlleyModel.GetTroopsOfAlleyForBattleMission(playerAlleyData.UnderAttackBy);
			int wallLevel = Settlement.CurrentSettlement.Town.GetWallLevel();
			string scene = Settlement.CurrentSettlement.LocationComplex.GetScene("center", wallLevel);
			Location locationWithId = LocationComplex.Current.GetLocationWithId("center");
			CampaignMission.OpenAlleyFightMission(scene, wallLevel, locationWithId, troopRoster, troopsOfAlleyForBattleMission);
			this._battleWillStartInCurrentSettlement = false;
			this._waitForBattleResults = true;
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0002E19C File Offset: 0x0002C39C
		protected void AddDialogs(CampaignGameStarter campaignGameStarter)
		{
			campaignGameStarter.AddDialogLine("alley_talk_start_player_owned_thug", "start", "alley_player_owned_start_thug", "{=!}{FURTHER_DETAIL}", new ConversationSentence.OnConditionDelegate(this.alley_talk_player_owned_thug_on_condition), null, 120, null);
			campaignGameStarter.AddPlayerLine("alley_talk_start_player_owned_thug_answer", "alley_player_owned_start_thug", "close_window", "{=GvpvZEba}Very well, take care.", null, null, 120, null, null);
			campaignGameStarter.AddDialogLine("alley_talk_start_player_owned_alley_manager_not_under_attack", "start", "alley_player_owned_start", "{=cwqR0pp1}Greetings my {?PLAYER.GENDER}lady{?}lord{\\?}. It's good to see you here. How can I help you?", new ConversationSentence.OnConditionDelegate(this.alley_talk_player_owned_alley_managed_not_under_attack_on_condition), null, 120, null);
			campaignGameStarter.AddDialogLine("alley_talk_start_player_owned_alley_manager_under_attack", "start", "close_window", "{=jaFWM6sN}Good to have you here, my {?PLAYER.GENDER}lady{?}lord{\\?}. We shall raze them down now.", new ConversationSentence.OnConditionDelegate(this.alley_talk_player_owned_alley_managed_common_condition), null, 120, null);
			campaignGameStarter.AddPlayerLine("alley_talk_start_player_owned_alley_manager_answer_1", "alley_player_owned_start", "alley_manager_general_answer", "{=xJJeXW6j}Let me inspect your troops.", null, new ConversationSentence.OnConsequenceDelegate(this.manage_troops_of_alley_from_dialog), 100, null, null);
			campaignGameStarter.AddPlayerLine("alley_talk_start_player_owned_alley_manager_answer_2", "alley_player_owned_start", "player_asked_for_volunteers", "{=ah3WKIc8}I could use some more troops in my party. Have you found any volunteers?", null, null, 100, null, null);
			campaignGameStarter.AddPlayerLine("alley_talk_start_player_owned_alley_manager_answer_3", "alley_player_owned_start", "alley_manager_general_answer", "{=ut26sd6p}I want somebody else to take charge of this place.", null, new ConversationSentence.OnConsequenceDelegate(this.change_leader_of_alley_from_dialog), 100, null, null);
			campaignGameStarter.AddPlayerLine("alley_talk_start_player_owned_alley_manager_answer_4", "alley_player_owned_start", "abandon_alley_are_you_sure", "{=I8o7oarw}I want to abandon this area.", null, null, 100, null, null);
			campaignGameStarter.AddDialogLine("alley_talk_start_player_owned_alley_manager_answer_4_1", "abandon_alley_are_you_sure", "abandon_alley_are_you_sure_player", "{=6dDXb4iI}Are you sure? If you are, we can pack up and join you.", null, null, 100, null);
			campaignGameStarter.AddPlayerLine("alley_talk_start_player_owned_alley_manager_answer_4_2", "abandon_alley_are_you_sure_player", "start", "{=ALWqXMiP}Yes, I am sure.", null, new ConversationSentence.OnConsequenceDelegate(this.abandon_alley_from_dialog_consequence), 100, null, null);
			campaignGameStarter.AddPlayerLine("alley_talk_start_player_owned_alley_manager_answer_4_3", "abandon_alley_are_you_sure_player", "start", "{=YJkiQ6nM}No, I have changed my mind. We will stay here.", null, null, 100, null, null);
			campaignGameStarter.AddPlayerLine("alley_talk_start_player_owned_alley_manager_answer_5", "alley_player_owned_start", "close_window", "{=D33fIGQe}Never mind.", null, null, 100, null, null);
			campaignGameStarter.AddDialogLine("alley_talk_start_player_owned_alley_manager_volunteers_1", "player_asked_for_volunteers", "alley_player_owned_start", "{=nRVrXSbv}Not yet my {?PLAYER.GENDER}lady{?}lord{\\?}, but I am working on it. Better check back next week.", new ConversationSentence.OnConditionDelegate(this.alley_has_no_troops_to_recruit), null, 100, null);
			campaignGameStarter.AddDialogLine("alley_talk_start_player_owned_alley_manager_volunteers_2", "player_asked_for_volunteers", "alley_player_ask_for_troops", "{=aLrK7Si7}Yes. I have {TROOPS_TO_RECRUIT} ready to join you.", new ConversationSentence.OnConditionDelegate(this.get_troops_to_recruit_from_alley), null, 100, null);
			campaignGameStarter.AddPlayerLine("alley_talk_start_player_owned_alley_manager_volunteers_3", "alley_player_ask_for_troops", "give_troops_to_player", "{=BNz4ZA6S}Very well. Have them join me now.", null, new ConversationSentence.OnConsequenceDelegate(this.player_recruited_troops_from_alley), 100, null, null);
			campaignGameStarter.AddDialogLine("alley_talk_start_player_owned_alley_manager_volunteers_4", "give_troops_to_player", "start", "{=PlIYRSIz}All right my {?PLAYER.GENDER}lady{?}lord{\\?}, they will be ready.", null, null, 100, null);
			campaignGameStarter.AddPlayerLine("alley_talk_start_player_owned_alley_manager_volunteers_5", "alley_player_ask_for_troops", "start", "{=n1qrbQVa}I don't need them right now.", null, null, 100, null, null);
			campaignGameStarter.AddDialogLine("alley_talk_start_player_owned_alley_manager_answer_2_di", "alley_manager_general_answer", "start", "{=lF5HkBDy}As you wish.", null, null, 100, null);
			campaignGameStarter.AddDialogLine("alley_talk_start_normal", "start", "alley_talk_start", "{=qT4nbaAY}Oi, you, what are you doing here?", new ConversationSentence.OnConditionDelegate(this.alley_talk_start_normal_on_condition), null, 120, null);
			campaignGameStarter.AddDialogLine("alley_talk_start_normal_2", "start", "alley_talk_start_confront", "{=MzHbdTYe}Well well well, I wasn't expecting to see you there. There must be some little birds informing you about my plans. That won't change anything, though. I'll still crush you.", new ConversationSentence.OnConditionDelegate(this.alley_confront_dialog_on_condition), null, 100, null);
			campaignGameStarter.AddPlayerLine("alley_talk_start_normal_3", "alley_talk_start_confront", "close_window", "{=GMsZZQzI}Bring it on.", null, new ConversationSentence.OnConsequenceDelegate(this.start_alley_fight_after_conversation), 100, null, null);
			campaignGameStarter.AddPlayerLine("alley_talk_start_normal_4", "alley_talk_start_confront", "close_window", "{=QNpuyzc4}Take it easy. I have no interest in the place any more. Take it.", null, new ConversationSentence.OnConsequenceDelegate(this.abandon_alley_from_dialog_consequence), 100, new ConversationSentence.OnClickableConditionDelegate(this.alley_abandon_while_under_attack_clickable_condition), null);
			campaignGameStarter.AddPlayerLine("alley_start_1", "alley_talk_start", "alley_activity", "{=1NSRPYZt}Just passing through. What goes on here?", null, null, 100, null, null);
			campaignGameStarter.AddPlayerLine("alley_start_2", "alley_talk_start", "first_entry_to_alley_2", "{=HCmQmZbe}I'm just having a look. Do you mind?", null, null, 100, null, null);
			campaignGameStarter.AddPlayerLine("alley_start_3", "alley_talk_start", "close_window", "{=iW9iKbb8}Nothing.", null, null, 100, null, null);
			campaignGameStarter.AddPlayerLine("alley_entry_start_1", "alley_first_talk_start", "first_entry_to_alley_2", "{=X18yfvX7}Just passing through.", null, null, 100, null, null);
			campaignGameStarter.AddPlayerLine("alley_entry_start_2", "alley_first_talk_start", "first_entry_to_alley_2", "{=Y1O5bPpJ}Having a look. Do you mind?", null, null, 100, null, null);
			campaignGameStarter.AddPlayerLine("alley_entry_start_3", "alley_first_talk_start", "first_entry_to_alley_2", "{=eQfL2UmE}None of your business.", null, null, 100, null, null);
			campaignGameStarter.AddDialogLine("first_entry_to_alley", "first_entry_to_alley_2", "alley_options", "{=Ll2wN2Gm}This is how it goes, friend. This is our turf. We answer to {ALLEY_BOSS.NAME}, and {?ALLEY_BOSS.GENDER}she's{?}he's{\\?} like the {?ALLEY_BOSS.GENDER}queen{?}king{\\?} here. So if you haven't got a good reason to be here, clear out.", new ConversationSentence.OnConditionDelegate(this.enter_alley_rude_on_occasion), null, 100, null);
			campaignGameStarter.AddDialogLine("first_entry_to_alley_friendly", "first_entry_to_alley_2", "alley_options", "{=Fo47BuSY}Fine, you know {ALLEY_BOSS.NAME}, you can be here. Just no trouble, you understand?", null, null, 100, null);
			campaignGameStarter.AddPlayerLine("ally_entry_start_fight", "alley_options", "alley_fight_start", "{=2Fxva3RY}I don't take orders from the likes of you.", null, new ConversationSentence.OnConsequenceDelegate(this.start_alley_fight_on_consequence), 100, null, null);
			campaignGameStarter.AddPlayerLine("alley_entry_question_activity", "alley_options", "alley_activity", "{=aNZKqAAS}So what goes on here?", null, null, 100, null, null);
			campaignGameStarter.AddPlayerLine("alley_entry_end_conversation", "alley_options", "close_window", "{=Mk3Qfb4D}I don't want any trouble. Later.", null, null, 100, null, null);
			campaignGameStarter.AddDialogLine("alley_fight_start", "alley_fight_start", "close_window", "{=EN3Zqyx5}A mouthy one, eh? At him, lads![ib:aggressive][if:convo_angry]", null, null, 100, null);
			campaignGameStarter.AddDialogLine("alley_activity", "alley_activity", "alley_activity_2", "{=bZ5rXBY5}{ALLEY_ACTIVITY_STRING}", new ConversationSentence.OnConditionDelegate(this.alley_activity_on_condition), null, 100, null);
			campaignGameStarter.AddDialogLine("alley_activity_2", "alley_activity_2", "alley_options_player", "{=eZq11NVD}And by the way, we take orders from {ALLEY_BOSS.NAME}, and no one else.", new ConversationSentence.OnConditionDelegate(this.alley_activity_2_on_condition), null, 100, null);
			campaignGameStarter.AddDialogLine("alley_activity_back", "alley_options_decline", "alley_options", "{=pf4EIcBQ}Anything else? Because unless you've got business here, maybe you'd best move along.[if:convo_confused_annoyed][ib:closed]", null, null, 100, null);
			campaignGameStarter.AddDialogLine("alley_activity_end", "alley_options_player", "close_window", "{=xb1Ps6ZC}Now get lost...", null, null, 100, null);
			campaignGameStarter.AddDialogLine("alley_meet_boss", "alley_meet_boss", "close_window", "{=NoxFbtEa}Wait here. We'll see if {?ALLEY_BOSS.GENDER}she{?}he{\\?} wants to talk to you. (NOT IMPLEMENTED)", null, null, 100, null);
			campaignGameStarter.AddDialogLine("gang_leader_bodyguard_start", "start", "close_window", "{=NVvfxdIc}You best talk to the boss.", new ConversationSentence.OnConditionDelegate(this.gang_leader_bodyguard_on_condition), null, 200, null);
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0002E792 File Offset: 0x0002C992
		private bool alley_abandon_while_under_attack_clickable_condition(out TextObject explanation)
		{
			explanation = new TextObject("{=3E1XVyGM}You will lose the ownership of the alley.", null);
			return true;
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0002E7A4 File Offset: 0x0002C9A4
		private bool alley_confront_dialog_on_condition()
		{
			AlleyCampaignBehavior.PlayerAlleyData playerAlleyData = this._playerOwnedCommonAreaData.FirstOrDefault((AlleyCampaignBehavior.PlayerAlleyData x) => x.Alley.Settlement == Settlement.CurrentSettlement);
			return playerAlleyData != null && playerAlleyData.IsUnderAttack && playerAlleyData.UnderAttackBy.Owner == Hero.OneToOneConversationHero;
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0002E7FB File Offset: 0x0002C9FB
		private void start_alley_fight_after_conversation()
		{
			this._battleWillStartInCurrentSettlement = true;
			Campaign.Current.GameMenuManager.SetNextMenu("manage_alley");
			if (Mission.Current != null)
			{
				Mission.Current.EndMission();
			}
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0002E82C File Offset: 0x0002CA2C
		private void player_recruited_troops_from_alley()
		{
			AlleyCampaignBehavior.PlayerAlleyData playerAlleyData = this._playerOwnedCommonAreaData.First((AlleyCampaignBehavior.PlayerAlleyData x) => x.Alley.Settlement == Settlement.CurrentSettlement);
			foreach (TroopRosterElement troopRosterElement in Campaign.Current.Models.AlleyModel.GetTroopsToRecruitFromAlleyDependingOnAlleyRandom(playerAlleyData.Alley, playerAlleyData.RandomFloatWeekly).GetTroopRoster())
			{
				MobileParty.MainParty.MemberRoster.AddToCounts(troopRosterElement.Character, troopRosterElement.Number, false, 0, 0, true, -1);
			}
			MBInformationManager.AddQuickInformation(new TextObject("{=8CW2y0p3}Troops have been joined to your party", null), 0, null, "");
			playerAlleyData.LastRecruitTime = CampaignTime.Now;
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0002E908 File Offset: 0x0002CB08
		private bool get_troops_to_recruit_from_alley()
		{
			AlleyCampaignBehavior.PlayerAlleyData playerAlleyData = this._playerOwnedCommonAreaData.First((AlleyCampaignBehavior.PlayerAlleyData x) => x.Alley.Settlement == Settlement.CurrentSettlement);
			TroopRoster troopsToRecruitFromAlleyDependingOnAlleyRandom = Campaign.Current.Models.AlleyModel.GetTroopsToRecruitFromAlleyDependingOnAlleyRandom(playerAlleyData.Alley, playerAlleyData.RandomFloatWeekly);
			List<TextObject> list = new List<TextObject>();
			foreach (TroopRosterElement troopRosterElement in troopsToRecruitFromAlleyDependingOnAlleyRandom.GetTroopRoster())
			{
				TextObject textObject = new TextObject("{=!}{TROOP_COUNT} {?TROOP_COUNT > 1}{TROOP_NAME}{.s}{?}{TROOP_NAME}{\\?}", null);
				textObject.SetTextVariable("TROOP_COUNT", troopRosterElement.Number);
				textObject.SetTextVariable("TROOP_NAME", troopRosterElement.Character.Name);
				list.Add(textObject);
			}
			TextObject text = GameTexts.GameTextHelper.MergeTextObjectsWithComma(list, true);
			MBTextManager.SetTextVariable("TROOPS_TO_RECRUIT", text, false);
			return true;
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0002E9FC File Offset: 0x0002CBFC
		private bool alley_has_no_troops_to_recruit()
		{
			return this._playerOwnedCommonAreaData.First((AlleyCampaignBehavior.PlayerAlleyData x) => x.Alley.Settlement == Settlement.CurrentSettlement).RandomFloatWeekly > 0.5f;
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x0002EA34 File Offset: 0x0002CC34
		private void change_leader_of_alley_from_dialog()
		{
			AlleyHelper.CreateMultiSelectionInquiryForSelectingClanMemberToAlley(this._playerOwnedCommonAreaData.First((AlleyCampaignBehavior.PlayerAlleyData x) => x.Alley.Settlement == Settlement.CurrentSettlement).Alley, new Action<List<InquiryElement>>(this.ChangeAssignedClanMemberOfAlley), null);
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x0002EA84 File Offset: 0x0002CC84
		private void manage_troops_of_alley_from_dialog()
		{
			AlleyHelper.OpenScreenForManagingAlley(this._playerOwnedCommonAreaData.First((AlleyCampaignBehavior.PlayerAlleyData x) => x.Alley.Settlement == Settlement.CurrentSettlement).TroopRoster, new PartyPresentationDoneButtonDelegate(this.OnPartyScreenClosed), new TextObject("{=dQBArrqh}Manage Alley", null), null);
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x0002EAE0 File Offset: 0x0002CCE0
		private void abandon_alley_from_dialog_consequence()
		{
			AlleyCampaignBehavior.PlayerAlleyData playerAlleyData = this._playerOwnedCommonAreaData.First((AlleyCampaignBehavior.PlayerAlleyData x) => x.Alley.Settlement == Settlement.CurrentSettlement);
			if (Mission.Current != null)
			{
				for (int i = Mission.Current.Agents.Count - 1; i >= 0; i--)
				{
					Agent agent = Mission.Current.Agents[i];
					if (agent.IsHuman)
					{
						CampaignAgentComponent component = agent.GetComponent<CampaignAgentComponent>();
						Hero hero;
						if (component == null)
						{
							hero = null;
						}
						else
						{
							AgentNavigator agentNavigator = component.AgentNavigator;
							if (agentNavigator == null)
							{
								hero = null;
							}
							else
							{
								Alley memberOfAlley = agentNavigator.MemberOfAlley;
								hero = ((memberOfAlley != null) ? memberOfAlley.Owner : null);
							}
						}
						if (hero == Hero.MainHero && Hero.OneToOneConversationHero.CharacterObject != agent.Character)
						{
							agent.FadeOut(false, true);
						}
					}
				}
			}
			this._playerOwnedCommonAreaData.Remove(playerAlleyData);
			playerAlleyData.AbandonTheAlley(false);
			if (Mission.Current != null && playerAlleyData.Alley.Owner != null)
			{
				foreach (TroopRosterElement troopRosterElement in Campaign.Current.Models.AlleyModel.GetTroopsOfAIOwnedAlley(playerAlleyData.Alley).GetTroopRoster())
				{
					for (int j = 0; j < troopRosterElement.Number; j++)
					{
						this.AddCharacterToAlley(troopRosterElement.Character, playerAlleyData.Alley);
					}
				}
			}
			this._playerAbandonedAlleyFromDialogRecently = true;
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x0002EC54 File Offset: 0x0002CE54
		private bool alley_talk_player_owned_alley_managed_not_under_attack_on_condition()
		{
			AlleyCampaignBehavior.PlayerAlleyData playerAlleyData = this._playerOwnedCommonAreaData.FirstOrDefault((AlleyCampaignBehavior.PlayerAlleyData x) => x.Alley.Settlement == Settlement.CurrentSettlement);
			return playerAlleyData != null && !playerAlleyData.IsUnderAttack && this.alley_talk_player_owned_alley_managed_common_condition();
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x0002ECA0 File Offset: 0x0002CEA0
		private bool alley_talk_player_owned_alley_managed_common_condition()
		{
			AlleyCampaignBehavior.PlayerAlleyData playerAlleyData = this._playerOwnedCommonAreaData.FirstOrDefault((AlleyCampaignBehavior.PlayerAlleyData x) => x.Alley.Settlement == Settlement.CurrentSettlement);
			return playerAlleyData != null && playerAlleyData.AssignedClanMember == Hero.OneToOneConversationHero;
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x0002ECEC File Offset: 0x0002CEEC
		private bool alley_talk_player_owned_thug_on_condition()
		{
			if (!CharacterObject.OneToOneConversationCharacter.IsHero)
			{
				AlleyCampaignBehavior.PlayerAlleyData playerAlleyData = this._playerOwnedCommonAreaData.FirstOrDefault((AlleyCampaignBehavior.PlayerAlleyData x) => x.Alley.Settlement == Settlement.CurrentSettlement);
				if (playerAlleyData != null)
				{
					CampaignAgentComponent component = ((Agent)Campaign.Current.ConversationManager.OneToOneConversationAgent).GetComponent<CampaignAgentComponent>();
					if (component != null && component.AgentNavigator.MemberOfAlley == playerAlleyData.Alley)
					{
						if (playerAlleyData.IsAssignedClanMemberDead)
						{
							TextObject text = new TextObject("{=SdKTUIVJ}Oi, my {?PLAYER.GENDER}lady{?}lord{\\?}. Sorry for your loss, {DEAD_ALLEY_LEADER.NAME} will be missed in these streets. We are waiting for you to appoint a new boss, whenever you’re ready.", null);
							StringHelpers.SetCharacterProperties("DEAD_ALLEY_LEADER", playerAlleyData.AssignedClanMember.CharacterObject, null, false);
							MBTextManager.SetTextVariable("FURTHER_DETAIL", text, false);
						}
						else if (playerAlleyData.AssignedClanMember.IsTraveling)
						{
							TextObject text2 = new TextObject("{=KKvOQAVa}We are waiting for {TRAVELING_ALLEY_LEADER.NAME} to come. Until {?TRAVELING_ALLEY_LEADER.GENDER}she{?}he{\\?} arrives, we'll be extra watchful.", null);
							StringHelpers.SetCharacterProperties("TRAVELING_ALLEY_LEADER", playerAlleyData.AssignedClanMember.CharacterObject, null, false);
							MBTextManager.SetTextVariable("FURTHER_DETAIL", text2, false);
						}
						else
						{
							TextObject text3 = new TextObject("{=OPwO5RXC}Welcome, boss. We're honored to have you here. You can be sure we're keeping an eye on everything going on.", null);
							MBTextManager.SetTextVariable("FURTHER_DETAIL", text3, false);
						}
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x0002EE08 File Offset: 0x0002D008
		private bool alley_activity_on_condition()
		{
			List<TextObject> list = new List<TextObject>();
			Alley lastVisitedAlley = CampaignMission.Current.LastVisitedAlley;
			if (lastVisitedAlley.Owner.GetTraitLevel(DefaultTraits.Thug) > 0)
			{
				list.Add(new TextObject("{=prJBRboS}we look after the honest folk here. Make sure no one smashes up their shops. And if they want to give us a coin or two as a way of saying thanks, well, who'd mind?", null));
			}
			if (lastVisitedAlley.Owner.GetTraitLevel(DefaultTraits.Smuggler) > 0)
			{
				list.Add(new TextObject("{=CqnAGehj}suppose someone wanted to buy some goods and didn't want to pay the customs tax. We might be able to help that person out.", null));
			}
			if (lastVisitedAlley.Owner.Gold > 100)
			{
				list.Add(new TextObject("{=U8iyCXmF}we help out those who are down on their luck. Give 'em a purse of silver to tide them by. With a bit of speculative interest, naturally.", null));
			}
			MBTextManager.SetTextVariable("ALLEY_ACTIVITY_STRING", "{=1rCk6xRa}Now then... If you're asking,[if:convo_normal][ib:normal]", false);
			for (int i = 0; i < list.Count; i++)
			{
				MBTextManager.SetTextVariable("ALLEY_ACTIVITY_ADDITION", list[i].ToString(), false);
				MBTextManager.SetTextVariable("ALLEY_ACTIVITY_STRING", new TextObject("{=jVjIkODa}{ALLEY_ACTIVITY_STRING} {ALLEY_ACTIVITY_ADDITION}", null).ToString(), false);
				if (i + 1 != list.Count)
				{
					MBTextManager.SetTextVariable("ALLEY_ACTIVITY_ADDITION", "{=lbNl0a8t}Also,", false);
					MBTextManager.SetTextVariable("ALLEY_ACTIVITY_STRING", new TextObject("{=jVjIkODa}{ALLEY_ACTIVITY_STRING} {ALLEY_ACTIVITY_ADDITION}", null).ToString(), false);
				}
			}
			return true;
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x0002EF16 File Offset: 0x0002D116
		private bool alley_activity_2_on_condition()
		{
			StringHelpers.SetCharacterProperties("ALLEY_BOSS", CampaignMission.Current.LastVisitedAlley.Owner.CharacterObject, null, false);
			return true;
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x0002EF3C File Offset: 0x0002D13C
		private bool alley_talk_start_normal_on_condition()
		{
			Agent oneToOneConversationAgent = ConversationMission.OneToOneConversationAgent;
			AgentNavigator agentNavigator = (oneToOneConversationAgent != null) ? oneToOneConversationAgent.GetComponent<CampaignAgentComponent>().AgentNavigator : null;
			if (((agentNavigator != null) ? agentNavigator.MemberOfAlley : null) != null && agentNavigator.MemberOfAlley.State == Alley.AreaState.OccupiedByGangLeader && agentNavigator.MemberOfAlley.Owner != Hero.MainHero)
			{
				CampaignMission.Current.LastVisitedAlley = agentNavigator.MemberOfAlley;
				return true;
			}
			return false;
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x0002EFA4 File Offset: 0x0002D1A4
		private bool enter_alley_rude_on_occasion()
		{
			Agent oneToOneConversationAgent = ConversationMission.OneToOneConversationAgent;
			Hero owner = ((oneToOneConversationAgent != null) ? oneToOneConversationAgent.GetComponent<CampaignAgentComponent>().AgentNavigator.MemberOfAlley : null).Owner;
			float relationWithPlayer = owner.GetRelationWithPlayer();
			StringHelpers.SetCharacterProperties("ALLEY_BOSS", owner.CharacterObject, null, false);
			return !owner.HasMet || relationWithPlayer < -5f;
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x0002F000 File Offset: 0x0002D200
		private void start_alley_fight_on_consequence()
		{
			this._playerIsInAlleyFightMission = true;
			Campaign.Current.ConversationManager.ConversationEndOneShot += delegate()
			{
				Mission.Current.GetMissionBehavior<MissionAlleyHandler>().StartCommonAreaBattle(CampaignMission.Current.LastVisitedAlley);
			};
			LogEntry.AddLogEntry(new PlayerAttackAlleyLogEntry(CampaignMission.Current.LastVisitedAlley.Owner, Hero.MainHero.CurrentSettlement));
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x0002F065 File Offset: 0x0002D265
		private bool gang_leader_bodyguard_on_condition()
		{
			return Settlement.CurrentSettlement != null && CharacterObject.OneToOneConversationCharacter == Settlement.CurrentSettlement.Culture.GangleaderBodyguard;
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x0002F088 File Offset: 0x0002D288
		private void OnHeroKilled(Hero victim, Hero killer, KillCharacterAction.KillCharacterActionDetail detail, bool showNotification)
		{
			AlleyCampaignBehavior.PlayerAlleyData playerAlleyData = this._playerOwnedCommonAreaData.FirstOrDefault((AlleyCampaignBehavior.PlayerAlleyData x) => x.AssignedClanMember == victim);
			if (playerAlleyData != null)
			{
				playerAlleyData.TroopRoster.RemoveTroop(victim.CharacterObject, 1, default(UniqueTroopDescriptor), 0);
				Campaign.Current.CampaignInformationManager.NewMapNoticeAdded(new AlleyLeaderDiedMapNotification(playerAlleyData.Alley, new TextObject("{=EAPYyktd}One of your alleys has lost its leader or is lacking troops", null)));
			}
			foreach (AlleyCampaignBehavior.PlayerAlleyData playerAlleyData2 in this._playerOwnedCommonAreaData)
			{
				if (playerAlleyData2.IsUnderAttack && playerAlleyData2.UnderAttackBy.Owner == victim)
				{
					playerAlleyData2.UnderAttackBy = null;
				}
			}
			if (victim.Clan != Clan.PlayerClan)
			{
				foreach (Alley alley in victim.OwnedAlleys.ToList<Alley>())
				{
					alley.SetOwner(null);
				}
			}
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x0002F1C8 File Offset: 0x0002D3C8
		public bool GetIsAlleyUnderAttack(Alley alley)
		{
			AlleyCampaignBehavior.PlayerAlleyData playerAlleyData = this._playerOwnedCommonAreaData.FirstOrDefault((AlleyCampaignBehavior.PlayerAlleyData x) => x.Alley == alley);
			return playerAlleyData != null && playerAlleyData.IsUnderAttack;
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x0002F208 File Offset: 0x0002D408
		public int GetPlayerOwnedAlleyTroopCount(Alley alley)
		{
			return this._playerOwnedCommonAreaData.FirstOrDefault((AlleyCampaignBehavior.PlayerAlleyData x) => x.Alley == alley).TroopRoster.TotalRegulars;
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x0002F244 File Offset: 0x0002D444
		public int GetResponseTimeLeftForAttackInDays(Alley alley)
		{
			return (int)this._playerOwnedCommonAreaData.First((AlleyCampaignBehavior.PlayerAlleyData x) => x.Alley == alley).AttackResponseDueDate.RemainingDaysFromNow;
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x0002F280 File Offset: 0x0002D480
		public void AbandonAlleyFromClanMenu(Alley alley)
		{
			AlleyCampaignBehavior.PlayerAlleyData playerAlleyData = this._playerOwnedCommonAreaData.FirstOrDefault((AlleyCampaignBehavior.PlayerAlleyData x) => x.Alley == alley);
			this._playerOwnedCommonAreaData.Remove(playerAlleyData);
			if (playerAlleyData != null)
			{
				playerAlleyData.AbandonTheAlley(true);
			}
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x0002F2CC File Offset: 0x0002D4CC
		public bool IsHeroAlleyLeaderOfAnyPlayerAlley(Hero hero)
		{
			return this._playerOwnedCommonAreaData.Any((AlleyCampaignBehavior.PlayerAlleyData x) => x.AssignedClanMember == hero);
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x0002F2FD File Offset: 0x0002D4FD
		public List<Hero> GetAllAssignedClanMembersForOwnedAlleys()
		{
			return (from x in this._playerOwnedCommonAreaData
			select x.AssignedClanMember).ToList<Hero>();
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x0002F330 File Offset: 0x0002D530
		public void ChangeAlleyMember(Alley alley, Hero newAlleyLead)
		{
			AlleyCampaignBehavior.PlayerAlleyData alleyData = this._playerOwnedCommonAreaData.FirstOrDefault((AlleyCampaignBehavior.PlayerAlleyData x) => x.Alley == alley);
			this.ChangeTheLeaderOfAlleyInternal(alleyData, newAlleyLead);
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x0002F36A File Offset: 0x0002D56A
		public void OnPlayerRetreatedFromMission()
		{
			this._playerRetreatedFromMission = true;
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x0002F373 File Offset: 0x0002D573
		public void OnPlayerDiedInMission()
		{
			this._playerDiedInMission = true;
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x0002F37C File Offset: 0x0002D57C
		public Hero GetAssignedClanMemberOfAlley(Alley alley)
		{
			AlleyCampaignBehavior.PlayerAlleyData playerAlleyData = this._playerOwnedCommonAreaData.FirstOrDefault((AlleyCampaignBehavior.PlayerAlleyData x) => x.Alley == alley);
			if (playerAlleyData != null)
			{
				return playerAlleyData.AssignedClanMember;
			}
			return null;
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0002F3BC File Offset: 0x0002D5BC
		private void ChangeTheLeaderOfAlleyInternal(AlleyCampaignBehavior.PlayerAlleyData alleyData, Hero newLeader)
		{
			Hero assignedClanMember = alleyData.AssignedClanMember;
			alleyData.AssignedClanMember = newLeader;
			if (!assignedClanMember.IsDead)
			{
				alleyData.TroopRoster.RemoveTroop(assignedClanMember.CharacterObject, 1, default(UniqueTroopDescriptor), 0);
			}
			alleyData.TroopRoster.AddToCounts(newLeader.CharacterObject, 1, true, 0, 0, true, -1);
			TeleportHeroAction.ApplyDelayedTeleportToSettlement(newLeader, alleyData.Alley.Settlement);
			if (Campaign.Current.CurrentMenuContext != null)
			{
				Campaign.Current.CurrentMenuContext.Refresh();
			}
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0002F440 File Offset: 0x0002D640
		[GameMenuInitializationHandler("manage_alley")]
		[GameMenuInitializationHandler("alley_fight_lost")]
		[GameMenuInitializationHandler("alley_fight_won")]
		[GameMenuInitializationHandler("manage_alley_abandon_are_you_sure")]
		public static void alley_related_menu_on_init(MenuCallbackArgs args)
		{
			string backgroundMeshName = Settlement.CurrentSettlement.Culture.StringId + "_alley";
			args.MenuContext.SetBackgroundMeshName(backgroundMeshName);
		}

		// Token: 0x040002C0 RID: 704
		private const int DesiredOccupiedAlleyPerTownFrequency = 2;

		// Token: 0x040002C1 RID: 705
		private const int RelationLossWithSettlementOwnerAfterOccupyingAnAlley = -2;

		// Token: 0x040002C2 RID: 706
		private const int RelationLossWithOldOwnerUponClearingAlley = -5;

		// Token: 0x040002C3 RID: 707
		private const int RelationGainWithOtherNotablesUponClearingAlley = 1;

		// Token: 0x040002C4 RID: 708
		private const float SpawningNewAlleyFightDailyPercentage = 0.015f;

		// Token: 0x040002C5 RID: 709
		private const float ConvertTroopsToThugsDailyPercentage = 0.01f;

		// Token: 0x040002C6 RID: 710
		private const float GainOrLoseAlleyDailyBasePercentage = 0.02f;

		// Token: 0x040002C7 RID: 711
		private CharacterObject _thug;

		// Token: 0x040002C8 RID: 712
		private CharacterObject _expertThug;

		// Token: 0x040002C9 RID: 713
		private CharacterObject _masterThug;

		// Token: 0x040002CA RID: 714
		private List<AlleyCampaignBehavior.PlayerAlleyData> _playerOwnedCommonAreaData = new List<AlleyCampaignBehavior.PlayerAlleyData>();

		// Token: 0x040002CB RID: 715
		private bool _battleWillStartInCurrentSettlement;

		// Token: 0x040002CC RID: 716
		private bool _waitForBattleResults;

		// Token: 0x040002CD RID: 717
		private bool _playerRetreatedFromMission;

		// Token: 0x040002CE RID: 718
		private bool _playerDiedInMission;

		// Token: 0x040002CF RID: 719
		private bool _playerIsInAlleyFightMission;

		// Token: 0x040002D0 RID: 720
		private bool _playerAbandonedAlleyFromDialogRecently;

		// Token: 0x0200017C RID: 380
		public class AlleyCampaignBehaviorTypeDefiner : SaveableTypeDefiner
		{
			// Token: 0x06001037 RID: 4151 RVA: 0x00063608 File Offset: 0x00061808
			public AlleyCampaignBehaviorTypeDefiner() : base(515253)
			{
			}

			// Token: 0x06001038 RID: 4152 RVA: 0x00063615 File Offset: 0x00061815
			protected override void DefineClassTypes()
			{
				base.AddClassDefinition(typeof(AlleyCampaignBehavior.PlayerAlleyData), 1, null);
			}

			// Token: 0x06001039 RID: 4153 RVA: 0x00063629 File Offset: 0x00061829
			protected override void DefineContainerDefinitions()
			{
				base.ConstructContainerDefinition(typeof(List<AlleyCampaignBehavior.PlayerAlleyData>));
			}
		}

		// Token: 0x0200017D RID: 381
		internal class PlayerAlleyData
		{
			// Token: 0x170001F2 RID: 498
			// (get) Token: 0x0600103A RID: 4154 RVA: 0x0006363C File Offset: 0x0006183C
			internal float RandomFloatWeekly
			{
				get
				{
					if (this.LastRecruitTime.ElapsedDaysUntilNow <= 7f)
					{
						return 2f;
					}
					return MBRandom.RandomFloatWithSeed((uint)CampaignTime.Now.ToWeeks, (uint)this.Alley.Tag.GetHashCode());
				}
			}

			// Token: 0x170001F3 RID: 499
			// (get) Token: 0x0600103B RID: 4155 RVA: 0x00063684 File Offset: 0x00061884
			internal bool IsUnderAttack
			{
				get
				{
					return this.UnderAttackBy != null;
				}
			}

			// Token: 0x170001F4 RID: 500
			// (get) Token: 0x0600103C RID: 4156 RVA: 0x0006368F File Offset: 0x0006188F
			internal bool IsAssignedClanMemberDead
			{
				get
				{
					return this.AssignedClanMember.IsDead;
				}
			}

			// Token: 0x0600103D RID: 4157 RVA: 0x0006369C File Offset: 0x0006189C
			internal PlayerAlleyData(Alley alley, TroopRoster roster)
			{
				this.Alley = alley;
				this.TroopRoster = roster;
				this.AssignedClanMember = roster.GetTroopRoster().First((TroopRosterElement c) => c.Character.IsHero).Character.HeroObject;
				this.UnderAttackBy = null;
			}

			// Token: 0x0600103E RID: 4158 RVA: 0x00063700 File Offset: 0x00061900
			internal void AlleyFightWon()
			{
				this.UnderAttackBy.Owner.AddPower(-(this.UnderAttackBy.Owner.Power * 0.2f));
				this.UnderAttackBy.SetOwner(null);
				this.UnderAttackBy = null;
				if (!this.TroopRoster.Contains(this.AssignedClanMember.CharacterObject))
				{
					this.TroopRoster.AddToCounts(this.AssignedClanMember.CharacterObject, 1, true, 0, 0, true, -1);
				}
				Hero.MainHero.AddSkillXp(DefaultSkills.Roguery, Campaign.Current.Models.AlleyModel.GetXpGainAfterSuccessfulAlleyDefenseForMainHero());
				GameMenu.SwitchToMenu("alley_fight_won");
			}

			// Token: 0x0600103F RID: 4159 RVA: 0x000637A9 File Offset: 0x000619A9
			internal void AlleyFightLost()
			{
				this.DestroyAlley(false);
				Hero.MainHero.HitPoints = 1;
				GameMenu.SwitchToMenu("alley_fight_lost");
			}

			// Token: 0x06001040 RID: 4160 RVA: 0x000637C8 File Offset: 0x000619C8
			internal void AbandonTheAlley(bool fromClanScreen = false)
			{
				if (!fromClanScreen)
				{
					foreach (TroopRosterElement troopRosterElement in this.TroopRoster.GetTroopRoster())
					{
						if (!troopRosterElement.Character.IsHero)
						{
							MobileParty.MainParty.MemberRoster.AddToCounts(troopRosterElement.Character, troopRosterElement.Number, false, 0, 0, true, -1);
						}
					}
				}
				this.DestroyAlley(true);
			}

			// Token: 0x06001041 RID: 4161 RVA: 0x00063854 File Offset: 0x00061A54
			internal void DestroyAlley(bool fromAbandoning = false)
			{
				if (!fromAbandoning && this.AssignedClanMember.IsAlive && this.AssignedClanMember.DeathMark == KillCharacterAction.KillCharacterActionDetail.None)
				{
					MakeHeroFugitiveAction.Apply(this.AssignedClanMember);
				}
				if (this.UnderAttackBy != null)
				{
					this.Alley.SetOwner(this.UnderAttackBy.Owner);
				}
				else
				{
					this.Alley.SetOwner(null);
				}
				this.TroopRoster.Clear();
				this.UnderAttackBy = null;
			}

			// Token: 0x06001042 RID: 4162 RVA: 0x000638C7 File Offset: 0x00061AC7
			internal static void AutoGeneratedStaticCollectObjectsPlayerAlleyData(object o, List<object> collectedObjects)
			{
				((AlleyCampaignBehavior.PlayerAlleyData)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
			}

			// Token: 0x06001043 RID: 4163 RVA: 0x000638D8 File Offset: 0x00061AD8
			protected virtual void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
			{
				collectedObjects.Add(this.Alley);
				collectedObjects.Add(this.AssignedClanMember);
				collectedObjects.Add(this.UnderAttackBy);
				collectedObjects.Add(this.TroopRoster);
				CampaignTime.AutoGeneratedStaticCollectObjectsCampaignTime(this.LastRecruitTime, collectedObjects);
				CampaignTime.AutoGeneratedStaticCollectObjectsCampaignTime(this.AttackResponseDueDate, collectedObjects);
			}

			// Token: 0x06001044 RID: 4164 RVA: 0x00063937 File Offset: 0x00061B37
			internal static object AutoGeneratedGetMemberValueAlley(object o)
			{
				return ((AlleyCampaignBehavior.PlayerAlleyData)o).Alley;
			}

			// Token: 0x06001045 RID: 4165 RVA: 0x00063944 File Offset: 0x00061B44
			internal static object AutoGeneratedGetMemberValueAssignedClanMember(object o)
			{
				return ((AlleyCampaignBehavior.PlayerAlleyData)o).AssignedClanMember;
			}

			// Token: 0x06001046 RID: 4166 RVA: 0x00063951 File Offset: 0x00061B51
			internal static object AutoGeneratedGetMemberValueUnderAttackBy(object o)
			{
				return ((AlleyCampaignBehavior.PlayerAlleyData)o).UnderAttackBy;
			}

			// Token: 0x06001047 RID: 4167 RVA: 0x0006395E File Offset: 0x00061B5E
			internal static object AutoGeneratedGetMemberValueTroopRoster(object o)
			{
				return ((AlleyCampaignBehavior.PlayerAlleyData)o).TroopRoster;
			}

			// Token: 0x06001048 RID: 4168 RVA: 0x0006396B File Offset: 0x00061B6B
			internal static object AutoGeneratedGetMemberValueLastRecruitTime(object o)
			{
				return ((AlleyCampaignBehavior.PlayerAlleyData)o).LastRecruitTime;
			}

			// Token: 0x06001049 RID: 4169 RVA: 0x0006397D File Offset: 0x00061B7D
			internal static object AutoGeneratedGetMemberValueAttackResponseDueDate(object o)
			{
				return ((AlleyCampaignBehavior.PlayerAlleyData)o).AttackResponseDueDate;
			}

			// Token: 0x040006AF RID: 1711
			[SaveableField(1)]
			internal readonly Alley Alley;

			// Token: 0x040006B0 RID: 1712
			[SaveableField(2)]
			internal Hero AssignedClanMember;

			// Token: 0x040006B1 RID: 1713
			[SaveableField(3)]
			internal Alley UnderAttackBy;

			// Token: 0x040006B2 RID: 1714
			[SaveableField(4)]
			internal TroopRoster TroopRoster;

			// Token: 0x040006B3 RID: 1715
			[SaveableField(5)]
			internal CampaignTime LastRecruitTime;

			// Token: 0x040006B4 RID: 1716
			[SaveableField(6)]
			internal CampaignTime AttackResponseDueDate;
		}
	}
}
