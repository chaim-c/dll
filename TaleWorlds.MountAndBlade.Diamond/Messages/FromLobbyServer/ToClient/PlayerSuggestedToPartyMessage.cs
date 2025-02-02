using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200004D RID: 77
	[MessageDescription("LobbyServer", "Client")]
	[Serializable]
	public class PlayerSuggestedToPartyMessage : Message
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00002F2A File Offset: 0x0000112A
		// (set) Token: 0x0600016A RID: 362 RVA: 0x00002F32 File Offset: 0x00001132
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00002F3B File Offset: 0x0000113B
		// (set) Token: 0x0600016C RID: 364 RVA: 0x00002F43 File Offset: 0x00001143
		[JsonProperty]
		public string PlayerName { get; private set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00002F4C File Offset: 0x0000114C
		// (set) Token: 0x0600016E RID: 366 RVA: 0x00002F54 File Offset: 0x00001154
		[JsonProperty]
		public PlayerId SuggestingPlayerId { get; private set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00002F5D File Offset: 0x0000115D
		// (set) Token: 0x06000170 RID: 368 RVA: 0x00002F65 File Offset: 0x00001165
		[JsonProperty]
		public string SuggestingPlayerName { get; private set; }

		// Token: 0x06000171 RID: 369 RVA: 0x00002F6E File Offset: 0x0000116E
		public PlayerSuggestedToPartyMessage()
		{
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00002F76 File Offset: 0x00001176
		public PlayerSuggestedToPartyMessage(PlayerId playerId, string playerName, PlayerId suggestingPlayerId, string suggestingPlayerName)
		{
			this.PlayerId = playerId;
			this.PlayerName = playerName;
			this.SuggestingPlayerId = suggestingPlayerId;
			this.SuggestingPlayerName = suggestingPlayerName;
		}
	}
}
