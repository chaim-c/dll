using System;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x0200000D RID: 13
	[EngineStruct("rglBounding_box", false)]
	public struct BoundingBox
	{
		// Token: 0x04000020 RID: 32
		[CustomEngineStructMemberData("box_min_")]
		public Vec3 min;

		// Token: 0x04000021 RID: 33
		[CustomEngineStructMemberData("box_max_")]
		public Vec3 max;

		// Token: 0x04000022 RID: 34
		[CustomEngineStructMemberData("box_center_")]
		public Vec3 center;

		// Token: 0x04000023 RID: 35
		[CustomEngineStructMemberData("radius_")]
		public float radius;
	}
}
