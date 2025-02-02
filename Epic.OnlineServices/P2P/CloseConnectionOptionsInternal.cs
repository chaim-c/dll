using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002B0 RID: 688
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CloseConnectionOptionsInternal : ISettable<CloseConnectionOptions>, IDisposable
	{
		// Token: 0x17000511 RID: 1297
		// (set) Token: 0x060012AE RID: 4782 RVA: 0x0001BA2E File Offset: 0x00019C2E
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000512 RID: 1298
		// (set) Token: 0x060012AF RID: 4783 RVA: 0x0001BA3E File Offset: 0x00019C3E
		public ProductUserId RemoteUserId
		{
			set
			{
				Helper.Set(value, ref this.m_RemoteUserId);
			}
		}

		// Token: 0x17000513 RID: 1299
		// (set) Token: 0x060012B0 RID: 4784 RVA: 0x0001BA4E File Offset: 0x00019C4E
		public SocketId? SocketId
		{
			set
			{
				Helper.Set<SocketId, SocketIdInternal>(ref value, ref this.m_SocketId);
			}
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x0001BA5F File Offset: 0x00019C5F
		public void Set(ref CloseConnectionOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.RemoteUserId = other.RemoteUserId;
			this.SocketId = other.SocketId;
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x0001BA90 File Offset: 0x00019C90
		public void Set(ref CloseConnectionOptions? other)
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

		// Token: 0x060012B3 RID: 4787 RVA: 0x0001BAF0 File Offset: 0x00019CF0
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_RemoteUserId);
			Helper.Dispose(ref this.m_SocketId);
		}

		// Token: 0x04000851 RID: 2129
		private int m_ApiVersion;

		// Token: 0x04000852 RID: 2130
		private IntPtr m_LocalUserId;

		// Token: 0x04000853 RID: 2131
		private IntPtr m_RemoteUserId;

		// Token: 0x04000854 RID: 2132
		private IntPtr m_SocketId;
	}
}
