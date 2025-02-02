using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TaleWorlds.Library;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200015B RID: 347
	public static class RecentPlayersManager
	{
		// Token: 0x1700030F RID: 783
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x0000E3F8 File Offset: 0x0000C5F8
		private static PlatformFilePath RecentPlayerFilePath
		{
			get
			{
				PlatformDirectoryPath folderPath = new PlatformDirectoryPath(PlatformFileType.User, "Data");
				return new PlatformFilePath(folderPath, "RecentPlayers.json");
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x060009B2 RID: 2482 RVA: 0x0000E41D File Offset: 0x0000C61D
		public static MBReadOnlyList<RecentPlayerInfo> RecentPlayers
		{
			get
			{
				return RecentPlayersManager._recentPlayers;
			}
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x0000E424 File Offset: 0x0000C624
		static RecentPlayersManager()
		{
			RecentPlayersManager._recentPlayers = new MBList<RecentPlayerInfo>();
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0000E490 File Offset: 0x0000C690
		public static async void Initialize()
		{
			await RecentPlayersManager.LoadRecentPlayers();
			RecentPlayersManager.DecayPlayers();
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0000E4C4 File Offset: 0x0000C6C4
		private static async Task LoadRecentPlayers()
		{
			if (RecentPlayersManager.IsRecentPlayersCacheDirty)
			{
				if (Common.PlatformFileHelper.FileExists(RecentPlayersManager.RecentPlayerFilePath))
				{
					try
					{
						TaskAwaiter<string> taskAwaiter = FileHelper.GetFileContentStringAsync(RecentPlayersManager.RecentPlayerFilePath).GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							await taskAwaiter;
							TaskAwaiter<string> taskAwaiter2;
							taskAwaiter = taskAwaiter2;
							taskAwaiter2 = default(TaskAwaiter<string>);
						}
						RecentPlayersManager._recentPlayers = JsonConvert.DeserializeObject<MBList<RecentPlayerInfo>>(taskAwaiter.GetResult());
						if (RecentPlayersManager._recentPlayers == null)
						{
							RecentPlayersManager._recentPlayers = new MBList<RecentPlayerInfo>();
							throw new Exception("_recentPlayers were null.");
						}
					}
					catch (Exception ex)
					{
						Debug.FailedAssert("Could not recent players. " + ex.Message, "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\RecentPlayersManager.cs", "LoadRecentPlayers", 80);
						try
						{
							FileHelper.DeleteFile(RecentPlayersManager.RecentPlayerFilePath);
						}
						catch (Exception ex2)
						{
							Debug.FailedAssert("Could not delete recent players file. " + ex2.Message, "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\RecentPlayersManager.cs", "LoadRecentPlayers", 87);
						}
					}
				}
				RecentPlayersManager.IsRecentPlayersCacheDirty = false;
			}
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0000E504 File Offset: 0x0000C704
		public static async Task<MBReadOnlyList<RecentPlayerInfo>> GetRecentPlayerInfos()
		{
			await RecentPlayersManager.LoadRecentPlayers();
			return RecentPlayersManager.RecentPlayers;
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x0000E541 File Offset: 0x0000C741
		public static PlayerId[] GetRecentPlayerIds()
		{
			return (from p in RecentPlayersManager._recentPlayers
			select PlayerId.FromString(p.PlayerId)).ToArray<PlayerId>();
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0000E574 File Offset: 0x0000C774
		public static void AddOrUpdatePlayerEntry(PlayerId playerId, string playerName, InteractionType interactionType, int forcedIndex)
		{
			if (forcedIndex == -1)
			{
				object lockObject = RecentPlayersManager._lockObject;
				lock (lockObject)
				{
					RecentPlayersManager.InteractionTypeInfo interactionTypeInfo = RecentPlayersManager.InteractionTypeScoreDictionary[interactionType];
					RecentPlayerInfo recentPlayerInfo = RecentPlayersManager.TryGetPlayer(playerId);
					if (recentPlayerInfo != null)
					{
						if (interactionTypeInfo.ProcessType == RecentPlayersManager.InteractionTypeInfo.InteractionProcessType.Cumulative)
						{
							recentPlayerInfo.ImportanceScore += interactionTypeInfo.Score;
						}
						else if (interactionTypeInfo.ProcessType == RecentPlayersManager.InteractionTypeInfo.InteractionProcessType.Fixed)
						{
							recentPlayerInfo.ImportanceScore += Math.Max(interactionTypeInfo.Score, recentPlayerInfo.ImportanceScore);
						}
						recentPlayerInfo.PlayerName = playerName;
						recentPlayerInfo.InteractionTime = DateTime.Now;
					}
					else
					{
						recentPlayerInfo = new RecentPlayerInfo();
						recentPlayerInfo.PlayerId = playerId.ToString();
						recentPlayerInfo.ImportanceScore = interactionTypeInfo.Score;
						recentPlayerInfo.InteractionTime = DateTime.Now;
						recentPlayerInfo.PlayerName = playerName;
						RecentPlayersManager._recentPlayers.Add(recentPlayerInfo);
					}
					Action<PlayerId, InteractionType> onRecentPlayerInteraction = RecentPlayersManager.OnRecentPlayerInteraction;
					if (onRecentPlayerInteraction != null)
					{
						onRecentPlayerInteraction(playerId, interactionType);
					}
				}
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060009B9 RID: 2489 RVA: 0x0000E678 File Offset: 0x0000C878
		// (remove) Token: 0x060009BA RID: 2490 RVA: 0x0000E6AC File Offset: 0x0000C8AC
		public static event Action<PlayerId, InteractionType> OnRecentPlayerInteraction;

		// Token: 0x060009BB RID: 2491 RVA: 0x0000E6E0 File Offset: 0x0000C8E0
		private static void DecayPlayers()
		{
			object lockObject = RecentPlayersManager._lockObject;
			lock (lockObject)
			{
				List<RecentPlayerInfo> list = new List<RecentPlayerInfo>();
				DateTime now = DateTime.Now;
				foreach (RecentPlayerInfo recentPlayerInfo in RecentPlayersManager._recentPlayers)
				{
					recentPlayerInfo.ImportanceScore -= (int)(now - recentPlayerInfo.InteractionTime).TotalHours;
					if (recentPlayerInfo.ImportanceScore <= 0)
					{
						list.Add(recentPlayerInfo);
					}
				}
				foreach (RecentPlayerInfo item in list)
				{
					RecentPlayersManager._recentPlayers.Remove(item);
				}
			}
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x0000E7DC File Offset: 0x0000C9DC
		public static void Serialize()
		{
			try
			{
				byte[] data = Common.SerializeObjectAsJson(RecentPlayersManager._recentPlayers);
				FileHelper.SaveFile(RecentPlayersManager.RecentPlayerFilePath, data);
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
			}
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x0000E81C File Offset: 0x0000CA1C
		public static IEnumerable<PlayerId> GetPlayersOrdered()
		{
			return from p in RecentPlayersManager._recentPlayers
			orderby p.InteractionTime descending
			select PlayerId.FromString(p.PlayerId);
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x0000E878 File Offset: 0x0000CA78
		private static RecentPlayerInfo TryGetPlayer(PlayerId playerId)
		{
			string b = playerId.ToString();
			foreach (RecentPlayerInfo recentPlayerInfo in RecentPlayersManager._recentPlayers)
			{
				if (recentPlayerInfo.PlayerId == b)
				{
					return recentPlayerInfo;
				}
			}
			return null;
		}

		// Token: 0x040003F9 RID: 1017
		private const string RecentPlayersDirectoryName = "Data";

		// Token: 0x040003FA RID: 1018
		private const string RecentPlayersFileName = "RecentPlayers.json";

		// Token: 0x040003FB RID: 1019
		private static bool IsRecentPlayersCacheDirty = true;

		// Token: 0x040003FC RID: 1020
		private static readonly object _lockObject = new object();

		// Token: 0x040003FD RID: 1021
		private static MBList<RecentPlayerInfo> _recentPlayers;

		// Token: 0x040003FE RID: 1022
		private static readonly Dictionary<InteractionType, RecentPlayersManager.InteractionTypeInfo> InteractionTypeScoreDictionary = new Dictionary<InteractionType, RecentPlayersManager.InteractionTypeInfo>
		{
			{
				InteractionType.Killed,
				new RecentPlayersManager.InteractionTypeInfo(5, RecentPlayersManager.InteractionTypeInfo.InteractionProcessType.Cumulative)
			},
			{
				InteractionType.KilledBy,
				new RecentPlayersManager.InteractionTypeInfo(5, RecentPlayersManager.InteractionTypeInfo.InteractionProcessType.Cumulative)
			},
			{
				InteractionType.InGameTogether,
				new RecentPlayersManager.InteractionTypeInfo(24, RecentPlayersManager.InteractionTypeInfo.InteractionProcessType.Fixed)
			},
			{
				InteractionType.InPartyTogether,
				new RecentPlayersManager.InteractionTypeInfo(48, RecentPlayersManager.InteractionTypeInfo.InteractionProcessType.Fixed)
			}
		};

		// Token: 0x020001DC RID: 476
		private class InteractionTypeInfo
		{
			// Token: 0x1700036A RID: 874
			// (get) Token: 0x06000B98 RID: 2968 RVA: 0x0001749E File Offset: 0x0001569E
			// (set) Token: 0x06000B99 RID: 2969 RVA: 0x000174A6 File Offset: 0x000156A6
			public int Score { get; private set; }

			// Token: 0x1700036B RID: 875
			// (get) Token: 0x06000B9A RID: 2970 RVA: 0x000174AF File Offset: 0x000156AF
			// (set) Token: 0x06000B9B RID: 2971 RVA: 0x000174B7 File Offset: 0x000156B7
			public RecentPlayersManager.InteractionTypeInfo.InteractionProcessType ProcessType { get; private set; }

			// Token: 0x06000B9C RID: 2972 RVA: 0x000174C0 File Offset: 0x000156C0
			public InteractionTypeInfo(int score, RecentPlayersManager.InteractionTypeInfo.InteractionProcessType type)
			{
				this.Score = score;
				this.ProcessType = type;
			}

			// Token: 0x020001EE RID: 494
			public enum InteractionProcessType
			{
				// Token: 0x040006FF RID: 1791
				Cumulative,
				// Token: 0x04000700 RID: 1792
				Fixed
			}
		}
	}
}
