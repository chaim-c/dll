using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromCustomBattleServerManager.ToCustomBattleServer
{
	// Token: 0x0200006E RID: 110
	[MessageDescription("CustomBattleServer", "CustomBattleServerManager")]
	[Serializable]
	public class CustomBattleServerStatsUpdateMessage : Message
	{
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000234 RID: 564 RVA: 0x00003878 File Offset: 0x00001A78
		// (set) Token: 0x06000235 RID: 565 RVA: 0x00003880 File Offset: 0x00001A80
		[JsonProperty]
		public BattleResult BattleResult { get; set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000236 RID: 566 RVA: 0x00003889 File Offset: 0x00001A89
		// (set) Token: 0x06000237 RID: 567 RVA: 0x00003891 File Offset: 0x00001A91
		[JsonProperty]
		public Dictionary<int, int> TeamScores { get; set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000238 RID: 568 RVA: 0x0000389A File Offset: 0x00001A9A
		// (set) Token: 0x06000239 RID: 569 RVA: 0x000038A2 File Offset: 0x00001AA2
		[JsonProperty]
		public Dictionary<string, int> PlayerScores { get; set; }

		// Token: 0x0600023A RID: 570 RVA: 0x000038AB File Offset: 0x00001AAB
		public CustomBattleServerStatsUpdateMessage()
		{
		}

		// Token: 0x0600023B RID: 571 RVA: 0x000038B4 File Offset: 0x00001AB4
		public CustomBattleServerStatsUpdateMessage(BattleResult battleResult, Dictionary<int, int> teamScores, Dictionary<PlayerId, int> playerScores)
		{
			this.BattleResult = battleResult;
			this.TeamScores = teamScores;
			this.PlayerScores = playerScores.ToDictionary((KeyValuePair<PlayerId, int> kvp) => kvp.Key.ToString(), (KeyValuePair<PlayerId, int> kvp) => kvp.Value);
		}
	}
}
