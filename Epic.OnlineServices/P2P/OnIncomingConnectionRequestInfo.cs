using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002C3 RID: 707
	public struct OnIncomingConnectionRequestInfo : ICallbackInfo
	{
		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x060012DA RID: 4826 RVA: 0x0001BD70 File Offset: 0x00019F70
		// (set) Token: 0x060012DB RID: 4827 RVA: 0x0001BD78 File Offset: 0x00019F78
		public object ClientData { get; set; }

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x060012DC RID: 4828 RVA: 0x0001BD81 File Offset: 0x00019F81
		// (set) Token: 0x060012DD RID: 4829 RVA: 0x0001BD89 File Offset: 0x00019F89
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x060012DE RID: 4830 RVA: 0x0001BD92 File Offset: 0x00019F92
		// (set) Token: 0x060012DF RID: 4831 RVA: 0x0001BD9A File Offset: 0x00019F9A
		public ProductUserId RemoteUserId { get; set; }

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x060012E0 RID: 4832 RVA: 0x0001BDA3 File Offset: 0x00019FA3
		// (set) Token: 0x060012E1 RID: 4833 RVA: 0x0001BDAB File Offset: 0x00019FAB
		public SocketId? SocketId { get; set; }

		// Token: 0x060012E2 RID: 4834 RVA: 0x0001BDB4 File Offset: 0x00019FB4
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x0001BDCF File Offset: 0x00019FCF
		internal void Set(ref OnIncomingConnectionRequestInfoInternal other)
		{
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.RemoteUserId = other.RemoteUserId;
			this.SocketId = other.SocketId;
		}
	}
}
