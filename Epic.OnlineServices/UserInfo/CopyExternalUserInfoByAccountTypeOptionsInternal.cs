using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000027 RID: 39
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyExternalUserInfoByAccountTypeOptionsInternal : ISettable<CopyExternalUserInfoByAccountTypeOptions>, IDisposable
	{
		// Token: 0x1700001B RID: 27
		// (set) Token: 0x06000310 RID: 784 RVA: 0x00004B3A File Offset: 0x00002D3A
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x1700001C RID: 28
		// (set) Token: 0x06000311 RID: 785 RVA: 0x00004B4A File Offset: 0x00002D4A
		public EpicAccountId TargetUserId
		{
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x1700001D RID: 29
		// (set) Token: 0x06000312 RID: 786 RVA: 0x00004B5A File Offset: 0x00002D5A
		public ExternalAccountType AccountType
		{
			set
			{
				this.m_AccountType = value;
			}
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00004B64 File Offset: 0x00002D64
		public void Set(ref CopyExternalUserInfoByAccountTypeOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
			this.AccountType = other.AccountType;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00004B98 File Offset: 0x00002D98
		public void Set(ref CopyExternalUserInfoByAccountTypeOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.TargetUserId = other.Value.TargetUserId;
				this.AccountType = other.Value.AccountType;
			}
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00004BF8 File Offset: 0x00002DF8
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x04000147 RID: 327
		private int m_ApiVersion;

		// Token: 0x04000148 RID: 328
		private IntPtr m_LocalUserId;

		// Token: 0x04000149 RID: 329
		private IntPtr m_TargetUserId;

		// Token: 0x0400014A RID: 330
		private ExternalAccountType m_AccountType;
	}
}
