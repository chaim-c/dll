using System;
using System.Collections.Generic;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200013C RID: 316
	[Serializable]
	public class MatchmakingWaitTimeStats
	{
		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x0000C8E5 File Offset: 0x0000AAE5
		// (set) Token: 0x0600088D RID: 2189 RVA: 0x0000C8EC File Offset: 0x0000AAEC
		public static MatchmakingWaitTimeStats Empty { get; private set; } = new MatchmakingWaitTimeStats();

		// Token: 0x0600088F RID: 2191 RVA: 0x0000C900 File Offset: 0x0000AB00
		public MatchmakingWaitTimeStats()
		{
			this._regionStats = new List<MatchmakingWaitTimeRegionStats>();
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0000C913 File Offset: 0x0000AB13
		public void AddRegionStats(MatchmakingWaitTimeRegionStats regionStats)
		{
			this._regionStats.Add(regionStats);
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x0000C924 File Offset: 0x0000AB24
		public MatchmakingWaitTimeRegionStats GetRegionStats(string region)
		{
			foreach (MatchmakingWaitTimeRegionStats matchmakingWaitTimeRegionStats in this._regionStats)
			{
				if (matchmakingWaitTimeRegionStats.Region.ToLower() == region.ToLower())
				{
					return matchmakingWaitTimeRegionStats;
				}
			}
			return null;
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x0000C990 File Offset: 0x0000AB90
		public int GetWaitTime(string region, string gameType, WaitTimeStatType statType)
		{
			int result = 0;
			if (!string.IsNullOrEmpty(region) && !string.IsNullOrEmpty(gameType))
			{
				MatchmakingWaitTimeRegionStats regionStats = this.GetRegionStats(region);
				if (regionStats != null)
				{
					result = regionStats.GetWaitTime(gameType, statType);
				}
			}
			return result;
		}

		// Token: 0x04000363 RID: 867
		private List<MatchmakingWaitTimeRegionStats> _regionStats;
	}
}
