using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.BarterSystem.Barterables;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Barter
{
	// Token: 0x0200013A RID: 314
	public class BarterItemVM : EncyclopediaLinkVM
	{
		// Token: 0x06001E29 RID: 7721 RVA: 0x0006BB48 File Offset: 0x00069D48
		public BarterItemVM(Barterable barterable, BarterItemVM.BarterTransferEventDelegate OnTransfer, Action onAmountChange, bool isFixed = false)
		{
			this.Barterable = barterable;
			base.ActiveLink = barterable.GetEncyclopediaLink();
			this._onTransfer = OnTransfer;
			this._onAmountChange = onAmountChange;
			this._isFixed = isFixed;
			this.IsItemTransferrable = !isFixed;
			this.BarterableType = this.Barterable.StringID;
			ImageIdentifier visualIdentifier = this.Barterable.GetVisualIdentifier();
			this.HasVisualIdentifier = (visualIdentifier != null);
			if (visualIdentifier != null)
			{
				this.VisualIdentifier = new ImageIdentifierVM(visualIdentifier);
			}
			else
			{
				this.VisualIdentifier = null;
				FiefBarterable fiefBarterable;
				if ((fiefBarterable = (this.Barterable as FiefBarterable)) != null)
				{
					this.FiefFileName = fiefBarterable.TargetSettlement.SettlementComponent.BackgroundMeshName;
				}
			}
			this.TotalItemCount = this.Barterable.MaxAmount;
			this.CurrentOfferedAmount = 1;
			this.IsMultiple = (this.TotalItemCount > 1);
			this.IsOffered = this.Barterable.IsOffered;
			this.RefreshValues();
		}

		// Token: 0x06001E2A RID: 7722 RVA: 0x0006BC4E File Offset: 0x00069E4E
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.ItemLbl = this.Barterable.Name.ToString();
		}

		// Token: 0x06001E2B RID: 7723 RVA: 0x0006BC6C File Offset: 0x00069E6C
		public void RefreshCompabilityWithItem(BarterItemVM item, bool isItemGotOffered)
		{
			if (isItemGotOffered && !item.Barterable.IsCompatible(this.Barterable))
			{
				this._incompatibleItems.Add(item.Barterable);
			}
			else if (!isItemGotOffered && this._incompatibleItems.Contains(item.Barterable))
			{
				this._incompatibleItems.Remove(item.Barterable);
			}
			this.IsItemTransferrable = (this._incompatibleItems.Count <= 0);
		}

		// Token: 0x06001E2C RID: 7724 RVA: 0x0006BCE4 File Offset: 0x00069EE4
		public void ExecuteAddOffered()
		{
			int num = BarterItemVM.IsEntireStackModifierActive ? this.TotalItemCount : (this.CurrentOfferedAmount + (BarterItemVM.IsFiveStackModifierActive ? 5 : 1));
			this.CurrentOfferedAmount = ((num < this.TotalItemCount) ? num : this.TotalItemCount);
		}

		// Token: 0x06001E2D RID: 7725 RVA: 0x0006BD2C File Offset: 0x00069F2C
		public void ExecuteRemoveOffered()
		{
			int num = BarterItemVM.IsEntireStackModifierActive ? 1 : (this.CurrentOfferedAmount - (BarterItemVM.IsFiveStackModifierActive ? 5 : 1));
			this.CurrentOfferedAmount = ((num > 1) ? num : 1);
		}

		// Token: 0x06001E2E RID: 7726 RVA: 0x0006BD64 File Offset: 0x00069F64
		public void ExecuteAction()
		{
			if (this.IsItemTransferrable)
			{
				this._onTransfer(this, false);
			}
		}

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x06001E2F RID: 7727 RVA: 0x0006BD7B File Offset: 0x00069F7B
		// (set) Token: 0x06001E30 RID: 7728 RVA: 0x0006BD83 File Offset: 0x00069F83
		[DataSourceProperty]
		public int TotalItemCount
		{
			get
			{
				return this._totalItemCount;
			}
			set
			{
				if (this._totalItemCount != value)
				{
					this._totalItemCount = value;
					base.OnPropertyChangedWithValue(value, "TotalItemCount");
					this.TotalItemCountText = CampaignUIHelper.GetAbbreviatedValueTextFromValue(value);
				}
			}
		}

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x06001E31 RID: 7729 RVA: 0x0006BDAD File Offset: 0x00069FAD
		// (set) Token: 0x06001E32 RID: 7730 RVA: 0x0006BDB5 File Offset: 0x00069FB5
		[DataSourceProperty]
		public string TotalItemCountText
		{
			get
			{
				return this._totalItemCountText;
			}
			set
			{
				if (this._totalItemCountText != value)
				{
					this._totalItemCountText = value;
					base.OnPropertyChangedWithValue<string>(value, "TotalItemCountText");
				}
			}
		}

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x06001E33 RID: 7731 RVA: 0x0006BDD8 File Offset: 0x00069FD8
		// (set) Token: 0x06001E34 RID: 7732 RVA: 0x0006BDE0 File Offset: 0x00069FE0
		[DataSourceProperty]
		public int CurrentOfferedAmount
		{
			get
			{
				return this._currentOfferedAmount;
			}
			set
			{
				if (this._currentOfferedAmount != value)
				{
					this.Barterable.CurrentAmount = value;
					Action onAmountChange = this._onAmountChange;
					if (onAmountChange != null)
					{
						onAmountChange();
					}
					this._currentOfferedAmount = value;
					base.OnPropertyChangedWithValue(value, "CurrentOfferedAmount");
					this.CurrentOfferedAmountText = CampaignUIHelper.GetAbbreviatedValueTextFromValue(value);
				}
			}
		}

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x06001E35 RID: 7733 RVA: 0x0006BE32 File Offset: 0x0006A032
		// (set) Token: 0x06001E36 RID: 7734 RVA: 0x0006BE3A File Offset: 0x0006A03A
		[DataSourceProperty]
		public string CurrentOfferedAmountText
		{
			get
			{
				return this._currentOfferedAmountText;
			}
			set
			{
				if (this._currentOfferedAmountText != value)
				{
					this._currentOfferedAmountText = value;
					base.OnPropertyChangedWithValue<string>(value, "CurrentOfferedAmountText");
				}
			}
		}

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x06001E37 RID: 7735 RVA: 0x0006BE5D File Offset: 0x0006A05D
		// (set) Token: 0x06001E38 RID: 7736 RVA: 0x0006BE65 File Offset: 0x0006A065
		[DataSourceProperty]
		public string BarterableType
		{
			get
			{
				return this._barterableType;
			}
			set
			{
				if (this._barterableType != value)
				{
					this._barterableType = value;
					base.OnPropertyChangedWithValue<string>(value, "BarterableType");
				}
			}
		}

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x06001E39 RID: 7737 RVA: 0x0006BE88 File Offset: 0x0006A088
		// (set) Token: 0x06001E3A RID: 7738 RVA: 0x0006BE90 File Offset: 0x0006A090
		[DataSourceProperty]
		public bool HasVisualIdentifier
		{
			get
			{
				return this._hasVisualIdentifier;
			}
			set
			{
				if (this._hasVisualIdentifier != value)
				{
					this._hasVisualIdentifier = value;
					base.OnPropertyChangedWithValue(value, "HasVisualIdentifier");
				}
			}
		}

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x06001E3B RID: 7739 RVA: 0x0006BEAE File Offset: 0x0006A0AE
		// (set) Token: 0x06001E3C RID: 7740 RVA: 0x0006BEB6 File Offset: 0x0006A0B6
		[DataSourceProperty]
		public bool IsMultiple
		{
			get
			{
				return this._isMultiple;
			}
			set
			{
				if (this._isMultiple != value)
				{
					this._isMultiple = value;
					base.OnPropertyChangedWithValue(value, "IsMultiple");
				}
			}
		}

		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x06001E3D RID: 7741 RVA: 0x0006BED4 File Offset: 0x0006A0D4
		// (set) Token: 0x06001E3E RID: 7742 RVA: 0x0006BEDC File Offset: 0x0006A0DC
		[DataSourceProperty]
		public bool IsSelectorActive
		{
			get
			{
				return this._isSelectorActive;
			}
			set
			{
				if (this._isSelectorActive != value)
				{
					this._isSelectorActive = value;
					base.OnPropertyChangedWithValue(value, "IsSelectorActive");
				}
			}
		}

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x06001E3F RID: 7743 RVA: 0x0006BEFA File Offset: 0x0006A0FA
		// (set) Token: 0x06001E40 RID: 7744 RVA: 0x0006BF02 File Offset: 0x0006A102
		[DataSourceProperty]
		public ImageIdentifierVM VisualIdentifier
		{
			get
			{
				return this._visualIdentifier;
			}
			set
			{
				if (this._visualIdentifier != value)
				{
					this._visualIdentifier = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "VisualIdentifier");
				}
			}
		}

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x06001E41 RID: 7745 RVA: 0x0006BF20 File Offset: 0x0006A120
		// (set) Token: 0x06001E42 RID: 7746 RVA: 0x0006BF28 File Offset: 0x0006A128
		[DataSourceProperty]
		public string ItemLbl
		{
			get
			{
				return this._itemLbl;
			}
			set
			{
				this._itemLbl = value;
				base.OnPropertyChangedWithValue<string>(value, "ItemLbl");
			}
		}

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x06001E43 RID: 7747 RVA: 0x0006BF3D File Offset: 0x0006A13D
		// (set) Token: 0x06001E44 RID: 7748 RVA: 0x0006BF45 File Offset: 0x0006A145
		[DataSourceProperty]
		public string FiefFileName
		{
			get
			{
				return this._fiefFileName;
			}
			set
			{
				this._fiefFileName = value;
				base.OnPropertyChangedWithValue<string>(value, "FiefFileName");
			}
		}

		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x06001E45 RID: 7749 RVA: 0x0006BF5A File Offset: 0x0006A15A
		// (set) Token: 0x06001E46 RID: 7750 RVA: 0x0006BF62 File Offset: 0x0006A162
		[DataSourceProperty]
		public bool IsItemTransferrable
		{
			get
			{
				return this._isItemTransferrable;
			}
			set
			{
				if (this._isFixed)
				{
					value = false;
				}
				if (this._isItemTransferrable != value)
				{
					this._isItemTransferrable = value;
					base.OnPropertyChangedWithValue(value, "IsItemTransferrable");
				}
			}
		}

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x06001E47 RID: 7751 RVA: 0x0006BF8B File Offset: 0x0006A18B
		// (set) Token: 0x06001E48 RID: 7752 RVA: 0x0006BF93 File Offset: 0x0006A193
		[DataSourceProperty]
		public bool IsOffered
		{
			get
			{
				return this._isOffered;
			}
			set
			{
				if (value != this._isOffered)
				{
					this._isOffered = value;
					base.OnPropertyChangedWithValue(value, "IsOffered");
				}
			}
		}

		// Token: 0x04000E34 RID: 3636
		public static bool IsEntireStackModifierActive;

		// Token: 0x04000E35 RID: 3637
		public static bool IsFiveStackModifierActive;

		// Token: 0x04000E36 RID: 3638
		private readonly BarterItemVM.BarterTransferEventDelegate _onTransfer;

		// Token: 0x04000E37 RID: 3639
		private readonly Action _onAmountChange;

		// Token: 0x04000E38 RID: 3640
		private bool _isFixed;

		// Token: 0x04000E39 RID: 3641
		private List<Barterable> _incompatibleItems = new List<Barterable>();

		// Token: 0x04000E3A RID: 3642
		public Barterable Barterable;

		// Token: 0x04000E3B RID: 3643
		public bool _isOffered;

		// Token: 0x04000E3C RID: 3644
		private bool _isItemTransferrable = true;

		// Token: 0x04000E3D RID: 3645
		private string _itemLbl;

		// Token: 0x04000E3E RID: 3646
		private string _fiefFileName;

		// Token: 0x04000E3F RID: 3647
		private string _barterableType = "NULL";

		// Token: 0x04000E40 RID: 3648
		private string _currentOfferedAmountText;

		// Token: 0x04000E41 RID: 3649
		private ImageIdentifierVM _visualIdentifier;

		// Token: 0x04000E42 RID: 3650
		private bool _isSelectorActive;

		// Token: 0x04000E43 RID: 3651
		private bool _hasVisualIdentifier;

		// Token: 0x04000E44 RID: 3652
		private bool _isMultiple;

		// Token: 0x04000E45 RID: 3653
		private int _totalItemCount;

		// Token: 0x04000E46 RID: 3654
		private string _totalItemCountText;

		// Token: 0x04000E47 RID: 3655
		private int _currentOfferedAmount;

		// Token: 0x0200029F RID: 671
		// (Invoke) Token: 0x06002408 RID: 9224
		public delegate void BarterTransferEventDelegate(BarterItemVM itemVM, bool transferAll);
	}
}
