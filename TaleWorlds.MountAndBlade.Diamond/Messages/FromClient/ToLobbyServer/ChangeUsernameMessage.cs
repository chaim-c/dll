using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x02000082 RID: 130
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class ChangeUsernameMessage : Message
	{
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600027E RID: 638 RVA: 0x00003BB7 File Offset: 0x00001DB7
		// (set) Token: 0x0600027F RID: 639 RVA: 0x00003BBF File Offset: 0x00001DBF
		[JsonProperty]
		public string Username { get; private set; }

		// Token: 0x06000280 RID: 640 RVA: 0x00003BC8 File Offset: 0x00001DC8
		public ChangeUsernameMessage()
		{
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00003BD0 File Offset: 0x00001DD0
		public ChangeUsernameMessage(string username)
		{
			this.Username = username;
		}
	}
}
