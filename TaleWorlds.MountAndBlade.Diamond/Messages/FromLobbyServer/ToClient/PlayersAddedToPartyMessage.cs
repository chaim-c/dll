using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200004C RID: 76
	[MessageDescription("LobbyServer", "Client")]
	[Serializable]
	public class PlayersAddedToPartyMessage : Message
	{
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00002EB0 File Offset: 0x000010B0
		// (set) Token: 0x06000162 RID: 354 RVA: 0x00002EB8 File Offset: 0x000010B8
		[TupleElementNames(new string[]
		{
			"PlayerId",
			"PlayerName",
			"IsPartyLeader"
		})]
		[JsonProperty]
		public List<ValueTuple<PlayerId, string, bool>> Players { [return: TupleElementNames(new string[]
		{
			"PlayerId",
			"PlayerName",
			"IsPartyLeader"
		})] get; [param: TupleElementNames(new string[]
		{
			"PlayerId",
			"PlayerName",
			"IsPartyLeader"
		})] private set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00002EC1 File Offset: 0x000010C1
		// (set) Token: 0x06000164 RID: 356 RVA: 0x00002EC9 File Offset: 0x000010C9
		[TupleElementNames(new string[]
		{
			"PlayerId",
			"PlayerName"
		})]
		[JsonProperty]
		public List<ValueTuple<PlayerId, string>> InvitedPlayers { [return: TupleElementNames(new string[]
		{
			"PlayerId",
			"PlayerName"
		})] get; [param: TupleElementNames(new string[]
		{
			"PlayerId",
			"PlayerName"
		})] private set; }

		// Token: 0x06000165 RID: 357 RVA: 0x00002ED2 File Offset: 0x000010D2
		public PlayersAddedToPartyMessage()
		{
			this.Players = new List<ValueTuple<PlayerId, string, bool>>();
			this.InvitedPlayers = new List<ValueTuple<PlayerId, string>>();
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00002EF0 File Offset: 0x000010F0
		public PlayersAddedToPartyMessage(PlayerId playerId, string playerName, bool isPartyLeader) : this()
		{
			this.AddPlayer(playerId, playerName, isPartyLeader);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00002F01 File Offset: 0x00001101
		public void AddPlayer(PlayerId playerId, string playerName, bool isPartyLeader)
		{
			this.Players.Add(new ValueTuple<PlayerId, string, bool>(playerId, playerName, isPartyLeader));
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00002F16 File Offset: 0x00001116
		public void AddInvitedPlayer(PlayerId playerId, string playerName)
		{
			this.InvitedPlayers.Add(new ValueTuple<PlayerId, string>(playerId, playerName));
		}
	}
}
