using System;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x0200019C RID: 412
	public struct AddNotifyAudioBeforeSendOptions
	{
		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000BCA RID: 3018 RVA: 0x000119AB File Offset: 0x0000FBAB
		// (set) Token: 0x06000BCB RID: 3019 RVA: 0x000119B3 File Offset: 0x0000FBB3
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000BCC RID: 3020 RVA: 0x000119BC File Offset: 0x0000FBBC
		// (set) Token: 0x06000BCD RID: 3021 RVA: 0x000119C4 File Offset: 0x0000FBC4
		public Utf8String RoomName { get; set; }
	}
}
