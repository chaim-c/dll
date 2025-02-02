using System;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000154 RID: 340
	[Serializable]
	public class PlayerStatsSiege : PlayerStatsBase
	{
		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x0000DF34 File Offset: 0x0000C134
		// (set) Token: 0x0600096B RID: 2411 RVA: 0x0000DF3C File Offset: 0x0000C13C
		public int WallsBreached { get; set; }

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x0000DF45 File Offset: 0x0000C145
		// (set) Token: 0x0600096D RID: 2413 RVA: 0x0000DF4D File Offset: 0x0000C14D
		public int SiegeEngineKills { get; set; }

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x0000DF56 File Offset: 0x0000C156
		// (set) Token: 0x0600096F RID: 2415 RVA: 0x0000DF5E File Offset: 0x0000C15E
		public int SiegeEnginesDestroyed { get; set; }

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000970 RID: 2416 RVA: 0x0000DF67 File Offset: 0x0000C167
		// (set) Token: 0x06000971 RID: 2417 RVA: 0x0000DF6F File Offset: 0x0000C16F
		public int ObjectiveGoldGained { get; set; }

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000972 RID: 2418 RVA: 0x0000DF78 File Offset: 0x0000C178
		// (set) Token: 0x06000973 RID: 2419 RVA: 0x0000DF80 File Offset: 0x0000C180
		public int Score { get; set; }

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000974 RID: 2420 RVA: 0x0000DF89 File Offset: 0x0000C189
		public int AverageScore
		{
			get
			{
				return this.Score / ((base.WinCount + base.LoseCount != 0) ? (base.WinCount + base.LoseCount) : 1);
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000975 RID: 2421 RVA: 0x0000DFB1 File Offset: 0x0000C1B1
		public int AverageKillCount
		{
			get
			{
				return base.KillCount / ((base.WinCount + base.LoseCount != 0) ? (base.WinCount + base.LoseCount) : 1);
			}
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x0000DFD9 File Offset: 0x0000C1D9
		public PlayerStatsSiege()
		{
			base.GameType = "Siege";
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x0000DFEC File Offset: 0x0000C1EC
		public void FillWith(PlayerId playerId, int killCount, int deathCount, int assistCount, int winCount, int loseCount, int forfeitCount, int wallsBreached, int siegeEngineKills, int siegeEnginesDestroyed, int objectiveGoldGained, int score)
		{
			base.FillWith(playerId, killCount, deathCount, assistCount, winCount, loseCount, forfeitCount);
			this.WallsBreached = wallsBreached;
			this.SiegeEngineKills = siegeEngineKills;
			this.SiegeEnginesDestroyed = siegeEnginesDestroyed;
			this.ObjectiveGoldGained = objectiveGoldGained;
			this.Score = score;
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0000E028 File Offset: 0x0000C228
		public void FillWithNewPlayer(PlayerId playerId)
		{
			this.FillWith(playerId, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x0000E048 File Offset: 0x0000C248
		public void Update(BattlePlayerStatsSiege stats, bool won)
		{
			base.Update(stats, won);
			this.WallsBreached += stats.WallsBreached;
			this.SiegeEngineKills += stats.SiegeEngineKills;
			this.SiegeEnginesDestroyed += stats.SiegeEnginesDestroyed;
			this.ObjectiveGoldGained += stats.ObjectiveGoldGained;
			this.Score += stats.Score;
		}
	}
}
