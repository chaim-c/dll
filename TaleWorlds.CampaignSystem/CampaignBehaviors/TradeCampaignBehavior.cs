using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x020003DC RID: 988
	public class TradeCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x06003D2E RID: 15662 RVA: 0x0012AB1F File Offset: 0x00128D1F
		public void OnNewGameCreated(CampaignGameStarter campaignGameStarter)
		{
			this.InitializeMarkets();
		}

		// Token: 0x06003D2F RID: 15663 RVA: 0x0012AB28 File Offset: 0x00128D28
		public override void RegisterEvents()
		{
			CampaignEvents.DailyTickTownEvent.AddNonSerializedListener(this, new Action<Town>(this.DailyTickTown));
			CampaignEvents.OnNewGameCreatedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnNewGameCreated));
			CampaignEvents.OnNewGameCreatedPartialFollowUpEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter, int>(this.OnNewGameCreatedPartialFollowUp));
			CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnSessionLaunched));
		}

		// Token: 0x06003D30 RID: 15664 RVA: 0x0012AB94 File Offset: 0x00128D94
		private void OnNewGameCreatedPartialFollowUp(CampaignGameStarter campaignGameStarter, int i)
		{
			if (i == 2)
			{
				this.InitializeTrade();
			}
			if (i % 10 == 0)
			{
				foreach (Settlement settlement in Settlement.All)
				{
					if (settlement.IsTown)
					{
						this.UpdateMarketStores(settlement.Town);
					}
				}
			}
		}

		// Token: 0x06003D31 RID: 15665 RVA: 0x0012AC04 File Offset: 0x00128E04
		private void OnSessionLaunched(CampaignGameStarter campaignGameStarter)
		{
			foreach (Town town in Town.AllTowns)
			{
				this.UpdateMarketStores(town);
			}
		}

		// Token: 0x06003D32 RID: 15666 RVA: 0x0012AC58 File Offset: 0x00128E58
		public override void SyncData(IDataStore dataStore)
		{
			dataStore.SyncData<Dictionary<ItemCategory, float>>("_numberOfTotalItemsAtGameWorld", ref this._numberOfTotalItemsAtGameWorld);
		}

		// Token: 0x06003D33 RID: 15667 RVA: 0x0012AC6C File Offset: 0x00128E6C
		private void InitializeTrade()
		{
			this._numberOfTotalItemsAtGameWorld = new Dictionary<ItemCategory, float>();
			(from settlement in Campaign.Current.Settlements
			where settlement.IsTown
			select settlement).ToList<Settlement>();
			foreach (Hero hero in Hero.AllAliveHeroes)
			{
				if (hero.CharacterObject.Occupation == Occupation.Lord && hero.Clan != Clan.PlayerClan)
				{
					Clan clan = hero.Clan;
					int amount;
					if (((clan != null) ? clan.Leader : null) == hero)
					{
						amount = 50000 + 10000 * hero.Clan.Tier + ((hero == hero.MapFaction.Leader) ? 50000 : 0);
					}
					else
					{
						amount = 10000;
					}
					GiveGoldAction.ApplyBetweenCharacters(null, hero, amount, false);
				}
			}
		}

		// Token: 0x06003D34 RID: 15668 RVA: 0x0012AD6C File Offset: 0x00128F6C
		public void DailyTickTown(Town town)
		{
			this.UpdateMarketStores(town);
		}

		// Token: 0x06003D35 RID: 15669 RVA: 0x0012AD75 File Offset: 0x00128F75
		private void UpdateMarketStores(Town town)
		{
			town.MarketData.UpdateStores();
		}

		// Token: 0x06003D36 RID: 15670 RVA: 0x0012AD84 File Offset: 0x00128F84
		private void InitializeMarkets()
		{
			foreach (Town town in Town.AllTowns)
			{
				foreach (ItemCategory itemCategory in ItemCategories.All)
				{
					if (itemCategory.IsValid)
					{
						town.MarketData.AddDemand(itemCategory, 3f);
						town.MarketData.AddSupply(itemCategory, 2f);
					}
				}
			}
		}

		// Token: 0x04001222 RID: 4642
		private Dictionary<ItemCategory, float> _numberOfTotalItemsAtGameWorld;

		// Token: 0x04001223 RID: 4643
		public const float MaximumTaxRatioForVillages = 1f;

		// Token: 0x04001224 RID: 4644
		public const float MaximumTaxRatioForTowns = 0.5f;

		// Token: 0x02000746 RID: 1862
		public enum TradeGoodType
		{
			// Token: 0x04001E8C RID: 7820
			Grain,
			// Token: 0x04001E8D RID: 7821
			Wood,
			// Token: 0x04001E8E RID: 7822
			Meat,
			// Token: 0x04001E8F RID: 7823
			Wool,
			// Token: 0x04001E90 RID: 7824
			Cheese,
			// Token: 0x04001E91 RID: 7825
			Iron,
			// Token: 0x04001E92 RID: 7826
			Salt,
			// Token: 0x04001E93 RID: 7827
			Spice,
			// Token: 0x04001E94 RID: 7828
			Raw_Silk,
			// Token: 0x04001E95 RID: 7829
			Fish,
			// Token: 0x04001E96 RID: 7830
			Flax,
			// Token: 0x04001E97 RID: 7831
			Grape,
			// Token: 0x04001E98 RID: 7832
			Hides,
			// Token: 0x04001E99 RID: 7833
			Clay,
			// Token: 0x04001E9A RID: 7834
			Date_Fruit,
			// Token: 0x04001E9B RID: 7835
			Bread,
			// Token: 0x04001E9C RID: 7836
			Beer,
			// Token: 0x04001E9D RID: 7837
			Wine,
			// Token: 0x04001E9E RID: 7838
			Tools,
			// Token: 0x04001E9F RID: 7839
			Pottery,
			// Token: 0x04001EA0 RID: 7840
			Cloth,
			// Token: 0x04001EA1 RID: 7841
			Linen,
			// Token: 0x04001EA2 RID: 7842
			Leather,
			// Token: 0x04001EA3 RID: 7843
			Velvet,
			// Token: 0x04001EA4 RID: 7844
			Saddle_Horse,
			// Token: 0x04001EA5 RID: 7845
			Steppe_Horse,
			// Token: 0x04001EA6 RID: 7846
			Hunter,
			// Token: 0x04001EA7 RID: 7847
			Desert_Horse,
			// Token: 0x04001EA8 RID: 7848
			Charger,
			// Token: 0x04001EA9 RID: 7849
			War_Horse,
			// Token: 0x04001EAA RID: 7850
			Steppe_Charger,
			// Token: 0x04001EAB RID: 7851
			Desert_War_Horse,
			// Token: 0x04001EAC RID: 7852
			Unknown,
			// Token: 0x04001EAD RID: 7853
			NumberOfTradeItems
		}
	}
}
