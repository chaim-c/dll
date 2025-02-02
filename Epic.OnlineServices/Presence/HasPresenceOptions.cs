using System;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000238 RID: 568
	public struct HasPresenceOptions
	{
		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000FA0 RID: 4000 RVA: 0x000171B6 File Offset: 0x000153B6
		// (set) Token: 0x06000FA1 RID: 4001 RVA: 0x000171BE File Offset: 0x000153BE
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000FA2 RID: 4002 RVA: 0x000171C7 File Offset: 0x000153C7
		// (set) Token: 0x06000FA3 RID: 4003 RVA: 0x000171CF File Offset: 0x000153CF
		public EpicAccountId TargetUserId { get; set; }
	}
}
