using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x0200052F RID: 1327
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetExternalAccountMappingsOptionsInternal : ISettable<GetExternalAccountMappingsOptions>, IDisposable
	{
		// Token: 0x170009F3 RID: 2547
		// (set) Token: 0x06002210 RID: 8720 RVA: 0x00032ECA File Offset: 0x000310CA
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x170009F4 RID: 2548
		// (set) Token: 0x06002211 RID: 8721 RVA: 0x00032EDA File Offset: 0x000310DA
		public ExternalAccountType AccountIdType
		{
			set
			{
				this.m_AccountIdType = value;
			}
		}

		// Token: 0x170009F5 RID: 2549
		// (set) Token: 0x06002212 RID: 8722 RVA: 0x00032EE4 File Offset: 0x000310E4
		public Utf8String TargetExternalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_TargetExternalUserId);
			}
		}

		// Token: 0x06002213 RID: 8723 RVA: 0x00032EF4 File Offset: 0x000310F4
		public void Set(ref GetExternalAccountMappingsOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.AccountIdType = other.AccountIdType;
			this.TargetExternalUserId = other.TargetExternalUserId;
		}

		// Token: 0x06002214 RID: 8724 RVA: 0x00032F28 File Offset: 0x00031128
		public void Set(ref GetExternalAccountMappingsOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.AccountIdType = other.Value.AccountIdType;
				this.TargetExternalUserId = other.Value.TargetExternalUserId;
			}
		}

		// Token: 0x06002215 RID: 8725 RVA: 0x00032F88 File Offset: 0x00031188
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_TargetExternalUserId);
		}

		// Token: 0x04000F26 RID: 3878
		private int m_ApiVersion;

		// Token: 0x04000F27 RID: 3879
		private IntPtr m_LocalUserId;

		// Token: 0x04000F28 RID: 3880
		private ExternalAccountType m_AccountIdType;

		// Token: 0x04000F29 RID: 3881
		private IntPtr m_TargetExternalUserId;
	}
}
