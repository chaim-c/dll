using System;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000042 RID: 66
	public struct UserInfoData
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x00005C0E File Offset: 0x00003E0E
		// (set) Token: 0x060003D1 RID: 977 RVA: 0x00005C16 File Offset: 0x00003E16
		public EpicAccountId UserId { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x00005C1F File Offset: 0x00003E1F
		// (set) Token: 0x060003D3 RID: 979 RVA: 0x00005C27 File Offset: 0x00003E27
		public Utf8String Country { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x00005C30 File Offset: 0x00003E30
		// (set) Token: 0x060003D5 RID: 981 RVA: 0x00005C38 File Offset: 0x00003E38
		public Utf8String DisplayName { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x00005C41 File Offset: 0x00003E41
		// (set) Token: 0x060003D7 RID: 983 RVA: 0x00005C49 File Offset: 0x00003E49
		public Utf8String PreferredLanguage { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x00005C52 File Offset: 0x00003E52
		// (set) Token: 0x060003D9 RID: 985 RVA: 0x00005C5A File Offset: 0x00003E5A
		public Utf8String Nickname { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060003DA RID: 986 RVA: 0x00005C63 File Offset: 0x00003E63
		// (set) Token: 0x060003DB RID: 987 RVA: 0x00005C6B File Offset: 0x00003E6B
		public Utf8String DisplayNameSanitized { get; set; }

		// Token: 0x060003DC RID: 988 RVA: 0x00005C74 File Offset: 0x00003E74
		internal void Set(ref UserInfoDataInternal other)
		{
			this.UserId = other.UserId;
			this.Country = other.Country;
			this.DisplayName = other.DisplayName;
			this.PreferredLanguage = other.PreferredLanguage;
			this.Nickname = other.Nickname;
			this.DisplayNameSanitized = other.DisplayNameSanitized;
		}
	}
}
