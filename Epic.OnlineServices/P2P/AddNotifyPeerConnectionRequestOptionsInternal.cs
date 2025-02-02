using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002AC RID: 684
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyPeerConnectionRequestOptionsInternal : ISettable<AddNotifyPeerConnectionRequestOptions>, IDisposable
	{
		// Token: 0x17000506 RID: 1286
		// (set) Token: 0x06001297 RID: 4759 RVA: 0x0001B830 File Offset: 0x00019A30
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000507 RID: 1287
		// (set) Token: 0x06001298 RID: 4760 RVA: 0x0001B840 File Offset: 0x00019A40
		public SocketId? SocketId
		{
			set
			{
				Helper.Set<SocketId, SocketIdInternal>(ref value, ref this.m_SocketId);
			}
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x0001B851 File Offset: 0x00019A51
		public void Set(ref AddNotifyPeerConnectionRequestOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.SocketId = other.SocketId;
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x0001B878 File Offset: 0x00019A78
		public void Set(ref AddNotifyPeerConnectionRequestOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.SocketId = other.Value.SocketId;
			}
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x0001B8C3 File Offset: 0x00019AC3
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_SocketId);
		}

		// Token: 0x04000844 RID: 2116
		private int m_ApiVersion;

		// Token: 0x04000845 RID: 2117
		private IntPtr m_LocalUserId;

		// Token: 0x04000846 RID: 2118
		private IntPtr m_SocketId;
	}
}
