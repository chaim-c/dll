using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000518 RID: 1304
	public struct CopyProductUserExternalAccountByAccountTypeOptions
	{
		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x0600218C RID: 8588 RVA: 0x0003231E File Offset: 0x0003051E
		// (set) Token: 0x0600218D RID: 8589 RVA: 0x00032326 File Offset: 0x00030526
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x0600218E RID: 8590 RVA: 0x0003232F File Offset: 0x0003052F
		// (set) Token: 0x0600218F RID: 8591 RVA: 0x00032337 File Offset: 0x00030537
		public ExternalAccountType AccountIdType { get; set; }
	}
}
