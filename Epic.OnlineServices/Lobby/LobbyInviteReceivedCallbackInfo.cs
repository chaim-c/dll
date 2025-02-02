using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200037F RID: 895
	public struct LobbyInviteReceivedCallbackInfo : ICallbackInfo
	{
		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x060017CE RID: 6094 RVA: 0x00024178 File Offset: 0x00022378
		// (set) Token: 0x060017CF RID: 6095 RVA: 0x00024180 File Offset: 0x00022380
		public object ClientData { get; set; }

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x060017D0 RID: 6096 RVA: 0x00024189 File Offset: 0x00022389
		// (set) Token: 0x060017D1 RID: 6097 RVA: 0x00024191 File Offset: 0x00022391
		public Utf8String InviteId { get; set; }

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x060017D2 RID: 6098 RVA: 0x0002419A File Offset: 0x0002239A
		// (set) Token: 0x060017D3 RID: 6099 RVA: 0x000241A2 File Offset: 0x000223A2
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x060017D4 RID: 6100 RVA: 0x000241AB File Offset: 0x000223AB
		// (set) Token: 0x060017D5 RID: 6101 RVA: 0x000241B3 File Offset: 0x000223B3
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x060017D6 RID: 6102 RVA: 0x000241BC File Offset: 0x000223BC
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x060017D7 RID: 6103 RVA: 0x000241D7 File Offset: 0x000223D7
		internal void Set(ref LobbyInviteReceivedCallbackInfoInternal other)
		{
			this.ClientData = other.ClientData;
			this.InviteId = other.InviteId;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
		}
	}
}
