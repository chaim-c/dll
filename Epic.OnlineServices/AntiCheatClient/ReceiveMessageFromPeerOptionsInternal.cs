using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x02000638 RID: 1592
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ReceiveMessageFromPeerOptionsInternal : ISettable<ReceiveMessageFromPeerOptions>, IDisposable
	{
		// Token: 0x17000C14 RID: 3092
		// (set) Token: 0x06002880 RID: 10368 RVA: 0x0003C48C File Offset: 0x0003A68C
		public IntPtr PeerHandle
		{
			set
			{
				this.m_PeerHandle = value;
			}
		}

		// Token: 0x17000C15 RID: 3093
		// (set) Token: 0x06002881 RID: 10369 RVA: 0x0003C496 File Offset: 0x0003A696
		public ArraySegment<byte> Data
		{
			set
			{
				Helper.Set(value, ref this.m_Data, out this.m_DataLengthBytes);
			}
		}

		// Token: 0x06002882 RID: 10370 RVA: 0x0003C4AC File Offset: 0x0003A6AC
		public void Set(ref ReceiveMessageFromPeerOptions other)
		{
			this.m_ApiVersion = 1;
			this.PeerHandle = other.PeerHandle;
			this.Data = other.Data;
		}

		// Token: 0x06002883 RID: 10371 RVA: 0x0003C4D0 File Offset: 0x0003A6D0
		public void Set(ref ReceiveMessageFromPeerOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.PeerHandle = other.Value.PeerHandle;
				this.Data = other.Value.Data;
			}
		}

		// Token: 0x06002884 RID: 10372 RVA: 0x0003C51B File Offset: 0x0003A71B
		public void Dispose()
		{
			Helper.Dispose(ref this.m_PeerHandle);
			Helper.Dispose(ref this.m_Data);
		}

		// Token: 0x0400123B RID: 4667
		private int m_ApiVersion;

		// Token: 0x0400123C RID: 4668
		private IntPtr m_PeerHandle;

		// Token: 0x0400123D RID: 4669
		private uint m_DataLengthBytes;

		// Token: 0x0400123E RID: 4670
		private IntPtr m_Data;
	}
}
