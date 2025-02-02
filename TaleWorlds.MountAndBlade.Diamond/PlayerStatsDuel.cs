using System;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000152 RID: 338
	[Serializable]
	public class PlayerStatsDuel : PlayerStatsBase
	{
		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000953 RID: 2387 RVA: 0x0000DD86 File Offset: 0x0000BF86
		// (set) Token: 0x06000954 RID: 2388 RVA: 0x0000DD8E File Offset: 0x0000BF8E
		public int DuelsWon { get; set; }

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000955 RID: 2389 RVA: 0x0000DD97 File Offset: 0x0000BF97
		// (set) Token: 0x06000956 RID: 2390 RVA: 0x0000DD9F File Offset: 0x0000BF9F
		public int InfantryWins { get; set; }

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000957 RID: 2391 RVA: 0x0000DDA8 File Offset: 0x0000BFA8
		// (set) Token: 0x06000958 RID: 2392 RVA: 0x0000DDB0 File Offset: 0x0000BFB0
		public int ArcherWins { get; set; }

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000959 RID: 2393 RVA: 0x0000DDB9 File Offset: 0x0000BFB9
		// (set) Token: 0x0600095A RID: 2394 RVA: 0x0000DDC1 File Offset: 0x0000BFC1
		public int CavalryWins { get; set; }

		// Token: 0x0600095B RID: 2395 RVA: 0x0000DDCA File Offset: 0x0000BFCA
		public PlayerStatsDuel()
		{
			base.GameType = "Duel";
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x0000DDDD File Offset: 0x0000BFDD
		public void FillWith(PlayerId playerId, int killCount, int deathCount, int assistCount, int winCount, int loseCount, int forfeitCount, int duelsWon, int infantryWins, int archerWins, int cavalryWins)
		{
			base.FillWith(playerId, killCount, deathCount, assistCount, winCount, loseCount, forfeitCount);
			this.DuelsWon = duelsWon;
			this.InfantryWins = infantryWins;
			this.ArcherWins = archerWins;
			this.CavalryWins = cavalryWins;
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x0000DE10 File Offset: 0x0000C010
		public void FillWithNewPlayer(PlayerId playerId)
		{
			this.FillWith(playerId, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x0000DE30 File Offset: 0x0000C030
		public void Update(BattlePlayerStatsDuel stats, bool won)
		{
			base.Update(stats, won);
			this.DuelsWon += stats.DuelsWon;
			this.InfantryWins += stats.InfantryWins;
			this.ArcherWins += stats.ArcherWins;
			this.CavalryWins += stats.CavalryWins;
		}
	}
}
