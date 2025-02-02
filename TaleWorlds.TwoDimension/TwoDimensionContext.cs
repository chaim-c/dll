using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using TaleWorlds.Library;

namespace TaleWorlds.TwoDimension
{
	// Token: 0x02000036 RID: 54
	public class TwoDimensionContext
	{
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000253 RID: 595 RVA: 0x00009469 File Offset: 0x00007669
		public float Width
		{
			get
			{
				return this.Platform.Width;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000254 RID: 596 RVA: 0x00009476 File Offset: 0x00007676
		public float Height
		{
			get
			{
				return this.Platform.Height;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000255 RID: 597 RVA: 0x00009483 File Offset: 0x00007683
		// (set) Token: 0x06000256 RID: 598 RVA: 0x0000948B File Offset: 0x0000768B
		public ITwoDimensionPlatform Platform { get; private set; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000257 RID: 599 RVA: 0x00009494 File Offset: 0x00007694
		// (set) Token: 0x06000258 RID: 600 RVA: 0x0000949C File Offset: 0x0000769C
		public ITwoDimensionResourceContext ResourceContext { get; private set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000259 RID: 601 RVA: 0x000094A5 File Offset: 0x000076A5
		// (set) Token: 0x0600025A RID: 602 RVA: 0x000094AD File Offset: 0x000076AD
		public ResourceDepot ResourceDepot { get; private set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600025B RID: 603 RVA: 0x000094B6 File Offset: 0x000076B6
		public bool ScissorTestEnabled
		{
			get
			{
				return this._scissorTestEnabled;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600025C RID: 604 RVA: 0x000094BE File Offset: 0x000076BE
		public bool CircularMaskEnabled
		{
			get
			{
				return this._circularMaskEnabled;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600025D RID: 605 RVA: 0x000094C6 File Offset: 0x000076C6
		public Vector2 CircularMaskCenter
		{
			get
			{
				return this._circularMaskCenter;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600025E RID: 606 RVA: 0x000094CE File Offset: 0x000076CE
		public float CircularMaskRadius
		{
			get
			{
				return this._circularMaskRadius;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600025F RID: 607 RVA: 0x000094D6 File Offset: 0x000076D6
		public float CircularMaskSmoothingRadius
		{
			get
			{
				return this._circularMaskSmoothingRadius;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000260 RID: 608 RVA: 0x000094DE File Offset: 0x000076DE
		public ScissorTestInfo CurrentScissor
		{
			get
			{
				return this._scissorStack[this._scissorStack.Count - 1];
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000261 RID: 609 RVA: 0x000094F8 File Offset: 0x000076F8
		public bool IsDebugModeEnabled
		{
			get
			{
				return this.Platform.IsDebugModeEnabled();
			}
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00009505 File Offset: 0x00007705
		public TwoDimensionContext(ITwoDimensionPlatform platform, ITwoDimensionResourceContext resourceContext, ResourceDepot resourceDepot)
		{
			this.ResourceDepot = resourceDepot;
			this._scissorStack = new List<ScissorTestInfo>();
			this._scissorTestEnabled = false;
			this.Platform = platform;
			this.ResourceContext = resourceContext;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00009534 File Offset: 0x00007734
		public void PlaySound(string soundName)
		{
			this.Platform.PlaySound(soundName);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00009542 File Offset: 0x00007742
		public void CreateSoundEvent(string soundName)
		{
			this.Platform.CreateSoundEvent(soundName);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00009550 File Offset: 0x00007750
		public void StopAndRemoveSoundEvent(string soundName)
		{
			this.Platform.StopAndRemoveSoundEvent(soundName);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000955E File Offset: 0x0000775E
		public void PlaySoundEvent(string soundName)
		{
			this.Platform.PlaySoundEvent(soundName);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000956C File Offset: 0x0000776C
		public void Draw(float x, float y, Material material, DrawObject2D drawObject2D, int layer = 0)
		{
			this.Platform.Draw(x, y, material, drawObject2D, layer);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00009580 File Offset: 0x00007780
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
				DrawObject2D drawObject2D2 = new DrawObject2D(drawObject2D.Topology, drawObject2D.Vertices.ToArray<float>(), drawObject2D.TextureCoordinates, drawObject2D.Indices, drawObject2D.VertexCount);
				this.Draw(x, y, material, drawObject2D2, 0);
			}
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000961C File Offset: 0x0000781C
		public void BeginDebugPanel(string panelTitle)
		{
			this.Platform.BeginDebugPanel(panelTitle);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000962A File Offset: 0x0000782A
		public void EndDebugPanel()
		{
			this.Platform.EndDebugPanel();
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00009637 File Offset: 0x00007837
		public void DrawDebugText(string text)
		{
			this.Platform.DrawDebugText(text);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00009645 File Offset: 0x00007845
		public bool DrawDebugTreeNode(string text)
		{
			return this.Platform.DrawDebugTreeNode(text);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00009653 File Offset: 0x00007853
		public void PopDebugTreeNode()
		{
			this.Platform.PopDebugTreeNode();
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00009660 File Offset: 0x00007860
		public void DrawCheckbox(string label, ref bool isChecked)
		{
			this.Platform.DrawCheckbox(label, ref isChecked);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000966F File Offset: 0x0000786F
		public bool IsDebugItemHovered()
		{
			return this.Platform.IsDebugItemHovered();
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000967C File Offset: 0x0000787C
		public Texture LoadTexture(string name)
		{
			return this.ResourceContext.LoadTexture(this.ResourceDepot, name);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00009690 File Offset: 0x00007890
		public void SetCircualMask(Vector2 position, float radius, float smoothingRadius)
		{
			this._circularMaskEnabled = true;
			this._circularMaskCenter = position;
			this._circularMaskRadius = radius;
			this._circularMaskSmoothingRadius = smoothingRadius;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x000096AE File Offset: 0x000078AE
		public void ClearCircualMask()
		{
			this._circularMaskEnabled = false;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x000096B8 File Offset: 0x000078B8
		public void DrawSprite(Sprite sprite, SimpleMaterial material, float x, float y, float scale, float width, float height, bool horizontalFlip, bool verticalFlip)
		{
			DrawObject2D arrays = sprite.GetArrays(new SpriteDrawData(0f, 0f, scale, width, height, horizontalFlip, verticalFlip));
			material.Texture = sprite.Texture;
			if (this._circularMaskEnabled)
			{
				material.CircularMaskingEnabled = true;
				material.CircularMaskingCenter = this._circularMaskCenter;
				material.CircularMaskingRadius = this._circularMaskRadius;
				material.CircularMaskingSmoothingRadius = this._circularMaskSmoothingRadius;
			}
			this.Draw(x, y, material, arrays, 0);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00009730 File Offset: 0x00007930
		public void SetScissor(int x, int y, int width, int height)
		{
			ScissorTestInfo scissor = new ScissorTestInfo(x, y, width, height);
			this.SetScissor(scissor);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00009750 File Offset: 0x00007950
		public void SetScissor(ScissorTestInfo scissor)
		{
			this.Platform.SetScissor(scissor);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000975E File Offset: 0x0000795E
		public void ResetScissor()
		{
			this.Platform.ResetScissor();
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000976C File Offset: 0x0000796C
		public void PushScissor(int x, int y, int width, int height)
		{
			ScissorTestInfo scissorTestInfo = new ScissorTestInfo(x, y, width, height);
			if (this._scissorStack.Count > 0)
			{
				ScissorTestInfo scissorTestInfo2 = this._scissorStack[this._scissorStack.Count - 1];
				int num = scissorTestInfo2.X + scissorTestInfo2.Width;
				int num2 = scissorTestInfo2.Y + scissorTestInfo2.Height;
				int num3 = x + width;
				int num4 = y + height;
				scissorTestInfo.X = ((scissorTestInfo.X > scissorTestInfo2.X) ? scissorTestInfo.X : scissorTestInfo2.X);
				scissorTestInfo.Y = ((scissorTestInfo.Y > scissorTestInfo2.Y) ? scissorTestInfo.Y : scissorTestInfo2.Y);
				int num5 = (num > num3) ? num3 : num;
				int num6 = (num2 > num4) ? num4 : num2;
				scissorTestInfo.Width = num5 - scissorTestInfo.X;
				scissorTestInfo.Height = num6 - scissorTestInfo.Y;
			}
			this._scissorStack.Add(scissorTestInfo);
			this._scissorTestEnabled = true;
			this.Platform.SetScissor(scissorTestInfo);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00009874 File Offset: 0x00007A74
		public void PopScissor()
		{
			this._scissorStack.RemoveAt(this._scissorStack.Count - 1);
			if (this._scissorTestEnabled)
			{
				if (this._scissorStack.Count > 0)
				{
					ScissorTestInfo scissor = this._scissorStack[this._scissorStack.Count - 1];
					this.Platform.SetScissor(scissor);
					return;
				}
				this.Platform.ResetScissor();
				this._scissorTestEnabled = false;
			}
		}

		// Token: 0x04000137 RID: 311
		private List<ScissorTestInfo> _scissorStack;

		// Token: 0x04000138 RID: 312
		private bool _scissorTestEnabled;

		// Token: 0x04000139 RID: 313
		private bool _circularMaskEnabled;

		// Token: 0x0400013A RID: 314
		private float _circularMaskRadius;

		// Token: 0x0400013B RID: 315
		private float _circularMaskSmoothingRadius;

		// Token: 0x0400013C RID: 316
		private Vector2 _circularMaskCenter;
	}
}
