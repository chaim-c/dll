using System;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x02000192 RID: 402
	public struct ParticipantStatusChangedCallbackInfo : ICallbackInfo
	{
		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000B72 RID: 2930 RVA: 0x00010EDC File Offset: 0x0000F0DC
		// (set) Token: 0x06000B73 RID: 2931 RVA: 0x00010EE4 File Offset: 0x0000F0E4
		public object ClientData { get; set; }

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000B74 RID: 2932 RVA: 0x00010EED File Offset: 0x0000F0ED
		// (set) Token: 0x06000B75 RID: 2933 RVA: 0x00010EF5 File Offset: 0x0000F0F5
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000B76 RID: 2934 RVA: 0x00010EFE File Offset: 0x0000F0FE
		// (set) Token: 0x06000B77 RID: 2935 RVA: 0x00010F06 File Offset: 0x0000F106
		public Utf8String RoomName { get; set; }

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000B78 RID: 2936 RVA: 0x00010F0F File Offset: 0x0000F10F
		// (set) Token: 0x06000B79 RID: 2937 RVA: 0x00010F17 File Offset: 0x0000F117
		public ProductUserId ParticipantId { get; set; }

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000B7A RID: 2938 RVA: 0x00010F20 File Offset: 0x0000F120
		// (set) Token: 0x06000B7B RID: 2939 RVA: 0x00010F28 File Offset: 0x0000F128
		public RTCParticipantStatus ParticipantStatus { get; set; }

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000B7C RID: 2940 RVA: 0x00010F31 File Offset: 0x0000F131
		// (set) Token: 0x06000B7D RID: 2941 RVA: 0x00010F39 File Offset: 0x0000F139
		public ParticipantMetadata[] ParticipantMetadata { get; set; }

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000B7E RID: 2942 RVA: 0x00010F42 File Offset: 0x0000F142
		// (set) Token: 0x06000B7F RID: 2943 RVA: 0x00010F4A File Offset: 0x0000F14A
		public bool ParticipantInBlocklist { get; set; }

		// Token: 0x06000B80 RID: 2944 RVA: 0x00010F54 File Offset: 0x0000F154
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x00010F70 File Offset: 0x0000F170
		internal void Set(ref ParticipantStatusChangedCallbackInfoInternal other)
		{
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.RoomName = other.RoomName;
			this.ParticipantId = other.ParticipantId;
			this.ParticipantStatus = other.ParticipantStatus;
			this.ParticipantMetadata = other.ParticipantMetadata;
			this.ParticipantInBlocklist = other.ParticipantInBlocklist;
		}
	}
}
