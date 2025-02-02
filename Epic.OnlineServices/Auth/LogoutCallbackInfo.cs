using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x0200058D RID: 1421
	public struct LogoutCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x0600247A RID: 9338 RVA: 0x000365DB File Offset: 0x000347DB
		// (set) Token: 0x0600247B RID: 9339 RVA: 0x000365E3 File Offset: 0x000347E3
		public Result ResultCode { get; set; }

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x0600247C RID: 9340 RVA: 0x000365EC File Offset: 0x000347EC
		// (set) Token: 0x0600247D RID: 9341 RVA: 0x000365F4 File Offset: 0x000347F4
		public object ClientData { get; set; }

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x0600247E RID: 9342 RVA: 0x000365FD File Offset: 0x000347FD
		// (set) Token: 0x0600247F RID: 9343 RVA: 0x00036605 File Offset: 0x00034805
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x06002480 RID: 9344 RVA: 0x00036610 File Offset: 0x00034810
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06002481 RID: 9345 RVA: 0x0003662D File Offset: 0x0003482D
		internal void Set(ref LogoutCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
		}
	}
}
