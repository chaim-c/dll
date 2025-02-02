using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002D8 RID: 728
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnRemoteConnectionClosedInfoInternal : ICallbackInfoInternal, IGettable<OnRemoteConnectionClosedInfo>, ISettable<OnRemoteConnectionClosedInfo>, IDisposable
	{
		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x0600138D RID: 5005 RVA: 0x0001CB1C File Offset: 0x0001AD1C
		// (set) Token: 0x0600138E RID: 5006 RVA: 0x0001CB3D File Offset: 0x0001AD3D
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

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x0600138F RID: 5007 RVA: 0x0001CB50 File Offset: 0x0001AD50
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06001390 RID: 5008 RVA: 0x0001CB68 File Offset: 0x0001AD68
		// (set) Token: 0x06001391 RID: 5009 RVA: 0x0001CB89 File Offset: 0x0001AD89
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

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001392 RID: 5010 RVA: 0x0001CB9C File Offset: 0x0001AD9C
		// (set) Token: 0x06001393 RID: 5011 RVA: 0x0001CBBD File Offset: 0x0001ADBD
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

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06001394 RID: 5012 RVA: 0x0001CBD0 File Offset: 0x0001ADD0
		// (set) Token: 0x06001395 RID: 5013 RVA: 0x0001CBF1 File Offset: 0x0001ADF1
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

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06001396 RID: 5014 RVA: 0x0001CC04 File Offset: 0x0001AE04
		// (set) Token: 0x06001397 RID: 5015 RVA: 0x0001CC1C File Offset: 0x0001AE1C
		public ConnectionClosedReason Reason
		{
			get
			{
				return this.m_Reason;
			}
			set
			{
				this.m_Reason = value;
			}
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x0001CC28 File Offset: 0x0001AE28
		public void Set(ref OnRemoteConnectionClosedInfo other)
		{
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.RemoteUserId = other.RemoteUserId;
			this.SocketId = other.SocketId;
			this.Reason = other.Reason;
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x0001CC78 File Offset: 0x0001AE78
		public void Set(ref OnRemoteConnectionClosedInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.RemoteUserId = other.Value.RemoteUserId;
				this.SocketId = other.Value.SocketId;
				this.Reason = other.Value.Reason;
			}
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x0001CCFB File Offset: 0x0001AEFB
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_RemoteUserId);
			Helper.Dispose(ref this.m_SocketId);
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x0001CD2E File Offset: 0x0001AF2E
		public void Get(out OnRemoteConnectionClosedInfo output)
		{
			output = default(OnRemoteConnectionClosedInfo);
			output.Set(ref this);
		}

		// Token: 0x040008AE RID: 2222
		private IntPtr m_ClientData;

		// Token: 0x040008AF RID: 2223
		private IntPtr m_LocalUserId;

		// Token: 0x040008B0 RID: 2224
		private IntPtr m_RemoteUserId;

		// Token: 0x040008B1 RID: 2225
		private IntPtr m_SocketId;

		// Token: 0x040008B2 RID: 2226
		private ConnectionClosedReason m_Reason;
	}
}
