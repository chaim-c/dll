using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.ExtraWidgets;
using TaleWorlds.GauntletUI.GamepadNavigation;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Inventory
{
	// Token: 0x02000136 RID: 310
	public class InventoryScreenWidget : Widget
	{
		// Token: 0x06001037 RID: 4151 RVA: 0x0002CA13 File Offset: 0x0002AC13
		public InventoryScreenWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x0002CA2C File Offset: 0x0002AC2C
		private T IsWidgetChildOfType<T>(Widget currentWidget) where T : Widget
		{
			while (currentWidget != null)
			{
				if (currentWidget is T)
				{
					return (T)((object)currentWidget);
				}
				currentWidget = currentWidget.ParentWidget;
			}
			return default(T);
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x0002CA5E File Offset: 0x0002AC5E
		private bool IsWidgetChildOf(Widget parentWidget, Widget currentWidget)
		{
			while (currentWidget != null)
			{
				if (currentWidget == parentWidget)
				{
					return true;
				}
				currentWidget = currentWidget.ParentWidget;
			}
			return false;
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x0002CA74 File Offset: 0x0002AC74
		private bool IsWidgetChildOfId(string parentId, Widget currentWidget)
		{
			while (currentWidget != null)
			{
				if (currentWidget.Id == parentId)
				{
					return true;
				}
				currentWidget = currentWidget.ParentWidget;
			}
			return false;
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x0002CA94 File Offset: 0x0002AC94
		private InventoryListPanel GetCurrentHoveredListPanel()
		{
			for (int i = 0; i < base.EventManager.MouseOveredViews.Count; i++)
			{
				InventoryListPanel result;
				if ((result = (base.EventManager.MouseOveredViews[i] as InventoryListPanel)) != null)
				{
					return result;
				}
			}
			return null;
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x0002CAD9 File Offset: 0x0002ACD9
		private Widget GetFirstBannerItem()
		{
			ListPanel listPanel = this.OtherInventoryListWidget.InnerPanel as ListPanel;
			ListPanel listPanel2 = ((listPanel != null) ? listPanel.GetChild(0) : null) as ListPanel;
			if (listPanel2 == null)
			{
				return null;
			}
			return listPanel2.FindChild((Widget x) => (x as InventoryItemTupleWidget).ItemType == this.BannerTypeCode);
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x0002CB14 File Offset: 0x0002AD14
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			if (!this._eventsRegistered)
			{
				ListPanel listPanel = this.OtherInventoryListWidget.InnerPanel as ListPanel;
				ListPanel listPanel2 = ((listPanel != null) ? listPanel.GetChild(0) : null) as ListPanel;
				if (listPanel2 != null)
				{
					listPanel2.ItemAddEventHandlers.Add(new Action<Widget, Widget>(this.OnNewInventoryItemAdded));
				}
				ListPanel listPanel3 = this.PlayerInventoryListWidget.InnerPanel as ListPanel;
				ListPanel listPanel4 = ((listPanel3 != null) ? listPanel3.GetChild(0) : null) as ListPanel;
				if (listPanel4 != null)
				{
					listPanel4.ItemAddEventHandlers.Add(new Action<Widget, Widget>(this.OnNewInventoryItemAdded));
				}
				this._eventsRegistered = true;
			}
			if (base.EventManager.DraggedWidget == null)
			{
				this.TargetEquipmentIndex = -1;
				this._currentDraggedItemWidget = null;
			}
			Widget latestMouseDownWidget = base.EventManager.LatestMouseDownWidget;
			bool flag;
			if (latestMouseDownWidget != null)
			{
				if (!(latestMouseDownWidget is InventoryItemButtonWidget))
				{
					InventoryEquippedItemControlsBrushWidget equippedItemControls = this.EquippedItemControls;
					if (equippedItemControls == null || !equippedItemControls.CheckIsMyChildRecursive(latestMouseDownWidget))
					{
						InventoryItemButtonWidget currentSelectedItemWidget = this._currentSelectedItemWidget;
						if (currentSelectedItemWidget == null || !currentSelectedItemWidget.CheckIsMyChildRecursive(latestMouseDownWidget))
						{
							InventoryItemButtonWidget currentSelectedOtherItemWidget = this._currentSelectedOtherItemWidget;
							flag = (currentSelectedOtherItemWidget != null && currentSelectedOtherItemWidget.CheckIsMyChildRecursive(latestMouseDownWidget));
							goto IL_10A;
						}
					}
				}
				flag = true;
			}
			else
			{
				flag = false;
			}
			IL_10A:
			bool flag2 = flag;
			bool flag3 = this.IsWidgetChildOf(this.InventoryTooltip, latestMouseDownWidget);
			if (latestMouseDownWidget == null || (this._currentSelectedItemWidget != null && !flag2 && !flag3 && !this.ItemPreviewWidget.IsVisible))
			{
				this.SetCurrentTuple(null, false);
			}
			Widget hoveredView = base.EventManager.HoveredView;
			if (hoveredView != null)
			{
				InventoryItemButtonWidget inventoryItemButtonWidget = this.IsWidgetChildOfType<InventoryItemButtonWidget>(hoveredView);
				bool flag4 = this.IsWidgetChildOfId("InventoryTooltip", hoveredView);
				if (inventoryItemButtonWidget != null)
				{
					this.ItemWidgetHoverBegin(inventoryItemButtonWidget);
				}
				else if (flag4 && GauntletGamepadNavigationManager.Instance.IsCursorMovingForNavigation)
				{
					this.ItemWidgetHoverEnd(null);
				}
				else if (!flag4 && hoveredView.ParentWidget != null)
				{
					this.ItemWidgetHoverEnd(null);
				}
			}
			else
			{
				this.ItemWidgetHoverEnd(null);
			}
			this.UpdateControllerTransferKeyVisuals();
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x0002CCD0 File Offset: 0x0002AED0
		private void UpdateControllerTransferKeyVisuals()
		{
			InventoryListPanel currentHoveredListPanel = this.GetCurrentHoveredListPanel();
			this.IsFocusedOnItemList = (currentHoveredListPanel != null);
			if (!base.EventManager.IsControllerActive || !this.IsFocusedOnItemList)
			{
				this.PreviousCharacterInputVisualParent.IsVisible = true;
				this.NextCharacterInputVisualParent.IsVisible = true;
				this.TransferInputKeyVisualWidget.IsVisible = false;
				return;
			}
			this.PreviousCharacterInputVisualParent.IsVisible = false;
			this.NextCharacterInputVisualParent.IsVisible = false;
			InventoryItemTupleWidget inventoryItemTupleWidget;
			if ((inventoryItemTupleWidget = (this._currentHoveredItemWidget as InventoryItemTupleWidget)) != null && inventoryItemTupleWidget.IsHovered && inventoryItemTupleWidget.IsTransferable)
			{
				this.TransferInputKeyVisualWidget.IsVisible = true;
				Vector2 vector;
				if (inventoryItemTupleWidget.IsRightSide)
				{
					InputKeyVisualWidget transferInputKeyVisualWidget = this.TransferInputKeyVisualWidget;
					InputKeyVisualWidget nextCharacterInputKeyVisual = this._nextCharacterInputKeyVisual;
					transferInputKeyVisualWidget.KeyID = (((nextCharacterInputKeyVisual != null) ? nextCharacterInputKeyVisual.KeyID : null) ?? "");
					vector = this._currentHoveredItemWidget.GlobalPosition - new Vector2(base.EventManager.LeftUsableAreaStart, base.EventManager.TopUsableAreaStart + 20f * base._scaleToUse);
				}
				else
				{
					InputKeyVisualWidget transferInputKeyVisualWidget2 = this.TransferInputKeyVisualWidget;
					InputKeyVisualWidget previousCharacterInputKeyVisual = this._previousCharacterInputKeyVisual;
					transferInputKeyVisualWidget2.KeyID = (((previousCharacterInputKeyVisual != null) ? previousCharacterInputKeyVisual.KeyID : null) ?? "");
					vector = this._currentHoveredItemWidget.GlobalPosition - new Vector2(base.EventManager.LeftUsableAreaStart + 60f * base._scaleToUse - this._currentHoveredItemWidget.Size.X, base.EventManager.TopUsableAreaStart + 20f * base._scaleToUse);
				}
				this.TransferInputKeyVisualWidget.ScaledPositionXOffset = vector.X;
				this.TransferInputKeyVisualWidget.ScaledPositionYOffset = vector.Y;
				return;
			}
			this.TransferInputKeyVisualWidget.IsVisible = false;
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x0002CE90 File Offset: 0x0002B090
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this._newAddedItem != null)
			{
				string itemID = this._newAddedItem.ItemID;
				InventoryItemTupleWidget inventoryItemTupleWidget = this._currentSelectedItemWidget as InventoryItemTupleWidget;
				if (itemID == ((inventoryItemTupleWidget != null) ? inventoryItemTupleWidget.ItemID : null))
				{
					this._currentSelectedOtherItemWidget = this._newAddedItem;
					(this._currentSelectedOtherItemWidget as InventoryItemTupleWidget).TransferRequestHandlers.Add(new Action<InventoryItemTupleWidget>(this.OnTransferItemRequested));
					this._newAddedItem.IsSelected = true;
					this.UpdateScrollTarget(this._newAddedItem.IsRightSide);
				}
				this._newAddedItem = null;
			}
			if (this._scrollToBannersInFrames > -1)
			{
				if (this._scrollToBannersInFrames == 0)
				{
					this.OtherInventoryListWidget.ScrollToChild(this.GetFirstBannerItem(), -1f, 0.2f, 0, 0, 0.35f, 0f);
				}
				this._scrollToBannersInFrames--;
			}
			if (this._focusLostThisFrame)
			{
				base.EventFired("OnFocusLose", Array.Empty<object>());
				this._focusLostThisFrame = false;
			}
			this.UpdateTooltipPosition();
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x0002CF94 File Offset: 0x0002B194
		private void UpdateTooltipPosition()
		{
			if (base.EventManager.DraggedWidget != null)
			{
				this.InventoryTooltip.IsHidden = true;
			}
			InventoryItemButtonWidget currentHoveredItemWidget = this._currentHoveredItemWidget;
			if (((currentHoveredItemWidget != null) ? currentHoveredItemWidget.ParentWidget : null) == null)
			{
				this._lastDisplayedTooltipItem = null;
				return;
			}
			if (this._tooltipHiddenFrameCount < this.TooltipHideFrameLength)
			{
				this._tooltipHiddenFrameCount++;
				this.InventoryTooltip.PositionXOffset = 5000f;
				this.InventoryTooltip.PositionYOffset = 5000f;
				return;
			}
			if (this._currentHoveredItemWidget.IsRightSide)
			{
				this.InventoryTooltip.ScaledPositionXOffset = this._currentHoveredItemWidget.ParentWidget.GlobalPosition.X - this.InventoryTooltip.Size.X + 10f * base._scaleToUse - base.EventManager.LeftUsableAreaStart;
			}
			else
			{
				this.InventoryTooltip.ScaledPositionXOffset = this._currentHoveredItemWidget.ParentWidget.GlobalPosition.X + this._currentHoveredItemWidget.ParentWidget.Size.X - 10f * base._scaleToUse - base.EventManager.LeftUsableAreaStart;
			}
			float max = base.EventManager.PageSize.Y - this.InventoryTooltip.MeasuredSize.Y;
			this.InventoryTooltip.ScaledPositionYOffset = Mathf.Clamp(this._currentHoveredItemWidget.GlobalPosition.Y - base.EventManager.TopUsableAreaStart, 0f, max);
			this._lastDisplayedTooltipItem = this._currentHoveredItemWidget;
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x0002D120 File Offset: 0x0002B320
		public void SetCurrentTuple(InventoryItemButtonWidget itemWidget, bool isLeftSide)
		{
			this._focusLostThisFrame = (itemWidget == null);
			if (this._currentSelectedItemWidget != null && this._currentSelectedItemWidget != itemWidget)
			{
				this._currentSelectedItemWidget.IsSelected = false;
				InventoryItemTupleWidget inventoryItemTupleWidget;
				if ((inventoryItemTupleWidget = (this._currentSelectedItemWidget as InventoryItemTupleWidget)) != null)
				{
					inventoryItemTupleWidget.TransferRequestHandlers.Remove(new Action<InventoryItemTupleWidget>(this.OnTransferItemRequested));
				}
				if (this._currentSelectedOtherItemWidget != null)
				{
					this._currentSelectedOtherItemWidget.IsSelected = false;
				}
			}
			InventoryItemTupleWidget inventoryItemTupleWidget2;
			InventoryItemTupleWidget inventoryItemTupleWidget3;
			if (itemWidget == null || ((inventoryItemTupleWidget2 = (itemWidget as InventoryItemTupleWidget)) != null && (inventoryItemTupleWidget3 = (this._currentSelectedOtherItemWidget as InventoryItemTupleWidget)) != null && inventoryItemTupleWidget2.ItemID == inventoryItemTupleWidget3.ItemID))
			{
				this._equippedItemControls.HidePanel();
				if (this._currentSelectedItemWidget != null)
				{
					this._currentSelectedItemWidget.IsSelected = false;
				}
				this._currentSelectedItemWidget = null;
				if (this._currentSelectedOtherItemWidget != null)
				{
					this._currentSelectedOtherItemWidget.IsSelected = false;
					(this._currentSelectedOtherItemWidget as InventoryItemTupleWidget).TransferRequestHandlers.Remove(new Action<InventoryItemTupleWidget>(this.OnTransferItemRequested));
				}
				this._currentSelectedOtherItemWidget = null;
				return;
			}
			if (this._currentSelectedItemWidget == itemWidget)
			{
				this.SetCurrentTuple(null, false);
				if (this._currentSelectedOtherItemWidget != null)
				{
					this._currentSelectedOtherItemWidget.IsSelected = false;
				}
				this._currentSelectedOtherItemWidget = null;
				return;
			}
			this._currentSelectedItemWidget = itemWidget;
			this.TargetEquipmentIndex = -1;
			this.TransactionCount = 1;
			if (this._currentSelectedItemWidget is InventoryEquippedItemSlotWidget)
			{
				this._equippedItemControls.ShowPanel(itemWidget);
				this._currentSelectedOtherItemWidget = null;
			}
			else
			{
				this._equippedItemControls.HidePanel();
				InventoryItemTupleWidget inventoryItemTupleWidget4;
				if ((inventoryItemTupleWidget4 = (this._currentSelectedItemWidget as InventoryItemTupleWidget)) != null)
				{
					inventoryItemTupleWidget4.TransferRequestHandlers.Add(new Action<InventoryItemTupleWidget>(this.OnTransferItemRequested));
					if (isLeftSide)
					{
						using (IEnumerator<Widget> enumerator = this.PlayerInventoryListWidget.AllChildren.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								InventoryItemTupleWidget inventoryItemTupleWidget5;
								if ((inventoryItemTupleWidget5 = (enumerator.Current as InventoryItemTupleWidget)) != null && inventoryItemTupleWidget5.ItemID == inventoryItemTupleWidget4.ItemID)
								{
									this._currentSelectedOtherItemWidget = inventoryItemTupleWidget5;
									this._currentSelectedOtherItemWidget.IsSelected = true;
									(this._currentSelectedOtherItemWidget as InventoryItemTupleWidget).TransferRequestHandlers.Add(new Action<InventoryItemTupleWidget>(this.OnTransferItemRequested));
									break;
								}
							}
							goto IL_2AD;
						}
					}
					using (IEnumerator<Widget> enumerator = this.OtherInventoryListWidget.AllChildren.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							InventoryItemTupleWidget inventoryItemTupleWidget6;
							if ((inventoryItemTupleWidget6 = (enumerator.Current as InventoryItemTupleWidget)) != null && inventoryItemTupleWidget6.ItemID == inventoryItemTupleWidget4.ItemID)
							{
								this._currentSelectedOtherItemWidget = inventoryItemTupleWidget6;
								this._currentSelectedOtherItemWidget.IsSelected = true;
								(this._currentSelectedOtherItemWidget as InventoryItemTupleWidget).TransferRequestHandlers.Add(new Action<InventoryItemTupleWidget>(this.OnTransferItemRequested));
								break;
							}
						}
					}
				}
			}
			IL_2AD:
			this.UpdateScrollTarget(isLeftSide);
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x0002D400 File Offset: 0x0002B600
		private void OnEquipmentControlsHidden()
		{
			this._currentSelectedItemWidget = null;
			if (this._currentSelectedOtherItemWidget != null)
			{
				this._currentSelectedOtherItemWidget.IsSelected = false;
				(this._currentSelectedOtherItemWidget as InventoryItemTupleWidget).TransferRequestHandlers.Remove(new Action<InventoryItemTupleWidget>(this.OnTransferItemRequested));
			}
			this._currentSelectedOtherItemWidget = null;
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x0002D451 File Offset: 0x0002B651
		private void OnTransferItemRequested(InventoryItemTupleWidget owner)
		{
			this.UpdateScrollTarget(!owner.IsRightSide);
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x0002D462 File Offset: 0x0002B662
		private void TradeLabelOnPropertyChanged(PropertyOwnerObject owner, string propertyName, object value)
		{
			if (propertyName == "Text")
			{
				this.TradeLabel.IsDisabled = string.IsNullOrEmpty(this.TradeLabel.Text);
			}
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x0002D48C File Offset: 0x0002B68C
		private void EquippedItemControlsOnPreviewClick(Widget itemwidget)
		{
			this.ItemPreviewWidget.SetLastFocusedItem(null);
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x0002D49C File Offset: 0x0002B69C
		private void ItemWidgetHoverBegin(InventoryItemButtonWidget itemWidget)
		{
			if (this._currentHoveredItemWidget != itemWidget)
			{
				this._currentHoveredItemWidget = itemWidget;
				this._tooltipHiddenFrameCount = 0;
				Widget widget = this.InventoryTooltip.FindChild("TargetItemTooltip");
				if (this._currentHoveredItemWidget.IsRightSide)
				{
					widget.SetSiblingIndex(1, false);
				}
				else
				{
					widget.SetSiblingIndex(0, false);
				}
				this.InventoryTooltip.IsHidden = false;
				base.EventFired("ItemHoverBegin", new object[]
				{
					itemWidget
				});
			}
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x0002D511 File Offset: 0x0002B711
		private void ItemWidgetHoverEnd(InventoryItemButtonWidget itemWidget)
		{
			if (this._currentHoveredItemWidget != null && itemWidget == null)
			{
				this._currentHoveredItemWidget = null;
				this.InventoryTooltip.IsHidden = true;
				base.EventFired("ItemHoverEnd", Array.Empty<object>());
			}
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x0002D544 File Offset: 0x0002B744
		public void ItemWidgetDragBegin(InventoryItemButtonWidget itemWidget)
		{
			InventoryEquippedItemControlsBrushWidget equippedItemControls = this.EquippedItemControls;
			if (equippedItemControls != null)
			{
				equippedItemControls.HidePanel();
			}
			this._currentDraggedItemWidget = itemWidget;
			InventoryEquippedItemSlotWidget inventoryEquippedItemSlotWidget = itemWidget as InventoryEquippedItemSlotWidget;
			if (inventoryEquippedItemSlotWidget != null)
			{
				this.TargetEquipmentIndex = inventoryEquippedItemSlotWidget.TargetEquipmentIndex;
				return;
			}
			this.TargetEquipmentIndex = itemWidget.EquipmentIndex;
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x0002D58C File Offset: 0x0002B78C
		public void ItemWidgetDrop(InventoryItemButtonWidget itemWidget)
		{
			if (this._currentDraggedItemWidget == itemWidget)
			{
				this._currentDraggedItemWidget = null;
				this.TargetEquipmentIndex = -1;
			}
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x0002D5A8 File Offset: 0x0002B7A8
		private void OtherInventoryGoldTextOnPropertyChanged(PropertyOwnerObject owner, string propertyName, int value)
		{
			if (propertyName == "IntText")
			{
				bool isVisible = this.OtherInventoryGoldText.IntText > 0;
				this.OtherInventoryGoldText.IsVisible = isVisible;
				this.OtherInventoryGoldImage.IsVisible = isVisible;
			}
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x0002D5EC File Offset: 0x0002B7EC
		private void UpdateScrollTarget(bool isLeftSide)
		{
			if (this._currentSelectedOtherItemWidget != null)
			{
				if (isLeftSide)
				{
					this.PlayerInventoryListWidget.ScrollToChild(this._currentSelectedOtherItemWidget, -1f, 1f, 0, 400, 0.35f, 0f);
					return;
				}
				this.OtherInventoryListWidget.ScrollToChild(this._currentSelectedOtherItemWidget, -1f, 1f, 0, 400, 0.35f, 0f);
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x0600104C RID: 4172 RVA: 0x0002D65B File Offset: 0x0002B85B
		// (set) Token: 0x0600104D RID: 4173 RVA: 0x0002D663 File Offset: 0x0002B863
		[Editor(false)]
		public InputKeyVisualWidget TransferInputKeyVisualWidget
		{
			get
			{
				return this._transferInputKeyVisualWidget;
			}
			set
			{
				if (this._transferInputKeyVisualWidget != value)
				{
					this._transferInputKeyVisualWidget = value;
					base.OnPropertyChanged<InputKeyVisualWidget>(value, "TransferInputKeyVisualWidget");
				}
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x0600104E RID: 4174 RVA: 0x0002D681 File Offset: 0x0002B881
		// (set) Token: 0x0600104F RID: 4175 RVA: 0x0002D68C File Offset: 0x0002B88C
		public Widget PreviousCharacterInputVisualParent
		{
			get
			{
				return this._previousCharacterInputVisualParent;
			}
			set
			{
				if (value != this._previousCharacterInputVisualParent)
				{
					this._previousCharacterInputVisualParent = value;
					if (this._previousCharacterInputVisualParent != null)
					{
						this._previousCharacterInputKeyVisual = (this._previousCharacterInputVisualParent.Children.FirstOrDefault((Widget x) => x is InputKeyVisualWidget) as InputKeyVisualWidget);
					}
				}
			}
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06001050 RID: 4176 RVA: 0x0002D6EB File Offset: 0x0002B8EB
		// (set) Token: 0x06001051 RID: 4177 RVA: 0x0002D6F4 File Offset: 0x0002B8F4
		public Widget NextCharacterInputVisualParent
		{
			get
			{
				return this._nextCharacterInputVisualParent;
			}
			set
			{
				if (value != this._nextCharacterInputVisualParent)
				{
					this._nextCharacterInputVisualParent = value;
					if (this._nextCharacterInputVisualParent != null)
					{
						this._nextCharacterInputKeyVisual = (this._nextCharacterInputVisualParent.Children.FirstOrDefault((Widget x) => x is InputKeyVisualWidget) as InputKeyVisualWidget);
					}
				}
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06001052 RID: 4178 RVA: 0x0002D753 File Offset: 0x0002B953
		// (set) Token: 0x06001053 RID: 4179 RVA: 0x0002D75C File Offset: 0x0002B95C
		[Editor(false)]
		public RichTextWidget TradeLabel
		{
			get
			{
				return this._tradeLabel;
			}
			set
			{
				if (this._tradeLabel != value)
				{
					if (this._tradeLabel != null)
					{
						this._tradeLabel.PropertyChanged -= this.TradeLabelOnPropertyChanged;
					}
					this._tradeLabel = value;
					if (this._tradeLabel != null)
					{
						this._tradeLabel.PropertyChanged += this.TradeLabelOnPropertyChanged;
					}
					base.OnPropertyChanged<RichTextWidget>(value, "TradeLabel");
				}
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06001054 RID: 4180 RVA: 0x0002D7C3 File Offset: 0x0002B9C3
		// (set) Token: 0x06001055 RID: 4181 RVA: 0x0002D7CC File Offset: 0x0002B9CC
		[Editor(false)]
		public InventoryEquippedItemControlsBrushWidget EquippedItemControls
		{
			get
			{
				return this._equippedItemControls;
			}
			set
			{
				if (this._equippedItemControls != value)
				{
					if (this._equippedItemControls != null)
					{
						this._equippedItemControls.OnPreviewClick -= this.EquippedItemControlsOnPreviewClick;
						this._equippedItemControls.OnHidePanel -= this.OnEquipmentControlsHidden;
					}
					this._equippedItemControls = value;
					if (this._equippedItemControls != null)
					{
						this._equippedItemControls.OnPreviewClick += this.EquippedItemControlsOnPreviewClick;
						this._equippedItemControls.OnHidePanel += this.OnEquipmentControlsHidden;
					}
					base.OnPropertyChanged<InventoryEquippedItemControlsBrushWidget>(value, "EquippedItemControls");
				}
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06001056 RID: 4182 RVA: 0x0002D861 File Offset: 0x0002BA61
		// (set) Token: 0x06001057 RID: 4183 RVA: 0x0002D869 File Offset: 0x0002BA69
		[Editor(false)]
		public Widget InventoryTooltip
		{
			get
			{
				return this._inventoryTooltip;
			}
			set
			{
				if (this._inventoryTooltip != value)
				{
					this._inventoryTooltip = value;
					base.OnPropertyChanged<Widget>(value, "InventoryTooltip");
				}
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06001058 RID: 4184 RVA: 0x0002D887 File Offset: 0x0002BA87
		// (set) Token: 0x06001059 RID: 4185 RVA: 0x0002D88F File Offset: 0x0002BA8F
		[Editor(false)]
		public InventoryItemPreviewWidget ItemPreviewWidget
		{
			get
			{
				return this._itemPreviewWidget;
			}
			set
			{
				if (this._itemPreviewWidget != value)
				{
					this._itemPreviewWidget = value;
					base.OnPropertyChanged<InventoryItemPreviewWidget>(value, "ItemPreviewWidget");
				}
			}
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x0600105A RID: 4186 RVA: 0x0002D8AD File Offset: 0x0002BAAD
		// (set) Token: 0x0600105B RID: 4187 RVA: 0x0002D8B5 File Offset: 0x0002BAB5
		[Editor(false)]
		public int TransactionCount
		{
			get
			{
				return this._transactionCount;
			}
			set
			{
				if (this._transactionCount != value)
				{
					this._transactionCount = value;
					base.OnPropertyChanged(value, "TransactionCount");
				}
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x0600105C RID: 4188 RVA: 0x0002D8D3 File Offset: 0x0002BAD3
		// (set) Token: 0x0600105D RID: 4189 RVA: 0x0002D8DB File Offset: 0x0002BADB
		[Editor(false)]
		public bool IsInWarSet
		{
			get
			{
				return this._isInWarSet;
			}
			set
			{
				if (this._isInWarSet != value)
				{
					this._isInWarSet = value;
					base.OnPropertyChanged(value, "IsInWarSet");
				}
			}
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x0600105E RID: 4190 RVA: 0x0002D8F9 File Offset: 0x0002BAF9
		// (set) Token: 0x0600105F RID: 4191 RVA: 0x0002D901 File Offset: 0x0002BB01
		[Editor(false)]
		public int TargetEquipmentIndex
		{
			get
			{
				return this._targetEquipmentIndex;
			}
			set
			{
				if (this._targetEquipmentIndex != value)
				{
					this._targetEquipmentIndex = value;
					base.OnPropertyChanged(value, "TargetEquipmentIndex");
				}
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06001060 RID: 4192 RVA: 0x0002D91F File Offset: 0x0002BB1F
		// (set) Token: 0x06001061 RID: 4193 RVA: 0x0002D928 File Offset: 0x0002BB28
		[Editor(false)]
		public TextWidget OtherInventoryGoldText
		{
			get
			{
				return this._otherInventoryGoldText;
			}
			set
			{
				if (value != this._otherInventoryGoldText)
				{
					if (this._otherInventoryGoldText != null)
					{
						this._otherInventoryGoldText.intPropertyChanged -= this.OtherInventoryGoldTextOnPropertyChanged;
					}
					this._otherInventoryGoldText = value;
					if (this._otherInventoryGoldText != null)
					{
						this._otherInventoryGoldText.intPropertyChanged += this.OtherInventoryGoldTextOnPropertyChanged;
					}
					base.OnPropertyChanged<TextWidget>(value, "OtherInventoryGoldText");
				}
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06001062 RID: 4194 RVA: 0x0002D98F File Offset: 0x0002BB8F
		// (set) Token: 0x06001063 RID: 4195 RVA: 0x0002D997 File Offset: 0x0002BB97
		[Editor(false)]
		public Widget OtherInventoryGoldImage
		{
			get
			{
				return this._otherInventoryGoldImage;
			}
			set
			{
				if (value != this._otherInventoryGoldImage)
				{
					this._otherInventoryGoldImage = value;
					base.OnPropertyChanged<Widget>(value, "OtherInventoryGoldImage");
				}
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06001064 RID: 4196 RVA: 0x0002D9B5 File Offset: 0x0002BBB5
		// (set) Token: 0x06001065 RID: 4197 RVA: 0x0002D9BD File Offset: 0x0002BBBD
		[Editor(false)]
		public ScrollablePanel OtherInventoryListWidget
		{
			get
			{
				return this._otherInventoryListWidget;
			}
			set
			{
				if (value != this._otherInventoryListWidget)
				{
					this._otherInventoryListWidget = value;
					base.OnPropertyChanged<ScrollablePanel>(value, "OtherInventoryListWidget");
				}
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06001066 RID: 4198 RVA: 0x0002D9DB File Offset: 0x0002BBDB
		// (set) Token: 0x06001067 RID: 4199 RVA: 0x0002D9E3 File Offset: 0x0002BBE3
		[Editor(false)]
		public ScrollablePanel PlayerInventoryListWidget
		{
			get
			{
				return this._playerInventoryListWidget;
			}
			set
			{
				if (value != this._playerInventoryListWidget)
				{
					this._playerInventoryListWidget = value;
					base.OnPropertyChanged<ScrollablePanel>(value, "PlayerInventoryListWidget");
				}
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06001068 RID: 4200 RVA: 0x0002DA01 File Offset: 0x0002BC01
		// (set) Token: 0x06001069 RID: 4201 RVA: 0x0002DA09 File Offset: 0x0002BC09
		[Editor(false)]
		public bool IsFocusedOnItemList
		{
			get
			{
				return this._isFocusedOnItemList;
			}
			set
			{
				if (value != this._isFocusedOnItemList)
				{
					this._isFocusedOnItemList = value;
					base.OnPropertyChanged(value, "IsFocusedOnItemList");
				}
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x0600106A RID: 4202 RVA: 0x0002DA27 File Offset: 0x0002BC27
		// (set) Token: 0x0600106B RID: 4203 RVA: 0x0002DA2F File Offset: 0x0002BC2F
		[Editor(false)]
		public bool IsBannerTutorialActive
		{
			get
			{
				return this._isBannerTutorialActive;
			}
			set
			{
				if (value != this._isBannerTutorialActive)
				{
					this._isBannerTutorialActive = value;
					base.OnPropertyChanged(value, "IsBannerTutorialActive");
					if (value)
					{
						this._scrollToBannersInFrames = 1;
					}
				}
			}
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x0600106C RID: 4204 RVA: 0x0002DA57 File Offset: 0x0002BC57
		// (set) Token: 0x0600106D RID: 4205 RVA: 0x0002DA5F File Offset: 0x0002BC5F
		[Editor(false)]
		public int BannerTypeCode
		{
			get
			{
				return this._bannerTypeCode;
			}
			set
			{
				if (value != this._bannerTypeCode)
				{
					this._bannerTypeCode = value;
					base.OnPropertyChanged(value, "BannerTypeCode");
				}
			}
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x0002DA80 File Offset: 0x0002BC80
		private void OnNewInventoryItemAdded(Widget parentWidget, Widget addedWidget)
		{
			InventoryItemTupleWidget newAddedItem;
			if (this._currentSelectedItemWidget != null && (newAddedItem = (addedWidget as InventoryItemTupleWidget)) != null)
			{
				this._newAddedItem = newAddedItem;
			}
		}

		// Token: 0x0400075F RID: 1887
		private readonly int TooltipHideFrameLength = 2;

		// Token: 0x04000760 RID: 1888
		private InventoryItemButtonWidget _currentSelectedItemWidget;

		// Token: 0x04000761 RID: 1889
		private InventoryItemButtonWidget _currentSelectedOtherItemWidget;

		// Token: 0x04000762 RID: 1890
		private InventoryItemButtonWidget _currentHoveredItemWidget;

		// Token: 0x04000763 RID: 1891
		private InventoryItemButtonWidget _currentDraggedItemWidget;

		// Token: 0x04000764 RID: 1892
		private InventoryItemButtonWidget _lastDisplayedTooltipItem;

		// Token: 0x04000765 RID: 1893
		private int _tooltipHiddenFrameCount;

		// Token: 0x04000766 RID: 1894
		private bool _eventsRegistered;

		// Token: 0x04000767 RID: 1895
		private int _scrollToBannersInFrames = -1;

		// Token: 0x04000768 RID: 1896
		private InputKeyVisualWidget _previousCharacterInputKeyVisual;

		// Token: 0x04000769 RID: 1897
		private InputKeyVisualWidget _nextCharacterInputKeyVisual;

		// Token: 0x0400076A RID: 1898
		private InventoryItemTupleWidget _newAddedItem;

		// Token: 0x0400076B RID: 1899
		private Widget _previousCharacterInputVisualParent;

		// Token: 0x0400076C RID: 1900
		private Widget _nextCharacterInputVisualParent;

		// Token: 0x0400076D RID: 1901
		private InputKeyVisualWidget _transferInputKeyVisualWidget;

		// Token: 0x0400076E RID: 1902
		private RichTextWidget _tradeLabel;

		// Token: 0x0400076F RID: 1903
		private Widget _inventoryTooltip;

		// Token: 0x04000770 RID: 1904
		private InventoryEquippedItemControlsBrushWidget _equippedItemControls;

		// Token: 0x04000771 RID: 1905
		private InventoryItemPreviewWidget _itemPreviewWidget;

		// Token: 0x04000772 RID: 1906
		private int _transactionCount;

		// Token: 0x04000773 RID: 1907
		private bool _isInWarSet;

		// Token: 0x04000774 RID: 1908
		private int _targetEquipmentIndex;

		// Token: 0x04000775 RID: 1909
		private TextWidget _otherInventoryGoldText;

		// Token: 0x04000776 RID: 1910
		private Widget _otherInventoryGoldImage;

		// Token: 0x04000777 RID: 1911
		private ScrollablePanel _otherInventoryListWidget;

		// Token: 0x04000778 RID: 1912
		private ScrollablePanel _playerInventoryListWidget;

		// Token: 0x04000779 RID: 1913
		private bool _focusLostThisFrame;

		// Token: 0x0400077A RID: 1914
		private bool _isFocusedOnItemList;

		// Token: 0x0400077B RID: 1915
		private bool _isBannerTutorialActive;

		// Token: 0x0400077C RID: 1916
		private int _bannerTypeCode;
	}
}
