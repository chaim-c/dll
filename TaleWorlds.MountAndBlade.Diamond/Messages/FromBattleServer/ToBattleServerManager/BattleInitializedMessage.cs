using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromBattleServer.ToBattleServerManager
{
	// Token: 0x020000D6 RID: 214
	[MessageDescription("BattleServer", "BattleServerManager")]
	[Serializable]
	public class BattleInitializedMessage : Message
	{
		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x00004AD8 File Offset: 0x00002CD8
		[JsonProperty]
		public string GameType { get; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x00004AE0 File Offset: 0x00002CE0
		[JsonProperty]
		public List<PlayerId> AssignedPlayers { get; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x00004AE8 File Offset: 0x00002CE8
		[JsonProperty]
		public string Faction1 { get; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x00004AF0 File Offset: 0x00002CF0
		[JsonProperty]
		public string Faction2 { get; }

		// Token: 0x060003EA RID: 1002 RVA: 0x00004AF8 File Offset: 0x00002CF8
		public BattleInitializedMessage()
		{
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00004B00 File Offset: 0x00002D00
		public BattleInitializedMessage(string gameType, List<PlayerId> assignedPlayers, string faction1, string faction2)
		{
			this.GameType = gameType;
			this.AssignedPlayers = assignedPlayers;
			this.Faction1 = faction1;
			this.Faction2 = faction2;
		}
	}
}
