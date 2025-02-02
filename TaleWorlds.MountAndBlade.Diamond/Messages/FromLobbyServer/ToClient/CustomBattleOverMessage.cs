using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000019 RID: 25
	[MessageDescription("LobbyServer", "Client")]
	[Serializable]
	public class CustomBattleOverMessage : Message
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600006A RID: 106 RVA: 0x0000248C File Offset: 0x0000068C
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00002494 File Offset: 0x00000694
		[JsonProperty]
		public int OldExperience { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600006C RID: 108 RVA: 0x0000249D File Offset: 0x0000069D
		// (set) Token: 0x0600006D RID: 109 RVA: 0x000024A5 File Offset: 0x000006A5
		[JsonProperty]
		public int NewExperience { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600006E RID: 110 RVA: 0x000024AE File Offset: 0x000006AE
		// (set) Token: 0x0600006F RID: 111 RVA: 0x000024B6 File Offset: 0x000006B6
		[JsonProperty]
		public int GoldGain { get; set; }

		// Token: 0x06000070 RID: 112 RVA: 0x000024BF File Offset: 0x000006BF
		public CustomBattleOverMessage()
		{
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000024C7 File Offset: 0x000006C7
		public CustomBattleOverMessage(int oldExperience, int newExperience, int goldGain)
		{
			this.OldExperience = oldExperience;
			this.NewExperience = newExperience;
			this.GoldGain = goldGain;
		}
	}
}
