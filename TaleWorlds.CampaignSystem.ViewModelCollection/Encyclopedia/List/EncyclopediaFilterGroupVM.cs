using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.Encyclopedia;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.List
{
	// Token: 0x020000C0 RID: 192
	public class EncyclopediaFilterGroupVM : ViewModel
	{
		// Token: 0x0600132E RID: 4910 RVA: 0x0004A720 File Offset: 0x00048920
		public EncyclopediaFilterGroupVM(EncyclopediaFilterGroup filterGroup, Action<EncyclopediaListFilterVM> UpdateFilters)
		{
			this.FilterGroup = filterGroup;
			this.Filters = new MBBindingList<EncyclopediaListFilterVM>();
			foreach (EncyclopediaFilterItem filter in filterGroup.Filters)
			{
				this.Filters.Add(new EncyclopediaListFilterVM(filter, UpdateFilters));
			}
			this.RefreshValues();
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x0004A79C File Offset: 0x0004899C
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Filters.ApplyActionOnAllItems(delegate(EncyclopediaListFilterVM x)
			{
				x.RefreshValues();
			});
			this.FilterName = this.FilterGroup.Name.ToString();
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x0004A7F0 File Offset: 0x000489F0
		public void CopyFiltersFrom(Dictionary<EncyclopediaFilterItem, bool> filters)
		{
			this.Filters.ApplyActionOnAllItems(delegate(EncyclopediaListFilterVM x)
			{
				x.CopyFilterFrom(filters);
			});
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06001331 RID: 4913 RVA: 0x0004A821 File Offset: 0x00048A21
		// (set) Token: 0x06001332 RID: 4914 RVA: 0x0004A829 File Offset: 0x00048A29
		[DataSourceProperty]
		public string FilterName
		{
			get
			{
				return this._filterName;
			}
			set
			{
				if (value != this._filterName)
				{
					this._filterName = value;
					base.OnPropertyChangedWithValue<string>(value, "FilterName");
				}
			}
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06001333 RID: 4915 RVA: 0x0004A84C File Offset: 0x00048A4C
		// (set) Token: 0x06001334 RID: 4916 RVA: 0x0004A854 File Offset: 0x00048A54
		[DataSourceProperty]
		public MBBindingList<EncyclopediaListFilterVM> Filters
		{
			get
			{
				return this._filters;
			}
			set
			{
				if (value != this._filters)
				{
					this._filters = value;
					base.OnPropertyChangedWithValue<MBBindingList<EncyclopediaListFilterVM>>(value, "Filters");
				}
			}
		}

		// Token: 0x040008E3 RID: 2275
		public readonly EncyclopediaFilterGroup FilterGroup;

		// Token: 0x040008E4 RID: 2276
		private MBBindingList<EncyclopediaListFilterVM> _filters;

		// Token: 0x040008E5 RID: 2277
		private string _filterName;
	}
}
