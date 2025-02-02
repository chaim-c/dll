using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000533 RID: 1331
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetProductUserIdMappingOptionsInternal : ISettable<GetProductUserIdMappingOptions>, IDisposable
	{
		// Token: 0x170009FB RID: 2555
		// (set) Token: 0x06002222 RID: 8738 RVA: 0x00033054 File Offset: 0x00031254
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x170009FC RID: 2556
		// (set) Token: 0x06002223 RID: 8739 RVA: 0x00033064 File Offset: 0x00031264
		public ExternalAccountType AccountIdType
		{
			set
			{
				this.m_AccountIdType = value;
			}
		}

		// Token: 0x170009FD RID: 2557
		// (set) Token: 0x06002224 RID: 8740 RVA: 0x0003306E File Offset: 0x0003126E
		public ProductUserId TargetProductUserId
		{
			set
			{
				Helper.Set(value, ref this.m_TargetProductUserId);
			}
		}

		// Token: 0x06002225 RID: 8741 RVA: 0x0003307E File Offset: 0x0003127E
		public void Set(ref GetProductUserIdMappingOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.AccountIdType = other.AccountIdType;
			this.TargetProductUserId = other.TargetProductUserId;
		}

		// Token: 0x06002226 RID: 8742 RVA: 0x000330B0 File Offset: 0x000312B0
		public void Set(ref GetProductUserIdMappingOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.AccountIdType = other.Value.AccountIdType;
				this.TargetProductUserId = other.Value.TargetProductUserId;
			}
		}

		// Token: 0x06002227 RID: 8743 RVA: 0x00033110 File Offset: 0x00031310
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_TargetProductUserId);
		}

		// Token: 0x04000F30 RID: 3888
		private int m_ApiVersion;

		// Token: 0x04000F31 RID: 3889
		private IntPtr m_LocalUserId;

		// Token: 0x04000F32 RID: 3890
		private ExternalAccountType m_AccountIdType;

		// Token: 0x04000F33 RID: 3891
		private IntPtr m_TargetProductUserId;
	}
}
