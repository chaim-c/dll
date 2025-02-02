using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000491 RID: 1169
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyEntitlementByIdOptionsInternal : ISettable<CopyEntitlementByIdOptions>, IDisposable
	{
		// Token: 0x170008BC RID: 2236
		// (set) Token: 0x06001E4E RID: 7758 RVA: 0x0002CE0D File Offset: 0x0002B00D
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x170008BD RID: 2237
		// (set) Token: 0x06001E4F RID: 7759 RVA: 0x0002CE1D File Offset: 0x0002B01D
		public Utf8String EntitlementId
		{
			set
			{
				Helper.Set(value, ref this.m_EntitlementId);
			}
		}

		// Token: 0x06001E50 RID: 7760 RVA: 0x0002CE2D File Offset: 0x0002B02D
		public void Set(ref CopyEntitlementByIdOptions other)
		{
			this.m_ApiVersion = 2;
			this.LocalUserId = other.LocalUserId;
			this.EntitlementId = other.EntitlementId;
		}

		// Token: 0x06001E51 RID: 7761 RVA: 0x0002CE54 File Offset: 0x0002B054
		public void Set(ref CopyEntitlementByIdOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 2;
				this.LocalUserId = other.Value.LocalUserId;
				this.EntitlementId = other.Value.EntitlementId;
			}
		}

		// Token: 0x06001E52 RID: 7762 RVA: 0x0002CE9F File Offset: 0x0002B09F
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_EntitlementId);
		}

		// Token: 0x04000D62 RID: 3426
		private int m_ApiVersion;

		// Token: 0x04000D63 RID: 3427
		private IntPtr m_LocalUserId;

		// Token: 0x04000D64 RID: 3428
		private IntPtr m_EntitlementId;
	}
}
