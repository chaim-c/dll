using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200039D RID: 925
	public struct LobbySearchFindCallbackInfo : ICallbackInfo
	{
		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x0600187D RID: 6269 RVA: 0x0002542D File Offset: 0x0002362D
		// (set) Token: 0x0600187E RID: 6270 RVA: 0x00025435 File Offset: 0x00023635
		public Result ResultCode { get; set; }

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x0600187F RID: 6271 RVA: 0x0002543E File Offset: 0x0002363E
		// (set) Token: 0x06001880 RID: 6272 RVA: 0x00025446 File Offset: 0x00023646
		public object ClientData { get; set; }

		// Token: 0x06001881 RID: 6273 RVA: 0x00025450 File Offset: 0x00023650
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001882 RID: 6274 RVA: 0x0002546D File Offset: 0x0002366D
		internal void Set(ref LobbySearchFindCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
		}
	}
}
