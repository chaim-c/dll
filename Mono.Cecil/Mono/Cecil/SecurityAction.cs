using System;

namespace Mono.Cecil
{
	// Token: 0x0200009C RID: 156
	public enum SecurityAction : ushort
	{
		// Token: 0x040003FF RID: 1023
		Request = 1,
		// Token: 0x04000400 RID: 1024
		Demand,
		// Token: 0x04000401 RID: 1025
		Assert,
		// Token: 0x04000402 RID: 1026
		Deny,
		// Token: 0x04000403 RID: 1027
		PermitOnly,
		// Token: 0x04000404 RID: 1028
		LinkDemand,
		// Token: 0x04000405 RID: 1029
		InheritDemand,
		// Token: 0x04000406 RID: 1030
		RequestMinimum,
		// Token: 0x04000407 RID: 1031
		RequestOptional,
		// Token: 0x04000408 RID: 1032
		RequestRefuse,
		// Token: 0x04000409 RID: 1033
		PreJitGrant,
		// Token: 0x0400040A RID: 1034
		PreJitDeny,
		// Token: 0x0400040B RID: 1035
		NonCasDemand,
		// Token: 0x0400040C RID: 1036
		NonCasLinkDemand,
		// Token: 0x0400040D RID: 1037
		NonCasInheritance
	}
}
