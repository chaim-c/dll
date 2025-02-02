using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromCustomBattleServer.ToCustomBattleServerManager
{
	// Token: 0x0200005F RID: 95
	[MessageDescription("CustomBattleServer", "CustomBattleServerManager")]
	[Serializable]
	public class CustomBattleFinishedMessage : Message
	{
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x000032E9 File Offset: 0x000014E9
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x000032F1 File Offset: 0x000014F1
		[JsonProperty]
		public BattleResult BattleResult { get; private set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x000032FA File Offset: 0x000014FA
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x00003302 File Offset: 0x00001502
		[JsonProperty]
		public Dictionary<int, int> TeamScores { get; private set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x0000330B File Offset: 0x0000150B
		// (set) Token: 0x060001C9 RID: 457 RVA: 0x00003313 File Offset: 0x00001513
		[JsonProperty]
		public Dictionary<string, int> PlayerScores { get; private set; }

		// Token: 0x060001CA RID: 458 RVA: 0x0000331C File Offset: 0x0000151C
		public CustomBattleFinishedMessage()
		{
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00003324 File Offset: 0x00001524
		public CustomBattleFinishedMessage(BattleResult battleResult, Dictionary<int, int> teamScores, Dictionary<PlayerId, int> playerScores)
		{
			this.BattleResult = battleResult;
			this.TeamScores = teamScores;
			this.PlayerScores = playerScores.ToDictionary((KeyValuePair<PlayerId, int> kvp) => kvp.Key.ToString(), (KeyValuePair<PlayerId, int> kvp) => kvp.Value);
		}
	}
}
