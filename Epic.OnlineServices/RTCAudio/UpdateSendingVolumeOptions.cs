using System;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001F6 RID: 502
	public struct UpdateSendingVolumeOptions
	{
		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000E37 RID: 3639 RVA: 0x000152D0 File Offset: 0x000134D0
		// (set) Token: 0x06000E38 RID: 3640 RVA: 0x000152D8 File Offset: 0x000134D8
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000E39 RID: 3641 RVA: 0x000152E1 File Offset: 0x000134E1
		// (set) Token: 0x06000E3A RID: 3642 RVA: 0x000152E9 File Offset: 0x000134E9
		public Utf8String RoomName { get; set; }

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000E3B RID: 3643 RVA: 0x000152F2 File Offset: 0x000134F2
		// (set) Token: 0x06000E3C RID: 3644 RVA: 0x000152FA File Offset: 0x000134FA
		public float Volume { get; set; }
	}
}
