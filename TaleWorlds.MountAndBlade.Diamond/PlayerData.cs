using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade.Diamond.MultiplayerBadges;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200014B RID: 331
	[Serializable]
	public class PlayerData
	{
		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x060008DD RID: 2269 RVA: 0x0000D336 File Offset: 0x0000B536
		// (set) Token: 0x060008DE RID: 2270 RVA: 0x0000D33E File Offset: 0x0000B53E
		public PlayerId PlayerId { get; set; }

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x060008DF RID: 2271 RVA: 0x0000D347 File Offset: 0x0000B547
		// (set) Token: 0x060008E0 RID: 2272 RVA: 0x0000D34F File Offset: 0x0000B54F
		public PlayerId OwnerPlayerId { get; set; }

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x060008E1 RID: 2273 RVA: 0x0000D358 File Offset: 0x0000B558
		// (set) Token: 0x060008E2 RID: 2274 RVA: 0x0000D360 File Offset: 0x0000B560
		public string Sigil { get; set; }

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x060008E3 RID: 2275 RVA: 0x0000D369 File Offset: 0x0000B569
		// (set) Token: 0x060008E4 RID: 2276 RVA: 0x0000D371 File Offset: 0x0000B571
		public BodyProperties BodyProperties
		{
			get
			{
				return this._bodyProperties;
			}
			set
			{
				this.SetBodyProperties(value);
			}
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x0000D37A File Offset: 0x0000B57A
		private void SetBodyProperties(BodyProperties bodyProperties)
		{
			this._bodyProperties = bodyProperties.ClampForMultiplayer();
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x060008E6 RID: 2278 RVA: 0x0000D389 File Offset: 0x0000B589
		[JsonIgnore]
		public int ShownBadgeIndex
		{
			get
			{
				Badge byId = BadgeManager.GetById(this.ShownBadgeId);
				if (byId == null)
				{
					return -1;
				}
				return byId.Index;
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x0000D3A1 File Offset: 0x0000B5A1
		// (set) Token: 0x060008E8 RID: 2280 RVA: 0x0000D3A9 File Offset: 0x0000B5A9
		public PlayerStatsBase[] Stats { get; set; }

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x0000D3B2 File Offset: 0x0000B5B2
		// (set) Token: 0x060008EA RID: 2282 RVA: 0x0000D3BA File Offset: 0x0000B5BA
		public int Race { get; set; }

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x060008EB RID: 2283 RVA: 0x0000D3C3 File Offset: 0x0000B5C3
		// (set) Token: 0x060008EC RID: 2284 RVA: 0x0000D3CB File Offset: 0x0000B5CB
		public bool IsFemale { get; set; }

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x060008ED RID: 2285 RVA: 0x0000D3D4 File Offset: 0x0000B5D4
		[JsonIgnore]
		public int KillCount
		{
			get
			{
				int num = 0;
				if (this.Stats != null)
				{
					foreach (PlayerStatsBase playerStatsBase in this.Stats)
					{
						num += playerStatsBase.KillCount;
					}
				}
				return num;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x060008EE RID: 2286 RVA: 0x0000D410 File Offset: 0x0000B610
		[JsonIgnore]
		public int DeathCount
		{
			get
			{
				int num = 0;
				if (this.Stats != null)
				{
					foreach (PlayerStatsBase playerStatsBase in this.Stats)
					{
						num += playerStatsBase.DeathCount;
					}
				}
				return num;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x0000D44C File Offset: 0x0000B64C
		[JsonIgnore]
		public int AssistCount
		{
			get
			{
				int num = 0;
				if (this.Stats != null)
				{
					foreach (PlayerStatsBase playerStatsBase in this.Stats)
					{
						num += playerStatsBase.AssistCount;
					}
				}
				return num;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x060008F0 RID: 2288 RVA: 0x0000D488 File Offset: 0x0000B688
		[JsonIgnore]
		public int WinCount
		{
			get
			{
				int num = 0;
				if (this.Stats != null)
				{
					foreach (PlayerStatsBase playerStatsBase in this.Stats)
					{
						num += playerStatsBase.WinCount;
					}
				}
				return num;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x060008F1 RID: 2289 RVA: 0x0000D4C4 File Offset: 0x0000B6C4
		[JsonIgnore]
		public int LoseCount
		{
			get
			{
				int num = 0;
				if (this.Stats != null)
				{
					foreach (PlayerStatsBase playerStatsBase in this.Stats)
					{
						num += playerStatsBase.LoseCount;
					}
				}
				return num;
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x060008F2 RID: 2290 RVA: 0x0000D4FE File Offset: 0x0000B6FE
		// (set) Token: 0x060008F3 RID: 2291 RVA: 0x0000D506 File Offset: 0x0000B706
		public int Experience { get; set; }

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x060008F4 RID: 2292 RVA: 0x0000D50F File Offset: 0x0000B70F
		// (set) Token: 0x060008F5 RID: 2293 RVA: 0x0000D517 File Offset: 0x0000B717
		public string LastPlayerName { get; set; }

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x060008F6 RID: 2294 RVA: 0x0000D520 File Offset: 0x0000B720
		// (set) Token: 0x060008F7 RID: 2295 RVA: 0x0000D528 File Offset: 0x0000B728
		public string Username { get; set; }

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x060008F8 RID: 2296 RVA: 0x0000D531 File Offset: 0x0000B731
		// (set) Token: 0x060008F9 RID: 2297 RVA: 0x0000D539 File Offset: 0x0000B739
		public int UserId { get; set; }

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x060008FA RID: 2298 RVA: 0x0000D542 File Offset: 0x0000B742
		// (set) Token: 0x060008FB RID: 2299 RVA: 0x0000D54A File Offset: 0x0000B74A
		public bool IsUsingClanSigil { get; set; }

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x060008FC RID: 2300 RVA: 0x0000D553 File Offset: 0x0000B753
		// (set) Token: 0x060008FD RID: 2301 RVA: 0x0000D55B File Offset: 0x0000B75B
		public string LastRegion { get; set; }

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x060008FE RID: 2302 RVA: 0x0000D564 File Offset: 0x0000B764
		// (set) Token: 0x060008FF RID: 2303 RVA: 0x0000D56C File Offset: 0x0000B76C
		public string[] LastGameTypes { get; set; }

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000900 RID: 2304 RVA: 0x0000D575 File Offset: 0x0000B775
		// (set) Token: 0x06000901 RID: 2305 RVA: 0x0000D57D File Offset: 0x0000B77D
		public DateTime? LastLogin { get; set; }

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000902 RID: 2306 RVA: 0x0000D586 File Offset: 0x0000B786
		// (set) Token: 0x06000903 RID: 2307 RVA: 0x0000D58E File Offset: 0x0000B78E
		public int Playtime { get; set; }

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000904 RID: 2308 RVA: 0x0000D597 File Offset: 0x0000B797
		// (set) Token: 0x06000905 RID: 2309 RVA: 0x0000D59F File Offset: 0x0000B79F
		public string ShownBadgeId { get; set; }

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000906 RID: 2310 RVA: 0x0000D5A8 File Offset: 0x0000B7A8
		// (set) Token: 0x06000907 RID: 2311 RVA: 0x0000D5B0 File Offset: 0x0000B7B0
		public int Gold { get; set; }

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000908 RID: 2312 RVA: 0x0000D5B9 File Offset: 0x0000B7B9
		// (set) Token: 0x06000909 RID: 2313 RVA: 0x0000D5C1 File Offset: 0x0000B7C1
		public bool IsMuted { get; set; }

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x0600090A RID: 2314 RVA: 0x0000D5CC File Offset: 0x0000B7CC
		[JsonIgnore]
		public int Level
		{
			get
			{
				return new PlayerDataExperience(this.Experience).Level;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x0600090B RID: 2315 RVA: 0x0000D5EC File Offset: 0x0000B7EC
		[JsonIgnore]
		public int ExperienceToNextLevel
		{
			get
			{
				return new PlayerDataExperience(this.Experience).ExperienceToNextLevel;
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x0600090C RID: 2316 RVA: 0x0000D60C File Offset: 0x0000B80C
		[JsonIgnore]
		public int ExperienceInCurrentLevel
		{
			get
			{
				return new PlayerDataExperience(this.Experience).ExperienceInCurrentLevel;
			}
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x0000D634 File Offset: 0x0000B834
		public void FillWith(PlayerId playerId, PlayerId ownerPlayerId, BodyProperties bodyProperties, bool isFemale, string sigil, int experience, string lastPlayerName, string username, int userId, string lastRegion, string[] lastGameTypes, DateTime? lastLogin, int playtime, string shownBadgeId, int gold, PlayerStatsBase[] stats, bool shouldLog, bool isUsingClanSigil)
		{
			this.PlayerId = playerId;
			this.OwnerPlayerId = ownerPlayerId;
			this.BodyProperties = bodyProperties;
			this.IsFemale = isFemale;
			this.Sigil = sigil;
			this.IsUsingClanSigil = isUsingClanSigil;
			this.Experience = experience;
			this.LastPlayerName = lastPlayerName;
			this.Username = username;
			this.UserId = userId;
			this.LastRegion = lastRegion;
			this.LastGameTypes = lastGameTypes;
			this.LastLogin = lastLogin;
			this.Playtime = playtime;
			this.ShownBadgeId = shownBadgeId;
			this.Gold = gold;
			this.Stats = stats;
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x0000D6C8 File Offset: 0x0000B8C8
		public void FillWithNewPlayer(PlayerId playerId, PlayerId ownerPlayerId, string[] gameTypes)
		{
			this.Stats = new PlayerStatsBase[0];
			this.PlayerId = playerId;
			this.OwnerPlayerId = ownerPlayerId;
			this.Sigil = "11.8.1.4345.4345.770.774.1.0.0.158.7.5.512.512.770.769.1.0.0";
			this.IsUsingClanSigil = false;
			this.LastGameTypes = gameTypes;
			this.Username = null;
			this.UserId = -1;
			this.Gold = 0;
			BodyProperties bodyProperties;
			if (BodyProperties.FromString("<BodyProperties version='4' age='36.35' weight='0.1025' build='0.7'  key='001C380CC000234B88E68BBA1372B7578B7BB5D788BC567878966669835754B604F926450F67798C000000000000000000000000000000000000000000DC10C4' />", out bodyProperties))
			{
				this.BodyProperties = bodyProperties;
			}
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x0000D732 File Offset: 0x0000B932
		public bool HasGameStats(string gameType)
		{
			return this.GetGameStats(gameType) != null;
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x0000D740 File Offset: 0x0000B940
		public PlayerStatsBase GetGameStats(string gameType)
		{
			if (this.Stats != null)
			{
				foreach (PlayerStatsBase playerStatsBase in this.Stats)
				{
					if (playerStatsBase.GameType == gameType)
					{
						return playerStatsBase;
					}
				}
			}
			return null;
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x0000D780 File Offset: 0x0000B980
		public void UpdateGameStats(PlayerStatsBase playerGameTypeStats)
		{
			bool flag = false;
			if (this.Stats != null)
			{
				for (int i = 0; i < this.Stats.Length; i++)
				{
					if (this.Stats[i].GameType == playerGameTypeStats.GameType)
					{
						this.Stats[i] = playerGameTypeStats;
						flag = true;
					}
				}
			}
			if (!flag)
			{
				List<PlayerStatsBase> list = new List<PlayerStatsBase>();
				if (this.Stats != null)
				{
					list.AddRange(this.Stats);
				}
				list.Add(playerGameTypeStats);
				this.Stats = list.ToArray();
			}
		}

		// Token: 0x040003AA RID: 938
		private const string DefaultBodyProperties1 = "<BodyProperties version='4' age='36.35' weight='0.1025' build='0.7'  key='001C380CC000234B88E68BBA1372B7578B7BB5D788BC567878966669835754B604F926450F67798C000000000000000000000000000000000000000000DC10C4' />";

		// Token: 0x040003AB RID: 939
		private const string DefaultBodyProperties2 = "<BodyProperties version='4' age='46.35' weight='0.1025' build='0.7'  key='001C380CC000234B88E68BBA1372B7578B7BB5D788BC567878966669835754B604F926450F67798C000000000000000000000000000000000000000000DC10C4' />";

		// Token: 0x040003AC RID: 940
		public const string DefaultSigil = "11.8.1.4345.4345.770.774.1.0.0.158.7.5.512.512.770.769.1.0.0";

		// Token: 0x040003B0 RID: 944
		private BodyProperties _bodyProperties;
	}
}
