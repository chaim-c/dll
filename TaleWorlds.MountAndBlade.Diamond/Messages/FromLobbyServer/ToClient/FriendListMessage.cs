using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200001F RID: 31
	[MessageDescription("LobbyServer", "Client")]
	[Serializable]
	public class FriendListMessage : Message
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000087 RID: 135 RVA: 0x000025BC File Offset: 0x000007BC
		// (set) Token: 0x06000088 RID: 136 RVA: 0x000025C4 File Offset: 0x000007C4
		[JsonProperty]
		public FriendInfo[] Friends { get; private set; }

		// Token: 0x06000089 RID: 137 RVA: 0x000025CD File Offset: 0x000007CD
		public FriendListMessage()
		{
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000025D5 File Offset: 0x000007D5
		public FriendListMessage(FriendInfo[] friends)
		{
			this.Friends = friends;
		}
	}
}
