using System;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001E0 RID: 480
	public struct SetAudioOutputSettingsOptions
	{
		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000D60 RID: 3424 RVA: 0x00013D60 File Offset: 0x00011F60
		// (set) Token: 0x06000D61 RID: 3425 RVA: 0x00013D68 File Offset: 0x00011F68
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000D62 RID: 3426 RVA: 0x00013D71 File Offset: 0x00011F71
		// (set) Token: 0x06000D63 RID: 3427 RVA: 0x00013D79 File Offset: 0x00011F79
		public Utf8String DeviceId { get; set; }

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000D64 RID: 3428 RVA: 0x00013D82 File Offset: 0x00011F82
		// (set) Token: 0x06000D65 RID: 3429 RVA: 0x00013D8A File Offset: 0x00011F8A
		public float Volume { get; set; }
	}
}
