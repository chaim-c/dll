using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.Encyclopedia;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.List
{
	// Token: 0x020000C1 RID: 193
	public class EncyclopediaListFilterVM : ViewModel
	{
		// Token: 0x06001335 RID: 4917 RVA: 0x0004A872 File Offset: 0x00048A72
		public EncyclopediaListFilterVM(EncyclopediaFilterItem filter, Action<EncyclopediaListFilterVM> UpdateFilters)
		{
			this.Filter = filter;
			this._isSelected = this.Filter.IsActive;
			this._updateFilters = UpdateFilters;
			this.RefreshValues();
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x0004A89F File Offset: 0x00048A9F
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Name = this.Filter.Name.ToString();
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x0004A8BD File Offset: 0x00048ABD
		public void CopyFilterFrom(Dictionary<EncyclopediaFilterItem, bool> filters)
		{
			if (filters.ContainsKey(this.Filter))
			{
				this.IsSelected = filters[this.Filter];
			}
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x0004A8DF File Offset: 0x00048ADF
		public void ExecuteOnFilterActivated()
		{
			Game.Current.EventManager.TriggerEvent<OnEncyclopediaFilterActivatedEvent>(new OnEncyclopediaFilterActivatedEvent());
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06001339 RID: 4921 RVA: 0x0004A8F5 File Offset: 0x00048AF5
		// (set) Token: 0x0600133A RID: 4922 RVA: 0x0004A8FD File Offset: 0x00048AFD
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
					this.Filter.IsActive = value;
					this._updateFilters(this);
				}
			}
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x0600133B RID: 4923 RVA: 0x0004A933 File Offset: 0x00048B33
		// (set) Token: 0x0600133C RID: 4924 RVA: 0x0004A93B File Offset: 0x00048B3B
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

		// Token: 0x040008E6 RID: 2278
		public readonly EncyclopediaFilterItem Filter;

		// Token: 0x040008E7 RID: 2279
		private readonly Action<EncyclopediaListFilterVM> _updateFilters;

		// Token: 0x040008E8 RID: 2280
		private string _name;

		// Token: 0x040008E9 RID: 2281
		private bool _isSelected;
	}
}
