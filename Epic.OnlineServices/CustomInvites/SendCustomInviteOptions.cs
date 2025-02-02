using System;

namespace Epic.OnlineServices.CustomInvites
{
	// Token: 0x02000509 RID: 1289
	public struct SendCustomInviteOptions
	{
		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x0600212F RID: 8495 RVA: 0x000314A3 File Offset: 0x0002F6A3
		// (set) Token: 0x06002130 RID: 8496 RVA: 0x000314AB File Offset: 0x0002F6AB
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x06002131 RID: 8497 RVA: 0x000314B4 File Offset: 0x0002F6B4
		// (set) Token: 0x06002132 RID: 8498 RVA: 0x000314BC File Offset: 0x0002F6BC
		public ProductUserId[] TargetUserIds { get; set; }
	}
}
