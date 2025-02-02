using System;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x0200030A RID: 778
	public struct UpdateModOptions
	{
		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x060014F9 RID: 5369 RVA: 0x0001F0EB File Offset: 0x0001D2EB
		// (set) Token: 0x060014FA RID: 5370 RVA: 0x0001F0F3 File Offset: 0x0001D2F3
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x060014FB RID: 5371 RVA: 0x0001F0FC File Offset: 0x0001D2FC
		// (set) Token: 0x060014FC RID: 5372 RVA: 0x0001F104 File Offset: 0x0001D304
		public ModIdentifier? Mod { get; set; }
	}
}
