using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002E2 RID: 738
	public struct SendPacketOptions
	{
		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x060013E9 RID: 5097 RVA: 0x0001D8DB File Offset: 0x0001BADB
		// (set) Token: 0x060013EA RID: 5098 RVA: 0x0001D8E3 File Offset: 0x0001BAE3
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x060013EB RID: 5099 RVA: 0x0001D8EC File Offset: 0x0001BAEC
		// (set) Token: 0x060013EC RID: 5100 RVA: 0x0001D8F4 File Offset: 0x0001BAF4
		public ProductUserId RemoteUserId { get; set; }

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x060013ED RID: 5101 RVA: 0x0001D8FD File Offset: 0x0001BAFD
		// (set) Token: 0x060013EE RID: 5102 RVA: 0x0001D905 File Offset: 0x0001BB05
		public SocketId? SocketId { get; set; }

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x060013EF RID: 5103 RVA: 0x0001D90E File Offset: 0x0001BB0E
		// (set) Token: 0x060013F0 RID: 5104 RVA: 0x0001D916 File Offset: 0x0001BB16
		public byte Channel { get; set; }

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x060013F1 RID: 5105 RVA: 0x0001D91F File Offset: 0x0001BB1F
		// (set) Token: 0x060013F2 RID: 5106 RVA: 0x0001D927 File Offset: 0x0001BB27
		public ArraySegment<byte> Data { get; set; }

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x060013F3 RID: 5107 RVA: 0x0001D930 File Offset: 0x0001BB30
		// (set) Token: 0x060013F4 RID: 5108 RVA: 0x0001D938 File Offset: 0x0001BB38
		public bool AllowDelayedDelivery { get; set; }

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x060013F5 RID: 5109 RVA: 0x0001D941 File Offset: 0x0001BB41
		// (set) Token: 0x060013F6 RID: 5110 RVA: 0x0001D949 File Offset: 0x0001BB49
		public PacketReliability Reliability { get; set; }

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x060013F7 RID: 5111 RVA: 0x0001D952 File Offset: 0x0001BB52
		// (set) Token: 0x060013F8 RID: 5112 RVA: 0x0001D95A File Offset: 0x0001BB5A
		public bool DisableAutoAcceptConnection { get; set; }
	}
}
