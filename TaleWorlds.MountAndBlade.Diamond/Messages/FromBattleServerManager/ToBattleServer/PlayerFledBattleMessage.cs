using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromBattleServerManager.ToBattleServer
{
	// Token: 0x020000E8 RID: 232
	[MessageDescription("BattleServerManager", "BattleServer")]
	[Serializable]
	public class PlayerFledBattleMessage : Message
	{
		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x00004EDB File Offset: 0x000030DB
		// (set) Token: 0x06000444 RID: 1092 RVA: 0x00004EE3 File Offset: 0x000030E3
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x06000445 RID: 1093 RVA: 0x00004EEC File Offset: 0x000030EC
		public PlayerFledBattleMessage()
		{
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00004EF4 File Offset: 0x000030F4
		public PlayerFledBattleMessage(PlayerId playerId)
		{
			this.PlayerId = playerId;
		}
	}
}
