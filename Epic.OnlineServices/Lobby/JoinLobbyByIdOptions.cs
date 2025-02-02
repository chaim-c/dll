using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000356 RID: 854
	public struct JoinLobbyByIdOptions
	{
		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06001694 RID: 5780 RVA: 0x00021886 File Offset: 0x0001FA86
		// (set) Token: 0x06001695 RID: 5781 RVA: 0x0002188E File Offset: 0x0001FA8E
		public Utf8String LobbyId { get; set; }

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06001696 RID: 5782 RVA: 0x00021897 File Offset: 0x0001FA97
		// (set) Token: 0x06001697 RID: 5783 RVA: 0x0002189F File Offset: 0x0001FA9F
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06001698 RID: 5784 RVA: 0x000218A8 File Offset: 0x0001FAA8
		// (set) Token: 0x06001699 RID: 5785 RVA: 0x000218B0 File Offset: 0x0001FAB0
		public bool PresenceEnabled { get; set; }

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x0600169A RID: 5786 RVA: 0x000218B9 File Offset: 0x0001FAB9
		// (set) Token: 0x0600169B RID: 5787 RVA: 0x000218C1 File Offset: 0x0001FAC1
		public LocalRTCOptions? LocalRTCOptions { get; set; }
	}
}
