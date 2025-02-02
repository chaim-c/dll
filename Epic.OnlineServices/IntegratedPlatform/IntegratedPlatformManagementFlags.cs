using System;

namespace Epic.OnlineServices.IntegratedPlatform
{
	// Token: 0x02000456 RID: 1110
	[Flags]
	public enum IntegratedPlatformManagementFlags
	{
		// Token: 0x04000C96 RID: 3222
		Disabled = 1,
		// Token: 0x04000C97 RID: 3223
		LibraryManagedByApplication = 2,
		// Token: 0x04000C98 RID: 3224
		LibraryManagedBySDK = 4,
		// Token: 0x04000C99 RID: 3225
		DisablePresenceMirroring = 8,
		// Token: 0x04000C9A RID: 3226
		DisableSDKManagedSessions = 16,
		// Token: 0x04000C9B RID: 3227
		PreferEOSIdentity = 32,
		// Token: 0x04000C9C RID: 3228
		PreferIntegratedIdentity = 64
	}
}
