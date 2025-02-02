using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.MountAndBlade.GauntletUI.Widgets.Inventory;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Party
{
	// Token: 0x02000063 RID: 99
	public class PartyTroopTupleButtonWidget : ButtonWidget
	{
		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x000102F2 File Offset: 0x0000E4F2
		// (set) Token: 0x06000549 RID: 1353 RVA: 0x000102FA File Offset: 0x0000E4FA
		public string CharacterID { get; set; }

		// Token: 0x0600054A RID: 1354 RVA: 0x00010303 File Offset: 0x0000E503
		public PartyTroopTupleButtonWidget(UIContext context) : base(context)
		{
			base.OverrideDefaultStateSwitchingEnabled = true;
			base.AddState("Selected");
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00010320 File Offset: 0x0000E520
		private void SetWidgetsState(string state)
		{
			this.SetState(state);
			string currentState = this._extendedControlsContainer.CurrentState;
			this._extendedControlsContainer.SetState(base.IsSelected ? "Selected" : "Default");
			this._main.SetState(state);
			if (currentState == "Default" && base.IsSelected)
			{
				base.EventFired("Opened", Array.Empty<object>());
				this.TransferSlider.IsExtended = true;
				this._extendedControlsContainer.IsExtended = true;
				return;
			}
			if (currentState == "Selected" && !base.IsSelected)
			{
				base.EventFired("Closed", Array.Empty<object>());
				this.TransferSlider.IsExtended = false;
				this._extendedControlsContainer.IsExtended = false;
			}
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x000103E6 File Offset: 0x0000E5E6
		protected override void OnDisconnectedFromRoot()
		{
			base.OnDisconnectedFromRoot();
			if (this.ScreenWidget.CurrentMainTuple == this)
			{
				this.ScreenWidget.SetCurrentTuple(null, false);
			}
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0001040C File Offset: 0x0000E60C
		protected override void RefreshState()
		{
			base.RefreshState();
			this._extendedControlsContainer.IsEnabled = base.IsSelected;
			if (base.IsDisabled)
			{
				this.SetWidgetsState("Disabled");
				return;
			}
			if (base.IsPressed)
			{
				this.SetWidgetsState("Pressed");
				return;
			}
			if (base.IsHovered)
			{
				this.SetWidgetsState("Hovered");
				return;
			}
			if (base.IsSelected)
			{
				this.SetWidgetsState("Selected");
				return;
			}
			this.SetWidgetsState("Default");
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0001048C File Offset: 0x0000E68C
		private void AssignScreenWidget()
		{
			Widget widget = this;
			while (widget != base.EventManager.Root && this._screenWidget == null)
			{
				PartyScreenWidget screenWidget;
				if ((screenWidget = (widget as PartyScreenWidget)) != null)
				{
					this._screenWidget = screenWidget;
				}
				else
				{
					widget = widget.ParentWidget;
				}
			}
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x000104CD File Offset: 0x0000E6CD
		protected override void OnMouseReleased()
		{
			base.OnMouseReleased();
			PartyScreenWidget screenWidget = this.ScreenWidget;
			if (screenWidget == null)
			{
				return;
			}
			screenWidget.SetCurrentTuple(this, this.IsTupleLeftSide);
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x000104EC File Offset: 0x0000E6EC
		public void ResetIsSelected()
		{
			base.IsSelected = false;
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x000104F5 File Offset: 0x0000E6F5
		private void OnValueChanged(PropertyOwnerObject arg1, string arg2, int arg3)
		{
			if (arg2 == "ValueInt")
			{
				base.AcceptDrag = (arg3 > 0);
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x0001050E File Offset: 0x0000E70E
		public PartyScreenWidget ScreenWidget
		{
			get
			{
				if (this._screenWidget == null)
				{
					this.AssignScreenWidget();
				}
				return this._screenWidget;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x00010524 File Offset: 0x0000E724
		// (set) Token: 0x06000554 RID: 1364 RVA: 0x0001052C File Offset: 0x0000E72C
		[Editor(false)]
		public bool IsTupleLeftSide
		{
			get
			{
				return this._isTupleLeftSide;
			}
			set
			{
				if (this._isTupleLeftSide != value)
				{
					this._isTupleLeftSide = value;
					base.OnPropertyChanged(value, "IsTupleLeftSide");
				}
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x0001054A File Offset: 0x0000E74A
		// (set) Token: 0x06000556 RID: 1366 RVA: 0x00010554 File Offset: 0x0000E754
		[Editor(false)]
		public InventoryTwoWaySliderWidget TransferSlider
		{
			get
			{
				return this._transferSlider;
			}
			set
			{
				if (this._transferSlider != value)
				{
					this._transferSlider = value;
					base.OnPropertyChanged<InventoryTwoWaySliderWidget>(value, "TransferSlider");
					value.intPropertyChanged += this.OnValueChanged;
					this._transferSlider.AddState("Selected");
					this._transferSlider.OverrideDefaultStateSwitchingEnabled = true;
				}
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x000105AB File Offset: 0x0000E7AB
		// (set) Token: 0x06000558 RID: 1368 RVA: 0x000105B3 File Offset: 0x0000E7B3
		[Editor(false)]
		public bool IsTransferable
		{
			get
			{
				return this._isTransferable;
			}
			set
			{
				if (this._isTransferable != value)
				{
					this._isTransferable = value;
					base.OnPropertyChanged(value, "IsTransferable");
				}
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x000105D1 File Offset: 0x0000E7D1
		// (set) Token: 0x0600055A RID: 1370 RVA: 0x000105D9 File Offset: 0x0000E7D9
		[Editor(false)]
		public bool IsMainHero
		{
			get
			{
				return this._isMainHero;
			}
			set
			{
				if (this._isMainHero != value)
				{
					base.AcceptDrag = !value;
					this._isMainHero = value;
					base.OnPropertyChanged(value, "IsMainHero");
				}
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x00010601 File Offset: 0x0000E801
		// (set) Token: 0x0600055C RID: 1372 RVA: 0x00010609 File Offset: 0x0000E809
		[Editor(false)]
		public bool IsPrisoner
		{
			get
			{
				return this._isPrisoner;
			}
			set
			{
				if (this._isPrisoner != value)
				{
					this._isPrisoner = value;
					base.OnPropertyChanged(value, "IsPrisoner");
				}
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x00010627 File Offset: 0x0000E827
		// (set) Token: 0x0600055E RID: 1374 RVA: 0x0001062F File Offset: 0x0000E82F
		[Editor(false)]
		public int TransferAmount
		{
			get
			{
				return this._transferAmount;
			}
			set
			{
				if (this._transferAmount != value)
				{
					this._transferAmount = value;
					base.OnPropertyChanged(value, "TransferAmount");
				}
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x0001064D File Offset: 0x0000E84D
		// (set) Token: 0x06000560 RID: 1376 RVA: 0x00010655 File Offset: 0x0000E855
		[Editor(false)]
		public InventoryTupleExtensionControlsWidget ExtendedControlsContainer
		{
			get
			{
				return this._extendedControlsContainer;
			}
			set
			{
				if (this._extendedControlsContainer != value)
				{
					this._extendedControlsContainer = value;
					base.OnPropertyChanged<InventoryTupleExtensionControlsWidget>(value, "ExtendedControlsContainer");
				}
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x00010673 File Offset: 0x0000E873
		// (set) Token: 0x06000562 RID: 1378 RVA: 0x0001067B File Offset: 0x0000E87B
		[Editor(false)]
		public Widget Main
		{
			get
			{
				return this._main;
			}
			set
			{
				if (this._main != value)
				{
					this._main = value;
					base.OnPropertyChanged<Widget>(value, "Main");
				}
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x00010699 File Offset: 0x0000E899
		// (set) Token: 0x06000564 RID: 1380 RVA: 0x000106A1 File Offset: 0x0000E8A1
		[Editor(false)]
		public Widget UpgradesPanel
		{
			get
			{
				return this._upgradesPanel;
			}
			set
			{
				if (this._upgradesPanel != value)
				{
					this._upgradesPanel = value;
					base.OnPropertyChanged<Widget>(value, "UpgradesPanel");
				}
			}
		}

		// Token: 0x0400024B RID: 587
		private PartyScreenWidget _screenWidget;

		// Token: 0x0400024C RID: 588
		public InventoryTwoWaySliderWidget _transferSlider;

		// Token: 0x0400024D RID: 589
		private bool _isTupleLeftSide;

		// Token: 0x0400024E RID: 590
		private bool _isTransferable;

		// Token: 0x0400024F RID: 591
		private bool _isMainHero;

		// Token: 0x04000250 RID: 592
		private bool _isPrisoner;

		// Token: 0x04000251 RID: 593
		private int _transferAmount;

		// Token: 0x04000252 RID: 594
		private InventoryTupleExtensionControlsWidget _extendedControlsContainer;

		// Token: 0x04000253 RID: 595
		private Widget _main;

		// Token: 0x04000254 RID: 596
		private Widget _upgradesPanel;
	}
}
