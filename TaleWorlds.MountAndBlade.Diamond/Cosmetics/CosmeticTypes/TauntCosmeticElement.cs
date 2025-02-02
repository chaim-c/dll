using System;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.Diamond.Cosmetics.CosmeticTypes
{
	// Token: 0x02000186 RID: 390
	public class TauntCosmeticElement : CosmeticElement
	{
		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000ADB RID: 2779 RVA: 0x00012871 File Offset: 0x00010A71
		public static int MaxNumberOfTaunts
		{
			get
			{
				return 6;
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x00012874 File Offset: 0x00010A74
		public TextObject Name { get; }

		// Token: 0x06000ADD RID: 2781 RVA: 0x0001287C File Offset: 0x00010A7C
		public TauntCosmeticElement(int index, string id, CosmeticsManager.CosmeticRarity rarity, int cost, string name) : base(id, rarity, cost, CosmeticsManager.CosmeticType.Taunt)
		{
			this.UsageIndex = index;
			this.Name = new TextObject(name, null);
		}
	}
}
