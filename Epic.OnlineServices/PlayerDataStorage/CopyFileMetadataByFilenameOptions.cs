using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000261 RID: 609
	public struct CopyFileMetadataByFilenameOptions
	{
		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x060010A5 RID: 4261 RVA: 0x00018AFE File Offset: 0x00016CFE
		// (set) Token: 0x060010A6 RID: 4262 RVA: 0x00018B06 File Offset: 0x00016D06
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x060010A7 RID: 4263 RVA: 0x00018B0F File Offset: 0x00016D0F
		// (set) Token: 0x060010A8 RID: 4264 RVA: 0x00018B17 File Offset: 0x00016D17
		public Utf8String Filename { get; set; }
	}
}
