using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003DF RID: 991
	public struct PromoteMemberOptions
	{
		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x060019A0 RID: 6560 RVA: 0x00025E4A File Offset: 0x0002404A
		// (set) Token: 0x060019A1 RID: 6561 RVA: 0x00025E52 File Offset: 0x00024052
		public Utf8String LobbyId { get; set; }

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x060019A2 RID: 6562 RVA: 0x00025E5B File Offset: 0x0002405B
		// (set) Token: 0x060019A3 RID: 6563 RVA: 0x00025E63 File Offset: 0x00024063
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x060019A4 RID: 6564 RVA: 0x00025E6C File Offset: 0x0002406C
		// (set) Token: 0x060019A5 RID: 6565 RVA: 0x00025E74 File Offset: 0x00024074
		public ProductUserId TargetUserId { get; set; }
	}
}
