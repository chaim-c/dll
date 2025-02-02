using System;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000042 RID: 66
	public static class EngineFilePaths
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x000034BF File Offset: 0x000016BF
		public static PlatformDirectoryPath ConfigsPath
		{
			get
			{
				return new PlatformDirectoryPath(PlatformFileType.User, "Configs");
			}
		}

		// Token: 0x04000052 RID: 82
		public const string ConfigsDirectoryName = "Configs";
	}
}
