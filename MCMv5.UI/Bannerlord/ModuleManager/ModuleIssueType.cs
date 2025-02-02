using System;

namespace Bannerlord.ModuleManager
{
	// Token: 0x0200004E RID: 78
	internal enum ModuleIssueType
	{
		// Token: 0x040000E0 RID: 224
		NONE,
		// Token: 0x040000E1 RID: 225
		Missing,
		// Token: 0x040000E2 RID: 226
		MissingDependencies,
		// Token: 0x040000E3 RID: 227
		DependencyMissingDependencies,
		// Token: 0x040000E4 RID: 228
		DependencyValidationError,
		// Token: 0x040000E5 RID: 229
		VersionMismatchLessThanOrEqual,
		// Token: 0x040000E6 RID: 230
		VersionMismatchLessThan,
		// Token: 0x040000E7 RID: 231
		VersionMismatchGreaterThan,
		// Token: 0x040000E8 RID: 232
		Incompatible,
		// Token: 0x040000E9 RID: 233
		DependencyConflictDependentAndIncompatible,
		// Token: 0x040000EA RID: 234
		DependencyConflictDependentLoadBeforeAndAfter,
		// Token: 0x040000EB RID: 235
		DependencyConflictCircular,
		// Token: 0x040000EC RID: 236
		DependencyNotLoadedBeforeThis,
		// Token: 0x040000ED RID: 237
		DependencyNotLoadedAfterThis
	}
}
