using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x020000F8 RID: 248
	[Serializable]
	public class BattlePlayerStatsCaptain : BattlePlayerStatsBase
	{
		// Token: 0x060004DF RID: 1247 RVA: 0x000057A2 File Offset: 0x000039A2
		public BattlePlayerStatsCaptain()
		{
			base.GameType = "Captain";
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x000057B5 File Offset: 0x000039B5
		// (set) Token: 0x060004E1 RID: 1249 RVA: 0x000057BD File Offset: 0x000039BD
		public int CaptainsKilled { get; set; }

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x000057C6 File Offset: 0x000039C6
		// (set) Token: 0x060004E3 RID: 1251 RVA: 0x000057CE File Offset: 0x000039CE
		public int MVPs { get; set; }

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x000057D7 File Offset: 0x000039D7
		// (set) Token: 0x060004E5 RID: 1253 RVA: 0x000057DF File Offset: 0x000039DF
		public int Score { get; set; }
	}
}
