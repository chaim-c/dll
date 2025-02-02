using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002A6 RID: 678
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyPeerConnectionClosedOptionsInternal : ISettable<AddNotifyPeerConnectionClosedOptions>, IDisposable
	{
		// Token: 0x170004FA RID: 1274
		// (set) Token: 0x0600127C RID: 4732 RVA: 0x0001B5C2 File Offset: 0x000197C2
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x170004FB RID: 1275
		// (set) Token: 0x0600127D RID: 4733 RVA: 0x0001B5D2 File Offset: 0x000197D2
		public SocketId? SocketId
		{
			set
			{
				Helper.Set<SocketId, SocketIdInternal>(ref value, ref this.m_SocketId);
			}
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x0001B5E3 File Offset: 0x000197E3
		public void Set(ref AddNotifyPeerConnectionClosedOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.SocketId = other.SocketId;
		}

		// Token: 0x0600127F RID: 4735 RVA: 0x0001B608 File Offset: 0x00019808
		public void Set(ref AddNotifyPeerConnectionClosedOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.SocketId = other.Value.SocketId;
			}
		}

		// Token: 0x06001280 RID: 4736 RVA: 0x0001B653 File Offset: 0x00019853
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_SocketId);
		}

		// Token: 0x04000835 RID: 2101
		private int m_ApiVersion;

		// Token: 0x04000836 RID: 2102
		private IntPtr m_LocalUserId;

		// Token: 0x04000837 RID: 2103
		private IntPtr m_SocketId;
	}
}
