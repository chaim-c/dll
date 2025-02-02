using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000532 RID: 1330
	public struct GetProductUserIdMappingOptions
	{
		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x0600221C RID: 8732 RVA: 0x00033021 File Offset: 0x00031221
		// (set) Token: 0x0600221D RID: 8733 RVA: 0x00033029 File Offset: 0x00031229
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x0600221E RID: 8734 RVA: 0x00033032 File Offset: 0x00031232
		// (set) Token: 0x0600221F RID: 8735 RVA: 0x0003303A File Offset: 0x0003123A
		public ExternalAccountType AccountIdType { get; set; }

		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x06002220 RID: 8736 RVA: 0x00033043 File Offset: 0x00031243
		// (set) Token: 0x06002221 RID: 8737 RVA: 0x0003304B File Offset: 0x0003124B
		public ProductUserId TargetProductUserId { get; set; }
	}
}
