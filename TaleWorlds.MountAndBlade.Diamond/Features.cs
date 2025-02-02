using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200011A RID: 282
	[Flags]
	public enum Features
	{
		// Token: 0x0400026A RID: 618
		None = 0,
		// Token: 0x0400026B RID: 619
		Matchmaking = 1,
		// Token: 0x0400026C RID: 620
		CustomGame = 2,
		// Token: 0x0400026D RID: 621
		Party = 4,
		// Token: 0x0400026E RID: 622
		Clan = 8,
		// Token: 0x0400026F RID: 623
		BannerlordFriendList = 16,
		// Token: 0x04000270 RID: 624
		TextChat = 32,
		// Token: 0x04000271 RID: 625
		All = -1
	}
}
