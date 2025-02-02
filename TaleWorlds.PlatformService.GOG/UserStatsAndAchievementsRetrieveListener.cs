using System;
using Galaxy.Api;

namespace TaleWorlds.PlatformService.GOG
{
	// Token: 0x0200000D RID: 13
	public class UserStatsAndAchievementsRetrieveListener : GlobalUserStatsAndAchievementsRetrieveListener
	{
		// Token: 0x14000009 RID: 9
		// (add) Token: 0x0600007E RID: 126 RVA: 0x000031F4 File Offset: 0x000013F4
		// (remove) Token: 0x0600007F RID: 127 RVA: 0x0000322C File Offset: 0x0000142C
		public event UserStatsAndAchievementsRetrieveListener.UserStatsAndAchievementsRetrieved OnUserStatsAndAchievementsRetrieved;

		// Token: 0x06000080 RID: 128 RVA: 0x00003264 File Offset: 0x00001464
		public override void OnUserStatsAndAchievementsRetrieveSuccess(GalaxyID userID)
		{
			UserStatsAndAchievementsRetrieveListener.UserStatsAndAchievementsRetrieved onUserStatsAndAchievementsRetrieved = this.OnUserStatsAndAchievementsRetrieved;
			if (onUserStatsAndAchievementsRetrieved == null)
			{
				return;
			}
			onUserStatsAndAchievementsRetrieved(userID, true, null);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000328C File Offset: 0x0000148C
		public override void OnUserStatsAndAchievementsRetrieveFailure(GalaxyID userID, IUserStatsAndAchievementsRetrieveListener.FailureReason failureReason)
		{
			UserStatsAndAchievementsRetrieveListener.UserStatsAndAchievementsRetrieved onUserStatsAndAchievementsRetrieved = this.OnUserStatsAndAchievementsRetrieved;
			if (onUserStatsAndAchievementsRetrieved == null)
			{
				return;
			}
			onUserStatsAndAchievementsRetrieved(userID, false, new IUserStatsAndAchievementsRetrieveListener.FailureReason?(failureReason));
		}

		// Token: 0x02000018 RID: 24
		// (Invoke) Token: 0x060000A1 RID: 161
		public delegate void UserStatsAndAchievementsRetrieved(GalaxyID userID, bool success, IUserStatsAndAchievementsRetrieveListener.FailureReason? failureReason);
	}
}
