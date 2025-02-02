using System;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000024 RID: 36
	public struct CopyExternalUserInfoByAccountIdOptions
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060002FE RID: 766 RVA: 0x000049E9 File Offset: 0x00002BE9
		// (set) Token: 0x060002FF RID: 767 RVA: 0x000049F1 File Offset: 0x00002BF1
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000300 RID: 768 RVA: 0x000049FA File Offset: 0x00002BFA
		// (set) Token: 0x06000301 RID: 769 RVA: 0x00004A02 File Offset: 0x00002C02
		public EpicAccountId TargetUserId { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000302 RID: 770 RVA: 0x00004A0B File Offset: 0x00002C0B
		// (set) Token: 0x06000303 RID: 771 RVA: 0x00004A13 File Offset: 0x00002C13
		public Utf8String AccountId { get; set; }
	}
}
