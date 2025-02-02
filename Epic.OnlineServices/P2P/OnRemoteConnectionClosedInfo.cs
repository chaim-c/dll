using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002D7 RID: 727
	public struct OnRemoteConnectionClosedInfo : ICallbackInfo
	{
		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06001381 RID: 4993 RVA: 0x0001CA5A File Offset: 0x0001AC5A
		// (set) Token: 0x06001382 RID: 4994 RVA: 0x0001CA62 File Offset: 0x0001AC62
		public object ClientData { get; set; }

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06001383 RID: 4995 RVA: 0x0001CA6B File Offset: 0x0001AC6B
		// (set) Token: 0x06001384 RID: 4996 RVA: 0x0001CA73 File Offset: 0x0001AC73
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06001385 RID: 4997 RVA: 0x0001CA7C File Offset: 0x0001AC7C
		// (set) Token: 0x06001386 RID: 4998 RVA: 0x0001CA84 File Offset: 0x0001AC84
		public ProductUserId RemoteUserId { get; set; }

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06001387 RID: 4999 RVA: 0x0001CA8D File Offset: 0x0001AC8D
		// (set) Token: 0x06001388 RID: 5000 RVA: 0x0001CA95 File Offset: 0x0001AC95
		public SocketId? SocketId { get; set; }

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06001389 RID: 5001 RVA: 0x0001CA9E File Offset: 0x0001AC9E
		// (set) Token: 0x0600138A RID: 5002 RVA: 0x0001CAA6 File Offset: 0x0001ACA6
		public ConnectionClosedReason Reason { get; set; }

		// Token: 0x0600138B RID: 5003 RVA: 0x0001CAB0 File Offset: 0x0001ACB0
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x0001CACC File Offset: 0x0001ACCC
		internal void Set(ref OnRemoteConnectionClosedInfoInternal other)
		{
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.RemoteUserId = other.RemoteUserId;
			this.SocketId = other.SocketId;
			this.Reason = other.Reason;
		}
	}
}
