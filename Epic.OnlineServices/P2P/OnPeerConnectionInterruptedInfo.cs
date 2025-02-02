using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002CF RID: 719
	public struct OnPeerConnectionInterruptedInfo : ICallbackInfo
	{
		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06001347 RID: 4935 RVA: 0x0001C63C File Offset: 0x0001A83C
		// (set) Token: 0x06001348 RID: 4936 RVA: 0x0001C644 File Offset: 0x0001A844
		public object ClientData { get; set; }

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06001349 RID: 4937 RVA: 0x0001C64D File Offset: 0x0001A84D
		// (set) Token: 0x0600134A RID: 4938 RVA: 0x0001C655 File Offset: 0x0001A855
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x0600134B RID: 4939 RVA: 0x0001C65E File Offset: 0x0001A85E
		// (set) Token: 0x0600134C RID: 4940 RVA: 0x0001C666 File Offset: 0x0001A866
		public ProductUserId RemoteUserId { get; set; }

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x0600134D RID: 4941 RVA: 0x0001C66F File Offset: 0x0001A86F
		// (set) Token: 0x0600134E RID: 4942 RVA: 0x0001C677 File Offset: 0x0001A877
		public SocketId? SocketId { get; set; }

		// Token: 0x0600134F RID: 4943 RVA: 0x0001C680 File Offset: 0x0001A880
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x0001C69B File Offset: 0x0001A89B
		internal void Set(ref OnPeerConnectionInterruptedInfoInternal other)
		{
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.RemoteUserId = other.RemoteUserId;
			this.SocketId = other.SocketId;
		}
	}
}
