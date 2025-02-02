using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004B1 RID: 1201
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetEntitlementsCountOptionsInternal : ISettable<GetEntitlementsCountOptions>, IDisposable
	{
		// Token: 0x17000909 RID: 2313
		// (set) Token: 0x06001F1C RID: 7964 RVA: 0x0002E7C3 File Offset: 0x0002C9C3
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x06001F1D RID: 7965 RVA: 0x0002E7D3 File Offset: 0x0002C9D3
		public void Set(ref GetEntitlementsCountOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x06001F1E RID: 7966 RVA: 0x0002E7EC File Offset: 0x0002C9EC
		public void Set(ref GetEntitlementsCountOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06001F1F RID: 7967 RVA: 0x0002E822 File Offset: 0x0002CA22
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000DF3 RID: 3571
		private int m_ApiVersion;

		// Token: 0x04000DF4 RID: 3572
		private IntPtr m_LocalUserId;
	}
}
