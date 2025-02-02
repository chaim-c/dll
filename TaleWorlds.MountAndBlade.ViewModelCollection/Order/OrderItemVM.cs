using System;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.ViewModelCollection.Input;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.Order
{
	// Token: 0x02000021 RID: 33
	public class OrderItemVM : ViewModel
	{
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000299 RID: 665 RVA: 0x0000C6CC File Offset: 0x0000A8CC
		// (set) Token: 0x0600029A RID: 666 RVA: 0x0000C6D4 File Offset: 0x0000A8D4
		internal OrderSubType OrderSubType { get; private set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0000C6DD File Offset: 0x0000A8DD
		// (set) Token: 0x0600029C RID: 668 RVA: 0x0000C6E5 File Offset: 0x0000A8E5
		internal OrderSetType OrderSetType { get; private set; }

		// Token: 0x0600029D RID: 669 RVA: 0x0000C6F0 File Offset: 0x0000A8F0
		public OrderItemVM(OrderSubType orderSubType, OrderSetType orderSetType, TextObject tooltipText, Action<OrderItemVM, bool> onExecuteAction)
		{
			this.OrderSetType = orderSetType;
			this.OrderSubType = orderSubType;
			this.OrderIconID = this.GetIconIDFromSubType(this.OrderSubType);
			if (orderSetType == OrderSetType.Toggle)
			{
				this.OrderIconID += "Active";
			}
			this.OnExecuteAction = onExecuteAction;
			this.MainTitle = tooltipText.ToString();
			this.IsActive = true;
			this._isActivationOrder = (this.IsTitle && this.OrderSetType == OrderSetType.None);
			this._isToggleActivationOrder = (this.OrderSubType > OrderSubType.ToggleStart && this.OrderSubType < OrderSubType.ToggleEnd);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000C7A4 File Offset: 0x0000A9A4
		public OrderItemVM(OrderSetType orderSetType, TextObject tooltipText, Action<OrderItemVM, bool> onExecuteAction)
		{
			this.OrderSubType = OrderSubType.None;
			this.IsTitle = true;
			this.OrderSetType = orderSetType;
			this.OrderIconID = orderSetType.ToString();
			this.OnExecuteAction = onExecuteAction;
			this.MainTitle = tooltipText.ToString();
			this.IsActive = true;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000C80C File Offset: 0x0000AA0C
		public void SetActiveState(bool isActive)
		{
			if ((this.OrderSetType == OrderSetType.Toggle && !this.IsTitle) || this._isToggleActivationOrder)
			{
				this.MainTitle = GameTexts.FindText(isActive ? "str_order_name_on" : "str_order_name_off", this.OrderSubType.ToString()).ToString();
			}
			if (this._isToggleActivationOrder)
			{
				this.OrderIconID = (isActive ? this.OrderSubType.ToString() : (this.OrderSubType.ToString() + "Active"));
			}
			this.IsActive = true;
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000C8B1 File Offset: 0x0000AAB1
		public override void OnFinalize()
		{
			base.OnFinalize();
			this.ShortcutKey.OnFinalize();
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000C8C4 File Offset: 0x0000AAC4
		public void ExecuteAction()
		{
			this.OnExecuteAction(this, false);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000C8D3 File Offset: 0x0000AAD3
		public void FinalizeActiveStatus()
		{
			base.OnPropertyChanged("SelectionState");
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000C8E0 File Offset: 0x0000AAE0
		private string GetIconIDFromSubType(OrderSubType orderSubType)
		{
			string result = string.Empty;
			if (orderSubType != OrderSubType.ActivationFaceDirection)
			{
				if (orderSubType != OrderSubType.FaceEnemy)
				{
					result = orderSubType.ToString();
				}
				else
				{
					result = OrderSubType.ToggleFacing.ToString() + "Active";
				}
			}
			else
			{
				result = OrderSubType.ToggleFacing.ToString();
			}
			return result;
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x0000C93E File Offset: 0x0000AB3E
		// (set) Token: 0x060002A5 RID: 677 RVA: 0x0000C946 File Offset: 0x0000AB46
		[DataSourceProperty]
		public string OrderIconID
		{
			get
			{
				return this._orderIconID;
			}
			set
			{
				if (value != this._orderIconID)
				{
					this._orderIconID = value;
					base.OnPropertyChangedWithValue<string>(value, "OrderIconID");
				}
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0000C969 File Offset: 0x0000AB69
		// (set) Token: 0x060002A7 RID: 679 RVA: 0x0000C971 File Offset: 0x0000AB71
		[DataSourceProperty]
		public bool IsActive
		{
			get
			{
				return this._isActive;
			}
			set
			{
				value = (value && this._selectionState != 0);
				this._isActive = value;
				base.OnPropertyChangedWithValue(value, "IsActive");
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x0000C997 File Offset: 0x0000AB97
		// (set) Token: 0x060002A9 RID: 681 RVA: 0x0000C99F File Offset: 0x0000AB9F
		[DataSourceProperty]
		public bool IsSelected
		{
			get
			{
				return this._isSelected;
			}
			set
			{
				if (value != this._isSelected)
				{
					this._isSelected = value;
					base.OnPropertyChanged("IsSelected");
					if (value)
					{
						this.OnExecuteAction(this, true);
					}
				}
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002AA RID: 682 RVA: 0x0000C9CC File Offset: 0x0000ABCC
		// (set) Token: 0x060002AB RID: 683 RVA: 0x0000C9D4 File Offset: 0x0000ABD4
		[DataSourceProperty]
		public bool CanUseShortcuts
		{
			get
			{
				return this._canUseShortcuts;
			}
			set
			{
				if (value != this._canUseShortcuts)
				{
					this._canUseShortcuts = value;
					base.OnPropertyChanged("CanUseShortcuts");
				}
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0000C9F1 File Offset: 0x0000ABF1
		// (set) Token: 0x060002AD RID: 685 RVA: 0x0000C9F9 File Offset: 0x0000ABF9
		[DataSourceProperty]
		public int SelectionState
		{
			get
			{
				return this._selectionState;
			}
			set
			{
				if (value != this._selectionState)
				{
					this._selectionState = value;
					base.OnPropertyChangedWithValue(value, "SelectionState");
				}
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002AE RID: 686 RVA: 0x0000CA17 File Offset: 0x0000AC17
		// (set) Token: 0x060002AF RID: 687 RVA: 0x0000CA1F File Offset: 0x0000AC1F
		[DataSourceProperty]
		public InputKeyItemVM ShortcutKey
		{
			get
			{
				return this._shortcutKey;
			}
			set
			{
				if (value != this._shortcutKey)
				{
					this._shortcutKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "ShortcutKey");
				}
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x0000CA3D File Offset: 0x0000AC3D
		// (set) Token: 0x060002B1 RID: 689 RVA: 0x0000CA45 File Offset: 0x0000AC45
		[DataSourceProperty]
		public string MainTitle
		{
			get
			{
				return this._mainTitle;
			}
			set
			{
				if (value != this._mainTitle)
				{
					this._mainTitle = value;
					base.OnPropertyChangedWithValue<string>(value, "MainTitle");
				}
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000CA68 File Offset: 0x0000AC68
		// (set) Token: 0x060002B3 RID: 691 RVA: 0x0000CA70 File Offset: 0x0000AC70
		[DataSourceProperty]
		public string SubSetTitle
		{
			get
			{
				return this._subSetTitle;
			}
			set
			{
				if (value != this._subSetTitle)
				{
					this._subSetTitle = value;
					base.OnPropertyChangedWithValue<string>(value, "SubSetTitle");
				}
			}
		}

		// Token: 0x0400011E RID: 286
		public Action<OrderItemVM, bool> OnExecuteAction;

		// Token: 0x0400011F RID: 287
		public bool IsTitle;

		// Token: 0x04000122 RID: 290
		private bool _isActivationOrder;

		// Token: 0x04000123 RID: 291
		private bool _isToggleActivationOrder;

		// Token: 0x04000124 RID: 292
		private bool _isActive;

		// Token: 0x04000125 RID: 293
		private bool _isSelected;

		// Token: 0x04000126 RID: 294
		private bool _canUseShortcuts;

		// Token: 0x04000127 RID: 295
		private int _selectionState = -1;

		// Token: 0x04000128 RID: 296
		private string _mainTitle;

		// Token: 0x04000129 RID: 297
		private string _subSetTitle;

		// Token: 0x0400012A RID: 298
		private InputKeyItemVM _shortcutKey;

		// Token: 0x0400012B RID: 299
		private string _orderIconID = "";

		// Token: 0x020000AC RID: 172
		public enum OrderSelectionState
		{
			// Token: 0x04000553 RID: 1363
			Disabled,
			// Token: 0x04000554 RID: 1364
			Default,
			// Token: 0x04000555 RID: 1365
			PartiallyActive,
			// Token: 0x04000556 RID: 1366
			Active
		}
	}
}
