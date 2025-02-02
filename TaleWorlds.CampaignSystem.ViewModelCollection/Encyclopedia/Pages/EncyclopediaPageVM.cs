using System;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.List;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Pages
{
	// Token: 0x020000BC RID: 188
	public class EncyclopediaPageVM : ViewModel
	{
		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x0600129B RID: 4763 RVA: 0x00048F9B File Offset: 0x0004719B
		public object Obj
		{
			get
			{
				return this._args.Obj;
			}
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x00048FA8 File Offset: 0x000471A8
		public virtual string GetName()
		{
			return "";
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x00048FAF File Offset: 0x000471AF
		public virtual string GetNavigationBarURL()
		{
			return "";
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x00048FB6 File Offset: 0x000471B6
		public virtual void Refresh()
		{
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x00048FB8 File Offset: 0x000471B8
		public EncyclopediaPageVM(EncyclopediaPageArgs args)
		{
			this._args = args;
			this.BookmarkHint = new HintViewModel();
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x00048FD2 File Offset: 0x000471D2
		public virtual void OnTick()
		{
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x00048FD4 File Offset: 0x000471D4
		public virtual void ExecuteSwitchBookmarkedState()
		{
			this.IsBookmarked = !this.IsBookmarked;
			this.UpdateBookmarkHintText();
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x00048FEB File Offset: 0x000471EB
		protected void UpdateBookmarkHintText()
		{
			if (this.IsBookmarked)
			{
				this.BookmarkHint.HintText = new TextObject("{=BV5exuPf}Remove From Bookmarks", null);
				return;
			}
			this.BookmarkHint.HintText = new TextObject("{=d8jrv3nA}Add To Bookmarks", null);
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x060012A3 RID: 4771 RVA: 0x00049022 File Offset: 0x00047222
		// (set) Token: 0x060012A4 RID: 4772 RVA: 0x0004902A File Offset: 0x0004722A
		[DataSourceProperty]
		public bool IsLoadingOver
		{
			get
			{
				return this._isLoadingOver;
			}
			set
			{
				if (value != this._isLoadingOver)
				{
					this._isLoadingOver = value;
					base.OnPropertyChangedWithValue(value, "IsLoadingOver");
				}
			}
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x060012A5 RID: 4773 RVA: 0x00049048 File Offset: 0x00047248
		// (set) Token: 0x060012A6 RID: 4774 RVA: 0x00049050 File Offset: 0x00047250
		[DataSourceProperty]
		public bool IsBookmarked
		{
			get
			{
				return this._isBookmarked;
			}
			set
			{
				if (value != this._isBookmarked)
				{
					this._isBookmarked = value;
					base.OnPropertyChanged("IsBookmarked");
				}
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x060012A7 RID: 4775 RVA: 0x0004906D File Offset: 0x0004726D
		// (set) Token: 0x060012A8 RID: 4776 RVA: 0x00049075 File Offset: 0x00047275
		[DataSourceProperty]
		public HintViewModel BookmarkHint
		{
			get
			{
				return this._bookmarkHint;
			}
			set
			{
				if (value != this._bookmarkHint)
				{
					this._bookmarkHint = value;
					base.OnPropertyChanged("BookmarkHint");
				}
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x060012A9 RID: 4777 RVA: 0x00049092 File Offset: 0x00047292
		// (set) Token: 0x060012AA RID: 4778 RVA: 0x00049095 File Offset: 0x00047295
		[DataSourceProperty]
		public virtual MBBindingList<EncyclopediaListItemVM> Items
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x060012AB RID: 4779 RVA: 0x00049097 File Offset: 0x00047297
		// (set) Token: 0x060012AC RID: 4780 RVA: 0x0004909A File Offset: 0x0004729A
		[DataSourceProperty]
		public virtual MBBindingList<EncyclopediaFilterGroupVM> FilterGroups
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x060012AD RID: 4781 RVA: 0x0004909C File Offset: 0x0004729C
		// (set) Token: 0x060012AE RID: 4782 RVA: 0x0004909F File Offset: 0x0004729F
		[DataSourceProperty]
		public virtual EncyclopediaListSortControllerVM SortController
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x040008AA RID: 2218
		private EncyclopediaPageArgs _args;

		// Token: 0x040008AB RID: 2219
		private bool _isLoadingOver;

		// Token: 0x040008AC RID: 2220
		private bool _isBookmarked;

		// Token: 0x040008AD RID: 2221
		private HintViewModel _bookmarkHint;
	}
}
