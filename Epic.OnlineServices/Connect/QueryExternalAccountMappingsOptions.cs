using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x0200055A RID: 1370
	public struct QueryExternalAccountMappingsOptions
	{
		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x060022FB RID: 8955 RVA: 0x00033C2A File Offset: 0x00031E2A
		// (set) Token: 0x060022FC RID: 8956 RVA: 0x00033C32 File Offset: 0x00031E32
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x060022FD RID: 8957 RVA: 0x00033C3B File Offset: 0x00031E3B
		// (set) Token: 0x060022FE RID: 8958 RVA: 0x00033C43 File Offset: 0x00031E43
		public ExternalAccountType AccountIdType { get; set; }

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x060022FF RID: 8959 RVA: 0x00033C4C File Offset: 0x00031E4C
		// (set) Token: 0x06002300 RID: 8960 RVA: 0x00033C54 File Offset: 0x00031E54
		public Utf8String[] ExternalAccountIds { get; set; }
	}
}
