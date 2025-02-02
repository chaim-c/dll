using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000B2 RID: 178
	[MessageDescription("LobbyServer", "Client")]
	[Serializable]
	public class KickFromClanMessage : Message
	{
		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000331 RID: 817 RVA: 0x000042FE File Offset: 0x000024FE
		// (set) Token: 0x06000332 RID: 818 RVA: 0x00004306 File Offset: 0x00002506
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x06000333 RID: 819 RVA: 0x0000430F File Offset: 0x0000250F
		public KickFromClanMessage()
		{
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00004317 File Offset: 0x00002517
		public KickFromClanMessage(PlayerId playerId)
		{
			this.PlayerId = playerId;
		}
	}
}
