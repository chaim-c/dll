using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002E3 RID: 739
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SendPacketOptionsInternal : ISettable<SendPacketOptions>, IDisposable
	{
		// Token: 0x17000574 RID: 1396
		// (set) Token: 0x060013F9 RID: 5113 RVA: 0x0001D963 File Offset: 0x0001BB63
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000575 RID: 1397
		// (set) Token: 0x060013FA RID: 5114 RVA: 0x0001D973 File Offset: 0x0001BB73
		public ProductUserId RemoteUserId
		{
			set
			{
				Helper.Set(value, ref this.m_RemoteUserId);
			}
		}

		// Token: 0x17000576 RID: 1398
		// (set) Token: 0x060013FB RID: 5115 RVA: 0x0001D983 File Offset: 0x0001BB83
		public SocketId? SocketId
		{
			set
			{
				Helper.Set<SocketId, SocketIdInternal>(ref value, ref this.m_SocketId);
			}
		}

		// Token: 0x17000577 RID: 1399
		// (set) Token: 0x060013FC RID: 5116 RVA: 0x0001D994 File Offset: 0x0001BB94
		public byte Channel
		{
			set
			{
				this.m_Channel = value;
			}
		}

		// Token: 0x17000578 RID: 1400
		// (set) Token: 0x060013FD RID: 5117 RVA: 0x0001D99E File Offset: 0x0001BB9E
		public ArraySegment<byte> Data
		{
			set
			{
				Helper.Set(value, ref this.m_Data, out this.m_DataLengthBytes);
			}
		}

		// Token: 0x17000579 RID: 1401
		// (set) Token: 0x060013FE RID: 5118 RVA: 0x0001D9B4 File Offset: 0x0001BBB4
		public bool AllowDelayedDelivery
		{
			set
			{
				Helper.Set(value, ref this.m_AllowDelayedDelivery);
			}
		}

		// Token: 0x1700057A RID: 1402
		// (set) Token: 0x060013FF RID: 5119 RVA: 0x0001D9C4 File Offset: 0x0001BBC4
		public PacketReliability Reliability
		{
			set
			{
				this.m_Reliability = value;
			}
		}

		// Token: 0x1700057B RID: 1403
		// (set) Token: 0x06001400 RID: 5120 RVA: 0x0001D9CE File Offset: 0x0001BBCE
		public bool DisableAutoAcceptConnection
		{
			set
			{
				Helper.Set(value, ref this.m_DisableAutoAcceptConnection);
			}
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x0001D9E0 File Offset: 0x0001BBE0
		public void Set(ref SendPacketOptions other)
		{
			this.m_ApiVersion = 3;
			this.LocalUserId = other.LocalUserId;
			this.RemoteUserId = other.RemoteUserId;
			this.SocketId = other.SocketId;
			this.Channel = other.Channel;
			this.Data = other.Data;
			this.AllowDelayedDelivery = other.AllowDelayedDelivery;
			this.Reliability = other.Reliability;
			this.DisableAutoAcceptConnection = other.DisableAutoAcceptConnection;
		}

		// Token: 0x06001402 RID: 5122 RVA: 0x0001DA60 File Offset: 0x0001BC60
		public void Set(ref SendPacketOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 3;
				this.LocalUserId = other.Value.LocalUserId;
				this.RemoteUserId = other.Value.RemoteUserId;
				this.SocketId = other.Value.SocketId;
				this.Channel = other.Value.Channel;
				this.Data = other.Value.Data;
				this.AllowDelayedDelivery = other.Value.AllowDelayedDelivery;
				this.Reliability = other.Value.Reliability;
				this.DisableAutoAcceptConnection = other.Value.DisableAutoAcceptConnection;
			}
		}

		// Token: 0x06001403 RID: 5123 RVA: 0x0001DB2C File Offset: 0x0001BD2C
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_RemoteUserId);
			Helper.Dispose(ref this.m_SocketId);
			Helper.Dispose(ref this.m_Data);
		}

		// Token: 0x040008F0 RID: 2288
		private int m_ApiVersion;

		// Token: 0x040008F1 RID: 2289
		private IntPtr m_LocalUserId;

		// Token: 0x040008F2 RID: 2290
		private IntPtr m_RemoteUserId;

		// Token: 0x040008F3 RID: 2291
		private IntPtr m_SocketId;

		// Token: 0x040008F4 RID: 2292
		private byte m_Channel;

		// Token: 0x040008F5 RID: 2293
		private uint m_DataLengthBytes;

		// Token: 0x040008F6 RID: 2294
		private IntPtr m_Data;

		// Token: 0x040008F7 RID: 2295
		private int m_AllowDelayedDelivery;

		// Token: 0x040008F8 RID: 2296
		private PacketReliability m_Reliability;

		// Token: 0x040008F9 RID: 2297
		private int m_DisableAutoAcceptConnection;
	}
}
