using System;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x0200041B RID: 1051
	public struct OnQueryLeaderboardUserScoresCompleteCallbackInfo : ICallbackInfo
	{
		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06001B14 RID: 6932 RVA: 0x000281ED File Offset: 0x000263ED
		// (set) Token: 0x06001B15 RID: 6933 RVA: 0x000281F5 File Offset: 0x000263F5
		public Result ResultCode { get; set; }

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06001B16 RID: 6934 RVA: 0x000281FE File Offset: 0x000263FE
		// (set) Token: 0x06001B17 RID: 6935 RVA: 0x00028206 File Offset: 0x00026406
		public object ClientData { get; set; }

		// Token: 0x06001B18 RID: 6936 RVA: 0x00028210 File Offset: 0x00026410
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001B19 RID: 6937 RVA: 0x0002822D File Offset: 0x0002642D
		internal void Set(ref OnQueryLeaderboardUserScoresCompleteCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
		}
	}
}
