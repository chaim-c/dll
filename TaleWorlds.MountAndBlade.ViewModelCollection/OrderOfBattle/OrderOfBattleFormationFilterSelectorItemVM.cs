using System;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.OrderOfBattle
{
	// Token: 0x0200002D RID: 45
	public class OrderOfBattleFormationFilterSelectorItemVM : ViewModel
	{
		// Token: 0x06000347 RID: 839 RVA: 0x0000E7AA File Offset: 0x0000C9AA
		public OrderOfBattleFormationFilterSelectorItemVM(FormationFilterType filterType, Action<OrderOfBattleFormationFilterSelectorItemVM> onToggled)
		{
			this.FilterType = filterType;
			this.FilterTypeValue = (int)filterType;
			this._onToggled = onToggled;
			this.RefreshValues();
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000E7CD File Offset: 0x0000C9CD
		public override void RefreshValues()
		{
			this.Hint = new HintViewModel(this.FilterType.GetFilterDescription(), null);
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000349 RID: 841 RVA: 0x0000E7E6 File Offset: 0x0000C9E6
		// (set) Token: 0x0600034A RID: 842 RVA: 0x0000E7EE File Offset: 0x0000C9EE
		[DataSourceProperty]
		public int FilterTypeValue
		{
			get
			{
				return this._filterType;
			}
			set
			{
				if (value != this._filterType)
				{
					this._filterType = value;
					base.OnPropertyChangedWithValue(value, "FilterTypeValue");
				}
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600034B RID: 843 RVA: 0x0000E80C File Offset: 0x0000CA0C
		// (set) Token: 0x0600034C RID: 844 RVA: 0x0000E814 File Offset: 0x0000CA14
		[DataSourceProperty]
		public bool IsActive
		{
			get
			{
				return this._isActive;
			}
			set
			{
				if (value != this._isActive)
				{
					this._isActive = value;
					base.OnPropertyChangedWithValue(value, "IsActive");
					Action<OrderOfBattleFormationFilterSelectorItemVM> onToggled = this._onToggled;
					if (onToggled == null)
					{
						return;
					}
					onToggled(this);
				}
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600034D RID: 845 RVA: 0x0000E843 File Offset: 0x0000CA43
		// (set) Token: 0x0600034E RID: 846 RVA: 0x0000E84B File Offset: 0x0000CA4B
		[DataSourceProperty]
		public HintViewModel Hint
		{
			get
			{
				return this._hint;
			}
			set
			{
				if (value != this._hint)
				{
					this._hint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "Hint");
				}
			}
		}

		// Token: 0x0400019A RID: 410
		public readonly FormationFilterType FilterType;

		// Token: 0x0400019B RID: 411
		private Action<OrderOfBattleFormationFilterSelectorItemVM> _onToggled;

		// Token: 0x0400019C RID: 412
		private int _filterType;

		// Token: 0x0400019D RID: 413
		private bool _isActive;

		// Token: 0x0400019E RID: 414
		private HintViewModel _hint;
	}
}
