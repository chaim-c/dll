using System;

namespace Mono.Cecil.Metadata
{
	// Token: 0x02000033 RID: 51
	internal enum ElementType : byte
	{
		// Token: 0x04000293 RID: 659
		None,
		// Token: 0x04000294 RID: 660
		Void,
		// Token: 0x04000295 RID: 661
		Boolean,
		// Token: 0x04000296 RID: 662
		Char,
		// Token: 0x04000297 RID: 663
		I1,
		// Token: 0x04000298 RID: 664
		U1,
		// Token: 0x04000299 RID: 665
		I2,
		// Token: 0x0400029A RID: 666
		U2,
		// Token: 0x0400029B RID: 667
		I4,
		// Token: 0x0400029C RID: 668
		U4,
		// Token: 0x0400029D RID: 669
		I8,
		// Token: 0x0400029E RID: 670
		U8,
		// Token: 0x0400029F RID: 671
		R4,
		// Token: 0x040002A0 RID: 672
		R8,
		// Token: 0x040002A1 RID: 673
		String,
		// Token: 0x040002A2 RID: 674
		Ptr,
		// Token: 0x040002A3 RID: 675
		ByRef,
		// Token: 0x040002A4 RID: 676
		ValueType,
		// Token: 0x040002A5 RID: 677
		Class,
		// Token: 0x040002A6 RID: 678
		Var,
		// Token: 0x040002A7 RID: 679
		Array,
		// Token: 0x040002A8 RID: 680
		GenericInst,
		// Token: 0x040002A9 RID: 681
		TypedByRef,
		// Token: 0x040002AA RID: 682
		I = 24,
		// Token: 0x040002AB RID: 683
		U,
		// Token: 0x040002AC RID: 684
		FnPtr = 27,
		// Token: 0x040002AD RID: 685
		Object,
		// Token: 0x040002AE RID: 686
		SzArray,
		// Token: 0x040002AF RID: 687
		MVar,
		// Token: 0x040002B0 RID: 688
		CModReqD,
		// Token: 0x040002B1 RID: 689
		CModOpt,
		// Token: 0x040002B2 RID: 690
		Internal,
		// Token: 0x040002B3 RID: 691
		Modifier = 64,
		// Token: 0x040002B4 RID: 692
		Sentinel,
		// Token: 0x040002B5 RID: 693
		Pinned = 69,
		// Token: 0x040002B6 RID: 694
		Type = 80,
		// Token: 0x040002B7 RID: 695
		Boxed,
		// Token: 0x040002B8 RID: 696
		Enum = 85
	}
}
