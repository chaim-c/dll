using System;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting.WeaponDesign
{
	// Token: 0x020000E6 RID: 230
	public class TierFilterTypeVM : ViewModel
	{
		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06001578 RID: 5496 RVA: 0x00050A6E File Offset: 0x0004EC6E
		public WeaponDesignVM.CraftingPieceTierFilter FilterType { get; }

		// Token: 0x06001579 RID: 5497 RVA: 0x00050A76 File Offset: 0x0004EC76
		public TierFilterTypeVM(WeaponDesignVM.CraftingPieceTierFilter filterType, Action<WeaponDesignVM.CraftingPieceTierFilter> onSelect, string tierName)
		{
			this.FilterType = filterType;
			this._onSelect = onSelect;
			this.TierName = tierName;
		}

		// Token: 0x0600157A RID: 5498 RVA: 0x00050A93 File Offset: 0x0004EC93
		public void ExecuteSelectTier()
		{
			Action<WeaponDesignVM.CraftingPieceTierFilter> onSelect = this._onSelect;
			if (onSelect == null)
			{
				return;
			}
			onSelect(this.FilterType);
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x0600157B RID: 5499 RVA: 0x00050AAB File Offset: 0x0004ECAB
		// (set) Token: 0x0600157C RID: 5500 RVA: 0x00050AB3 File Offset: 0x0004ECB3
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

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x0600157D RID: 5501 RVA: 0x00050AD1 File Offset: 0x0004ECD1
		// (set) Token: 0x0600157E RID: 5502 RVA: 0x00050AD9 File Offset: 0x0004ECD9
		[DataSourceProperty]
		public string TierName
		{
			get
			{
				return this._tierName;
			}
			set
			{
				if (value != this._tierName)
				{
					this._tierName = value;
					base.OnPropertyChangedWithValue<string>(value, "TierName");
				}
			}
		}

		// Token: 0x040009FB RID: 2555
		private readonly Action<WeaponDesignVM.CraftingPieceTierFilter> _onSelect;

		// Token: 0x040009FC RID: 2556
		private bool _isSelected;

		// Token: 0x040009FD RID: 2557
		private string _tierName;
	}
}
