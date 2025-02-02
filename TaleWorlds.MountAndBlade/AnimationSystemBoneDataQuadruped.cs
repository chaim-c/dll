using System;
using System.Runtime.InteropServices;
using TaleWorlds.DotNet;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200018B RID: 395
	[EngineStruct("Animation_system_bone_data_quadruped", false)]
	[Serializable]
	public struct AnimationSystemBoneDataQuadruped
	{
		// Token: 0x040005DE RID: 1502
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
		public sbyte[] BoneIndicesToModifyOnSlopingGround;

		// Token: 0x040005DF RID: 1503
		public sbyte BoneIndicesToModifyOnSlopingGroundCount;

		// Token: 0x040005E0 RID: 1504
		public sbyte BodyRotationReferenceBoneIndex;

		// Token: 0x040005E1 RID: 1505
		public sbyte RiderSitBoneIndex;

		// Token: 0x040005E2 RID: 1506
		public sbyte ReinHandleBoneIndex;

		// Token: 0x040005E3 RID: 1507
		[CustomEngineStructMemberData("rein_collision_1_bone_index")]
		public sbyte ReinCollision1BoneIndex;

		// Token: 0x040005E4 RID: 1508
		[CustomEngineStructMemberData("rein_collision_2_bone_index")]
		public sbyte ReinCollision2BoneIndex;

		// Token: 0x040005E5 RID: 1509
		public sbyte ReinHeadBoneIndex;

		// Token: 0x040005E6 RID: 1510
		public sbyte ReinHeadRightAttachmentBoneIndex;

		// Token: 0x040005E7 RID: 1511
		public sbyte ReinHeadLeftAttachmentBoneIndex;

		// Token: 0x040005E8 RID: 1512
		public sbyte ReinRightHandBoneIndex;

		// Token: 0x040005E9 RID: 1513
		public sbyte ReinLeftHandBoneIndex;
	}
}
