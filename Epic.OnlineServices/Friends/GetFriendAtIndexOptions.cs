using System;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x02000466 RID: 1126
	public struct GetFriendAtIndexOptions
	{
		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x06001CD3 RID: 7379 RVA: 0x0002ABDB File Offset: 0x00028DDB
		// (set) Token: 0x06001CD4 RID: 7380 RVA: 0x0002ABE3 File Offset: 0x00028DE3
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06001CD5 RID: 7381 RVA: 0x0002ABEC File Offset: 0x00028DEC
		// (set) Token: 0x06001CD6 RID: 7382 RVA: 0x0002ABF4 File Offset: 0x00028DF4
		public int Index { get; set; }
	}
}
