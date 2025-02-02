using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatServer
{
	// Token: 0x020005CB RID: 1483
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ProtectMessageOptionsInternal : ISettable<ProtectMessageOptions>, IDisposable
	{
		// Token: 0x17000B29 RID: 2857
		// (set) Token: 0x06002616 RID: 9750 RVA: 0x000389D8 File Offset: 0x00036BD8
		public IntPtr ClientHandle
		{
			set
			{
				this.m_ClientHandle = value;
			}
		}

		// Token: 0x17000B2A RID: 2858
		// (set) Token: 0x06002617 RID: 9751 RVA: 0x000389E2 File Offset: 0x00036BE2
		public ArraySegment<byte> Data
		{
			set
			{
				Helper.Set(value, ref this.m_Data, out this.m_DataLengthBytes);
			}
		}

		// Token: 0x17000B2B RID: 2859
		// (set) Token: 0x06002618 RID: 9752 RVA: 0x000389F8 File Offset: 0x00036BF8
		public uint OutBufferSizeBytes
		{
			set
			{
				this.m_OutBufferSizeBytes = value;
			}
		}

		// Token: 0x06002619 RID: 9753 RVA: 0x00038A02 File Offset: 0x00036C02
		public void Set(ref ProtectMessageOptions other)
		{
			this.m_ApiVersion = 1;
			this.ClientHandle = other.ClientHandle;
			this.Data = other.Data;
			this.OutBufferSizeBytes = other.OutBufferSizeBytes;
		}

		// Token: 0x0600261A RID: 9754 RVA: 0x00038A34 File Offset: 0x00036C34
		public void Set(ref ProtectMessageOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.ClientHandle = other.Value.ClientHandle;
				this.Data = other.Value.Data;
				this.OutBufferSizeBytes = other.Value.OutBufferSizeBytes;
			}
		}

		// Token: 0x0600261B RID: 9755 RVA: 0x00038A94 File Offset: 0x00036C94
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientHandle);
			Helper.Dispose(ref this.m_Data);
		}

		// Token: 0x040010A7 RID: 4263
		private int m_ApiVersion;

		// Token: 0x040010A8 RID: 4264
		private IntPtr m_ClientHandle;

		// Token: 0x040010A9 RID: 4265
		private uint m_DataLengthBytes;

		// Token: 0x040010AA RID: 4266
		private IntPtr m_Data;

		// Token: 0x040010AB RID: 4267
		private uint m_OutBufferSizeBytes;
	}
}
