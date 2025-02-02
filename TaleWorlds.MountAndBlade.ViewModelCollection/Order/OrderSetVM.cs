using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.ViewModelCollection.Input;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.Order
{
	// Token: 0x02000024 RID: 36
	public class OrderSetVM : ViewModel
	{
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000CA93 File Offset: 0x0000AC93
		internal IEnumerable<OrderSubType> SubOrdersSP
		{
			get
			{
				switch (this.OrderSetType)
				{
				case OrderSetType.Movement:
					yield return OrderSubType.MoveToPosition;
					yield return OrderSubType.FollowMe;
					yield return OrderSubType.Charge;
					yield return OrderSubType.Advance;
					yield return OrderSubType.Fallback;
					yield return OrderSubType.Stop;
					yield return OrderSubType.Retreat;
					yield return OrderSubType.Return;
					break;
				case OrderSetType.Form:
					yield return OrderSubType.FormLine;
					yield return OrderSubType.FormClose;
					yield return OrderSubType.FormLoose;
					yield return OrderSubType.FormCircular;
					yield return OrderSubType.FormSchiltron;
					yield return OrderSubType.FormV;
					yield return OrderSubType.FormColumn;
					yield return OrderSubType.FormScatter;
					yield return OrderSubType.Return;
					break;
				case OrderSetType.Toggle:
					yield return OrderSubType.ToggleFacing;
					yield return OrderSubType.ToggleFire;
					yield return OrderSubType.ToggleMount;
					yield return OrderSubType.ToggleAI;
					yield return OrderSubType.ToggleTransfer;
					yield return OrderSubType.Return;
					break;
				case OrderSetType.Facing:
					yield return OrderSubType.ActivationFaceDirection;
					yield return OrderSubType.FaceEnemy;
					break;
				default:
					yield return OrderSubType.None;
					break;
				}
				yield break;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x0000CAA3 File Offset: 0x0000ACA3
		internal IEnumerable<OrderSubType> SubOrdersMP
		{
			get
			{
				switch (this.OrderSetType)
				{
				case OrderSetType.Movement:
					yield return OrderSubType.MoveToPosition;
					yield return OrderSubType.FollowMe;
					yield return OrderSubType.Charge;
					yield return OrderSubType.Advance;
					yield return OrderSubType.Fallback;
					yield return OrderSubType.Stop;
					yield return OrderSubType.Retreat;
					yield return OrderSubType.Return;
					break;
				case OrderSetType.Form:
					yield return OrderSubType.FormLine;
					yield return OrderSubType.FormClose;
					yield return OrderSubType.FormLoose;
					yield return OrderSubType.FormCircular;
					yield return OrderSubType.FormSchiltron;
					yield return OrderSubType.FormV;
					yield return OrderSubType.FormColumn;
					yield return OrderSubType.FormScatter;
					yield return OrderSubType.Return;
					break;
				case OrderSetType.Toggle:
					yield return OrderSubType.ToggleFacing;
					yield return OrderSubType.ToggleFire;
					yield return OrderSubType.ToggleMount;
					yield return OrderSubType.Return;
					break;
				case OrderSetType.Facing:
					yield return OrderSubType.ActivationFaceDirection;
					yield return OrderSubType.FaceEnemy;
					break;
				default:
					yield return OrderSubType.None;
					break;
				}
				yield break;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000CAB3 File Offset: 0x0000ACB3
		// (set) Token: 0x060002B7 RID: 695 RVA: 0x0000CABB File Offset: 0x0000ACBB
		public bool ContainsOrders { get; private set; }

		// Token: 0x060002B8 RID: 696 RVA: 0x0000CAC4 File Offset: 0x0000ACC4
		internal OrderSetVM(OrderSetType orderSetType, Action<OrderItemVM, OrderSetType, bool> onExecution, bool isMultiplayer)
		{
			this.ContainsOrders = true;
			this.OrderSetType = orderSetType;
			this.OnSetExecution = onExecution;
			this._isMultiplayer = isMultiplayer;
			this.Orders = new MBBindingList<OrderItemVM>();
			this.TitleOrderKey = InputKeyItemVM.CreateFromGameKey(OrderSetVM.GetOrderGameKey((int)orderSetType), false);
			this.RefreshValues();
			this.TitleOrder.IsActive = true;
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000CB38 File Offset: 0x0000AD38
		internal OrderSetVM(OrderSubType orderSubType, int index, Action<OrderItemVM, OrderSetType, bool> onExecution, bool isMultiplayer)
		{
			this.ContainsOrders = false;
			this.OrderSubType = orderSubType;
			this.OnSetExecution = onExecution;
			this._isMultiplayer = isMultiplayer;
			this._index = index;
			this.Orders = new MBBindingList<OrderItemVM>();
			this.TitleOrderKey = InputKeyItemVM.CreateFromGameKey(OrderSetVM.GetOrderGameKey(index), false);
			this.RefreshValues();
			this.TitleOrder.IsActive = true;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000CBB4 File Offset: 0x0000ADB4
		public override void RefreshValues()
		{
			base.RefreshValues();
			OrderItemVM titleOrder = this.TitleOrder;
			if (titleOrder != null)
			{
				titleOrder.OnFinalize();
			}
			if (this.ContainsOrders)
			{
				this.TitleOrder = new OrderItemVM(this.OrderSetType, GameTexts.FindText("str_order_set_name", this.OrderSetType.ToString()), new Action<OrderItemVM, bool>(this.OnExecuteOrderSet))
				{
					ShortcutKey = InputKeyItemVM.CreateFromGameKey(OrderSetVM.GetOrderGameKey(this.GetOrderIndexFromOrderSetType(this.OrderSetType)), false),
					IsTitle = true
				};
				this.TitleText = GameTexts.FindText("str_order_set_name", this.OrderSetType.ToString()).ToString().Trim(new char[]
				{
					' '
				});
			}
			else
			{
				this._isToggleActivationOrder = (this.OrderSubType > OrderSubType.ToggleStart && this.OrderSubType < OrderSubType.ToggleEnd);
				TextObject textObject;
				if (this._isToggleActivationOrder)
				{
					textObject = GameTexts.FindText("str_order_name_off", this.OrderSubType.ToString());
				}
				else
				{
					textObject = GameTexts.FindText("str_order_name", this.OrderSubType.ToString());
				}
				this.TitleText = textObject.ToString();
				this.TitleOrder = new OrderItemVM(this.OrderSubType, OrderSetType.None, textObject, new Action<OrderItemVM, bool>(this.OnExecuteOrderSet))
				{
					IsTitle = true,
					ShortcutKey = InputKeyItemVM.CreateFromGameKey(OrderSetVM.GetOrderGameKey(this._index), false)
				};
			}
			MBTextManager.SetTextVariable("SHORTCUT", "", false);
			if (this.ContainsOrders)
			{
				OrderSubType[] array = this._isMultiplayer ? this.SubOrdersMP.ToArray<OrderSubType>() : this.SubOrdersSP.ToArray<OrderSubType>();
				foreach (OrderItemVM orderItemVM in this.Orders)
				{
					orderItemVM.ShortcutKey.OnFinalize();
				}
				this.Orders.Clear();
				for (int i = 0; i < array.Length; i++)
				{
					OrderSubType orderSubType = array[i];
					TextObject tooltipText;
					if (this.OrderSetType == OrderSetType.Toggle)
					{
						tooltipText = GameTexts.FindText("str_order_name_off", orderSubType.ToString());
					}
					else
					{
						tooltipText = GameTexts.FindText("str_order_name", orderSubType.ToString());
					}
					OrderItemVM orderItemVM2 = new OrderItemVM(orderSubType, this.OrderSetType, tooltipText, new Action<OrderItemVM, bool>(this.OnExecuteSubOrder));
					this.Orders.Add(orderItemVM2);
					if (orderSubType == OrderSubType.Return)
					{
						orderItemVM2.ShortcutKey = InputKeyItemVM.CreateFromGameKey(HotKeyManager.GetCategory("MissionOrderHotkeyCategory").GetGameKey(76), false);
					}
					else
					{
						orderItemVM2.ShortcutKey = InputKeyItemVM.CreateFromGameKey(OrderSetVM.GetOrderGameKey(i), false);
					}
				}
			}
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000CE6C File Offset: 0x0000B06C
		private int GetOrderIndexFromOrderSetType(OrderSetType orderSetType)
		{
			int result;
			if (BannerlordConfig.OrderLayoutType == 0)
			{
				result = (int)orderSetType;
			}
			else
			{
				switch (orderSetType)
				{
				case OrderSetType.Movement:
					return 0;
				case OrderSetType.Form:
					return 2;
				case OrderSetType.Facing:
					return 1;
				}
				result = -1;
			}
			return result;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000CEAC File Offset: 0x0000B0AC
		private static GameKey GetOrderGameKey(int index)
		{
			switch (index)
			{
			case 0:
				return HotKeyManager.GetCategory("MissionOrderHotkeyCategory").GetGameKey(68);
			case 1:
				return HotKeyManager.GetCategory("MissionOrderHotkeyCategory").GetGameKey(69);
			case 2:
				return HotKeyManager.GetCategory("MissionOrderHotkeyCategory").GetGameKey(70);
			case 3:
				return HotKeyManager.GetCategory("MissionOrderHotkeyCategory").GetGameKey(71);
			case 4:
				return HotKeyManager.GetCategory("MissionOrderHotkeyCategory").GetGameKey(72);
			case 5:
				return HotKeyManager.GetCategory("MissionOrderHotkeyCategory").GetGameKey(73);
			case 6:
				return HotKeyManager.GetCategory("MissionOrderHotkeyCategory").GetGameKey(74);
			case 7:
				return HotKeyManager.GetCategory("MissionOrderHotkeyCategory").GetGameKey(75);
			case 8:
				return HotKeyManager.GetCategory("MissionOrderHotkeyCategory").GetGameKey(76);
			default:
				Debug.FailedAssert("Invalid order game key index", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.ViewModelCollection\\Order\\OrderSetVM.cs", "GetOrderGameKey", 346);
				return null;
			}
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000CFA4 File Offset: 0x0000B1A4
		private void OnExecuteSubOrder(OrderItemVM orderItem, bool fromSelection)
		{
			this.OnSetExecution(orderItem, this.OrderSetType, fromSelection);
			if (fromSelection)
			{
				this.SelectedOrderText = orderItem.MainTitle;
			}
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000CFC8 File Offset: 0x0000B1C8
		private void OnExecuteOrderSet(OrderItemVM orderItem, bool fromSelection)
		{
			this.OnSetExecution(orderItem, this.OrderSetType, fromSelection);
			if (fromSelection)
			{
				this.SelectedOrderText = orderItem.MainTitle;
			}
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000CFEC File Offset: 0x0000B1EC
		public void ResetActiveStatus(bool disable = false)
		{
			this.TitleOrder.SelectionState = (disable ? 0 : 1);
			if (this.ContainsOrders)
			{
				foreach (OrderItemVM orderItemVM in this.Orders)
				{
					orderItemVM.SelectionState = (disable ? 0 : 1);
				}
				if (this.OrderSetType == OrderSetType.Toggle)
				{
					this.Orders.ApplyActionOnAllItems(delegate(OrderItemVM o)
					{
						o.SetActiveState(false);
					});
					return;
				}
			}
			else
			{
				this.TitleOrder.SetActiveState(false);
			}
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000D098 File Offset: 0x0000B298
		public void FinalizeActiveStatus(bool forceDisable = false)
		{
			this.TitleOrder.FinalizeActiveStatus();
			if (forceDisable)
			{
				return;
			}
			foreach (OrderItemVM orderItemVM in this.Orders)
			{
				orderItemVM.FinalizeActiveStatus();
			}
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000D0F4 File Offset: 0x0000B2F4
		internal OrderItemVM GetOrder(OrderSubType type)
		{
			if (this.ContainsOrders)
			{
				return this.Orders.FirstOrDefault((OrderItemVM order) => order.OrderSubType == type);
			}
			if (type == this.TitleOrder.OrderSubType)
			{
				return this.TitleOrder;
			}
			Debug.FailedAssert("Couldn't find order item " + type.ToString(), "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.ViewModelCollection\\Order\\OrderSetVM.cs", "GetOrder", 442);
			return null;
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000D178 File Offset: 0x0000B378
		public void SetActiveOrder(OrderItemVM order)
		{
			if (this.OrderSetType != OrderSetType.Toggle)
			{
				this.TitleOrder.OrderIconID = ((order.OrderSubType == OrderSubType.None) ? "MultipleSelection" : order.OrderIconID);
				this.TitleOrder.MainTitle = order.MainTitle;
				this.SelectedOrderText = order.MainTitle;
				return;
			}
			order.SetActiveState(true);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000D1D4 File Offset: 0x0000B3D4
		public void UpdateCanUseShortcuts(bool value)
		{
			this.CanUseShortcuts = value;
			if (this.TitleOrder != null)
			{
				this.TitleOrder.CanUseShortcuts = value;
			}
			for (int i = 0; i < this.Orders.Count; i++)
			{
				this.Orders[i].CanUseShortcuts = value;
			}
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000D224 File Offset: 0x0000B424
		public override void OnFinalize()
		{
			base.OnFinalize();
			if (this.ContainsOrders)
			{
				foreach (OrderItemVM orderItemVM in this.Orders)
				{
					orderItemVM.ShortcutKey.OnFinalize();
				}
			}
			this.TitleOrder.ShortcutKey.OnFinalize();
			this.TitleOrderKey.OnFinalize();
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x0000D29C File Offset: 0x0000B49C
		// (set) Token: 0x060002C6 RID: 710 RVA: 0x0000D2A4 File Offset: 0x0000B4A4
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
					base.OnPropertyChangedWithValue(value, "CanUseShortcuts");
				}
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x0000D2C2 File Offset: 0x0000B4C2
		// (set) Token: 0x060002C8 RID: 712 RVA: 0x0000D2CA File Offset: 0x0000B4CA
		[DataSourceProperty]
		public string SelectedOrderText
		{
			get
			{
				return this._selectedOrderText;
			}
			set
			{
				if (value != this._selectedOrderText)
				{
					this._selectedOrderText = value;
					base.OnPropertyChangedWithValue<string>(value, "SelectedOrderText");
				}
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x0000D2ED File Offset: 0x0000B4ED
		// (set) Token: 0x060002CA RID: 714 RVA: 0x0000D2F5 File Offset: 0x0000B4F5
		[DataSourceProperty]
		public string TitleText
		{
			get
			{
				return this._titleText;
			}
			set
			{
				if (value != this._titleText)
				{
					this._titleText = value;
					base.OnPropertyChangedWithValue<string>(value, "TitleText");
				}
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060002CB RID: 715 RVA: 0x0000D318 File Offset: 0x0000B518
		// (set) Token: 0x060002CC RID: 716 RVA: 0x0000D320 File Offset: 0x0000B520
		[DataSourceProperty]
		public MBBindingList<OrderItemVM> Orders
		{
			get
			{
				return this._orders;
			}
			set
			{
				if (value != this._orders)
				{
					this._orders = value;
					base.OnPropertyChangedWithValue<MBBindingList<OrderItemVM>>(value, "Orders");
				}
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060002CD RID: 717 RVA: 0x0000D33E File Offset: 0x0000B53E
		// (set) Token: 0x060002CE RID: 718 RVA: 0x0000D346 File Offset: 0x0000B546
		[DataSourceProperty]
		public OrderItemVM TitleOrder
		{
			get
			{
				return this._titleOrder;
			}
			set
			{
				if (value != this._titleOrder)
				{
					this._titleOrder = value;
					base.OnPropertyChangedWithValue<OrderItemVM>(value, "TitleOrder");
				}
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060002CF RID: 719 RVA: 0x0000D364 File Offset: 0x0000B564
		// (set) Token: 0x060002D0 RID: 720 RVA: 0x0000D36C File Offset: 0x0000B56C
		[DataSourceProperty]
		public InputKeyItemVM TitleOrderKey
		{
			get
			{
				return this._titleOrderKey;
			}
			set
			{
				if (value != this._titleOrderKey)
				{
					this._titleOrderKey = value;
					base.OnPropertyChanged("TitleOrderKey");
				}
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x0000D389 File Offset: 0x0000B589
		// (set) Token: 0x060002D2 RID: 722 RVA: 0x0000D39B File Offset: 0x0000B59B
		[DataSourceProperty]
		public bool ShowOrders
		{
			get
			{
				return this._showOrders && this.ContainsOrders;
			}
			set
			{
				this._showOrders = value;
				base.OnPropertyChangedWithValue(value, "ShowOrders");
			}
		}

		// Token: 0x0400014E RID: 334
		public OrderSetType OrderSetType = OrderSetType.None;

		// Token: 0x0400014F RID: 335
		internal OrderSubType OrderSubType = OrderSubType.None;

		// Token: 0x04000150 RID: 336
		private readonly bool _isMultiplayer;

		// Token: 0x04000151 RID: 337
		private readonly int _index = -1;

		// Token: 0x04000152 RID: 338
		private readonly Action<OrderItemVM, OrderSetType, bool> OnSetExecution;

		// Token: 0x04000153 RID: 339
		private bool _isToggleActivationOrder;

		// Token: 0x04000154 RID: 340
		private bool _showOrders;

		// Token: 0x04000155 RID: 341
		private bool _canUseShortcuts;

		// Token: 0x04000156 RID: 342
		private OrderItemVM _titleOrder;

		// Token: 0x04000157 RID: 343
		private MBBindingList<OrderItemVM> _orders;

		// Token: 0x04000158 RID: 344
		private string _titleText;

		// Token: 0x04000159 RID: 345
		private InputKeyItemVM _titleOrderKey;

		// Token: 0x0400015A RID: 346
		private string _selectedOrderText;
	}
}
