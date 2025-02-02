using System;

namespace TaleWorlds.CampaignSystem.BarterSystem
{
	// Token: 0x0200040E RID: 1038
	public class OtherBarterGroup : BarterGroup
	{
		// Token: 0x17000D0D RID: 3341
		// (get) Token: 0x06003F2B RID: 16171 RVA: 0x001390DC File Offset: 0x001372DC
		public override float AIDecisionWeight
		{
			get
			{
				return 0.25f;
			}
		}
	}
}
