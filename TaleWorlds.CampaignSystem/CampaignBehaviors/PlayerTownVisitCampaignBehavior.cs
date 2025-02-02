using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.CampaignSystem.Inventory;
using TaleWorlds.CampaignSystem.Overlay;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x020003C3 RID: 963
	public class PlayerTownVisitCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x06003ACC RID: 15052 RVA: 0x00116390 File Offset: 0x00114590
		public override void RegisterEvents()
		{
			CampaignEvents.SettlementEntered.AddNonSerializedListener(this, new Action<MobileParty, Settlement, Hero>(this.OnSettlementEntered));
			CampaignEvents.OnSettlementLeftEvent.AddNonSerializedListener(this, new Action<MobileParty, Settlement>(this.OnSettlementLeft));
			CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.AddGameMenus));
		}

		// Token: 0x06003ACD RID: 15053 RVA: 0x001163E2 File Offset: 0x001145E2
		public override void SyncData(IDataStore dataStore)
		{
			dataStore.SyncData<CampaignTime>("_lastTimeRelationGivenPathfinder", ref this._lastTimeRelationGivenPathfinder);
			dataStore.SyncData<CampaignTime>("_lastTimeRelationGivenWaterDiviner", ref this._lastTimeRelationGivenWaterDiviner);
		}

		// Token: 0x06003ACE RID: 15054 RVA: 0x00116408 File Offset: 0x00114608
		protected void AddGameMenus(CampaignGameStarter campaignGameSystemStarter)
		{
			campaignGameSystemStarter.AddGameMenu("town", "{=!}{SETTLEMENT_INFO}", new OnInitDelegate(PlayerTownVisitCampaignBehavior.game_menu_town_on_init), GameOverlays.MenuOverlayType.SettlementWithBoth, GameMenu.MenuFlags.None, null);
			campaignGameSystemStarter.AddGameMenuOption("town", "town_streets", "{=R5ObSaUN}Take a walk around the town center", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.game_menu_town_town_streets_on_condition), new GameMenuOption.OnConsequenceDelegate(PlayerTownVisitCampaignBehavior.game_menu_town_town_streets_on_consequence), false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("town", "town_keep", "{=!}{GO_TO_KEEP_TEXT}", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.game_menu_town_go_to_keep_on_condition), new GameMenuOption.OnConsequenceDelegate(PlayerTownVisitCampaignBehavior.game_menu_town_go_to_keep_on_consequence), false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("town", "town_arena", "{=CfDlOdTH}Go to the arena", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.game_menu_town_go_to_arena_on_condition), delegate(MenuCallbackArgs x)
			{
				GameMenu.SwitchToMenu("town_arena");
			}, false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("town", "town_backstreet", "{=l9sFJawW}Go to the tavern district", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.game_menu_go_to_tavern_district_on_condition), delegate(MenuCallbackArgs x)
			{
				GameMenu.SwitchToMenu("town_backstreet");
			}, false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("town", "manage_production", "{=dgf6q4qB}Manage town", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.game_menu_town_manage_town_on_condition), null, false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("town", "manage_production_cheat", "{=zZ3GqbzC}Manage town (Cheat)", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.game_menu_town_manage_town_cheat_on_condition), null, false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("town", "recruit_volunteers", "{=E31IJyqs}Recruit troops", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.game_menu_town_recruit_troops_on_condition), new GameMenuOption.OnConsequenceDelegate(PlayerTownVisitCampaignBehavior.game_menu_recruit_volunteers_on_consequence), false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("town", "trade", "{=GmcgoiGy}Trade", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.game_menu_trade_on_condition), new GameMenuOption.OnConsequenceDelegate(PlayerTownVisitCampaignBehavior.game_menu_town_town_market_on_consequence), false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("town", "town_smithy", "{=McHsHbH8}Enter smithy", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.game_menu_craft_item_on_condition), delegate(MenuCallbackArgs x)
			{
				CraftingHelper.OpenCrafting(CraftingTemplate.All[0], null);
			}, false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("town", "town_wait", "{=zEoHYEUS}Wait here for some time", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.game_menu_wait_here_on_condition), delegate(MenuCallbackArgs x)
			{
				GameMenu.SwitchToMenu("town_wait_menus");
			}, false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("town", "town_return_to_army", "{=SK43eB6y}Return to Army", new GameMenuOption.OnConditionDelegate(this.game_menu_return_to_army_on_condition), new GameMenuOption.OnConsequenceDelegate(this.game_menu_return_to_army_on_consequence), false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("town", "town_leave", "{=3sRdGQou}Leave", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.game_menu_town_town_leave_on_condition), new GameMenuOption.OnConsequenceDelegate(PlayerTownVisitCampaignBehavior.game_menu_settlement_leave_on_consequence), true, -1, false, null);
			campaignGameSystemStarter.AddGameMenu("town_keep", "{=!}{SETTLEMENT_INFO}", new OnInitDelegate(PlayerTownVisitCampaignBehavior.town_keep_on_init), GameOverlays.MenuOverlayType.SettlementWithCharacters, GameMenu.MenuFlags.None, null);
			campaignGameSystemStarter.AddGameMenuOption("town_keep", "town_lords_hall", "{=dv2ZNazN}Go to the lord's hall", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.game_menu_town_keep_go_to_lords_hall_on_condition), new GameMenuOption.OnConsequenceDelegate(PlayerTownVisitCampaignBehavior.game_menu_town_lordshall_on_consequence), false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("town_keep", "town_lords_hall_cheat", "{=!}Go to the lord's hall (Cheat)", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.game_menu_castle_go_to_lords_hall_cheat_on_condition), new GameMenuOption.OnConsequenceDelegate(PlayerTownVisitCampaignBehavior.game_menu_lordshall_cheat_on_consequence), false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("town_keep", "town_lords_hall_go_to_dungeon", "{=etjMHPjQ}Go to dungeon", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.game_menu_go_dungeon_on_condition), new GameMenuOption.OnConsequenceDelegate(PlayerTownVisitCampaignBehavior.game_menu_go_dungeon_on_consequence), false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("town_keep", "leave_troops_to_garrison", "{=7J9KNFTz}Donate troops to garrison", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.game_menu_leave_troops_garrison_on_condition), new GameMenuOption.OnConsequenceDelegate(PlayerTownVisitCampaignBehavior.game_menu_leave_troops_garrison_on_consequece), false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("town_keep", "manage_garrison", "{=QazTA60M}Manage garrison", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.game_menu_manage_garrison_on_condition), new GameMenuOption.OnConsequenceDelegate(PlayerTownVisitCampaignBehavior.game_menu_manage_garrison_on_consequence), false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("town_keep", "open_stash", "{=xl4K9ecB}Open stash", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.game_menu_town_keep_open_stash_on_condition), new GameMenuOption.OnConsequenceDelegate(PlayerTownVisitCampaignBehavior.game_menu_town_keep_open_stash_on_consequence), false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("town_keep", "town_castle_back", "{=qWAmxyYz}Back to town center", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.back_on_condition), delegate(MenuCallbackArgs x)
			{
				GameMenu.SwitchToMenu("town");
			}, true, -1, false, null);
			campaignGameSystemStarter.AddGameMenu("town_keep_dungeon", "{=!}{PRISONER_INTRODUCTION}", new OnInitDelegate(PlayerTownVisitCampaignBehavior.town_keep_dungeon_on_init), GameOverlays.MenuOverlayType.SettlementWithCharacters, GameMenu.MenuFlags.None, null);
			campaignGameSystemStarter.AddGameMenuOption("town_keep_dungeon", "town_prison", "{=UnQFawna}Enter the dungeon", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.game_menu_castle_enter_the_dungeon_on_condition), new GameMenuOption.OnConsequenceDelegate(PlayerTownVisitCampaignBehavior.game_menu_town_dungeon_on_consequence), false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("town_keep_dungeon", "town_prison_cheat", "{=KBxajw4c}Enter the dungeon (Cheat)", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.game_menu_castle_go_to_dungeon_cheat_on_condition), new GameMenuOption.OnConsequenceDelegate(PlayerTownVisitCampaignBehavior.game_menu_dungeon_cheat_on_consequence), false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("town_keep_dungeon", "town_prison_leave_prisoners", "{=kmsNUfbA}Donate prisoners", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.game_menu_castle_leave_prisoners_on_condition), new GameMenuOption.OnConsequenceDelegate(PlayerTownVisitCampaignBehavior.game_menu_castle_leave_prisoners_on_consequence), false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("town_keep_dungeon", "town_prison_manage_prisoners", "{=VXkL5Ysd}Manage prisoners", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.game_menu_castle_manage_prisoners_on_condition), new GameMenuOption.OnConsequenceDelegate(PlayerTownVisitCampaignBehavior.game_menu_castle_manage_prisoners_on_consequence), false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("town_keep_dungeon", "town_keep_dungeon_back", "{=3sRdGQou}Leave", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.back_on_condition), delegate(MenuCallbackArgs x)
			{
				GameMenu.SwitchToMenu("town_keep");
			}, true, -1, false, null);
			campaignGameSystemStarter.AddGameMenu("town_keep_bribe", "{=yyz111nn}The guards say that they can't just let anyone in.", new OnInitDelegate(PlayerTownVisitCampaignBehavior.town_keep_bribe_on_init), GameOverlays.MenuOverlayType.SettlementWithCharacters, GameMenu.MenuFlags.None, null);
			campaignGameSystemStarter.AddGameMenuOption("town_keep_bribe", "town_keep_bribe_pay", "{=fxEka7Bm}Pay a {AMOUNT}{GOLD_ICON} bribe to enter the keep", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.game_menu_town_keep_bribe_pay_bribe_on_condition), new GameMenuOption.OnConsequenceDelegate(PlayerTownVisitCampaignBehavior.game_menu_town_keep_bribe_pay_bribe_on_consequence), false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("town_keep_bribe", "town_keep_bribe_back", "{=3sRdGQou}Leave", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.back_on_condition), delegate(MenuCallbackArgs x)
			{
				GameMenu.SwitchToMenu("town");
			}, true, -1, false, null);
			campaignGameSystemStarter.AddGameMenu("town_enemy_town_keep", "{=!}{SCOUT_KEEP_TEXT}", new OnInitDelegate(PlayerTownVisitCampaignBehavior.town_enemy_keep_on_init), GameOverlays.MenuOverlayType.SettlementWithCharacters, GameMenu.MenuFlags.None, null);
			campaignGameSystemStarter.AddGameMenuOption("town_enemy_town_keep", "settlement_go_back_to_center", "{=3sRdGQou}Leave", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.back_on_condition), delegate(MenuCallbackArgs x)
			{
				GameMenu.SwitchToMenu("town");
			}, true, -1, false, null);
			campaignGameSystemStarter.AddGameMenu("town_backstreet", "{=Zwy8JybD}You are in the backstreets. The local tavern seems to be attracting its usual crowd.", new OnInitDelegate(PlayerTownVisitCampaignBehavior.town_backstreet_on_init), GameOverlays.MenuOverlayType.SettlementWithBoth, GameMenu.MenuFlags.None, null);
			campaignGameSystemStarter.AddGameMenuOption("town_backstreet", "town_tavern", "{=qcl3YTPh}Visit the tavern", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.visit_the_tavern_on_condition), new GameMenuOption.OnConsequenceDelegate(PlayerTownVisitCampaignBehavior.game_menu_town_town_tavern_on_consequence), false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("town_backstreet", "sell_all_prisoners", "{=xZIBKK0v}Ransom your prisoners ({RANSOM_AMOUNT}{GOLD_ICON})", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.SellPrisonersCondition), delegate(MenuCallbackArgs x)
			{
				PlayerTownVisitCampaignBehavior.SellAllTransferablePrisoners();
			}, false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("town_backstreet", "sell_some_prisoners", "{=Q8VN9UCq}Choose the prisoners to be ransomed", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.SellPrisonerOneStackCondition), delegate(MenuCallbackArgs x)
			{
				PlayerTownVisitCampaignBehavior.ChooseRansomPrisoners();
			}, false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("town_backstreet", "town_backstreet_back", "{=qWAmxyYz}Back to town center", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.back_on_condition), delegate(MenuCallbackArgs x)
			{
				GameMenu.SwitchToMenu("town");
			}, true, -1, false, null);
			campaignGameSystemStarter.AddGameMenu("town_arena", "{=5id9mGrc}You are near the arena. {ADDITIONAL_STRING}", new OnInitDelegate(PlayerTownVisitCampaignBehavior.town_arena_on_init), GameOverlays.MenuOverlayType.SettlementWithCharacters, GameMenu.MenuFlags.None, null);
			campaignGameSystemStarter.AddGameMenuOption("town_arena", "town_enter_arena", "{=YQ3vm6er}Enter the arena", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.game_menu_town_enter_the_arena_on_condition), new GameMenuOption.OnConsequenceDelegate(PlayerTownVisitCampaignBehavior.game_menu_town_town_arena_on_consequence), false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("town_arena", "town_arena_back", "{=qWAmxyYz}Back to town center", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.back_on_condition), delegate(MenuCallbackArgs x)
			{
				GameMenu.SwitchToMenu("town");
			}, true, -1, false, null);
			campaignGameSystemStarter.AddGameMenu("settlement_player_unconscious", "{=S5OEsjwg}You slip into unconsciousness. After a little while some of the friendlier locals manage to bring you around. A little confused but without any serious injuries, you resolve to be more careful next time.", null, GameOverlays.MenuOverlayType.SettlementWithBoth, GameMenu.MenuFlags.None, null);
			campaignGameSystemStarter.AddGameMenuOption("settlement_player_unconscious", "continue", "{=DM6luo3c}Continue", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.continue_on_condition), new GameMenuOption.OnConsequenceDelegate(PlayerTownVisitCampaignBehavior.settlement_player_unconscious_continue_on_consequence), false, -1, false, null);
			campaignGameSystemStarter.AddWaitGameMenu("settlement_wait", "{=8AbHxCM8}{CAPTIVITY_TEXT}\nWaiting in captivity...", new OnInitDelegate(PlayerTownVisitCampaignBehavior.settlement_wait_on_init), new OnConditionDelegate(PlayerTownVisitCampaignBehavior.wait_menu_prisoner_wait_on_condition), null, new OnTickDelegate(PlayerTownVisitCampaignBehavior.wait_menu_settlement_wait_on_tick), GameMenu.MenuAndOptionType.WaitMenuHideProgressAndHoursOption, GameOverlays.MenuOverlayType.None, 0f, GameMenu.MenuFlags.None, null);
			campaignGameSystemStarter.AddWaitGameMenu("town_wait_menus", "{=ydbVysqv}You are waiting in {CURRENT_SETTLEMENT}.", new OnInitDelegate(this.game_menu_settlement_wait_on_init), new OnConditionDelegate(PlayerTownVisitCampaignBehavior.game_menu_town_wait_on_condition), null, delegate(MenuCallbackArgs args, CampaignTime dt)
			{
				this.SwitchToMenuIfThereIsAnInterrupt(args.MenuContext.GameMenu.StringId);
			}, GameMenu.MenuAndOptionType.WaitMenuHideProgressAndHoursOption, GameOverlays.MenuOverlayType.SettlementWithBoth, 0f, GameMenu.MenuFlags.None, null);
			campaignGameSystemStarter.AddGameMenuOption("town_wait_menus", "wait_leave", "{=UqDNAZqM}Stop waiting", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.back_on_condition), delegate(MenuCallbackArgs args)
			{
				PlayerEncounter.Current.IsPlayerWaiting = false;
				this.SwitchToMenuIfThereIsAnInterrupt(args.MenuContext.GameMenu.StringId);
			}, true, -1, false, null);
			campaignGameSystemStarter.AddGameMenu("castle", "{=!}{SETTLEMENT_INFO}", new OnInitDelegate(PlayerTownVisitCampaignBehavior.game_menu_castle_on_init), GameOverlays.MenuOverlayType.SettlementWithBoth, GameMenu.MenuFlags.None, null);
			campaignGameSystemStarter.AddGameMenuOption("castle", "take_a_walk_around_the_castle", "{=R92XzKXE}Take a walk around the castle", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.game_menu_castle_take_a_walk_on_condition), new GameMenuOption.OnConsequenceDelegate(PlayerTownVisitCampaignBehavior.game_menu_castle_take_a_walk_around_the_castle_on_consequence), false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("castle", "castle_lords_hall", "{=dv2ZNazN}Go to the lord's hall", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.game_menu_castle_go_to_lords_hall_on_condition), new GameMenuOption.OnConsequenceDelegate(PlayerTownVisitCampaignBehavior.game_menu_castle_lordshall_on_consequence), false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("castle", "castle_lords_hall_cheat", "{=dl6YxNTT}Go to the lord's hall (Cheat)", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.game_menu_castle_go_to_lords_hall_cheat_on_condition), new GameMenuOption.OnConsequenceDelegate(PlayerTownVisitCampaignBehavior.game_menu_lordshall_cheat_on_consequence), false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("castle", "castle_prison", "{=esSm5V6t}Go to the dungeon", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.game_menu_castle_go_to_the_dungeon_on_condition), new GameMenuOption.OnConsequenceDelegate(PlayerTownVisitCampaignBehavior.game_menu_keep_dungeon_on_consequence), false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("castle", "castle_prison_cheat", "{=pa7oiQb1}Go to the dungeon (Cheat)", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.game_menu_castle_go_to_dungeon_cheat_on_condition), new GameMenuOption.OnConsequenceDelegate(PlayerTownVisitCampaignBehavior.game_menu_dungeon_cheat_on_consequence), false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("castle", "manage_garrison", "{=QazTA60M}Manage garrison", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.game_menu_manage_garrison_on_condition), new GameMenuOption.OnConsequenceDelegate(PlayerTownVisitCampaignBehavior.game_menu_manage_garrison_on_consequence), false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("castle", "manage_production", "{=Ll1EJHXF}Manage castle", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.game_menu_manage_castle_on_condition), null, false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("castle", "open_stash", "{=xl4K9ecB}Open stash", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.game_menu_town_keep_open_stash_on_condition), new GameMenuOption.OnConsequenceDelegate(PlayerTownVisitCampaignBehavior.game_menu_town_keep_open_stash_on_consequence), false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("castle", "leave_troops_to_garrison", "{=7J9KNFTz}Donate troops to garrison", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.game_menu_leave_troops_garrison_on_condition), new GameMenuOption.OnConsequenceDelegate(PlayerTownVisitCampaignBehavior.game_menu_leave_troops_garrison_on_consequece), false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("castle", "town_wait", "{=zEoHYEUS}Wait here for some time", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.game_menu_wait_here_on_condition), delegate(MenuCallbackArgs x)
			{
				GameMenu.SwitchToMenu("town_wait_menus");
			}, false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("castle", "castle_return_to_army", "{=SK43eB6y}Return to Army", new GameMenuOption.OnConditionDelegate(this.game_menu_return_to_army_on_condition), new GameMenuOption.OnConsequenceDelegate(this.game_menu_return_to_army_on_consequence), false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("castle", "leave", "{=3sRdGQou}Leave", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.game_menu_town_town_leave_on_condition), new GameMenuOption.OnConsequenceDelegate(PlayerTownVisitCampaignBehavior.game_menu_settlement_leave_on_consequence), true, -1, false, null);
			campaignGameSystemStarter.AddGameMenu("castle_lords_hall_bribe", "{=yyz111nn}The guards say that they can't just let anyone in.", null, GameOverlays.MenuOverlayType.SettlementWithCharacters, GameMenu.MenuFlags.None, null);
			campaignGameSystemStarter.AddGameMenuOption("castle_lords_hall_bribe", "castle_bribe_pay", "{=3lxq5fvI}Pay a {AMOUNT}{GOLD_ICON} bribe to go to the lord's hall.", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.game_menu_castle_lordshall_bribe_pay_bribe_on_condition), new GameMenuOption.OnConsequenceDelegate(PlayerTownVisitCampaignBehavior.game_menu_castle_lordshall_bribe_on_consequence), false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("castle_lords_hall_bribe", "castle_bribe_back", "{=3sRdGQou}Leave", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.back_on_condition), delegate(MenuCallbackArgs x)
			{
				GameMenu.SwitchToMenu("castle");
			}, true, -1, false, null);
			campaignGameSystemStarter.AddGameMenu("castle_dungeon", "{=!}{PRISONER_INTRODUCTION}", new OnInitDelegate(PlayerTownVisitCampaignBehavior.town_keep_dungeon_on_init), GameOverlays.MenuOverlayType.SettlementWithCharacters, GameMenu.MenuFlags.None, null);
			campaignGameSystemStarter.AddGameMenuOption("castle_dungeon", "town_prison", "{=UnQFawna}Enter the dungeon", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.game_menu_castle_enter_the_dungeon_on_condition), new GameMenuOption.OnConsequenceDelegate(PlayerTownVisitCampaignBehavior.game_menu_castle_dungeon_on_consequence), false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("castle_dungeon", "town_prison_cheat", "{=KBxajw4c}Enter the dungeon (Cheat)", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.game_menu_castle_go_to_dungeon_cheat_on_condition), new GameMenuOption.OnConsequenceDelegate(PlayerTownVisitCampaignBehavior.game_menu_dungeon_cheat_on_consequence), false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("castle_dungeon", "town_prison_leave_prisoners", "{=kmsNUfbA}Donate prisoners", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.game_menu_castle_leave_prisoners_on_condition), new GameMenuOption.OnConsequenceDelegate(PlayerTownVisitCampaignBehavior.game_menu_castle_leave_prisoners_on_consequence), false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("castle_dungeon", "town_prison_manage_prisoners", "{=VXkL5Ysd}Manage prisoners", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.game_menu_castle_manage_prisoners_on_condition), new GameMenuOption.OnConsequenceDelegate(PlayerTownVisitCampaignBehavior.game_menu_castle_manage_prisoners_on_consequence), false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("castle_dungeon", "town_keep_dungeon_back", "{=3sRdGQou}Leave", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.back_on_condition), delegate(MenuCallbackArgs x)
			{
				GameMenu.SwitchToMenu("castle");
			}, true, -1, false, null);
			campaignGameSystemStarter.AddGameMenu("village", "{=!}{SETTLEMENT_INFO}", new OnInitDelegate(PlayerTownVisitCampaignBehavior.game_menu_village_on_init), GameOverlays.MenuOverlayType.SettlementWithBoth, GameMenu.MenuFlags.None, null);
			campaignGameSystemStarter.AddGameMenuOption("village", "village_center", "{=U4azeSib}Take a walk through the lands", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.game_menu_village_village_center_on_condition), new GameMenuOption.OnConsequenceDelegate(PlayerTownVisitCampaignBehavior.game_menu_village_village_center_on_consequence), false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("village", "recruit_volunteers", "{=E31IJyqs}Recruit troops", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.game_menu_recruit_volunteers_on_condition), new GameMenuOption.OnConsequenceDelegate(PlayerTownVisitCampaignBehavior.game_menu_recruit_volunteers_on_consequence), false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("village", "trade", "{=VN4ctHIU}Buy products", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.game_menu_village_buy_good_on_condition), null, false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("village", "village_wait", "{=zEoHYEUS}Wait here for some time", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.game_menu_wait_here_on_condition), new GameMenuOption.OnConsequenceDelegate(this.game_menu_wait_village_on_consequence), false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("village", "village_return_to_army", "{=SK43eB6y}Return to Army", new GameMenuOption.OnConditionDelegate(this.game_menu_return_to_army_on_condition), new GameMenuOption.OnConsequenceDelegate(this.game_menu_return_to_army_on_consequence), false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("village", "leave", "{=3sRdGQou}Leave", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.game_menu_town_town_leave_on_condition), new GameMenuOption.OnConsequenceDelegate(PlayerTownVisitCampaignBehavior.game_menu_settlement_leave_on_consequence), true, -1, false, null);
			campaignGameSystemStarter.AddWaitGameMenu("village_wait_menus", "{=lsBuV9W7}You are waiting in the village.", new OnInitDelegate(this.game_menu_settlement_wait_on_init), new OnConditionDelegate(PlayerTownVisitCampaignBehavior.game_menu_village_wait_on_condition), null, delegate(MenuCallbackArgs args, CampaignTime dt)
			{
				this.SwitchToMenuIfThereIsAnInterrupt(args.MenuContext.GameMenu.StringId);
			}, GameMenu.MenuAndOptionType.WaitMenuHideProgressAndHoursOption, GameOverlays.MenuOverlayType.SettlementWithBoth, 0f, GameMenu.MenuFlags.None, null);
			campaignGameSystemStarter.AddGameMenuOption("village_wait_menus", "wait_leave", "{=UqDNAZqM}Stop waiting", new GameMenuOption.OnConditionDelegate(PlayerTownVisitCampaignBehavior.back_on_condition), new GameMenuOption.OnConsequenceDelegate(this.game_menu_stop_waiting_at_village_on_consequence), true, -1, false, null);
			campaignGameSystemStarter.AddWaitGameMenu("prisoner_wait", "{=!}{CAPTIVITY_TEXT}", new OnInitDelegate(PlayerTownVisitCampaignBehavior.wait_menu_prisoner_wait_on_init), new OnConditionDelegate(PlayerTownVisitCampaignBehavior.wait_menu_prisoner_wait_on_condition), null, new OnTickDelegate(PlayerTownVisitCampaignBehavior.wait_menu_prisoner_wait_on_tick), GameMenu.MenuAndOptionType.WaitMenuHideProgressAndHoursOption, GameOverlays.MenuOverlayType.None, 0f, GameMenu.MenuFlags.None, null);
		}

		// Token: 0x06003ACF RID: 15055 RVA: 0x0011730C File Offset: 0x0011550C
		private void game_menu_settlement_wait_on_init(MenuCallbackArgs args)
		{
			string text = PlayerEncounter.EncounterSettlement.IsVillage ? "village" : (PlayerEncounter.EncounterSettlement.IsTown ? "town" : (PlayerEncounter.EncounterSettlement.IsCastle ? "castle" : null));
			if (text != null)
			{
				PlayerTownVisitCampaignBehavior.UpdateMenuLocations(text);
			}
			if (PlayerEncounter.Current != null)
			{
				PlayerEncounter.Current.IsPlayerWaiting = true;
			}
		}

		// Token: 0x06003AD0 RID: 15056 RVA: 0x00117370 File Offset: 0x00115570
		private static void OpenMissionWithSettingPreviousLocation(string previousLocationId, string missionLocationId)
		{
			Campaign.Current.GameMenuManager.NextLocation = LocationComplex.Current.GetLocationWithId(missionLocationId);
			Campaign.Current.GameMenuManager.PreviousLocation = LocationComplex.Current.GetLocationWithId(previousLocationId);
			PlayerEncounter.LocationEncounter.CreateAndOpenMissionController(Campaign.Current.GameMenuManager.NextLocation, null, null, null);
			Campaign.Current.GameMenuManager.NextLocation = null;
			Campaign.Current.GameMenuManager.PreviousLocation = null;
		}

		// Token: 0x06003AD1 RID: 15057 RVA: 0x001173EE File Offset: 0x001155EE
		private void game_menu_stop_waiting_at_village_on_consequence(MenuCallbackArgs args)
		{
			EnterSettlementAction.ApplyForParty(MobileParty.MainParty, MobileParty.MainParty.LastVisitedSettlement);
			GameMenu.SwitchToMenu("village");
		}

		// Token: 0x06003AD2 RID: 15058 RVA: 0x0011740E File Offset: 0x0011560E
		private static bool continue_on_condition(MenuCallbackArgs args)
		{
			args.optionLeaveType = GameMenuOption.LeaveType.Continue;
			return true;
		}

		// Token: 0x06003AD3 RID: 15059 RVA: 0x0011741C File Offset: 0x0011561C
		private static bool game_menu_castle_go_to_the_dungeon_on_condition(MenuCallbackArgs args)
		{
			SettlementAccessModel.AccessDetails accessDetails;
			Campaign.Current.Models.SettlementAccessModel.CanMainHeroEnterDungeon(Settlement.CurrentSettlement, out accessDetails);
			if (accessDetails.AccessLevel == SettlementAccessModel.AccessLevel.LimitedAccess)
			{
				args.Tooltip = new TextObject("{=aPoS8wOW}You have limited access to Dungeon.", null);
				args.IsEnabled = false;
			}
			if (FactionManager.IsAtWarAgainstFaction(Settlement.CurrentSettlement.MapFaction, Hero.MainHero.MapFaction))
			{
				args.Tooltip = new TextObject("{=h9i9VXLd}You cannot enter an enemy lord's hall.", null);
				args.IsEnabled = false;
			}
			args.optionLeaveType = GameMenuOption.LeaveType.Submenu;
			List<Location> locations = Settlement.CurrentSettlement.LocationComplex.FindAll((string x) => x == "prison").ToList<Location>();
			MenuHelper.SetIssueAndQuestDataForLocations(args, locations);
			return true;
		}

		// Token: 0x06003AD4 RID: 15060 RVA: 0x001174DC File Offset: 0x001156DC
		private static bool game_menu_castle_enter_the_dungeon_on_condition(MenuCallbackArgs args)
		{
			args.optionLeaveType = GameMenuOption.LeaveType.Mission;
			List<Location> locations = Settlement.CurrentSettlement.LocationComplex.FindAll((string x) => x == "prison").ToList<Location>();
			MenuHelper.SetIssueAndQuestDataForLocations(args, locations);
			return true;
		}

		// Token: 0x06003AD5 RID: 15061 RVA: 0x0011752C File Offset: 0x0011572C
		private static bool game_menu_castle_go_to_dungeon_cheat_on_condition(MenuCallbackArgs args)
		{
			return Game.Current.IsDevelopmentMode;
		}

		// Token: 0x06003AD6 RID: 15062 RVA: 0x00117538 File Offset: 0x00115738
		private static bool game_menu_castle_leave_prisoners_on_condition(MenuCallbackArgs args)
		{
			args.optionLeaveType = GameMenuOption.LeaveType.DonatePrisoners;
			Settlement currentSettlement = Settlement.CurrentSettlement;
			if (currentSettlement.IsFortification)
			{
				if (currentSettlement.Party != null && currentSettlement.Party.PrisonerSizeLimit <= currentSettlement.Party.NumberOfPrisoners)
				{
					args.IsEnabled = false;
					args.Tooltip = GameTexts.FindText("str_dungeon_size_limit_exceeded", null);
					args.Tooltip.SetTextVariable("TROOP_NUMBER", currentSettlement.Party.NumberOfPrisoners);
				}
				return currentSettlement.OwnerClan != Clan.PlayerClan && currentSettlement.MapFaction == Hero.MainHero.MapFaction;
			}
			return false;
		}

		// Token: 0x06003AD7 RID: 15063 RVA: 0x001175D4 File Offset: 0x001157D4
		private static bool game_menu_castle_manage_prisoners_on_condition(MenuCallbackArgs args)
		{
			args.optionLeaveType = GameMenuOption.LeaveType.ManagePrisoners;
			Settlement currentSettlement = Settlement.CurrentSettlement;
			return currentSettlement.OwnerClan == Clan.PlayerClan && currentSettlement.MapFaction == Hero.MainHero.MapFaction && currentSettlement.IsFortification;
		}

		// Token: 0x06003AD8 RID: 15064 RVA: 0x00117616 File Offset: 0x00115816
		private static void game_menu_castle_leave_prisoners_on_consequence(MenuCallbackArgs args)
		{
			PartyScreenManager.OpenScreenAsDonatePrisoners();
		}

		// Token: 0x06003AD9 RID: 15065 RVA: 0x0011761D File Offset: 0x0011581D
		private static void game_menu_castle_manage_prisoners_on_consequence(MenuCallbackArgs args)
		{
			PartyScreenManager.OpenScreenAsManagePrisoners();
		}

		// Token: 0x06003ADA RID: 15066 RVA: 0x00117624 File Offset: 0x00115824
		private static bool game_menu_town_go_to_keep_on_condition(MenuCallbackArgs args)
		{
			TextObject text = new TextObject("{=XZFQ1Jf6}Go to the keep", null);
			SettlementAccessModel.AccessDetails accessDetails;
			Campaign.Current.Models.SettlementAccessModel.CanMainHeroEnterLordsHall(Settlement.CurrentSettlement, out accessDetails);
			if (accessDetails.AccessLevel == SettlementAccessModel.AccessLevel.NoAccess)
			{
				args.IsEnabled = false;
				PlayerTownVisitCampaignBehavior.SetLordsHallAccessLimitationReasonText(args, accessDetails);
			}
			else if (accessDetails.AccessLevel == SettlementAccessModel.AccessLevel.LimitedAccess && accessDetails.LimitedAccessSolution == SettlementAccessModel.LimitedAccessSolution.Disguise)
			{
				text = new TextObject("{=1GPa9aTQ}Scout the keep", null);
				args.Tooltip = new TextObject("{=ubOtRU3u}You have limited access to keep while in disguise.", null);
			}
			MBTextManager.SetTextVariable("GO_TO_KEEP_TEXT", text, false);
			List<Location> locations = Settlement.CurrentSettlement.LocationComplex.FindAll((string x) => x == "lordshall" || x == "prison").ToList<Location>();
			MenuHelper.SetIssueAndQuestDataForLocations(args, locations);
			args.optionLeaveType = GameMenuOption.LeaveType.Submenu;
			return true;
		}

		// Token: 0x06003ADB RID: 15067 RVA: 0x001176F0 File Offset: 0x001158F0
		private static bool game_menu_go_to_tavern_district_on_condition(MenuCallbackArgs args)
		{
			bool shouldBeDisabled;
			TextObject disabledText;
			bool canPlayerDo = Campaign.Current.Models.SettlementAccessModel.CanMainHeroAccessLocation(Settlement.CurrentSettlement, "tavern", out shouldBeDisabled, out disabledText);
			List<Location> locations = Settlement.CurrentSettlement.LocationComplex.FindAll((string x) => x == "tavern").ToList<Location>();
			MenuHelper.SetIssueAndQuestDataForLocations(args, locations);
			args.optionLeaveType = GameMenuOption.LeaveType.Submenu;
			return MenuHelper.SetOptionProperties(args, canPlayerDo, shouldBeDisabled, disabledText);
		}

		// Token: 0x06003ADC RID: 15068 RVA: 0x0011776C File Offset: 0x0011596C
		private static bool game_menu_trade_on_condition(MenuCallbackArgs args)
		{
			bool shouldBeDisabled;
			TextObject disabledText;
			bool canPlayerDo = Campaign.Current.Models.SettlementAccessModel.CanMainHeroDoSettlementAction(Settlement.CurrentSettlement, SettlementAccessModel.SettlementAction.Trade, out shouldBeDisabled, out disabledText);
			args.optionLeaveType = GameMenuOption.LeaveType.Trade;
			return MenuHelper.SetOptionProperties(args, canPlayerDo, shouldBeDisabled, disabledText);
		}

		// Token: 0x06003ADD RID: 15069 RVA: 0x001177AC File Offset: 0x001159AC
		private static bool game_menu_town_recruit_troops_on_condition(MenuCallbackArgs args)
		{
			bool shouldBeDisabled;
			TextObject disabledText;
			bool canPlayerDo = Campaign.Current.Models.SettlementAccessModel.CanMainHeroDoSettlementAction(Settlement.CurrentSettlement, SettlementAccessModel.SettlementAction.RecruitTroops, out shouldBeDisabled, out disabledText);
			args.optionLeaveType = GameMenuOption.LeaveType.Recruit;
			return MenuHelper.SetOptionProperties(args, canPlayerDo, shouldBeDisabled, disabledText);
		}

		// Token: 0x06003ADE RID: 15070 RVA: 0x001177EC File Offset: 0x001159EC
		private static bool game_menu_wait_here_on_condition(MenuCallbackArgs args)
		{
			bool shouldBeDisabled;
			TextObject disabledText;
			bool canPlayerDo = Campaign.Current.Models.SettlementAccessModel.CanMainHeroDoSettlementAction(Settlement.CurrentSettlement, SettlementAccessModel.SettlementAction.WaitInSettlement, out shouldBeDisabled, out disabledText);
			args.optionLeaveType = GameMenuOption.LeaveType.Wait;
			return MenuHelper.SetOptionProperties(args, canPlayerDo, shouldBeDisabled, disabledText);
		}

		// Token: 0x06003ADF RID: 15071 RVA: 0x00117829 File Offset: 0x00115A29
		private void game_menu_wait_village_on_consequence(MenuCallbackArgs args)
		{
			GameMenu.SwitchToMenu("village_wait_menus");
			LeaveSettlementAction.ApplyForParty(MobileParty.MainParty);
		}

		// Token: 0x06003AE0 RID: 15072 RVA: 0x0011783F File Offset: 0x00115A3F
		private bool game_menu_return_to_army_on_condition(MenuCallbackArgs args)
		{
			args.optionLeaveType = GameMenuOption.LeaveType.Wait;
			return MobileParty.MainParty.Army != null && MobileParty.MainParty.Army.LeaderParty != MobileParty.MainParty;
		}

		// Token: 0x06003AE1 RID: 15073 RVA: 0x00117870 File Offset: 0x00115A70
		private void game_menu_return_to_army_on_consequence(MenuCallbackArgs args)
		{
			GameMenu.SwitchToMenu("army_wait_at_settlement");
			if (MobileParty.MainParty.CurrentSettlement.IsVillage)
			{
				PlayerEncounter.LeaveSettlement();
				PlayerEncounter.Finish(true);
			}
		}

		// Token: 0x06003AE2 RID: 15074 RVA: 0x00117898 File Offset: 0x00115A98
		private static bool game_menu_castle_take_a_walk_on_condition(MenuCallbackArgs args)
		{
			List<Location> locations = Settlement.CurrentSettlement.LocationComplex.FindAll((string x) => x == "center").ToList<Location>();
			MenuHelper.SetIssueAndQuestDataForLocations(args, locations);
			args.optionLeaveType = GameMenuOption.LeaveType.Mission;
			return true;
		}

		// Token: 0x06003AE3 RID: 15075 RVA: 0x001178E8 File Offset: 0x00115AE8
		private static void game_menu_town_go_to_keep_on_consequence(MenuCallbackArgs args)
		{
			SettlementAccessModel.AccessDetails accessDetails;
			Campaign.Current.Models.SettlementAccessModel.CanMainHeroEnterLordsHall(Settlement.CurrentSettlement, out accessDetails);
			SettlementAccessModel.AccessLevel accessLevel = accessDetails.AccessLevel;
			int num = (int)accessLevel;
			if (num != 1)
			{
				if (num == 2)
				{
					GameMenu.SwitchToMenu("town_keep");
					return;
				}
			}
			else
			{
				if (accessDetails.LimitedAccessSolution == SettlementAccessModel.LimitedAccessSolution.Bribe)
				{
					GameMenu.SwitchToMenu("town_keep_bribe");
					return;
				}
				if (accessDetails.LimitedAccessSolution == SettlementAccessModel.LimitedAccessSolution.Disguise)
				{
					GameMenu.SwitchToMenu("town_enemy_town_keep");
					return;
				}
			}
			Debug.FailedAssert("invalid LimitedAccessSolution or AccessLevel for town keep", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\CampaignBehaviors\\PlayerTownVisitCampaignBehavior.cs", "game_menu_town_go_to_keep_on_consequence", 477);
		}

		// Token: 0x06003AE4 RID: 15076 RVA: 0x00117970 File Offset: 0x00115B70
		private static bool game_menu_go_dungeon_on_condition(MenuCallbackArgs args)
		{
			SettlementAccessModel.AccessDetails accessDetails;
			Campaign.Current.Models.SettlementAccessModel.CanMainHeroEnterDungeon(Settlement.CurrentSettlement, out accessDetails);
			if (Settlement.CurrentSettlement.BribePaid < Campaign.Current.Models.BribeCalculationModel.GetBribeToEnterDungeon(Settlement.CurrentSettlement) && accessDetails.AccessLevel == SettlementAccessModel.AccessLevel.LimitedAccess)
			{
				args.Tooltip = new TextObject("{=aPoS8wOW}You have limited access to Dungeon.", null);
				args.IsEnabled = false;
			}
			args.optionLeaveType = GameMenuOption.LeaveType.Submenu;
			List<Location> locations = Settlement.CurrentSettlement.LocationComplex.FindAll((string x) => x == "prison").ToList<Location>();
			MenuHelper.SetIssueAndQuestDataForLocations(args, locations);
			return true;
		}

		// Token: 0x06003AE5 RID: 15077 RVA: 0x00117A26 File Offset: 0x00115C26
		private static void game_menu_go_dungeon_on_consequence(MenuCallbackArgs args)
		{
			GameMenu.SwitchToMenu("town_keep_dungeon");
		}

		// Token: 0x06003AE6 RID: 15078 RVA: 0x00117A32 File Offset: 0x00115C32
		private static bool back_on_condition(MenuCallbackArgs args)
		{
			args.optionLeaveType = GameMenuOption.LeaveType.Leave;
			return true;
		}

		// Token: 0x06003AE7 RID: 15079 RVA: 0x00117A40 File Offset: 0x00115C40
		private static bool visit_the_tavern_on_condition(MenuCallbackArgs args)
		{
			args.optionLeaveType = GameMenuOption.LeaveType.Mission;
			List<Location> locations = Settlement.CurrentSettlement.LocationComplex.FindAll((string x) => x == "tavern").ToList<Location>();
			MenuHelper.SetIssueAndQuestDataForLocations(args, locations);
			return true;
		}

		// Token: 0x06003AE8 RID: 15080 RVA: 0x00117A90 File Offset: 0x00115C90
		private static bool game_menu_town_go_to_arena_on_condition(MenuCallbackArgs args)
		{
			bool shouldBeDisabled;
			TextObject disabledText;
			bool canPlayerDo = Campaign.Current.Models.SettlementAccessModel.CanMainHeroAccessLocation(Settlement.CurrentSettlement, "arena", out shouldBeDisabled, out disabledText);
			List<Location> locations = Settlement.CurrentSettlement.LocationComplex.FindAll((string x) => x == "arena").ToList<Location>();
			MenuHelper.SetIssueAndQuestDataForLocations(args, locations);
			args.optionLeaveType = GameMenuOption.LeaveType.Submenu;
			return MenuHelper.SetOptionProperties(args, canPlayerDo, shouldBeDisabled, disabledText);
		}

		// Token: 0x06003AE9 RID: 15081 RVA: 0x00117B0C File Offset: 0x00115D0C
		private static bool game_menu_town_enter_the_arena_on_condition(MenuCallbackArgs args)
		{
			bool shouldBeDisabled;
			TextObject disabledText;
			bool canPlayerDo = Campaign.Current.Models.SettlementAccessModel.CanMainHeroDoSettlementAction(Settlement.CurrentSettlement, SettlementAccessModel.SettlementAction.WalkAroundTheArena, out shouldBeDisabled, out disabledText);
			List<Location> locations = Settlement.CurrentSettlement.LocationComplex.FindAll((string x) => x == "arena").ToList<Location>();
			MenuHelper.SetIssueAndQuestDataForLocations(args, locations);
			args.optionLeaveType = GameMenuOption.LeaveType.Mission;
			return MenuHelper.SetOptionProperties(args, canPlayerDo, shouldBeDisabled, disabledText);
		}

		// Token: 0x06003AEA RID: 15082 RVA: 0x00117B84 File Offset: 0x00115D84
		private static bool game_menu_craft_item_on_condition(MenuCallbackArgs args)
		{
			bool shouldBeDisabled;
			TextObject disabledText;
			bool canPlayerDo = Campaign.Current.Models.SettlementAccessModel.CanMainHeroDoSettlementAction(Settlement.CurrentSettlement, SettlementAccessModel.SettlementAction.Craft, out shouldBeDisabled, out disabledText);
			args.optionLeaveType = GameMenuOption.LeaveType.Craft;
			ICraftingCampaignBehavior campaignBehavior = Campaign.Current.GetCampaignBehavior<ICraftingCampaignBehavior>();
			CraftingCampaignBehavior.CraftingOrderSlots craftingOrderSlots;
			if (Settlement.CurrentSettlement.IsTown && campaignBehavior != null && campaignBehavior.CraftingOrders != null && campaignBehavior.CraftingOrders.TryGetValue(Settlement.CurrentSettlement.Town, out craftingOrderSlots) && craftingOrderSlots.CustomOrders.Count > 0)
			{
				args.OptionQuestData |= GameMenuOption.IssueQuestFlags.ActiveIssue;
			}
			return MenuHelper.SetOptionProperties(args, canPlayerDo, shouldBeDisabled, disabledText);
		}

		// Token: 0x06003AEB RID: 15083 RVA: 0x00117C18 File Offset: 0x00115E18
		public static void wait_menu_prisoner_wait_on_init(MenuCallbackArgs args)
		{
			TextObject text = args.MenuContext.GameMenu.GetText();
			int captiveTimeInDays = PlayerCaptivity.CaptiveTimeInDays;
			TextObject textObject;
			if (captiveTimeInDays == 0)
			{
				textObject = GameTexts.FindText("str_prisoner_of_party_menu_text", null);
			}
			else
			{
				textObject = GameTexts.FindText("str_prisoner_of_party_for_days_menu_text", null);
				textObject.SetTextVariable("NUMBER_OF_DAYS", captiveTimeInDays);
				textObject.SetTextVariable("PLURAL", (captiveTimeInDays > 1) ? 1 : 0);
			}
			textObject.SetTextVariable("PARTY_NAME", (Hero.MainHero.PartyBelongedToAsPrisoner != null) ? Hero.MainHero.PartyBelongedToAsPrisoner.Name : TextObject.Empty);
			text.SetTextVariable("CAPTIVITY_TEXT", textObject);
		}

		// Token: 0x06003AEC RID: 15084 RVA: 0x00117CB3 File Offset: 0x00115EB3
		[GameMenuInitializationHandler("settlement_wait")]
		public static void wait_menu_prisoner_settlement_wait_ui_on_init(MenuCallbackArgs args)
		{
			if (Hero.MainHero.IsFemale)
			{
				args.MenuContext.SetBackgroundMeshName("wait_prisoner_female");
				return;
			}
			args.MenuContext.SetBackgroundMeshName("wait_prisoner_male");
		}

		// Token: 0x06003AED RID: 15085 RVA: 0x00117CE2 File Offset: 0x00115EE2
		public static bool wait_menu_prisoner_wait_on_condition(MenuCallbackArgs args)
		{
			return true;
		}

		// Token: 0x06003AEE RID: 15086 RVA: 0x00117CE8 File Offset: 0x00115EE8
		public static void wait_menu_prisoner_wait_on_tick(MenuCallbackArgs args, CampaignTime dt)
		{
			int captiveTimeInDays = PlayerCaptivity.CaptiveTimeInDays;
			if (captiveTimeInDays == 0)
			{
				return;
			}
			TextObject text = args.MenuContext.GameMenu.GetText();
			TextObject textObject = GameTexts.FindText("str_prisoner_of_party_for_days_menu_text", null);
			textObject.SetTextVariable("NUMBER_OF_DAYS", captiveTimeInDays);
			textObject.SetTextVariable("PLURAL", (captiveTimeInDays > 1) ? 1 : 0);
			textObject.SetTextVariable("PARTY_NAME", PlayerCaptivity.CaptorParty.Name);
			text.SetTextVariable("CAPTIVITY_TEXT", textObject);
		}

		// Token: 0x06003AEF RID: 15087 RVA: 0x00117D60 File Offset: 0x00115F60
		public static void wait_menu_settlement_wait_on_tick(MenuCallbackArgs args, CampaignTime dt)
		{
			int captiveTimeInDays = PlayerCaptivity.CaptiveTimeInDays;
			if (captiveTimeInDays == 0)
			{
				return;
			}
			TextObject variable = Hero.MainHero.IsPrisoner ? Hero.MainHero.PartyBelongedToAsPrisoner.Settlement.Name : Settlement.CurrentSettlement.Name;
			TextObject text = args.MenuContext.GameMenu.GetText();
			TextObject textObject = GameTexts.FindText("str_prisoner_of_settlement_for_days_menu_text", null);
			textObject.SetTextVariable("NUMBER_OF_DAYS", captiveTimeInDays);
			textObject.SetTextVariable("PLURAL", (captiveTimeInDays > 1) ? 1 : 0);
			textObject.SetTextVariable("SETTLEMENT_NAME", variable);
			text.SetTextVariable("CAPTIVITY_TEXT", textObject);
		}

		// Token: 0x06003AF0 RID: 15088 RVA: 0x00117DFC File Offset: 0x00115FFC
		private static bool SellPrisonersCondition(MenuCallbackArgs args)
		{
			if (PartyBase.MainParty.PrisonRoster.Count > 0)
			{
				int ransomValueOfAllTransferablePrisoners = PlayerTownVisitCampaignBehavior.GetRansomValueOfAllTransferablePrisoners();
				if (ransomValueOfAllTransferablePrisoners > 0)
				{
					MBTextManager.SetTextVariable("RANSOM_AMOUNT", ransomValueOfAllTransferablePrisoners);
					args.optionLeaveType = GameMenuOption.LeaveType.Ransom;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003AF1 RID: 15089 RVA: 0x00117E3B File Offset: 0x0011603B
		private static bool SellPrisonerOneStackCondition(MenuCallbackArgs args)
		{
			if (PartyBase.MainParty.PrisonRoster.Count > 0)
			{
				args.optionLeaveType = GameMenuOption.LeaveType.Ransom;
				return true;
			}
			return false;
		}

		// Token: 0x06003AF2 RID: 15090 RVA: 0x00117E5C File Offset: 0x0011605C
		private static int GetRansomValueOfAllTransferablePrisoners()
		{
			int num = 0;
			List<string> list = Campaign.Current.GetCampaignBehavior<IViewDataTracker>().GetPartyPrisonerLocks().ToList<string>();
			foreach (TroopRosterElement troopRosterElement in PartyBase.MainParty.PrisonRoster.GetTroopRoster())
			{
				if (!list.Contains(troopRosterElement.Character.StringId))
				{
					num += Campaign.Current.Models.RansomValueCalculationModel.PrisonerRansomValue(troopRosterElement.Character, Hero.MainHero) * troopRosterElement.Number;
				}
			}
			return num;
		}

		// Token: 0x06003AF3 RID: 15091 RVA: 0x00117F08 File Offset: 0x00116108
		private static void ChooseRansomPrisoners()
		{
			GameMenu.SwitchToMenu("town_backstreet");
			PartyScreenManager.OpenScreenAsRansom();
		}

		// Token: 0x06003AF4 RID: 15092 RVA: 0x00117F19 File Offset: 0x00116119
		private static void SellAllTransferablePrisoners()
		{
			SellPrisonersAction.ApplyForSelectedPrisoners(PartyBase.MainParty, Settlement.CurrentSettlement.Party, MobilePartyHelper.GetPlayerPrisonersPlayerCanSell());
			GameMenu.SwitchToMenu("town_backstreet");
		}

		// Token: 0x06003AF5 RID: 15093 RVA: 0x00117F40 File Offset: 0x00116140
		private static bool game_menu_castle_go_to_lords_hall_on_condition(MenuCallbackArgs args)
		{
			SettlementAccessModel.AccessDetails accessDetails;
			Campaign.Current.Models.SettlementAccessModel.CanMainHeroEnterLordsHall(Settlement.CurrentSettlement, out accessDetails);
			if (accessDetails.AccessLevel == SettlementAccessModel.AccessLevel.NoAccess)
			{
				args.IsEnabled = false;
				PlayerTownVisitCampaignBehavior.SetLordsHallAccessLimitationReasonText(args, accessDetails);
			}
			else if (accessDetails.AccessLevel == SettlementAccessModel.AccessLevel.LimitedAccess && accessDetails.LimitedAccessSolution == SettlementAccessModel.LimitedAccessSolution.Bribe && Campaign.Current.Models.BribeCalculationModel.GetBribeToEnterLordsHall(Settlement.CurrentSettlement) > Hero.MainHero.Gold)
			{
				args.IsEnabled = false;
				args.Tooltip = new TextObject("{=d0kbtGYn}You don't have enough gold.", null);
			}
			List<Location> locations = Settlement.CurrentSettlement.LocationComplex.FindAll((string x) => x == "lordshall").ToList<Location>();
			MenuHelper.SetIssueAndQuestDataForLocations(args, locations);
			args.optionLeaveType = GameMenuOption.LeaveType.Mission;
			return true;
		}

		// Token: 0x06003AF6 RID: 15094 RVA: 0x00118014 File Offset: 0x00116214
		private static void SetLordsHallAccessLimitationReasonText(MenuCallbackArgs args, SettlementAccessModel.AccessDetails accessDetails)
		{
			SettlementAccessModel.AccessLimitationReason accessLimitationReason = accessDetails.AccessLimitationReason;
			if (accessLimitationReason == SettlementAccessModel.AccessLimitationReason.HostileFaction)
			{
				args.Tooltip = new TextObject("{=h9i9VXLd}You cannot enter an enemy lord's hall.", null);
				return;
			}
			if (accessLimitationReason != SettlementAccessModel.AccessLimitationReason.LocationEmpty)
			{
				Debug.FailedAssert(string.Format("{0} is not a valid no access reason for lord's hall", accessDetails.AccessLimitationReason), "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\CampaignBehaviors\\PlayerTownVisitCampaignBehavior.cs", "SetLordsHallAccessLimitationReasonText", 726);
				return;
			}
			args.Tooltip = new TextObject("{=cojKmfSk}There is no one inside.", null);
		}

		// Token: 0x06003AF7 RID: 15095 RVA: 0x00118080 File Offset: 0x00116280
		private static bool game_menu_town_keep_go_to_lords_hall_on_condition(MenuCallbackArgs args)
		{
			if (FactionManager.IsAtWarAgainstFaction(Settlement.CurrentSettlement.MapFaction, Hero.MainHero.MapFaction))
			{
				args.Tooltip = new TextObject("{=h9i9VXLd}You cannot enter an enemy lord's hall.", null);
				args.IsEnabled = false;
			}
			List<Location> locations = Settlement.CurrentSettlement.LocationComplex.FindAll((string x) => x == "lordshall").ToList<Location>();
			MenuHelper.SetIssueAndQuestDataForLocations(args, locations);
			args.optionLeaveType = GameMenuOption.LeaveType.Mission;
			return true;
		}

		// Token: 0x06003AF8 RID: 15096 RVA: 0x00118104 File Offset: 0x00116304
		private static bool game_menu_town_keep_bribe_pay_bribe_on_condition(MenuCallbackArgs args)
		{
			int bribeToEnterLordsHall = Campaign.Current.Models.BribeCalculationModel.GetBribeToEnterLordsHall(Settlement.CurrentSettlement);
			MBTextManager.SetTextVariable("AMOUNT", bribeToEnterLordsHall);
			List<Location> locations = Settlement.CurrentSettlement.LocationComplex.FindAll((string x) => x == "lordshall").ToList<Location>();
			MenuHelper.SetIssueAndQuestDataForLocations(args, locations);
			args.optionLeaveType = GameMenuOption.LeaveType.Mission;
			if (Hero.MainHero.Gold < bribeToEnterLordsHall)
			{
				args.Tooltip = new TextObject("{=d0kbtGYn}You don't have enough gold.", null);
				args.IsEnabled = false;
			}
			return bribeToEnterLordsHall > 0;
		}

		// Token: 0x06003AF9 RID: 15097 RVA: 0x001181A1 File Offset: 0x001163A1
		private static bool game_menu_castle_go_to_lords_hall_cheat_on_condition(MenuCallbackArgs args)
		{
			return Game.Current.IsDevelopmentMode;
		}

		// Token: 0x06003AFA RID: 15098 RVA: 0x001181AD File Offset: 0x001163AD
		private static void game_menu_castle_take_a_walk_around_the_castle_on_consequence(MenuCallbackArgs args)
		{
			LocationEncounter locationEncounter = PlayerEncounter.LocationEncounter;
			PlayerEncounter.LocationEncounter.CreateAndOpenMissionController(LocationComplex.Current.GetLocationWithId("center"), null, null, null);
		}

		// Token: 0x06003AFB RID: 15099 RVA: 0x001181D4 File Offset: 0x001163D4
		private static bool CheckAndOpenNextLocation(MenuCallbackArgs args)
		{
			if (Campaign.Current.GameMenuManager.NextLocation != null && GameStateManager.Current.ActiveState is MapState)
			{
				PlayerEncounter.LocationEncounter.CreateAndOpenMissionController(Campaign.Current.GameMenuManager.NextLocation, Campaign.Current.GameMenuManager.PreviousLocation, null, null);
				string stringId = Campaign.Current.GameMenuManager.NextLocation.StringId;
				if (!(stringId == "center"))
				{
					if (!(stringId == "tavern"))
					{
						if (!(stringId == "arena"))
						{
							if (stringId == "lordshall" || stringId == "prison")
							{
								Campaign.Current.GameMenuManager.SetNextMenu("town_keep");
							}
						}
						else
						{
							Campaign.Current.GameMenuManager.SetNextMenu("town_arena");
						}
					}
					else
					{
						Campaign.Current.GameMenuManager.SetNextMenu("town_backstreet");
					}
				}
				else if (Settlement.CurrentSettlement.IsCastle)
				{
					Campaign.Current.GameMenuManager.SetNextMenu("castle");
				}
				else if (Settlement.CurrentSettlement.IsTown)
				{
					Campaign.Current.GameMenuManager.SetNextMenu("town");
				}
				else if (Settlement.CurrentSettlement.IsVillage)
				{
					Campaign.Current.GameMenuManager.SetNextMenu("village");
				}
				else
				{
					Debug.FailedAssert("false", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\CampaignBehaviors\\PlayerTownVisitCampaignBehavior.cs", "CheckAndOpenNextLocation", 809);
				}
				Campaign.Current.GameMenuManager.NextLocation = null;
				Campaign.Current.GameMenuManager.PreviousLocation = null;
				return true;
			}
			return false;
		}

		// Token: 0x06003AFC RID: 15100 RVA: 0x00118384 File Offset: 0x00116584
		private static void game_menu_town_on_init(MenuCallbackArgs args)
		{
			PlayerTownVisitCampaignBehavior.SetIntroductionText(Settlement.CurrentSettlement, false);
			PlayerTownVisitCampaignBehavior.UpdateMenuLocations(args.MenuContext.GameMenu.StringId);
			Campaign.Current.autoEnterTown = null;
			Settlement currentSettlement = Settlement.CurrentSettlement;
			if (PlayerTownVisitCampaignBehavior.CheckAndOpenNextLocation(args))
			{
				return;
			}
			MobileParty garrisonParty = Settlement.CurrentSettlement.Town.GarrisonParty;
			if (garrisonParty != null && garrisonParty.MemberRoster.Count <= 0)
			{
				MobileParty garrisonParty2 = Settlement.CurrentSettlement.Town.GarrisonParty;
				if (garrisonParty2 != null && garrisonParty2.PrisonRoster.Count <= 0)
				{
					DestroyPartyAction.Apply(null, Settlement.CurrentSettlement.Town.GarrisonParty);
				}
			}
			args.MenuTitle = new TextObject("{=mVKcvY2U}Town Center", null);
		}

		// Token: 0x06003AFD RID: 15101 RVA: 0x00118444 File Offset: 0x00116644
		private static void UpdateMenuLocations(string menuID)
		{
			Campaign.Current.GameMenuManager.MenuLocations.Clear();
			Settlement settlement = (Settlement.CurrentSettlement == null) ? MobileParty.MainParty.CurrentSettlement : Settlement.CurrentSettlement;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(menuID);
			if (num <= 1579208614U)
			{
				if (num <= 1329296901U)
				{
					if (num != 864577349U)
					{
						if (num != 1192893027U)
						{
							if (num != 1329296901U)
							{
								goto IL_3D2;
							}
							if (!(menuID == "castle_lords_hall_bribe"))
							{
								goto IL_3D2;
							}
							return;
						}
						else
						{
							if (!(menuID == "village"))
							{
								goto IL_3D2;
							}
							Campaign.Current.GameMenuManager.MenuLocations.AddRange(settlement.LocationComplex.GetListOfLocations());
							return;
						}
					}
					else
					{
						if (!(menuID == "town"))
						{
							goto IL_3D2;
						}
						Campaign.Current.GameMenuManager.MenuLocations.Add(settlement.LocationComplex.GetLocationWithId("center"));
						Campaign.Current.GameMenuManager.MenuLocations.Add(settlement.LocationComplex.GetLocationWithId("arena"));
						Campaign.Current.GameMenuManager.MenuLocations.Add(settlement.LocationComplex.GetLocationWithId("house_1"));
						Campaign.Current.GameMenuManager.MenuLocations.Add(settlement.LocationComplex.GetLocationWithId("house_2"));
						Campaign.Current.GameMenuManager.MenuLocations.Add(settlement.LocationComplex.GetLocationWithId("house_3"));
						return;
					}
				}
				else if (num != 1470125011U)
				{
					if (num != 1556184416U)
					{
						if (num != 1579208614U)
						{
							goto IL_3D2;
						}
						if (!(menuID == "town_backstreet"))
						{
							goto IL_3D2;
						}
						Campaign.Current.GameMenuManager.MenuLocations.Add(settlement.LocationComplex.GetLocationWithId("tavern"));
						return;
					}
					else if (!(menuID == "castle_dungeon"))
					{
						goto IL_3D2;
					}
				}
				else
				{
					if (!(menuID == "town_keep"))
					{
						goto IL_3D2;
					}
					Campaign.Current.GameMenuManager.MenuLocations.Add(settlement.LocationComplex.GetLocationWithId("lordshall"));
					return;
				}
			}
			else if (num <= 2781132822U)
			{
				if (num != 1924483413U)
				{
					if (num != 2596447321U)
					{
						if (num != 2781132822U)
						{
							goto IL_3D2;
						}
						if (!(menuID == "town_keep_dungeon"))
						{
							goto IL_3D2;
						}
					}
					else
					{
						if (!(menuID == "castle"))
						{
							goto IL_3D2;
						}
						Campaign.Current.GameMenuManager.MenuLocations.Add(settlement.LocationComplex.GetLocationWithId("center"));
						Campaign.Current.GameMenuManager.MenuLocations.Add(settlement.LocationComplex.GetLocationWithId("lordshall"));
						return;
					}
				}
				else if (!(menuID == "town_enemy_town_keep"))
				{
					goto IL_3D2;
				}
			}
			else if (num != 4029725827U)
			{
				if (num != 4147432362U)
				{
					if (num != 4246693001U)
					{
						goto IL_3D2;
					}
					if (!(menuID == "town_arena"))
					{
						goto IL_3D2;
					}
					Campaign.Current.GameMenuManager.MenuLocations.Add(settlement.LocationComplex.GetLocationWithId("arena"));
					return;
				}
				else
				{
					if (!(menuID == "town_keep_bribe"))
					{
						goto IL_3D2;
					}
					return;
				}
			}
			else
			{
				if (!(menuID == "town_center"))
				{
					goto IL_3D2;
				}
				Campaign.Current.GameMenuManager.MenuLocations.Add(settlement.LocationComplex.GetLocationWithId("center"));
				Campaign.Current.GameMenuManager.MenuLocations.Add(settlement.LocationComplex.GetLocationWithId("arena"));
				return;
			}
			Campaign.Current.GameMenuManager.MenuLocations.Add(settlement.LocationComplex.GetLocationWithId("prison"));
			return;
			IL_3D2:
			Debug.FailedAssert("Could not get the associated locations for Game Menu: " + menuID, "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\CampaignBehaviors\\PlayerTownVisitCampaignBehavior.cs", "UpdateMenuLocations", 916);
			Campaign.Current.GameMenuManager.MenuLocations.AddRange(settlement.LocationComplex.GetListOfLocations());
		}

		// Token: 0x06003AFE RID: 15102 RVA: 0x00118861 File Offset: 0x00116A61
		private static void town_keep_on_init(MenuCallbackArgs args)
		{
			PlayerTownVisitCampaignBehavior.UpdateMenuLocations(args.MenuContext.GameMenu.StringId);
			if (PlayerTownVisitCampaignBehavior.CheckAndOpenNextLocation(args))
			{
				return;
			}
			PlayerTownVisitCampaignBehavior.SetIntroductionText(Settlement.CurrentSettlement, true);
			args.MenuTitle = new TextObject("{=723ig40Q}Keep", null);
		}

		// Token: 0x06003AFF RID: 15103 RVA: 0x001188A0 File Offset: 0x00116AA0
		private static void town_enemy_keep_on_init(MenuCallbackArgs args)
		{
			PlayerTownVisitCampaignBehavior.UpdateMenuLocations(args.MenuContext.GameMenu.StringId);
			if (PlayerTownVisitCampaignBehavior.CheckAndOpenNextLocation(args))
			{
				return;
			}
			PlayerTownVisitCampaignBehavior.SetIntroductionText(Settlement.CurrentSettlement, true);
			TextObject text = args.MenuContext.GameMenu.GetText();
			MobileParty garrisonParty = Settlement.CurrentSettlement.Town.GarrisonParty;
			bool flag = (garrisonParty != null && garrisonParty.PrisonRoster.TotalHeroes > 0) || Settlement.CurrentSettlement.Party.PrisonRoster.TotalHeroes > 0;
			text.SetTextVariable("SCOUT_KEEP_TEXT", flag ? "{=Tfb9LNAr}You have observed the comings and goings of the guards at the keep. You think you've identified a guard who might be approached and offered a bribe." : "{=qGUrhBpI}After spending time observing the keep and eavesdropping on the guards, you conclude that there are no prisoners here who you can liberate.");
			args.MenuTitle = new TextObject("{=723ig40Q}Keep", null);
		}

		// Token: 0x06003B00 RID: 15104 RVA: 0x00118954 File Offset: 0x00116B54
		private static void town_keep_dungeon_on_init(MenuCallbackArgs args)
		{
			PlayerTownVisitCampaignBehavior.UpdateMenuLocations(args.MenuContext.GameMenu.StringId);
			if (PlayerTownVisitCampaignBehavior.CheckAndOpenNextLocation(args))
			{
				return;
			}
			args.MenuTitle = new TextObject("{=x04UGQDn}Dungeon", null);
			TextObject textObject = TextObject.Empty;
			if (Settlement.CurrentSettlement.SettlementComponent.GetPrisonerHeroes().Count == 0)
			{
				textObject = new TextObject("{=O4flV28Q}There are no prisoners here.", null);
			}
			else
			{
				int count = Settlement.CurrentSettlement.SettlementComponent.GetPrisonerHeroes().Count;
				textObject = new TextObject("{=gAc8SWDt}There {?(PRISONER_COUNT > 1)}are {PRISONER_COUNT} prisoners{?}is 1 prisoner{\\?} here.", null);
				textObject.SetTextVariable("PRISONER_COUNT", count);
			}
			MBTextManager.SetTextVariable("PRISONER_INTRODUCTION", textObject, false);
		}

		// Token: 0x06003B01 RID: 15105 RVA: 0x001189F4 File Offset: 0x00116BF4
		private static void town_keep_bribe_on_init(MenuCallbackArgs args)
		{
			args.MenuTitle = new TextObject("{=723ig40Q}Keep", null);
			PlayerTownVisitCampaignBehavior.UpdateMenuLocations(args.MenuContext.GameMenu.StringId);
			if (Campaign.Current.Models.BribeCalculationModel.GetBribeToEnterLordsHall(Settlement.CurrentSettlement) == 0)
			{
				GameMenu.ActivateGameMenu("town_keep");
			}
		}

		// Token: 0x06003B02 RID: 15106 RVA: 0x00118A4C File Offset: 0x00116C4C
		private static void town_backstreet_on_init(MenuCallbackArgs args)
		{
			PlayerTownVisitCampaignBehavior.UpdateMenuLocations(args.MenuContext.GameMenu.StringId);
			string backgroundMeshName = Settlement.CurrentSettlement.Culture.StringId + "_tavern";
			args.MenuContext.SetBackgroundMeshName(backgroundMeshName);
			if (PlayerTownVisitCampaignBehavior.CheckAndOpenNextLocation(args))
			{
				return;
			}
			args.MenuTitle = new TextObject("{=a0MVffcN}Backstreet", null);
		}

		// Token: 0x06003B03 RID: 15107 RVA: 0x00118AB0 File Offset: 0x00116CB0
		private static void town_arena_on_init(MenuCallbackArgs args)
		{
			PlayerTownVisitCampaignBehavior.UpdateMenuLocations(args.MenuContext.GameMenu.StringId);
			if (Settlement.CurrentSettlement != null && Settlement.CurrentSettlement.IsTown && Campaign.Current.TournamentManager.GetTournamentGame(Settlement.CurrentSettlement.Town) != null && Campaign.Current.IsDay)
			{
				TextObject text = GameTexts.FindText("str_town_new_tournament_text", null);
				MBTextManager.SetTextVariable("ADDITIONAL_STRING", text, false);
			}
			else
			{
				TextObject text2 = GameTexts.FindText("str_town_empty_arena_text", null);
				MBTextManager.SetTextVariable("ADDITIONAL_STRING", text2, false);
			}
			if (PlayerTownVisitCampaignBehavior.CheckAndOpenNextLocation(args))
			{
				return;
			}
			args.MenuTitle = new TextObject("{=mMU3H6HZ}Arena", null);
		}

		// Token: 0x06003B04 RID: 15108 RVA: 0x00118B58 File Offset: 0x00116D58
		public static bool game_menu_town_manage_town_on_condition(MenuCallbackArgs args)
		{
			args.optionLeaveType = GameMenuOption.LeaveType.Manage;
			Settlement currentSettlement = Settlement.CurrentSettlement;
			bool shouldBeDisabled;
			TextObject disabledText;
			return MenuHelper.SetOptionProperties(args, Campaign.Current.Models.SettlementAccessModel.CanMainHeroDoSettlementAction(currentSettlement, SettlementAccessModel.SettlementAction.ManageTown, out shouldBeDisabled, out disabledText), shouldBeDisabled, disabledText);
		}

		// Token: 0x06003B05 RID: 15109 RVA: 0x00118B95 File Offset: 0x00116D95
		public static bool game_menu_town_manage_town_cheat_on_condition(MenuCallbackArgs args)
		{
			args.optionLeaveType = GameMenuOption.LeaveType.Manage;
			return GameManagerBase.Current.IsDevelopmentMode && Settlement.CurrentSettlement.IsTown && Settlement.CurrentSettlement.OwnerClan.Leader != Hero.MainHero;
		}

		// Token: 0x06003B06 RID: 15110 RVA: 0x00118BD4 File Offset: 0x00116DD4
		private static bool game_menu_town_keep_open_stash_on_condition(MenuCallbackArgs args)
		{
			args.optionLeaveType = GameMenuOption.LeaveType.OpenStash;
			return Settlement.CurrentSettlement.OwnerClan == Clan.PlayerClan && !Settlement.CurrentSettlement.Town.IsOwnerUnassigned;
		}

		// Token: 0x06003B07 RID: 15111 RVA: 0x00118C03 File Offset: 0x00116E03
		private static void game_menu_town_keep_open_stash_on_consequence(MenuCallbackArgs args)
		{
			InventoryManager.OpenScreenAsStash(Settlement.CurrentSettlement.Stash);
		}

		// Token: 0x06003B08 RID: 15112 RVA: 0x00118C14 File Offset: 0x00116E14
		private static bool game_menu_manage_garrison_on_condition(MenuCallbackArgs args)
		{
			args.optionLeaveType = GameMenuOption.LeaveType.ManageGarrison;
			Settlement currentSettlement = Settlement.CurrentSettlement;
			return currentSettlement.OwnerClan == Clan.PlayerClan && currentSettlement.MapFaction == Hero.MainHero.MapFaction && currentSettlement.IsFortification;
		}

		// Token: 0x06003B09 RID: 15113 RVA: 0x00118C56 File Offset: 0x00116E56
		private static bool game_menu_manage_castle_on_condition(MenuCallbackArgs args)
		{
			args.optionLeaveType = GameMenuOption.LeaveType.Manage;
			return Settlement.CurrentSettlement.OwnerClan == Clan.PlayerClan && Settlement.CurrentSettlement.IsCastle;
		}

		// Token: 0x06003B0A RID: 15114 RVA: 0x00118C80 File Offset: 0x00116E80
		private static void game_menu_manage_garrison_on_consequence(MenuCallbackArgs args)
		{
			Settlement currentSettlement = Hero.MainHero.CurrentSettlement;
			if (currentSettlement.Town.GarrisonParty == null)
			{
				currentSettlement.AddGarrisonParty(false);
			}
			PartyScreenManager.OpenScreenAsManageTroops(currentSettlement.Town.GarrisonParty);
		}

		// Token: 0x06003B0B RID: 15115 RVA: 0x00118CBC File Offset: 0x00116EBC
		private static bool game_menu_leave_troops_garrison_on_condition(MenuCallbackArgs args)
		{
			args.optionLeaveType = GameMenuOption.LeaveType.DonateTroops;
			Settlement currentSettlement = Settlement.CurrentSettlement;
			return currentSettlement.OwnerClan != Clan.PlayerClan && currentSettlement.MapFaction == Hero.MainHero.MapFaction && currentSettlement.IsFortification && (currentSettlement.Town.GarrisonParty == null || currentSettlement.Town.GarrisonParty.Party.PartySizeLimit > currentSettlement.Town.GarrisonParty.Party.NumberOfAllMembers);
		}

		// Token: 0x06003B0C RID: 15116 RVA: 0x00118D3B File Offset: 0x00116F3B
		private static void game_menu_leave_troops_garrison_on_consequece(MenuCallbackArgs args)
		{
			PartyScreenManager.OpenScreenAsDonateGarrisonWithCurrentSettlement();
		}

		// Token: 0x06003B0D RID: 15117 RVA: 0x00118D44 File Offset: 0x00116F44
		private static bool game_menu_town_town_streets_on_condition(MenuCallbackArgs args)
		{
			bool shouldBeDisabled;
			TextObject disabledText;
			bool canPlayerDo = Campaign.Current.Models.SettlementAccessModel.CanMainHeroAccessLocation(Settlement.CurrentSettlement, "center", out shouldBeDisabled, out disabledText);
			List<Location> locations = Settlement.CurrentSettlement.LocationComplex.FindAll((string x) => x == "center").ToList<Location>();
			MenuHelper.SetIssueAndQuestDataForLocations(args, locations);
			args.optionLeaveType = GameMenuOption.LeaveType.Mission;
			return MenuHelper.SetOptionProperties(args, canPlayerDo, shouldBeDisabled, disabledText);
		}

		// Token: 0x06003B0E RID: 15118 RVA: 0x00118DBF File Offset: 0x00116FBF
		private static void game_menu_town_town_streets_on_consequence(MenuCallbackArgs args)
		{
			LocationEncounter locationEncounter = PlayerEncounter.LocationEncounter;
			PlayerEncounter.LocationEncounter.CreateAndOpenMissionController(LocationComplex.Current.GetLocationWithId("center"), null, null, null);
		}

		// Token: 0x06003B0F RID: 15119 RVA: 0x00118DE4 File Offset: 0x00116FE4
		private static void game_menu_town_lordshall_on_consequence(MenuCallbackArgs args)
		{
			LocationEncounter locationEncounter = PlayerEncounter.LocationEncounter;
			PlayerTownVisitCampaignBehavior.OpenMissionWithSettingPreviousLocation("center", "lordshall");
		}

		// Token: 0x06003B10 RID: 15120 RVA: 0x00118DFC File Offset: 0x00116FFC
		private static void game_menu_castle_lordshall_on_consequence(MenuCallbackArgs args)
		{
			SettlementAccessModel.AccessDetails accessDetails;
			Campaign.Current.Models.SettlementAccessModel.CanMainHeroEnterLordsHall(Settlement.CurrentSettlement, out accessDetails);
			if (accessDetails.AccessLevel == SettlementAccessModel.AccessLevel.FullAccess)
			{
				PlayerTownVisitCampaignBehavior.OpenMissionWithSettingPreviousLocation("center", "lordshall");
				return;
			}
			if (accessDetails.AccessLevel != SettlementAccessModel.AccessLevel.LimitedAccess || accessDetails.LimitedAccessSolution != SettlementAccessModel.LimitedAccessSolution.Bribe)
			{
				Debug.FailedAssert("invalid LimitedAccessSolution or AccessLevel for castle lord's hall", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\CampaignBehaviors\\PlayerTownVisitCampaignBehavior.cs", "game_menu_castle_lordshall_on_consequence", 1182);
				return;
			}
			if (Campaign.Current.Models.BribeCalculationModel.GetBribeToEnterLordsHall(Settlement.CurrentSettlement) > 0)
			{
				GameMenu.SwitchToMenu("castle_lords_hall_bribe");
				return;
			}
			PlayerTownVisitCampaignBehavior.OpenMissionWithSettingPreviousLocation("center", "lordshall");
		}

		// Token: 0x06003B11 RID: 15121 RVA: 0x00118EA0 File Offset: 0x001170A0
		private static void game_menu_town_keep_bribe_pay_bribe_on_consequence(MenuCallbackArgs args)
		{
			int bribeToEnterLordsHall = Campaign.Current.Models.BribeCalculationModel.GetBribeToEnterLordsHall(Settlement.CurrentSettlement);
			BribeGuardsAction.Apply(Settlement.CurrentSettlement, bribeToEnterLordsHall);
			LocationEncounter locationEncounter = PlayerEncounter.LocationEncounter;
			GameMenu.ActivateGameMenu("town_keep");
		}

		// Token: 0x06003B12 RID: 15122 RVA: 0x00118EE4 File Offset: 0x001170E4
		private static void game_menu_castle_lordshall_bribe_on_consequence(MenuCallbackArgs args)
		{
			int bribeToEnterLordsHall = Campaign.Current.Models.BribeCalculationModel.GetBribeToEnterLordsHall(Settlement.CurrentSettlement);
			BribeGuardsAction.Apply(Settlement.CurrentSettlement, bribeToEnterLordsHall);
			PlayerTownVisitCampaignBehavior.OpenMissionWithSettingPreviousLocation("center", "lordshall");
		}

		// Token: 0x06003B13 RID: 15123 RVA: 0x00118F25 File Offset: 0x00117125
		private static void game_menu_lordshall_cheat_on_consequence(MenuCallbackArgs args)
		{
			PlayerTownVisitCampaignBehavior.OpenMissionWithSettingPreviousLocation("center", "lordshall");
		}

		// Token: 0x06003B14 RID: 15124 RVA: 0x00118F36 File Offset: 0x00117136
		private static void game_menu_dungeon_cheat_on_consequence(MenuCallbackArgs ARGS)
		{
			GameMenu.SwitchToMenu("castle_dungeon");
		}

		// Token: 0x06003B15 RID: 15125 RVA: 0x00118F42 File Offset: 0x00117142
		private static void game_menu_town_dungeon_on_consequence(MenuCallbackArgs args)
		{
			LocationEncounter locationEncounter = PlayerEncounter.LocationEncounter;
			PlayerTownVisitCampaignBehavior.OpenMissionWithSettingPreviousLocation("center", "prison");
		}

		// Token: 0x06003B16 RID: 15126 RVA: 0x00118F59 File Offset: 0x00117159
		private static void game_menu_castle_dungeon_on_consequence(MenuCallbackArgs args)
		{
			PlayerTownVisitCampaignBehavior.OpenMissionWithSettingPreviousLocation("center", "prison");
		}

		// Token: 0x06003B17 RID: 15127 RVA: 0x00118F6A File Offset: 0x0011716A
		private static void game_menu_keep_dungeon_on_consequence(MenuCallbackArgs args)
		{
			GameMenu.SwitchToMenu("castle_dungeon");
		}

		// Token: 0x06003B18 RID: 15128 RVA: 0x00118F76 File Offset: 0x00117176
		private static void game_menu_town_town_tavern_on_consequence(MenuCallbackArgs args)
		{
			LocationEncounter locationEncounter = PlayerEncounter.LocationEncounter;
			PlayerTownVisitCampaignBehavior.OpenMissionWithSettingPreviousLocation("center", "tavern");
		}

		// Token: 0x06003B19 RID: 15129 RVA: 0x00118F8D File Offset: 0x0011718D
		private static void game_menu_town_town_arena_on_consequence(MenuCallbackArgs args)
		{
			LocationEncounter locationEncounter = PlayerEncounter.LocationEncounter;
			PlayerTownVisitCampaignBehavior.OpenMissionWithSettingPreviousLocation("center", "arena");
		}

		// Token: 0x06003B1A RID: 15130 RVA: 0x00118FA4 File Offset: 0x001171A4
		private static void game_menu_town_town_market_on_consequence(MenuCallbackArgs args)
		{
			LocationEncounter locationEncounter = PlayerEncounter.LocationEncounter;
			InventoryManager.OpenScreenAsTrade(Settlement.CurrentSettlement.ItemRoster, Settlement.CurrentSettlement.Town, InventoryManager.InventoryCategoryType.None, null);
		}

		// Token: 0x06003B1B RID: 15131 RVA: 0x00118FC7 File Offset: 0x001171C7
		private static bool game_menu_town_town_leave_on_condition(MenuCallbackArgs args)
		{
			args.optionLeaveType = GameMenuOption.LeaveType.Leave;
			return MobileParty.MainParty.Army == null || MobileParty.MainParty.Army.LeaderParty == MobileParty.MainParty;
		}

		// Token: 0x06003B1C RID: 15132 RVA: 0x00118FF5 File Offset: 0x001171F5
		private static void game_menu_settlement_leave_on_consequence(MenuCallbackArgs args)
		{
			PlayerEncounter.LeaveSettlement();
			PlayerEncounter.Finish(true);
			Campaign.Current.SaveHandler.SignalAutoSave();
		}

		// Token: 0x06003B1D RID: 15133 RVA: 0x00119014 File Offset: 0x00117214
		private static void settlement_wait_on_init(MenuCallbackArgs args)
		{
			TextObject text = args.MenuContext.GameMenu.GetText();
			TextObject variable = Hero.MainHero.IsPrisoner ? Hero.MainHero.PartyBelongedToAsPrisoner.Settlement.Name : Settlement.CurrentSettlement.Name;
			int captiveTimeInDays = PlayerCaptivity.CaptiveTimeInDays;
			TextObject textObject;
			if (captiveTimeInDays == 0)
			{
				textObject = GameTexts.FindText("str_prisoner_of_settlement_menu_text", null);
			}
			else
			{
				textObject = GameTexts.FindText("str_prisoner_of_settlement_for_days_menu_text", null);
				textObject.SetTextVariable("NUMBER_OF_DAYS", captiveTimeInDays);
				textObject.SetTextVariable("PLURAL", (captiveTimeInDays > 1) ? 1 : 0);
			}
			textObject.SetTextVariable("SETTLEMENT_NAME", variable);
			text.SetTextVariable("CAPTIVITY_TEXT", textObject);
		}

		// Token: 0x06003B1E RID: 15134 RVA: 0x001190BC File Offset: 0x001172BC
		private static void game_menu_village_on_init(MenuCallbackArgs args)
		{
			PlayerTownVisitCampaignBehavior.SetIntroductionText(Settlement.CurrentSettlement, false);
			PlayerTownVisitCampaignBehavior.UpdateMenuLocations(args.MenuContext.GameMenu.StringId);
			SettlementAccessModel settlementAccessModel = Campaign.Current.Models.SettlementAccessModel;
			Settlement currentSettlement = Settlement.CurrentSettlement;
			SettlementAccessModel.AccessDetails accessDetails;
			settlementAccessModel.CanMainHeroEnterSettlement(currentSettlement, out accessDetails);
			if (currentSettlement != null)
			{
				Village village = currentSettlement.Village;
				if (accessDetails.AccessLevel == SettlementAccessModel.AccessLevel.NoAccess && accessDetails.AccessLimitationReason == SettlementAccessModel.AccessLimitationReason.VillageIsLooted)
				{
					GameMenu.SwitchToMenu("village_looted");
				}
			}
			args.MenuTitle = new TextObject("{=Ua6CNLBZ}Village", null);
		}

		// Token: 0x06003B1F RID: 15135 RVA: 0x0011913C File Offset: 0x0011733C
		private static void game_menu_castle_on_init(MenuCallbackArgs args)
		{
			MobileParty garrisonParty = Settlement.CurrentSettlement.Town.GarrisonParty;
			if (garrisonParty != null && garrisonParty.MemberRoster.Count <= 0)
			{
				DestroyPartyAction.Apply(null, Settlement.CurrentSettlement.Town.GarrisonParty);
			}
			PlayerTownVisitCampaignBehavior.SetIntroductionText(Settlement.CurrentSettlement, true);
			PlayerTownVisitCampaignBehavior.UpdateMenuLocations(args.MenuContext.GameMenu.StringId);
			if (Campaign.Current.GameMenuManager.NextLocation != null)
			{
				PlayerEncounter.LocationEncounter.CreateAndOpenMissionController(Campaign.Current.GameMenuManager.NextLocation, Campaign.Current.GameMenuManager.PreviousLocation, null, null);
				Campaign.Current.GameMenuManager.NextLocation = null;
				Campaign.Current.GameMenuManager.PreviousLocation = null;
			}
			args.MenuTitle = new TextObject("{=sVXa3zFx}Castle", null);
		}

		// Token: 0x06003B20 RID: 15136 RVA: 0x00119214 File Offset: 0x00117414
		private static bool game_menu_castle_lordshall_bribe_pay_bribe_on_condition(MenuCallbackArgs args)
		{
			int bribeToEnterLordsHall = Campaign.Current.Models.BribeCalculationModel.GetBribeToEnterLordsHall(Settlement.CurrentSettlement);
			MBTextManager.SetTextVariable("AMOUNT", bribeToEnterLordsHall);
			List<Location> locations = Settlement.CurrentSettlement.LocationComplex.FindAll((string x) => x == "lordshall").ToList<Location>();
			MenuHelper.SetIssueAndQuestDataForLocations(args, locations);
			args.optionLeaveType = GameMenuOption.LeaveType.Mission;
			if (Hero.MainHero.Gold < bribeToEnterLordsHall)
			{
				args.Tooltip = new TextObject("{=d0kbtGYn}You don't have enough gold.", null);
				args.IsEnabled = false;
			}
			return bribeToEnterLordsHall > 0;
		}

		// Token: 0x06003B21 RID: 15137 RVA: 0x001192B4 File Offset: 0x001174B4
		private static bool game_menu_village_village_center_on_condition(MenuCallbackArgs args)
		{
			List<Location> locations = Settlement.CurrentSettlement.LocationComplex.GetListOfLocations().ToList<Location>();
			MenuHelper.SetIssueAndQuestDataForLocations(args, locations);
			args.optionLeaveType = GameMenuOption.LeaveType.Mission;
			return Settlement.CurrentSettlement.Village.VillageState == Village.VillageStates.Normal;
		}

		// Token: 0x06003B22 RID: 15138 RVA: 0x001192F6 File Offset: 0x001174F6
		private static void game_menu_village_village_center_on_consequence(MenuCallbackArgs args)
		{
			(PlayerEncounter.LocationEncounter as VillageEncounter).CreateAndOpenMissionController(LocationComplex.Current.GetLocationWithId("village_center"), null, null, null);
		}

		// Token: 0x06003B23 RID: 15139 RVA: 0x0011931C File Offset: 0x0011751C
		private static bool game_menu_village_buy_good_on_condition(MenuCallbackArgs args)
		{
			Village village = Settlement.CurrentSettlement.Village;
			if (village.VillageState == Village.VillageStates.BeingRaided)
			{
				return false;
			}
			args.optionLeaveType = GameMenuOption.LeaveType.Trade;
			if (village.VillageState == Village.VillageStates.Normal && village.Owner.ItemRoster.Count > 0)
			{
				foreach (ValueTuple<ItemObject, float> valueTuple in village.VillageType.Productions)
				{
				}
				return true;
			}
			if (village.Gold > 0)
			{
				args.Tooltip = new TextObject("{=FbowXAC0}There are no available products right now.", null);
				return true;
			}
			args.IsEnabled = false;
			args.Tooltip = new TextObject("{=bmfo7CaO}Village shop is not available right now.", null);
			return true;
		}

		// Token: 0x06003B24 RID: 15140 RVA: 0x001193E0 File Offset: 0x001175E0
		private static void game_menu_recruit_volunteers_on_consequence(MenuCallbackArgs args)
		{
		}

		// Token: 0x06003B25 RID: 15141 RVA: 0x001193E2 File Offset: 0x001175E2
		private static bool game_menu_recruit_volunteers_on_condition(MenuCallbackArgs args)
		{
			args.optionLeaveType = GameMenuOption.LeaveType.Recruit;
			return !Settlement.CurrentSettlement.IsVillage || Settlement.CurrentSettlement.Village.VillageState == Village.VillageStates.Normal;
		}

		// Token: 0x06003B26 RID: 15142 RVA: 0x0011940C File Offset: 0x0011760C
		private static bool game_menu_village_wait_on_condition(MenuCallbackArgs args)
		{
			Village village = Settlement.CurrentSettlement.Village;
			args.optionLeaveType = GameMenuOption.LeaveType.Wait;
			return village.VillageState == Village.VillageStates.Normal;
		}

		// Token: 0x06003B27 RID: 15143 RVA: 0x00119428 File Offset: 0x00117628
		private static bool game_menu_town_wait_on_condition(MenuCallbackArgs args)
		{
			args.optionLeaveType = GameMenuOption.LeaveType.Wait;
			MBTextManager.SetTextVariable("CURRENT_SETTLEMENT", Settlement.CurrentSettlement.EncyclopediaLinkWithName, false);
			return true;
		}

		// Token: 0x06003B28 RID: 15144 RVA: 0x00119448 File Offset: 0x00117648
		public static void settlement_player_unconscious_continue_on_consequence(MenuCallbackArgs args)
		{
			Settlement currentSettlement = MobileParty.MainParty.CurrentSettlement;
			GameMenu.SwitchToMenu(currentSettlement.IsVillage ? "village" : (currentSettlement.IsCastle ? "castle" : "town"));
		}

		// Token: 0x06003B29 RID: 15145 RVA: 0x00119488 File Offset: 0x00117688
		private static void SetIntroductionText(Settlement settlement, bool fromKeep)
		{
			TextObject textObject = new TextObject("", null);
			if (settlement.IsTown && !fromKeep)
			{
				if (settlement.OwnerClan == Clan.PlayerClan)
				{
					textObject = new TextObject("{=kXVHwjoV}You have arrived at your fief of {SETTLEMENT_LINK}. {PROSPERITY_INFO} {MORALE_INFO}", null);
				}
				else
				{
					textObject = new TextObject("{=UWzQsHA2}{SETTLEMENT_LINK} is governed by {LORD.LINK}, {FACTION_OFFICIAL} of the {FACTION_TERM}. {PROSPERITY_INFO} {MORALE_INFO}", null);
				}
			}
			else if (settlement.IsTown && fromKeep)
			{
				if (settlement.OwnerClan == Clan.PlayerClan)
				{
					textObject = new TextObject("{=u0Dc5g4Z}You are in your town of {SETTLEMENT_LINK}. {KEEP_INFO}", null);
				}
				else
				{
					textObject = new TextObject("{=q3wD0rbq}{SETTLEMENT_LINK} is governed by {LORD.LINK}, {FACTION_OFFICIAL} of the {FACTION_TERM}. {KEEP_INFO}", null);
				}
			}
			else if (settlement.IsCastle)
			{
				if (settlement.OwnerClan == Clan.PlayerClan)
				{
					textObject = new TextObject("{=dA8RGoQ1}You have arrived at {SETTLEMENT_LINK}. {KEEP_INFO}", null);
				}
				else
				{
					textObject = new TextObject("{=4pmvrnmN}The castle of {SETTLEMENT_LINK} is owned by {LORD.LINK}, {FACTION_OFFICIAL} of the {FACTION_TERM}. {KEEP_INFO}", null);
				}
			}
			else if (settlement.IsVillage)
			{
				if (settlement.OwnerClan == Clan.PlayerClan)
				{
					textObject = new TextObject("{=M5iR1e5h}You have arrived at your fief of {SETTLEMENT_LINK}. {PROSPERITY_INFO}", null);
				}
				else
				{
					textObject = new TextObject("{=RVDojUOM}The lands around {SETTLEMENT_LINK} are owned mostly by {LORD.LINK}, {FACTION_OFFICIAL} of the {FACTION_TERM}. {PROSPERITY_INFO}", null);
				}
			}
			settlement.OwnerClan.Leader.SetPropertiesToTextObject(textObject, "LORD");
			string text = settlement.OwnerClan.Leader.MapFaction.Culture.StringId;
			if (settlement.OwnerClan.Leader.IsFemale)
			{
				text += "_f";
			}
			if (settlement.OwnerClan.Leader == Hero.MainHero && !Hero.MainHero.MapFaction.IsKingdomFaction)
			{
				textObject.SetTextVariable("FACTION_TERM", Hero.MainHero.Clan.EncyclopediaLinkWithName);
				textObject.SetTextVariable("FACTION_OFFICIAL", new TextObject("{=hb30yQPN}leader", null));
			}
			else
			{
				textObject.SetTextVariable("FACTION_TERM", settlement.MapFaction.EncyclopediaLinkWithName);
				if (settlement.OwnerClan.MapFaction.IsKingdomFaction && settlement.OwnerClan.Leader == settlement.OwnerClan.Leader.MapFaction.Leader)
				{
					textObject.SetTextVariable("FACTION_OFFICIAL", GameTexts.FindText("str_faction_ruler", text));
				}
				else
				{
					textObject.SetTextVariable("FACTION_OFFICIAL", GameTexts.FindText("str_faction_official", text));
				}
			}
			textObject.SetTextVariable("SETTLEMENT_LINK", settlement.EncyclopediaLinkWithName);
			settlement.SetPropertiesToTextObject(textObject, "SETTLEMENT_OBJECT");
			string variation = settlement.SettlementComponent.GetProsperityLevel().ToString();
			if ((settlement.IsTown && settlement.Town.InRebelliousState) || (settlement.IsVillage && settlement.Village.Bound.Town.InRebelliousState))
			{
				textObject.SetTextVariable("PROSPERITY_INFO", GameTexts.FindText("str_settlement_rebellion", null));
				textObject.SetTextVariable("MORALE_INFO", "");
			}
			else if (settlement.IsTown)
			{
				textObject.SetTextVariable("PROSPERITY_INFO", GameTexts.FindText("str_town_long_prosperity_1", variation));
				textObject.SetTextVariable("MORALE_INFO", PlayerTownVisitCampaignBehavior.SetTownMoraleText(settlement));
			}
			else if (settlement.IsVillage)
			{
				textObject.SetTextVariable("PROSPERITY_INFO", GameTexts.FindText("str_village_long_prosperity", variation));
			}
			textObject.SetTextVariable("KEEP_INFO", "");
			if (fromKeep && LocationComplex.Current != null)
			{
				if (!LocationComplex.Current.GetLocationWithId("lordshall").GetCharacterList().Any((LocationCharacter x) => x.Character.IsHero))
				{
					textObject.SetTextVariable("KEEP_INFO", "{=OgkSLkFi}There is nobody in the lord's hall.");
				}
			}
			MBTextManager.SetTextVariable("SETTLEMENT_INFO", textObject, false);
		}

		// Token: 0x06003B2A RID: 15146 RVA: 0x001197EC File Offset: 0x001179EC
		private static TextObject SetTownMoraleText(Settlement settlement)
		{
			SettlementComponent.ProsperityLevel prosperityLevel = settlement.SettlementComponent.GetProsperityLevel();
			string id;
			if (settlement.Town.Loyalty < 25f)
			{
				if (prosperityLevel <= SettlementComponent.ProsperityLevel.Low)
				{
					id = "str_settlement_morale_rebellious_adversity";
				}
				else if (prosperityLevel <= SettlementComponent.ProsperityLevel.Mid)
				{
					id = "str_settlement_morale_rebellious_average";
				}
				else
				{
					id = "str_settlement_morale_rebellious_prosperity";
				}
			}
			else if (settlement.Town.Loyalty < 65f)
			{
				if (prosperityLevel <= SettlementComponent.ProsperityLevel.Low)
				{
				}
				if (prosperityLevel <= SettlementComponent.ProsperityLevel.Mid)
				{
					id = "str_settlement_morale_medium_average";
				}
				else
				{
					id = "str_settlement_morale_medium_prosperity";
				}
			}
			else if (prosperityLevel <= SettlementComponent.ProsperityLevel.Low)
			{
				id = "str_settlement_morale_high_adversity";
			}
			else if (prosperityLevel <= SettlementComponent.ProsperityLevel.Mid)
			{
				id = "str_settlement_morale_high_average";
			}
			else
			{
				id = "str_settlement_morale_high_prosperity";
			}
			return GameTexts.FindText(id, null);
		}

		// Token: 0x06003B2B RID: 15147 RVA: 0x00119892 File Offset: 0x00117A92
		[GameMenuInitializationHandler("town_guard")]
		[GameMenuInitializationHandler("menu_tournament_withdraw_verify")]
		[GameMenuInitializationHandler("menu_tournament_bet_confirm")]
		[GameMenuInitializationHandler("town_castle_not_enough_bribe")]
		[GameMenuInitializationHandler("settlement_player_unconscious")]
		[GameMenuInitializationHandler("castle")]
		[GameMenuInitializationHandler("town_castle_nobody_inside")]
		[GameMenuInitializationHandler("encounter_interrupted")]
		[GameMenuInitializationHandler("encounter_interrupted_siege_preparations")]
		[GameMenuInitializationHandler("castle_dungeon")]
		[GameMenuInitializationHandler("encounter_interrupted_raid_started")]
		public static void game_menu_town_menu_on_init(MenuCallbackArgs args)
		{
			args.MenuContext.SetBackgroundMeshName(Settlement.CurrentSettlement.SettlementComponent.WaitMeshName);
		}

		// Token: 0x06003B2C RID: 15148 RVA: 0x001198B0 File Offset: 0x00117AB0
		[GameMenuInitializationHandler("town_arena")]
		public static void game_menu_town_menu_arena_on_init(MenuCallbackArgs args)
		{
			string backgroundMeshName = Settlement.CurrentSettlement.Culture.StringId + "_arena";
			args.MenuContext.SetBackgroundMeshName(backgroundMeshName);
			args.MenuContext.SetAmbientSound("event:/map/ambient/node/settlements/2d/arena");
		}

		// Token: 0x06003B2D RID: 15149 RVA: 0x001198F4 File Offset: 0x00117AF4
		[GameMenuInitializationHandler("village_hostile_action")]
		[GameMenuInitializationHandler("force_volunteers_village")]
		[GameMenuInitializationHandler("force_supplies_village")]
		[GameMenuInitializationHandler("raid_village_no_resist_warn_player")]
		[GameMenuInitializationHandler("raid_village_resisted")]
		[GameMenuInitializationHandler("village_loot_no_resist")]
		[GameMenuInitializationHandler("village_take_food_confirm")]
		[GameMenuInitializationHandler("village_press_into_service_confirm")]
		[GameMenuInitializationHandler("menu_press_into_service_success")]
		[GameMenuInitializationHandler("menu_village_take_food_success")]
		public static void game_menu_village_menu_on_init(MenuCallbackArgs args)
		{
			Village village = Settlement.CurrentSettlement.Village;
			args.MenuContext.SetBackgroundMeshName(village.WaitMeshName);
		}

		// Token: 0x06003B2E RID: 15150 RVA: 0x00119920 File Offset: 0x00117B20
		[GameMenuInitializationHandler("town_keep")]
		public static void game_menu_town_menu_keep_on_init(MenuCallbackArgs args)
		{
			string backgroundMeshName = Settlement.CurrentSettlement.Culture.StringId + "_keep";
			args.MenuContext.SetBackgroundMeshName(backgroundMeshName);
			args.MenuContext.SetPanelSound("event:/ui/panels/settlement_keep");
			args.MenuContext.SetAmbientSound("event:/map/ambient/node/settlements/2d/keep");
		}

		// Token: 0x06003B2F RID: 15151 RVA: 0x00119973 File Offset: 0x00117B73
		[GameMenuEventHandler("town", "manage_production", GameMenuEventHandler.EventType.OnConsequence)]
		public static void game_menu_ui_town_manage_town_on_consequence(MenuCallbackArgs args)
		{
			args.MenuContext.OpenTownManagement();
		}

		// Token: 0x06003B30 RID: 15152 RVA: 0x00119980 File Offset: 0x00117B80
		[GameMenuEventHandler("town_keep", "manage_production", GameMenuEventHandler.EventType.OnConsequence)]
		public static void game_menu_ui_town_castle_manage_town_on_consequence(MenuCallbackArgs args)
		{
			args.MenuContext.OpenTownManagement();
		}

		// Token: 0x06003B31 RID: 15153 RVA: 0x0011998D File Offset: 0x00117B8D
		[GameMenuEventHandler("village", "trade", GameMenuEventHandler.EventType.OnConsequence)]
		private static void game_menu_ui_village_buy_good_on_consequence(MenuCallbackArgs args)
		{
			InventoryManager.OpenScreenAsTrade(Settlement.CurrentSettlement.ItemRoster, Settlement.CurrentSettlement.Village, InventoryManager.InventoryCategoryType.None, null);
		}

		// Token: 0x06003B32 RID: 15154 RVA: 0x001199AA File Offset: 0x00117BAA
		[GameMenuEventHandler("village", "manage_production", GameMenuEventHandler.EventType.OnConsequence)]
		private static void game_menu_ui_village_manage_village_on_consequence(MenuCallbackArgs args)
		{
			args.MenuContext.OpenTownManagement();
		}

		// Token: 0x06003B33 RID: 15155 RVA: 0x001199B7 File Offset: 0x00117BB7
		[GameMenuEventHandler("village", "recruit_volunteers", GameMenuEventHandler.EventType.OnConsequence)]
		[GameMenuEventHandler("town_backstreet", "recruit_volunteers", GameMenuEventHandler.EventType.OnConsequence)]
		[GameMenuEventHandler("town", "recruit_volunteers", GameMenuEventHandler.EventType.OnConsequence)]
		private static void game_menu_ui_recruit_volunteers_on_consequence(MenuCallbackArgs args)
		{
			args.MenuContext.OpenRecruitVolunteers();
		}

		// Token: 0x06003B34 RID: 15156 RVA: 0x001199C4 File Offset: 0x00117BC4
		[GameMenuInitializationHandler("prisoner_wait")]
		private static void wait_menu_ui_prisoner_wait_on_init(MenuCallbackArgs args)
		{
			if (Hero.MainHero.IsFemale)
			{
				args.MenuContext.SetBackgroundMeshName("wait_captive_female");
				return;
			}
			args.MenuContext.SetBackgroundMeshName("wait_captive_male");
		}

		// Token: 0x06003B35 RID: 15157 RVA: 0x001199F3 File Offset: 0x00117BF3
		[GameMenuInitializationHandler("town_backstreet")]
		public static void game_menu_town_menu_backstreet_sound_on_init(MenuCallbackArgs args)
		{
			args.MenuContext.SetAmbientSound("event:/map/ambient/node/settlements/2d/tavern");
		}

		// Token: 0x06003B36 RID: 15158 RVA: 0x00119A05 File Offset: 0x00117C05
		[GameMenuInitializationHandler("town_enemy_town_keep")]
		[GameMenuInitializationHandler("town_keep_dungeon")]
		[GameMenuInitializationHandler("town_keep_bribe")]
		public static void game_menu_town_menu_keep_sound_on_init(MenuCallbackArgs args)
		{
			args.MenuContext.SetBackgroundMeshName(Settlement.CurrentSettlement.SettlementComponent.WaitMeshName);
			args.MenuContext.SetAmbientSound("event:/map/ambient/node/settlements/2d/keep");
		}

		// Token: 0x06003B37 RID: 15159 RVA: 0x00119A31 File Offset: 0x00117C31
		[GameMenuInitializationHandler("town_wait_menus")]
		[GameMenuInitializationHandler("town_wait")]
		public static void game_menu_town_menu_sound_on_init(MenuCallbackArgs args)
		{
			args.MenuContext.SetAmbientSound("event:/map/ambient/node/settlements/2d/city");
			args.MenuContext.SetBackgroundMeshName(Settlement.CurrentSettlement.SettlementComponent.WaitMeshName);
		}

		// Token: 0x06003B38 RID: 15160 RVA: 0x00119A5D File Offset: 0x00117C5D
		[GameMenuInitializationHandler("town")]
		public static void game_menu_town_menu_enter_sound_on_init(MenuCallbackArgs args)
		{
			args.MenuContext.SetPanelSound("event:/ui/panels/settlement_city");
			args.MenuContext.SetAmbientSound("event:/map/ambient/node/settlements/2d/city");
			args.MenuContext.SetBackgroundMeshName(Settlement.CurrentSettlement.SettlementComponent.WaitMeshName);
		}

		// Token: 0x06003B39 RID: 15161 RVA: 0x00119A9C File Offset: 0x00117C9C
		[GameMenuInitializationHandler("village_wait_menus")]
		public static void game_menu_village_menu_sound_on_init(MenuCallbackArgs args)
		{
			args.MenuContext.SetAmbientSound("event:/map/ambient/node/settlements/2d/village");
			Village village = Settlement.CurrentSettlement.Village;
			args.MenuContext.SetBackgroundMeshName(village.WaitMeshName);
		}

		// Token: 0x06003B3A RID: 15162 RVA: 0x00119AD8 File Offset: 0x00117CD8
		[GameMenuInitializationHandler("village")]
		public static void game_menu_village__enter_menu_sound_on_init(MenuCallbackArgs args)
		{
			args.MenuContext.SetPanelSound("event:/ui/panels/settlement_village");
			args.MenuContext.SetAmbientSound("event:/map/ambient/node/settlements/2d/village");
			Village village = Settlement.CurrentSettlement.Village;
			args.MenuContext.SetBackgroundMeshName(village.WaitMeshName);
		}

		// Token: 0x06003B3B RID: 15163 RVA: 0x00119B21 File Offset: 0x00117D21
		private void OnSettlementEntered(MobileParty party, Settlement settlement, Hero hero)
		{
			if (party != null && party.IsMainParty)
			{
				settlement.HasVisited = true;
				if (MBRandom.RandomFloat > 0.5f && (settlement.IsVillage || settlement.IsTown))
				{
					this.CheckPerkAndGiveRelation(party, settlement);
				}
			}
		}

		// Token: 0x06003B3C RID: 15164 RVA: 0x00119B5C File Offset: 0x00117D5C
		private void CheckPerkAndGiveRelation(MobileParty party, Settlement settlement)
		{
			bool isVillage = settlement.IsVillage;
			bool flag = isVillage ? party.HasPerk(DefaultPerks.Scouting.WaterDiviner, true) : party.HasPerk(DefaultPerks.Scouting.Pathfinder, true);
			bool flag2 = isVillage ? (this._lastTimeRelationGivenWaterDiviner.Equals(CampaignTime.Zero) || this._lastTimeRelationGivenWaterDiviner.ElapsedDaysUntilNow >= 1f) : (this._lastTimeRelationGivenPathfinder.Equals(CampaignTime.Zero) || this._lastTimeRelationGivenPathfinder.ElapsedDaysUntilNow >= 1f);
			if (flag && flag2)
			{
				Hero randomElement = settlement.Notables.GetRandomElement<Hero>();
				if (randomElement != null)
				{
					ChangeRelationAction.ApplyRelationChangeBetweenHeroes(party.ActualClan.Leader, randomElement, 1, true);
				}
				if (isVillage)
				{
					this._lastTimeRelationGivenWaterDiviner = CampaignTime.Now;
					return;
				}
				this._lastTimeRelationGivenPathfinder = CampaignTime.Now;
			}
		}

		// Token: 0x06003B3D RID: 15165 RVA: 0x00119C29 File Offset: 0x00117E29
		private void OnSettlementLeft(MobileParty party, Settlement settlement)
		{
			if (party != null && party.IsMainParty)
			{
				Campaign.Current.IsMainHeroDisguised = false;
			}
		}

		// Token: 0x06003B3E RID: 15166 RVA: 0x00119C44 File Offset: 0x00117E44
		private void SwitchToMenuIfThereIsAnInterrupt(string currentMenuId)
		{
			string genericStateMenu = Campaign.Current.Models.EncounterGameMenuModel.GetGenericStateMenu();
			if (genericStateMenu != currentMenuId)
			{
				if (!string.IsNullOrEmpty(genericStateMenu))
				{
					GameMenu.SwitchToMenu(genericStateMenu);
					return;
				}
				GameMenu.ExitToLast();
			}
		}

		// Token: 0x040011CC RID: 4556
		private CampaignTime _lastTimeRelationGivenPathfinder = CampaignTime.Zero;

		// Token: 0x040011CD RID: 4557
		private CampaignTime _lastTimeRelationGivenWaterDiviner = CampaignTime.Zero;
	}
}
