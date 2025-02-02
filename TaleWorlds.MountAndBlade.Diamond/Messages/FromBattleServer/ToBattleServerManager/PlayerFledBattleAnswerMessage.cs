using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromBattleServer.ToBattleServerManager
{
	// Token: 0x020000DF RID: 223
	[MessageDescription("BattleServer", "BattleServerManager")]
	[Serializable]
	public class PlayerFledBattleAnswerMessage : Message
	{
		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x00004D7A File Offset: 0x00002F7A
		// (set) Token: 0x06000422 RID: 1058 RVA: 0x00004D82 File Offset: 0x00002F82
		[JsonProperty]
		public BattleResult BattleResult { get; private set; }

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x00004D8B File Offset: 0x00002F8B
		// (set) Token: 0x06000424 RID: 1060 RVA: 0x00004D93 File Offset: 0x00002F93
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x00004D9C File Offset: 0x00002F9C
		// (set) Token: 0x06000426 RID: 1062 RVA: 0x00004DA4 File Offset: 0x00002FA4
		[JsonProperty]
		public bool IsAllowedLeave { get; private set; }

		// Token: 0x06000427 RID: 1063 RVA: 0x00004DAD File Offset: 0x00002FAD
		public PlayerFledBattleAnswerMessage()
		{
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00004DB5 File Offset: 0x00002FB5
		public PlayerFledBattleAnswerMessage(PlayerId playerId, BattleResult battleResult, bool isAllowedLeave)
		{
			this.PlayerId = playerId;
			this.BattleResult = battleResult;
			this.IsAllowedLeave = isAllowedLeave;
		}
	}
}
