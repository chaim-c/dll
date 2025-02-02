using System;
using Newtonsoft.Json;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000155 RID: 341
	[Serializable]
	public class PlayerStatsSkirmish : PlayerStatsRanked
	{
		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x0600097A RID: 2426 RVA: 0x0000E0BC File Offset: 0x0000C2BC
		// (set) Token: 0x0600097B RID: 2427 RVA: 0x0000E0C4 File Offset: 0x0000C2C4
		public int MVPs { get; set; }

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x0600097C RID: 2428 RVA: 0x0000E0CD File Offset: 0x0000C2CD
		// (set) Token: 0x0600097D RID: 2429 RVA: 0x0000E0D5 File Offset: 0x0000C2D5
		public int Score { get; set; }

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x0600097E RID: 2430 RVA: 0x0000E0DE File Offset: 0x0000C2DE
		[JsonIgnore]
		public int AverageScore
		{
			get
			{
				return this.Score / ((base.WinCount + base.LoseCount != 0) ? (base.WinCount + base.LoseCount) : 1);
			}
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x0000E106 File Offset: 0x0000C306
		public PlayerStatsSkirmish()
		{
			base.GameType = "Skirmish";
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x0000E11C File Offset: 0x0000C31C
		public void FillWith(PlayerId playerId, int killCount, int deathCount, int assistCount, int winCount, int loseCount, int forfeitCount, int rating, int ratingDeviation, string rank, bool evaluating, int evaluationMatchesPlayedCount, int mvps, int score)
		{
			base.FillWith(playerId, killCount, deathCount, assistCount, winCount, loseCount, forfeitCount, rating, ratingDeviation, rank, evaluating, evaluationMatchesPlayedCount);
			this.MVPs = mvps;
			this.Score = score;
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x0000E154 File Offset: 0x0000C354
		public void FillWithNewPlayer(PlayerId playerId, int defaultRating, int defaultRatingDeviation)
		{
			this.FillWith(playerId, 0, 0, 0, 0, 0, 0, defaultRating, defaultRatingDeviation, "", true, 0, 0, 0);
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x0000E179 File Offset: 0x0000C379
		public void Update(BattlePlayerStatsSkirmish stats, bool won)
		{
			base.Update(stats, won);
			this.MVPs += stats.MVPs;
			this.Score += stats.Score;
		}
	}
}
