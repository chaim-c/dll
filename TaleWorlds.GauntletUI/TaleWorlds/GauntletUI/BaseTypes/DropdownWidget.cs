using System;
using System.Collections.Generic;
using System.Numerics;
using TaleWorlds.GauntletUI.GamepadNavigation;
using TaleWorlds.InputSystem;

namespace TaleWorlds.GauntletUI.BaseTypes
{
	// Token: 0x0200005D RID: 93
	public class DropdownWidget : Widget
	{
		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x0001892E File Offset: 0x00016B2E
		private Vector2 ListPanelPositionInsideUsableArea
		{
			get
			{
				return this.ListPanel.GlobalPosition - new Vector2(base.EventManager.LeftUsableAreaStart, base.EventManager.TopUsableAreaStart);
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x0001895B File Offset: 0x00016B5B
		// (set) Token: 0x060005DB RID: 1499 RVA: 0x00018963 File Offset: 0x00016B63
		[Editor(false)]
		public RichTextWidget RichTextWidget { get; set; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060005DC RID: 1500 RVA: 0x0001896C File Offset: 0x00016B6C
		// (set) Token: 0x060005DD RID: 1501 RVA: 0x00018974 File Offset: 0x00016B74
		[Editor(false)]
		public bool DoNotHandleDropdownListPanel { get; set; }

		// Token: 0x060005DE RID: 1502 RVA: 0x00018980 File Offset: 0x00016B80
		public DropdownWidget(UIContext context) : base(context)
		{
			this._clickHandler = new Action<Widget>(this.OnButtonClick);
			this._listSelectionHandler = new Action<Widget>(this.OnSelectionChanged);
			this._listItemRemovedHandler = new Action<Widget, Widget>(this.OnListItemRemoved);
			this._listItemAddedHandler = new Action<Widget, Widget>(this.OnListItemAdded);
			base.UsedNavigationMovements = GamepadNavigationTypes.Horizontal;
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x000189EC File Offset: 0x00016BEC
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			if (!this.DoNotHandleDropdownListPanel)
			{
				this.UpdateListPanelPosition();
			}
			if (this._buttonClicked)
			{
				if (this.ListPanel != null && !this._changedByControllerNavigation)
				{
					if (this._isOpen)
					{
						this.ClosePanel();
					}
					else
					{
						this.OpenPanel();
					}
				}
				this._buttonClicked = false;
			}
			else if (this._closeNextFrame && this._isOpen)
			{
				this.ClosePanel();
				this._closeNextFrame = false;
			}
			else if (base.EventManager.LatestMouseUpWidget != this._button && this._isOpen)
			{
				if (this.ListPanel.IsVisible)
				{
					this._closeNextFrame = true;
				}
			}
			else if (this._isOpen)
			{
				this._openFrameCounter++;
				if (this._openFrameCounter > 5)
				{
					if (Vector2.Distance(this.ListPanelPositionInsideUsableArea, this._listPanelOpenPosition) > 20f && !this.DoNotHandleDropdownListPanel)
					{
						this._closeNextFrame = true;
					}
				}
				else
				{
					this._listPanelOpenPosition = this.ListPanelPositionInsideUsableArea;
				}
			}
			this.RefreshSelectedItem();
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x00018AF5 File Offset: 0x00016CF5
		protected override void OnConnectedToRoot()
		{
			base.OnConnectedToRoot();
			this.ScrollablePanel = this.GetParentScrollablePanelOfWidget(this);
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00018B0A File Offset: 0x00016D0A
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (!this.DoNotHandleDropdownListPanel)
			{
				this.UpdateListPanelPosition();
			}
			this.UpdateGamepadNavigationControls();
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x00018B28 File Offset: 0x00016D28
		private void UpdateGamepadNavigationControls()
		{
			if (this._isOpen && base.EventManager.IsControllerActive && (Input.IsKeyPressed(InputKey.ControllerLBumper) || Input.IsKeyPressed(InputKey.ControllerLTrigger) || Input.IsKeyPressed(InputKey.ControllerRBumper) || Input.IsKeyPressed(InputKey.ControllerRTrigger)))
			{
				this.ClosePanel();
			}
			if (!this._isOpen && (base.IsPressed || this._button.IsPressed) && base.IsRecursivelyVisible() && base.EventManager.GetIsHitThisFrame())
			{
				if (Input.IsKeyReleased(InputKey.ControllerLLeft))
				{
					if (this.CurrentSelectedIndex > 0)
					{
						int currentSelectedIndex = this.CurrentSelectedIndex;
						this.CurrentSelectedIndex = currentSelectedIndex - 1;
					}
					else
					{
						this.CurrentSelectedIndex = this.ListPanel.ChildCount - 1;
					}
					this._isSelectedItemDirty = true;
					this._changedByControllerNavigation = true;
				}
				else if (Input.IsKeyReleased(InputKey.ControllerLRight))
				{
					if (this.CurrentSelectedIndex < this.ListPanel.ChildCount - 1)
					{
						int currentSelectedIndex = this.CurrentSelectedIndex;
						this.CurrentSelectedIndex = currentSelectedIndex + 1;
					}
					else
					{
						this.CurrentSelectedIndex = 0;
					}
					this._isSelectedItemDirty = true;
					this._changedByControllerNavigation = true;
				}
				base.IsUsingNavigation = true;
				return;
			}
			this._changedByControllerNavigation = false;
			base.IsUsingNavigation = false;
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00018C68 File Offset: 0x00016E68
		private void UpdateListPanelPosition()
		{
			this.ListPanel.HorizontalAlignment = HorizontalAlignment.Left;
			this.ListPanel.VerticalAlignment = VerticalAlignment.Top;
			float num = (base.Size.X - this._listPanel.Size.X) * 0.5f;
			this.ListPanel.MarginTop = (base.GlobalPosition.Y + this.Button.Size.Y - base.EventManager.TopUsableAreaStart) * base._inverseScaleToUse;
			this.ListPanel.MarginLeft = (base.GlobalPosition.X + num - base.EventManager.LeftUsableAreaStart) * base._inverseScaleToUse;
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00018D18 File Offset: 0x00016F18
		protected virtual void OpenPanel()
		{
			if (this.Button != null)
			{
				this.Button.IsSelected = true;
			}
			this.ListPanel.IsVisible = true;
			this._listPanelOpenPosition = this.ListPanelPositionInsideUsableArea;
			this._openFrameCounter = 0;
			this._isOpen = true;
			Action<DropdownWidget> onOpenStateChanged = this.OnOpenStateChanged;
			if (onOpenStateChanged != null)
			{
				onOpenStateChanged(this);
			}
			this.CreateGamepadNavigationScopeData();
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x00018D78 File Offset: 0x00016F78
		protected virtual void ClosePanel()
		{
			if (this.Button != null)
			{
				this.Button.IsSelected = false;
			}
			this.ListPanel.IsVisible = false;
			this._buttonClicked = false;
			this._isOpen = false;
			Action<DropdownWidget> onOpenStateChanged = this.OnOpenStateChanged;
			if (onOpenStateChanged != null)
			{
				onOpenStateChanged(this);
			}
			this.ClearGamepadScopeData();
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00018DCC File Offset: 0x00016FCC
		private void CreateGamepadNavigationScopeData()
		{
			if (this._navigationScope != null)
			{
				base.GamepadNavigationContext.RemoveNavigationScope(this._navigationScope);
			}
			this._scopeCollection = new GamepadNavigationForcedScopeCollection();
			this._scopeCollection.ParentWidget = (base.ParentWidget ?? this);
			this._scopeCollection.CollectionOrder = 999;
			this._navigationScope = this.BuildGamepadNavigationScopeData();
			base.GamepadNavigationContext.AddNavigationScope(this._navigationScope, true);
			this._button.GamepadNavigationIndex = 0;
			this._navigationScope.AddWidgetAtIndex(this._button, 0);
			ButtonWidget button = this._button;
			button.OnGamepadNavigationFocusGained = (Action<Widget>)Delegate.Combine(button.OnGamepadNavigationFocusGained, new Action<Widget>(this.OnWidgetGainedNavigationFocus));
			for (int i = 0; i < this.ListPanel.Children.Count; i++)
			{
				this.ListPanel.Children[i].GamepadNavigationIndex = i + 1;
				this._navigationScope.AddWidgetAtIndex(this.ListPanel.Children[i], i + 1);
				Widget widget = this.ListPanel.Children[i];
				widget.OnGamepadNavigationFocusGained = (Action<Widget>)Delegate.Combine(widget.OnGamepadNavigationFocusGained, new Action<Widget>(this.OnWidgetGainedNavigationFocus));
			}
			base.GamepadNavigationContext.AddForcedScopeCollection(this._scopeCollection);
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00018F1F File Offset: 0x0001711F
		private void OnWidgetGainedNavigationFocus(Widget widget)
		{
			ScrollablePanel scrollablePanel = this.ScrollablePanel;
			if (scrollablePanel == null)
			{
				return;
			}
			scrollablePanel.ScrollToChild(widget, -1f, -1f, 0, 0, 0f, 0f);
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00018F48 File Offset: 0x00017148
		private ScrollablePanel GetParentScrollablePanelOfWidget(Widget widget)
		{
			for (Widget widget2 = widget; widget2 != null; widget2 = widget2.ParentWidget)
			{
				ScrollablePanel result;
				if ((result = (widget2 as ScrollablePanel)) != null)
				{
					return result;
				}
			}
			return null;
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00018F70 File Offset: 0x00017170
		private GamepadNavigationScope BuildGamepadNavigationScopeData()
		{
			return new GamepadNavigationScope
			{
				ScopeMovements = GamepadNavigationTypes.Vertical,
				DoNotAutomaticallyFindChildren = true,
				DoNotAutoNavigateAfterSort = true,
				HasCircularMovement = true,
				ParentWidget = (base.ParentWidget ?? this),
				ScopeID = "DropdownScope"
			};
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00018FB0 File Offset: 0x000171B0
		private void ClearGamepadScopeData()
		{
			if (this._navigationScope != null)
			{
				base.GamepadNavigationContext.RemoveNavigationScope(this._navigationScope);
				for (int i = 0; i < this.ListPanel.Children.Count; i++)
				{
					this.ListPanel.Children[i].GamepadNavigationIndex = -1;
					Widget widget = this.ListPanel.Children[i];
					widget.OnGamepadNavigationFocusGained = (Action<Widget>)Delegate.Remove(widget.OnGamepadNavigationFocusGained, new Action<Widget>(this.OnWidgetGainedNavigationFocus));
				}
				this._button.GamepadNavigationIndex = -1;
				ButtonWidget button = this._button;
				button.OnGamepadNavigationFocusGained = (Action<Widget>)Delegate.Remove(button.OnGamepadNavigationFocusGained, new Action<Widget>(this.OnWidgetGainedNavigationFocus));
				this._navigationScope = null;
			}
			if (this._scopeCollection != null)
			{
				base.GamepadNavigationContext.RemoveForcedScopeCollection(this._scopeCollection);
			}
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00019090 File Offset: 0x00017290
		public void OnButtonClick(Widget widget)
		{
			this._buttonClicked = true;
			this._closeNextFrame = false;
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x000190A0 File Offset: 0x000172A0
		public void UpdateButtonText(string text)
		{
			if (this.RichTextWidget != null)
			{
				this.RichTextWidget.Text = ((!string.IsNullOrEmpty(text)) ? text : " ");
			}
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x000190C5 File Offset: 0x000172C5
		public void OnListItemAdded(Widget parentWidget, Widget newChild)
		{
			this._isSelectedItemDirty = true;
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x000190CE File Offset: 0x000172CE
		public void OnListItemRemoved(Widget removedItem, Widget removedChild)
		{
			this._isSelectedItemDirty = true;
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x000190D7 File Offset: 0x000172D7
		public void OnSelectionChanged(Widget widget)
		{
			this.CurrentSelectedIndex = this.ListPanelValue;
			this._isSelectedItemDirty = true;
			base.OnPropertyChanged(this.CurrentSelectedIndex, "CurrentSelectedIndex");
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00019100 File Offset: 0x00017300
		private void RefreshSelectedItem()
		{
			if (this._isSelectedItemDirty)
			{
				this.ListPanelValue = this.CurrentSelectedIndex;
				if (this.ListPanelValue >= 0)
				{
					string text = "";
					ListPanel listPanel = this.ListPanel;
					Widget widget = (listPanel != null) ? listPanel.GetChild(this.ListPanelValue) : null;
					if (widget != null)
					{
						using (IEnumerator<Widget> enumerator = widget.AllChildren.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								RichTextWidget richTextWidget;
								if ((richTextWidget = (enumerator.Current as RichTextWidget)) != null)
								{
									text = richTextWidget.Text;
								}
							}
						}
					}
					this.UpdateButtonText(text);
				}
				if (this.ListPanel != null)
				{
					for (int i = 0; i < this.ListPanel.ChildCount; i++)
					{
						ButtonWidget buttonWidget;
						if ((buttonWidget = (this.ListPanel.GetChild(i) as ButtonWidget)) != null)
						{
							buttonWidget.IsSelected = (this.CurrentSelectedIndex == i);
						}
					}
				}
				this._isSelectedItemDirty = false;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x000191F0 File Offset: 0x000173F0
		// (set) Token: 0x060005F2 RID: 1522 RVA: 0x000191F8 File Offset: 0x000173F8
		[Editor(false)]
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

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x00019216 File Offset: 0x00017416
		// (set) Token: 0x060005F4 RID: 1524 RVA: 0x00019220 File Offset: 0x00017420
		[Editor(false)]
		public ButtonWidget Button
		{
			get
			{
				return this._button;
			}
			set
			{
				ButtonWidget button = this._button;
				if (button != null)
				{
					button.ClickEventHandlers.Remove(this._clickHandler);
				}
				this._button = value;
				ButtonWidget button2 = this._button;
				if (button2 != null)
				{
					button2.ClickEventHandlers.Add(this._clickHandler);
				}
				this._isSelectedItemDirty = true;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x00019274 File Offset: 0x00017474
		// (set) Token: 0x060005F6 RID: 1526 RVA: 0x0001927C File Offset: 0x0001747C
		[Editor(false)]
		public ListPanel ListPanel
		{
			get
			{
				return this._listPanel;
			}
			set
			{
				if (this._listPanel != null)
				{
					this._listPanel.SelectEventHandlers.Remove(this._listSelectionHandler);
					this._listPanel.ItemAddEventHandlers.Remove(this._listItemAddedHandler);
					this._listPanel.ItemRemoveEventHandlers.Remove(this._listItemRemovedHandler);
				}
				this._listPanel = value;
				if (this._listPanel != null)
				{
					if (!this.DoNotHandleDropdownListPanel)
					{
						this._listPanel.ParentWidget = base.EventManager.Root;
						this._listPanel.HorizontalAlignment = HorizontalAlignment.Left;
						this._listPanel.VerticalAlignment = VerticalAlignment.Top;
					}
					this._listPanel.SelectEventHandlers.Add(this._listSelectionHandler);
					this._listPanel.ItemAddEventHandlers.Add(this._listItemAddedHandler);
					this._listPanel.ItemRemoveEventHandlers.Add(this._listItemRemovedHandler);
				}
				this._isSelectedItemDirty = true;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x00019364 File Offset: 0x00017564
		// (set) Token: 0x060005F8 RID: 1528 RVA: 0x0001936C File Offset: 0x0001756C
		public bool IsOpen
		{
			get
			{
				return this._isOpen;
			}
			set
			{
				if (value != this._isOpen && !this._buttonClicked)
				{
					if (this._isOpen)
					{
						this.ClosePanel();
						return;
					}
					this.OpenPanel();
				}
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x00019394 File Offset: 0x00017594
		// (set) Token: 0x060005FA RID: 1530 RVA: 0x000193AB File Offset: 0x000175AB
		[Editor(false)]
		public int ListPanelValue
		{
			get
			{
				if (this.ListPanel != null)
				{
					return this.ListPanel.IntValue;
				}
				return -1;
			}
			set
			{
				if (this.ListPanel != null && this.ListPanel.IntValue != value)
				{
					this.ListPanel.IntValue = value;
				}
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x000193CF File Offset: 0x000175CF
		// (set) Token: 0x060005FC RID: 1532 RVA: 0x000193D7 File Offset: 0x000175D7
		[Editor(false)]
		public int CurrentSelectedIndex
		{
			get
			{
				return this._currentSelectedIndex;
			}
			set
			{
				if (this._currentSelectedIndex != value)
				{
					this._currentSelectedIndex = value;
					this._isSelectedItemDirty = true;
				}
			}
		}

		// Token: 0x040002C2 RID: 706
		public Action<DropdownWidget> OnOpenStateChanged;

		// Token: 0x040002C3 RID: 707
		private readonly Action<Widget> _clickHandler;

		// Token: 0x040002C4 RID: 708
		private readonly Action<Widget> _listSelectionHandler;

		// Token: 0x040002C5 RID: 709
		private readonly Action<Widget, Widget> _listItemRemovedHandler;

		// Token: 0x040002C6 RID: 710
		private readonly Action<Widget, Widget> _listItemAddedHandler;

		// Token: 0x040002C7 RID: 711
		private Vector2 _listPanelOpenPosition;

		// Token: 0x040002C8 RID: 712
		private int _openFrameCounter;

		// Token: 0x040002C9 RID: 713
		private bool _isSelectedItemDirty = true;

		// Token: 0x040002CA RID: 714
		private bool _changedByControllerNavigation;

		// Token: 0x040002CB RID: 715
		private GamepadNavigationScope _navigationScope;

		// Token: 0x040002CC RID: 716
		private GamepadNavigationForcedScopeCollection _scopeCollection;

		// Token: 0x040002CF RID: 719
		private ScrollablePanel _scrollablePanel;

		// Token: 0x040002D0 RID: 720
		private ButtonWidget _button;

		// Token: 0x040002D1 RID: 721
		private ListPanel _listPanel;

		// Token: 0x040002D2 RID: 722
		private int _currentSelectedIndex;

		// Token: 0x040002D3 RID: 723
		private bool _closeNextFrame;

		// Token: 0x040002D4 RID: 724
		private bool _isOpen;

		// Token: 0x040002D5 RID: 725
		private bool _buttonClicked;
	}
}
