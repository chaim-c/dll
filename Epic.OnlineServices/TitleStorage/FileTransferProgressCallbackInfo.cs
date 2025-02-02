using System;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x02000084 RID: 132
	public struct FileTransferProgressCallbackInfo : ICallbackInfo
	{
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000540 RID: 1344 RVA: 0x00007F56 File Offset: 0x00006156
		// (set) Token: 0x06000541 RID: 1345 RVA: 0x00007F5E File Offset: 0x0000615E
		public object ClientData { get; set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x00007F67 File Offset: 0x00006167
		// (set) Token: 0x06000543 RID: 1347 RVA: 0x00007F6F File Offset: 0x0000616F
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x00007F78 File Offset: 0x00006178
		// (set) Token: 0x06000545 RID: 1349 RVA: 0x00007F80 File Offset: 0x00006180
		public Utf8String Filename { get; set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x00007F89 File Offset: 0x00006189
		// (set) Token: 0x06000547 RID: 1351 RVA: 0x00007F91 File Offset: 0x00006191
		public uint BytesTransferred { get; set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x00007F9A File Offset: 0x0000619A
		// (set) Token: 0x06000549 RID: 1353 RVA: 0x00007FA2 File Offset: 0x000061A2
		public uint TotalFileSizeBytes { get; set; }

		// Token: 0x0600054A RID: 1354 RVA: 0x00007FAC File Offset: 0x000061AC
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00007FC8 File Offset: 0x000061C8
		internal void Set(ref FileTransferProgressCallbackInfoInternal other)
		{
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.Filename = other.Filename;
			this.BytesTransferred = other.BytesTransferred;
			this.TotalFileSizeBytes = other.TotalFileSizeBytes;
		}
	}
}
