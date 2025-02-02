using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.BannerBuilder
{
	// Token: 0x02000075 RID: 117
	public class BannerBuilderColorItemVM : ViewModel
	{
		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x00025958 File Offset: 0x00023B58
		// (set) Token: 0x06000995 RID: 2453 RVA: 0x00025960 File Offset: 0x00023B60
		public int ColorID { get; private set; }

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000996 RID: 2454 RVA: 0x00025969 File Offset: 0x00023B69
		// (set) Token: 0x06000997 RID: 2455 RVA: 0x00025971 File Offset: 0x00023B71
		public BannerColor BannerColor { get; private set; }

		// Token: 0x06000998 RID: 2456 RVA: 0x0002597C File Offset: 0x00023B7C
		public BannerBuilderColorItemVM(Action<BannerBuilderColorItemVM> onItemSelection, int key, BannerColor value)
		{
			this._onItemSelection = onItemSelection;
			this.ColorID = key;
			this.BannerColor = value;
			this.ColorAsStr = Color.FromUint(value.Color).ToString();
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x000259C4 File Offset: 0x00023BC4
		public void ExecuteSelection()
		{
			Action<BannerBuilderColorItemVM> onItemSelection = this._onItemSelection;
			if (onItemSelection == null)
			{
				return;
			}
			onItemSelection(this);
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x0600099A RID: 2458 RVA: 0x000259D7 File Offset: 0x00023BD7
		// (set) Token: 0x0600099B RID: 2459 RVA: 0x000259DF File Offset: 0x00023BDF
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

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x0600099C RID: 2460 RVA: 0x000259FD File Offset: 0x00023BFD
		// (set) Token: 0x0600099D RID: 2461 RVA: 0x00025A05 File Offset: 0x00023C05
		[DataSourceProperty]
		public string ColorAsStr
		{
			get
			{
				return this._colorAsStr;
			}
			set
			{
				if (value != this._colorAsStr)
				{
					this._colorAsStr = value;
					base.OnPropertyChangedWithValue<string>(value, "ColorAsStr");
				}
			}
		}

		// Token: 0x04000492 RID: 1170
		private readonly Action<BannerBuilderColorItemVM> _onItemSelection;

		// Token: 0x04000495 RID: 1173
		private bool _isSelected;

		// Token: 0x04000496 RID: 1174
		private string _colorAsStr;
	}
}
