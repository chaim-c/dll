using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000058 RID: 88
	[MessageDescription("LobbyServer", "Client")]
	[Serializable]
	public class SigilChangeAnswerMessage : Message
	{
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00003184 File Offset: 0x00001384
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x0000318C File Offset: 0x0000138C
		[JsonProperty]
		public bool Successful { get; private set; }

		// Token: 0x060001A4 RID: 420 RVA: 0x00003195 File Offset: 0x00001395
		public SigilChangeAnswerMessage()
		{
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000319D File Offset: 0x0000139D
		public SigilChangeAnswerMessage(bool answer)
		{
			this.Successful = answer;
		}
	}
}
