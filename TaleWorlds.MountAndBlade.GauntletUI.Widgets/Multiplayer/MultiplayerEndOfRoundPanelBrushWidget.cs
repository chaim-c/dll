using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer
{
	// Token: 0x02000080 RID: 128
	public class MultiplayerEndOfRoundPanelBrushWidget : BrushWidget
	{
		// Token: 0x06000723 RID: 1827 RVA: 0x0001532F File Offset: 0x0001352F
		public MultiplayerEndOfRoundPanelBrushWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00015338 File Offset: 0x00013538
		private void IsShownUpdated()
		{
			if (this.IsShown)
			{
				string eventName = this.IsRoundWinner ? "Victory" : "Defeat";
				base.EventFired(eventName, Array.Empty<object>());
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000725 RID: 1829 RVA: 0x0001536E File Offset: 0x0001356E
		// (set) Token: 0x06000726 RID: 1830 RVA: 0x00015376 File Offset: 0x00013576
		[DataSourceProperty]
		public bool IsShown
		{
			get
			{
				return this._isShown;
			}
			set
			{
				if (value != this._isShown)
				{
					this._isShown = value;
					base.OnPropertyChanged(value, "IsShown");
					this.IsShownUpdated();
				}
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000727 RID: 1831 RVA: 0x0001539A File Offset: 0x0001359A
		// (set) Token: 0x06000728 RID: 1832 RVA: 0x000153A2 File Offset: 0x000135A2
		[DataSourceProperty]
		public bool IsRoundWinner
		{
			get
			{
				return this._isRoundWinner;
			}
			set
			{
				if (value != this._isRoundWinner)
				{
					this._isRoundWinner = value;
					base.OnPropertyChanged(value, "IsRoundWinner");
				}
			}
		}

		// Token: 0x04000329 RID: 809
		private bool _isShown;

		// Token: 0x0400032A RID: 810
		private bool _isRoundWinner;
	}
}
