using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200049D RID: 1181
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyLastRedeemedEntitlementByIndexOptionsInternal : ISettable<CopyLastRedeemedEntitlementByIndexOptions>, IDisposable
	{
		// Token: 0x170008DA RID: 2266
		// (set) Token: 0x06001E8D RID: 7821 RVA: 0x0002D385 File Offset: 0x0002B585
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x170008DB RID: 2267
		// (set) Token: 0x06001E8E RID: 7822 RVA: 0x0002D395 File Offset: 0x0002B595
		public uint RedeemedEntitlementIndex
		{
			set
			{
				this.m_RedeemedEntitlementIndex = value;
			}
		}

		// Token: 0x06001E8F RID: 7823 RVA: 0x0002D39F File Offset: 0x0002B59F
		public void Set(ref CopyLastRedeemedEntitlementByIndexOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.RedeemedEntitlementIndex = other.RedeemedEntitlementIndex;
		}

		// Token: 0x06001E90 RID: 7824 RVA: 0x0002D3C4 File Offset: 0x0002B5C4
		public void Set(ref CopyLastRedeemedEntitlementByIndexOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.RedeemedEntitlementIndex = other.Value.RedeemedEntitlementIndex;
			}
		}

		// Token: 0x06001E91 RID: 7825 RVA: 0x0002D40F File Offset: 0x0002B60F
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000D86 RID: 3462
		private int m_ApiVersion;

		// Token: 0x04000D87 RID: 3463
		private IntPtr m_LocalUserId;

		// Token: 0x04000D88 RID: 3464
		private uint m_RedeemedEntitlementIndex;
	}
}
