using System;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x0200007C RID: 124
	public struct CopyFileMetadataByFilenameOptions
	{
		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x00007A32 File Offset: 0x00005C32
		// (set) Token: 0x0600050A RID: 1290 RVA: 0x00007A3A File Offset: 0x00005C3A
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x00007A43 File Offset: 0x00005C43
		// (set) Token: 0x0600050C RID: 1292 RVA: 0x00007A4B File Offset: 0x00005C4B
		public Utf8String Filename { get; set; }
	}
}
