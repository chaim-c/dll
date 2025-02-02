using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000BF RID: 191
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class RemoveFriendMessage : Message
	{
		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600036F RID: 879 RVA: 0x00004598 File Offset: 0x00002798
		// (set) Token: 0x06000370 RID: 880 RVA: 0x000045A0 File Offset: 0x000027A0
		[JsonProperty]
		public PlayerId FriendId { get; private set; }

		// Token: 0x06000371 RID: 881 RVA: 0x000045A9 File Offset: 0x000027A9
		public RemoveFriendMessage()
		{
		}

		// Token: 0x06000372 RID: 882 RVA: 0x000045B1 File Offset: 0x000027B1
		public RemoveFriendMessage(PlayerId friendId)
		{
			this.FriendId = friendId;
		}
	}
}
