using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002DA RID: 730
	public struct PacketQueueInfo
	{
		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x060013BD RID: 5053 RVA: 0x0001D4F7 File Offset: 0x0001B6F7
		// (set) Token: 0x060013BE RID: 5054 RVA: 0x0001D4FF File Offset: 0x0001B6FF
		public ulong IncomingPacketQueueMaxSizeBytes { get; set; }

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x060013BF RID: 5055 RVA: 0x0001D508 File Offset: 0x0001B708
		// (set) Token: 0x060013C0 RID: 5056 RVA: 0x0001D510 File Offset: 0x0001B710
		public ulong IncomingPacketQueueCurrentSizeBytes { get; set; }

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x060013C1 RID: 5057 RVA: 0x0001D519 File Offset: 0x0001B719
		// (set) Token: 0x060013C2 RID: 5058 RVA: 0x0001D521 File Offset: 0x0001B721
		public ulong IncomingPacketQueueCurrentPacketCount { get; set; }

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x060013C3 RID: 5059 RVA: 0x0001D52A File Offset: 0x0001B72A
		// (set) Token: 0x060013C4 RID: 5060 RVA: 0x0001D532 File Offset: 0x0001B732
		public ulong OutgoingPacketQueueMaxSizeBytes { get; set; }

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x060013C5 RID: 5061 RVA: 0x0001D53B File Offset: 0x0001B73B
		// (set) Token: 0x060013C6 RID: 5062 RVA: 0x0001D543 File Offset: 0x0001B743
		public ulong OutgoingPacketQueueCurrentSizeBytes { get; set; }

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x060013C7 RID: 5063 RVA: 0x0001D54C File Offset: 0x0001B74C
		// (set) Token: 0x060013C8 RID: 5064 RVA: 0x0001D554 File Offset: 0x0001B754
		public ulong OutgoingPacketQueueCurrentPacketCount { get; set; }

		// Token: 0x060013C9 RID: 5065 RVA: 0x0001D560 File Offset: 0x0001B760
		internal void Set(ref PacketQueueInfoInternal other)
		{
			this.IncomingPacketQueueMaxSizeBytes = other.IncomingPacketQueueMaxSizeBytes;
			this.IncomingPacketQueueCurrentSizeBytes = other.IncomingPacketQueueCurrentSizeBytes;
			this.IncomingPacketQueueCurrentPacketCount = other.IncomingPacketQueueCurrentPacketCount;
			this.OutgoingPacketQueueMaxSizeBytes = other.OutgoingPacketQueueMaxSizeBytes;
			this.OutgoingPacketQueueCurrentSizeBytes = other.OutgoingPacketQueueCurrentSizeBytes;
			this.OutgoingPacketQueueCurrentPacketCount = other.OutgoingPacketQueueCurrentPacketCount;
		}
	}
}
