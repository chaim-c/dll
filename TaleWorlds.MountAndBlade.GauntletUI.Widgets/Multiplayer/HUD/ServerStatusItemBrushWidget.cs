using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.HUD
{
	// Token: 0x020000C1 RID: 193
	public class ServerStatusItemBrushWidget : BrushWidget
	{
		// Token: 0x06000A3C RID: 2620 RVA: 0x0001D084 File Offset: 0x0001B284
		public ServerStatusItemBrushWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0001D094 File Offset: 0x0001B294
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (!this._initialized)
			{
				this.RegisterBrushStatesOfWidget();
				this._initialized = true;
				this.OnStatusChange(this.Status);
			}
			if (Math.Abs(base.ReadOnlyBrush.GlobalAlphaFactor - this._currentAlphaTarget) > 0.001f)
			{
				this.SetGlobalAlphaRecursively(MathF.Lerp(base.ReadOnlyBrush.GlobalAlphaFactor, this._currentAlphaTarget, dt * 5f, 1E-05f));
			}
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0001D10F File Offset: 0x0001B30F
		private void OnStatusChange(int value)
		{
			this.SetState(value.ToString());
			if (value == 0)
			{
				this._currentAlphaTarget = 0f;
				return;
			}
			if (value - 1 > 1)
			{
				return;
			}
			this._currentAlphaTarget = 1f;
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000A3F RID: 2623 RVA: 0x0001D13F File Offset: 0x0001B33F
		// (set) Token: 0x06000A40 RID: 2624 RVA: 0x0001D147 File Offset: 0x0001B347
		public int Status
		{
			get
			{
				return this._status;
			}
			set
			{
				if (value != this._status)
				{
					this._status = value;
					base.OnPropertyChanged(value, "Status");
					this.OnStatusChange(value);
				}
			}
		}

		// Token: 0x040004B5 RID: 1205
		private float _currentAlphaTarget;

		// Token: 0x040004B6 RID: 1206
		private bool _initialized;

		// Token: 0x040004B7 RID: 1207
		private int _status = -1;
	}
}
