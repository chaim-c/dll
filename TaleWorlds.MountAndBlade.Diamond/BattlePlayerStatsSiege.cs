using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x020000FA RID: 250
	[Serializable]
	public class BattlePlayerStatsSiege : BattlePlayerStatsBase
	{
		// Token: 0x060004EF RID: 1263 RVA: 0x0000583F File Offset: 0x00003A3F
		public BattlePlayerStatsSiege()
		{
			base.GameType = "Siege";
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x00005852 File Offset: 0x00003A52
		// (set) Token: 0x060004F1 RID: 1265 RVA: 0x0000585A File Offset: 0x00003A5A
		public int WallsBreached { get; set; }

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x00005863 File Offset: 0x00003A63
		// (set) Token: 0x060004F3 RID: 1267 RVA: 0x0000586B File Offset: 0x00003A6B
		public int SiegeEngineKills { get; set; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x00005874 File Offset: 0x00003A74
		// (set) Token: 0x060004F5 RID: 1269 RVA: 0x0000587C File Offset: 0x00003A7C
		public int SiegeEnginesDestroyed { get; set; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x00005885 File Offset: 0x00003A85
		// (set) Token: 0x060004F7 RID: 1271 RVA: 0x0000588D File Offset: 0x00003A8D
		public int ObjectiveGoldGained { get; set; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x00005896 File Offset: 0x00003A96
		// (set) Token: 0x060004F9 RID: 1273 RVA: 0x0000589E File Offset: 0x00003A9E
		public int Score { get; set; }
	}
}
