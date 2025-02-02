using System;
using System.Collections.Generic;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer
{
	// Token: 0x02000081 RID: 129
	public class MultiplayerFactionBannerWidget : Widget
	{
		// Token: 0x06000729 RID: 1833 RVA: 0x000153C0 File Offset: 0x000135C0
		public MultiplayerFactionBannerWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x000153D0 File Offset: 0x000135D0
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			if (this._firstFrame)
			{
				this.UpdateBanner();
				this.UpdateIcon();
				this._firstFrame = false;
			}
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x000153F4 File Offset: 0x000135F4
		private void UpdateBanner()
		{
			if (this._bannerWidget == null)
			{
				return;
			}
			BrushWidget brushWidget;
			if ((brushWidget = (this.BannerWidget as BrushWidget)) != null)
			{
				using (Dictionary<string, Style>.ValueCollection.Enumerator enumerator = brushWidget.Brush.Styles.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Style style = enumerator.Current;
						StyleLayer[] layers = style.GetLayers();
						for (int i = 0; i < layers.Length; i++)
						{
							layers[i].Color = this.CultureColor1;
						}
					}
					return;
				}
			}
			this.BannerWidget.Color = this.CultureColor1;
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x00015490 File Offset: 0x00013690
		private void UpdateIcon()
		{
			if (string.IsNullOrEmpty(this.FactionCode) || this._iconWidget == null)
			{
				return;
			}
			this.IconWidget.Sprite = base.Context.SpriteData.GetSprite("StdAssets\\FactionIcons\\LargeIcons\\" + this.FactionCode);
			this.IconWidget.Color = this.CultureColor2;
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x000154EF File Offset: 0x000136EF
		// (set) Token: 0x0600072E RID: 1838 RVA: 0x000154F7 File Offset: 0x000136F7
		[DataSourceProperty]
		public Color CultureColor1
		{
			get
			{
				return this._cultureColor1;
			}
			set
			{
				if (value != this._cultureColor1)
				{
					this._cultureColor1 = value;
					base.OnPropertyChanged(value, "CultureColor1");
					this.UpdateBanner();
				}
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x00015520 File Offset: 0x00013720
		// (set) Token: 0x06000730 RID: 1840 RVA: 0x00015528 File Offset: 0x00013728
		[DataSourceProperty]
		public Color CultureColor2
		{
			get
			{
				return this._cultureColor2;
			}
			set
			{
				if (value != this._cultureColor2)
				{
					this._cultureColor2 = value;
					base.OnPropertyChanged(value, "CultureColor2");
					this.UpdateIcon();
				}
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000731 RID: 1841 RVA: 0x00015551 File Offset: 0x00013751
		// (set) Token: 0x06000732 RID: 1842 RVA: 0x00015559 File Offset: 0x00013759
		[DataSourceProperty]
		public string FactionCode
		{
			get
			{
				return this._factionCode;
			}
			set
			{
				if (value != this._factionCode)
				{
					this._factionCode = value;
					base.OnPropertyChanged<string>(value, "FactionCode");
					this.UpdateIcon();
				}
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000733 RID: 1843 RVA: 0x00015582 File Offset: 0x00013782
		// (set) Token: 0x06000734 RID: 1844 RVA: 0x0001558A File Offset: 0x0001378A
		[DataSourceProperty]
		public Widget BannerWidget
		{
			get
			{
				return this._bannerWidget;
			}
			set
			{
				if (value != this._bannerWidget)
				{
					this._bannerWidget = value;
					base.OnPropertyChanged<Widget>(value, "BannerWidget");
					this.UpdateBanner();
				}
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x000155AE File Offset: 0x000137AE
		// (set) Token: 0x06000736 RID: 1846 RVA: 0x000155B6 File Offset: 0x000137B6
		[DataSourceProperty]
		public Widget IconWidget
		{
			get
			{
				return this._iconWidget;
			}
			set
			{
				if (value != this._iconWidget)
				{
					this._iconWidget = value;
					base.OnPropertyChanged<Widget>(value, "IconWidget");
					this.UpdateIcon();
				}
			}
		}

		// Token: 0x0400032B RID: 811
		private bool _firstFrame = true;

		// Token: 0x0400032C RID: 812
		private Color _cultureColor1;

		// Token: 0x0400032D RID: 813
		private Color _cultureColor2;

		// Token: 0x0400032E RID: 814
		private string _factionCode;

		// Token: 0x0400032F RID: 815
		private Widget _bannerWidget;

		// Token: 0x04000330 RID: 816
		private Widget _iconWidget;
	}
}
