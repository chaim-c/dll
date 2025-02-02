using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.Order
{
	// Token: 0x02000027 RID: 39
	public class OrderTroopItemFilterVM : ViewModel
	{
		// Token: 0x060002F4 RID: 756 RVA: 0x0000D7F0 File Offset: 0x0000B9F0
		public OrderTroopItemFilterVM(int filterTypeValue)
		{
			this.FilterTypeValue = filterTypeValue;
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x0000D7FF File Offset: 0x0000B9FF
		// (set) Token: 0x060002F6 RID: 758 RVA: 0x0000D807 File Offset: 0x0000BA07
		[DataSourceProperty]
		public int FilterTypeValue
		{
			get
			{
				return this._filterTypeValue;
			}
			set
			{
				if (value != this._filterTypeValue)
				{
					this._filterTypeValue = value;
					base.OnPropertyChangedWithValue(value, "FilterTypeValue");
				}
			}
		}

		// Token: 0x0400016A RID: 362
		private int _filterTypeValue;
	}
}
