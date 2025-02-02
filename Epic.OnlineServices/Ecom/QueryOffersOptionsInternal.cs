using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004DE RID: 1246
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryOffersOptionsInternal : ISettable<QueryOffersOptions>, IDisposable
	{
		// Token: 0x1700094F RID: 2383
		// (set) Token: 0x06002006 RID: 8198 RVA: 0x0002F808 File Offset: 0x0002DA08
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000950 RID: 2384
		// (set) Token: 0x06002007 RID: 8199 RVA: 0x0002F818 File Offset: 0x0002DA18
		public Utf8String OverrideCatalogNamespace
		{
			set
			{
				Helper.Set(value, ref this.m_OverrideCatalogNamespace);
			}
		}

		// Token: 0x06002008 RID: 8200 RVA: 0x0002F828 File Offset: 0x0002DA28
		public void Set(ref QueryOffersOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.OverrideCatalogNamespace = other.OverrideCatalogNamespace;
		}

		// Token: 0x06002009 RID: 8201 RVA: 0x0002F84C File Offset: 0x0002DA4C
		public void Set(ref QueryOffersOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.OverrideCatalogNamespace = other.Value.OverrideCatalogNamespace;
			}
		}

		// Token: 0x0600200A RID: 8202 RVA: 0x0002F897 File Offset: 0x0002DA97
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_OverrideCatalogNamespace);
		}

		// Token: 0x04000E47 RID: 3655
		private int m_ApiVersion;

		// Token: 0x04000E48 RID: 3656
		private IntPtr m_LocalUserId;

		// Token: 0x04000E49 RID: 3657
		private IntPtr m_OverrideCatalogNamespace;
	}
}
