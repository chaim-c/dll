using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000015 RID: 21
	[MessageDescription("LobbyServer", "Client")]
	[Serializable]
	public class ClientWantsToConnectCustomGameMessage : Message
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600005A RID: 90 RVA: 0x000023EC File Offset: 0x000005EC
		// (set) Token: 0x0600005B RID: 91 RVA: 0x000023F4 File Offset: 0x000005F4
		[JsonProperty]
		public PlayerJoinGameData[] PlayerJoinGameData { get; private set; }

		// Token: 0x0600005C RID: 92 RVA: 0x000023FD File Offset: 0x000005FD
		public ClientWantsToConnectCustomGameMessage()
		{
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002405 File Offset: 0x00000605
		public ClientWantsToConnectCustomGameMessage(PlayerJoinGameData[] playerJoinGameData)
		{
			this.PlayerJoinGameData = playerJoinGameData;
		}
	}
}
