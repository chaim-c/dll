using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x020005A5 RID: 1445
	public struct QueryIdTokenOptions
	{
		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x060024FF RID: 9471 RVA: 0x00036C9B File Offset: 0x00034E9B
		// (set) Token: 0x06002500 RID: 9472 RVA: 0x00036CA3 File Offset: 0x00034EA3
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x06002501 RID: 9473 RVA: 0x00036CAC File Offset: 0x00034EAC
		// (set) Token: 0x06002502 RID: 9474 RVA: 0x00036CB4 File Offset: 0x00034EB4
		public EpicAccountId TargetAccountId { get; set; }
	}
}
