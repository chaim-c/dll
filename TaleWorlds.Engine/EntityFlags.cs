using System;

namespace TaleWorlds.Engine
{
	// Token: 0x02000046 RID: 70
	[Flags]
	public enum EntityFlags : uint
	{
		// Token: 0x04000059 RID: 89
		ForceLodMask = 240U,
		// Token: 0x0400005A RID: 90
		ForceLodBits = 4U,
		// Token: 0x0400005B RID: 91
		AnimateWhenVisible = 256U,
		// Token: 0x0400005C RID: 92
		NoOcclusionCulling = 512U,
		// Token: 0x0400005D RID: 93
		IsHelper = 1024U,
		// Token: 0x0400005E RID: 94
		ComputePerComponentLod = 2048U,
		// Token: 0x0400005F RID: 95
		DoesNotAffectParentsLocalBb = 4096U,
		// Token: 0x04000060 RID: 96
		ForceAsStatic = 8192U,
		// Token: 0x04000061 RID: 97
		SendInitCallback = 16384U,
		// Token: 0x04000062 RID: 98
		PhysicsDisabled = 32768U,
		// Token: 0x04000063 RID: 99
		AlignToTerrain = 65536U,
		// Token: 0x04000064 RID: 100
		DontSaveToScene = 131072U,
		// Token: 0x04000065 RID: 101
		RecordToSceneReplay = 262144U,
		// Token: 0x04000066 RID: 102
		GroupMeshesAfterLod4 = 524288U,
		// Token: 0x04000067 RID: 103
		SmoothLodTransitions = 1048576U,
		// Token: 0x04000068 RID: 104
		DontCheckHandness = 2097152U,
		// Token: 0x04000069 RID: 105
		NotAffectedBySeason = 4194304U,
		// Token: 0x0400006A RID: 106
		DontTickChildren = 8388608U,
		// Token: 0x0400006B RID: 107
		WaitUntilReady = 16777216U,
		// Token: 0x0400006C RID: 108
		NonModifiableFromEditor = 33554432U,
		// Token: 0x0400006D RID: 109
		DeferredParallelFrameSetup = 67108864U,
		// Token: 0x0400006E RID: 110
		PerComponentVisibility = 134217728U,
		// Token: 0x0400006F RID: 111
		Ignore = 268435456U,
		// Token: 0x04000070 RID: 112
		DoNotTick = 536870912U,
		// Token: 0x04000071 RID: 113
		DoNotRenderToEnvmap = 1073741824U,
		// Token: 0x04000072 RID: 114
		AlignRotationToTerrain = 2147483648U
	}
}
