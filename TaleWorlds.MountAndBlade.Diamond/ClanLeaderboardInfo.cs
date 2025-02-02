using System;
using Newtonsoft.Json;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200010F RID: 271
	[Serializable]
	public class ClanLeaderboardInfo
	{
		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x0000774D File Offset: 0x0000594D
		// (set) Token: 0x060005C9 RID: 1481 RVA: 0x00007754 File Offset: 0x00005954
		public static ClanLeaderboardInfo Empty { get; private set; } = new ClanLeaderboardInfo(new ClanLeaderboardEntry[0]);

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x0000776E File Offset: 0x0000596E
		// (set) Token: 0x060005CC RID: 1484 RVA: 0x00007776 File Offset: 0x00005976
		[JsonProperty]
		public ClanLeaderboardEntry[] ClanEntries { get; private set; }

		// Token: 0x060005CD RID: 1485 RVA: 0x0000777F File Offset: 0x0000597F
		public ClanLeaderboardInfo(ClanLeaderboardEntry[] entries)
		{
			this.ClanEntries = entries;
		}
	}
}
