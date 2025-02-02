using System;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;

namespace TaleWorlds.Core.ViewModelCollection
{
	// Token: 0x0200000D RID: 13
	public class ItemVM : ViewModel
	{
		// Token: 0x0600007D RID: 125 RVA: 0x00002D12 File Offset: 0x00000F12
		public ItemVM()
		{
			this.RefreshValues();
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002D2E File Offset: 0x00000F2E
		public override void RefreshValues()
		{
			base.RefreshValues();
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00002D36 File Offset: 0x00000F36
		[DataSourceProperty]
		public EquipmentIndex ItemType
		{
			get
			{
				if (this._itemType == EquipmentIndex.None)
				{
					return this.GetItemTypeWithItemObject();
				}
				return this._itemType;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00002D4E File Offset: 0x00000F4E
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00002D56 File Offset: 0x00000F56
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

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00002D74 File Offset: 0x00000F74
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00002D7C File Offset: 0x00000F7C
		[DataSourceProperty]
		public string StringId
		{
			get
			{
				return this._stringId;
			}
			set
			{
				if (value != this._stringId)
				{
					this._stringId = value;
					base.OnPropertyChangedWithValue<string>(value, "StringId");
				}
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00002D9F File Offset: 0x00000F9F
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00002DA7 File Offset: 0x00000FA7
		[DataSourceProperty]
		public string ItemDescription
		{
			get
			{
				return this._itemDescription;
			}
			set
			{
				if (value != this._itemDescription)
				{
					this._itemDescription = value;
					base.OnPropertyChangedWithValue<string>(value, "ItemDescription");
				}
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00002DCA File Offset: 0x00000FCA
		// (set) Token: 0x06000087 RID: 135 RVA: 0x00002DD2 File Offset: 0x00000FD2
		[DataSourceProperty]
		public bool IsFiltered
		{
			get
			{
				return this._isFiltered;
			}
			set
			{
				if (value != this._isFiltered)
				{
					this._isFiltered = value;
					base.OnPropertyChangedWithValue(value, "IsFiltered");
				}
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00002DF0 File Offset: 0x00000FF0
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00002DF8 File Offset: 0x00000FF8
		[DataSourceProperty]
		public int ItemCost
		{
			get
			{
				return this._itemCost;
			}
			set
			{
				if (value != this._itemCost)
				{
					this._itemCost = value;
					base.OnPropertyChangedWithValue(value, "ItemCost");
				}
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00002E16 File Offset: 0x00001016
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00002E1E File Offset: 0x0000101E
		[DataSourceProperty]
		public int TypeId
		{
			get
			{
				return this._typeId;
			}
			set
			{
				if (value != this._typeId)
				{
					this._typeId = value;
					base.OnPropertyChangedWithValue(value, "TypeId");
				}
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00002E3C File Offset: 0x0000103C
		// (set) Token: 0x0600008D RID: 141 RVA: 0x00002E44 File Offset: 0x00001044
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

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00002E62 File Offset: 0x00001062
		// (set) Token: 0x0600008F RID: 143 RVA: 0x00002E6A File Offset: 0x0000106A
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

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00002E88 File Offset: 0x00001088
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00002E90 File Offset: 0x00001090
		[DataSourceProperty]
		public BasicTooltipViewModel SlaughterHint
		{
			get
			{
				return this._slaughterHint;
			}
			set
			{
				if (value != this._slaughterHint)
				{
					this._slaughterHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "SlaughterHint");
				}
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00002EAE File Offset: 0x000010AE
		// (set) Token: 0x06000093 RID: 147 RVA: 0x00002EB6 File Offset: 0x000010B6
		[DataSourceProperty]
		public BasicTooltipViewModel DonateHint
		{
			get
			{
				return this._donateHint;
			}
			set
			{
				if (value != this._donateHint)
				{
					this._donateHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "DonateHint");
				}
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00002ED4 File Offset: 0x000010D4
		// (set) Token: 0x06000095 RID: 149 RVA: 0x00002EDC File Offset: 0x000010DC
		[DataSourceProperty]
		public BasicTooltipViewModel BuyAndEquipHint
		{
			get
			{
				return this._buyAndEquip;
			}
			set
			{
				if (value != this._buyAndEquip)
				{
					this._buyAndEquip = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "BuyAndEquipHint");
				}
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00002EFA File Offset: 0x000010FA
		// (set) Token: 0x06000097 RID: 151 RVA: 0x00002F02 File Offset: 0x00001102
		[DataSourceProperty]
		public BasicTooltipViewModel SellHint
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
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "SellHint");
				}
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00002F20 File Offset: 0x00001120
		// (set) Token: 0x06000099 RID: 153 RVA: 0x00002F28 File Offset: 0x00001128
		[DataSourceProperty]
		public BasicTooltipViewModel BuyHint
		{
			get
			{
				return this._buyHint;
			}
			set
			{
				if (value != this._buyHint)
				{
					this._buyHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "BuyHint");
				}
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00002F46 File Offset: 0x00001146
		// (set) Token: 0x0600009B RID: 155 RVA: 0x00002F4E File Offset: 0x0000114E
		[DataSourceProperty]
		public HintViewModel LockHint
		{
			get
			{
				return this._lockHint;
			}
			set
			{
				if (value != this._lockHint)
				{
					this._lockHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "LockHint");
				}
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00002F6C File Offset: 0x0000116C
		public void ExecutePreviewItem()
		{
			if (!UiStringHelper.IsStringNoneOrEmptyForUi(this.StringId))
			{
				ItemVM.ProcessPreviewItem(this);
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00002F86 File Offset: 0x00001186
		public void ExecuteUnequipItem()
		{
			if (!UiStringHelper.IsStringNoneOrEmptyForUi(this.StringId))
			{
				ItemVM.ProcessUnequipItem(this);
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00002FA0 File Offset: 0x000011A0
		public void ExecuteEquipItem()
		{
			if (!UiStringHelper.IsStringNoneOrEmptyForUi(this.StringId))
			{
				ItemVM.ProcessEquipItem(this);
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00002FBA File Offset: 0x000011BA
		public static void ReleaseStaticContent()
		{
			ItemVM.ProcessEquipItem = null;
			ItemVM.ProcessPreviewItem = null;
			ItemVM.ProcessUnequipItem = null;
			ItemVM.ProcessBuyItem = null;
			ItemVM.ProcessItemSelect = null;
			ItemVM.ProcessItemTooltip = null;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00002FE0 File Offset: 0x000011E0
		public void ExecuteRefreshTooltip()
		{
			if (ItemVM.ProcessItemTooltip != null && !UiStringHelper.IsStringNoneOrEmptyForUi(this.StringId))
			{
				ItemVM.ProcessItemTooltip(this);
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003001 File Offset: 0x00001201
		public void ExecuteCancelTooltip()
		{
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003003 File Offset: 0x00001203
		public void ExecuteBuyItem()
		{
			if (!UiStringHelper.IsStringNoneOrEmptyForUi(this.StringId))
			{
				ItemVM.ProcessBuyItem(this, false);
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000301E File Offset: 0x0000121E
		public void ExecuteSelectItem()
		{
			if (!UiStringHelper.IsStringNoneOrEmptyForUi(this.StringId))
			{
				ItemVM.ProcessItemSelect(this);
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003038 File Offset: 0x00001238
		public EquipmentIndex GetItemTypeWithItemObject()
		{
			if (this.ItemRosterElement.EquipmentElement.Item == null)
			{
				return EquipmentIndex.None;
			}
			ItemObject.ItemTypeEnum type = this.ItemRosterElement.EquipmentElement.Item.Type;
			switch (type)
			{
			case ItemObject.ItemTypeEnum.Horse:
				return EquipmentIndex.ArmorItemEndSlot;
			case ItemObject.ItemTypeEnum.OneHandedWeapon:
			case ItemObject.ItemTypeEnum.TwoHandedWeapon:
			case ItemObject.ItemTypeEnum.Polearm:
			case ItemObject.ItemTypeEnum.Bow:
			case ItemObject.ItemTypeEnum.Crossbow:
			case ItemObject.ItemTypeEnum.Thrown:
			case ItemObject.ItemTypeEnum.Goods:
				break;
			case ItemObject.ItemTypeEnum.Arrows:
				return EquipmentIndex.WeaponItemBeginSlot;
			case ItemObject.ItemTypeEnum.Bolts:
				return EquipmentIndex.WeaponItemBeginSlot;
			case ItemObject.ItemTypeEnum.Shield:
				if (this._typeId == 0)
				{
					this._typeId = 1;
				}
				return EquipmentIndex.WeaponItemBeginSlot;
			case ItemObject.ItemTypeEnum.HeadArmor:
				return EquipmentIndex.NumAllWeaponSlots;
			case ItemObject.ItemTypeEnum.BodyArmor:
				return EquipmentIndex.Body;
			case ItemObject.ItemTypeEnum.LegArmor:
				return EquipmentIndex.Leg;
			case ItemObject.ItemTypeEnum.HandArmor:
				return EquipmentIndex.Gloves;
			default:
				switch (type)
				{
				case ItemObject.ItemTypeEnum.Cape:
					return EquipmentIndex.Cape;
				case ItemObject.ItemTypeEnum.HorseHarness:
					return EquipmentIndex.HorseHarness;
				case ItemObject.ItemTypeEnum.Banner:
					return EquipmentIndex.ExtraWeaponSlot;
				}
				break;
			}
			if (this.ItemRosterElement.EquipmentElement.Item.WeaponComponent != null)
			{
				return EquipmentIndex.WeaponItemBeginSlot;
			}
			return EquipmentIndex.None;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003118 File Offset: 0x00001318
		protected void SetItemTypeId()
		{
			this.TypeId = (int)this.ItemRosterElement.EquipmentElement.Item.Type;
		}

		// Token: 0x0400002A RID: 42
		public static Action<ItemVM> ProcessEquipItem;

		// Token: 0x0400002B RID: 43
		public static Action<ItemVM> ProcessPreviewItem;

		// Token: 0x0400002C RID: 44
		public static Action<ItemVM> ProcessUnequipItem;

		// Token: 0x0400002D RID: 45
		public static Action<ItemVM, bool> ProcessBuyItem;

		// Token: 0x0400002E RID: 46
		public static Action<ItemVM> ProcessItemSelect;

		// Token: 0x0400002F RID: 47
		public static Action<ItemVM> ProcessItemTooltip;

		// Token: 0x04000030 RID: 48
		private int _typeId;

		// Token: 0x04000031 RID: 49
		private int _itemCost = -1;

		// Token: 0x04000032 RID: 50
		private bool _isFiltered;

		// Token: 0x04000033 RID: 51
		private string _itemDescription;

		// Token: 0x04000034 RID: 52
		public ItemRosterElement ItemRosterElement;

		// Token: 0x04000035 RID: 53
		public EquipmentIndex _itemType = EquipmentIndex.None;

		// Token: 0x04000036 RID: 54
		private ImageIdentifierVM _imageIdentifier;

		// Token: 0x04000037 RID: 55
		private HintViewModel _previewHint;

		// Token: 0x04000038 RID: 56
		private HintViewModel _equipHint;

		// Token: 0x04000039 RID: 57
		private BasicTooltipViewModel _buyAndEquip;

		// Token: 0x0400003A RID: 58
		private BasicTooltipViewModel _sellHint;

		// Token: 0x0400003B RID: 59
		private BasicTooltipViewModel _buyHint;

		// Token: 0x0400003C RID: 60
		private HintViewModel _lockHint;

		// Token: 0x0400003D RID: 61
		private BasicTooltipViewModel _slaughterHint;

		// Token: 0x0400003E RID: 62
		private BasicTooltipViewModel _donateHint;

		// Token: 0x0400003F RID: 63
		private string _stringId;
	}
}
