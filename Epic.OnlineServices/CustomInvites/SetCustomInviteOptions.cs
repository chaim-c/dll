using System;

namespace Epic.OnlineServices.CustomInvites
{
	// Token: 0x0200050B RID: 1291
	public struct SetCustomInviteOptions
	{
		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x06002138 RID: 8504 RVA: 0x00031576 File Offset: 0x0002F776
		// (set) Token: 0x06002139 RID: 8505 RVA: 0x0003157E File Offset: 0x0002F77E
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x0600213A RID: 8506 RVA: 0x00031587 File Offset: 0x0002F787
		// (set) Token: 0x0600213B RID: 8507 RVA: 0x0003158F File Offset: 0x0002F78F
		public Utf8String Payload { get; set; }
	}
}
