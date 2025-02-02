using System;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond.MultiplayerBadges
{
	// Token: 0x02000172 RID: 370
	public struct KillData
	{
		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000A3C RID: 2620 RVA: 0x00010D1C File Offset: 0x0000EF1C
		// (set) Token: 0x06000A3D RID: 2621 RVA: 0x00010D24 File Offset: 0x0000EF24
		public PlayerId KillerId { get; set; }

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000A3E RID: 2622 RVA: 0x00010D2D File Offset: 0x0000EF2D
		// (set) Token: 0x06000A3F RID: 2623 RVA: 0x00010D35 File Offset: 0x0000EF35
		public PlayerId VictimId { get; set; }

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000A40 RID: 2624 RVA: 0x00010D3E File Offset: 0x0000EF3E
		// (set) Token: 0x06000A41 RID: 2625 RVA: 0x00010D46 File Offset: 0x0000EF46
		public string KillerFaction { get; set; }

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000A42 RID: 2626 RVA: 0x00010D4F File Offset: 0x0000EF4F
		// (set) Token: 0x06000A43 RID: 2627 RVA: 0x00010D57 File Offset: 0x0000EF57
		public string VictimFaction { get; set; }

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000A44 RID: 2628 RVA: 0x00010D60 File Offset: 0x0000EF60
		// (set) Token: 0x06000A45 RID: 2629 RVA: 0x00010D68 File Offset: 0x0000EF68
		public string KillerTroop { get; set; }

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x00010D71 File Offset: 0x0000EF71
		// (set) Token: 0x06000A47 RID: 2631 RVA: 0x00010D79 File Offset: 0x0000EF79
		public string VictimTroop { get; set; }
	}
}
