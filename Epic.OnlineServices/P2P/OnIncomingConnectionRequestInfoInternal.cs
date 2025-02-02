using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002C4 RID: 708
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnIncomingConnectionRequestInfoInternal : ICallbackInfoInternal, IGettable<OnIncomingConnectionRequestInfo>, ISettable<OnIncomingConnectionRequestInfo>, IDisposable
	{
		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x060012E4 RID: 4836 RVA: 0x0001BE08 File Offset: 0x0001A008
		// (set) Token: 0x060012E5 RID: 4837 RVA: 0x0001BE29 File Offset: 0x0001A029
		public object ClientData
		{
			get
			{
				object result;
				Helper.Get(this.m_ClientData, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_ClientData);
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x060012E6 RID: 4838 RVA: 0x0001BE3C File Offset: 0x0001A03C
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x060012E7 RID: 4839 RVA: 0x0001BE54 File Offset: 0x0001A054
		// (set) Token: 0x060012E8 RID: 4840 RVA: 0x0001BE75 File Offset: 0x0001A075
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.Get<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x060012E9 RID: 4841 RVA: 0x0001BE88 File Offset: 0x0001A088
		// (set) Token: 0x060012EA RID: 4842 RVA: 0x0001BEA9 File Offset: 0x0001A0A9
		public ProductUserId RemoteUserId
		{
			get
			{
				ProductUserId result;
				Helper.Get<ProductUserId>(this.m_RemoteUserId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_RemoteUserId);
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x060012EB RID: 4843 RVA: 0x0001BEBC File Offset: 0x0001A0BC
		// (set) Token: 0x060012EC RID: 4844 RVA: 0x0001BEDD File Offset: 0x0001A0DD
		public SocketId? SocketId
		{
			get
			{
				SocketId? result;
				Helper.Get<SocketIdInternal, SocketId>(this.m_SocketId, out result);
				return result;
			}
			set
			{
				Helper.Set<SocketId, SocketIdInternal>(ref value, ref this.m_SocketId);
			}
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x0001BEEE File Offset: 0x0001A0EE
		public void Set(ref OnIncomingConnectionRequestInfo other)
		{
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.RemoteUserId = other.RemoteUserId;
			this.SocketId = other.SocketId;
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x0001BF28 File Offset: 0x0001A128
		public void Set(ref OnIncomingConnectionRequestInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.RemoteUserId = other.Value.RemoteUserId;
				this.SocketId = other.Value.SocketId;
			}
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x0001BF96 File Offset: 0x0001A196
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_RemoteUserId);
			Helper.Dispose(ref this.m_SocketId);
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x0001BFC9 File Offset: 0x0001A1C9
		public void Get(out OnIncomingConnectionRequestInfo output)
		{
			output = default(OnIncomingConnectionRequestInfo);
			output.Set(ref this);
		}

		// Token: 0x0400087F RID: 2175
		private IntPtr m_ClientData;

		// Token: 0x04000880 RID: 2176
		private IntPtr m_LocalUserId;

		// Token: 0x04000881 RID: 2177
		private IntPtr m_RemoteUserId;

		// Token: 0x04000882 RID: 2178
		private IntPtr m_SocketId;
	}
}
