using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004AE RID: 1198
	public struct GetEntitlementsByNameCountOptions
	{
		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x06001F11 RID: 7953 RVA: 0x0002E6E3 File Offset: 0x0002C8E3
		// (set) Token: 0x06001F12 RID: 7954 RVA: 0x0002E6EB File Offset: 0x0002C8EB
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x06001F13 RID: 7955 RVA: 0x0002E6F4 File Offset: 0x0002C8F4
		// (set) Token: 0x06001F14 RID: 7956 RVA: 0x0002E6FC File Offset: 0x0002C8FC
		public Utf8String EntitlementName { get; set; }
	}
}
