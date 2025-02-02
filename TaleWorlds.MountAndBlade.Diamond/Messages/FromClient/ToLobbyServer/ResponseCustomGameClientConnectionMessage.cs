using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000C5 RID: 197
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class ResponseCustomGameClientConnectionMessage : Message
	{
		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000394 RID: 916 RVA: 0x0000472A File Offset: 0x0000292A
		// (set) Token: 0x06000395 RID: 917 RVA: 0x00004732 File Offset: 0x00002932
		[JsonProperty]
		public PlayerJoinGameResponseDataFromHost[] PlayerJoinData { get; private set; }

		// Token: 0x06000396 RID: 918 RVA: 0x0000473B File Offset: 0x0000293B
		public ResponseCustomGameClientConnectionMessage()
		{
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00004743 File Offset: 0x00002943
		public ResponseCustomGameClientConnectionMessage(PlayerJoinGameResponseDataFromHost[] playerJoinData)
		{
			this.PlayerJoinData = playerJoinData;
		}
	}
}
