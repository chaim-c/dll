using System;
using System.Collections.Generic;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200013E RID: 318
	[Serializable]
	public class MatchmakingWaitTimeRegionStats
	{
		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000893 RID: 2195 RVA: 0x0000C9C4 File Offset: 0x0000ABC4
		// (set) Token: 0x06000894 RID: 2196 RVA: 0x0000C9CC File Offset: 0x0000ABCC
		public string Region { get; private set; }

		// Token: 0x06000895 RID: 2197 RVA: 0x0000C9D5 File Offset: 0x0000ABD5
		public MatchmakingWaitTimeRegionStats(string region)
		{
			this.Region = region;
			this._gameTypeAverageWaitTimes = new Dictionary<string, Dictionary<WaitTimeStatType, int>>();
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0000C9F0 File Offset: 0x0000ABF0
		public void SetGameTypeAverage(string gameType, WaitTimeStatType statType, int average)
		{
			Dictionary<WaitTimeStatType, int> value;
			if (!this._gameTypeAverageWaitTimes.TryGetValue(gameType, out value))
			{
				value = new Dictionary<WaitTimeStatType, int>();
				this._gameTypeAverageWaitTimes.Add(gameType, value);
			}
			this._gameTypeAverageWaitTimes[gameType][statType] = average;
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x0000CA33 File Offset: 0x0000AC33
		public bool HasStatsForGameType(string gameType)
		{
			return gameType != null && this._gameTypeAverageWaitTimes.ContainsKey(gameType);
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x0000CA48 File Offset: 0x0000AC48
		public int GetWaitTime(string gameType, WaitTimeStatType statType)
		{
			Dictionary<WaitTimeStatType, int> dictionary;
			int result;
			if (this._gameTypeAverageWaitTimes.TryGetValue(gameType, out dictionary) && dictionary.TryGetValue(statType, out result))
			{
				return result;
			}
			return int.MaxValue;
		}

		// Token: 0x04000369 RID: 873
		private Dictionary<string, Dictionary<WaitTimeStatType, int>> _gameTypeAverageWaitTimes;
	}
}
