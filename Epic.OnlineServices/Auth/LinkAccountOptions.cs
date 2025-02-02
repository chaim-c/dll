using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x02000584 RID: 1412
	public struct LinkAccountOptions
	{
		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x0600242B RID: 9259 RVA: 0x00035DE0 File Offset: 0x00033FE0
		// (set) Token: 0x0600242C RID: 9260 RVA: 0x00035DE8 File Offset: 0x00033FE8
		public LinkAccountFlags LinkAccountFlags { get; set; }

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x0600242D RID: 9261 RVA: 0x00035DF1 File Offset: 0x00033FF1
		// (set) Token: 0x0600242E RID: 9262 RVA: 0x00035DF9 File Offset: 0x00033FF9
		public ContinuanceToken ContinuanceToken { get; set; }

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x0600242F RID: 9263 RVA: 0x00035E02 File Offset: 0x00034002
		// (set) Token: 0x06002430 RID: 9264 RVA: 0x00035E0A File Offset: 0x0003400A
		public EpicAccountId LocalUserId { get; set; }
	}
}
