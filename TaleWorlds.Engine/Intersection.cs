using System;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x0200000C RID: 12
	[EngineStruct("rglIntersection", false)]
	public struct Intersection
	{
		// Token: 0x06000039 RID: 57 RVA: 0x000027F1 File Offset: 0x000009F1
		public static bool DoSegmentsIntersect(Vec2 line1Start, Vec2 line1Direction, Vec2 line2Start, Vec2 line2Direction, ref Vec2 intersectionPoint)
		{
			return EngineApplicationInterface.IBodyPart.DoSegmentsIntersect(line1Start, line1Direction, line2Start, line2Direction, ref intersectionPoint);
		}

		// Token: 0x04000019 RID: 25
		[CustomEngineStructMemberData("part")]
		internal UIntPtr doNotUse;

		// Token: 0x0400001A RID: 26
		[CustomEngineStructMemberData("collided_material")]
		internal UIntPtr doNotUse2;

		// Token: 0x0400001B RID: 27
		public float Penetration;

		// Token: 0x0400001C RID: 28
		[CustomEngineStructMemberData("intersection_type")]
		public IntersectionType Type;

		// Token: 0x0400001D RID: 29
		[CustomEngineStructMemberData("intersection_details")]
		public IntersectionDetails Details;

		// Token: 0x0400001E RID: 30
		public Vec3 IntersectionPoint;

		// Token: 0x0400001F RID: 31
		public Vec3 IntersectionNormal;
	}
}
