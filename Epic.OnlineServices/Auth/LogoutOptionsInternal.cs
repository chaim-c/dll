using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x02000590 RID: 1424
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LogoutOptionsInternal : ISettable<LogoutOptions>, IDisposable
	{
		// Token: 0x17000ABE RID: 2750
		// (set) Token: 0x0600248F RID: 9359 RVA: 0x000367BB File Offset: 0x000349BB
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x06002490 RID: 9360 RVA: 0x000367CB File Offset: 0x000349CB
		public void Set(ref LogoutOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x06002491 RID: 9361 RVA: 0x000367E4 File Offset: 0x000349E4
		public void Set(ref LogoutOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06002492 RID: 9362 RVA: 0x0003681A File Offset: 0x00034A1A
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x04001022 RID: 4130
		private int m_ApiVersion;

		// Token: 0x04001023 RID: 4131
		private IntPtr m_LocalUserId;
	}
}
