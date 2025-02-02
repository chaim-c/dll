using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.ExtraWidgets;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.CharacterDeveloper
{
	// Token: 0x0200017A RID: 378
	public class SkillProgressFillBarWidget : FillBarWidget
	{
		// Token: 0x0600138A RID: 5002 RVA: 0x00035824 File Offset: 0x00033A24
		public SkillProgressFillBarWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x00035830 File Offset: 0x00033A30
		protected override void OnRender(TwoDimensionContext twoDimensionContext, TwoDimensionDrawContext drawContext)
		{
			if (this.PercentageIndicatorWidget != null)
			{
				base.ScaledPositionXOffset = Mathf.Clamp((this.PercentageIndicatorWidget.ScaledPositionXOffset - base.Size.X / 2f) * base._scaleToUse, 0f, 600f * base._scaleToUse);
			}
			base.OnRender(twoDimensionContext, drawContext);
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x0600138C RID: 5004 RVA: 0x0003588D File Offset: 0x00033A8D
		// (set) Token: 0x0600138D RID: 5005 RVA: 0x00035895 File Offset: 0x00033A95
		public Widget PercentageIndicatorWidget
		{
			get
			{
				return this._percentageIndicatorWidget;
			}
			set
			{
				if (this._percentageIndicatorWidget != value)
				{
					this._percentageIndicatorWidget = value;
					base.OnPropertyChanged<Widget>(value, "PercentageIndicatorWidget");
				}
			}
		}

		// Token: 0x040008E8 RID: 2280
		private Widget _percentageIndicatorWidget;
	}
}
