using System;
using System.Collections.Generic;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.OrderOfBattle
{
	// Token: 0x0200002E RID: 46
	public class OrderOfBattleFormationFilterSelectorItemComparer : IComparer<OrderOfBattleFormationFilterSelectorItemVM>
	{
		// Token: 0x0600034F RID: 847 RVA: 0x0000E869 File Offset: 0x0000CA69
		public int Compare(OrderOfBattleFormationFilterSelectorItemVM x, OrderOfBattleFormationFilterSelectorItemVM y)
		{
			return x.FilterType.CompareTo(y.FilterType);
		}
	}
}
