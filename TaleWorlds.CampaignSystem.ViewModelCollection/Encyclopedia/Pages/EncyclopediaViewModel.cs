using System;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Pages
{
	// Token: 0x020000BD RID: 189
	public class EncyclopediaViewModel : Attribute
	{
		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x060012AF RID: 4783 RVA: 0x000490A1 File Offset: 0x000472A1
		// (set) Token: 0x060012B0 RID: 4784 RVA: 0x000490A9 File Offset: 0x000472A9
		public Type PageTargetType { get; private set; }

		// Token: 0x060012B1 RID: 4785 RVA: 0x000490B2 File Offset: 0x000472B2
		public EncyclopediaViewModel(Type pageTargetType)
		{
			this.PageTargetType = pageTargetType;
		}
	}
}
