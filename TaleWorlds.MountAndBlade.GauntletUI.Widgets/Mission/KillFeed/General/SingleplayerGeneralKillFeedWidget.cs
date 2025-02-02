using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Mission.KillFeed.General
{
	// Token: 0x020000F1 RID: 241
	public class SingleplayerGeneralKillFeedWidget : Widget
	{
		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06000CC6 RID: 3270 RVA: 0x00023391 File Offset: 0x00021591
		// (set) Token: 0x06000CC7 RID: 3271 RVA: 0x00023399 File Offset: 0x00021599
		public float VerticalPaddingAmount { get; set; } = 3f;

		// Token: 0x06000CC8 RID: 3272 RVA: 0x000233A2 File Offset: 0x000215A2
		public SingleplayerGeneralKillFeedWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x000233C0 File Offset: 0x000215C0
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

		// Token: 0x06000CCA RID: 3274 RVA: 0x0002343D File Offset: 0x0002163D
		protected override void OnChildAdded(Widget child)
		{
			base.OnChildAdded(child);
			child.PositionYOffset = this.GetVerticalPositionOfChildByIndex(child.GetSiblingIndex(), base.ChildCount);
			this.UpdateSpeedModifiers();
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x00023464 File Offset: 0x00021664
		private float GetVerticalPositionOfChildByIndex(int indexOfChild, int numOfTotalChild)
		{
			return (this._normalWidgetHeight + this.VerticalPaddingAmount) * (float)(numOfTotalChild - 1 - indexOfChild);
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x0002347C File Offset: 0x0002167C
		private void UpdateSpeedModifiers()
		{
			if (base.ChildCount > this._speedUpWidgetLimit)
			{
				float speedModifier = (float)(base.ChildCount - this._speedUpWidgetLimit) / 20f + 1f;
				for (int i = 0; i < base.ChildCount - this._speedUpWidgetLimit; i++)
				{
					(base.GetChild(i) as SingleplayerGeneralKillFeedItemWidget).SetSpeedModifier(speedModifier);
				}
			}
		}

		// Token: 0x040005DB RID: 1499
		private float _normalWidgetHeight;

		// Token: 0x040005DC RID: 1500
		private int _speedUpWidgetLimit = 10;
	}
}
