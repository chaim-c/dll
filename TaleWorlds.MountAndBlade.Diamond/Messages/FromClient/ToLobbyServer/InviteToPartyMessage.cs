using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000B0 RID: 176
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class InviteToPartyMessage : Message
	{
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000327 RID: 807 RVA: 0x00004296 File Offset: 0x00002496
		// (set) Token: 0x06000328 RID: 808 RVA: 0x0000429E File Offset: 0x0000249E
		[JsonProperty]
		public PlayerId InvitedPlayerId { get; private set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000329 RID: 809 RVA: 0x000042A7 File Offset: 0x000024A7
		// (set) Token: 0x0600032A RID: 810 RVA: 0x000042AF File Offset: 0x000024AF
		[JsonProperty]
		public bool DontUseNameForUnknownPlayer { get; private set; }

		// Token: 0x0600032B RID: 811 RVA: 0x000042B8 File Offset: 0x000024B8
		public InviteToPartyMessage()
		{
		}

		// Token: 0x0600032C RID: 812 RVA: 0x000042C0 File Offset: 0x000024C0
		public InviteToPartyMessage(PlayerId invitedPlayerId, bool dontUseNameForUnknownPlayer)
		{
			this.InvitedPlayerId = invitedPlayerId;
			this.DontUseNameForUnknownPlayer = dontUseNameForUnknownPlayer;
		}
	}
}
