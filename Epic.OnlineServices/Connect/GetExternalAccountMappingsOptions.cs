using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x0200052E RID: 1326
	public struct GetExternalAccountMappingsOptions
	{
		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x0600220A RID: 8714 RVA: 0x00032E97 File Offset: 0x00031097
		// (set) Token: 0x0600220B RID: 8715 RVA: 0x00032E9F File Offset: 0x0003109F
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x0600220C RID: 8716 RVA: 0x00032EA8 File Offset: 0x000310A8
		// (set) Token: 0x0600220D RID: 8717 RVA: 0x00032EB0 File Offset: 0x000310B0
		public ExternalAccountType AccountIdType { get; set; }

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x0600220E RID: 8718 RVA: 0x00032EB9 File Offset: 0x000310B9
		// (set) Token: 0x0600220F RID: 8719 RVA: 0x00032EC1 File Offset: 0x000310C1
		public Utf8String TargetExternalUserId { get; set; }
	}
}
