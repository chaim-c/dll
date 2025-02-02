using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004AF RID: 1199
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetEntitlementsByNameCountOptionsInternal : ISettable<GetEntitlementsByNameCountOptions>, IDisposable
	{
		// Token: 0x17000906 RID: 2310
		// (set) Token: 0x06001F15 RID: 7957 RVA: 0x0002E705 File Offset: 0x0002C905
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000907 RID: 2311
		// (set) Token: 0x06001F16 RID: 7958 RVA: 0x0002E715 File Offset: 0x0002C915
		public Utf8String EntitlementName
		{
			set
			{
				Helper.Set(value, ref this.m_EntitlementName);
			}
		}

		// Token: 0x06001F17 RID: 7959 RVA: 0x0002E725 File Offset: 0x0002C925
		public void Set(ref GetEntitlementsByNameCountOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.EntitlementName = other.EntitlementName;
		}

		// Token: 0x06001F18 RID: 7960 RVA: 0x0002E74C File Offset: 0x0002C94C
		public void Set(ref GetEntitlementsByNameCountOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.EntitlementName = other.Value.EntitlementName;
			}
		}

		// Token: 0x06001F19 RID: 7961 RVA: 0x0002E797 File Offset: 0x0002C997
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_EntitlementName);
		}

		// Token: 0x04000DEF RID: 3567
		private int m_ApiVersion;

		// Token: 0x04000DF0 RID: 3568
		private IntPtr m_LocalUserId;

		// Token: 0x04000DF1 RID: 3569
		private IntPtr m_EntitlementName;
	}
}
