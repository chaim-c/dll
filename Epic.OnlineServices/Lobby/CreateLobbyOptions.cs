using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200033E RID: 830
	public struct CreateLobbyOptions
	{
		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x060015DE RID: 5598 RVA: 0x000207D2 File Offset: 0x0001E9D2
		// (set) Token: 0x060015DF RID: 5599 RVA: 0x000207DA File Offset: 0x0001E9DA
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x060015E0 RID: 5600 RVA: 0x000207E3 File Offset: 0x0001E9E3
		// (set) Token: 0x060015E1 RID: 5601 RVA: 0x000207EB File Offset: 0x0001E9EB
		public uint MaxLobbyMembers { get; set; }

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x060015E2 RID: 5602 RVA: 0x000207F4 File Offset: 0x0001E9F4
		// (set) Token: 0x060015E3 RID: 5603 RVA: 0x000207FC File Offset: 0x0001E9FC
		public LobbyPermissionLevel PermissionLevel { get; set; }

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x060015E4 RID: 5604 RVA: 0x00020805 File Offset: 0x0001EA05
		// (set) Token: 0x060015E5 RID: 5605 RVA: 0x0002080D File Offset: 0x0001EA0D
		public bool PresenceEnabled { get; set; }

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x060015E6 RID: 5606 RVA: 0x00020816 File Offset: 0x0001EA16
		// (set) Token: 0x060015E7 RID: 5607 RVA: 0x0002081E File Offset: 0x0001EA1E
		public bool AllowInvites { get; set; }

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x060015E8 RID: 5608 RVA: 0x00020827 File Offset: 0x0001EA27
		// (set) Token: 0x060015E9 RID: 5609 RVA: 0x0002082F File Offset: 0x0001EA2F
		public Utf8String BucketId { get; set; }

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x060015EA RID: 5610 RVA: 0x00020838 File Offset: 0x0001EA38
		// (set) Token: 0x060015EB RID: 5611 RVA: 0x00020840 File Offset: 0x0001EA40
		public bool DisableHostMigration { get; set; }

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x060015EC RID: 5612 RVA: 0x00020849 File Offset: 0x0001EA49
		// (set) Token: 0x060015ED RID: 5613 RVA: 0x00020851 File Offset: 0x0001EA51
		public bool EnableRTCRoom { get; set; }

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x060015EE RID: 5614 RVA: 0x0002085A File Offset: 0x0001EA5A
		// (set) Token: 0x060015EF RID: 5615 RVA: 0x00020862 File Offset: 0x0001EA62
		public LocalRTCOptions? LocalRTCOptions { get; set; }

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x060015F0 RID: 5616 RVA: 0x0002086B File Offset: 0x0001EA6B
		// (set) Token: 0x060015F1 RID: 5617 RVA: 0x00020873 File Offset: 0x0001EA73
		public Utf8String LobbyId { get; set; }

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x060015F2 RID: 5618 RVA: 0x0002087C File Offset: 0x0001EA7C
		// (set) Token: 0x060015F3 RID: 5619 RVA: 0x00020884 File Offset: 0x0001EA84
		public bool EnableJoinById { get; set; }

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x060015F4 RID: 5620 RVA: 0x0002088D File Offset: 0x0001EA8D
		// (set) Token: 0x060015F5 RID: 5621 RVA: 0x00020895 File Offset: 0x0001EA95
		public bool RejoinAfterKickRequiresInvite { get; set; }
	}
}
