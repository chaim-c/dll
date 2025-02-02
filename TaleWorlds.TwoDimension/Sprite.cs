using System;

namespace TaleWorlds.TwoDimension
{
	// Token: 0x0200002D RID: 45
	public abstract class Sprite
	{
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001D7 RID: 471
		public abstract Texture Texture { get; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00007912 File Offset: 0x00005B12
		// (set) Token: 0x060001D9 RID: 473 RVA: 0x0000791A File Offset: 0x00005B1A
		public string Name { get; private set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00007923 File Offset: 0x00005B23
		// (set) Token: 0x060001DB RID: 475 RVA: 0x0000792B File Offset: 0x00005B2B
		public int Width { get; private set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00007934 File Offset: 0x00005B34
		// (set) Token: 0x060001DD RID: 477 RVA: 0x0000793C File Offset: 0x00005B3C
		public int Height { get; private set; }

		// Token: 0x060001DE RID: 478 RVA: 0x00007945 File Offset: 0x00005B45
		protected Sprite(string name, int width, int height)
		{
			this.Name = name;
			this.Width = width;
			this.Height = height;
			this.CachedDrawObject = null;
		}

		// Token: 0x060001DF RID: 479
		public abstract float GetScaleToUse(float width, float height, float scale);

		// Token: 0x060001E0 RID: 480
		protected internal abstract DrawObject2D GetArrays(SpriteDrawData spriteDrawData);

		// Token: 0x060001E1 RID: 481 RVA: 0x00007969 File Offset: 0x00005B69
		public override string ToString()
		{
			if (string.IsNullOrEmpty(this.Name))
			{
				return base.ToString();
			}
			return this.Name;
		}

		// Token: 0x040000F9 RID: 249
		protected DrawObject2D CachedDrawObject;

		// Token: 0x040000FA RID: 250
		protected SpriteDrawData CachedDrawData;
	}
}
