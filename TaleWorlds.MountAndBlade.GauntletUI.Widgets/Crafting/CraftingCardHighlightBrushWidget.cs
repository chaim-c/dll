using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Crafting
{
	// Token: 0x02000157 RID: 343
	public class CraftingCardHighlightBrushWidget : BrushWidget
	{
		// Token: 0x06001221 RID: 4641 RVA: 0x00032004 File Offset: 0x00030204
		public CraftingCardHighlightBrushWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x00032014 File Offset: 0x00030214
		protected override void OnParallelUpdate(float dt)
		{
			base.OnParallelUpdate(dt);
			if (this._firstFrame && base.IsVisible)
			{
				this._firstFrame = false;
				return;
			}
			if (!this._playingAnimation && !this._firstFrame)
			{
				base.BrushRenderer.RestartAnimation();
				this._playingAnimation = true;
			}
		}

		// Token: 0x04000846 RID: 2118
		private bool _playingAnimation;

		// Token: 0x04000847 RID: 2119
		private bool _firstFrame = true;
	}
}
