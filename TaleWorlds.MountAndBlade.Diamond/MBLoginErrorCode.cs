using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200013F RID: 319
	public enum MBLoginErrorCode
	{
		// Token: 0x0400036B RID: 875
		None,
		// Token: 0x0400036C RID: 876
		CouldNotLogin,
		// Token: 0x0400036D RID: 877
		VersionMismatch,
		// Token: 0x0400036E RID: 878
		IncorrectPassword,
		// Token: 0x0400036F RID: 879
		FamilyShareNotAllowed,
		// Token: 0x04000370 RID: 880
		BannedFromGame,
		// Token: 0x04000371 RID: 881
		NoAuthenticationToken,
		// Token: 0x04000372 RID: 882
		AuthTokenExpired,
		// Token: 0x04000373 RID: 883
		BannedFromHostingServers,
		// Token: 0x04000374 RID: 884
		CustomBattleServerIncompatibleVersion,
		// Token: 0x04000375 RID: 885
		ReachedMaxNumberofCustomBattleServers,
		// Token: 0x04000376 RID: 886
		CouldNotDestroyOldSession
	}
}
