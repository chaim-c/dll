using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002D0 RID: 720
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnPeerConnectionInterruptedInfoInternal : ICallbackInfoInternal, IGettable<OnPeerConnectionInterruptedInfo>, ISettable<OnPeerConnectionInterruptedInfo>, IDisposable
	{
		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06001351 RID: 4945 RVA: 0x0001C6D4 File Offset: 0x0001A8D4
		// (set) Token: 0x06001352 RID: 4946 RVA: 0x0001C6F5 File Offset: 0x0001A8F5
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

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06001353 RID: 4947 RVA: 0x0001C708 File Offset: 0x0001A908
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06001354 RID: 4948 RVA: 0x0001C720 File Offset: 0x0001A920
		// (set) Token: 0x06001355 RID: 4949 RVA: 0x0001C741 File Offset: 0x0001A941
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

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001356 RID: 4950 RVA: 0x0001C754 File Offset: 0x0001A954
		// (set) Token: 0x06001357 RID: 4951 RVA: 0x0001C775 File Offset: 0x0001A975
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

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06001358 RID: 4952 RVA: 0x0001C788 File Offset: 0x0001A988
		// (set) Token: 0x06001359 RID: 4953 RVA: 0x0001C7A9 File Offset: 0x0001A9A9
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

		// Token: 0x0600135A RID: 4954 RVA: 0x0001C7BA File Offset: 0x0001A9BA
		public void Set(ref OnPeerConnectionInterruptedInfo other)
		{
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.RemoteUserId = other.RemoteUserId;
			this.SocketId = other.SocketId;
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x0001C7F4 File Offset: 0x0001A9F4
		public void Set(ref OnPeerConnectionInterruptedInfo? other)
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

		// Token: 0x0600135C RID: 4956 RVA: 0x0001C862 File Offset: 0x0001AA62
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_RemoteUserId);
			Helper.Dispose(ref this.m_SocketId);
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x0001C895 File Offset: 0x0001AA95
		public void Get(out OnPeerConnectionInterruptedInfo output)
		{
			output = default(OnPeerConnectionInterruptedInfo);
			output.Set(ref this);
		}

		// Token: 0x0400089F RID: 2207
		private IntPtr m_ClientData;

		// Token: 0x040008A0 RID: 2208
		private IntPtr m_LocalUserId;

		// Token: 0x040008A1 RID: 2209
		private IntPtr m_RemoteUserId;

		// Token: 0x040008A2 RID: 2210
		private IntPtr m_SocketId;
	}
}
