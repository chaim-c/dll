using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000381 RID: 897
	public struct LobbyInviteRejectedCallbackInfo : ICallbackInfo
	{
		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x060017E5 RID: 6117 RVA: 0x000243DF File Offset: 0x000225DF
		// (set) Token: 0x060017E6 RID: 6118 RVA: 0x000243E7 File Offset: 0x000225E7
		public object ClientData { get; set; }

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x060017E7 RID: 6119 RVA: 0x000243F0 File Offset: 0x000225F0
		// (set) Token: 0x060017E8 RID: 6120 RVA: 0x000243F8 File Offset: 0x000225F8
		public Utf8String InviteId { get; set; }

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x060017E9 RID: 6121 RVA: 0x00024401 File Offset: 0x00022601
		// (set) Token: 0x060017EA RID: 6122 RVA: 0x00024409 File Offset: 0x00022609
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x060017EB RID: 6123 RVA: 0x00024412 File Offset: 0x00022612
		// (set) Token: 0x060017EC RID: 6124 RVA: 0x0002441A File Offset: 0x0002261A
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x060017ED RID: 6125 RVA: 0x00024423 File Offset: 0x00022623
		// (set) Token: 0x060017EE RID: 6126 RVA: 0x0002442B File Offset: 0x0002262B
		public Utf8String LobbyId { get; set; }

		// Token: 0x060017EF RID: 6127 RVA: 0x00024434 File Offset: 0x00022634
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x060017F0 RID: 6128 RVA: 0x00024450 File Offset: 0x00022650
		internal void Set(ref LobbyInviteRejectedCallbackInfoInternal other)
		{
			this.ClientData = other.ClientData;
			this.InviteId = other.InviteId;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
			this.LobbyId = other.LobbyId;
		}
	}
}
