using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer
{
	// Token: 0x02000087 RID: 135
	public class MultiplayerTroopTypeIconWidget : Widget
	{
		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000757 RID: 1879 RVA: 0x000159D4 File Offset: 0x00013BD4
		// (set) Token: 0x06000758 RID: 1880 RVA: 0x000159DC File Offset: 0x00013BDC
		public float ScaleFactor { get; set; } = 1f;

		// Token: 0x06000759 RID: 1881 RVA: 0x000159E5 File Offset: 0x00013BE5
		public MultiplayerTroopTypeIconWidget(UIContext context) : base(context)
		{
			this.BackgroundWidget = this;
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x00015A00 File Offset: 0x00013C00
		private void UpdateIcon()
		{
			if (this.BackgroundWidget == null || this.ForegroundWidget == null || string.IsNullOrEmpty(this.IconSpriteType))
			{
				return;
			}
			string text = "MPHud\\TroopIcons\\" + this.IconSpriteType;
			string name = text + "_Outline";
			this.ForegroundWidget.Sprite = base.Context.SpriteData.GetSprite(text);
			this.BackgroundWidget.Sprite = base.Context.SpriteData.GetSprite(name);
			if (this.BackgroundWidget.Sprite != null)
			{
				float num = (float)this.BackgroundWidget.Sprite.Width;
				this.BackgroundWidget.SuggestedWidth = num * this.ScaleFactor;
				this.ForegroundWidget.SuggestedWidth = num * this.ScaleFactor;
				float num2 = (float)this.BackgroundWidget.Sprite.Height;
				this.BackgroundWidget.SuggestedHeight = num2 * this.ScaleFactor;
				this.ForegroundWidget.SuggestedHeight = num2 * this.ScaleFactor;
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x0600075B RID: 1883 RVA: 0x00015AFD File Offset: 0x00013CFD
		// (set) Token: 0x0600075C RID: 1884 RVA: 0x00015B05 File Offset: 0x00013D05
		[DataSourceProperty]
		public Widget BackgroundWidget
		{
			get
			{
				return this._backgroundWidget;
			}
			set
			{
				if (this._backgroundWidget != value)
				{
					this._backgroundWidget = value;
					base.OnPropertyChanged<Widget>(value, "BackgroundWidget");
					this.UpdateIcon();
				}
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x00015B29 File Offset: 0x00013D29
		// (set) Token: 0x0600075E RID: 1886 RVA: 0x00015B31 File Offset: 0x00013D31
		[DataSourceProperty]
		public Widget ForegroundWidget
		{
			get
			{
				return this._foregroundWidget;
			}
			set
			{
				if (this._foregroundWidget != value)
				{
					this._foregroundWidget = value;
					base.OnPropertyChanged<Widget>(value, "ForegroundWidget");
					this.UpdateIcon();
				}
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x00015B55 File Offset: 0x00013D55
		// (set) Token: 0x06000760 RID: 1888 RVA: 0x00015B5D File Offset: 0x00013D5D
		[DataSourceProperty]
		public string IconSpriteType
		{
			get
			{
				return this._iconSpriteType;
			}
			set
			{
				if (this._iconSpriteType != value)
				{
					this._iconSpriteType = value;
					base.OnPropertyChanged<string>(value, "IconSpriteType");
					this.UpdateIcon();
				}
			}
		}

		// Token: 0x04000343 RID: 835
		private Widget _backgroundWidget;

		// Token: 0x04000344 RID: 836
		private Widget _foregroundWidget;

		// Token: 0x04000345 RID: 837
		private string _iconSpriteType;
	}
}
