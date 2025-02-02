using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004D6 RID: 1238
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryEntitlementsOptionsInternal : ISettable<QueryEntitlementsOptions>, IDisposable
	{
		// Token: 0x17000936 RID: 2358
		// (set) Token: 0x06001FC9 RID: 8137 RVA: 0x0002F215 File Offset: 0x0002D415
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000937 RID: 2359
		// (set) Token: 0x06001FCA RID: 8138 RVA: 0x0002F225 File Offset: 0x0002D425
		public Utf8String[] EntitlementNames
		{
			set
			{
				Helper.Set<Utf8String>(value, ref this.m_EntitlementNames, out this.m_EntitlementNameCount);
			}
		}

		// Token: 0x17000938 RID: 2360
		// (set) Token: 0x06001FCB RID: 8139 RVA: 0x0002F23B File Offset: 0x0002D43B
		public bool IncludeRedeemed
		{
			set
			{
				Helper.Set(value, ref this.m_IncludeRedeemed);
			}
		}

		// Token: 0x06001FCC RID: 8140 RVA: 0x0002F24B File Offset: 0x0002D44B
		public void Set(ref QueryEntitlementsOptions other)
		{
			this.m_ApiVersion = 2;
			this.LocalUserId = other.LocalUserId;
			this.EntitlementNames = other.EntitlementNames;
			this.IncludeRedeemed = other.IncludeRedeemed;
		}

		// Token: 0x06001FCD RID: 8141 RVA: 0x0002F27C File Offset: 0x0002D47C
		public void Set(ref QueryEntitlementsOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 2;
				this.LocalUserId = other.Value.LocalUserId;
				this.EntitlementNames = other.Value.EntitlementNames;
				this.IncludeRedeemed = other.Value.IncludeRedeemed;
			}
		}

		// Token: 0x06001FCE RID: 8142 RVA: 0x0002F2DC File Offset: 0x0002D4DC
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_EntitlementNames);
		}

		// Token: 0x04000E2C RID: 3628
		private int m_ApiVersion;

		// Token: 0x04000E2D RID: 3629
		private IntPtr m_LocalUserId;

		// Token: 0x04000E2E RID: 3630
		private IntPtr m_EntitlementNames;

		// Token: 0x04000E2F RID: 3631
		private uint m_EntitlementNameCount;

		// Token: 0x04000E30 RID: 3632
		private int m_IncludeRedeemed;
	}
}
