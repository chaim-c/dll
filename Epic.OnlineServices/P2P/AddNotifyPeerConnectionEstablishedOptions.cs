using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002A7 RID: 679
	public struct AddNotifyPeerConnectionEstablishedOptions
	{
		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06001281 RID: 4737 RVA: 0x0001B66E File Offset: 0x0001986E
		// (set) Token: 0x06001282 RID: 4738 RVA: 0x0001B676 File Offset: 0x00019876
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06001283 RID: 4739 RVA: 0x0001B67F File Offset: 0x0001987F
		// (set) Token: 0x06001284 RID: 4740 RVA: 0x0001B687 File Offset: 0x00019887
		public SocketId? SocketId { get; set; }
	}
}
