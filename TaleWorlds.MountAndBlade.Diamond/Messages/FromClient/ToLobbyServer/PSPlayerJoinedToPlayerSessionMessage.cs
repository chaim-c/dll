using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000B7 RID: 183
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class PSPlayerJoinedToPlayerSessionMessage : Message
	{
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000345 RID: 837 RVA: 0x000043C6 File Offset: 0x000025C6
		// (set) Token: 0x06000346 RID: 838 RVA: 0x000043CE File Offset: 0x000025CE
		[JsonProperty]
		public ulong InviterPlayerAccountId { get; private set; }

		// Token: 0x06000347 RID: 839 RVA: 0x000043D7 File Offset: 0x000025D7
		public PSPlayerJoinedToPlayerSessionMessage()
		{
		}

		// Token: 0x06000348 RID: 840 RVA: 0x000043DF File Offset: 0x000025DF
		public PSPlayerJoinedToPlayerSessionMessage(ulong inviterPlayerAccountId)
		{
			this.InviterPlayerAccountId = inviterPlayerAccountId;
		}
	}
}
