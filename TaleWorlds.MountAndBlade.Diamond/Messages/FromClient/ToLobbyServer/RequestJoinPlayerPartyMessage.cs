using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000C3 RID: 195
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class RequestJoinPlayerPartyMessage : Message
	{
		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000388 RID: 904 RVA: 0x000046AA File Offset: 0x000028AA
		// (set) Token: 0x06000389 RID: 905 RVA: 0x000046B2 File Offset: 0x000028B2
		[JsonProperty]
		public PlayerId TargetPlayer { get; private set; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600038A RID: 906 RVA: 0x000046BB File Offset: 0x000028BB
		// (set) Token: 0x0600038B RID: 907 RVA: 0x000046C3 File Offset: 0x000028C3
		[JsonProperty]
		public bool InviteRequest { get; private set; }

		// Token: 0x0600038C RID: 908 RVA: 0x000046CC File Offset: 0x000028CC
		public RequestJoinPlayerPartyMessage()
		{
		}

		// Token: 0x0600038D RID: 909 RVA: 0x000046D4 File Offset: 0x000028D4
		public RequestJoinPlayerPartyMessage(PlayerId targetPlayer, bool inviteRequest)
		{
			this.TargetPlayer = targetPlayer;
			this.InviteRequest = inviteRequest;
		}
	}
}
