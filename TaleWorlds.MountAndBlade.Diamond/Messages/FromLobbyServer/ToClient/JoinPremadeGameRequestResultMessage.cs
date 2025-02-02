using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200003E RID: 62
	[MessageDescription("LobbyServer", "Client")]
	[Serializable]
	public class JoinPremadeGameRequestResultMessage : Message
	{
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00002CA0 File Offset: 0x00000EA0
		// (set) Token: 0x0600012E RID: 302 RVA: 0x00002CA8 File Offset: 0x00000EA8
		[JsonProperty]
		public bool Successful { get; private set; }

		// Token: 0x0600012F RID: 303 RVA: 0x00002CB1 File Offset: 0x00000EB1
		public JoinPremadeGameRequestResultMessage()
		{
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00002CB9 File Offset: 0x00000EB9
		public JoinPremadeGameRequestResultMessage(bool successful)
		{
			this.Successful = successful;
		}
	}
}
