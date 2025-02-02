using System;
using Galaxy.Api;

namespace TaleWorlds.PlatformService.GOG
{
	// Token: 0x0200000E RID: 14
	public class StatsAndAchievementsStoreListener : GlobalStatsAndAchievementsStoreListener
	{
		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000083 RID: 131 RVA: 0x000032B0 File Offset: 0x000014B0
		// (remove) Token: 0x06000084 RID: 132 RVA: 0x000032E8 File Offset: 0x000014E8
		public event StatsAndAchievementsStoreListener.UserStatsAndAchievementsStored OnUserStatsAndAchievementsStored;

		// Token: 0x06000085 RID: 133 RVA: 0x0000331D File Offset: 0x0000151D
		public override void OnUserStatsAndAchievementsStoreFailure(IStatsAndAchievementsStoreListener.FailureReason failureReason)
		{
			StatsAndAchievementsStoreListener.UserStatsAndAchievementsStored onUserStatsAndAchievementsStored = this.OnUserStatsAndAchievementsStored;
			if (onUserStatsAndAchievementsStored == null)
			{
				return;
			}
			onUserStatsAndAchievementsStored(false, new IStatsAndAchievementsStoreListener.FailureReason?(failureReason));
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003338 File Offset: 0x00001538
		public override void OnUserStatsAndAchievementsStoreSuccess()
		{
			StatsAndAchievementsStoreListener.UserStatsAndAchievementsStored onUserStatsAndAchievementsStored = this.OnUserStatsAndAchievementsStored;
			if (onUserStatsAndAchievementsStored == null)
			{
				return;
			}
			onUserStatsAndAchievementsStored(true, null);
		}

		// Token: 0x02000019 RID: 25
		// (Invoke) Token: 0x060000A5 RID: 165
		public delegate void UserStatsAndAchievementsStored(bool success, IStatsAndAchievementsStoreListener.FailureReason? failureReason);
	}
}
