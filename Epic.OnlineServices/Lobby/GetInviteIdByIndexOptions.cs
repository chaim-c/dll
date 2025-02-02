using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000348 RID: 840
	public struct GetInviteIdByIndexOptions
	{
		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x0600162D RID: 5677 RVA: 0x00020EE1 File Offset: 0x0001F0E1
		// (set) Token: 0x0600162E RID: 5678 RVA: 0x00020EE9 File Offset: 0x0001F0E9
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x0600162F RID: 5679 RVA: 0x00020EF2 File Offset: 0x0001F0F2
		// (set) Token: 0x06001630 RID: 5680 RVA: 0x00020EFA File Offset: 0x0001F0FA
		public uint Index { get; set; }
	}
}
