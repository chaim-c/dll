using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000043 RID: 67
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UserInfoDataInternal : IGettable<UserInfoData>, ISettable<UserInfoData>, IDisposable
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060003DD RID: 989 RVA: 0x00005CD0 File Offset: 0x00003ED0
		// (set) Token: 0x060003DE RID: 990 RVA: 0x00005CF1 File Offset: 0x00003EF1
		public EpicAccountId UserId
		{
			get
			{
				EpicAccountId result;
				Helper.Get<EpicAccountId>(this.m_UserId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_UserId);
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060003DF RID: 991 RVA: 0x00005D04 File Offset: 0x00003F04
		// (set) Token: 0x060003E0 RID: 992 RVA: 0x00005D25 File Offset: 0x00003F25
		public Utf8String Country
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_Country, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_Country);
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x00005D38 File Offset: 0x00003F38
		// (set) Token: 0x060003E2 RID: 994 RVA: 0x00005D59 File Offset: 0x00003F59
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

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x00005D6C File Offset: 0x00003F6C
		// (set) Token: 0x060003E4 RID: 996 RVA: 0x00005D8D File Offset: 0x00003F8D
		public Utf8String PreferredLanguage
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_PreferredLanguage, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_PreferredLanguage);
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x00005DA0 File Offset: 0x00003FA0
		// (set) Token: 0x060003E6 RID: 998 RVA: 0x00005DC1 File Offset: 0x00003FC1
		public Utf8String Nickname
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_Nickname, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_Nickname);
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x00005DD4 File Offset: 0x00003FD4
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x00005DF5 File Offset: 0x00003FF5
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

		// Token: 0x060003E9 RID: 1001 RVA: 0x00005E08 File Offset: 0x00004008
		public void Set(ref UserInfoData other)
		{
			this.m_ApiVersion = 3;
			this.UserId = other.UserId;
			this.Country = other.Country;
			this.DisplayName = other.DisplayName;
			this.PreferredLanguage = other.PreferredLanguage;
			this.Nickname = other.Nickname;
			this.DisplayNameSanitized = other.DisplayNameSanitized;
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00005E6C File Offset: 0x0000406C
		public void Set(ref UserInfoData? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 3;
				this.UserId = other.Value.UserId;
				this.Country = other.Value.Country;
				this.DisplayName = other.Value.DisplayName;
				this.PreferredLanguage = other.Value.PreferredLanguage;
				this.Nickname = other.Value.Nickname;
				this.DisplayNameSanitized = other.Value.DisplayNameSanitized;
			}
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00005F10 File Offset: 0x00004110
		public void Dispose()
		{
			Helper.Dispose(ref this.m_UserId);
			Helper.Dispose(ref this.m_Country);
			Helper.Dispose(ref this.m_DisplayName);
			Helper.Dispose(ref this.m_PreferredLanguage);
			Helper.Dispose(ref this.m_Nickname);
			Helper.Dispose(ref this.m_DisplayNameSanitized);
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00005F66 File Offset: 0x00004166
		public void Get(out UserInfoData output)
		{
			output = default(UserInfoData);
			output.Set(ref this);
		}

		// Token: 0x0400019A RID: 410
		private int m_ApiVersion;

		// Token: 0x0400019B RID: 411
		private IntPtr m_UserId;

		// Token: 0x0400019C RID: 412
		private IntPtr m_Country;

		// Token: 0x0400019D RID: 413
		private IntPtr m_DisplayName;

		// Token: 0x0400019E RID: 414
		private IntPtr m_PreferredLanguage;

		// Token: 0x0400019F RID: 415
		private IntPtr m_Nickname;

		// Token: 0x040001A0 RID: 416
		private IntPtr m_DisplayNameSanitized;
	}
}
