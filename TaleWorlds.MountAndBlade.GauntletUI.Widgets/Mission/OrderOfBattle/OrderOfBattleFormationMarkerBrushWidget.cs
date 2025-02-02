using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Mission.OrderOfBattle
{
	// Token: 0x020000E4 RID: 228
	public class OrderOfBattleFormationMarkerBrushWidget : BrushWidget
	{
		// Token: 0x06000BDA RID: 3034 RVA: 0x00020A78 File Offset: 0x0001EC78
		public OrderOfBattleFormationMarkerBrushWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x00020A84 File Offset: 0x0001EC84
		protected override void OnUpdate(float dt)
		{
			base.IsVisible = (this.IsAvailable && this.WSign > 0);
			if (base.IsVisible)
			{
				base.ScaledPositionXOffset = this.Position.x - base.Size.X / 2f;
				base.ScaledPositionYOffset = this.Position.y - base.Size.Y / 2f;
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000BDC RID: 3036 RVA: 0x00020AF9 File Offset: 0x0001ECF9
		// (set) Token: 0x06000BDD RID: 3037 RVA: 0x00020B01 File Offset: 0x0001ED01
		[Editor(false)]
		public Vec2 Position
		{
			get
			{
				return this._position;
			}
			set
			{
				if (value != this._position)
				{
					this._position = value;
					base.OnPropertyChanged(value, "Position");
				}
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06000BDE RID: 3038 RVA: 0x00020B24 File Offset: 0x0001ED24
		// (set) Token: 0x06000BDF RID: 3039 RVA: 0x00020B2C File Offset: 0x0001ED2C
		[Editor(false)]
		public bool IsAvailable
		{
			get
			{
				return this._isAvailable;
			}
			set
			{
				if (value != this._isAvailable)
				{
					this._isAvailable = value;
					base.OnPropertyChanged(value, "IsAvailable");
				}
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000BE0 RID: 3040 RVA: 0x00020B4A File Offset: 0x0001ED4A
		// (set) Token: 0x06000BE1 RID: 3041 RVA: 0x00020B52 File Offset: 0x0001ED52
		[Editor(false)]
		public bool IsTracked
		{
			get
			{
				return this._isTracked;
			}
			set
			{
				if (value != this._isTracked)
				{
					this._isTracked = value;
					base.OnPropertyChanged(value, "IsTracked");
				}
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06000BE2 RID: 3042 RVA: 0x00020B70 File Offset: 0x0001ED70
		// (set) Token: 0x06000BE3 RID: 3043 RVA: 0x00020B78 File Offset: 0x0001ED78
		[Editor(false)]
		public int WSign
		{
			get
			{
				return this._wSign;
			}
			set
			{
				if (this._wSign != value)
				{
					this._wSign = value;
					base.OnPropertyChanged(value, "WSign");
				}
			}
		}

		// Token: 0x04000564 RID: 1380
		private Vec2 _position;

		// Token: 0x04000565 RID: 1381
		private bool _isAvailable;

		// Token: 0x04000566 RID: 1382
		private bool _isTracked;

		// Token: 0x04000567 RID: 1383
		private int _wSign;
	}
}
