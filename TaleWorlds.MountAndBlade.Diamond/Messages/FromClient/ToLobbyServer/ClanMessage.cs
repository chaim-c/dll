using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x02000086 RID: 134
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class ClanMessage : Message
	{
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000290 RID: 656 RVA: 0x00003C6F File Offset: 0x00001E6F
		// (set) Token: 0x06000291 RID: 657 RVA: 0x00003C77 File Offset: 0x00001E77
		[JsonProperty]
		public string Message { get; private set; }

		// Token: 0x06000292 RID: 658 RVA: 0x00003C80 File Offset: 0x00001E80
		public ClanMessage()
		{
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00003C88 File Offset: 0x00001E88
		public ClanMessage(string message)
		{
			this.Message = message;
		}
	}
}
