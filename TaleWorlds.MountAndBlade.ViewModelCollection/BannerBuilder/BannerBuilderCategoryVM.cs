using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.BannerBuilder
{
	// Token: 0x02000074 RID: 116
	public class BannerBuilderCategoryVM : ViewModel
	{
		// Token: 0x06000989 RID: 2441 RVA: 0x00025788 File Offset: 0x00023988
		public BannerBuilderCategoryVM(BannerIconGroup category, Action<BannerBuilderItemVM> onItemSelection)
		{
			this.ItemsList = new MBBindingList<BannerBuilderItemVM>();
			this._category = category;
			this._onItemSelection = onItemSelection;
			this.IsPattern = this._category.IsPattern;
			this.IsEnabled = true;
			this.PopulateItems();
			this.RefreshValues();
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x000257D8 File Offset: 0x000239D8
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Title = this._category.Name.ToString();
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x000257F8 File Offset: 0x000239F8
		private void PopulateItems()
		{
			this.ItemsList.Clear();
			if (this.IsPattern)
			{
				for (int i = 0; i < this._category.AllBackgrounds.Count; i++)
				{
					KeyValuePair<int, string> keyValuePair = this._category.AllBackgrounds.ElementAt(i);
					this.ItemsList.Add(new BannerBuilderItemVM(keyValuePair.Key, keyValuePair.Value, this._onItemSelection));
				}
				return;
			}
			for (int j = 0; j < this._category.AllIcons.Count; j++)
			{
				KeyValuePair<int, BannerIconData> keyValuePair2 = this._category.AllIcons.ElementAt(j);
				this.ItemsList.Add(new BannerBuilderItemVM(keyValuePair2.Key, keyValuePair2.Value, this._onItemSelection));
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x0600098C RID: 2444 RVA: 0x000258BB File Offset: 0x00023ABB
		// (set) Token: 0x0600098D RID: 2445 RVA: 0x000258C3 File Offset: 0x00023AC3
		[DataSourceProperty]
		public string Title
		{
			get
			{
				return this._title;
			}
			set
			{
				if (value != this._title)
				{
					this._title = value;
					base.OnPropertyChangedWithValue<string>(value, "Title");
				}
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x0600098E RID: 2446 RVA: 0x000258E6 File Offset: 0x00023AE6
		// (set) Token: 0x0600098F RID: 2447 RVA: 0x000258EE File Offset: 0x00023AEE
		[DataSourceProperty]
		public bool IsPattern
		{
			get
			{
				return this._isPattern;
			}
			set
			{
				if (value != this._isPattern)
				{
					this._isPattern = value;
					base.OnPropertyChangedWithValue(value, "IsPattern");
				}
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000990 RID: 2448 RVA: 0x0002590C File Offset: 0x00023B0C
		// (set) Token: 0x06000991 RID: 2449 RVA: 0x00025914 File Offset: 0x00023B14
		[DataSourceProperty]
		public bool IsEnabled
		{
			get
			{
				return this._isEnabled;
			}
			set
			{
				if (value != this._isEnabled)
				{
					this._isEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsEnabled");
				}
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000992 RID: 2450 RVA: 0x00025932 File Offset: 0x00023B32
		// (set) Token: 0x06000993 RID: 2451 RVA: 0x0002593A File Offset: 0x00023B3A
		[DataSourceProperty]
		public MBBindingList<BannerBuilderItemVM> ItemsList
		{
			get
			{
				return this._itemsList;
			}
			set
			{
				if (value != this._itemsList)
				{
					this._itemsList = value;
					base.OnPropertyChangedWithValue<MBBindingList<BannerBuilderItemVM>>(value, "ItemsList");
				}
			}
		}

		// Token: 0x0400048C RID: 1164
		private readonly BannerIconGroup _category;

		// Token: 0x0400048D RID: 1165
		private readonly Action<BannerBuilderItemVM> _onItemSelection;

		// Token: 0x0400048E RID: 1166
		private string _title;

		// Token: 0x0400048F RID: 1167
		private bool _isPattern;

		// Token: 0x04000490 RID: 1168
		private bool _isEnabled;

		// Token: 0x04000491 RID: 1169
		private MBBindingList<BannerBuilderItemVM> _itemsList;
	}
}
