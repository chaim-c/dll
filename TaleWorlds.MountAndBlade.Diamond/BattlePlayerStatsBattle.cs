using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x020000F7 RID: 247
	[Serializable]
	public class BattlePlayerStatsBattle : BattlePlayerStatsBase
	{
		// Token: 0x060004DA RID: 1242 RVA: 0x0000576D File Offset: 0x0000396D
		public BattlePlayerStatsBattle()
		{
			base.GameType = "Battle";
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x00005780 File Offset: 0x00003980
		// (set) Token: 0x060004DC RID: 1244 RVA: 0x00005788 File Offset: 0x00003988
		public int RoundsWon { get; set; }

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x00005791 File Offset: 0x00003991
		// (set) Token: 0x060004DE RID: 1246 RVA: 0x00005799 File Offset: 0x00003999
		public int RoundsLost { get; set; }
	}
}
