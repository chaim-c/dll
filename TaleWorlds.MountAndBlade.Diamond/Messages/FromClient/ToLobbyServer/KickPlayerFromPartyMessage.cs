using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000B3 RID: 179
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class KickPlayerFromPartyMessage : Message
	{
		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000335 RID: 821 RVA: 0x00004326 File Offset: 0x00002526
		// (set) Token: 0x06000336 RID: 822 RVA: 0x0000432E File Offset: 0x0000252E
		[JsonProperty]
		public PlayerId KickedPlayerId { get; private set; }

		// Token: 0x06000337 RID: 823 RVA: 0x00004337 File Offset: 0x00002537
		public KickPlayerFromPartyMessage()
		{
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000433F File Offset: 0x0000253F
		public KickPlayerFromPartyMessage(PlayerId kickedPlayerId)
		{
			this.KickedPlayerId = kickedPlayerId;
		}
	}
}
