using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000492 RID: 1170
	public struct CopyEntitlementByIndexOptions
	{
		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x06001E53 RID: 7763 RVA: 0x0002CEBA File Offset: 0x0002B0BA
		// (set) Token: 0x06001E54 RID: 7764 RVA: 0x0002CEC2 File Offset: 0x0002B0C2
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x06001E55 RID: 7765 RVA: 0x0002CECB File Offset: 0x0002B0CB
		// (set) Token: 0x06001E56 RID: 7766 RVA: 0x0002CED3 File Offset: 0x0002B0D3
		public uint EntitlementIndex { get; set; }
	}
}
