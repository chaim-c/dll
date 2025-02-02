using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000538 RID: 1336
	public struct LinkAccountOptions
	{
		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x06002248 RID: 8776 RVA: 0x0003343E File Offset: 0x0003163E
		// (set) Token: 0x06002249 RID: 8777 RVA: 0x00033446 File Offset: 0x00031646
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x0600224A RID: 8778 RVA: 0x0003344F File Offset: 0x0003164F
		// (set) Token: 0x0600224B RID: 8779 RVA: 0x00033457 File Offset: 0x00031657
		public ContinuanceToken ContinuanceToken { get; set; }
	}
}
