using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003DD RID: 989
	public struct PromoteMemberCallbackInfo : ICallbackInfo
	{
		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x0600198D RID: 6541 RVA: 0x00025C7A File Offset: 0x00023E7A
		// (set) Token: 0x0600198E RID: 6542 RVA: 0x00025C82 File Offset: 0x00023E82
		public Result ResultCode { get; set; }

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x0600198F RID: 6543 RVA: 0x00025C8B File Offset: 0x00023E8B
		// (set) Token: 0x06001990 RID: 6544 RVA: 0x00025C93 File Offset: 0x00023E93
		public object ClientData { get; set; }

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06001991 RID: 6545 RVA: 0x00025C9C File Offset: 0x00023E9C
		// (set) Token: 0x06001992 RID: 6546 RVA: 0x00025CA4 File Offset: 0x00023EA4
		public Utf8String LobbyId { get; set; }

		// Token: 0x06001993 RID: 6547 RVA: 0x00025CB0 File Offset: 0x00023EB0
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x00025CCD File Offset: 0x00023ECD
		internal void Set(ref PromoteMemberCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LobbyId = other.LobbyId;
		}
	}
}
