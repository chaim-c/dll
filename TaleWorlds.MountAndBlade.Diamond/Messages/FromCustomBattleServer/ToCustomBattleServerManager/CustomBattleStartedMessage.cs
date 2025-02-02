using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromCustomBattleServer.ToCustomBattleServerManager
{
	// Token: 0x02000062 RID: 98
	[MessageDescription("CustomBattleServer", "CustomBattleServerManager")]
	[Serializable]
	public class CustomBattleStartedMessage : Message
	{
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001DE RID: 478 RVA: 0x0000345F File Offset: 0x0000165F
		// (set) Token: 0x060001DF RID: 479 RVA: 0x00003467 File Offset: 0x00001667
		[JsonProperty]
		public string GameType { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x00003470 File Offset: 0x00001670
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x00003478 File Offset: 0x00001678
		[JsonProperty]
		public Dictionary<string, int> PlayerTeams { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00003481 File Offset: 0x00001681
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x00003489 File Offset: 0x00001689
		[JsonProperty]
		public List<string> FactionNames { get; set; }

		// Token: 0x060001E4 RID: 484 RVA: 0x00003492 File Offset: 0x00001692
		public CustomBattleStartedMessage()
		{
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000349C File Offset: 0x0000169C
		public CustomBattleStartedMessage(string gameType, Dictionary<PlayerId, int> playerTeams, List<string> factionNames)
		{
			this.GameType = gameType;
			this.PlayerTeams = playerTeams.ToDictionary((KeyValuePair<PlayerId, int> kvp) => kvp.Key.ToString(), (KeyValuePair<PlayerId, int> kvp) => kvp.Value);
			this.FactionNames = factionNames;
		}
	}
}
