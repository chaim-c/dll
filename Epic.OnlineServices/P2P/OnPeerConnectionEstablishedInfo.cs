using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002CB RID: 715
	public struct OnPeerConnectionEstablishedInfo : ICallbackInfo
	{
		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06001320 RID: 4896 RVA: 0x0001C2F0 File Offset: 0x0001A4F0
		// (set) Token: 0x06001321 RID: 4897 RVA: 0x0001C2F8 File Offset: 0x0001A4F8
		public object ClientData { get; set; }

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06001322 RID: 4898 RVA: 0x0001C301 File Offset: 0x0001A501
		// (set) Token: 0x06001323 RID: 4899 RVA: 0x0001C309 File Offset: 0x0001A509
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06001324 RID: 4900 RVA: 0x0001C312 File Offset: 0x0001A512
		// (set) Token: 0x06001325 RID: 4901 RVA: 0x0001C31A File Offset: 0x0001A51A
		public ProductUserId RemoteUserId { get; set; }

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06001326 RID: 4902 RVA: 0x0001C323 File Offset: 0x0001A523
		// (set) Token: 0x06001327 RID: 4903 RVA: 0x0001C32B File Offset: 0x0001A52B
		public SocketId? SocketId { get; set; }

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06001328 RID: 4904 RVA: 0x0001C334 File Offset: 0x0001A534
		// (set) Token: 0x06001329 RID: 4905 RVA: 0x0001C33C File Offset: 0x0001A53C
		public ConnectionEstablishedType ConnectionType { get; set; }

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x0600132A RID: 4906 RVA: 0x0001C345 File Offset: 0x0001A545
		// (set) Token: 0x0600132B RID: 4907 RVA: 0x0001C34D File Offset: 0x0001A54D
		public NetworkConnectionType NetworkType { get; set; }

		// Token: 0x0600132C RID: 4908 RVA: 0x0001C358 File Offset: 0x0001A558
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x0001C374 File Offset: 0x0001A574
		internal void Set(ref OnPeerConnectionEstablishedInfoInternal other)
		{
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.RemoteUserId = other.RemoteUserId;
			this.SocketId = other.SocketId;
			this.ConnectionType = other.ConnectionType;
			this.NetworkType = other.NetworkType;
		}
	}
}
