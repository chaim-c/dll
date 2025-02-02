using System;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000097 RID: 151
	public struct WorldFrame
	{
		// Token: 0x06000B9D RID: 2973 RVA: 0x0000CCF2 File Offset: 0x0000AEF2
		public WorldFrame(Mat3 rotation, WorldPosition origin)
		{
			this.Rotation = rotation;
			this.Origin = origin;
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000B9E RID: 2974 RVA: 0x0000CD02 File Offset: 0x0000AF02
		public bool IsValid
		{
			get
			{
				return this.Origin.IsValid;
			}
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x0000CD0F File Offset: 0x0000AF0F
		public MatrixFrame ToGroundMatrixFrame()
		{
			return new MatrixFrame(this.Rotation, this.Origin.GetGroundVec3());
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x0000CD27 File Offset: 0x0000AF27
		public MatrixFrame ToNavMeshMatrixFrame()
		{
			return new MatrixFrame(this.Rotation, this.Origin.GetNavMeshVec3());
		}

		// Token: 0x040001EB RID: 491
		public Mat3 Rotation;

		// Token: 0x040001EC RID: 492
		public WorldPosition Origin;

		// Token: 0x040001ED RID: 493
		public static readonly WorldFrame Invalid = new WorldFrame(Mat3.Identity, WorldPosition.Invalid);
	}
}
