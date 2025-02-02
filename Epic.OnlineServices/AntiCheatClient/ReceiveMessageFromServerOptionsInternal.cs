using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x0200063A RID: 1594
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ReceiveMessageFromServerOptionsInternal : ISettable<ReceiveMessageFromServerOptions>, IDisposable
	{
		// Token: 0x17000C17 RID: 3095
		// (set) Token: 0x06002887 RID: 10375 RVA: 0x0003C547 File Offset: 0x0003A747
		public ArraySegment<byte> Data
		{
			set
			{
				Helper.Set(value, ref this.m_Data, out this.m_DataLengthBytes);
			}
		}

		// Token: 0x06002888 RID: 10376 RVA: 0x0003C55D File Offset: 0x0003A75D
		public void Set(ref ReceiveMessageFromServerOptions other)
		{
			this.m_ApiVersion = 1;
			this.Data = other.Data;
		}

		// Token: 0x06002889 RID: 10377 RVA: 0x0003C574 File Offset: 0x0003A774
		public void Set(ref ReceiveMessageFromServerOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.Data = other.Value.Data;
			}
		}

		// Token: 0x0600288A RID: 10378 RVA: 0x0003C5AA File Offset: 0x0003A7AA
		public void Dispose()
		{
			Helper.Dispose(ref this.m_Data);
		}

		// Token: 0x04001240 RID: 4672
		private int m_ApiVersion;

		// Token: 0x04001241 RID: 4673
		private uint m_DataLengthBytes;

		// Token: 0x04001242 RID: 4674
		private IntPtr m_Data;
	}
}
