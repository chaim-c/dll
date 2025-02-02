using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x02000624 RID: 1572
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetProtectMessageOutputLengthOptionsInternal : ISettable<GetProtectMessageOutputLengthOptions>, IDisposable
	{
		// Token: 0x17000BFF RID: 3071
		// (set) Token: 0x0600281F RID: 10271 RVA: 0x0003BFA1 File Offset: 0x0003A1A1
		public uint DataLengthBytes
		{
			set
			{
				this.m_DataLengthBytes = value;
			}
		}

		// Token: 0x06002820 RID: 10272 RVA: 0x0003BFAB File Offset: 0x0003A1AB
		public void Set(ref GetProtectMessageOutputLengthOptions other)
		{
			this.m_ApiVersion = 1;
			this.DataLengthBytes = other.DataLengthBytes;
		}

		// Token: 0x06002821 RID: 10273 RVA: 0x0003BFC4 File Offset: 0x0003A1C4
		public void Set(ref GetProtectMessageOutputLengthOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.DataLengthBytes = other.Value.DataLengthBytes;
			}
		}

		// Token: 0x06002822 RID: 10274 RVA: 0x0003BFFA File Offset: 0x0003A1FA
		public void Dispose()
		{
		}

		// Token: 0x04001223 RID: 4643
		private int m_ApiVersion;

		// Token: 0x04001224 RID: 4644
		private uint m_DataLengthBytes;
	}
}
