using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000038 RID: 56
	[MessageDescription("LobbyServer", "Client")]
	[Serializable]
	public class InvitedPlayerOnlineMessage : Message
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00002A9D File Offset: 0x00000C9D
		// (set) Token: 0x06000100 RID: 256 RVA: 0x00002AA5 File Offset: 0x00000CA5
		[JsonProperty]
		public PlayerId PlayerId { get; set; }

		// Token: 0x06000101 RID: 257 RVA: 0x00002AAE File Offset: 0x00000CAE
		public InvitedPlayerOnlineMessage()
		{
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00002AB6 File Offset: 0x00000CB6
		public InvitedPlayerOnlineMessage(PlayerId playerId)
		{
			this.PlayerId = playerId;
		}
	}
}
