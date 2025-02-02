using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003ED RID: 1005
	public struct SendInviteOptions
	{
		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06001A0F RID: 6671 RVA: 0x000268F6 File Offset: 0x00024AF6
		// (set) Token: 0x06001A10 RID: 6672 RVA: 0x000268FE File Offset: 0x00024AFE
		public Utf8String LobbyId { get; set; }

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06001A11 RID: 6673 RVA: 0x00026907 File Offset: 0x00024B07
		// (set) Token: 0x06001A12 RID: 6674 RVA: 0x0002690F File Offset: 0x00024B0F
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x06001A13 RID: 6675 RVA: 0x00026918 File Offset: 0x00024B18
		// (set) Token: 0x06001A14 RID: 6676 RVA: 0x00026920 File Offset: 0x00024B20
		public ProductUserId TargetUserId { get; set; }
	}
}
