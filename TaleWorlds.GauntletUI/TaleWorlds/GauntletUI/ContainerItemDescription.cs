using System;

namespace TaleWorlds.GauntletUI
{
	// Token: 0x0200001D RID: 29
	public class ContainerItemDescription
	{
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600022E RID: 558 RVA: 0x0000B3C1 File Offset: 0x000095C1
		// (set) Token: 0x0600022F RID: 559 RVA: 0x0000B3C9 File Offset: 0x000095C9
		public string WidgetId { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000230 RID: 560 RVA: 0x0000B3D2 File Offset: 0x000095D2
		// (set) Token: 0x06000231 RID: 561 RVA: 0x0000B3DA File Offset: 0x000095DA
		public int WidgetIndex { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000B3E3 File Offset: 0x000095E3
		// (set) Token: 0x06000233 RID: 563 RVA: 0x0000B3EB File Offset: 0x000095EB
		public float WidthStretchRatio { get; set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000234 RID: 564 RVA: 0x0000B3F4 File Offset: 0x000095F4
		// (set) Token: 0x06000235 RID: 565 RVA: 0x0000B3FC File Offset: 0x000095FC
		public float HeightStretchRatio { get; set; }

		// Token: 0x06000236 RID: 566 RVA: 0x0000B405 File Offset: 0x00009605
		public ContainerItemDescription()
		{
			this.WidgetId = "";
			this.WidgetIndex = -1;
			this.WidthStretchRatio = 1f;
			this.HeightStretchRatio = 1f;
		}
	}
}
