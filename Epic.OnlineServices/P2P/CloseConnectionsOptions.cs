using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002B1 RID: 689
	public struct CloseConnectionsOptions
	{
		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x060012B4 RID: 4788 RVA: 0x0001BB17 File Offset: 0x00019D17
		// (set) Token: 0x060012B5 RID: 4789 RVA: 0x0001BB1F File Offset: 0x00019D1F
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x060012B6 RID: 4790 RVA: 0x0001BB28 File Offset: 0x00019D28
		// (set) Token: 0x060012B7 RID: 4791 RVA: 0x0001BB30 File Offset: 0x00019D30
		public SocketId? SocketId { get; set; }
	}
}
