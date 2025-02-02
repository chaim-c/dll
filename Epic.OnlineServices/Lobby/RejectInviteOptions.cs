using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003E7 RID: 999
	public struct RejectInviteOptions
	{
		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x060019D8 RID: 6616 RVA: 0x00026382 File Offset: 0x00024582
		// (set) Token: 0x060019D9 RID: 6617 RVA: 0x0002638A File Offset: 0x0002458A
		public Utf8String InviteId { get; set; }

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x060019DA RID: 6618 RVA: 0x00026393 File Offset: 0x00024593
		// (set) Token: 0x060019DB RID: 6619 RVA: 0x0002639B File Offset: 0x0002459B
		public ProductUserId LocalUserId { get; set; }
	}
}
