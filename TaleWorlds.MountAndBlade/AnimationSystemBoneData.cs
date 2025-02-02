using System;
using System.Runtime.InteropServices;
using TaleWorlds.DotNet;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000189 RID: 393
	[EngineStruct("Animation_system_bone_data", false)]
	[Serializable]
	public struct AnimationSystemBoneData
	{
		// Token: 0x040005B8 RID: 1464
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
		public sbyte[] IndicesOfRagdollBonesToCheckForCorpses;

		// Token: 0x040005B9 RID: 1465
		public sbyte CountOfRagdollBonesToCheckForCorpses;

		// Token: 0x040005BA RID: 1466
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public sbyte[] RagdollFallSoundBoneIndices;

		// Token: 0x040005BB RID: 1467
		public sbyte RagdollFallSoundBoneIndexCount;

		// Token: 0x040005BC RID: 1468
		public sbyte HeadLookDirectionBoneIndex;

		// Token: 0x040005BD RID: 1469
		public sbyte SpineLowerBoneIndex;

		// Token: 0x040005BE RID: 1470
		public sbyte SpineUpperBoneIndex;

		// Token: 0x040005BF RID: 1471
		public sbyte ThoraxLookDirectionBoneIndex;

		// Token: 0x040005C0 RID: 1472
		public sbyte NeckRootBoneIndex;

		// Token: 0x040005C1 RID: 1473
		public sbyte PelvisBoneIndex;

		// Token: 0x040005C2 RID: 1474
		public sbyte RightUpperArmBoneIndex;

		// Token: 0x040005C3 RID: 1475
		public sbyte LeftUpperArmBoneIndex;

		// Token: 0x040005C4 RID: 1476
		public sbyte FallBlowDamageBoneIndex;

		// Token: 0x040005C5 RID: 1477
		[CustomEngineStructMemberData("terrain_decal_bone_0_index")]
		public sbyte TerrainDecalBone0Index;

		// Token: 0x040005C6 RID: 1478
		[CustomEngineStructMemberData("terrain_decal_bone_1_index")]
		public sbyte TerrainDecalBone1Index;
	}
}
