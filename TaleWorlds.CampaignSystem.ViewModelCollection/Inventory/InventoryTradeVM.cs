using System;
using TaleWorlds.CampaignSystem.Inventory;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Inventory
{
	// Token: 0x0200007D RID: 125
	public class InventoryTradeVM : ViewModel
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000B31 RID: 2865 RVA: 0x0002C288 File Offset: 0x0002A488
		// (remove) Token: 0x06000B32 RID: 2866 RVA: 0x0002C2BC File Offset: 0x0002A4BC
		public static event Action RemoveZeroCounts;

		// Token: 0x06000B33 RID: 2867 RVA: 0x0002C2F0 File Offset: 0x0002A4F0
		public InventoryTradeVM(InventoryLogic inventoryLogic, ItemRosterElement itemRoster, InventoryLogic.InventorySide side, Action<int, bool> onApplyTransaction)
		{
			this._inventoryLogic = inventoryLogic;
			this._referenceItemRoster = itemRoster;
			this._isPlayerItem = (side == InventoryLogic.InventorySide.PlayerInventory);
			this._onApplyTransaction = onApplyTransaction;
			this.PieceLbl = this._pieceLblSingular;
			InventoryLogic inventoryLogic2 = this._inventoryLogic;
			this.IsTrading = (inventoryLogic2 != null && inventoryLogic2.IsTrading);
			this.UpdateItemData(itemRoster, side, true);
			this.RefreshValues();
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x0002C364 File Offset: 0x0002A564
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.ThisStockLbl = GameTexts.FindText("str_inventory_this_stock", null).ToString();
			this.OtherStockLbl = GameTexts.FindText("str_inventory_total_stock", null).ToString();
			this.AveragePriceLbl = GameTexts.FindText("str_inventory_average_price", null).ToString();
			this._pieceLblSingular = GameTexts.FindText("str_inventory_piece", null).ToString();
			this._pieceLblPlural = GameTexts.FindText("str_inventory_pieces", null).ToString();
			this.ApplyExchangeHint = new HintViewModel(GameTexts.FindText("str_party_apply_exchange", null), null);
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x0002C3FC File Offset: 0x0002A5FC
		public void UpdateItemData(ItemRosterElement itemRoster, InventoryLogic.InventorySide side, bool forceUpdate = true)
		{
			if (side != InventoryLogic.InventorySide.OtherInventory && side != InventoryLogic.InventorySide.PlayerInventory)
			{
				return;
			}
			ItemRosterElement? itemRosterElement = new ItemRosterElement?(itemRoster);
			ItemRosterElement? itemRosterElement2 = null;
			if (side == InventoryLogic.InventorySide.PlayerInventory)
			{
				itemRosterElement2 = this.FindItemFromSide(itemRoster.EquipmentElement, InventoryLogic.InventorySide.OtherInventory);
			}
			else if (side == InventoryLogic.InventorySide.OtherInventory)
			{
				itemRosterElement2 = this.FindItemFromSide(itemRoster.EquipmentElement, InventoryLogic.InventorySide.PlayerInventory);
			}
			if (forceUpdate)
			{
				this.InitialThisStock = ((itemRosterElement != null) ? itemRosterElement.GetValueOrDefault().Amount : 0);
				this.InitialOtherStock = ((itemRosterElement2 != null) ? itemRosterElement2.GetValueOrDefault().Amount : 0);
				this.TotalStock = this.InitialThisStock + this.InitialOtherStock;
				this.ThisStock = this.InitialThisStock;
				this.OtherStock = this.InitialOtherStock;
				this.ThisStockUpdated();
			}
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x0002C4BE File Offset: 0x0002A6BE
		private ItemRosterElement? FindItemFromSide(EquipmentElement item, InventoryLogic.InventorySide side)
		{
			return this._inventoryLogic.FindItemFromSide(side, item);
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x0002C4D0 File Offset: 0x0002A6D0
		private void ThisStockUpdated()
		{
			this.ExecuteApplyTransaction();
			this.OtherStock = this.TotalStock - this.ThisStock;
			this.IsThisStockIncreasable = (this.OtherStock > 0);
			this.IsOtherStockIncreasable = (this.OtherStock < this.TotalStock);
			this.UpdateProperties();
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x0002C520 File Offset: 0x0002A720
		private void UpdateProperties()
		{
			int num = this.ThisStock - this.InitialThisStock;
			bool flag = num >= 0;
			int num2 = flag ? num : (-num);
			if (num2 == 0)
			{
				this.PieceChange = num2.ToString();
				this.PriceChange = "0";
				this.AveragePrice = "0";
				this.IsExchangeAvailable = false;
			}
			else
			{
				int lastPrice;
				int itemTotalPrice = this._inventoryLogic.GetItemTotalPrice(this._referenceItemRoster, num2, out lastPrice, flag);
				this.PieceChange = (flag ? "+" : "-") + num2;
				this.PriceChange = (flag ? "-" : "+") + itemTotalPrice * num2;
				this.AveragePrice = this.GetAveragePrice(itemTotalPrice, lastPrice, flag);
				this.IsExchangeAvailable = true;
			}
			this.PieceLbl = ((num2 <= 1) ? this._pieceLblSingular : this._pieceLblPlural);
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x0002C604 File Offset: 0x0002A804
		public string GetAveragePrice(int totalPrice, int lastPrice, bool isBuying)
		{
			InventoryLogic.InventorySide side = isBuying ? InventoryLogic.InventorySide.OtherInventory : InventoryLogic.InventorySide.PlayerInventory;
			int costOfItemRosterElement = this._inventoryLogic.GetCostOfItemRosterElement(this._referenceItemRoster, side);
			if (costOfItemRosterElement == lastPrice)
			{
				return costOfItemRosterElement.ToString();
			}
			if (costOfItemRosterElement < lastPrice)
			{
				return costOfItemRosterElement + " - " + lastPrice;
			}
			return lastPrice + " - " + costOfItemRosterElement;
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x0002C669 File Offset: 0x0002A869
		public void ExecuteIncreaseThisStock()
		{
			if (this.ThisStock < this.TotalStock)
			{
				this.ThisStock++;
			}
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x0002C687 File Offset: 0x0002A887
		public void ExecuteIncreaseOtherStock()
		{
			if (this.ThisStock > 0)
			{
				this.ThisStock--;
			}
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x0002C6A0 File Offset: 0x0002A8A0
		public void ExecuteReset()
		{
			this.ThisStock = this.InitialThisStock;
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x0002C6B0 File Offset: 0x0002A8B0
		public void ExecuteApplyTransaction()
		{
			int num = this.ThisStock - this.InitialThisStock;
			if (num == 0 || this._onApplyTransaction == null)
			{
				return;
			}
			bool flag = num >= 0;
			int arg = flag ? num : (-num);
			bool arg2 = this._isPlayerItem ? flag : (!flag);
			this._onApplyTransaction(arg, arg2);
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x0002C705 File Offset: 0x0002A905
		public void ExecuteRemoveZeroCounts()
		{
			Action removeZeroCounts = InventoryTradeVM.RemoveZeroCounts;
			if (removeZeroCounts == null)
			{
				return;
			}
			removeZeroCounts();
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000B3F RID: 2879 RVA: 0x0002C716 File Offset: 0x0002A916
		// (set) Token: 0x06000B40 RID: 2880 RVA: 0x0002C71E File Offset: 0x0002A91E
		[DataSourceProperty]
		public string ThisStockLbl
		{
			get
			{
				return this._thisStockLbl;
			}
			set
			{
				if (value != this._thisStockLbl)
				{
					this._thisStockLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "ThisStockLbl");
				}
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000B41 RID: 2881 RVA: 0x0002C741 File Offset: 0x0002A941
		// (set) Token: 0x06000B42 RID: 2882 RVA: 0x0002C749 File Offset: 0x0002A949
		[DataSourceProperty]
		public string OtherStockLbl
		{
			get
			{
				return this._otherStockLbl;
			}
			set
			{
				if (value != this._otherStockLbl)
				{
					this._otherStockLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "OtherStockLbl");
				}
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000B43 RID: 2883 RVA: 0x0002C76C File Offset: 0x0002A96C
		// (set) Token: 0x06000B44 RID: 2884 RVA: 0x0002C774 File Offset: 0x0002A974
		[DataSourceProperty]
		public string PieceLbl
		{
			get
			{
				return this._pieceLbl;
			}
			set
			{
				if (value != this._pieceLbl)
				{
					this._pieceLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "PieceLbl");
				}
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000B45 RID: 2885 RVA: 0x0002C797 File Offset: 0x0002A997
		// (set) Token: 0x06000B46 RID: 2886 RVA: 0x0002C79F File Offset: 0x0002A99F
		[DataSourceProperty]
		public string AveragePriceLbl
		{
			get
			{
				return this._averagePriceLbl;
			}
			set
			{
				if (value != this._averagePriceLbl)
				{
					this._averagePriceLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "AveragePriceLbl");
				}
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000B47 RID: 2887 RVA: 0x0002C7C2 File Offset: 0x0002A9C2
		// (set) Token: 0x06000B48 RID: 2888 RVA: 0x0002C7CA File Offset: 0x0002A9CA
		[DataSourceProperty]
		public HintViewModel ApplyExchangeHint
		{
			get
			{
				return this._applyExchangeHint;
			}
			set
			{
				if (value != this._applyExchangeHint)
				{
					this._applyExchangeHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "ApplyExchangeHint");
				}
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000B49 RID: 2889 RVA: 0x0002C7E8 File Offset: 0x0002A9E8
		// (set) Token: 0x06000B4A RID: 2890 RVA: 0x0002C7F0 File Offset: 0x0002A9F0
		[DataSourceProperty]
		public bool IsExchangeAvailable
		{
			get
			{
				return this._isExchangeAvailable;
			}
			set
			{
				if (value != this._isExchangeAvailable)
				{
					this._isExchangeAvailable = value;
					base.OnPropertyChangedWithValue(value, "IsExchangeAvailable");
				}
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000B4B RID: 2891 RVA: 0x0002C80E File Offset: 0x0002AA0E
		// (set) Token: 0x06000B4C RID: 2892 RVA: 0x0002C816 File Offset: 0x0002AA16
		[DataSourceProperty]
		public string PriceChange
		{
			get
			{
				return this._priceChange;
			}
			set
			{
				if (value != this._priceChange)
				{
					this._priceChange = value;
					base.OnPropertyChangedWithValue<string>(value, "PriceChange");
				}
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000B4D RID: 2893 RVA: 0x0002C839 File Offset: 0x0002AA39
		// (set) Token: 0x06000B4E RID: 2894 RVA: 0x0002C841 File Offset: 0x0002AA41
		[DataSourceProperty]
		public string PieceChange
		{
			get
			{
				return this._pieceChange;
			}
			set
			{
				if (value != this._pieceChange)
				{
					this._pieceChange = value;
					base.OnPropertyChangedWithValue<string>(value, "PieceChange");
				}
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000B4F RID: 2895 RVA: 0x0002C864 File Offset: 0x0002AA64
		// (set) Token: 0x06000B50 RID: 2896 RVA: 0x0002C86C File Offset: 0x0002AA6C
		[DataSourceProperty]
		public string AveragePrice
		{
			get
			{
				return this._averagePrice;
			}
			set
			{
				if (value != this._averagePrice)
				{
					this._averagePrice = value;
					base.OnPropertyChangedWithValue<string>(value, "AveragePrice");
				}
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000B51 RID: 2897 RVA: 0x0002C88F File Offset: 0x0002AA8F
		// (set) Token: 0x06000B52 RID: 2898 RVA: 0x0002C897 File Offset: 0x0002AA97
		[DataSourceProperty]
		public int ThisStock
		{
			get
			{
				return this._thisStock;
			}
			set
			{
				if (value != this._thisStock)
				{
					this._thisStock = value;
					base.OnPropertyChangedWithValue(value, "ThisStock");
					this.ThisStockUpdated();
				}
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000B53 RID: 2899 RVA: 0x0002C8BB File Offset: 0x0002AABB
		// (set) Token: 0x06000B54 RID: 2900 RVA: 0x0002C8C3 File Offset: 0x0002AAC3
		[DataSourceProperty]
		public int InitialThisStock
		{
			get
			{
				return this._initialThisStock;
			}
			set
			{
				if (value != this._initialThisStock)
				{
					this._initialThisStock = value;
					base.OnPropertyChangedWithValue(value, "InitialThisStock");
				}
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000B55 RID: 2901 RVA: 0x0002C8E1 File Offset: 0x0002AAE1
		// (set) Token: 0x06000B56 RID: 2902 RVA: 0x0002C8E9 File Offset: 0x0002AAE9
		[DataSourceProperty]
		public int OtherStock
		{
			get
			{
				return this._otherStock;
			}
			set
			{
				if (value != this._otherStock)
				{
					this._otherStock = value;
					base.OnPropertyChangedWithValue(value, "OtherStock");
				}
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000B57 RID: 2903 RVA: 0x0002C907 File Offset: 0x0002AB07
		// (set) Token: 0x06000B58 RID: 2904 RVA: 0x0002C90F File Offset: 0x0002AB0F
		[DataSourceProperty]
		public int InitialOtherStock
		{
			get
			{
				return this._initialOtherStock;
			}
			set
			{
				if (value != this._initialOtherStock)
				{
					this._initialOtherStock = value;
					base.OnPropertyChangedWithValue(value, "InitialOtherStock");
				}
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000B59 RID: 2905 RVA: 0x0002C92D File Offset: 0x0002AB2D
		// (set) Token: 0x06000B5A RID: 2906 RVA: 0x0002C935 File Offset: 0x0002AB35
		[DataSourceProperty]
		public int TotalStock
		{
			get
			{
				return this._totalStock;
			}
			set
			{
				if (value != this._totalStock)
				{
					this._totalStock = value;
					base.OnPropertyChangedWithValue(value, "TotalStock");
				}
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000B5B RID: 2907 RVA: 0x0002C953 File Offset: 0x0002AB53
		// (set) Token: 0x06000B5C RID: 2908 RVA: 0x0002C95B File Offset: 0x0002AB5B
		[DataSourceProperty]
		public bool IsThisStockIncreasable
		{
			get
			{
				return this._isThisStockIncreasable;
			}
			set
			{
				if (value != this._isThisStockIncreasable)
				{
					this._isThisStockIncreasable = value;
					base.OnPropertyChangedWithValue(value, "IsThisStockIncreasable");
				}
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000B5D RID: 2909 RVA: 0x0002C979 File Offset: 0x0002AB79
		// (set) Token: 0x06000B5E RID: 2910 RVA: 0x0002C981 File Offset: 0x0002AB81
		[DataSourceProperty]
		public bool IsOtherStockIncreasable
		{
			get
			{
				return this._isOtherStockIncreasable;
			}
			set
			{
				if (value != this._isOtherStockIncreasable)
				{
					this._isOtherStockIncreasable = value;
					base.OnPropertyChangedWithValue(value, "IsOtherStockIncreasable");
				}
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000B5F RID: 2911 RVA: 0x0002C99F File Offset: 0x0002AB9F
		// (set) Token: 0x06000B60 RID: 2912 RVA: 0x0002C9A7 File Offset: 0x0002ABA7
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

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000B61 RID: 2913 RVA: 0x0002C9C5 File Offset: 0x0002ABC5
		// (set) Token: 0x06000B62 RID: 2914 RVA: 0x0002C9CD File Offset: 0x0002ABCD
		[DataSourceProperty]
		public bool IsTradeable
		{
			get
			{
				return this._isTradeable;
			}
			set
			{
				if (value != this._isTradeable)
				{
					this._isTradeable = value;
					base.OnPropertyChangedWithValue(value, "IsTradeable");
				}
			}
		}

		// Token: 0x04000510 RID: 1296
		private InventoryLogic _inventoryLogic;

		// Token: 0x04000511 RID: 1297
		private ItemRosterElement _referenceItemRoster;

		// Token: 0x04000512 RID: 1298
		private Action<int, bool> _onApplyTransaction;

		// Token: 0x04000513 RID: 1299
		private string _pieceLblSingular;

		// Token: 0x04000514 RID: 1300
		private string _pieceLblPlural;

		// Token: 0x04000515 RID: 1301
		private bool _isPlayerItem;

		// Token: 0x04000516 RID: 1302
		private string _thisStockLbl;

		// Token: 0x04000517 RID: 1303
		private string _otherStockLbl;

		// Token: 0x04000518 RID: 1304
		private string _averagePriceLbl;

		// Token: 0x04000519 RID: 1305
		private string _pieceLbl;

		// Token: 0x0400051A RID: 1306
		private HintViewModel _applyExchangeHint;

		// Token: 0x0400051B RID: 1307
		private bool _isExchangeAvailable;

		// Token: 0x0400051C RID: 1308
		private string _averagePrice;

		// Token: 0x0400051D RID: 1309
		private string _pieceChange;

		// Token: 0x0400051E RID: 1310
		private string _priceChange;

		// Token: 0x0400051F RID: 1311
		private int _thisStock = -1;

		// Token: 0x04000520 RID: 1312
		private int _initialThisStock;

		// Token: 0x04000521 RID: 1313
		private int _otherStock = -1;

		// Token: 0x04000522 RID: 1314
		private int _initialOtherStock;

		// Token: 0x04000523 RID: 1315
		private int _totalStock;

		// Token: 0x04000524 RID: 1316
		private bool _isThisStockIncreasable;

		// Token: 0x04000525 RID: 1317
		private bool _isOtherStockIncreasable;

		// Token: 0x04000526 RID: 1318
		private bool _isTrading;

		// Token: 0x04000527 RID: 1319
		private bool _isTradeable;
	}
}
