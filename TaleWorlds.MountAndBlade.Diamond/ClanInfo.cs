using System;
using Newtonsoft.Json;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200010E RID: 270
	[Serializable]
	public class ClanInfo
	{
		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x00007641 File Offset: 0x00005841
		// (set) Token: 0x060005B7 RID: 1463 RVA: 0x00007649 File Offset: 0x00005849
		[JsonProperty]
		public Guid ClanId { get; private set; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x00007652 File Offset: 0x00005852
		// (set) Token: 0x060005B9 RID: 1465 RVA: 0x0000765A File Offset: 0x0000585A
		[JsonProperty]
		public string Name { get; private set; }

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x00007663 File Offset: 0x00005863
		// (set) Token: 0x060005BB RID: 1467 RVA: 0x0000766B File Offset: 0x0000586B
		[JsonProperty]
		public string Tag { get; private set; }

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x00007674 File Offset: 0x00005874
		// (set) Token: 0x060005BD RID: 1469 RVA: 0x0000767C File Offset: 0x0000587C
		[JsonProperty]
		public string Faction { get; private set; }

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x00007685 File Offset: 0x00005885
		// (set) Token: 0x060005BF RID: 1471 RVA: 0x0000768D File Offset: 0x0000588D
		[JsonProperty]
		public string Sigil { get; private set; }

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x00007696 File Offset: 0x00005896
		// (set) Token: 0x060005C1 RID: 1473 RVA: 0x0000769E File Offset: 0x0000589E
		[JsonProperty]
		public string InformationText { get; private set; }

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060005C2 RID: 1474 RVA: 0x000076A7 File Offset: 0x000058A7
		// (set) Token: 0x060005C3 RID: 1475 RVA: 0x000076AF File Offset: 0x000058AF
		[JsonProperty]
		public ClanPlayer[] Players { get; private set; }

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x000076B8 File Offset: 0x000058B8
		// (set) Token: 0x060005C5 RID: 1477 RVA: 0x000076C0 File Offset: 0x000058C0
		[JsonProperty]
		public ClanAnnouncement[] Announcements { get; private set; }

		// Token: 0x060005C6 RID: 1478 RVA: 0x000076CC File Offset: 0x000058CC
		public ClanInfo(Guid clanId, string name, string tag, string faction, string sigil, string information, ClanPlayer[] players, ClanAnnouncement[] announcements)
		{
			this.ClanId = clanId;
			this.Name = name;
			this.Tag = tag;
			this.Faction = faction;
			this.Sigil = sigil;
			this.Players = players;
			this.InformationText = information;
			this.Announcements = announcements;
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0000771C File Offset: 0x0000591C
		public static ClanInfo CreateUnavailableClanInfo()
		{
			return new ClanInfo(Guid.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, new ClanPlayer[0], new ClanAnnouncement[0]);
		}
	}
}
