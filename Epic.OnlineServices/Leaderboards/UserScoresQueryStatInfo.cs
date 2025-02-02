using System;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x02000423 RID: 1059
	public struct UserScoresQueryStatInfo
	{
		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06001B4A RID: 6986 RVA: 0x000286D1 File Offset: 0x000268D1
		// (set) Token: 0x06001B4B RID: 6987 RVA: 0x000286D9 File Offset: 0x000268D9
		public Utf8String StatName { get; set; }

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06001B4C RID: 6988 RVA: 0x000286E2 File Offset: 0x000268E2
		// (set) Token: 0x06001B4D RID: 6989 RVA: 0x000286EA File Offset: 0x000268EA
		public LeaderboardAggregation Aggregation { get; set; }

		// Token: 0x06001B4E RID: 6990 RVA: 0x000286F3 File Offset: 0x000268F3
		internal void Set(ref UserScoresQueryStatInfoInternal other)
		{
			this.StatName = other.StatName;
			this.Aggregation = other.Aggregation;
		}
	}
}
