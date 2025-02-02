using System;
using TaleWorlds.Core.ViewModelCollection.Selector;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.List
{
	// Token: 0x020000C6 RID: 198
	public class EncyclopediaListSelectorItemVM : SelectorItemVM
	{
		// Token: 0x06001371 RID: 4977 RVA: 0x0004AFF2 File Offset: 0x000491F2
		public EncyclopediaListSelectorItemVM(EncyclopediaListItemComparer comparer) : base(comparer.SortController.Name.ToString())
		{
			this.Comparer = comparer;
		}

		// Token: 0x04000900 RID: 2304
		public EncyclopediaListItemComparer Comparer;
	}
}
