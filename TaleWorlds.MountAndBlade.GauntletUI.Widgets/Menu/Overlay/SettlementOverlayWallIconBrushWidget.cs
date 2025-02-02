using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Menu.Overlay
{
	// Token: 0x02000106 RID: 262
	public class SettlementOverlayWallIconBrushWidget : BrushWidget
	{
		// Token: 0x06000DE9 RID: 3561 RVA: 0x00026997 File Offset: 0x00024B97
		public SettlementOverlayWallIconBrushWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x000269A0 File Offset: 0x00024BA0
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			this.SetState(this.WallsLevel.ToString());
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06000DEB RID: 3563 RVA: 0x000269C8 File Offset: 0x00024BC8
		// (set) Token: 0x06000DEC RID: 3564 RVA: 0x000269D0 File Offset: 0x00024BD0
		[Editor(false)]
		public int WallsLevel
		{
			get
			{
				return this._wallsLevel;
			}
			set
			{
				if (this._wallsLevel != value)
				{
					this._wallsLevel = value;
					base.OnPropertyChanged(value, "WallsLevel");
				}
			}
		}

		// Token: 0x0400066B RID: 1643
		private int _wallsLevel;
	}
}
