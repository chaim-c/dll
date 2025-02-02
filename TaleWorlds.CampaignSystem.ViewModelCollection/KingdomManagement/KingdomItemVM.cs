using System;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.KingdomManagement
{
	// Token: 0x02000058 RID: 88
	public abstract class KingdomItemVM : ViewModel
	{
		// Token: 0x0600070E RID: 1806 RVA: 0x0001F9B7 File Offset: 0x0001DBB7
		protected virtual void OnSelect()
		{
			this.IsSelected = true;
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x0001F9C0 File Offset: 0x0001DBC0
		// (set) Token: 0x06000710 RID: 1808 RVA: 0x0001F9C8 File Offset: 0x0001DBC8
		[DataSourceProperty]
		public bool IsNew
		{
			get
			{
				return this._isNew;
			}
			set
			{
				if (value != this._isNew)
				{
					this._isNew = value;
					base.OnPropertyChangedWithValue(value, "IsNew");
				}
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000711 RID: 1809 RVA: 0x0001F9E6 File Offset: 0x0001DBE6
		// (set) Token: 0x06000712 RID: 1810 RVA: 0x0001F9EE File Offset: 0x0001DBEE
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

		// Token: 0x0400031E RID: 798
		private bool _isSelected;

		// Token: 0x0400031F RID: 799
		private bool _isNew;
	}
}
