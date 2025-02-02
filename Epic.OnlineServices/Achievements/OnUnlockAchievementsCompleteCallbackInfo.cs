using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000692 RID: 1682
	public struct OnUnlockAchievementsCompleteCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000D08 RID: 3336
		// (get) Token: 0x06002B21 RID: 11041 RVA: 0x0004088A File Offset: 0x0003EA8A
		// (set) Token: 0x06002B22 RID: 11042 RVA: 0x00040892 File Offset: 0x0003EA92
		public Result ResultCode { get; set; }

		// Token: 0x17000D09 RID: 3337
		// (get) Token: 0x06002B23 RID: 11043 RVA: 0x0004089B File Offset: 0x0003EA9B
		// (set) Token: 0x06002B24 RID: 11044 RVA: 0x000408A3 File Offset: 0x0003EAA3
		public object ClientData { get; set; }

		// Token: 0x17000D0A RID: 3338
		// (get) Token: 0x06002B25 RID: 11045 RVA: 0x000408AC File Offset: 0x0003EAAC
		// (set) Token: 0x06002B26 RID: 11046 RVA: 0x000408B4 File Offset: 0x0003EAB4
		public ProductUserId UserId { get; set; }

		// Token: 0x17000D0B RID: 3339
		// (get) Token: 0x06002B27 RID: 11047 RVA: 0x000408BD File Offset: 0x0003EABD
		// (set) Token: 0x06002B28 RID: 11048 RVA: 0x000408C5 File Offset: 0x0003EAC5
		public uint AchievementsCount { get; set; }

		// Token: 0x06002B29 RID: 11049 RVA: 0x000408D0 File Offset: 0x0003EAD0
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06002B2A RID: 11050 RVA: 0x000408ED File Offset: 0x0003EAED
		internal void Set(ref OnUnlockAchievementsCompleteCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.UserId = other.UserId;
			this.AchievementsCount = other.AchievementsCount;
		}
	}
}
