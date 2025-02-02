using System;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x0200002E RID: 46
	public struct GetExternalUserInfoCountOptions
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000340 RID: 832 RVA: 0x00005016 File Offset: 0x00003216
		// (set) Token: 0x06000341 RID: 833 RVA: 0x0000501E File Offset: 0x0000321E
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000342 RID: 834 RVA: 0x00005027 File Offset: 0x00003227
		// (set) Token: 0x06000343 RID: 835 RVA: 0x0000502F File Offset: 0x0000322F
		public EpicAccountId TargetUserId { get; set; }
	}
}
