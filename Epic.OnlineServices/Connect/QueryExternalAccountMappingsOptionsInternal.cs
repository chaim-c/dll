using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x0200055B RID: 1371
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryExternalAccountMappingsOptionsInternal : ISettable<QueryExternalAccountMappingsOptions>, IDisposable
	{
		// Token: 0x17000A2D RID: 2605
		// (set) Token: 0x06002301 RID: 8961 RVA: 0x00033C5D File Offset: 0x00031E5D
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000A2E RID: 2606
		// (set) Token: 0x06002302 RID: 8962 RVA: 0x00033C6D File Offset: 0x00031E6D
		public ExternalAccountType AccountIdType
		{
			set
			{
				this.m_AccountIdType = value;
			}
		}

		// Token: 0x17000A2F RID: 2607
		// (set) Token: 0x06002303 RID: 8963 RVA: 0x00033C77 File Offset: 0x00031E77
		public Utf8String[] ExternalAccountIds
		{
			set
			{
				Helper.Set<Utf8String>(value, ref this.m_ExternalAccountIds, true, out this.m_ExternalAccountIdCount);
			}
		}

		// Token: 0x06002304 RID: 8964 RVA: 0x00033C8E File Offset: 0x00031E8E
		public void Set(ref QueryExternalAccountMappingsOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.AccountIdType = other.AccountIdType;
			this.ExternalAccountIds = other.ExternalAccountIds;
		}

		// Token: 0x06002305 RID: 8965 RVA: 0x00033CC0 File Offset: 0x00031EC0
		public void Set(ref QueryExternalAccountMappingsOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.AccountIdType = other.Value.AccountIdType;
				this.ExternalAccountIds = other.Value.ExternalAccountIds;
			}
		}

		// Token: 0x06002306 RID: 8966 RVA: 0x00033D20 File Offset: 0x00031F20
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_ExternalAccountIds);
		}

		// Token: 0x04000F62 RID: 3938
		private int m_ApiVersion;

		// Token: 0x04000F63 RID: 3939
		private IntPtr m_LocalUserId;

		// Token: 0x04000F64 RID: 3940
		private ExternalAccountType m_AccountIdType;

		// Token: 0x04000F65 RID: 3941
		private IntPtr m_ExternalAccountIds;

		// Token: 0x04000F66 RID: 3942
		private uint m_ExternalAccountIdCount;
	}
}
