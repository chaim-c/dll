using System;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001DE RID: 478
	public struct SetAudioInputSettingsOptions
	{
		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000D51 RID: 3409 RVA: 0x00013C13 File Offset: 0x00011E13
		// (set) Token: 0x06000D52 RID: 3410 RVA: 0x00013C1B File Offset: 0x00011E1B
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000D53 RID: 3411 RVA: 0x00013C24 File Offset: 0x00011E24
		// (set) Token: 0x06000D54 RID: 3412 RVA: 0x00013C2C File Offset: 0x00011E2C
		public Utf8String DeviceId { get; set; }

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000D55 RID: 3413 RVA: 0x00013C35 File Offset: 0x00011E35
		// (set) Token: 0x06000D56 RID: 3414 RVA: 0x00013C3D File Offset: 0x00011E3D
		public float Volume { get; set; }

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000D57 RID: 3415 RVA: 0x00013C46 File Offset: 0x00011E46
		// (set) Token: 0x06000D58 RID: 3416 RVA: 0x00013C4E File Offset: 0x00011E4E
		public bool PlatformAEC { get; set; }
	}
}
