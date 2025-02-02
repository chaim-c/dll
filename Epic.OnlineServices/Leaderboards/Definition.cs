using System;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x02000403 RID: 1027
	public struct Definition
	{
		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06001A86 RID: 6790 RVA: 0x000273F6 File Offset: 0x000255F6
		// (set) Token: 0x06001A87 RID: 6791 RVA: 0x000273FE File Offset: 0x000255FE
		public Utf8String LeaderboardId { get; set; }

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06001A88 RID: 6792 RVA: 0x00027407 File Offset: 0x00025607
		// (set) Token: 0x06001A89 RID: 6793 RVA: 0x0002740F File Offset: 0x0002560F
		public Utf8String StatName { get; set; }

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x06001A8A RID: 6794 RVA: 0x00027418 File Offset: 0x00025618
		// (set) Token: 0x06001A8B RID: 6795 RVA: 0x00027420 File Offset: 0x00025620
		public LeaderboardAggregation Aggregation { get; set; }

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x06001A8C RID: 6796 RVA: 0x00027429 File Offset: 0x00025629
		// (set) Token: 0x06001A8D RID: 6797 RVA: 0x00027431 File Offset: 0x00025631
		public DateTimeOffset? StartTime { get; set; }

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x06001A8E RID: 6798 RVA: 0x0002743A File Offset: 0x0002563A
		// (set) Token: 0x06001A8F RID: 6799 RVA: 0x00027442 File Offset: 0x00025642
		public DateTimeOffset? EndTime { get; set; }

		// Token: 0x06001A90 RID: 6800 RVA: 0x0002744C File Offset: 0x0002564C
		internal void Set(ref DefinitionInternal other)
		{
			this.LeaderboardId = other.LeaderboardId;
			this.StatName = other.StatName;
			this.Aggregation = other.Aggregation;
			this.StartTime = other.StartTime;
			this.EndTime = other.EndTime;
		}
	}
}
