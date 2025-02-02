using System;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x0200040F RID: 1039
	public struct LeaderboardUserScore
	{
		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06001AD1 RID: 6865 RVA: 0x00027E1B File Offset: 0x0002601B
		// (set) Token: 0x06001AD2 RID: 6866 RVA: 0x00027E23 File Offset: 0x00026023
		public ProductUserId UserId { get; set; }

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06001AD3 RID: 6867 RVA: 0x00027E2C File Offset: 0x0002602C
		// (set) Token: 0x06001AD4 RID: 6868 RVA: 0x00027E34 File Offset: 0x00026034
		public int Score { get; set; }

		// Token: 0x06001AD5 RID: 6869 RVA: 0x00027E3D File Offset: 0x0002603D
		internal void Set(ref LeaderboardUserScoreInternal other)
		{
			this.UserId = other.UserId;
			this.Score = other.Score;
		}
	}
}
