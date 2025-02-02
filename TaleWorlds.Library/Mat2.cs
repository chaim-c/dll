using System;

namespace TaleWorlds.Library
{
	// Token: 0x0200005F RID: 95
	public struct Mat2
	{
		// Token: 0x060002B2 RID: 690 RVA: 0x000082ED File Offset: 0x000064ED
		public Mat2(float sx, float sy, float fx, float fy)
		{
			this.s.x = sx;
			this.s.y = sy;
			this.f.x = fx;
			this.f.y = fy;
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00008320 File Offset: 0x00006520
		public void RotateCounterClockWise(float a)
		{
			float num;
			float num2;
			MathF.SinCos(a, out num, out num2);
			Vec2 vec = this.s * num2 + this.f * num;
			Vec2 vec2 = this.f * num2 - this.s * num;
			this.s = vec;
			this.f = vec2;
		}

		// Token: 0x040000FA RID: 250
		public Vec2 s;

		// Token: 0x040000FB RID: 251
		public Vec2 f;

		// Token: 0x040000FC RID: 252
		public static Mat2 Identity = new Mat2(1f, 0f, 0f, 1f);
	}
}
