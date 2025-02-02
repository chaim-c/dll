using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x02000077 RID: 119
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class AddFriendMessage : Message
	{
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000257 RID: 599 RVA: 0x00003A2F File Offset: 0x00001C2F
		// (set) Token: 0x06000258 RID: 600 RVA: 0x00003A37 File Offset: 0x00001C37
		[JsonProperty]
		public PlayerId FriendId { get; private set; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000259 RID: 601 RVA: 0x00003A40 File Offset: 0x00001C40
		// (set) Token: 0x0600025A RID: 602 RVA: 0x00003A48 File Offset: 0x00001C48
		[JsonProperty]
		public bool DontUseNameForUnknownPlayer { get; private set; }

		// Token: 0x0600025B RID: 603 RVA: 0x00003A51 File Offset: 0x00001C51
		public AddFriendMessage()
		{
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00003A59 File Offset: 0x00001C59
		public AddFriendMessage(PlayerId friendId, bool dontUseNameForUnknownPlayer)
		{
			this.FriendId = friendId;
			this.DontUseNameForUnknownPlayer = dontUseNameForUnknownPlayer;
		}
	}
}
