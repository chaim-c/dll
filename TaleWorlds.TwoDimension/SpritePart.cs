using System;
using TaleWorlds.Library;

namespace TaleWorlds.TwoDimension
{
	// Token: 0x02000033 RID: 51
	public class SpritePart
	{
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00009096 File Offset: 0x00007296
		// (set) Token: 0x0600022B RID: 555 RVA: 0x0000909E File Offset: 0x0000729E
		public string Name { get; private set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600022C RID: 556 RVA: 0x000090A7 File Offset: 0x000072A7
		// (set) Token: 0x0600022D RID: 557 RVA: 0x000090AF File Offset: 0x000072AF
		public int Width { get; private set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600022E RID: 558 RVA: 0x000090B8 File Offset: 0x000072B8
		// (set) Token: 0x0600022F RID: 559 RVA: 0x000090C0 File Offset: 0x000072C0
		public int Height { get; private set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000230 RID: 560 RVA: 0x000090C9 File Offset: 0x000072C9
		// (set) Token: 0x06000231 RID: 561 RVA: 0x000090D1 File Offset: 0x000072D1
		public int SheetID { get; set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000232 RID: 562 RVA: 0x000090DA File Offset: 0x000072DA
		// (set) Token: 0x06000233 RID: 563 RVA: 0x000090E2 File Offset: 0x000072E2
		public int SheetX { get; set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000234 RID: 564 RVA: 0x000090EB File Offset: 0x000072EB
		// (set) Token: 0x06000235 RID: 565 RVA: 0x000090F3 File Offset: 0x000072F3
		public int SheetY { get; set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000236 RID: 566 RVA: 0x000090FC File Offset: 0x000072FC
		// (set) Token: 0x06000237 RID: 567 RVA: 0x00009104 File Offset: 0x00007304
		public float MinU { get; private set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000238 RID: 568 RVA: 0x0000910D File Offset: 0x0000730D
		// (set) Token: 0x06000239 RID: 569 RVA: 0x00009115 File Offset: 0x00007315
		public float MinV { get; private set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0000911E File Offset: 0x0000731E
		// (set) Token: 0x0600023B RID: 571 RVA: 0x00009126 File Offset: 0x00007326
		public float MaxU { get; private set; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0000912F File Offset: 0x0000732F
		// (set) Token: 0x0600023D RID: 573 RVA: 0x00009137 File Offset: 0x00007337
		public float MaxV { get; private set; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600023E RID: 574 RVA: 0x00009140 File Offset: 0x00007340
		// (set) Token: 0x0600023F RID: 575 RVA: 0x00009148 File Offset: 0x00007348
		public int SheetWidth { get; private set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000240 RID: 576 RVA: 0x00009151 File Offset: 0x00007351
		// (set) Token: 0x06000241 RID: 577 RVA: 0x00009159 File Offset: 0x00007359
		public int SheetHeight { get; private set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000242 RID: 578 RVA: 0x00009164 File Offset: 0x00007364
		public Texture Texture
		{
			get
			{
				if (this._category != null && this._category.IsLoaded && this._category.SpriteSheets != null && this._category.SpriteSheets.Count >= this.SheetID)
				{
					return this._category.SpriteSheets[this.SheetID - 1];
				}
				return null;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000243 RID: 579 RVA: 0x000091C5 File Offset: 0x000073C5
		public SpriteCategory Category
		{
			get
			{
				return this._category;
			}
		}

		// Token: 0x06000244 RID: 580 RVA: 0x000091CD File Offset: 0x000073CD
		public SpritePart(string name, SpriteCategory category, int width, int height)
		{
			this.Name = name;
			this.Width = width;
			this.Height = height;
			this._category = category;
			this._category.SpriteParts.Add(this);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00009204 File Offset: 0x00007404
		public void UpdateInitValues()
		{
			Vec2i vec2i = this._category.SheetSizes[this.SheetID - 1];
			this.SheetWidth = vec2i.X;
			this.SheetHeight = vec2i.Y;
			double num = 1.0 / (double)this.SheetWidth;
			double num2 = 1.0 / (double)this.SheetHeight;
			double num3 = (double)this.SheetX * num;
			double num4 = (double)(this.SheetX + this.Width) * num;
			double num5 = (double)this.SheetY * num2;
			double num6 = (double)(this.SheetY + this.Height) * num2;
			this.MinU = (float)num3;
			this.MaxU = (float)num4;
			this.MinV = (float)num5;
			this.MaxV = (float)num6;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x000092C4 File Offset: 0x000074C4
		public void DrawSpritePart(float screenX, float screenY, float[] outVertices, float[] outUvs, int verticesStartIndex, int uvsStartIndex)
		{
			this.DrawSpritePart(screenX, screenY, outVertices, outUvs, verticesStartIndex, uvsStartIndex, 1f, (float)this.Width, (float)this.Height, false, false);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x000092F8 File Offset: 0x000074F8
		public void DrawSpritePart(float screenX, float screenY, float[] outVertices, float[] outUvs, int verticesStartIndex, int uvsStartIndex, float scale, float customWidth, float customHeight, bool horizontalFlip, bool verticalFlip)
		{
			if (this.Texture != null)
			{
				float num = customWidth / (float)this.Width;
				float num2 = customHeight / (float)this.Height;
				float num3 = (float)this.Width * scale * num;
				float num4 = (float)this.Height * scale * num2;
				outVertices[verticesStartIndex] = screenX + 0f;
				outVertices[verticesStartIndex + 1] = screenY + 0f;
				outVertices[verticesStartIndex + 2] = screenX + 0f;
				outVertices[verticesStartIndex + 3] = screenY + num4;
				outVertices[verticesStartIndex + 4] = screenX + num3;
				outVertices[verticesStartIndex + 5] = screenY + num4;
				outVertices[verticesStartIndex + 6] = screenX + num3;
				outVertices[verticesStartIndex + 7] = screenY + 0f;
				this.FillTextureCoordinates(outUvs, uvsStartIndex, horizontalFlip, verticalFlip);
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x000093A8 File Offset: 0x000075A8
		public void FillTextureCoordinates(float[] outUVs, int uvsStartIndex, bool horizontalFlip, bool verticalFlip)
		{
			float num = this.MinU;
			float num2 = this.MaxU;
			if (horizontalFlip)
			{
				num = this.MaxU;
				num2 = this.MinU;
			}
			float num3 = this.MinV;
			float num4 = this.MaxV;
			if (verticalFlip)
			{
				num3 = this.MaxV;
				num4 = this.MinV;
			}
			outUVs[uvsStartIndex] = num;
			outUVs[uvsStartIndex + 1] = num3;
			outUVs[uvsStartIndex + 2] = num;
			outUVs[uvsStartIndex + 3] = num4;
			outUVs[uvsStartIndex + 4] = num2;
			outUVs[uvsStartIndex + 5] = num4;
			outUVs[uvsStartIndex + 6] = num2;
			outUVs[uvsStartIndex + 7] = num3;
		}

		// Token: 0x04000132 RID: 306
		private SpriteCategory _category;
	}
}
