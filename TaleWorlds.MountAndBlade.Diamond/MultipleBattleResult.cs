using System;
using System.Collections.Generic;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x020000FE RID: 254
	[Serializable]
	public class MultipleBattleResult
	{
		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x00005D09 File Offset: 0x00003F09
		// (set) Token: 0x06000515 RID: 1301 RVA: 0x00005D11 File Offset: 0x00003F11
		public List<BattleResult> BattleResults { get; set; }

		// Token: 0x06000516 RID: 1302 RVA: 0x00005D1A File Offset: 0x00003F1A
		public MultipleBattleResult()
		{
			this.BattleResults = new List<BattleResult>();
			this._currentBattleIndex = -1;
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00005D34 File Offset: 0x00003F34
		public void CreateNewBattleResult(string gameType)
		{
			BattleResult battleResult = new BattleResult();
			this.BattleResults.Add(battleResult);
			this._currentBattleIndex++;
			if (this._currentBattleIndex > 0)
			{
				foreach (KeyValuePair<string, BattlePlayerEntry> keyValuePair in this.BattleResults[this._currentBattleIndex - 1].PlayerEntries)
				{
					battleResult.AddOrUpdatePlayerEntry(PlayerId.FromString(keyValuePair.Key), keyValuePair.Value.TeamNo, gameType, Guid.Empty, -1);
				}
			}
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00005DE0 File Offset: 0x00003FE0
		public BattleResult GetCurrentBattleResult()
		{
			return this.BattleResults[this._currentBattleIndex];
		}

		// Token: 0x040001B2 RID: 434
		private int _currentBattleIndex;
	}
}
