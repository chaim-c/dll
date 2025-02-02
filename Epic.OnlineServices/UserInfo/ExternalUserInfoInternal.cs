using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x0200002D RID: 45
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ExternalUserInfoInternal : IGettable<ExternalUserInfo>, ISettable<ExternalUserInfo>, IDisposable
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000334 RID: 820 RVA: 0x00004E6C File Offset: 0x0000306C
		// (set) Token: 0x06000335 RID: 821 RVA: 0x00004E84 File Offset: 0x00003084
		public ExternalAccountType AccountType
		{
			get
			{
				return this.m_AccountType;
			}
			set
			{
				this.m_AccountType = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000336 RID: 822 RVA: 0x00004E90 File Offset: 0x00003090
		// (set) Token: 0x06000337 RID: 823 RVA: 0x00004EB1 File Offset: 0x000030B1
		public Utf8String AccountId
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_AccountId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_AccountId);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000338 RID: 824 RVA: 0x00004EC4 File Offset: 0x000030C4
		// (set) Token: 0x06000339 RID: 825 RVA: 0x00004EE5 File Offset: 0x000030E5
		public Utf8String DisplayName
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_DisplayName, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_DisplayName);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600033A RID: 826 RVA: 0x00004EF8 File Offset: 0x000030F8
		// (set) Token: 0x0600033B RID: 827 RVA: 0x00004F19 File Offset: 0x00003119
		public Utf8String DisplayNameSanitized
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_DisplayNameSanitized, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_DisplayNameSanitized);
			}
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00004F29 File Offset: 0x00003129
		public void Set(ref ExternalUserInfo other)
		{
			this.m_ApiVersion = 2;
			this.AccountType = other.AccountType;
			this.AccountId = other.AccountId;
			this.DisplayName = other.DisplayName;
			this.DisplayNameSanitized = other.DisplayNameSanitized;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00004F68 File Offset: 0x00003168
		public void Set(ref ExternalUserInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 2;
				this.AccountType = other.Value.AccountType;
				this.AccountId = other.Value.AccountId;
				this.DisplayName = other.Value.DisplayName;
				this.DisplayNameSanitized = other.Value.DisplayNameSanitized;
			}
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00004FDD File Offset: 0x000031DD
		public void Dispose()
		{
			Helper.Dispose(ref this.m_AccountId);
			Helper.Dispose(ref this.m_DisplayName);
			Helper.Dispose(ref this.m_DisplayNameSanitized);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00005004 File Offset: 0x00003204
		public void Get(out ExternalUserInfo output)
		{
			output = default(ExternalUserInfo);
			output.Set(ref this);
		}

		// Token: 0x0400015B RID: 347
		private int m_ApiVersion;

		// Token: 0x0400015C RID: 348
		private ExternalAccountType m_AccountType;

		// Token: 0x0400015D RID: 349
		private IntPtr m_AccountId;

		// Token: 0x0400015E RID: 350
		private IntPtr m_DisplayName;

		// Token: 0x0400015F RID: 351
		private IntPtr m_DisplayNameSanitized;
	}
}
