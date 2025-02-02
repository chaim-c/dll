using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200004B RID: 75
	[MessageDescription("LobbyServer", "Client")]
	[Serializable]
	public class PlayerRemovedFromPartyMessage : Message
	{
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00002E70 File Offset: 0x00001070
		// (set) Token: 0x0600015C RID: 348 RVA: 0x00002E78 File Offset: 0x00001078
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00002E81 File Offset: 0x00001081
		// (set) Token: 0x0600015E RID: 350 RVA: 0x00002E89 File Offset: 0x00001089
		[JsonProperty]
		public PartyRemoveReason Reason { get; private set; }

		// Token: 0x0600015F RID: 351 RVA: 0x00002E92 File Offset: 0x00001092
		public PlayerRemovedFromPartyMessage()
		{
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00002E9A File Offset: 0x0000109A
		public PlayerRemovedFromPartyMessage(PlayerId playerId, PartyRemoveReason reason)
		{
			this.PlayerId = playerId;
			this.Reason = reason;
		}
	}
}
