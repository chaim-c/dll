using System;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x02000417 RID: 1047
	public struct OnQueryLeaderboardRanksCompleteCallbackInfo : ICallbackInfo
	{
		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06001AFD RID: 6909 RVA: 0x00028099 File Offset: 0x00026299
		// (set) Token: 0x06001AFE RID: 6910 RVA: 0x000280A1 File Offset: 0x000262A1
		public Result ResultCode { get; set; }

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x06001AFF RID: 6911 RVA: 0x000280AA File Offset: 0x000262AA
		// (set) Token: 0x06001B00 RID: 6912 RVA: 0x000280B2 File Offset: 0x000262B2
		public object ClientData { get; set; }

		// Token: 0x06001B01 RID: 6913 RVA: 0x000280BC File Offset: 0x000262BC
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001B02 RID: 6914 RVA: 0x000280D9 File Offset: 0x000262D9
		internal void Set(ref OnQueryLeaderboardRanksCompleteCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
		}
	}
}
