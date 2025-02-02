using System;
using System.Collections.Generic;
using TaleWorlds.GauntletUI.GamepadNavigation;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI.BaseTypes
{
	// Token: 0x02000068 RID: 104
	public class ScrollablePanel : Widget
	{
		// Token: 0x1400000F RID: 15
		// (add) Token: 0x0600069E RID: 1694 RVA: 0x0001CF9C File Offset: 0x0001B19C
		// (remove) Token: 0x0600069F RID: 1695 RVA: 0x0001CFD4 File Offset: 0x0001B1D4
		public event Action<float> OnScroll;

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x0001D009 File Offset: 0x0001B209
		// (set) Token: 0x060006A1 RID: 1697 RVA: 0x0001D011 File Offset: 0x0001B211
		public Widget ClipRect { get; set; }

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x0001D01A File Offset: 0x0001B21A
		// (set) Token: 0x060006A3 RID: 1699 RVA: 0x0001D022 File Offset: 0x0001B222
		public Widget InnerPanel
		{
			get
			{
				return this._innerPanel;
			}
			set
			{
				if (value != this._innerPanel)
				{
					this._innerPanel = value;
					this.OnInnerPanelValueChanged();
				}
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x0001D03A File Offset: 0x0001B23A
		public ScrollbarWidget ActiveScrollbar
		{
			get
			{
				return this.VerticalScrollbar ?? this.HorizontalScrollbar;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x0001D04C File Offset: 0x0001B24C
		// (set) Token: 0x060006A6 RID: 1702 RVA: 0x0001D054 File Offset: 0x0001B254
		public bool UpdateScrollbarVisibility { get; set; } = true;

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x0001D05D File Offset: 0x0001B25D
		// (set) Token: 0x060006A8 RID: 1704 RVA: 0x0001D065 File Offset: 0x0001B265
		public Widget FixedHeader { get; set; }

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x0001D06E File Offset: 0x0001B26E
		// (set) Token: 0x060006AA RID: 1706 RVA: 0x0001D076 File Offset: 0x0001B276
		public Widget ScrolledHeader { get; set; }

		// Token: 0x060006AB RID: 1707 RVA: 0x0001D080 File Offset: 0x0001B280
		public ScrollablePanel(UIContext context) : base(context)
		{
			this._verticalScrollbarInterpolationController = new ScrollablePanel.ScrollbarInterpolationController();
			this._horizontalScrollbarInterpolationController = new ScrollablePanel.ScrollbarInterpolationController();
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0001D0D9 File Offset: 0x0001B2D9
		public void ResetTweenSpeed()
		{
			this._verticalScrollVelocity = 0f;
			this._horizontalScrollVelocity = 0f;
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0001D0F1 File Offset: 0x0001B2F1
		protected override bool OnPreviewMouseScroll()
		{
			return !this.OnlyAcceptScrollEventIfCanScroll || this._canScrollHorizontal || this._canScrollVertical;
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0001D10B File Offset: 0x0001B30B
		protected override bool OnPreviewRightStickMovement()
		{
			return (!this.OnlyAcceptScrollEventIfCanScroll || this._canScrollHorizontal || this._canScrollVertical) && !GauntletGamepadNavigationManager.Instance.IsCursorMovingForNavigation && !GauntletGamepadNavigationManager.Instance.AnyWidgetUsingNavigation;
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0001D140 File Offset: 0x0001B340
		protected internal override void OnMouseScroll()
		{
			float num = base.EventManager.DeltaMouseScroll * this.MouseScrollSpeed;
			if ((Input.IsKeyDown(InputKey.LeftShift) || Input.IsKeyDown(InputKey.RightShift) || this.VerticalScrollbar == null) && this.HorizontalScrollbar != null)
			{
				this._horizontalScrollVelocity += num;
			}
			else if (this.VerticalScrollbar != null)
			{
				this._verticalScrollVelocity += num;
			}
			this.StopAllInterpolations();
			Action<float> onScroll = this.OnScroll;
			if (onScroll == null)
			{
				return;
			}
			onScroll(num);
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x0001D1C0 File Offset: 0x0001B3C0
		protected internal override void OnRightStickMovement()
		{
			float num = -base.EventManager.RightStickHorizontalScrollAmount * this.ControllerScrollSpeed;
			float num2 = base.EventManager.RightStickVerticalScrollAmount * this.ControllerScrollSpeed;
			this._horizontalScrollVelocity += num;
			this._verticalScrollVelocity += num2;
			this.StopAllInterpolations();
			Action<float> onScroll = this.OnScroll;
			if (onScroll == null)
			{
				return;
			}
			onScroll(Mathf.Max(num, num2));
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x0001D22D File Offset: 0x0001B42D
		private void StopAllInterpolations()
		{
			this._verticalScrollbarInterpolationController.StopInterpolation();
			this._horizontalScrollbarInterpolationController.StopInterpolation();
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x0001D245 File Offset: 0x0001B445
		private void OnInnerPanelChildAddedEventFire(Widget widget, string eventName, object[] eventArgs)
		{
			if ((eventName == "ItemAdd" || eventName == "AfterItemRemove") && eventArgs.Length != 0 && eventArgs[0] is ScrollablePanelFixedHeaderWidget)
			{
				this.RefreshFixedHeaders();
				this.StopAllInterpolations();
			}
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x0001D27B File Offset: 0x0001B47B
		private void OnInnerPanelValueChanged()
		{
			if (this.InnerPanel != null)
			{
				this.InnerPanel.EventFire += this.OnInnerPanelChildAddedEventFire;
				this.RefreshFixedHeaders();
				this.StopAllInterpolations();
			}
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x0001D2A8 File Offset: 0x0001B4A8
		private void OnFixedHeaderPropertyChangedEventFire(Widget widget, string eventName, object[] eventArgs)
		{
			if (eventName == "FixedHeaderPropertyChanged")
			{
				this.RefreshFixedHeaders();
				this.StopAllInterpolations();
			}
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0001D2C4 File Offset: 0x0001B4C4
		private void RefreshFixedHeaders()
		{
			foreach (ScrollablePanelFixedHeaderWidget scrollablePanelFixedHeaderWidget in this._fixedHeaders)
			{
				scrollablePanelFixedHeaderWidget.EventFire -= this.OnFixedHeaderPropertyChangedEventFire;
			}
			this._fixedHeaders.Clear();
			float num = 0f;
			for (int i = 0; i < this.InnerPanel.ChildCount; i++)
			{
				ScrollablePanelFixedHeaderWidget scrollablePanelFixedHeaderWidget2;
				if ((scrollablePanelFixedHeaderWidget2 = (this.InnerPanel.GetChild(i) as ScrollablePanelFixedHeaderWidget)) != null && scrollablePanelFixedHeaderWidget2.IsRelevant)
				{
					num += scrollablePanelFixedHeaderWidget2.AdditionalTopOffset;
					scrollablePanelFixedHeaderWidget2.TopOffset = num;
					num += scrollablePanelFixedHeaderWidget2.SuggestedHeight;
					this._fixedHeaders.Add(scrollablePanelFixedHeaderWidget2);
					scrollablePanelFixedHeaderWidget2.EventFire += this.OnFixedHeaderPropertyChangedEventFire;
				}
			}
			float num2 = 0f;
			for (int j = this._fixedHeaders.Count - 1; j >= 0; j--)
			{
				num2 += this._fixedHeaders[j].AdditionalBottomOffset;
				this._fixedHeaders[j].BottomOffset = num2;
				num2 += this._fixedHeaders[j].SuggestedHeight;
			}
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0001D404 File Offset: 0x0001B604
		private void AdjustVerticalScrollBar()
		{
			if (this.VerticalScrollbar != null)
			{
				if (this.InnerPanel.VerticalAlignment == VerticalAlignment.Bottom)
				{
					this.VerticalScrollbar.ValueFloat = this.VerticalScrollbar.MaxValue - this.InnerPanel.ScaledPositionOffset.Y;
					return;
				}
				this.VerticalScrollbar.ValueFloat = -this.InnerPanel.ScaledPositionOffset.Y;
			}
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x0001D46B File Offset: 0x0001B66B
		private void AdjustHorizontalScrollBar()
		{
			if (this.HorizontalScrollbar != null)
			{
				this.HorizontalScrollbar.ValueFloat = -this.InnerPanel.ScaledPositionOffset.X;
			}
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0001D491 File Offset: 0x0001B691
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			this.UpdateScrollInterpolation(dt);
			this.UpdateScrollablePanel(dt);
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0001D4A8 File Offset: 0x0001B6A8
		protected void SetActiveCursor(UIContext.MouseCursors cursor)
		{
			base.Context.ActiveCursorOfContext = cursor;
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0001D4B6 File Offset: 0x0001B6B6
		private void UpdateScrollInterpolation(float dt)
		{
			this._verticalScrollbarInterpolationController.Tick(dt);
			this._horizontalScrollbarInterpolationController.Tick(dt);
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0001D4D0 File Offset: 0x0001B6D0
		private void UpdateScrollablePanel(float dt)
		{
			if (this.InnerPanel != null && this.ClipRect != null)
			{
				this._canScrollHorizontal = false;
				this._canScrollVertical = false;
				if (this.HorizontalScrollbar != null)
				{
					bool flag = base.IsVisible;
					bool flag2 = base.IsVisible;
					float num = this.InnerPanel.ScaledPositionXOffset - this.InnerPanel.Left;
					float valueFloat = this.HorizontalScrollbar.ValueFloat;
					this.InnerPanel.ScaledPositionXOffset = -valueFloat;
					this._scrollOffset = this.InnerPanel.ScaledPositionOffset.X;
					this.HorizontalScrollbar.ReverseDirection = false;
					this.HorizontalScrollbar.MinValue = 0f;
					if (this.FixedHeader != null && this.ScrolledHeader != null)
					{
						if (this.FixedHeader.GlobalPosition.Y > this.ScrolledHeader.GlobalPosition.Y)
						{
							this.FixedHeader.IsVisible = true;
						}
						else
						{
							this.FixedHeader.IsVisible = false;
						}
					}
					if (this.InnerPanel.Size.X > this.ClipRect.Size.X)
					{
						this._canScrollHorizontal = true;
						this.HorizontalScrollbar.MaxValue = MathF.Max(1f, this.InnerPanel.Size.X - this.ClipRect.Size.X);
						if (this.AutoAdjustScrollbarHandleSize && this.HorizontalScrollbar.Handle != null)
						{
							this.HorizontalScrollbar.Handle.ScaledSuggestedWidth = this.HorizontalScrollbar.Size.X * (this.ClipRect.Size.X / this.InnerPanel.Size.X);
						}
						if (MathF.Abs(this._horizontalScrollVelocity) > 0.001f)
						{
							this._scrollOffset += this._horizontalScrollVelocity * (dt / 0.016f) * (Input.Resolution.X / 1920f);
							this._horizontalScrollVelocity = MathF.Lerp(this._horizontalScrollVelocity, 0f, 1f - MathF.Pow(0.001f, dt), 1E-05f);
						}
						else
						{
							this._horizontalScrollVelocity = 0f;
						}
						this.InnerPanel.ScaledPositionXOffset = this._scrollOffset;
						this.AdjustHorizontalScrollBar();
						if (this.InnerPanel.HorizontalAlignment == HorizontalAlignment.Center)
						{
							this.InnerPanel.ScaledPositionXOffset += num;
						}
					}
					else
					{
						this.HorizontalScrollbar.Handle.ScaledSuggestedWidth = this.HorizontalScrollbar.Size.X;
						this.InnerPanel.ScaledPositionXOffset = 0f;
						this.HorizontalScrollbar.ValueFloat = 0f;
						this._horizontalScrollVelocity = 0f;
						this._scrollOffset = 0f;
						if (this.AutoHideScrollBars)
						{
							flag = false;
						}
						if (this.AutoHideScrollBarHandle)
						{
							flag2 = false;
						}
					}
					if (this.UpdateScrollbarVisibility)
					{
						this.HorizontalScrollbar.IsVisible = flag;
						this.HorizontalScrollbar.Handle.IsVisible = (flag2 && flag);
					}
				}
				if (this.VerticalScrollbar != null)
				{
					float valueFloat2 = this.VerticalScrollbar.ValueFloat;
					bool flag3 = base.IsVisible;
					bool flag4 = base.IsVisible;
					this.InnerPanel.ScaledPositionYOffset = -valueFloat2;
					this._scrollOffset = this.InnerPanel.ScaledPositionOffset.Y;
					this.VerticalScrollbar.ReverseDirection = false;
					this.VerticalScrollbar.MinValue = 0f;
					if (this.FixedHeader != null && this.ScrolledHeader != null)
					{
						if (this.FixedHeader.GlobalPosition.Y >= this.ScrolledHeader.GlobalPosition.Y)
						{
							this.FixedHeader.IsVisible = true;
						}
						else
						{
							this.FixedHeader.IsVisible = false;
						}
					}
					if (this.InnerPanel.Size.Y > this.ClipRect.Size.Y)
					{
						this._canScrollVertical = true;
						this.VerticalScrollbar.MaxValue = MathF.Max(1f, this.InnerPanel.Size.Y - this.ClipRect.Size.Y);
						if (this.InnerPanel.VerticalAlignment == VerticalAlignment.Bottom)
						{
							this._scrollOffset = this.VerticalScrollbar.MaxValue - valueFloat2;
						}
						if (this.AutoAdjustScrollbarHandleSize && this.VerticalScrollbar.Handle != null)
						{
							this.VerticalScrollbar.Handle.ScaledSuggestedHeight = this.VerticalScrollbar.Size.Y * (this.ClipRect.Size.Y / this.InnerPanel.Size.Y);
						}
						if (MathF.Abs(this._verticalScrollVelocity) > 0.001f)
						{
							this._scrollOffset += this._verticalScrollVelocity * (dt / 0.016f) * (Input.Resolution.Y / 1080f);
							this._verticalScrollVelocity = MathF.Lerp(this._verticalScrollVelocity, 0f, 1f - MathF.Pow(0.001f, dt), 1E-05f);
						}
						else
						{
							this._verticalScrollVelocity = 0f;
						}
						this.InnerPanel.ScaledPositionYOffset = this._scrollOffset;
						this.AdjustVerticalScrollBar();
					}
					else
					{
						if (this.AutoAdjustScrollbarHandleSize && this.VerticalScrollbar.Handle != null)
						{
							this.VerticalScrollbar.Handle.ScaledSuggestedHeight = this.VerticalScrollbar.Size.Y;
						}
						this.InnerPanel.ScaledPositionYOffset = 0f;
						this.VerticalScrollbar.ValueFloat = 0f;
						this._verticalScrollVelocity = 0f;
						this._scrollOffset = 0f;
						if (this.AutoHideScrollBars)
						{
							flag3 = false;
						}
						if (this.AutoHideScrollBarHandle)
						{
							flag4 = false;
						}
					}
					foreach (ScrollablePanelFixedHeaderWidget scrollablePanelFixedHeaderWidget in this._fixedHeaders)
					{
						if (scrollablePanelFixedHeaderWidget != null && scrollablePanelFixedHeaderWidget.FixedHeader != null && base.MeasuredSize != Vec2.Zero)
						{
							scrollablePanelFixedHeaderWidget.FixedHeader.ScaledPositionYOffset = MathF.Clamp(scrollablePanelFixedHeaderWidget.LocalPosition.Y + this._scrollOffset, scrollablePanelFixedHeaderWidget.TopOffset * base._scaleToUse, base.MeasuredSize.Y - scrollablePanelFixedHeaderWidget.BottomOffset * base._scaleToUse);
						}
					}
					if (this.UpdateScrollbarVisibility)
					{
						this.VerticalScrollbar.IsVisible = flag3;
						this.VerticalScrollbar.Handle.IsVisible = (flag4 && flag3);
					}
				}
			}
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0001DB50 File Offset: 0x0001BD50
		protected float GetScrollYValueForWidget(Widget widget, float widgetTargetYValue, float offset)
		{
			float amount = MBMath.ClampFloat(widgetTargetYValue, 0f, 1f);
			float value = Mathf.Lerp(widget.GlobalPosition.Y + offset, widget.GlobalPosition.Y - this.ClipRect.Size.Y + widget.Size.Y + offset, amount);
			float num = this.InverseLerp(this.InnerPanel.GlobalPosition.Y, this.InnerPanel.GlobalPosition.Y + this.InnerPanel.Size.Y - this.ClipRect.Size.Y, value);
			num = MathF.Clamp(num, 0f, 1f);
			return MathF.Lerp(this.VerticalScrollbar.MinValue, this.VerticalScrollbar.MaxValue, num, 1E-05f);
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0001DC28 File Offset: 0x0001BE28
		protected float GetScrollXValueForWidget(Widget widget, float widgetTargetXValue, float offset)
		{
			float amount = MBMath.ClampFloat(widgetTargetXValue, 0f, 1f);
			float value = Mathf.Lerp(widget.GlobalPosition.X + offset, widget.GlobalPosition.X - this.ClipRect.Size.X + widget.Size.X + offset, amount);
			float num = this.InverseLerp(this.InnerPanel.GlobalPosition.X, this.InnerPanel.GlobalPosition.X + this.InnerPanel.Size.X - this.ClipRect.Size.X, value);
			num = MathF.Clamp(num, 0f, 1f);
			return MathF.Lerp(this.HorizontalScrollbar.MinValue, this.HorizontalScrollbar.MaxValue, num, 1E-05f);
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x0001DD00 File Offset: 0x0001BF00
		private float InverseLerp(float fromValue, float toValue, float value)
		{
			if (fromValue == toValue)
			{
				return 0f;
			}
			return (value - fromValue) / (toValue - fromValue);
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0001DD14 File Offset: 0x0001BF14
		public void ScrollToChild(Widget targetWidget, float horizontalTargetValue = -1f, float verticalTargetValue = -1f, int horizontalOffsetInPixels = 0, int verticalOffsetInPixels = 0, float verticalInterpolationTime = 0f, float horizontalInterpolationTime = 0f)
		{
			if (this.ClipRect != null && this.InnerPanel != null && base.CheckIsMyChildRecursive(targetWidget))
			{
				if (this.VerticalScrollbar != null)
				{
					bool flag = targetWidget.GlobalPosition.Y - (float)verticalOffsetInPixels < this.ClipRect.GlobalPosition.Y;
					bool flag2 = targetWidget.GlobalPosition.Y + targetWidget.Size.Y + (float)verticalOffsetInPixels > this.ClipRect.GlobalPosition.Y + this.ClipRect.Size.Y;
					if (flag || flag2)
					{
						if (verticalTargetValue == -1f)
						{
							verticalTargetValue = (flag ? 0f : 1f);
						}
						float scrollYValueForWidget = this.GetScrollYValueForWidget(targetWidget, verticalTargetValue, (float)(flag ? (-(float)verticalOffsetInPixels) : verticalOffsetInPixels));
						if (verticalInterpolationTime <= 1E-45f)
						{
							this.VerticalScrollbar.ValueFloat = scrollYValueForWidget;
						}
						else
						{
							this._verticalScrollbarInterpolationController.StartInterpolation(scrollYValueForWidget, verticalInterpolationTime);
						}
					}
				}
				if (this.HorizontalScrollbar != null)
				{
					bool flag3 = targetWidget.GlobalPosition.X - (float)horizontalOffsetInPixels < this.ClipRect.GlobalPosition.X;
					bool flag4 = targetWidget.GlobalPosition.X + targetWidget.Size.X + (float)horizontalOffsetInPixels > this.ClipRect.GlobalPosition.X + this.ClipRect.Size.X;
					if (flag3 || flag4)
					{
						if (horizontalTargetValue == -1f)
						{
							horizontalTargetValue = (flag3 ? 0f : 1f);
						}
						float scrollXValueForWidget = this.GetScrollXValueForWidget(targetWidget, horizontalTargetValue, (float)(flag3 ? (-(float)horizontalOffsetInPixels) : horizontalOffsetInPixels));
						if (horizontalInterpolationTime <= 1E-45f)
						{
							this.HorizontalScrollbar.ValueFloat = scrollXValueForWidget;
							return;
						}
						this._horizontalScrollbarInterpolationController.StartInterpolation(scrollXValueForWidget, horizontalInterpolationTime);
					}
				}
			}
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x0001DECD File Offset: 0x0001C0CD
		public void SetVerticalScrollTarget(float targetValue, float interpolationDuration)
		{
			this._verticalScrollbarInterpolationController.StartInterpolation(targetValue, interpolationDuration);
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x0001DEDC File Offset: 0x0001C0DC
		public void SetHorizontalScrollTarget(float targetValue, float interpolationDuration)
		{
			this._horizontalScrollbarInterpolationController.StartInterpolation(targetValue, interpolationDuration);
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060006C2 RID: 1730 RVA: 0x0001DEEB File Offset: 0x0001C0EB
		// (set) Token: 0x060006C3 RID: 1731 RVA: 0x0001DEF3 File Offset: 0x0001C0F3
		[Editor(false)]
		public bool AutoHideScrollBars
		{
			get
			{
				return this._autoHideScrollBars;
			}
			set
			{
				if (this._autoHideScrollBars != value)
				{
					this._autoHideScrollBars = value;
					base.OnPropertyChanged(value, "AutoHideScrollBars");
				}
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x0001DF11 File Offset: 0x0001C111
		// (set) Token: 0x060006C5 RID: 1733 RVA: 0x0001DF19 File Offset: 0x0001C119
		[Editor(false)]
		public bool AutoHideScrollBarHandle
		{
			get
			{
				return this._autoHideScrollBarHandle;
			}
			set
			{
				if (this._autoHideScrollBarHandle != value)
				{
					this._autoHideScrollBarHandle = value;
					base.OnPropertyChanged(value, "AutoHideScrollBarHandle");
				}
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x0001DF37 File Offset: 0x0001C137
		// (set) Token: 0x060006C7 RID: 1735 RVA: 0x0001DF3F File Offset: 0x0001C13F
		[Editor(false)]
		public bool AutoAdjustScrollbarHandleSize
		{
			get
			{
				return this._autoAdjustScrollbarHandleSize;
			}
			set
			{
				if (this._autoAdjustScrollbarHandleSize != value)
				{
					this._autoAdjustScrollbarHandleSize = value;
					base.OnPropertyChanged(value, "AutoAdjustScrollbarHandleSize");
				}
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x0001DF5D File Offset: 0x0001C15D
		// (set) Token: 0x060006C9 RID: 1737 RVA: 0x0001DF65 File Offset: 0x0001C165
		[Editor(false)]
		public bool OnlyAcceptScrollEventIfCanScroll
		{
			get
			{
				return this._onlyAcceptScrollEventIfCanScroll;
			}
			set
			{
				if (this._onlyAcceptScrollEventIfCanScroll != value)
				{
					this._onlyAcceptScrollEventIfCanScroll = value;
					base.OnPropertyChanged(value, "OnlyAcceptScrollEventIfCanScroll");
				}
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x0001DF83 File Offset: 0x0001C183
		// (set) Token: 0x060006CB RID: 1739 RVA: 0x0001DF8B File Offset: 0x0001C18B
		public ScrollbarWidget HorizontalScrollbar
		{
			get
			{
				return this._horizontalScrollbar;
			}
			set
			{
				if (value != this._horizontalScrollbar)
				{
					this._horizontalScrollbar = value;
					this._horizontalScrollbarInterpolationController.SetControlledScrollbar(value);
					base.OnPropertyChanged<ScrollbarWidget>(value, "HorizontalScrollbar");
				}
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x0001DFB5 File Offset: 0x0001C1B5
		// (set) Token: 0x060006CD RID: 1741 RVA: 0x0001DFBD File Offset: 0x0001C1BD
		public ScrollbarWidget VerticalScrollbar
		{
			get
			{
				return this._verticalScrollbar;
			}
			set
			{
				if (value != this._verticalScrollbar)
				{
					this._verticalScrollbar = value;
					this._verticalScrollbarInterpolationController.SetControlledScrollbar(value);
					base.OnPropertyChanged<ScrollbarWidget>(value, "VerticalScrollbar");
				}
			}
		}

		// Token: 0x04000319 RID: 793
		private Widget _innerPanel;

		// Token: 0x0400031D RID: 797
		protected bool _canScrollHorizontal;

		// Token: 0x0400031E RID: 798
		protected bool _canScrollVertical;

		// Token: 0x0400031F RID: 799
		public float ControllerScrollSpeed = 0.2f;

		// Token: 0x04000320 RID: 800
		public float MouseScrollSpeed = 0.2f;

		// Token: 0x04000321 RID: 801
		public AlignmentAxis MouseScrollAxis;

		// Token: 0x04000322 RID: 802
		private float _verticalScrollVelocity;

		// Token: 0x04000323 RID: 803
		private float _horizontalScrollVelocity;

		// Token: 0x04000324 RID: 804
		private ScrollablePanel.ScrollbarInterpolationController _verticalScrollbarInterpolationController;

		// Token: 0x04000325 RID: 805
		private float _scrollOffset;

		// Token: 0x04000326 RID: 806
		private ScrollablePanel.ScrollbarInterpolationController _horizontalScrollbarInterpolationController;

		// Token: 0x04000327 RID: 807
		private List<ScrollablePanelFixedHeaderWidget> _fixedHeaders = new List<ScrollablePanelFixedHeaderWidget>();

		// Token: 0x04000328 RID: 808
		private ScrollbarWidget _horizontalScrollbar;

		// Token: 0x04000329 RID: 809
		private ScrollbarWidget _verticalScrollbar;

		// Token: 0x0400032A RID: 810
		private bool _autoHideScrollBars;

		// Token: 0x0400032B RID: 811
		private bool _autoHideScrollBarHandle;

		// Token: 0x0400032C RID: 812
		private bool _autoAdjustScrollbarHandleSize = true;

		// Token: 0x0400032D RID: 813
		private bool _onlyAcceptScrollEventIfCanScroll;

		// Token: 0x02000095 RID: 149
		private class ScrollbarInterpolationController
		{
			// Token: 0x06000921 RID: 2337 RVA: 0x00023F28 File Offset: 0x00022128
			public void SetControlledScrollbar(ScrollbarWidget scrollbar)
			{
				this._scrollbar = scrollbar;
			}

			// Token: 0x06000922 RID: 2338 RVA: 0x00023F31 File Offset: 0x00022131
			public void StartInterpolation(float targetValue, float duration)
			{
				this._targetValue = targetValue;
				this._duration = duration;
				this._timer = 0f;
				this._isInterpolating = true;
			}

			// Token: 0x06000923 RID: 2339 RVA: 0x00023F53 File Offset: 0x00022153
			public void StopInterpolation()
			{
				this._isInterpolating = false;
				this._targetValue = 0f;
				this._duration = 0f;
				this._timer = 0f;
				this._isInterpolating = false;
			}

			// Token: 0x06000924 RID: 2340 RVA: 0x00023F84 File Offset: 0x00022184
			public void Tick(float dt)
			{
				if (this._isInterpolating && this._scrollbar != null)
				{
					if (this._duration == 0f || this._timer > this._duration)
					{
						this._scrollbar.ValueFloat = this._targetValue;
						this.StopInterpolation();
						return;
					}
					float amount = MathF.Clamp(this._timer / this._duration, 0f, 1f);
					this._scrollbar.ValueFloat = MathF.Lerp(this._scrollbar.ValueFloat, this._targetValue, amount, 1E-05f);
					this._timer += dt;
				}
			}

			// Token: 0x04000490 RID: 1168
			private ScrollbarWidget _scrollbar;

			// Token: 0x04000491 RID: 1169
			private float _targetValue;

			// Token: 0x04000492 RID: 1170
			private float _duration;

			// Token: 0x04000493 RID: 1171
			private bool _isInterpolating;

			// Token: 0x04000494 RID: 1172
			private float _timer;
		}
	}
}
