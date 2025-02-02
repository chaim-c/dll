using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatServer
{
	// Token: 0x020005CD RID: 1485
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ReceiveMessageFromClientOptionsInternal : ISettable<ReceiveMessageFromClientOptions>, IDisposable
	{
		// Token: 0x17000B2E RID: 2862
		// (set) Token: 0x06002620 RID: 9760 RVA: 0x00038AD1 File Offset: 0x00036CD1
		public IntPtr ClientHandle
		{
			set
			{
				this.m_ClientHandle = value;
			}
		}

		// Token: 0x17000B2F RID: 2863
		// (set) Token: 0x06002621 RID: 9761 RVA: 0x00038ADB File Offset: 0x00036CDB
		public ArraySegment<byte> Data
		{
			set
			{
				Helper.Set(value, ref this.m_Data, out this.m_DataLengthBytes);
			}
		}

		// Token: 0x06002622 RID: 9762 RVA: 0x00038AF1 File Offset: 0x00036CF1
		public void Set(ref ReceiveMessageFromClientOptions other)
		{
			this.m_ApiVersion = 1;
			this.ClientHandle = other.ClientHandle;
			this.Data = other.Data;
		}

		// Token: 0x06002623 RID: 9763 RVA: 0x00038B18 File Offset: 0x00036D18
		public void Set(ref ReceiveMessageFromClientOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.ClientHandle = other.Value.ClientHandle;
				this.Data = other.Value.Data;
			}
		}

		// Token: 0x06002624 RID: 9764 RVA: 0x00038B63 File Offset: 0x00036D63
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientHandle);
			Helper.Dispose(ref this.m_Data);
		}

		// Token: 0x040010AE RID: 4270
		private int m_ApiVersion;

		// Token: 0x040010AF RID: 4271
		private IntPtr m_ClientHandle;

		// Token: 0x040010B0 RID: 4272
		private uint m_DataLengthBytes;

		// Token: 0x040010B1 RID: 4273
		private IntPtr m_Data;
	}
}
