using System;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x0200025C RID: 604
	public struct SetPresenceOptions
	{
		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06001093 RID: 4243 RVA: 0x00018976 File Offset: 0x00016B76
		// (set) Token: 0x06001094 RID: 4244 RVA: 0x0001897E File Offset: 0x00016B7E
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06001095 RID: 4245 RVA: 0x00018987 File Offset: 0x00016B87
		// (set) Token: 0x06001096 RID: 4246 RVA: 0x0001898F File Offset: 0x00016B8F
		public PresenceModification PresenceModificationHandle { get; set; }
	}
}
