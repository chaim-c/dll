using System;

namespace TaleWorlds.Library
{
	// Token: 0x02000083 RID: 131
	public enum SaveResult
	{
		// Token: 0x04000156 RID: 342
		Success,
		// Token: 0x04000157 RID: 343
		NoSpace,
		// Token: 0x04000158 RID: 344
		Corrupted,
		// Token: 0x04000159 RID: 345
		GeneralFailure,
		// Token: 0x0400015A RID: 346
		FileDriverFailure,
		// Token: 0x0400015B RID: 347
		PlatformFileHelperFailure,
		// Token: 0x0400015C RID: 348
		ConfigFileFailure,
		// Token: 0x0400015D RID: 349
		SaveLimitReached
	}
}
