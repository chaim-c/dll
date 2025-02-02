using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004E6 RID: 1254
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryOwnershipTokenOptionsInternal : ISettable<QueryOwnershipTokenOptions>, IDisposable
	{
		// Token: 0x1700096C RID: 2412
		// (set) Token: 0x0600204B RID: 8267 RVA: 0x0002FEB2 File Offset: 0x0002E0B2
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x1700096D RID: 2413
		// (set) Token: 0x0600204C RID: 8268 RVA: 0x0002FEC2 File Offset: 0x0002E0C2
		public Utf8String[] CatalogItemIds
		{
			set
			{
				Helper.Set<Utf8String>(value, ref this.m_CatalogItemIds, out this.m_CatalogItemIdCount);
			}
		}

		// Token: 0x1700096E RID: 2414
		// (set) Token: 0x0600204D RID: 8269 RVA: 0x0002FED8 File Offset: 0x0002E0D8
		public Utf8String CatalogNamespace
		{
			set
			{
				Helper.Set(value, ref this.m_CatalogNamespace);
			}
		}

		// Token: 0x0600204E RID: 8270 RVA: 0x0002FEE8 File Offset: 0x0002E0E8
		public void Set(ref QueryOwnershipTokenOptions other)
		{
			this.m_ApiVersion = 2;
			this.LocalUserId = other.LocalUserId;
			this.CatalogItemIds = other.CatalogItemIds;
			this.CatalogNamespace = other.CatalogNamespace;
		}

		// Token: 0x0600204F RID: 8271 RVA: 0x0002FF1C File Offset: 0x0002E11C
		public void Set(ref QueryOwnershipTokenOptions? other)
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

		// Token: 0x06002050 RID: 8272 RVA: 0x0002FF7C File Offset: 0x0002E17C
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_CatalogItemIds);
			Helper.Dispose(ref this.m_CatalogNamespace);
		}

		// Token: 0x04000E66 RID: 3686
		private int m_ApiVersion;

		// Token: 0x04000E67 RID: 3687
		private IntPtr m_LocalUserId;

		// Token: 0x04000E68 RID: 3688
		private IntPtr m_CatalogItemIds;

		// Token: 0x04000E69 RID: 3689
		private uint m_CatalogItemIdCount;

		// Token: 0x04000E6A RID: 3690
		private IntPtr m_CatalogNamespace;
	}
}
