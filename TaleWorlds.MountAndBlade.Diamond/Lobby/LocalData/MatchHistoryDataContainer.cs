using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.Diamond.Lobby.LocalData
{
	// Token: 0x0200017E RID: 382
	public class MatchHistoryDataContainer : MultiplayerLocalDataContainer<MatchHistoryData>
	{
		// Token: 0x06000AB2 RID: 2738 RVA: 0x00011ADB File Offset: 0x0000FCDB
		public MatchHistoryDataContainer()
		{
			this._matchesToRemove = new List<MatchHistoryData>();
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x00011AEE File Offset: 0x0000FCEE
		protected override string GetSaveDirectoryName()
		{
			return "Data";
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x00011AF5 File Offset: 0x0000FCF5
		protected override string GetSaveFileName()
		{
			return "History.json";
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x00011AFC File Offset: 0x0000FCFC
		protected override void OnBeforeRemoveEntry(MatchHistoryData item, out bool canRemoveEntry)
		{
			base.OnBeforeRemoveEntry(item, out canRemoveEntry);
			this._matchesToRemove.Remove(item);
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x00011B14 File Offset: 0x0000FD14
		protected override void OnBeforeAddEntry(MatchHistoryData item, out bool canAddEntry)
		{
			bool flag;
			base.OnBeforeAddEntry(item, out flag);
			MBReadOnlyList<MatchHistoryData> entries = base.GetEntries();
			bool flag2 = false;
			for (int i = 0; i < entries.Count; i++)
			{
				if (entries[i].MatchId == item.MatchId)
				{
					MatchHistoryDataContainer.PrintDebugLog("Found existing match with id trying to replace: " + entries[i].MatchId);
					base.RemoveEntry(entries[i]);
					this._matchesToRemove.Add(entries[i]);
					base.InsertEntry(item, i);
					MatchHistoryDataContainer.PrintDebugLog("Replaced existing match: (" + entries[i].MatchId + ") with: " + item.MatchId);
					flag2 = true;
					break;
				}
			}
			if (!flag2)
			{
				int num = this.GetEntryCountOfMatchType(item.MatchType) + 1 - 10;
				if (num > 0)
				{
					MatchHistoryDataContainer.PrintDebugLog(string.Format("Max match count is reached, removing ({0}) matches with type: {1}", num, item.MatchType));
					List<MatchHistoryData> oldestMatches = this.GetOldestMatches(item.MatchType, num);
					for (int j = 0; j < oldestMatches.Count; j++)
					{
						if (!this._matchesToRemove.Contains(oldestMatches[j]))
						{
							base.RemoveEntry(oldestMatches[j]);
							this._matchesToRemove.Add(oldestMatches[j]);
						}
					}
				}
			}
			canAddEntry = !flag2;
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x00011C70 File Offset: 0x0000FE70
		protected override List<MatchHistoryData> DeserializeInCompatibilityMode(string serializedJson)
		{
			List<MatchHistoryData> list = new List<MatchHistoryData>();
			try
			{
				MBList<MatchHistoryData> mblist = JsonConvert.DeserializeObject<MBList<MatchHistoryData>>(serializedJson);
				for (int i = 0; i < mblist.Count; i++)
				{
					list.Add(mblist[i]);
				}
			}
			catch
			{
				Debug.FailedAssert("Failed to resolve match history in compatibility mode. Resetting the file.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\Lobby\\LocalData\\MatchHistoryDataContainer.cs", "DeserializeInCompatibilityMode", 228);
			}
			return list;
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x00011CD8 File Offset: 0x0000FED8
		public bool TryGetHistoryData(string matchId, out MatchHistoryData historyData)
		{
			historyData = null;
			MBReadOnlyList<MatchHistoryData> entries = base.GetEntries();
			for (int i = 0; i < entries.Count; i++)
			{
				if (entries[i].MatchId == matchId)
				{
					historyData = entries[i];
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x00011D20 File Offset: 0x0000FF20
		private List<MatchHistoryData> GetOldestMatches(string matchType, int count = 1)
		{
			DateTime maxValue = DateTime.MaxValue;
			List<MatchHistoryData> list = new List<MatchHistoryData>();
			MBReadOnlyList<MatchHistoryData> entries = base.GetEntries();
			from e in entries
			orderby e.MatchDate
			select e;
			int num = 0;
			foreach (MatchHistoryData matchHistoryData in entries)
			{
				if (matchHistoryData == null)
				{
					Debug.FailedAssert("Trying to remove null match history data", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\Lobby\\LocalData\\MatchHistoryDataContainer.cs", "GetOldestMatches", 267);
				}
				else
				{
					if (matchHistoryData.MatchType == matchType)
					{
						list.Add(matchHistoryData);
						num++;
					}
					if (num == count)
					{
						break;
					}
				}
			}
			return list;
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x00011DE0 File Offset: 0x0000FFE0
		private int GetEntryCountOfMatchType(string matchType)
		{
			int num = 0;
			using (List<MatchHistoryData>.Enumerator enumerator = base.GetEntries().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.MatchType == matchType)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x00011E40 File Offset: 0x00010040
		private static void PrintDebugLog(string text)
		{
			Debug.Print("[MATCH_HISTORY]: " + text, 0, Debug.DebugColor.Yellow, 17592186044416UL);
		}

		// Token: 0x0400051E RID: 1310
		private const int MaxMatchCountPerMatchType = 10;

		// Token: 0x0400051F RID: 1311
		private List<MatchHistoryData> _matchesToRemove;
	}
}
