using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002A8 RID: 680
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyPeerConnectionEstablishedOptionsInternal : ISettable<AddNotifyPeerConnectionEstablishedOptions>, IDisposable
	{
		// Token: 0x170004FE RID: 1278
		// (set) Token: 0x06001285 RID: 4741 RVA: 0x0001B690 File Offset: 0x00019890
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x170004FF RID: 1279
		// (set) Token: 0x06001286 RID: 4742 RVA: 0x0001B6A0 File Offset: 0x000198A0
		public SocketId? SocketId
		{
			set
			{
				Helper.Set<SocketId, SocketIdInternal>(ref value, ref this.m_SocketId);
			}
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x0001B6B1 File Offset: 0x000198B1
		public void Set(ref AddNotifyPeerConnectionEstablishedOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.SocketId = other.SocketId;
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x0001B6D8 File Offset: 0x000198D8
		public void Set(ref AddNotifyPeerConnectionEstablishedOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.SocketId = other.Value.SocketId;
			}
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x0001B723 File Offset: 0x00019923
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_SocketId);
		}

		// Token: 0x0400083A RID: 2106
		private int m_ApiVersion;

		// Token: 0x0400083B RID: 2107
		private IntPtr m_LocalUserId;

		// Token: 0x0400083C RID: 2108
		private IntPtr m_SocketId;
	}
}
