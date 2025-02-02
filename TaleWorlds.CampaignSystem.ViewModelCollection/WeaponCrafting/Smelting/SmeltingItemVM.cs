using System;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting.Smelting
{
	// Token: 0x020000F3 RID: 243
	public class SmeltingItemVM : ViewModel
	{
		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x060016F6 RID: 5878 RVA: 0x00055A6E File Offset: 0x00053C6E
		// (set) Token: 0x060016F5 RID: 5877 RVA: 0x00055A65 File Offset: 0x00053C65
		public EquipmentElement EquipmentElement { get; private set; }

		// Token: 0x060016F7 RID: 5879 RVA: 0x00055A78 File Offset: 0x00053C78
		public SmeltingItemVM(EquipmentElement equipmentElement, Action<SmeltingItemVM> onSelection, Action<SmeltingItemVM, bool> onItemLockedStateChange, bool isLocked, int numOfItems)
		{
			this._onSelection = onSelection;
			this._onItemLockedStateChange = onItemLockedStateChange;
			this.EquipmentElement = equipmentElement;
			this.Yield = new MBBindingList<CraftingResourceItemVM>();
			this.InputMaterials = new MBBindingList<CraftingResourceItemVM>();
			this.LockHint = new HintViewModel(GameTexts.FindText("str_inventory_lock", null), null);
			int[] smeltingOutputForItem = Campaign.Current.Models.SmithingModel.GetSmeltingOutputForItem(equipmentElement.Item);
			for (int i = 0; i < smeltingOutputForItem.Length; i++)
			{
				if (smeltingOutputForItem[i] > 0)
				{
					this.Yield.Add(new CraftingResourceItemVM((CraftingMaterials)i, smeltingOutputForItem[i], 0));
				}
				else if (smeltingOutputForItem[i] < 0)
				{
					this.InputMaterials.Add(new CraftingResourceItemVM((CraftingMaterials)i, -smeltingOutputForItem[i], 0));
				}
			}
			this.IsLocked = isLocked;
			this.Visual = new ImageIdentifierVM(equipmentElement.Item, "");
			this.NumOfItems = numOfItems;
			this.HasMoreThanOneItem = (this.NumOfItems > 1);
			this.RefreshValues();
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x00055B6C File Offset: 0x00053D6C
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Name = this.EquipmentElement.Item.Name.ToString();
		}

		// Token: 0x060016F9 RID: 5881 RVA: 0x00055B9D File Offset: 0x00053D9D
		public void ExecuteSelection()
		{
			this._onSelection(this);
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x00055BAB File Offset: 0x00053DAB
		public void ExecuteShowItemTooltip()
		{
			InformationManager.ShowTooltip(typeof(ItemObject), new object[]
			{
				this.EquipmentElement
			});
		}

		// Token: 0x060016FB RID: 5883 RVA: 0x00055BD0 File Offset: 0x00053DD0
		public void ExecuteHideItemTooltip()
		{
			MBInformationManager.HideInformations();
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x060016FC RID: 5884 RVA: 0x00055BD7 File Offset: 0x00053DD7
		// (set) Token: 0x060016FD RID: 5885 RVA: 0x00055BDF File Offset: 0x00053DDF
		[DataSourceProperty]
		public ImageIdentifierVM Visual
		{
			get
			{
				return this._visual;
			}
			set
			{
				if (value != this._visual)
				{
					this._visual = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "Visual");
				}
			}
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x060016FE RID: 5886 RVA: 0x00055BFD File Offset: 0x00053DFD
		// (set) Token: 0x060016FF RID: 5887 RVA: 0x00055C05 File Offset: 0x00053E05
		[DataSourceProperty]
		public MBBindingList<CraftingResourceItemVM> Yield
		{
			get
			{
				return this._yield;
			}
			set
			{
				if (value != this._yield)
				{
					this._yield = value;
					base.OnPropertyChangedWithValue<MBBindingList<CraftingResourceItemVM>>(value, "Yield");
				}
			}
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06001700 RID: 5888 RVA: 0x00055C23 File Offset: 0x00053E23
		// (set) Token: 0x06001701 RID: 5889 RVA: 0x00055C2B File Offset: 0x00053E2B
		[DataSourceProperty]
		public MBBindingList<CraftingResourceItemVM> InputMaterials
		{
			get
			{
				return this._inputMaterials;
			}
			set
			{
				if (value != this._inputMaterials)
				{
					this._inputMaterials = value;
					base.OnPropertyChangedWithValue<MBBindingList<CraftingResourceItemVM>>(value, "InputMaterials");
				}
			}
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06001702 RID: 5890 RVA: 0x00055C49 File Offset: 0x00053E49
		// (set) Token: 0x06001703 RID: 5891 RVA: 0x00055C51 File Offset: 0x00053E51
		[DataSourceProperty]
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if (value != this._name)
				{
					this._name = value;
					base.OnPropertyChangedWithValue<string>(value, "Name");
				}
			}
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06001704 RID: 5892 RVA: 0x00055C74 File Offset: 0x00053E74
		// (set) Token: 0x06001705 RID: 5893 RVA: 0x00055C7C File Offset: 0x00053E7C
		[DataSourceProperty]
		public int NumOfItems
		{
			get
			{
				return this._numOfItems;
			}
			set
			{
				if (value != this._numOfItems)
				{
					this._numOfItems = value;
					base.OnPropertyChangedWithValue(value, "NumOfItems");
				}
			}
		}

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06001706 RID: 5894 RVA: 0x00055C9A File Offset: 0x00053E9A
		// (set) Token: 0x06001707 RID: 5895 RVA: 0x00055CA2 File Offset: 0x00053EA2
		[DataSourceProperty]
		public bool HasMoreThanOneItem
		{
			get
			{
				return this._hasMoreThanOneItem;
			}
			set
			{
				if (value != this._hasMoreThanOneItem)
				{
					this._hasMoreThanOneItem = value;
					base.OnPropertyChangedWithValue(value, "HasMoreThanOneItem");
				}
			}
		}

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06001708 RID: 5896 RVA: 0x00055CC0 File Offset: 0x00053EC0
		// (set) Token: 0x06001709 RID: 5897 RVA: 0x00055CC8 File Offset: 0x00053EC8
		[DataSourceProperty]
		public bool IsSelected
		{
			get
			{
				return this._isSelected;
			}
			set
			{
				if (value != this._isSelected)
				{
					this._isSelected = value;
					base.OnPropertyChangedWithValue(value, "IsSelected");
				}
			}
		}

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x0600170A RID: 5898 RVA: 0x00055CE6 File Offset: 0x00053EE6
		// (set) Token: 0x0600170B RID: 5899 RVA: 0x00055CEE File Offset: 0x00053EEE
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

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x0600170C RID: 5900 RVA: 0x00055D0C File Offset: 0x00053F0C
		// (set) Token: 0x0600170D RID: 5901 RVA: 0x00055D14 File Offset: 0x00053F14
		[DataSourceProperty]
		public bool IsLocked
		{
			get
			{
				return this._isLocked;
			}
			set
			{
				if (value != this._isLocked)
				{
					this._isLocked = value;
					base.OnPropertyChangedWithValue(value, "IsLocked");
					this._onItemLockedStateChange(this, value);
				}
			}
		}

		// Token: 0x04000AB7 RID: 2743
		private readonly Action<SmeltingItemVM> _onSelection;

		// Token: 0x04000AB8 RID: 2744
		private readonly Action<SmeltingItemVM, bool> _onItemLockedStateChange;

		// Token: 0x04000AB9 RID: 2745
		private ImageIdentifierVM _visual;

		// Token: 0x04000ABA RID: 2746
		private string _name;

		// Token: 0x04000ABB RID: 2747
		private int _numOfItems;

		// Token: 0x04000ABC RID: 2748
		private MBBindingList<CraftingResourceItemVM> _inputMaterials;

		// Token: 0x04000ABD RID: 2749
		private MBBindingList<CraftingResourceItemVM> _yield;

		// Token: 0x04000ABE RID: 2750
		private HintViewModel _lockHint;

		// Token: 0x04000ABF RID: 2751
		private bool _isSelected;

		// Token: 0x04000AC0 RID: 2752
		private bool _isLocked;

		// Token: 0x04000AC1 RID: 2753
		private bool _hasMoreThanOneItem;
	}
}
