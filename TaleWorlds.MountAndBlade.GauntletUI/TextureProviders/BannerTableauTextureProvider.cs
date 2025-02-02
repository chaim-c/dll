using System;
using TaleWorlds.Engine;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View.Tableaus;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.TextureProviders
{
	// Token: 0x02000016 RID: 22
	public class BannerTableauTextureProvider : TextureProvider
	{
		// Token: 0x17000013 RID: 19
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x0000699F File Offset: 0x00004B9F
		public string BannerCodeText
		{
			set
			{
				this._bannerTableau.SetBannerCode(value);
			}
		}

		// Token: 0x17000014 RID: 20
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x000069AD File Offset: 0x00004BAD
		public bool IsNineGrid
		{
			set
			{
				this._bannerTableau.SetIsNineGrid(value);
			}
		}

		// Token: 0x17000015 RID: 21
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x000069BB File Offset: 0x00004BBB
		public float CustomRenderScale
		{
			set
			{
				this._bannerTableau.SetCustomRenderScale(value);
			}
		}

		// Token: 0x17000016 RID: 22
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x000069C9 File Offset: 0x00004BC9
		public Vec2 UpdatePositionValueManual
		{
			set
			{
				this._bannerTableau.SetUpdatePositionValueManual(value);
			}
		}

		// Token: 0x17000017 RID: 23
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x000069D7 File Offset: 0x00004BD7
		public Vec2 UpdateSizeValueManual
		{
			set
			{
				this._bannerTableau.SetUpdateSizeValueManual(value);
			}
		}

		// Token: 0x17000018 RID: 24
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x000069E5 File Offset: 0x00004BE5
		public ValueTuple<float, bool> UpdateRotationValueManualWithMirror
		{
			set
			{
				this._bannerTableau.SetUpdateRotationValueManual(value);
			}
		}

		// Token: 0x17000019 RID: 25
		// (set) Token: 0x060000BA RID: 186 RVA: 0x000069F3 File Offset: 0x00004BF3
		public int MeshIndexToUpdate
		{
			set
			{
				this._bannerTableau.SetMeshIndexToUpdate(value);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00006A13 File Offset: 0x00004C13
		// (set) Token: 0x060000BB RID: 187 RVA: 0x00006A01 File Offset: 0x00004C01
		public bool IsHidden
		{
			get
			{
				return this._isHidden;
			}
			set
			{
				if (this._isHidden != value)
				{
					this._isHidden = value;
				}
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00006A1B File Offset: 0x00004C1B
		public BannerTableauTextureProvider()
		{
			this._bannerTableau = new BannerTableau();
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00006A2E File Offset: 0x00004C2E
		public override void Clear(bool clearNextFrame)
		{
			this._bannerTableau.OnFinalize();
			base.Clear(clearNextFrame);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00006A44 File Offset: 0x00004C44
		private void CheckTexture()
		{
			if (this._texture != this._bannerTableau.Texture)
			{
				this._texture = this._bannerTableau.Texture;
				if (this._texture != null)
				{
					EngineTexture platformTexture = new EngineTexture(this._texture);
					this._providedTexture = new TaleWorlds.TwoDimension.Texture(platformTexture);
					return;
				}
				this._providedTexture = null;
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00006AA8 File Offset: 0x00004CA8
		public override TaleWorlds.TwoDimension.Texture GetTexture(TwoDimensionContext twoDimensionContext, string name)
		{
			this.CheckTexture();
			return this._providedTexture;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00006AB6 File Offset: 0x00004CB6
		public override void SetTargetSize(int width, int height)
		{
			base.SetTargetSize(width, height);
			this._bannerTableau.SetTargetSize(width, height);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00006ACD File Offset: 0x00004CCD
		public override void Tick(float dt)
		{
			base.Tick(dt);
			this.CheckTexture();
			this._bannerTableau.OnTick(dt);
		}

		// Token: 0x04000084 RID: 132
		private BannerTableau _bannerTableau;

		// Token: 0x04000085 RID: 133
		private TaleWorlds.Engine.Texture _texture;

		// Token: 0x04000086 RID: 134
		private TaleWorlds.TwoDimension.Texture _providedTexture;

		// Token: 0x04000087 RID: 135
		private bool _isHidden;
	}
}
