using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200013A RID: 314
	[Serializable]
	public class MatchmakingQueueRegionStats
	{
		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000871 RID: 2161 RVA: 0x0000C665 File Offset: 0x0000A865
		// (set) Token: 0x06000872 RID: 2162 RVA: 0x0000C66D File Offset: 0x0000A86D
		[JsonProperty]
		public string Region { get; set; }

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000873 RID: 2163 RVA: 0x0000C678 File Offset: 0x0000A878
		[JsonIgnore]
		public int TotalCount
		{
			get
			{
				int num = 0;
				foreach (MatchmakingQueueGameTypeStats matchmakingQueueGameTypeStats in this.GameTypeStats)
				{
					num += matchmakingQueueGameTypeStats.Count;
				}
				return num;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000874 RID: 2164 RVA: 0x0000C6D0 File Offset: 0x0000A8D0
		// (set) Token: 0x06000875 RID: 2165 RVA: 0x0000C6D8 File Offset: 0x0000A8D8
		[JsonProperty]
		public int MaxWaitTime { get; set; }

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000876 RID: 2166 RVA: 0x0000C6E1 File Offset: 0x0000A8E1
		// (set) Token: 0x06000877 RID: 2167 RVA: 0x0000C6E9 File Offset: 0x0000A8E9
		[JsonProperty]
		public int MinWaitTime { get; set; }

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000878 RID: 2168 RVA: 0x0000C6F2 File Offset: 0x0000A8F2
		// (set) Token: 0x06000879 RID: 2169 RVA: 0x0000C6FA File Offset: 0x0000A8FA
		[JsonProperty]
		public int MedianWaitTime { get; set; }

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x0600087A RID: 2170 RVA: 0x0000C703 File Offset: 0x0000A903
		// (set) Token: 0x0600087B RID: 2171 RVA: 0x0000C70B File Offset: 0x0000A90B
		[JsonProperty]
		public int AverageWaitTime { get; set; }

		// Token: 0x0600087C RID: 2172 RVA: 0x0000C714 File Offset: 0x0000A914
		public MatchmakingQueueRegionStats(string region)
		{
			this.Region = region;
			this.GameTypeStats = new List<MatchmakingQueueGameTypeStats>();
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x0000C730 File Offset: 0x0000A930
		public MatchmakingQueueGameTypeStats GetQueueCountObjectOf(string[] gameTypes)
		{
			if (gameTypes != null)
			{
				foreach (MatchmakingQueueGameTypeStats matchmakingQueueGameTypeStats in this.GameTypeStats)
				{
					if (matchmakingQueueGameTypeStats.EqualWith(gameTypes))
					{
						return matchmakingQueueGameTypeStats;
					}
				}
			}
			return null;
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x0000C790 File Offset: 0x0000A990
		public void AddStats(MatchmakingQueueGameTypeStats matchmakingQueueGameTypeStats)
		{
			this.GameTypeStats.Add(matchmakingQueueGameTypeStats);
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x0000C7A0 File Offset: 0x0000A9A0
		public int GetQueueCountOf(string[] gameTypes)
		{
			int num = 0;
			if (gameTypes != null)
			{
				foreach (MatchmakingQueueGameTypeStats matchmakingQueueGameTypeStats in this.GameTypeStats)
				{
					if (matchmakingQueueGameTypeStats.HasAnyGameType(gameTypes))
					{
						num += matchmakingQueueGameTypeStats.Count;
					}
				}
			}
			return num;
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x0000C804 File Offset: 0x0000AA04
		public void SetWaitTimeStats(int averageWaitTime, int maxWaitTime, int minWaitTime, int medianWaitTime)
		{
			this.AverageWaitTime = averageWaitTime;
			this.MaxWaitTime = maxWaitTime;
			this.MinWaitTime = minWaitTime;
			this.MedianWaitTime = medianWaitTime;
		}

		// Token: 0x0400035A RID: 858
		[JsonProperty]
		public List<MatchmakingQueueGameTypeStats> GameTypeStats;
	}
}
