using System;
using System.Runtime.CompilerServices;

namespace MCM.Abstractions.Base.PerCampaign
{
	// Token: 0x020000B4 RID: 180
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1
	})]
	public abstract class AttributePerCampaignSettings<[Nullable(0)] T> : PerCampaignSettings<T> where T : PerCampaignSettings, new()
	{
		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060003BC RID: 956 RVA: 0x0000B754 File Offset: 0x00009954
		public sealed override string DiscoveryType
		{
			get
			{
				return "attributes";
			}
		}
	}
}
