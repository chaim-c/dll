﻿using System;
using System.Collections.Generic;
using Helpers;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.Inventory
{
	// Token: 0x020000D3 RID: 211
	public class InventoryManager
	{
		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x0600135D RID: 4957 RVA: 0x000579DE File Offset: 0x00055BDE
		public InventoryMode CurrentMode
		{
			get
			{
				return this._currentMode;
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x0600135E RID: 4958 RVA: 0x000579E6 File Offset: 0x00055BE6
		public static InventoryManager Instance
		{
			get
			{
				return Campaign.Current.InventoryManager;
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x0600135F RID: 4959 RVA: 0x000579F2 File Offset: 0x00055BF2
		public static InventoryLogic InventoryLogic
		{
			get
			{
				return InventoryManager.Instance._inventoryLogic;
			}
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x000579FE File Offset: 0x00055BFE
		public void PlayerAcceptTradeOffer()
		{
			InventoryLogic inventoryLogic = this._inventoryLogic;
			if (inventoryLogic == null)
			{
				return;
			}
			inventoryLogic.SetPlayerAcceptTraderOffer();
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x00057A10 File Offset: 0x00055C10
		public void CloseInventoryPresentation(bool fromCancel)
		{
			if (this._inventoryLogic.DoneLogic())
			{
				Game.Current.GameStateManager.PopState(0);
				InventoryManager.DoneLogicExtrasDelegate doneLogicExtrasDelegate = this._doneLogicExtrasDelegate;
				if (doneLogicExtrasDelegate != null)
				{
					doneLogicExtrasDelegate();
				}
				this._doneLogicExtrasDelegate = null;
				this._inventoryLogic = null;
			}
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x00057A50 File Offset: 0x00055C50
		private void OpenInventoryPresentation(TextObject leftRosterName)
		{
			ItemRoster itemRoster = new ItemRoster();
			if (Game.Current.CheatMode)
			{
				TestCommonBase baseInstance = TestCommonBase.BaseInstance;
				if (baseInstance == null || !baseInstance.IsTestEnabled)
				{
					MBReadOnlyList<ItemObject> objectTypeList = Game.Current.ObjectManager.GetObjectTypeList<ItemObject>();
					for (int num = 0; num != objectTypeList.Count; num++)
					{
						ItemObject item = objectTypeList[num];
						itemRoster.AddToCounts(item, 10);
					}
				}
			}
			this._inventoryLogic = new InventoryLogic(null);
			this._inventoryLogic.Initialize(itemRoster, MobileParty.MainParty, false, true, CharacterObject.PlayerCharacter, InventoryManager.InventoryCategoryType.None, InventoryManager.GetCurrentMarketData(), false, leftRosterName, null, null);
			InventoryState inventoryState = Game.Current.GameStateManager.CreateState<InventoryState>();
			inventoryState.InitializeLogic(this._inventoryLogic);
			Game.Current.GameStateManager.PushState(inventoryState, 0);
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x00057B18 File Offset: 0x00055D18
		private static IMarketData GetCurrentMarketData()
		{
			IMarketData marketData = null;
			if (Campaign.Current.GameMode == CampaignGameMode.Campaign)
			{
				Settlement settlement = MobileParty.MainParty.CurrentSettlement;
				if (settlement == null)
				{
					settlement = SettlementHelper.FindNearestTown(null, null);
				}
				if (settlement != null)
				{
					if (settlement.IsVillage)
					{
						marketData = settlement.Village.MarketData;
					}
					else if (settlement.IsTown)
					{
						marketData = settlement.Town.MarketData;
					}
				}
			}
			if (marketData == null)
			{
				marketData = new FakeMarketData();
			}
			return marketData;
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x00057B84 File Offset: 0x00055D84
		public static void OpenScreenAsInventoryOfSubParty(MobileParty rightParty, MobileParty leftParty, InventoryManager.DoneLogicExtrasDelegate doneLogicExtrasDelegate)
		{
			Hero leaderHero = rightParty.LeaderHero;
			InventoryLogic inventoryLogic = new InventoryLogic(rightParty, (leaderHero != null) ? leaderHero.CharacterObject : null, leftParty.Party);
			InventoryLogic inventoryLogic2 = inventoryLogic;
			ItemRoster itemRoster = leftParty.ItemRoster;
			ItemRoster itemRoster2 = rightParty.ItemRoster;
			TroopRoster memberRoster = rightParty.MemberRoster;
			bool isTrading = false;
			bool isSpecialActionsPermitted = false;
			Hero leaderHero2 = rightParty.LeaderHero;
			inventoryLogic2.Initialize(itemRoster, itemRoster2, memberRoster, isTrading, isSpecialActionsPermitted, (leaderHero2 != null) ? leaderHero2.CharacterObject : null, InventoryManager.InventoryCategoryType.None, InventoryManager.GetCurrentMarketData(), false, null, null, null);
			InventoryManager.Instance._doneLogicExtrasDelegate = doneLogicExtrasDelegate;
			InventoryState inventoryState = Game.Current.GameStateManager.CreateState<InventoryState>();
			inventoryState.InitializeLogic(inventoryLogic);
			Game.Current.GameStateManager.PushState(inventoryState, 0);
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x00057C1C File Offset: 0x00055E1C
		public static void OpenScreenAsInventoryForCraftedItemDecomposition(MobileParty party, CharacterObject character, InventoryManager.DoneLogicExtrasDelegate doneLogicExtrasDelegate)
		{
			InventoryManager.Instance._inventoryLogic = new InventoryLogic(null);
			InventoryManager.Instance._inventoryLogic.Initialize(new ItemRoster(), party.ItemRoster, party.MemberRoster, false, false, character, InventoryManager.InventoryCategoryType.None, InventoryManager.GetCurrentMarketData(), false, null, null, null);
			InventoryManager.Instance._doneLogicExtrasDelegate = doneLogicExtrasDelegate;
			InventoryState inventoryState = Game.Current.GameStateManager.CreateState<InventoryState>();
			inventoryState.InitializeLogic(InventoryManager.Instance._inventoryLogic);
			Game.Current.GameStateManager.PushState(inventoryState, 0);
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x00057CA4 File Offset: 0x00055EA4
		public static void OpenScreenAsInventoryOf(MobileParty party, CharacterObject character)
		{
			InventoryManager.Instance._inventoryLogic = new InventoryLogic(null);
			InventoryManager.Instance._inventoryLogic.Initialize(new ItemRoster(), party.ItemRoster, party.MemberRoster, false, true, character, InventoryManager.InventoryCategoryType.None, InventoryManager.GetCurrentMarketData(), false, null, null, null);
			InventoryState inventoryState = Game.Current.GameStateManager.CreateState<InventoryState>();
			inventoryState.InitializeLogic(InventoryManager.Instance._inventoryLogic);
			Game.Current.GameStateManager.PushState(inventoryState, 0);
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x00057D20 File Offset: 0x00055F20
		public static void OpenScreenAsInventoryOf(PartyBase rightParty, PartyBase leftParty)
		{
			InventoryManager.Instance._inventoryLogic = new InventoryLogic(leftParty);
			InventoryLogic inventoryLogic = InventoryManager.Instance._inventoryLogic;
			ItemRoster itemRoster = leftParty.ItemRoster;
			ItemRoster itemRoster2 = rightParty.ItemRoster;
			TroopRoster memberRoster = rightParty.MemberRoster;
			bool isTrading = false;
			bool isSpecialActionsPermitted = false;
			Hero leaderHero = rightParty.LeaderHero;
			inventoryLogic.Initialize(itemRoster, itemRoster2, memberRoster, isTrading, isSpecialActionsPermitted, (leaderHero != null) ? leaderHero.CharacterObject : null, InventoryManager.InventoryCategoryType.None, InventoryManager.GetCurrentMarketData(), false, null, leftParty.MemberRoster, null);
			InventoryState inventoryState = Game.Current.GameStateManager.CreateState<InventoryState>();
			inventoryState.InitializeLogic(InventoryManager.Instance._inventoryLogic);
			Game.Current.GameStateManager.PushState(inventoryState, 0);
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x00057DB2 File Offset: 0x00055FB2
		public static void OpenScreenAsInventory(InventoryManager.DoneLogicExtrasDelegate doneLogicExtrasDelegate = null)
		{
			InventoryManager.Instance._currentMode = InventoryMode.Default;
			InventoryManager.Instance.OpenInventoryPresentation(new TextObject("{=02c5bQSM}Discard", null));
			InventoryManager.Instance._doneLogicExtrasDelegate = doneLogicExtrasDelegate;
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x00057DDF File Offset: 0x00055FDF
		public static void OpenCampaignBattleLootScreen()
		{
			InventoryManager.OpenScreenAsLoot(new Dictionary<PartyBase, ItemRoster>
			{
				{
					PartyBase.MainParty,
					MapEvent.PlayerMapEvent.ItemRosterForPlayerLootShare(PartyBase.MainParty)
				}
			});
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x00057E08 File Offset: 0x00056008
		public static void OpenScreenAsLoot(Dictionary<PartyBase, ItemRoster> itemRostersToLoot)
		{
			ItemRoster leftItemRoster = itemRostersToLoot[PartyBase.MainParty];
			InventoryManager.Instance._currentMode = InventoryMode.Loot;
			InventoryManager.Instance._inventoryLogic = new InventoryLogic(null);
			InventoryManager.Instance._inventoryLogic.Initialize(leftItemRoster, MobileParty.MainParty.ItemRoster, MobileParty.MainParty.MemberRoster, false, true, CharacterObject.PlayerCharacter, InventoryManager.InventoryCategoryType.None, InventoryManager.GetCurrentMarketData(), false, null, null, null);
			InventoryState inventoryState = Game.Current.GameStateManager.CreateState<InventoryState>();
			inventoryState.InitializeLogic(InventoryManager.Instance._inventoryLogic);
			Game.Current.GameStateManager.PushState(inventoryState, 0);
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x00057EA4 File Offset: 0x000560A4
		public static void OpenScreenAsStash(ItemRoster stash)
		{
			InventoryManager.Instance._currentMode = InventoryMode.Stash;
			InventoryManager.Instance._inventoryLogic = new InventoryLogic(null);
			InventoryManager.Instance._inventoryLogic.Initialize(stash, MobileParty.MainParty, false, false, CharacterObject.PlayerCharacter, InventoryManager.InventoryCategoryType.None, InventoryManager.GetCurrentMarketData(), false, new TextObject("{=nZbaYvVx}Stash", null), null, null);
			InventoryState inventoryState = Game.Current.GameStateManager.CreateState<InventoryState>();
			inventoryState.InitializeLogic(InventoryManager.Instance._inventoryLogic);
			Game.Current.GameStateManager.PushState(inventoryState, 0);
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x00057F30 File Offset: 0x00056130
		public static void OpenScreenAsWarehouse(ItemRoster stash, InventoryLogic.CapacityData otherSideCapacity)
		{
			InventoryManager.Instance._currentMode = InventoryMode.Warehouse;
			InventoryManager.Instance._inventoryLogic = new InventoryLogic(null);
			InventoryManager.Instance._inventoryLogic.Initialize(stash, MobileParty.MainParty, false, false, CharacterObject.PlayerCharacter, InventoryManager.InventoryCategoryType.None, InventoryManager.GetCurrentMarketData(), false, new TextObject("{=anTRftmb}Warehouse", null), null, otherSideCapacity);
			InventoryState inventoryState = Game.Current.GameStateManager.CreateState<InventoryState>();
			inventoryState.InitializeLogic(InventoryManager.Instance._inventoryLogic);
			Game.Current.GameStateManager.PushState(inventoryState, 0);
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x00057FBC File Offset: 0x000561BC
		public static void OpenScreenAsReceiveItems(ItemRoster items, TextObject leftRosterName, InventoryManager.DoneLogicExtrasDelegate doneLogicDelegate = null)
		{
			InventoryManager.Instance._currentMode = InventoryMode.Default;
			InventoryManager.Instance._inventoryLogic = new InventoryLogic(null);
			InventoryManager.Instance._inventoryLogic.Initialize(items, MobileParty.MainParty.ItemRoster, MobileParty.MainParty.MemberRoster, false, true, CharacterObject.PlayerCharacter, InventoryManager.InventoryCategoryType.None, InventoryManager.GetCurrentMarketData(), false, leftRosterName, null, null);
			InventoryManager.Instance._doneLogicExtrasDelegate = doneLogicDelegate;
			InventoryState inventoryState = Game.Current.GameStateManager.CreateState<InventoryState>();
			inventoryState.InitializeLogic(InventoryManager.Instance._inventoryLogic);
			Game.Current.GameStateManager.PushState(inventoryState, 0);
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x00058058 File Offset: 0x00056258
		public static void OpenTradeWithCaravanOrAlleyParty(MobileParty caravan, InventoryManager.InventoryCategoryType merchantItemType = InventoryManager.InventoryCategoryType.None)
		{
			InventoryManager.Instance._currentMode = InventoryMode.Trade;
			InventoryManager.Instance._inventoryLogic = new InventoryLogic(caravan.Party);
			InventoryManager.Instance._inventoryLogic.Initialize(caravan.Party.ItemRoster, PartyBase.MainParty.ItemRoster, PartyBase.MainParty.MemberRoster, true, true, CharacterObject.PlayerCharacter, merchantItemType, InventoryManager.GetCurrentMarketData(), false, null, null, null);
			InventoryManager.Instance._inventoryLogic.SetInventoryListener(new InventoryManager.CaravanInventoryListener(caravan));
			InventoryState inventoryState = Game.Current.GameStateManager.CreateState<InventoryState>();
			inventoryState.InitializeLogic(InventoryManager.Instance._inventoryLogic);
			Game.Current.GameStateManager.PushState(inventoryState, 0);
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x0005810A File Offset: 0x0005630A
		public static void ActivateTradeWithCurrentSettlement()
		{
			InventoryManager.OpenScreenAsTrade(Settlement.CurrentSettlement.ItemRoster, Settlement.CurrentSettlement.SettlementComponent, InventoryManager.InventoryCategoryType.None, null);
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x00058128 File Offset: 0x00056328
		public static void OpenScreenAsTrade(ItemRoster leftRoster, SettlementComponent settlementComponent, InventoryManager.InventoryCategoryType merchantItemType = InventoryManager.InventoryCategoryType.None, InventoryManager.DoneLogicExtrasDelegate doneLogicExtrasDelegate = null)
		{
			InventoryManager.Instance._currentMode = InventoryMode.Trade;
			InventoryManager.Instance._inventoryLogic = new InventoryLogic(settlementComponent.Owner);
			InventoryManager.Instance._inventoryLogic.Initialize(leftRoster, PartyBase.MainParty.ItemRoster, PartyBase.MainParty.MemberRoster, true, true, CharacterObject.PlayerCharacter, merchantItemType, InventoryManager.GetCurrentMarketData(), false, null, null, null);
			InventoryManager.Instance._inventoryLogic.SetInventoryListener(new InventoryManager.MerchantInventoryListener(settlementComponent));
			InventoryManager.Instance._doneLogicExtrasDelegate = doneLogicExtrasDelegate;
			InventoryState inventoryState = Game.Current.GameStateManager.CreateState<InventoryState>();
			inventoryState.InitializeLogic(InventoryManager.Instance._inventoryLogic);
			Game.Current.GameStateManager.PushState(inventoryState, 0);
			if (inventoryState.Handler != null)
			{
				inventoryState.Handler.FilterInventoryAtOpening(merchantItemType);
				return;
			}
			Debug.FailedAssert("Inventory State handler is not initialized when filtering inventory", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Inventory\\InventoryManager.cs", "OpenScreenAsTrade", 395);
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x0005820C File Offset: 0x0005640C
		public static InventoryItemType GetInventoryItemTypeOfItem(ItemObject item)
		{
			if (item != null)
			{
				switch (item.ItemType)
				{
				case ItemObject.ItemTypeEnum.Horse:
					return InventoryItemType.Horse;
				case ItemObject.ItemTypeEnum.OneHandedWeapon:
					return InventoryItemType.Weapon;
				case ItemObject.ItemTypeEnum.TwoHandedWeapon:
					return InventoryItemType.Weapon;
				case ItemObject.ItemTypeEnum.Polearm:
					return InventoryItemType.Weapon;
				case ItemObject.ItemTypeEnum.Arrows:
					return InventoryItemType.Weapon;
				case ItemObject.ItemTypeEnum.Bolts:
					return InventoryItemType.Weapon;
				case ItemObject.ItemTypeEnum.Shield:
					return InventoryItemType.Shield;
				case ItemObject.ItemTypeEnum.Bow:
					return InventoryItemType.Weapon;
				case ItemObject.ItemTypeEnum.Crossbow:
					return InventoryItemType.Weapon;
				case ItemObject.ItemTypeEnum.Thrown:
					return InventoryItemType.Weapon;
				case ItemObject.ItemTypeEnum.Goods:
					return InventoryItemType.Goods;
				case ItemObject.ItemTypeEnum.HeadArmor:
					return InventoryItemType.HeadArmor;
				case ItemObject.ItemTypeEnum.BodyArmor:
					return InventoryItemType.BodyArmor;
				case ItemObject.ItemTypeEnum.LegArmor:
					return InventoryItemType.LegArmor;
				case ItemObject.ItemTypeEnum.HandArmor:
					return InventoryItemType.HandArmor;
				case ItemObject.ItemTypeEnum.Pistol:
					return InventoryItemType.Weapon;
				case ItemObject.ItemTypeEnum.Musket:
					return InventoryItemType.Weapon;
				case ItemObject.ItemTypeEnum.Bullets:
					return InventoryItemType.Weapon;
				case ItemObject.ItemTypeEnum.Animal:
					return InventoryItemType.Animal;
				case ItemObject.ItemTypeEnum.Book:
					return InventoryItemType.Book;
				case ItemObject.ItemTypeEnum.Cape:
					return InventoryItemType.Cape;
				case ItemObject.ItemTypeEnum.HorseHarness:
					return InventoryItemType.HorseHarness;
				case ItemObject.ItemTypeEnum.Banner:
					return InventoryItemType.Banner;
				}
			}
			return InventoryItemType.None;
		}

		// Token: 0x040006A5 RID: 1701
		private InventoryMode _currentMode;

		// Token: 0x040006A6 RID: 1702
		private InventoryLogic _inventoryLogic;

		// Token: 0x040006A7 RID: 1703
		private InventoryManager.DoneLogicExtrasDelegate _doneLogicExtrasDelegate;

		// Token: 0x020004E8 RID: 1256
		public enum InventoryCategoryType
		{
			// Token: 0x04001513 RID: 5395
			None = -1,
			// Token: 0x04001514 RID: 5396
			All,
			// Token: 0x04001515 RID: 5397
			Armors,
			// Token: 0x04001516 RID: 5398
			Weapon,
			// Token: 0x04001517 RID: 5399
			Shield,
			// Token: 0x04001518 RID: 5400
			HorseCategory,
			// Token: 0x04001519 RID: 5401
			Goods,
			// Token: 0x0400151A RID: 5402
			CategoryTypeAmount
		}

		// Token: 0x020004E9 RID: 1257
		// (Invoke) Token: 0x06004377 RID: 17271
		public delegate void DoneLogicExtrasDelegate();

		// Token: 0x020004EA RID: 1258
		private class CaravanInventoryListener : InventoryListener
		{
			// Token: 0x0600437A RID: 17274 RVA: 0x001465E6 File Offset: 0x001447E6
			public CaravanInventoryListener(MobileParty caravan)
			{
				this._caravan = caravan;
			}

			// Token: 0x0600437B RID: 17275 RVA: 0x001465F5 File Offset: 0x001447F5
			public override int GetGold()
			{
				return this._caravan.PartyTradeGold;
			}

			// Token: 0x0600437C RID: 17276 RVA: 0x00146602 File Offset: 0x00144802
			public override TextObject GetTraderName()
			{
				if (this._caravan.LeaderHero == null)
				{
					return this._caravan.Name;
				}
				return this._caravan.LeaderHero.Name;
			}

			// Token: 0x0600437D RID: 17277 RVA: 0x0014662D File Offset: 0x0014482D
			public override void SetGold(int gold)
			{
				this._caravan.PartyTradeGold = gold;
			}

			// Token: 0x0600437E RID: 17278 RVA: 0x0014663B File Offset: 0x0014483B
			public override PartyBase GetOppositeParty()
			{
				return this._caravan.Party;
			}

			// Token: 0x0600437F RID: 17279 RVA: 0x00146648 File Offset: 0x00144848
			public override void OnTransaction()
			{
				throw new NotImplementedException();
			}

			// Token: 0x0400151B RID: 5403
			private MobileParty _caravan;
		}

		// Token: 0x020004EB RID: 1259
		private class MerchantInventoryListener : InventoryListener
		{
			// Token: 0x06004380 RID: 17280 RVA: 0x0014664F File Offset: 0x0014484F
			public MerchantInventoryListener(SettlementComponent settlementComponent)
			{
				this._settlementComponent = settlementComponent;
			}

			// Token: 0x06004381 RID: 17281 RVA: 0x0014665E File Offset: 0x0014485E
			public override TextObject GetTraderName()
			{
				return this._settlementComponent.Owner.Name;
			}

			// Token: 0x06004382 RID: 17282 RVA: 0x00146670 File Offset: 0x00144870
			public override PartyBase GetOppositeParty()
			{
				return this._settlementComponent.Owner;
			}

			// Token: 0x06004383 RID: 17283 RVA: 0x0014667D File Offset: 0x0014487D
			public override int GetGold()
			{
				return this._settlementComponent.Gold;
			}

			// Token: 0x06004384 RID: 17284 RVA: 0x0014668A File Offset: 0x0014488A
			public override void SetGold(int gold)
			{
				this._settlementComponent.ChangeGold(gold - this._settlementComponent.Gold);
			}

			// Token: 0x06004385 RID: 17285 RVA: 0x001466A4 File Offset: 0x001448A4
			public override void OnTransaction()
			{
				throw new NotImplementedException();
			}

			// Token: 0x0400151C RID: 5404
			private SettlementComponent _settlementComponent;
		}
	}
}
