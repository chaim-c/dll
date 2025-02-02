using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003F1 RID: 1009
	public struct UpdateLobbyCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06001A3A RID: 6714 RVA: 0x00026D7C File Offset: 0x00024F7C
		// (set) Token: 0x06001A3B RID: 6715 RVA: 0x00026D84 File Offset: 0x00024F84
		public Result ResultCode { get; set; }

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06001A3C RID: 6716 RVA: 0x00026D8D File Offset: 0x00024F8D
		// (set) Token: 0x06001A3D RID: 6717 RVA: 0x00026D95 File Offset: 0x00024F95
		public object ClientData { get; set; }

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06001A3E RID: 6718 RVA: 0x00026D9E File Offset: 0x00024F9E
		// (set) Token: 0x06001A3F RID: 6719 RVA: 0x00026DA6 File Offset: 0x00024FA6
		public Utf8String LobbyId { get; set; }

		// Token: 0x06001A40 RID: 6720 RVA: 0x00026DB0 File Offset: 0x00024FB0
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001A41 RID: 6721 RVA: 0x00026DCD File Offset: 0x00024FCD
		internal void Set(ref UpdateLobbyCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LobbyId = other.LobbyId;
		}
	}
}
