using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000CA RID: 202
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class Test_RemoveChatRoomUser : Message
	{
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x000047F2 File Offset: 0x000029F2
		// (set) Token: 0x060003A9 RID: 937 RVA: 0x000047FA File Offset: 0x000029FA
		[JsonProperty]
		public string Name { get; private set; }

		// Token: 0x060003AA RID: 938 RVA: 0x00004803 File Offset: 0x00002A03
		public Test_RemoveChatRoomUser()
		{
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000480B File Offset: 0x00002A0B
		public Test_RemoveChatRoomUser(string name)
		{
			this.Name = name;
		}
	}
}
