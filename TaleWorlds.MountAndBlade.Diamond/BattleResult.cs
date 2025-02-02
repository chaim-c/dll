using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TaleWorlds.Library;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x020000FD RID: 253
	[Serializable]
	public class BattleResult
	{
		// Token: 0x06000502 RID: 1282 RVA: 0x00005900 File Offset: 0x00003B00
		public BattleResult()
		{
			this.PlayerEntries = new Dictionary<string, BattlePlayerEntry>();
			this.IsCancelled = false;
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0000591C File Offset: 0x00003B1C
		public void AddOrUpdatePlayerEntry(PlayerId playerId, int teamNo, string gameMode, Guid party, int overriddenInitialPlayTime = -1)
		{
			BattlePlayerEntry battlePlayerEntry;
			if (this.PlayerEntries.TryGetValue(playerId.ToString(), out battlePlayerEntry))
			{
				battlePlayerEntry.TeamNo = teamNo;
				battlePlayerEntry.Party = party;
				battlePlayerEntry.GameType = gameMode;
				if (battlePlayerEntry.Disconnected)
				{
					battlePlayerEntry.Disconnected = false;
					battlePlayerEntry.LastJoinTime = DateTime.Now;
					return;
				}
			}
			else
			{
				BattlePlayerStatsBase playerStats = this.CreatePlayerBattleStats(gameMode);
				battlePlayerEntry = new BattlePlayerEntry();
				battlePlayerEntry.PlayerId = playerId;
				battlePlayerEntry.TeamNo = teamNo;
				battlePlayerEntry.Party = party;
				battlePlayerEntry.GameType = gameMode;
				battlePlayerEntry.PlayerStats = playerStats;
				battlePlayerEntry.LastJoinTime = DateTime.Now;
				battlePlayerEntry.PlayTime = ((overriddenInitialPlayTime != -1) ? overriddenInitialPlayTime : 0);
				battlePlayerEntry.Disconnected = false;
				this.PlayerEntries.Add(playerId.ToString(), battlePlayerEntry);
			}
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x000059E6 File Offset: 0x00003BE6
		public bool TryGetPlayerEntry(PlayerId playerId, out BattlePlayerEntry battlePlayerEntry)
		{
			return this.PlayerEntries.TryGetValue(playerId.ToString(), out battlePlayerEntry);
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00005A04 File Offset: 0x00003C04
		public void HandlePlayerDisconnect(PlayerId playerId)
		{
			BattlePlayerEntry battlePlayerEntry;
			if (this.PlayerEntries.TryGetValue(playerId.ToString(), out battlePlayerEntry))
			{
				battlePlayerEntry.Disconnected = true;
				battlePlayerEntry.PlayTime += (int)(DateTime.Now - battlePlayerEntry.LastJoinTime).TotalSeconds;
			}
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00005A5C File Offset: 0x00003C5C
		public void DebugPrint()
		{
			Debug.Print("-----PRINTING BATTLE RESULT-----", 0, Debug.DebugColor.White, 17592186044416UL);
			foreach (BattlePlayerEntry battlePlayerEntry in this.PlayerEntries.Values)
			{
				Debug.Print("Player: " + battlePlayerEntry.PlayerId + "[DEBUG] ", 0, Debug.DebugColor.White, 17592186044416UL);
				Debug.Print("Kill: " + battlePlayerEntry.PlayerStats.Kills + "[DEBUG] ", 0, Debug.DebugColor.White, 17592186044416UL);
				Debug.Print("Death: " + battlePlayerEntry.PlayerStats.Deaths + "[DEBUG] ", 0, Debug.DebugColor.White, 17592186044416UL);
				Debug.Print("----", 0, Debug.DebugColor.White, 17592186044416UL);
			}
			Debug.Print("-----PRINTING OVER-----", 0, Debug.DebugColor.White, 17592186044416UL);
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00005B80 File Offset: 0x00003D80
		public void SetBattleFinished(int winnerTeamNo, bool isPremadeGame, PremadeGameType premadeGameType)
		{
			this.WinnerTeamNo = winnerTeamNo;
			this.IsPremadeGame = isPremadeGame;
			this.PremadeGameType = premadeGameType;
			foreach (BattlePlayerEntry battlePlayerEntry in this.PlayerEntries.Values)
			{
				battlePlayerEntry.Won = (battlePlayerEntry.TeamNo == winnerTeamNo);
				if (!battlePlayerEntry.Disconnected)
				{
					battlePlayerEntry.PlayTime += (int)(DateTime.Now - battlePlayerEntry.LastJoinTime).TotalSeconds;
				}
			}
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00005C24 File Offset: 0x00003E24
		public void SetBattleCancelled()
		{
			this.IsCancelled = true;
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00005C30 File Offset: 0x00003E30
		private BattlePlayerStatsBase CreatePlayerBattleStats(string gameType)
		{
			if (gameType == "Skirmish")
			{
				return new BattlePlayerStatsSkirmish();
			}
			if (gameType == "Captain")
			{
				return new BattlePlayerStatsCaptain();
			}
			if (gameType == "Siege")
			{
				return new BattlePlayerStatsSiege();
			}
			if (gameType == "TeamDeathmatch")
			{
				return new BattlePlayerStatsTeamDeathmatch();
			}
			if (gameType == "Duel")
			{
				return new BattlePlayerStatsDuel();
			}
			if (gameType == "Battle")
			{
				return new BattlePlayerStatsBattle();
			}
			return new BattlePlayerStatsBase();
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x00005CB4 File Offset: 0x00003EB4
		// (set) Token: 0x0600050B RID: 1291 RVA: 0x00005CBC File Offset: 0x00003EBC
		[JsonProperty]
		public bool IsCancelled { get; private set; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x00005CC5 File Offset: 0x00003EC5
		// (set) Token: 0x0600050D RID: 1293 RVA: 0x00005CCD File Offset: 0x00003ECD
		[JsonProperty]
		public int WinnerTeamNo { get; private set; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x00005CD6 File Offset: 0x00003ED6
		// (set) Token: 0x0600050F RID: 1295 RVA: 0x00005CDE File Offset: 0x00003EDE
		[JsonProperty]
		public bool IsPremadeGame { get; private set; }

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x00005CE7 File Offset: 0x00003EE7
		// (set) Token: 0x06000511 RID: 1297 RVA: 0x00005CEF File Offset: 0x00003EEF
		[JsonProperty]
		public PremadeGameType PremadeGameType { get; private set; }

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x00005CF8 File Offset: 0x00003EF8
		// (set) Token: 0x06000513 RID: 1299 RVA: 0x00005D00 File Offset: 0x00003F00
		[JsonProperty]
		public Dictionary<string, BattlePlayerEntry> PlayerEntries { get; private set; }
	}
}
