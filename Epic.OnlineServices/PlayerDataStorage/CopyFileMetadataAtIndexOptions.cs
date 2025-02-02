using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x0200025F RID: 607
	public struct CopyFileMetadataAtIndexOptions
	{
		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x0600109C RID: 4252 RVA: 0x00018A42 File Offset: 0x00016C42
		// (set) Token: 0x0600109D RID: 4253 RVA: 0x00018A4A File Offset: 0x00016C4A
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x0600109E RID: 4254 RVA: 0x00018A53 File Offset: 0x00016C53
		// (set) Token: 0x0600109F RID: 4255 RVA: 0x00018A5B File Offset: 0x00016C5B
		public uint Index { get; set; }
	}
}
