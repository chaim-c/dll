using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002A9 RID: 681
	public struct AddNotifyPeerConnectionInterruptedOptions
	{
		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x0600128A RID: 4746 RVA: 0x0001B73E File Offset: 0x0001993E
		// (set) Token: 0x0600128B RID: 4747 RVA: 0x0001B746 File Offset: 0x00019946
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x0600128C RID: 4748 RVA: 0x0001B74F File Offset: 0x0001994F
		// (set) Token: 0x0600128D RID: 4749 RVA: 0x0001B757 File Offset: 0x00019957
		public SocketId? SocketId { get; set; }
	}
}
