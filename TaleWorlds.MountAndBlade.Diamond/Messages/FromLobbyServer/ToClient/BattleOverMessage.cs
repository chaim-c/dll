using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.MountAndBlade.Diamond.Ranked;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000003 RID: 3
	[MessageDescription("LobbyServer", "Client")]
	[Serializable]
	public class BattleOverMessage : Message
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002070 File Offset: 0x00000270
		// (set) Token: 0x06000006 RID: 6 RVA: 0x00002078 File Offset: 0x00000278
		[JsonProperty]
		public int OldExperience { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002081 File Offset: 0x00000281
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002089 File Offset: 0x00000289
		[JsonProperty]
		public int NewExperience { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002092 File Offset: 0x00000292
		// (set) Token: 0x0600000A RID: 10 RVA: 0x0000209A File Offset: 0x0000029A
		[JsonProperty]
		public List<string> EarnedBadges { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000020A3 File Offset: 0x000002A3
		// (set) Token: 0x0600000C RID: 12 RVA: 0x000020AB File Offset: 0x000002AB
		[JsonProperty]
		public int GoldGained { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000020B4 File Offset: 0x000002B4
		// (set) Token: 0x0600000E RID: 14 RVA: 0x000020BC File Offset: 0x000002BC
		[JsonProperty]
		public RankBarInfo OldInfo { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000020C5 File Offset: 0x000002C5
		// (set) Token: 0x06000010 RID: 16 RVA: 0x000020CD File Offset: 0x000002CD
		[JsonProperty]
		public RankBarInfo NewInfo { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000020D6 File Offset: 0x000002D6
		// (set) Token: 0x06000012 RID: 18 RVA: 0x000020DE File Offset: 0x000002DE
		[JsonProperty]
		public BattleCancelReason BattleCancelReason { get; private set; }

		// Token: 0x06000013 RID: 19 RVA: 0x000020E7 File Offset: 0x000002E7
		public BattleOverMessage()
		{
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000020EF File Offset: 0x000002EF
		public BattleOverMessage(int oldExperience, int newExperience, List<string> earnedBadges, int goldGained, BattleCancelReason battleCancelReason = BattleCancelReason.None)
		{
			this.OldExperience = oldExperience;
			this.NewExperience = newExperience;
			this.EarnedBadges = earnedBadges;
			this.GoldGained = goldGained;
			this.BattleCancelReason = battleCancelReason;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000211C File Offset: 0x0000031C
		public BattleOverMessage(BattleCancelReason battleCancelReason)
		{
			this.BattleCancelReason = battleCancelReason;
		}
	}
}
