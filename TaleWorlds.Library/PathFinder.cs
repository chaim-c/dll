using System;
using System.Collections.Generic;

namespace TaleWorlds.Library
{
	// Token: 0x02000073 RID: 115
	public abstract class PathFinder
	{
		// Token: 0x060003F4 RID: 1012 RVA: 0x0000D068 File Offset: 0x0000B268
		public PathFinder()
		{
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000D070 File Offset: 0x0000B270
		public virtual void Destroy()
		{
		}

		// Token: 0x060003F6 RID: 1014
		public abstract void Initialize(Vec3 bbSize);

		// Token: 0x060003F7 RID: 1015
		public abstract bool FindPath(Vec3 wSource, Vec3 wDestination, List<Vec3> path, float craftWidth = 5f);

		// Token: 0x04000129 RID: 297
		public static float BuildingCost = 5000f;

		// Token: 0x0400012A RID: 298
		public static float WaterCost = 400f;

		// Token: 0x0400012B RID: 299
		public static float ShallowWaterCost = 100f;
	}
}
