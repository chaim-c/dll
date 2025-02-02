using System;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000236 RID: 566
	public struct GetJoinInfoOptions
	{
		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000F97 RID: 3991 RVA: 0x000170E8 File Offset: 0x000152E8
		// (set) Token: 0x06000F98 RID: 3992 RVA: 0x000170F0 File Offset: 0x000152F0
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000F99 RID: 3993 RVA: 0x000170F9 File Offset: 0x000152F9
		// (set) Token: 0x06000F9A RID: 3994 RVA: 0x00017101 File Offset: 0x00015301
		public EpicAccountId TargetUserId { get; set; }
	}
}
