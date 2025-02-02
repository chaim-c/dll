using System;
using TaleWorlds.DotNet;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001D6 RID: 470
	[EngineStruct("Bone_body_type_data", false)]
	public struct BoneBodyTypeData
	{
		// Token: 0x0400085A RID: 2138
		[CustomEngineStructMemberData(true)]
		public readonly BoneBodyPartType BodyPartType;

		// Token: 0x0400085B RID: 2139
		[CustomEngineStructMemberData(true)]
		public readonly sbyte Priority;

		// Token: 0x0400085C RID: 2140
		[CustomEngineStructMemberData(true)]
		public readonly SkeletonModelBoundsRecFlags DataFlags;
	}
}
