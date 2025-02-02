using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002E5 RID: 741
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SetPacketQueueSizeOptionsInternal : ISettable<SetPacketQueueSizeOptions>, IDisposable
	{
		// Token: 0x1700057E RID: 1406
		// (set) Token: 0x06001408 RID: 5128 RVA: 0x0001DB81 File Offset: 0x0001BD81
		public ulong IncomingPacketQueueMaxSizeBytes
		{
			set
			{
				this.m_IncomingPacketQueueMaxSizeBytes = value;
			}
		}

		// Token: 0x1700057F RID: 1407
		// (set) Token: 0x06001409 RID: 5129 RVA: 0x0001DB8B File Offset: 0x0001BD8B
		public ulong OutgoingPacketQueueMaxSizeBytes
		{
			set
			{
				this.m_OutgoingPacketQueueMaxSizeBytes = value;
			}
		}

		// Token: 0x0600140A RID: 5130 RVA: 0x0001DB95 File Offset: 0x0001BD95
		public void Set(ref SetPacketQueueSizeOptions other)
		{
			this.m_ApiVersion = 1;
			this.IncomingPacketQueueMaxSizeBytes = other.IncomingPacketQueueMaxSizeBytes;
			this.OutgoingPacketQueueMaxSizeBytes = other.OutgoingPacketQueueMaxSizeBytes;
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x0001DBBC File Offset: 0x0001BDBC
		public void Set(ref SetPacketQueueSizeOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.IncomingPacketQueueMaxSizeBytes = other.Value.IncomingPacketQueueMaxSizeBytes;
				this.OutgoingPacketQueueMaxSizeBytes = other.Value.OutgoingPacketQueueMaxSizeBytes;
			}
		}

		// Token: 0x0600140C RID: 5132 RVA: 0x0001DC07 File Offset: 0x0001BE07
		public void Dispose()
		{
		}

		// Token: 0x040008FC RID: 2300
		private int m_ApiVersion;

		// Token: 0x040008FD RID: 2301
		private ulong m_IncomingPacketQueueMaxSizeBytes;

		// Token: 0x040008FE RID: 2302
		private ulong m_OutgoingPacketQueueMaxSizeBytes;
	}
}
