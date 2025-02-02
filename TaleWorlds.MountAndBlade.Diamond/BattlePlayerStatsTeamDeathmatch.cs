using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x020000FC RID: 252
	[Serializable]
	public class BattlePlayerStatsTeamDeathmatch : BattlePlayerStatsBase
	{
		// Token: 0x060004FF RID: 1279 RVA: 0x000058DC File Offset: 0x00003ADC
		public BattlePlayerStatsTeamDeathmatch()
		{
			base.GameType = "TeamDeathmatch";
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x000058EF File Offset: 0x00003AEF
		// (set) Token: 0x06000501 RID: 1281 RVA: 0x000058F7 File Offset: 0x00003AF7
		public int Score { get; set; }
	}
}
