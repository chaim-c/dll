using System;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x02000082 RID: 130
	public struct FileMetadata
	{
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x00007D4D File Offset: 0x00005F4D
		// (set) Token: 0x0600052C RID: 1324 RVA: 0x00007D55 File Offset: 0x00005F55
		public uint FileSizeBytes { get; set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x00007D5E File Offset: 0x00005F5E
		// (set) Token: 0x0600052E RID: 1326 RVA: 0x00007D66 File Offset: 0x00005F66
		public Utf8String MD5Hash { get; set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x00007D6F File Offset: 0x00005F6F
		// (set) Token: 0x06000530 RID: 1328 RVA: 0x00007D77 File Offset: 0x00005F77
		public Utf8String Filename { get; set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x00007D80 File Offset: 0x00005F80
		// (set) Token: 0x06000532 RID: 1330 RVA: 0x00007D88 File Offset: 0x00005F88
		public uint UnencryptedDataSizeBytes { get; set; }

		// Token: 0x06000533 RID: 1331 RVA: 0x00007D91 File Offset: 0x00005F91
		internal void Set(ref FileMetadataInternal other)
		{
			this.FileSizeBytes = other.FileSizeBytes;
			this.MD5Hash = other.MD5Hash;
			this.Filename = other.Filename;
			this.UnencryptedDataSizeBytes = other.UnencryptedDataSizeBytes;
		}
	}
}
