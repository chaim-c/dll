using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000D7 RID: 215
	public struct CreateSessionModificationOptions
	{
		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600073A RID: 1850 RVA: 0x0000AF59 File Offset: 0x00009159
		// (set) Token: 0x0600073B RID: 1851 RVA: 0x0000AF61 File Offset: 0x00009161
		public Utf8String SessionName { get; set; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600073C RID: 1852 RVA: 0x0000AF6A File Offset: 0x0000916A
		// (set) Token: 0x0600073D RID: 1853 RVA: 0x0000AF72 File Offset: 0x00009172
		public Utf8String BucketId { get; set; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600073E RID: 1854 RVA: 0x0000AF7B File Offset: 0x0000917B
		// (set) Token: 0x0600073F RID: 1855 RVA: 0x0000AF83 File Offset: 0x00009183
		public uint MaxPlayers { get; set; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000740 RID: 1856 RVA: 0x0000AF8C File Offset: 0x0000918C
		// (set) Token: 0x06000741 RID: 1857 RVA: 0x0000AF94 File Offset: 0x00009194
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000742 RID: 1858 RVA: 0x0000AF9D File Offset: 0x0000919D
		// (set) Token: 0x06000743 RID: 1859 RVA: 0x0000AFA5 File Offset: 0x000091A5
		public bool PresenceEnabled { get; set; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x0000AFAE File Offset: 0x000091AE
		// (set) Token: 0x06000745 RID: 1861 RVA: 0x0000AFB6 File Offset: 0x000091B6
		public Utf8String SessionId { get; set; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000746 RID: 1862 RVA: 0x0000AFBF File Offset: 0x000091BF
		// (set) Token: 0x06000747 RID: 1863 RVA: 0x0000AFC7 File Offset: 0x000091C7
		public bool SanctionsEnabled { get; set; }
	}
}
