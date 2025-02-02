using System;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000258 RID: 600
	public struct SetPresenceCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06001078 RID: 4216 RVA: 0x000187A6 File Offset: 0x000169A6
		// (set) Token: 0x06001079 RID: 4217 RVA: 0x000187AE File Offset: 0x000169AE
		public Result ResultCode { get; set; }

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x0600107A RID: 4218 RVA: 0x000187B7 File Offset: 0x000169B7
		// (set) Token: 0x0600107B RID: 4219 RVA: 0x000187BF File Offset: 0x000169BF
		public object ClientData { get; set; }

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x0600107C RID: 4220 RVA: 0x000187C8 File Offset: 0x000169C8
		// (set) Token: 0x0600107D RID: 4221 RVA: 0x000187D0 File Offset: 0x000169D0
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x0600107E RID: 4222 RVA: 0x000187DC File Offset: 0x000169DC
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x000187F9 File Offset: 0x000169F9
		internal void Set(ref SetPresenceCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
		}
	}
}
