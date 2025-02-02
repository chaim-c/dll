using System;
using Newtonsoft.Json;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200010C RID: 268
	[Serializable]
	public class ClanPlayerInfo
	{
		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x000075D8 File Offset: 0x000057D8
		// (set) Token: 0x060005AE RID: 1454 RVA: 0x000075E0 File Offset: 0x000057E0
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x000075E9 File Offset: 0x000057E9
		// (set) Token: 0x060005B0 RID: 1456 RVA: 0x000075F1 File Offset: 0x000057F1
		[JsonProperty]
		public string PlayerName { get; private set; }

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x000075FA File Offset: 0x000057FA
		// (set) Token: 0x060005B2 RID: 1458 RVA: 0x00007602 File Offset: 0x00005802
		[JsonProperty]
		public AnotherPlayerState State { get; private set; }

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x0000760B File Offset: 0x0000580B
		// (set) Token: 0x060005B4 RID: 1460 RVA: 0x00007613 File Offset: 0x00005813
		[JsonProperty]
		public string ActiveBadgeId { get; private set; }

		// Token: 0x060005B5 RID: 1461 RVA: 0x0000761C File Offset: 0x0000581C
		public ClanPlayerInfo(PlayerId playerId, string playerName, AnotherPlayerState anotherPlayerState, string activeBadgeId)
		{
			this.PlayerId = playerId;
			this.PlayerName = playerName;
			this.ActiveBadgeId = activeBadgeId;
			this.State = anotherPlayerState;
		}
	}
}
