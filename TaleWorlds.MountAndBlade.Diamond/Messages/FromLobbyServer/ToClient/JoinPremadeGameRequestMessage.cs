using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200003D RID: 61
	[MessageDescription("LobbyServer", "Client")]
	[Serializable]
	public class JoinPremadeGameRequestMessage : Message
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00002BDF File Offset: 0x00000DDF
		// (set) Token: 0x0600011E RID: 286 RVA: 0x00002BE7 File Offset: 0x00000DE7
		[JsonProperty]
		public Guid ChallengerPartyId { get; private set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00002BF0 File Offset: 0x00000DF0
		// (set) Token: 0x06000120 RID: 288 RVA: 0x00002BF8 File Offset: 0x00000DF8
		[JsonProperty]
		public string ClanName { get; private set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00002C01 File Offset: 0x00000E01
		// (set) Token: 0x06000122 RID: 290 RVA: 0x00002C09 File Offset: 0x00000E09
		[JsonProperty]
		public string Sigil { get; private set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00002C12 File Offset: 0x00000E12
		// (set) Token: 0x06000124 RID: 292 RVA: 0x00002C1A File Offset: 0x00000E1A
		[JsonProperty]
		public PlayerId[] ChallengerPlayers { get; private set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00002C23 File Offset: 0x00000E23
		// (set) Token: 0x06000126 RID: 294 RVA: 0x00002C2B File Offset: 0x00000E2B
		[JsonProperty]
		public PlayerId ChallengerPartyLeaderId { get; private set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00002C34 File Offset: 0x00000E34
		// (set) Token: 0x06000128 RID: 296 RVA: 0x00002C3C File Offset: 0x00000E3C
		[JsonProperty]
		public PremadeGameType PremadeGameType { get; private set; }

		// Token: 0x06000129 RID: 297 RVA: 0x00002C45 File Offset: 0x00000E45
		public JoinPremadeGameRequestMessage()
		{
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00002C4D File Offset: 0x00000E4D
		public JoinPremadeGameRequestMessage(Guid challengerPartyId, string clanName, string sigil, PlayerId[] challengerPlayers, PlayerId challengerPartyLeaderId, PremadeGameType premadeGameType)
		{
			this.ChallengerPartyId = challengerPartyId;
			this.ClanName = clanName;
			this.Sigil = sigil;
			this.ChallengerPlayers = challengerPlayers;
			this.ChallengerPartyLeaderId = challengerPartyLeaderId;
			this.PremadeGameType = premadeGameType;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00002C82 File Offset: 0x00000E82
		public static JoinPremadeGameRequestMessage CreateClanGameRequest(Guid challengerPartyId, string clanName, string sigil, PlayerId[] challengerPlayers)
		{
			return new JoinPremadeGameRequestMessage(challengerPartyId, clanName, sigil, challengerPlayers, PlayerId.Empty, PremadeGameType.Clan);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00002C93 File Offset: 0x00000E93
		public static JoinPremadeGameRequestMessage CreatePracticeGameRequest(Guid challengerPartyId, PlayerId leaderId, PlayerId[] challengerPlayers)
		{
			return new JoinPremadeGameRequestMessage(challengerPartyId, null, null, challengerPlayers, leaderId, PremadeGameType.Practice);
		}
	}
}
