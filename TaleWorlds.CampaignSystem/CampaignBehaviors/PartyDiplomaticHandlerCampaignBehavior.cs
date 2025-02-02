using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Overlay;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Party.PartyComponents;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Siege;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x020003BA RID: 954
	public class PartyDiplomaticHandlerCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x06003A39 RID: 14905 RVA: 0x001124BC File Offset: 0x001106BC
		public override void RegisterEvents()
		{
			CampaignEvents.OnMapEventContinuityNeedsUpdateEvent.AddNonSerializedListener(this, new Action<IFaction>(this.OnMapEventContinuityNeedsUpdate));
			CampaignEvents.OnSettlementOwnerChangedEvent.AddNonSerializedListener(this, new Action<Settlement, bool, Hero, Hero, Hero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail>(this.OnSettlementOwnerChanged));
			CampaignEvents.OnClanChangedKingdomEvent.AddNonSerializedListener(this, new Action<Clan, Kingdom, Kingdom, ChangeKingdomAction.ChangeKingdomActionDetail, bool>(this.OnClanChangedKingdom));
			CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnSessionLaunched));
		}

		// Token: 0x06003A3A RID: 14906 RVA: 0x00112528 File Offset: 0x00110728
		private void OnSessionLaunched(CampaignGameStarter gameSystemInitializer)
		{
			gameSystemInitializer.AddGameMenu("hostile_action_end_by_peace", "{=1rbg3Hz2}The {FACTION_1} and {FACTION_2} are no longer enemies.", new OnInitDelegate(this.game_menu_hostile_action_end_by_peace_on_init), GameOverlays.MenuOverlayType.None, GameMenu.MenuFlags.None, null);
			gameSystemInitializer.AddGameMenuOption("hostile_action_end_by_peace", "hostile_action_en_by_peace_end", "{=WVkc4UgX}Continue.", delegate(MenuCallbackArgs args)
			{
				args.optionLeaveType = GameMenuOption.LeaveType.Leave;
				return true;
			}, delegate(MenuCallbackArgs args)
			{
				if (PlayerEncounter.Current != null)
				{
					PlayerEncounter.Finish(true);
					return;
				}
				GameMenu.ExitToLast();
			}, true, -1, false, null);
		}

		// Token: 0x06003A3B RID: 14907 RVA: 0x001125AB File Offset: 0x001107AB
		private void OnClanChangedKingdom(Clan clan, Kingdom oldKingdom, Kingdom newKingdom, ChangeKingdomAction.ChangeKingdomActionDetail detail, bool arg5)
		{
			if (newKingdom != null)
			{
				CampaignEventDispatcher.Instance.OnMapEventContinuityNeedsUpdate(newKingdom);
				return;
			}
			CampaignEventDispatcher.Instance.OnMapEventContinuityNeedsUpdate(clan);
		}

		// Token: 0x06003A3C RID: 14908 RVA: 0x001125C7 File Offset: 0x001107C7
		private void OnMapEventContinuityNeedsUpdate(IFaction faction)
		{
			this.CheckMapEvents(faction);
			this.CheckSiegeEvents(faction);
			this.CheckFactionPartiesAndSettlements(faction);
		}

		// Token: 0x06003A3D RID: 14909 RVA: 0x001125DE File Offset: 0x001107DE
		private void OnSettlementOwnerChanged(Settlement settlement, bool openToClaim, Hero hero1, Hero hero2, Hero hero3, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail detail)
		{
			if (detail != ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail.BySiege && settlement.SiegeEvent != null)
			{
				this.CheckSiegeEventContinuity(settlement.SiegeEvent);
			}
			this.CheckSettlementSuitabilityForParties(settlement.Parties);
		}

		// Token: 0x06003A3E RID: 14910 RVA: 0x00112608 File Offset: 0x00110808
		private void CheckFactionPartiesAndSettlements(IFaction faction)
		{
			this.CheckSettlementSuitabilityForParties(from x in faction.WarPartyComponents
			select x.MobileParty);
			foreach (Settlement settlement in faction.Settlements)
			{
				this.CheckSettlementSuitabilityForParties(settlement.Parties);
			}
		}

		// Token: 0x06003A3F RID: 14911 RVA: 0x00112690 File Offset: 0x00110890
		private void CheckMapEvents(IFaction faction)
		{
			MapEventManager mapEventManager = Campaign.Current.MapEventManager;
			List<MapEvent> list = (mapEventManager != null) ? mapEventManager.MapEvents : null;
			List<MapEvent> list2 = new List<MapEvent>();
			Func<PartyBase, bool> <>9__0;
			foreach (MapEvent mapEvent in list)
			{
				if (!mapEvent.IsFinalized)
				{
					IEnumerable<PartyBase> involvedParties = mapEvent.InvolvedParties;
					Func<PartyBase, bool> predicate;
					if ((predicate = <>9__0) == null)
					{
						predicate = (<>9__0 = ((PartyBase x) => x.MapFaction == faction));
					}
					if (involvedParties.Any(predicate))
					{
						list2.Add(mapEvent);
					}
				}
			}
			foreach (MapEvent mapEvent2 in list2)
			{
				List<MapEventParty> list3 = mapEvent2.AttackerSide.Parties.ToList<MapEventParty>();
				MapEventState state = mapEvent2.State;
				bool flag = false;
				foreach (MapEventParty mapEventParty in list3)
				{
					if (!mapEvent2.CanPartyJoinBattle(mapEventParty.Party, BattleSideEnum.Attacker))
					{
						flag = (flag || mapEvent2.IsPlayerMapEvent);
						if (mapEventParty.Party != PartyBase.MainParty)
						{
							mapEventParty.Party.MapEventSide = null;
						}
					}
				}
				if (state != MapEventState.WaitingRemoval && mapEvent2.State == MapEventState.WaitingRemoval)
				{
					mapEvent2.DiplomaticallyFinished = true;
				}
				if (flag)
				{
					if (PlayerEncounter.Current != null)
					{
						PlayerEncounter.Finish(true);
					}
					else
					{
						GameMenu.ExitToLast();
					}
				}
			}
		}

		// Token: 0x06003A40 RID: 14912 RVA: 0x00112838 File Offset: 0x00110A38
		private void CheckSiegeEvents(IFaction faction)
		{
			SiegeEventManager siegeEventManager = Campaign.Current.SiegeEventManager;
			List<SiegeEvent> list = (siegeEventManager != null) ? siegeEventManager.SiegeEvents : null;
			List<SiegeEvent> list2 = new List<SiegeEvent>();
			Func<PartyBase, bool> <>9__0;
			foreach (SiegeEvent siegeEvent in list)
			{
				if (!siegeEvent.ReadyToBeRemoved)
				{
					IEnumerable<PartyBase> involvedPartiesForEventType = siegeEvent.GetInvolvedPartiesForEventType(siegeEvent.GetCurrentBattleType());
					Func<PartyBase, bool> predicate;
					if ((predicate = <>9__0) == null)
					{
						predicate = (<>9__0 = ((PartyBase x) => x.MapFaction == faction));
					}
					if (involvedPartiesForEventType.Any(predicate))
					{
						list2.Add(siegeEvent);
					}
				}
			}
			foreach (SiegeEvent siegeEvent2 in list2)
			{
				this.CheckSiegeEventContinuity(siegeEvent2);
			}
		}

		// Token: 0x06003A41 RID: 14913 RVA: 0x0011292C File Offset: 0x00110B2C
		private void CheckSiegeEventContinuity(SiegeEvent siegeEvent)
		{
			List<PartyBase> list = siegeEvent.BesiegerCamp.GetInvolvedPartiesForEventType(siegeEvent.GetCurrentBattleType()).ToList<PartyBase>();
			bool flag = false;
			foreach (PartyBase partyBase in list)
			{
				if (!siegeEvent.CanPartyJoinSide(partyBase, BattleSideEnum.Attacker))
				{
					if (PlayerSiege.PlayerSiegeEvent == siegeEvent && PlayerSiege.PlayerSide == BattleSideEnum.Attacker)
					{
						flag = true;
					}
					else
					{
						partyBase.MobileParty.BesiegerCamp = null;
					}
				}
			}
			if (flag)
			{
				if (PlayerEncounter.Current != null)
				{
					PlayerEncounter.Finish(true);
					return;
				}
				GameMenu.ExitToLast();
			}
		}

		// Token: 0x06003A42 RID: 14914 RVA: 0x001129CC File Offset: 0x00110BCC
		private void CheckSettlementSuitabilityForParties(IEnumerable<MobileParty> parties)
		{
			foreach (MobileParty mobileParty in parties.ToList<MobileParty>())
			{
				if (mobileParty.CurrentSettlement != null && mobileParty.MapFaction.IsAtWarWith(mobileParty.CurrentSettlement.MapFaction))
				{
					if (mobileParty != MobileParty.MainParty)
					{
						if (mobileParty.Army == null || mobileParty.Army.LeaderParty == mobileParty)
						{
							if (mobileParty.Army != null && mobileParty.Army.Parties.Contains(MobileParty.MainParty))
							{
								GameMenu.SwitchToMenu("army_left_settlement_due_to_war_declaration");
							}
							else
							{
								Settlement currentSettlement = mobileParty.CurrentSettlement;
								LeaveSettlementAction.ApplyForParty(mobileParty);
								SetPartyAiAction.GetActionForPatrollingAroundSettlement(mobileParty, currentSettlement);
							}
						}
					}
					else if (mobileParty.CurrentSettlement.IsFortification)
					{
						GameMenu.SwitchToMenu("fortification_crime_rating");
					}
				}
			}
		}

		// Token: 0x06003A43 RID: 14915 RVA: 0x00112AB8 File Offset: 0x00110CB8
		[GameMenuInitializationHandler("hostile_action_end_by_peace")]
		public static void hostile_action_end_by_peace_on_init(MenuCallbackArgs args)
		{
			if (MobileParty.MainParty.BesiegedSettlement != null)
			{
				args.MenuContext.SetBackgroundMeshName(MobileParty.MainParty.BesiegedSettlement.SettlementComponent.WaitMeshName);
				return;
			}
			if (MobileParty.MainParty.LastVisitedSettlement != null)
			{
				args.MenuContext.SetBackgroundMeshName(MobileParty.MainParty.LastVisitedSettlement.SettlementComponent.WaitMeshName);
				return;
			}
			if (PlayerEncounter.EncounterSettlement != null)
			{
				args.MenuContext.SetBackgroundMeshName(PlayerEncounter.EncounterSettlement.SettlementComponent.WaitMeshName);
				return;
			}
			Debug.FailedAssert("no menu background to initialize!", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\CampaignBehaviors\\PartyDiplomaticHandlerCampaignBehavior.cs", "hostile_action_end_by_peace_on_init", 259);
		}

		// Token: 0x06003A44 RID: 14916 RVA: 0x00112B58 File Offset: 0x00110D58
		private void game_menu_hostile_action_end_by_peace_on_init(MenuCallbackArgs args)
		{
			if (this._lastFactionMadePeaceWithCausedPlayerToLeaveEvent == null)
			{
				StanceLink stanceLink = (from x in MobileParty.MainParty.MapFaction.Stances
				where !x.IsAtWar
				orderby x.PeaceDeclarationDate.ElapsedHoursUntilNow
				select x).FirstOrDefault<StanceLink>();
				this._lastFactionMadePeaceWithCausedPlayerToLeaveEvent = ((stanceLink.Faction1 != Clan.PlayerClan.MapFaction) ? stanceLink.Faction1 : stanceLink.Faction2);
			}
			GameTexts.SetVariable("FACTION_1", Clan.PlayerClan.MapFaction.EncyclopediaLinkWithName);
			GameTexts.SetVariable("FACTION_2", this._lastFactionMadePeaceWithCausedPlayerToLeaveEvent.EncyclopediaLinkWithName);
			if (PlayerEncounter.Battle != null)
			{
				PlayerEncounter.Battle.DiplomaticallyFinished = true;
			}
		}

		// Token: 0x06003A45 RID: 14917 RVA: 0x00112C33 File Offset: 0x00110E33
		public override void SyncData(IDataStore dataStore)
		{
			dataStore.SyncData<IFaction>("_lastFactionMadePeaceWithCausedPlayerToLeaveEvent", ref this._lastFactionMadePeaceWithCausedPlayerToLeaveEvent);
		}

		// Token: 0x040011B3 RID: 4531
		private IFaction _lastFactionMadePeaceWithCausedPlayerToLeaveEvent;
	}
}
