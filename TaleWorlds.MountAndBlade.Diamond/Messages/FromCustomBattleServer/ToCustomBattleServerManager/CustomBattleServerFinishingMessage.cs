using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromCustomBattleServer.ToCustomBattleServerManager
{
	// Token: 0x02000060 RID: 96
	[MessageDescription("CustomBattleServer", "CustomBattleServerManager")]
	[Serializable]
	public class CustomBattleServerFinishingMessage : Message
	{
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001CC RID: 460 RVA: 0x0000338F File Offset: 0x0000158F
		// (set) Token: 0x060001CD RID: 461 RVA: 0x00003397 File Offset: 0x00001597
		[JsonProperty]
		public GameLog[] GameLogs { get; private set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001CE RID: 462 RVA: 0x000033A0 File Offset: 0x000015A0
		// (set) Token: 0x060001CF RID: 463 RVA: 0x000033A8 File Offset: 0x000015A8
		[JsonProperty]
		public List<BadgeDataEntry> BadgeDataEntries { get; private set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x000033B1 File Offset: 0x000015B1
		// (set) Token: 0x060001D1 RID: 465 RVA: 0x000033B9 File Offset: 0x000015B9
		[JsonProperty]
		public MultipleBattleResult BattleResult { get; private set; }

		// Token: 0x060001D2 RID: 466 RVA: 0x000033C2 File Offset: 0x000015C2
		public CustomBattleServerFinishingMessage()
		{
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x000033CA File Offset: 0x000015CA
		public CustomBattleServerFinishingMessage(GameLog[] gameLogs, Dictionary<ValueTuple<PlayerId, string, string>, int> badgeDataDictionary, MultipleBattleResult battleResult)
		{
			this.GameLogs = gameLogs;
			this.BadgeDataEntries = BadgeDataEntry.ToList(badgeDataDictionary);
			this.BattleResult = battleResult;
		}
	}
}
