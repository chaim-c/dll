using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Library;
using TaleWorlds.Library.NewsManager;

namespace TaleWorlds.MountAndBlade.Launcher.Library
{
	// Token: 0x02000016 RID: 22
	public class LauncherNewsVM : ViewModel
	{
		// Token: 0x060000C4 RID: 196 RVA: 0x00004A0B File Offset: 0x00002C0B
		public LauncherNewsVM(NewsManager newsManager, bool isDefaultMultiplayer)
		{
			this._newsManager = newsManager;
			this.NewsItems = new MBBindingList<LauncherNewsItemVM>();
			this.GetNewsItems(isDefaultMultiplayer);
			this.IsDisabledOnMultiplayer = false;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00004A34 File Offset: 0x00002C34
		private async void GetNewsItems(bool isMultiplayer)
		{
			await this._newsManager.GetNewsItems(false);
			this.Refresh(isMultiplayer);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004A78 File Offset: 0x00002C78
		public void Refresh(bool isMultiplayer)
		{
			this.NewsItems.Clear();
			this.MainNews = new LauncherNewsItemVM(default(NewsItem), isMultiplayer);
			NewsItem.NewsTypes singleplayerMultiplayerEnum = isMultiplayer ? NewsItem.NewsTypes.LauncherMultiplayer : NewsItem.NewsTypes.LauncherSingleplayer;
			Func<NewsType, bool> <>9__3;
			Func<NewsType, bool> <>9__4;
			List<IGrouping<int, NewsItem>> list = (from i in this._newsManager.NewsItems.Where(delegate(NewsItem n)
			{
				IEnumerable<NewsType> feeds = n.Feeds;
				Func<NewsType, bool> predicate;
				if ((predicate = <>9__3) == null)
				{
					predicate = (<>9__3 = ((NewsType t) => t.Type == singleplayerMultiplayerEnum));
				}
				return feeds.Any(predicate) && !string.IsNullOrEmpty(n.Title) && !string.IsNullOrEmpty(n.NewsLink) && !string.IsNullOrEmpty(n.ImageSourcePath);
			}).ToList<NewsItem>().GroupBy(delegate(NewsItem i)
			{
				IEnumerable<NewsType> feeds = i.Feeds;
				Func<NewsType, bool> predicate;
				if ((predicate = <>9__4) == null)
				{
					predicate = (<>9__4 = ((NewsType t) => t.Type == singleplayerMultiplayerEnum));
				}
				return feeds.First(predicate).Index;
			}).ToList<IGrouping<int, NewsItem>>()
			orderby i.Key
			select i).ToList<IGrouping<int, NewsItem>>();
			int num = 0;
			while (num < list.Count && this.NewsItems.Count < 3)
			{
				NewsItem newsItem = list[num].First<NewsItem>();
				NewsItem item = newsItem.Equals(default(NewsItem)) ? default(NewsItem) : newsItem;
				this.NewsItems.Add(new LauncherNewsItemVM(item, isMultiplayer));
				num++;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00004B85 File Offset: 0x00002D85
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00004B8D File Offset: 0x00002D8D
		[DataSourceProperty]
		public bool IsDisabledOnMultiplayer
		{
			get
			{
				return this._isDisabledOnMultiplayer;
			}
			set
			{
				if (value != this._isDisabledOnMultiplayer)
				{
					this._isDisabledOnMultiplayer = value;
					base.OnPropertyChangedWithValue(value, "IsDisabledOnMultiplayer");
				}
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00004BAB File Offset: 0x00002DAB
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00004BB3 File Offset: 0x00002DB3
		[DataSourceProperty]
		public LauncherNewsItemVM MainNews
		{
			get
			{
				return this._mainNews;
			}
			set
			{
				if (value != this._mainNews)
				{
					this._mainNews = value;
					base.OnPropertyChangedWithValue<LauncherNewsItemVM>(value, "MainNews");
				}
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00004BD1 File Offset: 0x00002DD1
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00004BD9 File Offset: 0x00002DD9
		[DataSourceProperty]
		public MBBindingList<LauncherNewsItemVM> NewsItems
		{
			get
			{
				return this._newsItems;
			}
			set
			{
				if (value != this._newsItems)
				{
					this._newsItems = value;
					base.OnPropertyChangedWithValue<MBBindingList<LauncherNewsItemVM>>(value, "NewsItems");
				}
			}
		}

		// Token: 0x04000064 RID: 100
		private readonly NewsManager _newsManager;

		// Token: 0x04000065 RID: 101
		private const int _numOfNewsItemsToShow = 3;

		// Token: 0x04000066 RID: 102
		private LauncherNewsItemVM _mainNews;

		// Token: 0x04000067 RID: 103
		private MBBindingList<LauncherNewsItemVM> _newsItems;

		// Token: 0x04000068 RID: 104
		private bool _isDisabledOnMultiplayer;
	}
}
