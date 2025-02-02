using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200000A RID: 10
	[MessageDescription("LobbyServer", "Client")]
	[Serializable]
	public class ChatRoomClosedMessage : Message
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002203 File Offset: 0x00000403
		// (set) Token: 0x0600002C RID: 44 RVA: 0x0000220B File Offset: 0x0000040B
		[JsonProperty]
		public Guid ChatRoomId { get; private set; }

		// Token: 0x0600002D RID: 45 RVA: 0x00002214 File Offset: 0x00000414
		public ChatRoomClosedMessage()
		{
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000221C File Offset: 0x0000041C
		public ChatRoomClosedMessage(Guid chatRoomId)
		{
			this.ChatRoomId = chatRoomId;
		}
	}
}
