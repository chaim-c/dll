using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x0200007F RID: 127
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class ChangeGameTypesMessage : Message
	{
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000272 RID: 626 RVA: 0x00003B3F File Offset: 0x00001D3F
		// (set) Token: 0x06000273 RID: 627 RVA: 0x00003B47 File Offset: 0x00001D47
		[JsonProperty]
		public string[] GameTypes { get; private set; }

		// Token: 0x06000274 RID: 628 RVA: 0x00003B50 File Offset: 0x00001D50
		public ChangeGameTypesMessage()
		{
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00003B58 File Offset: 0x00001D58
		public ChangeGameTypesMessage(string[] gameTypes)
		{
			this.GameTypes = gameTypes;
		}
	}
}
