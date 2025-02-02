using System;
using System.Runtime.CompilerServices;

namespace MCM.Abstractions.Base.PerCampaign
{
	// Token: 0x020000B6 RID: 182
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1
	})]
	public sealed class EmptyPerCampaignSettings : PerCampaignSettings<EmptyPerCampaignSettings>
	{
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060003CA RID: 970 RVA: 0x0000B92C File Offset: 0x00009B2C
		public override string Id
		{
			get
			{
				return "empty_percampaign_v1";
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060003CB RID: 971 RVA: 0x0000B933 File Offset: 0x00009B33
		public override string DisplayName
		{
			get
			{
				return "Empty PerCampaign Settings";
			}
		}
	}
}
