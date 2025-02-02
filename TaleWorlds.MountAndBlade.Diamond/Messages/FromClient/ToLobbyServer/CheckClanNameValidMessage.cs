using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x02000084 RID: 132
	[MessageDescription("LobbyServer", "Client")]
	[Serializable]
	public class CheckClanNameValidMessage : Message
	{
		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000288 RID: 648 RVA: 0x00003C1F File Offset: 0x00001E1F
		// (set) Token: 0x06000289 RID: 649 RVA: 0x00003C27 File Offset: 0x00001E27
		[JsonProperty]
		public string ClanName { get; private set; }

		// Token: 0x0600028A RID: 650 RVA: 0x00003C30 File Offset: 0x00001E30
		public CheckClanNameValidMessage()
		{
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00003C38 File Offset: 0x00001E38
		public CheckClanNameValidMessage(string clanName)
		{
			this.ClanName = clanName;
		}
	}
}
