using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.GamepadNavigation;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Library.EventSystem;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI
{
	// Token: 0x0200001F RID: 31
	public class EventManager
	{
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000238 RID: 568 RVA: 0x0000B444 File Offset: 0x00009644
		// (set) Token: 0x06000239 RID: 569 RVA: 0x0000B44C File Offset: 0x0000964C
		public float Time { get; private set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0000B455 File Offset: 0x00009655
		// (set) Token: 0x0600023B RID: 571 RVA: 0x0000B45D File Offset: 0x0000965D
		public Vec2 UsableArea { get; set; } = new Vec2(1f, 1f);

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0000B466 File Offset: 0x00009666
		// (set) Token: 0x0600023D RID: 573 RVA: 0x0000B46E File Offset: 0x0000966E
		public float LeftUsableAreaStart { get; private set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600023E RID: 574 RVA: 0x0000B477 File Offset: 0x00009677
		// (set) Token: 0x0600023F RID: 575 RVA: 0x0000B47F File Offset: 0x0000967F
		public float TopUsableAreaStart { get; private set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000240 RID: 576 RVA: 0x0000B488 File Offset: 0x00009688
		// (set) Token: 0x06000241 RID: 577 RVA: 0x0000B48F File Offset: 0x0000968F
		public static EventManager UIEventManager { get; private set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000242 RID: 578 RVA: 0x0000B497 File Offset: 0x00009697
		public Vector2 MousePositionInReferenceResolution
		{
			get
			{
				return this.MousePosition * this.Context.CustomInverseScale;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0000B4AF File Offset: 0x000096AF
		// (set) Token: 0x06000244 RID: 580 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public bool IsControllerActive { get; private set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0000B4C0 File Offset: 0x000096C0
		// (set) Token: 0x06000246 RID: 582 RVA: 0x0000B4C8 File Offset: 0x000096C8
		public Vector2 PageSize { get; private set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000B4D1 File Offset: 0x000096D1
		// (set) Token: 0x06000248 RID: 584 RVA: 0x0000B4D9 File Offset: 0x000096D9
		public UIContext Context { get; private set; }

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000249 RID: 585 RVA: 0x0000B4E4 File Offset: 0x000096E4
		// (remove) Token: 0x0600024A RID: 586 RVA: 0x0000B51C File Offset: 0x0000971C
		public event Action OnDragStarted;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600024B RID: 587 RVA: 0x0000B554 File Offset: 0x00009754
		// (remove) Token: 0x0600024C RID: 588 RVA: 0x0000B58C File Offset: 0x0000978C
		public event Action OnDragEnded;

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600024D RID: 589 RVA: 0x0000B5C1 File Offset: 0x000097C1
		// (set) Token: 0x0600024E RID: 590 RVA: 0x0000B5C9 File Offset: 0x000097C9
		public IInputService InputService { get; internal set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600024F RID: 591 RVA: 0x0000B5D2 File Offset: 0x000097D2
		// (set) Token: 0x06000250 RID: 592 RVA: 0x0000B5DA File Offset: 0x000097DA
		public IInputContext InputContext { get; internal set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000251 RID: 593 RVA: 0x0000B5E3 File Offset: 0x000097E3
		// (set) Token: 0x06000252 RID: 594 RVA: 0x0000B5EB File Offset: 0x000097EB
		public Widget Root { get; private set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000253 RID: 595 RVA: 0x0000B5F4 File Offset: 0x000097F4
		// (set) Token: 0x06000254 RID: 596 RVA: 0x0000B5FC File Offset: 0x000097FC
		public Widget FocusedWidget
		{
			get
			{
				return this._focusedWidget;
			}
			private set
			{
				if (value != null && value.ConnectedToRoot)
				{
					this._focusedWidget = value;
					return;
				}
				this._focusedWidget = null;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000255 RID: 597 RVA: 0x0000B618 File Offset: 0x00009818
		// (set) Token: 0x06000256 RID: 598 RVA: 0x0000B620 File Offset: 0x00009820
		public Widget HoveredView
		{
			get
			{
				return this._hoveredView;
			}
			private set
			{
				if (value != null && value.ConnectedToRoot)
				{
					this._hoveredView = value;
					return;
				}
				this._hoveredView = null;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000257 RID: 599 RVA: 0x0000B63C File Offset: 0x0000983C
		// (set) Token: 0x06000258 RID: 600 RVA: 0x0000B644 File Offset: 0x00009844
		public List<Widget> MouseOveredViews
		{
			get
			{
				return this._mouseOveredViews;
			}
			private set
			{
				if (value != null)
				{
					this._mouseOveredViews = value;
					return;
				}
				this._mouseOveredViews = null;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000259 RID: 601 RVA: 0x0000B658 File Offset: 0x00009858
		// (set) Token: 0x0600025A RID: 602 RVA: 0x0000B660 File Offset: 0x00009860
		public Widget DragHoveredView
		{
			get
			{
				return this._dragHoveredView;
			}
			private set
			{
				if (value != null && value.ConnectedToRoot)
				{
					this._dragHoveredView = value;
					return;
				}
				this._dragHoveredView = null;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600025B RID: 603 RVA: 0x0000B67C File Offset: 0x0000987C
		// (set) Token: 0x0600025C RID: 604 RVA: 0x0000B684 File Offset: 0x00009884
		public Widget DraggedWidget { get; private set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000B690 File Offset: 0x00009890
		public Vector2 DraggedWidgetPosition
		{
			get
			{
				if (this.DraggedWidget != null)
				{
					return this._dragCarrier.GlobalPosition * this.Context.CustomScale - new Vector2(this.LeftUsableAreaStart, this.TopUsableAreaStart);
				}
				return this.MousePositionInReferenceResolution;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600025E RID: 606 RVA: 0x0000B6DD File Offset: 0x000098DD
		// (set) Token: 0x0600025F RID: 607 RVA: 0x0000B6E5 File Offset: 0x000098E5
		public Widget LatestMouseDownWidget
		{
			get
			{
				return this._latestMouseDownWidget;
			}
			private set
			{
				if (value != null && value.ConnectedToRoot)
				{
					this._latestMouseDownWidget = value;
					return;
				}
				this._latestMouseDownWidget = null;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000260 RID: 608 RVA: 0x0000B701 File Offset: 0x00009901
		// (set) Token: 0x06000261 RID: 609 RVA: 0x0000B709 File Offset: 0x00009909
		public Widget LatestMouseUpWidget
		{
			get
			{
				return this._latestMouseUpWidget;
			}
			private set
			{
				if (value != null && value.ConnectedToRoot)
				{
					this._latestMouseUpWidget = value;
					return;
				}
				this._latestMouseUpWidget = null;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0000B725 File Offset: 0x00009925
		// (set) Token: 0x06000263 RID: 611 RVA: 0x0000B72D File Offset: 0x0000992D
		public Widget LatestMouseAlternateDownWidget
		{
			get
			{
				return this._latestMouseAlternateDownWidget;
			}
			private set
			{
				if (value != null && value.ConnectedToRoot)
				{
					this._latestMouseAlternateDownWidget = value;
					return;
				}
				this._latestMouseAlternateDownWidget = null;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000B749 File Offset: 0x00009949
		// (set) Token: 0x06000265 RID: 613 RVA: 0x0000B751 File Offset: 0x00009951
		public Widget LatestMouseAlternateUpWidget
		{
			get
			{
				return this._latestMouseAlternateUpWidget;
			}
			private set
			{
				if (value != null && value.ConnectedToRoot)
				{
					this._latestMouseAlternateUpWidget = value;
					return;
				}
				this._latestMouseAlternateUpWidget = null;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000266 RID: 614 RVA: 0x0000B76D File Offset: 0x0000996D
		// (set) Token: 0x06000267 RID: 615 RVA: 0x0000B775 File Offset: 0x00009975
		public Vector2 MousePosition { get; private set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000268 RID: 616 RVA: 0x0000B77E File Offset: 0x0000997E
		private bool IsDragging
		{
			get
			{
				return this.DraggedWidget != null;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000B789 File Offset: 0x00009989
		public float DeltaMouseScroll
		{
			get
			{
				return this.InputContext.GetDeltaMouseScroll() * 0.4f;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600026A RID: 618 RVA: 0x0000B79C File Offset: 0x0000999C
		public float RightStickVerticalScrollAmount
		{
			get
			{
				float y = Input.GetKeyState(InputKey.ControllerRStick).Y;
				return 3000f * y * 0.4f * this.CachedDt;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600026B RID: 619 RVA: 0x0000B7D0 File Offset: 0x000099D0
		public float RightStickHorizontalScrollAmount
		{
			get
			{
				float x = Input.GetKeyState(InputKey.ControllerRStick).X;
				return 3000f * x * 0.4f * this.CachedDt;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0000B804 File Offset: 0x00009A04
		// (set) Token: 0x0600026D RID: 621 RVA: 0x0000B80C File Offset: 0x00009A0C
		internal float CachedDt { get; private set; }

		// Token: 0x0600026E RID: 622 RVA: 0x0000B818 File Offset: 0x00009A18
		internal EventManager(UIContext context)
		{
			this.Context = context;
			this.Root = new Widget(context)
			{
				Id = "Root"
			};
			if (EventManager.UIEventManager == null)
			{
				EventManager.UIEventManager = new EventManager();
			}
			this._widgetsWithUpdateContainer = new WidgetContainer(context, 64, WidgetContainer.ContainerType.Update);
			this._widgetsWithParallelUpdateContainer = new WidgetContainer(context, 64, WidgetContainer.ContainerType.ParallelUpdate);
			this._widgetsWithLateUpdateContainer = new WidgetContainer(context, 64, WidgetContainer.ContainerType.LateUpdate);
			this._widgetsWithTweenPositionsContainer = new WidgetContainer(context, 64, WidgetContainer.ContainerType.TweenPosition);
			this._widgetsWithVisualDefinitionsContainer = new WidgetContainer(context, 64, WidgetContainer.ContainerType.VisualDefinition);
			this._widgetsWithUpdateBrushesContainer = new WidgetContainer(context, 64, WidgetContainer.ContainerType.UpdateBrushes);
			this._lateUpdateActionLocker = new object();
			this._lateUpdateActions = new Dictionary<int, List<UpdateAction>>();
			this._lateUpdateActionsRunning = new Dictionary<int, List<UpdateAction>>();
			this._onAfterFinalizedCallbacks = new List<Action>();
			for (int i = 1; i <= 5; i++)
			{
				this._lateUpdateActions.Add(i, new List<UpdateAction>(32));
				this._lateUpdateActionsRunning.Add(i, new List<UpdateAction>(32));
			}
			this._drawContext = new TwoDimensionDrawContext();
			this.MouseOveredViews = new List<Widget>();
			this.ParallelUpdateWidgetPredicate = new TWParallel.ParallelForWithDtAuxPredicate(this.ParallelUpdateWidget);
			this.WidgetDoTweenPositionAuxPredicate = new TWParallel.ParallelForWithDtAuxPredicate(this.WidgetDoTweenPositionAux);
			this.UpdateBrushesWidgetPredicate = new TWParallel.ParallelForWithDtAuxPredicate(this.UpdateBrushesWidget);
			this.IsControllerActive = (Input.IsControllerConnected && !Input.IsMouseActive);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000B9B0 File Offset: 0x00009BB0
		internal void OnFinalize()
		{
			if (!this._lastSetFrictionValue.ApproximatelyEqualsTo(1f, 1E-05f))
			{
				this._lastSetFrictionValue = 1f;
				Input.SetCursorFriction(this._lastSetFrictionValue);
			}
			this._widgetsWithLateUpdateContainer.Clear();
			this._widgetsWithParallelUpdateContainer.Clear();
			this._widgetsWithTweenPositionsContainer.Clear();
			this._widgetsWithUpdateBrushesContainer.Clear();
			this._widgetsWithUpdateContainer.Clear();
			this._widgetsWithVisualDefinitionsContainer.Clear();
			for (int i = 0; i < this._onAfterFinalizedCallbacks.Count; i++)
			{
				Action action = this._onAfterFinalizedCallbacks[i];
				if (action != null)
				{
					action();
				}
			}
			this._onAfterFinalizedCallbacks.Clear();
			this._onAfterFinalizedCallbacks = null;
			this._widgetsWithLateUpdateContainer = null;
			this._widgetsWithParallelUpdateContainer = null;
			this._widgetsWithTweenPositionsContainer = null;
			this._widgetsWithUpdateBrushesContainer = null;
			this._widgetsWithUpdateContainer = null;
			this._widgetsWithVisualDefinitionsContainer = null;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000BA95 File Offset: 0x00009C95
		public void AddAfterFinalizedCallback(Action callback)
		{
			this._onAfterFinalizedCallbacks.Add(callback);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000BAA4 File Offset: 0x00009CA4
		internal void OnWidgetConnectedToRoot(Widget widget)
		{
			widget.HandleOnConnectedToRoot();
			foreach (Widget widget2 in widget.AllChildrenAndThis)
			{
				widget2.HandleOnConnectedToRoot();
				this.RegisterWidgetForEvent(WidgetContainer.ContainerType.Update, widget2);
				this.RegisterWidgetForEvent(WidgetContainer.ContainerType.LateUpdate, widget2);
				this.RegisterWidgetForEvent(WidgetContainer.ContainerType.UpdateBrushes, widget2);
				this.RegisterWidgetForEvent(WidgetContainer.ContainerType.ParallelUpdate, widget2);
				this.RegisterWidgetForEvent(WidgetContainer.ContainerType.VisualDefinition, widget2);
				this.RegisterWidgetForEvent(WidgetContainer.ContainerType.TweenPosition, widget2);
			}
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000BB28 File Offset: 0x00009D28
		internal void OnWidgetDisconnectedFromRoot(Widget widget)
		{
			widget.HandleOnDisconnectedFromRoot();
			if (widget == this.DraggedWidget && this.DraggedWidget.DragWidget != null)
			{
				this.ReleaseDraggedWidget();
				this.ClearDragObject();
			}
			GauntletGamepadNavigationManager.Instance.OnWidgetDisconnectedFromRoot(widget);
			foreach (Widget widget2 in widget.AllChildrenAndThis)
			{
				widget2.HandleOnDisconnectedFromRoot();
				this.UnRegisterWidgetForEvent(WidgetContainer.ContainerType.Update, widget2);
				this.UnRegisterWidgetForEvent(WidgetContainer.ContainerType.LateUpdate, widget2);
				this.UnRegisterWidgetForEvent(WidgetContainer.ContainerType.UpdateBrushes, widget2);
				this.UnRegisterWidgetForEvent(WidgetContainer.ContainerType.ParallelUpdate, widget2);
				this.UnRegisterWidgetForEvent(WidgetContainer.ContainerType.VisualDefinition, widget2);
				this.UnRegisterWidgetForEvent(WidgetContainer.ContainerType.TweenPosition, widget2);
				GauntletGamepadNavigationManager.Instance.OnWidgetDisconnectedFromRoot(widget2);
				widget2.GamepadNavigationIndex = -1;
				widget2.UsedNavigationMovements = GamepadNavigationTypes.None;
				widget2.IsUsingNavigation = false;
			}
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000BBF8 File Offset: 0x00009DF8
		internal void RegisterWidgetForEvent(WidgetContainer.ContainerType type, Widget widget)
		{
			WidgetContainer obj;
			switch (type)
			{
			case WidgetContainer.ContainerType.None:
				return;
			case WidgetContainer.ContainerType.Update:
				obj = this._widgetsWithUpdateContainer;
				lock (obj)
				{
					if (widget.WidgetInfo.GotCustomUpdate && widget.OnUpdateListIndex < 0)
					{
						widget.OnUpdateListIndex = this._widgetsWithUpdateContainer.Add(widget);
					}
					return;
				}
				break;
			case WidgetContainer.ContainerType.ParallelUpdate:
				break;
			case WidgetContainer.ContainerType.LateUpdate:
				goto IL_B3;
			case WidgetContainer.ContainerType.VisualDefinition:
				goto IL_FB;
			case WidgetContainer.ContainerType.TweenPosition:
				goto IL_13E;
			case WidgetContainer.ContainerType.UpdateBrushes:
				goto IL_17E;
			default:
				return;
			}
			obj = this._widgetsWithParallelUpdateContainer;
			lock (obj)
			{
				if (widget.WidgetInfo.GotCustomParallelUpdate && widget.OnParallelUpdateListIndex < 0)
				{
					widget.OnParallelUpdateListIndex = this._widgetsWithParallelUpdateContainer.Add(widget);
				}
				return;
			}
			IL_B3:
			obj = this._widgetsWithLateUpdateContainer;
			lock (obj)
			{
				if (widget.WidgetInfo.GotCustomLateUpdate && widget.OnLateUpdateListIndex < 0)
				{
					widget.OnLateUpdateListIndex = this._widgetsWithLateUpdateContainer.Add(widget);
				}
				return;
			}
			IL_FB:
			obj = this._widgetsWithVisualDefinitionsContainer;
			lock (obj)
			{
				if (widget.VisualDefinition != null && widget.OnVisualDefinitionListIndex < 0)
				{
					widget.OnVisualDefinitionListIndex = this._widgetsWithVisualDefinitionsContainer.Add(widget);
				}
				return;
			}
			IL_13E:
			obj = this._widgetsWithTweenPositionsContainer;
			lock (obj)
			{
				if (widget.TweenPosition && widget.OnTweenPositionListIndex < 0)
				{
					widget.OnTweenPositionListIndex = this._widgetsWithTweenPositionsContainer.Add(widget);
				}
				return;
			}
			IL_17E:
			obj = this._widgetsWithUpdateBrushesContainer;
			lock (obj)
			{
				if (widget.WidgetInfo.GotUpdateBrushes && widget.OnUpdateBrushesIndex < 0)
				{
					widget.OnUpdateBrushesIndex = this._widgetsWithUpdateBrushesContainer.Add(widget);
				}
			}
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000BE14 File Offset: 0x0000A014
		internal void UnRegisterWidgetForEvent(WidgetContainer.ContainerType type, Widget widget)
		{
			WidgetContainer obj;
			switch (type)
			{
			case WidgetContainer.ContainerType.None:
				return;
			case WidgetContainer.ContainerType.Update:
				obj = this._widgetsWithUpdateContainer;
				lock (obj)
				{
					if (widget.WidgetInfo.GotCustomUpdate && widget.OnUpdateListIndex != -1)
					{
						this._widgetsWithUpdateContainer.RemoveFromIndex(widget.OnUpdateListIndex);
						widget.OnUpdateListIndex = -1;
					}
					return;
				}
				break;
			case WidgetContainer.ContainerType.ParallelUpdate:
				break;
			case WidgetContainer.ContainerType.LateUpdate:
				goto IL_BF;
			case WidgetContainer.ContainerType.VisualDefinition:
				goto IL_10D;
			case WidgetContainer.ContainerType.TweenPosition:
				goto IL_156;
			case WidgetContainer.ContainerType.UpdateBrushes:
				goto IL_19C;
			default:
				return;
			}
			obj = this._widgetsWithParallelUpdateContainer;
			lock (obj)
			{
				if (widget.WidgetInfo.GotCustomParallelUpdate && widget.OnParallelUpdateListIndex != -1)
				{
					this._widgetsWithParallelUpdateContainer.RemoveFromIndex(widget.OnParallelUpdateListIndex);
					widget.OnParallelUpdateListIndex = -1;
				}
				return;
			}
			IL_BF:
			obj = this._widgetsWithLateUpdateContainer;
			lock (obj)
			{
				if (widget.WidgetInfo.GotCustomLateUpdate && widget.OnLateUpdateListIndex != -1)
				{
					this._widgetsWithLateUpdateContainer.RemoveFromIndex(widget.OnLateUpdateListIndex);
					widget.OnLateUpdateListIndex = -1;
				}
				return;
			}
			IL_10D:
			obj = this._widgetsWithVisualDefinitionsContainer;
			lock (obj)
			{
				if (widget.VisualDefinition != null && widget.OnVisualDefinitionListIndex != -1)
				{
					this._widgetsWithVisualDefinitionsContainer.RemoveFromIndex(widget.OnVisualDefinitionListIndex);
					widget.OnVisualDefinitionListIndex = -1;
				}
				return;
			}
			IL_156:
			obj = this._widgetsWithTweenPositionsContainer;
			lock (obj)
			{
				if (widget.TweenPosition && widget.OnTweenPositionListIndex != -1)
				{
					this._widgetsWithTweenPositionsContainer.RemoveFromIndex(widget.OnTweenPositionListIndex);
					widget.OnTweenPositionListIndex = -1;
				}
				return;
			}
			IL_19C:
			obj = this._widgetsWithUpdateBrushesContainer;
			lock (obj)
			{
				if (widget.WidgetInfo.GotUpdateBrushes && widget.OnUpdateBrushesIndex != -1)
				{
					this._widgetsWithUpdateBrushesContainer.RemoveFromIndex(widget.OnUpdateBrushesIndex);
					widget.OnUpdateBrushesIndex = -1;
				}
			}
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000C054 File Offset: 0x0000A254
		internal void OnWidgetVisualDefinitionChanged(Widget widget)
		{
			if (widget.VisualDefinition != null)
			{
				this.RegisterWidgetForEvent(WidgetContainer.ContainerType.VisualDefinition, widget);
				return;
			}
			this.UnRegisterWidgetForEvent(WidgetContainer.ContainerType.VisualDefinition, widget);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000C06F File Offset: 0x0000A26F
		internal void OnWidgetTweenPositionChanged(Widget widget)
		{
			if (widget.TweenPosition)
			{
				this.RegisterWidgetForEvent(WidgetContainer.ContainerType.TweenPosition, widget);
				return;
			}
			this.UnRegisterWidgetForEvent(WidgetContainer.ContainerType.TweenPosition, widget);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000C08A File Offset: 0x0000A28A
		private void MeasureAll()
		{
			this.Root.Measure(this.PageSize);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000C09D File Offset: 0x0000A29D
		private void LayoutAll(float left, float bottom, float right, float top)
		{
			this.Root.Layout(left, bottom, right, top);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000C0AF File Offset: 0x0000A2AF
		private void UpdatePositions()
		{
			this.Root.UpdatePosition(Vector2.Zero);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000C0C4 File Offset: 0x0000A2C4
		private void WidgetDoTweenPositionAux(int startInclusive, int endExclusive, float deltaTime)
		{
			List<Widget> currentList = this._widgetsWithParallelUpdateContainer.GetCurrentList();
			for (int i = startInclusive; i < endExclusive; i++)
			{
				currentList[i].DoTweenPosition(deltaTime);
			}
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000C0F6 File Offset: 0x0000A2F6
		private void ParallelDoTweenPositions(float dt)
		{
			TWParallel.For(0, this._widgetsWithTweenPositionsContainer.Count, dt, this.WidgetDoTweenPositionAuxPredicate, 16);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000C114 File Offset: 0x0000A314
		private void TweenPositions(float dt)
		{
			if (this._widgetsWithTweenPositionsContainer.CheckFragmentation())
			{
				WidgetContainer widgetsWithTweenPositionsContainer = this._widgetsWithTweenPositionsContainer;
				lock (widgetsWithTweenPositionsContainer)
				{
					this._widgetsWithTweenPositionsContainer.DoDefragmentation();
				}
			}
			if (this._widgetsWithTweenPositionsContainer.Count > 64)
			{
				this.ParallelDoTweenPositions(dt);
				return;
			}
			List<Widget> currentList = this._widgetsWithTweenPositionsContainer.GetCurrentList();
			for (int i = 0; i < currentList.Count; i++)
			{
				currentList[i].DoTweenPosition(dt);
			}
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000C1A8 File Offset: 0x0000A3A8
		internal void CalculateCanvas(Vector2 pageSize, float dt)
		{
			if (this._measureDirty > 0 || this._layoutDirty > 0)
			{
				this.PageSize = pageSize;
				Vec2 vec = new Vec2(pageSize.X / this.UsableArea.X, pageSize.Y / this.UsableArea.Y);
				this.LeftUsableAreaStart = (vec.X - vec.X * this.UsableArea.X) / 2f;
				this.TopUsableAreaStart = (vec.Y - vec.Y * this.UsableArea.Y) / 2f;
				if (this._measureDirty > 0)
				{
					this.MeasureAll();
				}
				this.LayoutAll(this.LeftUsableAreaStart, this.PageSize.Y, this.PageSize.X, this.TopUsableAreaStart);
				this.TweenPositions(dt);
				this.UpdatePositions();
				if (this._measureDirty > 0)
				{
					this._measureDirty--;
				}
				if (this._layoutDirty > 0)
				{
					this._layoutDirty--;
				}
				this._positionsDirty = false;
			}
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000C2D0 File Offset: 0x0000A4D0
		internal void RecalculateCanvas()
		{
			if (this._measureDirty == 2 || this._layoutDirty == 2)
			{
				if (this._measureDirty == 2)
				{
					this.MeasureAll();
				}
				this.LayoutAll(this.LeftUsableAreaStart, this.PageSize.Y, this.PageSize.X, this.TopUsableAreaStart);
				if (this._positionsDirty)
				{
					this.UpdatePositions();
					this._positionsDirty = false;
				}
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000C33C File Offset: 0x0000A53C
		internal void MouseDown()
		{
			this._mouseIsDown = true;
			this._lastClickPosition = new Vector2((float)this.InputContext.GetPointerX(), (float)this.InputContext.GetPointerY());
			Widget widgetAtPositionForEvent = this.GetWidgetAtPositionForEvent(GauntletEvent.MousePressed, this._lastClickPosition);
			if (widgetAtPositionForEvent != null)
			{
				this.DispatchEvent(widgetAtPositionForEvent, GauntletEvent.MousePressed);
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000C38C File Offset: 0x0000A58C
		internal void MouseUp()
		{
			this._mouseIsDown = false;
			this.MousePosition = new Vector2((float)this.InputContext.GetPointerX(), (float)this.InputContext.GetPointerY());
			if (this.IsDragging)
			{
				if (this.DraggedWidget.PreviewEvent(GauntletEvent.DragEnd))
				{
					this.DispatchEvent(this.DraggedWidget, GauntletEvent.DragEnd);
				}
				Widget widgetAtPositionForEvent = this.GetWidgetAtPositionForEvent(GauntletEvent.Drop, this.MousePosition);
				if (widgetAtPositionForEvent != null)
				{
					this.DispatchEvent(widgetAtPositionForEvent, GauntletEvent.Drop);
				}
				else
				{
					this.CancelAndReturnDrag();
				}
				if (this.DraggedWidget != null)
				{
					this.ClearDragObject();
					return;
				}
			}
			else
			{
				Widget widgetAtPositionForEvent2 = this.GetWidgetAtPositionForEvent(GauntletEvent.MouseReleased, new Vector2((float)this.InputContext.GetPointerX(), (float)this.InputContext.GetPointerY()));
				this.DispatchEvent(widgetAtPositionForEvent2, GauntletEvent.MouseReleased);
				this.LatestMouseUpWidget = widgetAtPositionForEvent2;
			}
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000C44C File Offset: 0x0000A64C
		internal void MouseAlternateDown()
		{
			this._mouseAlternateIsDown = true;
			this._lastAlternateClickPosition = new Vector2((float)this.InputContext.GetPointerX(), (float)this.InputContext.GetPointerY());
			Widget widgetAtPositionForEvent = this.GetWidgetAtPositionForEvent(GauntletEvent.MouseAlternatePressed, this._lastAlternateClickPosition);
			if (widgetAtPositionForEvent != null)
			{
				this.DispatchEvent(widgetAtPositionForEvent, GauntletEvent.MouseAlternatePressed);
			}
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000C49C File Offset: 0x0000A69C
		internal void MouseAlternateUp()
		{
			this._mouseAlternateIsDown = false;
			this.MousePosition = new Vector2((float)this.InputContext.GetPointerX(), (float)this.InputContext.GetPointerY());
			Widget widgetAtPositionForEvent = this.GetWidgetAtPositionForEvent(GauntletEvent.MouseAlternateReleased, this._lastAlternateClickPosition);
			this.DispatchEvent(widgetAtPositionForEvent, GauntletEvent.MouseAlternateReleased);
			this.LatestMouseAlternateUpWidget = widgetAtPositionForEvent;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000C4F0 File Offset: 0x0000A6F0
		internal void MouseScroll()
		{
			if (MathF.Abs(this.DeltaMouseScroll) > 0.001f)
			{
				Widget widgetAtPositionForEvent = this.GetWidgetAtPositionForEvent(GauntletEvent.MouseScroll, this.MousePosition);
				if (widgetAtPositionForEvent != null)
				{
					this.DispatchEvent(widgetAtPositionForEvent, GauntletEvent.MouseScroll);
				}
			}
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000C52C File Offset: 0x0000A72C
		internal void RightStickMovement()
		{
			if (Input.GetKeyState(InputKey.ControllerRStick).X != 0f || Input.GetKeyState(InputKey.ControllerRStick).Y != 0f)
			{
				Widget widgetAtPositionForEvent = this.GetWidgetAtPositionForEvent(GauntletEvent.RightStickMovement, this.MousePosition);
				if (widgetAtPositionForEvent != null)
				{
					this.DispatchEvent(widgetAtPositionForEvent, GauntletEvent.RightStickMovement);
				}
			}
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000C586 File Offset: 0x0000A786
		public void ClearFocus()
		{
			this.SetWidgetFocused(null, false);
			this.SetHoveredView(null);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000C598 File Offset: 0x0000A798
		private void CancelAndReturnDrag()
		{
			if (this._draggedWidgetPreviousParent != null)
			{
				this.DraggedWidget.ParentWidget = this._draggedWidgetPreviousParent;
				this.DraggedWidget.SetSiblingIndex(this._draggedWidgetIndex, false);
				this.DraggedWidget.PosOffset = new Vector2(0f, 0f);
				if (this.DraggedWidget.DragWidget != null)
				{
					this.DraggedWidget.DragWidget.ParentWidget = this.DraggedWidget;
					this.DraggedWidget.DragWidget.IsVisible = false;
				}
			}
			else
			{
				this.ReleaseDraggedWidget();
			}
			this._draggedWidgetPreviousParent = null;
			this._draggedWidgetIndex = -1;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000C638 File Offset: 0x0000A838
		private void ClearDragObject()
		{
			this.DraggedWidget = null;
			Action onDragEnded = this.OnDragEnded;
			if (onDragEnded != null)
			{
				onDragEnded();
			}
			this._dragOffset = new Vector2(0f, 0f);
			this._dragCarrier.ParentWidget = null;
			this._dragCarrier = null;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000C685 File Offset: 0x0000A885
		internal void UpdateMousePosition(Vector2 mousePos)
		{
			this.MousePosition = mousePos;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000C690 File Offset: 0x0000A890
		internal void MouseMove()
		{
			if (this._mouseIsDown)
			{
				if (this.IsDragging)
				{
					Widget widgetAtPositionForEvent = this.GetWidgetAtPositionForEvent(GauntletEvent.DragHover, this.MousePosition);
					if (widgetAtPositionForEvent != null)
					{
						this.DispatchEvent(widgetAtPositionForEvent, GauntletEvent.DragHover);
					}
					else
					{
						this.SetDragHoveredView(null);
					}
				}
				else if (this.LatestMouseDownWidget != null)
				{
					if (this.LatestMouseDownWidget.PreviewEvent(GauntletEvent.MouseMove))
					{
						this.DispatchEvent(this.LatestMouseDownWidget, GauntletEvent.MouseMove);
					}
					if (!this.IsDragging && this.LatestMouseDownWidget.PreviewEvent(GauntletEvent.DragBegin))
					{
						Vector2 vector = this._lastClickPosition - this.MousePosition;
						Vector2 vector2 = new Vector2(vector.X, vector.Y);
						if (vector2.LengthSquared() > 100f * this.Context.Scale)
						{
							this.DispatchEvent(this.LatestMouseDownWidget, GauntletEvent.DragBegin);
						}
					}
				}
			}
			else if (!this._mouseAlternateIsDown)
			{
				Widget widgetAtPositionForEvent2 = this.GetWidgetAtPositionForEvent(GauntletEvent.MouseMove, this.MousePosition);
				if (widgetAtPositionForEvent2 != null)
				{
					this.DispatchEvent(widgetAtPositionForEvent2, GauntletEvent.MouseMove);
				}
			}
			List<Widget> list = new List<Widget>();
			foreach (Widget widget in EventManager.AllVisibleWidgetsAt(this.Root, this.MousePosition))
			{
				if (!this.MouseOveredViews.Contains(widget))
				{
					widget.OnMouseOverBegin();
					GauntletGamepadNavigationManager instance = GauntletGamepadNavigationManager.Instance;
					if (instance != null)
					{
						instance.OnWidgetHoverBegin(widget);
					}
				}
				list.Add(widget);
			}
			foreach (Widget widget2 in this.MouseOveredViews.Except(list))
			{
				widget2.OnMouseOverEnd();
				if (widget2.GamepadNavigationIndex != -1)
				{
					GauntletGamepadNavigationManager instance2 = GauntletGamepadNavigationManager.Instance;
					if (instance2 != null)
					{
						instance2.OnWidgetHoverEnd(widget2);
					}
				}
			}
			this.MouseOveredViews = list;
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000C878 File Offset: 0x0000AA78
		private static bool IsPointInsideMeasuredArea(Widget w, Vector2 p)
		{
			return w.IsPointInsideMeasuredArea(p);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000C884 File Offset: 0x0000AA84
		private Widget GetWidgetAtPositionForEvent(GauntletEvent gauntletEvent, Vector2 pointerPosition)
		{
			Widget result = null;
			foreach (Widget widget in EventManager.AllEnabledWidgetsAt(this.Root, pointerPosition))
			{
				if (widget.PreviewEvent(gauntletEvent))
				{
					result = widget;
					break;
				}
			}
			return result;
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600028C RID: 652 RVA: 0x0000C8E0 File Offset: 0x0000AAE0
		// (remove) Token: 0x0600028D RID: 653 RVA: 0x0000C918 File Offset: 0x0000AB18
		public event Action LoseFocus;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600028E RID: 654 RVA: 0x0000C950 File Offset: 0x0000AB50
		// (remove) Token: 0x0600028F RID: 655 RVA: 0x0000C988 File Offset: 0x0000AB88
		public event Action GainFocus;

		// Token: 0x06000290 RID: 656 RVA: 0x0000C9C0 File Offset: 0x0000ABC0
		private void DispatchEvent(Widget selectedWidget, GauntletEvent gauntletEvent)
		{
			if (gauntletEvent != GauntletEvent.MouseReleased)
			{
			}
			switch (gauntletEvent)
			{
			case GauntletEvent.MouseMove:
				selectedWidget.OnMouseMove();
				this.SetHoveredView(selectedWidget);
				return;
			case GauntletEvent.MousePressed:
				this.LatestMouseDownWidget = selectedWidget;
				selectedWidget.OnMousePressed();
				if (this.FocusedWidget != selectedWidget)
				{
					if (this.FocusedWidget != null)
					{
						this.FocusedWidget.OnLoseFocus();
						Action loseFocus = this.LoseFocus;
						if (loseFocus != null)
						{
							loseFocus();
						}
					}
					if (selectedWidget.IsFocusable)
					{
						selectedWidget.OnGainFocus();
						this.FocusedWidget = selectedWidget;
						Action gainFocus = this.GainFocus;
						if (gainFocus != null)
						{
							gainFocus();
						}
					}
					else
					{
						this.FocusedWidget = null;
					}
					EditableTextWidget editableTextWidget;
					if ((editableTextWidget = (selectedWidget as EditableTextWidget)) != null && this.IsControllerActive)
					{
						string initialText = editableTextWidget.Text ?? string.Empty;
						string descriptionText = editableTextWidget.KeyboardInfoText ?? string.Empty;
						int maxLength = editableTextWidget.MaxLength;
						int keyboardTypeEnum = editableTextWidget.IsObfuscationEnabled ? 2 : 0;
						if (this.FocusedWidget is IntegerInputTextWidget || this.FocusedWidget is FloatInputTextWidget)
						{
							keyboardTypeEnum = 1;
						}
						this.Context.TwoDimensionContext.Platform.OpenOnScreenKeyboard(initialText, descriptionText, maxLength, keyboardTypeEnum);
						return;
					}
				}
				break;
			case GauntletEvent.MouseReleased:
				if (this.LatestMouseDownWidget != null && this.LatestMouseDownWidget != selectedWidget)
				{
					this.LatestMouseDownWidget.OnMouseReleased();
				}
				if (selectedWidget != null)
				{
					selectedWidget.OnMouseReleased();
					return;
				}
				break;
			case GauntletEvent.MouseAlternatePressed:
				this.LatestMouseAlternateDownWidget = selectedWidget;
				selectedWidget.OnMouseAlternatePressed();
				if (this.FocusedWidget != selectedWidget)
				{
					if (this.FocusedWidget != null)
					{
						this.FocusedWidget.OnLoseFocus();
						Action loseFocus2 = this.LoseFocus;
						if (loseFocus2 != null)
						{
							loseFocus2();
						}
					}
					if (selectedWidget.IsFocusable)
					{
						selectedWidget.OnGainFocus();
						this.FocusedWidget = selectedWidget;
						Action gainFocus2 = this.GainFocus;
						if (gainFocus2 != null)
						{
							gainFocus2();
						}
					}
					else
					{
						this.FocusedWidget = null;
					}
					EditableTextWidget editableTextWidget2;
					if ((editableTextWidget2 = (selectedWidget as EditableTextWidget)) != null && this.IsControllerActive)
					{
						string initialText2 = editableTextWidget2.Text ?? string.Empty;
						string descriptionText2 = editableTextWidget2.KeyboardInfoText ?? string.Empty;
						int maxLength2 = editableTextWidget2.MaxLength;
						int keyboardTypeEnum2 = editableTextWidget2.IsObfuscationEnabled ? 2 : 0;
						if (this.FocusedWidget is IntegerInputTextWidget || this.FocusedWidget is FloatInputTextWidget)
						{
							keyboardTypeEnum2 = 1;
						}
						this.Context.TwoDimensionContext.Platform.OpenOnScreenKeyboard(initialText2, descriptionText2, maxLength2, keyboardTypeEnum2);
						return;
					}
				}
				break;
			case GauntletEvent.MouseAlternateReleased:
				if (this.LatestMouseAlternateDownWidget != null && this.LatestMouseAlternateDownWidget != selectedWidget)
				{
					this.LatestMouseAlternateDownWidget.OnMouseAlternateReleased();
				}
				if (selectedWidget != null)
				{
					selectedWidget.OnMouseAlternateReleased();
					return;
				}
				break;
			case GauntletEvent.DragHover:
				this.SetDragHoveredView(selectedWidget);
				return;
			case GauntletEvent.DragBegin:
				selectedWidget.OnDragBegin();
				return;
			case GauntletEvent.DragEnd:
				selectedWidget.OnDragEnd();
				return;
			case GauntletEvent.Drop:
				selectedWidget.OnDrop();
				return;
			case GauntletEvent.MouseScroll:
				selectedWidget.OnMouseScroll();
				return;
			case GauntletEvent.RightStickMovement:
				selectedWidget.OnRightStickMovement();
				break;
			default:
				return;
			}
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000CC7E File Offset: 0x0000AE7E
		public static bool HitTest(Widget widget, Vector2 position)
		{
			if (widget == null)
			{
				Debug.FailedAssert("Calling HitTest using null widget!", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\GauntletUI\\TaleWorlds.GauntletUI\\EventManager.cs", "HitTest", 1141);
				return false;
			}
			return EventManager.AnyWidgetsAt(widget, position);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000CCA8 File Offset: 0x0000AEA8
		public bool FocusTest(Widget root)
		{
			for (Widget widget = this.FocusedWidget; widget != null; widget = widget.ParentWidget)
			{
				if (root == widget)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000CCD0 File Offset: 0x0000AED0
		private static bool AnyWidgetsAt(Widget widget, Vector2 position)
		{
			if (widget.IsEnabled && widget.IsVisible)
			{
				if (!widget.DoNotAcceptEvents && EventManager.IsPointInsideMeasuredArea(widget, position))
				{
					return true;
				}
				if (!widget.DoNotPassEventsToChildren)
				{
					for (int i = widget.ChildCount - 1; i >= 0; i--)
					{
						Widget child = widget.GetChild(i);
						if (!child.IsHidden && !child.IsDisabled && EventManager.AnyWidgetsAt(child, position))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000CD3F File Offset: 0x0000AF3F
		private static IEnumerable<Widget> AllEnabledWidgetsAt(Widget widget, Vector2 position)
		{
			if (widget.IsEnabled && widget.IsVisible)
			{
				if (!widget.DoNotPassEventsToChildren)
				{
					int num;
					for (int i = widget.ChildCount - 1; i >= 0; i = num - 1)
					{
						Widget child = widget.GetChild(i);
						if (!child.IsHidden && !child.IsDisabled && EventManager.IsPointInsideMeasuredArea(child, position))
						{
							foreach (Widget widget2 in EventManager.AllEnabledWidgetsAt(child, position))
							{
								yield return widget2;
							}
							IEnumerator<Widget> enumerator = null;
						}
						num = i;
					}
				}
				if (!widget.DoNotAcceptEvents && EventManager.IsPointInsideMeasuredArea(widget, position))
				{
					yield return widget;
				}
			}
			yield break;
			yield break;
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000CD56 File Offset: 0x0000AF56
		private static IEnumerable<Widget> AllVisibleWidgetsAt(Widget widget, Vector2 position)
		{
			if (widget.IsVisible)
			{
				int num;
				for (int i = widget.ChildCount - 1; i >= 0; i = num - 1)
				{
					Widget child = widget.GetChild(i);
					if (child.IsVisible && EventManager.IsPointInsideMeasuredArea(child, position))
					{
						foreach (Widget widget2 in EventManager.AllVisibleWidgetsAt(child, position))
						{
							yield return widget2;
						}
						IEnumerator<Widget> enumerator = null;
					}
					num = i;
				}
				if (EventManager.IsPointInsideMeasuredArea(widget, position))
				{
					yield return widget;
				}
			}
			yield break;
			yield break;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000CD70 File Offset: 0x0000AF70
		internal void ManualAddRange(List<Widget> list, LinkedList<Widget> linked_list)
		{
			if (list.Capacity < linked_list.Count)
			{
				list.Capacity = linked_list.Count;
			}
			for (LinkedListNode<Widget> linkedListNode = linked_list.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				list.Add(linkedListNode.Value);
			}
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000CDB8 File Offset: 0x0000AFB8
		private void ParallelUpdateWidget(int startInclusive, int endExclusive, float dt)
		{
			List<Widget> currentList = this._widgetsWithParallelUpdateContainer.GetCurrentList();
			for (int i = startInclusive; i < endExclusive; i++)
			{
				currentList[i].ParallelUpdate(dt);
			}
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000CDEA File Offset: 0x0000AFEA
		internal void ParallelUpdateWidgets(float dt)
		{
			TWParallel.For(0, this._widgetsWithParallelUpdateContainer.Count, dt, this.ParallelUpdateWidgetPredicate, 16);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000CE08 File Offset: 0x0000B008
		internal void Update(float dt)
		{
			this.Time += dt;
			this.CachedDt = dt;
			this.IsControllerActive = (Input.IsControllerConnected && !Input.IsMouseActive);
			int realCount = this._widgetsWithUpdateContainer.RealCount;
			int realCount2 = this._widgetsWithParallelUpdateContainer.RealCount;
			int realCount3 = this._widgetsWithLateUpdateContainer.RealCount;
			int num = MathF.Max(this._widgetsWithUpdateBrushesContainer.RealCount, MathF.Max(realCount, MathF.Max(realCount2, realCount3)));
			if (this._widgetsWithUpdateContainerDoDefragmentationDelegate == null)
			{
				this._widgetsWithUpdateContainerDoDefragmentationDelegate = delegate()
				{
					WidgetContainer widgetsWithUpdateContainer = this._widgetsWithUpdateContainer;
					lock (widgetsWithUpdateContainer)
					{
						this._widgetsWithUpdateContainer.DoDefragmentation();
					}
				};
				this._widgetsWithParallelUpdateContainerDoDefragmentationDelegate = delegate()
				{
					WidgetContainer widgetsWithParallelUpdateContainer = this._widgetsWithParallelUpdateContainer;
					lock (widgetsWithParallelUpdateContainer)
					{
						this._widgetsWithParallelUpdateContainer.DoDefragmentation();
					}
				};
				this._widgetsWithLateUpdateContainerDoDefragmentationDelegate = delegate()
				{
					WidgetContainer widgetsWithLateUpdateContainer = this._widgetsWithLateUpdateContainer;
					lock (widgetsWithLateUpdateContainer)
					{
						this._widgetsWithLateUpdateContainer.DoDefragmentation();
					}
				};
				this._widgetsWithUpdateBrushesContainerDoDefragmentationDelegate = delegate()
				{
					WidgetContainer widgetsWithUpdateBrushesContainer = this._widgetsWithUpdateBrushesContainer;
					lock (widgetsWithUpdateBrushesContainer)
					{
						this._widgetsWithUpdateBrushesContainer.DoDefragmentation();
					}
				};
			}
			bool flag = this._widgetsWithUpdateContainer.CheckFragmentation() || this._widgetsWithParallelUpdateContainer.CheckFragmentation() || this._widgetsWithLateUpdateContainer.CheckFragmentation() || this._widgetsWithUpdateBrushesContainer.CheckFragmentation();
			Task task = null;
			Task task2 = null;
			Task task3 = null;
			Task task4 = null;
			if (flag && num > 64)
			{
				task = Task.Run(this._widgetsWithUpdateContainerDoDefragmentationDelegate);
				task2 = Task.Run(this._widgetsWithParallelUpdateContainerDoDefragmentationDelegate);
				task3 = Task.Run(this._widgetsWithLateUpdateContainerDoDefragmentationDelegate);
				task4 = Task.Run(this._widgetsWithUpdateBrushesContainerDoDefragmentationDelegate);
			}
			this.UpdateDragCarrier();
			if (this._widgetsWithVisualDefinitionsContainer.CheckFragmentation())
			{
				WidgetContainer widgetsWithVisualDefinitionsContainer = this._widgetsWithVisualDefinitionsContainer;
				lock (widgetsWithVisualDefinitionsContainer)
				{
					this._widgetsWithVisualDefinitionsContainer.DoDefragmentation();
				}
			}
			List<Widget> currentList = this._widgetsWithVisualDefinitionsContainer.GetCurrentList();
			for (int i = 0; i < currentList.Count; i++)
			{
				currentList[i].UpdateVisualDefinitions(dt);
			}
			if (flag)
			{
				if (num > 64)
				{
					Task.WaitAll(new Task[]
					{
						task,
						task2,
						task3,
						task4
					});
				}
				else
				{
					this._widgetsWithUpdateContainerDoDefragmentationDelegate();
					this._widgetsWithParallelUpdateContainerDoDefragmentationDelegate();
					this._widgetsWithLateUpdateContainerDoDefragmentationDelegate();
					this._widgetsWithUpdateBrushesContainerDoDefragmentationDelegate();
				}
			}
			Widget hoveredView = this.HoveredView;
			UIContext.MouseCursors activeCursorOfContext = (((hoveredView != null) ? hoveredView.HoveredCursorState : null) != null) ? ((UIContext.MouseCursors)Enum.Parse(typeof(UIContext.MouseCursors), this.HoveredView.HoveredCursorState)) : UIContext.MouseCursors.Default;
			this.Context.ActiveCursorOfContext = activeCursorOfContext;
			List<Widget> currentList2 = this._widgetsWithUpdateContainer.GetCurrentList();
			for (int j = 0; j < currentList2.Count; j++)
			{
				currentList2[j].Update(dt);
			}
			this._doingParallelTask = true;
			if (this._widgetsWithParallelUpdateContainer.Count > 64)
			{
				this.ParallelUpdateWidgets(dt);
			}
			else
			{
				List<Widget> currentList3 = this._widgetsWithParallelUpdateContainer.GetCurrentList();
				for (int k = 0; k < currentList3.Count; k++)
				{
					currentList3[k].ParallelUpdate(dt);
				}
			}
			this._doingParallelTask = false;
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000D100 File Offset: 0x0000B300
		internal void ParallelUpdateBrushes(float dt)
		{
			TWParallel.For(0, this._widgetsWithUpdateBrushesContainer.Count, dt, this.UpdateBrushesWidgetPredicate, 16);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000D11C File Offset: 0x0000B31C
		internal void UpdateBrushes(float dt)
		{
			if (this._widgetsWithUpdateBrushesContainer.Count > 64)
			{
				this.ParallelUpdateBrushes(dt);
				return;
			}
			List<Widget> currentList = this._widgetsWithUpdateBrushesContainer.GetCurrentList();
			for (int i = 0; i < currentList.Count; i++)
			{
				currentList[i].UpdateBrushes(dt);
			}
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000D16C File Offset: 0x0000B36C
		private void UpdateBrushesWidget(int startInclusive, int endExclusive, float dt)
		{
			List<Widget> currentList = this._widgetsWithUpdateBrushesContainer.GetCurrentList();
			for (int i = startInclusive; i < endExclusive; i++)
			{
				currentList[i].UpdateBrushes(dt);
			}
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000D1A0 File Offset: 0x0000B3A0
		public void AddLateUpdateAction(Widget owner, Action<float> action, int order)
		{
			UpdateAction item = default(UpdateAction);
			item.Target = owner;
			item.Action = action;
			item.Order = order;
			if (this._doingParallelTask)
			{
				object lateUpdateActionLocker = this._lateUpdateActionLocker;
				lock (lateUpdateActionLocker)
				{
					this._lateUpdateActions[order].Add(item);
					return;
				}
			}
			this._lateUpdateActions[order].Add(item);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000D228 File Offset: 0x0000B428
		internal void LateUpdate(float dt)
		{
			List<Widget> currentList = this._widgetsWithLateUpdateContainer.GetCurrentList();
			for (int i = 0; i < currentList.Count; i++)
			{
				currentList[i].LateUpdate(dt);
			}
			Dictionary<int, List<UpdateAction>> lateUpdateActions = this._lateUpdateActions;
			this._lateUpdateActions = this._lateUpdateActionsRunning;
			this._lateUpdateActionsRunning = lateUpdateActions;
			for (int j = 1; j <= 5; j++)
			{
				List<UpdateAction> list = this._lateUpdateActionsRunning[j];
				foreach (UpdateAction updateAction in list)
				{
					updateAction.Action(dt);
				}
				list.Clear();
			}
			if (this.IsControllerActive)
			{
				if (this.HoveredView != null && this.HoveredView.IsRecursivelyVisible())
				{
					if (this.HoveredView.FrictionEnabled && this.DraggedWidget == null)
					{
						this._lastSetFrictionValue = 0.45f;
					}
					else
					{
						this._lastSetFrictionValue = 1f;
					}
					Input.SetCursorFriction(this._lastSetFrictionValue);
				}
				if (!this._lastSetFrictionValue.ApproximatelyEqualsTo(1f, 1E-05f) && this.HoveredView == null)
				{
					this._lastSetFrictionValue = 1f;
					Input.SetCursorFriction(this._lastSetFrictionValue);
				}
			}
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000D370 File Offset: 0x0000B570
		public void SetWidgetFocused(Widget widget, bool fromClick = false)
		{
			if (this.FocusedWidget != widget)
			{
				Widget focusedWidget = this.FocusedWidget;
				if (focusedWidget != null)
				{
					focusedWidget.OnLoseFocus();
				}
				if (widget != null)
				{
					widget.OnGainFocus();
				}
				this.FocusedWidget = widget;
				EditableTextWidget editableTextWidget;
				if ((editableTextWidget = (this.FocusedWidget as EditableTextWidget)) != null && this.IsControllerActive)
				{
					string initialText = editableTextWidget.Text ?? string.Empty;
					string descriptionText = editableTextWidget.KeyboardInfoText ?? string.Empty;
					int maxLength = editableTextWidget.MaxLength;
					int keyboardTypeEnum = editableTextWidget.IsObfuscationEnabled ? 2 : 0;
					if (this.FocusedWidget is IntegerInputTextWidget || this.FocusedWidget is FloatInputTextWidget)
					{
						keyboardTypeEnum = 1;
					}
					this.Context.TwoDimensionContext.Platform.OpenOnScreenKeyboard(initialText, descriptionText, maxLength, keyboardTypeEnum);
				}
			}
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000D430 File Offset: 0x0000B630
		private void UpdateDragCarrier()
		{
			if (this._dragCarrier != null)
			{
				this._dragCarrier.PosOffset = this.MousePositionInReferenceResolution + this._dragOffset - new Vector2(this.LeftUsableAreaStart, this.TopUsableAreaStart) * this.Context.InverseScale;
			}
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000D487 File Offset: 0x0000B687
		public void SetHoveredView(Widget view)
		{
			if (this.HoveredView != view)
			{
				if (this.HoveredView != null)
				{
					this.HoveredView.OnHoverEnd();
				}
				this.HoveredView = view;
				if (this.HoveredView != null)
				{
					this.HoveredView.OnHoverBegin();
				}
			}
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000D4C0 File Offset: 0x0000B6C0
		internal bool SetDragHoveredView(Widget view)
		{
			if (this.DragHoveredView != view)
			{
				Widget dragHoveredView = this.DragHoveredView;
				if (dragHoveredView != null)
				{
					dragHoveredView.OnDragHoverEnd();
				}
			}
			this.DragHoveredView = view;
			if (this.DragHoveredView != null && this.DragHoveredView.AcceptDrop)
			{
				this.DragHoveredView.OnDragHoverBegin();
				return true;
			}
			this.DragHoveredView = null;
			return false;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000D518 File Offset: 0x0000B718
		internal void BeginDragging(Widget draggedObject)
		{
			if (this.DraggedWidget != null)
			{
				Debug.FailedAssert("Trying to BeginDragging while there is already a dragged object.", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\GauntletUI\\TaleWorlds.GauntletUI\\EventManager.cs", "BeginDragging", 1628);
				this.ClearDragObject();
			}
			if (!draggedObject.ConnectedToRoot)
			{
				Debug.FailedAssert("Trying to drag a widget with no parent, possibly a widget which is already being dragged", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\GauntletUI\\TaleWorlds.GauntletUI\\EventManager.cs", "BeginDragging", 1634);
				return;
			}
			draggedObject.IsPressed = false;
			this._draggedWidgetPreviousParent = null;
			this._draggedWidgetIndex = -1;
			Widget parentWidget = draggedObject.ParentWidget;
			this.DraggedWidget = draggedObject;
			Vector2 globalPosition = this.DraggedWidget.GlobalPosition;
			this._dragCarrier = new DragCarrierWidget(this.Context);
			this._dragCarrier.ParentWidget = this.Root;
			if (draggedObject.DragWidget != null)
			{
				Widget dragWidget = draggedObject.DragWidget;
				this._dragCarrier.WidthSizePolicy = SizePolicy.CoverChildren;
				this._dragCarrier.HeightSizePolicy = SizePolicy.CoverChildren;
				this._dragOffset = Vector2.Zero;
				dragWidget.IsVisible = true;
				dragWidget.ParentWidget = this._dragCarrier;
				if (this.DraggedWidget.HideOnDrag)
				{
					this.DraggedWidget.IsVisible = false;
				}
				this._draggedWidgetPreviousParent = null;
			}
			else
			{
				this._dragOffset = (globalPosition - this.MousePosition) * this.Context.InverseScale;
				this._dragCarrier.WidthSizePolicy = SizePolicy.Fixed;
				this._dragCarrier.HeightSizePolicy = SizePolicy.Fixed;
				if (this.DraggedWidget.WidthSizePolicy == SizePolicy.StretchToParent)
				{
					this._dragCarrier.ScaledSuggestedWidth = this.DraggedWidget.Size.X + (this.DraggedWidget.MarginRight + this.DraggedWidget.MarginLeft) * this.Context.Scale;
					this._dragOffset += new Vector2(-this.DraggedWidget.MarginLeft, 0f);
				}
				else
				{
					this._dragCarrier.ScaledSuggestedWidth = this.DraggedWidget.Size.X;
				}
				if (this.DraggedWidget.HeightSizePolicy == SizePolicy.StretchToParent)
				{
					this._dragCarrier.ScaledSuggestedHeight = this.DraggedWidget.Size.Y + (this.DraggedWidget.MarginTop + this.DraggedWidget.MarginBottom) * this.Context.Scale;
					this._dragOffset += new Vector2(0f, -this.DraggedWidget.MarginTop);
				}
				else
				{
					this._dragCarrier.ScaledSuggestedHeight = this.DraggedWidget.Size.Y;
				}
				if (parentWidget != null)
				{
					this._draggedWidgetPreviousParent = parentWidget;
					this._draggedWidgetIndex = draggedObject.GetSiblingIndex();
				}
				this.DraggedWidget.ParentWidget = this._dragCarrier;
			}
			this._dragCarrier.PosOffset = this.MousePositionInReferenceResolution + this._dragOffset - new Vector2(this.LeftUsableAreaStart, this.TopUsableAreaStart) * this.Context.InverseScale;
			Action onDragStarted = this.OnDragStarted;
			if (onDragStarted == null)
			{
				return;
			}
			onDragStarted();
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000D7FC File Offset: 0x0000B9FC
		internal Widget ReleaseDraggedWidget()
		{
			Widget draggedWidget = this.DraggedWidget;
			if (this._draggedWidgetPreviousParent != null)
			{
				this.DraggedWidget.ParentWidget = this._draggedWidgetPreviousParent;
				this._draggedWidgetIndex = MathF.Max(0, MathF.Min(MathF.Max(0, this.DraggedWidget.ParentWidget.ChildCount - 1), this._draggedWidgetIndex));
				this.DraggedWidget.SetSiblingIndex(this._draggedWidgetIndex, false);
			}
			else
			{
				this.DraggedWidget.IsVisible = true;
			}
			this.SetDragHoveredView(null);
			return draggedWidget;
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000D87F File Offset: 0x0000BA7F
		internal void Render(TwoDimensionContext twoDimensionContext)
		{
			this._drawContext.Reset();
			this.Root.Render(twoDimensionContext, this._drawContext);
			this._drawContext.DrawTo(twoDimensionContext);
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000D8AA File Offset: 0x0000BAAA
		public void UpdateLayout()
		{
			this.SetMeasureDirty();
			this.SetLayoutDirty();
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000D8B8 File Offset: 0x0000BAB8
		internal void SetMeasureDirty()
		{
			this._measureDirty = 2;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000D8C1 File Offset: 0x0000BAC1
		internal void SetLayoutDirty()
		{
			this._layoutDirty = 2;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000D8CA File Offset: 0x0000BACA
		internal void SetPositionsDirty()
		{
			this._positionsDirty = true;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000D8D3 File Offset: 0x0000BAD3
		public bool GetIsHitThisFrame()
		{
			return this.OnGetIsHitThisFrame();
		}

		// Token: 0x0400011A RID: 282
		public const int MinParallelUpdateCount = 64;

		// Token: 0x0400011B RID: 283
		private const int DirtyCount = 2;

		// Token: 0x0400011C RID: 284
		private const float DragStartThreshold = 100f;

		// Token: 0x0400011D RID: 285
		private const float ScrollScale = 0.4f;

		// Token: 0x04000126 RID: 294
		private List<Action> _onAfterFinalizedCallbacks;

		// Token: 0x0400012A RID: 298
		private Widget _focusedWidget;

		// Token: 0x0400012B RID: 299
		private Widget _hoveredView;

		// Token: 0x0400012C RID: 300
		private List<Widget> _mouseOveredViews;

		// Token: 0x0400012D RID: 301
		private Widget _dragHoveredView;

		// Token: 0x0400012F RID: 303
		private Widget _latestMouseDownWidget;

		// Token: 0x04000130 RID: 304
		private Widget _latestMouseUpWidget;

		// Token: 0x04000131 RID: 305
		private Widget _latestMouseAlternateDownWidget;

		// Token: 0x04000132 RID: 306
		private Widget _latestMouseAlternateUpWidget;

		// Token: 0x04000134 RID: 308
		private int _measureDirty;

		// Token: 0x04000135 RID: 309
		private int _layoutDirty;

		// Token: 0x04000136 RID: 310
		private bool _positionsDirty;

		// Token: 0x04000137 RID: 311
		private const int _stickMovementScaleAmount = 3000;

		// Token: 0x04000139 RID: 313
		private Vector2 _lastClickPosition;

		// Token: 0x0400013A RID: 314
		private bool _mouseIsDown;

		// Token: 0x0400013B RID: 315
		private Vector2 _lastAlternateClickPosition;

		// Token: 0x0400013C RID: 316
		private bool _mouseAlternateIsDown;

		// Token: 0x0400013D RID: 317
		private Vector2 _dragOffset = new Vector2(0f, 0f);

		// Token: 0x0400013E RID: 318
		private Widget _draggedWidgetPreviousParent;

		// Token: 0x0400013F RID: 319
		private int _draggedWidgetIndex;

		// Token: 0x04000140 RID: 320
		private DragCarrierWidget _dragCarrier;

		// Token: 0x04000141 RID: 321
		private object _lateUpdateActionLocker;

		// Token: 0x04000142 RID: 322
		private Dictionary<int, List<UpdateAction>> _lateUpdateActions;

		// Token: 0x04000143 RID: 323
		private Dictionary<int, List<UpdateAction>> _lateUpdateActionsRunning;

		// Token: 0x04000144 RID: 324
		private WidgetContainer _widgetsWithUpdateContainer;

		// Token: 0x04000145 RID: 325
		private WidgetContainer _widgetsWithLateUpdateContainer;

		// Token: 0x04000146 RID: 326
		private WidgetContainer _widgetsWithParallelUpdateContainer;

		// Token: 0x04000147 RID: 327
		private WidgetContainer _widgetsWithVisualDefinitionsContainer;

		// Token: 0x04000148 RID: 328
		private WidgetContainer _widgetsWithTweenPositionsContainer;

		// Token: 0x04000149 RID: 329
		private WidgetContainer _widgetsWithUpdateBrushesContainer;

		// Token: 0x0400014A RID: 330
		private const int UpdateActionOrderCount = 5;

		// Token: 0x0400014B RID: 331
		private volatile bool _doingParallelTask;

		// Token: 0x0400014C RID: 332
		private TwoDimensionDrawContext _drawContext;

		// Token: 0x0400014D RID: 333
		private Action _widgetsWithUpdateContainerDoDefragmentationDelegate;

		// Token: 0x0400014E RID: 334
		private Action _widgetsWithParallelUpdateContainerDoDefragmentationDelegate;

		// Token: 0x0400014F RID: 335
		private Action _widgetsWithLateUpdateContainerDoDefragmentationDelegate;

		// Token: 0x04000150 RID: 336
		private Action _widgetsWithUpdateBrushesContainerDoDefragmentationDelegate;

		// Token: 0x04000151 RID: 337
		private readonly TWParallel.ParallelForWithDtAuxPredicate ParallelUpdateWidgetPredicate;

		// Token: 0x04000152 RID: 338
		private readonly TWParallel.ParallelForWithDtAuxPredicate UpdateBrushesWidgetPredicate;

		// Token: 0x04000153 RID: 339
		private readonly TWParallel.ParallelForWithDtAuxPredicate WidgetDoTweenPositionAuxPredicate;

		// Token: 0x04000154 RID: 340
		private float _lastSetFrictionValue = 1f;

		// Token: 0x04000157 RID: 343
		public Func<bool> OnGetIsHitThisFrame;
	}
}
