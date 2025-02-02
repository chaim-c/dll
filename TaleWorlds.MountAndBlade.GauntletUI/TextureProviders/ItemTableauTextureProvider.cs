using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI;
using TaleWorlds.MountAndBlade.View.Tableaus;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.TextureProviders
{
	// Token: 0x0200001A RID: 26
	public class ItemTableauTextureProvider : TextureProvider
	{
		// Token: 0x1700003A RID: 58
		// (set) Token: 0x060000FF RID: 255 RVA: 0x000074E8 File Offset: 0x000056E8
		public string ItemModifierId
		{
			set
			{
				this._itemTableau.SetItemModifierId(value);
			}
		}

		// Token: 0x1700003B RID: 59
		// (set) Token: 0x06000100 RID: 256 RVA: 0x000074F6 File Offset: 0x000056F6
		public string StringId
		{
			set
			{
				this._itemTableau.SetStringId(value);
			}
		}

		// Token: 0x1700003C RID: 60
		// (set) Token: 0x06000101 RID: 257 RVA: 0x00007504 File Offset: 0x00005704
		public ItemRosterElement Item
		{
			set
			{
				this._itemTableau.SetItem(value);
			}
		}

		// Token: 0x1700003D RID: 61
		// (set) Token: 0x06000102 RID: 258 RVA: 0x00007512 File Offset: 0x00005712
		public int Ammo
		{
			set
			{
				this._itemTableau.SetAmmo(value);
			}
		}

		// Token: 0x1700003E RID: 62
		// (set) Token: 0x06000103 RID: 259 RVA: 0x00007520 File Offset: 0x00005720
		public int AverageUnitCost
		{
			set
			{
				this._itemTableau.SetAverageUnitCost(value);
			}
		}

		// Token: 0x1700003F RID: 63
		// (set) Token: 0x06000104 RID: 260 RVA: 0x0000752E File Offset: 0x0000572E
		public string BannerCode
		{
			set
			{
				this._itemTableau.SetBannerCode(value);
			}
		}

		// Token: 0x17000040 RID: 64
		// (set) Token: 0x06000105 RID: 261 RVA: 0x0000753C File Offset: 0x0000573C
		public bool CurrentlyRotating
		{
			set
			{
				this._itemTableau.RotateItem(value);
			}
		}

		// Token: 0x17000041 RID: 65
		// (set) Token: 0x06000106 RID: 262 RVA: 0x0000754A File Offset: 0x0000574A
		public float RotateItemVertical
		{
			set
			{
				this._itemTableau.RotateItemVerticalWithAmount(value);
			}
		}

		// Token: 0x17000042 RID: 66
		// (set) Token: 0x06000107 RID: 263 RVA: 0x00007558 File Offset: 0x00005758
		public float RotateItemHorizontal
		{
			set
			{
				this._itemTableau.RotateItemHorizontalWithAmount(value);
			}
		}

		// Token: 0x17000043 RID: 67
		// (set) Token: 0x06000108 RID: 264 RVA: 0x00007566 File Offset: 0x00005766
		public float InitialTiltRotation
		{
			set
			{
				this._itemTableau.SetInitialTiltRotation(value);
			}
		}

		// Token: 0x17000044 RID: 68
		// (set) Token: 0x06000109 RID: 265 RVA: 0x00007574 File Offset: 0x00005774
		public float InitialPanRotation
		{
			set
			{
				this._itemTableau.SetInitialPanRotation(value);
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00007582 File Offset: 0x00005782
		public ItemTableauTextureProvider()
		{
			this._itemTableau = new ItemTableau();
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00007595 File Offset: 0x00005795
		public override void Clear(bool clearNextFrame)
		{
			this._itemTableau.OnFinalize();
			base.Clear(clearNextFrame);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000075AC File Offset: 0x000057AC
		private void CheckTexture()
		{
			if (this._texture != this._itemTableau.Texture)
			{
				this._texture = this._itemTableau.Texture;
				if (this._texture != null)
				{
					EngineTexture platformTexture = new EngineTexture(this._texture);
					this._providedTexture = new TaleWorlds.TwoDimension.Texture(platformTexture);
					return;
				}
				this._providedTexture = null;
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00007610 File Offset: 0x00005810
		public override TaleWorlds.TwoDimension.Texture GetTexture(TwoDimensionContext twoDimensionContext, string name)
		{
			this.CheckTexture();
			return this._providedTexture;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000761E File Offset: 0x0000581E
		public override void SetTargetSize(int width, int height)
		{
			base.SetTargetSize(width, height);
			this._itemTableau.SetTargetSize(width, height);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00007635 File Offset: 0x00005835
		public override void Tick(float dt)
		{
			base.Tick(dt);
			this.CheckTexture();
			this._itemTableau.OnTick(dt);
		}

		// Token: 0x040000A1 RID: 161
		private readonly ItemTableau _itemTableau;

		// Token: 0x040000A2 RID: 162
		private TaleWorlds.Engine.Texture _texture;

		// Token: 0x040000A3 RID: 163
		private TaleWorlds.TwoDimension.Texture _providedTexture;
	}
}
