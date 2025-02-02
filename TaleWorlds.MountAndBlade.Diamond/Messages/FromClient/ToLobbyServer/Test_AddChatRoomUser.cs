using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000C7 RID: 199
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class Test_AddChatRoomUser : Message
	{
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600039C RID: 924 RVA: 0x0000477A File Offset: 0x0000297A
		// (set) Token: 0x0600039D RID: 925 RVA: 0x00004782 File Offset: 0x00002982
		[JsonProperty]
		public string Name { get; private set; }

		// Token: 0x0600039E RID: 926 RVA: 0x0000478B File Offset: 0x0000298B
		public Test_AddChatRoomUser()
		{
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00004793 File Offset: 0x00002993
		public Test_AddChatRoomUser(string name)
		{
			this.Name = name;
		}
	}
}
