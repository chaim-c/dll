using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.HUD
{
	// Token: 0x02000049 RID: 73
	public class EquipmentActionItemVM : ViewModel
	{
		// Token: 0x06000621 RID: 1569 RVA: 0x000196DB File Offset: 0x000178DB
		public EquipmentActionItemVM(string item, string itemTypeAsString, object identifier, Action<EquipmentActionItemVM> onSelection, bool isCurrentlyWielded = false)
		{
			this.Identifier = identifier;
			this.ActionText = item;
			this.TypeAsString = itemTypeAsString;
			this.IsWielded = isCurrentlyWielded;
			this._onSelection = onSelection;
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x00019708 File Offset: 0x00017908
		// (set) Token: 0x06000623 RID: 1571 RVA: 0x00019710 File Offset: 0x00017910
		[DataSourceProperty]
		public string ActionText
		{
			get
			{
				return this._actionText;
			}
			set
			{
				if (value != this._actionText)
				{
					this._actionText = value;
					base.OnPropertyChangedWithValue<string>(value, "ActionText");
				}
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000624 RID: 1572 RVA: 0x00019733 File Offset: 0x00017933
		// (set) Token: 0x06000625 RID: 1573 RVA: 0x0001973B File Offset: 0x0001793B
		[DataSourceProperty]
		public bool IsWielded
		{
			get
			{
				return this._isWielded;
			}
			set
			{
				if (value != this._isWielded)
				{
					this._isWielded = value;
					base.OnPropertyChangedWithValue(value, "IsWielded");
				}
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000626 RID: 1574 RVA: 0x00019759 File Offset: 0x00017959
		// (set) Token: 0x06000627 RID: 1575 RVA: 0x00019761 File Offset: 0x00017961
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
					base.OnPropertyChangedWithValue(value, "IsSelected");
					if (value)
					{
						this._onSelection(this);
					}
				}
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x0001978E File Offset: 0x0001798E
		// (set) Token: 0x06000629 RID: 1577 RVA: 0x00019796 File Offset: 0x00017996
		[DataSourceProperty]
		public string TypeAsString
		{
			get
			{
				return this._typeAsString;
			}
			set
			{
				if (value != this._typeAsString)
				{
					this._typeAsString = value;
					base.OnPropertyChangedWithValue<string>(value, "TypeAsString");
				}
			}
		}

		// Token: 0x040002EA RID: 746
		private readonly Action<EquipmentActionItemVM> _onSelection;

		// Token: 0x040002EB RID: 747
		public object Identifier;

		// Token: 0x040002EC RID: 748
		private string _actionText;

		// Token: 0x040002ED RID: 749
		private string _typeAsString;

		// Token: 0x040002EE RID: 750
		private bool _isSelected;

		// Token: 0x040002EF RID: 751
		private bool _isWielded;
	}
}
