using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002DC RID: 732
	[Flags]
	public enum MultiplayerMessageFilter : ulong
	{
		// Token: 0x04000F17 RID: 3863
		None = 0UL,
		// Token: 0x04000F18 RID: 3864
		Peers = 1UL,
		// Token: 0x04000F19 RID: 3865
		Messaging = 2UL,
		// Token: 0x04000F1A RID: 3866
		Items = 4UL,
		// Token: 0x04000F1B RID: 3867
		General = 8UL,
		// Token: 0x04000F1C RID: 3868
		Equipment = 16UL,
		// Token: 0x04000F1D RID: 3869
		EquipmentDetailed = 32UL,
		// Token: 0x04000F1E RID: 3870
		Formations = 64UL,
		// Token: 0x04000F1F RID: 3871
		Agents = 128UL,
		// Token: 0x04000F20 RID: 3872
		AgentsDetailed = 256UL,
		// Token: 0x04000F21 RID: 3873
		Mission = 512UL,
		// Token: 0x04000F22 RID: 3874
		MissionDetailed = 1024UL,
		// Token: 0x04000F23 RID: 3875
		AgentAnimations = 2048UL,
		// Token: 0x04000F24 RID: 3876
		SiegeWeapons = 4096UL,
		// Token: 0x04000F25 RID: 3877
		MissionObjects = 8192UL,
		// Token: 0x04000F26 RID: 3878
		MissionObjectsDetailed = 16384UL,
		// Token: 0x04000F27 RID: 3879
		SiegeWeaponsDetailed = 32768UL,
		// Token: 0x04000F28 RID: 3880
		Orders = 65536UL,
		// Token: 0x04000F29 RID: 3881
		GameMode = 131072UL,
		// Token: 0x04000F2A RID: 3882
		Administration = 262144UL,
		// Token: 0x04000F2B RID: 3883
		Particles = 524288UL,
		// Token: 0x04000F2C RID: 3884
		RPC = 1048576UL,
		// Token: 0x04000F2D RID: 3885
		All = 4294967295UL,
		// Token: 0x04000F2E RID: 3886
		LightLogging = 139913UL,
		// Token: 0x04000F2F RID: 3887
		NormalLogging = 1979037UL,
		// Token: 0x04000F30 RID: 3888
		AllWithoutDetails = 2044639UL
	}
}
