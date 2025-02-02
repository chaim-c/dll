using System;
using System.Numerics;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI.BaseTypes
{
	// Token: 0x02000064 RID: 100
	public class MaskedTextureWidget : TextureWidget
	{
		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x0001BFE3 File Offset: 0x0001A1E3
		// (set) Token: 0x06000672 RID: 1650 RVA: 0x0001BFEB File Offset: 0x0001A1EB
		[Editor(false)]
		public string ImageId
		{
			get
			{
				return this._imageId;
			}
			set
			{
				if (this._imageId != value)
				{
					this._imageId = value;
					base.OnPropertyChanged<string>(value, "ImageId");
					base.SetTextureProviderProperty("ImageId", value);
				}
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x0001C01A File Offset: 0x0001A21A
		// (set) Token: 0x06000674 RID: 1652 RVA: 0x0001C022 File Offset: 0x0001A222
		[Editor(false)]
		public string AdditionalArgs
		{
			get
			{
				return this._additionalArgs;
			}
			set
			{
				if (this._additionalArgs != value)
				{
					this._additionalArgs = value;
					base.OnPropertyChanged<string>(value, "AdditionalArgs");
					base.SetTextureProviderProperty("AdditionalArgs", value);
				}
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000675 RID: 1653 RVA: 0x0001C051 File Offset: 0x0001A251
		// (set) Token: 0x06000676 RID: 1654 RVA: 0x0001C059 File Offset: 0x0001A259
		[Editor(false)]
		public int ImageTypeCode
		{
			get
			{
				return this._imageTypeCode;
			}
			set
			{
				if (this._imageTypeCode != value)
				{
					this._imageTypeCode = value;
					base.OnPropertyChanged(value, "ImageTypeCode");
					base.SetTextureProviderProperty("ImageTypeCode", value);
				}
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x0001C088 File Offset: 0x0001A288
		// (set) Token: 0x06000678 RID: 1656 RVA: 0x0001C090 File Offset: 0x0001A290
		[Editor(false)]
		public bool IsBig
		{
			get
			{
				return this._isBig;
			}
			set
			{
				if (this._isBig != value)
				{
					this._isBig = value;
					base.OnPropertyChanged(value, "IsBig");
					base.SetTextureProviderProperty("IsBig", value);
				}
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x0001C0BF File Offset: 0x0001A2BF
		// (set) Token: 0x0600067A RID: 1658 RVA: 0x0001C0C7 File Offset: 0x0001A2C7
		[Editor(false)]
		public float OverlayTextureScale { get; set; }

		// Token: 0x0600067B RID: 1659 RVA: 0x0001C0D0 File Offset: 0x0001A2D0
		public MaskedTextureWidget(UIContext context) : base(context)
		{
			base.TextureProviderName = "ImageIdentifierTextureProvider";
			this.OverlayTextureScale = 1f;
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0001C0F0 File Offset: 0x0001A2F0
		protected override void OnRender(TwoDimensionContext twoDimensionContext, TwoDimensionDrawContext drawContext)
		{
			this._isRenderRequestedPreviousFrame = true;
			if (base.TextureProvider != null)
			{
				Texture texture = base.TextureProvider.GetTexture(twoDimensionContext, "ui_backgrounds_1");
				bool flag = false;
				if (texture != this._textureCache)
				{
					base.Brush.DefaultLayer.OverlayMethod = BrushOverlayMethod.CoverWithTexture;
					this._textureCache = texture;
					flag = true;
					base.HandleUpdateNeededOnRender();
				}
				if (this._textureCache != null)
				{
					bool flag2 = this.ImageTypeCode == 3 || this.ImageTypeCode == 6;
					int num = flag2 ? ((int)(((base.Size.X > base.Size.Y) ? base.Size.Y : base.Size.X) * 2.5f * this.OverlayTextureScale)) : ((int)(((base.Size.X > base.Size.Y) ? base.Size.X : base.Size.Y) * this.OverlayTextureScale));
					Vector2 overlayOffset = default(Vector2);
					if (flag2)
					{
						float x = ((float)num - base.Size.X) * 0.5f - base.Brush.DefaultLayer.OverlayXOffset;
						float y = ((float)num - base.Size.Y) * 0.5f - base.Brush.DefaultLayer.OverlayYOffset;
						overlayOffset = new Vector2(x, y) * base._inverseScaleToUse;
					}
					if (this._overlaySpriteCache == null || flag || this._overlaySpriteSizeCache != num)
					{
						this._overlaySpriteSizeCache = num;
						this._overlaySpriteCache = new SpriteFromTexture(this._textureCache, this._overlaySpriteSizeCache, this._overlaySpriteSizeCache);
					}
					base.Brush.DefaultLayer.OverlaySprite = this._overlaySpriteCache;
					base.BrushRenderer.Render(drawContext, base.GlobalPosition, base.Size, base._scaleToUse, base.Context.ContextAlpha, overlayOffset);
				}
			}
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0001C2CB File Offset: 0x0001A4CB
		protected override void OnDisconnectedFromRoot()
		{
			base.OnDisconnectedFromRoot();
			this._textureCache = null;
		}

		// Token: 0x040002FF RID: 767
		private Texture _textureCache;

		// Token: 0x04000300 RID: 768
		private string _imageId;

		// Token: 0x04000301 RID: 769
		private string _additionalArgs;

		// Token: 0x04000302 RID: 770
		private int _imageTypeCode;

		// Token: 0x04000303 RID: 771
		private bool _isBig;

		// Token: 0x04000305 RID: 773
		private SpriteFromTexture _overlaySpriteCache;

		// Token: 0x04000306 RID: 774
		private int _overlaySpriteSizeCache;
	}
}
