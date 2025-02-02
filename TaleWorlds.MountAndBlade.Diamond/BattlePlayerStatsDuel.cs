using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x020000F9 RID: 249
	[Serializable]
	public class BattlePlayerStatsDuel : BattlePlayerStatsBase
	{
		// Token: 0x060004E6 RID: 1254 RVA: 0x000057E8 File Offset: 0x000039E8
		public BattlePlayerStatsDuel()
		{
			base.GameType = "Duel";
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x000057FB File Offset: 0x000039FB
		// (set) Token: 0x060004E8 RID: 1256 RVA: 0x00005803 File Offset: 0x00003A03
		public int DuelsWon { get; set; }

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x0000580C File Offset: 0x00003A0C
		// (set) Token: 0x060004EA RID: 1258 RVA: 0x00005814 File Offset: 0x00003A14
		public int InfantryWins { get; set; }

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x0000581D File Offset: 0x00003A1D
		// (set) Token: 0x060004EC RID: 1260 RVA: 0x00005825 File Offset: 0x00003A25
		public int ArcherWins { get; set; }

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060004ED RID: 1261 RVA: 0x0000582E File Offset: 0x00003A2E
		// (set) Token: 0x060004EE RID: 1262 RVA: 0x00005836 File Offset: 0x00003A36
		public int CavalryWins { get; set; }
	}
}
