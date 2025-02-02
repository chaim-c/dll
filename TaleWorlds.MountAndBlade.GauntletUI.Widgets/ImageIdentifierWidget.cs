using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x02000025 RID: 37
	public class ImageIdentifierWidget : TextureWidget
	{
		// Token: 0x060001E2 RID: 482 RVA: 0x000073E4 File Offset: 0x000055E4
		public ImageIdentifierWidget(UIContext context) : base(context)
		{
			base.TextureProviderName = "ImageIdentifierTextureProvider";
			this._calculateSizeFirstFrame = false;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x000073FF File Offset: 0x000055FF
		private void RefreshVisibility()
		{
			if (this.HideWhenNull)
			{
				base.IsVisible = (this.ImageTypeCode != 0);
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00007418 File Offset: 0x00005618
		protected override void OnDisconnectedFromRoot()
		{
			base.SetTextureProviderProperty("IsReleased", true);
			base.OnDisconnectedFromRoot();
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00007431 File Offset: 0x00005631
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x0000743C File Offset: 0x0000563C
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
					if (!string.IsNullOrEmpty(this._imageId))
					{
						base.SetTextureProviderProperty("IsReleased", true);
					}
					this._imageId = value;
					base.OnPropertyChanged<string>(value, "ImageId");
					base.SetTextureProviderProperty("ImageId", value);
					base.SetTextureProviderProperty("IsReleased", false);
					this.RefreshVisibility();
					base.Texture = null;
					TextureProvider textureProvider = base.TextureProvider;
					if (textureProvider == null)
					{
						return;
					}
					textureProvider.Clear(true);
				}
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x000074C3 File Offset: 0x000056C3
		// (set) Token: 0x060001E8 RID: 488 RVA: 0x000074CC File Offset: 0x000056CC
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
					if (!string.IsNullOrEmpty(this._additionalArgs))
					{
						base.SetTextureProviderProperty("IsReleased", true);
					}
					this._additionalArgs = value;
					base.OnPropertyChanged<string>(value, "AdditionalArgs");
					base.SetTextureProviderProperty("AdditionalArgs", value);
					base.SetTextureProviderProperty("IsReleased", false);
					this.RefreshVisibility();
					base.Texture = null;
					TextureProvider textureProvider = base.TextureProvider;
					if (textureProvider == null)
					{
						return;
					}
					textureProvider.Clear(true);
				}
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00007553 File Offset: 0x00005753
		// (set) Token: 0x060001EA RID: 490 RVA: 0x0000755C File Offset: 0x0000575C
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
					this.RefreshVisibility();
					base.Texture = null;
					TextureProvider textureProvider = base.TextureProvider;
					if (textureProvider == null)
					{
						return;
					}
					textureProvider.Clear(true);
				}
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001EB RID: 491 RVA: 0x000075B4 File Offset: 0x000057B4
		// (set) Token: 0x060001EC RID: 492 RVA: 0x000075BC File Offset: 0x000057BC
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
					this.RefreshVisibility();
					base.Texture = null;
					TextureProvider textureProvider = base.TextureProvider;
					if (textureProvider == null)
					{
						return;
					}
					textureProvider.Clear(true);
				}
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001ED RID: 493 RVA: 0x00007614 File Offset: 0x00005814
		// (set) Token: 0x060001EE RID: 494 RVA: 0x0000761C File Offset: 0x0000581C
		[Editor(false)]
		public bool HideWhenNull
		{
			get
			{
				return this._hideWhenNull;
			}
			set
			{
				if (this._hideWhenNull != value)
				{
					this._hideWhenNull = value;
					base.OnPropertyChanged(value, "HideWhenNull");
					this.RefreshVisibility();
					base.Texture = null;
					TextureProvider textureProvider = base.TextureProvider;
					if (textureProvider == null)
					{
						return;
					}
					textureProvider.Clear(true);
				}
			}
		}

		// Token: 0x040000E6 RID: 230
		private string _imageId;

		// Token: 0x040000E7 RID: 231
		private string _additionalArgs;

		// Token: 0x040000E8 RID: 232
		private int _imageTypeCode;

		// Token: 0x040000E9 RID: 233
		private bool _isBig;

		// Token: 0x040000EA RID: 234
		private bool _hideWhenNull;
	}
}
