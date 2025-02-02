using System;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001A0 RID: 416
	public struct AddNotifyAudioInputStateOptions
	{
		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000BD6 RID: 3030 RVA: 0x00011AA8 File Offset: 0x0000FCA8
		// (set) Token: 0x06000BD7 RID: 3031 RVA: 0x00011AB0 File Offset: 0x0000FCB0
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000BD8 RID: 3032 RVA: 0x00011AB9 File Offset: 0x0000FCB9
		// (set) Token: 0x06000BD9 RID: 3033 RVA: 0x00011AC1 File Offset: 0x0000FCC1
		public Utf8String RoomName { get; set; }
	}
}
