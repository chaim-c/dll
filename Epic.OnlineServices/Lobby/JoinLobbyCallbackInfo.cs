using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000358 RID: 856
	public struct JoinLobbyCallbackInfo : ICallbackInfo
	{
		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x060016A3 RID: 5795 RVA: 0x000219E8 File Offset: 0x0001FBE8
		// (set) Token: 0x060016A4 RID: 5796 RVA: 0x000219F0 File Offset: 0x0001FBF0
		public Result ResultCode { get; set; }

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x060016A5 RID: 5797 RVA: 0x000219F9 File Offset: 0x0001FBF9
		// (set) Token: 0x060016A6 RID: 5798 RVA: 0x00021A01 File Offset: 0x0001FC01
		public object ClientData { get; set; }

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x060016A7 RID: 5799 RVA: 0x00021A0A File Offset: 0x0001FC0A
		// (set) Token: 0x060016A8 RID: 5800 RVA: 0x00021A12 File Offset: 0x0001FC12
		public Utf8String LobbyId { get; set; }

		// Token: 0x060016A9 RID: 5801 RVA: 0x00021A1C File Offset: 0x0001FC1C
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x00021A39 File Offset: 0x0001FC39
		internal void Set(ref JoinLobbyCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LobbyId = other.LobbyId;
		}
	}
}
