using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.HUD
{
	// Token: 0x020000BC RID: 188
	public class DuelArenaFlagVisualBrushWidget : BrushWidget
	{
		// Token: 0x060009E2 RID: 2530 RVA: 0x0001C152 File Offset: 0x0001A352
		public DuelArenaFlagVisualBrushWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x0001C164 File Offset: 0x0001A364
		private void UpdateVisual()
		{
			switch (this.ArenaType)
			{
			case 0:
				this.SetState("Infantry");
				return;
			case 1:
				this.SetState("Archery");
				return;
			case 2:
				this.SetState("Cavalry");
				return;
			default:
				this.SetState("Infantry");
				return;
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x060009E4 RID: 2532 RVA: 0x0001C1BB File Offset: 0x0001A3BB
		// (set) Token: 0x060009E5 RID: 2533 RVA: 0x0001C1C3 File Offset: 0x0001A3C3
		[Editor(false)]
		public int ArenaType
		{
			get
			{
				return this._arenaType;
			}
			set
			{
				if (this._arenaType != value)
				{
					this._arenaType = value;
					base.OnPropertyChanged(value, "ArenaType");
					this.UpdateVisual();
				}
			}
		}

		// Token: 0x0400047F RID: 1151
		private int _arenaType = -1;
	}
}
