using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000498 RID: 1176
	public struct CopyItemImageInfoByIndexOptions
	{
		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x06001E71 RID: 7793 RVA: 0x0002D14E File Offset: 0x0002B34E
		// (set) Token: 0x06001E72 RID: 7794 RVA: 0x0002D156 File Offset: 0x0002B356
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06001E73 RID: 7795 RVA: 0x0002D15F File Offset: 0x0002B35F
		// (set) Token: 0x06001E74 RID: 7796 RVA: 0x0002D167 File Offset: 0x0002B367
		public Utf8String ItemId { get; set; }

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x06001E75 RID: 7797 RVA: 0x0002D170 File Offset: 0x0002B370
		// (set) Token: 0x06001E76 RID: 7798 RVA: 0x0002D178 File Offset: 0x0002B378
		public uint ImageInfoIndex { get; set; }
	}
}
