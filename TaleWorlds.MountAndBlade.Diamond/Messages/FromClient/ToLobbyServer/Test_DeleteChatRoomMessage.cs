using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000C9 RID: 201
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class Test_DeleteChatRoomMessage : Message
	{
		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x000047CA File Offset: 0x000029CA
		// (set) Token: 0x060003A5 RID: 933 RVA: 0x000047D2 File Offset: 0x000029D2
		[JsonProperty]
		public Guid ChatRoomId { get; private set; }

		// Token: 0x060003A6 RID: 934 RVA: 0x000047DB File Offset: 0x000029DB
		public Test_DeleteChatRoomMessage()
		{
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x000047E3 File Offset: 0x000029E3
		public Test_DeleteChatRoomMessage(Guid chatRoomId)
		{
			this.ChatRoomId = chatRoomId;
		}
	}
}
