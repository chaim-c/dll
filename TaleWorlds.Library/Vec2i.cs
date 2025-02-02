using System;

namespace TaleWorlds.Library
{
	// Token: 0x0200009C RID: 156
	[Serializable]
	public struct Vec2i : IEquatable<Vec2i>
	{
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x00011D0D File Offset: 0x0000FF0D
		public int Item1
		{
			get
			{
				return this.X;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x00011D15 File Offset: 0x0000FF15
		public int Item2
		{
			get
			{
				return this.Y;
			}
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00011D1D File Offset: 0x0000FF1D
		public Vec2i(int x = 0, int y = 0)
		{
			this.X = x;
			this.Y = y;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00011D2D File Offset: 0x0000FF2D
		public static bool operator ==(Vec2i a, Vec2i b)
		{
			return a.X == b.X && a.Y == b.Y;
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00011D4D File Offset: 0x0000FF4D
		public static bool operator !=(Vec2i a, Vec2i b)
		{
			return a.X != b.X || a.Y != b.Y;
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00011D70 File Offset: 0x0000FF70
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && ((Vec2i)obj).X == this.X && ((Vec2i)obj).Y == this.Y;
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00011DC7 File Offset: 0x0000FFC7
		public bool Equals(Vec2i value)
		{
			return value.X == this.X && value.Y == this.Y;
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00011DE7 File Offset: 0x0000FFE7
		public override int GetHashCode()
		{
			return (23 * 31 + this.X.GetHashCode()) * 31 + this.Y.GetHashCode();
		}

		// Token: 0x04000193 RID: 403
		public int X;

		// Token: 0x04000194 RID: 404
		public int Y;

		// Token: 0x04000195 RID: 405
		public static readonly Vec2i Side = new Vec2i(1, 0);

		// Token: 0x04000196 RID: 406
		public static readonly Vec2i Forward = new Vec2i(0, 1);

		// Token: 0x04000197 RID: 407
		public static readonly Vec2i One = new Vec2i(1, 1);

		// Token: 0x04000198 RID: 408
		public static readonly Vec2i Zero = new Vec2i(0, 0);
	}
}
