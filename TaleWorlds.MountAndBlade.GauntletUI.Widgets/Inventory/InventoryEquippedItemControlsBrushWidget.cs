using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Inventory
{
	// Token: 0x0200012D RID: 301
	public class InventoryEquippedItemControlsBrushWidget : BrushWidget
	{
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000F7A RID: 3962 RVA: 0x0002A8C8 File Offset: 0x00028AC8
		// (remove) Token: 0x06000F7B RID: 3963 RVA: 0x0002A900 File Offset: 0x00028B00
		public event InventoryEquippedItemControlsBrushWidget.ButtonClickEventHandler OnPreviewClick;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000F7C RID: 3964 RVA: 0x0002A938 File Offset: 0x00028B38
		// (remove) Token: 0x06000F7D RID: 3965 RVA: 0x0002A970 File Offset: 0x00028B70
		public event InventoryEquippedItemControlsBrushWidget.ButtonClickEventHandler OnUnequipClick;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000F7E RID: 3966 RVA: 0x0002A9A8 File Offset: 0x00028BA8
		// (remove) Token: 0x06000F7F RID: 3967 RVA: 0x0002A9E0 File Offset: 0x00028BE0
		public event InventoryEquippedItemControlsBrushWidget.ButtonClickEventHandler OnSellClick;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000F80 RID: 3968 RVA: 0x0002AA18 File Offset: 0x00028C18
		// (remove) Token: 0x06000F81 RID: 3969 RVA: 0x0002AA50 File Offset: 0x00028C50
		public event Action OnHidePanel;

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06000F82 RID: 3970 RVA: 0x0002AA85 File Offset: 0x00028C85
		// (set) Token: 0x06000F83 RID: 3971 RVA: 0x0002AA8D File Offset: 0x00028C8D
		public NavigationForcedScopeCollectionTargeter ForcedScopeCollection { get; set; }

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06000F84 RID: 3972 RVA: 0x0002AA96 File Offset: 0x00028C96
		// (set) Token: 0x06000F85 RID: 3973 RVA: 0x0002AA9E File Offset: 0x00028C9E
		public NavigationScopeTargeter NavigationScope { get; set; }

		// Token: 0x06000F86 RID: 3974 RVA: 0x0002AAA8 File Offset: 0x00028CA8
		public InventoryEquippedItemControlsBrushWidget(UIContext context) : base(context)
		{
			this._previewClickHandler = new Action<Widget>(this.PreviewClicked);
			this._unequipClickHandler = new Action<Widget>(this.UnequipClicked);
			this._sellClickHandler = new Action<Widget>(this.SellClicked);
			base.AddState("LeftHidden");
			base.AddState("LeftVisible");
			base.AddState("RightHidden");
			base.AddState("RightVisible");
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x0002AB20 File Offset: 0x00028D20
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this.ScreenWidget == null)
			{
				Widget widget = this;
				while (widget != base.EventManager.Root && this.ScreenWidget == null)
				{
					if (widget is InventoryScreenWidget)
					{
						this.ScreenWidget = (InventoryScreenWidget)widget;
					}
					else
					{
						widget = widget.ParentWidget;
					}
				}
			}
			if (this._isScopeDirty && base.EventManager.Time - this._lastTransitionStartTime > base.VisualDefinition.TransitionDuration)
			{
				this.ForcedScopeCollection.IsCollectionDisabled = (base.CurrentState == "RightHidden" || base.CurrentState == "LeftHidden");
				this.NavigationScope.IsScopeDisabled = this.ForcedScopeCollection.IsCollectionDisabled;
				this._isScopeDirty = false;
			}
			if (!this.IsControlsEnabled && this._itemWidget != null)
			{
				this.HidePanel();
			}
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x0002AC00 File Offset: 0x00028E00
		public void ShowPanel(InventoryItemButtonWidget itemWidget)
		{
			if (itemWidget.IsRightSide)
			{
				base.HorizontalAlignment = HorizontalAlignment.Right;
				base.Brush.HorizontalFlip = false;
				this.SetState("RightHidden");
				base.PositionXOffset = base.VisualDefinition.VisualStates["RightHidden"].PositionXOffset;
				this.SetState("RightVisible");
			}
			else
			{
				base.HorizontalAlignment = HorizontalAlignment.Left;
				base.Brush.HorizontalFlip = true;
				this.SetState("LeftHidden");
				base.PositionXOffset = base.VisualDefinition.VisualStates["LeftHidden"].PositionXOffset;
				this.SetState("LeftVisible");
			}
			base.ScaledPositionYOffset = itemWidget.GlobalPosition.Y + itemWidget.Size.Y - 10f * base._scaleToUse - base.EventManager.TopUsableAreaStart;
			base.IsVisible = true;
			this._itemWidget = itemWidget;
			this._isScopeDirty = true;
			this._lastTransitionStartTime = base.Context.EventManager.Time;
			this.IsControlsEnabled = true;
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x0002AD14 File Offset: 0x00028F14
		public void HidePanel()
		{
			if (!base.IsVisible || this._itemWidget == null)
			{
				return;
			}
			if (this._itemWidget.IsRightSide)
			{
				this.SetState("RightHidden");
			}
			else
			{
				this.SetState("LeftHidden");
			}
			this._itemWidget = null;
			Action onHidePanel = this.OnHidePanel;
			if (onHidePanel != null)
			{
				onHidePanel();
			}
			this._isScopeDirty = true;
			this._lastTransitionStartTime = base.Context.EventManager.Time;
			this.IsControlsEnabled = false;
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x0002AD93 File Offset: 0x00028F93
		private void PreviewClicked(Widget widget)
		{
			if (this._itemWidget == null)
			{
				return;
			}
			InventoryEquippedItemControlsBrushWidget.ButtonClickEventHandler onPreviewClick = this.OnPreviewClick;
			if (onPreviewClick != null)
			{
				onPreviewClick(this._itemWidget);
			}
			this._itemWidget.PreviewItem();
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x0002ADC0 File Offset: 0x00028FC0
		private void UnequipClicked(Widget widget)
		{
			if (this._itemWidget == null)
			{
				return;
			}
			InventoryEquippedItemControlsBrushWidget.ButtonClickEventHandler onUnequipClick = this.OnUnequipClick;
			if (onUnequipClick != null)
			{
				onUnequipClick(this._itemWidget);
			}
			this._itemWidget.UnequipItem();
			this.HidePanel();
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x0002ADF3 File Offset: 0x00028FF3
		private void SellClicked(Widget widget)
		{
			if (this._itemWidget == null)
			{
				return;
			}
			InventoryEquippedItemControlsBrushWidget.ButtonClickEventHandler onSellClick = this.OnSellClick;
			if (onSellClick != null)
			{
				onSellClick(this._itemWidget);
			}
			this.ScreenWidget.TransactionCount = 1;
			this._itemWidget.SellItem();
			this.HidePanel();
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x0002AE32 File Offset: 0x00029032
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			InventoryItemButtonWidget itemWidget = this._itemWidget;
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06000F8E RID: 3982 RVA: 0x0002AE42 File Offset: 0x00029042
		// (set) Token: 0x06000F8F RID: 3983 RVA: 0x0002AE4A File Offset: 0x0002904A
		public bool IsControlsEnabled
		{
			get
			{
				return this._isControlsEnabled;
			}
			set
			{
				if (value != this._isControlsEnabled)
				{
					this._isControlsEnabled = value;
					base.OnPropertyChanged(value, "IsControlsEnabled");
				}
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06000F90 RID: 3984 RVA: 0x0002AE68 File Offset: 0x00029068
		// (set) Token: 0x06000F91 RID: 3985 RVA: 0x0002AE70 File Offset: 0x00029070
		[Editor(false)]
		public InventoryScreenWidget ScreenWidget
		{
			get
			{
				return this._screenWidget;
			}
			set
			{
				if (this._screenWidget != value)
				{
					this._screenWidget = value;
					base.OnPropertyChanged<InventoryScreenWidget>(value, "ScreenWidget");
				}
			}
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06000F92 RID: 3986 RVA: 0x0002AE8E File Offset: 0x0002908E
		// (set) Token: 0x06000F93 RID: 3987 RVA: 0x0002AE98 File Offset: 0x00029098
		[Editor(false)]
		public ButtonWidget PreviewButton
		{
			get
			{
				return this._previewButton;
			}
			set
			{
				if (this._previewButton != value)
				{
					ButtonWidget previewButton = this._previewButton;
					if (previewButton != null)
					{
						previewButton.ClickEventHandlers.Remove(this._previewClickHandler);
					}
					this._previewButton = value;
					ButtonWidget previewButton2 = this._previewButton;
					if (previewButton2 != null)
					{
						previewButton2.ClickEventHandlers.Add(this._previewClickHandler);
					}
					base.OnPropertyChanged<ButtonWidget>(value, "PreviewButton");
				}
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06000F94 RID: 3988 RVA: 0x0002AEFA File Offset: 0x000290FA
		// (set) Token: 0x06000F95 RID: 3989 RVA: 0x0002AF04 File Offset: 0x00029104
		[Editor(false)]
		public ButtonWidget UnequipButton
		{
			get
			{
				return this._unequipButton;
			}
			set
			{
				if (this._unequipButton != value)
				{
					ButtonWidget unequipButton = this._unequipButton;
					if (unequipButton != null)
					{
						unequipButton.ClickEventHandlers.Remove(this._unequipClickHandler);
					}
					this._unequipButton = value;
					ButtonWidget unequipButton2 = this._unequipButton;
					if (unequipButton2 != null)
					{
						unequipButton2.ClickEventHandlers.Add(this._unequipClickHandler);
					}
					base.OnPropertyChanged<ButtonWidget>(value, "UnequipButton");
				}
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06000F96 RID: 3990 RVA: 0x0002AF66 File Offset: 0x00029166
		// (set) Token: 0x06000F97 RID: 3991 RVA: 0x0002AF70 File Offset: 0x00029170
		[Editor(false)]
		public ButtonWidget SellButton
		{
			get
			{
				return this._sellButton;
			}
			set
			{
				if (this._sellButton != value)
				{
					ButtonWidget sellButton = this._sellButton;
					if (sellButton != null)
					{
						sellButton.ClickEventHandlers.Remove(this._sellClickHandler);
					}
					this._sellButton = value;
					ButtonWidget sellButton2 = this._sellButton;
					if (sellButton2 != null)
					{
						sellButton2.ClickEventHandlers.Add(this._sellClickHandler);
					}
					base.OnPropertyChanged<ButtonWidget>(value, "SellButton");
				}
			}
		}

		// Token: 0x04000713 RID: 1811
		private InventoryItemButtonWidget _itemWidget;

		// Token: 0x04000714 RID: 1812
		private Action<Widget> _previewClickHandler;

		// Token: 0x04000715 RID: 1813
		private Action<Widget> _unequipClickHandler;

		// Token: 0x04000716 RID: 1814
		private Action<Widget> _sellClickHandler;

		// Token: 0x04000717 RID: 1815
		private float _lastTransitionStartTime;

		// Token: 0x04000718 RID: 1816
		private bool _isScopeDirty;

		// Token: 0x0400071B RID: 1819
		private bool _isControlsEnabled;

		// Token: 0x0400071C RID: 1820
		private InventoryScreenWidget _screenWidget;

		// Token: 0x0400071D RID: 1821
		private ButtonWidget _previewButton;

		// Token: 0x0400071E RID: 1822
		private ButtonWidget _unequipButton;

		// Token: 0x0400071F RID: 1823
		private ButtonWidget _sellButton;

		// Token: 0x020001B3 RID: 435
		// (Invoke) Token: 0x0600148C RID: 5260
		public delegate void ButtonClickEventHandler(Widget itemWidget);
	}
}
