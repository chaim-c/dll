using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200004F RID: 79
	[MessageDescription("LobbyServer", "Client")]
	[Serializable]
	public class PromoteToClanLeaderMessage : Message
	{
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00002FC3 File Offset: 0x000011C3
		// (set) Token: 0x06000178 RID: 376 RVA: 0x00002FCB File Offset: 0x000011CB
		[JsonProperty]
		public PlayerId PromotedPlayerId { get; private set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00002FD4 File Offset: 0x000011D4
		// (set) Token: 0x0600017A RID: 378 RVA: 0x00002FDC File Offset: 0x000011DC
		[JsonProperty]
		public bool DontUseNameForUnknownPlayer { get; private set; }

		// Token: 0x0600017B RID: 379 RVA: 0x00002FE5 File Offset: 0x000011E5
		public PromoteToClanLeaderMessage()
		{
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00002FED File Offset: 0x000011ED
		public PromoteToClanLeaderMessage(PlayerId promotedPlayerId, bool dontUseNameForUnknownPlayer)
		{
			this.PromotedPlayerId = promotedPlayerId;
			this.DontUseNameForUnknownPlayer = dontUseNameForUnknownPlayer;
		}
	}
}
