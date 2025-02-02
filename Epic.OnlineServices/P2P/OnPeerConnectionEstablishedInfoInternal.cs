using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002CC RID: 716
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnPeerConnectionEstablishedInfoInternal : ICallbackInfoInternal, IGettable<OnPeerConnectionEstablishedInfo>, ISettable<OnPeerConnectionEstablishedInfo>, IDisposable
	{
		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x0600132E RID: 4910 RVA: 0x0001C3D0 File Offset: 0x0001A5D0
		// (set) Token: 0x0600132F RID: 4911 RVA: 0x0001C3F1 File Offset: 0x0001A5F1
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

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06001330 RID: 4912 RVA: 0x0001C404 File Offset: 0x0001A604
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06001331 RID: 4913 RVA: 0x0001C41C File Offset: 0x0001A61C
		// (set) Token: 0x06001332 RID: 4914 RVA: 0x0001C43D File Offset: 0x0001A63D
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

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06001333 RID: 4915 RVA: 0x0001C450 File Offset: 0x0001A650
		// (set) Token: 0x06001334 RID: 4916 RVA: 0x0001C471 File Offset: 0x0001A671
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

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06001335 RID: 4917 RVA: 0x0001C484 File Offset: 0x0001A684
		// (set) Token: 0x06001336 RID: 4918 RVA: 0x0001C4A5 File Offset: 0x0001A6A5
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

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06001337 RID: 4919 RVA: 0x0001C4B8 File Offset: 0x0001A6B8
		// (set) Token: 0x06001338 RID: 4920 RVA: 0x0001C4D0 File Offset: 0x0001A6D0
		public ConnectionEstablishedType ConnectionType
		{
			get
			{
				return this.m_ConnectionType;
			}
			set
			{
				this.m_ConnectionType = value;
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06001339 RID: 4921 RVA: 0x0001C4DC File Offset: 0x0001A6DC
		// (set) Token: 0x0600133A RID: 4922 RVA: 0x0001C4F4 File Offset: 0x0001A6F4
		public NetworkConnectionType NetworkType
		{
			get
			{
				return this.m_NetworkType;
			}
			set
			{
				this.m_NetworkType = value;
			}
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x0001C500 File Offset: 0x0001A700
		public void Set(ref OnPeerConnectionEstablishedInfo other)
		{
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.RemoteUserId = other.RemoteUserId;
			this.SocketId = other.SocketId;
			this.ConnectionType = other.ConnectionType;
			this.NetworkType = other.NetworkType;
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x0001C55C File Offset: 0x0001A75C
		public void Set(ref OnPeerConnectionEstablishedInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.RemoteUserId = other.Value.RemoteUserId;
				this.SocketId = other.Value.SocketId;
				this.ConnectionType = other.Value.ConnectionType;
				this.NetworkType = other.Value.NetworkType;
			}
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x0001C5F7 File Offset: 0x0001A7F7
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_RemoteUserId);
			Helper.Dispose(ref this.m_SocketId);
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x0001C62A File Offset: 0x0001A82A
		public void Get(out OnPeerConnectionEstablishedInfo output)
		{
			output = default(OnPeerConnectionEstablishedInfo);
			output.Set(ref this);
		}

		// Token: 0x04000895 RID: 2197
		private IntPtr m_ClientData;

		// Token: 0x04000896 RID: 2198
		private IntPtr m_LocalUserId;

		// Token: 0x04000897 RID: 2199
		private IntPtr m_RemoteUserId;

		// Token: 0x04000898 RID: 2200
		private IntPtr m_SocketId;

		// Token: 0x04000899 RID: 2201
		private ConnectionEstablishedType m_ConnectionType;

		// Token: 0x0400089A RID: 2202
		private NetworkConnectionType m_NetworkType;
	}
}
