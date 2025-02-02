using System;

namespace TaleWorlds.MountAndBlade.Diamond.Cosmetics.CosmeticTypes
{
	// Token: 0x02000185 RID: 389
	public class SigilCosmeticElement : CosmeticElement
	{
		// Token: 0x06000ADA RID: 2778 RVA: 0x0001285D File Offset: 0x00010A5D
		public SigilCosmeticElement(string id, CosmeticsManager.CosmeticRarity rarity, int cost, string bannerCode) : base(id, rarity, cost, CosmeticsManager.CosmeticType.Sigil)
		{
			this.BannerCode = bannerCode;
		}

		// Token: 0x0400052D RID: 1325
		public string BannerCode;
	}
}
