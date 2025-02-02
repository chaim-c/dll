using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromBattleServerManager.ToBattleServer
{
	// Token: 0x020000E5 RID: 229
	[MessageDescription("BattleServerManager", "BattleServer")]
	[Serializable]
	public class FriendlyDamageKickPlayerResponseMessage : Message
	{
		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x00004E1A File Offset: 0x0000301A
		// (set) Token: 0x06000432 RID: 1074 RVA: 0x00004E22 File Offset: 0x00003022
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x06000433 RID: 1075 RVA: 0x00004E2B File Offset: 0x0000302B
		public FriendlyDamageKickPlayerResponseMessage()
		{
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00004E33 File Offset: 0x00003033
		public FriendlyDamageKickPlayerResponseMessage(PlayerId playerId)
		{
			this.PlayerId = playerId;
		}
	}
}
