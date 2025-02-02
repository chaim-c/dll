using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000297 RID: 663
	public struct ReadFileOptions
	{
		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06001217 RID: 4631 RVA: 0x0001AB14 File Offset: 0x00018D14
		// (set) Token: 0x06001218 RID: 4632 RVA: 0x0001AB1C File Offset: 0x00018D1C
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06001219 RID: 4633 RVA: 0x0001AB25 File Offset: 0x00018D25
		// (set) Token: 0x0600121A RID: 4634 RVA: 0x0001AB2D File Offset: 0x00018D2D
		public Utf8String Filename { get; set; }

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x0600121B RID: 4635 RVA: 0x0001AB36 File Offset: 0x00018D36
		// (set) Token: 0x0600121C RID: 4636 RVA: 0x0001AB3E File Offset: 0x00018D3E
		public uint ReadChunkLengthBytes { get; set; }

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x0600121D RID: 4637 RVA: 0x0001AB47 File Offset: 0x00018D47
		// (set) Token: 0x0600121E RID: 4638 RVA: 0x0001AB4F File Offset: 0x00018D4F
		public OnReadFileDataCallback ReadFileDataCallback { get; set; }

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x0600121F RID: 4639 RVA: 0x0001AB58 File Offset: 0x00018D58
		// (set) Token: 0x06001220 RID: 4640 RVA: 0x0001AB60 File Offset: 0x00018D60
		public OnFileTransferProgressCallback FileTransferProgressCallback { get; set; }
	}
}
