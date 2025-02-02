using System;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x02000184 RID: 388
	public struct LeaveRoomOptions
	{
		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000B34 RID: 2868 RVA: 0x00010CC7 File Offset: 0x0000EEC7
		// (set) Token: 0x06000B35 RID: 2869 RVA: 0x00010CCF File Offset: 0x0000EECF
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000B36 RID: 2870 RVA: 0x00010CD8 File Offset: 0x0000EED8
		// (set) Token: 0x06000B37 RID: 2871 RVA: 0x00010CE0 File Offset: 0x0000EEE0
		public Utf8String RoomName { get; set; }
	}
}
