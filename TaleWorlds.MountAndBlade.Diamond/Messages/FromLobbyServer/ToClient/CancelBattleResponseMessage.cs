using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000007 RID: 7
	[MessageDescription("LobbyServer", "Client")]
	[Serializable]
	public class CancelBattleResponseMessage : Message
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001E RID: 30 RVA: 0x0000217B File Offset: 0x0000037B
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002183 File Offset: 0x00000383
		[JsonProperty]
		public bool Successful { get; private set; }

		// Token: 0x06000020 RID: 32 RVA: 0x0000218C File Offset: 0x0000038C
		public CancelBattleResponseMessage()
		{
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002194 File Offset: 0x00000394
		public CancelBattleResponseMessage(bool successful)
		{
			this.Successful = successful;
		}
	}
}
