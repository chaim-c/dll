using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200037D RID: 893
	public struct LobbyInviteAcceptedCallbackInfo : ICallbackInfo
	{
		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x060017B3 RID: 6067 RVA: 0x00023E76 File Offset: 0x00022076
		// (set) Token: 0x060017B4 RID: 6068 RVA: 0x00023E7E File Offset: 0x0002207E
		public object ClientData { get; set; }

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x060017B5 RID: 6069 RVA: 0x00023E87 File Offset: 0x00022087
		// (set) Token: 0x060017B6 RID: 6070 RVA: 0x00023E8F File Offset: 0x0002208F
		public Utf8String InviteId { get; set; }

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x060017B7 RID: 6071 RVA: 0x00023E98 File Offset: 0x00022098
		// (set) Token: 0x060017B8 RID: 6072 RVA: 0x00023EA0 File Offset: 0x000220A0
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x060017B9 RID: 6073 RVA: 0x00023EA9 File Offset: 0x000220A9
		// (set) Token: 0x060017BA RID: 6074 RVA: 0x00023EB1 File Offset: 0x000220B1
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x060017BB RID: 6075 RVA: 0x00023EBA File Offset: 0x000220BA
		// (set) Token: 0x060017BC RID: 6076 RVA: 0x00023EC2 File Offset: 0x000220C2
		public Utf8String LobbyId { get; set; }

		// Token: 0x060017BD RID: 6077 RVA: 0x00023ECC File Offset: 0x000220CC
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x060017BE RID: 6078 RVA: 0x00023EE8 File Offset: 0x000220E8
		internal void Set(ref LobbyInviteAcceptedCallbackInfoInternal other)
		{
			this.ClientData = other.ClientData;
			this.InviteId = other.InviteId;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
			this.LobbyId = other.LobbyId;
		}
	}
}
