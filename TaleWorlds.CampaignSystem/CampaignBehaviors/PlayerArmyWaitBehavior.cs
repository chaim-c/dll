using System;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.CampaignSystem.Overlay;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Siege;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x020003C1 RID: 961
	public class PlayerArmyWaitBehavior : CampaignBehaviorBase
	{
		// Token: 0x06003AA6 RID: 15014 RVA: 0x00114B91 File Offset: 0x00112D91
		public PlayerArmyWaitBehavior()
		{
			this._leadingArmyDescriptionText = GameTexts.FindText("str_you_are_leading_army", null);
			this._armyDescriptionText = GameTexts.FindText("str_army_of_HERO", null);
			this._disbandingArmyDescriptionText = new TextObject("{=Yan3ZG1w}Disbanding Army!", null);
		}

		// Token: 0x06003AA7 RID: 15015 RVA: 0x00114BCC File Offset: 0x00112DCC
		public override void RegisterEvents()
		{
			CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnSessionLaunched));
			CampaignEvents.ArmyDispersed.AddNonSerializedListener(this, new Action<Army, Army.ArmyDispersionReason, bool>(this.OnArmyDispersed));
			CampaignEvents.TickEvent.AddNonSerializedListener(this, new Action<float>(this.OnTick));
		}

		// Token: 0x06003AA8 RID: 15016 RVA: 0x00114C1E File Offset: 0x00112E1E
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x06003AA9 RID: 15017 RVA: 0x00114C20 File Offset: 0x00112E20
		private void AddMenus(CampaignGameStarter starter)
		{
			starter.AddWaitGameMenu("army_wait", "{=0gwQGnm4}{ARMY_OWNER_TEXT} {ARMY_BEHAVIOR}", new OnInitDelegate(this.wait_menu_army_wait_on_init), new OnConditionDelegate(this.wait_menu_army_wait_on_condition), null, new OnTickDelegate(this.ArmyWaitMenuTick), GameMenu.MenuAndOptionType.WaitMenuHideProgressAndHoursOption, GameOverlays.MenuOverlayType.None, 0f, GameMenu.MenuFlags.None, null);
			starter.AddGameMenuOption("army_wait", "leave_army", "{=hSdJ0UUv}Leave Army", new GameMenuOption.OnConditionDelegate(this.wait_menu_army_leave_on_condition), new GameMenuOption.OnConsequenceDelegate(this.wait_menu_army_leave_on_consequence), true, -1, false, null);
			starter.AddGameMenuOption("army_wait", "abandon_army", "{=0vnegjxf}Abandon Army", new GameMenuOption.OnConditionDelegate(this.wait_menu_army_abandon_on_condition), new GameMenuOption.OnConsequenceDelegate(this.wait_menu_army_abandon_on_consequence), true, -1, false, null);
			starter.AddWaitGameMenu("army_wait_at_settlement", "{=0gwQGnm4}{ARMY_OWNER_TEXT} {ARMY_BEHAVIOR}", new OnInitDelegate(this.wait_menu_army_wait_at_settlement_on_init), new OnConditionDelegate(this.wait_menu_army_wait_on_condition), null, new OnTickDelegate(this.wait_menu_army_wait_at_settlement_on_tick), GameMenu.MenuAndOptionType.WaitMenuHideProgressAndHoursOption, GameOverlays.MenuOverlayType.None, 0f, GameMenu.MenuFlags.None, null);
			starter.AddGameMenuOption("army_wait_at_settlement", "enter_settlement", "{=!}{ENTER_SETTLEMENT}", new GameMenuOption.OnConditionDelegate(this.wait_menu_army_enter_settlement_on_condition), new GameMenuOption.OnConsequenceDelegate(this.wait_menu_army_enter_settlement_on_consequence), false, -1, false, null);
			starter.AddGameMenuOption("army_wait_at_settlement", "leave_army", "{=hSdJ0UUv}Leave Army", new GameMenuOption.OnConditionDelegate(this.wait_menu_army_leave_on_condition), new GameMenuOption.OnConsequenceDelegate(this.wait_menu_army_leave_on_consequence), true, -1, false, null);
			starter.AddGameMenu("army_dispersed", "{=!}{ARMY_DISPERSE_REASON}", null, GameOverlays.MenuOverlayType.None, GameMenu.MenuFlags.None, null);
			starter.AddGameMenuOption("army_dispersed", "army_dispersed_continue", "{=DM6luo3c}Continue", new GameMenuOption.OnConditionDelegate(this.army_dispersed_continue_on_condition), new GameMenuOption.OnConsequenceDelegate(this.army_dispersed_continue_on_consequence), false, -1, false, null);
		}

		// Token: 0x06003AAA RID: 15018 RVA: 0x00114DB4 File Offset: 0x00112FB4
		private void ArmyWaitMenuTick(MenuCallbackArgs args, CampaignTime dt)
		{
			string genericStateMenu = Campaign.Current.Models.EncounterGameMenuModel.GetGenericStateMenu();
			if (!(genericStateMenu != "army_wait"))
			{
				this.RefreshArmyTexts(args);
				return;
			}
			args.MenuContext.GameMenu.EndWait();
			if (!string.IsNullOrEmpty(genericStateMenu))
			{
				GameMenu.SwitchToMenu(genericStateMenu);
				return;
			}
			GameMenu.ExitToLast();
		}

		// Token: 0x06003AAB RID: 15019 RVA: 0x00114E0F File Offset: 0x0011300F
		private void OnSessionLaunched(CampaignGameStarter starter)
		{
			this.AddMenus(starter);
		}

		// Token: 0x06003AAC RID: 15020 RVA: 0x00114E18 File Offset: 0x00113018
		private void OnArmyDispersed(Army army, Army.ArmyDispersionReason reason, bool isPlayersArmy)
		{
			if (isPlayersArmy && army.LeaderParty != MobileParty.MainParty && Campaign.Current.CurrentMenuContext != null)
			{
				Campaign.Current.CurrentMenuContext.GameMenu.EndWait();
				GameMenu.SwitchToMenu("army_dispersed");
				TextObject textObject = TextObject.Empty;
				if (reason == Army.ArmyDispersionReason.NoActiveWar)
				{
					textObject = new TextObject("{=tvAdOGzc}{ARMY_LEADER}'s army has disbanded. The kingdom is now at peace.", null);
				}
				else if (reason == Army.ArmyDispersionReason.CohesionDepleted)
				{
					textObject = new TextObject("{=5wwO7ozf}{ARMY_LEADER}'s army has disbanded due to a lack of cohesion.", null);
				}
				else if (reason == Army.ArmyDispersionReason.FoodProblem)
				{
					textObject = new TextObject("{=eVdUaG3x}{ARMY_LEADER}'s army has disbanded due to a lack of food.", null);
				}
				else
				{
					textObject = new TextObject("{=FXPvGTEa}Army you are in is dispersed.", null);
				}
				textObject.SetTextVariable("ARMY_LEADER", army.LeaderParty.LeaderHero.Name);
				MBTextManager.SetTextVariable("ARMY_DISPERSE_REASON", textObject, false);
			}
		}

		// Token: 0x06003AAD RID: 15021 RVA: 0x00114EDC File Offset: 0x001130DC
		private void wait_menu_army_wait_on_init(MenuCallbackArgs args)
		{
			Army army = MobileParty.MainParty.Army;
			bool flag;
			if (army == null)
			{
				flag = (null != null);
			}
			else
			{
				MobileParty leaderParty = army.LeaderParty;
				flag = (((leaderParty != null) ? leaderParty.LeaderHero : null) != null);
			}
			if (flag)
			{
				this._armyDescriptionText.SetTextVariable("HERO", army.LeaderParty.LeaderHero.Name);
				args.MenuTitle = this._armyDescriptionText;
			}
			else
			{
				args.MenuTitle = this._disbandingArmyDescriptionText;
			}
			this.RefreshArmyTexts(args);
		}

		// Token: 0x06003AAE RID: 15022 RVA: 0x00114F50 File Offset: 0x00113150
		private void wait_menu_army_wait_at_settlement_on_init(MenuCallbackArgs args)
		{
			if (!PlayerEncounter.InsideSettlement && MobileParty.MainParty.Army.LeaderParty == MobileParty.MainParty)
			{
				PlayerEncounter.EnterSettlement();
			}
			this._armyDescriptionText.SetTextVariable("HERO", MobileParty.MainParty.Army.LeaderParty.LeaderHero.Name);
			args.MenuTitle = this._armyDescriptionText;
			if (PlayerEncounter.Current != null)
			{
				PlayerEncounter.Current.IsPlayerWaiting = true;
			}
			this.RefreshArmyTexts(args);
		}

		// Token: 0x06003AAF RID: 15023 RVA: 0x00114FD0 File Offset: 0x001131D0
		private void wait_menu_army_wait_at_settlement_on_tick(MenuCallbackArgs args, CampaignTime dt)
		{
			string genericStateMenu = Campaign.Current.Models.EncounterGameMenuModel.GetGenericStateMenu();
			if (genericStateMenu != "army_wait_at_settlement")
			{
				args.MenuContext.GameMenu.EndWait();
				if (!string.IsNullOrEmpty(genericStateMenu))
				{
					GameMenu.SwitchToMenu(genericStateMenu);
					return;
				}
				GameMenu.ExitToLast();
			}
		}

		// Token: 0x06003AB0 RID: 15024 RVA: 0x00115023 File Offset: 0x00113223
		[GameMenuInitializationHandler("army_wait")]
		private static void wait_menu_ui_army_wait_on_init(MenuCallbackArgs args)
		{
			args.MenuContext.SetBackgroundMeshName(Hero.MainHero.MapFaction.Culture.EncounterBackgroundMesh);
		}

		// Token: 0x06003AB1 RID: 15025 RVA: 0x00115044 File Offset: 0x00113244
		private void RefreshArmyTexts(MenuCallbackArgs args)
		{
			if (MobileParty.MainParty.Army != null)
			{
				TextObject text = args.MenuContext.GameMenu.GetText();
				if (MobileParty.MainParty.Army.LeaderParty != MobileParty.MainParty)
				{
					TextObject textObject = GameTexts.FindText("str_you_are_following_army", null);
					textObject.SetTextVariable("ARMY_LEADER", MobileParty.MainParty.Army.LeaderParty.LeaderHero.Name);
					text.SetTextVariable("ARMY_OWNER_TEXT", textObject);
					text.SetTextVariable("ARMY_BEHAVIOR", MobileParty.MainParty.Army.GetBehaviorText(false));
					return;
				}
				text.SetTextVariable("ARMY_OWNER_TEXT", this._leadingArmyDescriptionText);
				text.SetTextVariable("ARMY_BEHAVIOR", "");
			}
		}

		// Token: 0x06003AB2 RID: 15026 RVA: 0x00115105 File Offset: 0x00113305
		private bool wait_menu_army_wait_on_condition(MenuCallbackArgs args)
		{
			return true;
		}

		// Token: 0x06003AB3 RID: 15027 RVA: 0x00115108 File Offset: 0x00113308
		private bool wait_menu_army_abandon_on_condition(MenuCallbackArgs args)
		{
			args.optionLeaveType = GameMenuOption.LeaveType.Leave;
			if (MobileParty.MainParty.Army == null || (MobileParty.MainParty.MapEvent == null && MobileParty.MainParty.BesiegedSettlement == null))
			{
				return false;
			}
			args.Tooltip = GameTexts.FindText("str_abandon_army", null);
			args.Tooltip.SetTextVariable("INFLUENCE_COST", Campaign.Current.Models.DiplomacyModel.GetInfluenceCostOfAbandoningArmy());
			return true;
		}

		// Token: 0x06003AB4 RID: 15028 RVA: 0x00115184 File Offset: 0x00113384
		private bool wait_menu_army_enter_settlement_on_condition(MenuCallbackArgs args)
		{
			args.optionLeaveType = GameMenuOption.LeaveType.Submenu;
			if (MobileParty.MainParty.Army != null && MobileParty.MainParty.Army.LeaderParty != MobileParty.MainParty && MobileParty.MainParty.MapEvent == null && MobileParty.MainParty.BesiegedSettlement == null)
			{
				Settlement settlement = null;
				if (MobileParty.MainParty.CurrentSettlement != null)
				{
					settlement = MobileParty.MainParty.CurrentSettlement;
				}
				else if (MobileParty.MainParty.Army.LeaderParty.LastVisitedSettlement != null && MobileParty.MainParty.Army.LeaderParty.LastVisitedSettlement.Position2D.Distance(MobileParty.MainParty.Army.LeaderParty.Position2D) < 1f)
				{
					settlement = MobileParty.MainParty.Army.LeaderParty.LastVisitedSettlement;
				}
				if (settlement != null)
				{
					if (settlement.IsTown)
					{
						MBTextManager.SetTextVariable("ENTER_SETTLEMENT", "{=bkoJ57h3}Enter the Town", false);
					}
					else if (settlement.IsCastle)
					{
						MBTextManager.SetTextVariable("ENTER_SETTLEMENT", "{=aa3kbW8j}Enter the Castle", false);
					}
					else if (settlement.IsVillage)
					{
						MBTextManager.SetTextVariable("ENTER_SETTLEMENT", "{=8UzRj1YW}Enter the Village", false);
					}
					else
					{
						MBTextManager.SetTextVariable("ENTER_SETTLEMENT", "{=eabR87ne}Enter the Settlement", false);
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003AB5 RID: 15029 RVA: 0x001152C4 File Offset: 0x001134C4
		private void wait_menu_army_enter_settlement_on_consequence(MenuCallbackArgs args)
		{
			if (MobileParty.MainParty.Army != null && MobileParty.MainParty.Army.LeaderParty != MobileParty.MainParty && MobileParty.MainParty.CurrentSettlement == null)
			{
				EncounterManager.StartSettlementEncounter(MobileParty.MainParty, MobileParty.MainParty.Army.LeaderParty.LastVisitedSettlement);
				return;
			}
			Settlement currentSettlement = MobileParty.MainParty.CurrentSettlement;
			if (currentSettlement.IsTown)
			{
				GameMenu.ActivateGameMenu("town");
				return;
			}
			if (currentSettlement.IsCastle)
			{
				GameMenu.ActivateGameMenu("castle");
				return;
			}
			GameMenu.ActivateGameMenu("village");
		}

		// Token: 0x06003AB6 RID: 15030 RVA: 0x00115359 File Offset: 0x00113559
		private bool wait_menu_army_leave_on_condition(MenuCallbackArgs args)
		{
			args.optionLeaveType = GameMenuOption.LeaveType.Leave;
			return MobileParty.MainParty.Army != null && MobileParty.MainParty.MapEvent == null && MobileParty.MainParty.BesiegedSettlement == null;
		}

		// Token: 0x06003AB7 RID: 15031 RVA: 0x0011538A File Offset: 0x0011358A
		private void wait_menu_army_leave_on_consequence(MenuCallbackArgs args)
		{
			if (PlayerEncounter.Current != null)
			{
				PlayerEncounter.Finish(true);
			}
			else
			{
				GameMenu.ExitToLast();
			}
			if (Settlement.CurrentSettlement != null)
			{
				LeaveSettlementAction.ApplyForParty(MobileParty.MainParty);
				PartyBase.MainParty.SetVisualAsDirty();
			}
			MobileParty.MainParty.Army = null;
		}

		// Token: 0x06003AB8 RID: 15032 RVA: 0x001153C8 File Offset: 0x001135C8
		private void wait_menu_army_abandon_on_consequence(MenuCallbackArgs args)
		{
			ChangeClanInfluenceAction.Apply(Clan.PlayerClan, (float)(-(float)Campaign.Current.Models.DiplomacyModel.GetInfluenceCostOfAbandoningArmy()));
			if (PlayerEncounter.Current != null)
			{
				PlayerEncounter.Finish(true);
			}
			else
			{
				GameMenu.ExitToLast();
			}
			MobileParty.MainParty.Army = null;
		}

		// Token: 0x06003AB9 RID: 15033 RVA: 0x00115414 File Offset: 0x00113614
		private void OnTick(float dt)
		{
			if (MobileParty.MainParty.AttachedTo != null)
			{
				MenuContext currentMenuContext = Campaign.Current.CurrentMenuContext;
				string a;
				if (currentMenuContext == null)
				{
					a = null;
				}
				else
				{
					GameMenu gameMenu = currentMenuContext.GameMenu;
					a = ((gameMenu != null) ? gameMenu.StringId : null);
				}
				Settlement settlement;
				if (a == "army_wait" && (settlement = (MobileParty.MainParty.AttachedTo.Army.AiBehaviorObject as Settlement)) != null && settlement.SiegeEvent != null && Hero.MainHero.PartyBelongedTo.Army.LeaderParty.BesiegedSettlement == settlement)
				{
					PlayerSiege.StartPlayerSiege(BattleSideEnum.Attacker, false, settlement);
					PlayerSiege.StartSiegePreparation();
				}
			}
		}

		// Token: 0x06003ABA RID: 15034 RVA: 0x001154AC File Offset: 0x001136AC
		private void army_dispersed_continue_on_consequence(MenuCallbackArgs args)
		{
			if (MobileParty.MainParty.CurrentSettlement == null)
			{
				GameMenu.ExitToLast();
				return;
			}
			if (MobileParty.MainParty.CurrentSettlement.IsVillage)
			{
				GameMenu.SwitchToMenu("village");
				return;
			}
			if (MobileParty.MainParty.CurrentSettlement.IsTown)
			{
				GameMenu.SwitchToMenu((MobileParty.MainParty.CurrentSettlement.SiegeEvent != null) ? "menu_siege_strategies" : "town");
				return;
			}
			if (MobileParty.MainParty.CurrentSettlement.IsCastle)
			{
				GameMenu.SwitchToMenu((MobileParty.MainParty.CurrentSettlement.SiegeEvent != null) ? "menu_siege_strategies" : "castle");
				return;
			}
			LeaveSettlementAction.ApplyForParty(MobileParty.MainParty);
		}

		// Token: 0x06003ABB RID: 15035 RVA: 0x0011555C File Offset: 0x0011375C
		private bool army_dispersed_continue_on_condition(MenuCallbackArgs args)
		{
			args.optionLeaveType = GameMenuOption.LeaveType.Continue;
			return true;
		}

		// Token: 0x06003ABC RID: 15036 RVA: 0x00115568 File Offset: 0x00113768
		[GameMenuInitializationHandler("army_wait_at_settlement")]
		private static void game_menu_army_wait_at_settlement_on_init(MenuCallbackArgs args)
		{
			Settlement settlement = (Settlement.CurrentSettlement != null) ? Settlement.CurrentSettlement : ((MobileParty.MainParty.LastVisitedSettlement != null) ? MobileParty.MainParty.LastVisitedSettlement : MobileParty.MainParty.AttachedTo.LastVisitedSettlement);
			args.MenuContext.SetBackgroundMeshName(settlement.SettlementComponent.WaitMeshName);
		}

		// Token: 0x06003ABD RID: 15037 RVA: 0x001155C1 File Offset: 0x001137C1
		[GameMenuInitializationHandler("army_dispersed")]
		private static void game_menu_army_dispersed_on_init(MenuCallbackArgs args)
		{
			args.MenuContext.SetBackgroundMeshName("wait_fallback");
		}

		// Token: 0x040011C4 RID: 4548
		private readonly TextObject _leadingArmyDescriptionText;

		// Token: 0x040011C5 RID: 4549
		private readonly TextObject _armyDescriptionText;

		// Token: 0x040011C6 RID: 4550
		private readonly TextObject _disbandingArmyDescriptionText;
	}
}
