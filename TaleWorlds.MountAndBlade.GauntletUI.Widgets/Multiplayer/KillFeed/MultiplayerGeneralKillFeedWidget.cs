using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.KillFeed
{
	// Token: 0x020000B9 RID: 185
	public class MultiplayerGeneralKillFeedWidget : Widget
	{
		// Token: 0x1700036B RID: 875
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x0001B6C1 File Offset: 0x000198C1
		// (set) Token: 0x060009B0 RID: 2480 RVA: 0x0001B6C9 File Offset: 0x000198C9
		public float VerticalPaddingAmount { get; set; } = 3f;

		// Token: 0x060009B1 RID: 2481 RVA: 0x0001B6D2 File Offset: 0x000198D2
		public MultiplayerGeneralKillFeedWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x0001B6F0 File Offset: 0x000198F0
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this._normalWidgetHeight <= 0f && base.ChildCount > 1)
			{
				this._normalWidgetHeight = base.GetChild(0).ScaledSuggestedHeight * base._inverseScaleToUse;
			}
			for (int i = 0; i < base.ChildCount; i++)
			{
				Widget child = base.GetChild(i);
				child.PositionYOffset = Mathf.Lerp(child.PositionYOffset, this.GetVerticalPositionOfChildByIndex(i, base.ChildCount), 0.35f);
			}
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x0001B76D File Offset: 0x0001996D
		protected override void OnChildAdded(Widget child)
		{
			base.OnChildAdded(child);
			child.PositionYOffset = this.GetVerticalPositionOfChildByIndex(child.GetSiblingIndex(), base.ChildCount);
			this.UpdateSpeedModifiers();
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0001B794 File Offset: 0x00019994
		private float GetVerticalPositionOfChildByIndex(int indexOfChild, int numOfTotalChild)
		{
			int num = numOfTotalChild - 1 - indexOfChild;
			return (this._normalWidgetHeight + this.VerticalPaddingAmount) * (float)num;
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0001B7B8 File Offset: 0x000199B8
		private void UpdateSpeedModifiers()
		{
			if (base.ChildCount > this._speedUpWidgetLimit)
			{
				float speedModifier = (float)(base.ChildCount - this._speedUpWidgetLimit) / 20f + 1f;
				for (int i = 0; i < base.ChildCount - this._speedUpWidgetLimit; i++)
				{
					(base.GetChild(i) as MultiplayerGeneralKillFeedItemWidget).SetSpeedModifier(speedModifier);
				}
			}
		}

		// Token: 0x0400046C RID: 1132
		private float _normalWidgetHeight;

		// Token: 0x0400046D RID: 1133
		private int _speedUpWidgetLimit = 10;
	}
}
