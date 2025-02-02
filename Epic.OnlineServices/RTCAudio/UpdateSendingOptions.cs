using System;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001F2 RID: 498
	public struct UpdateSendingOptions
	{
		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000E10 RID: 3600 RVA: 0x00014EF8 File Offset: 0x000130F8
		// (set) Token: 0x06000E11 RID: 3601 RVA: 0x00014F00 File Offset: 0x00013100
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000E12 RID: 3602 RVA: 0x00014F09 File Offset: 0x00013109
		// (set) Token: 0x06000E13 RID: 3603 RVA: 0x00014F11 File Offset: 0x00013111
		public Utf8String RoomName { get; set; }

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000E14 RID: 3604 RVA: 0x00014F1A File Offset: 0x0001311A
		// (set) Token: 0x06000E15 RID: 3605 RVA: 0x00014F22 File Offset: 0x00013122
		public RTCAudioStatus AudioStatus { get; set; }
	}
}
