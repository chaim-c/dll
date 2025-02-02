using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatServer
{
	// Token: 0x020005D5 RID: 1493
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UnregisterClientOptionsInternal : ISettable<UnregisterClientOptions>, IDisposable
	{
		// Token: 0x17000B47 RID: 2887
		// (set) Token: 0x06002651 RID: 9809 RVA: 0x00038F44 File Offset: 0x00037144
		public IntPtr ClientHandle
		{
			set
			{
				this.m_ClientHandle = value;
			}
		}

		// Token: 0x06002652 RID: 9810 RVA: 0x00038F4E File Offset: 0x0003714E
		public void Set(ref UnregisterClientOptions other)
		{
			this.m_ApiVersion = 1;
			this.ClientHandle = other.ClientHandle;
		}

		// Token: 0x06002653 RID: 9811 RVA: 0x00038F68 File Offset: 0x00037168
		public void Set(ref UnregisterClientOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.ClientHandle = other.Value.ClientHandle;
			}
		}

		// Token: 0x06002654 RID: 9812 RVA: 0x00038F9E File Offset: 0x0003719E
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientHandle);
		}

		// Token: 0x040010CD RID: 4301
		private int m_ApiVersion;

		// Token: 0x040010CE RID: 4302
		private IntPtr m_ClientHandle;
	}
}
