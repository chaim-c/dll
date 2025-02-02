using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatServer
{
	// Token: 0x020005D3 RID: 1491
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UnprotectMessageOptionsInternal : ISettable<UnprotectMessageOptions>, IDisposable
	{
		// Token: 0x17000B43 RID: 2883
		// (set) Token: 0x06002649 RID: 9801 RVA: 0x00038E5D File Offset: 0x0003705D
		public IntPtr ClientHandle
		{
			set
			{
				this.m_ClientHandle = value;
			}
		}

		// Token: 0x17000B44 RID: 2884
		// (set) Token: 0x0600264A RID: 9802 RVA: 0x00038E67 File Offset: 0x00037067
		public ArraySegment<byte> Data
		{
			set
			{
				Helper.Set(value, ref this.m_Data, out this.m_DataLengthBytes);
			}
		}

		// Token: 0x17000B45 RID: 2885
		// (set) Token: 0x0600264B RID: 9803 RVA: 0x00038E7D File Offset: 0x0003707D
		public uint OutBufferSizeBytes
		{
			set
			{
				this.m_OutBufferSizeBytes = value;
			}
		}

		// Token: 0x0600264C RID: 9804 RVA: 0x00038E87 File Offset: 0x00037087
		public void Set(ref UnprotectMessageOptions other)
		{
			this.m_ApiVersion = 1;
			this.ClientHandle = other.ClientHandle;
			this.Data = other.Data;
			this.OutBufferSizeBytes = other.OutBufferSizeBytes;
		}

		// Token: 0x0600264D RID: 9805 RVA: 0x00038EB8 File Offset: 0x000370B8
		public void Set(ref UnprotectMessageOptions? other)
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

		// Token: 0x0600264E RID: 9806 RVA: 0x00038F18 File Offset: 0x00037118
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientHandle);
			Helper.Dispose(ref this.m_Data);
		}

		// Token: 0x040010C7 RID: 4295
		private int m_ApiVersion;

		// Token: 0x040010C8 RID: 4296
		private IntPtr m_ClientHandle;

		// Token: 0x040010C9 RID: 4297
		private uint m_DataLengthBytes;

		// Token: 0x040010CA RID: 4298
		private IntPtr m_Data;

		// Token: 0x040010CB RID: 4299
		private uint m_OutBufferSizeBytes;
	}
}
