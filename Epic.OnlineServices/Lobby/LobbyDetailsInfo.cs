using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200037A RID: 890
	public struct LobbyDetailsInfo
	{
		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06001743 RID: 5955 RVA: 0x000229F4 File Offset: 0x00020BF4
		// (set) Token: 0x06001744 RID: 5956 RVA: 0x000229FC File Offset: 0x00020BFC
		public Utf8String LobbyId { get; set; }

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06001745 RID: 5957 RVA: 0x00022A05 File Offset: 0x00020C05
		// (set) Token: 0x06001746 RID: 5958 RVA: 0x00022A0D File Offset: 0x00020C0D
		public ProductUserId LobbyOwnerUserId { get; set; }

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06001747 RID: 5959 RVA: 0x00022A16 File Offset: 0x00020C16
		// (set) Token: 0x06001748 RID: 5960 RVA: 0x00022A1E File Offset: 0x00020C1E
		public LobbyPermissionLevel PermissionLevel { get; set; }

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06001749 RID: 5961 RVA: 0x00022A27 File Offset: 0x00020C27
		// (set) Token: 0x0600174A RID: 5962 RVA: 0x00022A2F File Offset: 0x00020C2F
		public uint AvailableSlots { get; set; }

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x0600174B RID: 5963 RVA: 0x00022A38 File Offset: 0x00020C38
		// (set) Token: 0x0600174C RID: 5964 RVA: 0x00022A40 File Offset: 0x00020C40
		public uint MaxMembers { get; set; }

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x0600174D RID: 5965 RVA: 0x00022A49 File Offset: 0x00020C49
		// (set) Token: 0x0600174E RID: 5966 RVA: 0x00022A51 File Offset: 0x00020C51
		public bool AllowInvites { get; set; }

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x0600174F RID: 5967 RVA: 0x00022A5A File Offset: 0x00020C5A
		// (set) Token: 0x06001750 RID: 5968 RVA: 0x00022A62 File Offset: 0x00020C62
		public Utf8String BucketId { get; set; }

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06001751 RID: 5969 RVA: 0x00022A6B File Offset: 0x00020C6B
		// (set) Token: 0x06001752 RID: 5970 RVA: 0x00022A73 File Offset: 0x00020C73
		public bool AllowHostMigration { get; set; }

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06001753 RID: 5971 RVA: 0x00022A7C File Offset: 0x00020C7C
		// (set) Token: 0x06001754 RID: 5972 RVA: 0x00022A84 File Offset: 0x00020C84
		public bool RTCRoomEnabled { get; set; }

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06001755 RID: 5973 RVA: 0x00022A8D File Offset: 0x00020C8D
		// (set) Token: 0x06001756 RID: 5974 RVA: 0x00022A95 File Offset: 0x00020C95
		public bool AllowJoinById { get; set; }

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06001757 RID: 5975 RVA: 0x00022A9E File Offset: 0x00020C9E
		// (set) Token: 0x06001758 RID: 5976 RVA: 0x00022AA6 File Offset: 0x00020CA6
		public bool RejoinAfterKickRequiresInvite { get; set; }

		// Token: 0x06001759 RID: 5977 RVA: 0x00022AB0 File Offset: 0x00020CB0
		internal void Set(ref LobbyDetailsInfoInternal other)
		{
			this.LobbyId = other.LobbyId;
			this.LobbyOwnerUserId = other.LobbyOwnerUserId;
			this.PermissionLevel = other.PermissionLevel;
			this.AvailableSlots = other.AvailableSlots;
			this.MaxMembers = other.MaxMembers;
			this.AllowInvites = other.AllowInvites;
			this.BucketId = other.BucketId;
			this.AllowHostMigration = other.AllowHostMigration;
			this.RTCRoomEnabled = other.RTCRoomEnabled;
			this.AllowJoinById = other.AllowJoinById;
			this.RejoinAfterKickRequiresInvite = other.RejoinAfterKickRequiresInvite;
		}
	}
}
