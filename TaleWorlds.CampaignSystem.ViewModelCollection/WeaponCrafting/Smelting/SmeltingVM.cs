using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting.Smelting
{
	// Token: 0x020000F5 RID: 245
	public class SmeltingVM : ViewModel
	{
		// Token: 0x06001728 RID: 5928 RVA: 0x000560F8 File Offset: 0x000542F8
		public SmeltingVM(Action updateValuesOnSelectItemAction, Action updateValuesOnSmeltItemAction)
		{
			this.SortController = new SmeltingSortControllerVM();
			this._updateValuesOnSelectItemAction = updateValuesOnSelectItemAction;
			this._updateValuesOnSmeltItemAction = updateValuesOnSmeltItemAction;
			this._playerItemRoster = MobileParty.MainParty.ItemRoster;
			this._smithingBehavior = Campaign.Current.GetCampaignBehavior<ICraftingCampaignBehavior>();
			IViewDataTracker campaignBehavior = Campaign.Current.GetCampaignBehavior<IViewDataTracker>();
			this._lockedItemIDs = campaignBehavior.GetInventoryLocks().ToList<string>();
			this.RefreshList();
			this.RefreshValues();
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x0005616C File Offset: 0x0005436C
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.SelectAllHint = new HintViewModel(new TextObject("{=k1E9DuKi}Select All", null), null);
			SmeltingItemVM currentSelectedItem = this.CurrentSelectedItem;
			if (currentSelectedItem != null)
			{
				currentSelectedItem.RefreshValues();
			}
			this.SmeltableItemList.ApplyActionOnAllItems(delegate(SmeltingItemVM x)
			{
				x.RefreshValues();
			});
			this.SortController.RefreshValues();
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x000561DC File Offset: 0x000543DC
		internal void OnCraftingHeroChanged(CraftingAvailableHeroItemVM newHero)
		{
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x000561E0 File Offset: 0x000543E0
		public void RefreshList()
		{
			this.SmeltableItemList = new MBBindingList<SmeltingItemVM>();
			this.SortController.SetListToControl(this.SmeltableItemList);
			for (int i = 0; i < this._playerItemRoster.Count; i++)
			{
				ItemRosterElement elementCopyAtIndex = this._playerItemRoster.GetElementCopyAtIndex(i);
				if (elementCopyAtIndex.EquipmentElement.Item.IsCraftedWeapon)
				{
					bool isLocked = this.IsItemLocked(elementCopyAtIndex.EquipmentElement);
					SmeltingItemVM item = new SmeltingItemVM(elementCopyAtIndex.EquipmentElement, new Action<SmeltingItemVM>(this.OnItemSelection), new Action<SmeltingItemVM, bool>(this.ProcessLockItem), isLocked, elementCopyAtIndex.Amount);
					this.SmeltableItemList.Add(item);
				}
			}
			if (this.SmeltableItemList.Count == 0)
			{
				this.CurrentSelectedItem = null;
			}
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x000562A0 File Offset: 0x000544A0
		private void OnItemSelection(SmeltingItemVM newItem)
		{
			if (newItem != this.CurrentSelectedItem)
			{
				if (this.CurrentSelectedItem != null)
				{
					this.CurrentSelectedItem.IsSelected = false;
				}
				this.CurrentSelectedItem = newItem;
				this.CurrentSelectedItem.IsSelected = true;
			}
			this._updateValuesOnSelectItemAction();
			WeaponDesign weaponDesign = this.CurrentSelectedItem.EquipmentElement.Item.WeaponDesign;
			this.WeaponTypeName = (((weaponDesign != null) ? weaponDesign.Template.TemplateName.ToString() : null) ?? string.Empty);
			WeaponDesign weaponDesign2 = this.CurrentSelectedItem.EquipmentElement.Item.WeaponDesign;
			this.WeaponTypeCode = (((weaponDesign2 != null) ? weaponDesign2.Template.StringId : null) ?? string.Empty);
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x00056360 File Offset: 0x00054560
		public void TrySmeltingSelectedItems(Hero currentCraftingHero)
		{
			if (this._currentSelectedItem != null)
			{
				if (this._currentSelectedItem.IsLocked)
				{
					string text = new TextObject("{=wMiLUTNY}Are you sure you want to smelt this weapon? It is locked in the inventory.", null).ToString();
					InformationManager.ShowInquiry(new InquiryData("", text, true, true, GameTexts.FindText("str_yes", null).ToString(), GameTexts.FindText("str_no", null).ToString(), delegate()
					{
						this.SmeltSelectedItems(currentCraftingHero);
					}, null, "", 0f, null, null, null), false, false);
					return;
				}
				this.SmeltSelectedItems(currentCraftingHero);
			}
		}

		// Token: 0x0600172E RID: 5934 RVA: 0x00056404 File Offset: 0x00054604
		private void ProcessLockItem(SmeltingItemVM item, bool isLocked)
		{
			if (item == null)
			{
				return;
			}
			string itemLockStringID = CampaignUIHelper.GetItemLockStringID(item.EquipmentElement);
			if (isLocked && !this._lockedItemIDs.Contains(itemLockStringID))
			{
				this._lockedItemIDs.Add(itemLockStringID);
				return;
			}
			if (!isLocked && this._lockedItemIDs.Contains(itemLockStringID))
			{
				this._lockedItemIDs.Remove(itemLockStringID);
			}
		}

		// Token: 0x0600172F RID: 5935 RVA: 0x00056460 File Offset: 0x00054660
		private void SmeltSelectedItems(Hero currentCraftingHero)
		{
			if (this._currentSelectedItem != null && this._smithingBehavior != null)
			{
				ICraftingCampaignBehavior smithingBehavior = this._smithingBehavior;
				if (smithingBehavior != null)
				{
					smithingBehavior.DoSmelting(currentCraftingHero, this._currentSelectedItem.EquipmentElement);
				}
			}
			this.RefreshList();
			this.SortController.SortByCurrentState();
			if (this.CurrentSelectedItem != null)
			{
				int num = this.SmeltableItemList.FindIndex((SmeltingItemVM i) => i.EquipmentElement.Item == this.CurrentSelectedItem.EquipmentElement.Item);
				SmeltingItemVM newItem = (num != -1) ? this.SmeltableItemList[num] : this.SmeltableItemList.FirstOrDefault<SmeltingItemVM>();
				this.OnItemSelection(newItem);
			}
			this._updateValuesOnSmeltItemAction();
		}

		// Token: 0x06001730 RID: 5936 RVA: 0x000564FC File Offset: 0x000546FC
		private bool IsItemLocked(EquipmentElement equipmentElement)
		{
			string itemLockStringID = CampaignUIHelper.GetItemLockStringID(equipmentElement);
			return this._lockedItemIDs.Contains(itemLockStringID);
		}

		// Token: 0x06001731 RID: 5937 RVA: 0x0005651C File Offset: 0x0005471C
		public void SaveItemLockStates()
		{
			Campaign.Current.GetCampaignBehavior<IViewDataTracker>().SetInventoryLocks(this._lockedItemIDs);
		}

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06001732 RID: 5938 RVA: 0x00056533 File Offset: 0x00054733
		// (set) Token: 0x06001733 RID: 5939 RVA: 0x0005653B File Offset: 0x0005473B
		[DataSourceProperty]
		public string WeaponTypeName
		{
			get
			{
				return this._weaponTypeName;
			}
			set
			{
				if (value != this._weaponTypeName)
				{
					this._weaponTypeName = value;
					base.OnPropertyChangedWithValue<string>(value, "WeaponTypeName");
				}
			}
		}

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06001734 RID: 5940 RVA: 0x0005655E File Offset: 0x0005475E
		// (set) Token: 0x06001735 RID: 5941 RVA: 0x00056566 File Offset: 0x00054766
		[DataSourceProperty]
		public string WeaponTypeCode
		{
			get
			{
				return this._weaponTypeCode;
			}
			set
			{
				if (value != this._weaponTypeCode)
				{
					this._weaponTypeCode = value;
					base.OnPropertyChangedWithValue<string>(value, "WeaponTypeCode");
				}
			}
		}

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06001736 RID: 5942 RVA: 0x00056589 File Offset: 0x00054789
		// (set) Token: 0x06001737 RID: 5943 RVA: 0x00056591 File Offset: 0x00054791
		[DataSourceProperty]
		public SmeltingItemVM CurrentSelectedItem
		{
			get
			{
				return this._currentSelectedItem;
			}
			set
			{
				if (value != this._currentSelectedItem)
				{
					this._currentSelectedItem = value;
					base.OnPropertyChangedWithValue<SmeltingItemVM>(value, "CurrentSelectedItem");
					this.IsAnyItemSelected = (value != null);
				}
			}
		}

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06001738 RID: 5944 RVA: 0x000565B9 File Offset: 0x000547B9
		// (set) Token: 0x06001739 RID: 5945 RVA: 0x000565C1 File Offset: 0x000547C1
		[DataSourceProperty]
		public bool IsAnyItemSelected
		{
			get
			{
				return this._isAnyItemSelected;
			}
			set
			{
				if (value != this._isAnyItemSelected)
				{
					this._isAnyItemSelected = value;
					base.OnPropertyChangedWithValue(value, "IsAnyItemSelected");
				}
			}
		}

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x0600173A RID: 5946 RVA: 0x000565DF File Offset: 0x000547DF
		// (set) Token: 0x0600173B RID: 5947 RVA: 0x000565E7 File Offset: 0x000547E7
		[DataSourceProperty]
		public MBBindingList<SmeltingItemVM> SmeltableItemList
		{
			get
			{
				return this._smeltableItemList;
			}
			set
			{
				if (value != this._smeltableItemList)
				{
					this._smeltableItemList = value;
					base.OnPropertyChangedWithValue<MBBindingList<SmeltingItemVM>>(value, "SmeltableItemList");
				}
			}
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x0600173C RID: 5948 RVA: 0x00056605 File Offset: 0x00054805
		// (set) Token: 0x0600173D RID: 5949 RVA: 0x0005660D File Offset: 0x0005480D
		[DataSourceProperty]
		public HintViewModel SelectAllHint
		{
			get
			{
				return this._selectAllHint;
			}
			set
			{
				if (value != this._selectAllHint)
				{
					this._selectAllHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "SelectAllHint");
				}
			}
		}

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x0600173E RID: 5950 RVA: 0x0005662B File Offset: 0x0005482B
		// (set) Token: 0x0600173F RID: 5951 RVA: 0x00056633 File Offset: 0x00054833
		[DataSourceProperty]
		public SmeltingSortControllerVM SortController
		{
			get
			{
				return this._sortController;
			}
			set
			{
				if (value != this._sortController)
				{
					this._sortController = value;
					base.OnPropertyChangedWithValue<SmeltingSortControllerVM>(value, "SortController");
				}
			}
		}

		// Token: 0x04000ACF RID: 2767
		private ItemRoster _playerItemRoster;

		// Token: 0x04000AD0 RID: 2768
		private Action _updateValuesOnSelectItemAction;

		// Token: 0x04000AD1 RID: 2769
		private Action _updateValuesOnSmeltItemAction;

		// Token: 0x04000AD2 RID: 2770
		private List<string> _lockedItemIDs;

		// Token: 0x04000AD3 RID: 2771
		private readonly ICraftingCampaignBehavior _smithingBehavior;

		// Token: 0x04000AD4 RID: 2772
		private string _weaponTypeName;

		// Token: 0x04000AD5 RID: 2773
		private string _weaponTypeCode;

		// Token: 0x04000AD6 RID: 2774
		private SmeltingItemVM _currentSelectedItem;

		// Token: 0x04000AD7 RID: 2775
		private MBBindingList<SmeltingItemVM> _smeltableItemList;

		// Token: 0x04000AD8 RID: 2776
		private SmeltingSortControllerVM _sortController;

		// Token: 0x04000AD9 RID: 2777
		private HintViewModel _selectAllHint;

		// Token: 0x04000ADA RID: 2778
		private bool _isAnyItemSelected;
	}
}
