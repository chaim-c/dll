using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromCustomBattleServerManager.ToCustomBattleServer
{
	// Token: 0x0200006A RID: 106
	[MessageDescription("CustomBattleServerManager", "CustomBattleServer")]
	[Serializable]
	public class PlayerDisconnectedFromLobbyMessage : Message
	{
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000225 RID: 549 RVA: 0x000037E0 File Offset: 0x000019E0
		// (set) Token: 0x06000226 RID: 550 RVA: 0x000037E8 File Offset: 0x000019E8
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x06000227 RID: 551 RVA: 0x000037F1 File Offset: 0x000019F1
		public PlayerDisconnectedFromLobbyMessage()
		{
		}

		// Token: 0x06000228 RID: 552 RVA: 0x000037F9 File Offset: 0x000019F9
		public PlayerDisconnectedFromLobbyMessage(PlayerId playerId)
		{
			this.PlayerId = playerId;
		}
	}
}
