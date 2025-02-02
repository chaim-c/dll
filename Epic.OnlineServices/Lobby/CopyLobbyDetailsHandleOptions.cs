using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200033A RID: 826
	public struct CopyLobbyDetailsHandleOptions
	{
		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x060015C2 RID: 5570 RVA: 0x00020535 File Offset: 0x0001E735
		// (set) Token: 0x060015C3 RID: 5571 RVA: 0x0002053D File Offset: 0x0001E73D
		public Utf8String LobbyId { get; set; }

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x060015C4 RID: 5572 RVA: 0x00020546 File Offset: 0x0001E746
		// (set) Token: 0x060015C5 RID: 5573 RVA: 0x0002054E File Offset: 0x0001E74E
		public ProductUserId LocalUserId { get; set; }
	}
}
