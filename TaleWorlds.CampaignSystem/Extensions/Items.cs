using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.Extensions
{
	// Token: 0x0200014C RID: 332
	public static class Items
	{
		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06001861 RID: 6241 RVA: 0x0007C608 File Offset: 0x0007A808
		public static MBReadOnlyList<ItemObject> All
		{
			get
			{
				return Campaign.Current.AllItems;
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06001862 RID: 6242 RVA: 0x0007C614 File Offset: 0x0007A814
		public static IEnumerable<ItemObject> AllTradeGoods
		{
			get
			{
				MBReadOnlyList<ItemObject> all = Items.All;
				foreach (ItemObject itemObject in all)
				{
					if (itemObject.IsTradeGood)
					{
						yield return itemObject;
					}
				}
				List<ItemObject>.Enumerator enumerator = default(List<ItemObject>.Enumerator);
				yield break;
				yield break;
			}
		}
	}
}
