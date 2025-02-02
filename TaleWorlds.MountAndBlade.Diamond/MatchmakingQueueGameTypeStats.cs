using System;
using System.Linq;
using Newtonsoft.Json;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200013B RID: 315
	[Serializable]
	public class MatchmakingQueueGameTypeStats
	{
		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000881 RID: 2177 RVA: 0x0000C823 File Offset: 0x0000AA23
		// (set) Token: 0x06000882 RID: 2178 RVA: 0x0000C82B File Offset: 0x0000AA2B
		[JsonProperty]
		public string[] GameTypes { get; set; }

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000883 RID: 2179 RVA: 0x0000C834 File Offset: 0x0000AA34
		// (set) Token: 0x06000884 RID: 2180 RVA: 0x0000C83C File Offset: 0x0000AA3C
		[JsonProperty]
		public int Count { get; set; }

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000885 RID: 2181 RVA: 0x0000C845 File Offset: 0x0000AA45
		// (set) Token: 0x06000886 RID: 2182 RVA: 0x0000C84D File Offset: 0x0000AA4D
		[JsonProperty]
		public int TotalWaitTime { get; set; }

		// Token: 0x06000887 RID: 2183 RVA: 0x0000C856 File Offset: 0x0000AA56
		public MatchmakingQueueGameTypeStats()
		{
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0000C85E File Offset: 0x0000AA5E
		public MatchmakingQueueGameTypeStats(string[] gameTypes)
		{
			this.GameTypes = gameTypes;
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0000C86D File Offset: 0x0000AA6D
		public bool HasGameType(string gameType)
		{
			return this.GameTypes.Contains(gameType);
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x0000C87C File Offset: 0x0000AA7C
		public bool EqualWith(string[] gameTypes)
		{
			if (this.GameTypes.Length == gameTypes.Length)
			{
				foreach (string gameType in gameTypes)
				{
					if (!this.HasGameType(gameType))
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0000C8B8 File Offset: 0x0000AAB8
		internal bool HasAnyGameType(string[] gameTypes)
		{
			foreach (string gameType in gameTypes)
			{
				if (this.HasGameType(gameType))
				{
					return true;
				}
			}
			return false;
		}
	}
}
