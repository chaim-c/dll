using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.Encyclopedia;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.List
{
	// Token: 0x020000C4 RID: 196
	public class EncyclopediaListItemComparer : IComparer<EncyclopediaListItemVM>
	{
		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x0600136B RID: 4971 RVA: 0x0004AF5B File Offset: 0x0004915B
		public EncyclopediaSortController SortController { get; }

		// Token: 0x0600136C RID: 4972 RVA: 0x0004AF63 File Offset: 0x00049163
		public EncyclopediaListItemComparer(EncyclopediaSortController sortController)
		{
			this.SortController = sortController;
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x0004AF74 File Offset: 0x00049174
		private int GetBookmarkComparison(EncyclopediaListItemVM x, EncyclopediaListItemVM y)
		{
			return -x.IsBookmarked.CompareTo(y.IsBookmarked);
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x0004AF98 File Offset: 0x00049198
		public int Compare(EncyclopediaListItemVM x, EncyclopediaListItemVM y)
		{
			int bookmarkComparison = this.GetBookmarkComparison(x, y);
			if (bookmarkComparison != 0)
			{
				return bookmarkComparison;
			}
			return this.SortController.Comparer.Compare(x.ListItem, y.ListItem);
		}
	}
}
