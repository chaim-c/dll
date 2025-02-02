using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000493 RID: 1171
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyEntitlementByIndexOptionsInternal : ISettable<CopyEntitlementByIndexOptions>, IDisposable
	{
		// Token: 0x170008C0 RID: 2240
		// (set) Token: 0x06001E57 RID: 7767 RVA: 0x0002CEDC File Offset: 0x0002B0DC
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x170008C1 RID: 2241
		// (set) Token: 0x06001E58 RID: 7768 RVA: 0x0002CEEC File Offset: 0x0002B0EC
		public uint EntitlementIndex
		{
			set
			{
				this.m_EntitlementIndex = value;
			}
		}

		// Token: 0x06001E59 RID: 7769 RVA: 0x0002CEF6 File Offset: 0x0002B0F6
		public void Set(ref CopyEntitlementByIndexOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.EntitlementIndex = other.EntitlementIndex;
		}

		// Token: 0x06001E5A RID: 7770 RVA: 0x0002CF1C File Offset: 0x0002B11C
		public void Set(ref CopyEntitlementByIndexOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.EntitlementIndex = other.Value.EntitlementIndex;
			}
		}

		// Token: 0x06001E5B RID: 7771 RVA: 0x0002CF67 File Offset: 0x0002B167
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000D67 RID: 3431
		private int m_ApiVersion;

		// Token: 0x04000D68 RID: 3432
		private IntPtr m_LocalUserId;

		// Token: 0x04000D69 RID: 3433
		private uint m_EntitlementIndex;
	}
}
