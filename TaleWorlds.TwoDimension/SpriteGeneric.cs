using System;
using System.Linq;

namespace TaleWorlds.TwoDimension
{
	// Token: 0x02000031 RID: 49
	public class SpriteGeneric : Sprite
	{
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000854D File Offset: 0x0000674D
		public override Texture Texture
		{
			get
			{
				return this.SpritePart.Texture;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0000855A File Offset: 0x0000675A
		// (set) Token: 0x06000211 RID: 529 RVA: 0x00008562 File Offset: 0x00006762
		public SpritePart SpritePart { get; private set; }

		// Token: 0x06000212 RID: 530 RVA: 0x0000856C File Offset: 0x0000676C
		public SpriteGeneric(string name, SpritePart spritePart) : base(name, spritePart.Width, spritePart.Height)
		{
			this.SpritePart = spritePart;
			this._vertices = new float[8];
			this._uvs = new float[8];
			this._indices = new uint[6];
			this._indices[0] = 0U;
			this._indices[1] = 1U;
			this._indices[2] = 2U;
			this._indices[3] = 0U;
			this._indices[4] = 2U;
			this._indices[5] = 3U;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x000085ED File Offset: 0x000067ED
		public override float GetScaleToUse(float width, float height, float scale)
		{
			return scale;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x000085F0 File Offset: 0x000067F0
		protected internal override DrawObject2D GetArrays(SpriteDrawData spriteDrawData)
		{
			if (this.CachedDrawObject != null && this.CachedDrawData == spriteDrawData)
			{
				return this.CachedDrawObject;
			}
			if (spriteDrawData.MapX == 0f && spriteDrawData.MapY == 0f)
			{
				float num = spriteDrawData.Width / (float)this.SpritePart.Width;
				float num2 = spriteDrawData.Height / (float)this.SpritePart.Height;
				float width = (float)base.Width * 1f * num;
				float height = (float)base.Height * 1f * num2;
				this.SpritePart.DrawSpritePart(spriteDrawData.MapX, spriteDrawData.MapY, this._vertices, this._uvs, 0, 0, 1f, spriteDrawData.Width, spriteDrawData.Height, spriteDrawData.HorizontalFlip, spriteDrawData.VerticalFlip);
				DrawObject2D drawObject2D = new DrawObject2D(MeshTopology.Triangles, this._vertices.ToArray<float>(), this._uvs.ToArray<float>(), this._indices.ToArray<uint>(), 6);
				drawObject2D.DrawObjectType = DrawObjectType.Quad;
				drawObject2D.Width = width;
				drawObject2D.Height = height;
				drawObject2D.MinU = this.SpritePart.MinU;
				drawObject2D.MaxU = this.SpritePart.MaxU;
				if (spriteDrawData.HorizontalFlip)
				{
					drawObject2D.MinU = this.SpritePart.MaxU;
					drawObject2D.MaxU = this.SpritePart.MinU;
				}
				drawObject2D.MinV = this.SpritePart.MinV;
				drawObject2D.MaxV = this.SpritePart.MaxV;
				if (spriteDrawData.VerticalFlip)
				{
					drawObject2D.MinV = this.SpritePart.MaxV;
					drawObject2D.MaxV = this.SpritePart.MinV;
				}
				this.CachedDrawData = spriteDrawData;
				this.CachedDrawObject = drawObject2D;
				return drawObject2D;
			}
			this.SpritePart.DrawSpritePart(spriteDrawData.MapX, spriteDrawData.MapY, this._vertices, this._uvs, 0, 0, 1f, spriteDrawData.Width, spriteDrawData.Height, spriteDrawData.HorizontalFlip, spriteDrawData.VerticalFlip);
			DrawObject2D drawObject2D2 = new DrawObject2D(MeshTopology.Triangles, this._vertices.ToArray<float>(), this._uvs.ToArray<float>(), this._indices.ToArray<uint>(), 6);
			drawObject2D2.DrawObjectType = DrawObjectType.Mesh;
			this.CachedDrawData = spriteDrawData;
			this.CachedDrawObject = drawObject2D2;
			return drawObject2D2;
		}

		// Token: 0x04000113 RID: 275
		private float[] _vertices;

		// Token: 0x04000114 RID: 276
		private float[] _uvs;

		// Token: 0x04000115 RID: 277
		private uint[] _indices;
	}
}
