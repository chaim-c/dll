using System;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001E4 RID: 484
	public struct UpdateParticipantVolumeCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000D72 RID: 3442 RVA: 0x00013EE9 File Offset: 0x000120E9
		// (set) Token: 0x06000D73 RID: 3443 RVA: 0x00013EF1 File Offset: 0x000120F1
		public Result ResultCode { get; set; }

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000D74 RID: 3444 RVA: 0x00013EFA File Offset: 0x000120FA
		// (set) Token: 0x06000D75 RID: 3445 RVA: 0x00013F02 File Offset: 0x00012102
		public object ClientData { get; set; }

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000D76 RID: 3446 RVA: 0x00013F0B File Offset: 0x0001210B
		// (set) Token: 0x06000D77 RID: 3447 RVA: 0x00013F13 File Offset: 0x00012113
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000D78 RID: 3448 RVA: 0x00013F1C File Offset: 0x0001211C
		// (set) Token: 0x06000D79 RID: 3449 RVA: 0x00013F24 File Offset: 0x00012124
		public Utf8String RoomName { get; set; }

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000D7A RID: 3450 RVA: 0x00013F2D File Offset: 0x0001212D
		// (set) Token: 0x06000D7B RID: 3451 RVA: 0x00013F35 File Offset: 0x00012135
		public ProductUserId ParticipantId { get; set; }

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000D7C RID: 3452 RVA: 0x00013F3E File Offset: 0x0001213E
		// (set) Token: 0x06000D7D RID: 3453 RVA: 0x00013F46 File Offset: 0x00012146
		public float Volume { get; set; }

		// Token: 0x06000D7E RID: 3454 RVA: 0x00013F50 File Offset: 0x00012150
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x00013F70 File Offset: 0x00012170
		internal void Set(ref UpdateParticipantVolumeCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.RoomName = other.RoomName;
			this.ParticipantId = other.ParticipantId;
			this.Volume = other.Volume;
		}
	}
}
