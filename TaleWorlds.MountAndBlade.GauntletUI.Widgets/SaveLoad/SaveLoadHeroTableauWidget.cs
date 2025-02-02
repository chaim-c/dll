using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.SaveLoad
{
	// Token: 0x02000054 RID: 84
	public class SaveLoadHeroTableauWidget : TextureWidget
	{
		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x0000E502 File Offset: 0x0000C702
		public bool IsVersionCompatible
		{
			get
			{
				return (bool)base.GetTextureProviderProperty("IsVersionCompatible");
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x0000E514 File Offset: 0x0000C714
		// (set) Token: 0x06000484 RID: 1156 RVA: 0x0000E51C File Offset: 0x0000C71C
		[Editor(false)]
		public string HeroVisualCode
		{
			get
			{
				return this._heroVisualCode;
			}
			set
			{
				if (value != this._heroVisualCode)
				{
					this._heroVisualCode = value;
					base.OnPropertyChanged<string>(value, "HeroVisualCode");
					base.SetTextureProviderProperty("HeroVisualCode", value);
				}
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x0000E54B File Offset: 0x0000C74B
		// (set) Token: 0x06000486 RID: 1158 RVA: 0x0000E553 File Offset: 0x0000C753
		[Editor(false)]
		public string BannerCode
		{
			get
			{
				return this._bannerCode;
			}
			set
			{
				if (value != this._bannerCode)
				{
					this._bannerCode = value;
					base.OnPropertyChanged<string>(value, "BannerCode");
					base.SetTextureProviderProperty("BannerCode", value);
				}
			}
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0000E582 File Offset: 0x0000C782
		public SaveLoadHeroTableauWidget(UIContext context) : base(context)
		{
			base.TextureProviderName = "SaveLoadHeroTableauTextureProvider";
			this._isRenderRequestedPreviousFrame = true;
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0000E59D File Offset: 0x0000C79D
		protected override void OnMousePressed()
		{
			base.SetTextureProviderProperty("CurrentlyRotating", true);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0000E5B0 File Offset: 0x0000C7B0
		protected override void OnMouseReleased()
		{
			base.SetTextureProviderProperty("CurrentlyRotating", false);
		}

		// Token: 0x040001F6 RID: 502
		private string _heroVisualCode;

		// Token: 0x040001F7 RID: 503
		private string _bannerCode;
	}
}
