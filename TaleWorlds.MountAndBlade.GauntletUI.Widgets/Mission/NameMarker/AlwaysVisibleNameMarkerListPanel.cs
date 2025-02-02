using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Mission.NameMarker
{
	// Token: 0x020000E9 RID: 233
	public class AlwaysVisibleNameMarkerListPanel : ListPanel
	{
		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06000C0D RID: 3085 RVA: 0x000211F5 File Offset: 0x0001F3F5
		private float _normalOpacity
		{
			get
			{
				return 0.5f;
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x000211FC File Offset: 0x0001F3FC
		private float _screenCenterOpacity
		{
			get
			{
				return 0.15f;
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06000C0F RID: 3087 RVA: 0x00021203 File Offset: 0x0001F403
		private float _stayOnScreenTimeInSeconds
		{
			get
			{
				return 5f;
			}
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x0002120A File Offset: 0x0001F40A
		public AlwaysVisibleNameMarkerListPanel(UIContext context) : base(context)
		{
			this._parentScreenWidget = base.EventManager.Root.GetChild(0).GetChild(0);
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x00021230 File Offset: 0x0001F430
		protected override void OnLateUpdate(float dt)
		{
			foreach (Widget widget in base.AllChildrenAndThis)
			{
				widget.IsVisible = true;
			}
			base.ScaledPositionYOffset = this.Position.y - base.Size.Y / 2f;
			base.ScaledPositionXOffset = this.Position.x - base.Size.X / 2f;
			this.UpdateOpacity();
			if (this._totalDt > this._stayOnScreenTimeInSeconds)
			{
				base.EventFired("Remove", Array.Empty<object>());
			}
			this._totalDt += dt;
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x000212F4 File Offset: 0x0001F4F4
		private void UpdateOpacity()
		{
			Vec2 v = new Vec2(base.Context.TwoDimensionContext.Platform.Width / 2f, base.Context.TwoDimensionContext.Platform.Height / 2f);
			Vec2 vec = new Vec2(base.ScaledPositionXOffset, base.ScaledPositionYOffset);
			float alphaFactor = (vec.Distance(v) <= 150f) ? this._screenCenterOpacity : this._normalOpacity;
			this.SetGlobalAlphaRecursively(alphaFactor);
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06000C13 RID: 3091 RVA: 0x0002137B File Offset: 0x0001F57B
		// (set) Token: 0x06000C14 RID: 3092 RVA: 0x00021383 File Offset: 0x0001F583
		[DataSourceProperty]
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

		// Token: 0x0400057C RID: 1404
		private Widget _parentScreenWidget;

		// Token: 0x0400057D RID: 1405
		private float _totalDt;

		// Token: 0x0400057E RID: 1406
		private Vec2 _position;
	}
}
