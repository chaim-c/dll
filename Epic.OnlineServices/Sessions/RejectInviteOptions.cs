using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000117 RID: 279
	public struct RejectInviteOptions
	{
		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000881 RID: 2177 RVA: 0x0000C38D File Offset: 0x0000A58D
		// (set) Token: 0x06000882 RID: 2178 RVA: 0x0000C395 File Offset: 0x0000A595
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000883 RID: 2179 RVA: 0x0000C39E File Offset: 0x0000A59E
		// (set) Token: 0x06000884 RID: 2180 RVA: 0x0000C3A6 File Offset: 0x0000A5A6
		public Utf8String InviteId { get; set; }
	}
}
