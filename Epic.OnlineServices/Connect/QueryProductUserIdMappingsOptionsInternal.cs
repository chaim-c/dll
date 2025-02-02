using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x0200055F RID: 1375
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryProductUserIdMappingsOptionsInternal : ISettable<QueryProductUserIdMappingsOptions>, IDisposable
	{
		// Token: 0x17000A3A RID: 2618
		// (set) Token: 0x06002320 RID: 8992 RVA: 0x00033F3D File Offset: 0x0003213D
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000A3B RID: 2619
		// (set) Token: 0x06002321 RID: 8993 RVA: 0x00033F4D File Offset: 0x0003214D
		public ExternalAccountType AccountIdType_DEPRECATED
		{
			set
			{
				this.m_AccountIdType_DEPRECATED = value;
			}
		}

		// Token: 0x17000A3C RID: 2620
		// (set) Token: 0x06002322 RID: 8994 RVA: 0x00033F57 File Offset: 0x00032157
		public ProductUserId[] ProductUserIds
		{
			set
			{
				Helper.Set<ProductUserId>(value, ref this.m_ProductUserIds, out this.m_ProductUserIdCount);
			}
		}

		// Token: 0x06002323 RID: 8995 RVA: 0x00033F6D File Offset: 0x0003216D
		public void Set(ref QueryProductUserIdMappingsOptions other)
		{
			this.m_ApiVersion = 2;
			this.LocalUserId = other.LocalUserId;
			this.AccountIdType_DEPRECATED = other.AccountIdType_DEPRECATED;
			this.ProductUserIds = other.ProductUserIds;
		}

		// Token: 0x06002324 RID: 8996 RVA: 0x00033FA0 File Offset: 0x000321A0
		public void Set(ref QueryProductUserIdMappingsOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 2;
				this.LocalUserId = other.Value.LocalUserId;
				this.AccountIdType_DEPRECATED = other.Value.AccountIdType_DEPRECATED;
				this.ProductUserIds = other.Value.ProductUserIds;
			}
		}

		// Token: 0x06002325 RID: 8997 RVA: 0x00034000 File Offset: 0x00032200
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_ProductUserIds);
		}

		// Token: 0x04000F70 RID: 3952
		private int m_ApiVersion;

		// Token: 0x04000F71 RID: 3953
		private IntPtr m_LocalUserId;

		// Token: 0x04000F72 RID: 3954
		private ExternalAccountType m_AccountIdType_DEPRECATED;

		// Token: 0x04000F73 RID: 3955
		private IntPtr m_ProductUserIds;

		// Token: 0x04000F74 RID: 3956
		private uint m_ProductUserIdCount;
	}
}
