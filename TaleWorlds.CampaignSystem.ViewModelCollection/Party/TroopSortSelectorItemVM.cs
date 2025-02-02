using System;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core.ViewModelCollection.Selector;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Party
{
	// Token: 0x02000027 RID: 39
	public class TroopSortSelectorItemVM : SelectorItemVM
	{
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060002FB RID: 763 RVA: 0x00013B94 File Offset: 0x00011D94
		// (set) Token: 0x060002FC RID: 764 RVA: 0x00013B9C File Offset: 0x00011D9C
		public PartyScreenLogic.TroopSortType SortType { get; private set; }

		// Token: 0x060002FD RID: 765 RVA: 0x00013BA5 File Offset: 0x00011DA5
		public TroopSortSelectorItemVM(TextObject s, PartyScreenLogic.TroopSortType sortType) : base(s)
		{
			this.SortType = sortType;
		}
	}
}
