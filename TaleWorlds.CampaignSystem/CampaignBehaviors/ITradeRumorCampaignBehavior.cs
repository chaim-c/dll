using System;
using System.Collections.Generic;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x020003A6 RID: 934
	public interface ITradeRumorCampaignBehavior : ICampaignBehavior
	{
		// Token: 0x17000CEA RID: 3306
		// (get) Token: 0x06003826 RID: 14374
		IEnumerable<TradeRumor> TradeRumors { get; }
	}
}
