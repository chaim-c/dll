using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000E7 RID: 231
	public struct GetInviteIdByIndexOptions
	{
		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600078E RID: 1934 RVA: 0x0000B6A9 File Offset: 0x000098A9
		// (set) Token: 0x0600078F RID: 1935 RVA: 0x0000B6B1 File Offset: 0x000098B1
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000790 RID: 1936 RVA: 0x0000B6BA File Offset: 0x000098BA
		// (set) Token: 0x06000791 RID: 1937 RVA: 0x0000B6C2 File Offset: 0x000098C2
		public uint Index { get; set; }
	}
}
