using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Map
{
	// Token: 0x02000109 RID: 265
	public class MapEventVisualBrushWidget : BrushWidget
	{
		// Token: 0x06000DF2 RID: 3570 RVA: 0x00026ACE File Offset: 0x00024CCE
		public MapEventVisualBrushWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x00026AE8 File Offset: 0x00024CE8
		private void UpdateVisual(int type)
		{
			if (this._initialUpdate)
			{
				this.RegisterBrushStatesOfWidget();
				this._initialUpdate = false;
			}
			switch (type)
			{
			case 1:
				this.SetState("Raid");
				return;
			case 2:
				this.SetState("Siege");
				return;
			case 3:
				this.SetState("Battle");
				return;
			case 4:
				this.SetState("Rebellion");
				return;
			case 5:
				this.SetState("SallyOut");
				return;
			default:
				this.SetState("None");
				return;
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06000DF4 RID: 3572 RVA: 0x00026B6F File Offset: 0x00024D6F
		// (set) Token: 0x06000DF5 RID: 3573 RVA: 0x00026B77 File Offset: 0x00024D77
		[Editor(false)]
		public int MapEventType
		{
			get
			{
				return this._mapEventType;
			}
			set
			{
				if (this._mapEventType != value)
				{
					this._mapEventType = value;
					this.UpdateVisual(value);
				}
			}
		}

		// Token: 0x0400066F RID: 1647
		private bool _initialUpdate = true;

		// Token: 0x04000670 RID: 1648
		private int _mapEventType = -1;
	}
}
