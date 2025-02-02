using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000139 RID: 313
	[Serializable]
	public class MatchmakingQueueStats
	{
		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000867 RID: 2151 RVA: 0x0000C471 File Offset: 0x0000A671
		// (set) Token: 0x06000868 RID: 2152 RVA: 0x0000C478 File Offset: 0x0000A678
		public static MatchmakingQueueStats Empty { get; private set; } = new MatchmakingQueueStats();

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000869 RID: 2153 RVA: 0x0000C480 File Offset: 0x0000A680
		[JsonIgnore]
		public int TotalCount
		{
			get
			{
				int num = 0;
				foreach (MatchmakingQueueRegionStats matchmakingQueueRegionStats in this.RegionStats)
				{
					num += matchmakingQueueRegionStats.TotalCount;
				}
				return num;
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x0600086A RID: 2154 RVA: 0x0000C4D8 File Offset: 0x0000A6D8
		[JsonIgnore]
		public int AverageWaitTime
		{
			get
			{
				int result = 0;
				int num = 0;
				if (this.RegionStats.Count > 0)
				{
					foreach (MatchmakingQueueRegionStats matchmakingQueueRegionStats in this.RegionStats)
					{
						num += matchmakingQueueRegionStats.AverageWaitTime;
					}
					result = num / this.RegionStats.Count;
				}
				return result;
			}
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x0000C55C File Offset: 0x0000A75C
		public MatchmakingQueueStats()
		{
			this.RegionStats = new List<MatchmakingQueueRegionStats>();
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x0000C56F File Offset: 0x0000A76F
		public void AddRegionStats(MatchmakingQueueRegionStats matchmakingQueueRegionStats)
		{
			this.RegionStats.Add(matchmakingQueueRegionStats);
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x0000C580 File Offset: 0x0000A780
		public MatchmakingQueueRegionStats GetRegionStats(string region)
		{
			foreach (MatchmakingQueueRegionStats matchmakingQueueRegionStats in this.RegionStats)
			{
				if (matchmakingQueueRegionStats.Region.ToLower() == region.ToLower())
				{
					return matchmakingQueueRegionStats;
				}
			}
			return null;
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0000C5EC File Offset: 0x0000A7EC
		public int GetQueueCountOf(string region, string[] gameTypes)
		{
			int result = 0;
			if (!string.IsNullOrEmpty(region) && gameTypes != null)
			{
				MatchmakingQueueRegionStats regionStats = this.GetRegionStats(region);
				if (regionStats != null)
				{
					result = regionStats.GetQueueCountOf(gameTypes);
				}
			}
			return result;
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0000C61C File Offset: 0x0000A81C
		public string[] GetRegionNames()
		{
			string[] array = new string[this.RegionStats.Count];
			for (int i = 0; i < this.RegionStats.Count; i++)
			{
				array[i] = this.RegionStats[i].Region;
			}
			return array;
		}

		// Token: 0x04000358 RID: 856
		[JsonProperty]
		public List<MatchmakingQueueRegionStats> RegionStats;
	}
}
