using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.Launcher.Library.CustomWidgets
{
	// Token: 0x02000025 RID: 37
	public class LauncherOnlineImageTextureWidget : TextureWidget
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000173 RID: 371 RVA: 0x0000676A File Offset: 0x0000496A
		// (set) Token: 0x06000174 RID: 372 RVA: 0x00006772 File Offset: 0x00004972
		public LauncherOnlineImageTextureWidget.ImageSizePolicies ImageSizePolicy { get; set; }

		// Token: 0x06000175 RID: 373 RVA: 0x0000677B File Offset: 0x0000497B
		public LauncherOnlineImageTextureWidget(UIContext context) : base(context)
		{
			base.TextureProviderName = "LauncherOnlineImageTextureProvider";
			base.WidthSizePolicy = SizePolicy.Fixed;
			base.HeightSizePolicy = SizePolicy.Fixed;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000679D File Offset: 0x0000499D
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			this.UpdateSizePolicy();
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000067AC File Offset: 0x000049AC
		protected override void OnTextureUpdated()
		{
			base.OnTextureUpdated();
			this.SetGlobalAlphaRecursively(0f);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x000067C0 File Offset: 0x000049C0
		private void UpdateSizePolicy()
		{
			if (base.Texture != null && base.ReadOnlyBrush.GlobalAlphaFactor < 1f)
			{
				float alphaFactor = Mathf.Lerp(base.ReadOnlyBrush.GlobalAlphaFactor, 1f, 0.1f);
				this.SetGlobalAlphaRecursively(alphaFactor);
			}
			else if (base.Texture == null)
			{
				this.SetGlobalAlphaRecursively(0f);
			}
			if (this.ImageSizePolicy == LauncherOnlineImageTextureWidget.ImageSizePolicies.OriginalSize)
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
				if (this.ImageSizePolicy == LauncherOnlineImageTextureWidget.ImageSizePolicies.Stretch)
				{
					base.WidthSizePolicy = SizePolicy.StretchToParent;
					base.HeightSizePolicy = SizePolicy.StretchToParent;
					return;
				}
				if (this.ImageSizePolicy == LauncherOnlineImageTextureWidget.ImageSizePolicies.ScaleToBiggerDimension && base.Texture != null)
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
					base.SuggestedWidth = num * (float)base.Texture.Width * base._inverseScaleToUse;
					base.SuggestedHeight = num * (float)base.Texture.Height * base._inverseScaleToUse;
					base.ScaledSuggestedWidth = num * (float)base.Texture.Width;
					base.ScaledSuggestedHeight = num * (float)base.Texture.Height;
				}
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000179 RID: 377 RVA: 0x000069D9 File Offset: 0x00004BD9
		// (set) Token: 0x0600017A RID: 378 RVA: 0x000069E1 File Offset: 0x00004BE1
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

		// Token: 0x040000B6 RID: 182
		private string _onlineImageSourceUrl;

		// Token: 0x02000045 RID: 69
		public enum ImageSizePolicies
		{
			// Token: 0x040000F7 RID: 247
			Stretch,
			// Token: 0x040000F8 RID: 248
			OriginalSize,
			// Token: 0x040000F9 RID: 249
			ScaleToBiggerDimension
		}
	}
}
