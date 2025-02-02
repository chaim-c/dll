using System;
using TaleWorlds.Library;

namespace TaleWorlds.Core.ViewModelCollection.BannerEditor
{
	// Token: 0x02000027 RID: 39
	public class BannerIconVM : ViewModel
	{
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00005AE6 File Offset: 0x00003CE6
		public int IconID { get; }

		// Token: 0x060001CE RID: 462 RVA: 0x00005AEE File Offset: 0x00003CEE
		public BannerIconVM(int iconID, Action<BannerIconVM> onSelection)
		{
			this.IconPath = iconID.ToString();
			this.IconID = iconID;
			this._onSelection = onSelection;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00005B11 File Offset: 0x00003D11
		public void ExecuteSelectIcon()
		{
			this._onSelection(this);
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x00005B1F File Offset: 0x00003D1F
		// (set) Token: 0x060001D1 RID: 465 RVA: 0x00005B27 File Offset: 0x00003D27
		[DataSourceProperty]
		public string IconPath
		{
			get
			{
				return this._iconPath;
			}
			set
			{
				if (value != this._iconPath)
				{
					this._iconPath = value;
					base.OnPropertyChangedWithValue<string>(value, "IconPath");
				}
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00005B4A File Offset: 0x00003D4A
		// (set) Token: 0x060001D3 RID: 467 RVA: 0x00005B52 File Offset: 0x00003D52
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

		// Token: 0x040000BC RID: 188
		private readonly Action<BannerIconVM> _onSelection;

		// Token: 0x040000BD RID: 189
		private string _iconPath;

		// Token: 0x040000BE RID: 190
		private bool _isSelected;
	}
}
