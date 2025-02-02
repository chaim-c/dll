using System;

namespace TaleWorlds.Library
{
	// Token: 0x0200009E RID: 158
	[Serializable]
	public struct Vec3i
	{
		// Token: 0x060005B8 RID: 1464 RVA: 0x00012E41 File Offset: 0x00011041
		public Vec3i(int x = 0, int y = 0, int z = 0)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00012E58 File Offset: 0x00011058
		public static bool operator ==(Vec3i v1, Vec3i v2)
		{
			return v1.X == v2.X && v1.Y == v2.Y && v1.Z == v2.Z;
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00012E86 File Offset: 0x00011086
		public static bool operator !=(Vec3i v1, Vec3i v2)
		{
			return v1.X != v2.X || v1.Y != v2.Y || v1.Z != v2.Z;
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x00012EB7 File Offset: 0x000110B7
		public Vec3 ToVec3()
		{
			return new Vec3((float)this.X, (float)this.Y, (float)this.Z, -1f);
		}

		// Token: 0x170000A6 RID: 166
		public int this[int index]
		{
			get
			{
				if (index == 0)
				{
					return this.X;
				}
				if (index != 1)
				{
					return this.Z;
				}
				return this.Y;
			}
			set
			{
				if (index == 0)
				{
					this.X = value;
					return;
				}
				if (index == 1)
				{
					this.Y = value;
					return;
				}
				this.Z = value;
			}
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00012F15 File Offset: 0x00011115
		public static Vec3i operator *(Vec3i v, int mult)
		{
			return new Vec3i(v.X * mult, v.Y * mult, v.Z * mult);
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00012F34 File Offset: 0x00011134
		public static Vec3i operator +(Vec3i v1, Vec3i v2)
		{
			return new Vec3i(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00012F62 File Offset: 0x00011162
		public static Vec3i operator -(Vec3i v1, Vec3i v2)
		{
			return new Vec3i(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00012F90 File Offset: 0x00011190
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && (((Vec3i)obj).X == this.X && ((Vec3i)obj).Y == this.Y) && ((Vec3i)obj).Z == this.Z;
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00012FFA File Offset: 0x000111FA
		public override int GetHashCode()
		{
			return (this.X * 397 ^ this.Y) * 397 ^ this.Z;
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0001301C File Offset: 0x0001121C
		public override string ToString()
		{
			return string.Format("{0}: {1}, {2}: {3}, {4}: {5}", new object[]
			{
				"X",
				this.X,
				"Y",
				this.Y,
				"Z",
				this.Z
			});
		}

		// Token: 0x040001A3 RID: 419
		public int X;

		// Token: 0x040001A4 RID: 420
		public int Y;

		// Token: 0x040001A5 RID: 421
		public int Z;

		// Token: 0x040001A6 RID: 422
		public static readonly Vec3i Zero = new Vec3i(0, 0, 0);
	}
}
