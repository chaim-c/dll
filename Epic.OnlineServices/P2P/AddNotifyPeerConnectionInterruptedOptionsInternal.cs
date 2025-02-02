using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002AA RID: 682
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyPeerConnectionInterruptedOptionsInternal : ISettable<AddNotifyPeerConnectionInterruptedOptions>, IDisposable
	{
		// Token: 0x17000502 RID: 1282
		// (set) Token: 0x0600128E RID: 4750 RVA: 0x0001B760 File Offset: 0x00019960
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000503 RID: 1283
		// (set) Token: 0x0600128F RID: 4751 RVA: 0x0001B770 File Offset: 0x00019970
		public SocketId? SocketId
		{
			set
			{
				Helper.Set<SocketId, SocketIdInternal>(ref value, ref this.m_SocketId);
			}
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x0001B781 File Offset: 0x00019981
		public void Set(ref AddNotifyPeerConnectionInterruptedOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.SocketId = other.SocketId;
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x0001B7A8 File Offset: 0x000199A8
		public void Set(ref AddNotifyPeerConnectionInterruptedOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.SocketId = other.Value.SocketId;
			}
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x0001B7F3 File Offset: 0x000199F3
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_SocketId);
		}

		// Token: 0x0400083F RID: 2111
		private int m_ApiVersion;

		// Token: 0x04000840 RID: 2112
		private IntPtr m_LocalUserId;

		// Token: 0x04000841 RID: 2113
		private IntPtr m_SocketId;
	}
}
