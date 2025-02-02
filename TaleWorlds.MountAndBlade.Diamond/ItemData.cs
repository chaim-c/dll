using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000129 RID: 297
	public class ItemData
	{
		// Token: 0x17000224 RID: 548
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x0000887C File Offset: 0x00006A7C
		// (set) Token: 0x06000690 RID: 1680 RVA: 0x00008884 File Offset: 0x00006A84
		public string TypeId { get; set; }

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000691 RID: 1681 RVA: 0x0000888D File Offset: 0x00006A8D
		// (set) Token: 0x06000692 RID: 1682 RVA: 0x00008895 File Offset: 0x00006A95
		public string ModifierId { get; set; }

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x0000889E File Offset: 0x00006A9E
		// (set) Token: 0x06000694 RID: 1684 RVA: 0x000088A6 File Offset: 0x00006AA6
		public int? Index { get; set; }

		// Token: 0x06000695 RID: 1685 RVA: 0x000088AF File Offset: 0x00006AAF
		public void CopyItemData(ItemData itemdata)
		{
			this.TypeId = itemdata.TypeId;
			this.ModifierId = itemdata.ModifierId;
			this.Index = itemdata.Index;
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000696 RID: 1686 RVA: 0x000088D5 File Offset: 0x00006AD5
		private ItemType ItemType
		{
			get
			{
				return ItemList.GetItemTypeOf(this.TypeId);
			}
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x000088E4 File Offset: 0x00006AE4
		private static int GetInventoryItemTypeOfItem(ItemType itemType)
		{
			switch (itemType)
			{
			case ItemType.Horse:
				return 64;
			case ItemType.OneHandedWeapon:
				return 1;
			case ItemType.TwoHandedWeapon:
				return 1;
			case ItemType.Polearm:
				return 1;
			case ItemType.Arrows:
				return 1;
			case ItemType.Bolts:
				return 1;
			case ItemType.Shield:
				return 2;
			case ItemType.Bow:
				return 1;
			case ItemType.Crossbow:
				return 1;
			case ItemType.Thrown:
				return 1;
			case ItemType.Goods:
				return 256;
			case ItemType.HeadArmor:
				return 4;
			case ItemType.BodyArmor:
				return 8;
			case ItemType.LegArmor:
				return 16;
			case ItemType.HandArmor:
				return 32;
			case ItemType.Pistol:
				return 1;
			case ItemType.Musket:
				return 1;
			case ItemType.Bullets:
				return 1;
			case ItemType.Animal:
				return 1024;
			case ItemType.Book:
				return 512;
			case ItemType.Cape:
				return 2048;
			case ItemType.HorseHarness:
				return 128;
			}
			return 0;
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0000899B File Offset: 0x00006B9B
		public bool CanItemToEquipmentDragPossible(int equipmentIndex)
		{
			return ItemData.CanItemToEquipmentDragPossible(this.TypeId, equipmentIndex);
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x000089AC File Offset: 0x00006BAC
		public static bool CanItemToEquipmentDragPossible(string itemTypeId, int equipmentIndex)
		{
			InventoryItemType inventoryItemTypeOfItem = (InventoryItemType)ItemData.GetInventoryItemTypeOfItem(ItemList.GetItemTypeOf(itemTypeId));
			bool result = false;
			if (equipmentIndex == 0 || equipmentIndex == 1 || equipmentIndex == 2 || equipmentIndex == 3)
			{
				result = (inventoryItemTypeOfItem == InventoryItemType.Weapon || inventoryItemTypeOfItem == InventoryItemType.Shield);
			}
			else if (equipmentIndex == 5)
			{
				result = (inventoryItemTypeOfItem == InventoryItemType.HeadArmor);
			}
			else if (equipmentIndex == 6)
			{
				result = (inventoryItemTypeOfItem == InventoryItemType.BodyArmor);
			}
			else if (equipmentIndex == 7)
			{
				result = (inventoryItemTypeOfItem == InventoryItemType.LegArmor);
			}
			else if (equipmentIndex == 8)
			{
				result = (inventoryItemTypeOfItem == InventoryItemType.HandArmor);
			}
			else if (equipmentIndex == 9)
			{
				result = (inventoryItemTypeOfItem == InventoryItemType.Cape);
			}
			else if (equipmentIndex == 10)
			{
				result = (inventoryItemTypeOfItem == InventoryItemType.Horse);
			}
			else if (equipmentIndex == 11)
			{
				result = (inventoryItemTypeOfItem == InventoryItemType.HorseHarness);
			}
			return result;
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x00008A40 File Offset: 0x00006C40
		public int Price
		{
			get
			{
				return ItemData.GetPriceOf(this.TypeId, this.ModifierId);
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x00008A53 File Offset: 0x00006C53
		public bool IsValid
		{
			get
			{
				return ItemData.IsItemValid(this.TypeId, this.ModifierId);
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x00008A66 File Offset: 0x00006C66
		public string ItemKey
		{
			get
			{
				return this.TypeId + "|" + this.ModifierId;
			}
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x00008A7E File Offset: 0x00006C7E
		public static int GetPriceOf(string itemId, string modifierId)
		{
			return ItemList.GetPriceOf(itemId, modifierId);
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x00008A87 File Offset: 0x00006C87
		public static bool IsItemValid(string itemId, string modifierId)
		{
			return ItemList.IsItemValid(itemId, modifierId);
		}
	}
}
