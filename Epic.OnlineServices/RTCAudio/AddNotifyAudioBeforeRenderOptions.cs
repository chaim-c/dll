using System;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x0200019A RID: 410
	public struct AddNotifyAudioBeforeRenderOptions
	{
		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000BBE RID: 3006 RVA: 0x0001189A File Offset: 0x0000FA9A
		// (set) Token: 0x06000BBF RID: 3007 RVA: 0x000118A2 File Offset: 0x0000FAA2
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000BC0 RID: 3008 RVA: 0x000118AB File Offset: 0x0000FAAB
		// (set) Token: 0x06000BC1 RID: 3009 RVA: 0x000118B3 File Offset: 0x0000FAB3
		public Utf8String RoomName { get; set; }

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000BC2 RID: 3010 RVA: 0x000118BC File Offset: 0x0000FABC
		// (set) Token: 0x06000BC3 RID: 3011 RVA: 0x000118C4 File Offset: 0x0000FAC4
		public bool UnmixedAudio { get; set; }
	}
}
