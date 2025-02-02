using System;
using System.Collections.Generic;

namespace TaleWorlds.TwoDimension
{
	// Token: 0x02000032 RID: 50
	public class SpriteNineRegion : Sprite
	{
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000215 RID: 533 RVA: 0x0000883D File Offset: 0x00006A3D
		public override Texture Texture
		{
			get
			{
				return this.BaseSprite.Texture;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000216 RID: 534 RVA: 0x0000884A File Offset: 0x00006A4A
		// (set) Token: 0x06000217 RID: 535 RVA: 0x00008852 File Offset: 0x00006A52
		public SpritePart BaseSprite { get; private set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000218 RID: 536 RVA: 0x0000885B File Offset: 0x00006A5B
		// (set) Token: 0x06000219 RID: 537 RVA: 0x00008863 File Offset: 0x00006A63
		public int LeftWidth { get; private set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600021A RID: 538 RVA: 0x0000886C File Offset: 0x00006A6C
		// (set) Token: 0x0600021B RID: 539 RVA: 0x00008874 File Offset: 0x00006A74
		public int RightWidth { get; private set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600021C RID: 540 RVA: 0x0000887D File Offset: 0x00006A7D
		// (set) Token: 0x0600021D RID: 541 RVA: 0x00008885 File Offset: 0x00006A85
		public int TopHeight { get; private set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0000888E File Offset: 0x00006A8E
		// (set) Token: 0x0600021F RID: 543 RVA: 0x00008896 File Offset: 0x00006A96
		public int BottomHeight { get; private set; }

		// Token: 0x06000220 RID: 544 RVA: 0x000088A0 File Offset: 0x00006AA0
		public SpriteNineRegion(string name, SpritePart baseSprite, int leftWidth, int rightWidth, int topHeight, int bottomHeight) : base(name, baseSprite.Width, baseSprite.Height)
		{
			this.BaseSprite = baseSprite;
			this.LeftWidth = leftWidth;
			this.RightWidth = rightWidth;
			this.TopHeight = topHeight;
			this.BottomHeight = bottomHeight;
			this._centerWidth = baseSprite.Width - leftWidth - rightWidth;
			this._centerHeight = baseSprite.Height - topHeight - bottomHeight;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000890C File Offset: 0x00006B0C
		public override float GetScaleToUse(float width, float height, float scale)
		{
			float num = 1f;
			float num2 = (float)(this.LeftWidth + this.RightWidth) * scale;
			if (width < num2)
			{
				num = width / num2;
			}
			float num3 = (float)(this.TopHeight + this.BottomHeight) * scale;
			float num4 = 1f;
			if (height < num3)
			{
				num4 = height / num3;
			}
			float num5 = (num < num4) ? num : num4;
			if (this._centerWidth == 0)
			{
				num = width / num2;
				num5 = ((num < num5) ? num5 : num);
			}
			if (this._centerHeight == 0)
			{
				num4 = height / num3;
				num5 = ((num4 < num5) ? num5 : num4);
			}
			return scale * num5;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00008998 File Offset: 0x00006B98
		protected internal override DrawObject2D GetArrays(SpriteDrawData spriteDrawData)
		{
			if (this.CachedDrawObject != null && this.CachedDrawData == spriteDrawData)
			{
				return this.CachedDrawObject;
			}
			this._outVertices = new float[32];
			this._outIndices = new uint[54];
			this._verticesStartIndex = 0;
			this._uvsStartIndex = 0;
			this._indicesStartIndex = 0U;
			this._scale = this.GetScaleToUse(spriteDrawData.Width, spriteDrawData.Height, spriteDrawData.Scale);
			this._customWidth = spriteDrawData.Width;
			this._customHeight = spriteDrawData.Height;
			this.SetVerticesData(spriteDrawData.HorizontalFlip, spriteDrawData.VerticalFlip);
			this.SetIndicesData();
			if (this._outUvs == null)
			{
				this._outUvs = new List<float[]>();
				this._outUvs.Add(new float[32]);
				this._outUvs.Add(new float[32]);
				this._outUvs.Add(new float[32]);
				this._outUvs.Add(new float[32]);
				this.CalculateTextureCoordinates(this._outUvs[this.GetUVArrayIndex(false, false)], false, false);
				this.CalculateTextureCoordinates(this._outUvs[this.GetUVArrayIndex(true, false)], true, false);
				this.CalculateTextureCoordinates(this._outUvs[this.GetUVArrayIndex(false, true)], false, true);
				this.CalculateTextureCoordinates(this._outUvs[this.GetUVArrayIndex(true, true)], true, true);
			}
			for (int i = 0; i < 16; i++)
			{
				this._outVertices[2 * i] += spriteDrawData.MapX;
				this._outVertices[2 * i + 1] += spriteDrawData.MapY;
			}
			DrawObject2D drawObject2D = new DrawObject2D(MeshTopology.Triangles, this._outVertices, this._outUvs[this.GetUVArrayIndex(spriteDrawData.HorizontalFlip, spriteDrawData.VerticalFlip)], this._outIndices, this._outIndices.Length);
			drawObject2D.DrawObjectType = DrawObjectType.NineGrid;
			this._outVertices = null;
			this._outIndices = null;
			this.CachedDrawObject = drawObject2D;
			this.CachedDrawData = spriteDrawData;
			return drawObject2D;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00008BA4 File Offset: 0x00006DA4
		private int GetUVArrayIndex(bool horizontalFlip, bool verticalFlip)
		{
			int result;
			if (horizontalFlip && verticalFlip)
			{
				result = 3;
			}
			else if (verticalFlip)
			{
				result = 2;
			}
			else if (horizontalFlip)
			{
				result = 1;
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00008BCD File Offset: 0x00006DCD
		private void SetVertexData(float x, float y)
		{
			this._outVertices[this._verticesStartIndex] = x;
			this._verticesStartIndex++;
			this._outVertices[this._verticesStartIndex] = y;
			this._verticesStartIndex++;
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00008C07 File Offset: 0x00006E07
		private void SetTextureData(float[] outUvs, float u, float v)
		{
			outUvs[this._uvsStartIndex] = u;
			this._uvsStartIndex++;
			outUvs[this._uvsStartIndex] = v;
			this._uvsStartIndex++;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00008C38 File Offset: 0x00006E38
		private void AddQuad(uint i1, uint i2, uint i3, uint i4)
		{
			this._outIndices[(int)this._indicesStartIndex] = i1;
			this._indicesStartIndex += 1U;
			this._outIndices[(int)this._indicesStartIndex] = i2;
			this._indicesStartIndex += 1U;
			this._outIndices[(int)this._indicesStartIndex] = i4;
			this._indicesStartIndex += 1U;
			this._outIndices[(int)this._indicesStartIndex] = i1;
			this._indicesStartIndex += 1U;
			this._outIndices[(int)this._indicesStartIndex] = i4;
			this._indicesStartIndex += 1U;
			this._outIndices[(int)this._indicesStartIndex] = i3;
			this._indicesStartIndex += 1U;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00008CF0 File Offset: 0x00006EF0
		private void SetIndicesData()
		{
			this.AddQuad(0U, 1U, 4U, 5U);
			this.AddQuad(1U, 2U, 5U, 6U);
			this.AddQuad(2U, 3U, 6U, 7U);
			this.AddQuad(4U, 5U, 8U, 9U);
			this.AddQuad(5U, 6U, 9U, 10U);
			this.AddQuad(6U, 7U, 10U, 11U);
			this.AddQuad(8U, 9U, 12U, 13U);
			this.AddQuad(9U, 10U, 13U, 14U);
			this.AddQuad(10U, 11U, 14U, 15U);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00008D68 File Offset: 0x00006F68
		private void SetVerticesData(bool horizontalFlip, bool verticalFlip)
		{
			float num = (float)this.LeftWidth;
			float num2 = (float)this.RightWidth;
			float num3 = (float)this.TopHeight;
			float num4 = (float)this.BottomHeight;
			if (horizontalFlip)
			{
				num = (float)this.RightWidth;
				num2 = (float)this.LeftWidth;
			}
			if (verticalFlip)
			{
				num3 = (float)this.BottomHeight;
				num4 = (float)this.TopHeight;
			}
			float y = 0f;
			float y2 = num3 * this._scale;
			float y3 = this._customHeight - num4 * this._scale;
			float customHeight = this._customHeight;
			float x = 0f;
			float x2 = num * this._scale;
			float x3 = this._customWidth - num2 * this._scale;
			float customWidth = this._customWidth;
			this.SetVertexData(x, y);
			this.SetVertexData(x2, y);
			this.SetVertexData(x3, y);
			this.SetVertexData(customWidth, y);
			this.SetVertexData(x, y2);
			this.SetVertexData(x2, y2);
			this.SetVertexData(x3, y2);
			this.SetVertexData(customWidth, y2);
			this.SetVertexData(x, y3);
			this.SetVertexData(x2, y3);
			this.SetVertexData(x3, y3);
			this.SetVertexData(customWidth, y3);
			this.SetVertexData(x, customHeight);
			this.SetVertexData(x2, customHeight);
			this.SetVertexData(x3, customHeight);
			this.SetVertexData(customWidth, customHeight);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00008EB0 File Offset: 0x000070B0
		private void CalculateTextureCoordinates(float[] outUvs, bool horizontalFlip, bool verticalFlip)
		{
			this._uvsStartIndex = 0;
			float minU = this.BaseSprite.MinU;
			float minV = this.BaseSprite.MinV;
			float maxU = this.BaseSprite.MaxU;
			float maxV = this.BaseSprite.MaxV;
			float u = minU;
			float u2 = minU + (maxU - minU) * ((float)this.LeftWidth / (float)base.Width);
			float u3 = minU + (maxU - minU) * ((float)(this.LeftWidth + this._centerWidth) / (float)base.Width);
			float u4 = maxU;
			if (horizontalFlip)
			{
				u4 = minU;
				u3 = minU + (maxU - minU) * ((float)this.LeftWidth / (float)base.Width);
				u2 = minU + (maxU - minU) * ((float)(this.LeftWidth + this._centerWidth) / (float)base.Width);
				u = maxU;
			}
			float v = minV;
			float v2 = minV + (maxV - minV) * ((float)this.TopHeight / (float)base.Height);
			float v3 = minV + (maxV - minV) * ((float)(this.TopHeight + this._centerHeight) / (float)base.Height);
			float v4 = maxV;
			if (verticalFlip)
			{
				v4 = minV;
				v3 = minV + (maxV - minV) * ((float)this.TopHeight / (float)base.Height);
				v2 = minV + (maxV - minV) * ((float)(this.TopHeight + this._centerHeight) / (float)base.Height);
				v = maxV;
			}
			this.SetTextureData(outUvs, u, v);
			this.SetTextureData(outUvs, u2, v);
			this.SetTextureData(outUvs, u3, v);
			this.SetTextureData(outUvs, u4, v);
			this.SetTextureData(outUvs, u, v2);
			this.SetTextureData(outUvs, u2, v2);
			this.SetTextureData(outUvs, u3, v2);
			this.SetTextureData(outUvs, u4, v2);
			this.SetTextureData(outUvs, u, v3);
			this.SetTextureData(outUvs, u2, v3);
			this.SetTextureData(outUvs, u3, v3);
			this.SetTextureData(outUvs, u4, v3);
			this.SetTextureData(outUvs, u, v4);
			this.SetTextureData(outUvs, u2, v4);
			this.SetTextureData(outUvs, u3, v4);
			this.SetTextureData(outUvs, u4, v4);
		}

		// Token: 0x0400011B RID: 283
		private int _centerWidth;

		// Token: 0x0400011C RID: 284
		private int _centerHeight;

		// Token: 0x0400011D RID: 285
		private List<float[]> _outUvs;

		// Token: 0x0400011E RID: 286
		private float[] _outVertices;

		// Token: 0x0400011F RID: 287
		private uint[] _outIndices;

		// Token: 0x04000120 RID: 288
		private int _verticesStartIndex;

		// Token: 0x04000121 RID: 289
		private int _uvsStartIndex;

		// Token: 0x04000122 RID: 290
		private uint _indicesStartIndex;

		// Token: 0x04000123 RID: 291
		private float _scale;

		// Token: 0x04000124 RID: 292
		private float _customWidth;

		// Token: 0x04000125 RID: 293
		private float _customHeight;
	}
}
