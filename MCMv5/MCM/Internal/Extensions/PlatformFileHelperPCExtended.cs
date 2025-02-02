using System;
using System.Runtime.CompilerServices;
using HarmonyLib.BUTR.Extensions;
using TaleWorlds.Library;

namespace MCM.Internal.Extensions
{
	// Token: 0x02000011 RID: 17
	internal static class PlatformFileHelperPCExtended
	{
		// Token: 0x06000058 RID: 88 RVA: 0x000034D8 File Offset: 0x000016D8
		[NullableContext(2)]
		public static string GetDirectoryFullPath(PlatformDirectoryPath directoryPath)
		{
			if (PlatformFileHelperPCExtended.GetPlatformFileHelper != null && PlatformFileHelperPCExtended.GetDirectoryFullPathMethod != null)
			{
				object obj = PlatformFileHelperPCExtended.GetPlatformFileHelper();
				if (obj != null)
				{
					return PlatformFileHelperPCExtended.GetDirectoryFullPathMethod(obj, directoryPath);
				}
			}
			return null;
		}

		// Token: 0x04000015 RID: 21
		[Nullable(2)]
		private static PlatformFileHelperPCExtended.GetDirectoryFullPathDelegate GetDirectoryFullPathMethod = AccessTools2.GetDelegate<PlatformFileHelperPCExtended.GetDirectoryFullPathDelegate>("TaleWorlds.Library.PlatformFileHelperPC:GetDirectoryFullPath", null, null, true);

		// Token: 0x04000016 RID: 22
		[Nullable(2)]
		private static PlatformFileHelperPCExtended.GetPlatformFileHelperDelegate GetPlatformFileHelper = AccessTools2.GetPropertyGetterDelegate<PlatformFileHelperPCExtended.GetPlatformFileHelperDelegate>("TaleWorlds.Library.Common:PlatformFileHelper", true);

		// Token: 0x02000168 RID: 360
		// (Invoke) Token: 0x060009DF RID: 2527
		private delegate string GetDirectoryFullPathDelegate(object instance, PlatformDirectoryPath directoryPath);

		// Token: 0x02000169 RID: 361
		// (Invoke) Token: 0x060009E3 RID: 2531
		private delegate object GetPlatformFileHelperDelegate();
	}
}
