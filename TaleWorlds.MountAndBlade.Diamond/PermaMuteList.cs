using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TaleWorlds.Library;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000145 RID: 325
	public static class PermaMuteList
	{
		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x060008AA RID: 2218 RVA: 0x0000CD46 File Offset: 0x0000AF46
		// (set) Token: 0x060008AB RID: 2219 RVA: 0x0000CD4D File Offset: 0x0000AF4D
		public static bool HasMutedPlayersLoaded { get; private set; }

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x060008AC RID: 2220 RVA: 0x0000CD58 File Offset: 0x0000AF58
		private static PlatformFilePath PermaMuteFilePath
		{
			get
			{
				PlatformDirectoryPath folderPath = new PlatformDirectoryPath(PlatformFileType.User, "Data");
				return new PlatformFilePath(folderPath, "Muted.json");
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x060008AD RID: 2221 RVA: 0x0000CD80 File Offset: 0x0000AF80
		[TupleElementNames(new string[]
		{
			"Id",
			"Name"
		})]
		public static IReadOnlyList<ValueTuple<string, string>> MutedPlayers
		{
			[return: TupleElementNames(new string[]
			{
				"Id",
				"Name"
			})]
			get
			{
				List<ValueTuple<string, string>> result;
				if (!PermaMuteList.HasMutedPlayersLoaded || !PermaMuteList._mutedPlayers.TryGetValue(PermaMuteList.CurrentPlayerId, out result))
				{
					return new List<ValueTuple<string, string>>();
				}
				return result;
			}
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x0000CDBA File Offset: 0x0000AFBA
		public static void SetPermanentMuteAvailableCallback(Func<bool> getPermanentMuteAvailable)
		{
			PermaMuteList._getPermanentMuteAvailable = getPermanentMuteAvailable;
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x0000CDC4 File Offset: 0x0000AFC4
		public static async Task LoadMutedPlayers(PlayerId currentPlayerId)
		{
			PermaMuteList.CurrentPlayerId = currentPlayerId.ToString();
			if (FileHelper.FileExists(PermaMuteList.PermaMuteFilePath))
			{
				try
				{
					Dictionary<string, List<ValueTuple<string, string>>> dictionary = JsonConvert.DeserializeObject<Dictionary<string, List<ValueTuple<string, string>>>>(await FileHelper.GetFileContentStringAsync(PermaMuteList.PermaMuteFilePath));
					if (dictionary != null)
					{
						PermaMuteList._mutedPlayers = dictionary;
					}
					PermaMuteList.HasMutedPlayersLoaded = true;
				}
				catch (Exception ex)
				{
					Debug.FailedAssert("Could not load muted players. " + ex.Message, "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\PermaMuteList.cs", "LoadMutedPlayers", 61);
					try
					{
						FileHelper.DeleteFile(PermaMuteList.PermaMuteFilePath);
					}
					catch (Exception ex2)
					{
						Debug.FailedAssert("Could not delete muted players file. " + ex2.Message, "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\PermaMuteList.cs", "LoadMutedPlayers", 68);
					}
				}
			}
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x0000CE0C File Offset: 0x0000B00C
		public static async void SaveMutedPlayers()
		{
			try
			{
				byte[] data = Common.SerializeObjectAsJson(PermaMuteList._mutedPlayers);
				await FileHelper.SaveFileAsync(PermaMuteList.PermaMuteFilePath, data);
			}
			catch (Exception ex)
			{
				Debug.FailedAssert("Could not save muted players. " + ex.Message, "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\PermaMuteList.cs", "SaveMutedPlayers", 83);
			}
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x0000CE40 File Offset: 0x0000B040
		public static bool IsPlayerMuted(PlayerId player)
		{
			Func<bool> getPermanentMuteAvailable = PermaMuteList._getPermanentMuteAvailable;
			if ((getPermanentMuteAvailable == null || getPermanentMuteAvailable()) && PermaMuteList.CurrentPlayerId != null)
			{
				string b = player.ToString();
				Dictionary<string, List<ValueTuple<string, string>>> mutedPlayers = PermaMuteList._mutedPlayers;
				lock (mutedPlayers)
				{
					List<ValueTuple<string, string>> list;
					if (!PermaMuteList._mutedPlayers.TryGetValue(PermaMuteList.CurrentPlayerId, out list))
					{
						return false;
					}
					using (List<ValueTuple<string, string>>.Enumerator enumerator = list.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current.Item1 == b)
							{
								return true;
							}
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x0000CF08 File Offset: 0x0000B108
		public static void MutePlayer(PlayerId player, string name)
		{
			Func<bool> getPermanentMuteAvailable = PermaMuteList._getPermanentMuteAvailable;
			if (getPermanentMuteAvailable == null || getPermanentMuteAvailable())
			{
				Dictionary<string, List<ValueTuple<string, string>>> mutedPlayers = PermaMuteList._mutedPlayers;
				lock (mutedPlayers)
				{
					List<ValueTuple<string, string>> list;
					if (!PermaMuteList._mutedPlayers.TryGetValue(PermaMuteList.CurrentPlayerId, out list))
					{
						list = new List<ValueTuple<string, string>>();
						PermaMuteList._mutedPlayers.Add(PermaMuteList.CurrentPlayerId, list);
					}
					list.Add(new ValueTuple<string, string>(player.ToString(), name));
				}
			}
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x0000CF98 File Offset: 0x0000B198
		public static void RemoveMutedPlayer(PlayerId player)
		{
			Func<bool> getPermanentMuteAvailable = PermaMuteList._getPermanentMuteAvailable;
			if (getPermanentMuteAvailable == null || getPermanentMuteAvailable())
			{
				string b = player.ToString();
				Dictionary<string, List<ValueTuple<string, string>>> mutedPlayers = PermaMuteList._mutedPlayers;
				lock (mutedPlayers)
				{
					List<ValueTuple<string, string>> list;
					if (PermaMuteList._mutedPlayers.TryGetValue(PermaMuteList.CurrentPlayerId, out list))
					{
						int num = -1;
						for (int i = 0; i < list.Count; i++)
						{
							if (list[i].Item1 == b)
							{
								num = i;
								break;
							}
						}
						if (num >= 0)
						{
							list.RemoveAt(num);
						}
					}
				}
			}
		}

		// Token: 0x04000396 RID: 918
		[TupleElementNames(new string[]
		{
			"Id",
			"Name"
		})]
		private static Dictionary<string, List<ValueTuple<string, string>>> _mutedPlayers = new Dictionary<string, List<ValueTuple<string, string>>>();

		// Token: 0x04000397 RID: 919
		private static string CurrentPlayerId;

		// Token: 0x04000398 RID: 920
		private static Func<bool> _getPermanentMuteAvailable;
	}
}
