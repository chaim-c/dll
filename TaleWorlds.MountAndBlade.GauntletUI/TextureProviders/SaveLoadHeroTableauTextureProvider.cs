using System;
using TaleWorlds.Engine;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI;
using TaleWorlds.MountAndBlade.View.Tableaus;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.TextureProviders
{
	// Token: 0x0200001C RID: 28
	public class SaveLoadHeroTableauTextureProvider : TextureProvider
	{
		// Token: 0x17000046 RID: 70
		// (set) Token: 0x06000118 RID: 280 RVA: 0x000077B1 File Offset: 0x000059B1
		public string HeroVisualCode
		{
			set
			{
				this._characterCode = value;
				this.DeserializeCharacterCode(this._characterCode);
			}
		}

		// Token: 0x17000047 RID: 71
		// (set) Token: 0x06000119 RID: 281 RVA: 0x000077C6 File Offset: 0x000059C6
		public string BannerCode
		{
			set
			{
				this._tableau.SetBannerCode(value);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600011A RID: 282 RVA: 0x000077D4 File Offset: 0x000059D4
		public bool IsVersionCompatible
		{
			get
			{
				return this._tableau.IsVersionCompatible;
			}
		}

		// Token: 0x17000049 RID: 73
		// (set) Token: 0x0600011B RID: 283 RVA: 0x000077E1 File Offset: 0x000059E1
		public bool CurrentlyRotating
		{
			set
			{
				this._tableau.RotateCharacter(value);
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000077EF File Offset: 0x000059EF
		public SaveLoadHeroTableauTextureProvider()
		{
			this._tableau = new BasicCharacterTableau();
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00007802 File Offset: 0x00005A02
		public override void Tick(float dt)
		{
			this.CheckTexture();
			this._tableau.OnTick(dt);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00007816 File Offset: 0x00005A16
		public override void SetTargetSize(int width, int height)
		{
			base.SetTargetSize(width, height);
			this._tableau.SetTargetSize(width, height);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000782D File Offset: 0x00005A2D
		private void DeserializeCharacterCode(string characterCode)
		{
			if (!string.IsNullOrEmpty(characterCode))
			{
				this._tableau.DeserializeCharacterCode(characterCode);
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00007844 File Offset: 0x00005A44
		private void CheckTexture()
		{
			if (this._texture != this._tableau.Texture)
			{
				this._texture = this._tableau.Texture;
				if (this._texture != null)
				{
					EngineTexture platformTexture = new EngineTexture(this._texture);
					this._providedTexture = new TaleWorlds.TwoDimension.Texture(platformTexture);
					return;
				}
				this._providedTexture = null;
			}
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000078A8 File Offset: 0x00005AA8
		public override void Clear(bool clearNextFrame)
		{
			this._tableau.OnFinalize();
			base.Clear(clearNextFrame);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000078BC File Offset: 0x00005ABC
		public override TaleWorlds.TwoDimension.Texture GetTexture(TwoDimensionContext twoDimensionContext, string name)
		{
			this.CheckTexture();
			return this._providedTexture;
		}

		// Token: 0x040000AC RID: 172
		private string _characterCode;

		// Token: 0x040000AD RID: 173
		private BasicCharacterTableau _tableau;

		// Token: 0x040000AE RID: 174
		private TaleWorlds.Engine.Texture _texture;

		// Token: 0x040000AF RID: 175
		private TaleWorlds.TwoDimension.Texture _providedTexture;
	}
}
