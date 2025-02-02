using System;
using System.IO;
using System.Reflection;

namespace TaleWorlds.Library
{
	// Token: 0x02000031 RID: 49
	public static class BasePath
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00006AF4 File Offset: 0x00004CF4
		public static string Name
		{
			get
			{
				if (ApplicationPlatform.CurrentEngine == EngineType.UnrealEngine)
				{
					return Path.GetFullPath(Path.GetDirectoryName(typeof(BasePath).Assembly.Location) + "/../../");
				}
				if (ApplicationPlatform.CurrentPlatform == Platform.Orbis)
				{
					return "/app0/";
				}
				if (ApplicationPlatform.CurrentPlatform == Platform.Durango)
				{
					return "/";
				}
				if (ApplicationPlatform.CurrentPlatform == Platform.Web)
				{
					return Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "/../../";
				}
				return "../../";
			}
		}
	}
}
