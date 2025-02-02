using System;

namespace TaleWorlds.Library
{
	// Token: 0x0200005E RID: 94
	public static class ManagedDllFolder
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x000082B0 File Offset: 0x000064B0
		public static string Name
		{
			get
			{
				if (!string.IsNullOrEmpty(ManagedDllFolder._overridenFolder))
				{
					return ManagedDllFolder._overridenFolder;
				}
				if (ApplicationPlatform.CurrentPlatform == Platform.Orbis)
				{
					return "/app0/";
				}
				if (ApplicationPlatform.CurrentPlatform == Platform.Durango)
				{
					return "/";
				}
				return "";
			}
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x000082E5 File Offset: 0x000064E5
		public static void OverrideManagedDllFolder(string overridenFolder)
		{
			ManagedDllFolder._overridenFolder = overridenFolder;
		}

		// Token: 0x040000F9 RID: 249
		private static string _overridenFolder;
	}
}
