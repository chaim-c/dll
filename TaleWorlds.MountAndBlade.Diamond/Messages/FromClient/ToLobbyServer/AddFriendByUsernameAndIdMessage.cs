using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x02000076 RID: 118
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class AddFriendByUsernameAndIdMessage : Message
	{
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600024F RID: 591 RVA: 0x000039D7 File Offset: 0x00001BD7
		// (set) Token: 0x06000250 RID: 592 RVA: 0x000039DF File Offset: 0x00001BDF
		[JsonProperty]
		public string Username { get; private set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000251 RID: 593 RVA: 0x000039E8 File Offset: 0x00001BE8
		// (set) Token: 0x06000252 RID: 594 RVA: 0x000039F0 File Offset: 0x00001BF0
		[JsonProperty]
		public int UserId { get; private set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000253 RID: 595 RVA: 0x000039F9 File Offset: 0x00001BF9
		// (set) Token: 0x06000254 RID: 596 RVA: 0x00003A01 File Offset: 0x00001C01
		[JsonProperty]
		public bool DontUseNameForUnknownPlayer { get; private set; }

		// Token: 0x06000255 RID: 597 RVA: 0x00003A0A File Offset: 0x00001C0A
		public AddFriendByUsernameAndIdMessage()
		{
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00003A12 File Offset: 0x00001C12
		public AddFriendByUsernameAndIdMessage(string username, int userId, bool dontUseNameForUnknownPlayer)
		{
			this.Username = username;
			this.UserId = userId;
			this.DontUseNameForUnknownPlayer = dontUseNameForUnknownPlayer;
		}
	}
}
