using System;

namespace TaleWorlds.CampaignSystem.BarterSystem
{
	// Token: 0x0200040D RID: 1037
	public class PrisonerBarterGroup : BarterGroup
	{
		// Token: 0x17000D0C RID: 3340
		// (get) Token: 0x06003F29 RID: 16169 RVA: 0x001390CD File Offset: 0x001372CD
		public override float AIDecisionWeight
		{
			get
			{
				return 0.7f;
			}
		}
	}
}
