using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000127 RID: 295
	[Flags]
	internal enum InventoryItemType
	{
		// Token: 0x040002A6 RID: 678
		None = 0,
		// Token: 0x040002A7 RID: 679
		Weapon = 1,
		// Token: 0x040002A8 RID: 680
		Shield = 2,
		// Token: 0x040002A9 RID: 681
		HeadArmor = 4,
		// Token: 0x040002AA RID: 682
		BodyArmor = 8,
		// Token: 0x040002AB RID: 683
		LegArmor = 16,
		// Token: 0x040002AC RID: 684
		HandArmor = 32,
		// Token: 0x040002AD RID: 685
		Horse = 64,
		// Token: 0x040002AE RID: 686
		HorseHarness = 128,
		// Token: 0x040002AF RID: 687
		Goods = 256,
		// Token: 0x040002B0 RID: 688
		Book = 512,
		// Token: 0x040002B1 RID: 689
		Animal = 1024,
		// Token: 0x040002B2 RID: 690
		Cape = 2048,
		// Token: 0x040002B3 RID: 691
		HorseCategory = 192,
		// Token: 0x040002B4 RID: 692
		Armors = 2108,
		// Token: 0x040002B5 RID: 693
		Equipable = 2303,
		// Token: 0x040002B6 RID: 694
		All = 4095
	}
}
