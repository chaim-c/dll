using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Map.Siege
{
	// Token: 0x0200010B RID: 267
	public class MapSiegeConstructionControllerWidget : Widget
	{
		// Token: 0x06000E07 RID: 3591 RVA: 0x0002705B File Offset: 0x0002525B
		public MapSiegeConstructionControllerWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x00027064 File Offset: 0x00025264
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			float num;
			if (this._currentWidget != null)
			{
				base.PositionXOffset = this._currentWidget.PositionXOffset + this._currentWidget.Size.X * base._inverseScaleToUse;
				base.PositionYOffset = this._currentWidget.PositionYOffset;
				num = this._currentWidget.ReadOnlyBrush.GlobalAlphaFactor;
			}
			else
			{
				base.PositionXOffset = -1000f;
				base.PositionYOffset = -1000f;
				num = 0f;
			}
			base.IsEnabled = (num >= 0.95f);
			this.SetGlobalAlphaRecursively(num);
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x00027107 File Offset: 0x00025307
		public void SetCurrentPOIWidget(MapSiegePOIBrushWidget widget)
		{
			if (widget == null || widget == this._currentWidget)
			{
				this._currentWidget = null;
				return;
			}
			this._currentWidget = (widget.IsPlayerSidePOI ? widget : null);
		}

		// Token: 0x0400067B RID: 1659
		private MapSiegePOIBrushWidget _currentWidget;
	}
}
