using System;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000040 RID: 64
	public struct QueryUserInfoOptions
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x00005B3F File Offset: 0x00003D3F
		// (set) Token: 0x060003C8 RID: 968 RVA: 0x00005B47 File Offset: 0x00003D47
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x00005B50 File Offset: 0x00003D50
		// (set) Token: 0x060003CA RID: 970 RVA: 0x00005B58 File Offset: 0x00003D58
		public EpicAccountId TargetUserId { get; set; }
	}
}
