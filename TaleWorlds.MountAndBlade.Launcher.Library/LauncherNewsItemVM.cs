using System;
using System.Diagnostics;
using TaleWorlds.Library;
using TaleWorlds.Library.NewsManager;

namespace TaleWorlds.MountAndBlade.Launcher.Library
{
	// Token: 0x02000015 RID: 21
	public class LauncherNewsItemVM : ViewModel
	{
		// Token: 0x060000BC RID: 188 RVA: 0x00004908 File Offset: 0x00002B08
		public LauncherNewsItemVM(NewsItem item, bool isMultiplayer)
		{
			this.Category = item.Title;
			this.Title = item.Description;
			this.NewsImageUrl = item.ImageSourcePath;
			this._link = item.NewsLink + (isMultiplayer ? "?referrer=launchermp" : "?referrer=launchersp");
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004963 File Offset: 0x00002B63
		private void ExecuteOpenLink()
		{
			if (!string.IsNullOrEmpty(this._link))
			{
				Process.Start(new ProcessStartInfo(this._link)
				{
					UseShellExecute = true
				});
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000BE RID: 190 RVA: 0x0000498A File Offset: 0x00002B8A
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00004992 File Offset: 0x00002B92
		[DataSourceProperty]
		public string NewsImageUrl
		{
			get
			{
				return this._newsImageUrl;
			}
			set
			{
				if (value != this._newsImageUrl)
				{
					this._newsImageUrl = value;
					base.OnPropertyChangedWithValue<string>(value, "NewsImageUrl");
				}
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x000049B5 File Offset: 0x00002BB5
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x000049BD File Offset: 0x00002BBD
		[DataSourceProperty]
		public string Category
		{
			get
			{
				return this._category;
			}
			set
			{
				if (value != this._category)
				{
					this._category = value;
					base.OnPropertyChangedWithValue<string>(value, "Category");
				}
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x000049E0 File Offset: 0x00002BE0
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x000049E8 File Offset: 0x00002BE8
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

		// Token: 0x04000060 RID: 96
		private string _link;

		// Token: 0x04000061 RID: 97
		private string _newsImageUrl;

		// Token: 0x04000062 RID: 98
		private string _category;

		// Token: 0x04000063 RID: 99
		private string _title;
	}
}
