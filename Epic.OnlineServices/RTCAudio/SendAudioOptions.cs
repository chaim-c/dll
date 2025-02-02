using System;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001DC RID: 476
	public struct SendAudioOptions
	{
		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000D45 RID: 3397 RVA: 0x00013AF7 File Offset: 0x00011CF7
		// (set) Token: 0x06000D46 RID: 3398 RVA: 0x00013AFF File Offset: 0x00011CFF
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000D47 RID: 3399 RVA: 0x00013B08 File Offset: 0x00011D08
		// (set) Token: 0x06000D48 RID: 3400 RVA: 0x00013B10 File Offset: 0x00011D10
		public Utf8String RoomName { get; set; }

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000D49 RID: 3401 RVA: 0x00013B19 File Offset: 0x00011D19
		// (set) Token: 0x06000D4A RID: 3402 RVA: 0x00013B21 File Offset: 0x00011D21
		public AudioBuffer? Buffer { get; set; }
	}
}
