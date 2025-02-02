using System;
using System.Collections.Generic;
using System.Numerics;

namespace TaleWorlds.TwoDimension
{
	// Token: 0x02000038 RID: 56
	public class TwoDimensionDrawContext
	{
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600027C RID: 636 RVA: 0x00009907 File Offset: 0x00007B07
		public bool ScissorTestEnabled
		{
			get
			{
				return this._scissorTestEnabled;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0000990F File Offset: 0x00007B0F
		public bool CircularMaskEnabled
		{
			get
			{
				return this._circularMaskEnabled;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600027E RID: 638 RVA: 0x00009917 File Offset: 0x00007B17
		public Vector2 CircularMaskCenter
		{
			get
			{
				return this._circularMaskCenter;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000991F File Offset: 0x00007B1F
		public float CircularMaskRadius
		{
			get
			{
				return this._circularMaskRadius;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000280 RID: 640 RVA: 0x00009927 File Offset: 0x00007B27
		public float CircularMaskSmoothingRadius
		{
			get
			{
				return this._circularMaskSmoothingRadius;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0000992F File Offset: 0x00007B2F
		public ScissorTestInfo CurrentScissor
		{
			get
			{
				return this._scissorStack[this._scissorStack.Count - 1];
			}
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000994C File Offset: 0x00007B4C
		public TwoDimensionDrawContext()
		{
			this._scissorStack = new List<ScissorTestInfo>();
			this._scissorTestEnabled = false;
			this._layers = new List<TwoDimensionDrawLayer>();
			this._cachedDrawObjects = new Dictionary<TwoDimensionDrawContext.SpriteCacheKey, DrawObject2D>();
		}

		// Token: 0x06000283 RID: 643 RVA: 0x000099A0 File Offset: 0x00007BA0
		public void Reset()
		{
			this._scissorStack.Clear();
			this._scissorTestEnabled = false;
			for (int i = 0; i < this._usedLayersCount; i++)
			{
				this._layers[i].Reset();
			}
			this._usedLayersCount = 0;
			this._simpleMaterialPool.ResetAll();
			this._textMaterialPool.ResetAll();
		}

		// Token: 0x06000284 RID: 644 RVA: 0x000099FE File Offset: 0x00007BFE
		public SimpleMaterial CreateSimpleMaterial()
		{
			return this._simpleMaterialPool.New();
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00009A0B File Offset: 0x00007C0B
		public TextMaterial CreateTextMaterial()
		{
			return this._textMaterialPool.New();
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00009A18 File Offset: 0x00007C18
		public void PushScissor(int x, int y, int width, int height)
		{
			ScissorTestInfo scissorTestInfo = new ScissorTestInfo(x, y, width, height);
			if (this._scissorStack.Count > 0)
			{
				ScissorTestInfo scissorTestInfo2 = this._scissorStack[this._scissorStack.Count - 1];
				if (width != -1)
				{
					int num = scissorTestInfo2.X + scissorTestInfo2.Width;
					int num2 = x + width;
					scissorTestInfo.X = ((scissorTestInfo.X > scissorTestInfo2.X) ? scissorTestInfo.X : scissorTestInfo2.X);
					int num3 = (num > num2) ? num2 : num;
					scissorTestInfo.Width = num3 - scissorTestInfo.X;
				}
				else
				{
					scissorTestInfo.X = scissorTestInfo2.X;
					scissorTestInfo.Width = scissorTestInfo2.Width;
				}
				if (height != -1)
				{
					int num4 = scissorTestInfo2.Y + scissorTestInfo2.Height;
					int num5 = y + height;
					scissorTestInfo.Y = ((scissorTestInfo.Y > scissorTestInfo2.Y) ? scissorTestInfo.Y : scissorTestInfo2.Y);
					int num6 = (num4 > num5) ? num5 : num4;
					scissorTestInfo.Height = num6 - scissorTestInfo.Y;
				}
				else
				{
					scissorTestInfo.Y = scissorTestInfo2.Y;
					scissorTestInfo.Height = scissorTestInfo2.Height;
				}
			}
			else
			{
				if (width == -1)
				{
					scissorTestInfo.Width = int.MaxValue;
				}
				if (height == -1)
				{
					scissorTestInfo.Height = int.MaxValue;
				}
			}
			this._scissorStack.Add(scissorTestInfo);
			this._scissorTestEnabled = true;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00009B77 File Offset: 0x00007D77
		public void PopScissor()
		{
			this._scissorStack.RemoveAt(this._scissorStack.Count - 1);
			if (this._scissorTestEnabled && this._scissorStack.Count == 0)
			{
				this._scissorTestEnabled = false;
			}
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00009BAD File Offset: 0x00007DAD
		public void SetCircualMask(Vector2 position, float radius, float smoothingRadius)
		{
			this._circularMaskEnabled = true;
			this._circularMaskCenter = position;
			this._circularMaskRadius = radius;
			this._circularMaskSmoothingRadius = smoothingRadius;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00009BCB File Offset: 0x00007DCB
		public void ClearCircualMask()
		{
			this._circularMaskEnabled = false;
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00009BD4 File Offset: 0x00007DD4
		public void DrawTo(TwoDimensionContext twoDimensionContext)
		{
			for (int i = 0; i < this._usedLayersCount; i++)
			{
				this._layers[i].DrawTo(twoDimensionContext, i + 1);
			}
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00009C08 File Offset: 0x00007E08
		public void DrawSprite(Sprite sprite, SimpleMaterial material, float x, float y, float scale, float width, float height, bool horizontalFlip, bool verticalFlip)
		{
			SpriteDrawData spriteDrawData = new SpriteDrawData(0f, 0f, scale, width, height, horizontalFlip, verticalFlip);
			DrawObject2D drawObject2D = null;
			TwoDimensionDrawContext.SpriteCacheKey key = new TwoDimensionDrawContext.SpriteCacheKey(sprite, spriteDrawData);
			if (!this._cachedDrawObjects.TryGetValue(key, out drawObject2D))
			{
				drawObject2D = sprite.GetArrays(spriteDrawData);
				this._cachedDrawObjects.Add(key, drawObject2D);
			}
			material.Texture = sprite.Texture;
			if (this._circularMaskEnabled)
			{
				material.CircularMaskingEnabled = true;
				material.CircularMaskingCenter = this._circularMaskCenter;
				material.CircularMaskingRadius = this._circularMaskRadius;
				material.CircularMaskingSmoothingRadius = this._circularMaskSmoothingRadius;
			}
			this.Draw(x, y, material, drawObject2D, width, height);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00009CB0 File Offset: 0x00007EB0
		public void Draw(float x, float y, Material material, DrawObject2D drawObject2D, float width, float height)
		{
			TwoDimensionDrawData drawData = new TwoDimensionDrawData(this._scissorTestEnabled, this._scissorTestEnabled ? this.CurrentScissor : default(ScissorTestInfo), x, y, material, drawObject2D, width, height);
			TwoDimensionDrawLayer twoDimensionDrawLayer = null;
			if (this._layers.Count == 0)
			{
				twoDimensionDrawLayer = new TwoDimensionDrawLayer();
				this._layers.Add(twoDimensionDrawLayer);
				this._usedLayersCount++;
			}
			else
			{
				for (int i = this._usedLayersCount - 1; i >= 0; i--)
				{
					TwoDimensionDrawLayer twoDimensionDrawLayer2 = this._layers[i];
					if (twoDimensionDrawLayer2.IsIntersects(drawData.Rectangle))
					{
						break;
					}
					twoDimensionDrawLayer = twoDimensionDrawLayer2;
				}
				if (twoDimensionDrawLayer == null)
				{
					if (this._usedLayersCount == this._layers.Count)
					{
						twoDimensionDrawLayer = new TwoDimensionDrawLayer();
						this._layers.Add(twoDimensionDrawLayer);
					}
					else
					{
						twoDimensionDrawLayer = this._layers[this._usedLayersCount];
					}
					this._usedLayersCount++;
				}
			}
			this._totalDrawCount++;
			twoDimensionDrawLayer.AddDrawData(drawData);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00009DB0 File Offset: 0x00007FB0
		public void Draw(Text text, TextMaterial material, float x, float y, float width, float height)
		{
			text.UpdateSize((int)width, (int)height);
			DrawObject2D drawObject2D = text.DrawObject2D;
			if (drawObject2D != null)
			{
				material.Texture = text.Font.FontSprite.Texture;
				material.ScaleFactor = text.FontSize;
				material.SmoothingConstant = text.Font.SmoothingConstant;
				material.Smooth = text.Font.Smooth;
				DrawObject2D drawObject2D2 = drawObject2D;
				if (material.GlowRadius > 0f || material.Blur > 0f || material.OutlineAmount > 0f)
				{
					TextMaterial textMaterial = this.CreateTextMaterial();
					textMaterial.CopyFrom(material);
					this.Draw(x, y, textMaterial, drawObject2D2, (float)((int)width), (float)((int)height));
				}
				material.GlowRadius = 0f;
				material.Blur = 0f;
				material.OutlineAmount = 0f;
				this.Draw(x, y, material, drawObject2D2, (float)((int)width), (float)((int)height));
			}
		}

		// Token: 0x0400013E RID: 318
		private List<ScissorTestInfo> _scissorStack;

		// Token: 0x0400013F RID: 319
		private bool _scissorTestEnabled;

		// Token: 0x04000140 RID: 320
		private bool _circularMaskEnabled;

		// Token: 0x04000141 RID: 321
		private float _circularMaskRadius;

		// Token: 0x04000142 RID: 322
		private float _circularMaskSmoothingRadius;

		// Token: 0x04000143 RID: 323
		private Vector2 _circularMaskCenter;

		// Token: 0x04000144 RID: 324
		private List<TwoDimensionDrawLayer> _layers;

		// Token: 0x04000145 RID: 325
		private int _usedLayersCount;

		// Token: 0x04000146 RID: 326
		private int _totalDrawCount;

		// Token: 0x04000147 RID: 327
		private Dictionary<TwoDimensionDrawContext.SpriteCacheKey, DrawObject2D> _cachedDrawObjects;

		// Token: 0x04000148 RID: 328
		private MaterialPool<SimpleMaterial> _simpleMaterialPool = new MaterialPool<SimpleMaterial>(8);

		// Token: 0x04000149 RID: 329
		private MaterialPool<TextMaterial> _textMaterialPool = new MaterialPool<TextMaterial>(8);

		// Token: 0x02000043 RID: 67
		public struct SpriteCacheKey : IEquatable<TwoDimensionDrawContext.SpriteCacheKey>
		{
			// Token: 0x060002B3 RID: 691 RVA: 0x0000A4D1 File Offset: 0x000086D1
			public SpriteCacheKey(Sprite sprite, SpriteDrawData drawData)
			{
				this.Sprite = sprite;
				this.DrawData = drawData;
			}

			// Token: 0x060002B4 RID: 692 RVA: 0x0000A4E1 File Offset: 0x000086E1
			public bool Equals(TwoDimensionDrawContext.SpriteCacheKey other)
			{
				return other.Sprite == this.Sprite && other.DrawData == this.DrawData;
			}

			// Token: 0x060002B5 RID: 693 RVA: 0x0000A504 File Offset: 0x00008704
			public override int GetHashCode()
			{
				return this.Sprite.GetHashCode() * 397 ^ this.DrawData.GetHashCode();
			}

			// Token: 0x0400016F RID: 367
			public Sprite Sprite;

			// Token: 0x04000170 RID: 368
			public SpriteDrawData DrawData;
		}
	}
}
