using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004E2 RID: 1250
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryOwnershipOptionsInternal : ISettable<QueryOwnershipOptions>, IDisposable
	{
		// Token: 0x1700095D RID: 2397
		// (set) Token: 0x06002028 RID: 8232 RVA: 0x0002FB42 File Offset: 0x0002DD42
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x1700095E RID: 2398
		// (set) Token: 0x06002029 RID: 8233 RVA: 0x0002FB52 File Offset: 0x0002DD52
		public Utf8String[] CatalogItemIds
		{
			set
			{
				Helper.Set<Utf8String>(value, ref this.m_CatalogItemIds, out this.m_CatalogItemIdCount);
			}
		}

		// Token: 0x1700095F RID: 2399
		// (set) Token: 0x0600202A RID: 8234 RVA: 0x0002FB68 File Offset: 0x0002DD68
		public Utf8String CatalogNamespace
		{
			set
			{
				Helper.Set(value, ref this.m_CatalogNamespace);
			}
		}

		// Token: 0x0600202B RID: 8235 RVA: 0x0002FB78 File Offset: 0x0002DD78
		public void Set(ref QueryOwnershipOptions other)
		{
			this.m_ApiVersion = 2;
			this.LocalUserId = other.LocalUserId;
			this.CatalogItemIds = other.CatalogItemIds;
			this.CatalogNamespace = other.CatalogNamespace;
		}

		// Token: 0x0600202C RID: 8236 RVA: 0x0002FBAC File Offset: 0x0002DDAC
		public void Set(ref QueryOwnershipOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 2;
				this.LocalUserId = other.Value.LocalUserId;
				this.CatalogItemIds = other.Value.CatalogItemIds;
				this.CatalogNamespace = other.Value.CatalogNamespace;
			}
		}

		// Token: 0x0600202D RID: 8237 RVA: 0x0002FC0C File Offset: 0x0002DE0C
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_CatalogItemIds);
			Helper.Dispose(ref this.m_CatalogNamespace);
		}

		// Token: 0x04000E56 RID: 3670
		private int m_ApiVersion;

		// Token: 0x04000E57 RID: 3671
		private IntPtr m_LocalUserId;

		// Token: 0x04000E58 RID: 3672
		private IntPtr m_CatalogItemIds;

		// Token: 0x04000E59 RID: 3673
		private uint m_CatalogItemIdCount;

		// Token: 0x04000E5A RID: 3674
		private IntPtr m_CatalogNamespace;
	}
}
