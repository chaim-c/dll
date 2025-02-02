﻿using System;
using Helpers;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Settlements;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x020003E5 RID: 997
	internal class VillageTradeBoundCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x06003E25 RID: 15909 RVA: 0x00130D48 File Offset: 0x0012EF48
		public override void RegisterEvents()
		{
			CampaignEvents.OnNewGameCreatedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnNewGameCreated));
			CampaignEvents.OnSettlementOwnerChangedEvent.AddNonSerializedListener(this, new Action<Settlement, bool, Hero, Hero, Hero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail>(this.OnSettlementOwnerChanged));
			CampaignEvents.OnGameLoadedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnGameLoaded));
			CampaignEvents.WarDeclared.AddNonSerializedListener(this, new Action<IFaction, IFaction, DeclareWarAction.DeclareWarDetail>(this.WarDeclared));
			CampaignEvents.MakePeace.AddNonSerializedListener(this, new Action<IFaction, IFaction, MakePeaceAction.MakePeaceDetail>(this.OnMakePeace));
			CampaignEvents.OnClanChangedKingdomEvent.AddNonSerializedListener(this, new Action<Clan, Kingdom, Kingdom, ChangeKingdomAction.ChangeKingdomActionDetail, bool>(this.ClanChangedKingdom));
			CampaignEvents.OnClanDestroyedEvent.AddNonSerializedListener(this, new Action<Clan>(this.OnClanDestroyed));
		}

		// Token: 0x06003E26 RID: 15910 RVA: 0x00130DF6 File Offset: 0x0012EFF6
		private void OnClanDestroyed(Clan obj)
		{
			this.UpdateTradeBounds();
		}

		// Token: 0x06003E27 RID: 15911 RVA: 0x00130DFE File Offset: 0x0012EFFE
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x06003E28 RID: 15912 RVA: 0x00130E00 File Offset: 0x0012F000
		private void ClanChangedKingdom(Clan clan, Kingdom oldKingdom, Kingdom newKingdom, ChangeKingdomAction.ChangeKingdomActionDetail detail, bool showNotification = true)
		{
			this.UpdateTradeBounds();
		}

		// Token: 0x06003E29 RID: 15913 RVA: 0x00130E08 File Offset: 0x0012F008
		private void OnGameLoaded(CampaignGameStarter obj)
		{
			this.UpdateTradeBounds();
		}

		// Token: 0x06003E2A RID: 15914 RVA: 0x00130E10 File Offset: 0x0012F010
		private void OnMakePeace(IFaction faction1, IFaction faction2, MakePeaceAction.MakePeaceDetail detail)
		{
			this.UpdateTradeBounds();
		}

		// Token: 0x06003E2B RID: 15915 RVA: 0x00130E18 File Offset: 0x0012F018
		private void WarDeclared(IFaction faction1, IFaction faction2, DeclareWarAction.DeclareWarDetail declareWarDetail)
		{
			this.UpdateTradeBounds();
		}

		// Token: 0x06003E2C RID: 15916 RVA: 0x00130E20 File Offset: 0x0012F020
		private void OnSettlementOwnerChanged(Settlement settlement, bool openToClaim, Hero newOwner, Hero oldOwner, Hero capturerHero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail detail)
		{
			this.UpdateTradeBounds();
		}

		// Token: 0x06003E2D RID: 15917 RVA: 0x00130E28 File Offset: 0x0012F028
		public void OnNewGameCreated(CampaignGameStarter campaignGameStarter)
		{
			this.UpdateTradeBounds();
		}

		// Token: 0x06003E2E RID: 15918 RVA: 0x00130E30 File Offset: 0x0012F030
		private void UpdateTradeBounds()
		{
			foreach (Town town in Campaign.Current.AllCastles)
			{
				foreach (Village village in town.Villages)
				{
					this.TryToAssignTradeBoundForVillage(village);
				}
			}
		}

		// Token: 0x06003E2F RID: 15919 RVA: 0x00130EC0 File Offset: 0x0012F0C0
		private void TryToAssignTradeBoundForVillage(Village village)
		{
			Settlement settlement = SettlementHelper.FindNearestSettlement((Settlement x) => x.IsTown && x.Town.MapFaction == village.Settlement.MapFaction, village.Settlement);
			if (settlement != null && Campaign.Current.Models.MapDistanceModel.GetDistance(settlement, village.Settlement) < 150f)
			{
				village.TradeBound = settlement;
				return;
			}
			Settlement settlement2 = SettlementHelper.FindNearestSettlement((Settlement x) => x.IsTown && x.Town.MapFaction != village.Settlement.MapFaction && !x.Town.MapFaction.IsAtWarWith(village.Settlement.MapFaction) && Campaign.Current.Models.MapDistanceModel.GetDistance(x, village.Settlement) <= 150f, village.Settlement);
			if (settlement2 != null && Campaign.Current.Models.MapDistanceModel.GetDistance(settlement2, village.Settlement) < 150f)
			{
				village.TradeBound = settlement2;
				return;
			}
			village.TradeBound = null;
		}

		// Token: 0x04001259 RID: 4697
		public const float TradeBoundDistanceLimit = 150f;
	}
}
