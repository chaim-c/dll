using System;

namespace Epic.OnlineServices.CustomInvites
{
	// Token: 0x02000507 RID: 1287
	public struct SendCustomInviteCallbackInfo : ICallbackInfo
	{
		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x06002118 RID: 8472 RVA: 0x0003124C File Offset: 0x0002F44C
		// (set) Token: 0x06002119 RID: 8473 RVA: 0x00031254 File Offset: 0x0002F454
		public Result ResultCode { get; set; }

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x0600211A RID: 8474 RVA: 0x0003125D File Offset: 0x0002F45D
		// (set) Token: 0x0600211B RID: 8475 RVA: 0x00031265 File Offset: 0x0002F465
		public object ClientData { get; set; }

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x0600211C RID: 8476 RVA: 0x0003126E File Offset: 0x0002F46E
		// (set) Token: 0x0600211D RID: 8477 RVA: 0x00031276 File Offset: 0x0002F476
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x0600211E RID: 8478 RVA: 0x0003127F File Offset: 0x0002F47F
		// (set) Token: 0x0600211F RID: 8479 RVA: 0x00031287 File Offset: 0x0002F487
		public ProductUserId[] TargetUserIds { get; set; }

		// Token: 0x06002120 RID: 8480 RVA: 0x00031290 File Offset: 0x0002F490
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06002121 RID: 8481 RVA: 0x000312AD File Offset: 0x0002F4AD
		internal void Set(ref SendCustomInviteCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserIds = other.TargetUserIds;
		}
	}
}
