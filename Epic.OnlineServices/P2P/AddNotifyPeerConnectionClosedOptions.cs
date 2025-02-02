using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002A5 RID: 677
	public struct AddNotifyPeerConnectionClosedOptions
	{
		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06001278 RID: 4728 RVA: 0x0001B5A0 File Offset: 0x000197A0
		// (set) Token: 0x06001279 RID: 4729 RVA: 0x0001B5A8 File Offset: 0x000197A8
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x0600127A RID: 4730 RVA: 0x0001B5B1 File Offset: 0x000197B1
		// (set) Token: 0x0600127B RID: 4731 RVA: 0x0001B5B9 File Offset: 0x000197B9
		public SocketId? SocketId { get; set; }
	}
}
