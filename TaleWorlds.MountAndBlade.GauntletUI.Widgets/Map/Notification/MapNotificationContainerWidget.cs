using System;
using System.Collections.Generic;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Map.Notification
{
	// Token: 0x02000110 RID: 272
	public class MapNotificationContainerWidget : Widget
	{
		// Token: 0x06000E49 RID: 3657 RVA: 0x000278BC File Offset: 0x00025ABC
		public MapNotificationContainerWidget(UIContext context) : base(context)
		{
			this._newChildren = new List<Widget>();
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x000278D8 File Offset: 0x00025AD8
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this._newChildren.Count > 0)
			{
				foreach (Widget widget in this._newChildren)
				{
					widget.PositionYOffset = this.DetermineChildTargetYOffset(widget, base.GetChildIndex(widget));
				}
				this.DetermineChildrenVisibility();
				this.DetermineMoreTextStatus();
				this.DetermineNavigationIndicies();
				this._newChildren.Clear();
			}
			for (int i = 0; i < base.ChildCount; i++)
			{
				Widget child = base.GetChild(i);
				if (i < this.MaxAmountOfNotificationsToShow)
				{
					float end = this.DetermineChildTargetYOffset(child, i);
					child.PositionYOffset = this.LocalLerp(child.PositionYOffset, end, dt * 18f);
				}
			}
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x000279B4 File Offset: 0x00025BB4
		private void DetermineNavigationIndicies()
		{
			for (int i = 0; i < base.ChildCount; i++)
			{
				MapNotificationItemWidget mapNotificationItemWidget = base.GetChild(i) as MapNotificationItemWidget;
				if (i < this.MaxAmountOfNotificationsToShow)
				{
					mapNotificationItemWidget.NotificationRingWidget.GamepadNavigationIndex = base.ChildCount - 1 - i;
				}
				else
				{
					mapNotificationItemWidget.NotificationRingWidget.GamepadNavigationIndex = -1;
				}
			}
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x00027A0B File Offset: 0x00025C0B
		protected override void OnChildAdded(Widget child)
		{
			base.OnChildAdded(child);
			this._newChildren.Add(child);
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x00027A20 File Offset: 0x00025C20
		protected override void OnAfterChildRemoved(Widget child)
		{
			base.OnAfterChildRemoved(child);
			if (this._newChildren.Contains(child))
			{
				this._newChildren.Remove(child);
			}
			this.DetermineChildrenVisibility();
			this.DetermineMoreTextStatus();
			this.DetermineNavigationIndicies();
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x00027A58 File Offset: 0x00025C58
		private void DetermineChildrenVisibility()
		{
			for (int i = 0; i < base.ChildCount; i++)
			{
				Widget child = base.GetChild(i);
				bool isVisible = child.IsVisible;
				child.IsVisible = (i < this.MaxAmountOfNotificationsToShow);
				if (!isVisible)
				{
					child.PositionYOffset = this.DetermineChildTargetYOffset(child, i);
				}
			}
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x00027AA4 File Offset: 0x00025CA4
		private void DetermineMoreTextStatus()
		{
			this.MoreTextWidgetContainer.IsVisible = (base.ChildCount > this.MaxAmountOfNotificationsToShow);
			if (this.MoreTextWidgetContainer.IsVisible)
			{
				this.MoreTextWidget.Text = "+" + (base.ChildCount - this.MaxAmountOfNotificationsToShow);
				this.MoreTextWidgetContainer.BrushRenderer.RestartAnimation();
				this.MoreTextWidget.BrushRenderer.RestartAnimation();
			}
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x00027B1E File Offset: 0x00025D1E
		private float DetermineChildTargetYOffset(Widget child, int childIndex)
		{
			if (childIndex < this.MaxAmountOfNotificationsToShow)
			{
				return -child.Size.Y * (float)childIndex * base._inverseScaleToUse;
			}
			return -child.Size.Y * (float)this.MaxAmountOfNotificationsToShow * base._inverseScaleToUse;
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06000E51 RID: 3665 RVA: 0x00027B5B File Offset: 0x00025D5B
		// (set) Token: 0x06000E52 RID: 3666 RVA: 0x00027B63 File Offset: 0x00025D63
		[Editor(false)]
		public BrushWidget MoreTextWidgetContainer
		{
			get
			{
				return this._moreTextWidgetContainer;
			}
			set
			{
				if (this._moreTextWidgetContainer != value)
				{
					this._moreTextWidgetContainer = value;
					base.OnPropertyChanged<BrushWidget>(value, "MoreTextWidgetContainer");
				}
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06000E53 RID: 3667 RVA: 0x00027B81 File Offset: 0x00025D81
		// (set) Token: 0x06000E54 RID: 3668 RVA: 0x00027B89 File Offset: 0x00025D89
		[Editor(false)]
		public TextWidget MoreTextWidget
		{
			get
			{
				return this._moreTextWidget;
			}
			set
			{
				if (this._moreTextWidget != value)
				{
					this._moreTextWidget = value;
					base.OnPropertyChanged<TextWidget>(value, "MoreTextWidget");
				}
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06000E55 RID: 3669 RVA: 0x00027BA7 File Offset: 0x00025DA7
		// (set) Token: 0x06000E56 RID: 3670 RVA: 0x00027BAF File Offset: 0x00025DAF
		[Editor(false)]
		public int MaxAmountOfNotificationsToShow
		{
			get
			{
				return this._maxAmountOfNotificationsToShow;
			}
			set
			{
				if (this._maxAmountOfNotificationsToShow != value)
				{
					this._maxAmountOfNotificationsToShow = value;
					base.OnPropertyChanged(value, "MaxAmountOfNotificationsToShow");
				}
			}
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x00027BCD File Offset: 0x00025DCD
		private float LocalLerp(float start, float end, float delta)
		{
			if (MathF.Abs(start - end) > 1E-45f)
			{
				return (end - start) * delta + start;
			}
			return end;
		}

		// Token: 0x04000693 RID: 1683
		private List<Widget> _newChildren;

		// Token: 0x04000694 RID: 1684
		private TextWidget _moreTextWidget;

		// Token: 0x04000695 RID: 1685
		private BrushWidget _moreTextWidgetContainer;

		// Token: 0x04000696 RID: 1686
		private int _maxAmountOfNotificationsToShow = 5;
	}
}
