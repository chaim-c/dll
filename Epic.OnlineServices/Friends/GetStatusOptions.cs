using System;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x0200046A RID: 1130
	public struct GetStatusOptions
	{
		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06001CE2 RID: 7394 RVA: 0x0002AD15 File Offset: 0x00028F15
		// (set) Token: 0x06001CE3 RID: 7395 RVA: 0x0002AD1D File Offset: 0x00028F1D
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x06001CE4 RID: 7396 RVA: 0x0002AD26 File Offset: 0x00028F26
		// (set) Token: 0x06001CE5 RID: 7397 RVA: 0x0002AD2E File Offset: 0x00028F2E
		public EpicAccountId TargetUserId { get; set; }
	}
}
