using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x0200029E RID: 670
	public struct WriteFileOptions
	{
		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06001257 RID: 4695 RVA: 0x0001B1FF File Offset: 0x000193FF
		// (set) Token: 0x06001258 RID: 4696 RVA: 0x0001B207 File Offset: 0x00019407
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06001259 RID: 4697 RVA: 0x0001B210 File Offset: 0x00019410
		// (set) Token: 0x0600125A RID: 4698 RVA: 0x0001B218 File Offset: 0x00019418
		public Utf8String Filename { get; set; }

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x0600125B RID: 4699 RVA: 0x0001B221 File Offset: 0x00019421
		// (set) Token: 0x0600125C RID: 4700 RVA: 0x0001B229 File Offset: 0x00019429
		public uint ChunkLengthBytes { get; set; }

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x0600125D RID: 4701 RVA: 0x0001B232 File Offset: 0x00019432
		// (set) Token: 0x0600125E RID: 4702 RVA: 0x0001B23A File Offset: 0x0001943A
		public OnWriteFileDataCallback WriteFileDataCallback { get; set; }

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x0600125F RID: 4703 RVA: 0x0001B243 File Offset: 0x00019443
		// (set) Token: 0x06001260 RID: 4704 RVA: 0x0001B24B File Offset: 0x0001944B
		public OnFileTransferProgressCallback FileTransferProgressCallback { get; set; }
	}
}
