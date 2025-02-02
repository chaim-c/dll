using System;

namespace Mono.Cecil
{
	// Token: 0x020000F2 RID: 242
	public enum VariantType
	{
		// Token: 0x0400061D RID: 1565
		None,
		// Token: 0x0400061E RID: 1566
		I2 = 2,
		// Token: 0x0400061F RID: 1567
		I4,
		// Token: 0x04000620 RID: 1568
		R4,
		// Token: 0x04000621 RID: 1569
		R8,
		// Token: 0x04000622 RID: 1570
		CY,
		// Token: 0x04000623 RID: 1571
		Date,
		// Token: 0x04000624 RID: 1572
		BStr,
		// Token: 0x04000625 RID: 1573
		Dispatch,
		// Token: 0x04000626 RID: 1574
		Error,
		// Token: 0x04000627 RID: 1575
		Bool,
		// Token: 0x04000628 RID: 1576
		Variant,
		// Token: 0x04000629 RID: 1577
		Unknown,
		// Token: 0x0400062A RID: 1578
		Decimal,
		// Token: 0x0400062B RID: 1579
		I1 = 16,
		// Token: 0x0400062C RID: 1580
		UI1,
		// Token: 0x0400062D RID: 1581
		UI2,
		// Token: 0x0400062E RID: 1582
		UI4,
		// Token: 0x0400062F RID: 1583
		Int = 22,
		// Token: 0x04000630 RID: 1584
		UInt
	}
}
