using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x0200003D RID: 61
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryUserInfoByExternalAccountOptionsInternal : ISettable<QueryUserInfoByExternalAccountOptions>, IDisposable
	{
		// Token: 0x17000053 RID: 83
		// (set) Token: 0x060003AA RID: 938 RVA: 0x0000581B File Offset: 0x00003A1B
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000054 RID: 84
		// (set) Token: 0x060003AB RID: 939 RVA: 0x0000582B File Offset: 0x00003A2B
		public Utf8String ExternalAccountId
		{
			set
			{
				Helper.Set(value, ref this.m_ExternalAccountId);
			}
		}

		// Token: 0x17000055 RID: 85
		// (set) Token: 0x060003AC RID: 940 RVA: 0x0000583B File Offset: 0x00003A3B
		public ExternalAccountType AccountType
		{
			set
			{
				this.m_AccountType = value;
			}
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00005845 File Offset: 0x00003A45
		public void Set(ref QueryUserInfoByExternalAccountOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.ExternalAccountId = other.ExternalAccountId;
			this.AccountType = other.AccountType;
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00005878 File Offset: 0x00003A78
		public void Set(ref QueryUserInfoByExternalAccountOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.ExternalAccountId = other.Value.ExternalAccountId;
				this.AccountType = other.Value.AccountType;
			}
		}

		// Token: 0x060003AF RID: 943 RVA: 0x000058D8 File Offset: 0x00003AD8
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_ExternalAccountId);
		}

		// Token: 0x04000183 RID: 387
		private int m_ApiVersion;

		// Token: 0x04000184 RID: 388
		private IntPtr m_LocalUserId;

		// Token: 0x04000185 RID: 389
		private IntPtr m_ExternalAccountId;

		// Token: 0x04000186 RID: 390
		private ExternalAccountType m_AccountType;
	}
}
