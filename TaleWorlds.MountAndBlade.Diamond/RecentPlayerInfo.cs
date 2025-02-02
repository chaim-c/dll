using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200015C RID: 348
	public class RecentPlayerInfo
	{
		// Token: 0x17000311 RID: 785
		// (get) Token: 0x060009BF RID: 2495 RVA: 0x0000E8E8 File Offset: 0x0000CAE8
		// (set) Token: 0x060009C0 RID: 2496 RVA: 0x0000E8F0 File Offset: 0x0000CAF0
		public string PlayerId { get; set; }

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x060009C1 RID: 2497 RVA: 0x0000E8F9 File Offset: 0x0000CAF9
		// (set) Token: 0x060009C2 RID: 2498 RVA: 0x0000E901 File Offset: 0x0000CB01
		public string PlayerName { get; set; }

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x060009C3 RID: 2499 RVA: 0x0000E90A File Offset: 0x0000CB0A
		// (set) Token: 0x060009C4 RID: 2500 RVA: 0x0000E912 File Offset: 0x0000CB12
		public int ImportanceScore { get; set; }

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x060009C5 RID: 2501 RVA: 0x0000E91B File Offset: 0x0000CB1B
		// (set) Token: 0x060009C6 RID: 2502 RVA: 0x0000E923 File Offset: 0x0000CB23
		public DateTime InteractionTime { get; set; }
	}
}
