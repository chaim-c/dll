using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromBattleServerManager.ToBattleServer
{
	// Token: 0x020000E7 RID: 231
	[MessageDescription("BattleServerManager", "BattleServer")]
	[Serializable]
	public class PlayerDisconnectedFromLobbyMessage : Message
	{
		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x00004EB3 File Offset: 0x000030B3
		// (set) Token: 0x06000440 RID: 1088 RVA: 0x00004EBB File Offset: 0x000030BB
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x06000441 RID: 1089 RVA: 0x00004EC4 File Offset: 0x000030C4
		public PlayerDisconnectedFromLobbyMessage()
		{
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00004ECC File Offset: 0x000030CC
		public PlayerDisconnectedFromLobbyMessage(PlayerId playerId)
		{
			this.PlayerId = playerId;
		}
	}
}
