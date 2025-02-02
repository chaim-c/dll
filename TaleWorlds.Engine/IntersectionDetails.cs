using System;
using TaleWorlds.DotNet;

namespace TaleWorlds.Engine
{
	// Token: 0x0200000A RID: 10
	[EngineStruct("rglIntersection_details", false)]
	public enum IntersectionDetails : uint
	{
		// Token: 0x0400000D RID: 13
		None,
		// Token: 0x0400000E RID: 14
		Sphere,
		// Token: 0x0400000F RID: 15
		Plane,
		// Token: 0x04000010 RID: 16
		Capsule,
		// Token: 0x04000011 RID: 17
		Box,
		// Token: 0x04000012 RID: 18
		Convexmesh,
		// Token: 0x04000013 RID: 19
		Trianglemesh,
		// Token: 0x04000014 RID: 20
		Heightfield
	}
}
