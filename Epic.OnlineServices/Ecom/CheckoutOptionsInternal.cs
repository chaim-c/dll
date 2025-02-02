using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200048F RID: 1167
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CheckoutOptionsInternal : ISettable<CheckoutOptions>, IDisposable
	{
		// Token: 0x170008B7 RID: 2231
		// (set) Token: 0x06001E44 RID: 7748 RVA: 0x0002CCFA File Offset: 0x0002AEFA
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x170008B8 RID: 2232
		// (set) Token: 0x06001E45 RID: 7749 RVA: 0x0002CD0A File Offset: 0x0002AF0A
		public Utf8String OverrideCatalogNamespace
		{
			set
			{
				Helper.Set(value, ref this.m_OverrideCatalogNamespace);
			}
		}

		// Token: 0x170008B9 RID: 2233
		// (set) Token: 0x06001E46 RID: 7750 RVA: 0x0002CD1A File Offset: 0x0002AF1A
		public CheckoutEntry[] Entries
		{
			set
			{
				Helper.Set<CheckoutEntry, CheckoutEntryInternal>(ref value, ref this.m_Entries, out this.m_EntryCount);
			}
		}

		// Token: 0x06001E47 RID: 7751 RVA: 0x0002CD31 File Offset: 0x0002AF31
		public void Set(ref CheckoutOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.OverrideCatalogNamespace = other.OverrideCatalogNamespace;
			this.Entries = other.Entries;
		}

		// Token: 0x06001E48 RID: 7752 RVA: 0x0002CD64 File Offset: 0x0002AF64
		public void Set(ref CheckoutOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.OverrideCatalogNamespace = other.Value.OverrideCatalogNamespace;
				this.Entries = other.Value.Entries;
			}
		}

		// Token: 0x06001E49 RID: 7753 RVA: 0x0002CDC4 File Offset: 0x0002AFC4
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_OverrideCatalogNamespace);
			Helper.Dispose(ref this.m_Entries);
		}

		// Token: 0x04000D5B RID: 3419
		private int m_ApiVersion;

		// Token: 0x04000D5C RID: 3420
		private IntPtr m_LocalUserId;

		// Token: 0x04000D5D RID: 3421
		private IntPtr m_OverrideCatalogNamespace;

		// Token: 0x04000D5E RID: 3422
		private uint m_EntryCount;

		// Token: 0x04000D5F RID: 3423
		private IntPtr m_Entries;
	}
}
