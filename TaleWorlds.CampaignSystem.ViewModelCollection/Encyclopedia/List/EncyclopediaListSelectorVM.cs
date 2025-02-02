using System;
using TaleWorlds.Core.ViewModelCollection.Selector;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.List
{
	// Token: 0x020000C5 RID: 197
	public class EncyclopediaListSelectorVM : SelectorVM<EncyclopediaListSelectorItemVM>
	{
		// Token: 0x0600136F RID: 4975 RVA: 0x0004AFCF File Offset: 0x000491CF
		public EncyclopediaListSelectorVM(int selectedIndex, Action<SelectorVM<EncyclopediaListSelectorItemVM>> onChange, Action onActivate) : base(selectedIndex, onChange)
		{
			this._onActivate = onActivate;
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x0004AFE0 File Offset: 0x000491E0
		public void ExecuteOnDropdownActivated()
		{
			Action onActivate = this._onActivate;
			if (onActivate == null)
			{
				return;
			}
			onActivate();
		}

		// Token: 0x040008FF RID: 2303
		private Action _onActivate;
	}
}
