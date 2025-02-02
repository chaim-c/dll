using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000C6 RID: 198
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class SetClanInformationMessage : Message
	{
		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000398 RID: 920 RVA: 0x00004752 File Offset: 0x00002952
		// (set) Token: 0x06000399 RID: 921 RVA: 0x0000475A File Offset: 0x0000295A
		[JsonProperty]
		public string Information { get; private set; }

		// Token: 0x0600039A RID: 922 RVA: 0x00004763 File Offset: 0x00002963
		public SetClanInformationMessage()
		{
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000476B File Offset: 0x0000296B
		public SetClanInformationMessage(string information)
		{
			this.Information = information;
		}
	}
}
