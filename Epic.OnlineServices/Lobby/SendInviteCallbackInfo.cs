using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003EB RID: 1003
	public struct SendInviteCallbackInfo : ICallbackInfo
	{
		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x060019FC RID: 6652 RVA: 0x00026728 File Offset: 0x00024928
		// (set) Token: 0x060019FD RID: 6653 RVA: 0x00026730 File Offset: 0x00024930
		public Result ResultCode { get; set; }

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x060019FE RID: 6654 RVA: 0x00026739 File Offset: 0x00024939
		// (set) Token: 0x060019FF RID: 6655 RVA: 0x00026741 File Offset: 0x00024941
		public object ClientData { get; set; }

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06001A00 RID: 6656 RVA: 0x0002674A File Offset: 0x0002494A
		// (set) Token: 0x06001A01 RID: 6657 RVA: 0x00026752 File Offset: 0x00024952
		public Utf8String LobbyId { get; set; }

		// Token: 0x06001A02 RID: 6658 RVA: 0x0002675C File Offset: 0x0002495C
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001A03 RID: 6659 RVA: 0x00026779 File Offset: 0x00024979
		internal void Set(ref SendInviteCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LobbyId = other.LobbyId;
		}
	}
}
