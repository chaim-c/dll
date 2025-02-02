using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.Lobby.Armory
{
	// Token: 0x020000B2 RID: 178
	public class MultiplayerLobbyClassFilterClassItemWidget : ToggleStateButtonWidget
	{
		// Token: 0x06000972 RID: 2418 RVA: 0x0001AD6A File Offset: 0x00018F6A
		public MultiplayerLobbyClassFilterClassItemWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x0001AD73 File Offset: 0x00018F73
		private void SetFactionColor()
		{
			if (this.FactionColorWidget == null)
			{
				return;
			}
			this.FactionColorWidget.Color = this.CultureColor;
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x0001AD90 File Offset: 0x00018F90
		private void UpdateIcon()
		{
			if (string.IsNullOrEmpty(this.TroopType) || this._iconWidget == null)
			{
				return;
			}
			this.IconWidget.Sprite = base.Context.SpriteData.GetSprite("General\\compass\\" + this.TroopType);
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000975 RID: 2421 RVA: 0x0001ADDE File Offset: 0x00018FDE
		// (set) Token: 0x06000976 RID: 2422 RVA: 0x0001ADE6 File Offset: 0x00018FE6
		[Editor(false)]
		public Widget FactionColorWidget
		{
			get
			{
				return this._factionColorWidget;
			}
			set
			{
				if (this._factionColorWidget != value)
				{
					this._factionColorWidget = value;
					base.OnPropertyChanged<Widget>(value, "FactionColorWidget");
					this.SetFactionColor();
				}
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000977 RID: 2423 RVA: 0x0001AE0A File Offset: 0x0001900A
		// (set) Token: 0x06000978 RID: 2424 RVA: 0x0001AE12 File Offset: 0x00019012
		[Editor(false)]
		public Color CultureColor
		{
			get
			{
				return this._cultureColor;
			}
			set
			{
				if (this._cultureColor != value)
				{
					this._cultureColor = value;
					base.OnPropertyChanged(value, "CultureColor");
					this.SetFactionColor();
				}
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000979 RID: 2425 RVA: 0x0001AE3B File Offset: 0x0001903B
		// (set) Token: 0x0600097A RID: 2426 RVA: 0x0001AE43 File Offset: 0x00019043
		[DataSourceProperty]
		public string TroopType
		{
			get
			{
				return this._troopType;
			}
			set
			{
				if (value != this._troopType)
				{
					this._troopType = value;
					base.OnPropertyChanged<string>(value, "TroopType");
					this.UpdateIcon();
				}
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x0600097B RID: 2427 RVA: 0x0001AE6C File Offset: 0x0001906C
		// (set) Token: 0x0600097C RID: 2428 RVA: 0x0001AE74 File Offset: 0x00019074
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

		// Token: 0x0400044E RID: 1102
		private Widget _factionColorWidget;

		// Token: 0x0400044F RID: 1103
		private Color _cultureColor;

		// Token: 0x04000450 RID: 1104
		private string _troopType;

		// Token: 0x04000451 RID: 1105
		private Widget _iconWidget;
	}
}
