using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.Lobby.Home
{
	// Token: 0x020000A6 RID: 166
	public class MultiplayerLobbyAnnouncementIconBrushWidget : BrushWidget
	{
		// Token: 0x060008D6 RID: 2262 RVA: 0x00019674 File Offset: 0x00017874
		public MultiplayerLobbyAnnouncementIconBrushWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x00019680 File Offset: 0x00017880
		private void UpdateIcon()
		{
			if (this.AnnouncementType == null)
			{
				return;
			}
			Brush iconBrush = this.IconBrush;
			Sprite sprite;
			if (iconBrush == null)
			{
				sprite = null;
			}
			else
			{
				BrushLayer layer = iconBrush.GetLayer(this.AnnouncementType);
				sprite = ((layer != null) ? layer.Sprite : null);
			}
			Sprite sprite2 = sprite;
			if (base.Brush != null)
			{
				base.Brush.Sprite = sprite2;
				foreach (BrushLayer brushLayer in base.Brush.Layers)
				{
					brushLayer.Sprite = sprite2;
				}
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x060008D8 RID: 2264 RVA: 0x00019718 File Offset: 0x00017918
		// (set) Token: 0x060008D9 RID: 2265 RVA: 0x00019720 File Offset: 0x00017920
		public string AnnouncementType
		{
			get
			{
				return this._announcementType;
			}
			set
			{
				if (value != this._announcementType)
				{
					this._announcementType = value;
					base.OnPropertyChanged<string>(value, "AnnouncementType");
					this.UpdateIcon();
				}
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x060008DA RID: 2266 RVA: 0x00019749 File Offset: 0x00017949
		// (set) Token: 0x060008DB RID: 2267 RVA: 0x00019751 File Offset: 0x00017951
		public Brush IconBrush
		{
			get
			{
				return this._iconBrush;
			}
			set
			{
				if (value != this._iconBrush)
				{
					this._iconBrush = value;
					base.OnPropertyChanged<Brush>(value, "IconBrush");
					this.UpdateIcon();
				}
			}
		}

		// Token: 0x0400040C RID: 1036
		private string _announcementType;

		// Token: 0x0400040D RID: 1037
		private Brush _iconBrush;
	}
}
