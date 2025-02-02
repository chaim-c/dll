using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x02000636 RID: 1590
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ProtectMessageOptionsInternal : ISettable<ProtectMessageOptions>, IDisposable
	{
		// Token: 0x17000C10 RID: 3088
		// (set) Token: 0x06002877 RID: 10359 RVA: 0x0003C3CB File Offset: 0x0003A5CB
		public ArraySegment<byte> Data
		{
			set
			{
				Helper.Set(value, ref this.m_Data, out this.m_DataLengthBytes);
			}
		}

		// Token: 0x17000C11 RID: 3089
		// (set) Token: 0x06002878 RID: 10360 RVA: 0x0003C3E1 File Offset: 0x0003A5E1
		public uint OutBufferSizeBytes
		{
			set
			{
				this.m_OutBufferSizeBytes = value;
			}
		}

		// Token: 0x06002879 RID: 10361 RVA: 0x0003C3EB File Offset: 0x0003A5EB
		public void Set(ref ProtectMessageOptions other)
		{
			this.m_ApiVersion = 1;
			this.Data = other.Data;
			this.OutBufferSizeBytes = other.OutBufferSizeBytes;
		}

		// Token: 0x0600287A RID: 10362 RVA: 0x0003C410 File Offset: 0x0003A610
		public void Set(ref ProtectMessageOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.Data = other.Value.Data;
				this.OutBufferSizeBytes = other.Value.OutBufferSizeBytes;
			}
		}

		// Token: 0x0600287B RID: 10363 RVA: 0x0003C45B File Offset: 0x0003A65B
		public void Dispose()
		{
			Helper.Dispose(ref this.m_Data);
		}

		// Token: 0x04001235 RID: 4661
		private int m_ApiVersion;

		// Token: 0x04001236 RID: 4662
		private uint m_DataLengthBytes;

		// Token: 0x04001237 RID: 4663
		private IntPtr m_Data;

		// Token: 0x04001238 RID: 4664
		private uint m_OutBufferSizeBytes;
	}
}
