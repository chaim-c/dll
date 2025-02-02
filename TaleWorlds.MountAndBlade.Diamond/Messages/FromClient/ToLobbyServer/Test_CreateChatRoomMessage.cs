using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000C8 RID: 200
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class Test_CreateChatRoomMessage : Message
	{
		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x000047A2 File Offset: 0x000029A2
		// (set) Token: 0x060003A1 RID: 929 RVA: 0x000047AA File Offset: 0x000029AA
		[JsonProperty]
		public string Name { get; private set; }

		// Token: 0x060003A2 RID: 930 RVA: 0x000047B3 File Offset: 0x000029B3
		public Test_CreateChatRoomMessage()
		{
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x000047BB File Offset: 0x000029BB
		public Test_CreateChatRoomMessage(string name)
		{
			this.Name = name;
		}
	}
}
