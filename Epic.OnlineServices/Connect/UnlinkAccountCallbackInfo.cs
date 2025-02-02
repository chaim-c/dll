using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000564 RID: 1380
	public struct UnlinkAccountCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x06002345 RID: 9029 RVA: 0x00034307 File Offset: 0x00032507
		// (set) Token: 0x06002346 RID: 9030 RVA: 0x0003430F File Offset: 0x0003250F
		public Result ResultCode { get; set; }

		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x06002347 RID: 9031 RVA: 0x00034318 File Offset: 0x00032518
		// (set) Token: 0x06002348 RID: 9032 RVA: 0x00034320 File Offset: 0x00032520
		public object ClientData { get; set; }

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x06002349 RID: 9033 RVA: 0x00034329 File Offset: 0x00032529
		// (set) Token: 0x0600234A RID: 9034 RVA: 0x00034331 File Offset: 0x00032531
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x0600234B RID: 9035 RVA: 0x0003433C File Offset: 0x0003253C
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x0600234C RID: 9036 RVA: 0x00034359 File Offset: 0x00032559
		internal void Set(ref UnlinkAccountCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
		}
	}
}
