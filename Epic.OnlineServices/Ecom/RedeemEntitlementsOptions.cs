using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004E9 RID: 1257
	public struct RedeemEntitlementsOptions
	{
		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x06002068 RID: 8296 RVA: 0x000301D7 File Offset: 0x0002E3D7
		// (set) Token: 0x06002069 RID: 8297 RVA: 0x000301DF File Offset: 0x0002E3DF
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x0600206A RID: 8298 RVA: 0x000301E8 File Offset: 0x0002E3E8
		// (set) Token: 0x0600206B RID: 8299 RVA: 0x000301F0 File Offset: 0x0002E3F0
		public Utf8String[] EntitlementIds { get; set; }
	}
}
