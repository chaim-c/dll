using System;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x0200040C RID: 1036
	public struct LeaderboardRecord
	{
		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06001AAB RID: 6827 RVA: 0x0002777D File Offset: 0x0002597D
		// (set) Token: 0x06001AAC RID: 6828 RVA: 0x00027785 File Offset: 0x00025985
		public ProductUserId UserId { get; set; }

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06001AAD RID: 6829 RVA: 0x0002778E File Offset: 0x0002598E
		// (set) Token: 0x06001AAE RID: 6830 RVA: 0x00027796 File Offset: 0x00025996
		public uint Rank { get; set; }

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06001AAF RID: 6831 RVA: 0x0002779F File Offset: 0x0002599F
		// (set) Token: 0x06001AB0 RID: 6832 RVA: 0x000277A7 File Offset: 0x000259A7
		public int Score { get; set; }

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06001AB1 RID: 6833 RVA: 0x000277B0 File Offset: 0x000259B0
		// (set) Token: 0x06001AB2 RID: 6834 RVA: 0x000277B8 File Offset: 0x000259B8
		public Utf8String UserDisplayName { get; set; }

		// Token: 0x06001AB3 RID: 6835 RVA: 0x000277C1 File Offset: 0x000259C1
		internal void Set(ref LeaderboardRecordInternal other)
		{
			this.UserId = other.UserId;
			this.Rank = other.Rank;
			this.Score = other.Score;
			this.UserDisplayName = other.UserDisplayName;
		}
	}
}
