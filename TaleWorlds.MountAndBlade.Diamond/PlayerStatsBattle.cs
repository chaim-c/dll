using System;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000150 RID: 336
	[Serializable]
	public class PlayerStatsBattle : PlayerStatsBase
	{
		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x0000DBB9 File Offset: 0x0000BDB9
		// (set) Token: 0x06000941 RID: 2369 RVA: 0x0000DBC1 File Offset: 0x0000BDC1
		public int RoundsWon { get; private set; }

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000942 RID: 2370 RVA: 0x0000DBCA File Offset: 0x0000BDCA
		// (set) Token: 0x06000943 RID: 2371 RVA: 0x0000DBD2 File Offset: 0x0000BDD2
		public int RoundsLost { get; private set; }

		// Token: 0x06000944 RID: 2372 RVA: 0x0000DBDB File Offset: 0x0000BDDB
		public PlayerStatsBattle()
		{
			base.GameType = "Battle";
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x0000DBEE File Offset: 0x0000BDEE
		public void FillWith(PlayerId playerId, int killCount, int deathCount, int assistCount, int winCount, int loseCount, int forfeitCount, int roundsWon, int roundsLost)
		{
			base.FillWith(playerId, killCount, deathCount, assistCount, winCount, loseCount, forfeitCount);
			this.RoundsWon = roundsWon;
			this.RoundsLost = roundsLost;
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x0000DC14 File Offset: 0x0000BE14
		public void FillWithNewPlayer(PlayerId playerId)
		{
			this.FillWith(playerId, 0, 0, 0, 0, 0, 0, 0, 0);
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x0000DC30 File Offset: 0x0000BE30
		public void Update(BattlePlayerStatsBattle stats, bool won)
		{
			base.Update(stats, won);
			this.RoundsWon += stats.RoundsWon;
			this.RoundsLost += stats.RoundsLost;
		}
	}
}
