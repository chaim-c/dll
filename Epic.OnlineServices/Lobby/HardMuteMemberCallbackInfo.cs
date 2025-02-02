using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200034C RID: 844
	public struct HardMuteMemberCallbackInfo : ICallbackInfo
	{
		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x0600163F RID: 5695 RVA: 0x0002106A File Offset: 0x0001F26A
		// (set) Token: 0x06001640 RID: 5696 RVA: 0x00021072 File Offset: 0x0001F272
		public Result ResultCode { get; set; }

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06001641 RID: 5697 RVA: 0x0002107B File Offset: 0x0001F27B
		// (set) Token: 0x06001642 RID: 5698 RVA: 0x00021083 File Offset: 0x0001F283
		public object ClientData { get; set; }

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06001643 RID: 5699 RVA: 0x0002108C File Offset: 0x0001F28C
		// (set) Token: 0x06001644 RID: 5700 RVA: 0x00021094 File Offset: 0x0001F294
		public Utf8String LobbyId { get; set; }

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06001645 RID: 5701 RVA: 0x0002109D File Offset: 0x0001F29D
		// (set) Token: 0x06001646 RID: 5702 RVA: 0x000210A5 File Offset: 0x0001F2A5
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x06001647 RID: 5703 RVA: 0x000210B0 File Offset: 0x0001F2B0
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001648 RID: 5704 RVA: 0x000210CD File Offset: 0x0001F2CD
		internal void Set(ref HardMuteMemberCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LobbyId = other.LobbyId;
			this.TargetUserId = other.TargetUserId;
		}
	}
}
