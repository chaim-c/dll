using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.Lobby.Armory
{
	// Token: 0x020000B5 RID: 181
	public class TauntEquipmentUsageIconBrushWidget : BrushWidget
	{
		// Token: 0x0600098A RID: 2442 RVA: 0x0001B1A0 File Offset: 0x000193A0
		public TauntEquipmentUsageIconBrushWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x0001B1AC File Offset: 0x000193AC
		private void OnUsageNameUpdated(string usageName)
		{
			Sprite sprite;
			if (usageName == null)
			{
				sprite = null;
			}
			else
			{
				Brush iconsBrush = this.IconsBrush;
				if (iconsBrush == null)
				{
					sprite = null;
				}
				else
				{
					BrushLayer layer = iconsBrush.GetLayer(usageName);
					sprite = ((layer != null) ? layer.Sprite : null);
				}
			}
			Sprite sprite2 = sprite;
			base.IsVisible = (sprite2 != null);
			if (base.IsVisible)
			{
				base.Brush.Sprite = sprite2;
				base.Brush.DefaultLayer.Sprite = sprite2;
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x0600098C RID: 2444 RVA: 0x0001B20E File Offset: 0x0001940E
		// (set) Token: 0x0600098D RID: 2445 RVA: 0x0001B216 File Offset: 0x00019416
		public Brush IconsBrush
		{
			get
			{
				return this._iconsBrush;
			}
			set
			{
				if (value != this._iconsBrush)
				{
					this._iconsBrush = value;
					base.OnPropertyChanged<Brush>(value, "IconsBrush");
					this.OnUsageNameUpdated(this.UsageName);
				}
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x0600098E RID: 2446 RVA: 0x0001B240 File Offset: 0x00019440
		// (set) Token: 0x0600098F RID: 2447 RVA: 0x0001B248 File Offset: 0x00019448
		public string UsageName
		{
			get
			{
				return this._usageName;
			}
			set
			{
				if (value != this._usageName)
				{
					this._usageName = value;
					base.OnPropertyChanged<string>(value, "UsageName");
					this.OnUsageNameUpdated(value);
				}
			}
		}

		// Token: 0x04000458 RID: 1112
		private Brush _iconsBrush;

		// Token: 0x04000459 RID: 1113
		private string _usageName;
	}
}
