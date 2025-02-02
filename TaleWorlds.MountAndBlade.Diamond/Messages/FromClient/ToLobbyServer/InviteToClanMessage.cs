using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000AE RID: 174
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class InviteToClanMessage : Message
	{
		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600031D RID: 797 RVA: 0x0000422E File Offset: 0x0000242E
		// (set) Token: 0x0600031E RID: 798 RVA: 0x00004236 File Offset: 0x00002436
		[JsonProperty]
		public PlayerId InvitedPlayerId { get; private set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600031F RID: 799 RVA: 0x0000423F File Offset: 0x0000243F
		// (set) Token: 0x06000320 RID: 800 RVA: 0x00004247 File Offset: 0x00002447
		[JsonProperty]
		public bool DontUseNameForUnknownPlayer { get; private set; }

		// Token: 0x06000321 RID: 801 RVA: 0x00004250 File Offset: 0x00002450
		public InviteToClanMessage()
		{
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00004258 File Offset: 0x00002458
		public InviteToClanMessage(PlayerId invitedPlayerId, bool dontUseNameForUnknownPlayer)
		{
			this.InvitedPlayerId = invitedPlayerId;
			this.DontUseNameForUnknownPlayer = dontUseNameForUnknownPlayer;
		}
	}
}
