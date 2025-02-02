using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000135 RID: 309
	public enum CustomGameJoinResponse
	{
		// Token: 0x0400033B RID: 827
		Success,
		// Token: 0x0400033C RID: 828
		IncorrectPlayerState,
		// Token: 0x0400033D RID: 829
		ServerCapacityIsFull,
		// Token: 0x0400033E RID: 830
		ErrorOnGameServer,
		// Token: 0x0400033F RID: 831
		GameServerAccessError,
		// Token: 0x04000340 RID: 832
		CustomGameServerNotAvailable,
		// Token: 0x04000341 RID: 833
		CustomGameServerFinishing,
		// Token: 0x04000342 RID: 834
		IncorrectPassword,
		// Token: 0x04000343 RID: 835
		PlayerBanned,
		// Token: 0x04000344 RID: 836
		HostReplyTimedOut,
		// Token: 0x04000345 RID: 837
		NoPlayerDataFound,
		// Token: 0x04000346 RID: 838
		UnspecifiedError,
		// Token: 0x04000347 RID: 839
		NoPlayersCanJoin,
		// Token: 0x04000348 RID: 840
		AlreadyRequestedWaitingForServerResponse,
		// Token: 0x04000349 RID: 841
		RequesterIsNotPartyLeader,
		// Token: 0x0400034A RID: 842
		NotAllPlayersReady,
		// Token: 0x0400034B RID: 843
		NotAllPlayersModulesMatchWithServer
	}
}
