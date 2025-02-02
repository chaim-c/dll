using System;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000052 RID: 82
	[MessageDescription("LobbyServer", "Client")]
	[Serializable]
	public class RejoinBattleRequestAnswerMessage : Message
	{
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00003053 File Offset: 0x00001253
		// (set) Token: 0x06000186 RID: 390 RVA: 0x0000305B File Offset: 0x0000125B
		public bool IsRejoinAccepted { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00003064 File Offset: 0x00001264
		// (set) Token: 0x06000188 RID: 392 RVA: 0x0000306C File Offset: 0x0000126C
		public bool IsSuccessful { get; set; }

		// Token: 0x06000189 RID: 393 RVA: 0x00003075 File Offset: 0x00001275
		public RejoinBattleRequestAnswerMessage()
		{
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000307D File Offset: 0x0000127D
		public RejoinBattleRequestAnswerMessage(bool isRejoinAccepted, bool isSuccessful)
		{
			this.IsRejoinAccepted = isRejoinAccepted;
			this.IsSuccessful = isSuccessful;
		}
	}
}
