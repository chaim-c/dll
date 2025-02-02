using System;

namespace Mono.Cecil
{
	// Token: 0x020000AD RID: 173
	[Flags]
	public enum GenericParameterAttributes : ushort
	{
		// Token: 0x0400043C RID: 1084
		VarianceMask = 3,
		// Token: 0x0400043D RID: 1085
		NonVariant = 0,
		// Token: 0x0400043E RID: 1086
		Covariant = 1,
		// Token: 0x0400043F RID: 1087
		Contravariant = 2,
		// Token: 0x04000440 RID: 1088
		SpecialConstraintMask = 28,
		// Token: 0x04000441 RID: 1089
		ReferenceTypeConstraint = 4,
		// Token: 0x04000442 RID: 1090
		NotNullableValueTypeConstraint = 8,
		// Token: 0x04000443 RID: 1091
		DefaultConstructorConstraint = 16
	}
}
