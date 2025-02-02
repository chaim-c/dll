using System;

namespace TaleWorlds.CampaignSystem.BarterSystem
{
	// Token: 0x0200040A RID: 1034
	public class GoldBarterGroup : BarterGroup
	{
		// Token: 0x17000D09 RID: 3337
		// (get) Token: 0x06003F23 RID: 16163 RVA: 0x001390A0 File Offset: 0x001372A0
		public override float AIDecisionWeight
		{
			get
			{
				return 0.6f;
			}
		}
	}
}
