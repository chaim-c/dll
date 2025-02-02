using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer
{
	// Token: 0x02000082 RID: 130
	public class MultiplayerIntermissionNextMapImageWidget : Widget
	{
		// Token: 0x06000737 RID: 1847 RVA: 0x000155DA File Offset: 0x000137DA
		public MultiplayerIntermissionNextMapImageWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x000155E3 File Offset: 0x000137E3
		private void UpdateMapImage()
		{
			if (string.IsNullOrEmpty(this.MapID))
			{
				return;
			}
			base.Sprite = base.Context.SpriteData.GetSprite(this.MapID);
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x0001560F File Offset: 0x0001380F
		// (set) Token: 0x0600073A RID: 1850 RVA: 0x00015617 File Offset: 0x00013817
		[DataSourceProperty]
		public string MapID
		{
			get
			{
				return this._mapID;
			}
			set
			{
				if (value != this._mapID)
				{
					this._mapID = value;
					base.OnPropertyChanged<string>(value, "MapID");
					this.UpdateMapImage();
				}
			}
		}

		// Token: 0x04000331 RID: 817
		private string _mapID;
	}
}
