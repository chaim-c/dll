using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.KillFeed
{
	// Token: 0x020000BB RID: 187
	public class MultiplayerPersonalKillFeedWidget : Widget
	{
		// Token: 0x17000379 RID: 889
		// (get) Token: 0x060009DB RID: 2523 RVA: 0x0001BF8E File Offset: 0x0001A18E
		private int _speedUpWidgetLimit
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0001BF91 File Offset: 0x0001A191
		public MultiplayerPersonalKillFeedWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0001BF9C File Offset: 0x0001A19C
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			for (int i = 0; i < base.ChildCount; i++)
			{
				Widget child = base.GetChild(i);
				child.PositionYOffset = Mathf.Lerp(child.PositionYOffset, this.GetVerticalPositionOfChildByIndex(i), 0.35f);
			}
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x0001BFE4 File Offset: 0x0001A1E4
		protected override void OnChildAdded(Widget child)
		{
			base.OnChildAdded(child);
			this.UpdateSpeedModifiers();
			this.UpdateMaxTargetAlphas();
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x0001BFFC File Offset: 0x0001A1FC
		private void UpdateMaxTargetAlphas()
		{
			for (int i = base.ChildCount - 1; i >= 0; i--)
			{
				MultiplayerPersonalKillFeedItemWidget multiplayerPersonalKillFeedItemWidget = base.GetChild(i) as MultiplayerPersonalKillFeedItemWidget;
				if (i <= base.ChildCount - 1 && i >= base.ChildCount - 4)
				{
					multiplayerPersonalKillFeedItemWidget.SetMaxAlphaValue(1f);
				}
				else if (i == base.ChildCount - 5)
				{
					multiplayerPersonalKillFeedItemWidget.SetMaxAlphaValue(0.7f);
				}
				else if (i == base.ChildCount - 6)
				{
					multiplayerPersonalKillFeedItemWidget.SetMaxAlphaValue(0.4f);
				}
				else if (i == base.ChildCount - 7)
				{
					multiplayerPersonalKillFeedItemWidget.SetMaxAlphaValue(0.15f);
				}
				else
				{
					multiplayerPersonalKillFeedItemWidget.SetMaxAlphaValue(0f);
				}
			}
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x0001C0A8 File Offset: 0x0001A2A8
		private float GetVerticalPositionOfChildByIndex(int indexOfChild)
		{
			float num = 0f;
			for (int i = base.ChildCount - 1; i > indexOfChild; i--)
			{
				num += base.GetChild(i).Size.Y * base._inverseScaleToUse;
			}
			return num;
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x0001C0EC File Offset: 0x0001A2EC
		private void UpdateSpeedModifiers()
		{
			if (base.ChildCount > this._speedUpWidgetLimit)
			{
				float speedModifier = (float)(base.ChildCount - this._speedUpWidgetLimit) / 2f + 1f;
				for (int i = 0; i < base.ChildCount - this._speedUpWidgetLimit; i++)
				{
					MultiplayerPersonalKillFeedItemWidget multiplayerPersonalKillFeedItemWidget = base.GetChild(i) as MultiplayerPersonalKillFeedItemWidget;
					if (multiplayerPersonalKillFeedItemWidget != null)
					{
						multiplayerPersonalKillFeedItemWidget.SetSpeedModifier(speedModifier);
					}
				}
			}
		}
	}
}
