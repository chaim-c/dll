using System;
using System.Numerics;

namespace TaleWorlds.TwoDimension
{
	// Token: 0x0200002B RID: 43
	public struct Rectangle
	{
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001CE RID: 462 RVA: 0x0000778C File Offset: 0x0000598C
		public float Width
		{
			get
			{
				return this.X2 - this.X;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001CF RID: 463 RVA: 0x0000779B File Offset: 0x0000599B
		public float Height
		{
			get
			{
				return this.Y2 - this.Y;
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x000077AA File Offset: 0x000059AA
		public Rectangle(float x, float y, float width, float height)
		{
			this.X = x;
			this.Y = y;
			this.X2 = x + width;
			this.Y2 = y + height;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x000077CD File Offset: 0x000059CD
		public bool IsCollide(Rectangle other)
		{
			return other.X <= this.X2 && other.X2 >= this.X && other.Y <= this.Y2 && other.Y2 >= this.Y;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000780C File Offset: 0x00005A0C
		public bool IsSubRectOf(Rectangle other)
		{
			return other.X <= this.X && other.X2 >= this.X2 && other.Y <= this.Y && other.Y2 >= this.Y2;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000784B File Offset: 0x00005A4B
		public bool IsValid()
		{
			return this.Width > 0f && this.Height > 0f;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00007869 File Offset: 0x00005A69
		public bool IsPointInside(Vector2 point)
		{
			return point.X >= this.X && point.Y >= this.Y && point.X <= this.X2 && point.Y <= this.Y2;
		}

		// Token: 0x040000EE RID: 238
		public float X;

		// Token: 0x040000EF RID: 239
		public float Y;

		// Token: 0x040000F0 RID: 240
		public float X2;

		// Token: 0x040000F1 RID: 241
		public float Y2;
	}
}
