using System;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000008 RID: 8
	[EngineStruct("rglCapsule_data", false)]
	public struct CapsuleData
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002621 File Offset: 0x00000821
		// (set) Token: 0x0600002A RID: 42 RVA: 0x0000262E File Offset: 0x0000082E
		public Vec3 P1
		{
			get
			{
				return this._globalData.P1;
			}
			set
			{
				this._globalData.P1 = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600002B RID: 43 RVA: 0x0000263C File Offset: 0x0000083C
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002649 File Offset: 0x00000849
		public Vec3 P2
		{
			get
			{
				return this._globalData.P2;
			}
			set
			{
				this._globalData.P2 = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002657 File Offset: 0x00000857
		// (set) Token: 0x0600002E RID: 46 RVA: 0x00002664 File Offset: 0x00000864
		public float Radius
		{
			get
			{
				return this._globalData.Radius;
			}
			set
			{
				this._globalData.Radius = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002672 File Offset: 0x00000872
		// (set) Token: 0x06000030 RID: 48 RVA: 0x0000267F File Offset: 0x0000087F
		internal float LocalRadius
		{
			get
			{
				return this._localData.Radius;
			}
			set
			{
				this._localData.Radius = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000031 RID: 49 RVA: 0x0000268D File Offset: 0x0000088D
		// (set) Token: 0x06000032 RID: 50 RVA: 0x0000269A File Offset: 0x0000089A
		internal Vec3 LocalP1
		{
			get
			{
				return this._localData.P1;
			}
			set
			{
				this._localData.P1 = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000033 RID: 51 RVA: 0x000026A8 File Offset: 0x000008A8
		// (set) Token: 0x06000034 RID: 52 RVA: 0x000026B5 File Offset: 0x000008B5
		internal Vec3 LocalP2
		{
			get
			{
				return this._localData.P2;
			}
			set
			{
				this._localData.P2 = value;
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000026C3 File Offset: 0x000008C3
		public CapsuleData(float radius, Vec3 p1, Vec3 p2)
		{
			this._globalData = new FtlCapsuleData(radius, p1, p2);
			this._localData = new FtlCapsuleData(radius, p1, p2);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000026E4 File Offset: 0x000008E4
		public Vec3 GetBoxMin()
		{
			return new Vec3(MathF.Min(this.P1.x, this.P2.x) - this.Radius, MathF.Min(this.P1.y, this.P2.y) - this.Radius, MathF.Min(this.P1.z, this.P2.z) - this.Radius, -1f);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002764 File Offset: 0x00000964
		public Vec3 GetBoxMax()
		{
			return new Vec3(MathF.Max(this.P1.x, this.P2.x) + this.Radius, MathF.Max(this.P1.y, this.P2.y) + this.Radius, MathF.Max(this.P1.z, this.P2.z) + this.Radius, -1f);
		}

		// Token: 0x04000008 RID: 8
		private FtlCapsuleData _globalData;

		// Token: 0x04000009 RID: 9
		private FtlCapsuleData _localData;
	}
}
