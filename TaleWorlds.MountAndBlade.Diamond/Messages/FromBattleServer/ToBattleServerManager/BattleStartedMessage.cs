using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromBattleServer.ToBattleServerManager
{
	// Token: 0x020000DA RID: 218
	[MessageDescription("BattleServer", "BattleServerManager")]
	[Serializable]
	public class BattleStartedMessage : Message
	{
		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x00004C2B File Offset: 0x00002E2B
		// (set) Token: 0x06000404 RID: 1028 RVA: 0x00004C33 File Offset: 0x00002E33
		[JsonProperty]
		public bool Report { get; private set; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x00004C3C File Offset: 0x00002E3C
		// (set) Token: 0x06000406 RID: 1030 RVA: 0x00004C44 File Offset: 0x00002E44
		[JsonProperty]
		public Dictionary<string, int> PlayerTeams { get; private set; }

		// Token: 0x06000407 RID: 1031 RVA: 0x00004C4D File Offset: 0x00002E4D
		public BattleStartedMessage()
		{
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00004C55 File Offset: 0x00002E55
		public BattleStartedMessage(bool report)
		{
			this.Report = report;
			this.PlayerTeams = null;
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00004C6B File Offset: 0x00002E6B
		public BattleStartedMessage(bool report, Dictionary<string, int> playerTeams)
		{
			this.Report = report;
			this.PlayerTeams = playerTeams;
		}
	}
}
