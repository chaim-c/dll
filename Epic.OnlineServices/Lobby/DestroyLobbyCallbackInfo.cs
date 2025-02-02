using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000342 RID: 834
	public struct DestroyLobbyCallbackInfo : ICallbackInfo
	{
		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x0600160B RID: 5643 RVA: 0x00020BC9 File Offset: 0x0001EDC9
		// (set) Token: 0x0600160C RID: 5644 RVA: 0x00020BD1 File Offset: 0x0001EDD1
		public Result ResultCode { get; set; }

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x0600160D RID: 5645 RVA: 0x00020BDA File Offset: 0x0001EDDA
		// (set) Token: 0x0600160E RID: 5646 RVA: 0x00020BE2 File Offset: 0x0001EDE2
		public object ClientData { get; set; }

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x0600160F RID: 5647 RVA: 0x00020BEB File Offset: 0x0001EDEB
		// (set) Token: 0x06001610 RID: 5648 RVA: 0x00020BF3 File Offset: 0x0001EDF3
		public Utf8String LobbyId { get; set; }

		// Token: 0x06001611 RID: 5649 RVA: 0x00020BFC File Offset: 0x0001EDFC
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001612 RID: 5650 RVA: 0x00020C19 File Offset: 0x0001EE19
		internal void Set(ref DestroyLobbyCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LobbyId = other.LobbyId;
		}
	}
}
