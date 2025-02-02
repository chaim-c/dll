using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatServer
{
	// Token: 0x020005C3 RID: 1475
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetProtectMessageOutputLengthOptionsInternal : ISettable<GetProtectMessageOutputLengthOptions>, IDisposable
	{
		// Token: 0x17000B25 RID: 2853
		// (set) Token: 0x060025F4 RID: 9716 RVA: 0x00038949 File Offset: 0x00036B49
		public uint DataLengthBytes
		{
			set
			{
				this.m_DataLengthBytes = value;
			}
		}

		// Token: 0x060025F5 RID: 9717 RVA: 0x00038953 File Offset: 0x00036B53
		public void Set(ref GetProtectMessageOutputLengthOptions other)
		{
			this.m_ApiVersion = 1;
			this.DataLengthBytes = other.DataLengthBytes;
		}

		// Token: 0x060025F6 RID: 9718 RVA: 0x0003896C File Offset: 0x00036B6C
		public void Set(ref GetProtectMessageOutputLengthOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.DataLengthBytes = other.Value.DataLengthBytes;
			}
		}

		// Token: 0x060025F7 RID: 9719 RVA: 0x000389A2 File Offset: 0x00036BA2
		public void Dispose()
		{
		}

		// Token: 0x040010A2 RID: 4258
		private int m_ApiVersion;

		// Token: 0x040010A3 RID: 4259
		private uint m_DataLengthBytes;
	}
}
