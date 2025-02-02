using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004EA RID: 1258
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RedeemEntitlementsOptionsInternal : ISettable<RedeemEntitlementsOptions>, IDisposable
	{
		// Token: 0x1700097A RID: 2426
		// (set) Token: 0x0600206C RID: 8300 RVA: 0x000301F9 File Offset: 0x0002E3F9
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x1700097B RID: 2427
		// (set) Token: 0x0600206D RID: 8301 RVA: 0x00030209 File Offset: 0x0002E409
		public Utf8String[] EntitlementIds
		{
			set
			{
				Helper.Set<Utf8String>(value, ref this.m_EntitlementIds, out this.m_EntitlementIdCount);
			}
		}

		// Token: 0x0600206E RID: 8302 RVA: 0x0003021F File Offset: 0x0002E41F
		public void Set(ref RedeemEntitlementsOptions other)
		{
			this.m_ApiVersion = 2;
			this.LocalUserId = other.LocalUserId;
			this.EntitlementIds = other.EntitlementIds;
		}

		// Token: 0x0600206F RID: 8303 RVA: 0x00030244 File Offset: 0x0002E444
		public void Set(ref RedeemEntitlementsOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 2;
				this.LocalUserId = other.Value.LocalUserId;
				this.EntitlementIds = other.Value.EntitlementIds;
			}
		}

		// Token: 0x06002070 RID: 8304 RVA: 0x0003028F File Offset: 0x0002E48F
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_EntitlementIds);
		}

		// Token: 0x04000E75 RID: 3701
		private int m_ApiVersion;

		// Token: 0x04000E76 RID: 3702
		private IntPtr m_LocalUserId;

		// Token: 0x04000E77 RID: 3703
		private uint m_EntitlementIdCount;

		// Token: 0x04000E78 RID: 3704
		private IntPtr m_EntitlementIds;
	}
}
