﻿using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Inventory;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.SaveSystem;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x020003DE RID: 990
	public class TradeSkillCampaingBehavior : CampaignBehaviorBase, IPlayerTradeBehavior
	{
		// Token: 0x06003D44 RID: 15684 RVA: 0x0012B620 File Offset: 0x00129820
		private void RecordPurchases(ItemRosterElement itemRosterElement, int totalPrice)
		{
			TradeSkillCampaingBehavior.ItemTradeData itemTradeData;
			if (!this.ItemsTradeData.TryGetValue(itemRosterElement.EquipmentElement.Item, out itemTradeData))
			{
				itemTradeData = default(TradeSkillCampaingBehavior.ItemTradeData);
			}
			int num = itemTradeData.NumItemsPurchased + itemRosterElement.Amount;
			float averagePrice = (itemTradeData.AveragePrice * (float)itemTradeData.NumItemsPurchased + (float)totalPrice) / MathF.Max(0.0001f, (float)num);
			this.ItemsTradeData[itemRosterElement.EquipmentElement.Item] = new TradeSkillCampaingBehavior.ItemTradeData(averagePrice, num);
		}

		// Token: 0x06003D45 RID: 15685 RVA: 0x0012B6A4 File Offset: 0x001298A4
		private int RecordSales(ItemRosterElement itemRosterElement, int totalPrice)
		{
			bool flag = false;
			TradeSkillCampaingBehavior.ItemTradeData itemTradeData;
			if (this.ItemsTradeData.TryGetValue(itemRosterElement.EquipmentElement.Item, out itemTradeData))
			{
				flag = true;
			}
			else
			{
				itemTradeData = default(TradeSkillCampaingBehavior.ItemTradeData);
			}
			int num = MathF.Min(itemTradeData.NumItemsPurchased, itemRosterElement.Amount);
			int num2 = itemTradeData.NumItemsPurchased - num;
			float f = (float)num * itemTradeData.AveragePrice;
			float num3 = (float)totalPrice / MathF.Max(0.001f, (float)itemRosterElement.Amount);
			int num4 = MathF.Round((float)num * num3);
			int result = MathF.Max(0, num4 - MathF.Floor(f));
			if (num2 == 0)
			{
				if (flag)
				{
					this.ItemsTradeData.Remove(itemRosterElement.EquipmentElement.Item);
					return result;
				}
			}
			else
			{
				this.ItemsTradeData[itemRosterElement.EquipmentElement.Item] = new TradeSkillCampaingBehavior.ItemTradeData(itemTradeData.AveragePrice, num2);
			}
			return result;
		}

		// Token: 0x06003D46 RID: 15686 RVA: 0x0012B784 File Offset: 0x00129984
		private int GetAveragePriceForItem(ItemRosterElement itemRosterElement)
		{
			TradeSkillCampaingBehavior.ItemTradeData itemTradeData;
			if (!this.ItemsTradeData.TryGetValue(itemRosterElement.EquipmentElement.Item, out itemTradeData))
			{
				return 0;
			}
			return MathF.Round(itemTradeData.AveragePrice);
		}

		// Token: 0x06003D47 RID: 15687 RVA: 0x0012B7BC File Offset: 0x001299BC
		public override void RegisterEvents()
		{
			CampaignEvents.PlayerInventoryExchangeEvent.AddNonSerializedListener(this, new Action<List<ValueTuple<ItemRosterElement, int>>, List<ValueTuple<ItemRosterElement, int>>, bool>(this.InventoryUpdated));
		}

		// Token: 0x06003D48 RID: 15688 RVA: 0x0012B7D8 File Offset: 0x001299D8
		private void InventoryUpdated(List<ValueTuple<ItemRosterElement, int>> purchasedItems, List<ValueTuple<ItemRosterElement, int>> soldItems, bool isTrading)
		{
			if (isTrading)
			{
				foreach (ValueTuple<ItemRosterElement, int> valueTuple in purchasedItems)
				{
					this.ProcessPurchases(valueTuple.Item1, valueTuple.Item2);
				}
				int num = 0;
				foreach (ValueTuple<ItemRosterElement, int> valueTuple2 in soldItems)
				{
					num += this.ProcessSales(valueTuple2.Item1, valueTuple2.Item2);
				}
				SkillLevelingManager.OnTradeProfitMade(PartyBase.MainParty, num);
				CampaignEventDispatcher.Instance.OnPlayerTradeProfit(num);
			}
		}

		// Token: 0x06003D49 RID: 15689 RVA: 0x0012B89C File Offset: 0x00129A9C
		private int ProcessSales(ItemRosterElement itemRosterElement, int totalPrice)
		{
			if (itemRosterElement.EquipmentElement.ItemModifier == null)
			{
				return this.RecordSales(itemRosterElement, totalPrice);
			}
			return 0;
		}

		// Token: 0x06003D4A RID: 15690 RVA: 0x0012B8C4 File Offset: 0x00129AC4
		private void ProcessPurchases(ItemRosterElement itemRosterElement, int totalPrice)
		{
			if (itemRosterElement.EquipmentElement.ItemModifier == null)
			{
				this.RecordPurchases(itemRosterElement, totalPrice);
			}
		}

		// Token: 0x06003D4B RID: 15691 RVA: 0x0012B8EA File Offset: 0x00129AEA
		public override void SyncData(IDataStore dataStore)
		{
			dataStore.SyncData<Dictionary<ItemObject, TradeSkillCampaingBehavior.ItemTradeData>>("ItemsTradeData", ref this.ItemsTradeData);
		}

		// Token: 0x06003D4C RID: 15692 RVA: 0x0012B900 File Offset: 0x00129B00
		public int GetProjectedProfit(ItemRosterElement itemRosterElement, int itemCost)
		{
			if (itemRosterElement.EquipmentElement.ItemModifier != null)
			{
				return 0;
			}
			int averagePriceForItem = this.GetAveragePriceForItem(itemRosterElement);
			return itemCost - averagePriceForItem;
		}

		// Token: 0x04001227 RID: 4647
		private Dictionary<ItemObject, TradeSkillCampaingBehavior.ItemTradeData> ItemsTradeData = new Dictionary<ItemObject, TradeSkillCampaingBehavior.ItemTradeData>();

		// Token: 0x0200074B RID: 1867
		public class TradeSkillCampaingBehaviorTypeDefiner : SaveableTypeDefiner
		{
			// Token: 0x060059AD RID: 22957 RVA: 0x0018452F File Offset: 0x0018272F
			public TradeSkillCampaingBehaviorTypeDefiner() : base(150794)
			{
			}

			// Token: 0x060059AE RID: 22958 RVA: 0x0018453C File Offset: 0x0018273C
			protected override void DefineStructTypes()
			{
				base.AddStructDefinition(typeof(TradeSkillCampaingBehavior.ItemTradeData), 10, null);
			}

			// Token: 0x060059AF RID: 22959 RVA: 0x00184551 File Offset: 0x00182751
			protected override void DefineContainerDefinitions()
			{
				base.ConstructContainerDefinition(typeof(Dictionary<ItemObject, TradeSkillCampaingBehavior.ItemTradeData>));
			}
		}

		// Token: 0x0200074C RID: 1868
		internal struct ItemTradeData
		{
			// Token: 0x060059B0 RID: 22960 RVA: 0x00184563 File Offset: 0x00182763
			public ItemTradeData(float averagePrice, int numItemsPurchased)
			{
				this.AveragePrice = averagePrice;
				this.NumItemsPurchased = numItemsPurchased;
			}

			// Token: 0x060059B1 RID: 22961 RVA: 0x00184574 File Offset: 0x00182774
			public static void AutoGeneratedStaticCollectObjectsItemTradeData(object o, List<object> collectedObjects)
			{
				((TradeSkillCampaingBehavior.ItemTradeData)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
			}

			// Token: 0x060059B2 RID: 22962 RVA: 0x00184590 File Offset: 0x00182790
			private void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
			{
			}

			// Token: 0x060059B3 RID: 22963 RVA: 0x00184592 File Offset: 0x00182792
			internal static object AutoGeneratedGetMemberValueAveragePrice(object o)
			{
				return ((TradeSkillCampaingBehavior.ItemTradeData)o).AveragePrice;
			}

			// Token: 0x060059B4 RID: 22964 RVA: 0x001845A4 File Offset: 0x001827A4
			internal static object AutoGeneratedGetMemberValueNumItemsPurchased(object o)
			{
				return ((TradeSkillCampaingBehavior.ItemTradeData)o).NumItemsPurchased;
			}

			// Token: 0x04001EB9 RID: 7865
			[SaveableField(10)]
			public readonly float AveragePrice;

			// Token: 0x04001EBA RID: 7866
			[SaveableField(20)]
			public readonly int NumItemsPurchased;
		}
	}
}
