using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200003C RID: 60
	[MessageDescription("LobbyServer", "Client")]
	[Serializable]
	public class JoinPremadeGameAnswerMessage : Message
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00002BB7 File Offset: 0x00000DB7
		// (set) Token: 0x0600011A RID: 282 RVA: 0x00002BBF File Offset: 0x00000DBF
		[JsonProperty]
		public bool Successful { get; private set; }

		// Token: 0x0600011B RID: 283 RVA: 0x00002BC8 File Offset: 0x00000DC8
		public JoinPremadeGameAnswerMessage()
		{
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00002BD0 File Offset: 0x00000DD0
		public JoinPremadeGameAnswerMessage(bool successful)
		{
			this.Successful = successful;
		}
	}
}
