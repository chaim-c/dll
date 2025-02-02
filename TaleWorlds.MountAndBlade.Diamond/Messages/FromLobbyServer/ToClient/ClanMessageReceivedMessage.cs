using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000013 RID: 19
	[MessageDescription("LobbyServer", "Client")]
	[Serializable]
	public class ClanMessageReceivedMessage : Message
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002384 File Offset: 0x00000584
		// (set) Token: 0x06000051 RID: 81 RVA: 0x0000238C File Offset: 0x0000058C
		[JsonProperty]
		public string PlayerName { get; private set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002395 File Offset: 0x00000595
		// (set) Token: 0x06000053 RID: 83 RVA: 0x0000239D File Offset: 0x0000059D
		[JsonProperty]
		public string Message { get; private set; }

		// Token: 0x06000054 RID: 84 RVA: 0x000023A6 File Offset: 0x000005A6
		public ClanMessageReceivedMessage()
		{
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000023AE File Offset: 0x000005AE
		public ClanMessageReceivedMessage(string playerName, string message)
		{
			this.PlayerName = playerName;
			this.Message = message;
		}
	}
}
