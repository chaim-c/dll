using System;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000022 RID: 34
	[ApplicationInterfaceBase]
	internal interface IBodyPart
	{
		// Token: 0x060001C2 RID: 450
		[EngineMethod("do_segments_intersect", false)]
		bool DoSegmentsIntersect(Vec2 line1Start, Vec2 line1Direction, Vec2 line2Start, Vec2 line2Direction, ref Vec2 intersectionPoint);
	}
}
