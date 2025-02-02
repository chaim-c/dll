using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromBattleServer.ToBattleServerManager
{
	// Token: 0x020000D9 RID: 217
	[MessageDescription("BattleServer", "BattleServerManager")]
	[Serializable]
	public class BattleServerStatsUpdateMessage : Message
	{
		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x00004BEB File Offset: 0x00002DEB
		// (set) Token: 0x060003FE RID: 1022 RVA: 0x00004BF3 File Offset: 0x00002DF3
		[JsonProperty]
		public BattleResult BattleResult { get; private set; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x00004BFC File Offset: 0x00002DFC
		// (set) Token: 0x06000400 RID: 1024 RVA: 0x00004C04 File Offset: 0x00002E04
		[JsonProperty]
		public Dictionary<int, int> TeamScores { get; private set; }

		// Token: 0x06000401 RID: 1025 RVA: 0x00004C0D File Offset: 0x00002E0D
		public BattleServerStatsUpdateMessage()
		{
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00004C15 File Offset: 0x00002E15
		public BattleServerStatsUpdateMessage(BattleResult battleResult, Dictionary<int, int> teamScores)
		{
			this.BattleResult = battleResult;
			this.TeamScores = teamScores;
		}
	}
}
