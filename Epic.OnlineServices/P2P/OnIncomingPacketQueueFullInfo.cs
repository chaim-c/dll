using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002C7 RID: 711
	public struct OnIncomingPacketQueueFullInfo : ICallbackInfo
	{
		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x060012F9 RID: 4857 RVA: 0x0001BFDB File Offset: 0x0001A1DB
		// (set) Token: 0x060012FA RID: 4858 RVA: 0x0001BFE3 File Offset: 0x0001A1E3
		public object ClientData { get; set; }

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x060012FB RID: 4859 RVA: 0x0001BFEC File Offset: 0x0001A1EC
		// (set) Token: 0x060012FC RID: 4860 RVA: 0x0001BFF4 File Offset: 0x0001A1F4
		public ulong PacketQueueMaxSizeBytes { get; set; }

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x060012FD RID: 4861 RVA: 0x0001BFFD File Offset: 0x0001A1FD
		// (set) Token: 0x060012FE RID: 4862 RVA: 0x0001C005 File Offset: 0x0001A205
		public ulong PacketQueueCurrentSizeBytes { get; set; }

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x060012FF RID: 4863 RVA: 0x0001C00E File Offset: 0x0001A20E
		// (set) Token: 0x06001300 RID: 4864 RVA: 0x0001C016 File Offset: 0x0001A216
		public ProductUserId OverflowPacketLocalUserId { get; set; }

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06001301 RID: 4865 RVA: 0x0001C01F File Offset: 0x0001A21F
		// (set) Token: 0x06001302 RID: 4866 RVA: 0x0001C027 File Offset: 0x0001A227
		public byte OverflowPacketChannel { get; set; }

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06001303 RID: 4867 RVA: 0x0001C030 File Offset: 0x0001A230
		// (set) Token: 0x06001304 RID: 4868 RVA: 0x0001C038 File Offset: 0x0001A238
		public uint OverflowPacketSizeBytes { get; set; }

		// Token: 0x06001305 RID: 4869 RVA: 0x0001C044 File Offset: 0x0001A244
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x0001C060 File Offset: 0x0001A260
		internal void Set(ref OnIncomingPacketQueueFullInfoInternal other)
		{
			this.ClientData = other.ClientData;
			this.PacketQueueMaxSizeBytes = other.PacketQueueMaxSizeBytes;
			this.PacketQueueCurrentSizeBytes = other.PacketQueueCurrentSizeBytes;
			this.OverflowPacketLocalUserId = other.OverflowPacketLocalUserId;
			this.OverflowPacketChannel = other.OverflowPacketChannel;
			this.OverflowPacketSizeBytes = other.OverflowPacketSizeBytes;
		}
	}
}
