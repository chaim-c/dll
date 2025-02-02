using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000162 RID: 354
	[Serializable]
	public class ServerStatus
	{
		// Token: 0x17000317 RID: 791
		// (get) Token: 0x060009CD RID: 2509 RVA: 0x0000F4A9 File Offset: 0x0000D6A9
		// (set) Token: 0x060009CE RID: 2510 RVA: 0x0000F4B1 File Offset: 0x0000D6B1
		public bool IsMatchmakingEnabled { get; set; }

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x060009CF RID: 2511 RVA: 0x0000F4BA File Offset: 0x0000D6BA
		// (set) Token: 0x060009D0 RID: 2512 RVA: 0x0000F4C2 File Offset: 0x0000D6C2
		public bool IsCustomBattleEnabled { get; set; }

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x060009D1 RID: 2513 RVA: 0x0000F4CB File Offset: 0x0000D6CB
		// (set) Token: 0x060009D2 RID: 2514 RVA: 0x0000F4D3 File Offset: 0x0000D6D3
		public bool IsPlayerBasedCustomBattleEnabled { get; set; }

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x060009D3 RID: 2515 RVA: 0x0000F4DC File Offset: 0x0000D6DC
		// (set) Token: 0x060009D4 RID: 2516 RVA: 0x0000F4E4 File Offset: 0x0000D6E4
		public bool IsPremadeGameEnabled { get; set; }

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x060009D5 RID: 2517 RVA: 0x0000F4ED File Offset: 0x0000D6ED
		// (set) Token: 0x060009D6 RID: 2518 RVA: 0x0000F4F5 File Offset: 0x0000D6F5
		public bool IsTestRegionEnabled { get; set; }

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x060009D7 RID: 2519 RVA: 0x0000F4FE File Offset: 0x0000D6FE
		// (set) Token: 0x060009D8 RID: 2520 RVA: 0x0000F506 File Offset: 0x0000D706
		public Announcement Announcement { get; set; }

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x060009D9 RID: 2521 RVA: 0x0000F50F File Offset: 0x0000D70F
		public ServerNotification[] ServerNotifications { get; }

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x060009DA RID: 2522 RVA: 0x0000F517 File Offset: 0x0000D717
		// (set) Token: 0x060009DB RID: 2523 RVA: 0x0000F51F File Offset: 0x0000D71F
		public int FriendListUpdatePeriod { get; set; }

		// Token: 0x060009DC RID: 2524 RVA: 0x0000F528 File Offset: 0x0000D728
		public ServerStatus()
		{
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0000F530 File Offset: 0x0000D730
		public ServerStatus(bool isMatchmakingEnabled, bool isCustomBattleEnabled, bool isPlayerBasedCustomBattleEnabled, bool isPremadeGameEnabled, bool isTestRegionEnabled, Announcement announcement, ServerNotification[] serverNotifications, int friendListUpdatePeriod)
		{
			this.IsMatchmakingEnabled = isMatchmakingEnabled;
			this.IsCustomBattleEnabled = isCustomBattleEnabled;
			this.IsPlayerBasedCustomBattleEnabled = isPlayerBasedCustomBattleEnabled;
			this.IsPremadeGameEnabled = isPremadeGameEnabled;
			this.IsTestRegionEnabled = isTestRegionEnabled;
			this.Announcement = announcement;
			this.ServerNotifications = serverNotifications;
			this.FriendListUpdatePeriod = friendListUpdatePeriod;
		}
	}
}
