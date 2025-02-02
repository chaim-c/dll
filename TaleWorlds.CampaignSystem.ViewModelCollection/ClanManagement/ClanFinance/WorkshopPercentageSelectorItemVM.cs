using System;
using TaleWorlds.Core.ViewModelCollection.Selector;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement.ClanFinance
{
	// Token: 0x02000118 RID: 280
	public class WorkshopPercentageSelectorItemVM : SelectorItemVM
	{
		// Token: 0x06001AE0 RID: 6880 RVA: 0x00061419 File Offset: 0x0005F619
		public WorkshopPercentageSelectorItemVM(string s, float percentage) : base(s)
		{
			this.Percentage = percentage;
		}

		// Token: 0x04000CB2 RID: 3250
		public readonly float Percentage;
	}
}
