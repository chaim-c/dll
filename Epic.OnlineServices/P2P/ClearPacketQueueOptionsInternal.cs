using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002AE RID: 686
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ClearPacketQueueOptionsInternal : ISettable<ClearPacketQueueOptions>, IDisposable
	{
		// Token: 0x1700050B RID: 1291
		// (set) Token: 0x060012A2 RID: 4770 RVA: 0x0001B911 File Offset: 0x00019B11
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x1700050C RID: 1292
		// (set) Token: 0x060012A3 RID: 4771 RVA: 0x0001B921 File Offset: 0x00019B21
		public ProductUserId RemoteUserId
		{
			set
			{
				Helper.Set(value, ref this.m_RemoteUserId);
			}
		}

		// Token: 0x1700050D RID: 1293
		// (set) Token: 0x060012A4 RID: 4772 RVA: 0x0001B931 File Offset: 0x00019B31
		public SocketId? SocketId
		{
			set
			{
				Helper.Set<SocketId, SocketIdInternal>(ref value, ref this.m_SocketId);
			}
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x0001B942 File Offset: 0x00019B42
		public void Set(ref ClearPacketQueueOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.RemoteUserId = other.RemoteUserId;
			this.SocketId = other.SocketId;
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x0001B974 File Offset: 0x00019B74
		public void Set(ref ClearPacketQueueOptions? other)
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

		// Token: 0x060012A7 RID: 4775 RVA: 0x0001B9D4 File Offset: 0x00019BD4
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_RemoteUserId);
			Helper.Dispose(ref this.m_SocketId);
		}

		// Token: 0x0400084A RID: 2122
		private int m_ApiVersion;

		// Token: 0x0400084B RID: 2123
		private IntPtr m_LocalUserId;

		// Token: 0x0400084C RID: 2124
		private IntPtr m_RemoteUserId;

		// Token: 0x0400084D RID: 2125
		private IntPtr m_SocketId;
	}
}
