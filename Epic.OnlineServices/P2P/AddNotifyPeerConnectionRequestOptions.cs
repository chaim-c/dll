using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002AB RID: 683
	public struct AddNotifyPeerConnectionRequestOptions
	{
		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06001293 RID: 4755 RVA: 0x0001B80E File Offset: 0x00019A0E
		// (set) Token: 0x06001294 RID: 4756 RVA: 0x0001B816 File Offset: 0x00019A16
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06001295 RID: 4757 RVA: 0x0001B81F File Offset: 0x00019A1F
		// (set) Token: 0x06001296 RID: 4758 RVA: 0x0001B827 File Offset: 0x00019A27
		public SocketId? SocketId { get; set; }
	}
}
