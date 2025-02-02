using System;
using TaleWorlds.Engine;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI;
using TaleWorlds.MountAndBlade.View.Tableaus;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.TextureProviders
{
	// Token: 0x02000017 RID: 23
	public class BrightnessDemoTextureProvider : TextureProvider
	{
		// Token: 0x1700001B RID: 27
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x00006AE8 File Offset: 0x00004CE8
		public int DemoType
		{
			set
			{
				this._sceneTableau.SetDemoType(value);
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00006AF6 File Offset: 0x00004CF6
		public BrightnessDemoTextureProvider()
		{
			this._sceneTableau = new BrightnessDemoTableau();
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00006B0C File Offset: 0x00004D0C
		private void CheckTexture()
		{
			if (this._sceneTableau != null)
			{
				if (this._texture != this._sceneTableau.Texture)
				{
					this._texture = this._sceneTableau.Texture;
					if (this._texture != null)
					{
						this.wrappedTexture = new EngineTexture(this._texture);
						this._providedTexture = new TaleWorlds.TwoDimension.Texture(this.wrappedTexture);
						return;
					}
					this._providedTexture = null;
					return;
				}
			}
			else
			{
				this._providedTexture = null;
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00006B8A File Offset: 0x00004D8A
		public override void Tick(float dt)
		{
			base.Tick(dt);
			this.CheckTexture();
			BrightnessDemoTableau sceneTableau = this._sceneTableau;
			if (sceneTableau == null)
			{
				return;
			}
			sceneTableau.OnTick(dt);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00006BAA File Offset: 0x00004DAA
		public override void Clear(bool clearNextFrame)
		{
			BrightnessDemoTableau sceneTableau = this._sceneTableau;
			if (sceneTableau != null)
			{
				sceneTableau.OnFinalize();
			}
			base.Clear(clearNextFrame);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00006BC4 File Offset: 0x00004DC4
		public override void SetTargetSize(int width, int height)
		{
			base.SetTargetSize(width, height);
			this._sceneTableau.SetTargetSize(width, height);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00006BDB File Offset: 0x00004DDB
		public override TaleWorlds.TwoDimension.Texture GetTexture(TwoDimensionContext twoDimensionContext, string name)
		{
			this.CheckTexture();
			return this._providedTexture;
		}

		// Token: 0x04000088 RID: 136
		private BrightnessDemoTableau _sceneTableau;

		// Token: 0x04000089 RID: 137
		private TaleWorlds.Engine.Texture _texture;

		// Token: 0x0400008A RID: 138
		private TaleWorlds.TwoDimension.Texture _providedTexture;

		// Token: 0x0400008B RID: 139
		private EngineTexture wrappedTexture;
	}
}
