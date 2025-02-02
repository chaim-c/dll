using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200068E RID: 1678
	public struct OnQueryPlayerAchievementsCompleteCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000D01 RID: 3329
		// (get) Token: 0x06002B06 RID: 11014 RVA: 0x000406BD File Offset: 0x0003E8BD
		// (set) Token: 0x06002B07 RID: 11015 RVA: 0x000406C5 File Offset: 0x0003E8C5
		public Result ResultCode { get; set; }

		// Token: 0x17000D02 RID: 3330
		// (get) Token: 0x06002B08 RID: 11016 RVA: 0x000406CE File Offset: 0x0003E8CE
		// (set) Token: 0x06002B09 RID: 11017 RVA: 0x000406D6 File Offset: 0x0003E8D6
		public object ClientData { get; set; }

		// Token: 0x17000D03 RID: 3331
		// (get) Token: 0x06002B0A RID: 11018 RVA: 0x000406DF File Offset: 0x0003E8DF
		// (set) Token: 0x06002B0B RID: 11019 RVA: 0x000406E7 File Offset: 0x0003E8E7
		public ProductUserId UserId { get; set; }

		// Token: 0x06002B0C RID: 11020 RVA: 0x000406F0 File Offset: 0x0003E8F0
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06002B0D RID: 11021 RVA: 0x0004070D File Offset: 0x0003E90D
		internal void Set(ref OnQueryPlayerAchievementsCompleteCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.UserId = other.UserId;
		}
	}
}
