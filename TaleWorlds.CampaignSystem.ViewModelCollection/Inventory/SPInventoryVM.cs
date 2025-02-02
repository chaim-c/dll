using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.CampaignSystem.Inventory;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.ViewModelCollection.Input;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Core.ViewModelCollection.Selector;
using TaleWorlds.Core.ViewModelCollection.Tutorial;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Inventory
{
	// Token: 0x02000083 RID: 131
	public class SPInventoryVM : ViewModel, IInventoryStateHandler
	{
		// Token: 0x06000BDA RID: 3034 RVA: 0x00030388 File Offset: 0x0002E588
		public SPInventoryVM(InventoryLogic inventoryLogic, bool isInCivilianModeByDefault, Func<WeaponComponentData, ItemObject.ItemUsageSetFlags> getItemUsageSetFlags, string fiveStackShortcutkeyText, string entireStackShortcutkeyText)
		{
			this._usageType = InventoryManager.Instance.CurrentMode;
			this._inventoryLogic = inventoryLogic;
			this._viewDataTracker = Campaign.Current.GetCampaignBehavior<IViewDataTracker>();
			this._getItemUsageSetFlags = getItemUsageSetFlags;
			this._fiveStackShortcutkeyText = fiveStackShortcutkeyText;
			this._entireStackShortcutkeyText = entireStackShortcutkeyText;
			this._filters = new Dictionary<SPInventoryVM.Filters, List<int>>();
			this._filters.Add(SPInventoryVM.Filters.All, this._everyItemType);
			this._filters.Add(SPInventoryVM.Filters.Weapons, this._weaponItemTypes);
			this._filters.Add(SPInventoryVM.Filters.Armors, this._armorItemTypes);
			this._filters.Add(SPInventoryVM.Filters.Mounts, this._mountItemTypes);
			this._filters.Add(SPInventoryVM.Filters.ShieldsAndRanged, this._shieldAndRangedItemTypes);
			this._filters.Add(SPInventoryVM.Filters.Miscellaneous, this._miscellaneousItemTypes);
			this._equipAfterTransferStack = new Stack<SPItemVM>();
			this._comparedItemList = new List<ItemVM>();
			this._donationMaxShareableXp = MobilePartyHelper.GetMaximumXpAmountPartyCanGet(MobileParty.MainParty);
			MBTextManager.SetTextVariable("XP_DONATION_LIMIT", this._donationMaxShareableXp);
			if (this._inventoryLogic != null)
			{
				this._currentCharacter = this._inventoryLogic.InitialEquipmentCharacter;
				this._isTrading = inventoryLogic.IsTrading;
				this._inventoryLogic.AfterReset += this.AfterReset;
				InventoryLogic inventoryLogic2 = this._inventoryLogic;
				inventoryLogic2.TotalAmountChange = (Action<int>)Delegate.Combine(inventoryLogic2.TotalAmountChange, new Action<int>(this.OnTotalAmountChange));
				InventoryLogic inventoryLogic3 = this._inventoryLogic;
				inventoryLogic3.DonationXpChange = (Action)Delegate.Combine(inventoryLogic3.DonationXpChange, new Action(this.OnDonationXpChange));
				this._inventoryLogic.AfterTransfer += this.AfterTransfer;
				this._rightTroopRoster = inventoryLogic.RightMemberRoster;
				this._leftTroopRoster = inventoryLogic.LeftMemberRoster;
				this._currentInventoryCharacterIndex = this._rightTroopRoster.FindIndexOfTroop(this._currentCharacter);
				this.OnDonationXpChange();
				this.CompanionExists = this.DoesCompanionExist();
			}
			this.MainCharacter = new HeroViewModel(CharacterViewModel.StanceTypes.None);
			this.MainCharacter.FillFrom(this._currentCharacter.HeroObject, -1, false, false);
			this.ItemMenu = new ItemMenuVM(new Action<ItemVM, int>(this.ResetComparedItems), this._inventoryLogic, this._getItemUsageSetFlags, new Func<EquipmentIndex, SPItemVM>(this.GetItemFromIndex));
			this.IsRefreshed = false;
			this.RightItemListVM = new MBBindingList<SPItemVM>();
			this.LeftItemListVM = new MBBindingList<SPItemVM>();
			this.CharacterHelmSlot = new SPItemVM();
			this.CharacterCloakSlot = new SPItemVM();
			this.CharacterTorsoSlot = new SPItemVM();
			this.CharacterGloveSlot = new SPItemVM();
			this.CharacterBootSlot = new SPItemVM();
			this.CharacterMountSlot = new SPItemVM();
			this.CharacterMountArmorSlot = new SPItemVM();
			this.CharacterWeapon1Slot = new SPItemVM();
			this.CharacterWeapon2Slot = new SPItemVM();
			this.CharacterWeapon3Slot = new SPItemVM();
			this.CharacterWeapon4Slot = new SPItemVM();
			this.CharacterBannerSlot = new SPItemVM();
			this.ProductionTooltip = new BasicTooltipViewModel();
			this.CurrentCharacterSkillsTooltip = new BasicTooltipViewModel(() => CampaignUIHelper.GetInventoryCharacterTooltip(this._currentCharacter.HeroObject));
			this.RefreshCallbacks();
			this._selectedEquipmentIndex = 0;
			this.EquipmentMaxCountHint = new BasicTooltipViewModel();
			this.IsInWarSet = !isInCivilianModeByDefault;
			if (this._inventoryLogic != null)
			{
				this.UpdateRightCharacter();
				this.UpdateLeftCharacter();
				this.InitializeInventory();
			}
			this.RightInventoryOwnerGold = Hero.MainHero.Gold;
			if (this._inventoryLogic.OtherSideCapacityData != null)
			{
				this.OtherSideHasCapacity = (this._inventoryLogic.OtherSideCapacityData.GetCapacity() != -1);
			}
			this.IsOtherInventoryGoldRelevant = (this._usageType != InventoryMode.Loot);
			this.PlayerInventorySortController = new SPInventorySortControllerVM(ref this._rightItemListVM);
			this.OtherInventorySortController = new SPInventorySortControllerVM(ref this._leftItemListVM);
			this.PlayerInventorySortController.SortByDefaultState();
			if (this._usageType == InventoryMode.Loot)
			{
				this.OtherInventorySortController.CostState = 1;
				this.OtherInventorySortController.ExecuteSortByCost();
			}
			else
			{
				this.OtherInventorySortController.SortByDefaultState();
			}
			Tuple<int, int> tuple = this._viewDataTracker.InventoryGetSortPreference((int)this._usageType);
			if (tuple != null)
			{
				this.PlayerInventorySortController.SortByOption((SPInventorySortControllerVM.InventoryItemSortOption)tuple.Item1, (SPInventorySortControllerVM.InventoryItemSortState)tuple.Item2);
			}
			this.ItemPreview = new ItemPreviewVM(new Action(this.OnPreviewClosed));
			this._characterList = new SelectorVM<InventoryCharacterSelectorItemVM>(0, new Action<SelectorVM<InventoryCharacterSelectorItemVM>>(this.OnCharacterSelected));
			this.AddApplicableCharactersToListFromRoster(this._rightTroopRoster.GetTroopRoster());
			if (this._inventoryLogic.IsOtherPartyFromPlayerClan && this._leftTroopRoster != null)
			{
				this.AddApplicableCharactersToListFromRoster(this._leftTroopRoster.GetTroopRoster());
			}
			if (this._characterList.SelectedIndex == -1 && this._characterList.ItemList.Count > 0)
			{
				this._characterList.SelectedIndex = 0;
			}
			this.BannerTypeCode = 24;
			InventoryTradeVM.RemoveZeroCounts += this.ExecuteRemoveZeroCounts;
			Game.Current.EventManager.RegisterEvent<TutorialNotificationElementChangeEvent>(new Action<TutorialNotificationElementChangeEvent>(this.OnTutorialNotificationElementIDChange));
			this.RefreshValues();
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x00030A74 File Offset: 0x0002EC74
		private void AddApplicableCharactersToListFromRoster(MBList<TroopRosterElement> roster)
		{
			for (int i = 0; i < roster.Count; i++)
			{
				CharacterObject character = roster[i].Character;
				if (character.IsHero && this.CanSelectHero(character.HeroObject))
				{
					this._characterList.AddItem(new InventoryCharacterSelectorItemVM(character.HeroObject.StringId, character.HeroObject, character.HeroObject.Name));
					if (character == this._currentCharacter)
					{
						this._characterList.SelectedIndex = this._characterList.ItemList.Count - 1;
					}
				}
			}
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x00030B0C File Offset: 0x0002ED0C
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.RightInventoryOwnerName = PartyBase.MainParty.Name.ToString();
			this.DoneLbl = GameTexts.FindText("str_done", null).ToString();
			this.CancelLbl = GameTexts.FindText("str_cancel", null).ToString();
			this.ResetLbl = GameTexts.FindText("str_reset", null).ToString();
			this.TypeText = GameTexts.FindText("str_sort_by_type_label", null).ToString();
			this.NameText = GameTexts.FindText("str_sort_by_name_label", null).ToString();
			this.QuantityText = GameTexts.FindText("str_quantity_sign", null).ToString();
			this.CostText = GameTexts.FindText("str_value", null).ToString();
			this.SearchPlaceholderText = new TextObject("{=tQOPRBFg}Search...", null).ToString();
			this.FilterAllHint = new HintViewModel(GameTexts.FindText("str_inventory_filter_all", null), null);
			this.FilterWeaponHint = new HintViewModel(GameTexts.FindText("str_inventory_filter_weapons", null), null);
			this.FilterArmorHint = new HintViewModel(GameTexts.FindText("str_inventory_filter_armors", null), null);
			this.FilterShieldAndRangedHint = new HintViewModel(GameTexts.FindText("str_inventory_filter_shields_ranged", null), null);
			this.FilterMountAndHarnessHint = new HintViewModel(GameTexts.FindText("str_inventory_filter_mounts", null), null);
			this.FilterMiscHint = new HintViewModel(GameTexts.FindText("str_inventory_filter_other", null), null);
			this.CivilianOutfitHint = new HintViewModel(GameTexts.FindText("str_inventory_civilian_outfit", null), null);
			this.BattleOutfitHint = new HintViewModel(GameTexts.FindText("str_inventory_battle_outfit", null), null);
			this.EquipmentHelmSlotHint = new HintViewModel(GameTexts.FindText("str_inventory_helm_slot", null), null);
			this.EquipmentArmorSlotHint = new HintViewModel(GameTexts.FindText("str_inventory_armor_slot", null), null);
			this.EquipmentBootSlotHint = new HintViewModel(GameTexts.FindText("str_inventory_boot_slot", null), null);
			this.EquipmentCloakSlotHint = new HintViewModel(GameTexts.FindText("str_inventory_cloak_slot", null), null);
			this.EquipmentGloveSlotHint = new HintViewModel(GameTexts.FindText("str_inventory_glove_slot", null), null);
			this.EquipmentHarnessSlotHint = new HintViewModel(GameTexts.FindText("str_inventory_mount_armor_slot", null), null);
			this.EquipmentMountSlotHint = new HintViewModel(GameTexts.FindText("str_inventory_mount_slot", null), null);
			this.EquipmentWeaponSlotHint = new HintViewModel(GameTexts.FindText("str_inventory_filter_weapons", null), null);
			this.EquipmentBannerSlotHint = new HintViewModel(GameTexts.FindText("str_inventory_banner_slot", null), null);
			this.WeightHint = new HintViewModel(GameTexts.FindText("str_inventory_weight_desc", null), null);
			this.ArmArmorHint = new HintViewModel(GameTexts.FindText("str_inventory_arm_armor", null), null);
			this.BodyArmorHint = new HintViewModel(GameTexts.FindText("str_inventory_body_armor", null), null);
			this.HeadArmorHint = new HintViewModel(GameTexts.FindText("str_inventory_head_armor", null), null);
			this.LegArmorHint = new HintViewModel(GameTexts.FindText("str_inventory_leg_armor", null), null);
			this.HorseArmorHint = new HintViewModel(GameTexts.FindText("str_inventory_horse_armor", null), null);
			this.DonationLblHint = new HintViewModel(GameTexts.FindText("str_inventory_donation_label_hint", null), null);
			this.SetPreviousCharacterHint();
			this.SetNextCharacterHint();
			this.PreviewHint = new HintViewModel(GameTexts.FindText("str_inventory_preview", null), null);
			this.EquipHint = new HintViewModel(GameTexts.FindText("str_inventory_equip", null), null);
			this.UnequipHint = new HintViewModel(GameTexts.FindText("str_inventory_unequip", null), null);
			this.ResetHint = new HintViewModel(GameTexts.FindText("str_reset", null), null);
			this.PlayerSideCapacityExceededText = GameTexts.FindText("str_capacity_exceeded", null).ToString();
			this.PlayerSideCapacityExceededHint = new HintViewModel(GameTexts.FindText("str_capacity_exceeded_hint", null), null);
			if (this._inventoryLogic.OtherSideCapacityData != null)
			{
				TextObject capacityExceededWarningText = this._inventoryLogic.OtherSideCapacityData.GetCapacityExceededWarningText();
				this.OtherSideCapacityExceededText = ((capacityExceededWarningText != null) ? capacityExceededWarningText.ToString() : null);
				this.OtherSideCapacityExceededHint = new HintViewModel(this._inventoryLogic.OtherSideCapacityData.GetCapacityExceededHintText(), null);
			}
			this.SetBuyAllHint();
			this.SetSellAllHint();
			if (this._usageType == InventoryMode.Loot || this._usageType == InventoryMode.Stash)
			{
				this.SellHint = new HintViewModel(GameTexts.FindText("str_inventory_give", null), null);
			}
			else if (this._usageType == InventoryMode.Default)
			{
				this.SellHint = new HintViewModel(GameTexts.FindText("str_inventory_discard", null), null);
			}
			else
			{
				this.SellHint = new HintViewModel(GameTexts.FindText("str_inventory_sell", null), null);
			}
			this.CharacterHelmSlot.RefreshValues();
			this.CharacterCloakSlot.RefreshValues();
			this.CharacterTorsoSlot.RefreshValues();
			this.CharacterGloveSlot.RefreshValues();
			this.CharacterBootSlot.RefreshValues();
			this.CharacterMountSlot.RefreshValues();
			this.CharacterMountArmorSlot.RefreshValues();
			this.CharacterWeapon1Slot.RefreshValues();
			this.CharacterWeapon2Slot.RefreshValues();
			this.CharacterWeapon3Slot.RefreshValues();
			this.CharacterWeapon4Slot.RefreshValues();
			this.CharacterBannerSlot.RefreshValues();
			SPInventorySortControllerVM playerInventorySortController = this.PlayerInventorySortController;
			if (playerInventorySortController != null)
			{
				playerInventorySortController.RefreshValues();
			}
			SPInventorySortControllerVM otherInventorySortController = this.OtherInventorySortController;
			if (otherInventorySortController == null)
			{
				return;
			}
			otherInventorySortController.RefreshValues();
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x00031000 File Offset: 0x0002F200
		public override void OnFinalize()
		{
			ItemVM.ProcessEquipItem = null;
			ItemVM.ProcessUnequipItem = null;
			ItemVM.ProcessPreviewItem = null;
			ItemVM.ProcessBuyItem = null;
			SPItemVM.ProcessSellItem = null;
			ItemVM.ProcessItemSelect = null;
			ItemVM.ProcessItemTooltip = null;
			SPItemVM.ProcessItemSlaughter = null;
			SPItemVM.ProcessItemDonate = null;
			SPItemVM.OnFocus = null;
			InventoryTradeVM.RemoveZeroCounts -= this.ExecuteRemoveZeroCounts;
			Game.Current.EventManager.UnregisterEvent<TutorialNotificationElementChangeEvent>(new Action<TutorialNotificationElementChangeEvent>(this.OnTutorialNotificationElementIDChange));
			this.ItemPreview.OnFinalize();
			this.ItemPreview = null;
			this.CancelInputKey.OnFinalize();
			this.DoneInputKey.OnFinalize();
			this.ResetInputKey.OnFinalize();
			this.PreviousCharacterInputKey.OnFinalize();
			this.NextCharacterInputKey.OnFinalize();
			this.BuyAllInputKey.OnFinalize();
			this.SellAllInputKey.OnFinalize();
			ItemVM.ProcessEquipItem = null;
			ItemVM.ProcessUnequipItem = null;
			ItemVM.ProcessPreviewItem = null;
			ItemVM.ProcessBuyItem = null;
			SPItemVM.ProcessLockItem = null;
			SPItemVM.ProcessSellItem = null;
			ItemVM.ProcessItemSelect = null;
			ItemVM.ProcessItemTooltip = null;
			SPItemVM.ProcessItemSlaughter = null;
			SPItemVM.ProcessItemDonate = null;
			SPItemVM.OnFocus = null;
			this.MainCharacter.OnFinalize();
			this._isFinalized = true;
			this._inventoryLogic = null;
			base.OnFinalize();
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x00031138 File Offset: 0x0002F338
		public void RefreshCallbacks()
		{
			ItemVM.ProcessEquipItem = new Action<ItemVM>(this.ProcessEquipItem);
			ItemVM.ProcessUnequipItem = new Action<ItemVM>(this.ProcessUnequipItem);
			ItemVM.ProcessPreviewItem = new Action<ItemVM>(this.ProcessPreviewItem);
			ItemVM.ProcessBuyItem = new Action<ItemVM, bool>(this.ProcessBuyItem);
			SPItemVM.ProcessLockItem = new Action<SPItemVM, bool>(this.ProcessLockItem);
			SPItemVM.ProcessSellItem = new Action<SPItemVM, bool>(this.ProcessSellItem);
			ItemVM.ProcessItemSelect = new Action<ItemVM>(this.ProcessItemSelect);
			ItemVM.ProcessItemTooltip = new Action<ItemVM>(this.ProcessItemTooltip);
			SPItemVM.ProcessItemSlaughter = new Action<SPItemVM>(this.ProcessItemSlaughter);
			SPItemVM.ProcessItemDonate = new Action<SPItemVM>(this.ProcessItemDonate);
			SPItemVM.OnFocus = new Action<SPItemVM>(this.OnItemFocus);
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x00031200 File Offset: 0x0002F400
		private bool CanSelectHero(Hero hero)
		{
			return hero.IsAlive && hero.CanHeroEquipmentBeChanged() && hero.Clan == Clan.PlayerClan && hero.HeroState != Hero.CharacterStates.Disabled && !hero.IsChild;
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x00031233 File Offset: 0x0002F433
		private void SetPreviousCharacterHint()
		{
			this.PreviousCharacterHint = new BasicTooltipViewModel(delegate()
			{
				GameTexts.SetVariable("HOTKEY", this.GetPreviousCharacterKeyText());
				GameTexts.SetVariable("TEXT", GameTexts.FindText("str_inventory_prev_char", null));
				return GameTexts.FindText("str_hotkey_with_hint", null).ToString();
			});
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x0003124C File Offset: 0x0002F44C
		private void SetNextCharacterHint()
		{
			this.NextCharacterHint = new BasicTooltipViewModel(delegate()
			{
				GameTexts.SetVariable("HOTKEY", this.GetNextCharacterKeyText());
				GameTexts.SetVariable("TEXT", GameTexts.FindText("str_inventory_next_char", null));
				return GameTexts.FindText("str_hotkey_with_hint", null).ToString();
			});
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x00031268 File Offset: 0x0002F468
		private void SetBuyAllHint()
		{
			TextObject buyAllHintText;
			if (this._usageType == InventoryMode.Trade)
			{
				buyAllHintText = GameTexts.FindText("str_inventory_buy_all", null);
			}
			else
			{
				buyAllHintText = GameTexts.FindText("str_inventory_take_all", null);
			}
			this.BuyAllHint = new BasicTooltipViewModel(delegate()
			{
				GameTexts.SetVariable("HOTKEY", this.GetBuyAllKeyText());
				GameTexts.SetVariable("TEXT", buyAllHintText);
				return GameTexts.FindText("str_hotkey_with_hint", null).ToString();
			});
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x000312C8 File Offset: 0x0002F4C8
		private void SetSellAllHint()
		{
			TextObject sellAllHintText;
			if (this._usageType == InventoryMode.Loot || this._usageType == InventoryMode.Stash)
			{
				sellAllHintText = GameTexts.FindText("str_inventory_give_all", null);
			}
			else if (this._usageType == InventoryMode.Default)
			{
				sellAllHintText = GameTexts.FindText("str_inventory_discard_all", null);
			}
			else
			{
				sellAllHintText = GameTexts.FindText("str_inventory_sell_all", null);
			}
			this.SellAllHint = new BasicTooltipViewModel(delegate()
			{
				GameTexts.SetVariable("HOTKEY", this.GetSellAllKeyText());
				GameTexts.SetVariable("TEXT", sellAllHintText);
				return GameTexts.FindText("str_hotkey_with_hint", null).ToString();
			});
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x0003134C File Offset: 0x0002F54C
		private void OnCharacterSelected(SelectorVM<InventoryCharacterSelectorItemVM> selector)
		{
			if (this._inventoryLogic == null || selector.SelectedItem == null)
			{
				return;
			}
			for (int i = 0; i < this._rightTroopRoster.Count; i++)
			{
				if (this._rightTroopRoster.GetCharacterAtIndex(i).StringId == selector.SelectedItem.CharacterID)
				{
					this.UpdateCurrentCharacterIfPossible(i, true);
					return;
				}
			}
			if (this._leftTroopRoster != null)
			{
				for (int j = 0; j < this._leftTroopRoster.Count; j++)
				{
					if (this._leftTroopRoster.GetCharacterAtIndex(j).StringId == selector.SelectedItem.CharacterID)
					{
						this.UpdateCurrentCharacterIfPossible(j, false);
						return;
					}
				}
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000BE5 RID: 3045 RVA: 0x000313FE File Offset: 0x0002F5FE
		private Equipment ActiveEquipment
		{
			get
			{
				if (!this.IsInWarSet)
				{
					return this._currentCharacter.FirstCivilianEquipment;
				}
				return this._currentCharacter.FirstBattleEquipment;
			}
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x0003141F File Offset: 0x0002F61F
		public void ExecuteShowRecap()
		{
			InformationManager.ShowTooltip(typeof(InventoryLogic), new object[]
			{
				this._inventoryLogic
			});
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x0003143F File Offset: 0x0002F63F
		public void ExecuteCancelRecap()
		{
			MBInformationManager.HideInformations();
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x00031448 File Offset: 0x0002F648
		public void ExecuteRemoveZeroCounts()
		{
			List<SPItemVM> list = this.LeftItemListVM.ToList<SPItemVM>();
			for (int i = list.Count - 1; i >= 0; i--)
			{
				if (list[i].ItemCount == 0 && i >= 0 && i < this.LeftItemListVM.Count)
				{
					this.LeftItemListVM.RemoveAt(i);
				}
			}
			List<SPItemVM> list2 = this.RightItemListVM.ToList<SPItemVM>();
			for (int j = list2.Count - 1; j >= 0; j--)
			{
				if (list2[j].ItemCount == 0 && j >= 0 && j < this.RightItemListVM.Count)
				{
					this.RightItemListVM.RemoveAt(j);
				}
			}
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x000314EB File Offset: 0x0002F6EB
		private void ProcessPreviewItem(ItemVM item)
		{
			this._inventoryLogic.IsPreviewingItem = true;
			this.ItemPreview.Open(item.ItemRosterElement.EquipmentElement);
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x0003150F File Offset: 0x0002F70F
		public void ClosePreview()
		{
			this.ItemPreview.Close();
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x0003151C File Offset: 0x0002F71C
		private void OnPreviewClosed()
		{
			this._inventoryLogic.IsPreviewingItem = false;
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x0003152C File Offset: 0x0002F72C
		private void ProcessEquipItem(ItemVM draggedItem)
		{
			SPItemVM spitemVM = draggedItem as SPItemVM;
			if (!spitemVM.IsCivilianItem && !this.IsInWarSet)
			{
				return;
			}
			this.IsRefreshed = false;
			this.EquipEquipment(spitemVM);
			this.RefreshInformationValues();
			this.ExecuteRemoveZeroCounts();
			this.IsRefreshed = true;
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x00031572 File Offset: 0x0002F772
		private void ProcessUnequipItem(ItemVM draggedItem)
		{
			this.IsRefreshed = false;
			this.UnequipEquipment(draggedItem as SPItemVM);
			this.RefreshInformationValues();
			this.IsRefreshed = true;
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x00031594 File Offset: 0x0002F794
		private void ProcessBuyItem(ItemVM itemBase, bool cameFromTradeData)
		{
			SPItemVM spitemVM = itemBase as SPItemVM;
			if (this.IsEntireStackModifierActive && !cameFromTradeData)
			{
				ItemRosterElement? itemRosterElement;
				this.TransactionCount = ((this._inventoryLogic.FindItemFromSide(InventoryLogic.InventorySide.OtherInventory, (spitemVM != null) ? spitemVM.ItemRosterElement.EquipmentElement : EquipmentElement.Invalid) != null) ? itemRosterElement.GetValueOrDefault().Amount : 0);
			}
			else if (this.IsFiveStackModifierActive && !cameFromTradeData)
			{
				this.TransactionCount = 5;
			}
			else
			{
				this.TransactionCount = ((spitemVM != null) ? spitemVM.TransactionCount : 0);
			}
			if (this.TransactionCount == 0)
			{
				Debug.FailedAssert("Transaction count should not be zero", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem.ViewModelCollection\\Inventory\\SPInventoryVM.cs", "ProcessBuyItem", 577);
				return;
			}
			this.IsRefreshed = false;
			MBTextManager.SetTextVariable("ITEM_DESCRIPTION", itemBase.ItemDescription, false);
			MBTextManager.SetTextVariable("ITEM_COST", itemBase.ItemCost);
			this.BuyItem(spitemVM);
			if (!cameFromTradeData)
			{
				this.ExecuteRemoveZeroCounts();
			}
			this.RefreshInformationValues();
			this.IsRefreshed = true;
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x00031688 File Offset: 0x0002F888
		private void ProcessSellItem(SPItemVM item, bool cameFromTradeData)
		{
			if (item.InventorySide == InventoryLogic.InventorySide.Equipment)
			{
				this.TransactionCount = 1;
			}
			else if (this.IsEntireStackModifierActive && !cameFromTradeData)
			{
				ItemRosterElement? itemRosterElement = this._inventoryLogic.FindItemFromSide(InventoryLogic.InventorySide.PlayerInventory, item.ItemRosterElement.EquipmentElement);
				this.TransactionCount = ((itemRosterElement != null) ? itemRosterElement.GetValueOrDefault().Amount : 0);
			}
			else if (this.IsFiveStackModifierActive && !cameFromTradeData)
			{
				this.TransactionCount = 5;
			}
			else
			{
				this.TransactionCount = item.TransactionCount;
			}
			if (this.TransactionCount == 0)
			{
				Debug.FailedAssert("Transaction count should not be zero", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem.ViewModelCollection\\Inventory\\SPInventoryVM.cs", "ProcessSellItem", 622);
				return;
			}
			this.IsRefreshed = false;
			MBTextManager.SetTextVariable("ITEM_DESCRIPTION", item.ItemDescription, false);
			MBTextManager.SetTextVariable("ITEM_COST", item.ItemCost);
			this.SellItem(item);
			if (!cameFromTradeData)
			{
				this.ExecuteRemoveZeroCounts();
			}
			this.RefreshInformationValues();
			this.IsRefreshed = true;
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x00031774 File Offset: 0x0002F974
		private void ProcessLockItem(SPItemVM item, bool isLocked)
		{
			if (isLocked && item.InventorySide == InventoryLogic.InventorySide.PlayerInventory && !this._lockedItemIDs.Contains(item.StringId))
			{
				this._lockedItemIDs.Add(item.StringId);
				return;
			}
			if (!isLocked && item.InventorySide == InventoryLogic.InventorySide.PlayerInventory && this._lockedItemIDs.Contains(item.StringId))
			{
				this._lockedItemIDs.Remove(item.StringId);
			}
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x000317E4 File Offset: 0x0002F9E4
		private ItemVM ProcessCompareItem(ItemVM item, int alternativeUsageIndex = 0)
		{
			this._selectedEquipmentIndex = 0;
			this._comparedItemList.Clear();
			ItemVM itemVM = null;
			bool flag = false;
			EquipmentIndex equipmentIndex = EquipmentIndex.None;
			SPItemVM spitemVM = null;
			bool flag2 = item.ItemType >= EquipmentIndex.WeaponItemBeginSlot && item.ItemType < EquipmentIndex.ExtraWeaponSlot;
			if (((SPItemVM)item).InventorySide != InventoryLogic.InventorySide.Equipment)
			{
				if (flag2)
				{
					for (EquipmentIndex equipmentIndex2 = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex2 < EquipmentIndex.ExtraWeaponSlot; equipmentIndex2++)
					{
						EquipmentIndex itemType = equipmentIndex2;
						SPItemVM itemFromIndex = this.GetItemFromIndex(itemType);
						if (itemFromIndex != null && itemFromIndex.ItemRosterElement.EquipmentElement.Item != null && ItemHelper.CheckComparability(item.ItemRosterElement.EquipmentElement.Item, itemFromIndex.ItemRosterElement.EquipmentElement.Item, alternativeUsageIndex))
						{
							this._comparedItemList.Add(itemFromIndex);
						}
					}
					if (!this._comparedItemList.IsEmpty<ItemVM>())
					{
						this.SortComparedItems(item);
						itemVM = this._comparedItemList[0];
						this._lastComparedItemIndex = 0;
					}
					if (itemVM != null)
					{
						equipmentIndex = itemVM.ItemType;
					}
				}
				else
				{
					equipmentIndex = item.ItemType;
				}
			}
			if (item.ItemType >= EquipmentIndex.WeaponItemBeginSlot && item.ItemType < EquipmentIndex.NumEquipmentSetSlots)
			{
				spitemVM = ((equipmentIndex != EquipmentIndex.None) ? this.GetItemFromIndex(equipmentIndex) : null);
				flag = (spitemVM != null && !string.IsNullOrEmpty(spitemVM.StringId) && item.StringId != spitemVM.StringId);
			}
			if (!this._selectedTooltipItemStringID.Equals(item.StringId) || (flag && !this._comparedTooltipItemStringID.Equals(spitemVM.StringId)))
			{
				this._selectedTooltipItemStringID = item.StringId;
				if (flag)
				{
					this._comparedTooltipItemStringID = spitemVM.StringId;
				}
			}
			this._selectedEquipmentIndex = (int)equipmentIndex;
			if (spitemVM == null || spitemVM.ItemRosterElement.IsEmpty)
			{
				return null;
			}
			return spitemVM;
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x00031994 File Offset: 0x0002FB94
		private void ResetComparedItems(ItemVM item, int alternativeUsageIndex)
		{
			ItemVM comparedItem = this.ProcessCompareItem(item, alternativeUsageIndex);
			this.ItemMenu.SetItem(this._selectedItem, comparedItem, this._currentCharacter, alternativeUsageIndex);
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x000319C4 File Offset: 0x0002FBC4
		private void SortComparedItems(ItemVM selectedItem)
		{
			List<ItemVM> list = new List<ItemVM>();
			for (int i = 0; i < this._comparedItemList.Count; i++)
			{
				if (selectedItem.StringId == this._comparedItemList[i].StringId && !list.Contains(this._comparedItemList[i]))
				{
					list.Add(this._comparedItemList[i]);
				}
			}
			for (int j = 0; j < this._comparedItemList.Count; j++)
			{
				if (this._comparedItemList[j].ItemRosterElement.EquipmentElement.Item.Type == selectedItem.ItemRosterElement.EquipmentElement.Item.Type && !list.Contains(this._comparedItemList[j]))
				{
					list.Add(this._comparedItemList[j]);
				}
			}
			for (int k = 0; k < this._comparedItemList.Count; k++)
			{
				WeaponComponent weaponComponent = this._comparedItemList[k].ItemRosterElement.EquipmentElement.Item.WeaponComponent;
				WeaponComponent weaponComponent2 = selectedItem.ItemRosterElement.EquipmentElement.Item.WeaponComponent;
				if (((weaponComponent2.Weapons.Count > 1 && weaponComponent2.Weapons[1].WeaponClass == weaponComponent.Weapons[0].WeaponClass) || (weaponComponent.Weapons.Count > 1 && weaponComponent.Weapons[1].WeaponClass == weaponComponent2.Weapons[0].WeaponClass) || (weaponComponent2.Weapons.Count > 1 && weaponComponent.Weapons.Count > 1 && weaponComponent2.Weapons[1].WeaponClass == weaponComponent.Weapons[1].WeaponClass)) && !list.Contains(this._comparedItemList[k]))
				{
					list.Add(this._comparedItemList[k]);
				}
			}
			if (this._comparedItemList.Count != list.Count)
			{
				foreach (ItemVM item in this._comparedItemList)
				{
					if (!list.Contains(item))
					{
						list.Add(item);
					}
				}
			}
			this._comparedItemList = list;
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x00031C50 File Offset: 0x0002FE50
		public void ProcessItemTooltip(ItemVM item)
		{
			if (item == null || string.IsNullOrEmpty(item.StringId))
			{
				return;
			}
			this._selectedItem = (item as SPItemVM);
			ItemVM comparedItem = this.ProcessCompareItem(item, 0);
			this.ItemMenu.SetItem(this._selectedItem, comparedItem, this._currentCharacter, 0);
			this.RefreshTransactionCost(1);
			this._selectedItem.UpdateCanBeSlaughtered();
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x00031CAE File Offset: 0x0002FEAE
		public void ResetSelectedItem()
		{
			this._selectedItem = null;
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x00031CB8 File Offset: 0x0002FEB8
		private void ProcessItemSlaughter(SPItemVM item)
		{
			this.IsRefreshed = false;
			if (string.IsNullOrEmpty(item.StringId) || !item.CanBeSlaughtered)
			{
				return;
			}
			this.SlaughterItem(item);
			this.RefreshInformationValues();
			if (item.ItemCount == 0)
			{
				this.ExecuteRemoveZeroCounts();
			}
			this.IsRefreshed = true;
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x00031D04 File Offset: 0x0002FF04
		private void ProcessItemDonate(SPItemVM item)
		{
			this.IsRefreshed = false;
			if (string.IsNullOrEmpty(item.StringId) || !item.CanBeDonated)
			{
				return;
			}
			this.DonateItem(item);
			this.RefreshInformationValues();
			if (item.ItemCount == 0)
			{
				this.ExecuteRemoveZeroCounts();
			}
			this.IsRefreshed = true;
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x00031D50 File Offset: 0x0002FF50
		private void OnItemFocus(SPItemVM item)
		{
			this.CurrentFocusedItem = item;
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x00031D59 File Offset: 0x0002FF59
		private void ProcessItemSelect(ItemVM item)
		{
			this.ExecuteRemoveZeroCounts();
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x00031D64 File Offset: 0x0002FF64
		private void RefreshTransactionCost(int transactionCount = 1)
		{
			if (this._selectedItem != null && this.IsTrading)
			{
				int maxIndividualPrice;
				int itemTotalPrice = this._inventoryLogic.GetItemTotalPrice(this._selectedItem.ItemRosterElement, transactionCount, out maxIndividualPrice, this._selectedItem.InventorySide == InventoryLogic.InventorySide.OtherInventory);
				this.ItemMenu.SetTransactionCost(itemTotalPrice, maxIndividualPrice);
			}
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x00031DB8 File Offset: 0x0002FFB8
		public void RefreshComparedItem()
		{
			this._lastComparedItemIndex++;
			if (this._lastComparedItemIndex > this._comparedItemList.Count - 1)
			{
				this._lastComparedItemIndex = 0;
			}
			if (!this._comparedItemList.IsEmpty<ItemVM>() && this._selectedItem != null && this._comparedItemList[this._lastComparedItemIndex] != null)
			{
				this.ItemMenu.SetItem(this._selectedItem, this._comparedItemList[this._lastComparedItemIndex], this._currentCharacter, 0);
			}
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x00031E40 File Offset: 0x00030040
		private void AfterReset(InventoryLogic itemRoster, bool fromCancel)
		{
			this._inventoryLogic = itemRoster;
			if (!fromCancel)
			{
				switch (this.ActiveFilterIndex)
				{
				case 1:
					this._inventoryLogic.MerchantItemType = InventoryManager.InventoryCategoryType.Weapon;
					break;
				case 2:
					this._inventoryLogic.MerchantItemType = InventoryManager.InventoryCategoryType.Shield;
					break;
				case 3:
					this._inventoryLogic.MerchantItemType = InventoryManager.InventoryCategoryType.Armors;
					break;
				case 4:
					this._inventoryLogic.MerchantItemType = InventoryManager.InventoryCategoryType.HorseCategory;
					break;
				case 5:
					this._inventoryLogic.MerchantItemType = InventoryManager.InventoryCategoryType.Goods;
					break;
				default:
					this._inventoryLogic.MerchantItemType = InventoryManager.InventoryCategoryType.All;
					break;
				}
				this.InitializeInventory();
				this.PlayerInventorySortController = new SPInventorySortControllerVM(ref this._rightItemListVM);
				this.OtherInventorySortController = new SPInventorySortControllerVM(ref this._leftItemListVM);
				this.PlayerInventorySortController.SortByDefaultState();
				this.OtherInventorySortController.SortByDefaultState();
				Tuple<int, int> tuple = this._viewDataTracker.InventoryGetSortPreference((int)this._usageType);
				if (tuple != null)
				{
					this.PlayerInventorySortController.SortByOption((SPInventorySortControllerVM.InventoryItemSortOption)tuple.Item1, (SPInventorySortControllerVM.InventoryItemSortState)tuple.Item2);
				}
				this.UpdateRightCharacter();
				this.UpdateLeftCharacter();
				this.RightInventoryOwnerName = PartyBase.MainParty.Name.ToString();
				this.RightInventoryOwnerGold = Hero.MainHero.Gold;
			}
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x00031F6C File Offset: 0x0003016C
		private void OnTotalAmountChange(int newTotalAmount)
		{
			MBTextManager.SetTextVariable("PAY_OR_GET", (this._inventoryLogic.TotalAmount < 0) ? 1 : 0);
			int f = MathF.Min(-this._inventoryLogic.TotalAmount, this._inventoryLogic.InventoryListener.GetGold());
			MBTextManager.SetTextVariable("TRADE_AMOUNT", MathF.Abs(f));
			this.TradeLbl = ((this._inventoryLogic.TotalAmount == 0) ? "" : GameTexts.FindText("str_inventory_trade_label", null).ToString());
			this.RightInventoryOwnerGold = Hero.MainHero.Gold - this._inventoryLogic.TotalAmount;
			InventoryListener inventoryListener = this._inventoryLogic.InventoryListener;
			this.LeftInventoryOwnerGold = ((((inventoryListener != null) ? new int?(inventoryListener.GetGold()) : null) + this._inventoryLogic.TotalAmount) ?? 0);
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x0003207C File Offset: 0x0003027C
		private void OnDonationXpChange()
		{
			int num = (int)this._inventoryLogic.XpGainFromDonations;
			bool isDonationXpGainExceedsMax = false;
			if (num > this._donationMaxShareableXp)
			{
				num = this._donationMaxShareableXp;
				isDonationXpGainExceedsMax = true;
			}
			this.IsDonationXpGainExceedsMax = isDonationXpGainExceedsMax;
			this.HasGainedExperience = (num > 0);
			MBTextManager.SetTextVariable("XP_AMOUNT", num);
			this.ExperienceLbl = ((num == 0) ? "" : GameTexts.FindText("str_inventory_donation_label", null).ToString());
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x000320E8 File Offset: 0x000302E8
		private void AfterTransfer(InventoryLogic inventoryLogic, List<TransferCommandResult> results)
		{
			this._isCharacterEquipmentDirty = false;
			List<SPItemVM> list = new List<SPItemVM>();
			HashSet<ItemCategory> hashSet = new HashSet<ItemCategory>();
			for (int num = 0; num != results.Count; num++)
			{
				TransferCommandResult transferCommandResult = results[num];
				if (transferCommandResult.ResultSide == InventoryLogic.InventorySide.OtherInventory || transferCommandResult.ResultSide == InventoryLogic.InventorySide.PlayerInventory)
				{
					if (transferCommandResult.ResultSide == InventoryLogic.InventorySide.PlayerInventory && transferCommandResult.EffectedItemRosterElement.EquipmentElement.Item != null && !transferCommandResult.EffectedItemRosterElement.EquipmentElement.Item.IsMountable && !transferCommandResult.EffectedItemRosterElement.EquipmentElement.Item.IsAnimal)
					{
						this._equipmentCount += (float)transferCommandResult.EffectedNumber * transferCommandResult.EffectedItemRosterElement.EquipmentElement.GetEquipmentElementWeight();
					}
					bool flag = false;
					MBBindingList<SPItemVM> mbbindingList = (transferCommandResult.ResultSide == InventoryLogic.InventorySide.OtherInventory) ? this.LeftItemListVM : this.RightItemListVM;
					for (int i = 0; i < mbbindingList.Count; i++)
					{
						SPItemVM spitemVM = mbbindingList[i];
						if (spitemVM != null && spitemVM.ItemRosterElement.EquipmentElement.IsEqualTo(transferCommandResult.EffectedItemRosterElement.EquipmentElement))
						{
							spitemVM.ItemRosterElement.Amount = transferCommandResult.FinalNumber;
							spitemVM.ItemCount = transferCommandResult.FinalNumber;
							spitemVM.ItemCost = this._inventoryLogic.GetItemPrice(spitemVM.ItemRosterElement.EquipmentElement, transferCommandResult.ResultSide == InventoryLogic.InventorySide.OtherInventory);
							list.Add(spitemVM);
							if (!hashSet.Contains(spitemVM.ItemRosterElement.EquipmentElement.Item.GetItemCategory()))
							{
								hashSet.Add(spitemVM.ItemRosterElement.EquipmentElement.Item.GetItemCategory());
							}
							flag = true;
							break;
						}
					}
					if (!flag && transferCommandResult.EffectedNumber > 0 && this._inventoryLogic != null)
					{
						SPItemVM spitemVM2;
						if (transferCommandResult.ResultSide == InventoryLogic.InventorySide.OtherInventory)
						{
							spitemVM2 = new SPItemVM(this._inventoryLogic, this.MainCharacter.IsFemale, this.CanCharacterUseItemBasedOnSkills(transferCommandResult.EffectedItemRosterElement), this._usageType, transferCommandResult.EffectedItemRosterElement, InventoryLogic.InventorySide.OtherInventory, this._fiveStackShortcutkeyText, this._entireStackShortcutkeyText, this._inventoryLogic.GetCostOfItemRosterElement(transferCommandResult.EffectedItemRosterElement, transferCommandResult.ResultSide), null);
						}
						else
						{
							spitemVM2 = new SPItemVM(this._inventoryLogic, this.MainCharacter.IsFemale, this.CanCharacterUseItemBasedOnSkills(transferCommandResult.EffectedItemRosterElement), this._usageType, transferCommandResult.EffectedItemRosterElement, InventoryLogic.InventorySide.PlayerInventory, this._fiveStackShortcutkeyText, this._entireStackShortcutkeyText, this._inventoryLogic.GetCostOfItemRosterElement(transferCommandResult.EffectedItemRosterElement, transferCommandResult.ResultSide), null);
						}
						this.UpdateFilteredStatusOfItem(spitemVM2);
						spitemVM2.ItemCount = transferCommandResult.FinalNumber;
						spitemVM2.IsLocked = (spitemVM2.InventorySide == InventoryLogic.InventorySide.PlayerInventory && this._lockedItemIDs.Contains(spitemVM2.StringId));
						spitemVM2.IsNew = true;
						mbbindingList.Add(spitemVM2);
					}
				}
				else if (transferCommandResult.ResultSide == InventoryLogic.InventorySide.Equipment)
				{
					SPItemVM spitemVM3 = null;
					if (transferCommandResult.FinalNumber > 0)
					{
						spitemVM3 = new SPItemVM(this._inventoryLogic, this.MainCharacter.IsFemale, this.CanCharacterUseItemBasedOnSkills(transferCommandResult.EffectedItemRosterElement), this._usageType, transferCommandResult.EffectedItemRosterElement, InventoryLogic.InventorySide.Equipment, this._fiveStackShortcutkeyText, this._entireStackShortcutkeyText, this._inventoryLogic.GetCostOfItemRosterElement(transferCommandResult.EffectedItemRosterElement, transferCommandResult.ResultSide), new EquipmentIndex?(transferCommandResult.EffectedEquipmentIndex));
						spitemVM3.IsNew = true;
					}
					this.UpdateEquipment(transferCommandResult.TransferEquipment, spitemVM3, transferCommandResult.EffectedEquipmentIndex);
					this._isCharacterEquipmentDirty = true;
				}
			}
			SPItemVM selectedItem = this._selectedItem;
			if (selectedItem != null && selectedItem.ItemCount > 1)
			{
				this.ProcessItemTooltip(this._selectedItem);
				this._selectedItem.UpdateCanBeSlaughtered();
			}
			this.CheckEquipAfterTransferStack();
			if (!this.ActiveEquipment[EquipmentIndex.HorseHarness].IsEmpty && this.ActiveEquipment[EquipmentIndex.ArmorItemEndSlot].IsEmpty)
			{
				this.UnequipEquipment(this.CharacterMountArmorSlot);
			}
			if (!this.ActiveEquipment[EquipmentIndex.ArmorItemEndSlot].IsEmpty && !this.ActiveEquipment[EquipmentIndex.HorseHarness].IsEmpty && this.ActiveEquipment[EquipmentIndex.ArmorItemEndSlot].Item.HorseComponent.Monster.FamilyType != this.ActiveEquipment[EquipmentIndex.HorseHarness].Item.ArmorComponent.FamilyType)
			{
				this.UnequipEquipment(this.CharacterMountArmorSlot);
			}
			foreach (SPItemVM spitemVM4 in list)
			{
				spitemVM4.UpdateTradeData(true);
				spitemVM4.UpdateCanBeSlaughtered();
			}
			this.UpdateCostOfItemsInCategory(hashSet);
			if (PartyBase.MainParty.IsMobile)
			{
				PartyBase.MainParty.MobileParty.MemberRoster.UpdateVersion();
				PartyBase.MainParty.MobileParty.PrisonRoster.UpdateVersion();
			}
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x0003261C File Offset: 0x0003081C
		private void UpdateCostOfItemsInCategory(HashSet<ItemCategory> categories)
		{
			foreach (SPItemVM spitemVM in this.LeftItemListVM)
			{
				if (categories.Contains(spitemVM.ItemRosterElement.EquipmentElement.Item.GetItemCategory()))
				{
					spitemVM.ItemCost = this._inventoryLogic.GetCostOfItemRosterElement(spitemVM.ItemRosterElement, InventoryLogic.InventorySide.OtherInventory);
				}
			}
			foreach (SPItemVM spitemVM2 in this.RightItemListVM)
			{
				if (categories.Contains(spitemVM2.ItemRosterElement.EquipmentElement.Item.GetItemCategory()))
				{
					spitemVM2.ItemCost = this._inventoryLogic.GetCostOfItemRosterElement(spitemVM2.ItemRosterElement, InventoryLogic.InventorySide.PlayerInventory);
				}
			}
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x00032708 File Offset: 0x00030908
		private void CheckEquipAfterTransferStack()
		{
			while (this._equipAfterTransferStack.Count > 0)
			{
				SPItemVM spitemVM = new SPItemVM();
				spitemVM.RefreshWith(this._equipAfterTransferStack.Pop(), InventoryLogic.InventorySide.PlayerInventory);
				this.EquipEquipment(spitemVM);
			}
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x00032744 File Offset: 0x00030944
		private void RefreshInformationValues()
		{
			TextObject textObject = GameTexts.FindText("str_LEFT_over_RIGHT", null);
			int inventoryCapacity = PartyBase.MainParty.InventoryCapacity;
			int num = MathF.Ceiling(this._equipmentCount);
			textObject.SetTextVariable("LEFT", num.ToString());
			textObject.SetTextVariable("RIGHT", inventoryCapacity.ToString());
			this.PlayerEquipmentCountText = textObject.ToString();
			this.PlayerEquipmentCountWarned = (num > inventoryCapacity);
			if (this.OtherSideHasCapacity)
			{
				int num2 = MathF.Ceiling(this.LeftItemListVM.Sum((SPItemVM x) => x.ItemRosterElement.GetRosterElementWeight()));
				int capacity = this._inventoryLogic.OtherSideCapacityData.GetCapacity();
				textObject.SetTextVariable("LEFT", num2);
				textObject.SetTextVariable("RIGHT", capacity);
				this.OtherEquipmentCountText = textObject.ToString();
				this.OtherEquipmentCountWarned = (num2 > capacity);
			}
			this.NoSaddleText = new TextObject("{=QSPrSsHv}No Saddle!", null).ToString();
			this.NoSaddleHint = new HintViewModel(new TextObject("{=VzCoqt8D}No sadle equipped. -10% penalty to mounted speed and maneuver.", null), null);
			SPItemVM characterMountSlot = this.CharacterMountSlot;
			bool noSaddleWarned;
			if (characterMountSlot != null && !characterMountSlot.ItemRosterElement.IsEmpty)
			{
				SPItemVM characterMountArmorSlot = this.CharacterMountArmorSlot;
				noSaddleWarned = (characterMountArmorSlot != null && characterMountArmorSlot.ItemRosterElement.IsEmpty);
			}
			else
			{
				noSaddleWarned = false;
			}
			this.NoSaddleWarned = noSaddleWarned;
			if (Campaign.Current.GameMode == CampaignGameMode.Campaign)
			{
				this.EquipmentMaxCountHint = new BasicTooltipViewModel(() => CampaignUIHelper.GetPartyInventoryCapacityTooltip(MobileParty.MainParty));
			}
			if (this._isCharacterEquipmentDirty)
			{
				this.MainCharacter.SetEquipment(this.ActiveEquipment);
				this.UpdateCharacterArmorValues();
				this.RefreshCharacterTotalWeight();
			}
			this._isCharacterEquipmentDirty = false;
			this.UpdateIsDoneDisabled();
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x00032900 File Offset: 0x00030B00
		public bool IsItemEquipmentPossible(SPItemVM itemVM)
		{
			if (itemVM == null)
			{
				return false;
			}
			if (this.TargetEquipmentType == EquipmentIndex.None)
			{
				this.TargetEquipmentType = itemVM.GetItemTypeWithItemObject();
				if (this.TargetEquipmentType == EquipmentIndex.None)
				{
					return false;
				}
				if (this.TargetEquipmentType == EquipmentIndex.WeaponItemBeginSlot)
				{
					EquipmentIndex targetEquipmentType = EquipmentIndex.WeaponItemBeginSlot;
					bool flag = false;
					bool flag2 = false;
					SPItemVM[] array = new SPItemVM[]
					{
						this.CharacterWeapon1Slot,
						this.CharacterWeapon2Slot,
						this.CharacterWeapon3Slot,
						this.CharacterWeapon4Slot
					};
					for (int i = 0; i < array.Length; i++)
					{
						if (string.IsNullOrEmpty(array[i].StringId))
						{
							flag = true;
							targetEquipmentType = EquipmentIndex.WeaponItemBeginSlot + i;
							break;
						}
						if (array[i].ItemRosterElement.EquipmentElement.Item.Type == itemVM.ItemRosterElement.EquipmentElement.Item.Type)
						{
							flag2 = true;
							targetEquipmentType = EquipmentIndex.WeaponItemBeginSlot + i;
							break;
						}
					}
					if (flag || flag2)
					{
						this.TargetEquipmentType = targetEquipmentType;
					}
					else
					{
						this.TargetEquipmentType = EquipmentIndex.WeaponItemBeginSlot;
					}
				}
			}
			else if (itemVM.ItemType != this.TargetEquipmentType && (this.TargetEquipmentType < EquipmentIndex.WeaponItemBeginSlot || this.TargetEquipmentType > EquipmentIndex.Weapon3 || itemVM.ItemType < EquipmentIndex.WeaponItemBeginSlot || itemVM.ItemType > EquipmentIndex.Weapon3))
			{
				return false;
			}
			if (!this.CanCharacterUseItemBasedOnSkills(itemVM.ItemRosterElement))
			{
				TextObject textObject = new TextObject("{=rgqA29b8}You don't have enough {SKILL_NAME} skill to equip this item", null);
				textObject.SetTextVariable("SKILL_NAME", itemVM.ItemRosterElement.EquipmentElement.Item.RelevantSkill.Name);
				MBInformationManager.AddQuickInformation(textObject, 0, null, "");
				return false;
			}
			if (!this.CanCharacterUserItemBasedOnUsability(itemVM.ItemRosterElement))
			{
				TextObject textObject2 = new TextObject("{=ITKb4cKv}{ITEM_NAME} is not equippable.", null);
				textObject2.SetTextVariable("ITEM_NAME", itemVM.ItemRosterElement.EquipmentElement.GetModifiedItemName());
				MBInformationManager.AddQuickInformation(textObject2, 0, null, "");
				return false;
			}
			if (!Equipment.IsItemFitsToSlot((EquipmentIndex)this.TargetEquipmentIndex, itemVM.ItemRosterElement.EquipmentElement.Item))
			{
				TextObject textObject3 = new TextObject("{=Omjlnsk3}{ITEM_NAME} cannot be equipped on this slot.", null);
				textObject3.SetTextVariable("ITEM_NAME", itemVM.ItemRosterElement.EquipmentElement.GetModifiedItemName());
				MBInformationManager.AddQuickInformation(textObject3, 0, null, "");
				return false;
			}
			if (this.TargetEquipmentType == EquipmentIndex.HorseHarness)
			{
				if (string.IsNullOrEmpty(this.CharacterMountSlot.StringId))
				{
					return false;
				}
				if (!this.ActiveEquipment[EquipmentIndex.ArmorItemEndSlot].IsEmpty && this.ActiveEquipment[EquipmentIndex.ArmorItemEndSlot].Item.HorseComponent.Monster.FamilyType != itemVM.ItemRosterElement.EquipmentElement.Item.ArmorComponent.FamilyType)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x00032B98 File Offset: 0x00030D98
		private bool CanCharacterUserItemBasedOnUsability(ItemRosterElement itemRosterElement)
		{
			return !itemRosterElement.EquipmentElement.Item.HasHorseComponent || itemRosterElement.EquipmentElement.Item.HorseComponent.IsRideable;
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x00032BD9 File Offset: 0x00030DD9
		private bool CanCharacterUseItemBasedOnSkills(ItemRosterElement itemRosterElement)
		{
			return CharacterHelper.CanUseItemBasedOnSkill(this._currentCharacter, itemRosterElement.EquipmentElement);
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x00032BF0 File Offset: 0x00030DF0
		private void EquipEquipment(SPItemVM itemVM)
		{
			if (itemVM == null || string.IsNullOrEmpty(itemVM.StringId))
			{
				return;
			}
			SPItemVM spitemVM = new SPItemVM();
			spitemVM.RefreshWith(itemVM, InventoryLogic.InventorySide.Equipment);
			if (!this.IsItemEquipmentPossible(spitemVM))
			{
				return;
			}
			SPItemVM itemFromIndex = this.GetItemFromIndex(this.TargetEquipmentType);
			if (itemFromIndex != null && itemFromIndex.ItemRosterElement.EquipmentElement.Item == spitemVM.ItemRosterElement.EquipmentElement.Item && itemFromIndex.ItemRosterElement.EquipmentElement.ItemModifier == spitemVM.ItemRosterElement.EquipmentElement.ItemModifier)
			{
				return;
			}
			bool flag = itemFromIndex != null && itemFromIndex.ItemType != EquipmentIndex.None && itemVM.InventorySide == InventoryLogic.InventorySide.Equipment;
			if (!flag)
			{
				EquipmentIndex equipmentIndex = EquipmentIndex.None;
				if (itemVM.ItemRosterElement.EquipmentElement.Item.Type == ItemObject.ItemTypeEnum.Shield && itemVM.InventorySide != InventoryLogic.InventorySide.Equipment)
				{
					for (EquipmentIndex equipmentIndex2 = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex2 <= EquipmentIndex.NumAllWeaponSlots; equipmentIndex2++)
					{
						SPItemVM itemFromIndex2 = this.GetItemFromIndex(equipmentIndex2);
						bool flag2;
						if (itemFromIndex2 == null)
						{
							flag2 = false;
						}
						else
						{
							ItemObject item = itemFromIndex2.ItemRosterElement.EquipmentElement.Item;
							ItemObject.ItemTypeEnum? itemTypeEnum = (item != null) ? new ItemObject.ItemTypeEnum?(item.Type) : null;
							ItemObject.ItemTypeEnum itemTypeEnum2 = ItemObject.ItemTypeEnum.Shield;
							flag2 = (itemTypeEnum.GetValueOrDefault() == itemTypeEnum2 & itemTypeEnum != null);
						}
						if (flag2)
						{
							equipmentIndex = equipmentIndex2;
							break;
						}
					}
				}
				if (itemVM != null)
				{
					ItemObject item2 = itemVM.ItemRosterElement.EquipmentElement.Item;
					ItemObject.ItemTypeEnum? itemTypeEnum = (item2 != null) ? new ItemObject.ItemTypeEnum?(item2.Type) : null;
					ItemObject.ItemTypeEnum itemTypeEnum2 = ItemObject.ItemTypeEnum.Shield;
					if ((itemTypeEnum.GetValueOrDefault() == itemTypeEnum2 & itemTypeEnum != null) && equipmentIndex != EquipmentIndex.None)
					{
						this.TargetEquipmentType = equipmentIndex;
					}
				}
			}
			List<TransferCommand> list = new List<TransferCommand>();
			TransferCommand item3 = TransferCommand.Transfer(1, itemVM.InventorySide, InventoryLogic.InventorySide.Equipment, spitemVM.ItemRosterElement, spitemVM.ItemType, this.TargetEquipmentType, this._currentCharacter, !this.IsInWarSet);
			list.Add(item3);
			if (flag)
			{
				TransferCommand item4 = TransferCommand.Transfer(1, InventoryLogic.InventorySide.PlayerInventory, InventoryLogic.InventorySide.Equipment, itemFromIndex.ItemRosterElement, EquipmentIndex.None, spitemVM.ItemType, this._currentCharacter, !this.IsInWarSet);
				list.Add(item4);
			}
			this._inventoryLogic.AddTransferCommands(list);
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x00032E14 File Offset: 0x00031014
		private void UnequipEquipment(SPItemVM itemVM)
		{
			if (itemVM == null || string.IsNullOrEmpty(itemVM.StringId))
			{
				return;
			}
			TransferCommand command = TransferCommand.Transfer(1, InventoryLogic.InventorySide.Equipment, InventoryLogic.InventorySide.PlayerInventory, itemVM.ItemRosterElement, itemVM.ItemType, itemVM.ItemType, this._currentCharacter, !this.IsInWarSet);
			this._inventoryLogic.AddTransferCommand(command);
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x00032E68 File Offset: 0x00031068
		private void UpdateEquipment(Equipment equipment, SPItemVM itemVM, EquipmentIndex itemType)
		{
			if (this.ActiveEquipment == equipment)
			{
				this.RefreshEquipment(itemVM, itemType);
			}
			equipment[itemType] = ((itemVM == null) ? default(EquipmentElement) : itemVM.ItemRosterElement.EquipmentElement);
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x00032EA8 File Offset: 0x000310A8
		private void UnequipEquipmentWithEquipmentIndex(EquipmentIndex slotType)
		{
			switch (slotType)
			{
			case EquipmentIndex.None:
				return;
			case EquipmentIndex.WeaponItemBeginSlot:
				this.UnequipEquipment(this.CharacterWeapon1Slot);
				return;
			case EquipmentIndex.Weapon1:
				this.UnequipEquipment(this.CharacterWeapon2Slot);
				return;
			case EquipmentIndex.Weapon2:
				this.UnequipEquipment(this.CharacterWeapon3Slot);
				return;
			case EquipmentIndex.Weapon3:
				this.UnequipEquipment(this.CharacterWeapon4Slot);
				return;
			case EquipmentIndex.ExtraWeaponSlot:
				this.UnequipEquipment(this.CharacterBannerSlot);
				return;
			case EquipmentIndex.NumAllWeaponSlots:
				this.UnequipEquipment(this.CharacterHelmSlot);
				return;
			case EquipmentIndex.Body:
				this.UnequipEquipment(this.CharacterTorsoSlot);
				return;
			case EquipmentIndex.Leg:
				this.UnequipEquipment(this.CharacterBootSlot);
				return;
			case EquipmentIndex.Gloves:
				this.UnequipEquipment(this.CharacterGloveSlot);
				return;
			case EquipmentIndex.Cape:
				this.UnequipEquipment(this.CharacterCloakSlot);
				return;
			case EquipmentIndex.ArmorItemEndSlot:
				this.UnequipEquipment(this.CharacterMountSlot);
				if (!string.IsNullOrEmpty(this.CharacterMountArmorSlot.StringId))
				{
					this.UnequipEquipment(this.CharacterMountArmorSlot);
				}
				return;
			case EquipmentIndex.HorseHarness:
				this.UnequipEquipment(this.CharacterMountArmorSlot);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x00032FAC File Offset: 0x000311AC
		protected void RefreshEquipment(SPItemVM itemVM, EquipmentIndex itemType)
		{
			switch (itemType)
			{
			case EquipmentIndex.None:
				return;
			case EquipmentIndex.WeaponItemBeginSlot:
				this.CharacterWeapon1Slot.RefreshWith(itemVM, InventoryLogic.InventorySide.Equipment);
				return;
			case EquipmentIndex.Weapon1:
				this.CharacterWeapon2Slot.RefreshWith(itemVM, InventoryLogic.InventorySide.Equipment);
				return;
			case EquipmentIndex.Weapon2:
				this.CharacterWeapon3Slot.RefreshWith(itemVM, InventoryLogic.InventorySide.Equipment);
				return;
			case EquipmentIndex.Weapon3:
				this.CharacterWeapon4Slot.RefreshWith(itemVM, InventoryLogic.InventorySide.Equipment);
				return;
			case EquipmentIndex.ExtraWeaponSlot:
				this.CharacterBannerSlot.RefreshWith(itemVM, InventoryLogic.InventorySide.Equipment);
				return;
			case EquipmentIndex.NumAllWeaponSlots:
				this.CharacterHelmSlot.RefreshWith(itemVM, InventoryLogic.InventorySide.Equipment);
				return;
			case EquipmentIndex.Body:
				this.CharacterTorsoSlot.RefreshWith(itemVM, InventoryLogic.InventorySide.Equipment);
				return;
			case EquipmentIndex.Leg:
				this.CharacterBootSlot.RefreshWith(itemVM, InventoryLogic.InventorySide.Equipment);
				return;
			case EquipmentIndex.Gloves:
				this.CharacterGloveSlot.RefreshWith(itemVM, InventoryLogic.InventorySide.Equipment);
				return;
			case EquipmentIndex.Cape:
				this.CharacterCloakSlot.RefreshWith(itemVM, InventoryLogic.InventorySide.Equipment);
				return;
			case EquipmentIndex.ArmorItemEndSlot:
				this.CharacterMountSlot.RefreshWith(itemVM, InventoryLogic.InventorySide.Equipment);
				return;
			case EquipmentIndex.HorseHarness:
				this.CharacterMountArmorSlot.RefreshWith(itemVM, InventoryLogic.InventorySide.Equipment);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x000330A0 File Offset: 0x000312A0
		private bool UpdateCurrentCharacterIfPossible(int characterIndex, bool isFromRightSide)
		{
			CharacterObject character = (isFromRightSide ? this._rightTroopRoster : this._leftTroopRoster).GetElementCopyAtIndex(characterIndex).Character;
			if (character.IsHero)
			{
				if (!character.HeroObject.CanHeroEquipmentBeChanged())
				{
					Hero mainHero = Hero.MainHero;
					bool flag;
					if (mainHero == null)
					{
						flag = false;
					}
					else
					{
						Clan clan = mainHero.Clan;
						bool? flag2 = (clan != null) ? new bool?(clan.Lords.Contains(character.HeroObject)) : null;
						bool flag3 = true;
						flag = (flag2.GetValueOrDefault() == flag3 & flag2 != null);
					}
					if (!flag)
					{
						return false;
					}
				}
				this._currentInventoryCharacterIndex = characterIndex;
				this._currentCharacter = character;
				this.MainCharacter.FillFrom(this._currentCharacter.HeroObject, -1, false, false);
				if (this._currentCharacter.IsHero)
				{
					CharacterViewModel mainCharacter = this.MainCharacter;
					IFaction mapFaction = this._currentCharacter.HeroObject.MapFaction;
					mainCharacter.ArmorColor1 = ((mapFaction != null) ? mapFaction.Color : 0U);
					CharacterViewModel mainCharacter2 = this.MainCharacter;
					IFaction mapFaction2 = this._currentCharacter.HeroObject.MapFaction;
					mainCharacter2.ArmorColor2 = ((mapFaction2 != null) ? mapFaction2.Color2 : 0U);
				}
				this.UpdateRightCharacter();
				this.RefreshInformationValues();
				return true;
			}
			return false;
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x000331C4 File Offset: 0x000313C4
		private bool DoesCompanionExist()
		{
			for (int i = 1; i < this._rightTroopRoster.Count; i++)
			{
				CharacterObject character = this._rightTroopRoster.GetElementCopyAtIndex(i).Character;
				if (character.IsHero && !character.HeroObject.CanHeroEquipmentBeChanged() && character.HeroObject != Hero.MainHero)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x00033220 File Offset: 0x00031420
		private void UpdateLeftCharacter()
		{
			this.IsTradingWithSettlement = false;
			if (this._inventoryLogic.LeftRosterName != null)
			{
				this.LeftInventoryOwnerName = this._inventoryLogic.LeftRosterName.ToString();
				Settlement settlement = this._currentCharacter.HeroObject.CurrentSettlement;
				if (settlement != null && InventoryManager.Instance.CurrentMode == InventoryMode.Warehouse)
				{
					this.IsTradingWithSettlement = true;
					this.ProductionTooltip = new BasicTooltipViewModel(() => CampaignUIHelper.GetSettlementProductionTooltip(settlement));
					return;
				}
			}
			else
			{
				Settlement settlement = this._currentCharacter.HeroObject.CurrentSettlement;
				if (settlement != null)
				{
					this.LeftInventoryOwnerName = settlement.Name.ToString();
					this.ProductionTooltip = new BasicTooltipViewModel(() => CampaignUIHelper.GetSettlementProductionTooltip(settlement));
					this.IsTradingWithSettlement = !settlement.IsHideout;
					if (this._inventoryLogic.InventoryListener != null)
					{
						this.LeftInventoryOwnerGold = this._inventoryLogic.InventoryListener.GetGold();
						return;
					}
				}
				else
				{
					PartyBase oppositePartyFromListener = this._inventoryLogic.OppositePartyFromListener;
					MobileParty mobileParty = (oppositePartyFromListener != null) ? oppositePartyFromListener.MobileParty : null;
					if (mobileParty != null && (mobileParty.IsCaravan || mobileParty.IsVillager))
					{
						this.LeftInventoryOwnerName = mobileParty.Name.ToString();
						InventoryListener inventoryListener = this._inventoryLogic.InventoryListener;
						this.LeftInventoryOwnerGold = ((inventoryListener != null) ? inventoryListener.GetGold() : 0);
						return;
					}
					this.LeftInventoryOwnerName = GameTexts.FindText("str_loot", null).ToString();
				}
			}
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x000333AC File Offset: 0x000315AC
		private void UpdateRightCharacter()
		{
			this.UpdateCharacterEquipment();
			this.UpdateCharacterArmorValues();
			this.RefreshCharacterTotalWeight();
			this.RefreshCharacterCanUseItem();
			this.CurrentCharacterName = this._currentCharacter.Name.ToString();
			this.RightInventoryOwnerGold = Hero.MainHero.Gold - this._inventoryLogic.TotalAmount;
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x00033404 File Offset: 0x00031604
		private SPItemVM InitializeCharacterEquipmentSlot(ItemRosterElement itemRosterElement, EquipmentIndex equipmentIndex)
		{
			SPItemVM spitemVM;
			if (!itemRosterElement.IsEmpty)
			{
				spitemVM = new SPItemVM(this._inventoryLogic, this.MainCharacter.IsFemale, this.CanCharacterUseItemBasedOnSkills(itemRosterElement), this._usageType, itemRosterElement, InventoryLogic.InventorySide.Equipment, this._fiveStackShortcutkeyText, this._entireStackShortcutkeyText, this._inventoryLogic.GetCostOfItemRosterElement(itemRosterElement, InventoryLogic.InventorySide.Equipment), new EquipmentIndex?(equipmentIndex));
			}
			else
			{
				spitemVM = new SPItemVM();
				spitemVM.RefreshWith(null, InventoryLogic.InventorySide.Equipment);
			}
			return spitemVM;
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x00033474 File Offset: 0x00031674
		private void UpdateCharacterEquipment()
		{
			this.CharacterHelmSlot = this.InitializeCharacterEquipmentSlot(new ItemRosterElement(this.ActiveEquipment.GetEquipmentFromSlot(EquipmentIndex.NumAllWeaponSlots), 1), EquipmentIndex.NumAllWeaponSlots);
			this.CharacterCloakSlot = this.InitializeCharacterEquipmentSlot(new ItemRosterElement(this.ActiveEquipment.GetEquipmentFromSlot(EquipmentIndex.Cape), 1), EquipmentIndex.Cape);
			this.CharacterTorsoSlot = this.InitializeCharacterEquipmentSlot(new ItemRosterElement(this.ActiveEquipment.GetEquipmentFromSlot(EquipmentIndex.Body), 1), EquipmentIndex.Body);
			this.CharacterGloveSlot = this.InitializeCharacterEquipmentSlot(new ItemRosterElement(this.ActiveEquipment.GetEquipmentFromSlot(EquipmentIndex.Gloves), 1), EquipmentIndex.Gloves);
			this.CharacterBootSlot = this.InitializeCharacterEquipmentSlot(new ItemRosterElement(this.ActiveEquipment.GetEquipmentFromSlot(EquipmentIndex.Leg), 1), EquipmentIndex.Leg);
			this.CharacterMountSlot = this.InitializeCharacterEquipmentSlot(new ItemRosterElement(this.ActiveEquipment.GetEquipmentFromSlot(EquipmentIndex.ArmorItemEndSlot), 1), EquipmentIndex.ArmorItemEndSlot);
			this.CharacterMountArmorSlot = this.InitializeCharacterEquipmentSlot(new ItemRosterElement(this.ActiveEquipment.GetEquipmentFromSlot(EquipmentIndex.HorseHarness), 1), EquipmentIndex.HorseHarness);
			this.CharacterWeapon1Slot = this.InitializeCharacterEquipmentSlot(new ItemRosterElement(this.ActiveEquipment.GetEquipmentFromSlot(EquipmentIndex.WeaponItemBeginSlot), 1), EquipmentIndex.WeaponItemBeginSlot);
			this.CharacterWeapon2Slot = this.InitializeCharacterEquipmentSlot(new ItemRosterElement(this.ActiveEquipment.GetEquipmentFromSlot(EquipmentIndex.Weapon1), 1), EquipmentIndex.Weapon1);
			this.CharacterWeapon3Slot = this.InitializeCharacterEquipmentSlot(new ItemRosterElement(this.ActiveEquipment.GetEquipmentFromSlot(EquipmentIndex.Weapon2), 1), EquipmentIndex.Weapon2);
			this.CharacterWeapon4Slot = this.InitializeCharacterEquipmentSlot(new ItemRosterElement(this.ActiveEquipment.GetEquipmentFromSlot(EquipmentIndex.Weapon3), 1), EquipmentIndex.Weapon3);
			this.CharacterBannerSlot = this.InitializeCharacterEquipmentSlot(new ItemRosterElement(this.ActiveEquipment.GetEquipmentFromSlot(EquipmentIndex.ExtraWeaponSlot), 1), EquipmentIndex.ExtraWeaponSlot);
			this.MainCharacter.SetEquipment(this.ActiveEquipment);
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x0003360C File Offset: 0x0003180C
		private void UpdateCharacterArmorValues()
		{
			this.CurrentCharacterArmArmor = this._currentCharacter.GetArmArmorSum(!this.IsInWarSet);
			this.CurrentCharacterBodyArmor = this._currentCharacter.GetBodyArmorSum(!this.IsInWarSet);
			this.CurrentCharacterHeadArmor = this._currentCharacter.GetHeadArmorSum(!this.IsInWarSet);
			this.CurrentCharacterLegArmor = this._currentCharacter.GetLegArmorSum(!this.IsInWarSet);
			this.CurrentCharacterHorseArmor = this._currentCharacter.GetHorseArmorSum(!this.IsInWarSet);
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x0003369C File Offset: 0x0003189C
		private void RefreshCharacterTotalWeight()
		{
			CharacterObject currentCharacter = this._currentCharacter;
			float num = (currentCharacter != null && currentCharacter.GetPerkValue(DefaultPerks.Athletics.FormFittingArmor)) ? (1f + DefaultPerks.Athletics.FormFittingArmor.PrimaryBonus) : 1f;
			this.CurrentCharacterTotalEncumbrance = MathF.Round(this.ActiveEquipment.GetTotalWeightOfWeapons() + this.ActiveEquipment.GetTotalWeightOfArmor(true) * num, 1).ToString("0.0");
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x00033710 File Offset: 0x00031910
		private void RefreshCharacterCanUseItem()
		{
			for (int i = 0; i < this.RightItemListVM.Count; i++)
			{
				this.RightItemListVM[i].CanCharacterUseItem = this.CanCharacterUseItemBasedOnSkills(this.RightItemListVM[i].ItemRosterElement);
			}
			for (int j = 0; j < this.LeftItemListVM.Count; j++)
			{
				this.LeftItemListVM[j].CanCharacterUseItem = this.CanCharacterUseItemBasedOnSkills(this.LeftItemListVM[j].ItemRosterElement);
			}
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x0003379C File Offset: 0x0003199C
		private void InitializeInventory()
		{
			this.IsRefreshed = false;
			switch (this._inventoryLogic.MerchantItemType)
			{
			case InventoryManager.InventoryCategoryType.Armors:
				this.ActiveFilterIndex = 3;
				break;
			case InventoryManager.InventoryCategoryType.Weapon:
				this.ActiveFilterIndex = 1;
				break;
			case InventoryManager.InventoryCategoryType.Shield:
				this.ActiveFilterIndex = 2;
				break;
			case InventoryManager.InventoryCategoryType.HorseCategory:
				this.ActiveFilterIndex = 4;
				break;
			case InventoryManager.InventoryCategoryType.Goods:
				this.ActiveFilterIndex = 5;
				break;
			default:
				this.ActiveFilterIndex = 0;
				break;
			}
			this._equipmentCount = 0f;
			this.RightItemListVM.Clear();
			this.LeftItemListVM.Clear();
			int num = MathF.Max(this._inventoryLogic.GetElementCountOnSide(InventoryLogic.InventorySide.PlayerInventory), this._inventoryLogic.GetElementCountOnSide(InventoryLogic.InventorySide.OtherInventory));
			ItemRosterElement[] array = (from i in this._inventoryLogic.GetElementsInRoster(InventoryLogic.InventorySide.PlayerInventory)
			orderby i.EquipmentElement.GetModifiedItemName().ToString()
			select i).ToArray<ItemRosterElement>();
			ItemRosterElement[] array2 = (from i in this._inventoryLogic.GetElementsInRoster(InventoryLogic.InventorySide.OtherInventory)
			orderby i.EquipmentElement.GetModifiedItemName().ToString()
			select i).ToArray<ItemRosterElement>();
			this._lockedItemIDs = this._viewDataTracker.GetInventoryLocks().ToList<string>();
			for (int j = 0; j < num; j++)
			{
				if (j < array.Length)
				{
					ItemRosterElement itemRosterElement = array[j];
					SPItemVM spitemVM = new SPItemVM(this._inventoryLogic, this.MainCharacter.IsFemale, this.CanCharacterUseItemBasedOnSkills(itemRosterElement), this._usageType, itemRosterElement, InventoryLogic.InventorySide.PlayerInventory, this._fiveStackShortcutkeyText, this._entireStackShortcutkeyText, this._inventoryLogic.GetCostOfItemRosterElement(itemRosterElement, InventoryLogic.InventorySide.PlayerInventory), null);
					this.UpdateFilteredStatusOfItem(spitemVM);
					spitemVM.IsLocked = (spitemVM.InventorySide == InventoryLogic.InventorySide.PlayerInventory && this.IsItemLocked(itemRosterElement));
					this.RightItemListVM.Add(spitemVM);
					if (!itemRosterElement.EquipmentElement.Item.IsMountable && !itemRosterElement.EquipmentElement.Item.IsAnimal)
					{
						this._equipmentCount += itemRosterElement.GetRosterElementWeight();
					}
				}
				if (j < array2.Length)
				{
					ItemRosterElement itemRosterElement2 = array2[j];
					SPItemVM spitemVM2 = new SPItemVM(this._inventoryLogic, this.MainCharacter.IsFemale, this.CanCharacterUseItemBasedOnSkills(itemRosterElement2), this._usageType, itemRosterElement2, InventoryLogic.InventorySide.OtherInventory, this._fiveStackShortcutkeyText, this._entireStackShortcutkeyText, this._inventoryLogic.GetCostOfItemRosterElement(itemRosterElement2, InventoryLogic.InventorySide.OtherInventory), null);
					this.UpdateFilteredStatusOfItem(spitemVM2);
					spitemVM2.IsLocked = (spitemVM2.InventorySide == InventoryLogic.InventorySide.PlayerInventory && this.IsItemLocked(itemRosterElement2));
					this.LeftItemListVM.Add(spitemVM2);
				}
			}
			this.RefreshInformationValues();
			this.IsRefreshed = true;
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x00033A58 File Offset: 0x00031C58
		private bool IsItemLocked(ItemRosterElement item)
		{
			string text = item.EquipmentElement.Item.StringId;
			if (item.EquipmentElement.ItemModifier != null)
			{
				text += item.EquipmentElement.ItemModifier.StringId;
			}
			return this._lockedItemIDs.Contains(text);
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x00033AB2 File Offset: 0x00031CB2
		public void CompareNextItem()
		{
			this.CycleBetweenWeaponSlots();
			this.RefreshComparedItem();
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x00033AC0 File Offset: 0x00031CC0
		private void BuyItem(SPItemVM item)
		{
			if (this.TargetEquipmentType != EquipmentIndex.None && item.ItemType != this.TargetEquipmentType && (this.TargetEquipmentType < EquipmentIndex.WeaponItemBeginSlot || this.TargetEquipmentType > EquipmentIndex.ExtraWeaponSlot || item.ItemType < EquipmentIndex.WeaponItemBeginSlot || item.ItemType > EquipmentIndex.ExtraWeaponSlot))
			{
				return;
			}
			if (this.TargetEquipmentType == EquipmentIndex.None)
			{
				this.TargetEquipmentType = item.ItemType;
				if (item.ItemType >= EquipmentIndex.WeaponItemBeginSlot && item.ItemType <= EquipmentIndex.ExtraWeaponSlot)
				{
					this.TargetEquipmentType = this.ActiveEquipment.GetWeaponPickUpSlotIndex(item.ItemRosterElement.EquipmentElement, false);
				}
			}
			int b = item.ItemCount;
			if (item.InventorySide == InventoryLogic.InventorySide.PlayerInventory)
			{
				ItemRosterElement? itemRosterElement = this._inventoryLogic.FindItemFromSide(InventoryLogic.InventorySide.OtherInventory, item.ItemRosterElement.EquipmentElement);
				if (itemRosterElement != null)
				{
					b = itemRosterElement.Value.Amount;
				}
			}
			TransferCommand command = TransferCommand.Transfer(MathF.Min(this.TransactionCount, b), InventoryLogic.InventorySide.OtherInventory, InventoryLogic.InventorySide.PlayerInventory, item.ItemRosterElement, item.ItemType, this.TargetEquipmentType, this._currentCharacter, !this.IsInWarSet);
			this._inventoryLogic.AddTransferCommand(command);
			if (this.EquipAfterBuy)
			{
				this._equipAfterTransferStack.Push(item);
			}
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x00033BE4 File Offset: 0x00031DE4
		private void SellItem(SPItemVM item)
		{
			InventoryLogic.InventorySide inventorySide = item.InventorySide;
			int b = item.ItemCount;
			if (inventorySide == InventoryLogic.InventorySide.OtherInventory)
			{
				inventorySide = InventoryLogic.InventorySide.PlayerInventory;
				ItemRosterElement? itemRosterElement = this._inventoryLogic.FindItemFromSide(InventoryLogic.InventorySide.PlayerInventory, item.ItemRosterElement.EquipmentElement);
				if (itemRosterElement != null)
				{
					b = itemRosterElement.Value.Amount;
				}
			}
			TransferCommand command = TransferCommand.Transfer(MathF.Min(this.TransactionCount, b), inventorySide, InventoryLogic.InventorySide.OtherInventory, item.ItemRosterElement, item.ItemType, this.TargetEquipmentType, this._currentCharacter, !this.IsInWarSet);
			this._inventoryLogic.AddTransferCommand(command);
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x00033C78 File Offset: 0x00031E78
		private void SlaughterItem(SPItemVM item)
		{
			int num = 1;
			if (this.IsFiveStackModifierActive)
			{
				num = MathF.Min(5, item.ItemCount);
			}
			else if (this.IsEntireStackModifierActive)
			{
				num = item.ItemCount;
			}
			for (int i = 0; i < num; i++)
			{
				this._inventoryLogic.SlaughterItem(item.ItemRosterElement);
			}
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x00033CCC File Offset: 0x00031ECC
		private void DonateItem(SPItemVM item)
		{
			if (this.IsFiveStackModifierActive)
			{
				int itemCount = item.ItemCount;
				for (int i = 0; i < MathF.Min(5, itemCount); i++)
				{
					this._inventoryLogic.DonateItem(item.ItemRosterElement);
				}
				return;
			}
			this._inventoryLogic.DonateItem(item.ItemRosterElement);
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x00033D20 File Offset: 0x00031F20
		private float GetCapacityBudget(MobileParty party, bool isBuy)
		{
			if (isBuy)
			{
				int? num = (party != null) ? new int?(party.InventoryCapacity) : null;
				float? num2 = ((num != null) ? new float?((float)num.GetValueOrDefault()) : null) - this._equipmentCount;
				if (num2 == null)
				{
					return 0f;
				}
				return num2.GetValueOrDefault();
			}
			else
			{
				if (this._inventoryLogic.OtherSideCapacityData != null)
				{
					return (float)this._inventoryLogic.OtherSideCapacityData.GetCapacity() - this.LeftItemListVM.Sum((SPItemVM x) => x.ItemRosterElement.GetRosterElementWeight());
				}
				return 0f;
			}
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x00033E04 File Offset: 0x00032004
		private void TransferAll(bool isBuy)
		{
			this.IsRefreshed = false;
			List<TransferCommand> list = new List<TransferCommand>(this.LeftItemListVM.Count);
			MBBindingList<SPItemVM> mbbindingList = isBuy ? this.LeftItemListVM : this.RightItemListVM;
			MobileParty mobileParty;
			if (!isBuy)
			{
				PartyBase oppositePartyFromListener = this._inventoryLogic.OppositePartyFromListener;
				mobileParty = ((oppositePartyFromListener != null) ? oppositePartyFromListener.MobileParty : null);
			}
			else
			{
				mobileParty = MobileParty.MainParty;
			}
			MobileParty party = mobileParty;
			float num = 0f;
			float capacityBudget = this.GetCapacityBudget(party, isBuy);
			SPItemVM spitemVM = mbbindingList.FirstOrDefault((SPItemVM x) => !x.IsFiltered && !x.IsLocked);
			float num2 = (spitemVM != null) ? spitemVM.ItemRosterElement.EquipmentElement.GetEquipmentElementWeight() : 0f;
			bool flag = capacityBudget <= num2;
			InventoryLogic.InventorySide fromSide = isBuy ? InventoryLogic.InventorySide.OtherInventory : InventoryLogic.InventorySide.PlayerInventory;
			InventoryLogic.InventorySide inventorySide = isBuy ? InventoryLogic.InventorySide.PlayerInventory : InventoryLogic.InventorySide.OtherInventory;
			List<SPItemVM> list2 = new List<SPItemVM>();
			bool flag2 = this._inventoryLogic.CanInventoryCapacityIncrease(inventorySide);
			for (int i = 0; i < mbbindingList.Count; i++)
			{
				SPItemVM spitemVM2 = mbbindingList[i];
				if (spitemVM2 != null && !spitemVM2.IsFiltered && spitemVM2 != null && !spitemVM2.IsLocked && spitemVM2 != null && spitemVM2.IsTransferable)
				{
					int num3 = spitemVM2.ItemRosterElement.Amount;
					if (!flag)
					{
						float equipmentElementWeight = spitemVM2.ItemRosterElement.EquipmentElement.GetEquipmentElementWeight();
						float num4 = num + equipmentElementWeight * (float)num3;
						if (flag2)
						{
							if (this._inventoryLogic.GetCanItemIncreaseInventoryCapacity(mbbindingList[i].ItemRosterElement.EquipmentElement.Item))
							{
								list2.Add(mbbindingList[i]);
								goto IL_29E;
							}
							if (num4 >= capacityBudget && list2.Count > 0)
							{
								List<TransferCommand> list3 = new List<TransferCommand>(list2.Count);
								for (int j = 0; j < list2.Count; j++)
								{
									SPItemVM spitemVM3 = list2[j];
									TransferCommand item = TransferCommand.Transfer(spitemVM3.ItemRosterElement.Amount, fromSide, inventorySide, spitemVM3.ItemRosterElement, EquipmentIndex.None, EquipmentIndex.None, this._currentCharacter, !this.IsInWarSet);
									list3.Add(item);
								}
								this._inventoryLogic.AddTransferCommands(list3);
								list3.Clear();
								list2.Clear();
								capacityBudget = this.GetCapacityBudget(party, isBuy);
							}
						}
						if (num3 > 0 && num4 > capacityBudget)
						{
							num3 = MBMath.ClampInt(num3, 0, MathF.Floor((capacityBudget - num) / equipmentElementWeight));
							i = mbbindingList.Count;
						}
						num += (float)num3 * equipmentElementWeight;
					}
					if (num3 > 0)
					{
						TransferCommand item2 = TransferCommand.Transfer(num3, fromSide, inventorySide, spitemVM2.ItemRosterElement, EquipmentIndex.None, EquipmentIndex.None, this._currentCharacter, !this.IsInWarSet);
						list.Add(item2);
					}
				}
				IL_29E:;
			}
			if (num <= capacityBudget)
			{
				foreach (SPItemVM spitemVM4 in list2)
				{
					TransferCommand item3 = TransferCommand.Transfer(spitemVM4.ItemRosterElement.Amount, fromSide, inventorySide, spitemVM4.ItemRosterElement, EquipmentIndex.None, EquipmentIndex.None, this._currentCharacter, !this.IsInWarSet);
					list.Add(item3);
				}
			}
			this._inventoryLogic.AddTransferCommands(list);
			this.RefreshInformationValues();
			this.ExecuteRemoveZeroCounts();
			this.IsRefreshed = true;
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x0003415C File Offset: 0x0003235C
		public void ExecuteBuyAllItems()
		{
			this.TransferAll(true);
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x00034165 File Offset: 0x00032365
		public void ExecuteSellAllItems()
		{
			this.TransferAll(false);
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x00034170 File Offset: 0x00032370
		public void ExecuteBuyItemTest()
		{
			this.TransactionCount = 1;
			this.EquipAfterBuy = false;
			int totalGold = Hero.MainHero.Gold;
			foreach (SPItemVM spitemVM in this.LeftItemListVM.Where(delegate(SPItemVM i)
			{
				ItemObject item = i.ItemRosterElement.EquipmentElement.Item;
				return item != null && item.IsFood && i.ItemCost <= totalGold;
			}))
			{
				if (spitemVM.ItemCost <= totalGold)
				{
					this.ProcessBuyItem(spitemVM, false);
					totalGold -= spitemVM.ItemCost;
				}
			}
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x00034214 File Offset: 0x00032414
		public void ExecuteResetTranstactions()
		{
			this._inventoryLogic.Reset(false);
			InformationManager.DisplayMessage(new InformationMessage(GameTexts.FindText("str_inventory_reset_message", null).ToString()));
			this.CurrentFocusedItem = null;
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x00034244 File Offset: 0x00032444
		public void ExecuteResetAndCompleteTranstactions()
		{
			if (InventoryManager.Instance.CurrentMode == InventoryMode.Loot)
			{
				InformationManager.ShowInquiry(new InquiryData("", GameTexts.FindText("str_leaving_loot_behind", null).ToString(), true, true, GameTexts.FindText("str_yes", null).ToString(), GameTexts.FindText("str_no", null).ToString(), delegate()
				{
					if (!this._isFinalized)
					{
						this._inventoryLogic.Reset(true);
						InventoryManager.Instance.CloseInventoryPresentation(true);
					}
				}, null, "", 0f, null, null, null), false, false);
				return;
			}
			this._inventoryLogic.Reset(true);
			InventoryManager.Instance.CloseInventoryPresentation(true);
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x000342D4 File Offset: 0x000324D4
		public void ExecuteCompleteTranstactions()
		{
			if (InventoryManager.Instance.CurrentMode == InventoryMode.Loot && !this._inventoryLogic.IsThereAnyChanges() && this._inventoryLogic.GetElementsInInitialRoster(InventoryLogic.InventorySide.OtherInventory).Any<ItemRosterElement>())
			{
				InformationManager.ShowInquiry(new InquiryData("", GameTexts.FindText("str_leaving_loot_behind", null).ToString(), true, true, GameTexts.FindText("str_yes", null).ToString(), GameTexts.FindText("str_no", null).ToString(), new Action(this.HandleDone), null, "", 0f, null, null, null), false, false);
				return;
			}
			this.HandleDone();
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x00034374 File Offset: 0x00032574
		private void HandleDone()
		{
			if (!this._isFinalized)
			{
				MBInformationManager.HideInformations();
				bool flag = this._inventoryLogic.TotalAmount < 0;
				InventoryListener inventoryListener = this._inventoryLogic.InventoryListener;
				bool flag2 = ((inventoryListener != null) ? inventoryListener.GetGold() : 0) >= MathF.Abs(this._inventoryLogic.TotalAmount);
				int num = (int)this._inventoryLogic.XpGainFromDonations;
				int num2 = (this._usageType == InventoryMode.Default && num == 0 && !Game.Current.CheatMode) ? this._inventoryLogic.GetElementCountOnSide(InventoryLogic.InventorySide.OtherInventory) : 0;
				if (flag && !flag2)
				{
					InformationManager.ShowInquiry(new InquiryData("", GameTexts.FindText("str_trader_doesnt_have_enough_money", null).ToString(), true, true, GameTexts.FindText("str_yes", null).ToString(), GameTexts.FindText("str_no", null).ToString(), delegate()
					{
						InventoryManager.Instance.CloseInventoryPresentation(false);
					}, null, "", 0f, null, null, null), false, false);
				}
				else if (num2 > 0)
				{
					InformationManager.ShowInquiry(new InquiryData("", GameTexts.FindText("str_discarding_items", null).ToString(), true, true, GameTexts.FindText("str_yes", null).ToString(), GameTexts.FindText("str_no", null).ToString(), delegate()
					{
						InventoryManager.Instance.CloseInventoryPresentation(false);
					}, null, "", 0f, null, null, null), false, false);
				}
				else
				{
					InventoryManager.Instance.CloseInventoryPresentation(false);
				}
				this.SaveItemLockStates();
				this.SaveItemSortStates();
			}
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x0003450D File Offset: 0x0003270D
		private void SaveItemLockStates()
		{
			this._viewDataTracker.SetInventoryLocks(this._lockedItemIDs);
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x00034520 File Offset: 0x00032720
		private void SaveItemSortStates()
		{
			this._viewDataTracker.InventorySetSortPreference((int)this._usageType, (int)this.PlayerInventorySortController.CurrentSortOption.Value, (int)this.PlayerInventorySortController.CurrentSortState.Value);
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x00034564 File Offset: 0x00032764
		public void ExecuteTransferWithParameters(SPItemVM item, int index, string targetTag)
		{
			if (targetTag == "OverCharacter")
			{
				this.TargetEquipmentIndex = -1;
				if (item.InventorySide == InventoryLogic.InventorySide.OtherInventory)
				{
					item.TransactionCount = 1;
					this.TransactionCount = 1;
					this.ProcessEquipItem(item);
					return;
				}
				if (item.InventorySide == InventoryLogic.InventorySide.PlayerInventory)
				{
					this.ProcessEquipItem(item);
					return;
				}
			}
			else if (targetTag == "PlayerInventory")
			{
				this.TargetEquipmentIndex = -1;
				if (item.InventorySide == InventoryLogic.InventorySide.Equipment)
				{
					this.ProcessUnequipItem(item);
					return;
				}
				if (item.InventorySide == InventoryLogic.InventorySide.OtherInventory)
				{
					item.TransactionCount = item.ItemCount;
					this.TransactionCount = item.ItemCount;
					this.ProcessBuyItem(item, false);
					return;
				}
			}
			else if (targetTag == "OtherInventory")
			{
				if (item.InventorySide != InventoryLogic.InventorySide.OtherInventory)
				{
					item.TransactionCount = item.ItemCount;
					this.TransactionCount = item.ItemCount;
					this.ProcessSellItem(item, false);
					return;
				}
			}
			else if (targetTag.StartsWith("Equipment"))
			{
				this.TargetEquipmentIndex = int.Parse(targetTag.Substring("Equipment".Length + 1));
				if (item.InventorySide == InventoryLogic.InventorySide.OtherInventory)
				{
					item.TransactionCount = 1;
					this.TransactionCount = 1;
					this.ProcessEquipItem(item);
					return;
				}
				if (item.InventorySide == InventoryLogic.InventorySide.PlayerInventory || item.InventorySide == InventoryLogic.InventorySide.Equipment)
				{
					this.ProcessEquipItem(item);
				}
			}
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x000346A2 File Offset: 0x000328A2
		private void UpdateIsDoneDisabled()
		{
			this.IsDoneDisabled = !this._inventoryLogic.CanPlayerCompleteTransaction();
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x000346B8 File Offset: 0x000328B8
		private void ProcessFilter(SPInventoryVM.Filters filterIndex)
		{
			this.ActiveFilterIndex = (int)filterIndex;
			this.IsRefreshed = false;
			foreach (SPItemVM spitemVM in this.LeftItemListVM)
			{
				if (spitemVM != null)
				{
					this.UpdateFilteredStatusOfItem(spitemVM);
				}
			}
			foreach (SPItemVM spitemVM2 in this.RightItemListVM)
			{
				if (spitemVM2 != null)
				{
					this.UpdateFilteredStatusOfItem(spitemVM2);
				}
			}
			this.IsRefreshed = true;
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x0003475C File Offset: 0x0003295C
		private void UpdateFilteredStatusOfItem(SPItemVM item)
		{
			bool flag = !this._filters[this._activeFilterIndex].Contains(item.TypeId);
			bool flag2 = false;
			if (this.IsSearchAvailable && (item.InventorySide == InventoryLogic.InventorySide.OtherInventory || item.InventorySide == InventoryLogic.InventorySide.PlayerInventory))
			{
				string text = (item.InventorySide == InventoryLogic.InventorySide.OtherInventory) ? this.LeftSearchText : this.RightSearchText;
				if (text.Length > 1)
				{
					flag2 = !item.ItemDescription.ToLower().Contains(text);
				}
			}
			item.IsFiltered = (flag || flag2);
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x000347E7 File Offset: 0x000329E7
		private void OnSearchTextChanged(bool isLeft)
		{
			if (this.IsSearchAvailable)
			{
				(isLeft ? this.LeftItemListVM : this.RightItemListVM).ApplyActionOnAllItems(delegate(SPItemVM x)
				{
					this.UpdateFilteredStatusOfItem(x);
				});
			}
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x00034813 File Offset: 0x00032A13
		public void ExecuteFilterNone()
		{
			this.ProcessFilter(SPInventoryVM.Filters.All);
			Game.Current.EventManager.TriggerEvent<InventoryFilterChangedEvent>(new InventoryFilterChangedEvent(SPInventoryVM.Filters.All));
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x00034831 File Offset: 0x00032A31
		public void ExecuteFilterWeapons()
		{
			this.ProcessFilter(SPInventoryVM.Filters.Weapons);
			Game.Current.EventManager.TriggerEvent<InventoryFilterChangedEvent>(new InventoryFilterChangedEvent(SPInventoryVM.Filters.Weapons));
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x0003484F File Offset: 0x00032A4F
		public void ExecuteFilterArmors()
		{
			this.ProcessFilter(SPInventoryVM.Filters.Armors);
			Game.Current.EventManager.TriggerEvent<InventoryFilterChangedEvent>(new InventoryFilterChangedEvent(SPInventoryVM.Filters.Armors));
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x0003486D File Offset: 0x00032A6D
		public void ExecuteFilterShieldsAndRanged()
		{
			this.ProcessFilter(SPInventoryVM.Filters.ShieldsAndRanged);
			Game.Current.EventManager.TriggerEvent<InventoryFilterChangedEvent>(new InventoryFilterChangedEvent(SPInventoryVM.Filters.ShieldsAndRanged));
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x0003488B File Offset: 0x00032A8B
		public void ExecuteFilterMounts()
		{
			this.ProcessFilter(SPInventoryVM.Filters.Mounts);
			Game.Current.EventManager.TriggerEvent<InventoryFilterChangedEvent>(new InventoryFilterChangedEvent(SPInventoryVM.Filters.Mounts));
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x000348A9 File Offset: 0x00032AA9
		public void ExecuteFilterMisc()
		{
			this.ProcessFilter(SPInventoryVM.Filters.Miscellaneous);
			Game.Current.EventManager.TriggerEvent<InventoryFilterChangedEvent>(new InventoryFilterChangedEvent(SPInventoryVM.Filters.Miscellaneous));
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x000348C8 File Offset: 0x00032AC8
		public void CycleBetweenWeaponSlots()
		{
			EquipmentIndex selectedEquipmentIndex = (EquipmentIndex)this._selectedEquipmentIndex;
			if (selectedEquipmentIndex >= EquipmentIndex.WeaponItemBeginSlot && selectedEquipmentIndex < EquipmentIndex.NumAllWeaponSlots)
			{
				int selectedEquipmentIndex2 = this._selectedEquipmentIndex;
				do
				{
					if (this._selectedEquipmentIndex < 3)
					{
						this._selectedEquipmentIndex++;
					}
					else
					{
						this._selectedEquipmentIndex = 0;
					}
				}
				while (this._selectedEquipmentIndex != selectedEquipmentIndex2 && this.GetItemFromIndex((EquipmentIndex)this._selectedEquipmentIndex).ItemRosterElement.EquipmentElement.Item == null);
			}
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x00034934 File Offset: 0x00032B34
		private SPItemVM GetItemFromIndex(EquipmentIndex itemType)
		{
			switch (itemType)
			{
			case EquipmentIndex.WeaponItemBeginSlot:
				return this.CharacterWeapon1Slot;
			case EquipmentIndex.Weapon1:
				return this.CharacterWeapon2Slot;
			case EquipmentIndex.Weapon2:
				return this.CharacterWeapon3Slot;
			case EquipmentIndex.Weapon3:
				return this.CharacterWeapon4Slot;
			case EquipmentIndex.ExtraWeaponSlot:
				return this.CharacterBannerSlot;
			case EquipmentIndex.NumAllWeaponSlots:
				return this.CharacterHelmSlot;
			case EquipmentIndex.Body:
				return this.CharacterTorsoSlot;
			case EquipmentIndex.Leg:
				return this.CharacterBootSlot;
			case EquipmentIndex.Gloves:
				return this.CharacterGloveSlot;
			case EquipmentIndex.Cape:
				return this.CharacterCloakSlot;
			case EquipmentIndex.ArmorItemEndSlot:
				return this.CharacterMountSlot;
			case EquipmentIndex.HorseHarness:
				return this.CharacterMountArmorSlot;
			default:
				return null;
			}
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x000349D0 File Offset: 0x00032BD0
		private void OnTutorialNotificationElementIDChange(TutorialNotificationElementChangeEvent obj)
		{
			if (obj.NewNotificationElementID != this._latestTutorialElementID)
			{
				if (this._latestTutorialElementID != null)
				{
					if (obj.NewNotificationElementID != "TransferButtonOnlyFood" && this._isFoodTransferButtonHighlightApplied)
					{
						this.SetFoodTransferButtonHighlightState(false);
						this._isFoodTransferButtonHighlightApplied = false;
					}
					if (obj.NewNotificationElementID != "InventoryMicsFilter" && this.IsMicsFilterHighlightEnabled)
					{
						this.IsMicsFilterHighlightEnabled = false;
					}
					if (obj.NewNotificationElementID != "CivilianFilter" && this.IsCivilianFilterHighlightEnabled)
					{
						this.IsCivilianFilterHighlightEnabled = false;
					}
					if (obj.NewNotificationElementID != "InventoryOtherBannerItems" && this.IsBannerItemsHighlightApplied)
					{
						this.SetBannerItemsHighlightState(false);
						this.IsCivilianFilterHighlightEnabled = false;
					}
				}
				this._latestTutorialElementID = obj.NewNotificationElementID;
				if (!string.IsNullOrEmpty(this._latestTutorialElementID))
				{
					if (!this._isFoodTransferButtonHighlightApplied && this._latestTutorialElementID == "TransferButtonOnlyFood")
					{
						this.SetFoodTransferButtonHighlightState(true);
						this._isFoodTransferButtonHighlightApplied = true;
					}
					if (!this.IsMicsFilterHighlightEnabled && this._latestTutorialElementID == "InventoryMicsFilter")
					{
						this.IsMicsFilterHighlightEnabled = true;
					}
					if (!this.IsCivilianFilterHighlightEnabled && this._latestTutorialElementID == "CivilianFilter")
					{
						this.IsCivilianFilterHighlightEnabled = true;
					}
					if (!this.IsBannerItemsHighlightApplied && this._latestTutorialElementID == "InventoryOtherBannerItems")
					{
						this.IsBannerItemsHighlightApplied = true;
						this.ExecuteFilterMisc();
						this.SetBannerItemsHighlightState(true);
						return;
					}
				}
				else
				{
					if (this._isFoodTransferButtonHighlightApplied)
					{
						this.SetFoodTransferButtonHighlightState(false);
						this._isFoodTransferButtonHighlightApplied = false;
					}
					if (this.IsMicsFilterHighlightEnabled)
					{
						this.IsMicsFilterHighlightEnabled = false;
					}
					if (this.IsCivilianFilterHighlightEnabled)
					{
						this.IsCivilianFilterHighlightEnabled = false;
					}
					if (this.IsBannerItemsHighlightApplied)
					{
						this.SetBannerItemsHighlightState(false);
						this.IsBannerItemsHighlightApplied = false;
					}
				}
			}
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x00034B90 File Offset: 0x00032D90
		private void SetFoodTransferButtonHighlightState(bool state)
		{
			for (int i = 0; i < this.LeftItemListVM.Count; i++)
			{
				SPItemVM spitemVM = this.LeftItemListVM[i];
				if (spitemVM.ItemRosterElement.EquipmentElement.Item.IsFood)
				{
					spitemVM.IsTransferButtonHighlighted = state;
				}
			}
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x00034BE4 File Offset: 0x00032DE4
		private void SetBannerItemsHighlightState(bool state)
		{
			for (int i = 0; i < this.LeftItemListVM.Count; i++)
			{
				SPItemVM spitemVM = this.LeftItemListVM[i];
				if (spitemVM.ItemRosterElement.EquipmentElement.Item.IsBannerItem)
				{
					spitemVM.IsItemHighlightEnabled = state;
				}
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000C36 RID: 3126 RVA: 0x00034C35 File Offset: 0x00032E35
		// (set) Token: 0x06000C37 RID: 3127 RVA: 0x00034C3D File Offset: 0x00032E3D
		[DataSourceProperty]
		public HintViewModel ResetHint
		{
			get
			{
				return this._resetHint;
			}
			set
			{
				if (value != this._resetHint)
				{
					this._resetHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "ResetHint");
				}
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000C38 RID: 3128 RVA: 0x00034C5B File Offset: 0x00032E5B
		// (set) Token: 0x06000C39 RID: 3129 RVA: 0x00034C63 File Offset: 0x00032E63
		[DataSourceProperty]
		public string LeftInventoryLabel
		{
			get
			{
				return this._leftInventoryLabel;
			}
			set
			{
				if (value != this._leftInventoryLabel)
				{
					this._leftInventoryLabel = value;
					base.OnPropertyChangedWithValue<string>(value, "LeftInventoryLabel");
				}
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000C3A RID: 3130 RVA: 0x00034C86 File Offset: 0x00032E86
		// (set) Token: 0x06000C3B RID: 3131 RVA: 0x00034C8E File Offset: 0x00032E8E
		[DataSourceProperty]
		public string RightInventoryLabel
		{
			get
			{
				return this._rightInventoryLabel;
			}
			set
			{
				if (value != this._rightInventoryLabel)
				{
					this._rightInventoryLabel = value;
					base.OnPropertyChangedWithValue<string>(value, "RightInventoryLabel");
				}
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000C3C RID: 3132 RVA: 0x00034CB1 File Offset: 0x00032EB1
		// (set) Token: 0x06000C3D RID: 3133 RVA: 0x00034CB9 File Offset: 0x00032EB9
		[DataSourceProperty]
		public string DoneLbl
		{
			get
			{
				return this._doneLbl;
			}
			set
			{
				if (value != this._doneLbl)
				{
					this._doneLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "DoneLbl");
				}
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000C3E RID: 3134 RVA: 0x00034CDC File Offset: 0x00032EDC
		// (set) Token: 0x06000C3F RID: 3135 RVA: 0x00034CE4 File Offset: 0x00032EE4
		[DataSourceProperty]
		public bool IsDoneDisabled
		{
			get
			{
				return this._isDoneDisabled;
			}
			set
			{
				if (value != this._isDoneDisabled)
				{
					this._isDoneDisabled = value;
					base.OnPropertyChangedWithValue(value, "IsDoneDisabled");
				}
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000C40 RID: 3136 RVA: 0x00034D02 File Offset: 0x00032F02
		// (set) Token: 0x06000C41 RID: 3137 RVA: 0x00034D0A File Offset: 0x00032F0A
		[DataSourceProperty]
		public bool OtherSideHasCapacity
		{
			get
			{
				return this._otherSideHasCapacity;
			}
			set
			{
				if (value != this._otherSideHasCapacity)
				{
					this._otherSideHasCapacity = value;
					base.OnPropertyChangedWithValue(value, "OtherSideHasCapacity");
				}
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000C42 RID: 3138 RVA: 0x00034D28 File Offset: 0x00032F28
		// (set) Token: 0x06000C43 RID: 3139 RVA: 0x00034D30 File Offset: 0x00032F30
		[DataSourceProperty]
		public bool IsSearchAvailable
		{
			get
			{
				return this._isSearchAvailable;
			}
			set
			{
				if (value != this._isSearchAvailable)
				{
					if (!value)
					{
						this.LeftSearchText = string.Empty;
						this.RightSearchText = string.Empty;
					}
					this._isSearchAvailable = value;
					base.OnPropertyChangedWithValue(value, "IsSearchAvailable");
				}
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000C44 RID: 3140 RVA: 0x00034D67 File Offset: 0x00032F67
		// (set) Token: 0x06000C45 RID: 3141 RVA: 0x00034D6F File Offset: 0x00032F6F
		[DataSourceProperty]
		public bool IsOtherInventoryGoldRelevant
		{
			get
			{
				return this._isOtherInventoryGoldRelevant;
			}
			set
			{
				if (value != this._isOtherInventoryGoldRelevant)
				{
					this._isOtherInventoryGoldRelevant = value;
					base.OnPropertyChangedWithValue(value, "IsOtherInventoryGoldRelevant");
				}
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000C46 RID: 3142 RVA: 0x00034D8D File Offset: 0x00032F8D
		// (set) Token: 0x06000C47 RID: 3143 RVA: 0x00034D95 File Offset: 0x00032F95
		[DataSourceProperty]
		public string CancelLbl
		{
			get
			{
				return this._cancelLbl;
			}
			set
			{
				if (value != this._cancelLbl)
				{
					this._cancelLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "CancelLbl");
				}
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000C48 RID: 3144 RVA: 0x00034DB8 File Offset: 0x00032FB8
		// (set) Token: 0x06000C49 RID: 3145 RVA: 0x00034DC0 File Offset: 0x00032FC0
		[DataSourceProperty]
		public string ResetLbl
		{
			get
			{
				return this._resetLbl;
			}
			set
			{
				if (value != this._resetLbl)
				{
					this._resetLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "ResetLbl");
				}
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000C4A RID: 3146 RVA: 0x00034DE3 File Offset: 0x00032FE3
		// (set) Token: 0x06000C4B RID: 3147 RVA: 0x00034DEB File Offset: 0x00032FEB
		[DataSourceProperty]
		public string TypeText
		{
			get
			{
				return this._typeText;
			}
			set
			{
				if (value != this._typeText)
				{
					this._typeText = value;
					base.OnPropertyChangedWithValue<string>(value, "TypeText");
				}
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000C4C RID: 3148 RVA: 0x00034E0E File Offset: 0x0003300E
		// (set) Token: 0x06000C4D RID: 3149 RVA: 0x00034E16 File Offset: 0x00033016
		[DataSourceProperty]
		public string NameText
		{
			get
			{
				return this._nameText;
			}
			set
			{
				if (value != this._nameText)
				{
					this._nameText = value;
					base.OnPropertyChangedWithValue<string>(value, "NameText");
				}
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000C4E RID: 3150 RVA: 0x00034E39 File Offset: 0x00033039
		// (set) Token: 0x06000C4F RID: 3151 RVA: 0x00034E41 File Offset: 0x00033041
		[DataSourceProperty]
		public string QuantityText
		{
			get
			{
				return this._quantityText;
			}
			set
			{
				if (value != this._quantityText)
				{
					this._quantityText = value;
					base.OnPropertyChangedWithValue<string>(value, "QuantityText");
				}
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000C50 RID: 3152 RVA: 0x00034E64 File Offset: 0x00033064
		// (set) Token: 0x06000C51 RID: 3153 RVA: 0x00034E6C File Offset: 0x0003306C
		[DataSourceProperty]
		public string CostText
		{
			get
			{
				return this._costText;
			}
			set
			{
				if (value != this._costText)
				{
					this._costText = value;
					base.OnPropertyChangedWithValue<string>(value, "CostText");
				}
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000C52 RID: 3154 RVA: 0x00034E8F File Offset: 0x0003308F
		// (set) Token: 0x06000C53 RID: 3155 RVA: 0x00034E97 File Offset: 0x00033097
		[DataSourceProperty]
		public string SearchPlaceholderText
		{
			get
			{
				return this._searchPlaceholderText;
			}
			set
			{
				if (value != this._searchPlaceholderText)
				{
					this._searchPlaceholderText = value;
					base.OnPropertyChangedWithValue<string>(value, "SearchPlaceholderText");
				}
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000C54 RID: 3156 RVA: 0x00034EBA File Offset: 0x000330BA
		// (set) Token: 0x06000C55 RID: 3157 RVA: 0x00034EC2 File Offset: 0x000330C2
		[DataSourceProperty]
		public BasicTooltipViewModel ProductionTooltip
		{
			get
			{
				return this._productionTooltip;
			}
			set
			{
				if (value != this._productionTooltip)
				{
					this._productionTooltip = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "ProductionTooltip");
				}
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000C56 RID: 3158 RVA: 0x00034EE0 File Offset: 0x000330E0
		// (set) Token: 0x06000C57 RID: 3159 RVA: 0x00034EE8 File Offset: 0x000330E8
		[DataSourceProperty]
		public BasicTooltipViewModel EquipmentMaxCountHint
		{
			get
			{
				return this._equipmentMaxCountHint;
			}
			set
			{
				if (value != this._equipmentMaxCountHint)
				{
					this._equipmentMaxCountHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "EquipmentMaxCountHint");
				}
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000C58 RID: 3160 RVA: 0x00034F06 File Offset: 0x00033106
		// (set) Token: 0x06000C59 RID: 3161 RVA: 0x00034F0E File Offset: 0x0003310E
		[DataSourceProperty]
		public BasicTooltipViewModel CurrentCharacterSkillsTooltip
		{
			get
			{
				return this._currentCharacterSkillsTooltip;
			}
			set
			{
				if (value != this._currentCharacterSkillsTooltip)
				{
					this._currentCharacterSkillsTooltip = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "CurrentCharacterSkillsTooltip");
				}
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000C5A RID: 3162 RVA: 0x00034F2C File Offset: 0x0003312C
		// (set) Token: 0x06000C5B RID: 3163 RVA: 0x00034F34 File Offset: 0x00033134
		[DataSourceProperty]
		public HintViewModel NoSaddleHint
		{
			get
			{
				return this._noSaddleHint;
			}
			set
			{
				if (value != this._noSaddleHint)
				{
					this._noSaddleHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "NoSaddleHint");
				}
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000C5C RID: 3164 RVA: 0x00034F52 File Offset: 0x00033152
		// (set) Token: 0x06000C5D RID: 3165 RVA: 0x00034F5A File Offset: 0x0003315A
		[DataSourceProperty]
		public HintViewModel DonationLblHint
		{
			get
			{
				return this._donationLblHint;
			}
			set
			{
				if (value != this._donationLblHint)
				{
					this._donationLblHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "DonationLblHint");
				}
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000C5E RID: 3166 RVA: 0x00034F78 File Offset: 0x00033178
		// (set) Token: 0x06000C5F RID: 3167 RVA: 0x00034F80 File Offset: 0x00033180
		[DataSourceProperty]
		public HintViewModel ArmArmorHint
		{
			get
			{
				return this._armArmorHint;
			}
			set
			{
				if (value != this._armArmorHint)
				{
					this._armArmorHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "ArmArmorHint");
				}
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000C60 RID: 3168 RVA: 0x00034F9E File Offset: 0x0003319E
		// (set) Token: 0x06000C61 RID: 3169 RVA: 0x00034FA6 File Offset: 0x000331A6
		[DataSourceProperty]
		public HintViewModel BodyArmorHint
		{
			get
			{
				return this._bodyArmorHint;
			}
			set
			{
				if (value != this._bodyArmorHint)
				{
					this._bodyArmorHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "BodyArmorHint");
				}
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000C62 RID: 3170 RVA: 0x00034FC4 File Offset: 0x000331C4
		// (set) Token: 0x06000C63 RID: 3171 RVA: 0x00034FCC File Offset: 0x000331CC
		[DataSourceProperty]
		public HintViewModel HeadArmorHint
		{
			get
			{
				return this._headArmorHint;
			}
			set
			{
				if (value != this._headArmorHint)
				{
					this._headArmorHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "HeadArmorHint");
				}
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000C64 RID: 3172 RVA: 0x00034FEA File Offset: 0x000331EA
		// (set) Token: 0x06000C65 RID: 3173 RVA: 0x00034FF2 File Offset: 0x000331F2
		[DataSourceProperty]
		public HintViewModel LegArmorHint
		{
			get
			{
				return this._legArmorHint;
			}
			set
			{
				if (value != this._legArmorHint)
				{
					this._legArmorHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "LegArmorHint");
				}
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000C66 RID: 3174 RVA: 0x00035010 File Offset: 0x00033210
		// (set) Token: 0x06000C67 RID: 3175 RVA: 0x00035018 File Offset: 0x00033218
		[DataSourceProperty]
		public HintViewModel HorseArmorHint
		{
			get
			{
				return this._horseArmorHint;
			}
			set
			{
				if (value != this._horseArmorHint)
				{
					this._horseArmorHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "HorseArmorHint");
				}
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000C68 RID: 3176 RVA: 0x00035036 File Offset: 0x00033236
		// (set) Token: 0x06000C69 RID: 3177 RVA: 0x0003503E File Offset: 0x0003323E
		[DataSourceProperty]
		public HintViewModel FilterAllHint
		{
			get
			{
				return this._filterAllHint;
			}
			set
			{
				if (value != this._filterAllHint)
				{
					this._filterAllHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "FilterAllHint");
				}
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000C6A RID: 3178 RVA: 0x0003505C File Offset: 0x0003325C
		// (set) Token: 0x06000C6B RID: 3179 RVA: 0x00035064 File Offset: 0x00033264
		[DataSourceProperty]
		public HintViewModel FilterWeaponHint
		{
			get
			{
				return this._filterWeaponHint;
			}
			set
			{
				if (value != this._filterWeaponHint)
				{
					this._filterWeaponHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "FilterWeaponHint");
				}
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000C6C RID: 3180 RVA: 0x00035082 File Offset: 0x00033282
		// (set) Token: 0x06000C6D RID: 3181 RVA: 0x0003508A File Offset: 0x0003328A
		[DataSourceProperty]
		public HintViewModel FilterArmorHint
		{
			get
			{
				return this._filterArmorHint;
			}
			set
			{
				if (value != this._filterArmorHint)
				{
					this._filterArmorHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "FilterArmorHint");
				}
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000C6E RID: 3182 RVA: 0x000350A8 File Offset: 0x000332A8
		// (set) Token: 0x06000C6F RID: 3183 RVA: 0x000350B0 File Offset: 0x000332B0
		[DataSourceProperty]
		public HintViewModel FilterShieldAndRangedHint
		{
			get
			{
				return this._filterShieldAndRangedHint;
			}
			set
			{
				if (value != this._filterShieldAndRangedHint)
				{
					this._filterShieldAndRangedHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "FilterShieldAndRangedHint");
				}
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000C70 RID: 3184 RVA: 0x000350CE File Offset: 0x000332CE
		// (set) Token: 0x06000C71 RID: 3185 RVA: 0x000350D6 File Offset: 0x000332D6
		[DataSourceProperty]
		public HintViewModel FilterMountAndHarnessHint
		{
			get
			{
				return this._filterMountAndHarnessHint;
			}
			set
			{
				if (value != this._filterMountAndHarnessHint)
				{
					this._filterMountAndHarnessHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "FilterMountAndHarnessHint");
				}
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000C72 RID: 3186 RVA: 0x000350F4 File Offset: 0x000332F4
		// (set) Token: 0x06000C73 RID: 3187 RVA: 0x000350FC File Offset: 0x000332FC
		[DataSourceProperty]
		public HintViewModel FilterMiscHint
		{
			get
			{
				return this._filterMiscHint;
			}
			set
			{
				if (value != this._filterMiscHint)
				{
					this._filterMiscHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "FilterMiscHint");
				}
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000C74 RID: 3188 RVA: 0x0003511A File Offset: 0x0003331A
		// (set) Token: 0x06000C75 RID: 3189 RVA: 0x00035122 File Offset: 0x00033322
		[DataSourceProperty]
		public HintViewModel CivilianOutfitHint
		{
			get
			{
				return this._civilianOutfitHint;
			}
			set
			{
				if (value != this._civilianOutfitHint)
				{
					this._civilianOutfitHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "CivilianOutfitHint");
				}
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000C76 RID: 3190 RVA: 0x00035140 File Offset: 0x00033340
		// (set) Token: 0x06000C77 RID: 3191 RVA: 0x00035148 File Offset: 0x00033348
		[DataSourceProperty]
		public HintViewModel BattleOutfitHint
		{
			get
			{
				return this._battleOutfitHint;
			}
			set
			{
				if (value != this._battleOutfitHint)
				{
					this._battleOutfitHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "BattleOutfitHint");
				}
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000C78 RID: 3192 RVA: 0x00035166 File Offset: 0x00033366
		// (set) Token: 0x06000C79 RID: 3193 RVA: 0x0003516E File Offset: 0x0003336E
		[DataSourceProperty]
		public HintViewModel EquipmentHelmSlotHint
		{
			get
			{
				return this._equipmentHelmSlotHint;
			}
			set
			{
				if (value != this._equipmentHelmSlotHint)
				{
					this._equipmentHelmSlotHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "EquipmentHelmSlotHint");
				}
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000C7A RID: 3194 RVA: 0x0003518C File Offset: 0x0003338C
		// (set) Token: 0x06000C7B RID: 3195 RVA: 0x00035194 File Offset: 0x00033394
		[DataSourceProperty]
		public HintViewModel EquipmentArmorSlotHint
		{
			get
			{
				return this._equipmentArmorSlotHint;
			}
			set
			{
				if (value != this._equipmentArmorSlotHint)
				{
					this._equipmentArmorSlotHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "EquipmentArmorSlotHint");
				}
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000C7C RID: 3196 RVA: 0x000351B2 File Offset: 0x000333B2
		// (set) Token: 0x06000C7D RID: 3197 RVA: 0x000351BA File Offset: 0x000333BA
		[DataSourceProperty]
		public HintViewModel EquipmentBootSlotHint
		{
			get
			{
				return this._equipmentBootSlotHint;
			}
			set
			{
				if (value != this._equipmentBootSlotHint)
				{
					this._equipmentBootSlotHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "EquipmentBootSlotHint");
				}
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000C7E RID: 3198 RVA: 0x000351D8 File Offset: 0x000333D8
		// (set) Token: 0x06000C7F RID: 3199 RVA: 0x000351E0 File Offset: 0x000333E0
		[DataSourceProperty]
		public HintViewModel EquipmentCloakSlotHint
		{
			get
			{
				return this._equipmentCloakSlotHint;
			}
			set
			{
				if (value != this._equipmentCloakSlotHint)
				{
					this._equipmentCloakSlotHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "EquipmentCloakSlotHint");
				}
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000C80 RID: 3200 RVA: 0x000351FE File Offset: 0x000333FE
		// (set) Token: 0x06000C81 RID: 3201 RVA: 0x00035206 File Offset: 0x00033406
		[DataSourceProperty]
		public HintViewModel EquipmentGloveSlotHint
		{
			get
			{
				return this._equipmentGloveSlotHint;
			}
			set
			{
				if (value != this._equipmentGloveSlotHint)
				{
					this._equipmentGloveSlotHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "EquipmentGloveSlotHint");
				}
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000C82 RID: 3202 RVA: 0x00035224 File Offset: 0x00033424
		// (set) Token: 0x06000C83 RID: 3203 RVA: 0x0003522C File Offset: 0x0003342C
		[DataSourceProperty]
		public HintViewModel EquipmentHarnessSlotHint
		{
			get
			{
				return this._equipmentHarnessSlotHint;
			}
			set
			{
				if (value != this._equipmentHarnessSlotHint)
				{
					this._equipmentHarnessSlotHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "EquipmentHarnessSlotHint");
				}
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000C84 RID: 3204 RVA: 0x0003524A File Offset: 0x0003344A
		// (set) Token: 0x06000C85 RID: 3205 RVA: 0x00035252 File Offset: 0x00033452
		[DataSourceProperty]
		public HintViewModel EquipmentMountSlotHint
		{
			get
			{
				return this._equipmentMountSlotHint;
			}
			set
			{
				if (value != this._equipmentMountSlotHint)
				{
					this._equipmentMountSlotHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "EquipmentMountSlotHint");
				}
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000C86 RID: 3206 RVA: 0x00035270 File Offset: 0x00033470
		// (set) Token: 0x06000C87 RID: 3207 RVA: 0x00035278 File Offset: 0x00033478
		[DataSourceProperty]
		public HintViewModel EquipmentWeaponSlotHint
		{
			get
			{
				return this._equipmentWeaponSlotHint;
			}
			set
			{
				if (value != this._equipmentWeaponSlotHint)
				{
					this._equipmentWeaponSlotHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "EquipmentWeaponSlotHint");
				}
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000C88 RID: 3208 RVA: 0x00035296 File Offset: 0x00033496
		// (set) Token: 0x06000C89 RID: 3209 RVA: 0x0003529E File Offset: 0x0003349E
		[DataSourceProperty]
		public HintViewModel EquipmentBannerSlotHint
		{
			get
			{
				return this._equipmentBannerSlotHint;
			}
			set
			{
				if (value != this._equipmentBannerSlotHint)
				{
					this._equipmentBannerSlotHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "EquipmentBannerSlotHint");
				}
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000C8A RID: 3210 RVA: 0x000352BC File Offset: 0x000334BC
		// (set) Token: 0x06000C8B RID: 3211 RVA: 0x000352C4 File Offset: 0x000334C4
		[DataSourceProperty]
		public BasicTooltipViewModel BuyAllHint
		{
			get
			{
				return this._buyAllHint;
			}
			set
			{
				if (value != this._buyAllHint)
				{
					this._buyAllHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "BuyAllHint");
				}
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000C8C RID: 3212 RVA: 0x000352E2 File Offset: 0x000334E2
		// (set) Token: 0x06000C8D RID: 3213 RVA: 0x000352EA File Offset: 0x000334EA
		[DataSourceProperty]
		public BasicTooltipViewModel SellAllHint
		{
			get
			{
				return this._sellAllHint;
			}
			set
			{
				if (value != this._sellAllHint)
				{
					this._sellAllHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "SellAllHint");
				}
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000C8E RID: 3214 RVA: 0x00035308 File Offset: 0x00033508
		// (set) Token: 0x06000C8F RID: 3215 RVA: 0x00035310 File Offset: 0x00033510
		[DataSourceProperty]
		public BasicTooltipViewModel PreviousCharacterHint
		{
			get
			{
				return this._previousCharacterHint;
			}
			set
			{
				if (value != this._previousCharacterHint)
				{
					this._previousCharacterHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "PreviousCharacterHint");
				}
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000C90 RID: 3216 RVA: 0x0003532E File Offset: 0x0003352E
		// (set) Token: 0x06000C91 RID: 3217 RVA: 0x00035336 File Offset: 0x00033536
		[DataSourceProperty]
		public BasicTooltipViewModel NextCharacterHint
		{
			get
			{
				return this._nextCharacterHint;
			}
			set
			{
				if (value != this._nextCharacterHint)
				{
					this._nextCharacterHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "NextCharacterHint");
				}
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000C92 RID: 3218 RVA: 0x00035354 File Offset: 0x00033554
		// (set) Token: 0x06000C93 RID: 3219 RVA: 0x0003535C File Offset: 0x0003355C
		[DataSourceProperty]
		public HintViewModel WeightHint
		{
			get
			{
				return this._weightHint;
			}
			set
			{
				if (value != this._weightHint)
				{
					this._weightHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "WeightHint");
				}
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000C94 RID: 3220 RVA: 0x0003537A File Offset: 0x0003357A
		// (set) Token: 0x06000C95 RID: 3221 RVA: 0x00035382 File Offset: 0x00033582
		[DataSourceProperty]
		public HintViewModel PreviewHint
		{
			get
			{
				return this._previewHint;
			}
			set
			{
				if (value != this._previewHint)
				{
					this._previewHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "PreviewHint");
				}
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000C96 RID: 3222 RVA: 0x000353A0 File Offset: 0x000335A0
		// (set) Token: 0x06000C97 RID: 3223 RVA: 0x000353A8 File Offset: 0x000335A8
		[DataSourceProperty]
		public HintViewModel EquipHint
		{
			get
			{
				return this._equipHint;
			}
			set
			{
				if (value != this._equipHint)
				{
					this._equipHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "EquipHint");
				}
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000C98 RID: 3224 RVA: 0x000353C6 File Offset: 0x000335C6
		// (set) Token: 0x06000C99 RID: 3225 RVA: 0x000353CE File Offset: 0x000335CE
		[DataSourceProperty]
		public HintViewModel UnequipHint
		{
			get
			{
				return this._unequipHint;
			}
			set
			{
				if (value != this._unequipHint)
				{
					this._unequipHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "UnequipHint");
				}
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000C9A RID: 3226 RVA: 0x000353EC File Offset: 0x000335EC
		// (set) Token: 0x06000C9B RID: 3227 RVA: 0x000353F4 File Offset: 0x000335F4
		[DataSourceProperty]
		public HintViewModel SellHint
		{
			get
			{
				return this._sellHint;
			}
			set
			{
				if (value != this._sellHint)
				{
					this._sellHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "SellHint");
				}
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000C9C RID: 3228 RVA: 0x00035412 File Offset: 0x00033612
		// (set) Token: 0x06000C9D RID: 3229 RVA: 0x0003541A File Offset: 0x0003361A
		[DataSourceProperty]
		public HintViewModel PlayerSideCapacityExceededHint
		{
			get
			{
				return this._playerSideCapacityExceededHint;
			}
			set
			{
				if (value != this._playerSideCapacityExceededHint)
				{
					this._playerSideCapacityExceededHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "PlayerSideCapacityExceededHint");
				}
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000C9E RID: 3230 RVA: 0x00035438 File Offset: 0x00033638
		// (set) Token: 0x06000C9F RID: 3231 RVA: 0x00035440 File Offset: 0x00033640
		[DataSourceProperty]
		public HintViewModel OtherSideCapacityExceededHint
		{
			get
			{
				return this._otherSideCapacityExceededHint;
			}
			set
			{
				if (value != this._otherSideCapacityExceededHint)
				{
					this._otherSideCapacityExceededHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "OtherSideCapacityExceededHint");
				}
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000CA0 RID: 3232 RVA: 0x0003545E File Offset: 0x0003365E
		// (set) Token: 0x06000CA1 RID: 3233 RVA: 0x00035466 File Offset: 0x00033666
		[DataSourceProperty]
		public SelectorVM<InventoryCharacterSelectorItemVM> CharacterList
		{
			get
			{
				return this._characterList;
			}
			set
			{
				if (value != this._characterList)
				{
					this._characterList = value;
					base.OnPropertyChangedWithValue<SelectorVM<InventoryCharacterSelectorItemVM>>(value, "CharacterList");
				}
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x00035484 File Offset: 0x00033684
		// (set) Token: 0x06000CA3 RID: 3235 RVA: 0x0003548C File Offset: 0x0003368C
		[DataSourceProperty]
		public SPInventorySortControllerVM PlayerInventorySortController
		{
			get
			{
				return this._playerInventorySortController;
			}
			set
			{
				if (value != this._playerInventorySortController)
				{
					this._playerInventorySortController = value;
					base.OnPropertyChangedWithValue<SPInventorySortControllerVM>(value, "PlayerInventorySortController");
				}
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000CA4 RID: 3236 RVA: 0x000354AA File Offset: 0x000336AA
		// (set) Token: 0x06000CA5 RID: 3237 RVA: 0x000354B2 File Offset: 0x000336B2
		[DataSourceProperty]
		public SPInventorySortControllerVM OtherInventorySortController
		{
			get
			{
				return this._otherInventorySortController;
			}
			set
			{
				if (value != this._otherInventorySortController)
				{
					this._otherInventorySortController = value;
					base.OnPropertyChangedWithValue<SPInventorySortControllerVM>(value, "OtherInventorySortController");
				}
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000CA6 RID: 3238 RVA: 0x000354D0 File Offset: 0x000336D0
		// (set) Token: 0x06000CA7 RID: 3239 RVA: 0x000354D8 File Offset: 0x000336D8
		[DataSourceProperty]
		public ItemPreviewVM ItemPreview
		{
			get
			{
				return this._itemPreview;
			}
			set
			{
				if (value != this._itemPreview)
				{
					this._itemPreview = value;
					base.OnPropertyChangedWithValue<ItemPreviewVM>(value, "ItemPreview");
				}
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x000354F6 File Offset: 0x000336F6
		// (set) Token: 0x06000CA9 RID: 3241 RVA: 0x000354FE File Offset: 0x000336FE
		[DataSourceProperty]
		public int ActiveFilterIndex
		{
			get
			{
				return (int)this._activeFilterIndex;
			}
			set
			{
				if (value != (int)this._activeFilterIndex)
				{
					this._activeFilterIndex = (SPInventoryVM.Filters)value;
					base.OnPropertyChangedWithValue(value, "ActiveFilterIndex");
				}
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000CAA RID: 3242 RVA: 0x0003551C File Offset: 0x0003371C
		// (set) Token: 0x06000CAB RID: 3243 RVA: 0x00035524 File Offset: 0x00033724
		[DataSourceProperty]
		public bool CompanionExists
		{
			get
			{
				return this._companionExists;
			}
			set
			{
				if (value != this._companionExists)
				{
					this._companionExists = value;
					base.OnPropertyChangedWithValue(value, "CompanionExists");
				}
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000CAC RID: 3244 RVA: 0x00035542 File Offset: 0x00033742
		// (set) Token: 0x06000CAD RID: 3245 RVA: 0x0003554A File Offset: 0x0003374A
		[DataSourceProperty]
		public bool IsTradingWithSettlement
		{
			get
			{
				return this._isTradingWithSettlement;
			}
			set
			{
				if (value != this._isTradingWithSettlement)
				{
					this._isTradingWithSettlement = value;
					base.OnPropertyChangedWithValue(value, "IsTradingWithSettlement");
				}
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000CAE RID: 3246 RVA: 0x00035568 File Offset: 0x00033768
		// (set) Token: 0x06000CAF RID: 3247 RVA: 0x00035570 File Offset: 0x00033770
		[DataSourceProperty]
		public bool IsInWarSet
		{
			get
			{
				return this._isInWarSet;
			}
			set
			{
				if (value != this._isInWarSet)
				{
					this._isInWarSet = value;
					base.OnPropertyChangedWithValue(value, "IsInWarSet");
					this.UpdateRightCharacter();
					Game.Current.EventManager.TriggerEvent<InventoryEquipmentTypeChangedEvent>(new InventoryEquipmentTypeChangedEvent(value));
				}
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x000355A9 File Offset: 0x000337A9
		// (set) Token: 0x06000CB1 RID: 3249 RVA: 0x000355B1 File Offset: 0x000337B1
		[DataSourceProperty]
		public bool IsMicsFilterHighlightEnabled
		{
			get
			{
				return this._isMicsFilterHighlightEnabled;
			}
			set
			{
				if (value != this._isMicsFilterHighlightEnabled)
				{
					this._isMicsFilterHighlightEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsMicsFilterHighlightEnabled");
				}
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000CB2 RID: 3250 RVA: 0x000355CF File Offset: 0x000337CF
		// (set) Token: 0x06000CB3 RID: 3251 RVA: 0x000355D7 File Offset: 0x000337D7
		[DataSourceProperty]
		public bool IsCivilianFilterHighlightEnabled
		{
			get
			{
				return this._isCivilianFilterHighlightEnabled;
			}
			set
			{
				if (value != this._isCivilianFilterHighlightEnabled)
				{
					this._isCivilianFilterHighlightEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsCivilianFilterHighlightEnabled");
				}
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000CB4 RID: 3252 RVA: 0x000355F5 File Offset: 0x000337F5
		// (set) Token: 0x06000CB5 RID: 3253 RVA: 0x000355FD File Offset: 0x000337FD
		[DataSourceProperty]
		public ItemMenuVM ItemMenu
		{
			get
			{
				return this._itemMenu;
			}
			set
			{
				if (value != this._itemMenu)
				{
					this._itemMenu = value;
					base.OnPropertyChangedWithValue<ItemMenuVM>(value, "ItemMenu");
				}
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06000CB6 RID: 3254 RVA: 0x0003561B File Offset: 0x0003381B
		// (set) Token: 0x06000CB7 RID: 3255 RVA: 0x00035623 File Offset: 0x00033823
		[DataSourceProperty]
		public string PlayerSideCapacityExceededText
		{
			get
			{
				return this._playerSideCapacityExceededText;
			}
			set
			{
				if (value != this._playerSideCapacityExceededText)
				{
					this._playerSideCapacityExceededText = value;
					base.OnPropertyChangedWithValue<string>(value, "PlayerSideCapacityExceededText");
				}
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000CB8 RID: 3256 RVA: 0x00035646 File Offset: 0x00033846
		// (set) Token: 0x06000CB9 RID: 3257 RVA: 0x0003564E File Offset: 0x0003384E
		[DataSourceProperty]
		public string OtherSideCapacityExceededText
		{
			get
			{
				return this._otherSideCapacityExceededText;
			}
			set
			{
				if (value != this._otherSideCapacityExceededText)
				{
					this._otherSideCapacityExceededText = value;
					base.OnPropertyChangedWithValue<string>(value, "OtherSideCapacityExceededText");
				}
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000CBA RID: 3258 RVA: 0x00035671 File Offset: 0x00033871
		// (set) Token: 0x06000CBB RID: 3259 RVA: 0x00035679 File Offset: 0x00033879
		[DataSourceProperty]
		public string LeftSearchText
		{
			get
			{
				return this._leftSearchText;
			}
			set
			{
				if (value != this._leftSearchText)
				{
					this._leftSearchText = value;
					base.OnPropertyChangedWithValue<string>(value, "LeftSearchText");
					this.OnSearchTextChanged(true);
				}
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000CBC RID: 3260 RVA: 0x000356A3 File Offset: 0x000338A3
		// (set) Token: 0x06000CBD RID: 3261 RVA: 0x000356AB File Offset: 0x000338AB
		[DataSourceProperty]
		public string RightSearchText
		{
			get
			{
				return this._rightSearchText;
			}
			set
			{
				if (value != this._rightSearchText)
				{
					this._rightSearchText = value;
					base.OnPropertyChangedWithValue<string>(value, "RightSearchText");
					this.OnSearchTextChanged(false);
				}
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000CBE RID: 3262 RVA: 0x000356D5 File Offset: 0x000338D5
		// (set) Token: 0x06000CBF RID: 3263 RVA: 0x000356DD File Offset: 0x000338DD
		[DataSourceProperty]
		public bool HasGainedExperience
		{
			get
			{
				return this._hasGainedExperience;
			}
			set
			{
				if (value != this._hasGainedExperience)
				{
					this._hasGainedExperience = value;
					base.OnPropertyChangedWithValue(value, "HasGainedExperience");
				}
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x000356FB File Offset: 0x000338FB
		// (set) Token: 0x06000CC1 RID: 3265 RVA: 0x00035703 File Offset: 0x00033903
		[DataSourceProperty]
		public bool IsDonationXpGainExceedsMax
		{
			get
			{
				return this._isDonationXpGainExceedsMax;
			}
			set
			{
				if (value != this._isDonationXpGainExceedsMax)
				{
					this._isDonationXpGainExceedsMax = value;
					base.OnPropertyChangedWithValue(value, "IsDonationXpGainExceedsMax");
				}
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000CC2 RID: 3266 RVA: 0x00035721 File Offset: 0x00033921
		// (set) Token: 0x06000CC3 RID: 3267 RVA: 0x00035729 File Offset: 0x00033929
		[DataSourceProperty]
		public bool NoSaddleWarned
		{
			get
			{
				return this._noSaddleWarned;
			}
			set
			{
				if (value != this._noSaddleWarned)
				{
					this._noSaddleWarned = value;
					base.OnPropertyChangedWithValue(value, "NoSaddleWarned");
				}
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06000CC4 RID: 3268 RVA: 0x00035747 File Offset: 0x00033947
		// (set) Token: 0x06000CC5 RID: 3269 RVA: 0x0003574F File Offset: 0x0003394F
		[DataSourceProperty]
		public bool PlayerEquipmentCountWarned
		{
			get
			{
				return this._playerEquipmentCountWarned;
			}
			set
			{
				if (value != this._playerEquipmentCountWarned)
				{
					this._playerEquipmentCountWarned = value;
					base.OnPropertyChangedWithValue(value, "PlayerEquipmentCountWarned");
				}
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000CC6 RID: 3270 RVA: 0x0003576D File Offset: 0x0003396D
		// (set) Token: 0x06000CC7 RID: 3271 RVA: 0x00035775 File Offset: 0x00033975
		[DataSourceProperty]
		public bool OtherEquipmentCountWarned
		{
			get
			{
				return this._otherEquipmentCountWarned;
			}
			set
			{
				if (value != this._otherEquipmentCountWarned)
				{
					this._otherEquipmentCountWarned = value;
					base.OnPropertyChangedWithValue(value, "OtherEquipmentCountWarned");
				}
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000CC8 RID: 3272 RVA: 0x00035793 File Offset: 0x00033993
		// (set) Token: 0x06000CC9 RID: 3273 RVA: 0x0003579B File Offset: 0x0003399B
		[DataSourceProperty]
		public string OtherEquipmentCountText
		{
			get
			{
				return this._otherEquipmentCountText;
			}
			set
			{
				if (value != this._otherEquipmentCountText)
				{
					this._otherEquipmentCountText = value;
					base.OnPropertyChangedWithValue<string>(value, "OtherEquipmentCountText");
				}
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000CCA RID: 3274 RVA: 0x000357BE File Offset: 0x000339BE
		// (set) Token: 0x06000CCB RID: 3275 RVA: 0x000357C6 File Offset: 0x000339C6
		[DataSourceProperty]
		public string PlayerEquipmentCountText
		{
			get
			{
				return this._playerEquipmentCountText;
			}
			set
			{
				if (value != this._playerEquipmentCountText)
				{
					this._playerEquipmentCountText = value;
					base.OnPropertyChangedWithValue<string>(value, "PlayerEquipmentCountText");
				}
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000CCC RID: 3276 RVA: 0x000357E9 File Offset: 0x000339E9
		// (set) Token: 0x06000CCD RID: 3277 RVA: 0x000357F1 File Offset: 0x000339F1
		[DataSourceProperty]
		public string NoSaddleText
		{
			get
			{
				return this._noSaddleText;
			}
			set
			{
				if (value != this._noSaddleText)
				{
					this._noSaddleText = value;
					base.OnPropertyChangedWithValue<string>(value, "NoSaddleText");
				}
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000CCE RID: 3278 RVA: 0x00035814 File Offset: 0x00033A14
		// (set) Token: 0x06000CCF RID: 3279 RVA: 0x0003581C File Offset: 0x00033A1C
		[DataSourceProperty]
		public int TargetEquipmentIndex
		{
			get
			{
				return (int)this._targetEquipmentIndex;
			}
			set
			{
				if (value != (int)this._targetEquipmentIndex)
				{
					this._targetEquipmentIndex = (EquipmentIndex)value;
					base.OnPropertyChangedWithValue(value, "TargetEquipmentIndex");
				}
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06000CD0 RID: 3280 RVA: 0x0003583A File Offset: 0x00033A3A
		// (set) Token: 0x06000CD1 RID: 3281 RVA: 0x00035842 File Offset: 0x00033A42
		public EquipmentIndex TargetEquipmentType
		{
			get
			{
				return this._targetEquipmentIndex;
			}
			set
			{
				if (value != this._targetEquipmentIndex)
				{
					this._targetEquipmentIndex = value;
					base.OnPropertyChanged("TargetEquipmentIndex");
				}
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06000CD2 RID: 3282 RVA: 0x0003585F File Offset: 0x00033A5F
		// (set) Token: 0x06000CD3 RID: 3283 RVA: 0x00035867 File Offset: 0x00033A67
		[DataSourceProperty]
		public int TransactionCount
		{
			get
			{
				return this._transactionCount;
			}
			set
			{
				if (value != this._transactionCount)
				{
					this._transactionCount = value;
					base.OnPropertyChangedWithValue(value, "TransactionCount");
				}
				this.RefreshTransactionCost(value);
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06000CD4 RID: 3284 RVA: 0x0003588C File Offset: 0x00033A8C
		// (set) Token: 0x06000CD5 RID: 3285 RVA: 0x00035894 File Offset: 0x00033A94
		[DataSourceProperty]
		public bool IsTrading
		{
			get
			{
				return this._isTrading;
			}
			set
			{
				if (value != this._isTrading)
				{
					this._isTrading = value;
					base.OnPropertyChangedWithValue(value, "IsTrading");
				}
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000CD6 RID: 3286 RVA: 0x000358B2 File Offset: 0x00033AB2
		// (set) Token: 0x06000CD7 RID: 3287 RVA: 0x000358BA File Offset: 0x00033ABA
		[DataSourceProperty]
		public bool EquipAfterBuy
		{
			get
			{
				return this._equipAfterBuy;
			}
			set
			{
				if (value != this._equipAfterBuy)
				{
					this._equipAfterBuy = value;
					base.OnPropertyChangedWithValue(value, "EquipAfterBuy");
				}
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000CD8 RID: 3288 RVA: 0x000358D8 File Offset: 0x00033AD8
		// (set) Token: 0x06000CD9 RID: 3289 RVA: 0x000358E0 File Offset: 0x00033AE0
		[DataSourceProperty]
		public string TradeLbl
		{
			get
			{
				return this._tradeLbl;
			}
			set
			{
				if (value != this._tradeLbl)
				{
					this._tradeLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "TradeLbl");
				}
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000CDA RID: 3290 RVA: 0x00035903 File Offset: 0x00033B03
		// (set) Token: 0x06000CDB RID: 3291 RVA: 0x0003590B File Offset: 0x00033B0B
		[DataSourceProperty]
		public string ExperienceLbl
		{
			get
			{
				return this._experienceLbl;
			}
			set
			{
				if (value != this._experienceLbl)
				{
					this._experienceLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "ExperienceLbl");
				}
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000CDC RID: 3292 RVA: 0x0003592E File Offset: 0x00033B2E
		// (set) Token: 0x06000CDD RID: 3293 RVA: 0x00035936 File Offset: 0x00033B36
		[DataSourceProperty]
		public string CurrentCharacterName
		{
			get
			{
				return this._currentCharacterName;
			}
			set
			{
				if (value != this._currentCharacterName)
				{
					this._currentCharacterName = value;
					base.OnPropertyChangedWithValue<string>(value, "CurrentCharacterName");
				}
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000CDE RID: 3294 RVA: 0x00035959 File Offset: 0x00033B59
		// (set) Token: 0x06000CDF RID: 3295 RVA: 0x00035961 File Offset: 0x00033B61
		[DataSourceProperty]
		public string RightInventoryOwnerName
		{
			get
			{
				return this._rightInventoryOwnerName;
			}
			set
			{
				if (value != this._rightInventoryOwnerName)
				{
					this._rightInventoryOwnerName = value;
					base.OnPropertyChangedWithValue<string>(value, "RightInventoryOwnerName");
				}
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000CE0 RID: 3296 RVA: 0x00035984 File Offset: 0x00033B84
		// (set) Token: 0x06000CE1 RID: 3297 RVA: 0x0003598C File Offset: 0x00033B8C
		[DataSourceProperty]
		public string LeftInventoryOwnerName
		{
			get
			{
				return this._leftInventoryOwnerName;
			}
			set
			{
				if (value != this._leftInventoryOwnerName)
				{
					this._leftInventoryOwnerName = value;
					base.OnPropertyChangedWithValue<string>(value, "LeftInventoryOwnerName");
				}
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000CE2 RID: 3298 RVA: 0x000359AF File Offset: 0x00033BAF
		// (set) Token: 0x06000CE3 RID: 3299 RVA: 0x000359B7 File Offset: 0x00033BB7
		[DataSourceProperty]
		public int RightInventoryOwnerGold
		{
			get
			{
				return this._rightInventoryOwnerGold;
			}
			set
			{
				if (value != this._rightInventoryOwnerGold)
				{
					this._rightInventoryOwnerGold = value;
					base.OnPropertyChangedWithValue(value, "RightInventoryOwnerGold");
				}
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000CE4 RID: 3300 RVA: 0x000359D5 File Offset: 0x00033BD5
		// (set) Token: 0x06000CE5 RID: 3301 RVA: 0x000359DD File Offset: 0x00033BDD
		[DataSourceProperty]
		public int LeftInventoryOwnerGold
		{
			get
			{
				return this._leftInventoryOwnerGold;
			}
			set
			{
				if (value != this._leftInventoryOwnerGold)
				{
					this._leftInventoryOwnerGold = value;
					base.OnPropertyChangedWithValue(value, "LeftInventoryOwnerGold");
				}
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000CE6 RID: 3302 RVA: 0x000359FB File Offset: 0x00033BFB
		// (set) Token: 0x06000CE7 RID: 3303 RVA: 0x00035A03 File Offset: 0x00033C03
		[DataSourceProperty]
		public int ItemCountToBuy
		{
			get
			{
				return this._itemCountToBuy;
			}
			set
			{
				if (value != this._itemCountToBuy)
				{
					this._itemCountToBuy = value;
					base.OnPropertyChangedWithValue(value, "ItemCountToBuy");
				}
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000CE8 RID: 3304 RVA: 0x00035A21 File Offset: 0x00033C21
		// (set) Token: 0x06000CE9 RID: 3305 RVA: 0x00035A29 File Offset: 0x00033C29
		[DataSourceProperty]
		public string CurrentCharacterTotalEncumbrance
		{
			get
			{
				return this._currentCharacterTotalEncumbrance;
			}
			set
			{
				if (value != this._currentCharacterTotalEncumbrance)
				{
					this._currentCharacterTotalEncumbrance = value;
					base.OnPropertyChangedWithValue<string>(value, "CurrentCharacterTotalEncumbrance");
				}
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000CEA RID: 3306 RVA: 0x00035A4C File Offset: 0x00033C4C
		// (set) Token: 0x06000CEB RID: 3307 RVA: 0x00035A54 File Offset: 0x00033C54
		[DataSourceProperty]
		public float CurrentCharacterLegArmor
		{
			get
			{
				return this._currentCharacterLegArmor;
			}
			set
			{
				if (MathF.Abs(value - this._currentCharacterLegArmor) > 0.01f)
				{
					this._currentCharacterLegArmor = value;
					base.OnPropertyChangedWithValue(value, "CurrentCharacterLegArmor");
				}
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06000CEC RID: 3308 RVA: 0x00035A7D File Offset: 0x00033C7D
		// (set) Token: 0x06000CED RID: 3309 RVA: 0x00035A85 File Offset: 0x00033C85
		[DataSourceProperty]
		public float CurrentCharacterHeadArmor
		{
			get
			{
				return this._currentCharacterHeadArmor;
			}
			set
			{
				if (MathF.Abs(value - this._currentCharacterHeadArmor) > 0.01f)
				{
					this._currentCharacterHeadArmor = value;
					base.OnPropertyChangedWithValue(value, "CurrentCharacterHeadArmor");
				}
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000CEE RID: 3310 RVA: 0x00035AAE File Offset: 0x00033CAE
		// (set) Token: 0x06000CEF RID: 3311 RVA: 0x00035AB6 File Offset: 0x00033CB6
		[DataSourceProperty]
		public float CurrentCharacterBodyArmor
		{
			get
			{
				return this._currentCharacterBodyArmor;
			}
			set
			{
				if (MathF.Abs(value - this._currentCharacterBodyArmor) > 0.01f)
				{
					this._currentCharacterBodyArmor = value;
					base.OnPropertyChangedWithValue(value, "CurrentCharacterBodyArmor");
				}
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06000CF0 RID: 3312 RVA: 0x00035ADF File Offset: 0x00033CDF
		// (set) Token: 0x06000CF1 RID: 3313 RVA: 0x00035AE7 File Offset: 0x00033CE7
		[DataSourceProperty]
		public float CurrentCharacterArmArmor
		{
			get
			{
				return this._currentCharacterArmArmor;
			}
			set
			{
				if (MathF.Abs(value - this._currentCharacterArmArmor) > 0.01f)
				{
					this._currentCharacterArmArmor = value;
					base.OnPropertyChangedWithValue(value, "CurrentCharacterArmArmor");
				}
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x00035B10 File Offset: 0x00033D10
		// (set) Token: 0x06000CF3 RID: 3315 RVA: 0x00035B18 File Offset: 0x00033D18
		[DataSourceProperty]
		public float CurrentCharacterHorseArmor
		{
			get
			{
				return this._currentCharacterHorseArmor;
			}
			set
			{
				if (MathF.Abs(value - this._currentCharacterHorseArmor) > 0.01f)
				{
					this._currentCharacterHorseArmor = value;
					base.OnPropertyChangedWithValue(value, "CurrentCharacterHorseArmor");
				}
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x00035B41 File Offset: 0x00033D41
		// (set) Token: 0x06000CF5 RID: 3317 RVA: 0x00035B49 File Offset: 0x00033D49
		[DataSourceProperty]
		public bool IsRefreshed
		{
			get
			{
				return this._isRefreshed;
			}
			set
			{
				if (this._isRefreshed != value)
				{
					this._isRefreshed = value;
					base.OnPropertyChangedWithValue(value, "IsRefreshed");
				}
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x00035B67 File Offset: 0x00033D67
		// (set) Token: 0x06000CF7 RID: 3319 RVA: 0x00035B6F File Offset: 0x00033D6F
		[DataSourceProperty]
		public bool IsExtendedEquipmentControlsEnabled
		{
			get
			{
				return this._isExtendedEquipmentControlsEnabled;
			}
			set
			{
				if (value != this._isExtendedEquipmentControlsEnabled)
				{
					this._isExtendedEquipmentControlsEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsExtendedEquipmentControlsEnabled");
				}
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06000CF8 RID: 3320 RVA: 0x00035B8D File Offset: 0x00033D8D
		// (set) Token: 0x06000CF9 RID: 3321 RVA: 0x00035B95 File Offset: 0x00033D95
		[DataSourceProperty]
		public bool IsFocusedOnItemList
		{
			get
			{
				return this._isFocusedOnItemList;
			}
			set
			{
				if (value != this._isFocusedOnItemList)
				{
					this._isFocusedOnItemList = value;
					base.OnPropertyChangedWithValue(value, "IsFocusedOnItemList");
				}
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06000CFA RID: 3322 RVA: 0x00035BB3 File Offset: 0x00033DB3
		// (set) Token: 0x06000CFB RID: 3323 RVA: 0x00035BBB File Offset: 0x00033DBB
		[DataSourceProperty]
		public SPItemVM CurrentFocusedItem
		{
			get
			{
				return this._currentFocusedItem;
			}
			set
			{
				if (value != this._currentFocusedItem)
				{
					this._currentFocusedItem = value;
					base.OnPropertyChangedWithValue<SPItemVM>(value, "CurrentFocusedItem");
				}
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06000CFC RID: 3324 RVA: 0x00035BD9 File Offset: 0x00033DD9
		// (set) Token: 0x06000CFD RID: 3325 RVA: 0x00035BE1 File Offset: 0x00033DE1
		[DataSourceProperty]
		public SPItemVM CharacterHelmSlot
		{
			get
			{
				return this._characterHelmSlot;
			}
			set
			{
				if (value != this._characterHelmSlot)
				{
					this._characterHelmSlot = value;
					base.OnPropertyChangedWithValue<SPItemVM>(value, "CharacterHelmSlot");
				}
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06000CFE RID: 3326 RVA: 0x00035BFF File Offset: 0x00033DFF
		// (set) Token: 0x06000CFF RID: 3327 RVA: 0x00035C07 File Offset: 0x00033E07
		[DataSourceProperty]
		public SPItemVM CharacterCloakSlot
		{
			get
			{
				return this._characterCloakSlot;
			}
			set
			{
				if (value != this._characterCloakSlot)
				{
					this._characterCloakSlot = value;
					base.OnPropertyChangedWithValue<SPItemVM>(value, "CharacterCloakSlot");
				}
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06000D00 RID: 3328 RVA: 0x00035C25 File Offset: 0x00033E25
		// (set) Token: 0x06000D01 RID: 3329 RVA: 0x00035C2D File Offset: 0x00033E2D
		[DataSourceProperty]
		public SPItemVM CharacterTorsoSlot
		{
			get
			{
				return this._characterTorsoSlot;
			}
			set
			{
				if (value != this._characterTorsoSlot)
				{
					this._characterTorsoSlot = value;
					base.OnPropertyChangedWithValue<SPItemVM>(value, "CharacterTorsoSlot");
				}
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06000D02 RID: 3330 RVA: 0x00035C4B File Offset: 0x00033E4B
		// (set) Token: 0x06000D03 RID: 3331 RVA: 0x00035C53 File Offset: 0x00033E53
		[DataSourceProperty]
		public SPItemVM CharacterGloveSlot
		{
			get
			{
				return this._characterGloveSlot;
			}
			set
			{
				if (value != this._characterGloveSlot)
				{
					this._characterGloveSlot = value;
					base.OnPropertyChangedWithValue<SPItemVM>(value, "CharacterGloveSlot");
				}
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06000D04 RID: 3332 RVA: 0x00035C71 File Offset: 0x00033E71
		// (set) Token: 0x06000D05 RID: 3333 RVA: 0x00035C79 File Offset: 0x00033E79
		[DataSourceProperty]
		public SPItemVM CharacterBootSlot
		{
			get
			{
				return this._characterBootSlot;
			}
			set
			{
				if (value != this._characterBootSlot)
				{
					this._characterBootSlot = value;
					base.OnPropertyChangedWithValue<SPItemVM>(value, "CharacterBootSlot");
				}
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06000D06 RID: 3334 RVA: 0x00035C97 File Offset: 0x00033E97
		// (set) Token: 0x06000D07 RID: 3335 RVA: 0x00035C9F File Offset: 0x00033E9F
		[DataSourceProperty]
		public SPItemVM CharacterMountSlot
		{
			get
			{
				return this._characterMountSlot;
			}
			set
			{
				if (value != this._characterMountSlot)
				{
					this._characterMountSlot = value;
					base.OnPropertyChangedWithValue<SPItemVM>(value, "CharacterMountSlot");
				}
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06000D08 RID: 3336 RVA: 0x00035CBD File Offset: 0x00033EBD
		// (set) Token: 0x06000D09 RID: 3337 RVA: 0x00035CC5 File Offset: 0x00033EC5
		[DataSourceProperty]
		public SPItemVM CharacterMountArmorSlot
		{
			get
			{
				return this._characterMountArmorSlot;
			}
			set
			{
				if (value != this._characterMountArmorSlot)
				{
					this._characterMountArmorSlot = value;
					base.OnPropertyChangedWithValue<SPItemVM>(value, "CharacterMountArmorSlot");
				}
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06000D0A RID: 3338 RVA: 0x00035CE3 File Offset: 0x00033EE3
		// (set) Token: 0x06000D0B RID: 3339 RVA: 0x00035CEB File Offset: 0x00033EEB
		[DataSourceProperty]
		public SPItemVM CharacterWeapon1Slot
		{
			get
			{
				return this._characterWeapon1Slot;
			}
			set
			{
				if (value != this._characterWeapon1Slot)
				{
					this._characterWeapon1Slot = value;
					base.OnPropertyChangedWithValue<SPItemVM>(value, "CharacterWeapon1Slot");
				}
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06000D0C RID: 3340 RVA: 0x00035D09 File Offset: 0x00033F09
		// (set) Token: 0x06000D0D RID: 3341 RVA: 0x00035D11 File Offset: 0x00033F11
		[DataSourceProperty]
		public SPItemVM CharacterWeapon2Slot
		{
			get
			{
				return this._characterWeapon2Slot;
			}
			set
			{
				if (value != this._characterWeapon2Slot)
				{
					this._characterWeapon2Slot = value;
					base.OnPropertyChangedWithValue<SPItemVM>(value, "CharacterWeapon2Slot");
				}
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06000D0E RID: 3342 RVA: 0x00035D2F File Offset: 0x00033F2F
		// (set) Token: 0x06000D0F RID: 3343 RVA: 0x00035D37 File Offset: 0x00033F37
		[DataSourceProperty]
		public SPItemVM CharacterWeapon3Slot
		{
			get
			{
				return this._characterWeapon3Slot;
			}
			set
			{
				if (value != this._characterWeapon3Slot)
				{
					this._characterWeapon3Slot = value;
					base.OnPropertyChangedWithValue<SPItemVM>(value, "CharacterWeapon3Slot");
				}
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000D10 RID: 3344 RVA: 0x00035D55 File Offset: 0x00033F55
		// (set) Token: 0x06000D11 RID: 3345 RVA: 0x00035D5D File Offset: 0x00033F5D
		[DataSourceProperty]
		public SPItemVM CharacterWeapon4Slot
		{
			get
			{
				return this._characterWeapon4Slot;
			}
			set
			{
				if (value != this._characterWeapon4Slot)
				{
					this._characterWeapon4Slot = value;
					base.OnPropertyChangedWithValue<SPItemVM>(value, "CharacterWeapon4Slot");
				}
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06000D12 RID: 3346 RVA: 0x00035D7B File Offset: 0x00033F7B
		// (set) Token: 0x06000D13 RID: 3347 RVA: 0x00035D83 File Offset: 0x00033F83
		[DataSourceProperty]
		public SPItemVM CharacterBannerSlot
		{
			get
			{
				return this._characterBannerSlot;
			}
			set
			{
				if (value != this._characterBannerSlot)
				{
					this._characterBannerSlot = value;
					base.OnPropertyChangedWithValue<SPItemVM>(value, "CharacterBannerSlot");
				}
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06000D14 RID: 3348 RVA: 0x00035DA1 File Offset: 0x00033FA1
		// (set) Token: 0x06000D15 RID: 3349 RVA: 0x00035DA9 File Offset: 0x00033FA9
		[DataSourceProperty]
		public HeroViewModel MainCharacter
		{
			get
			{
				return this._mainCharacter;
			}
			set
			{
				if (value != this._mainCharacter)
				{
					this._mainCharacter = value;
					base.OnPropertyChangedWithValue<HeroViewModel>(value, "MainCharacter");
				}
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06000D16 RID: 3350 RVA: 0x00035DC7 File Offset: 0x00033FC7
		// (set) Token: 0x06000D17 RID: 3351 RVA: 0x00035DCF File Offset: 0x00033FCF
		[DataSourceProperty]
		public MBBindingList<SPItemVM> RightItemListVM
		{
			get
			{
				return this._rightItemListVM;
			}
			set
			{
				if (value != this._rightItemListVM)
				{
					this._rightItemListVM = value;
					base.OnPropertyChangedWithValue<MBBindingList<SPItemVM>>(value, "RightItemListVM");
				}
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06000D18 RID: 3352 RVA: 0x00035DED File Offset: 0x00033FED
		// (set) Token: 0x06000D19 RID: 3353 RVA: 0x00035DF5 File Offset: 0x00033FF5
		[DataSourceProperty]
		public MBBindingList<SPItemVM> LeftItemListVM
		{
			get
			{
				return this._leftItemListVM;
			}
			set
			{
				if (value != this._leftItemListVM)
				{
					this._leftItemListVM = value;
					base.OnPropertyChangedWithValue<MBBindingList<SPItemVM>>(value, "LeftItemListVM");
				}
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06000D1A RID: 3354 RVA: 0x00035E13 File Offset: 0x00034013
		// (set) Token: 0x06000D1B RID: 3355 RVA: 0x00035E1B File Offset: 0x0003401B
		[DataSourceProperty]
		public bool IsBannerItemsHighlightApplied
		{
			get
			{
				return this._isBannerItemsHighlightApplied;
			}
			set
			{
				if (value != this._isBannerItemsHighlightApplied)
				{
					this._isBannerItemsHighlightApplied = value;
					base.OnPropertyChangedWithValue(value, "IsBannerItemsHighlightApplied");
				}
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06000D1C RID: 3356 RVA: 0x00035E39 File Offset: 0x00034039
		// (set) Token: 0x06000D1D RID: 3357 RVA: 0x00035E41 File Offset: 0x00034041
		[DataSourceProperty]
		public int BannerTypeCode
		{
			get
			{
				return this._bannerTypeCode;
			}
			set
			{
				if (value != this._bannerTypeCode)
				{
					this._bannerTypeCode = value;
					base.OnPropertyChangedWithValue(value, "BannerTypeCode");
				}
			}
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x00035E5F File Offset: 0x0003405F
		private TextObject GetPreviousCharacterKeyText()
		{
			if (this.PreviousCharacterInputKey == null || this._getKeyTextFromKeyId == null)
			{
				return TextObject.Empty;
			}
			return this._getKeyTextFromKeyId(this.PreviousCharacterInputKey.KeyID);
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x00035E8D File Offset: 0x0003408D
		private TextObject GetNextCharacterKeyText()
		{
			if (this.NextCharacterInputKey == null || this._getKeyTextFromKeyId == null)
			{
				return TextObject.Empty;
			}
			return this._getKeyTextFromKeyId(this.NextCharacterInputKey.KeyID);
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x00035EBB File Offset: 0x000340BB
		private TextObject GetBuyAllKeyText()
		{
			if (this.BuyAllInputKey == null || this._getKeyTextFromKeyId == null)
			{
				return TextObject.Empty;
			}
			return this._getKeyTextFromKeyId(this.BuyAllInputKey.KeyID);
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x00035EE9 File Offset: 0x000340E9
		private TextObject GetSellAllKeyText()
		{
			if (this.SellAllInputKey == null || this._getKeyTextFromKeyId == null)
			{
				return TextObject.Empty;
			}
			return this._getKeyTextFromKeyId(this.SellAllInputKey.KeyID);
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x00035F17 File Offset: 0x00034117
		public void SetResetInputKey(HotKey hotkey)
		{
			this.ResetInputKey = InputKeyItemVM.CreateFromHotKey(hotkey, true);
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x00035F26 File Offset: 0x00034126
		public void SetCancelInputKey(HotKey gameKey)
		{
			this.CancelInputKey = InputKeyItemVM.CreateFromHotKey(gameKey, true);
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x00035F35 File Offset: 0x00034135
		public void SetDoneInputKey(HotKey hotKey)
		{
			this.DoneInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x00035F44 File Offset: 0x00034144
		public void SetPreviousCharacterInputKey(HotKey hotKey)
		{
			this.PreviousCharacterInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
			this.SetPreviousCharacterHint();
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x00035F59 File Offset: 0x00034159
		public void SetNextCharacterInputKey(HotKey hotKey)
		{
			this.NextCharacterInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
			this.SetNextCharacterHint();
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x00035F6E File Offset: 0x0003416E
		public void SetBuyAllInputKey(HotKey hotKey)
		{
			this.BuyAllInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
			this.SetBuyAllHint();
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x00035F83 File Offset: 0x00034183
		public void SetSellAllInputKey(HotKey hotKey)
		{
			this.SellAllInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
			this.SetSellAllHint();
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x00035F98 File Offset: 0x00034198
		public void SetGetKeyTextFromKeyIDFunc(Func<string, TextObject> getKeyTextFromKeyId)
		{
			this._getKeyTextFromKeyId = getKeyTextFromKeyId;
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06000D2A RID: 3370 RVA: 0x00035FA1 File Offset: 0x000341A1
		// (set) Token: 0x06000D2B RID: 3371 RVA: 0x00035FA9 File Offset: 0x000341A9
		[DataSourceProperty]
		public InputKeyItemVM ResetInputKey
		{
			get
			{
				return this._resetInputKey;
			}
			set
			{
				if (value != this._resetInputKey)
				{
					this._resetInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "ResetInputKey");
				}
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06000D2C RID: 3372 RVA: 0x00035FC7 File Offset: 0x000341C7
		// (set) Token: 0x06000D2D RID: 3373 RVA: 0x00035FCF File Offset: 0x000341CF
		[DataSourceProperty]
		public InputKeyItemVM CancelInputKey
		{
			get
			{
				return this._cancelInputKey;
			}
			set
			{
				if (value != this._cancelInputKey)
				{
					this._cancelInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "CancelInputKey");
				}
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06000D2E RID: 3374 RVA: 0x00035FED File Offset: 0x000341ED
		// (set) Token: 0x06000D2F RID: 3375 RVA: 0x00035FF5 File Offset: 0x000341F5
		[DataSourceProperty]
		public InputKeyItemVM DoneInputKey
		{
			get
			{
				return this._doneInputKey;
			}
			set
			{
				if (value != this._doneInputKey)
				{
					this._doneInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "DoneInputKey");
				}
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06000D30 RID: 3376 RVA: 0x00036013 File Offset: 0x00034213
		// (set) Token: 0x06000D31 RID: 3377 RVA: 0x0003601B File Offset: 0x0003421B
		[DataSourceProperty]
		public InputKeyItemVM PreviousCharacterInputKey
		{
			get
			{
				return this._previousCharacterInputKey;
			}
			set
			{
				if (value != this._previousCharacterInputKey)
				{
					this._previousCharacterInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "PreviousCharacterInputKey");
				}
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06000D32 RID: 3378 RVA: 0x00036039 File Offset: 0x00034239
		// (set) Token: 0x06000D33 RID: 3379 RVA: 0x00036041 File Offset: 0x00034241
		[DataSourceProperty]
		public InputKeyItemVM NextCharacterInputKey
		{
			get
			{
				return this._nextCharacterInputKey;
			}
			set
			{
				if (value != this._nextCharacterInputKey)
				{
					this._nextCharacterInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "NextCharacterInputKey");
				}
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06000D34 RID: 3380 RVA: 0x0003605F File Offset: 0x0003425F
		// (set) Token: 0x06000D35 RID: 3381 RVA: 0x00036067 File Offset: 0x00034267
		[DataSourceProperty]
		public InputKeyItemVM BuyAllInputKey
		{
			get
			{
				return this._buyAllInputKey;
			}
			set
			{
				if (value != this._buyAllInputKey)
				{
					this._buyAllInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "BuyAllInputKey");
				}
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06000D36 RID: 3382 RVA: 0x00036085 File Offset: 0x00034285
		// (set) Token: 0x06000D37 RID: 3383 RVA: 0x0003608D File Offset: 0x0003428D
		[DataSourceProperty]
		public InputKeyItemVM SellAllInputKey
		{
			get
			{
				return this._sellAllInputKey;
			}
			set
			{
				if (value != this._sellAllInputKey)
				{
					this._sellAllInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "SellAllInputKey");
				}
			}
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x000360AB File Offset: 0x000342AB
		void IInventoryStateHandler.ExecuteLootingScript()
		{
			this.ExecuteBuyAllItems();
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x000360B3 File Offset: 0x000342B3
		void IInventoryStateHandler.ExecuteBuyConsumableItem()
		{
			this.ExecuteBuyItemTest();
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x000360BC File Offset: 0x000342BC
		void IInventoryStateHandler.ExecuteSellAllLoot()
		{
			for (int i = this.RightItemListVM.Count - 1; i >= 0; i--)
			{
				SPItemVM spitemVM = this.RightItemListVM[i];
				if (spitemVM.GetItemTypeWithItemObject() != EquipmentIndex.None)
				{
					this.SellItem(spitemVM);
				}
			}
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x00036100 File Offset: 0x00034300
		void IInventoryStateHandler.FilterInventoryAtOpening(InventoryManager.InventoryCategoryType inventoryCategoryType)
		{
			if (Game.Current == null)
			{
				Debug.FailedAssert("Game is not initialized when filtering inventory", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem.ViewModelCollection\\Inventory\\SPInventoryVM.cs", "FilterInventoryAtOpening", 4700);
				return;
			}
			switch (inventoryCategoryType)
			{
			case InventoryManager.InventoryCategoryType.Armors:
				this.ExecuteFilterArmors();
				return;
			case InventoryManager.InventoryCategoryType.Weapon:
				this.ExecuteFilterWeapons();
				return;
			case InventoryManager.InventoryCategoryType.Shield:
				break;
			case InventoryManager.InventoryCategoryType.HorseCategory:
				this.ExecuteFilterMounts();
				return;
			case InventoryManager.InventoryCategoryType.Goods:
				this.ExecuteFilterMisc();
				break;
			default:
				return;
			}
		}

		// Token: 0x0400057A RID: 1402
		public bool DoNotSync;

		// Token: 0x0400057B RID: 1403
		private readonly Func<WeaponComponentData, ItemObject.ItemUsageSetFlags> _getItemUsageSetFlags;

		// Token: 0x0400057C RID: 1404
		private readonly IViewDataTracker _viewDataTracker;

		// Token: 0x0400057D RID: 1405
		public bool IsFiveStackModifierActive;

		// Token: 0x0400057E RID: 1406
		public bool IsEntireStackModifierActive;

		// Token: 0x0400057F RID: 1407
		private readonly int _donationMaxShareableXp;

		// Token: 0x04000580 RID: 1408
		private readonly TroopRoster _rightTroopRoster;

		// Token: 0x04000581 RID: 1409
		private InventoryMode _usageType = InventoryMode.Trade;

		// Token: 0x04000582 RID: 1410
		private readonly TroopRoster _leftTroopRoster;

		// Token: 0x04000583 RID: 1411
		private int _lastComparedItemIndex;

		// Token: 0x04000584 RID: 1412
		private readonly Stack<SPItemVM> _equipAfterTransferStack;

		// Token: 0x04000585 RID: 1413
		private int _currentInventoryCharacterIndex;

		// Token: 0x04000586 RID: 1414
		private bool _isTrading;

		// Token: 0x04000587 RID: 1415
		private bool _isFinalized;

		// Token: 0x04000588 RID: 1416
		private bool _isCharacterEquipmentDirty;

		// Token: 0x04000589 RID: 1417
		private float _equipmentCount;

		// Token: 0x0400058A RID: 1418
		private string _selectedTooltipItemStringID = "";

		// Token: 0x0400058B RID: 1419
		private string _comparedTooltipItemStringID = "";

		// Token: 0x0400058C RID: 1420
		private InventoryLogic _inventoryLogic;

		// Token: 0x0400058D RID: 1421
		private CharacterObject _currentCharacter;

		// Token: 0x0400058E RID: 1422
		private SPItemVM _selectedItem;

		// Token: 0x0400058F RID: 1423
		private string _fiveStackShortcutkeyText;

		// Token: 0x04000590 RID: 1424
		private string _entireStackShortcutkeyText;

		// Token: 0x04000591 RID: 1425
		private List<ItemVM> _comparedItemList;

		// Token: 0x04000592 RID: 1426
		private List<string> _lockedItemIDs;

		// Token: 0x04000593 RID: 1427
		private Func<string, TextObject> _getKeyTextFromKeyId;

		// Token: 0x04000594 RID: 1428
		private readonly List<int> _everyItemType = new List<int>
		{
			1,
			2,
			3,
			4,
			5,
			6,
			7,
			8,
			9,
			10,
			11,
			12,
			13,
			14,
			15,
			16,
			17,
			18,
			19,
			20,
			21,
			22,
			23,
			24
		};

		// Token: 0x04000595 RID: 1429
		private readonly List<int> _weaponItemTypes = new List<int>
		{
			2,
			3,
			4
		};

		// Token: 0x04000596 RID: 1430
		private readonly List<int> _armorItemTypes = new List<int>
		{
			12,
			13,
			14,
			15,
			21,
			22
		};

		// Token: 0x04000597 RID: 1431
		private readonly List<int> _mountItemTypes = new List<int>
		{
			1,
			23
		};

		// Token: 0x04000598 RID: 1432
		private readonly List<int> _shieldAndRangedItemTypes = new List<int>
		{
			7,
			5,
			6,
			8,
			9,
			10,
			16,
			17,
			18
		};

		// Token: 0x04000599 RID: 1433
		private readonly List<int> _miscellaneousItemTypes = new List<int>
		{
			11,
			19,
			20,
			24
		};

		// Token: 0x0400059A RID: 1434
		private readonly Dictionary<SPInventoryVM.Filters, List<int>> _filters;

		// Token: 0x0400059B RID: 1435
		private int _selectedEquipmentIndex;

		// Token: 0x0400059C RID: 1436
		private bool _isFoodTransferButtonHighlightApplied;

		// Token: 0x0400059D RID: 1437
		private bool _isBannerItemsHighlightApplied;

		// Token: 0x0400059E RID: 1438
		private string _latestTutorialElementID;

		// Token: 0x0400059F RID: 1439
		private string _leftInventoryLabel;

		// Token: 0x040005A0 RID: 1440
		private string _rightInventoryLabel;

		// Token: 0x040005A1 RID: 1441
		private bool _otherSideHasCapacity;

		// Token: 0x040005A2 RID: 1442
		private bool _isDoneDisabled;

		// Token: 0x040005A3 RID: 1443
		private bool _isSearchAvailable;

		// Token: 0x040005A4 RID: 1444
		private bool _isOtherInventoryGoldRelevant;

		// Token: 0x040005A5 RID: 1445
		private string _doneLbl;

		// Token: 0x040005A6 RID: 1446
		private string _cancelLbl;

		// Token: 0x040005A7 RID: 1447
		private string _resetLbl;

		// Token: 0x040005A8 RID: 1448
		private string _typeText;

		// Token: 0x040005A9 RID: 1449
		private string _nameText;

		// Token: 0x040005AA RID: 1450
		private string _quantityText;

		// Token: 0x040005AB RID: 1451
		private string _costText;

		// Token: 0x040005AC RID: 1452
		private string _searchPlaceholderText;

		// Token: 0x040005AD RID: 1453
		private HintViewModel _resetHint;

		// Token: 0x040005AE RID: 1454
		private HintViewModel _filterAllHint;

		// Token: 0x040005AF RID: 1455
		private HintViewModel _filterWeaponHint;

		// Token: 0x040005B0 RID: 1456
		private HintViewModel _filterArmorHint;

		// Token: 0x040005B1 RID: 1457
		private HintViewModel _filterShieldAndRangedHint;

		// Token: 0x040005B2 RID: 1458
		private HintViewModel _filterMountAndHarnessHint;

		// Token: 0x040005B3 RID: 1459
		private HintViewModel _filterMiscHint;

		// Token: 0x040005B4 RID: 1460
		private HintViewModel _civilianOutfitHint;

		// Token: 0x040005B5 RID: 1461
		private HintViewModel _battleOutfitHint;

		// Token: 0x040005B6 RID: 1462
		private HintViewModel _equipmentHelmSlotHint;

		// Token: 0x040005B7 RID: 1463
		private HintViewModel _equipmentArmorSlotHint;

		// Token: 0x040005B8 RID: 1464
		private HintViewModel _equipmentBootSlotHint;

		// Token: 0x040005B9 RID: 1465
		private HintViewModel _equipmentCloakSlotHint;

		// Token: 0x040005BA RID: 1466
		private HintViewModel _equipmentGloveSlotHint;

		// Token: 0x040005BB RID: 1467
		private HintViewModel _equipmentHarnessSlotHint;

		// Token: 0x040005BC RID: 1468
		private HintViewModel _equipmentMountSlotHint;

		// Token: 0x040005BD RID: 1469
		private HintViewModel _equipmentWeaponSlotHint;

		// Token: 0x040005BE RID: 1470
		private HintViewModel _equipmentBannerSlotHint;

		// Token: 0x040005BF RID: 1471
		private BasicTooltipViewModel _buyAllHint;

		// Token: 0x040005C0 RID: 1472
		private BasicTooltipViewModel _sellAllHint;

		// Token: 0x040005C1 RID: 1473
		private BasicTooltipViewModel _previousCharacterHint;

		// Token: 0x040005C2 RID: 1474
		private BasicTooltipViewModel _nextCharacterHint;

		// Token: 0x040005C3 RID: 1475
		private HintViewModel _weightHint;

		// Token: 0x040005C4 RID: 1476
		private HintViewModel _armArmorHint;

		// Token: 0x040005C5 RID: 1477
		private HintViewModel _bodyArmorHint;

		// Token: 0x040005C6 RID: 1478
		private HintViewModel _headArmorHint;

		// Token: 0x040005C7 RID: 1479
		private HintViewModel _legArmorHint;

		// Token: 0x040005C8 RID: 1480
		private HintViewModel _horseArmorHint;

		// Token: 0x040005C9 RID: 1481
		private HintViewModel _previewHint;

		// Token: 0x040005CA RID: 1482
		private HintViewModel _equipHint;

		// Token: 0x040005CB RID: 1483
		private HintViewModel _unequipHint;

		// Token: 0x040005CC RID: 1484
		private HintViewModel _sellHint;

		// Token: 0x040005CD RID: 1485
		private HintViewModel _playerSideCapacityExceededHint;

		// Token: 0x040005CE RID: 1486
		private HintViewModel _noSaddleHint;

		// Token: 0x040005CF RID: 1487
		private HintViewModel _donationLblHint;

		// Token: 0x040005D0 RID: 1488
		private HintViewModel _otherSideCapacityExceededHint;

		// Token: 0x040005D1 RID: 1489
		private BasicTooltipViewModel _equipmentMaxCountHint;

		// Token: 0x040005D2 RID: 1490
		private BasicTooltipViewModel _currentCharacterSkillsTooltip;

		// Token: 0x040005D3 RID: 1491
		private BasicTooltipViewModel _productionTooltip;

		// Token: 0x040005D4 RID: 1492
		private HeroViewModel _mainCharacter;

		// Token: 0x040005D5 RID: 1493
		private bool _isExtendedEquipmentControlsEnabled;

		// Token: 0x040005D6 RID: 1494
		private bool _isFocusedOnItemList;

		// Token: 0x040005D7 RID: 1495
		private SPItemVM _currentFocusedItem;

		// Token: 0x040005D8 RID: 1496
		private bool _equipAfterBuy;

		// Token: 0x040005D9 RID: 1497
		private MBBindingList<SPItemVM> _leftItemListVM;

		// Token: 0x040005DA RID: 1498
		private MBBindingList<SPItemVM> _rightItemListVM;

		// Token: 0x040005DB RID: 1499
		private ItemMenuVM _itemMenu;

		// Token: 0x040005DC RID: 1500
		private SPItemVM _characterHelmSlot;

		// Token: 0x040005DD RID: 1501
		private SPItemVM _characterCloakSlot;

		// Token: 0x040005DE RID: 1502
		private SPItemVM _characterTorsoSlot;

		// Token: 0x040005DF RID: 1503
		private SPItemVM _characterGloveSlot;

		// Token: 0x040005E0 RID: 1504
		private SPItemVM _characterBootSlot;

		// Token: 0x040005E1 RID: 1505
		private SPItemVM _characterMountSlot;

		// Token: 0x040005E2 RID: 1506
		private SPItemVM _characterMountArmorSlot;

		// Token: 0x040005E3 RID: 1507
		private SPItemVM _characterWeapon1Slot;

		// Token: 0x040005E4 RID: 1508
		private SPItemVM _characterWeapon2Slot;

		// Token: 0x040005E5 RID: 1509
		private SPItemVM _characterWeapon3Slot;

		// Token: 0x040005E6 RID: 1510
		private SPItemVM _characterWeapon4Slot;

		// Token: 0x040005E7 RID: 1511
		private SPItemVM _characterBannerSlot;

		// Token: 0x040005E8 RID: 1512
		private EquipmentIndex _targetEquipmentIndex = EquipmentIndex.None;

		// Token: 0x040005E9 RID: 1513
		private int _transactionCount = -1;

		// Token: 0x040005EA RID: 1514
		private bool _isRefreshed;

		// Token: 0x040005EB RID: 1515
		private string _tradeLbl = "";

		// Token: 0x040005EC RID: 1516
		private string _experienceLbl = "";

		// Token: 0x040005ED RID: 1517
		private bool _hasGainedExperience;

		// Token: 0x040005EE RID: 1518
		private bool _isDonationXpGainExceedsMax;

		// Token: 0x040005EF RID: 1519
		private bool _noSaddleWarned;

		// Token: 0x040005F0 RID: 1520
		private bool _otherEquipmentCountWarned;

		// Token: 0x040005F1 RID: 1521
		private bool _playerEquipmentCountWarned;

		// Token: 0x040005F2 RID: 1522
		private bool _isTradingWithSettlement;

		// Token: 0x040005F3 RID: 1523
		private string _otherEquipmentCountText;

		// Token: 0x040005F4 RID: 1524
		private string _playerEquipmentCountText;

		// Token: 0x040005F5 RID: 1525
		private string _noSaddleText;

		// Token: 0x040005F6 RID: 1526
		private string _leftSearchText = "";

		// Token: 0x040005F7 RID: 1527
		private string _playerSideCapacityExceededText;

		// Token: 0x040005F8 RID: 1528
		private string _otherSideCapacityExceededText;

		// Token: 0x040005F9 RID: 1529
		private string _rightSearchText = "";

		// Token: 0x040005FA RID: 1530
		private bool _isInWarSet = true;

		// Token: 0x040005FB RID: 1531
		private bool _companionExists;

		// Token: 0x040005FC RID: 1532
		private SPInventoryVM.Filters _activeFilterIndex;

		// Token: 0x040005FD RID: 1533
		private bool _isMicsFilterHighlightEnabled;

		// Token: 0x040005FE RID: 1534
		private bool _isCivilianFilterHighlightEnabled;

		// Token: 0x040005FF RID: 1535
		private ItemPreviewVM _itemPreview;

		// Token: 0x04000600 RID: 1536
		private SelectorVM<InventoryCharacterSelectorItemVM> _characterList;

		// Token: 0x04000601 RID: 1537
		private SPInventorySortControllerVM _otherInventorySortController;

		// Token: 0x04000602 RID: 1538
		private SPInventorySortControllerVM _playerInventorySortController;

		// Token: 0x04000603 RID: 1539
		private int _bannerTypeCode;

		// Token: 0x04000604 RID: 1540
		private string _leftInventoryOwnerName;

		// Token: 0x04000605 RID: 1541
		private int _leftInventoryOwnerGold;

		// Token: 0x04000606 RID: 1542
		private string _rightInventoryOwnerName;

		// Token: 0x04000607 RID: 1543
		private string _currentCharacterName;

		// Token: 0x04000608 RID: 1544
		private int _rightInventoryOwnerGold;

		// Token: 0x04000609 RID: 1545
		private int _itemCountToBuy;

		// Token: 0x0400060A RID: 1546
		private float _currentCharacterArmArmor;

		// Token: 0x0400060B RID: 1547
		private float _currentCharacterBodyArmor;

		// Token: 0x0400060C RID: 1548
		private float _currentCharacterHeadArmor;

		// Token: 0x0400060D RID: 1549
		private float _currentCharacterLegArmor;

		// Token: 0x0400060E RID: 1550
		private float _currentCharacterHorseArmor;

		// Token: 0x0400060F RID: 1551
		private string _currentCharacterTotalEncumbrance;

		// Token: 0x04000610 RID: 1552
		private InputKeyItemVM _resetInputKey;

		// Token: 0x04000611 RID: 1553
		private InputKeyItemVM _cancelInputKey;

		// Token: 0x04000612 RID: 1554
		private InputKeyItemVM _doneInputKey;

		// Token: 0x04000613 RID: 1555
		private InputKeyItemVM _previousCharacterInputKey;

		// Token: 0x04000614 RID: 1556
		private InputKeyItemVM _nextCharacterInputKey;

		// Token: 0x04000615 RID: 1557
		private InputKeyItemVM _buyAllInputKey;

		// Token: 0x04000616 RID: 1558
		private InputKeyItemVM _sellAllInputKey;

		// Token: 0x020001CB RID: 459
		public enum Filters
		{
			// Token: 0x04001027 RID: 4135
			All,
			// Token: 0x04001028 RID: 4136
			Weapons,
			// Token: 0x04001029 RID: 4137
			ShieldsAndRanged,
			// Token: 0x0400102A RID: 4138
			Armors,
			// Token: 0x0400102B RID: 4139
			Mounts,
			// Token: 0x0400102C RID: 4140
			Miscellaneous
		}
	}
}
