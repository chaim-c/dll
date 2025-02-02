using System;

namespace Epic.OnlineServices.AntiCheatServer
{
	// Token: 0x020005CC RID: 1484
	public struct ReceiveMessageFromClientOptions
	{
		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x0600261C RID: 9756 RVA: 0x00038AAF File Offset: 0x00036CAF
		// (set) Token: 0x0600261D RID: 9757 RVA: 0x00038AB7 File Offset: 0x00036CB7
		public IntPtr ClientHandle { get; set; }

		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x0600261E RID: 9758 RVA: 0x00038AC0 File Offset: 0x00036CC0
		// (set) Token: 0x0600261F RID: 9759 RVA: 0x00038AC8 File Offset: 0x00036CC8
		public ArraySegment<byte> Data { get; set; }
	}
}
