using System;
using System.Collections.Generic;
using Helpers;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x02000377 RID: 887
	public class BannerCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x06003449 RID: 13385 RVA: 0x000DB92C File Offset: 0x000D9B2C
		public override void RegisterEvents()
		{
			CampaignEvents.OnNewGameCreatedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnNewGameCreated));
			CampaignEvents.OnGameLoadFinishedEvent.AddNonSerializedListener(this, new Action(this.GiveBannersToHeroes));
			CampaignEvents.DailyTickHeroEvent.AddNonSerializedListener(this, new Action<Hero>(this.DailyTickHero));
			CampaignEvents.CollectLootsEvent.AddNonSerializedListener(this, new Action<MapEvent, PartyBase, Dictionary<PartyBase, ItemRoster>, ItemRoster, MBList<TroopRosterElement>, float>(this.CollectLoots));
			CampaignEvents.HeroComesOfAgeEvent.AddNonSerializedListener(this, new Action<Hero>(this.OnHeroComesOfAge));
			CampaignEvents.HeroCreated.AddNonSerializedListener(this, new Action<Hero, bool>(this.OnHeroCreated));
			CampaignEvents.OnCompanionClanCreatedEvent.AddNonSerializedListener(this, new Action<Clan>(this.OnCompanionClanCreated));
		}

		// Token: 0x0600344A RID: 13386 RVA: 0x000DB9DA File Offset: 0x000D9BDA
		public override void SyncData(IDataStore dataStore)
		{
			dataStore.SyncData<Dictionary<Hero, CampaignTime>>("_heroNextBannerLootTime", ref this._heroNextBannerLootTime);
		}

		// Token: 0x0600344B RID: 13387 RVA: 0x000DB9EE File Offset: 0x000D9BEE
		private void OnNewGameCreated(CampaignGameStarter campaignGameStarter)
		{
			this.GiveBannersToHeroes();
		}

		// Token: 0x0600344C RID: 13388 RVA: 0x000DB9F8 File Offset: 0x000D9BF8
		private void GiveBannersToHeroes()
		{
			foreach (Hero hero in Hero.AllAliveHeroes)
			{
				if (this.CanBannerBeGivenToHero(hero))
				{
					ItemObject randomBannerItemForHero = BannerHelper.GetRandomBannerItemForHero(hero);
					if (randomBannerItemForHero != null)
					{
						hero.BannerItem = new EquipmentElement(randomBannerItemForHero, null, null, false);
					}
				}
			}
		}

		// Token: 0x0600344D RID: 13389 RVA: 0x000DBA68 File Offset: 0x000D9C68
		private void DailyTickHero(Hero hero)
		{
			if (hero.Clan != Clan.PlayerClan)
			{
				EquipmentElement bannerItem = hero.BannerItem;
				BannerItemModel bannerItemModel = Campaign.Current.Models.BannerItemModel;
				if (!bannerItem.IsInvalid() && bannerItemModel.CanBannerBeUpdated(bannerItem.Item) && MBRandom.RandomFloat < 0.1f)
				{
					int bannerLevel = ((BannerComponent)bannerItem.Item.ItemComponent).BannerLevel;
					int bannerItemLevelForHero = bannerItemModel.GetBannerItemLevelForHero(hero);
					if (bannerLevel != bannerItemLevelForHero)
					{
						ItemObject upgradeBannerForHero = this.GetUpgradeBannerForHero(hero, bannerItemLevelForHero);
						if (upgradeBannerForHero != null)
						{
							hero.BannerItem = new EquipmentElement(upgradeBannerForHero, null, null, false);
						}
					}
				}
			}
		}

		// Token: 0x0600344E RID: 13390 RVA: 0x000DBAFC File Offset: 0x000D9CFC
		private ItemObject GetUpgradeBannerForHero(Hero hero, int upgradeBannerLevel)
		{
			ItemObject item = hero.BannerItem.Item;
			foreach (ItemObject itemObject in Campaign.Current.Models.BannerItemModel.GetPossibleRewardBannerItems())
			{
				BannerComponent bannerComponent = (BannerComponent)itemObject.ItemComponent;
				if (itemObject.Culture == item.Culture && bannerComponent.BannerLevel == upgradeBannerLevel && bannerComponent.BannerEffect == ((BannerComponent)item.ItemComponent).BannerEffect)
				{
					return itemObject;
				}
			}
			return BannerHelper.GetRandomBannerItemForHero(hero);
		}

		// Token: 0x0600344F RID: 13391 RVA: 0x000DBBAC File Offset: 0x000D9DAC
		private void CollectLoots(MapEvent mapEvent, PartyBase party, Dictionary<PartyBase, ItemRoster> loot, ItemRoster lootedItems, MBList<TroopRosterElement> lootedCasualties, float lootAmount)
		{
			if (party == PartyBase.MainParty && mapEvent.IsPlayerMapEvent && mapEvent.WinningSide == mapEvent.PlayerSide)
			{
				ItemObject[] array = new ItemObject[2];
				if (mapEvent.IsHideoutBattle)
				{
					array[0] = this.GetBannerRewardForHideoutBattle();
				}
				else if (mapEvent.AttackerSide.MissionSide == mapEvent.PlayerSide && mapEvent.IsSiegeAssault)
				{
					array[0] = this.GetBannerRewardForCapturingFortification(mapEvent.MapEventSettlement);
				}
				array[1] = this.GetBannerRewardForDefeatingNoble(mapEvent, lootedCasualties);
				foreach (ItemObject itemObject in array)
				{
					if (itemObject != null)
					{
						lootedItems.AddToCounts(itemObject, 1);
					}
				}
			}
		}

		// Token: 0x06003450 RID: 13392 RVA: 0x000DBC4C File Offset: 0x000D9E4C
		private void OnHeroComesOfAge(Hero hero)
		{
			if (this.CanBannerBeGivenToHero(hero))
			{
				ItemObject randomBannerItemForHero = BannerHelper.GetRandomBannerItemForHero(hero);
				if (randomBannerItemForHero != null)
				{
					hero.BannerItem = new EquipmentElement(randomBannerItemForHero, null, null, false);
				}
			}
		}

		// Token: 0x06003451 RID: 13393 RVA: 0x000DBC7C File Offset: 0x000D9E7C
		private void OnHeroCreated(Hero hero, bool isBornNaturally = false)
		{
			if (this.CanBannerBeGivenToHero(hero))
			{
				ItemObject randomBannerItemForHero = BannerHelper.GetRandomBannerItemForHero(hero);
				if (randomBannerItemForHero != null)
				{
					hero.BannerItem = new EquipmentElement(randomBannerItemForHero, null, null, false);
				}
			}
		}

		// Token: 0x06003452 RID: 13394 RVA: 0x000DBCAC File Offset: 0x000D9EAC
		private void OnCompanionClanCreated(Clan clan)
		{
			Hero leader = clan.Leader;
			if (leader.BannerItem.IsInvalid())
			{
				ItemObject randomBannerItemForHero = BannerHelper.GetRandomBannerItemForHero(leader);
				if (randomBannerItemForHero != null)
				{
					leader.BannerItem = new EquipmentElement(randomBannerItemForHero, null, null, false);
				}
			}
		}

		// Token: 0x06003453 RID: 13395 RVA: 0x000DBCEC File Offset: 0x000D9EEC
		private bool CanBannerBeLootedFromHero(Hero hero)
		{
			return !this._heroNextBannerLootTime.ContainsKey(hero) || this._heroNextBannerLootTime[hero].IsPast;
		}

		// Token: 0x06003454 RID: 13396 RVA: 0x000DBD1D File Offset: 0x000D9F1D
		private int GetCooldownDays(int bannerLevel)
		{
			if (bannerLevel == 1)
			{
				return 4;
			}
			if (bannerLevel == 1)
			{
				return 8;
			}
			return 12;
		}

		// Token: 0x06003455 RID: 13397 RVA: 0x000DBD30 File Offset: 0x000D9F30
		private void LogBannerLootForHero(Hero hero, int bannerLevel)
		{
			CampaignTime value = CampaignTime.DaysFromNow((float)this.GetCooldownDays(bannerLevel));
			if (!this._heroNextBannerLootTime.ContainsKey(hero))
			{
				this._heroNextBannerLootTime.Add(hero, value);
				return;
			}
			this._heroNextBannerLootTime[hero] = value;
		}

		// Token: 0x06003456 RID: 13398 RVA: 0x000DBD74 File Offset: 0x000D9F74
		private ItemObject GetBannerRewardForHideoutBattle()
		{
			if (MBRandom.RandomFloat <= Campaign.Current.Models.BattleRewardModel.DestroyHideoutBannerLootChance)
			{
				return Campaign.Current.Models.BannerItemModel.GetPossibleRewardBannerItems().ToMBList<ItemObject>().GetRandomElementWithPredicate((ItemObject i) => ((BannerComponent)i.ItemComponent).BannerLevel == 1 && (i.Culture == null || i.Culture.StringId == "neutral_culture"));
			}
			return null;
		}

		// Token: 0x06003457 RID: 13399 RVA: 0x000DBDDC File Offset: 0x000D9FDC
		private ItemObject GetBannerRewardForCapturingFortification(Settlement settlement)
		{
			if (MBRandom.RandomFloat <= Campaign.Current.Models.BattleRewardModel.CaptureSettlementBannerLootChance)
			{
				MBList<ItemObject> mblist = Campaign.Current.Models.BannerItemModel.GetPossibleRewardBannerItems().ToMBList<ItemObject>();
				mblist.Shuffle<ItemObject>();
				int wallLevel = settlement.Town.GetWallLevel();
				foreach (ItemObject itemObject in mblist)
				{
					if (((BannerComponent)itemObject.ItemComponent).BannerLevel == wallLevel && (itemObject.Culture == null || itemObject.Culture.StringId == "neutral_culture" || itemObject.Culture == settlement.Culture))
					{
						return itemObject;
					}
				}
			}
			return null;
		}

		// Token: 0x06003458 RID: 13400 RVA: 0x000DBEB4 File Offset: 0x000DA0B4
		private ItemObject GetBannerRewardForDefeatingNoble(MapEvent mapEvent, MBList<TroopRosterElement> lootedCasualties)
		{
			Hero hero = null;
			foreach (MapEventParty mapEventParty in mapEvent.PartiesOnSide(mapEvent.DefeatedSide))
			{
				if (mapEventParty.Party.IsMobile && mapEventParty.Party.MobileParty.Army != null)
				{
					hero = mapEventParty.Party.MobileParty.Army.ArmyOwner;
					if (hero.BannerItem.IsInvalid())
					{
						hero = null;
						break;
					}
					break;
				}
			}
			if (hero == null)
			{
				CharacterObject character = lootedCasualties.GetRandomElementWithPredicate((TroopRosterElement t) => t.Character.IsHero && !t.Character.HeroObject.BannerItem.IsInvalid()).Character;
				hero = ((character != null) ? character.HeroObject : null);
			}
			if (hero != null && this.CanBannerBeLootedFromHero(hero))
			{
				Clan clan = hero.Clan;
				Hero hero2;
				if (clan == null)
				{
					hero2 = null;
				}
				else
				{
					Kingdom kingdom = clan.Kingdom;
					hero2 = ((kingdom != null) ? kingdom.RulingClan.Leader : null);
				}
				float num;
				if (hero2 == hero)
				{
					num = Campaign.Current.Models.BattleRewardModel.DefeatKingdomRulerBannerLootChance;
				}
				else
				{
					Clan clan2 = hero.Clan;
					if (((clan2 != null) ? clan2.Leader : null) == hero)
					{
						num = Campaign.Current.Models.BattleRewardModel.DefeatClanLeaderBannerLootChance;
					}
					else
					{
						num = Campaign.Current.Models.BattleRewardModel.DefeatRegularHeroBannerLootChance;
					}
				}
				if (MBRandom.RandomFloat <= num)
				{
					this.LogBannerLootForHero(hero, ((BannerComponent)hero.BannerItem.Item.ItemComponent).BannerLevel);
					return hero.BannerItem.Item;
				}
			}
			return null;
		}

		// Token: 0x06003459 RID: 13401 RVA: 0x000DC05C File Offset: 0x000DA25C
		private bool CanBannerBeGivenToHero(Hero hero)
		{
			int heroComesOfAge = Campaign.Current.Models.AgeModel.HeroComesOfAge;
			return hero.Occupation == Occupation.Lord && hero.Age >= (float)heroComesOfAge && hero.BannerItem.IsInvalid() && hero.Clan != Clan.PlayerClan;
		}

		// Token: 0x040010F9 RID: 4345
		private const int BannerLevel1CooldownDays = 4;

		// Token: 0x040010FA RID: 4346
		private const int BannerLevel2CooldownDays = 8;

		// Token: 0x040010FB RID: 4347
		private const int BannerLevel3CooldownDays = 12;

		// Token: 0x040010FC RID: 4348
		private const float BannerItemUpdateChance = 0.1f;

		// Token: 0x040010FD RID: 4349
		private Dictionary<Hero, CampaignTime> _heroNextBannerLootTime = new Dictionary<Hero, CampaignTime>();
	}
}
