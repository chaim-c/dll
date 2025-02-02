using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Inventory;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.Core.ViewModelCollection.Generic;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Inventory
{
	// Token: 0x02000080 RID: 128
	public class ItemMenuVM : ViewModel
	{
		// Token: 0x06000B74 RID: 2932 RVA: 0x0002CB90 File Offset: 0x0002AD90
		public ItemMenuVM(Action<ItemVM, int> resetComparedItems, InventoryLogic inventoryLogic, Func<WeaponComponentData, ItemObject.ItemUsageSetFlags> getItemUsageSetFlags, Func<EquipmentIndex, SPItemVM> getEquipmentAtIndex)
		{
			this._resetComparedItems = resetComparedItems;
			this._inventoryLogic = inventoryLogic;
			this._comparedItemProperties = new MBBindingList<ItemMenuTooltipPropertyVM>();
			this._targetItemProperties = new MBBindingList<ItemMenuTooltipPropertyVM>();
			this._getItemUsageSetFlags = getItemUsageSetFlags;
			this._getEquipmentAtIndex = getEquipmentAtIndex;
			this.TargetItemFlagList = new MBBindingList<ItemFlagVM>();
			this.ComparedItemFlagList = new MBBindingList<ItemFlagVM>();
			this.AlternativeUsages = new MBBindingList<StringItemWithHintVM>();
			this._tradeRumorsBehavior = Campaign.Current.GetCampaignBehavior<ITradeRumorCampaignBehavior>();
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x0002CE7C File Offset: 0x0002B07C
		public void SetItem(SPItemVM item, ItemVM comparedItem = null, BasicCharacterObject character = null, int alternativeUsageIndex = 0)
		{
			this.IsInitializationOver = false;
			this._targetItem = item;
			this._comparedItem = comparedItem;
			ItemVM comparedItem2 = this._comparedItem;
			object obj;
			EquipmentElement equipmentElement;
			if (comparedItem2 == null)
			{
				obj = null;
			}
			else
			{
				equipmentElement = comparedItem2.ItemRosterElement.EquipmentElement;
				obj = equipmentElement.Item;
			}
			this.IsComparing = (obj != null);
			this.IsPlayerItem = (item.InventorySide == InventoryLogic.InventorySide.PlayerInventory);
			this._character = character;
			this.ImageIdentifier = item.ImageIdentifier;
			this.ComparedImageIdentifier = ((comparedItem != null) ? comparedItem.ImageIdentifier : null);
			this.ItemName = item.ItemDescription;
			string comparedItemName;
			if (comparedItem == null)
			{
				comparedItemName = null;
			}
			else
			{
				equipmentElement = comparedItem.ItemRosterElement.EquipmentElement;
				comparedItemName = equipmentElement.GetModifiedItemName().ToString();
			}
			this.ComparedItemName = comparedItemName;
			this.TargetItemProperties.Clear();
			this.ComparedItemProperties.Clear();
			this.TargetItemFlagList.Clear();
			this.ComparedItemFlagList.Clear();
			this.AlternativeUsages.Clear();
			this.SetGeneralComponentTooltip(true);
			Town town = this._inventoryLogic.CurrentSettlementComponent as Town;
			if (town != null && Game.Current.IsDevelopmentMode)
			{
				MBBindingList<ItemMenuTooltipPropertyVM> targetItemProperties = this.TargetItemProperties;
				string definition = "Category:";
				equipmentElement = item.ItemRosterElement.EquipmentElement;
				this.CreateProperty(targetItemProperties, definition, equipmentElement.Item.ItemCategory.GetName().ToString(), 0, null);
				MBBindingList<ItemMenuTooltipPropertyVM> targetItemProperties2 = this.TargetItemProperties;
				string definition2 = "Supply:";
				TownMarketData marketData = town.MarketData;
				equipmentElement = item.ItemRosterElement.EquipmentElement;
				this.CreateProperty(targetItemProperties2, definition2, marketData.GetSupply(equipmentElement.Item.ItemCategory).ToString(), 0, null);
				MBBindingList<ItemMenuTooltipPropertyVM> targetItemProperties3 = this.TargetItemProperties;
				string definition3 = "Demand:";
				TownMarketData marketData2 = town.MarketData;
				equipmentElement = item.ItemRosterElement.EquipmentElement;
				this.CreateProperty(targetItemProperties3, definition3, marketData2.GetDemand(equipmentElement.Item.ItemCategory).ToString(), 0, null);
				MBBindingList<ItemMenuTooltipPropertyVM> targetItemProperties4 = this.TargetItemProperties;
				string definition4 = "Price Index:";
				TownMarketData marketData3 = town.MarketData;
				equipmentElement = item.ItemRosterElement.EquipmentElement;
				this.CreateProperty(targetItemProperties4, definition4, marketData3.GetPriceFactor(equipmentElement.Item.ItemCategory).ToString(), 0, null);
			}
			equipmentElement = item.ItemRosterElement.EquipmentElement;
			if (equipmentElement.Item.HasArmorComponent)
			{
				this.SetArmorComponentTooltip();
			}
			else
			{
				equipmentElement = item.ItemRosterElement.EquipmentElement;
				if (equipmentElement.Item.WeaponComponent != null)
				{
					equipmentElement = this._targetItem.ItemRosterElement.EquipmentElement;
					this.SetWeaponComponentTooltip(equipmentElement, alternativeUsageIndex, EquipmentElement.Invalid, -1, true);
				}
				else
				{
					equipmentElement = item.ItemRosterElement.EquipmentElement;
					if (equipmentElement.Item.HasHorseComponent)
					{
						this.SetHorseComponentTooltip();
					}
					else
					{
						equipmentElement = item.ItemRosterElement.EquipmentElement;
						if (equipmentElement.Item.IsFood)
						{
							this.SetFoodTooltip();
						}
					}
				}
			}
			equipmentElement = item.ItemRosterElement.EquipmentElement;
			if (InventoryManager.GetInventoryItemTypeOfItem(equipmentElement.Item) == InventoryItemType.Goods)
			{
				this.SetMerchandiseComponentTooltip();
			}
			if (this.IsComparing && !Input.IsGamepadActive)
			{
				for (EquipmentIndex equipmentIndex = this._comparedItem.ItemType + 1; equipmentIndex != this._comparedItem.ItemType; equipmentIndex = (equipmentIndex + 1) % EquipmentIndex.NumEquipmentSetSlots)
				{
					SPItemVM spitemVM = this._getEquipmentAtIndex(equipmentIndex);
					if (spitemVM != null)
					{
						equipmentElement = spitemVM.ItemRosterElement.EquipmentElement;
						ItemObject item2 = equipmentElement.Item;
						equipmentElement = comparedItem.ItemRosterElement.EquipmentElement;
						if (ItemHelper.CheckComparability(item2, equipmentElement.Item))
						{
							TextObject textObject = new TextObject("{=8fqFGxD9}Press {KEY} to compare with: {ITEM}", null);
							textObject.SetTextVariable("KEY", GameTexts.FindText("str_game_key_text", "anyalt"));
							textObject.SetTextVariable("ITEM", spitemVM.ItemDescription);
							this.CreateProperty(this.TargetItemProperties, "", textObject.ToString(), 0, null);
							this.CreateProperty(this.ComparedItemProperties, "", "", 0, null);
							break;
						}
					}
				}
			}
			this.IsInitializationOver = true;
			Game game = Game.Current;
			if (game == null)
			{
				return;
			}
			game.EventManager.TriggerEvent<InventoryItemInspectedEvent>(new InventoryItemInspectedEvent(item.ItemRosterElement, item.InventorySide));
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x0002D270 File Offset: 0x0002B470
		private int CompareValues(float currentValue, float comparedValue)
		{
			int num = (int)(currentValue * 10f);
			int num2 = (int)(comparedValue * 10f);
			if ((num != 0 && (float)MathF.Abs(num) <= MathF.Abs(currentValue)) || (num2 != 0 && (float)MathF.Abs(num2) <= MathF.Abs(comparedValue)))
			{
				return 0;
			}
			return this.CompareValues(num, num2);
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x0002D2C5 File Offset: 0x0002B4C5
		private int CompareValues(int currentValue, int comparedValue)
		{
			if (this._comparedItem == null || currentValue == comparedValue)
			{
				return 0;
			}
			if (currentValue <= comparedValue)
			{
				return -1;
			}
			return 1;
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x0002D2DC File Offset: 0x0002B4DC
		private void AlternativeUsageIndexUpdated()
		{
			if (this.AlternativeUsageIndex < 0 || !this.IsInitializationOver)
			{
				return;
			}
			EquipmentElement equipmentElement = this._targetItem.ItemRosterElement.EquipmentElement;
			if (equipmentElement.Item.WeaponComponent != null)
			{
				equipmentElement = this._targetItem.ItemRosterElement.EquipmentElement;
				WeaponComponentData weaponComponentData = equipmentElement.Item.Weapons[this.AlternativeUsageIndex];
				EquipmentElement comparedWeapon;
				int comparedWeaponUsageIndex;
				this.GetComparedWeapon(weaponComponentData.WeaponDescriptionId, out comparedWeapon, out comparedWeaponUsageIndex);
				if (!comparedWeapon.IsEmpty)
				{
					this.TargetItemProperties.Clear();
					this.ComparedItemProperties.Clear();
					this.SetGeneralComponentTooltip(false);
					equipmentElement = this._targetItem.ItemRosterElement.EquipmentElement;
					this.SetWeaponComponentTooltip(equipmentElement, this.AlternativeUsageIndex, comparedWeapon, comparedWeaponUsageIndex, false);
					this.TargetItemFlagList.Clear();
					this.ComparedItemFlagList.Clear();
					this.AddWeaponItemFlags(this.TargetItemFlagList, weaponComponentData);
					this.AddWeaponItemFlags(this.ComparedItemFlagList, weaponComponentData);
					return;
				}
				this._resetComparedItems(this._targetItem, this.AlternativeUsageIndex);
			}
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0002D3E8 File Offset: 0x0002B5E8
		private void GetComparedWeapon(string weaponUsageId, out EquipmentElement comparedWeapon, out int comparedUsageIndex)
		{
			comparedWeapon = EquipmentElement.Invalid;
			comparedUsageIndex = -1;
			int num;
			if (this.IsComparing && this._comparedItem != null && ItemHelper.IsWeaponComparableWithUsage(this._comparedItem.ItemRosterElement.EquipmentElement.Item, weaponUsageId, out num))
			{
				comparedWeapon = this._comparedItem.ItemRosterElement.EquipmentElement;
				comparedUsageIndex = num;
			}
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0002D450 File Offset: 0x0002B650
		private void SetGeneralComponentTooltip(bool isInit = true)
		{
			if (this._targetItem.ItemCost >= 0)
			{
				if (this._targetItem.ItemRosterElement.EquipmentElement.Item.Type == ItemObject.ItemTypeEnum.Goods || this._targetItem.ItemRosterElement.EquipmentElement.Item.Type == ItemObject.ItemTypeEnum.Animal || this._targetItem.ItemRosterElement.EquipmentElement.Item.Type == ItemObject.ItemTypeEnum.Horse)
				{
					GameTexts.SetVariable("PERCENTAGE", (int)MathF.Abs((float)(this._targetItem.ItemCost - this._targetItem.ItemRosterElement.EquipmentElement.Item.Value) / (float)this._targetItem.ItemRosterElement.EquipmentElement.Item.Value * 100f));
					ItemCategory itemCategory = this._targetItem.ItemRosterElement.EquipmentElement.Item.ItemCategory;
					float num = 0f;
					float num2 = 0f;
					if (Town.AllTowns != null)
					{
						foreach (Town town in Town.AllTowns)
						{
							num += town.GetItemCategoryPriceIndex(itemCategory);
							num2 += 1f;
						}
					}
					num /= num2;
					if ((float)this._targetItem.ItemCost / (float)this._targetItem.ItemRosterElement.EquipmentElement.Item.Value > num * 1.3f)
					{
						this._costProperty = this.CreateColoredProperty(this.TargetItemProperties, "", this._targetItem.ItemCost + this.GoldIcon, UIColors.NegativeIndicator, 1, new HintViewModel(GameTexts.FindText("str_inventory_cost_higher", null), null), TooltipProperty.TooltipPropertyFlags.Cost);
					}
					else if ((float)this._targetItem.ItemCost / (float)this._targetItem.ItemRosterElement.EquipmentElement.Item.Value < num * 0.8f)
					{
						this._costProperty = this.CreateColoredProperty(this.TargetItemProperties, "", this._targetItem.ItemCost + this.GoldIcon, UIColors.PositiveIndicator, 1, new HintViewModel(GameTexts.FindText("str_inventory_cost_lower", null), null), TooltipProperty.TooltipPropertyFlags.Cost);
					}
					else
					{
						this._costProperty = this.CreateColoredProperty(this.TargetItemProperties, "", this._targetItem.ItemCost + this.GoldIcon, UIColors.Gold, 1, new HintViewModel(GameTexts.FindText("str_inventory_cost_normal", null), null), TooltipProperty.TooltipPropertyFlags.Cost);
					}
				}
				else
				{
					this._costProperty = this.CreateColoredProperty(this.TargetItemProperties, "", this._targetItem.ItemCost + this.GoldIcon, UIColors.Gold, 1, null, TooltipProperty.TooltipPropertyFlags.Cost);
				}
			}
			if (this.IsComparing)
			{
				this.CreateColoredProperty(this.ComparedItemProperties, "", this._comparedItem.ItemCost + this.GoldIcon, UIColors.Gold, 2, null, TooltipProperty.TooltipPropertyFlags.Cost);
			}
			if (Game.Current.IsDevelopmentMode)
			{
				if (this._targetItem.ItemRosterElement.EquipmentElement.Item.Culture != null)
				{
					this.CreateColoredProperty(this.TargetItemProperties, "Culture: ", this._targetItem.ItemRosterElement.EquipmentElement.Item.Culture.StringId, UIColors.Gold, 0, null, TooltipProperty.TooltipPropertyFlags.None);
				}
				else
				{
					this.CreateColoredProperty(this.TargetItemProperties, "Culture: ", "No Culture", UIColors.Gold, 0, null, TooltipProperty.TooltipPropertyFlags.None);
				}
				this.CreateColoredProperty(this.TargetItemProperties, "ID: ", this._targetItem.ItemRosterElement.EquipmentElement.Item.StringId, UIColors.Gold, 0, null, TooltipProperty.TooltipPropertyFlags.None);
			}
			float equipmentWeightMultiplier = 1f;
			CharacterObject characterObject;
			bool flag = (characterObject = (this._character as CharacterObject)) != null && characterObject.GetPerkValue(DefaultPerks.Athletics.FormFittingArmor);
			SPItemVM spitemVM = this._getEquipmentAtIndex(this._targetItem.ItemType);
			bool flag2 = spitemVM != null && spitemVM.ItemType != EquipmentIndex.None && spitemVM.ItemType != EquipmentIndex.HorseHarness && this._targetItem.ItemRosterElement.EquipmentElement.Item.HasArmorComponent;
			if (flag && flag2)
			{
				equipmentWeightMultiplier += DefaultPerks.Athletics.FormFittingArmor.PrimaryBonus;
			}
			this.AddFloatProperty(this._weightText, (EquipmentElement x) => x.GetEquipmentElementWeight() * equipmentWeightMultiplier, true);
			ItemObject item = this._targetItem.ItemRosterElement.EquipmentElement.Item;
			if (item.RelevantSkill != null && (item.Difficulty > 0 || (this.IsComparing && this._comparedItem.ItemRosterElement.EquipmentElement.Item.Difficulty > 0)))
			{
				this.AddSkillRequirement(this._targetItem, this.TargetItemProperties, false);
				if (this.IsComparing)
				{
					this.AddSkillRequirement(this._comparedItem, this.ComparedItemProperties, true);
				}
			}
			if (isInit)
			{
				this.AddGeneralItemFlags(this.TargetItemFlagList, item);
				if (this.IsComparing)
				{
					this.AddGeneralItemFlags(this.ComparedItemFlagList, this._comparedItem.ItemRosterElement.EquipmentElement.Item);
				}
			}
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0002D9E8 File Offset: 0x0002BBE8
		private void AddSkillRequirement(ItemVM itemVm, MBBindingList<ItemMenuTooltipPropertyVM> itemProperties, bool isComparison)
		{
			ItemObject item = itemVm.ItemRosterElement.EquipmentElement.Item;
			string text = "";
			if (item.Difficulty > 0)
			{
				text = item.RelevantSkill.Name.ToString();
				text += " ";
				text += item.Difficulty.ToString();
			}
			string definition = "";
			if (!isComparison)
			{
				definition = this._requiresText.ToString();
			}
			this.CreateColoredProperty(itemProperties, definition, text, this.GetColorFromBool(this._character == null || CharacterHelper.CanUseItemBasedOnSkill(this._character, itemVm.ItemRosterElement.EquipmentElement)), 0, null, TooltipProperty.TooltipPropertyFlags.None);
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x0002DA94 File Offset: 0x0002BC94
		private void AddGeneralItemFlags(MBBindingList<ItemFlagVM> list, ItemObject item)
		{
			if (item.IsUniqueItem)
			{
				list.Add(new ItemFlagVM("GeneralFlagIcons\\unique", GameTexts.FindText("str_inventory_flag_unique", null)));
			}
			if (item.IsCivilian)
			{
				list.Add(new ItemFlagVM("GeneralFlagIcons\\civillian", GameTexts.FindText("str_inventory_flag_civillian", null)));
			}
			if (item.ItemFlags.HasAnyFlag(ItemFlags.NotUsableByFemale))
			{
				list.Add(new ItemFlagVM("GeneralFlagIcons\\male_only", GameTexts.FindText("str_inventory_flag_male_only", null)));
			}
			if (item.ItemFlags.HasAnyFlag(ItemFlags.NotUsableByMale))
			{
				list.Add(new ItemFlagVM("GeneralFlagIcons\\female_only", GameTexts.FindText("str_inventory_flag_female_only", null)));
			}
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0002DB41 File Offset: 0x0002BD41
		private void AddFoodItemFlags(MBBindingList<ItemFlagVM> list, ItemObject item)
		{
			list.Add(new ItemFlagVM("GoodsFlagIcons\\consumable", GameTexts.FindText("str_inventory_flag_consumable", null)));
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x0002DB60 File Offset: 0x0002BD60
		private void AddWeaponItemFlags(MBBindingList<ItemFlagVM> list, WeaponComponentData weapon)
		{
			if (weapon == null)
			{
				Debug.FailedAssert("Trying to add flags for a null weapon", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem.ViewModelCollection\\Inventory\\ItemMenuVM.cs", "AddWeaponItemFlags", 412);
				return;
			}
			ItemObject.ItemUsageSetFlags itemUsageFlags = this._getItemUsageSetFlags(weapon);
			foreach (ValueTuple<string, TextObject> valueTuple in CampaignUIHelper.GetFlagDetailsForWeapon(weapon, itemUsageFlags, this._character as CharacterObject))
			{
				list.Add(new ItemFlagVM(valueTuple.Item1, valueTuple.Item2));
			}
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x0002DBFC File Offset: 0x0002BDFC
		private Color GetColorFromBool(bool booleanValue)
		{
			if (!booleanValue)
			{
				return UIColors.NegativeIndicator;
			}
			return UIColors.PositiveIndicator;
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x0002DC0C File Offset: 0x0002BE0C
		private void SetFoodTooltip()
		{
			this.CreateColoredProperty(this.TargetItemProperties, "", this._foodText.ToString(), this.ConsumableColor, 1, null, TooltipProperty.TooltipPropertyFlags.None);
			this.AddFoodItemFlags(this.TargetItemFlagList, this._targetItem.ItemRosterElement.EquipmentElement.Item);
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0002DC64 File Offset: 0x0002BE64
		private void SetHorseComponentTooltip()
		{
			HorseComponent horseComponent = this._targetItem.ItemRosterElement.EquipmentElement.Item.HorseComponent;
			HorseComponent horse = this.IsComparing ? this._comparedItem.ItemRosterElement.EquipmentElement.Item.HorseComponent : null;
			this.CreateProperty(this.TargetItemProperties, this._typeText.ToString(), GameTexts.FindText("str_inventory_type_" + (int)this._targetItem.ItemRosterElement.EquipmentElement.Item.Type, null).ToString(), 0, null);
			this.AddHorseItemFlags(this.TargetItemFlagList, this._targetItem.ItemRosterElement.EquipmentElement.Item, horseComponent);
			if (this.IsComparing)
			{
				this.CreateProperty(this.ComparedItemProperties, " ", GameTexts.FindText("str_inventory_type_" + (int)this._comparedItem.ItemRosterElement.EquipmentElement.Item.Type, null).ToString(), 0, null);
				this.AddHorseItemFlags(this.ComparedItemFlagList, this._comparedItem.ItemRosterElement.EquipmentElement.Item, horse);
			}
			if (this._targetItem.ItemRosterElement.EquipmentElement.Item.IsMountable)
			{
				this.AddIntProperty(this._horseTierText, (int)(this._targetItem.ItemRosterElement.EquipmentElement.Item.Tier + 1), (this.IsComparing && this._comparedItem != null) ? new int?((int)(this._comparedItem.ItemRosterElement.EquipmentElement.Item.Tier + 1)) : null);
				this.AddIntProperty(this._chargeDamageText, this._targetItem.ItemRosterElement.EquipmentElement.GetModifiedMountCharge(EquipmentElement.Invalid), (this.IsComparing && this._comparedItem != null) ? new int?(this._comparedItem.ItemRosterElement.EquipmentElement.GetModifiedMountCharge(EquipmentElement.Invalid)) : null);
				this.AddIntProperty(this._speedText, this._targetItem.ItemRosterElement.EquipmentElement.GetModifiedMountSpeed(EquipmentElement.Invalid), (this.IsComparing && this._comparedItem != null) ? new int?(this._comparedItem.ItemRosterElement.EquipmentElement.GetModifiedMountSpeed(EquipmentElement.Invalid)) : null);
				this.AddIntProperty(this._maneuverText, this._targetItem.ItemRosterElement.EquipmentElement.GetModifiedMountManeuver(EquipmentElement.Invalid), (this.IsComparing && this._comparedItem != null) ? new int?(this._comparedItem.ItemRosterElement.EquipmentElement.GetModifiedMountManeuver(EquipmentElement.Invalid)) : null);
				this.AddIntProperty(this._hitPointsText, this._targetItem.ItemRosterElement.EquipmentElement.GetModifiedMountHitPoints(), (this.IsComparing && this._comparedItem != null) ? new int?(this._comparedItem.ItemRosterElement.EquipmentElement.GetModifiedMountHitPoints()) : null);
				if (this._targetItem.ItemRosterElement.EquipmentElement.Item.HasHorseComponent && this._targetItem.ItemRosterElement.EquipmentElement.Item.HorseComponent.IsMount)
				{
					this.AddComparableStringProperty(this._horseTypeText, (EquipmentElement x) => x.Item.ItemCategory.GetName().ToString(), (EquipmentElement x) => this.GetHorseCategoryValue(x.Item.ItemCategory));
				}
			}
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x0002E03C File Offset: 0x0002C23C
		private void AddHorseItemFlags(MBBindingList<ItemFlagVM> list, ItemObject item, HorseComponent horse)
		{
			if (!horse.IsLiveStock)
			{
				if (item.ItemCategory == DefaultItemCategories.PackAnimal)
				{
					list.Add(new ItemFlagVM("MountFlagIcons\\weight_carrying_mount", GameTexts.FindText("str_inventory_flag_carrying_mount", null)));
				}
				else
				{
					list.Add(new ItemFlagVM("MountFlagIcons\\speed_mount", GameTexts.FindText("str_inventory_flag_speed_mount", null)));
				}
			}
			if (this._inventoryLogic.IsSlaughterable(item))
			{
				list.Add(new ItemFlagVM("MountFlagIcons\\slaughterable", GameTexts.FindText("str_inventory_flag_slaughterable", null)));
			}
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x0002E0C0 File Offset: 0x0002C2C0
		private void SetMerchandiseComponentTooltip()
		{
			if (Campaign.Current.GameMode == CampaignGameMode.Campaign)
			{
				if (this._tradeRumorsBehavior == null)
				{
					return;
				}
				IEnumerable<TradeRumor> tradeRumors = this._tradeRumorsBehavior.TradeRumors;
				bool flag = true;
				IMarketData marketData = this._inventoryLogic.MarketData;
				foreach (TradeRumor tradeRumor in from x in tradeRumors
				orderby x.SellPrice descending, x.BuyPrice descending
				select x)
				{
					bool flag2 = false;
					bool flag3 = false;
					if (this._targetItem.ItemRosterElement.EquipmentElement.Item == tradeRumor.ItemCategory)
					{
						if ((float)tradeRumor.BuyPrice < 0.9f * (float)marketData.GetPrice(tradeRumor.ItemCategory, MobileParty.MainParty, true, this._inventoryLogic.OtherParty))
						{
							flag3 = true;
						}
						if ((float)tradeRumor.SellPrice > 1.1f * (float)marketData.GetPrice(tradeRumor.ItemCategory, MobileParty.MainParty, false, this._inventoryLogic.OtherParty))
						{
							flag2 = true;
						}
						if ((Settlement.CurrentSettlement == null || Settlement.CurrentSettlement != tradeRumor.Settlement) && this._targetItem.ItemRosterElement.EquipmentElement.Item == tradeRumor.ItemCategory && (flag3 || flag2))
						{
							if (flag)
							{
								this.CreateColoredProperty(this.TargetItemProperties, "", this._tradeRumorsText.ToString(), this.TitleColor, 1, null, TooltipProperty.TooltipPropertyFlags.None);
								if (this.IsComparing)
								{
									this.CreateProperty(this.ComparedItemProperties, "", "", 0, null);
									this.CreateProperty(this.ComparedItemProperties, "", "", 0, null);
								}
								flag = false;
							}
							MBTextManager.SetTextVariable("SETTLEMENT_NAME", tradeRumor.Settlement.Name, false);
							MBTextManager.SetTextVariable("SELL_PRICE", tradeRumor.SellPrice);
							MBTextManager.SetTextVariable("BUY_PRICE", tradeRumor.BuyPrice);
							float alpha = this.CalculateTradeRumorOldnessFactor(tradeRumor);
							Color color = new Color(this.TitleColor.Red, this.TitleColor.Green, this.TitleColor.Blue, alpha);
							TextObject textObject = flag3 ? GameTexts.FindText("str_trade_rumors_text_buy", null) : GameTexts.FindText("str_trade_rumors_text_sell", null);
							this.CreateColoredProperty(this.TargetItemProperties, "", textObject.ToString(), color, 0, null, TooltipProperty.TooltipPropertyFlags.None);
							if (this.IsComparing)
							{
								this.CreateProperty(this.ComparedItemProperties, "", "", 0, null);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0002E388 File Offset: 0x0002C588
		private float CalculateTradeRumorOldnessFactor(TradeRumor rumor)
		{
			return MathF.Clamp((float)((int)rumor.RumorEndTime.RemainingDaysFromNow) / 5f, 0.5f, 1f);
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0002E3BC File Offset: 0x0002C5BC
		private void SetWeaponComponentTooltip(in EquipmentElement targetWeapon, int targetWeaponUsageIndex, EquipmentElement comparedWeapon, int comparedWeaponUsageIndex, bool isInit)
		{
			EquipmentElement equipmentElement = targetWeapon;
			WeaponComponentData weaponWithUsageIndex = equipmentElement.Item.GetWeaponWithUsageIndex(targetWeaponUsageIndex);
			if (this.IsComparing && this._comparedItem != null && comparedWeapon.IsEmpty)
			{
				this.GetComparedWeapon(weaponWithUsageIndex.WeaponDescriptionId, out comparedWeapon, out comparedWeaponUsageIndex);
			}
			WeaponComponentData weaponComponentData = comparedWeapon.IsEmpty ? null : comparedWeapon.Item.GetWeaponWithUsageIndex(comparedWeaponUsageIndex);
			if (isInit)
			{
				this.AddWeaponItemFlags(this.TargetItemFlagList, weaponWithUsageIndex);
				if (this.IsComparing)
				{
					this.AddWeaponItemFlags(this.ComparedItemFlagList, weaponComponentData);
				}
				if (targetWeaponUsageIndex == 0)
				{
					this.AlternativeUsageIndex = -1;
				}
				equipmentElement = targetWeapon;
				foreach (WeaponComponentData weaponComponentData2 in equipmentElement.Item.Weapons)
				{
					if (CampaignUIHelper.IsItemUsageApplicable(weaponComponentData2))
					{
						this.AlternativeUsages.Add(new StringItemWithHintVM(GameTexts.FindText("str_weapon_usage", weaponComponentData2.WeaponDescriptionId).ToString(), GameTexts.FindText("str_inventory_alternative_usage_hint", null)));
					}
				}
				this.AlternativeUsageIndex = targetWeaponUsageIndex;
			}
			this.CreateProperty(this.TargetItemProperties, this._classText.ToString(), GameTexts.FindText("str_inventory_weapon", ((int)weaponWithUsageIndex.WeaponClass).ToString()).ToString(), 0, null);
			if (!comparedWeapon.IsEmpty)
			{
				this.CreateProperty(this.ComparedItemProperties, " ", GameTexts.FindText("str_inventory_weapon", ((int)weaponWithUsageIndex.WeaponClass).ToString()).ToString(), 0, null);
			}
			else if (this.IsComparing)
			{
				this.CreateProperty(this.ComparedItemProperties, "", "", 0, null);
			}
			equipmentElement = targetWeapon;
			if (equipmentElement.Item.BannerComponent == null)
			{
				int value = 0;
				if (!comparedWeapon.IsEmpty)
				{
					value = (int)(comparedWeapon.Item.Tier + 1);
				}
				TextObject weaponTierText = this._weaponTierText;
				equipmentElement = targetWeapon;
				this.AddIntProperty(weaponTierText, (int)(equipmentElement.Item.Tier + 1), new int?(value));
			}
			ItemObject.ItemTypeEnum itemTypeFromWeaponClass = WeaponComponentData.GetItemTypeFromWeaponClass(weaponWithUsageIndex.WeaponClass);
			ItemObject.ItemTypeEnum itemTypeEnum = (!comparedWeapon.IsEmpty) ? WeaponComponentData.GetItemTypeFromWeaponClass(weaponWithUsageIndex.WeaponClass) : ItemObject.ItemTypeEnum.Invalid;
			if (itemTypeFromWeaponClass == ItemObject.ItemTypeEnum.OneHandedWeapon || itemTypeFromWeaponClass == ItemObject.ItemTypeEnum.TwoHandedWeapon || itemTypeFromWeaponClass == ItemObject.ItemTypeEnum.Polearm || itemTypeEnum == ItemObject.ItemTypeEnum.OneHandedWeapon || itemTypeEnum == ItemObject.ItemTypeEnum.TwoHandedWeapon || itemTypeEnum == ItemObject.ItemTypeEnum.Polearm)
			{
				if (weaponWithUsageIndex.SwingDamageType != DamageTypes.Invalid)
				{
					TextObject swingSpeedText = this._swingSpeedText;
					equipmentElement = targetWeapon;
					this.AddIntProperty(swingSpeedText, equipmentElement.GetModifiedSwingSpeedForUsage(targetWeaponUsageIndex), comparedWeapon.IsEmpty ? null : new int?(comparedWeapon.GetModifiedSwingSpeedForUsage(comparedWeaponUsageIndex)));
					this.AddSwingDamageProperty(this._swingDamageText, targetWeapon, targetWeaponUsageIndex, comparedWeapon, comparedWeaponUsageIndex);
				}
				if (weaponWithUsageIndex.ThrustDamageType != DamageTypes.Invalid)
				{
					TextObject thrustSpeedText = this._thrustSpeedText;
					equipmentElement = targetWeapon;
					this.AddIntProperty(thrustSpeedText, equipmentElement.GetModifiedThrustSpeedForUsage(targetWeaponUsageIndex), comparedWeapon.IsEmpty ? null : new int?(comparedWeapon.GetModifiedThrustSpeedForUsage(comparedWeaponUsageIndex)));
					this.AddThrustDamageProperty(this._thrustDamageText, targetWeapon, targetWeaponUsageIndex, comparedWeapon, comparedWeaponUsageIndex);
				}
				this.AddIntProperty(this._lengthText, weaponWithUsageIndex.WeaponLength, (weaponComponentData != null) ? new int?(weaponComponentData.WeaponLength) : null);
				TextObject handlingText = this._handlingText;
				equipmentElement = targetWeapon;
				this.AddIntProperty(handlingText, equipmentElement.GetModifiedHandlingForUsage(targetWeaponUsageIndex), comparedWeapon.IsEmpty ? null : new int?(comparedWeapon.GetModifiedHandlingForUsage(comparedWeaponUsageIndex)));
			}
			if (itemTypeFromWeaponClass == ItemObject.ItemTypeEnum.Thrown || itemTypeEnum == ItemObject.ItemTypeEnum.Thrown)
			{
				this.AddIntProperty(this._weaponLengthText, weaponWithUsageIndex.WeaponLength, (weaponComponentData != null) ? new int?(weaponComponentData.WeaponLength) : null);
				this.AddMissileDamageProperty(this._damageText, targetWeapon, targetWeaponUsageIndex, comparedWeapon, comparedWeaponUsageIndex);
				TextObject missileSpeedText = this._missileSpeedText;
				equipmentElement = targetWeapon;
				this.AddIntProperty(missileSpeedText, equipmentElement.GetModifiedMissileSpeedForUsage(targetWeaponUsageIndex), comparedWeapon.IsEmpty ? null : new int?(comparedWeapon.GetModifiedMissileSpeedForUsage(comparedWeaponUsageIndex)));
				this.AddIntProperty(this._accuracyText, weaponWithUsageIndex.Accuracy, (weaponComponentData != null) ? new int?(weaponComponentData.Accuracy) : null);
				TextObject stackAmountText = this._stackAmountText;
				equipmentElement = targetWeapon;
				this.AddIntProperty(stackAmountText, (int)equipmentElement.GetModifiedStackCountForUsage(targetWeaponUsageIndex), comparedWeapon.IsEmpty ? null : new int?((int)comparedWeapon.GetModifiedStackCountForUsage(comparedWeaponUsageIndex)));
			}
			if (itemTypeFromWeaponClass == ItemObject.ItemTypeEnum.Shield || itemTypeEnum == ItemObject.ItemTypeEnum.Shield)
			{
				TextObject speedText = this._speedText;
				equipmentElement = targetWeapon;
				this.AddIntProperty(speedText, equipmentElement.GetModifiedSwingSpeedForUsage(targetWeaponUsageIndex), comparedWeapon.IsEmpty ? null : new int?(comparedWeapon.GetModifiedSwingSpeedForUsage(comparedWeaponUsageIndex)));
				TextObject hitPointsText = this._hitPointsText;
				equipmentElement = targetWeapon;
				this.AddIntProperty(hitPointsText, (int)equipmentElement.GetModifiedMaximumHitPointsForUsage(targetWeaponUsageIndex), comparedWeapon.IsEmpty ? null : new int?((int)comparedWeapon.GetModifiedMaximumHitPointsForUsage(comparedWeaponUsageIndex)));
			}
			if (itemTypeFromWeaponClass == ItemObject.ItemTypeEnum.Bow || itemTypeFromWeaponClass == ItemObject.ItemTypeEnum.Crossbow || itemTypeEnum == ItemObject.ItemTypeEnum.Bow || itemTypeEnum == ItemObject.ItemTypeEnum.Crossbow)
			{
				TextObject speedText2 = this._speedText;
				equipmentElement = targetWeapon;
				this.AddIntProperty(speedText2, equipmentElement.GetModifiedSwingSpeedForUsage(targetWeaponUsageIndex), comparedWeapon.IsEmpty ? null : new int?(comparedWeapon.GetModifiedSwingSpeedForUsage(comparedWeaponUsageIndex)));
				this.AddThrustDamageProperty(this._damageText, targetWeapon, targetWeaponUsageIndex, comparedWeapon, comparedWeaponUsageIndex);
				this.AddIntProperty(this._accuracyText, weaponWithUsageIndex.Accuracy, (weaponComponentData != null) ? new int?(weaponComponentData.Accuracy) : null);
				TextObject missileSpeedText2 = this._missileSpeedText;
				equipmentElement = targetWeapon;
				this.AddIntProperty(missileSpeedText2, equipmentElement.GetModifiedMissileSpeedForUsage(targetWeaponUsageIndex), comparedWeapon.IsEmpty ? null : new int?(comparedWeapon.GetModifiedMissileSpeedForUsage(comparedWeaponUsageIndex)));
				if (itemTypeFromWeaponClass == ItemObject.ItemTypeEnum.Crossbow || itemTypeEnum == ItemObject.ItemTypeEnum.Crossbow)
				{
					TextObject ammoLimitText = this._ammoLimitText;
					int maxDataValue = (int)weaponWithUsageIndex.MaxDataValue;
					short? num = (weaponComponentData != null) ? new short?(weaponComponentData.MaxDataValue) : null;
					this.AddIntProperty(ammoLimitText, maxDataValue, (num != null) ? new int?((int)num.GetValueOrDefault()) : null);
				}
			}
			if (weaponWithUsageIndex.IsAmmo || (weaponComponentData != null && weaponComponentData.IsAmmo))
			{
				if ((itemTypeFromWeaponClass != ItemObject.ItemTypeEnum.Arrows && itemTypeFromWeaponClass != ItemObject.ItemTypeEnum.Bolts) || (weaponComponentData != null && itemTypeEnum != ItemObject.ItemTypeEnum.Arrows && itemTypeEnum != ItemObject.ItemTypeEnum.Bolts))
				{
					this.AddIntProperty(this._accuracyText, weaponWithUsageIndex.Accuracy, (weaponComponentData != null) ? new int?(weaponComponentData.Accuracy) : null);
				}
				this.AddThrustDamageProperty(this._damageText, targetWeapon, targetWeaponUsageIndex, comparedWeapon, comparedWeaponUsageIndex);
				TextObject stackAmountText2 = this._stackAmountText;
				equipmentElement = targetWeapon;
				this.AddIntProperty(stackAmountText2, (int)equipmentElement.GetModifiedStackCountForUsage(targetWeaponUsageIndex), comparedWeapon.IsEmpty ? null : new int?((int)comparedWeapon.GetModifiedStackCountForUsage(comparedWeaponUsageIndex)));
			}
			equipmentElement = targetWeapon;
			ItemObject item = equipmentElement.Item;
			if (item == null || !item.HasBannerComponent)
			{
				ItemObject item2 = comparedWeapon.Item;
				if (item2 == null || !item2.HasBannerComponent)
				{
					goto IL_717;
				}
			}
			Func<EquipmentElement, string> valueAsStringFunc = delegate(EquipmentElement x)
			{
				ItemObject item3 = x.Item;
				bool flag;
				if (item3 == null)
				{
					flag = (null != null);
				}
				else
				{
					BannerComponent bannerComponent = item3.BannerComponent;
					flag = (((bannerComponent != null) ? bannerComponent.BannerEffect : null) != null);
				}
				if (flag)
				{
					GameTexts.SetVariable("RANK", x.Item.BannerComponent.BannerEffect.Name);
					string content = string.Empty;
					if (x.Item.BannerComponent.BannerEffect.IncrementType == BannerEffect.EffectIncrementType.AddFactor)
					{
						GameTexts.FindText("str_NUMBER_percent", null).SetTextVariable("NUMBER", ((int)Math.Abs(x.Item.BannerComponent.GetBannerEffectBonus() * 100f)).ToString());
						object obj;
						content = obj.ToString();
					}
					else if (x.Item.BannerComponent.BannerEffect.IncrementType == BannerEffect.EffectIncrementType.Add)
					{
						content = x.Item.BannerComponent.GetBannerEffectBonus().ToString();
					}
					GameTexts.SetVariable("NUMBER", content);
					return GameTexts.FindText("str_RANK_with_NUM_between_parenthesis", null).ToString();
				}
				return this._noneText.ToString();
			};
			this.AddComparableStringProperty(this._bannerEffectText, valueAsStringFunc, (EquipmentElement x) => 0);
			IL_717:
			this.AddDonationXpTooltip();
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000B86 RID: 2950 RVA: 0x0002EAF8 File Offset: 0x0002CCF8
		// (set) Token: 0x06000B87 RID: 2951 RVA: 0x0002EB00 File Offset: 0x0002CD00
		[DataSourceProperty]
		public bool IsComparing
		{
			get
			{
				return this._isComparing;
			}
			set
			{
				if (value != this._isComparing)
				{
					this._isComparing = value;
					base.OnPropertyChangedWithValue(value, "IsComparing");
				}
			}
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x0002EB20 File Offset: 0x0002CD20
		private void AddIntProperty(TextObject description, int targetValue, int? comparedValue)
		{
			string value = targetValue.ToString();
			if (this.IsComparing && comparedValue != null)
			{
				string value2 = comparedValue.Value.ToString();
				int result = this.CompareValues(targetValue, comparedValue.Value);
				this.CreateColoredProperty(this.TargetItemProperties, description.ToString(), value, this.GetColorFromComparison(result, false), 0, null, TooltipProperty.TooltipPropertyFlags.None);
				this.CreateColoredProperty(this.ComparedItemProperties, " ", value2, this.GetColorFromComparison(result, true), 0, null, TooltipProperty.TooltipPropertyFlags.None);
				return;
			}
			this.CreateColoredProperty(this.TargetItemProperties, description.ToString(), value, this.GetColorFromComparison(0, false), 0, null, TooltipProperty.TooltipPropertyFlags.None);
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000B89 RID: 2953 RVA: 0x0002EBC2 File Offset: 0x0002CDC2
		// (set) Token: 0x06000B8A RID: 2954 RVA: 0x0002EBCA File Offset: 0x0002CDCA
		[DataSourceProperty]
		public bool IsPlayerItem
		{
			get
			{
				return this._isPlayerItem;
			}
			set
			{
				if (value != this._isPlayerItem)
				{
					this._isPlayerItem = value;
					base.OnPropertyChangedWithValue(value, "IsPlayerItem");
				}
			}
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x0002EBE8 File Offset: 0x0002CDE8
		private void AddFloatProperty(TextObject description, Func<EquipmentElement, float> func, bool reversedCompare = false)
		{
			float targetValue = func(this._targetItem.ItemRosterElement.EquipmentElement);
			float? comparedValue = null;
			if (this.IsComparing && this._comparedItem != null)
			{
				comparedValue = new float?(func(this._comparedItem.ItemRosterElement.EquipmentElement));
			}
			this.AddFloatProperty(description, targetValue, comparedValue, reversedCompare);
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000B8C RID: 2956 RVA: 0x0002EC4B File Offset: 0x0002CE4B
		// (set) Token: 0x06000B8D RID: 2957 RVA: 0x0002EC53 File Offset: 0x0002CE53
		[DataSourceProperty]
		public ImageIdentifierVM ImageIdentifier
		{
			get
			{
				return this._imageIdentifier;
			}
			set
			{
				if (value != this._imageIdentifier)
				{
					this._imageIdentifier = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "ImageIdentifier");
				}
			}
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x0002EC74 File Offset: 0x0002CE74
		private void AddFloatProperty(TextObject description, float targetValue, float? comparedValue, bool reversedCompare = false)
		{
			string formattedItemPropertyText = CampaignUIHelper.GetFormattedItemPropertyText(targetValue, false);
			if (this.IsComparing && comparedValue != null)
			{
				string formattedItemPropertyText2 = CampaignUIHelper.GetFormattedItemPropertyText(comparedValue.Value, false);
				int num = this.CompareValues(targetValue, comparedValue.Value);
				if (reversedCompare)
				{
					num *= -1;
				}
				this.CreateColoredProperty(this.TargetItemProperties, description.ToString(), formattedItemPropertyText, this.GetColorFromComparison(num, false), 0, null, TooltipProperty.TooltipPropertyFlags.None);
				this.CreateColoredProperty(this.ComparedItemProperties, " ", formattedItemPropertyText2, this.GetColorFromComparison(num, true), 0, null, TooltipProperty.TooltipPropertyFlags.None);
				return;
			}
			this.CreateColoredProperty(this.TargetItemProperties, description.ToString(), formattedItemPropertyText, this.GetColorFromComparison(0, false), 0, null, TooltipProperty.TooltipPropertyFlags.None);
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000B8F RID: 2959 RVA: 0x0002ED1C File Offset: 0x0002CF1C
		// (set) Token: 0x06000B90 RID: 2960 RVA: 0x0002ED24 File Offset: 0x0002CF24
		[DataSourceProperty]
		public ImageIdentifierVM ComparedImageIdentifier
		{
			get
			{
				return this._comparedImageIdentifier;
			}
			set
			{
				if (value != this._comparedImageIdentifier)
				{
					this._comparedImageIdentifier = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "ComparedImageIdentifier");
				}
			}
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x0002ED44 File Offset: 0x0002CF44
		private void AddComparableStringProperty(TextObject description, Func<EquipmentElement, string> valueAsStringFunc, Func<EquipmentElement, int> valueAsIntFunc)
		{
			string value = valueAsStringFunc(this._targetItem.ItemRosterElement.EquipmentElement);
			int currentValue = valueAsIntFunc(this._targetItem.ItemRosterElement.EquipmentElement);
			if (this.IsComparing && this._comparedItem != null)
			{
				int comparedValue = valueAsIntFunc(this._comparedItem.ItemRosterElement.EquipmentElement);
				int result = this.CompareValues(currentValue, comparedValue);
				this.CreateColoredProperty(this.TargetItemProperties, description.ToString(), value, this.GetColorFromComparison(result, false), 0, null, TooltipProperty.TooltipPropertyFlags.None);
				this.CreateColoredProperty(this.ComparedItemProperties, " ", valueAsStringFunc(this._comparedItem.ItemRosterElement.EquipmentElement), this.GetColorFromComparison(result, true), 0, null, TooltipProperty.TooltipPropertyFlags.None);
				return;
			}
			this.CreateColoredProperty(this.TargetItemProperties, description.ToString(), value, this.GetColorFromComparison(0, false), 0, null, TooltipProperty.TooltipPropertyFlags.None);
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000B92 RID: 2962 RVA: 0x0002EE21 File Offset: 0x0002D021
		// (set) Token: 0x06000B93 RID: 2963 RVA: 0x0002EE29 File Offset: 0x0002D029
		[DataSourceProperty]
		public int TransactionTotalCost
		{
			get
			{
				return this._transactionTotalCost;
			}
			set
			{
				if (value != this._transactionTotalCost)
				{
					this._transactionTotalCost = value;
					base.OnPropertyChangedWithValue(value, "TransactionTotalCost");
				}
			}
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x0002EE48 File Offset: 0x0002D048
		private void AddSwingDamageProperty(TextObject description, in EquipmentElement targetWeapon, int targetWeaponUsageIndex, in EquipmentElement comparedWeapon, int comparedWeaponUsageIndex)
		{
			EquipmentElement equipmentElement = targetWeapon;
			int modifiedSwingDamageForUsage = equipmentElement.GetModifiedSwingDamageForUsage(targetWeaponUsageIndex);
			equipmentElement = targetWeapon;
			WeaponComponentData weaponWithUsageIndex = equipmentElement.Item.GetWeaponWithUsageIndex(targetWeaponUsageIndex);
			equipmentElement = targetWeapon;
			string value = ItemHelper.GetSwingDamageText(weaponWithUsageIndex, equipmentElement.ItemModifier).ToString();
			if (this.IsComparing)
			{
				equipmentElement = comparedWeapon;
				if (!equipmentElement.IsEmpty)
				{
					equipmentElement = comparedWeapon;
					int modifiedSwingDamageForUsage2 = equipmentElement.GetModifiedSwingDamageForUsage(comparedWeaponUsageIndex);
					equipmentElement = comparedWeapon;
					WeaponComponentData weaponWithUsageIndex2 = equipmentElement.Item.GetWeaponWithUsageIndex(comparedWeaponUsageIndex);
					equipmentElement = comparedWeapon;
					string value2 = ItemHelper.GetSwingDamageText(weaponWithUsageIndex2, equipmentElement.ItemModifier).ToString();
					int result = this.CompareValues(modifiedSwingDamageForUsage, modifiedSwingDamageForUsage2);
					this.CreateColoredProperty(this.TargetItemProperties, description.ToString(), value, this.GetColorFromComparison(result, false), 0, null, TooltipProperty.TooltipPropertyFlags.None);
					this.CreateColoredProperty(this.ComparedItemProperties, " ", value2, this.GetColorFromComparison(result, true), 0, null, TooltipProperty.TooltipPropertyFlags.None);
					return;
				}
			}
			this.CreateColoredProperty(this.TargetItemProperties, description.ToString(), value, this.GetColorFromComparison(0, true), 0, null, TooltipProperty.TooltipPropertyFlags.None);
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000B95 RID: 2965 RVA: 0x0002EF5E File Offset: 0x0002D15E
		// (set) Token: 0x06000B96 RID: 2966 RVA: 0x0002EF66 File Offset: 0x0002D166
		[DataSourceProperty]
		public bool IsInitializationOver
		{
			get
			{
				return this._isInitializationOver;
			}
			set
			{
				if (value != this._isInitializationOver)
				{
					this._isInitializationOver = value;
					base.OnPropertyChangedWithValue(value, "IsInitializationOver");
				}
			}
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x0002EF84 File Offset: 0x0002D184
		private void AddMissileDamageProperty(TextObject description, in EquipmentElement targetWeapon, int targetWeaponUsageIndex, in EquipmentElement comparedWeapon, int comparedWeaponUsageIndex)
		{
			EquipmentElement equipmentElement = targetWeapon;
			int modifiedMissileDamageForUsage = equipmentElement.GetModifiedMissileDamageForUsage(targetWeaponUsageIndex);
			equipmentElement = targetWeapon;
			WeaponComponentData weaponWithUsageIndex = equipmentElement.Item.GetWeaponWithUsageIndex(targetWeaponUsageIndex);
			equipmentElement = targetWeapon;
			string value = ItemHelper.GetMissileDamageText(weaponWithUsageIndex, equipmentElement.ItemModifier).ToString();
			if (this.IsComparing)
			{
				equipmentElement = comparedWeapon;
				if (!equipmentElement.IsEmpty)
				{
					equipmentElement = comparedWeapon;
					int modifiedMissileDamageForUsage2 = equipmentElement.GetModifiedMissileDamageForUsage(comparedWeaponUsageIndex);
					equipmentElement = comparedWeapon;
					WeaponComponentData weaponWithUsageIndex2 = equipmentElement.Item.GetWeaponWithUsageIndex(comparedWeaponUsageIndex);
					equipmentElement = comparedWeapon;
					string value2 = ItemHelper.GetMissileDamageText(weaponWithUsageIndex2, equipmentElement.ItemModifier).ToString();
					int result = this.CompareValues(modifiedMissileDamageForUsage, modifiedMissileDamageForUsage2);
					this.CreateColoredProperty(this.TargetItemProperties, description.ToString(), value, this.GetColorFromComparison(result, false), 0, null, TooltipProperty.TooltipPropertyFlags.None);
					this.CreateColoredProperty(this.ComparedItemProperties, " ", value2, this.GetColorFromComparison(result, true), 0, null, TooltipProperty.TooltipPropertyFlags.None);
					return;
				}
			}
			this.CreateColoredProperty(this.TargetItemProperties, description.ToString(), value, this.GetColorFromComparison(0, true), 0, null, TooltipProperty.TooltipPropertyFlags.None);
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000B98 RID: 2968 RVA: 0x0002F09A File Offset: 0x0002D29A
		// (set) Token: 0x06000B99 RID: 2969 RVA: 0x0002F0A2 File Offset: 0x0002D2A2
		[DataSourceProperty]
		public string ItemName
		{
			get
			{
				return this._itemName;
			}
			set
			{
				if (value != this._itemName)
				{
					this._itemName = value;
					base.OnPropertyChangedWithValue<string>(value, "ItemName");
				}
			}
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x0002F0C8 File Offset: 0x0002D2C8
		private void AddThrustDamageProperty(TextObject description, in EquipmentElement targetWeapon, int targetWeaponUsageIndex, in EquipmentElement comparedWeapon, int comparedWeaponUsageIndex)
		{
			EquipmentElement equipmentElement = targetWeapon;
			int modifiedThrustDamageForUsage = equipmentElement.GetModifiedThrustDamageForUsage(targetWeaponUsageIndex);
			equipmentElement = targetWeapon;
			WeaponComponentData weaponWithUsageIndex = equipmentElement.Item.GetWeaponWithUsageIndex(targetWeaponUsageIndex);
			equipmentElement = targetWeapon;
			string value = ItemHelper.GetThrustDamageText(weaponWithUsageIndex, equipmentElement.ItemModifier).ToString();
			if (this.IsComparing)
			{
				equipmentElement = comparedWeapon;
				if (!equipmentElement.IsEmpty)
				{
					equipmentElement = comparedWeapon;
					int modifiedThrustDamageForUsage2 = equipmentElement.GetModifiedThrustDamageForUsage(comparedWeaponUsageIndex);
					equipmentElement = comparedWeapon;
					WeaponComponentData weaponWithUsageIndex2 = equipmentElement.Item.GetWeaponWithUsageIndex(comparedWeaponUsageIndex);
					equipmentElement = comparedWeapon;
					string value2 = ItemHelper.GetThrustDamageText(weaponWithUsageIndex2, equipmentElement.ItemModifier).ToString();
					int result = this.CompareValues(modifiedThrustDamageForUsage, modifiedThrustDamageForUsage2);
					this.CreateColoredProperty(this.TargetItemProperties, description.ToString(), value, this.GetColorFromComparison(result, false), 0, null, TooltipProperty.TooltipPropertyFlags.None);
					this.CreateColoredProperty(this.ComparedItemProperties, " ", value2, this.GetColorFromComparison(result, true), 0, null, TooltipProperty.TooltipPropertyFlags.None);
					return;
				}
			}
			this.CreateColoredProperty(this.TargetItemProperties, description.ToString(), value, this.GetColorFromComparison(0, true), 0, null, TooltipProperty.TooltipPropertyFlags.None);
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000B9B RID: 2971 RVA: 0x0002F1DE File Offset: 0x0002D3DE
		// (set) Token: 0x06000B9C RID: 2972 RVA: 0x0002F1E6 File Offset: 0x0002D3E6
		[DataSourceProperty]
		public string ComparedItemName
		{
			get
			{
				return this._comparedItemName;
			}
			set
			{
				if (value != this._comparedItemName)
				{
					this._comparedItemName = value;
					base.OnPropertyChangedWithValue<string>(value, "ComparedItemName");
				}
			}
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x0002F20C File Offset: 0x0002D40C
		private void SetArmorComponentTooltip()
		{
			int value = 0;
			if (this._comparedItem != null && this._comparedItem.ItemRosterElement.EquipmentElement.Item != null)
			{
				value = (int)(this._comparedItem.ItemRosterElement.EquipmentElement.Item.Tier + 1);
			}
			this.AddIntProperty(this._armorTierText, (int)(this._targetItem.ItemRosterElement.EquipmentElement.Item.Tier + 1), new int?(value));
			this.CreateProperty(this.TargetItemProperties, this._typeText.ToString(), GameTexts.FindText("str_inventory_type_" + (int)this._targetItem.ItemRosterElement.EquipmentElement.Item.Type, null).ToString(), 0, null);
			if (this.IsComparing)
			{
				this.CreateProperty(this.ComparedItemProperties, " ", GameTexts.FindText("str_inventory_type_" + (int)this._targetItem.ItemRosterElement.EquipmentElement.Item.Type, null).ToString(), 0, null);
			}
			ArmorComponent armorComponent = this._targetItem.ItemRosterElement.EquipmentElement.Item.ArmorComponent;
			ArmorComponent armorComponent2 = this.IsComparing ? this._comparedItem.ItemRosterElement.EquipmentElement.Item.ArmorComponent : null;
			if (armorComponent.HeadArmor != 0 || (this.IsComparing && armorComponent2.HeadArmor != 0))
			{
				int result = this.IsComparing ? this.CompareValues(this._targetItem.ItemRosterElement.EquipmentElement.GetModifiedHeadArmor(), this._comparedItem.ItemRosterElement.EquipmentElement.GetModifiedHeadArmor()) : 0;
				this.CreateColoredProperty(this.TargetItemProperties, this._headArmorText.ToString(), this._targetItem.ItemRosterElement.EquipmentElement.GetModifiedHeadArmor().ToString(), this.GetColorFromComparison(result, false), 0, null, TooltipProperty.TooltipPropertyFlags.None);
				if (this.IsComparing)
				{
					this.CreateColoredProperty(this.ComparedItemProperties, " ", this._comparedItem.ItemRosterElement.EquipmentElement.GetModifiedHeadArmor().ToString(), this.GetColorFromComparison(result, true), 0, null, TooltipProperty.TooltipPropertyFlags.None);
				}
			}
			if (armorComponent.BodyArmor != 0 || (this.IsComparing && this._comparedItem.ItemRosterElement.EquipmentElement.GetModifiedBodyArmor() != 0))
			{
				if (this._targetItem.ItemType == EquipmentIndex.HorseHarness)
				{
					int result = this.IsComparing ? this.CompareValues(this._targetItem.ItemRosterElement.EquipmentElement.GetModifiedMountBodyArmor(), this._comparedItem.ItemRosterElement.EquipmentElement.GetModifiedMountBodyArmor()) : 0;
					this.CreateColoredProperty(this.TargetItemProperties, this._horseArmorText.ToString(), this._targetItem.ItemRosterElement.EquipmentElement.GetModifiedMountBodyArmor().ToString(), this.GetColorFromComparison(result, false), 0, null, TooltipProperty.TooltipPropertyFlags.None);
					if (this.IsComparing)
					{
						this.CreateColoredProperty(this.ComparedItemProperties, " ", this._comparedItem.ItemRosterElement.EquipmentElement.GetModifiedMountBodyArmor().ToString(), this.GetColorFromComparison(result, true), 0, null, TooltipProperty.TooltipPropertyFlags.None);
					}
				}
				else
				{
					int result = this.IsComparing ? this.CompareValues(this._targetItem.ItemRosterElement.EquipmentElement.GetModifiedBodyArmor(), this._comparedItem.ItemRosterElement.EquipmentElement.GetModifiedBodyArmor()) : 0;
					this.CreateColoredProperty(this.TargetItemProperties, this._bodyArmorText.ToString(), this._targetItem.ItemRosterElement.EquipmentElement.GetModifiedBodyArmor().ToString(), this.GetColorFromComparison(result, false), 0, null, TooltipProperty.TooltipPropertyFlags.None);
					if (this.IsComparing)
					{
						this.CreateColoredProperty(this.ComparedItemProperties, " ", this._comparedItem.ItemRosterElement.EquipmentElement.GetModifiedBodyArmor().ToString(), this.GetColorFromComparison(result, true), 0, null, TooltipProperty.TooltipPropertyFlags.None);
					}
				}
			}
			if (this._targetItem.ItemRosterElement.EquipmentElement.GetModifiedLegArmor() != 0 || (this.IsComparing && this._comparedItem.ItemRosterElement.EquipmentElement.GetModifiedLegArmor() != 0))
			{
				int result = this.IsComparing ? this.CompareValues(this._targetItem.ItemRosterElement.EquipmentElement.GetModifiedLegArmor(), this._comparedItem.ItemRosterElement.EquipmentElement.GetModifiedLegArmor()) : 0;
				this.CreateColoredProperty(this.TargetItemProperties, this._legArmorText.ToString(), this._targetItem.ItemRosterElement.EquipmentElement.GetModifiedLegArmor().ToString(), this.GetColorFromComparison(result, false), 0, null, TooltipProperty.TooltipPropertyFlags.None);
				if (this.IsComparing)
				{
					this.CreateColoredProperty(this.ComparedItemProperties, " ", this._comparedItem.ItemRosterElement.EquipmentElement.GetModifiedLegArmor().ToString(), this.GetColorFromComparison(result, true), 0, null, TooltipProperty.TooltipPropertyFlags.None);
				}
			}
			if (this._targetItem.ItemRosterElement.EquipmentElement.GetModifiedArmArmor() != 0 || (this.IsComparing && this._comparedItem.ItemRosterElement.EquipmentElement.GetModifiedArmArmor() != 0))
			{
				int result = this.IsComparing ? this.CompareValues(this._targetItem.ItemRosterElement.EquipmentElement.GetModifiedArmArmor(), this._comparedItem.ItemRosterElement.EquipmentElement.GetModifiedArmArmor()) : 0;
				this.CreateColoredProperty(this.TargetItemProperties, this._armArmorText.ToString(), this._targetItem.ItemRosterElement.EquipmentElement.GetModifiedArmArmor().ToString(), this.GetColorFromComparison(result, false), 0, null, TooltipProperty.TooltipPropertyFlags.None);
				if (this.IsComparing)
				{
					this.CreateColoredProperty(this.ComparedItemProperties, " ", this._comparedItem.ItemRosterElement.EquipmentElement.GetModifiedArmArmor().ToString(), this.GetColorFromComparison(result, true), 0, null, TooltipProperty.TooltipPropertyFlags.None);
				}
			}
			this.AddDonationXpTooltip();
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000B9E RID: 2974 RVA: 0x0002F867 File Offset: 0x0002DA67
		// (set) Token: 0x06000B9F RID: 2975 RVA: 0x0002F86F File Offset: 0x0002DA6F
		[DataSourceProperty]
		public MBBindingList<ItemMenuTooltipPropertyVM> TargetItemProperties
		{
			get
			{
				return this._targetItemProperties;
			}
			set
			{
				if (value != this._targetItemProperties)
				{
					this._targetItemProperties = value;
					base.OnPropertyChangedWithValue<MBBindingList<ItemMenuTooltipPropertyVM>>(value, "TargetItemProperties");
				}
			}
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x0002F890 File Offset: 0x0002DA90
		private void AddDonationXpTooltip()
		{
			ItemDiscardModel itemDiscardModel = Campaign.Current.Models.ItemDiscardModel;
			int xpBonusForDiscardingItem = itemDiscardModel.GetXpBonusForDiscardingItem(this._targetItem.ItemRosterElement.EquipmentElement.Item, 1);
			int num = this.IsComparing ? itemDiscardModel.GetXpBonusForDiscardingItem(this._comparedItem.ItemRosterElement.EquipmentElement.Item, 1) : 0;
			if (xpBonusForDiscardingItem > 0 || (this.IsComparing && num > 0))
			{
				InventoryLogic inventoryLogic = this._inventoryLogic;
				if (inventoryLogic != null && inventoryLogic.IsDiscardDonating)
				{
					MBTextManager.SetTextVariable("LEFT", GameTexts.FindText("str_inventory_donation_item_hint", null).ToString(), false);
					int result = this.IsComparing ? this.CompareValues(xpBonusForDiscardingItem, num) : 0;
					this.CreateColoredProperty(this.TargetItemProperties, GameTexts.FindText("str_LEFT_colon", null).ToString(), xpBonusForDiscardingItem.ToString(), this.GetColorFromComparison(result, false), 0, null, TooltipProperty.TooltipPropertyFlags.None);
					if (this.IsComparing)
					{
						this.CreateColoredProperty(this.ComparedItemProperties, " ", num.ToString(), this.GetColorFromComparison(result, true), 0, null, TooltipProperty.TooltipPropertyFlags.None);
					}
				}
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000BA1 RID: 2977 RVA: 0x0002F9B2 File Offset: 0x0002DBB2
		// (set) Token: 0x06000BA2 RID: 2978 RVA: 0x0002F9BA File Offset: 0x0002DBBA
		[DataSourceProperty]
		public MBBindingList<ItemMenuTooltipPropertyVM> ComparedItemProperties
		{
			get
			{
				return this._comparedItemProperties;
			}
			set
			{
				if (value != this._comparedItemProperties)
				{
					this._comparedItemProperties = value;
					base.OnPropertyChangedWithValue<MBBindingList<ItemMenuTooltipPropertyVM>>(value, "ComparedItemProperties");
				}
			}
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x0002F9D8 File Offset: 0x0002DBD8
		private Color GetColorFromComparison(int result, bool isCompared)
		{
			if (result != -1)
			{
				if (result != 1)
				{
					return Colors.Black;
				}
				if (!isCompared)
				{
					return UIColors.PositiveIndicator;
				}
				return UIColors.NegativeIndicator;
			}
			else
			{
				if (!isCompared)
				{
					return UIColors.NegativeIndicator;
				}
				return UIColors.PositiveIndicator;
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000BA4 RID: 2980 RVA: 0x0002FA07 File Offset: 0x0002DC07
		// (set) Token: 0x06000BA5 RID: 2981 RVA: 0x0002FA0F File Offset: 0x0002DC0F
		[DataSourceProperty]
		public MBBindingList<ItemFlagVM> TargetItemFlagList
		{
			get
			{
				return this._targetItemFlagList;
			}
			set
			{
				if (value != this._targetItemFlagList)
				{
					this._targetItemFlagList = value;
					base.OnPropertyChangedWithValue<MBBindingList<ItemFlagVM>>(value, "TargetItemFlagList");
				}
			}
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0002FA30 File Offset: 0x0002DC30
		private int GetHorseCategoryValue(ItemCategory itemCategory)
		{
			if (itemCategory.IsAnimal)
			{
				if (itemCategory == DefaultItemCategories.PackAnimal)
				{
					return 1;
				}
				if (itemCategory == DefaultItemCategories.Horse)
				{
					return 2;
				}
				if (itemCategory == DefaultItemCategories.WarHorse)
				{
					return 3;
				}
				if (itemCategory == DefaultItemCategories.NobleHorse)
				{
					return 4;
				}
			}
			Debug.FailedAssert("This horse item category is not defined", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem.ViewModelCollection\\Inventory\\ItemMenuVM.cs", "GetHorseCategoryValue", 1378);
			return -1;
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000BA7 RID: 2983 RVA: 0x0002FA87 File Offset: 0x0002DC87
		// (set) Token: 0x06000BA8 RID: 2984 RVA: 0x0002FA8F File Offset: 0x0002DC8F
		[DataSourceProperty]
		public MBBindingList<ItemFlagVM> ComparedItemFlagList
		{
			get
			{
				return this._comparedItemFlagList;
			}
			set
			{
				if (value != this._comparedItemFlagList)
				{
					this._comparedItemFlagList = value;
					base.OnPropertyChangedWithValue<MBBindingList<ItemFlagVM>>(value, "ComparedItemFlagList");
				}
			}
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x0002FAB0 File Offset: 0x0002DCB0
		private ItemMenuTooltipPropertyVM CreateProperty(MBBindingList<ItemMenuTooltipPropertyVM> targetList, string definition, string value, int textHeight = 0, HintViewModel hint = null)
		{
			ItemMenuTooltipPropertyVM itemMenuTooltipPropertyVM = new ItemMenuTooltipPropertyVM(definition, value, textHeight, false, hint);
			targetList.Add(itemMenuTooltipPropertyVM);
			return itemMenuTooltipPropertyVM;
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000BAA RID: 2986 RVA: 0x0002FAD2 File Offset: 0x0002DCD2
		// (set) Token: 0x06000BAB RID: 2987 RVA: 0x0002FADA File Offset: 0x0002DCDA
		[DataSourceProperty]
		public int AlternativeUsageIndex
		{
			get
			{
				return this._alternativeUsageIndex;
			}
			set
			{
				if (value != this._alternativeUsageIndex)
				{
					this._alternativeUsageIndex = value;
					base.OnPropertyChangedWithValue(value, "AlternativeUsageIndex");
					this.AlternativeUsageIndexUpdated();
				}
			}
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x0002FB00 File Offset: 0x0002DD00
		private ItemMenuTooltipPropertyVM CreateColoredProperty(MBBindingList<ItemMenuTooltipPropertyVM> targetList, string definition, string value, Color color, int textHeight = 0, HintViewModel hint = null, TooltipProperty.TooltipPropertyFlags propertyFlags = TooltipProperty.TooltipPropertyFlags.None)
		{
			if (color == Colors.Black)
			{
				this.CreateProperty(targetList, definition, value, textHeight, hint);
				return null;
			}
			ItemMenuTooltipPropertyVM itemMenuTooltipPropertyVM = new ItemMenuTooltipPropertyVM(definition, value, textHeight, color, false, hint, propertyFlags);
			targetList.Add(itemMenuTooltipPropertyVM);
			return itemMenuTooltipPropertyVM;
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000BAD RID: 2989 RVA: 0x0002FB44 File Offset: 0x0002DD44
		// (set) Token: 0x06000BAE RID: 2990 RVA: 0x0002FB4C File Offset: 0x0002DD4C
		[DataSourceProperty]
		public MBBindingList<StringItemWithHintVM> AlternativeUsages
		{
			get
			{
				return this._alternativeUsages;
			}
			set
			{
				if (value != this._alternativeUsages)
				{
					this._alternativeUsages = value;
					base.OnPropertyChangedWithValue<MBBindingList<StringItemWithHintVM>>(value, "AlternativeUsages");
				}
			}
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x0002FB6C File Offset: 0x0002DD6C
		public void SetTransactionCost(int getItemTotalPrice, int maxIndividualPrice)
		{
			this.TransactionTotalCost = getItemTotalPrice;
			if (this._targetItem.ItemCost == maxIndividualPrice)
			{
				this._costProperty.ValueLabel = this._targetItem.ItemCost + this.GoldIcon;
				return;
			}
			if (this._targetItem.ItemCost < maxIndividualPrice)
			{
				this._costProperty.ValueLabel = string.Concat(new object[]
				{
					this._targetItem.ItemCost,
					" - ",
					maxIndividualPrice,
					this.GoldIcon
				});
				return;
			}
			this._costProperty.ValueLabel = string.Concat(new object[]
			{
				maxIndividualPrice,
				" - ",
				this._targetItem.ItemCost,
				this.GoldIcon
			});
		}

		// Token: 0x0400052B RID: 1323
		private readonly TextObject _swingDamageText = GameTexts.FindText("str_swing_damage", null);

		// Token: 0x0400052C RID: 1324
		private readonly TextObject _swingSpeedText = new TextObject("{=345a87fcc69f626ae3916939ef2fc135}Swing Speed: ", null);

		// Token: 0x0400052D RID: 1325
		private readonly TextObject _weaponTierText = new TextObject("{=weaponTier}Weapon Tier: ", null);

		// Token: 0x0400052E RID: 1326
		private readonly TextObject _armorTierText = new TextObject("{=armorTier}Armor Tier: ", null);

		// Token: 0x0400052F RID: 1327
		private readonly TextObject _horseTierText = new TextObject("{=mountTier}Mount Tier: ", null);

		// Token: 0x04000530 RID: 1328
		private readonly TextObject _horseTypeText = new TextObject("{=9sxECG6e}Mount Type: ", null);

		// Token: 0x04000531 RID: 1329
		private readonly TextObject _chargeDamageText = new TextObject("{=c7638a0869219ae845de0f660fd57a9d}Charge Damage: ", null);

		// Token: 0x04000532 RID: 1330
		private readonly TextObject _hitPointsText = GameTexts.FindText("str_hit_points", null);

		// Token: 0x04000533 RID: 1331
		private readonly TextObject _speedText = new TextObject("{=74dc1908cb0b990e80fb977b5a0ef10d}Speed: ", null);

		// Token: 0x04000534 RID: 1332
		private readonly TextObject _maneuverText = new TextObject("{=3025020b83b218707499f0de3135ed0a}Maneuver: ", null);

		// Token: 0x04000535 RID: 1333
		private readonly TextObject _thrustSpeedText = GameTexts.FindText("str_thrust_speed", null);

		// Token: 0x04000536 RID: 1334
		private readonly TextObject _thrustDamageText = GameTexts.FindText("str_thrust_damage", null);

		// Token: 0x04000537 RID: 1335
		private readonly TextObject _lengthText = new TextObject("{=c6e4c8588ca9e42f6e1b47b11f0f367b}Length: ", null);

		// Token: 0x04000538 RID: 1336
		private readonly TextObject _weightText = GameTexts.FindText("str_weight_text", null);

		// Token: 0x04000539 RID: 1337
		private readonly TextObject _handlingText = new TextObject("{=ca8b1e8956057b831dfc665f54bae4b0}Handling: ", null);

		// Token: 0x0400053A RID: 1338
		private readonly TextObject _weaponLengthText = new TextObject("{=5fa36d2798479803b4518a64beb4d732}Weapon Length: ", null);

		// Token: 0x0400053B RID: 1339
		private readonly TextObject _damageText = new TextObject("{=c9c5dfed2ca6bcb7a73d905004c97b23}Damage: ", null);

		// Token: 0x0400053C RID: 1340
		private readonly TextObject _missileSpeedText = GameTexts.FindText("str_missile_speed", null);

		// Token: 0x0400053D RID: 1341
		private readonly TextObject _accuracyText = new TextObject("{=5dec16fa0be433ade3c4cb0074ef366d}Accuracy: ", null);

		// Token: 0x0400053E RID: 1342
		private readonly TextObject _stackAmountText = new TextObject("{=05fdfc6e238429753ef282f2ce97c1f8}Stack Amount: ", null);

		// Token: 0x0400053F RID: 1343
		private readonly TextObject _ammoLimitText = new TextObject("{=6adabc1f82216992571c3e22abc164d7}Ammo Limit: ", null);

		// Token: 0x04000540 RID: 1344
		private readonly TextObject _requiresText = new TextObject("{=154a34f8caccfc833238cc89d38861e8}Requires: ", null);

		// Token: 0x04000541 RID: 1345
		private readonly TextObject _foodText = new TextObject("{=qSi4DlT4}Food", null);

		// Token: 0x04000542 RID: 1346
		private readonly TextObject _partyMoraleText = new TextObject("{=a241aacb1780599430c79fd9f667b67f}Party Morale: ", null);

		// Token: 0x04000543 RID: 1347
		private readonly TextObject _typeText = new TextObject("{=08abd5af7774d311cadc3ed900b47754}Type: ", null);

		// Token: 0x04000544 RID: 1348
		private readonly TextObject _tradeRumorsText = new TextObject("{=f2971dc587a9777223ad2d7be236fb05}Trade Rumors", null);

		// Token: 0x04000545 RID: 1349
		private readonly TextObject _classText = new TextObject("{=8cad4a279770f269c4bb0dc7a357ee1e}Class: ", null);

		// Token: 0x04000546 RID: 1350
		private readonly TextObject _headArmorText = GameTexts.FindText("str_head_armor", null);

		// Token: 0x04000547 RID: 1351
		private readonly TextObject _horseArmorText = new TextObject("{=305cf7f98458b22e9af72b60a131714f}Horse Armor: ", null);

		// Token: 0x04000548 RID: 1352
		private readonly TextObject _bodyArmorText = GameTexts.FindText("str_body_armor", null);

		// Token: 0x04000549 RID: 1353
		private readonly TextObject _legArmorText = GameTexts.FindText("str_leg_armor", null);

		// Token: 0x0400054A RID: 1354
		private readonly TextObject _armArmorText = new TextObject("{=cf61cce254c7dca65be9bebac7fb9bf5}Arm Armor: ", null);

		// Token: 0x0400054B RID: 1355
		private readonly TextObject _bannerEffectText = new TextObject("{=DbXZjPdf}Banner Effect: ", null);

		// Token: 0x0400054C RID: 1356
		private readonly TextObject _noneText = new TextObject("{=koX9okuG}None", null);

		// Token: 0x0400054D RID: 1357
		private readonly string GoldIcon = "<img src=\"General\\Icons\\Coin@2x\" extend=\"8\"/>";

		// Token: 0x0400054E RID: 1358
		private readonly Color ConsumableColor = Color.FromUint(4290873921U);

		// Token: 0x0400054F RID: 1359
		private readonly Color TitleColor = Color.FromUint(4293446041U);

		// Token: 0x04000550 RID: 1360
		private TooltipProperty _costProperty;

		// Token: 0x04000551 RID: 1361
		private InventoryLogic _inventoryLogic;

		// Token: 0x04000552 RID: 1362
		private Action<ItemVM, int> _resetComparedItems;

		// Token: 0x04000553 RID: 1363
		private readonly Func<WeaponComponentData, ItemObject.ItemUsageSetFlags> _getItemUsageSetFlags;

		// Token: 0x04000554 RID: 1364
		private readonly Func<EquipmentIndex, SPItemVM> _getEquipmentAtIndex;

		// Token: 0x04000555 RID: 1365
		private ItemVM _targetItem;

		// Token: 0x04000556 RID: 1366
		private bool _isComparing;

		// Token: 0x04000557 RID: 1367
		private ItemVM _comparedItem;

		// Token: 0x04000558 RID: 1368
		private bool _isPlayerItem;

		// Token: 0x04000559 RID: 1369
		private BasicCharacterObject _character;

		// Token: 0x0400055A RID: 1370
		private ImageIdentifierVM _imageIdentifier;

		// Token: 0x0400055B RID: 1371
		private ImageIdentifierVM _comparedImageIdentifier;

		// Token: 0x0400055C RID: 1372
		private string _itemName;

		// Token: 0x0400055D RID: 1373
		private string _comparedItemName;

		// Token: 0x0400055E RID: 1374
		private MBBindingList<ItemMenuTooltipPropertyVM> _comparedItemProperties;

		// Token: 0x0400055F RID: 1375
		private MBBindingList<ItemMenuTooltipPropertyVM> _targetItemProperties;

		// Token: 0x04000560 RID: 1376
		private bool _isInitializationOver;

		// Token: 0x04000561 RID: 1377
		private int _transactionTotalCost = -1;

		// Token: 0x04000562 RID: 1378
		private MBBindingList<ItemFlagVM> _targetItemFlagList;

		// Token: 0x04000563 RID: 1379
		private MBBindingList<ItemFlagVM> _comparedItemFlagList;

		// Token: 0x04000564 RID: 1380
		private int _alternativeUsageIndex;

		// Token: 0x04000565 RID: 1381
		private MBBindingList<StringItemWithHintVM> _alternativeUsages;

		// Token: 0x04000566 RID: 1382
		private ITradeRumorCampaignBehavior _tradeRumorsBehavior;
	}
}
