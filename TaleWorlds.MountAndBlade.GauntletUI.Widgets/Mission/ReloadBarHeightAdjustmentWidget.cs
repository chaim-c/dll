using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Mission
{
	// Token: 0x020000D9 RID: 217
	public class ReloadBarHeightAdjustmentWidget : Widget
	{
		// Token: 0x06000B52 RID: 2898 RVA: 0x0001FBA3 File Offset: 0x0001DDA3
		public ReloadBarHeightAdjustmentWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x0001FBAC File Offset: 0x0001DDAC
		private void Refresh()
		{
			if (this.FillWidget != null)
			{
				base.ScaledSuggestedHeight = 50f * this.RelativeDurationToMaxDuration * base._scaleToUse;
				this.FillWidget.ScaledSuggestedHeight = base.ScaledSuggestedHeight - (this.FillWidget.MarginBottom + this.FillWidget.MarginTop) * base._scaleToUse;
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000B54 RID: 2900 RVA: 0x0001FC0A File Offset: 0x0001DE0A
		// (set) Token: 0x06000B55 RID: 2901 RVA: 0x0001FC12 File Offset: 0x0001DE12
		public float RelativeDurationToMaxDuration
		{
			get
			{
				return this._relativeDurationToMaxDuration;
			}
			set
			{
				if (value != this._relativeDurationToMaxDuration)
				{
					this._relativeDurationToMaxDuration = value;
					this.Refresh();
				}
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000B56 RID: 2902 RVA: 0x0001FC2A File Offset: 0x0001DE2A
		// (set) Token: 0x06000B57 RID: 2903 RVA: 0x0001FC32 File Offset: 0x0001DE32
		public Widget FillWidget
		{
			get
			{
				return this._fillWidget;
			}
			set
			{
				if (value != this._fillWidget)
				{
					this._fillWidget = value;
					this.Refresh();
				}
			}
		}

		// Token: 0x04000528 RID: 1320
		private const float _baseHeight = 50f;

		// Token: 0x04000529 RID: 1321
		private float _relativeDurationToMaxDuration;

		// Token: 0x0400052A RID: 1322
		private Widget _fillWidget;
	}
}
