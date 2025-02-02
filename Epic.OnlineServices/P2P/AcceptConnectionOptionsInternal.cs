using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002A2 RID: 674
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AcceptConnectionOptionsInternal : ISettable<AcceptConnectionOptions>, IDisposable
	{
		// Token: 0x170004F5 RID: 1269
		// (set) Token: 0x0600126F RID: 4719 RVA: 0x0001B483 File Offset: 0x00019683
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (set) Token: 0x06001270 RID: 4720 RVA: 0x0001B493 File Offset: 0x00019693
		public ProductUserId RemoteUserId
		{
			set
			{
				Helper.Set(value, ref this.m_RemoteUserId);
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (set) Token: 0x06001271 RID: 4721 RVA: 0x0001B4A3 File Offset: 0x000196A3
		public SocketId? SocketId
		{
			set
			{
				Helper.Set<SocketId, SocketIdInternal>(ref value, ref this.m_SocketId);
			}
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x0001B4B4 File Offset: 0x000196B4
		public void Set(ref AcceptConnectionOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.RemoteUserId = other.RemoteUserId;
			this.SocketId = other.SocketId;
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x0001B4E8 File Offset: 0x000196E8
		public void Set(ref AcceptConnectionOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.RemoteUserId = other.Value.RemoteUserId;
				this.SocketId = other.Value.SocketId;
			}
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x0001B548 File Offset: 0x00019748
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_RemoteUserId);
			Helper.Dispose(ref this.m_SocketId);
		}

		// Token: 0x0400082E RID: 2094
		private int m_ApiVersion;

		// Token: 0x0400082F RID: 2095
		private IntPtr m_LocalUserId;

		// Token: 0x04000830 RID: 2096
		private IntPtr m_RemoteUserId;

		// Token: 0x04000831 RID: 2097
		private IntPtr m_SocketId;
	}
}
