using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000132 RID: 306
	public struct SessionModificationAddAttributeOptions
	{
		// Token: 0x17000201 RID: 513
		// (get) Token: 0x0600093A RID: 2362 RVA: 0x0000D74E File Offset: 0x0000B94E
		// (set) Token: 0x0600093B RID: 2363 RVA: 0x0000D756 File Offset: 0x0000B956
		public AttributeData? SessionAttribute { get; set; }

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x0600093C RID: 2364 RVA: 0x0000D75F File Offset: 0x0000B95F
		// (set) Token: 0x0600093D RID: 2365 RVA: 0x0000D767 File Offset: 0x0000B967
		public SessionAttributeAdvertisementType AdvertisementType { get; set; }
	}
}
