using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Map.MapEvents
{
	// Token: 0x02000115 RID: 277
	public class MapEventVisualItemWidget : Widget
	{
		// Token: 0x06000E8E RID: 3726 RVA: 0x00028886 File Offset: 0x00026A86
		public MapEventVisualItemWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x0002888F File Offset: 0x00026A8F
		protected override void OnParallelUpdate(float dt)
		{
			base.OnParallelUpdate(dt);
			this.UpdatePosition();
			this.UpdateVisibility();
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x000288A4 File Offset: 0x00026AA4
		private void UpdateVisibility()
		{
			base.IsVisible = this.IsVisibleOnMap;
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x000288B4 File Offset: 0x00026AB4
		private void UpdatePosition()
		{
			if (this.IsVisibleOnMap)
			{
				base.ScaledPositionXOffset = this.Position.x - base.Size.X / 2f;
				base.ScaledPositionYOffset = this.Position.y - base.Size.Y;
				return;
			}
			base.ScaledPositionXOffset = -10000f;
			base.ScaledPositionYOffset = -10000f;
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06000E92 RID: 3730 RVA: 0x00028920 File Offset: 0x00026B20
		// (set) Token: 0x06000E93 RID: 3731 RVA: 0x00028928 File Offset: 0x00026B28
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

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06000E94 RID: 3732 RVA: 0x0002894B File Offset: 0x00026B4B
		// (set) Token: 0x06000E95 RID: 3733 RVA: 0x00028953 File Offset: 0x00026B53
		public bool IsVisibleOnMap
		{
			get
			{
				return this._isVisibleOnMap;
			}
			set
			{
				if (this._isVisibleOnMap != value)
				{
					this._isVisibleOnMap = value;
					base.OnPropertyChanged(value, "IsVisibleOnMap");
				}
			}
		}

		// Token: 0x040006B3 RID: 1715
		private Vec2 _position;

		// Token: 0x040006B4 RID: 1716
		private bool _isVisibleOnMap;
	}
}
