using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002E0 RID: 736
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ReceivePacketOptionsInternal : ISettable<ReceivePacketOptions>, IDisposable
	{
		// Token: 0x17000569 RID: 1385
		// (set) Token: 0x060013E3 RID: 5091 RVA: 0x0001D803 File Offset: 0x0001BA03
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x1700056A RID: 1386
		// (set) Token: 0x060013E4 RID: 5092 RVA: 0x0001D813 File Offset: 0x0001BA13
		public uint MaxDataSizeBytes
		{
			set
			{
				this.m_MaxDataSizeBytes = value;
			}
		}

		// Token: 0x1700056B RID: 1387
		// (set) Token: 0x060013E5 RID: 5093 RVA: 0x0001D81D File Offset: 0x0001BA1D
		public byte? RequestedChannel
		{
			set
			{
				Helper.Set<byte>(value, ref this.m_RequestedChannel);
			}
		}

		// Token: 0x060013E6 RID: 5094 RVA: 0x0001D82D File Offset: 0x0001BA2D
		public void Set(ref ReceivePacketOptions other)
		{
			this.m_ApiVersion = 2;
			this.LocalUserId = other.LocalUserId;
			this.MaxDataSizeBytes = other.MaxDataSizeBytes;
			this.RequestedChannel = other.RequestedChannel;
		}

		// Token: 0x060013E7 RID: 5095 RVA: 0x0001D860 File Offset: 0x0001BA60
		public void Set(ref ReceivePacketOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 2;
				this.LocalUserId = other.Value.LocalUserId;
				this.MaxDataSizeBytes = other.Value.MaxDataSizeBytes;
				this.RequestedChannel = other.Value.RequestedChannel;
			}
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x0001D8C0 File Offset: 0x0001BAC0
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_RequestedChannel);
		}

		// Token: 0x040008E0 RID: 2272
		private int m_ApiVersion;

		// Token: 0x040008E1 RID: 2273
		private IntPtr m_LocalUserId;

		// Token: 0x040008E2 RID: 2274
		private uint m_MaxDataSizeBytes;

		// Token: 0x040008E3 RID: 2275
		private IntPtr m_RequestedChannel;
	}
}
