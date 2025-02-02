using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000515 RID: 1301
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyIdTokenOptionsInternal : ISettable<CopyIdTokenOptions>, IDisposable
	{
		// Token: 0x170009BE RID: 2494
		// (set) Token: 0x0600217F RID: 8575 RVA: 0x000321E4 File Offset: 0x000303E4
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x06002180 RID: 8576 RVA: 0x000321F4 File Offset: 0x000303F4
		public void Set(ref CopyIdTokenOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x06002181 RID: 8577 RVA: 0x0003220C File Offset: 0x0003040C
		public void Set(ref CopyIdTokenOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06002182 RID: 8578 RVA: 0x00032242 File Offset: 0x00030442
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000EEA RID: 3818
		private int m_ApiVersion;

		// Token: 0x04000EEB RID: 3819
		private IntPtr m_LocalUserId;
	}
}
