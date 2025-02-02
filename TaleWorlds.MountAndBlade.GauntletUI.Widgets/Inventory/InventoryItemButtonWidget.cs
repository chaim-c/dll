using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Inventory
{
	// Token: 0x02000130 RID: 304
	public abstract class InventoryItemButtonWidget : ButtonWidget
	{
		// Token: 0x06000FA5 RID: 4005 RVA: 0x0002B199 File Offset: 0x00029399
		protected InventoryItemButtonWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x0002B1A2 File Offset: 0x000293A2
		protected override void OnDragBegin()
		{
			InventoryScreenWidget screenWidget = this.ScreenWidget;
			if (screenWidget != null)
			{
				screenWidget.ItemWidgetDragBegin(this);
			}
			base.OnDragBegin();
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x0002B1BC File Offset: 0x000293BC
		protected override bool OnDrop()
		{
			InventoryScreenWidget screenWidget = this.ScreenWidget;
			if (screenWidget != null)
			{
				screenWidget.ItemWidgetDrop(this);
			}
			return base.OnDrop();
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x0002B1D6 File Offset: 0x000293D6
		public virtual void ResetIsSelected()
		{
			base.IsSelected = false;
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x0002B1DF File Offset: 0x000293DF
		public void PreviewItem()
		{
			base.EventFired("PreviewItem", Array.Empty<object>());
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x0002B1F1 File Offset: 0x000293F1
		public void SellItem()
		{
			base.EventFired("SellItem", Array.Empty<object>());
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x0002B203 File Offset: 0x00029403
		public void EquipItem()
		{
			base.EventFired("EquipItem", Array.Empty<object>());
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x0002B215 File Offset: 0x00029415
		public void UnequipItem()
		{
			base.EventFired("UnequipItem", Array.Empty<object>());
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x0002B228 File Offset: 0x00029428
		private void AssignScreenWidget()
		{
			Widget widget = this;
			while (widget != base.EventManager.Root && this._screenWidget == null)
			{
				if (widget is InventoryScreenWidget)
				{
					this._screenWidget = (InventoryScreenWidget)widget;
				}
				else
				{
					widget = widget.ParentWidget;
				}
			}
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x0002B26C File Offset: 0x0002946C
		private void ItemTypeUpdated()
		{
			AudioProperty audioProperty = base.Brush.SoundProperties.GetEventAudioProperty("DragEnd");
			if (audioProperty == null)
			{
				audioProperty = new AudioProperty();
				base.Brush.SoundProperties.AddEventSound("DragEnd", audioProperty);
			}
			audioProperty.AudioName = this.GetSound(this.ItemType);
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x0002B2C0 File Offset: 0x000294C0
		private string GetSound(int typeID)
		{
			switch (typeID)
			{
			case 1:
				return "inventory/horse";
			case 2:
				return "inventory/onehanded";
			case 3:
				return "inventory/twohanded";
			case 4:
				return "inventory/polearm";
			case 5:
			case 6:
				return "inventory/quiver";
			case 7:
				return "inventory/shield";
			case 8:
				return "inventory/bow";
			case 9:
				return "inventory/crossbow";
			case 10:
				return "inventory/throwing";
			case 11:
				return "inventory/sack";
			case 12:
				return "inventory/helmet";
			case 13:
				return "inventory/leather";
			case 14:
			case 15:
				return "inventory/leather_lite";
			case 19:
				return "inventory/animal";
			case 20:
				return "inventory/book";
			case 21:
			case 22:
				return "inventory/leather";
			case 23:
				return "inventory/horsearmor";
			case 24:
				return "inventory/perk";
			case 25:
				return "inventory/leather";
			case 26:
				return "inventory/siegeweapon";
			}
			return "inventory/leather";
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06000FB0 RID: 4016 RVA: 0x0002B3BC File Offset: 0x000295BC
		// (set) Token: 0x06000FB1 RID: 4017 RVA: 0x0002B3C4 File Offset: 0x000295C4
		[Editor(false)]
		public bool IsRightSide
		{
			get
			{
				return this._isRightSide;
			}
			set
			{
				if (this._isRightSide != value)
				{
					this._isRightSide = value;
					base.OnPropertyChanged(value, "IsRightSide");
				}
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06000FB2 RID: 4018 RVA: 0x0002B3E2 File Offset: 0x000295E2
		// (set) Token: 0x06000FB3 RID: 4019 RVA: 0x0002B3EA File Offset: 0x000295EA
		[Editor(false)]
		public int ItemType
		{
			get
			{
				return this._itemType;
			}
			set
			{
				if (this._itemType != value)
				{
					this._itemType = value;
					base.OnPropertyChanged(value, "ItemType");
					this.ItemTypeUpdated();
				}
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06000FB4 RID: 4020 RVA: 0x0002B40E File Offset: 0x0002960E
		// (set) Token: 0x06000FB5 RID: 4021 RVA: 0x0002B416 File Offset: 0x00029616
		[Editor(false)]
		public int EquipmentIndex
		{
			get
			{
				return this._equipmentIndex;
			}
			set
			{
				if (this._equipmentIndex != value)
				{
					this._equipmentIndex = value;
					base.OnPropertyChanged(value, "EquipmentIndex");
				}
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06000FB6 RID: 4022 RVA: 0x0002B434 File Offset: 0x00029634
		public InventoryScreenWidget ScreenWidget
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

		// Token: 0x04000723 RID: 1827
		private bool _isRightSide;

		// Token: 0x04000724 RID: 1828
		private int _itemType;

		// Token: 0x04000725 RID: 1829
		private int _equipmentIndex;

		// Token: 0x04000726 RID: 1830
		private InventoryScreenWidget _screenWidget;
	}
}
