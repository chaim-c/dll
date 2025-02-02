using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003F3 RID: 1011
	public struct UpdateLobbyModificationOptions
	{
		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06001A4D RID: 6733 RVA: 0x00026F4A File Offset: 0x0002514A
		// (set) Token: 0x06001A4E RID: 6734 RVA: 0x00026F52 File Offset: 0x00025152
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06001A4F RID: 6735 RVA: 0x00026F5B File Offset: 0x0002515B
		// (set) Token: 0x06001A50 RID: 6736 RVA: 0x00026F63 File Offset: 0x00025163
		public Utf8String LobbyId { get; set; }
	}
}
