using System;
using System.Runtime.CompilerServices;

namespace MCM.Abstractions.GameFeatures
{
	// Token: 0x0200006E RID: 110
	[NullableContext(2)]
	public interface ICampaignIdProvider
	{
		// Token: 0x06000273 RID: 627
		string GetCurrentCampaignId();
	}
}
