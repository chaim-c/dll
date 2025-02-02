using System;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000132 RID: 306
	[Serializable]
	public class PlayerJoinGameResponseDataFromHost
	{
		// Token: 0x17000273 RID: 627
		// (get) Token: 0x060007DD RID: 2013 RVA: 0x0000C028 File Offset: 0x0000A228
		// (set) Token: 0x060007DE RID: 2014 RVA: 0x0000C030 File Offset: 0x0000A230
		public PlayerId PlayerId { get; set; }

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x060007DF RID: 2015 RVA: 0x0000C039 File Offset: 0x0000A239
		// (set) Token: 0x060007E0 RID: 2016 RVA: 0x0000C041 File Offset: 0x0000A241
		public int PeerIndex { get; set; }

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x060007E1 RID: 2017 RVA: 0x0000C04A File Offset: 0x0000A24A
		// (set) Token: 0x060007E2 RID: 2018 RVA: 0x0000C052 File Offset: 0x0000A252
		public int SessionKey { get; set; }

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x060007E3 RID: 2019 RVA: 0x0000C05B File Offset: 0x0000A25B
		// (set) Token: 0x060007E4 RID: 2020 RVA: 0x0000C063 File Offset: 0x0000A263
		public bool IsAdmin { get; set; }

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x060007E5 RID: 2021 RVA: 0x0000C06C File Offset: 0x0000A26C
		// (set) Token: 0x060007E6 RID: 2022 RVA: 0x0000C074 File Offset: 0x0000A274
		public CustomGameJoinResponse CustomGameJoinResponse { get; set; }
	}
}
