using System;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000153 RID: 339
	[Serializable]
	public class PlayerStatsRanked : PlayerStatsBase
	{
		// Token: 0x170002EE RID: 750
		// (get) Token: 0x0600095F RID: 2399 RVA: 0x0000DE91 File Offset: 0x0000C091
		// (set) Token: 0x06000960 RID: 2400 RVA: 0x0000DE99 File Offset: 0x0000C099
		public int Rating { get; set; }

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000961 RID: 2401 RVA: 0x0000DEA2 File Offset: 0x0000C0A2
		// (set) Token: 0x06000962 RID: 2402 RVA: 0x0000DEAA File Offset: 0x0000C0AA
		public string Rank { get; set; }

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000963 RID: 2403 RVA: 0x0000DEB3 File Offset: 0x0000C0B3
		// (set) Token: 0x06000964 RID: 2404 RVA: 0x0000DEBB File Offset: 0x0000C0BB
		public bool Evaluating { get; set; }

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000965 RID: 2405 RVA: 0x0000DEC4 File Offset: 0x0000C0C4
		// (set) Token: 0x06000966 RID: 2406 RVA: 0x0000DECC File Offset: 0x0000C0CC
		public int EvaluationMatchesPlayedCount { get; set; }

		// Token: 0x06000967 RID: 2407 RVA: 0x0000DED5 File Offset: 0x0000C0D5
		public void FillWith(PlayerId playerId, int killCount, int deathCount, int assistCount, int winCount, int loseCount, int forfeitCount, int rating, int ratingDeviation, string rank, bool evaluating, int evaluationMatchesPlayedCount)
		{
			base.FillWith(playerId, killCount, deathCount, assistCount, winCount, loseCount, forfeitCount);
			this.Rating = rating;
			this.Rank = rank;
			this.Evaluating = evaluating;
			this.EvaluationMatchesPlayedCount = evaluationMatchesPlayedCount;
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x0000DF08 File Offset: 0x0000C108
		public virtual void FillWithNewPlayer(PlayerId playerId, string gameType, int defaultRating, int defaultRatingDeviation)
		{
			this.FillWith(playerId, 0, 0, 0, 0, 0, 0, defaultRating, defaultRatingDeviation, "", true, 0);
		}
	}
}
