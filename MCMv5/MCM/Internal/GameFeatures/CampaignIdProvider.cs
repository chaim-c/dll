using System;
using System.Runtime.CompilerServices;
using MCM.Abstractions.GameFeatures;
using TaleWorlds.CampaignSystem;

namespace MCM.Internal.GameFeatures
{
	// Token: 0x0200000E RID: 14
	internal sealed class CampaignIdProvider : ICampaignIdProvider
	{
		// Token: 0x06000041 RID: 65 RVA: 0x0000301E File Offset: 0x0000121E
		[NullableContext(2)]
		public string GetCurrentCampaignId()
		{
			Campaign campaign = Campaign.Current;
			return (campaign != null) ? campaign.UniqueGameId : null;
		}
	}
}
