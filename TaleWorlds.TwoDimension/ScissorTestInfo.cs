using System;

namespace TaleWorlds.TwoDimension
{
	// Token: 0x0200002C RID: 44
	public struct ScissorTestInfo
	{
		// Token: 0x060001D5 RID: 469 RVA: 0x000078A8 File Offset: 0x00005AA8
		public ScissorTestInfo(int x, int y, int width, int height)
		{
			this.X = x;
			this.Y = y;
			this.Width = width;
			this.Height = height;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x000078C8 File Offset: 0x00005AC8
		public static explicit operator Quad(ScissorTestInfo scissor)
		{
			return new Quad
			{
				X = scissor.X,
				Y = scissor.Y,
				Width = scissor.Width,
				Height = scissor.Height
			};
		}

		// Token: 0x040000F2 RID: 242
		public int X;

		// Token: 0x040000F3 RID: 243
		public int Y;

		// Token: 0x040000F4 RID: 244
		public int Width;

		// Token: 0x040000F5 RID: 245
		public int Height;
	}
}
