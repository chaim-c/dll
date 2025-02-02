using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002AF RID: 687
	public struct CloseConnectionOptions
	{
		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x060012A8 RID: 4776 RVA: 0x0001B9FB File Offset: 0x00019BFB
		// (set) Token: 0x060012A9 RID: 4777 RVA: 0x0001BA03 File Offset: 0x00019C03
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x060012AA RID: 4778 RVA: 0x0001BA0C File Offset: 0x00019C0C
		// (set) Token: 0x060012AB RID: 4779 RVA: 0x0001BA14 File Offset: 0x00019C14
		public ProductUserId RemoteUserId { get; set; }

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x060012AC RID: 4780 RVA: 0x0001BA1D File Offset: 0x00019C1D
		// (set) Token: 0x060012AD RID: 4781 RVA: 0x0001BA25 File Offset: 0x00019C25
		public SocketId? SocketId { get; set; }
	}
}
