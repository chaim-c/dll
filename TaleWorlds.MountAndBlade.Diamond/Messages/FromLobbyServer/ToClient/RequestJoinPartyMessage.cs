using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000054 RID: 84
	[MessageDescription("LobbyServer", "Client")]
	[Serializable]
	public class RequestJoinPartyMessage : Message
	{
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600018C RID: 396 RVA: 0x0000309B File Offset: 0x0000129B
		// (set) Token: 0x0600018D RID: 397 RVA: 0x000030A3 File Offset: 0x000012A3
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600018E RID: 398 RVA: 0x000030AC File Offset: 0x000012AC
		// (set) Token: 0x0600018F RID: 399 RVA: 0x000030B4 File Offset: 0x000012B4
		[JsonProperty]
		public string PlayerName { get; private set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000190 RID: 400 RVA: 0x000030BD File Offset: 0x000012BD
		// (set) Token: 0x06000191 RID: 401 RVA: 0x000030C5 File Offset: 0x000012C5
		[JsonProperty]
		public PlayerId ViaPlayerId { get; private set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000192 RID: 402 RVA: 0x000030CE File Offset: 0x000012CE
		// (set) Token: 0x06000193 RID: 403 RVA: 0x000030D6 File Offset: 0x000012D6
		[JsonProperty]
		public string ViaPlayerName { get; private set; }

		// Token: 0x06000194 RID: 404 RVA: 0x000030DF File Offset: 0x000012DF
		public RequestJoinPartyMessage()
		{
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000030E7 File Offset: 0x000012E7
		public RequestJoinPartyMessage(PlayerId playerId, string playerName, PlayerId viaPlayerId, string viaPlayerName)
		{
			this.PlayerId = playerId;
			this.PlayerName = playerName;
			this.ViaPlayerId = viaPlayerId;
			this.ViaPlayerName = viaPlayerName;
		}
	}
}
