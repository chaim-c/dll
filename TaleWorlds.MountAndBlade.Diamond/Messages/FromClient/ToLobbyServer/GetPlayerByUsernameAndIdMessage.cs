using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000A1 RID: 161
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class GetPlayerByUsernameAndIdMessage : Message
	{
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x00003FFD File Offset: 0x000021FD
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x00004005 File Offset: 0x00002205
		[JsonProperty]
		public string Username { get; private set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000400E File Offset: 0x0000220E
		// (set) Token: 0x060002EB RID: 747 RVA: 0x00004016 File Offset: 0x00002216
		[JsonProperty]
		public int UserId { get; private set; }

		// Token: 0x060002EC RID: 748 RVA: 0x0000401F File Offset: 0x0000221F
		public GetPlayerByUsernameAndIdMessage()
		{
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00004027 File Offset: 0x00002227
		public GetPlayerByUsernameAndIdMessage(string username, int userId)
		{
			this.Username = username;
			this.UserId = userId;
		}
	}
}
