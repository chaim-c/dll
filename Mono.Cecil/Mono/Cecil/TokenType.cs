using System;

namespace Mono.Cecil
{
	// Token: 0x02000041 RID: 65
	public enum TokenType : uint
	{
		// Token: 0x0400030F RID: 783
		Module,
		// Token: 0x04000310 RID: 784
		TypeRef = 16777216U,
		// Token: 0x04000311 RID: 785
		TypeDef = 33554432U,
		// Token: 0x04000312 RID: 786
		Field = 67108864U,
		// Token: 0x04000313 RID: 787
		Method = 100663296U,
		// Token: 0x04000314 RID: 788
		Param = 134217728U,
		// Token: 0x04000315 RID: 789
		InterfaceImpl = 150994944U,
		// Token: 0x04000316 RID: 790
		MemberRef = 167772160U,
		// Token: 0x04000317 RID: 791
		CustomAttribute = 201326592U,
		// Token: 0x04000318 RID: 792
		Permission = 234881024U,
		// Token: 0x04000319 RID: 793
		Signature = 285212672U,
		// Token: 0x0400031A RID: 794
		Event = 335544320U,
		// Token: 0x0400031B RID: 795
		Property = 385875968U,
		// Token: 0x0400031C RID: 796
		ModuleRef = 436207616U,
		// Token: 0x0400031D RID: 797
		TypeSpec = 452984832U,
		// Token: 0x0400031E RID: 798
		Assembly = 536870912U,
		// Token: 0x0400031F RID: 799
		AssemblyRef = 587202560U,
		// Token: 0x04000320 RID: 800
		File = 637534208U,
		// Token: 0x04000321 RID: 801
		ExportedType = 654311424U,
		// Token: 0x04000322 RID: 802
		ManifestResource = 671088640U,
		// Token: 0x04000323 RID: 803
		GenericParam = 704643072U,
		// Token: 0x04000324 RID: 804
		MethodSpec = 721420288U,
		// Token: 0x04000325 RID: 805
		String = 1879048192U
	}
}
