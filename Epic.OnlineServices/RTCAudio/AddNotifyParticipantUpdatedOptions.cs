using System;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001A4 RID: 420
	public struct AddNotifyParticipantUpdatedOptions
	{
		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000BE8 RID: 3048 RVA: 0x00011C42 File Offset: 0x0000FE42
		// (set) Token: 0x06000BE9 RID: 3049 RVA: 0x00011C4A File Offset: 0x0000FE4A
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000BEA RID: 3050 RVA: 0x00011C53 File Offset: 0x0000FE53
		// (set) Token: 0x06000BEB RID: 3051 RVA: 0x00011C5B File Offset: 0x0000FE5B
		public Utf8String RoomName { get; set; }
	}
}
