using System;
using Newtonsoft.Json;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000109 RID: 265
	[Serializable]
	public class ClanHomeInfo
	{
		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x00007499 File Offset: 0x00005699
		// (set) Token: 0x06000593 RID: 1427 RVA: 0x000074A1 File Offset: 0x000056A1
		[JsonProperty]
		public bool IsInClan { get; private set; }

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x000074AA File Offset: 0x000056AA
		// (set) Token: 0x06000595 RID: 1429 RVA: 0x000074B2 File Offset: 0x000056B2
		[JsonProperty]
		public bool CanCreateClan { get; private set; }

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x000074BB File Offset: 0x000056BB
		// (set) Token: 0x06000597 RID: 1431 RVA: 0x000074C3 File Offset: 0x000056C3
		[JsonProperty]
		public ClanInfo ClanInfo { get; private set; }

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x000074CC File Offset: 0x000056CC
		// (set) Token: 0x06000599 RID: 1433 RVA: 0x000074D4 File Offset: 0x000056D4
		[JsonProperty]
		public NotEnoughPlayersInfo NotEnoughPlayersInfo { get; private set; }

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x000074DD File Offset: 0x000056DD
		// (set) Token: 0x0600059B RID: 1435 RVA: 0x000074E5 File Offset: 0x000056E5
		[JsonProperty]
		public PlayerNotEligibleInfo[] PlayerNotEligibleInfos { get; private set; }

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x000074EE File Offset: 0x000056EE
		// (set) Token: 0x0600059D RID: 1437 RVA: 0x000074F6 File Offset: 0x000056F6
		[JsonProperty]
		public ClanPlayerInfo[] ClanPlayerInfos { get; private set; }

		// Token: 0x0600059E RID: 1438 RVA: 0x000074FF File Offset: 0x000056FF
		public ClanHomeInfo(bool isInClan, bool canCreateClan, ClanInfo clanInfo, NotEnoughPlayersInfo notEnoughPlayersInfo, PlayerNotEligibleInfo[] playerNotEligibleInfos, ClanPlayerInfo[] clanPlayerInfos)
		{
			this.IsInClan = isInClan;
			this.CanCreateClan = canCreateClan;
			this.ClanInfo = clanInfo;
			this.NotEnoughPlayersInfo = notEnoughPlayersInfo;
			this.PlayerNotEligibleInfos = playerNotEligibleInfos;
			this.ClanPlayerInfos = clanPlayerInfos;
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x00007534 File Offset: 0x00005734
		public static ClanHomeInfo CreateInClanInfo(ClanInfo clanInfo, ClanPlayerInfo[] clanPlayerInfos)
		{
			return new ClanHomeInfo(true, false, clanInfo, null, null, clanPlayerInfos);
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00007541 File Offset: 0x00005741
		public static ClanHomeInfo CreateCanCreateClanInfo()
		{
			return new ClanHomeInfo(false, true, null, null, null, null);
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x0000754E File Offset: 0x0000574E
		public static ClanHomeInfo CreateCantCreateClanInfo(NotEnoughPlayersInfo notEnoughPlayersInfo, PlayerNotEligibleInfo[] playerNotEligibleInfos)
		{
			return new ClanHomeInfo(false, false, null, notEnoughPlayersInfo, playerNotEligibleInfos, null);
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0000755B File Offset: 0x0000575B
		public static ClanHomeInfo CreateInvalidStateClanInfo()
		{
			return new ClanHomeInfo(false, false, null, null, null, null);
		}
	}
}
