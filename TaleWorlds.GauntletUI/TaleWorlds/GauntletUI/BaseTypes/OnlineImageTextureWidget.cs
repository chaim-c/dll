using System;

namespace TaleWorlds.GauntletUI.BaseTypes
{
	// Token: 0x02000066 RID: 102
	public class OnlineImageTextureWidget : TextureWidget
	{
		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x0001C306 File Offset: 0x0001A506
		// (set) Token: 0x06000683 RID: 1667 RVA: 0x0001C30E File Offset: 0x0001A50E
		public OnlineImageTextureWidget.ImageSizePolicies ImageSizePolicy { get; set; }

		// Token: 0x06000684 RID: 1668 RVA: 0x0001C317 File Offset: 0x0001A517
		public OnlineImageTextureWidget(UIContext context) : base(context)
		{
			base.TextureProviderName = "OnlineImageTextureProvider";
			if (!OnlineImageTextureWidget._textureProviderTypeCollectionRequested)
			{
				TextureWidget._typeCollector.Collect();
				OnlineImageTextureWidget._textureProviderTypeCollectionRequested = true;
			}
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0001C342 File Offset: 0x0001A542
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			this.UpdateSizePolicy();
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0001C354 File Offset: 0x0001A554
		private void UpdateSizePolicy()
		{
			if (this.ImageSizePolicy == OnlineImageTextureWidget.ImageSizePolicies.OriginalSize)
			{
				if (base.Texture != null)
				{
					base.WidthSizePolicy = SizePolicy.Fixed;
					base.HeightSizePolicy = SizePolicy.Fixed;
					base.SuggestedWidth = (float)base.Texture.Width;
					base.SuggestedHeight = (float)base.Texture.Height;
					return;
				}
			}
			else
			{
				if (this.ImageSizePolicy == OnlineImageTextureWidget.ImageSizePolicies.Stretch)
				{
					base.WidthSizePolicy = SizePolicy.StretchToParent;
					base.HeightSizePolicy = SizePolicy.StretchToParent;
					return;
				}
				if (this.ImageSizePolicy == OnlineImageTextureWidget.ImageSizePolicies.ScaleToBiggerDimension && base.Texture != null)
				{
					base.WidthSizePolicy = SizePolicy.Fixed;
					base.HeightSizePolicy = SizePolicy.Fixed;
					float num;
					if (base.Texture.Width > base.Texture.Height)
					{
						num = base.ParentWidget.Size.Y / (float)base.Texture.Height;
						if (num * (float)base.Texture.Width < base.ParentWidget.Size.X)
						{
							num = base.ParentWidget.Size.X / (float)base.Texture.Width;
						}
					}
					else
					{
						num = base.ParentWidget.Size.X / (float)base.Texture.Width;
						if (num * (float)base.Texture.Height < base.ParentWidget.Size.Y)
						{
							num = base.ParentWidget.Size.Y / (float)base.Texture.Height;
						}
					}
					base.ScaledSuggestedWidth = num * (float)base.Texture.Width;
					base.ScaledSuggestedHeight = num * (float)base.Texture.Height;
				}
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x0001C4E6 File Offset: 0x0001A6E6
		// (set) Token: 0x06000688 RID: 1672 RVA: 0x0001C4EE File Offset: 0x0001A6EE
		[Editor(false)]
		public string OnlineImageSourceUrl
		{
			get
			{
				return this._onlineImageSourceUrl;
			}
			set
			{
				if (this._onlineImageSourceUrl != value)
				{
					this._onlineImageSourceUrl = value;
					base.OnPropertyChanged<string>(value, "OnlineImageSourceUrl");
					base.SetTextureProviderProperty("OnlineSourceUrl", value);
					this.RefreshState();
				}
			}
		}

		// Token: 0x04000308 RID: 776
		private static bool _textureProviderTypeCollectionRequested;

		// Token: 0x0400030A RID: 778
		private string _onlineImageSourceUrl;

		// Token: 0x02000093 RID: 147
		public enum ImageSizePolicies
		{
			// Token: 0x04000487 RID: 1159
			Stretch,
			// Token: 0x04000488 RID: 1160
			OriginalSize,
			// Token: 0x04000489 RID: 1161
			ScaleToBiggerDimension
		}
	}
}
