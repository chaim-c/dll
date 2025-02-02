using System;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000009 RID: 9
	[EngineStruct("ftlSphere_data", false)]
	public struct SphereData
	{
		// Token: 0x06000038 RID: 56 RVA: 0x000027E1 File Offset: 0x000009E1
		public SphereData(float radius, Vec3 origin)
		{
			this.Radius = radius;
			this.Origin = origin;
		}

		// Token: 0x0400000A RID: 10
		public Vec3 Origin;

		// Token: 0x0400000B RID: 11
		public float Radius;
	}
}
