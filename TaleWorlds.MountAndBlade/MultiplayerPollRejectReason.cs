using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002AB RID: 683
	public enum MultiplayerPollRejectReason
	{
		// Token: 0x04000DBE RID: 3518
		NotEnoughPlayersToOpenPoll,
		// Token: 0x04000DBF RID: 3519
		HasOngoingPoll,
		// Token: 0x04000DC0 RID: 3520
		TooManyPollRequests,
		// Token: 0x04000DC1 RID: 3521
		KickPollTargetNotSynced,
		// Token: 0x04000DC2 RID: 3522
		Count
	}
}
