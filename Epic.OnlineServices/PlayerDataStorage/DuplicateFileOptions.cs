using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x0200026D RID: 621
	public struct DuplicateFileOptions
	{
		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x060010F6 RID: 4342 RVA: 0x00019282 File Offset: 0x00017482
		// (set) Token: 0x060010F7 RID: 4343 RVA: 0x0001928A File Offset: 0x0001748A
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x060010F8 RID: 4344 RVA: 0x00019293 File Offset: 0x00017493
		// (set) Token: 0x060010F9 RID: 4345 RVA: 0x0001929B File Offset: 0x0001749B
		public Utf8String SourceFilename { get; set; }

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x060010FA RID: 4346 RVA: 0x000192A4 File Offset: 0x000174A4
		// (set) Token: 0x060010FB RID: 4347 RVA: 0x000192AC File Offset: 0x000174AC
		public Utf8String DestinationFilename { get; set; }
	}
}
