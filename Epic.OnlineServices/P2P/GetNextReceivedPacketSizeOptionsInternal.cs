using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002B8 RID: 696
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetNextReceivedPacketSizeOptionsInternal : ISettable<GetNextReceivedPacketSizeOptions>, IDisposable
	{
		// Token: 0x1700051A RID: 1306
		// (set) Token: 0x060012C4 RID: 4804 RVA: 0x0001BC36 File Offset: 0x00019E36
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x1700051B RID: 1307
		// (set) Token: 0x060012C5 RID: 4805 RVA: 0x0001BC46 File Offset: 0x00019E46
		public byte? RequestedChannel
		{
			set
			{
				Helper.Set<byte>(value, ref this.m_RequestedChannel);
			}
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x0001BC56 File Offset: 0x00019E56
		public void Set(ref GetNextReceivedPacketSizeOptions other)
		{
			this.m_ApiVersion = 2;
			this.LocalUserId = other.LocalUserId;
			this.RequestedChannel = other.RequestedChannel;
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x0001BC7C File Offset: 0x00019E7C
		public void Set(ref GetNextReceivedPacketSizeOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 2;
				this.LocalUserId = other.Value.LocalUserId;
				this.RequestedChannel = other.Value.RequestedChannel;
			}
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x0001BCC7 File Offset: 0x00019EC7
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_RequestedChannel);
		}

		// Token: 0x0400086C RID: 2156
		private int m_ApiVersion;

		// Token: 0x0400086D RID: 2157
		private IntPtr m_LocalUserId;

		// Token: 0x0400086E RID: 2158
		private IntPtr m_RequestedChannel;
	}
}
