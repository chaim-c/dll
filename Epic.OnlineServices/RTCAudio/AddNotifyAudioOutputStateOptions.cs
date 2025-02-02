using System;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001A2 RID: 418
	public struct AddNotifyAudioOutputStateOptions
	{
		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000BDF RID: 3039 RVA: 0x00011B76 File Offset: 0x0000FD76
		// (set) Token: 0x06000BE0 RID: 3040 RVA: 0x00011B7E File Offset: 0x0000FD7E
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000BE1 RID: 3041 RVA: 0x00011B87 File Offset: 0x0000FD87
		// (set) Token: 0x06000BE2 RID: 3042 RVA: 0x00011B8F File Offset: 0x0000FD8F
		public Utf8String RoomName { get; set; }
	}
}
