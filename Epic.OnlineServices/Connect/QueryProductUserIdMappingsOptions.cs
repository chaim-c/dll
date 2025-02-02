using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x0200055E RID: 1374
	public struct QueryProductUserIdMappingsOptions
	{
		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x0600231A RID: 8986 RVA: 0x00033F0A File Offset: 0x0003210A
		// (set) Token: 0x0600231B RID: 8987 RVA: 0x00033F12 File Offset: 0x00032112
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x0600231C RID: 8988 RVA: 0x00033F1B File Offset: 0x0003211B
		// (set) Token: 0x0600231D RID: 8989 RVA: 0x00033F23 File Offset: 0x00032123
		public ExternalAccountType AccountIdType_DEPRECATED { get; set; }

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x0600231E RID: 8990 RVA: 0x00033F2C File Offset: 0x0003212C
		// (set) Token: 0x0600231F RID: 8991 RVA: 0x00033F34 File Offset: 0x00032134
		public ProductUserId[] ProductUserIds { get; set; }
	}
}
