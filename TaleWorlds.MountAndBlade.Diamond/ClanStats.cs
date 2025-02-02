using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000113 RID: 275
	[Serializable]
	public class ClanStats
	{
		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x00007892 File Offset: 0x00005A92
		// (set) Token: 0x060005E5 RID: 1509 RVA: 0x0000789A File Offset: 0x00005A9A
		public int WinCount { get; private set; }

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x000078A3 File Offset: 0x00005AA3
		// (set) Token: 0x060005E7 RID: 1511 RVA: 0x000078AB File Offset: 0x00005AAB
		public int LossCount { get; private set; }

		// Token: 0x060005E8 RID: 1512 RVA: 0x000078B4 File Offset: 0x00005AB4
		public ClanStats(int winCount, int lossCount)
		{
			this.WinCount = winCount;
			this.LossCount = lossCount;
		}
	}
}
