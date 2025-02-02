using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002E4 RID: 740
	public struct SetPacketQueueSizeOptions
	{
		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06001404 RID: 5124 RVA: 0x0001DB5F File Offset: 0x0001BD5F
		// (set) Token: 0x06001405 RID: 5125 RVA: 0x0001DB67 File Offset: 0x0001BD67
		public ulong IncomingPacketQueueMaxSizeBytes { get; set; }

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06001406 RID: 5126 RVA: 0x0001DB70 File Offset: 0x0001BD70
		// (set) Token: 0x06001407 RID: 5127 RVA: 0x0001DB78 File Offset: 0x0001BD78
		public ulong OutgoingPacketQueueMaxSizeBytes { get; set; }
	}
}
