using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Kingdom
{
	// Token: 0x02000122 RID: 290
	public class KingdomCardItemContainerWidget : Widget
	{
		// Token: 0x06000F07 RID: 3847 RVA: 0x000299FB File Offset: 0x00027BFB
		public KingdomCardItemContainerWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x00029A1A File Offset: 0x00027C1A
		protected override void OnChildRemoved(Widget child)
		{
			base.OnChildRemoved(child);
			child.EventFire -= this.ChildrenWidgetEventFired;
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x00029A35 File Offset: 0x00027C35
		protected override void OnChildAdded(Widget child)
		{
			base.OnChildAdded(child);
			child.EventFire += this.ChildrenWidgetEventFired;
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x00029A50 File Offset: 0x00027C50
		private void ChildrenWidgetEventFired(Widget widget, string eventName, object[] args)
		{
			if (eventName == "HoverBegin")
			{
				this._isMouseOverChildren = true;
				widget.RenderLate = true;
				return;
			}
			if (eventName == "HoverEnd")
			{
				this._isMouseOverChildren = false;
				widget.RenderLate = false;
			}
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x00029A8C File Offset: 0x00027C8C
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			float num = 0f;
			float num2 = 0f;
			if (base.ChildCount > 0)
			{
				num = base.GetChild(0).Size.X * (float)base.ChildCount;
				num2 = this._defaultXOffset * base._inverseScaleToUse * (float)(base.ChildCount - 1) + base.GetChild(0).Size.X;
				base.IsEnabled = true;
			}
			else
			{
				base.IsEnabled = false;
			}
			for (int i = 0; i < base.ChildCount; i++)
			{
				Widget child = base.GetChild(i);
				if (this._isMouseOverChildren || this._isMouseOverSelf)
				{
					if (base.ChildCount > 1)
					{
						if (num < base.Size.X)
						{
							float num3 = base.Size.X / 2f - num / 2f;
							this._targetXOffset = (float)i * child.Size.X + num3;
						}
						else
						{
							this._targetXOffset = (float)i / ((float)base.ChildCount - 1f) * (base.Size.X - child.Size.X);
						}
					}
					else if (base.ChildCount == 1)
					{
						this._targetXOffset = base.Size.X / 2f - child.Size.X / 2f;
					}
				}
				else if (base.ChildCount > 1)
				{
					float num4 = this._defaultXOffset;
					while (num2 > base.Size.X && num4 > 5f)
					{
						num4 -= 0.5f;
						num2 = num4 * (float)(base.ChildCount - 1) + child.Size.X;
					}
					this._targetXOffset = base.Size.X / 2f - num2 / 2f + num4 * (float)i;
				}
				else if (base.ChildCount == 1)
				{
					this._targetXOffset = base.Size.X / 2f - child.Size.X / 2f;
				}
				child.PositionXOffset = Mathf.Lerp(child.PositionXOffset, this._targetXOffset * base._inverseScaleToUse, dt * this._lerpFactor);
			}
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x00029CC2 File Offset: 0x00027EC2
		protected override void OnHoverBegin()
		{
			base.OnHoverBegin();
			this._isMouseOverSelf = true;
			base.RenderLate = true;
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x00029CD8 File Offset: 0x00027ED8
		protected override void OnHoverEnd()
		{
			base.OnHoverEnd();
			this._isMouseOverSelf = false;
			base.RenderLate = false;
		}

		// Token: 0x040006E4 RID: 1764
		private float _targetXOffset;

		// Token: 0x040006E5 RID: 1765
		private bool _isMouseOverChildren;

		// Token: 0x040006E6 RID: 1766
		private bool _isMouseOverSelf;

		// Token: 0x040006E7 RID: 1767
		private float _lerpFactor = 15f;

		// Token: 0x040006E8 RID: 1768
		private float _defaultXOffset = 20f;
	}
}
