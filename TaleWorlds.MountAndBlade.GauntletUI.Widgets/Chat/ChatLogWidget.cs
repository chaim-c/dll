using System;
using System.Collections.Generic;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Chat
{
	// Token: 0x02000170 RID: 368
	public class ChatLogWidget : Widget
	{
		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06001302 RID: 4866 RVA: 0x00033EC2 File Offset: 0x000320C2
		private float _resizeTransitionTime
		{
			get
			{
				return 0.14f;
			}
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x00033EC9 File Offset: 0x000320C9
		public ChatLogWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x00033EE0 File Offset: 0x000320E0
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			if (!this.IsChatDisabled && this.TextInputWidget != null && this.FullyShowChatWithTyping && this._focusOnNextUpdate)
			{
				base.EventManager.SetWidgetFocused(this.TextInputWidget, false);
				this._focusOnNextUpdate = false;
			}
			if (!this.FullyShowChat)
			{
				this.ScrollablePanel.ResetTweenSpeed();
				this.Scrollbar.ValueFloat = this.Scrollbar.MaxValue;
			}
			base.ParentWidget.DoNotPassEventsToChildren = !this.FullyShowChat;
			if (this.ResizerWidget != null && this.ResizeFrameWidget != null)
			{
				this.UpdateResize(dt);
			}
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x00033F84 File Offset: 0x00032184
		private void UpdateResize(float dt)
		{
			if (Input.IsKeyPressed(InputKey.LeftMouseButton) && base.EventManager.HoveredView == this.ResizerWidget)
			{
				this._isResizing = true;
				this._resizeStartMousePosition = Input.MousePositionPixel;
				this._resizeOriginalSize = new Vec2(this.SizeX, this.SizeY);
				this.ResizeFrameWidget.IsVisible = true;
				this.ResizeFrameWidget.WidthSizePolicy = SizePolicy.Fixed;
				this.ResizeFrameWidget.HeightSizePolicy = SizePolicy.Fixed;
				this.ResizeFrameWidget.SuggestedHeight = this.SizeY;
				this.ResizeFrameWidget.SuggestedWidth = this.SizeX;
				this._innerPanelDefaultSizePolicies = new ValueTuple<SizePolicy, SizePolicy>(this.ScrollablePanel.InnerPanel.WidthSizePolicy, this.ScrollablePanel.InnerPanel.HeightSizePolicy);
				this.ScrollablePanel.InnerPanel.WidthSizePolicy = SizePolicy.Fixed;
				this.ScrollablePanel.InnerPanel.HeightSizePolicy = SizePolicy.Fixed;
				this.ScrollablePanel.InnerPanel.SuggestedWidth = this.ScrollablePanel.InnerPanel.Size.X;
				this.ScrollablePanel.InnerPanel.SuggestedHeight = this.ScrollablePanel.InnerPanel.Size.Y;
			}
			else if (Input.IsKeyReleased(InputKey.LeftMouseButton))
			{
				if (this._isResizing)
				{
					this.ResizeFrameWidget.IsVisible = false;
					this._resizeActualPanel = true;
					this._lerpRatio = 0f;
				}
				this._isResizing = false;
			}
			if (this._isResizing)
			{
				Vec2 vec = this._resizeOriginalSize + new Vec2((Input.MousePositionPixel - this._resizeStartMousePosition).X, -(Input.MousePositionPixel - this._resizeStartMousePosition).Y);
				this.ResizeFrameWidget.SuggestedWidth = Mathf.Clamp(vec.X, base.MinWidth, base.MaxWidth);
				this.ResizeFrameWidget.SuggestedHeight = Mathf.Clamp(vec.Y, base.MinHeight, base.MaxHeight) - this.ResizeFrameWidget.MarginBottom;
				return;
			}
			if (this._resizeActualPanel)
			{
				this._lerpRatio = Mathf.Clamp(this._lerpRatio + dt / this._resizeTransitionTime, 0f, 1f);
				this.SizeX = Mathf.Lerp(this._resizeOriginalSize.x, this.ResizeFrameWidget.SuggestedWidth, this._lerpRatio);
				this.SizeY = Mathf.Lerp(this._resizeOriginalSize.y, this.ResizeFrameWidget.SuggestedHeight + this.ResizeFrameWidget.MarginBottom, this._lerpRatio);
				if (this.SizeX.ApproximatelyEqualsTo(this.ResizeFrameWidget.SuggestedWidth, 0.01f) && this.SizeY.ApproximatelyEqualsTo(this.ResizeFrameWidget.SuggestedHeight + this.ResizeFrameWidget.MarginBottom, 0.01f))
				{
					this.SizeX = this.ResizeFrameWidget.SuggestedWidth;
					this.SizeY = this.ResizeFrameWidget.SuggestedHeight + this.ResizeFrameWidget.MarginBottom;
					this.ResizeFrameWidget.WidthSizePolicy = SizePolicy.StretchToParent;
					this.ResizeFrameWidget.HeightSizePolicy = SizePolicy.StretchToParent;
					this.ScrollablePanel.InnerPanel.WidthSizePolicy = this._innerPanelDefaultSizePolicies.Item1;
					this.ScrollablePanel.InnerPanel.HeightSizePolicy = this._innerPanelDefaultSizePolicies.Item2;
					this._resizeActualPanel = false;
					base.EventFired("FinishResize", Array.Empty<object>());
					return;
				}
			}
			else if (!this._isInitialized)
			{
				this.SizeX = base.SuggestedWidth;
				this.SizeY = base.SuggestedHeight;
				this._isInitialized = true;
			}
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x00034327 File Offset: 0x00032527
		public void RegisterMultiLineElement(ChatCollapsableListPanel element)
		{
			if (!this._registeredMultilineWidgets.Contains(element))
			{
				this._registeredMultilineWidgets.Add(element);
			}
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x00034343 File Offset: 0x00032543
		public void RemoveMultiLineElement(ChatCollapsableListPanel element)
		{
			if (this._registeredMultilineWidgets.Contains(element))
			{
				this._registeredMultilineWidgets.Remove(element);
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06001308 RID: 4872 RVA: 0x00034360 File Offset: 0x00032560
		// (set) Token: 0x06001309 RID: 4873 RVA: 0x00034368 File Offset: 0x00032568
		[DataSourceProperty]
		public bool IsChatDisabled
		{
			get
			{
				return this._isChatDisabled;
			}
			set
			{
				if (value != this._isChatDisabled)
				{
					this._isChatDisabled = value;
					base.OnPropertyChanged(value, "IsChatDisabled");
				}
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x0600130A RID: 4874 RVA: 0x00034386 File Offset: 0x00032586
		// (set) Token: 0x0600130B RID: 4875 RVA: 0x0003438E File Offset: 0x0003258E
		[DataSourceProperty]
		public bool FinishedResizing
		{
			get
			{
				return this._finishedResizing;
			}
			set
			{
				if (value != this._finishedResizing)
				{
					this._finishedResizing = value;
					base.OnPropertyChanged(value, "FinishedResizing");
				}
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x0600130C RID: 4876 RVA: 0x000343AC File Offset: 0x000325AC
		// (set) Token: 0x0600130D RID: 4877 RVA: 0x000343B4 File Offset: 0x000325B4
		[DataSourceProperty]
		public bool FullyShowChat
		{
			get
			{
				return this._fullyShowChat;
			}
			set
			{
				if (value != this._fullyShowChat)
				{
					this._fullyShowChat = value;
				}
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x0600130E RID: 4878 RVA: 0x000343C6 File Offset: 0x000325C6
		// (set) Token: 0x0600130F RID: 4879 RVA: 0x000343D0 File Offset: 0x000325D0
		[DataSourceProperty]
		public bool FullyShowChatWithTyping
		{
			get
			{
				return this._fullyShowChatWithTyping;
			}
			set
			{
				if (value != this._fullyShowChatWithTyping)
				{
					this._fullyShowChatWithTyping = value;
					if (!this.IsChatDisabled && this.TextInputWidget != null && this._fullyShowChatWithTyping)
					{
						this._focusOnNextUpdate = true;
					}
					base.EventManager.SetWidgetFocused(null, false);
					base.OnPropertyChanged(value, "FullyShowChatWithTyping");
				}
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06001310 RID: 4880 RVA: 0x00034425 File Offset: 0x00032625
		// (set) Token: 0x06001311 RID: 4881 RVA: 0x0003442D File Offset: 0x0003262D
		[DataSourceProperty]
		public EditableTextWidget TextInputWidget
		{
			get
			{
				return this._textInputWidget;
			}
			set
			{
				if (value != this._textInputWidget)
				{
					this._textInputWidget = value;
					base.OnPropertyChanged<EditableTextWidget>(value, "TextInputWidget");
				}
			}
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06001312 RID: 4882 RVA: 0x0003444B File Offset: 0x0003264B
		// (set) Token: 0x06001313 RID: 4883 RVA: 0x00034453 File Offset: 0x00032653
		[DataSourceProperty]
		public ScrollbarWidget Scrollbar
		{
			get
			{
				return this._scrollbar;
			}
			set
			{
				if (value != this._scrollbar)
				{
					this._scrollbar = value;
					base.OnPropertyChanged<ScrollbarWidget>(value, "Scrollbar");
				}
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06001314 RID: 4884 RVA: 0x00034471 File Offset: 0x00032671
		// (set) Token: 0x06001315 RID: 4885 RVA: 0x00034479 File Offset: 0x00032679
		[DataSourceProperty]
		public ScrollablePanel ScrollablePanel
		{
			get
			{
				return this._scrollablePanel;
			}
			set
			{
				if (value != this._scrollablePanel)
				{
					this._scrollablePanel = value;
					base.OnPropertyChanged<ScrollablePanel>(value, "ScrollablePanel");
				}
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06001316 RID: 4886 RVA: 0x00034497 File Offset: 0x00032697
		// (set) Token: 0x06001317 RID: 4887 RVA: 0x0003449F File Offset: 0x0003269F
		[DataSourceProperty]
		public Widget ResizerWidget
		{
			get
			{
				return this._resizerWidget;
			}
			set
			{
				if (value != this._resizerWidget)
				{
					this._resizerWidget = value;
					base.OnPropertyChanged<Widget>(value, "ResizerWidget");
				}
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06001318 RID: 4888 RVA: 0x000344BD File Offset: 0x000326BD
		// (set) Token: 0x06001319 RID: 4889 RVA: 0x000344C5 File Offset: 0x000326C5
		[DataSourceProperty]
		public Widget ResizeFrameWidget
		{
			get
			{
				return this._resizeFrameWidget;
			}
			set
			{
				if (value != this._resizeFrameWidget)
				{
					this._resizeFrameWidget = value;
					base.OnPropertyChanged<Widget>(value, "ResizeFrameWidget");
				}
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x0600131A RID: 4890 RVA: 0x000344E3 File Offset: 0x000326E3
		// (set) Token: 0x0600131B RID: 4891 RVA: 0x000344EB File Offset: 0x000326EB
		[DataSourceProperty]
		public float SizeX
		{
			get
			{
				return this._sizeX;
			}
			set
			{
				if (value != this._sizeX)
				{
					this._sizeX = value;
					base.SuggestedWidth = value;
					base.OnPropertyChanged(value, "SizeX");
				}
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x0600131C RID: 4892 RVA: 0x00034510 File Offset: 0x00032710
		// (set) Token: 0x0600131D RID: 4893 RVA: 0x00034518 File Offset: 0x00032718
		[DataSourceProperty]
		public float SizeY
		{
			get
			{
				return this._sizeY;
			}
			set
			{
				if (value != this._sizeY)
				{
					this._sizeY = value;
					base.SuggestedHeight = value;
					base.OnPropertyChanged(value, "SizeY");
				}
			}
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x0600131E RID: 4894 RVA: 0x0003453D File Offset: 0x0003273D
		// (set) Token: 0x0600131F RID: 4895 RVA: 0x00034545 File Offset: 0x00032745
		[DataSourceProperty]
		public ListPanel MessageHistoryList
		{
			get
			{
				return this._messageHistoryList;
			}
			set
			{
				if (value != this._messageHistoryList)
				{
					this._messageHistoryList = value;
					base.OnPropertyChanged<ListPanel>(value, "MessageHistoryList");
				}
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06001320 RID: 4896 RVA: 0x00034563 File Offset: 0x00032763
		// (set) Token: 0x06001321 RID: 4897 RVA: 0x0003456B File Offset: 0x0003276B
		[DataSourceProperty]
		public bool IsMPChatLog
		{
			get
			{
				return this._isMPChatLog;
			}
			set
			{
				if (value != this._isMPChatLog)
				{
					this._isMPChatLog = value;
					base.OnPropertyChanged(value, "IsMPChatLog");
				}
			}
		}

		// Token: 0x040008A1 RID: 2209
		private List<ChatCollapsableListPanel> _registeredMultilineWidgets = new List<ChatCollapsableListPanel>();

		// Token: 0x040008A2 RID: 2210
		private bool _isInitialized;

		// Token: 0x040008A3 RID: 2211
		private float _lerpRatio;

		// Token: 0x040008A4 RID: 2212
		private bool _isResizing;

		// Token: 0x040008A5 RID: 2213
		private bool _resizeActualPanel;

		// Token: 0x040008A6 RID: 2214
		private Vec2 _resizeStartMousePosition;

		// Token: 0x040008A7 RID: 2215
		private Vec2 _resizeOriginalSize;

		// Token: 0x040008A8 RID: 2216
		private ValueTuple<SizePolicy, SizePolicy> _innerPanelDefaultSizePolicies;

		// Token: 0x040008A9 RID: 2217
		private bool _focusOnNextUpdate;

		// Token: 0x040008AA RID: 2218
		private bool _isChatDisabled;

		// Token: 0x040008AB RID: 2219
		private bool _isMPChatLog;

		// Token: 0x040008AC RID: 2220
		private bool _finishedResizing;

		// Token: 0x040008AD RID: 2221
		private bool _fullyShowChat;

		// Token: 0x040008AE RID: 2222
		private bool _fullyShowChatWithTyping;

		// Token: 0x040008AF RID: 2223
		private EditableTextWidget _textInputWidget;

		// Token: 0x040008B0 RID: 2224
		private ScrollbarWidget _scrollbar;

		// Token: 0x040008B1 RID: 2225
		private ScrollablePanel _scrollablePanel;

		// Token: 0x040008B2 RID: 2226
		private Widget _resizerWidget;

		// Token: 0x040008B3 RID: 2227
		private Widget _resizeFrameWidget;

		// Token: 0x040008B4 RID: 2228
		private float _sizeX;

		// Token: 0x040008B5 RID: 2229
		private float _sizeY;

		// Token: 0x040008B6 RID: 2230
		private ListPanel _messageHistoryList;
	}
}
