using System;
using System.Collections.Generic;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement
{
	// Token: 0x02000102 RID: 258
	public readonly struct ClanCardSelectionInfo
	{
		// Token: 0x06001843 RID: 6211 RVA: 0x00059685 File Offset: 0x00057885
		public ClanCardSelectionInfo(TextObject title, IEnumerable<ClanCardSelectionItemInfo> items, Action<List<object>, Action> onClosedAction, bool isMultiSelection)
		{
			this.Title = title;
			this.Items = items;
			this.OnClosedAction = onClosedAction;
			this.IsMultiSelection = isMultiSelection;
		}

		// Token: 0x04000B68 RID: 2920
		public readonly TextObject Title;

		// Token: 0x04000B69 RID: 2921
		public readonly IEnumerable<ClanCardSelectionItemInfo> Items;

		// Token: 0x04000B6A RID: 2922
		public readonly Action<List<object>, Action> OnClosedAction;

		// Token: 0x04000B6B RID: 2923
		public readonly bool IsMultiSelection;
	}
}
