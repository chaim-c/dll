using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002B2 RID: 690
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CloseConnectionsOptionsInternal : ISettable<CloseConnectionsOptions>, IDisposable
	{
		// Token: 0x17000516 RID: 1302
		// (set) Token: 0x060012B8 RID: 4792 RVA: 0x0001BB39 File Offset: 0x00019D39
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000517 RID: 1303
		// (set) Token: 0x060012B9 RID: 4793 RVA: 0x0001BB49 File Offset: 0x00019D49
		public SocketId? SocketId
		{
			set
			{
				Helper.Set<SocketId, SocketIdInternal>(ref value, ref this.m_SocketId);
			}
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x0001BB5A File Offset: 0x00019D5A
		public void Set(ref CloseConnectionsOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.SocketId = other.SocketId;
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x0001BB80 File Offset: 0x00019D80
		public void Set(ref CloseConnectionsOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.SocketId = other.Value.SocketId;
			}
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x0001BBCB File Offset: 0x00019DCB
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_SocketId);
		}

		// Token: 0x04000857 RID: 2135
		private int m_ApiVersion;

		// Token: 0x04000858 RID: 2136
		private IntPtr m_LocalUserId;

		// Token: 0x04000859 RID: 2137
		private IntPtr m_SocketId;
	}
}
