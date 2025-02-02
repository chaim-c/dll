using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000354 RID: 852
	public struct JoinLobbyByIdCallbackInfo : ICallbackInfo
	{
		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06001681 RID: 5761 RVA: 0x000216B6 File Offset: 0x0001F8B6
		// (set) Token: 0x06001682 RID: 5762 RVA: 0x000216BE File Offset: 0x0001F8BE
		public Result ResultCode { get; set; }

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06001683 RID: 5763 RVA: 0x000216C7 File Offset: 0x0001F8C7
		// (set) Token: 0x06001684 RID: 5764 RVA: 0x000216CF File Offset: 0x0001F8CF
		public object ClientData { get; set; }

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06001685 RID: 5765 RVA: 0x000216D8 File Offset: 0x0001F8D8
		// (set) Token: 0x06001686 RID: 5766 RVA: 0x000216E0 File Offset: 0x0001F8E0
		public Utf8String LobbyId { get; set; }

		// Token: 0x06001687 RID: 5767 RVA: 0x000216EC File Offset: 0x0001F8EC
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x00021709 File Offset: 0x0001F909
		internal void Set(ref JoinLobbyByIdCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LobbyId = other.LobbyId;
		}
	}
}
