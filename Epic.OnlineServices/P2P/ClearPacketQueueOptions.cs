using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002AD RID: 685
	public struct ClearPacketQueueOptions
	{
		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x0600129C RID: 4764 RVA: 0x0001B8DE File Offset: 0x00019ADE
		// (set) Token: 0x0600129D RID: 4765 RVA: 0x0001B8E6 File Offset: 0x00019AE6
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x0600129E RID: 4766 RVA: 0x0001B8EF File Offset: 0x00019AEF
		// (set) Token: 0x0600129F RID: 4767 RVA: 0x0001B8F7 File Offset: 0x00019AF7
		public ProductUserId RemoteUserId { get; set; }

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x060012A0 RID: 4768 RVA: 0x0001B900 File Offset: 0x00019B00
		// (set) Token: 0x060012A1 RID: 4769 RVA: 0x0001B908 File Offset: 0x00019B08
		public SocketId? SocketId { get; set; }
	}
}
