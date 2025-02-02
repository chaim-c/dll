using System;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000256 RID: 598
	public struct QueryPresenceOptions
	{
		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x0600106F RID: 4207 RVA: 0x000186D7 File Offset: 0x000168D7
		// (set) Token: 0x06001070 RID: 4208 RVA: 0x000186DF File Offset: 0x000168DF
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06001071 RID: 4209 RVA: 0x000186E8 File Offset: 0x000168E8
		// (set) Token: 0x06001072 RID: 4210 RVA: 0x000186F0 File Offset: 0x000168F0
		public EpicAccountId TargetUserId { get; set; }
	}
}
