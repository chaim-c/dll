using System;
using System.Collections.Generic;
using System.Numerics;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.GamepadNavigation;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x02000013 RID: 19
	public class ContextMenuBrushWidget : BrushWidget
	{
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00004510 File Offset: 0x00002710
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x00004518 File Offset: 0x00002718
		public float HorizontalPadding { get; set; } = 10f;

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00004521 File Offset: 0x00002721
		// (set) Token: 0x060000EA RID: 234 RVA: 0x00004529 File Offset: 0x00002729
		public float VerticalPadding { get; set; } = 10f;

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00004532 File Offset: 0x00002732
		private bool _isClickedOnOtherWidget
		{
			get
			{
				return this._isPrimaryClickedOnOtherWidget || this._isAlternateClickedOnOtherWidget;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00004544 File Offset: 0x00002744
		private bool _isPrimaryClickedOnOtherWidget
		{
			get
			{
				return this._latestMouseUpWidgetWhenActivated != base.EventManager.LatestMouseDownWidget && !base.CheckIsMyChildRecursive(base.EventManager.LatestMouseDownWidget);
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000ED RID: 237 RVA: 0x0000456F File Offset: 0x0000276F
		private bool _isAlternateClickedOnOtherWidget
		{
			get
			{
				return this._latestAltMouseUpWidgetWhenActivated != base.EventManager.LatestMouseAlternateDownWidget && !base.CheckIsMyChildRecursive(base.EventManager.LatestMouseAlternateDownWidget);
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000459C File Offset: 0x0000279C
		public ContextMenuBrushWidget(UIContext context) : base(context)
		{
			this._newlyAddedItemList = new List<ContextMenuItemWidget>();
			this._newlyRemovedItemList = new List<ContextMenuItemWidget>();
			base.EventManager.AddLateUpdateAction(this, new Action<float>(this.CustomLateUpdate), 1);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000045F8 File Offset: 0x000027F8
		private void CustomLateUpdate(float dt)
		{
			if (!this._isDestroyed)
			{
				base.EventManager.AddLateUpdateAction(this, new Action<float>(this.CustomLateUpdate), 1);
				if (base.IsVisible && !base.IsRecursivelyVisible())
				{
					this.Deactivate();
				}
				if (base.IsVisible && !this._isActivatedThisFrame && this._isClickedOnOtherWidget)
				{
					this.Deactivate();
				}
				if (this._isActivatedThisFrame)
				{
					Vector2 globalPoint = this.DetermineMenuPositionFromMousePosition(base.EventManager.MousePosition);
					this._targetPosition = base.ParentWidget.GetLocalPoint(globalPoint);
					this._isActivatedThisFrame = false;
				}
				base.ScaledPositionXOffset = MathF.Clamp(this._targetPosition.X, 0f, base.EventManager.PageSize.X - base.Size.X);
				base.ScaledPositionYOffset = MathF.Clamp(this._targetPosition.Y, 0f, base.EventManager.PageSize.Y - base.Size.Y);
				this.HandleNewlyAddedRemovedList();
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004704 File Offset: 0x00002904
		private void HandleNewlyAddedRemovedList()
		{
			foreach (ContextMenuItemWidget contextMenuItemWidget in this._newlyAddedItemList)
			{
				contextMenuItemWidget.ActionButtonWidget.ClickEventHandlers.Add(new Action<Widget>(this.OnAnyAction));
			}
			this._newlyAddedItemList.Clear();
			foreach (ContextMenuItemWidget contextMenuItemWidget2 in this._newlyRemovedItemList)
			{
				contextMenuItemWidget2.ActionButtonWidget.ClickEventHandlers.Remove(new Action<Widget>(this.OnAnyAction));
			}
			this._newlyRemovedItemList.Clear();
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000047D8 File Offset: 0x000029D8
		protected override void OnDisconnectedFromRoot()
		{
			base.OnDisconnectedFromRoot();
			this._isDestroyed = true;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000047E7 File Offset: 0x000029E7
		private void Activate()
		{
			this._isActivatedThisFrame = true;
			this._latestMouseUpWidgetWhenActivated = base.EventManager.LatestMouseDownWidget;
			this._latestAltMouseUpWidgetWhenActivated = base.EventManager.LatestMouseAlternateDownWidget;
			base.IsVisible = true;
			this.AddGamepadNavigation();
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004820 File Offset: 0x00002A20
		private void Deactivate()
		{
			base.ScaledPositionXOffset = base.EventManager.PageSize.X;
			base.ScaledPositionYOffset = base.EventManager.PageSize.Y;
			base.IsVisible = false;
			this.IsActivated = false;
			this.DestroyGamepadNavigation();
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004870 File Offset: 0x00002A70
		private void AddGamepadNavigation()
		{
			if (this._navigationScope == null && this._scopeCollection == null)
			{
				this._navigationScope = new GamepadNavigationScope
				{
					ScopeID = "ContextMenuScope",
					ScopeMovements = GamepadNavigationTypes.Vertical,
					ParentWidget = this,
					DoNotAutomaticallyFindChildren = true,
					HasCircularMovement = true
				};
				base.GamepadNavigationContext.AddNavigationScope(this._navigationScope, false);
				for (int i = 0; i < this.ActionListPanel.Children.Count; i++)
				{
					ContextMenuItemWidget widget;
					if ((widget = (this.ActionListPanel.Children[i] as ContextMenuItemWidget)) != null)
					{
						this._navigationScope.AddWidgetAtIndex(widget, i);
					}
				}
				this._scopeCollection = new GamepadNavigationForcedScopeCollection
				{
					CollectionID = "ContextMenuCollection",
					CollectionOrder = 999,
					ParentWidget = this
				};
				base.GamepadNavigationContext.AddForcedScopeCollection(this._scopeCollection);
			}
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004954 File Offset: 0x00002B54
		private void DestroyGamepadNavigation()
		{
			if (this._navigationScope != null && this._scopeCollection != null)
			{
				this._navigationScope.ClearNavigatableWidgets();
				this._scopeCollection.ClearScopes();
				base.GamepadNavigationContext.RemoveNavigationScope(this._navigationScope);
				base.GamepadNavigationContext.RemoveForcedScopeCollection(this._scopeCollection);
				this._navigationScope = null;
				this._scopeCollection = null;
			}
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000049B7 File Offset: 0x00002BB7
		private void OnScrollOfContextItem(float scrollAmount)
		{
			this.Deactivate();
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000049BF File Offset: 0x00002BBF
		private void OnAnyAction(Widget obj)
		{
			this.Deactivate();
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000049C8 File Offset: 0x00002BC8
		private void OnNewActionButtonRemoved(Widget obj, Widget child)
		{
			ContextMenuItemWidget item;
			if ((item = (child as ContextMenuItemWidget)) != null)
			{
				this._newlyRemovedItemList.Add(item);
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000049EC File Offset: 0x00002BEC
		private void OnNewActionButtonAdded(Widget listPanel, Widget child)
		{
			ContextMenuItemWidget item;
			if ((item = (child as ContextMenuItemWidget)) != null)
			{
				this._newlyAddedItemList.Add(item);
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004A10 File Offset: 0x00002C10
		private Vector2 DetermineMenuPositionFromMousePosition(Vector2 mousePosition)
		{
			bool flag = mousePosition.X > base.EventManager.PageSize.X / 2f;
			bool flag2 = mousePosition.Y > base.EventManager.PageSize.Y / 2f;
			float num = flag ? (mousePosition.X - base.Size.X) : mousePosition.X;
			float num2 = flag2 ? (mousePosition.Y - base.Size.Y) : mousePosition.Y;
			float x = num + (flag ? (-this.HorizontalPadding) : this.HorizontalPadding);
			num2 += (flag2 ? (-this.VerticalPadding) : this.VerticalPadding);
			return new Vector2(x, num2);
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00004AC4 File Offset: 0x00002CC4
		// (set) Token: 0x060000FC RID: 252 RVA: 0x00004ACC File Offset: 0x00002CCC
		[Editor(false)]
		public bool IsActivated
		{
			get
			{
				return this._isActivated;
			}
			set
			{
				if (this._isActivated != value)
				{
					this._isActivated = value;
					base.OnPropertyChanged(value, "IsActivated");
					if (this._isActivated)
					{
						this.Activate();
						return;
					}
					this.Deactivate();
				}
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00004AFF File Offset: 0x00002CFF
		// (set) Token: 0x060000FE RID: 254 RVA: 0x00004B08 File Offset: 0x00002D08
		[Editor(false)]
		public ListPanel ActionListPanel
		{
			get
			{
				return this._actionListPanel;
			}
			set
			{
				if (this._actionListPanel != value)
				{
					this._actionListPanel = value;
					this._actionListPanel.ItemAddEventHandlers.Add(new Action<Widget, Widget>(this.OnNewActionButtonAdded));
					this._actionListPanel.ItemRemoveEventHandlers.Add(new Action<Widget, Widget>(this.OnNewActionButtonRemoved));
					base.OnPropertyChanged<ListPanel>(value, "ActionListPanel");
				}
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00004B69 File Offset: 0x00002D69
		// (set) Token: 0x06000100 RID: 256 RVA: 0x00004B71 File Offset: 0x00002D71
		[Editor(false)]
		public ScrollablePanel ScrollPanelToWatch
		{
			get
			{
				return this._scrollPanelToWatch;
			}
			set
			{
				if (this._scrollPanelToWatch != value)
				{
					this._scrollPanelToWatch = value;
					this._scrollPanelToWatch.OnScroll += this.OnScrollOfContextItem;
					base.OnPropertyChanged<ScrollablePanel>(value, "ScrollPanelToWatch");
				}
			}
		}

		// Token: 0x04000070 RID: 112
		private Vector2 _targetPosition;

		// Token: 0x04000071 RID: 113
		private Widget _latestMouseUpWidgetWhenActivated;

		// Token: 0x04000072 RID: 114
		private Widget _latestAltMouseUpWidgetWhenActivated;

		// Token: 0x04000073 RID: 115
		private bool _isDestroyed;

		// Token: 0x04000074 RID: 116
		private bool _isActivatedThisFrame;

		// Token: 0x04000075 RID: 117
		private List<ContextMenuItemWidget> _newlyAddedItemList;

		// Token: 0x04000076 RID: 118
		private List<ContextMenuItemWidget> _newlyRemovedItemList;

		// Token: 0x04000077 RID: 119
		private GamepadNavigationScope _navigationScope;

		// Token: 0x04000078 RID: 120
		private GamepadNavigationForcedScopeCollection _scopeCollection;

		// Token: 0x04000079 RID: 121
		private bool _isActivated;

		// Token: 0x0400007A RID: 122
		public ScrollablePanel _scrollPanelToWatch;

		// Token: 0x0400007B RID: 123
		public ListPanel _actionListPanel;
	}
}
