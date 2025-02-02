using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000AF RID: 175
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class InviteToGameMessage : Message
	{
		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0000426E File Offset: 0x0000246E
		// (set) Token: 0x06000324 RID: 804 RVA: 0x00004276 File Offset: 0x00002476
		[JsonProperty]
		public PlayerId InvitedPlayerId { get; private set; }

		// Token: 0x06000325 RID: 805 RVA: 0x0000427F File Offset: 0x0000247F
		public InviteToGameMessage()
		{
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00004287 File Offset: 0x00002487
		public InviteToGameMessage(PlayerId invitedPlayerId)
		{
			this.InvitedPlayerId = invitedPlayerId;
		}
	}
}
