using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x0200026F RID: 623
	public struct FileMetadata
	{
		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06001102 RID: 4354 RVA: 0x0001939F File Offset: 0x0001759F
		// (set) Token: 0x06001103 RID: 4355 RVA: 0x000193A7 File Offset: 0x000175A7
		public uint FileSizeBytes { get; set; }

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06001104 RID: 4356 RVA: 0x000193B0 File Offset: 0x000175B0
		// (set) Token: 0x06001105 RID: 4357 RVA: 0x000193B8 File Offset: 0x000175B8
		public Utf8String MD5Hash { get; set; }

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06001106 RID: 4358 RVA: 0x000193C1 File Offset: 0x000175C1
		// (set) Token: 0x06001107 RID: 4359 RVA: 0x000193C9 File Offset: 0x000175C9
		public Utf8String Filename { get; set; }

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06001108 RID: 4360 RVA: 0x000193D2 File Offset: 0x000175D2
		// (set) Token: 0x06001109 RID: 4361 RVA: 0x000193DA File Offset: 0x000175DA
		public DateTimeOffset? LastModifiedTime { get; set; }

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x0600110A RID: 4362 RVA: 0x000193E3 File Offset: 0x000175E3
		// (set) Token: 0x0600110B RID: 4363 RVA: 0x000193EB File Offset: 0x000175EB
		public uint UnencryptedDataSizeBytes { get; set; }

		// Token: 0x0600110C RID: 4364 RVA: 0x000193F4 File Offset: 0x000175F4
		internal void Set(ref FileMetadataInternal other)
		{
			this.FileSizeBytes = other.FileSizeBytes;
			this.MD5Hash = other.MD5Hash;
			this.Filename = other.Filename;
			this.LastModifiedTime = other.LastModifiedTime;
			this.UnencryptedDataSizeBytes = other.UnencryptedDataSizeBytes;
		}
	}
}
