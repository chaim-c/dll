using System;

namespace Epic.OnlineServices.RTCAdmin
{
	// Token: 0x02000208 RID: 520
	public struct QueryJoinRoomTokenOptions
	{
		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000EA4 RID: 3748 RVA: 0x00015A94 File Offset: 0x00013C94
		// (set) Token: 0x06000EA5 RID: 3749 RVA: 0x00015A9C File Offset: 0x00013C9C
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000EA6 RID: 3750 RVA: 0x00015AA5 File Offset: 0x00013CA5
		// (set) Token: 0x06000EA7 RID: 3751 RVA: 0x00015AAD File Offset: 0x00013CAD
		public Utf8String RoomName { get; set; }

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000EA8 RID: 3752 RVA: 0x00015AB6 File Offset: 0x00013CB6
		// (set) Token: 0x06000EA9 RID: 3753 RVA: 0x00015ABE File Offset: 0x00013CBE
		public ProductUserId[] TargetUserIds { get; set; }

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000EAA RID: 3754 RVA: 0x00015AC7 File Offset: 0x00013CC7
		// (set) Token: 0x06000EAB RID: 3755 RVA: 0x00015ACF File Offset: 0x00013CCF
		public Utf8String TargetUserIpAddresses { get; set; }
	}
}
