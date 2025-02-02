using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Mission.KillFeed.Personal
{
	// Token: 0x020000EF RID: 239
	public class SingleplayerPersonalKillFeedWidget : Widget
	{
		// Token: 0x06000CA6 RID: 3238 RVA: 0x00022E3A File Offset: 0x0002103A
		public SingleplayerPersonalKillFeedWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x00022E58 File Offset: 0x00021058
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this._normalWidgetHeight == -1f && base.ChildCount > 1)
			{
				this._normalWidgetHeight = base.GetChild(0).ScaledSuggestedHeight * base._inverseScaleToUse;
			}
			for (int i = 0; i < base.ChildCount; i++)
			{
				Widget child = base.GetChild(i);
				child.PositionYOffset = Mathf.Lerp(child.PositionYOffset, this.GetVerticalPositionOfChildByIndex(i, base.ChildCount), 0.2f);
			}
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x00022ED5 File Offset: 0x000210D5
		protected override void OnChildAdded(Widget child)
		{
			base.OnChildAdded(child);
			child.PositionYOffset = this.GetVerticalPositionOfChildByIndex(child.GetSiblingIndex(), base.ChildCount);
			this.UpdateSpeedModifiers();
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x00022EFC File Offset: 0x000210FC
		private float GetVerticalPositionOfChildByIndex(int indexOfChild, int numOfTotalChild)
		{
			return -(this._normalWidgetHeight * (float)indexOfChild);
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x00022F08 File Offset: 0x00021108
		private void UpdateSpeedModifiers()
		{
			if (base.ChildCount > this._speedUpWidgetLimit)
			{
				float speedModifier = (float)(base.ChildCount - this._speedUpWidgetLimit) / 3f + 1f;
				for (int i = 0; i < base.ChildCount - this._speedUpWidgetLimit; i++)
				{
					(base.GetChild(i) as SingleplayerPersonalKillFeedItemWidget).SetSpeedModifier(speedModifier);
				}
			}
		}

		// Token: 0x040005C4 RID: 1476
		private float _normalWidgetHeight = -1f;

		// Token: 0x040005C5 RID: 1477
		private int _speedUpWidgetLimit = 3;
	}
}
