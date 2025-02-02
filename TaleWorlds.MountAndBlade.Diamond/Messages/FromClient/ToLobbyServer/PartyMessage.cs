using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000B5 RID: 181
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class PartyMessage : Message
	{
		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600033D RID: 829 RVA: 0x00004376 File Offset: 0x00002576
		// (set) Token: 0x0600033E RID: 830 RVA: 0x0000437E File Offset: 0x0000257E
		[JsonProperty]
		public string Message { get; private set; }

		// Token: 0x0600033F RID: 831 RVA: 0x00004387 File Offset: 0x00002587
		public PartyMessage()
		{
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000438F File Offset: 0x0000258F
		public PartyMessage(string message)
		{
			this.Message = message;
		}
	}
}
