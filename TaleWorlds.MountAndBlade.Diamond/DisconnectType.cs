using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000117 RID: 279
	public enum DisconnectType
	{
		// Token: 0x0400025B RID: 603
		QuitFromGame,
		// Token: 0x0400025C RID: 604
		TimedOut,
		// Token: 0x0400025D RID: 605
		KickedByHost,
		// Token: 0x0400025E RID: 606
		KickedByPoll,
		// Token: 0x0400025F RID: 607
		BannedByPoll,
		// Token: 0x04000260 RID: 608
		Inactivity,
		// Token: 0x04000261 RID: 609
		DisconnectedFromLobby,
		// Token: 0x04000262 RID: 610
		GameEnded,
		// Token: 0x04000263 RID: 611
		ServerNotResponding,
		// Token: 0x04000264 RID: 612
		KickedDueToFriendlyDamage,
		// Token: 0x04000265 RID: 613
		PlayStateMismatch,
		// Token: 0x04000266 RID: 614
		Unknown
	}
}
