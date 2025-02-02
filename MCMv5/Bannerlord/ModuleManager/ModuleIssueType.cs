using System;

namespace Bannerlord.ModuleManager
{
	// Token: 0x0200013B RID: 315
	internal enum ModuleIssueType
	{
		// Token: 0x0400026D RID: 621
		NONE,
		// Token: 0x0400026E RID: 622
		Missing,
		// Token: 0x0400026F RID: 623
		MissingDependencies,
		// Token: 0x04000270 RID: 624
		DependencyMissingDependencies,
		// Token: 0x04000271 RID: 625
		DependencyValidationError,
		// Token: 0x04000272 RID: 626
		VersionMismatchLessThanOrEqual,
		// Token: 0x04000273 RID: 627
		VersionMismatchLessThan,
		// Token: 0x04000274 RID: 628
		VersionMismatchGreaterThan,
		// Token: 0x04000275 RID: 629
		Incompatible,
		// Token: 0x04000276 RID: 630
		DependencyConflictDependentAndIncompatible,
		// Token: 0x04000277 RID: 631
		DependencyConflictDependentLoadBeforeAndAfter,
		// Token: 0x04000278 RID: 632
		DependencyConflictCircular,
		// Token: 0x04000279 RID: 633
		DependencyNotLoadedBeforeThis,
		// Token: 0x0400027A RID: 634
		DependencyNotLoadedAfterThis
	}
}
