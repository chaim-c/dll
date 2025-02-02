using System;
using Newtonsoft.Json;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000111 RID: 273
	[Serializable]
	public class ClanPlayer
	{
		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060005DD RID: 1501 RVA: 0x00007842 File Offset: 0x00005A42
		// (set) Token: 0x060005DE RID: 1502 RVA: 0x0000784A File Offset: 0x00005A4A
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x00007853 File Offset: 0x00005A53
		// (set) Token: 0x060005E0 RID: 1504 RVA: 0x0000785B File Offset: 0x00005A5B
		[JsonProperty]
		public Guid ClanId { get; private set; }

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x00007864 File Offset: 0x00005A64
		// (set) Token: 0x060005E2 RID: 1506 RVA: 0x0000786C File Offset: 0x00005A6C
		[JsonProperty]
		public ClanPlayerRole Role { get; private set; }

		// Token: 0x060005E3 RID: 1507 RVA: 0x00007875 File Offset: 0x00005A75
		public ClanPlayer(PlayerId playerId, Guid clanId, ClanPlayerRole role)
		{
			this.PlayerId = playerId;
			this.ClanId = clanId;
			this.Role = role;
		}
	}
}
