using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000B6 RID: 182
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class PromotePlayerToPartyLeaderMessage : Message
	{
		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000341 RID: 833 RVA: 0x0000439E File Offset: 0x0000259E
		// (set) Token: 0x06000342 RID: 834 RVA: 0x000043A6 File Offset: 0x000025A6
		[JsonProperty]
		public PlayerId PromotedPlayerId { get; private set; }

		// Token: 0x06000343 RID: 835 RVA: 0x000043AF File Offset: 0x000025AF
		public PromotePlayerToPartyLeaderMessage()
		{
		}

		// Token: 0x06000344 RID: 836 RVA: 0x000043B7 File Offset: 0x000025B7
		public PromotePlayerToPartyLeaderMessage(PlayerId promotedPlayerId)
		{
			this.PromotedPlayerId = promotedPlayerId;
		}
	}
}
