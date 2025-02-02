using System;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001E8 RID: 488
	public struct UpdateReceivingCallbackInfo : ICallbackInfo
	{
		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000DA0 RID: 3488 RVA: 0x00014390 File Offset: 0x00012590
		// (set) Token: 0x06000DA1 RID: 3489 RVA: 0x00014398 File Offset: 0x00012598
		public Result ResultCode { get; set; }

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000DA2 RID: 3490 RVA: 0x000143A1 File Offset: 0x000125A1
		// (set) Token: 0x06000DA3 RID: 3491 RVA: 0x000143A9 File Offset: 0x000125A9
		public object ClientData { get; set; }

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000DA4 RID: 3492 RVA: 0x000143B2 File Offset: 0x000125B2
		// (set) Token: 0x06000DA5 RID: 3493 RVA: 0x000143BA File Offset: 0x000125BA
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000DA6 RID: 3494 RVA: 0x000143C3 File Offset: 0x000125C3
		// (set) Token: 0x06000DA7 RID: 3495 RVA: 0x000143CB File Offset: 0x000125CB
		public Utf8String RoomName { get; set; }

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000DA8 RID: 3496 RVA: 0x000143D4 File Offset: 0x000125D4
		// (set) Token: 0x06000DA9 RID: 3497 RVA: 0x000143DC File Offset: 0x000125DC
		public ProductUserId ParticipantId { get; set; }

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000DAA RID: 3498 RVA: 0x000143E5 File Offset: 0x000125E5
		// (set) Token: 0x06000DAB RID: 3499 RVA: 0x000143ED File Offset: 0x000125ED
		public bool AudioEnabled { get; set; }

		// Token: 0x06000DAC RID: 3500 RVA: 0x000143F8 File Offset: 0x000125F8
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x00014418 File Offset: 0x00012618
		internal void Set(ref UpdateReceivingCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.RoomName = other.RoomName;
			this.ParticipantId = other.ParticipantId;
			this.AudioEnabled = other.AudioEnabled;
		}
	}
}
