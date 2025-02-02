using System;

namespace TaleWorlds.MountAndBlade.Diamond.Ranked
{
	// Token: 0x02000167 RID: 359
	[Serializable]
	public class RankBarInfo
	{
		// Token: 0x17000321 RID: 801
		// (get) Token: 0x060009F1 RID: 2545 RVA: 0x0000FC89 File Offset: 0x0000DE89
		// (set) Token: 0x060009F2 RID: 2546 RVA: 0x0000FC91 File Offset: 0x0000DE91
		public string RankId { get; set; }

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x060009F3 RID: 2547 RVA: 0x0000FC9A File Offset: 0x0000DE9A
		// (set) Token: 0x060009F4 RID: 2548 RVA: 0x0000FCA2 File Offset: 0x0000DEA2
		public string PreviousRankId { get; set; }

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x060009F5 RID: 2549 RVA: 0x0000FCAB File Offset: 0x0000DEAB
		// (set) Token: 0x060009F6 RID: 2550 RVA: 0x0000FCB3 File Offset: 0x0000DEB3
		public string NextRankId { get; set; }

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x060009F7 RID: 2551 RVA: 0x0000FCBC File Offset: 0x0000DEBC
		// (set) Token: 0x060009F8 RID: 2552 RVA: 0x0000FCC4 File Offset: 0x0000DEC4
		public float ProgressPercentage { get; set; }

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x060009F9 RID: 2553 RVA: 0x0000FCCD File Offset: 0x0000DECD
		// (set) Token: 0x060009FA RID: 2554 RVA: 0x0000FCD5 File Offset: 0x0000DED5
		public int Rating { get; set; }

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x060009FB RID: 2555 RVA: 0x0000FCDE File Offset: 0x0000DEDE
		// (set) Token: 0x060009FC RID: 2556 RVA: 0x0000FCE6 File Offset: 0x0000DEE6
		public int RatingToNextRank { get; set; }

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x060009FD RID: 2557 RVA: 0x0000FCEF File Offset: 0x0000DEEF
		// (set) Token: 0x060009FE RID: 2558 RVA: 0x0000FCF7 File Offset: 0x0000DEF7
		public bool IsEvaluating { get; set; }

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x0000FD00 File Offset: 0x0000DF00
		// (set) Token: 0x06000A00 RID: 2560 RVA: 0x0000FD08 File Offset: 0x0000DF08
		public int EvaluationMatchesPlayed { get; set; }

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x0000FD11 File Offset: 0x0000DF11
		// (set) Token: 0x06000A02 RID: 2562 RVA: 0x0000FD19 File Offset: 0x0000DF19
		public int TotalEvaluationMatchesRequired { get; set; }

		// Token: 0x06000A03 RID: 2563 RVA: 0x0000FD22 File Offset: 0x0000DF22
		public RankBarInfo()
		{
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x0000FD2C File Offset: 0x0000DF2C
		public RankBarInfo(string rankId, string previousRankId, string nextRankId, float progressPercentage, int rating, int ratingToNextRank, bool isEvaluating, int evaluationMatchesPlayed, int totalEvaluationMatchesRequired)
		{
			this.RankId = rankId;
			this.PreviousRankId = previousRankId;
			this.NextRankId = nextRankId;
			this.ProgressPercentage = progressPercentage;
			this.Rating = rating;
			this.RatingToNextRank = ratingToNextRank;
			this.IsEvaluating = isEvaluating;
			this.EvaluationMatchesPlayed = evaluationMatchesPlayed;
			this.TotalEvaluationMatchesRequired = totalEvaluationMatchesRequired;
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x0000FD84 File Offset: 0x0000DF84
		public static RankBarInfo CreateBarInfo(string rankId, string previousRankId, string nextRankId, float progressPercentage, int rating, int ratingToNextRank)
		{
			return new RankBarInfo(rankId, previousRankId, nextRankId, progressPercentage, rating, ratingToNextRank, false, 0, 0);
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x0000FDA4 File Offset: 0x0000DFA4
		public static RankBarInfo CreateUnrankedInfo(int matchesPlayed, int totalMatchesRequired)
		{
			return new RankBarInfo("", "", "", 0f, 0, 0, true, matchesPlayed, totalMatchesRequired);
		}
	}
}
