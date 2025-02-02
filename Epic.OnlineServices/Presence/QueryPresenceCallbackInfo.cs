using System;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000254 RID: 596
	public struct QueryPresenceCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06001058 RID: 4184 RVA: 0x00018489 File Offset: 0x00016689
		// (set) Token: 0x06001059 RID: 4185 RVA: 0x00018491 File Offset: 0x00016691
		public Result ResultCode { get; set; }

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x0600105A RID: 4186 RVA: 0x0001849A File Offset: 0x0001669A
		// (set) Token: 0x0600105B RID: 4187 RVA: 0x000184A2 File Offset: 0x000166A2
		public object ClientData { get; set; }

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x0600105C RID: 4188 RVA: 0x000184AB File Offset: 0x000166AB
		// (set) Token: 0x0600105D RID: 4189 RVA: 0x000184B3 File Offset: 0x000166B3
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x0600105E RID: 4190 RVA: 0x000184BC File Offset: 0x000166BC
		// (set) Token: 0x0600105F RID: 4191 RVA: 0x000184C4 File Offset: 0x000166C4
		public EpicAccountId TargetUserId { get; set; }

		// Token: 0x06001060 RID: 4192 RVA: 0x000184D0 File Offset: 0x000166D0
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x000184ED File Offset: 0x000166ED
		internal void Set(ref QueryPresenceCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
		}
	}
}
