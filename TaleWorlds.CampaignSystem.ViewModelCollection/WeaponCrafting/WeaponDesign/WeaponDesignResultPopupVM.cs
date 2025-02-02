using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.CraftingSystem;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.CampaignSystem.ViewModelCollection.Input;
using TaleWorlds.CampaignSystem.ViewModelCollection.Inventory;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Core.ViewModelCollection.Selector;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting.WeaponDesign
{
	// Token: 0x020000EC RID: 236
	public class WeaponDesignResultPopupVM : ViewModel
	{
		// Token: 0x060015AF RID: 5551 RVA: 0x00051190 File Offset: 0x0004F390
		public WeaponDesignResultPopupVM(ItemObject craftedItem, string itemName, Action onFinalize, Crafting crafting, CraftingOrder completedOrder, ItemCollectionElementViewModel itemVisualModel, MBBindingList<ItemFlagVM> weaponFlagIconsList, Func<CraftingSecondaryUsageItemVM, MBBindingList<WeaponDesignResultPropertyItemVM>> onGetPropertyList, Action<CraftingSecondaryUsageItemVM> onUsageSelected)
		{
			this._craftedItem = craftedItem;
			this._onFinalize = onFinalize;
			this._crafting = crafting;
			this._completedOrder = completedOrder;
			this._craftingBehavior = Campaign.Current.GetCampaignBehavior<ICraftingCampaignBehavior>();
			this._onUsageSelected = onUsageSelected;
			this.SecondaryUsageSelector = new SelectorVM<CraftingSecondaryUsageItemVM>(new List<string>(), -1, new Action<SelectorVM<CraftingSecondaryUsageItemVM>>(this.OnUsageSelected));
			this.WeaponFlagIconsList = weaponFlagIconsList;
			this._onGetPropertyList = onGetPropertyList;
			ItemModifier currentItemModifier = this._craftingBehavior.GetCurrentItemModifier();
			if (currentItemModifier != null)
			{
				TextObject textObject = currentItemModifier.Name.CopyTextObject();
				textObject.SetTextVariable("ITEMNAME", itemName);
				this.ItemName = textObject.ToString();
			}
			else
			{
				this.ItemName = itemName;
			}
			this.ItemName = this.ItemName.Trim();
			this.ItemVisualModel = itemVisualModel;
			Game game = Game.Current;
			if (game != null)
			{
				game.EventManager.TriggerEvent<CraftingWeaponResultPopupToggledEvent>(new CraftingWeaponResultPopupToggledEvent(true));
			}
			this.RefreshValues();
		}

		// Token: 0x060015B0 RID: 5552 RVA: 0x00051280 File Offset: 0x0004F480
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.IsInOrderMode = (this._completedOrder != null);
			this.WeaponCraftedText = new TextObject("{=0mqdFC2x}Weapon Crafted!", null).ToString();
			this.DoneLbl = GameTexts.FindText("str_done", null).ToString();
			this.RefreshUsages();
		}

		// Token: 0x060015B1 RID: 5553 RVA: 0x000512D4 File Offset: 0x0004F4D4
		public override void OnFinalize()
		{
			base.OnFinalize();
			this.DoneInputKey.OnFinalize();
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x000512E8 File Offset: 0x0004F4E8
		private void RefreshUsages()
		{
			this.SecondaryUsageSelector.ItemList.Clear();
			MBReadOnlyList<WeaponComponentData> weapons = this._crafting.GetCurrentCraftedItemObject(false).Weapons;
			int num = this.SecondaryUsageSelector.SelectedIndex;
			int num2 = 0;
			for (int i = 0; i < weapons.Count; i++)
			{
				if (CampaignUIHelper.IsItemUsageApplicable(weapons[i]))
				{
					TextObject name = GameTexts.FindText("str_weapon_usage", weapons[i].WeaponDescriptionId);
					this.SecondaryUsageSelector.AddItem(new CraftingSecondaryUsageItemVM(name, num2, i, this.SecondaryUsageSelector));
					if (this.IsInOrderMode)
					{
						WeaponComponentData orderWeapon = this._completedOrder.GetStatWeapon();
						num = this._crafting.GetCurrentCraftedItemObject(false).Weapons.FindIndex((WeaponComponentData x) => x.WeaponDescriptionId == orderWeapon.WeaponDescriptionId);
					}
					else
					{
						CraftingOrder completedOrder = this._completedOrder;
						if (((completedOrder != null) ? completedOrder.GetStatWeapon().WeaponDescriptionId : null) == weapons[i].WeaponDescriptionId)
						{
							num = num2;
						}
					}
					num2++;
				}
			}
			this.SecondaryUsageSelector.SelectedIndex = ((num >= 0) ? num : 0);
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x0005140C File Offset: 0x0004F60C
		private void OnUsageSelected(SelectorVM<CraftingSecondaryUsageItemVM> selector)
		{
			Func<CraftingSecondaryUsageItemVM, MBBindingList<WeaponDesignResultPropertyItemVM>> onGetPropertyList = this._onGetPropertyList;
			this.DesignResultPropertyList = ((onGetPropertyList != null) ? onGetPropertyList(selector.SelectedItem) : null);
			if (this._isInOrderMode)
			{
				bool isOrderSuccessful;
				TextObject textObject;
				TextObject textObject2;
				int craftedWeaponFinalWorth;
				this._craftingBehavior.GetOrderResult(this._completedOrder, this._craftedItem, out isOrderSuccessful, out textObject, out textObject2, out craftedWeaponFinalWorth);
				this.CraftedWeaponInitialWorth = this._completedOrder.BaseGoldReward;
				this.CraftedWeaponFinalWorth = craftedWeaponFinalWorth;
				this.IsOrderSuccessful = isOrderSuccessful;
				this.CraftedWeaponWorthText = new TextObject("{=ZIn8W5ZG}Worth", null).ToString();
				this.DesignResultPropertyList.Add(new WeaponDesignResultPropertyItemVM(new TextObject("{=QmfZjCo1}Worth: ", null), (float)this.CraftedWeaponInitialWorth, (float)this.CraftedWeaponInitialWorth, (float)(this.CraftedWeaponFinalWorth - this.CraftedWeaponInitialWorth), false, true, false));
				this.OrderOwnerRemarkText = textObject.ToString();
				this.OrderResultText = textObject2.ToString();
			}
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x000514E8 File Offset: 0x0004F6E8
		private void UpdateConfirmAvailability()
		{
			Tuple<bool, TextObject> tuple = CampaignUIHelper.IsStringApplicableForItemName(this.ItemName);
			this.CanConfirm = tuple.Item1;
			this.ConfirmDisabledReasonHint = new HintViewModel(tuple.Item2, null);
		}

		// Token: 0x060015B5 RID: 5557 RVA: 0x00051520 File Offset: 0x0004F720
		public void ExecuteFinalizeCrafting()
		{
			TextObject textObject = new TextObject("{=!}" + this.ItemName, null);
			this._crafting.SetCraftedWeaponName(textObject);
			this._craftingBehavior.SetCraftedWeaponName(this._craftedItem, textObject);
			Action onFinalize = this._onFinalize;
			if (onFinalize != null)
			{
				onFinalize();
			}
			Game game = Game.Current;
			if (game != null)
			{
				game.EventManager.TriggerEvent<CraftingWeaponResultPopupToggledEvent>(new CraftingWeaponResultPopupToggledEvent(false));
			}
			TextObject textObject2 = GameTexts.FindText("crafting_added_to_inventory", null);
			textObject2.SetCharacterProperties("PLAYER", Hero.MainHero.CharacterObject, false);
			textObject2.SetTextVariable("ITEM_NAME", this.ItemName);
			MBInformationManager.AddQuickInformation(textObject2, 0, null, "");
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x000515CD File Offset: 0x0004F7CD
		public void ExecuteRandomCraftName()
		{
			this.ItemName = this._crafting.GetRandomCraftName().ToString();
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x060015B7 RID: 5559 RVA: 0x000515E5 File Offset: 0x0004F7E5
		// (set) Token: 0x060015B8 RID: 5560 RVA: 0x000515ED File Offset: 0x0004F7ED
		[DataSourceProperty]
		public MBBindingList<ItemFlagVM> WeaponFlagIconsList
		{
			get
			{
				return this._weaponFlagIconsList;
			}
			set
			{
				if (value != this._weaponFlagIconsList)
				{
					this._weaponFlagIconsList = value;
					base.OnPropertyChangedWithValue<MBBindingList<ItemFlagVM>>(value, "WeaponFlagIconsList");
				}
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x060015B9 RID: 5561 RVA: 0x0005160B File Offset: 0x0004F80B
		// (set) Token: 0x060015BA RID: 5562 RVA: 0x00051613 File Offset: 0x0004F813
		[DataSourceProperty]
		public bool IsInOrderMode
		{
			get
			{
				return this._isInOrderMode;
			}
			set
			{
				if (value != this._isInOrderMode)
				{
					this._isInOrderMode = value;
					base.OnPropertyChangedWithValue(value, "IsInOrderMode");
				}
			}
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x060015BB RID: 5563 RVA: 0x00051631 File Offset: 0x0004F831
		// (set) Token: 0x060015BC RID: 5564 RVA: 0x00051639 File Offset: 0x0004F839
		[DataSourceProperty]
		public int CraftedWeaponFinalWorth
		{
			get
			{
				return this._craftedWeaponFinalWorth;
			}
			set
			{
				if (value != this._craftedWeaponFinalWorth)
				{
					this._craftedWeaponFinalWorth = value;
					base.OnPropertyChangedWithValue(value, "CraftedWeaponFinalWorth");
				}
			}
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x060015BD RID: 5565 RVA: 0x00051657 File Offset: 0x0004F857
		// (set) Token: 0x060015BE RID: 5566 RVA: 0x0005165F File Offset: 0x0004F85F
		[DataSourceProperty]
		public int CraftedWeaponPriceDifference
		{
			get
			{
				return this._craftedWeaponPriceDifference;
			}
			set
			{
				if (value != this._craftedWeaponPriceDifference)
				{
					this._craftedWeaponPriceDifference = value;
					base.OnPropertyChangedWithValue(value, "CraftedWeaponPriceDifference");
				}
			}
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x060015BF RID: 5567 RVA: 0x0005167D File Offset: 0x0004F87D
		// (set) Token: 0x060015C0 RID: 5568 RVA: 0x00051685 File Offset: 0x0004F885
		[DataSourceProperty]
		public int CraftedWeaponInitialWorth
		{
			get
			{
				return this._craftedWeaponInitialWorth;
			}
			set
			{
				if (value != this._craftedWeaponInitialWorth)
				{
					this._craftedWeaponInitialWorth = value;
					base.OnPropertyChangedWithValue(value, "CraftedWeaponInitialWorth");
				}
			}
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x060015C1 RID: 5569 RVA: 0x000516A3 File Offset: 0x0004F8A3
		// (set) Token: 0x060015C2 RID: 5570 RVA: 0x000516AB File Offset: 0x0004F8AB
		[DataSourceProperty]
		public string CraftedWeaponWorthText
		{
			get
			{
				return this._craftedWeaponWorthText;
			}
			set
			{
				if (value != this._craftedWeaponWorthText)
				{
					this._craftedWeaponWorthText = value;
					base.OnPropertyChangedWithValue<string>(value, "CraftedWeaponWorthText");
				}
			}
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x060015C3 RID: 5571 RVA: 0x000516CE File Offset: 0x0004F8CE
		// (set) Token: 0x060015C4 RID: 5572 RVA: 0x000516D6 File Offset: 0x0004F8D6
		[DataSourceProperty]
		public bool IsOrderSuccessful
		{
			get
			{
				return this._isOrderSuccessful;
			}
			set
			{
				if (value != this._isOrderSuccessful)
				{
					this._isOrderSuccessful = value;
					base.OnPropertyChangedWithValue(value, "IsOrderSuccessful");
				}
			}
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x060015C5 RID: 5573 RVA: 0x000516F4 File Offset: 0x0004F8F4
		// (set) Token: 0x060015C6 RID: 5574 RVA: 0x000516FC File Offset: 0x0004F8FC
		[DataSourceProperty]
		public bool CanConfirm
		{
			get
			{
				return this._canConfirm;
			}
			set
			{
				if (value != this._canConfirm)
				{
					this._canConfirm = value;
					base.OnPropertyChangedWithValue(value, "CanConfirm");
				}
			}
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x060015C7 RID: 5575 RVA: 0x0005171A File Offset: 0x0004F91A
		// (set) Token: 0x060015C8 RID: 5576 RVA: 0x00051722 File Offset: 0x0004F922
		[DataSourceProperty]
		public string OrderResultText
		{
			get
			{
				return this._orderResultText;
			}
			set
			{
				if (value != this._orderResultText)
				{
					this._orderResultText = value;
					base.OnPropertyChangedWithValue<string>(value, "OrderResultText");
				}
			}
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x060015C9 RID: 5577 RVA: 0x00051745 File Offset: 0x0004F945
		// (set) Token: 0x060015CA RID: 5578 RVA: 0x0005174D File Offset: 0x0004F94D
		[DataSourceProperty]
		public string OrderOwnerRemarkText
		{
			get
			{
				return this._orderOwnerRemarkText;
			}
			set
			{
				if (value != this._orderOwnerRemarkText)
				{
					this._orderOwnerRemarkText = value;
					base.OnPropertyChangedWithValue<string>(value, "OrderOwnerRemarkText");
				}
			}
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x060015CB RID: 5579 RVA: 0x00051770 File Offset: 0x0004F970
		// (set) Token: 0x060015CC RID: 5580 RVA: 0x00051778 File Offset: 0x0004F978
		[DataSourceProperty]
		public string WeaponCraftedText
		{
			get
			{
				return this._weaponCraftedText;
			}
			set
			{
				if (value != this._weaponCraftedText)
				{
					this._weaponCraftedText = value;
					base.OnPropertyChangedWithValue<string>(value, "WeaponCraftedText");
				}
			}
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x060015CD RID: 5581 RVA: 0x0005179B File Offset: 0x0004F99B
		// (set) Token: 0x060015CE RID: 5582 RVA: 0x000517A3 File Offset: 0x0004F9A3
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

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x060015CF RID: 5583 RVA: 0x000517C6 File Offset: 0x0004F9C6
		// (set) Token: 0x060015D0 RID: 5584 RVA: 0x000517CE File Offset: 0x0004F9CE
		[DataSourceProperty]
		public MBBindingList<WeaponDesignResultPropertyItemVM> DesignResultPropertyList
		{
			get
			{
				return this._designResultPropertyList;
			}
			set
			{
				if (value != this._designResultPropertyList)
				{
					this._designResultPropertyList = value;
					base.OnPropertyChangedWithValue<MBBindingList<WeaponDesignResultPropertyItemVM>>(value, "DesignResultPropertyList");
				}
			}
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x060015D1 RID: 5585 RVA: 0x000517EC File Offset: 0x0004F9EC
		// (set) Token: 0x060015D2 RID: 5586 RVA: 0x000517F4 File Offset: 0x0004F9F4
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
					this.UpdateConfirmAvailability();
					base.OnPropertyChangedWithValue<string>(value, "ItemName");
				}
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x060015D3 RID: 5587 RVA: 0x0005181D File Offset: 0x0004FA1D
		// (set) Token: 0x060015D4 RID: 5588 RVA: 0x00051825 File Offset: 0x0004FA25
		[DataSourceProperty]
		public ItemCollectionElementViewModel ItemVisualModel
		{
			get
			{
				return this._itemVisualModel;
			}
			set
			{
				if (value != this._itemVisualModel)
				{
					this._itemVisualModel = value;
					base.OnPropertyChangedWithValue<ItemCollectionElementViewModel>(value, "ItemVisualModel");
				}
			}
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x060015D5 RID: 5589 RVA: 0x00051843 File Offset: 0x0004FA43
		// (set) Token: 0x060015D6 RID: 5590 RVA: 0x0005184B File Offset: 0x0004FA4B
		[DataSourceProperty]
		public HintViewModel ConfirmDisabledReasonHint
		{
			get
			{
				return this._confirmDisabledReasonHint;
			}
			set
			{
				if (value != this._confirmDisabledReasonHint)
				{
					this._confirmDisabledReasonHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "ConfirmDisabledReasonHint");
				}
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x060015D7 RID: 5591 RVA: 0x00051869 File Offset: 0x0004FA69
		// (set) Token: 0x060015D8 RID: 5592 RVA: 0x00051871 File Offset: 0x0004FA71
		[DataSourceProperty]
		public SelectorVM<CraftingSecondaryUsageItemVM> SecondaryUsageSelector
		{
			get
			{
				return this._secondaryUsageSelector;
			}
			set
			{
				if (value != this._secondaryUsageSelector)
				{
					this._secondaryUsageSelector = value;
					base.OnPropertyChangedWithValue<SelectorVM<CraftingSecondaryUsageItemVM>>(value, "SecondaryUsageSelector");
				}
			}
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x0005188F File Offset: 0x0004FA8F
		public void SetDoneInputKey(HotKey hotkey)
		{
			this.DoneInputKey = InputKeyItemVM.CreateFromHotKey(hotkey, true);
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x060015DA RID: 5594 RVA: 0x0005189E File Offset: 0x0004FA9E
		// (set) Token: 0x060015DB RID: 5595 RVA: 0x000518A6 File Offset: 0x0004FAA6
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

		// Token: 0x04000A16 RID: 2582
		private readonly Action<CraftingSecondaryUsageItemVM> _onUsageSelected;

		// Token: 0x04000A17 RID: 2583
		private readonly Func<CraftingSecondaryUsageItemVM, MBBindingList<WeaponDesignResultPropertyItemVM>> _onGetPropertyList;

		// Token: 0x04000A18 RID: 2584
		private readonly Action _onFinalize;

		// Token: 0x04000A19 RID: 2585
		private readonly Crafting _crafting;

		// Token: 0x04000A1A RID: 2586
		private readonly CraftingOrder _completedOrder;

		// Token: 0x04000A1B RID: 2587
		private readonly ItemObject _craftedItem;

		// Token: 0x04000A1C RID: 2588
		private readonly ICraftingCampaignBehavior _craftingBehavior;

		// Token: 0x04000A1D RID: 2589
		private MBBindingList<ItemFlagVM> _weaponFlagIconsList;

		// Token: 0x04000A1E RID: 2590
		private bool _isInOrderMode;

		// Token: 0x04000A1F RID: 2591
		private string _orderResultText;

		// Token: 0x04000A20 RID: 2592
		private string _orderOwnerRemarkText;

		// Token: 0x04000A21 RID: 2593
		private bool _isOrderSuccessful;

		// Token: 0x04000A22 RID: 2594
		private bool _canConfirm;

		// Token: 0x04000A23 RID: 2595
		private string _craftedWeaponWorthText;

		// Token: 0x04000A24 RID: 2596
		private int _craftedWeaponInitialWorth;

		// Token: 0x04000A25 RID: 2597
		private int _craftedWeaponPriceDifference;

		// Token: 0x04000A26 RID: 2598
		private int _craftedWeaponFinalWorth;

		// Token: 0x04000A27 RID: 2599
		private string _weaponCraftedText;

		// Token: 0x04000A28 RID: 2600
		private string _doneLbl;

		// Token: 0x04000A29 RID: 2601
		private MBBindingList<WeaponDesignResultPropertyItemVM> _designResultPropertyList;

		// Token: 0x04000A2A RID: 2602
		private string _itemName;

		// Token: 0x04000A2B RID: 2603
		private ItemCollectionElementViewModel _itemVisualModel;

		// Token: 0x04000A2C RID: 2604
		private HintViewModel _confirmDisabledReasonHint;

		// Token: 0x04000A2D RID: 2605
		private SelectorVM<CraftingSecondaryUsageItemVM> _secondaryUsageSelector;

		// Token: 0x04000A2E RID: 2606
		private InputKeyItemVM _doneInputKey;
	}
}
