using System;
using Newtonsoft.Json;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200014E RID: 334
	[JsonConverter(typeof(PlayerStatsBaseJsonConverter))]
	[Serializable]
	public class PlayerStatsBase
	{
		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000927 RID: 2343 RVA: 0x0000D984 File Offset: 0x0000BB84
		// (set) Token: 0x06000928 RID: 2344 RVA: 0x0000D98C File Offset: 0x0000BB8C
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000929 RID: 2345 RVA: 0x0000D995 File Offset: 0x0000BB95
		// (set) Token: 0x0600092A RID: 2346 RVA: 0x0000D99D File Offset: 0x0000BB9D
		[JsonProperty]
		public int KillCount { get; set; }

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x0000D9A6 File Offset: 0x0000BBA6
		// (set) Token: 0x0600092C RID: 2348 RVA: 0x0000D9AE File Offset: 0x0000BBAE
		[JsonProperty]
		public int DeathCount { get; set; }

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x0000D9B7 File Offset: 0x0000BBB7
		// (set) Token: 0x0600092E RID: 2350 RVA: 0x0000D9BF File Offset: 0x0000BBBF
		[JsonProperty]
		public int AssistCount { get; set; }

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x0000D9C8 File Offset: 0x0000BBC8
		// (set) Token: 0x06000930 RID: 2352 RVA: 0x0000D9D0 File Offset: 0x0000BBD0
		[JsonProperty]
		public int WinCount { get; set; }

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000931 RID: 2353 RVA: 0x0000D9D9 File Offset: 0x0000BBD9
		// (set) Token: 0x06000932 RID: 2354 RVA: 0x0000D9E1 File Offset: 0x0000BBE1
		[JsonProperty]
		public int LoseCount { get; set; }

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x0000D9EA File Offset: 0x0000BBEA
		// (set) Token: 0x06000934 RID: 2356 RVA: 0x0000D9F2 File Offset: 0x0000BBF2
		[JsonProperty]
		public int ForfeitCount { get; set; }

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000935 RID: 2357 RVA: 0x0000D9FB File Offset: 0x0000BBFB
		[JsonIgnore]
		public float AverageKillPerDeath
		{
			get
			{
				return (float)this.KillCount / (float)((this.DeathCount != 0) ? this.DeathCount : 1);
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000936 RID: 2358 RVA: 0x0000DA17 File Offset: 0x0000BC17
		// (set) Token: 0x06000937 RID: 2359 RVA: 0x0000DA1F File Offset: 0x0000BC1F
		[JsonProperty]
		public string GameType { get; set; }

		// Token: 0x06000939 RID: 2361 RVA: 0x0000DA30 File Offset: 0x0000BC30
		public void FillWith(PlayerId playerId, int killCount, int deathCount, int assistCount, int winCount, int loseCount, int forfeitCount)
		{
			this.PlayerId = playerId;
			this.KillCount = killCount;
			this.DeathCount = deathCount;
			this.AssistCount = assistCount;
			this.WinCount = winCount;
			this.LoseCount = loseCount;
			this.ForfeitCount = forfeitCount;
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x0000DA68 File Offset: 0x0000BC68
		public virtual void Update(BattlePlayerStatsBase battleStats, bool won)
		{
			this.KillCount += battleStats.Kills;
			this.DeathCount += battleStats.Deaths;
			this.AssistCount += battleStats.Assists;
			int num;
			if (won)
			{
				num = this.WinCount;
				this.WinCount = num + 1;
				return;
			}
			num = this.LoseCount;
			this.LoseCount = num + 1;
		}
	}
}
