using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000110 RID: 272
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryInvitesOptionsInternal : ISettable<QueryInvitesOptions>, IDisposable
	{
		// Token: 0x170001AF RID: 431
		// (set) Token: 0x0600084E RID: 2126 RVA: 0x0000BE93 File Offset: 0x0000A093
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x0000BEA3 File Offset: 0x0000A0A3
		public void Set(ref QueryInvitesOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0000BEBC File Offset: 0x0000A0BC
		public void Set(ref QueryInvitesOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x0000BEF2 File Offset: 0x0000A0F2
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x040003CA RID: 970
		private int m_ApiVersion;

		// Token: 0x040003CB RID: 971
		private IntPtr m_LocalUserId;
	}
}
