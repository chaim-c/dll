using System;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200018C RID: 396
	[EngineStruct("Animation_system_data_quadruped", false)]
	[Serializable]
	public struct AnimationSystemDataQuadruped
	{
		// Token: 0x040005EA RID: 1514
		public Vec3 ReinHandleLeftLocalPosition;

		// Token: 0x040005EB RID: 1515
		public Vec3 ReinHandleRightLocalPosition;

		// Token: 0x040005EC RID: 1516
		public string ReinSkeleton;

		// Token: 0x040005ED RID: 1517
		public string ReinCollisionBody;

		// Token: 0x040005EE RID: 1518
		public sbyte IndexOfBoneToDetectGroundSlopeFront;

		// Token: 0x040005EF RID: 1519
		public sbyte IndexOfBoneToDetectGroundSlopeBack;

		// Token: 0x040005F0 RID: 1520
		public AnimationSystemBoneDataQuadruped Bones;
	}
}
