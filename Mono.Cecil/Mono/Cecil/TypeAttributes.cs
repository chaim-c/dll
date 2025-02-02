using System;

namespace Mono.Cecil
{
	// Token: 0x020000EB RID: 235
	[Flags]
	public enum TypeAttributes : uint
	{
		// Token: 0x040005AE RID: 1454
		VisibilityMask = 7U,
		// Token: 0x040005AF RID: 1455
		NotPublic = 0U,
		// Token: 0x040005B0 RID: 1456
		Public = 1U,
		// Token: 0x040005B1 RID: 1457
		NestedPublic = 2U,
		// Token: 0x040005B2 RID: 1458
		NestedPrivate = 3U,
		// Token: 0x040005B3 RID: 1459
		NestedFamily = 4U,
		// Token: 0x040005B4 RID: 1460
		NestedAssembly = 5U,
		// Token: 0x040005B5 RID: 1461
		NestedFamANDAssem = 6U,
		// Token: 0x040005B6 RID: 1462
		NestedFamORAssem = 7U,
		// Token: 0x040005B7 RID: 1463
		LayoutMask = 24U,
		// Token: 0x040005B8 RID: 1464
		AutoLayout = 0U,
		// Token: 0x040005B9 RID: 1465
		SequentialLayout = 8U,
		// Token: 0x040005BA RID: 1466
		ExplicitLayout = 16U,
		// Token: 0x040005BB RID: 1467
		ClassSemanticMask = 32U,
		// Token: 0x040005BC RID: 1468
		Class = 0U,
		// Token: 0x040005BD RID: 1469
		Interface = 32U,
		// Token: 0x040005BE RID: 1470
		Abstract = 128U,
		// Token: 0x040005BF RID: 1471
		Sealed = 256U,
		// Token: 0x040005C0 RID: 1472
		SpecialName = 1024U,
		// Token: 0x040005C1 RID: 1473
		Import = 4096U,
		// Token: 0x040005C2 RID: 1474
		Serializable = 8192U,
		// Token: 0x040005C3 RID: 1475
		WindowsRuntime = 16384U,
		// Token: 0x040005C4 RID: 1476
		StringFormatMask = 196608U,
		// Token: 0x040005C5 RID: 1477
		AnsiClass = 0U,
		// Token: 0x040005C6 RID: 1478
		UnicodeClass = 65536U,
		// Token: 0x040005C7 RID: 1479
		AutoClass = 131072U,
		// Token: 0x040005C8 RID: 1480
		BeforeFieldInit = 1048576U,
		// Token: 0x040005C9 RID: 1481
		RTSpecialName = 2048U,
		// Token: 0x040005CA RID: 1482
		HasSecurity = 262144U,
		// Token: 0x040005CB RID: 1483
		Forwarder = 2097152U
	}
}
