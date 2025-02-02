using System;
using Newtonsoft.Json;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000110 RID: 272
	[Serializable]
	public class ClanLeaderboardEntry
	{
		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x0000778E File Offset: 0x0000598E
		// (set) Token: 0x060005CF RID: 1487 RVA: 0x00007796 File Offset: 0x00005996
		public Guid ClanId { get; private set; }

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x0000779F File Offset: 0x0000599F
		// (set) Token: 0x060005D1 RID: 1489 RVA: 0x000077A7 File Offset: 0x000059A7
		public string Name { get; private set; }

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x000077B0 File Offset: 0x000059B0
		// (set) Token: 0x060005D3 RID: 1491 RVA: 0x000077B8 File Offset: 0x000059B8
		public string Tag { get; private set; }

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x000077C1 File Offset: 0x000059C1
		// (set) Token: 0x060005D5 RID: 1493 RVA: 0x000077C9 File Offset: 0x000059C9
		public string Sigil { get; private set; }

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x000077D2 File Offset: 0x000059D2
		// (set) Token: 0x060005D7 RID: 1495 RVA: 0x000077DA File Offset: 0x000059DA
		public int WinCount { get; private set; }

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x000077E3 File Offset: 0x000059E3
		// (set) Token: 0x060005D9 RID: 1497 RVA: 0x000077EB File Offset: 0x000059EB
		public int LossCount { get; private set; }

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x000077F4 File Offset: 0x000059F4
		// (set) Token: 0x060005DB RID: 1499 RVA: 0x000077FC File Offset: 0x000059FC
		public float Score { get; private set; }

		// Token: 0x060005DC RID: 1500 RVA: 0x00007805 File Offset: 0x00005A05
		[JsonConstructor]
		public ClanLeaderboardEntry(Guid clanId, string name, string tag, string sigil, int winCount, int lossCount, float score)
		{
			this.ClanId = clanId;
			this.Name = name;
			this.Tag = tag;
			this.Sigil = sigil;
			this.WinCount = winCount;
			this.LossCount = lossCount;
			this.Score = score;
		}
	}
}
