using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000496 RID: 1174
	public struct CopyItemByIdOptions
	{
		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x06001E68 RID: 7784 RVA: 0x0002D07F File Offset: 0x0002B27F
		// (set) Token: 0x06001E69 RID: 7785 RVA: 0x0002D087 File Offset: 0x0002B287
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x06001E6A RID: 7786 RVA: 0x0002D090 File Offset: 0x0002B290
		// (set) Token: 0x06001E6B RID: 7787 RVA: 0x0002D098 File Offset: 0x0002B298
		public Utf8String ItemId { get; set; }
	}
}
