using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004B7 RID: 1207
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetLastRedeemedEntitlementsCountOptionsInternal : ISettable<GetLastRedeemedEntitlementsCountOptions>, IDisposable
	{
		// Token: 0x17000913 RID: 2323
		// (set) Token: 0x06001F34 RID: 7988 RVA: 0x0002E9DB File Offset: 0x0002CBDB
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x06001F35 RID: 7989 RVA: 0x0002E9EB File Offset: 0x0002CBEB
		public void Set(ref GetLastRedeemedEntitlementsCountOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x06001F36 RID: 7990 RVA: 0x0002EA04 File Offset: 0x0002CC04
		public void Set(ref GetLastRedeemedEntitlementsCountOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06001F37 RID: 7991 RVA: 0x0002EA3A File Offset: 0x0002CC3A
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000E00 RID: 3584
		private int m_ApiVersion;

		// Token: 0x04000E01 RID: 3585
		private IntPtr m_LocalUserId;
	}
}
