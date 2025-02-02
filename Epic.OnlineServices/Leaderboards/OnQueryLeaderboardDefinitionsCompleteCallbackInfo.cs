using System;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x02000413 RID: 1043
	public struct OnQueryLeaderboardDefinitionsCompleteCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06001AE6 RID: 6886 RVA: 0x00027F44 File Offset: 0x00026144
		// (set) Token: 0x06001AE7 RID: 6887 RVA: 0x00027F4C File Offset: 0x0002614C
		public Result ResultCode { get; set; }

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06001AE8 RID: 6888 RVA: 0x00027F55 File Offset: 0x00026155
		// (set) Token: 0x06001AE9 RID: 6889 RVA: 0x00027F5D File Offset: 0x0002615D
		public object ClientData { get; set; }

		// Token: 0x06001AEA RID: 6890 RVA: 0x00027F68 File Offset: 0x00026168
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001AEB RID: 6891 RVA: 0x00027F85 File Offset: 0x00026185
		internal void Set(ref OnQueryLeaderboardDefinitionsCompleteCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
		}
	}
}
