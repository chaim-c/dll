using System;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000156 RID: 342
	[Serializable]
	public class PlayerStatsTeamDeathmatch : PlayerStatsBase
	{
		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000983 RID: 2435 RVA: 0x0000E1A9 File Offset: 0x0000C3A9
		// (set) Token: 0x06000984 RID: 2436 RVA: 0x0000E1B1 File Offset: 0x0000C3B1
		public int Score { get; set; }

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000985 RID: 2437 RVA: 0x0000E1BA File Offset: 0x0000C3BA
		public float AverageScore
		{
			get
			{
				return (float)this.Score / (float)((base.WinCount + base.LoseCount != 0) ? (base.WinCount + base.LoseCount) : 1);
			}
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x0000E1E4 File Offset: 0x0000C3E4
		public PlayerStatsTeamDeathmatch()
		{
			base.GameType = "TeamDeathmatch";
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x0000E1F7 File Offset: 0x0000C3F7
		public void FillWith(PlayerId playerId, int killCount, int deathCount, int assistCount, int winCount, int loseCount, int forfeitCount, int score)
		{
			base.FillWith(playerId, killCount, deathCount, assistCount, winCount, loseCount, forfeitCount);
			this.Score = score;
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x0000E214 File Offset: 0x0000C414
		public void FillWithNewPlayer(PlayerId playerId)
		{
			this.FillWith(playerId, 0, 0, 0, 0, 0, 0, 0);
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x0000E22F File Offset: 0x0000C42F
		public void Update(BattlePlayerStatsTeamDeathmatch stats, bool won)
		{
			base.Update(stats, won);
			this.Score += stats.Score;
		}
	}
}
