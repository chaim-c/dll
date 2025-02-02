using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200011B RID: 283
	public struct SendInviteOptions
	{
		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000899 RID: 2201 RVA: 0x0000C5AD File Offset: 0x0000A7AD
		// (set) Token: 0x0600089A RID: 2202 RVA: 0x0000C5B5 File Offset: 0x0000A7B5
		public Utf8String SessionName { get; set; }

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x0600089B RID: 2203 RVA: 0x0000C5BE File Offset: 0x0000A7BE
		// (set) Token: 0x0600089C RID: 2204 RVA: 0x0000C5C6 File Offset: 0x0000A7C6
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x0600089D RID: 2205 RVA: 0x0000C5CF File Offset: 0x0000A7CF
		// (set) Token: 0x0600089E RID: 2206 RVA: 0x0000C5D7 File Offset: 0x0000A7D7
		public ProductUserId TargetUserId { get; set; }
	}
}
