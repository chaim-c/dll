using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000016 RID: 22
	[MessageDescription("LobbyServer", "Client")]
	[Serializable]
	public class CreateClanAnswerMessage : Message
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002414 File Offset: 0x00000614
		// (set) Token: 0x0600005F RID: 95 RVA: 0x0000241C File Offset: 0x0000061C
		[JsonProperty]
		public bool Successful { get; private set; }

		// Token: 0x06000060 RID: 96 RVA: 0x00002425 File Offset: 0x00000625
		public CreateClanAnswerMessage()
		{
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000242D File Offset: 0x0000062D
		public CreateClanAnswerMessage(bool successful)
		{
			this.Successful = successful;
		}
	}
}
