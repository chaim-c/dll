using System;
using Newtonsoft.Json;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000151 RID: 337
	[Serializable]
	public class PlayerStatsCaptain : PlayerStatsRanked
	{
		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000948 RID: 2376 RVA: 0x0000DC60 File Offset: 0x0000BE60
		// (set) Token: 0x06000949 RID: 2377 RVA: 0x0000DC68 File Offset: 0x0000BE68
		public int CaptainsKilled { get; set; }

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x0600094A RID: 2378 RVA: 0x0000DC71 File Offset: 0x0000BE71
		// (set) Token: 0x0600094B RID: 2379 RVA: 0x0000DC79 File Offset: 0x0000BE79
		public int MVPs { get; set; }

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x0600094C RID: 2380 RVA: 0x0000DC82 File Offset: 0x0000BE82
		// (set) Token: 0x0600094D RID: 2381 RVA: 0x0000DC8A File Offset: 0x0000BE8A
		public int Score { get; set; }

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x0600094E RID: 2382 RVA: 0x0000DC93 File Offset: 0x0000BE93
		[JsonIgnore]
		public int AverageScore
		{
			get
			{
				if (this.Score / (base.WinCount + base.LoseCount) == 0)
				{
					return 1;
				}
				return base.WinCount + base.LoseCount;
			}
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x0000DCBA File Offset: 0x0000BEBA
		public PlayerStatsCaptain()
		{
			base.GameType = "Captain";
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x0000DCD0 File Offset: 0x0000BED0
		public void FillWith(PlayerId playerId, int killCount, int deathCount, int assistCount, int winCount, int loseCount, int forfeitCount, int rating, int ratingDeviation, string rank, bool evaluating, int evaluationMatchesPlayedCount, int captainsKilled, int mvps, int score)
		{
			base.FillWith(playerId, killCount, deathCount, assistCount, winCount, loseCount, forfeitCount, rating, ratingDeviation, rank, evaluating, evaluationMatchesPlayedCount);
			this.CaptainsKilled = captainsKilled;
			this.MVPs = mvps;
			this.Score = score;
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x0000DD10 File Offset: 0x0000BF10
		public void FillWithNewPlayer(PlayerId playerId, int defaultRating, int defaultRatingDeviation)
		{
			this.FillWith(playerId, 0, 0, 0, 0, 0, 0, defaultRating, defaultRatingDeviation, "", true, 0, 0, 0, 0);
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x0000DD38 File Offset: 0x0000BF38
		public void Update(BattlePlayerStatsCaptain stats, bool won)
		{
			base.Update(stats, won);
			this.CaptainsKilled += stats.CaptainsKilled;
			this.MVPs += stats.MVPs;
			this.Score += stats.Score;
		}
	}
}
