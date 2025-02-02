using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000043 RID: 67
	[MessageDescription("LobbyServer", "Client")]
	[Serializable]
	public class PartyMessageReceivedMessage : Message
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00002D28 File Offset: 0x00000F28
		// (set) Token: 0x0600013C RID: 316 RVA: 0x00002D30 File Offset: 0x00000F30
		[JsonProperty]
		public string PlayerName { get; private set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00002D39 File Offset: 0x00000F39
		// (set) Token: 0x0600013E RID: 318 RVA: 0x00002D41 File Offset: 0x00000F41
		[JsonProperty]
		public string Message { get; private set; }

		// Token: 0x0600013F RID: 319 RVA: 0x00002D4A File Offset: 0x00000F4A
		public PartyMessageReceivedMessage()
		{
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00002D52 File Offset: 0x00000F52
		public PartyMessageReceivedMessage(string playerName, string message)
		{
			this.PlayerName = playerName;
			this.Message = message;
		}
	}
}
