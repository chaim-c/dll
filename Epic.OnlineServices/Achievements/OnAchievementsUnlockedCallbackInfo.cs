using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000682 RID: 1666
	public struct OnAchievementsUnlockedCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000CEC RID: 3308
		// (get) Token: 0x06002AB5 RID: 10933 RVA: 0x00040111 File Offset: 0x0003E311
		// (set) Token: 0x06002AB6 RID: 10934 RVA: 0x00040119 File Offset: 0x0003E319
		public object ClientData { get; set; }

		// Token: 0x17000CED RID: 3309
		// (get) Token: 0x06002AB7 RID: 10935 RVA: 0x00040122 File Offset: 0x0003E322
		// (set) Token: 0x06002AB8 RID: 10936 RVA: 0x0004012A File Offset: 0x0003E32A
		public ProductUserId UserId { get; set; }

		// Token: 0x17000CEE RID: 3310
		// (get) Token: 0x06002AB9 RID: 10937 RVA: 0x00040133 File Offset: 0x0003E333
		// (set) Token: 0x06002ABA RID: 10938 RVA: 0x0004013B File Offset: 0x0003E33B
		public Utf8String[] AchievementIds { get; set; }

		// Token: 0x06002ABB RID: 10939 RVA: 0x00040144 File Offset: 0x0003E344
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x06002ABC RID: 10940 RVA: 0x0004015F File Offset: 0x0003E35F
		internal void Set(ref OnAchievementsUnlockedCallbackInfoInternal other)
		{
			this.ClientData = other.ClientData;
			this.UserId = other.UserId;
			this.AchievementIds = other.AchievementIds;
		}
	}
}
