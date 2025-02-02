using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000BC RID: 188
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class RejoinBattleRequestMessage : Message
	{
		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000363 RID: 867 RVA: 0x00004520 File Offset: 0x00002720
		// (set) Token: 0x06000364 RID: 868 RVA: 0x00004528 File Offset: 0x00002728
		[JsonProperty]
		public bool IsRejoinAccepted { get; private set; }

		// Token: 0x06000365 RID: 869 RVA: 0x00004531 File Offset: 0x00002731
		public RejoinBattleRequestMessage()
		{
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00004539 File Offset: 0x00002739
		public RejoinBattleRequestMessage(bool isRejoinAccepted)
		{
			this.IsRejoinAccepted = isRejoinAccepted;
		}
	}
}
