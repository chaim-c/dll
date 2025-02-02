using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000046 RID: 70
	[MessageDescription("LobbyServer", "Client")]
	[Serializable]
	public class PlayerInvitedToPartyMessage : Message
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00002D98 File Offset: 0x00000F98
		// (set) Token: 0x06000147 RID: 327 RVA: 0x00002DA0 File Offset: 0x00000FA0
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00002DA9 File Offset: 0x00000FA9
		// (set) Token: 0x06000149 RID: 329 RVA: 0x00002DB1 File Offset: 0x00000FB1
		[JsonProperty]
		public string PlayerName { get; private set; }

		// Token: 0x0600014A RID: 330 RVA: 0x00002DBA File Offset: 0x00000FBA
		public PlayerInvitedToPartyMessage()
		{
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00002DC2 File Offset: 0x00000FC2
		public PlayerInvitedToPartyMessage(PlayerId playerId, string playerName)
		{
			this.PlayerId = playerId;
			this.PlayerName = playerName;
		}
	}
}
