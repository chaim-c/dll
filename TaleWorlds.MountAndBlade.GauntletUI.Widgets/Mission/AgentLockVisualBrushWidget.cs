using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Mission
{
	// Token: 0x020000CE RID: 206
	public class AgentLockVisualBrushWidget : BrushWidget
	{
		// Token: 0x06000A9F RID: 2719 RVA: 0x0001E100 File Offset: 0x0001C300
		public AgentLockVisualBrushWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x0001E110 File Offset: 0x0001C310
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			base.ScaledPositionXOffset = this.Position.X - base.Size.X / 2f;
			base.ScaledPositionYOffset = this.Position.Y - base.Size.Y / 2f;
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x0001E170 File Offset: 0x0001C370
		private void UpdateVisualState(int lockState)
		{
			if (lockState == 0)
			{
				this.SetState("Possible");
				return;
			}
			if (lockState != 1)
			{
				return;
			}
			this.SetState("Active");
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000AA2 RID: 2722 RVA: 0x0001E191 File Offset: 0x0001C391
		// (set) Token: 0x06000AA3 RID: 2723 RVA: 0x0001E199 File Offset: 0x0001C399
		[Editor(false)]
		public Vec2 Position
		{
			get
			{
				return this._position;
			}
			set
			{
				if (this._position != value)
				{
					this._position = value;
					base.OnPropertyChanged(value, "Position");
				}
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000AA4 RID: 2724 RVA: 0x0001E1BC File Offset: 0x0001C3BC
		// (set) Token: 0x06000AA5 RID: 2725 RVA: 0x0001E1C4 File Offset: 0x0001C3C4
		[Editor(false)]
		public int LockState
		{
			get
			{
				return this._lockState;
			}
			set
			{
				if (this._lockState != value)
				{
					this._lockState = value;
					base.OnPropertyChanged(value, "LockState");
					this.UpdateVisualState(value);
				}
			}
		}

		// Token: 0x040004DB RID: 1243
		private Vec2 _position;

		// Token: 0x040004DC RID: 1244
		private int _lockState = -1;
	}
}
