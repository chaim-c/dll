using System;
using System.Collections.Generic;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.ViewModelCollection.Input;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.Order
{
	// Token: 0x02000026 RID: 38
	public class OrderSubjectVM : ViewModel
	{
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x0000D6A7 File Offset: 0x0000B8A7
		private bool _isGamepadActive
		{
			get
			{
				return Input.IsControllerConnected && !Input.IsMouseActive;
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000D6BA File Offset: 0x0000B8BA
		public OrderSubjectVM()
		{
			this.ActiveOrders = new List<OrderItemVM>();
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000D6CD File Offset: 0x0000B8CD
		protected virtual void OnSelectionStateChanged(bool isSelected)
		{
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x0000D6CF File Offset: 0x0000B8CF
		// (set) Token: 0x060002E7 RID: 743 RVA: 0x0000D6D7 File Offset: 0x0000B8D7
		[DataSourceProperty]
		public bool IsSelectable
		{
			get
			{
				return this._isSelectable;
			}
			set
			{
				if (value != this._isSelectable)
				{
					this._isSelectable = value;
					base.OnPropertyChangedWithValue(value, "IsSelectable");
				}
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x0000D6F5 File Offset: 0x0000B8F5
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x0000D6FD File Offset: 0x0000B8FD
		[DataSourceProperty]
		public bool IsSelected
		{
			get
			{
				return this._isSelected;
			}
			set
			{
				if ((!value || this.IsSelectable) && value != this._isSelected)
				{
					this._isSelected = value;
					this.OnSelectionStateChanged(value);
					base.OnPropertyChangedWithValue(value, "IsSelected");
				}
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000D72D File Offset: 0x0000B92D
		// (set) Token: 0x060002EB RID: 747 RVA: 0x0000D735 File Offset: 0x0000B935
		[DataSourceProperty]
		public bool IsSelectionActive
		{
			get
			{
				return this._isSelectionActive;
			}
			set
			{
				if (value != this._isSelectionActive)
				{
					this._isSelectionActive = value;
					base.OnPropertyChangedWithValue(value, "IsSelectionActive");
				}
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0000D753 File Offset: 0x0000B953
		// (set) Token: 0x060002ED RID: 749 RVA: 0x0000D75B File Offset: 0x0000B95B
		[DataSourceProperty]
		public int BehaviorType
		{
			get
			{
				return this._behaviorType;
			}
			set
			{
				if (value != this._behaviorType)
				{
					this._behaviorType = value;
					base.OnPropertyChangedWithValue(value, "BehaviorType");
				}
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000D779 File Offset: 0x0000B979
		// (set) Token: 0x060002EF RID: 751 RVA: 0x0000D781 File Offset: 0x0000B981
		[DataSourceProperty]
		public int UnderAttackOfType
		{
			get
			{
				return this._underAttackOfType;
			}
			set
			{
				if (value != this._underAttackOfType)
				{
					this._underAttackOfType = value;
					base.OnPropertyChangedWithValue(value, "UnderAttackOfType");
				}
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000D79F File Offset: 0x0000B99F
		// (set) Token: 0x060002F1 RID: 753 RVA: 0x0000D7A7 File Offset: 0x0000B9A7
		[DataSourceProperty]
		public string ShortcutText
		{
			get
			{
				return this._shortcutText;
			}
			set
			{
				if (value != this._shortcutText)
				{
					this._shortcutText = value;
					base.OnPropertyChangedWithValue<string>(value, "ShortcutText");
				}
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x0000D7CA File Offset: 0x0000B9CA
		// (set) Token: 0x060002F3 RID: 755 RVA: 0x0000D7D2 File Offset: 0x0000B9D2
		[DataSourceProperty]
		public InputKeyItemVM SelectionKey
		{
			get
			{
				return this._selectionKey;
			}
			set
			{
				if (value != this._selectionKey)
				{
					this._selectionKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "SelectionKey");
				}
			}
		}

		// Token: 0x04000162 RID: 354
		internal List<OrderItemVM> ActiveOrders;

		// Token: 0x04000163 RID: 355
		private int _behaviorType;

		// Token: 0x04000164 RID: 356
		private int _underAttackOfType;

		// Token: 0x04000165 RID: 357
		private bool _isSelectable;

		// Token: 0x04000166 RID: 358
		private bool _isSelected;

		// Token: 0x04000167 RID: 359
		private bool _isSelectionActive;

		// Token: 0x04000168 RID: 360
		private string _shortcutText;

		// Token: 0x04000169 RID: 361
		private InputKeyItemVM _selectionKey;
	}
}
