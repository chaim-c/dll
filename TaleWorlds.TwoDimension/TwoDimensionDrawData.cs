using System;

namespace TaleWorlds.TwoDimension
{
	// Token: 0x0200003A RID: 58
	internal struct TwoDimensionDrawData
	{
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000293 RID: 659 RVA: 0x00009F41 File Offset: 0x00008141
		public Rectangle Rectangle
		{
			get
			{
				return this._rectangle;
			}
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00009F4C File Offset: 0x0000814C
		public TwoDimensionDrawData(bool scissorTestEnabled, ScissorTestInfo scissorTestInfo, float x, float y, Material material, DrawObject2D drawObject2D, float width, float height)
		{
			this._scissorTestEnabled = scissorTestEnabled;
			this._scissorTestInfo = scissorTestInfo;
			this._x = x;
			this._y = y;
			this._material = material;
			this._drawObject2D = drawObject2D;
			this._rectangle = new Rectangle(this._x, this._y, width, height);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00009FA1 File Offset: 0x000081A1
		public bool IsIntersects(Rectangle rectangle)
		{
			return this._rectangle.IsCollide(rectangle);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00009FB0 File Offset: 0x000081B0
		public void DrawTo(TwoDimensionContext twoDimensionContext, int layer)
		{
			if (this._scissorTestEnabled)
			{
				twoDimensionContext.SetScissor(this._scissorTestInfo);
			}
			twoDimensionContext.Draw(this._x, this._y, this._material, this._drawObject2D, layer);
			if (this._scissorTestEnabled)
			{
				twoDimensionContext.ResetScissor();
			}
		}

		// Token: 0x0400014B RID: 331
		private bool _scissorTestEnabled;

		// Token: 0x0400014C RID: 332
		private ScissorTestInfo _scissorTestInfo;

		// Token: 0x0400014D RID: 333
		private float _x;

		// Token: 0x0400014E RID: 334
		private float _y;

		// Token: 0x0400014F RID: 335
		private Material _material;

		// Token: 0x04000150 RID: 336
		private DrawObject2D _drawObject2D;

		// Token: 0x04000151 RID: 337
		private Rectangle _rectangle;
	}
}
