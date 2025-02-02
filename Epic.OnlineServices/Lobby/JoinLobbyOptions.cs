using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200035A RID: 858
	public struct JoinLobbyOptions
	{
		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x060016B6 RID: 5814 RVA: 0x00021BB6 File Offset: 0x0001FDB6
		// (set) Token: 0x060016B7 RID: 5815 RVA: 0x00021BBE File Offset: 0x0001FDBE
		public LobbyDetails LobbyDetailsHandle { get; set; }

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x060016B8 RID: 5816 RVA: 0x00021BC7 File Offset: 0x0001FDC7
		// (set) Token: 0x060016B9 RID: 5817 RVA: 0x00021BCF File Offset: 0x0001FDCF
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x060016BA RID: 5818 RVA: 0x00021BD8 File Offset: 0x0001FDD8
		// (set) Token: 0x060016BB RID: 5819 RVA: 0x00021BE0 File Offset: 0x0001FDE0
		public bool PresenceEnabled { get; set; }

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x060016BC RID: 5820 RVA: 0x00021BE9 File Offset: 0x0001FDE9
		// (set) Token: 0x060016BD RID: 5821 RVA: 0x00021BF1 File Offset: 0x0001FDF1
		public LocalRTCOptions? LocalRTCOptions { get; set; }
	}
}
