using System;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x02000460 RID: 1120
	public struct AcceptInviteOptions
	{
		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06001CB7 RID: 7351 RVA: 0x0002A74F File Offset: 0x0002894F
		// (set) Token: 0x06001CB8 RID: 7352 RVA: 0x0002A757 File Offset: 0x00028957
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06001CB9 RID: 7353 RVA: 0x0002A760 File Offset: 0x00028960
		// (set) Token: 0x06001CBA RID: 7354 RVA: 0x0002A768 File Offset: 0x00028968
		public EpicAccountId TargetUserId { get; set; }
	}
}
