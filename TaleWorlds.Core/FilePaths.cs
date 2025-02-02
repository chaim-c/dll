using System;
using TaleWorlds.Library;

namespace TaleWorlds.Core
{
	// Token: 0x02000058 RID: 88
	public static class FilePaths
	{
		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x0001723F File Offset: 0x0001543F
		public static PlatformDirectoryPath SavePath
		{
			get
			{
				return new PlatformDirectoryPath(PlatformFileType.User, "Game Saves");
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x0001724C File Offset: 0x0001544C
		public static PlatformDirectoryPath RecordingsPath
		{
			get
			{
				return new PlatformDirectoryPath(PlatformFileType.User, "Recordings");
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x00017259 File Offset: 0x00015459
		public static PlatformDirectoryPath StatisticsPath
		{
			get
			{
				return new PlatformDirectoryPath(PlatformFileType.User, "Statistics");
			}
		}

		// Token: 0x0400033E RID: 830
		public const string SaveDirectoryName = "Game Saves";

		// Token: 0x0400033F RID: 831
		public const string RecordingsDirectoryName = "Recordings";

		// Token: 0x04000340 RID: 832
		public const string StatisticsDirectoryName = "Statistics";
	}
}
