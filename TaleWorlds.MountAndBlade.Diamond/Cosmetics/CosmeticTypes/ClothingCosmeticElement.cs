using System;
using System.Collections.Generic;

namespace TaleWorlds.MountAndBlade.Diamond.Cosmetics.CosmeticTypes
{
	// Token: 0x02000184 RID: 388
	public class ClothingCosmeticElement : CosmeticElement
	{
		// Token: 0x06000AD9 RID: 2777 RVA: 0x00012841 File Offset: 0x00010A41
		public ClothingCosmeticElement(string id, CosmeticsManager.CosmeticRarity rarity, int cost, List<string> replaceItemsId, List<Tuple<string, string>> replaceItemless) : base(id, rarity, cost, CosmeticsManager.CosmeticType.Clothing)
		{
			this.ReplaceItemsId = replaceItemsId;
			this.ReplaceItemless = replaceItemless;
		}

		// Token: 0x0400052B RID: 1323
		public readonly List<string> ReplaceItemsId;

		// Token: 0x0400052C RID: 1324
		public readonly List<Tuple<string, string>> ReplaceItemless;
	}
}
