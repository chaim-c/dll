using System;
using TaleWorlds.Engine;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI;
using TaleWorlds.MountAndBlade.View.Tableaus;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.TextureProviders
{
	// Token: 0x0200001D RID: 29
	public class SceneTextureProvider : TextureProvider
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000123 RID: 291 RVA: 0x000078CA File Offset: 0x00005ACA
		// (set) Token: 0x06000124 RID: 292 RVA: 0x000078D2 File Offset: 0x00005AD2
		public Scene WantedScene { get; private set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000125 RID: 293 RVA: 0x000078DC File Offset: 0x00005ADC
		public bool? IsReady
		{
			get
			{
				SceneTableau sceneTableau = this._sceneTableau;
				if (sceneTableau == null)
				{
					return null;
				}
				return sceneTableau.IsReady;
			}
		}

		// Token: 0x1700004C RID: 76
		// (set) Token: 0x06000126 RID: 294 RVA: 0x00007902 File Offset: 0x00005B02
		public object Scene
		{
			set
			{
				if (value != null)
				{
					this._sceneTableau = new SceneTableau();
					this._sceneTableau.SetScene(value);
					return;
				}
				this._sceneTableau.OnFinalize();
				this._sceneTableau = null;
			}
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00007931 File Offset: 0x00005B31
		public SceneTextureProvider()
		{
			this._sceneTableau = new SceneTableau();
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00007944 File Offset: 0x00005B44
		private void CheckTexture()
		{
			if (this._sceneTableau != null)
			{
				if (this._texture != this._sceneTableau._texture)
				{
					this._texture = this._sceneTableau._texture;
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

		// Token: 0x06000129 RID: 297 RVA: 0x000079C2 File Offset: 0x00005BC2
		public override void Tick(float dt)
		{
			base.Tick(dt);
			this.CheckTexture();
			SceneTableau sceneTableau = this._sceneTableau;
			if (sceneTableau == null)
			{
				return;
			}
			sceneTableau.OnTick(dt);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x000079E2 File Offset: 0x00005BE2
		public override void SetTargetSize(int width, int height)
		{
			base.SetTargetSize(width, height);
			this._sceneTableau.SetTargetSize(width, height);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000079F9 File Offset: 0x00005BF9
		public override TaleWorlds.TwoDimension.Texture GetTexture(TwoDimensionContext twoDimensionContext, string name)
		{
			this.CheckTexture();
			return this._providedTexture;
		}

		// Token: 0x040000B0 RID: 176
		private SceneTableau _sceneTableau;

		// Token: 0x040000B1 RID: 177
		private TaleWorlds.Engine.Texture _texture;

		// Token: 0x040000B2 RID: 178
		private TaleWorlds.TwoDimension.Texture _providedTexture;

		// Token: 0x040000B3 RID: 179
		private EngineTexture wrappedTexture;
	}
}
