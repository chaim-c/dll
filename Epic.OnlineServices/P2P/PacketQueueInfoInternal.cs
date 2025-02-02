using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002DB RID: 731
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PacketQueueInfoInternal : IGettable<PacketQueueInfo>, ISettable<PacketQueueInfo>, IDisposable
	{
		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x060013CA RID: 5066 RVA: 0x0001D5BC File Offset: 0x0001B7BC
		// (set) Token: 0x060013CB RID: 5067 RVA: 0x0001D5D4 File Offset: 0x0001B7D4
		public ulong IncomingPacketQueueMaxSizeBytes
		{
			get
			{
				return this.m_IncomingPacketQueueMaxSizeBytes;
			}
			set
			{
				this.m_IncomingPacketQueueMaxSizeBytes = value;
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x060013CC RID: 5068 RVA: 0x0001D5E0 File Offset: 0x0001B7E0
		// (set) Token: 0x060013CD RID: 5069 RVA: 0x0001D5F8 File Offset: 0x0001B7F8
		public ulong IncomingPacketQueueCurrentSizeBytes
		{
			get
			{
				return this.m_IncomingPacketQueueCurrentSizeBytes;
			}
			set
			{
				this.m_IncomingPacketQueueCurrentSizeBytes = value;
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x060013CE RID: 5070 RVA: 0x0001D604 File Offset: 0x0001B804
		// (set) Token: 0x060013CF RID: 5071 RVA: 0x0001D61C File Offset: 0x0001B81C
		public ulong IncomingPacketQueueCurrentPacketCount
		{
			get
			{
				return this.m_IncomingPacketQueueCurrentPacketCount;
			}
			set
			{
				this.m_IncomingPacketQueueCurrentPacketCount = value;
			}
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x060013D0 RID: 5072 RVA: 0x0001D628 File Offset: 0x0001B828
		// (set) Token: 0x060013D1 RID: 5073 RVA: 0x0001D640 File Offset: 0x0001B840
		public ulong OutgoingPacketQueueMaxSizeBytes
		{
			get
			{
				return this.m_OutgoingPacketQueueMaxSizeBytes;
			}
			set
			{
				this.m_OutgoingPacketQueueMaxSizeBytes = value;
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x060013D2 RID: 5074 RVA: 0x0001D64C File Offset: 0x0001B84C
		// (set) Token: 0x060013D3 RID: 5075 RVA: 0x0001D664 File Offset: 0x0001B864
		public ulong OutgoingPacketQueueCurrentSizeBytes
		{
			get
			{
				return this.m_OutgoingPacketQueueCurrentSizeBytes;
			}
			set
			{
				this.m_OutgoingPacketQueueCurrentSizeBytes = value;
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x060013D4 RID: 5076 RVA: 0x0001D670 File Offset: 0x0001B870
		// (set) Token: 0x060013D5 RID: 5077 RVA: 0x0001D688 File Offset: 0x0001B888
		public ulong OutgoingPacketQueueCurrentPacketCount
		{
			get
			{
				return this.m_OutgoingPacketQueueCurrentPacketCount;
			}
			set
			{
				this.m_OutgoingPacketQueueCurrentPacketCount = value;
			}
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x0001D694 File Offset: 0x0001B894
		public void Set(ref PacketQueueInfo other)
		{
			this.IncomingPacketQueueMaxSizeBytes = other.IncomingPacketQueueMaxSizeBytes;
			this.IncomingPacketQueueCurrentSizeBytes = other.IncomingPacketQueueCurrentSizeBytes;
			this.IncomingPacketQueueCurrentPacketCount = other.IncomingPacketQueueCurrentPacketCount;
			this.OutgoingPacketQueueMaxSizeBytes = other.OutgoingPacketQueueMaxSizeBytes;
			this.OutgoingPacketQueueCurrentSizeBytes = other.OutgoingPacketQueueCurrentSizeBytes;
			this.OutgoingPacketQueueCurrentPacketCount = other.OutgoingPacketQueueCurrentPacketCount;
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x0001D6F0 File Offset: 0x0001B8F0
		public void Set(ref PacketQueueInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.IncomingPacketQueueMaxSizeBytes = other.Value.IncomingPacketQueueMaxSizeBytes;
				this.IncomingPacketQueueCurrentSizeBytes = other.Value.IncomingPacketQueueCurrentSizeBytes;
				this.IncomingPacketQueueCurrentPacketCount = other.Value.IncomingPacketQueueCurrentPacketCount;
				this.OutgoingPacketQueueMaxSizeBytes = other.Value.OutgoingPacketQueueMaxSizeBytes;
				this.OutgoingPacketQueueCurrentSizeBytes = other.Value.OutgoingPacketQueueCurrentSizeBytes;
				this.OutgoingPacketQueueCurrentPacketCount = other.Value.OutgoingPacketQueueCurrentPacketCount;
			}
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x0001D78B File Offset: 0x0001B98B
		public void Dispose()
		{
		}

		// Token: 0x060013D9 RID: 5081 RVA: 0x0001D78E File Offset: 0x0001B98E
		public void Get(out PacketQueueInfo output)
		{
			output = default(PacketQueueInfo);
			output.Set(ref this);
		}

		// Token: 0x040008D2 RID: 2258
		private ulong m_IncomingPacketQueueMaxSizeBytes;

		// Token: 0x040008D3 RID: 2259
		private ulong m_IncomingPacketQueueCurrentSizeBytes;

		// Token: 0x040008D4 RID: 2260
		private ulong m_IncomingPacketQueueCurrentPacketCount;

		// Token: 0x040008D5 RID: 2261
		private ulong m_OutgoingPacketQueueMaxSizeBytes;

		// Token: 0x040008D6 RID: 2262
		private ulong m_OutgoingPacketQueueCurrentSizeBytes;

		// Token: 0x040008D7 RID: 2263
		private ulong m_OutgoingPacketQueueCurrentPacketCount;
	}
}
