using System;

namespace TaleWorlds.Engine
{
	// Token: 0x02000048 RID: 72
	[Flags]
	public enum BodyFlags : uint
	{
		// Token: 0x0400007A RID: 122
		None = 0U,
		// Token: 0x0400007B RID: 123
		Disabled = 1U,
		// Token: 0x0400007C RID: 124
		NotDestructible = 2U,
		// Token: 0x0400007D RID: 125
		TwoSided = 4U,
		// Token: 0x0400007E RID: 126
		Dynamic = 8U,
		// Token: 0x0400007F RID: 127
		Moveable = 16U,
		// Token: 0x04000080 RID: 128
		DynamicConvexHull = 32U,
		// Token: 0x04000081 RID: 129
		Ladder = 64U,
		// Token: 0x04000082 RID: 130
		OnlyCollideWithRaycast = 128U,
		// Token: 0x04000083 RID: 131
		AILimiter = 256U,
		// Token: 0x04000084 RID: 132
		Barrier = 512U,
		// Token: 0x04000085 RID: 133
		Barrier3D = 1024U,
		// Token: 0x04000086 RID: 134
		HasSteps = 2048U,
		// Token: 0x04000087 RID: 135
		Ragdoll = 4096U,
		// Token: 0x04000088 RID: 136
		RagdollLimiter = 8192U,
		// Token: 0x04000089 RID: 137
		DestructibleDoor = 16384U,
		// Token: 0x0400008A RID: 138
		DroppedItem = 32768U,
		// Token: 0x0400008B RID: 139
		DoNotCollideWithRaycast = 65536U,
		// Token: 0x0400008C RID: 140
		DontTransferToPhysicsEngine = 131072U,
		// Token: 0x0400008D RID: 141
		DontCollideWithCamera = 262144U,
		// Token: 0x0400008E RID: 142
		ExcludePathSnap = 524288U,
		// Token: 0x0400008F RID: 143
		IsOpoed = 1048576U,
		// Token: 0x04000090 RID: 144
		AfterAddFlags = 1048576U,
		// Token: 0x04000091 RID: 145
		AgentOnly = 2097152U,
		// Token: 0x04000092 RID: 146
		MissileOnly = 4194304U,
		// Token: 0x04000093 RID: 147
		HasMaterial = 8388608U,
		// Token: 0x04000094 RID: 148
		BodyFlagFilter = 16777215U,
		// Token: 0x04000095 RID: 149
		CommonCollisionExcludeFlags = 6402441U,
		// Token: 0x04000096 RID: 150
		CameraCollisionRayCastExludeFlags = 6404041U,
		// Token: 0x04000097 RID: 151
		CommonCollisionExcludeFlagsForAgent = 4305289U,
		// Token: 0x04000098 RID: 152
		CommonCollisionExcludeFlagsForMissile = 2209673U,
		// Token: 0x04000099 RID: 153
		CommonCollisionExcludeFlagsForCombat = 2208137U,
		// Token: 0x0400009A RID: 154
		CommonCollisionExcludeFlagsForEditor = 2208137U,
		// Token: 0x0400009B RID: 155
		CommonFlagsThatDoNotBlocksRay = 16727871U,
		// Token: 0x0400009C RID: 156
		CommonFocusRayCastExcludeFlags = 79617U,
		// Token: 0x0400009D RID: 157
		BodyOwnerNone = 0U,
		// Token: 0x0400009E RID: 158
		BodyOwnerEntity = 16777216U,
		// Token: 0x0400009F RID: 159
		BodyOwnerTerrain = 33554432U,
		// Token: 0x040000A0 RID: 160
		BodyOwnerFlora = 67108864U,
		// Token: 0x040000A1 RID: 161
		BodyOwnerFilter = 251658240U,
		// Token: 0x040000A2 RID: 162
		IgnoreSoundOcclusion = 268435456U
	}
}
