using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002A1 RID: 673
	public struct AcceptConnectionOptions
	{
		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06001269 RID: 4713 RVA: 0x0001B450 File Offset: 0x00019650
		// (set) Token: 0x0600126A RID: 4714 RVA: 0x0001B458 File Offset: 0x00019658
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x0600126B RID: 4715 RVA: 0x0001B461 File Offset: 0x00019661
		// (set) Token: 0x0600126C RID: 4716 RVA: 0x0001B469 File Offset: 0x00019669
		public ProductUserId RemoteUserId { get; set; }

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x0600126D RID: 4717 RVA: 0x0001B472 File Offset: 0x00019672
		// (set) Token: 0x0600126E RID: 4718 RVA: 0x0001B47A File Offset: 0x0001967A
		public SocketId? SocketId { get; set; }
	}
}
