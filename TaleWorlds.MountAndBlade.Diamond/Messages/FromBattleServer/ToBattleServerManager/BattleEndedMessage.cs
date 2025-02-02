using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromBattleServer.ToBattleServerManager
{
	// Token: 0x020000D4 RID: 212
	[MessageDescription("BattleServer", "BattleServerManager")]
	[Serializable]
	public class BattleEndedMessage : Message
	{
		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x000049DA File Offset: 0x00002BDA
		// (set) Token: 0x060003D8 RID: 984 RVA: 0x000049E2 File Offset: 0x00002BE2
		[JsonProperty]
		public BattleResult BattleResult { get; set; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x000049EB File Offset: 0x00002BEB
		// (set) Token: 0x060003DA RID: 986 RVA: 0x000049F3 File Offset: 0x00002BF3
		[JsonProperty]
		public GameLog[] GameLogs { get; set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060003DB RID: 987 RVA: 0x000049FC File Offset: 0x00002BFC
		// (set) Token: 0x060003DC RID: 988 RVA: 0x00004A04 File Offset: 0x00002C04
		[JsonProperty]
		public List<BadgeDataEntry> BadgeDataEntries { get; set; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060003DD RID: 989 RVA: 0x00004A0D File Offset: 0x00002C0D
		// (set) Token: 0x060003DE RID: 990 RVA: 0x00004A15 File Offset: 0x00002C15
		[JsonProperty]
		public Dictionary<int, int> TeamScores { get; set; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060003DF RID: 991 RVA: 0x00004A1E File Offset: 0x00002C1E
		// (set) Token: 0x060003E0 RID: 992 RVA: 0x00004A26 File Offset: 0x00002C26
		[JsonProperty]
		public Dictionary<string, int> PlayerScores { get; set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x00004A2F File Offset: 0x00002C2F
		// (set) Token: 0x060003E2 RID: 994 RVA: 0x00004A37 File Offset: 0x00002C37
		[JsonProperty]
		public int GameTime { get; set; }

		// Token: 0x060003E3 RID: 995 RVA: 0x00004A40 File Offset: 0x00002C40
		public BattleEndedMessage()
		{
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00004A48 File Offset: 0x00002C48
		public BattleEndedMessage(BattleResult battleResult, GameLog[] gameLogs, Dictionary<ValueTuple<PlayerId, string, string>, int> badgeDataDictionary, int gameTime, Dictionary<int, int> teamScores, Dictionary<PlayerId, int> playerScores)
		{
			this.BattleResult = battleResult;
			this.GameLogs = gameLogs;
			this.BadgeDataEntries = BadgeDataEntry.ToList(badgeDataDictionary);
			this.TeamScores = teamScores;
			this.PlayerScores = playerScores.ToDictionary((KeyValuePair<PlayerId, int> kvp) => kvp.Key.ToString(), (KeyValuePair<PlayerId, int> kvp) => kvp.Value);
			this.GameTime = gameTime;
		}
	}
}
