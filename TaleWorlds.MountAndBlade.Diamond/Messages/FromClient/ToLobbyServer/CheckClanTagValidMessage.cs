using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x02000085 RID: 133
	[MessageDescription("LobbyServer", "Client")]
	[Serializable]
	public class CheckClanTagValidMessage : Message
	{
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600028C RID: 652 RVA: 0x00003C47 File Offset: 0x00001E47
		// (set) Token: 0x0600028D RID: 653 RVA: 0x00003C4F File Offset: 0x00001E4F
		[JsonProperty]
		public string ClanTag { get; private set; }

		// Token: 0x0600028E RID: 654 RVA: 0x00003C58 File Offset: 0x00001E58
		public CheckClanTagValidMessage()
		{
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00003C60 File Offset: 0x00001E60
		public CheckClanTagValidMessage(string clanTag)
		{
			this.ClanTag = clanTag;
		}
	}
}
