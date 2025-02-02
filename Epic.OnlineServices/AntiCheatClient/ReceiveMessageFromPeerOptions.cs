using System;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x02000637 RID: 1591
	public struct ReceiveMessageFromPeerOptions
	{
		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x0600287C RID: 10364 RVA: 0x0003C46A File Offset: 0x0003A66A
		// (set) Token: 0x0600287D RID: 10365 RVA: 0x0003C472 File Offset: 0x0003A672
		public IntPtr PeerHandle { get; set; }

		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x0600287E RID: 10366 RVA: 0x0003C47B File Offset: 0x0003A67B
		// (set) Token: 0x0600287F RID: 10367 RVA: 0x0003C483 File Offset: 0x0003A683
		public ArraySegment<byte> Data { get; set; }
	}
}
