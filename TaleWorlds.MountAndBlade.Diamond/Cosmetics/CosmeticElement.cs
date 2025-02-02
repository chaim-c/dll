using System;

namespace TaleWorlds.MountAndBlade.Diamond.Cosmetics
{
	// Token: 0x02000182 RID: 386
	public class CosmeticElement
	{
		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x00012263 File Offset: 0x00010463
		public bool IsFree
		{
			get
			{
				return this.Cost <= 0;
			}
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x00012271 File Offset: 0x00010471
		public CosmeticElement(string id, CosmeticsManager.CosmeticRarity rarity, int cost, CosmeticsManager.CosmeticType type)
		{
			this.UsageIndex = -1;
			this.Id = id;
			this.Rarity = rarity;
			this.Cost = cost;
			this.Type = type;
		}

		// Token: 0x04000524 RID: 1316
		public int UsageIndex;

		// Token: 0x04000525 RID: 1317
		public string Id;

		// Token: 0x04000526 RID: 1318
		public CosmeticsManager.CosmeticRarity Rarity;

		// Token: 0x04000527 RID: 1319
		public int Cost;

		// Token: 0x04000528 RID: 1320
		public CosmeticsManager.CosmeticType Type;
	}
}
