using System;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001D4 RID: 468
	public struct ParticipantUpdatedCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000CF9 RID: 3321 RVA: 0x00012E48 File Offset: 0x00011048
		// (set) Token: 0x06000CFA RID: 3322 RVA: 0x00012E50 File Offset: 0x00011050
		public object ClientData { get; set; }

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000CFB RID: 3323 RVA: 0x00012E59 File Offset: 0x00011059
		// (set) Token: 0x06000CFC RID: 3324 RVA: 0x00012E61 File Offset: 0x00011061
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000CFD RID: 3325 RVA: 0x00012E6A File Offset: 0x0001106A
		// (set) Token: 0x06000CFE RID: 3326 RVA: 0x00012E72 File Offset: 0x00011072
		public Utf8String RoomName { get; set; }

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000CFF RID: 3327 RVA: 0x00012E7B File Offset: 0x0001107B
		// (set) Token: 0x06000D00 RID: 3328 RVA: 0x00012E83 File Offset: 0x00011083
		public ProductUserId ParticipantId { get; set; }

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000D01 RID: 3329 RVA: 0x00012E8C File Offset: 0x0001108C
		// (set) Token: 0x06000D02 RID: 3330 RVA: 0x00012E94 File Offset: 0x00011094
		public bool Speaking { get; set; }

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000D03 RID: 3331 RVA: 0x00012E9D File Offset: 0x0001109D
		// (set) Token: 0x06000D04 RID: 3332 RVA: 0x00012EA5 File Offset: 0x000110A5
		public RTCAudioStatus AudioStatus { get; set; }

		// Token: 0x06000D05 RID: 3333 RVA: 0x00012EB0 File Offset: 0x000110B0
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x00012ECC File Offset: 0x000110CC
		internal void Set(ref ParticipantUpdatedCallbackInfoInternal other)
		{
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.RoomName = other.RoomName;
			this.ParticipantId = other.ParticipantId;
			this.Speaking = other.Speaking;
			this.AudioStatus = other.AudioStatus;
		}
	}
}
