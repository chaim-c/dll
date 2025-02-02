using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.Perks
{
	// Token: 0x02000090 RID: 144
	public class MultiplayerPerkItemToggleWidget : ToggleButtonWidget
	{
		// Token: 0x060007B6 RID: 1974 RVA: 0x00016A50 File Offset: 0x00014C50
		public MultiplayerPerkItemToggleWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x00016A59 File Offset: 0x00014C59
		protected override void OnMouseReleased()
		{
			base.OnMouseReleased();
			MultiplayerPerkContainerPanelWidget containerPanel = this.ContainerPanel;
			if (containerPanel == null)
			{
				return;
			}
			containerPanel.PerkSelected(this._isSelectable ? this : null);
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x00016A80 File Offset: 0x00014C80
		private void UpdateIcon()
		{
			if (string.IsNullOrEmpty(this.IconType) || this._iconWidget == null)
			{
				return;
			}
			foreach (Style style in this.IconWidget.Brush.Styles)
			{
				StyleLayer[] layers = style.GetLayers();
				for (int i = 0; i < layers.Length; i++)
				{
					layers[i].Sprite = base.Context.SpriteData.GetSprite("General\\Perks\\" + this.IconType);
				}
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x060007B9 RID: 1977 RVA: 0x00016B28 File Offset: 0x00014D28
		// (set) Token: 0x060007BA RID: 1978 RVA: 0x00016B30 File Offset: 0x00014D30
		[DataSourceProperty]
		public string IconType
		{
			get
			{
				return this._iconType;
			}
			set
			{
				if (value != this._iconType)
				{
					this._iconType = value;
					base.OnPropertyChanged<string>(value, "IconType");
					this.UpdateIcon();
				}
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x060007BB RID: 1979 RVA: 0x00016B59 File Offset: 0x00014D59
		// (set) Token: 0x060007BC RID: 1980 RVA: 0x00016B61 File Offset: 0x00014D61
		[DataSourceProperty]
		public BrushWidget IconWidget
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
					base.OnPropertyChanged<BrushWidget>(value, "IconWidget");
					this.UpdateIcon();
				}
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x060007BD RID: 1981 RVA: 0x00016B85 File Offset: 0x00014D85
		// (set) Token: 0x060007BE RID: 1982 RVA: 0x00016B8D File Offset: 0x00014D8D
		[DataSourceProperty]
		public bool IsSelectable
		{
			get
			{
				return this._isSelectable;
			}
			set
			{
				if (value != this._isSelectable)
				{
					this._isSelectable = value;
					base.OnPropertyChanged(value, "IsSelectable");
				}
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x060007BF RID: 1983 RVA: 0x00016BAB File Offset: 0x00014DAB
		// (set) Token: 0x060007C0 RID: 1984 RVA: 0x00016BB3 File Offset: 0x00014DB3
		[DataSourceProperty]
		public MultiplayerPerkContainerPanelWidget ContainerPanel
		{
			get
			{
				return this._containerPanel;
			}
			set
			{
				if (value != this._containerPanel)
				{
					this._containerPanel = value;
					base.OnPropertyChanged<MultiplayerPerkContainerPanelWidget>(value, "ContainerPanel");
				}
			}
		}

		// Token: 0x04000379 RID: 889
		private string _iconType;

		// Token: 0x0400037A RID: 890
		private BrushWidget _iconWidget;

		// Token: 0x0400037B RID: 891
		private bool _isSelectable;

		// Token: 0x0400037C RID: 892
		private MultiplayerPerkContainerPanelWidget _containerPanel;
	}
}
