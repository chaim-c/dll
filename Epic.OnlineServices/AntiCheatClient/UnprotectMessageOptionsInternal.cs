using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x0200063E RID: 1598
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UnprotectMessageOptionsInternal : ISettable<UnprotectMessageOptions>, IDisposable
	{
		// Token: 0x17000C28 RID: 3112
		// (set) Token: 0x060028A7 RID: 10407 RVA: 0x0003C804 File Offset: 0x0003AA04
		public ArraySegment<byte> Data
		{
			set
			{
				Helper.Set(value, ref this.m_Data, out this.m_DataLengthBytes);
			}
		}

		// Token: 0x17000C29 RID: 3113
		// (set) Token: 0x060028A8 RID: 10408 RVA: 0x0003C81A File Offset: 0x0003AA1A
		public uint OutBufferSizeBytes
		{
			set
			{
				this.m_OutBufferSizeBytes = value;
			}
		}

		// Token: 0x060028A9 RID: 10409 RVA: 0x0003C824 File Offset: 0x0003AA24
		public void Set(ref UnprotectMessageOptions other)
		{
			this.m_ApiVersion = 1;
			this.Data = other.Data;
			this.OutBufferSizeBytes = other.OutBufferSizeBytes;
		}

		// Token: 0x060028AA RID: 10410 RVA: 0x0003C848 File Offset: 0x0003AA48
		public void Set(ref UnprotectMessageOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.Data = other.Value.Data;
				this.OutBufferSizeBytes = other.Value.OutBufferSizeBytes;
			}
		}

		// Token: 0x060028AB RID: 10411 RVA: 0x0003C893 File Offset: 0x0003AA93
		public void Dispose()
		{
			Helper.Dispose(ref this.m_Data);
		}

		// Token: 0x04001254 RID: 4692
		private int m_ApiVersion;

		// Token: 0x04001255 RID: 4693
		private uint m_DataLengthBytes;

		// Token: 0x04001256 RID: 4694
		private IntPtr m_Data;

		// Token: 0x04001257 RID: 4695
		private uint m_OutBufferSizeBytes;
	}
}
