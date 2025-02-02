using System;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000007 RID: 7
	[EngineStruct("ftlCapsule_data", false)]
	internal struct FtlCapsuleData
	{
		// Token: 0x06000026 RID: 38 RVA: 0x0000250A File Offset: 0x0000070A
		public FtlCapsuleData(float radius, Vec3 p1, Vec3 p2)
		{
			this.P1 = p1;
			this.P2 = p2;
			this.Radius = radius;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002524 File Offset: 0x00000724
		public Vec3 GetBoxMin()
		{
			return new Vec3(MathF.Min(this.P1.x, this.P2.x) - this.Radius, MathF.Min(this.P1.y, this.P2.y) - this.Radius, MathF.Min(this.P1.z, this.P2.z) - this.Radius, -1f);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000025A4 File Offset: 0x000007A4
		public Vec3 GetBoxMax()
		{
			return new Vec3(MathF.Max(this.P1.x, this.P2.x) + this.Radius, MathF.Max(this.P1.y, this.P2.y) + this.Radius, MathF.Max(this.P1.z, this.P2.z) + this.Radius, -1f);
		}

		// Token: 0x04000005 RID: 5
		public Vec3 P1;

		// Token: 0x04000006 RID: 6
		public Vec3 P2;

		// Token: 0x04000007 RID: 7
		public float Radius;
	}
}
