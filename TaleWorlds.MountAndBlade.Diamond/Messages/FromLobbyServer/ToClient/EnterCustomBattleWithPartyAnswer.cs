using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200001D RID: 29
	[MessageDescription("LobbyServer", "Client")]
	[Serializable]
	public class EnterCustomBattleWithPartyAnswer : Message
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00002554 File Offset: 0x00000754
		// (set) Token: 0x0600007E RID: 126 RVA: 0x0000255C File Offset: 0x0000075C
		[JsonProperty]
		public bool Successful { get; private set; }

		// Token: 0x0600007F RID: 127 RVA: 0x00002565 File Offset: 0x00000765
		public EnterCustomBattleWithPartyAnswer()
		{
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000256D File Offset: 0x0000076D
		public EnterCustomBattleWithPartyAnswer(bool successful)
		{
			this.Successful = successful;
		}
	}
}
