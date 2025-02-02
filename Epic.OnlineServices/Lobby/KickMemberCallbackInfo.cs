using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200035C RID: 860
	public struct KickMemberCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x060016C5 RID: 5829 RVA: 0x00021D18 File Offset: 0x0001FF18
		// (set) Token: 0x060016C6 RID: 5830 RVA: 0x00021D20 File Offset: 0x0001FF20
		public Result ResultCode { get; set; }

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x060016C7 RID: 5831 RVA: 0x00021D29 File Offset: 0x0001FF29
		// (set) Token: 0x060016C8 RID: 5832 RVA: 0x00021D31 File Offset: 0x0001FF31
		public object ClientData { get; set; }

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x060016C9 RID: 5833 RVA: 0x00021D3A File Offset: 0x0001FF3A
		// (set) Token: 0x060016CA RID: 5834 RVA: 0x00021D42 File Offset: 0x0001FF42
		public Utf8String LobbyId { get; set; }

		// Token: 0x060016CB RID: 5835 RVA: 0x00021D4C File Offset: 0x0001FF4C
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x060016CC RID: 5836 RVA: 0x00021D69 File Offset: 0x0001FF69
		internal void Set(ref KickMemberCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LobbyId = other.LobbyId;
		}
	}
}
