using System;
using Helpers;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Siege;
using TaleWorlds.Core;
using TaleWorlds.LinQuick;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x020003D6 RID: 982
	public class SiegeAmbushCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x06003C98 RID: 15512 RVA: 0x00126480 File Offset: 0x00124680
		public override void RegisterEvents()
		{
			CampaignEvents.OnAfterSessionLaunchedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnAfterSessionLaunched));
			CampaignEvents.MapEventStarted.AddNonSerializedListener(this, new Action<MapEvent, PartyBase, PartyBase>(this.OnMapEventStarted));
			CampaignEvents.OnSiegeEventEndedEvent.AddNonSerializedListener(this, new Action<SiegeEvent>(this.OnSiegeEventEnded));
			CampaignEvents.HourlyTickEvent.AddNonSerializedListener(this, new Action(this.HourlyTick));
			CampaignEvents.OnMissionEndedEvent.AddNonSerializedListener(this, new Action<IMission>(this.OnMissionEnded));
		}

		// Token: 0x06003C99 RID: 15513 RVA: 0x00126500 File Offset: 0x00124700
		public override void SyncData(IDataStore dataStore)
		{
			dataStore.SyncData<CampaignTime>("_lastAmbushTime", ref this._lastAmbushTime);
		}

		// Token: 0x06003C9A RID: 15514 RVA: 0x00126514 File Offset: 0x00124714
		private void OnAfterSessionLaunched(CampaignGameStarter campaignGameStarter)
		{
			this.AddGameMenus(campaignGameStarter);
		}

		// Token: 0x06003C9B RID: 15515 RVA: 0x0012651D File Offset: 0x0012471D
		private void OnMapEventStarted(MapEvent mapEvent, PartyBase attackerParty, PartyBase defenderParty)
		{
			if (mapEvent.IsSiegeAmbush)
			{
				this._lastAmbushTime = CampaignTime.Now;
			}
		}

		// Token: 0x06003C9C RID: 15516 RVA: 0x00126532 File Offset: 0x00124732
		private void OnSiegeEventEnded(SiegeEvent siegeEvent)
		{
			if (siegeEvent == PlayerSiege.PlayerSiegeEvent)
			{
				this._lastAmbushTime = CampaignTime.Never;
			}
		}

		// Token: 0x06003C9D RID: 15517 RVA: 0x00126547 File Offset: 0x00124747
		private void HourlyTick()
		{
			if (PlayerSiege.PlayerSiegeEvent != null && this._lastAmbushTime == CampaignTime.Never && PlayerSiege.PlayerSiegeEvent.BesiegerCamp.IsPreparationComplete)
			{
				this._lastAmbushTime = CampaignTime.Now;
			}
		}

		// Token: 0x06003C9E RID: 15518 RVA: 0x0012657E File Offset: 0x0012477E
		private void OnMissionEnded(IMission mission)
		{
			MapEvent battle = PlayerEncounter.Battle;
			if (battle != null && battle.IsSiegeAmbush)
			{
				PlayerEncounter.Current.FinalizeBattle();
				PlayerEncounter.Current.SetIsSallyOutAmbush(false);
			}
		}

		// Token: 0x06003C9F RID: 15519 RVA: 0x001265A8 File Offset: 0x001247A8
		private bool CanMainHeroAmbush(out TextObject reason)
		{
			if (this._lastAmbushTime.ElapsedHoursUntilNow < 24f)
			{
				reason = new TextObject("{=lCYPxuWN}The enemy is alert, you cannot ambush right now.", null);
				return false;
			}
			if (Hero.MainHero.IsWounded)
			{
				reason = new TextObject("{=pQaQW1As}You cannot ambush right now due to your wounds.", null);
				return false;
			}
			SiegeEvent playerSiegeEvent = PlayerSiege.PlayerSiegeEvent;
			if (playerSiegeEvent.BesiegerCamp.LeaderParty.MapEvent != null && MobileParty.MainParty.MapEvent == null)
			{
				reason = new TextObject("{=GAh1gNYn}Enemies are already engaged in battle.", null);
				return false;
			}
			if (playerSiegeEvent.GetPreparedSiegeEnginesAsDictionary(playerSiegeEvent.GetSiegeEventSide(BattleSideEnum.Attacker)).Count <= 0)
			{
				reason = new TextObject("{=f4g7r0xF}The enemy does not have any vulnerabilities.", null);
				return false;
			}
			if (playerSiegeEvent.BesiegedSettlement.SettlementWallSectionHitPointsRatioList.AnyQ((float r) => r <= 0f))
			{
				reason = new TextObject("{=Nzt8Xkro}You cannot ambush because the settlement walls are breached.", null);
				return false;
			}
			reason = TextObject.Empty;
			return true;
		}

		// Token: 0x06003CA0 RID: 15520 RVA: 0x00126690 File Offset: 0x00124890
		private void AddGameMenus(CampaignGameStarter campaignGameSystemStarter)
		{
			campaignGameSystemStarter.AddGameMenuOption("menu_siege_strategies", "menu_siege_strategies_ambush", "{=LEKzuGzi}Ambush", new GameMenuOption.OnConditionDelegate(this.menu_siege_strategies_ambush_condition), new GameMenuOption.OnConsequenceDelegate(this.menu_siege_strategies_ambush_on_consequence), false, -1, false, null);
		}

		// Token: 0x06003CA1 RID: 15521 RVA: 0x001266D0 File Offset: 0x001248D0
		private bool menu_siege_strategies_ambush_condition(MenuCallbackArgs args)
		{
			if (PlayerSiege.PlayerSiegeEvent == null || PlayerSiege.PlayerSide != BattleSideEnum.Defender)
			{
				return false;
			}
			args.optionLeaveType = GameMenuOption.LeaveType.SiegeAmbush;
			TextObject tooltip;
			if (!this.CanMainHeroAmbush(out tooltip))
			{
				args.IsEnabled = false;
				args.Tooltip = tooltip;
			}
			return true;
		}

		// Token: 0x06003CA2 RID: 15522 RVA: 0x00126710 File Offset: 0x00124910
		private void menu_siege_strategies_ambush_on_consequence(MenuCallbackArgs args)
		{
			this._lastAmbushTime = CampaignTime.Now;
			if (PlayerEncounter.EncounterSettlement != null && PlayerEncounter.EncounterSettlement.SiegeEvent != null && !PlayerEncounter.EncounterSettlement.MapFaction.IsAtWarWith(MobileParty.MainParty.MapFaction))
			{
				PlayerEncounter.RestartPlayerEncounter(PartyBase.MainParty, PlayerEncounter.EncounterSettlement.SiegeEvent.BesiegerCamp.LeaderParty.Party, false);
			}
			PlayerEncounter.Current.SetIsSallyOutAmbush(true);
			PlayerEncounter.StartBattle();
			MenuHelper.EncounterAttackConsequence(args);
		}

		// Token: 0x0400120C RID: 4620
		private const int SiegeAmbushCooldownPeriodAsHours = 24;

		// Token: 0x0400120D RID: 4621
		private CampaignTime _lastAmbushTime = CampaignTime.Never;
	}
}
