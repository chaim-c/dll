using System;

namespace Epic.OnlineServices.CustomInvites
{
	// Token: 0x020004F9 RID: 1273
	public struct FinalizeInviteOptions
	{
		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x060020B3 RID: 8371 RVA: 0x00030AEB File Offset: 0x0002ECEB
		// (set) Token: 0x060020B4 RID: 8372 RVA: 0x00030AF3 File Offset: 0x0002ECF3
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x060020B5 RID: 8373 RVA: 0x00030AFC File Offset: 0x0002ECFC
		// (set) Token: 0x060020B6 RID: 8374 RVA: 0x00030B04 File Offset: 0x0002ED04
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x060020B7 RID: 8375 RVA: 0x00030B0D File Offset: 0x0002ED0D
		// (set) Token: 0x060020B8 RID: 8376 RVA: 0x00030B15 File Offset: 0x0002ED15
		public Utf8String CustomInviteId { get; set; }

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x060020B9 RID: 8377 RVA: 0x00030B1E File Offset: 0x0002ED1E
		// (set) Token: 0x060020BA RID: 8378 RVA: 0x00030B26 File Offset: 0x0002ED26
		public Result ProcessingResult { get; set; }
	}
}
