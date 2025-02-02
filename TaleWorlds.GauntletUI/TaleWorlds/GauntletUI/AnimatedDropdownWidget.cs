using System;
using System.Numerics;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.GamepadNavigation;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;

namespace TaleWorlds.GauntletUI
{
	// Token: 0x02000009 RID: 9
	public class AnimatedDropdownWidget : Widget
	{
		// Token: 0x06000045 RID: 69 RVA: 0x000022F8 File Offset: 0x000004F8
		public AnimatedDropdownWidget(UIContext context) : base(context)
		{
			this._clickHandler = new Action<Widget>(this.OnButtonClick);
			this._listSelectionHandler = new Action<Widget>(this.OnSelectionChanged);
			this._listItemRemovedHandler = new Action<Widget, Widget>(this.OnListChanged);
			this._listItemAddedHandler = new Action<Widget, Widget>(this.OnListChanged);
			base.UsedNavigationMovements = GamepadNavigationTypes.Horizontal;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000046 RID: 70 RVA: 0x0000236E File Offset: 0x0000056E
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00002376 File Offset: 0x00000576
		[Editor(false)]
		public RichTextWidget TextWidget { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000048 RID: 72 RVA: 0x0000237F File Offset: 0x0000057F
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00002387 File Offset: 0x00000587
		public ScrollbarWidget ScrollbarWidget { get; set; }

		// Token: 0x0600004A RID: 74 RVA: 0x00002390 File Offset: 0x00000590
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			if (!this._initialized)
			{
				this.DropdownClipWidget.ParentWidget = this.FindRelativeRoot(this);
				this._initialized = true;
			}
			if (this._buttonClicked)
			{
				this._buttonClicked = false;
			}
			else if (!this.IsLatestUpOrDown(this._button, false) && !this.IsLatestUpOrDown(this.ScrollbarWidget, true) && this._isOpen && this.DropdownClipWidget.IsVisible)
			{
				this.ClosePanel();
			}
			if (this._isOpen && !base.IsRecursivelyVisible())
			{
				this.ClosePanelInOneFrame();
			}
			this.RefreshSelectedItem();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000242C File Offset: 0x0000062C
		private bool IsLatestUpOrDown(Widget widget, bool includeChildren)
		{
			if (widget == null)
			{
				return false;
			}
			if (includeChildren)
			{
				return widget.CheckIsMyChildRecursive(base.EventManager.LatestMouseUpWidget) || widget.CheckIsMyChildRecursive(base.EventManager.LatestMouseDownWidget);
			}
			return widget == base.EventManager.LatestMouseUpWidget || widget == base.EventManager.LatestMouseDownWidget;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002486 File Offset: 0x00000686
		protected override void OnDisconnectedFromRoot()
		{
			base.OnDisconnectedFromRoot();
			this.ClosePanelInOneFrame();
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002494 File Offset: 0x00000694
		private Widget FindRelativeRoot(Widget widget)
		{
			if (widget.ParentWidget == base.EventManager.Root)
			{
				return widget;
			}
			return this.FindRelativeRoot(widget.ParentWidget);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000024B8 File Offset: 0x000006B8
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this._previousOpenState && this._isOpen && Vector2.Distance(this.DropdownClipWidget.GlobalPosition, this._dropdownOpenPosition) > 5f)
			{
				this.ClosePanelInOneFrame();
			}
			this.UpdateListPanelPosition(dt);
			if (!base.IsRecursivelyVisible())
			{
				this.ClosePanelInOneFrame();
			}
			if (!this._isOpen && (base.IsPressed || this._button.IsPressed) && base.IsRecursivelyVisible() && base.EventManager.GetIsHitThisFrame())
			{
				if (Input.IsKeyReleased(InputKey.ControllerLLeft))
				{
					base.Context.TwoDimensionContext.PlaySound("checkbox");
					if (this.CurrentSelectedIndex > 0)
					{
						int currentSelectedIndex = this.CurrentSelectedIndex;
						this.CurrentSelectedIndex = currentSelectedIndex - 1;
					}
					else
					{
						this.CurrentSelectedIndex = this.ListPanel.ChildCount - 1;
					}
					this.RefreshSelectedItem();
					this._changedByControllerNavigation = true;
				}
				else if (Input.IsKeyReleased(InputKey.ControllerLRight))
				{
					base.Context.TwoDimensionContext.PlaySound("checkbox");
					if (this.CurrentSelectedIndex < this.ListPanel.ChildCount - 1)
					{
						int currentSelectedIndex = this.CurrentSelectedIndex;
						this.CurrentSelectedIndex = currentSelectedIndex + 1;
					}
					else
					{
						this.CurrentSelectedIndex = 0;
					}
					this.RefreshSelectedItem();
					this._changedByControllerNavigation = true;
				}
				base.IsUsingNavigation = true;
			}
			else
			{
				this._changedByControllerNavigation = false;
				base.IsUsingNavigation = false;
			}
			if (!this._previousOpenState && this._isOpen)
			{
				this._dropdownOpenPosition = this.Button.GlobalPosition + new Vector2((this.Button.Size.X - this.DropdownClipWidget.Size.X) / 2f, this.Button.Size.Y);
			}
			this._previousOpenState = this._isOpen;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002694 File Offset: 0x00000894
		private void UpdateListPanelPosition(float dt)
		{
			this.DropdownClipWidget.HorizontalAlignment = HorizontalAlignment.Left;
			this.DropdownClipWidget.VerticalAlignment = VerticalAlignment.Top;
			Vector2 vector = Vector2.One;
			float num;
			if (this._isOpen)
			{
				Widget child = this.DropdownContainerWidget.GetChild(0);
				num = child.Size.Y + child.MarginBottom * base._scaleToUse;
			}
			else
			{
				num = 0f;
			}
			vector = this.Button.GlobalPosition + new Vector2((this.Button.Size.X - this.DropdownClipWidget.Size.X) / 2f, this.Button.Size.Y) - new Vector2(base.EventManager.LeftUsableAreaStart, base.EventManager.TopUsableAreaStart);
			this.DropdownClipWidget.ScaledPositionXOffset = vector.X;
			float amount = MathF.Clamp(dt * this._animationSpeedModifier, 0f, 1f);
			this.DropdownClipWidget.ScaledSuggestedHeight = MathF.Lerp(this.DropdownClipWidget.ScaledSuggestedHeight, num, amount, 1E-05f);
			this.DropdownClipWidget.ScaledPositionYOffset = MathF.Lerp(this.DropdownClipWidget.ScaledPositionYOffset, vector.Y, amount, 1E-05f);
			if (!this._isOpen && MathF.Abs(this.DropdownClipWidget.ScaledSuggestedHeight - num) < 0.5f)
			{
				this.DropdownClipWidget.IsVisible = false;
				return;
			}
			if (this._isOpen)
			{
				this.DropdownClipWidget.IsVisible = true;
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000281D File Offset: 0x00000A1D
		protected virtual void OpenPanel()
		{
			this._isOpen = true;
			this.DropdownClipWidget.IsVisible = true;
			this.CreateNavigationScope();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002838 File Offset: 0x00000A38
		protected virtual void ClosePanel()
		{
			this._isOpen = false;
			this.ClearGamepadNavigationScopeData();
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002847 File Offset: 0x00000A47
		private void ClosePanelInOneFrame()
		{
			this._isOpen = false;
			this.DropdownClipWidget.IsVisible = false;
			this.DropdownClipWidget.ScaledSuggestedHeight = 0f;
			this.ClearGamepadNavigationScopeData();
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002874 File Offset: 0x00000A74
		private void CreateNavigationScope()
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

		// Token: 0x06000054 RID: 84 RVA: 0x000029C8 File Offset: 0x00000BC8
		private void OnWidgetGainedNavigationFocus(Widget widget)
		{
			ScrollablePanel parentScrollablePanelOfWidget = this.GetParentScrollablePanelOfWidget(widget);
			if (parentScrollablePanelOfWidget != null)
			{
				parentScrollablePanelOfWidget.ScrollToChild(widget, -1f, -1f, 0, 0, 0f, 0f);
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002A00 File Offset: 0x00000C00
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

		// Token: 0x06000056 RID: 86 RVA: 0x00002A28 File Offset: 0x00000C28
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

		// Token: 0x06000057 RID: 87 RVA: 0x00002A68 File Offset: 0x00000C68
		private void ClearGamepadNavigationScopeData()
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

		// Token: 0x06000058 RID: 88 RVA: 0x00002B48 File Offset: 0x00000D48
		public void OnButtonClick(Widget widget)
		{
			if (!this._changedByControllerNavigation)
			{
				this._buttonClicked = true;
				if (this._isOpen)
				{
					this.ClosePanel();
				}
				else
				{
					this.OpenPanel();
				}
			}
			base.EventFired("OnDropdownClick", Array.Empty<object>());
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002B7F File Offset: 0x00000D7F
		public void UpdateButtonText(string text)
		{
			if (this.TextWidget == null)
			{
				return;
			}
			if (text != null)
			{
				this.TextWidget.Text = text;
				return;
			}
			this.TextWidget.Text = " ";
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002BAA File Offset: 0x00000DAA
		public void OnListChanged(Widget widget)
		{
			this.RefreshSelectedItem();
			this.DropdownContainerWidget.IsVisible = (widget.ChildCount > 1);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002BC6 File Offset: 0x00000DC6
		public void OnListChanged(Widget parentWidget, Widget addedWidget)
		{
			this.RefreshSelectedItem();
			this.DropdownContainerWidget.IsVisible = (parentWidget.ChildCount > 0);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002BE2 File Offset: 0x00000DE2
		public void OnSelectionChanged(Widget widget)
		{
			if (this.UpdateSelectedItem)
			{
				this.CurrentSelectedIndex = this.ListPanelValue;
				this.RefreshSelectedItem();
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002C00 File Offset: 0x00000E00
		private void RefreshSelectedItem()
		{
			if (this.UpdateSelectedItem)
			{
				this.ListPanelValue = this.CurrentSelectedIndex;
				string text = "";
				if (this.ListPanelValue >= 0 && this.ListPanel != null)
				{
					Widget child = this.ListPanel.GetChild(this.ListPanelValue);
					if (child != null)
					{
						foreach (Widget widget in child.AllChildren)
						{
							RichTextWidget richTextWidget = widget as RichTextWidget;
							if (richTextWidget != null)
							{
								text = richTextWidget.Text;
							}
						}
					}
				}
				this.UpdateButtonText(text);
				if (this.ListPanel != null)
				{
					for (int i = 0; i < this.ListPanel.ChildCount; i++)
					{
						Widget child2 = this.ListPanel.GetChild(i);
						if (this.CurrentSelectedIndex == i)
						{
							if (child2.CurrentState != "Selected")
							{
								child2.SetState("Selected");
							}
							if (child2 is ButtonWidget)
							{
								(child2 as ButtonWidget).IsSelected = (this.CurrentSelectedIndex == i);
							}
						}
					}
				}
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002D18 File Offset: 0x00000F18
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00002D20 File Offset: 0x00000F20
		[Editor(false)]
		public ButtonWidget Button
		{
			get
			{
				return this._button;
			}
			set
			{
				if (this._button != null)
				{
					this._button.ClickEventHandlers.Remove(this._clickHandler);
				}
				this._button = value;
				if (this._button != null)
				{
					this._button.ClickEventHandlers.Add(this._clickHandler);
				}
				this.RefreshSelectedItem();
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002D77 File Offset: 0x00000F77
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00002D7F File Offset: 0x00000F7F
		[Editor(false)]
		public Widget DropdownContainerWidget
		{
			get
			{
				return this._dropdownContainerWidget;
			}
			set
			{
				this._dropdownContainerWidget = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002D88 File Offset: 0x00000F88
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00002D90 File Offset: 0x00000F90
		[Editor(false)]
		public Widget DropdownClipWidget
		{
			get
			{
				return this._dropdownClipWidget;
			}
			set
			{
				this._dropdownClipWidget = value;
				this._dropdownClipWidget.HorizontalAlignment = HorizontalAlignment.Left;
				this._dropdownClipWidget.VerticalAlignment = VerticalAlignment.Top;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002DB1 File Offset: 0x00000FB1
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00002DBC File Offset: 0x00000FBC
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
					this._listPanel.SelectEventHandlers.Add(this._listSelectionHandler);
					this._listPanel.ItemAddEventHandlers.Add(this._listItemAddedHandler);
					this._listPanel.ItemRemoveEventHandlers.Add(this._listItemRemovedHandler);
				}
				this.RefreshSelectedItem();
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002E6D File Offset: 0x0000106D
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00002E84 File Offset: 0x00001084
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

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002EA8 File Offset: 0x000010A8
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00002EB0 File Offset: 0x000010B0
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
					base.OnPropertyChanged(this.CurrentSelectedIndex, "CurrentSelectedIndex");
				}
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00002ED3 File Offset: 0x000010D3
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00002EDB File Offset: 0x000010DB
		[Editor(false)]
		public bool UpdateSelectedItem
		{
			get
			{
				return this._updateSelectedItem;
			}
			set
			{
				if (this._updateSelectedItem != value)
				{
					this._updateSelectedItem = value;
				}
			}
		}

		// Token: 0x0400000A RID: 10
		private const string _checkboxSound = "checkbox";

		// Token: 0x0400000B RID: 11
		private Action<Widget> _clickHandler;

		// Token: 0x0400000C RID: 12
		private Action<Widget> _listSelectionHandler;

		// Token: 0x0400000D RID: 13
		private Action<Widget, Widget> _listItemRemovedHandler;

		// Token: 0x0400000E RID: 14
		private Action<Widget, Widget> _listItemAddedHandler;

		// Token: 0x0400000F RID: 15
		private Vector2 _dropdownOpenPosition;

		// Token: 0x04000010 RID: 16
		private float _animationSpeedModifier = 15f;

		// Token: 0x04000011 RID: 17
		private bool _initialized;

		// Token: 0x04000012 RID: 18
		private bool _changedByControllerNavigation;

		// Token: 0x04000013 RID: 19
		private GamepadNavigationScope _navigationScope;

		// Token: 0x04000014 RID: 20
		private GamepadNavigationForcedScopeCollection _scopeCollection;

		// Token: 0x04000015 RID: 21
		private bool _previousOpenState;

		// Token: 0x04000018 RID: 24
		private ButtonWidget _button;

		// Token: 0x04000019 RID: 25
		private ListPanel _listPanel;

		// Token: 0x0400001A RID: 26
		private int _currentSelectedIndex;

		// Token: 0x0400001B RID: 27
		private Widget _dropdownContainerWidget;

		// Token: 0x0400001C RID: 28
		private Widget _dropdownClipWidget;

		// Token: 0x0400001D RID: 29
		private bool _isOpen;

		// Token: 0x0400001E RID: 30
		private bool _buttonClicked;

		// Token: 0x0400001F RID: 31
		private bool _updateSelectedItem = true;
	}
}
