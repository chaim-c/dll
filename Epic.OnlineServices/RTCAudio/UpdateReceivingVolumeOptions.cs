using System;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001EE RID: 494
	public struct UpdateReceivingVolumeOptions
	{
		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000DE9 RID: 3561 RVA: 0x00014B20 File Offset: 0x00012D20
		// (set) Token: 0x06000DEA RID: 3562 RVA: 0x00014B28 File Offset: 0x00012D28
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000DEB RID: 3563 RVA: 0x00014B31 File Offset: 0x00012D31
		// (set) Token: 0x06000DEC RID: 3564 RVA: 0x00014B39 File Offset: 0x00012D39
		public Utf8String RoomName { get; set; }

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000DED RID: 3565 RVA: 0x00014B42 File Offset: 0x00012D42
		// (set) Token: 0x06000DEE RID: 3566 RVA: 0x00014B4A File Offset: 0x00012D4A
		public float Volume { get; set; }
	}
}
