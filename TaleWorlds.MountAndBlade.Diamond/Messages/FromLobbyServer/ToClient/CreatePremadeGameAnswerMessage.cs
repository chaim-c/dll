using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000017 RID: 23
	[MessageDescription("LobbyServer", "Client")]
	[Serializable]
	public class CreatePremadeGameAnswerMessage : Message
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000062 RID: 98 RVA: 0x0000243C File Offset: 0x0000063C
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00002444 File Offset: 0x00000644
		[JsonProperty]
		public bool Successful { get; private set; }

		// Token: 0x06000064 RID: 100 RVA: 0x0000244D File Offset: 0x0000064D
		public CreatePremadeGameAnswerMessage()
		{
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002455 File Offset: 0x00000655
		public CreatePremadeGameAnswerMessage(bool successful)
		{
			this.Successful = successful;
		}
	}
}
