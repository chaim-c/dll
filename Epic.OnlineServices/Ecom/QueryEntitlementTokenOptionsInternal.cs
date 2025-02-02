using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004DA RID: 1242
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryEntitlementTokenOptionsInternal : ISettable<QueryEntitlementTokenOptions>, IDisposable
	{
		// Token: 0x17000944 RID: 2372
		// (set) Token: 0x06001FEA RID: 8170 RVA: 0x0002F565 File Offset: 0x0002D765
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000945 RID: 2373
		// (set) Token: 0x06001FEB RID: 8171 RVA: 0x0002F575 File Offset: 0x0002D775
		public Utf8String[] EntitlementNames
		{
			set
			{
				Helper.Set<Utf8String>(value, ref this.m_EntitlementNames, out this.m_EntitlementNameCount);
			}
		}

		// Token: 0x06001FEC RID: 8172 RVA: 0x0002F58B File Offset: 0x0002D78B
		public void Set(ref QueryEntitlementTokenOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.EntitlementNames = other.EntitlementNames;
		}

		// Token: 0x06001FED RID: 8173 RVA: 0x0002F5B0 File Offset: 0x0002D7B0
		public void Set(ref QueryEntitlementTokenOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.EntitlementNames = other.Value.EntitlementNames;
			}
		}

		// Token: 0x06001FEE RID: 8174 RVA: 0x0002F5FB File Offset: 0x0002D7FB
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_EntitlementNames);
		}

		// Token: 0x04000E3B RID: 3643
		private int m_ApiVersion;

		// Token: 0x04000E3C RID: 3644
		private IntPtr m_LocalUserId;

		// Token: 0x04000E3D RID: 3645
		private IntPtr m_EntitlementNames;

		// Token: 0x04000E3E RID: 3646
		private uint m_EntitlementNameCount;
	}
}
