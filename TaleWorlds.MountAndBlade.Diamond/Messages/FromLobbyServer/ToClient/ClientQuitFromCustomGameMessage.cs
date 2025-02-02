using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000014 RID: 20
	[Serializable]
	public class ClientQuitFromCustomGameMessage : Message
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000056 RID: 86 RVA: 0x000023C4 File Offset: 0x000005C4
		// (set) Token: 0x06000057 RID: 87 RVA: 0x000023CC File Offset: 0x000005CC
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x06000058 RID: 88 RVA: 0x000023D5 File Offset: 0x000005D5
		public ClientQuitFromCustomGameMessage()
		{
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000023DD File Offset: 0x000005DD
		public ClientQuitFromCustomGameMessage(PlayerId playerId)
		{
			this.PlayerId = playerId;
		}
	}
}
